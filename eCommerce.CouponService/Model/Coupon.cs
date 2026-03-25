namespace eCommerce.CouponService.Model
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public decimal DiscountAmount { get; set; }
        public int MinAmount { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
