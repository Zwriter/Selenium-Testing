using NUnit.Framework;

[TestFixture]
public class TestClass : BaseTest
{
    private UserCreation? _registerUser;
    private UserLogIn? _userLogIn;
    private ProductBrowsing? _productBrowsing;
    private ShoppingCart? _shoppingChartVerifications;
    private Checkout? _checkout;
    private PostOrder? _postOrderVerif;

    [Test, Explicit("Run only when registering a new account")]
    public void RegisterTest()
    {
        _registerUser = new UserCreation(driver, testData!.GenderEnum,
            testData.FirstName, testData.LastName, testData.Email, testData.Password);
        _registerUser.Navigate();
        _registerUser.Test();
    }

    [Test, Order(1)]
    public void LoginTest()
    {
        new PreConditions(driver).Verify();
        Console.WriteLine("========================================");

        _userLogIn = new UserLogIn(driver, testData!.Email, testData.Password);
        _userLogIn.Navigate();
        _userLogIn.Test();
    }

    [Test, Order(2)]
    public void BrowsingTest()
    {
        Console.WriteLine("========================================");
        _productBrowsing = new(driver);
        _productBrowsing.Test();
    }

    [Test, Order(3)]
    public void CartTest()
    {
        Console.WriteLine("========================================");
        _shoppingChartVerifications = new(driver);
        _shoppingChartVerifications.Navigate();
        _shoppingChartVerifications.Verify();
    }

    [Test, Order(4)]
    public void CheckoutTest()
    {
        Console.WriteLine("========================================");
        _checkout = new(driver, testData!);
        _checkout.Test();
    }

    [Test, Order(5)]
    public void PostOrderTest()
    {
        Console.WriteLine("========================================");
        _postOrderVerif = new(driver, testData!);
        _postOrderVerif.Test();

        Console.WriteLine("========================================");
        new PostConditions(driver).Verify();
    }
}

// using NUnit.Framework;
// using NUnit.Framework.Internal;

// [TestFixture]
// public class TestClass : BaseTest
// {
//     private UserCreation? _registerUser;
//     private UserLogIn? _userLogIn;
//     private ProductBrowsing? _productBrowsing;
//     private ShoppingCart? _shoppingChartVerifications;
//     private Checkout? _checkout;
//     private PostOrder? _postOrderVerif;

//     [Test]
//     public void RegisterTest()
//     {
//         _registerUser = new UserCreation(
//             driver,
//             testData!.GenderEnum,
//             testData.FirstName,
//             testData.LastName,
//             testData.Email,
//             testData.Password
//         );

//         _registerUser.Navigate();
//         _registerUser.Test();
//     }

//     [Test]
//     public void LoginTest()
//     {
//         _userLogIn = new UserLogIn(
//             driver,
//             testData!.Email,
//             testData.Password
//         );

//         _userLogIn.Navigate();
//         _userLogIn.Test();
//     }

//     [Test]
//     public void BrowsingTest()
//     {
//         LoginTest();

//         Console.WriteLine("========================================");
//         _productBrowsing = new(driver);
//         _productBrowsing.Test();
//     }

//     [Test]
//     public void CartTest()
//     {
//         BrowsingTest();

//         Console.WriteLine("========================================");
//         _shoppingChartVerifications = new(driver);
//         _shoppingChartVerifications.Navigate();
//         _shoppingChartVerifications.Verify();
//     }

//     [Test]
//     public void CheckoutTest()
//     {
//         CartTest();

//         Console.WriteLine("========================================");
//         _checkout = new(driver, testData);
//         _checkout.Test();
//     }

//     [Test]
//     public void PostOrderTest()
//     {
//         CheckoutTest();

//         Console.WriteLine("========================================");
//         _postOrderVerif = new(driver, testData);
//         _postOrderVerif.Test();
//     }
// }