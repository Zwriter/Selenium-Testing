using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class Checkout
{
    private IWebDriver _driver;
    private WebDriverWait _wait;

    public Checkout(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    }

    public void Test()
    {
        BillingAddress();
        ShippingMethod();
        PaymentMethod();
        PaymentInfo();
        ConfirmOrder();
        GoToOrderDetails();
    }

    private void BillingAddress()
    {
        _wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("input[onclick='Billing.save()']")));

        var countryField = _driver.FindElements(By.Id("BillingNewAddress_CountryId"));

        if (countryField.Count > 0 && countryField[0].Displayed)
        {
            new SelectElement(countryField[0]).SelectByText(AdressData.COUNTRY);
            SetField("BillingNewAddress_City", AdressData.CITY);
            SetField("BillingNewAddress_Address1", AdressData.ADRESS1);
            SetField("BillingNewAddress_ZipPostalCode", AdressData.ZIP);
            SetField("BillingNewAddress_PhoneNumber", UserData.PHONENUMBER);
        }
        else
        {
            Console.WriteLine("TASK_COMPLETED:: Saved billing address");
        }

        _driver.FindElement(By.CssSelector("input[onclick='Billing.save()']")).Click();

        _wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("input[onclick='Shipping.save()']"))).Click();

        Console.WriteLine("TASK_COMPLETED:: Chose shipping address");
    }

    private void ShippingMethod()
    {
        _wait.Until(ExpectedConditions.ElementToBeClickable(
            By.XPath("//label[contains(text(),'Next Day Air')]/../input"))).Click();

        _driver.FindElement(By.CssSelector("input[onclick='ShippingMethod.save()']")).Click();

        Console.WriteLine("TASK_COMPLETED:: Shipping method selected: Next Day Air");
    }

    private void PaymentMethod()
    {
        _wait.Until(ExpectedConditions.ElementToBeClickable(
            By.XPath("//label[contains(text(),'Credit Card')]/../input"))).Click();

        _driver.FindElement(By.CssSelector("input[onclick='PaymentMethod.save()']")).Click();

        Console.WriteLine("TASK_COMPLETED:: Payment method selected: Credit Card");
    }

    private void PaymentInfo()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("CardholderName")));
        var monthField = _driver.FindElements(By.Id("ExpireMonth"));
        var yearField = _driver.FindElements(By.Id("ExpireYear"));

        SetField("CardholderName", PaymentData.NAME);
        SetField("CardNumber", PaymentData.CARDNUMBER);
        SetField("CardCode", PaymentData.CVV);
        new SelectElement(monthField[0]).SelectByText(PaymentData.EXPIRATIONMONTH);
        new SelectElement(yearField[0]).SelectByText(PaymentData.EXPIRATIONYEAR);

        _driver.FindElement(By.CssSelector("input[onclick='PaymentInfo.save()']")).Click();

        Console.WriteLine("TASK_COMPLETED:: Payment information entered");
    }

    private void ConfirmOrder()
    {
        _wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("input[onclick='ConfirmOrder.save()']"))).Click();

        _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".order-completed")));
        string successMsg = _driver.FindElement(By.CssSelector(".title strong")).Text;
        Assert.That(successMsg, Does.Contain("successfully processed"));
        Console.WriteLine("TASK_COMPLETED:: Order placed successfully");

        string orderNumber = _driver.FindElement(By.CssSelector(".details li")).Text;
        Assert.That(orderNumber, Does.Contain("Order number"));
        Console.WriteLine($"TASK_COMPLETED:: {orderNumber}");
    }

    private void GoToOrderDetails()
    {
        _driver.FindElement(By.CssSelector(".details li a")).Click();
        Console.WriteLine($"TASK_COMPLETED:: Navigated to order details");
    }

    private void SetField(string id, string value)
    {
        var field = _driver.FindElement(By.Id(id));
        field.Clear();
        field.SendKeys(value);
    }
}