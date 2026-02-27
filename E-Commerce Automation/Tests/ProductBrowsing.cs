using System.ComponentModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

public class ProductBrowsing
{
    private IWebDriver _driver;

    public ProductBrowsing(IWebDriver driver)
    {
        _driver = driver;
    }

    public void Navigate()
    {
        _driver.FindElement(By.CssSelector("a[href='/computers']")).Click();
        _driver.FindElement(By.CssSelector("a[title='Show products in category Desktops']")).Click();
    }

    public void Test()
    {
        //_driver.FindElement(By.Id("Email")).SendKeys();
        //_driver.FindElement(By.Id("Password")).SendKeys();

        //_driver.FindElement(By.Id("RememberMe")).Click();
        //_driver.FindElement(By.CssSelector("input[class='button-1 login-button']")).Click();
    }
}