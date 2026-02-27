using NUnit.Framework;
using NUnit.Framework.Internal;

[TestFixture]
public class TestClass : BaseTest
{
    PaginationTest? _paginationTest;

    [Test]
    public void Test001()
    {
        _paginationTest = new(driver, 8);

        _paginationTest.Navigate();
        _paginationTest.Test();
        _paginationTest.Validate();
    }
}