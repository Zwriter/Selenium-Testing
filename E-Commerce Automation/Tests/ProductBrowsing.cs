using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class ProductBrowsing
{
    private IWebDriver _driver;
    private WebDriverWait _wait;

    public ProductBrowsing(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    }

    public void Test()
    {
        AddFictionBook();
        AddSimpleComputer();
        AddBlueJeans();
    }

    private void AddFictionBook()
    {
        var booksMenu = _wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("a[href='/books']")));
        booksMenu.Click();

        // Sort by Price: High to Low
        var products = _driver.FindElements(By.CssSelector(".product-item"));

        var sorted = products
            .Select(p => new
            {
                element = p,
                name = p.FindElement(By.CssSelector(".product-title a")).Text,
                price = double.Parse(p.FindElement(By.CssSelector(".price.actual-price")).Text.Replace("$", ""))
            })
            .OrderByDescending(p => p.price)
            .ToList();


        var fiction = sorted.First(p => p.name.Contains("Fiction"));
        fiction.element.FindElement(By.CssSelector(".product-title a")).Click();
        Console.WriteLine("TASK_COMPLETED:: Found element afther sorting");

        _driver.FindElement(By.Id("add-to-cart-button-45")).Click();
        _wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("#bar-notification.success")));
        Console.WriteLine("TASK_COMPLETED:: Fiction book added to cart");
    }

    private void AddSimpleComputer()
    {
        var computersMenu = _wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("a[href='/computers']")));
        Actions actions = new Actions(_driver);
        actions.MoveToElement(computersMenu).Perform();

        _wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("a[href='/desktops']"))).Click();

        /// Filter by price under 1000 and pick the first element that pops up
        var desktops = _driver.FindElements(By.CssSelector(".product-item"));
        var underBudget = desktops
            .Select(p => new
            {
                element = p,
                name = p.FindElement(By.CssSelector(".product-title a")).Text,
                price = double.Parse(p.FindElement(By.CssSelector(".price.actual-price")).Text.Replace("$", ""))
            })
            .Where(p => p.price < 1000)
            .ToList();

        Assert.That(underBudget.Count, Is.GreaterThan(0));
        underBudget[0].element.FindElement(By.CssSelector(".product-title a")).Click();
        Console.WriteLine("TASK_COMPLETED:: Found element afther filtering");

        _driver.FindElement(By.Id("product_attribute_72_5_18_65")).Click();
        _driver.FindElement(By.Id("product_attribute_72_6_19_91")).Click();
        _driver.FindElement(By.Id("product_attribute_72_3_20_58")).Click();

        _driver.FindElement(By.Id("product_attribute_72_8_30_93")).Click();
        _driver.FindElement(By.Id("product_attribute_72_8_30_94")).Click();

        _driver.FindElement(By.Id("add-to-cart-button-72")).Click();
        _wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("#bar-notification.success")));
        Console.WriteLine("TASK_COMPLETED:: Build your own cheap computer added to cart");
    }

    private void AddBlueJeans()
    {
        var apparelMenu = _wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("a[href='/apparel-shoes']")));
        apparelMenu.Click();

        _wait.Until(ExpectedConditions.ElementToBeClickable(
            By.LinkText("Blue Jeans"))).Click();

        var quantity = _driver.FindElement(By.ClassName("qty-input"));
        quantity.Clear();
        quantity.SendKeys("2");

        _driver.FindElement(By.Id("add-to-cart-button-36")).Click();
        _wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("#bar-notification.success")));
        Console.WriteLine("TASK_COMPLETED:: Blue Jeans with quantity 2 added to cart");
    }
}