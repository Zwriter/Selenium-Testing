using NUnit.Framework;
using NUnit.Framework.Internal;

[TestFixture]
public class TestClass : BaseTest
{
    Pagination? _paginationTest;
    ProgressBar? _barTest;
    DynamicDOM? _dynamicTest;

    [Test]
    public void PaginationTest()
    {
        _paginationTest = new(driver, 8);

        _paginationTest.Navigate();
        _paginationTest.Test();
        _paginationTest.Validate();
    }

    [Test]
    public void ProgressBarTest()
    {
        _barTest = new(driver);
        _barTest.Navigate();
        _barTest.Test();
    }

    [Test]
    public void DynamicDOMTest()
    {
        _dynamicTest = new(driver);
        _dynamicTest.Navigate();
        _dynamicTest.Test();
    }
}