using InventoryManagement.Model;

namespace InventoryManagement.Source
{
    internal interface IInventorySource
    {
        public List<Product> GetAll();
        public Product? Get(string id);
        public void Add(Product product);
        public bool Update(Product product);
    }
}