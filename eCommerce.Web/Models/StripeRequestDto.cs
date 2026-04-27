namespace eCommerce.Web.Models
{
    public class StripeRequestDto
    {
        public string? StripeSessionUrl { get; set; }
        public string? StripeSessionId { get; set; }
        public string ApprovedUrl { get; set; } = string.Empty;
        public string CancelUrl { get; set; } = string.Empty;
        public OrderHeaderDto? OrderHeader { get; set; }
    }
}
