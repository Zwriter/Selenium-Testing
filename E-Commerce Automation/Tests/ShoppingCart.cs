using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class ShoppingCart
{
    private IWebDriver _driver;
    private WebDriverWait _wait;

    public ShoppingCart(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    }

    public void Navigate()
    {
        _driver.FindElement(By.CssSelector("a[href='/cart']")).Click();
    }

    public void Verify()
    {
        var cartItems = _driver.FindElements(By.CssSelector(".cart-item-row"));
        Assert.That(cartItems.Count, Is.EqualTo(3));
        Console.WriteLine($"TASK_COMPLETED:: Cart contains {cartItems.Count} items");

        string cartContent = _driver.FindElement(By.CssSelector(".cart")).Text;
        Assert.That(cartContent, Does.Contain("Fiction"));
        Assert.That(cartContent, Does.Contain("Build your own cheap computer"));
        Assert.That(cartContent, Does.Contain("Blue Jeans"));
        Console.WriteLine("TASK_COMPLETED:: All expected items are present in cart");

        var blueJeansRow = cartItems.First(row => row.Text.Contains("Blue Jeans"));
        string blueJeansQty = blueJeansRow.FindElement(By.CssSelector(".qty-input")).GetAttribute("value");
        Assert.That(blueJeansQty, Is.EqualTo("2"), "Blue Jeans quantity should be 2");
        Console.WriteLine("TASK_COMPLETED:: Blue Jeans quantity is 2");

        var unitPrices = cartItems.Select(row =>
        {
            string priceText = row.FindElement(By.ClassName("product-unit-price")).Text.Replace("$", "").Trim();
            string qtyText = row.FindElement(By.ClassName("qty-input")).GetAttribute("value")?.Trim();

            Console.WriteLine($"DEBUG — price: '{priceText}', qty: '{qtyText}'");

            if (string.IsNullOrEmpty(priceText) || string.IsNullOrEmpty(qtyText))
                return 0;

            double unitPrice = double.Parse(priceText);
            double qty = double.Parse(qtyText);
            return unitPrice * qty;
        }).Sum();

        string totalText = _driver.FindElement(
            By.CssSelector(".product-price.order-total strong")).Text;
        double actualTotal = double.Parse(totalText.Replace("$", ""));
        Assert.That(actualTotal, Is.EqualTo(unitPrices).Within(0.01));
        Console.WriteLine($"TASK_COMPLETED:: Cart total ${actualTotal} matches sum of item prices");

        var blueJeansQtyInput = blueJeansRow.FindElement(By.CssSelector(".qty-input"));
        blueJeansQtyInput.Clear();
        blueJeansQtyInput.SendKeys("0");
        _driver.FindElement(By.Name("updatecart")).Click();

        cartItems = _driver.FindElements(By.CssSelector(".cart-item-row"));
        Assert.That(cartItems.Count, Is.EqualTo(2));
        Console.WriteLine("TASK_COMPLETED:: Blue Jeans removed from cart");

        _driver.FindElement(By.Id("termsofservice")).Click();
        _driver.FindElement(By.Id("checkout")).Click();
        Console.WriteLine("TASK_COMPLETED:: Proceeding to checkout");
    }
}