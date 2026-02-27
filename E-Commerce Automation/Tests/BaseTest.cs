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

        driver = new ChromeDriver(options);
        driver.Url = "https://demowebshop.tricentis.com/";
    }

    [OneTimeTearDown]
    public void Close()
    {
        //driver?.Quit();
    }
}