using System;
using System.Linq;
using FluentAssertions;
using Unosquare.EntityFramework.Specification.Common.Primitive;
using Unosquare.EntityFramework.Specification.Common.Tests.TestModels;
using Unosquare.EntityFramework.Specification.Common.Extensions;
using Xunit;

namespace Unosquare.EntityFramework.Specification.Tests
{
    public class CollectionExtensionsTests
    {
        [Fact]
        public void Where_WhenApplicableSpecificationSupplied_ShouldCorrectlyFilterItems()
        {
            // Arrange
            var specification = new ExpressionSpec<ItemForTest>(x => x.Id.Equals("1", StringComparison.InvariantCultureIgnoreCase));
            var items = new[] { new ItemForTest("1"), new ItemForTest("2"), new ItemForTest("3") }.AsQueryable();
            
            // Act
            var results = items.Where(specification);

            // Assert
            results
                .Should()
                .Contain(x => x.Id == "1")
                .And
                .HaveCount(1);
        }
    }
}
