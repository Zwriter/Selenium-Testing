using System.ComponentModel;
using NUnit.Framework;
using OpenQA.Selenium;

public class UserCreation
{
    private IWebDriver _driver;

    private string _firstName;
    private string _lastName;
    private string _email;
    private Gender _gender;
    private string _password;

    public UserCreation(IWebDriver driver, Gender gender, string name, string last_name, string email, string password)
    {
        _driver = driver;
        _gender = gender;
        _firstName = name;
        _lastName = last_name;
        _email = email;
        _password = password;
    }
    public void Navigate()
    {
        _driver.FindElement(By.CssSelector("a[href='/register']")).Click();
    }

    public void Test()
    {
        var gender = _gender == Gender.Male ? "male" : "female";

        _driver.FindElement(By.CssSelector($"input[id='gender-{gender}']")).Click();
        _driver.FindElement(By.Id("FirstName")).SendKeys(_firstName);
        _driver.FindElement(By.Id("LastName")).SendKeys(_lastName);
        _driver.FindElement(By.Id("Email")).SendKeys(_email);

        _driver.FindElement(By.Id("Password")).SendKeys(_password);
        _driver.FindElement(By.Id("ConfirmPassword")).SendKeys(_password);

        _driver.FindElement(By.CssSelector("input[id='register-button']")).Click();

        string resultText = _driver.FindElement(By.ClassName("result")).Text;
        Assert.That(resultText, Is.EqualTo("Your registration completed"));
        Console.WriteLine("TASK_COMPLETED:: User registration succsedded");

        _driver.FindElement(By.CssSelector("input[class='button-1 register-continue-button']")).Click();
    }
}