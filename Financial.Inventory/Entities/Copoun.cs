using Financial.Common;
using System;

namespace Financial.Inventory.Entities
{
    public class Coupon : Entity
    {
        public int? UserId { get; set; }
        public ProductCategory? Category { get; set; }
        public Product? Product { get; set; }
        public string Title { get; set; }
        public bool IsPercent { get; set; }
        public int Amount { get; set; }

        public Coupon(int? userId, Product? product, string title, bool isPercent, int amount)
        {
            UserId = userId;
            Product = product;
            Title = title;
            IsPercent = isPercent;
            Amount = amount;
        }

        public Coupon(int? userId, ProductCategory? category, string title, bool isPercent, int amount)
        {
            UserId = userId;
            Category = category;
            Title = title;
            IsPercent = isPercent;
            Amount = amount;
        }

        private Coupon()
        {
        }

        public int Apply(Product product, int userId) 
        {
            if (UserId.HasValue && UserId != userId) {
                return product.Price;
            }

            if (Product != null && Product.Id != product.Id) {
                return product.Price;
            }

            if (Category != null && Category.Id != product.Category.Id)
            {
                return product.Price;
            }

            if (IsPercent) 
            {
                return product.Price * (100 - Amount) / 100;
            }

            return Math.Max(product.Price - Amount, 0);
        }
    }
}
