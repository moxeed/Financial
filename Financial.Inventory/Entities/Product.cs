using Financial.Common;
using System.Collections.Generic;

namespace Financial.Inventory.Entities
{
    public class Product : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int QuantityPerStore { get; set; }
        public int Price { get; set; }
        public ProductCategory Category { get; set; }
        public ICollection<ProductChangeLog> ChangeLogs { get; set; }

        public Product(string title, string description, int quantityPerStore, int price, ProductCategory category)
        {
            Title = title;
            Description = description;
            QuantityPerStore = quantityPerStore;
            Price = price;
            Category = category;
            ChangeLogs = new HashSet<ProductChangeLog>();
        }

        public void ChangePrice(int newPrice) {
            ChangeLogs.Add(new ProductChangeLog(Price, this));
            Price = newPrice;
        }
    }
}
