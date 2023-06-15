using Financial.Inventory.Entities;

namespace Financial.Inventory.Test
{
    public class ProductTest
    {
        [Fact]
        public void ValidProduct_ChangePrice_ChangeLogIsInserted()
        {
            var category = new ProductCategory();
            var product = new Product("test", "test", 100, 100, category);

            product.ChangePrice(200);

            Assert.NotEmpty(product.ChangeLogs);
            var first = product.ChangeLogs.First();
            Assert.Equal(100, first.Price);
        }
    }
}