using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class PostOrder
{
    private IWebDriver _driver;
    private TestDataModel _data;
    private WebDriverWait _wait;

    public PostOrder(IWebDriver driver, TestDataModel data)
    {
        _driver = driver;
        _data = data;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    }

    public void Test()
    {
        VerifyOrderHistory();
        Logout();
    }

    private void VerifyOrderHistory()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".order-details-page")));
        string orderDetails = _driver.FindElement(By.CssSelector(".order-details-page")).Text;

        Assert.That(orderDetails, Does.Contain("Fiction"));
        Assert.That(orderDetails, Does.Contain("Build your own cheap computer"));
        Assert.That(orderDetails, Does.Contain(_data.Address));
        Console.WriteLine("TASK_COMPLETED:: Order details verified — products and address match");
    }

    private void Logout()
    {
        _wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("a[href='/logout']"))).Click();

        _wait.Until(ExpectedConditions.UrlToBe("https://demowebshop.tricentis.com/"));
        Assert.That(_driver.Url, Is.EqualTo("https://demowebshop.tricentis.com/"));

        var loginLink = _driver.FindElement(By.CssSelector("a[href='/login']"));
        Assert.That(loginLink.Displayed, Is.True);
        Console.WriteLine("TASK_COMPLETED:: Logged out successfully — redirected to homepage");
    }
}