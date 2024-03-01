namespace SimpleResults.Tests.Core;

public class PagedInfoTests
{
    [TestCase(1)]
    [TestCase(0)]
    public void HasPrevious_WhenThereIsNoPreviousPage_ShouldReturnsFalse(int pageNumber)
    {
        // Arrange
        var pagedInfo = new PagedInfo(pageNumber, pageSize: 1, totalRecords: 10);

        // Act
        bool actual = pagedInfo.HasPrevious;

        // Assert
        actual.Should().BeFalse();
    }

    [TestCase(2)]
    [TestCase(3)]
    public void HasPrevious_WhenPreviousPageExists_ShouldReturnsTrue(int pageNumber) 
    {
        // Arrange
        var pagedInfo = new PagedInfo(pageNumber, pageSize: 1, totalRecords: 10);

        // Act
        bool actual = pagedInfo.HasPrevious;

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void HasNext_WhenThereIsNoNextPage_ShouldReturnsFalse()
    {
        // Arrange
        var pagedInfo = new PagedInfo(pageNumber: 2, pageSize: 5, totalRecords: 10);

        // Act
        bool actual = pagedInfo.HasNext;

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void HasNext_WhenNextPageExists_ShouldReturnsTrue()
    {
        // Arrange
        var pagedInfo = new PagedInfo(pageNumber: 1, pageSize: 5, totalRecords: 10);

        // Act
        bool actual = pagedInfo.HasNext;

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void Constructor_WhenPageSizeIsZero_ShouldThrowDivideByZeroException()
    {
        // Arrange
        int pageSize = 0;
        var expectedMessage = new PagedInfo.DivideByZeroError(nameof(pageSize)).Message;

        // Act
        Action act = () =>
        {
            PagedInfo pagedInfo = new (pageNumber: 1, pageSize, totalRecords: 10);
        };

        // Assert
        act.Should()
           .Throw<DivideByZeroException>()
           .WithMessage(expectedMessage);
    }

    [TestCase(200, 33, 7)]
    [TestCase(100, 11, 10)]
    [TestCase(0,   3,  0)]
    [TestCase(200, 10, 20)]
    [TestCase(10,  3,  4)]
    public void Constructor_WhenTotalPagesIsObtained_ShouldBeDivisionBetweenTotalRecordsPerPageSize(
        int totalRecords,
        int pageSize,
        int expectedTotalPages)
    {
        // Arrange
        var pagedInfo = new PagedInfo(pageNumber: 1, pageSize, totalRecords);

        // Act
        int actual = pagedInfo.TotalPages;

        // Assert
        actual.Should().Be(expectedTotalPages);
    }
}
