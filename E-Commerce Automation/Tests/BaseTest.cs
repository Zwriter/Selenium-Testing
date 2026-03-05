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
        driver.Url = "https://demowebshop.tricentis.com/";
        driver.Manage().Window.Maximize();
    }

    //[OneTimeTearDown]
    public void Close()
    {
        driver!.Navigate().GoToUrl("https://demowebshop.tricentis.com/cart");

        ((IJavaScriptExecutor)driver).ExecuteScript(@"
            var inputs = document.querySelectorAll('.qty-input');
            if(inputs.length > 0) {
                inputs.forEach(i => i.value = '0');
                document.querySelector('[name=""updatecart""]').click();
            }
        ");

        driver.Quit();
    }
}