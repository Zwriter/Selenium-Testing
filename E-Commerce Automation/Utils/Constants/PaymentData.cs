public enum PaymentMethod
{
    COD,
    Check,
    CreditCard,
    PurchaseOrder
};

public static class PaymentData
{
    public const PaymentMethod PAYMENT = PaymentMethod.CreditCard;
    public const string NAME = UserData.FIRST_NAME + " " + UserData.LAST_NAME;
    public const string CARDNUMBER = "4111 1111 1111 1111";
    public const string EXPIRATIONMONTH = "12";
    public const string EXPIRATIONYEAR = "2028";
    public const string CVV = "123";
    public const string PHONENUMBER = "0734321124";
}