using NUnit.Framework;
using NUnit.Framework.Internal;

[TestFixture]
public class TestClass : BaseTest
{
    public UserCreation? _registerUser;
    public UserLogIn? _userLogIn;
    public ProductBrowsing? _productBrowsing;

    [Test]
    public void RegisterTest()
    {
        _registerUser = new UserCreation(
            driver,
            Gender.Male,
            TestData.FIRST_NAME,
            TestData.LAST_NAME,
            TestData.EMAIL,
            TestData.PASSWORD
        );

        _registerUser.Navigate();
        _registerUser.Test();
    }

    [Test]
    public void LoginTest()
    {
        _userLogIn = new UserLogIn(
            driver,
            TestData.EMAIL,
            TestData.PASSWORD
        );

        _userLogIn.Navigate();
        _userLogIn.Test();
    }

    [Test]
    public void BrowsingTest()
    {
        //LoginTest();

        _productBrowsing = new(driver);
        _productBrowsing.Navigate();
    }
}