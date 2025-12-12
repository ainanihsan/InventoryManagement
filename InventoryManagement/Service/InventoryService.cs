using InventoryManagement.Model;
using InventoryManagement.Source;

namespace InventoryManagement.Service
{
    internal class InventoryService : IInventoryService
    {
        protected readonly IInventorySource _inventorySource;

        public InventoryService(IInventorySource inventorySource)
        {
            _inventorySource = inventorySource;
        }

        public List<Product> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public List<Product> SearchProducts(string searchString, Func<string, Product, bool> searchCallback)
        {
            throw new NotImplementedException();
        }

        public Product? GetProduct(string id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
