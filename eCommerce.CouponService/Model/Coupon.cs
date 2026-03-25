using System.ComponentModel.DataAnnotations;

namespace eCommerce.CouponService.Model
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; } = string.Empty;
        
        [Required]
        public decimal DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
