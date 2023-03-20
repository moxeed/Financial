using Financial.Common;

namespace Financial.Inventory.Entities
{
    public class ProductCategory : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public ProductCategory()
        {
            Title = string.Empty;
            Description = string.Empty;
        }
    }
}