using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

public class BaseTest
{
    protected ChromeDriver driver = null!;
    protected TestDataModel testData = null!;

    [OneTimeSetUp]
    public void Open()
    {
        testData = JsonFileLoader.Load<TestDataModel>("testdata.json");

        var options = new ChromeOptions();
        options.AddArgument("--ignore-certificate-errors");
        options.AddArgument("--force-device-scale-factor=0.75");

        driver = new ChromeDriver(options);
        driver.Manage().Window.Maximize();
        driver.Url = "https://demowebshop.tricentis.com/";

        var preConditions = new PreConditions(driver);
        preConditions.Verify();
    }

    [OneTimeTearDown]
    public void Close()
    {
        try
        {
            var postConditions = new PostConditions(driver);
            postConditions.Verify();
        }
        catch (Exception e)
        {
            Console.WriteLine($"TearDown warning: {e.Message}");
        }
        finally
        {
            driver?.Quit();
        }
    }
}