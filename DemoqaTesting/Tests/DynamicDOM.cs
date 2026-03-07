using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class DynamicDOM
{
    private IWebDriver _driver;
    private WebDriverWait _wait;

    public DynamicDOM(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    }

    public void Navigate()
    {
        _driver.Navigate().GoToUrl("https://demoqa.com/dynamic-properties");
    }

    public void Test()
    {
        _wait.Until(d => d.FindElement(By.Id("colorChange")).GetAttribute("class").Contains("text-danger"));
        var colorChangeBtn = _driver.FindElement(By.Id("colorChange"));
        Assert.That(colorChangeBtn.GetAttribute("class"), Does.Contain("text-danger"));
        TestContext.WriteLine("TASK_COMPLETED:: Color change button has text-danger class");

        var enableAfterBtn = _driver.FindElement(By.Id("enableAfter"));
        _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("enableAfter")));
        Assert.That(enableAfterBtn.Enabled, Is.True);
        TestContext.WriteLine("TASK_COMPLETED:: Enable-after button is now enabled");

        _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("visibleAfter")));
        Assert.That(_driver.FindElement(By.Id("visibleAfter")).Displayed, Is.True);
        TestContext.WriteLine("TASK_COMPLETED:: Visible-after button is now visible");
    }
}