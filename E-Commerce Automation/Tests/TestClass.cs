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
    private PostOrder? _postOrderVerif;

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

        Console.WriteLine("========================================");
        _productBrowsing = new(driver);
        _productBrowsing.Test();
    }

    [Test]
    public void CartTest()
    {
        BrowsingTest();

        Console.WriteLine("========================================");
        _shoppingChartVerifications = new(driver);
        _shoppingChartVerifications.Navigate();
        _shoppingChartVerifications.Verify();
    }

    [Test]
    public void CheckoutTest()
    {
        CartTest();

        Console.WriteLine("========================================");
        _checkout = new(driver);
        _checkout.Test();
    }

    [Test]
    public void PostOrderTest()
    {
        CheckoutTest();

        Console.WriteLine("========================================");
        _postOrderVerif = new(driver);
        _postOrderVerif.Test();
    }
}