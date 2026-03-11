using OpenQA.Selenium;
using NUnit.Framework;

public class PostConditions
{
    private IWebDriver _driver;

    public PostConditions(IWebDriver driver)
    {
        _driver = driver;
    }

    public void Verify()
    {
        ClearCart();
        LogOut();
    }

    private void ClearCart()
    {
        try
        {
            string cartText = _driver.FindElement(By.ClassName("cart-qty")).Text;
            int cartValue = Int32.Parse(cartText.Replace("(", "").Replace(")", "").Trim());

            if (cartValue > 0)
            {
                _driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/cart");
                ((IJavaScriptExecutor)_driver).ExecuteScript(@"
                    var inputs = document.querySelectorAll('.qty-input');
                    if(inputs.length > 0) {
                        inputs.forEach(i => i.value = '0');
                        document.querySelector('[name=""updatecart""]').click();
                    }
                ");
                Console.WriteLine("POSTCONDITION:: Cart cleared");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"CART_POSTCONDITION_ERROR:: {e.Message}");
        }
    }

    private void LogOut()
    {
        try
        {
            var logoutLinks = _driver.FindElements(By.CssSelector("a[href='/logout']"));
            if (logoutLinks.Count > 0)
            {
                logoutLinks[0].Click();
                Console.WriteLine("POSTCONDITION:: User logged out");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"LOGOUT_POSTCONDITION_ERROR:: {e.Message}");
        }
    }
}