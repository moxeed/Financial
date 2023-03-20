using Financial.Common;

namespace Financial.Inventory.Entities
{
    public class ProductChangeLog : Entity
    {
        public int Price { get; set; }
        public Product Product { get; set; }

        public ProductChangeLog(int price, Product product)
        {
            Price = price;
            Product = product;
        }
    }
}
