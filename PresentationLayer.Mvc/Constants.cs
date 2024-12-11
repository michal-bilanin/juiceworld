namespace PresentationLayer.Mvc;

public static class Constants
{
    public static class Areas
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
    }

    public static string JwtToken = "JWT_TOKEN";

    public static string DefaultArea = Areas.Customer;
    public static string DefaultController = "Home";
    public static string DefaultAction = "Index";
}
