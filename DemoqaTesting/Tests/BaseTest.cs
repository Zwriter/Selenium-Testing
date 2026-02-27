using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

public class BaseTest
{
    protected IWebDriver? driver;

    [OneTimeSetUp]
    public void Open()
    {
        var options = new ChromeOptions();

        options.AddArgument("--ignore-certificate-errors");
        options.AddArgument("--force-device-scale-factor=0.75");

        driver = new ChromeDriver(options);
        driver.Url = "https://demoqa.com/";
        driver.Manage().Window.Maximize();
    }

    [OneTimeTearDown]
    public void Close()
    {
        driver?.Quit();
    }
}