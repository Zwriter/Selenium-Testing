using System.ComponentModel;
using NUnit.Framework;
using OpenQA.Selenium;

public class UserLogIn
{
    private IWebDriver _driver;

    private string _email;
    private string _password;

    public UserLogIn(IWebDriver driver, string email, string password)
    {
        _driver = driver;
        _email = email;
        _password = password;
    }
    public void Navigate()
    {
        _driver.FindElement(By.CssSelector("a[href='/login']")).Click();
    }

    public void Test()
    {
        _driver.FindElement(By.Id("Email")).SendKeys(_email);
        _driver.FindElement(By.Id("Password")).SendKeys(_password);

        _driver.FindElement(By.Id("RememberMe")).Click();
        _driver.FindElement(By.CssSelector("input[class='button-1 login-button']")).Click();

        //Validation
        var checkEmail = _driver.FindElement(By.CssSelector("a[href='/customer/info']")).Text;
        Assert.That(checkEmail, Is.EqualTo(_email));
        Console.WriteLine($"TASK_COMPLETED:: User Log In successful for {_email}");
    }
}