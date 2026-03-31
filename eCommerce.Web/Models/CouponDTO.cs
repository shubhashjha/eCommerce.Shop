namespace eCommerce.Web.Models
{
    public class CouponDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public decimal DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
