using Financial.Inventory.Entities;
using System.Threading.Tasks;

namespace Financial.Inventory.Dependencies
{
    public interface IProductRepository
    {
        Task SaveProduct(Product product);
    }
}
