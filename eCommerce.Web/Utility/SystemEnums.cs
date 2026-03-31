namespace eCommerce.Web.Utility
{
    public class SystemEnums
    {
        public static string COUPON_BASE_URL { get; set; }
        public enum APITYPE 
        {
            GET,
            POST,
            PUT,
            DELETE,
            PATCH
        }
    }
}
