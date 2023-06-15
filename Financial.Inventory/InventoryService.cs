using Financial.Inventory.Dependencies;
using Financial.Inventory.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financial.Inventory
{
    public class InventoryService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICouponRepository _couponRepository;

        public InventoryService(IProductRepository productRepository
            , ICouponRepository couponRepository)
        {
            _productRepository = productRepository;
            _couponRepository = couponRepository;
        }

        public Task CreateProduct(Product product)
        {
            return _productRepository.SaveProduct(product);
        }

        public async Task<Coupon> GetBestCoupon(int userId, List<int> productIds)
        {
            return new Coupon(1, new ProductCategory(), "test", false, 1000);
        }
    }
}
