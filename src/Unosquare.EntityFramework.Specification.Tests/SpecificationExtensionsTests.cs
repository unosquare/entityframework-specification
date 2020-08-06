using System;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using Unosquare.EntityFramework.Specification.Extensions;
using Unosquare.EntityFramework.Specification.Primitive;
using Xunit;

namespace Unosquare.EntityFramework.Specification.Tests
{
    public class SpecificationExtensionsTests
    {
        [Fact]
        public void And_WhenSpecificationsAreApplicable_ShouldCorrectlyFilterItems()
        {
            // Arrange
            var first = new ExpressionSpec<ItemForTest>(x => x.Id.Contains("1"));
            var second = new ExpressionSpec<ItemForTest>(x => x.Id.Contains("2"));
            var items = new[] { new ItemForTest("11"), new ItemForTest("21"), new ItemForTest("22") };
            var andSpec = first.And(second);

            // Act
            var result = items.Where(x => andSpec.IsSatisfy(x)).ToArray();

            // Assert
            result
                .Should()
                .Contain(x => x.Id == "21")
                .And
                .HaveCount(1);
        }
        
        [Fact]
        public void And_WhenSelectionExpressionApplicableBetweenSpecifications_ShouldCorrectlyFilterItems()
        {
            // Arrange
            var first = new ExpressionSpec<ItemForTest>(x => x.Id.Contains("2"));
            var second = new ExpressionSpec<SubItemForTest>(x => x != null && x.Active);
            var items = new[] { new ItemForTest("11"), new ItemForTest("22"), new ItemForTest("22", new SubItemForTest(true)) };
            var andSpec = first.And(second, x => x.SubItem);

            // Act
            var result = items.Where(x => andSpec.IsSatisfy(x)).ToArray();

            // Assert
            result
                .Should()
                .Contain(x => x.Id == "22")
                .And
                .HaveCount(1);
        }
        
        [Fact]
        public void And_WhenSelectorApplicableBetweenSpecifications_ShouldCorrectlyFilterItems()
        {
            // Arrange
            var first = new ExpressionSpec<ItemForTest>(x => x.Id.Contains("2"));
            var second = new ExpressionSpec<SubItemForTest>(x => x != null && x.Active);
            var items = new[] { new ItemForTest("11"), new ItemForTest("22"), new ItemForTest("22", new SubItemForTest(true)) };
            var andSpec = first.And(second, new SubItemSelector());

            // Act
            var result = items.Where(x => andSpec.IsSatisfy(x)).ToArray();

            // Assert
            result
                .Should()
                .Contain(x => x.Id == "22")
                .And
                .HaveCount(1);
        }
        
        [Fact]
        public void And_WhenShowAllIsApplied_NoItemsAreFiltered()
        {
            // Arrange
            var first = new ExpressionSpec<ItemForTest>();
            var second = new ExpressionSpec<ItemForTest>();
            var items = new[] { new ItemForTest("11"), new ItemForTest("22")};
            var andSpec = first.And(second);

            // Act
            var result = items.Where(x => andSpec.IsSatisfy(x)).ToArray();

            // Assert
            result
                .Should()
                .BeEquivalentTo(items.ToList());
        }

        [Fact]
        public void Not_SpecificationTest()
        {
            // Arrange
            var spec = new ExpressionSpec<ItemForTest>(x => x.Id.Contains("1"));

            var items = new[] { new ItemForTest("11"), new ItemForTest("21"), new ItemForTest("22") };
            var notSpec =spec.Not();

            // Act
            var result = items.Where(x => notSpec.IsSatisfy(x)).ToArray();

            // Assert
            result.Should()
                .Contain(x => x.Id == "22")
                .And
                .HaveCount(1);
        }

        [Fact]
        public void OrSpecificationTest()
        {
            // Arrange
            var first = new ExpressionSpec<ItemForTest>(x => x.Id.Contains("1"));
            var second = new ExpressionSpec<ItemForTest>(x => x.Id.Contains("2"));
            var items = new[] { new ItemForTest("11"), new ItemForTest("22"), new ItemForTest("33"), };
            var orSpec = first.Or(second);

            // Act
            var result = items.Where(x => orSpec.IsSatisfy(x)).ToArray();

            // Assert
            result.Should()
                .NotContain(x => x.Id == "33")
                .And.HaveCount(2);
        }

        [Fact]
        public void OrSpecification_PassShowAllSpec_ShouldIgnoreIt()
        {
            // Arrange
            var first = new ExpressionSpec<ItemForTest>(x => true);
            var second = new ExpressionSpec<ItemForTest>(x => x.Id.Contains("2"));
            var items = new[] { new ItemForTest("11"), new ItemForTest("22"), new ItemForTest("33"), };
            var orSpec = first.Or(second);

            // Act
            var result = items.Where(x => orSpec.IsSatisfy(x)).ToArray();

            // Assert
            result.Should()
                .Contain(x => x.Id == "22")
                .And.HaveCount(1);
        }
    }

    internal class ItemForTest
    {
        public ItemForTest(string id, SubItemForTest subItem = null)
        {
            Id = id;
            SubItem = subItem;
        }
        public string Id { get; }
        
        public SubItemForTest SubItem { get; }
    }
    
    internal class SubItemForTest
    {
        public bool Active { get; }
        
        public SubItemForTest(bool active)
        {
            Active = active;
        }
    }

    internal class SubItemSelector : Selector<ItemForTest, SubItemForTest>
    {
        public override Expression<Func<ItemForTest, SubItemForTest>> BuildExpression() =>
            x => x.SubItem;
    }
}
