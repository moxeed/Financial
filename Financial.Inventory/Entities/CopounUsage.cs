namespace Financial.Inventory.Entities
{
    public class CouponUsage
    {
        public int OrderDetailId { get; }
        public int UserId { get; }
        public Coupon Coupon { get; }

        public CouponUsage(Coupon coupon, int orderDetailId, int userId)
        {
            Coupon = coupon;
            OrderDetailId = orderDetailId;
            UserId = userId;
        }

        private CouponUsage()
        {
        }
    }
}
