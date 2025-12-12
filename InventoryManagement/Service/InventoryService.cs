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
            return _inventorySource.GetAll();
        }

        public void AddProduct(Product product)
        {
            _inventorySource.Add(product);
        }

        public List<Product> SearchProducts(string searchString, Func<string, Product, bool> searchCallback)
        {
            return _inventorySource.GetAll()
                .Where(p => searchCallback(searchString, p))
                .ToList();
        }

        public Product? GetProduct(string id)
        {
            return _inventorySource.Get(id);
        }

        public bool UpdateProduct(Product product)
        {
            return _inventorySource.Update(product);
        }
    }
}
