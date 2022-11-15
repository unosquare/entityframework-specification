using FluentAssertions;
using Unosquare.EntityFramework.Specification.Common.Extensions;
using Unosquare.EntityFramework.Specification.Common.Primitive;
using Unosquare.EntityFramework.Specification.Tests.TestModels;
using Xunit;

namespace Unosquare.EntityFramework.Specification.Tests;

public class CollectionExtensionsTests
{
    [Fact]
    public void Where_WhenApplicableSpecificationSupplied_ShouldCorrectlyFilterItems()
    {
        // Arrange
        var specification = RetrieveSpecificationForId();
        var items = RetrieveTestItems()
            .AsQueryable();

        // Act
        var results = items
            .Where(specification);

        // Assert
        results
            .Should()
            .Contain(x => x.Id == "1")
            .And
            .HaveCount(1);
    }

    [Fact]
    public void Where_WhenApplicableSpecificationSuppliedForEnumerable_ShouldCorrectlyFilterItems()
    {
        // Arrange
        var specification = RetrieveSpecificationForId();
        var items = RetrieveTestItems();

        // Act
        var results = items
            .Where(specification);

        // Assert
        results
            .Should()
            .Contain(x => x.Id == "1")
            .And
            .HaveCount(1);
    }

    [Fact]
    public void Where_WhenApplicableSpecificationSuppliedWithSelector_ShouldCorrectlyFilterItems()
    {
        // Arrange
        var specification = RetrieveSpecificationForActiveSubItem();
        var items = RetrieveTestItems()
            .AsQueryable();

        // Act
        var results = items
            .Where(specification, new SubItemSelector());

        // Assert
        results
            .Should()
            .Contain(x => x.Id == "1")
            .And
            .HaveCount(1);
    }

    [Fact]
    public void Where_WhenApplicableSpecificationSuppliedWithSelectorForEnumerable_ShouldCorrectlyFilterItems()
    {
        // Arrange
        var specification = RetrieveSpecificationForActiveSubItem();
        var items = RetrieveTestItems();

        // Act
        var results = items
            .Where(specification, new SubItemSelector());

        // Assert
        results
            .Should()
            .Contain(x => x.Id == "1")
            .And
            .HaveCount(1);
    }
    
    
    [Fact]
    public void Where_WithResolveEmbedded_LimitsCorrectOutput()
    {
        // Arrange
        var items = RetrieveTestItemsWithItemsWithin()
            .AsQueryable();

        // Act
        var results = items
            .Where(x => x.Any(y => RetrieveSpecificationForActiveSubItem(true).Embed()(y)))
            .ResolveEmbedded();

        // Assert
        results
            .Count()
            .Should().Be(1);
    }

    [Fact]
    public void Where_WhenApplicableSpecificationSuppliedWithSelectorFunction_ShouldCorrectlyFilterItems()
    {
        // Arrange
        var specification = RetrieveSpecificationForActiveSubItem();
        var items = RetrieveTestItems()
            .AsQueryable();

        // Act
        var results = items
            .Where(specification, x => x.SubItem);

        // Assert
        results
            .Should()
            .Contain(x => x.Id == "1")
            .And
            .HaveCount(1);
    }

    [Fact]
    public void Where_WhenApplicableSpecificationSuppliedWithSelectorFunctionForEnumerable_ShouldCorrectlyFilterItems()
    {
        // Arrange
        var specification = RetrieveSpecificationForActiveSubItem();
        var items = RetrieveTestItems();

        // Act
        var results = items
            .Where(specification, x => x.SubItem);

        // Assert
        results
            .Should()
            .Contain(x => x.Id == "1")
            .And
            .HaveCount(1);
    }
    
    [Fact]
    public void Count_WhenApplicableSpecificationSupplied_ShouldCorrectlyCountResults()
    {
        // Arrange
        var specification = RetrieveSpecificationForId();
        var items = RetrieveTestItems()
            .AsQueryable();

        // Act
        var results = items
            .Count(specification);

        // Assert
        results
            .Should()
            .Be(1);
    }

    [Fact]
    public void Count_WhenApplicableSpecificationSuppliedForEnumerable_ShouldCorrectlyCountResults()
    {
        // Arrange
        var specification = RetrieveSpecificationForId();
        var items = RetrieveTestItems();

        // Act
        var results = items
            .Count(specification);

        // Assert
        results
            .Should()
            .Be(1);
    }

    [Fact]
    public void Count_WhenApplicableSpecificationSuppliedWithSelector_ShouldCorrectlyCountResults()
    {
        // Arrange
        var specification = RetrieveSpecificationForActiveSubItem();
        var items = RetrieveTestItems()
            .AsQueryable();

        // Act
        var results = items
            .Count(specification, new SubItemSelector());

        // Assert
        results
            .Should()
            .Be(1);
    }

