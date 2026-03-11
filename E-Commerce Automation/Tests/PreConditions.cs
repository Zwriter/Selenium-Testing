using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

public class PreConditions
{
    private IWebDriver _driver;

    public PreConditions(IWebDriver driver)
    {
        _driver = driver;
    }

    public void Verify()
    {
        PageLoad();
        EmpyCart();
    }

    private void PageLoad()
    {
        Assert.That(_driver.Title, Does.Contain("Demo Web Shop"),
            "Precondition failed: site did not load");
        Console.WriteLine("PRECONDITION:: Site loaded successfully");
    }

    private void EmpyCart()
    {
        _driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/cart");
        ((IJavaScriptExecutor)_driver).ExecuteScript(@"
            var inputs = document.querySelectorAll('.qty-input');
            if(inputs.length > 0) {
                inputs.forEach(i => i.value = '0');
                document.querySelector('[name=""updatecart""]').click();
            }
        ");
        _driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");
        Console.WriteLine("PRECONDITION:: Cart is empty");
    }
}