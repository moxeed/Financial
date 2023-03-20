using Financial.Common;

namespace Financial.Inventory.Entities
{
    public class Copoun : Entity
    {
        public int? UserId { get; set; }
        public ProductCategory? Category { get; set; }
        public Product? Product { get; set; }
        public string Title { get; set; } 
        public bool IsPercent { get; set; }
        public int Amount { get; set; }

        public Copoun(int? userId, Product? product, string title, bool isPercent, int amount)
        {
            UserId = userId;
            Product = product;
            Title = title;
            IsPercent = isPercent;
            Amount = amount;
        }

        public Copoun(int? userId, ProductCategory? category, string title, bool isPercent, int amount)
        {
            UserId = userId;
            Category = category;
            Title = title;
            IsPercent = isPercent;
            Amount = amount;
        }

        private Copoun() {
            Title = string.Empty;
        }
    }
}
