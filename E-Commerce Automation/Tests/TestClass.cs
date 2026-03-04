using NUnit.Framework;
using NUnit.Framework.Internal;

[TestFixture]
public class TestClass : BaseTest
{
    private UserCreation? _registerUser;
    private UserLogIn? _userLogIn;
    private ProductBrowsing? _productBrowsing;
    private ShoppingCart? _shoppingChartVerifications;
    private Checkout? _checkout;

    [Test]
    public void RegisterTest()
    {
        _registerUser = new UserCreation(
            driver,
            UserData.GENDER,
            UserData.FIRST_NAME,
            UserData.LAST_NAME,
            UserData.EMAIL,
            UserData.PASSWORD
        );

        _registerUser.Navigate();
        _registerUser.Test();
    }

    [Test]
    public void LoginTest()
    {
        _userLogIn = new UserLogIn(
            driver,
            UserData.EMAIL,
            UserData.PASSWORD
        );

        _userLogIn.Navigate();
        _userLogIn.Test();
    }

    [Test]
    public void BrowsingTest()
    {
        LoginTest();

        _productBrowsing = new(driver);
        _shoppingChartVerifications = new(driver);
        _checkout = new(driver);

        _productBrowsing.Test();

        Console.WriteLine("========================================");

        _shoppingChartVerifications.Navigate();
        _shoppingChartVerifications.Verify();

        Console.WriteLine("========================================");

        _checkout.Test();

        Console.WriteLine("========================================");
    }
}