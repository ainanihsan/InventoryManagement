using InventoryManagement.Model;

namespace InventoryManagement.Source
{
    /// <summary>
    /// Disk storage implementation of IInventoryStorage.
    /// Instantiated with a JSON file name and performs CRUD operations to that file.
    /// </summary>
    internal class DiskInventorySource : IInventorySource
    {
        public Product? Get(string id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Product product)
        {
            throw new NotImplementedException();
        }

        public bool Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
