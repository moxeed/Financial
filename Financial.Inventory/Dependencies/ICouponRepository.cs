using Financial.Inventory.Entities;
using System.Threading.Tasks;

namespace Financial.Inventory.Dependencies
{
    public interface ICouponRepository
    {
        Task SaveCoupon(Coupon product);
    }
}
