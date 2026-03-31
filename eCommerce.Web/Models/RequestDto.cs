using static eCommerce.Web.Utility.SystemEnums;

namespace eCommerce.Web.Models
{
    public class RequestDto
    {
        public APITYPE APIType { get; set; } = APITYPE.GET;
        public string URL { get; set; } = string.Empty;
        public object RequestBody { get; set; }
        public string AccessToken { get; set; } = string.Empty;
    }
}
