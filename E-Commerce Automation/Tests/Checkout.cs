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
        //ShippingMethod();
        //PaymentMethod();
        //PaymentInfo();
        //ConfirmOrder();
    }

    private void BillingAddress()
    {
        _wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("input[onclick='Billing.save()']")));

        var countryField = _driver.FindElements(By.Id("BillingNewAddress_CountryId"));

        if (countryField.Count > 0 && countryField[0].Displayed)
        {
            new SelectElement(countryField[0]).SelectByText("Romania");
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

        _wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("#shipping-method-block")));

        _wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("input[onclick='Shipping.save()']"))).Click();

        Console.WriteLine("TASK_COMPLETED:: Chose shipping address");
    }

    private void ShippingMethod()
    {
        // Select Next Day Air
        _wait.Until(ExpectedConditions.ElementToBeClickable(
            By.XPath("//label[contains(text(),'Next Day Air')]/../input"))).Click();

        _driver.FindElement(By.CssSelector("input[onclick='ShippingMethod.save()']")).Click();

        _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("payment-method-block")));
        Console.WriteLine("✔ Shipping method selected: Next Day Air");
    }

    private void PaymentMethod()
    {
        // Select Credit Card
        _wait.Until(ExpectedConditions.ElementToBeClickable(
            By.XPath("//label[contains(text(),'Credit Card')]/../input"))).Click();

        _driver.FindElement(By.CssSelector("input[onclick='PaymentMethod.save()']")).Click();

        _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("payment-info-block")));
        Console.WriteLine("✔ Payment method selected: Credit Card");
    }

    private void PaymentInfo()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("CardholderName")));

        SetField("CardholderName", PaymentData.NAME);
        SetField("CardNumber", PaymentData.CARDNUMBER);
        SetField("ExpireMonth", PaymentData.EXPIRATIONMONTH);
        SetField("ExpireYear", PaymentData.EXPIRATIONYEAR);
        SetField("CardCode", PaymentData.CVV);

        _driver.FindElement(By.CssSelector("input[onclick='PaymentInfo.save()']")).Click();

        _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("confirm-order-block")));
        Console.WriteLine("✔ Payment information entered");
    }

    private void ConfirmOrder()
    {
        // Verify order summary is shown
        string orderSummary = _driver.FindElement(By.CssSelector(".confirm-order")).Text;
        Assert.That(orderSummary, Does.Contain("Fiction").Or.Contain("Simple Computer"),
            "Order summary should contain selected products");

        _driver.FindElement(By.CssSelector("input[onclick='ConfirmOrder.save()']")).Click();

        // Verify success message
        _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".order-completed")));
        string successMsg = _driver.FindElement(By.CssSelector(".title strong")).Text;
        Assert.That(successMsg, Does.Contain("successfully processed"),
            "Order should be successfully processed");
        Console.WriteLine("✔ Order placed successfully");

        // Verify order number exists
        string orderNumber = _driver.FindElement(By.CssSelector(".order-number strong")).Text;
        Assert.That(orderNumber, Is.Not.Empty, "Order number should be displayed");
        Console.WriteLine($"✔ Order number: {orderNumber}");
    }

    private void SetField(string id, string value)
    {
        var field = _driver.FindElement(By.Id(id));
        field.Clear();
        field.SendKeys(value);
    }
}