    [Fact]
    public void Count_WhenApplicableSpecificationSuppliedWithSelectorForEnumerable_ShouldCorrectlyCountResults()
    {
        // Arrange
        var specification = RetrieveSpecificationForActiveSubItem();
        var items = RetrieveTestItems();

        // Act
        var results = items
            .Count(specification, new SubItemSelector());

        // Assert
        results
            .Should()
            .Be(1);
    }

    [Fact]
    public void Count_WhenApplicableSpecificationSuppliedWithSelectorFunction_ShouldCorrectlyCountResults()
    {
        // Arrange
        var specification = RetrieveSpecificationForActiveSubItem();
        var items = RetrieveTestItems()
            .AsQueryable();

        // Act
        var results = items
            .Count(specification, x => x.SubItem);

        // Assert
        results
            .Should()
            .Be(1);
    }

    [Fact]
    public void Count_WhenApplicableSpecificationSuppliedWithSelectorFunctionForEnumerable_ShouldCorrectlyCountResults()
    {
        // Arrange
        var specification = RetrieveSpecificationForActiveSubItem();
        var items = RetrieveTestItems();

        // Act
        var results = items
            .Count(specification, x => x.SubItem);

        // Assert
        results
            .Should()
            .Be(1);
    }

    [Fact]
    public void Any_WhenApplicableSpecificationSupplied_ShouldIdentifyPresence()
    {
        // Arrange
        var specification = RetrieveSpecificationForId();
        var items = RetrieveTestItems()
            .AsQueryable();

        // Act
        var results = items
            .Any(specification);

        // Assert
        results
            .Should()
            .BeTrue();
    }

    [Fact]
    public void Any_WhenApplicableSpecificationSuppliedForEnumerable_ShouldIdentifyPresence()
    {
        // Arrange
        var specification = RetrieveSpecificationForId();
        var items = RetrieveTestItems();

        // Act
        var results = items
            .Any(specification);

        // Assert
        results
            .Should()
            .BeTrue();
    }

    [Fact]
    public void Any_WhenApplicableSpecificationSuppliedWithSelector_ShouldIdentifyPresence()
    {
        // Arrange
        var specification = RetrieveSpecificationForActiveSubItem();
        var items = RetrieveTestItems()
            .AsQueryable();

        // Act
        var results = items
            .Any(specification, new SubItemSelector());

        // Assert
        results
            .Should()
            .BeTrue();
    }

    [Fact]
    public void Any_WhenApplicableSpecificationSuppliedWithSelectorForEnumerable_ShouldIdentifyPresence()
    {
        // Arrange
        var specification = RetrieveSpecificationForActiveSubItem();
        var items = RetrieveTestItems();

        // Act
        var results = items
            .Any(specification, new SubItemSelector());

        // Assert
        results
            .Should()
            .BeTrue();
    }

    [Fact]
    public void Any_WhenApplicableSpecificationSuppliedWithSelectorFunction_ShouldIdentifyPresence()
    {
        // Arrange
        var specification = RetrieveSpecificationForActiveSubItem();
        var items = RetrieveTestItems()
            .AsQueryable();

        // Act
        var results = items
            .Any(specification, x => x.SubItem);

        // Assert
        results
            .Should()
            .BeTrue();
    }

    [Fact]
    public void Any_WhenApplicableSpecificationSuppliedWithSelectorFunctionForEnumerable_ShouldIdentifyPresence()
    {
        // Arrange
        var specification = RetrieveSpecificationForActiveSubItem();
        var items = RetrieveTestItems();

        // Act
        var results = items
            .Any(specification, x => x.SubItem);

        // Assert
        results
            .Should()
            .BeTrue();
    }

    [Fact]
    public void FirstOrDefault_WhenApplicableSpecificationSupplied_ShouldIdentifyPresence()
    {
        // Arrange
        var specification = RetrieveSpecificationForId();
        var items = RetrieveTestItems()
            .AsQueryable();

        // Act
        var results = items
            .FirstOrDefault(specification);

        // Assert
        results
            .Should()
            .Be(items.ElementAt(0));
    }

    [Fact]
    public void FirstOrDefault_WhenApplicableSpecificationSuppliedForEnumerable_ShouldIdentifyPresence()
    {
        // Arrange
        var specification = RetrieveSpecificationForId();
        var items = RetrieveTestItems();

        // Act
        var results = items
            .FirstOrDefault(specification);

        // Assert
        results
            .Should()
            .Be(items.ElementAt(0));
    }

    [Fact]
    public void FirstOrDefault_WhenApplicableSpecificationSuppliedWithSelector_ShouldIdentifyPresence()
    {
        // Arrange
        var specification = RetrieveSpecificationForActiveSubItem();
        var items = RetrieveTestItems()
            .AsQueryable();

        // Act
        var results = items
            .FirstOrDefault(specification, new SubItemSelector());

        // Assert
        results
            .Should()
            .Be(items.ElementAt(0));
    }

