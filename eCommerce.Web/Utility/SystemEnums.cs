namespace eCommerce.Web.Utility
{
    public class SystemEnums
    {
        public static string COUPON_BASE_URL { get; set; }
        public static string PRODUCT_BASE_URL { get; set; }
        public static string AUTH_BASE_URL { get; set; }
        public static string SHOPPING_CART_BASE_URL { get; set; }
        public static string ORDER_BASE_URL { get; set; }

        public const string ROLE_ADMIN = "ADMIN";
        public const string ROLE_CUSTOMER = "CUSTOMER";
        public const string TOKEN_COOKIE = "JWTToken";

        public const string STATUS_PENDING = "Pending";
        public const string STATUS_APPROVED = "Approved";
        public const string STATUS_READY_FOR_PICKUP = "ReadyForPickup";
        public const string STATUS_COMPLETED = "Completed";
        public const string STATUS_REFUNDED = "Refunded";
        public const string STATUS_CANCELLED = "Cancelled";

        public enum APITYPE 
        {
            GET,
            POST,
            PUT,
            DELETE,
            PATCH
        }

        public enum CONTENTTYPE
        {
            Json,
            MultipartFormData
        }
    }
}
