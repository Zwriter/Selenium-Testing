public enum PaymentMethod
{
    COD,
    Check,
    CreditCard,
    PurchaseOrder
};
public enum Gender
{
    Male,
    Female
}

public class TestDataModel
{
    public string Gender { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string Country { get; set; } = "";
    public string City { get; set; } = "";
    public string Address { get; set; } = "";
    public string Zip { get; set; } = "";
    public string Phone { get; set; } = "";
    public string PaymentMethod { get; set; } = "";
    public string CardNumber { get; set; } = "";
    public string CardExpiryMonth { get; set; } = "";
    public string CardExpiryYear { get; set; } = "";
    public string CardCvv { get; set; } = "";

    public Gender GenderEnum => Enum.Parse<Gender>(Gender);
    public PaymentMethod PaymentMethodEnum => Enum.Parse<PaymentMethod>(PaymentMethod);
}