    [Fact]
    public void FirstOrDefault_WhenApplicableSpecificationSuppliedWithSelectorForEnumerable_ShouldIdentifyPresence()
    {
        // Arrange
        var specification = RetrieveSpecificationForActiveSubItem();
        var items = RetrieveTestItems();

        // Act
        var results = items
            .FirstOrDefault(specification, new SubItemSelector());

        // Assert
        results
            .Should()
            .Be(items.ElementAt(0));
    }

    [Fact]
    public void FirstOrDefault_WhenApplicableSpecificationSuppliedWithSelectorFunction_ShouldIdentifyPresence()
    {
        // Arrange
        var specification = RetrieveSpecificationForActiveSubItem();
        var items = RetrieveTestItems()
            .AsQueryable();

        // Act
        var results = items
            .FirstOrDefault(specification, x => x.SubItem);

        // Assert
        results
            .Should()
            .Be(items.ElementAt(0));
    }

    [Fact]
    public void
        FirstOrDefault_WhenApplicableSpecificationSuppliedWithSelectorFunctionForEnumerable_ShouldIdentifyPresence()
    {
        // Arrange
        var specification = RetrieveSpecificationForActiveSubItem();
        var items = RetrieveTestItems();

        // Act
        var results = items
            .FirstOrDefault(specification, x => x.SubItem);

        // Assert
        results
            .Should()
            .Be(items.ElementAt(0));
    }

    [Fact]
    public void Select_WhenApplicableSelectorSupplied_ShouldSelectCorrectOutput()
    {
        // Arrange
        var selector = new SubItemSelector();
        var items = RetrieveTestItems()
            .AsQueryable();

        // Act
        var results = items
            .Where(RetrieveSpecificationForActiveSubItem(), x => x.SubItem)
            .Select(selector);

        // Assert
        results
            .Should()
            .Contain(items.ElementAt(0).SubItem);
    }
    
    [Fact]
    public void Select_WithResolveEmbedded_ShouldSelectCorrectOutput()
    {
        // Arrange
        var items = RetrieveTestItemsWithItemsWithin()
            .AsQueryable();

        // Act
        var results = items
            .Select(x => new
            {
                Items = x.Where(y => RetrieveSpecificationForActiveSubItem(false).Embed()(y))
            })
            .ResolveEmbedded();

        // Assert
        results
            .SelectMany(x => x.Items)
            .Count()
            .Should().Be(1);
    }

    [Fact]
    public void Select_WhenApplicableSelectorSuppliedForEnumerable_ShouldSelectCorrectOutput()
    {
        // Arrange
        var selector = new SubItemSelector();
        var items = RetrieveTestItems();

        // Act
        var results = items
            .Where(RetrieveSpecificationForActiveSubItem(), x => x.SubItem)
            .Select(selector);

        // Assert
        results
            .Should()
            .Contain(items.ElementAt(0).SubItem);
    }

    [Fact]
    public void Select_WhenApplicableSelectorSuppliedWithAdditionalSelectorFunction_ShouldSelectCorrectOutput()
    {
        // Arrange
        var selector = new SubItemSelector();
        var items = RetrieveTestItems()
            .AsQueryable();

        // Act
        var results = items
            .Where(RetrieveSpecificationForActiveSubItem(), selector)
            .Select(RetrieveSelectorForActive(), x => x.SubItem);

        // Assert
        results
            .Should()
            .ContainInOrder(true);
    }

    [Fact]
    public void Select_WhenApplicableSelectorSuppliedWithAdditionalSelector_ShouldSelectCorrectOutput()
    {
        // Arrange
        var selector = new SubItemSelector();
        var items = RetrieveTestItems()
            .AsQueryable();

        // Act
        var results = items
            .Where(RetrieveSpecificationForActiveSubItem(), selector)
            .Select(RetrieveSelectorForActive(), selector);

        // Assert
        results
            .Should()
            .ContainInOrder(true);
    }

    private static Specification<ItemForTest> RetrieveSpecificationForId(string id = "1") =>
        new ExpressionSpec<ItemForTest>(x => x.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));

    private static Specification<SubItemForTest> RetrieveSpecificationForActiveSubItem(bool active = true) =>
        new ExpressionSpec<SubItemForTest>(x => x != null && x.Active == active);

    private static Selector<SubItemForTest, bool> RetrieveSelectorForActive() =>
        new ExpressionSelector<SubItemForTest, bool>(x => x.Active);

    private static IList<ItemForTest> RetrieveTestItems() =>
        new[] { new ItemForTest("1", new(true)), new ItemForTest("2"), new ItemForTest("3") };

    private static IList<IList<SubItemForTest>> RetrieveTestItemsWithItemsWithin() =>
        new List<IList<SubItemForTest>>()
        {
            new List<SubItemForTest> { new SubItemForTest(true), new SubItemForTest(false) }
        };
}