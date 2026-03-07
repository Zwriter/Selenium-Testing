using System.ComponentModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

public class Pagination
{
    private IWebDriver _driver;
    private int _numberOfElements;

    public Pagination(IWebDriver driver, int elements)
    {
        _driver = driver;
        _numberOfElements = elements;
    }
    public void Navigate()
    {
        _driver.FindElement(By.CssSelector("a[href='/elements']")).Click();

        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        var element = wait.Until(d => d.FindElement(By.CssSelector("a[href='/webtables']")));
        element.Click();
    }

    public void Test()
    {
        _driver.FindElement(By.Id("addNewRecordButton")).Click();
        int i;
        for (i = 1; i <= _numberOfElements; i++)
        {
            var age = FormData.AGE + i;

            _driver.FindElement(By.CssSelector("input[id='firstName']")).SendKeys(FormData.NAME + i);
            _driver.FindElement(By.CssSelector("input[id='lastName']")).SendKeys(FormData.LASTNAME + i);
            _driver.FindElement(By.CssSelector("input[id='userEmail']")).SendKeys(FormData.EMAIL + i + "@gmail.com");
            _driver.FindElement(By.CssSelector("input[id='age']")).SendKeys(age.ToString());
            _driver.FindElement(By.CssSelector("input[id='salary']")).SendKeys(FormData.SALARY.ToString());
            _driver.FindElement(By.CssSelector("input[id='department']")).SendKeys(FormData.DEPARTMENT);

            _driver.FindElement(By.Id("submit")).Click();

            if (i != _numberOfElements)
                _driver.FindElement(By.Id("addNewRecordButton")).Click();
        }

        _driver.FindElement(By.XPath("//button[text()='Next']")).Click();

        _driver.FindElement(By.Id($"delete-record-{i + 2}")).Click();
    }

    public void Validate()
    {
        try
        {
            var pageInfo = _driver.FindElement(By.CssSelector("div.col-auto strong")).Text;
            Assert.That(pageInfo, Is.EqualTo("1 of 1"));
            TestContext.WriteLine($"TASK_COMPLETED:: Pagination collapsed correctly: {pageInfo}");

            var rows = _driver.FindElements(By.CssSelector(".rt-tr-group"));
            var found = rows.First(row => row.Text.Contains(FormData.NAME + "1"));
            Assert.That(found, Is.True);
            TestContext.WriteLine($"TASK_COMPLETED:: Inserted rows found: {pageInfo}");
        }
        catch (Exception err)
        {
            TestContext.WriteLine("ERROR:: " + err);
            throw new Exception("ERROR:: " + err);
        }
    }
}