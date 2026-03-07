using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class ProgressBar
{
    private IWebDriver _driver;
    private WebDriverWait _wait;

    public ProgressBar(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
    }

    public void Navigate()
    {
        _driver.Navigate().GoToUrl("https://demoqa.com/progress-bar");
    }

    public void Test()
    {
        var progressBar = _driver.FindElement(By.ClassName("progress-bar"));
        var startStopButton = _driver.FindElement(By.Id("startStopButton"));

        Assert.That(progressBar.GetAttribute("aria-valuenow"), Is.EqualTo("0"));
        TestContext.WriteLine("TASK_COMPLETED:: Progress bar starts at 0%");

        startStopButton.Click();
        _wait.Until(d => int.Parse(
            d.FindElement(By.CssSelector(".progress-bar")).GetAttribute("aria-valuenow")) == 100);

        Assert.That(progressBar.GetAttribute("aria-valuenow"), Is.EqualTo("100"));
        TestContext.WriteLine("TASK_COMPLETED:: Progress bar reached 100%");

        var resetButton = _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("resetButton")));
        resetButton.Click();
        _wait.Until(d => d.FindElement(By.CssSelector(".progress-bar")).GetAttribute("aria-valuenow") == "0");
        Assert.That(progressBar.GetAttribute("aria-valuenow"), Is.EqualTo("0"));
        TestContext.WriteLine("TASK_COMPLETED:: Progress bar reset to 0%");
    }
}