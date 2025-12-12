using InventoryManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service
{
    internal class CachedInventoryService : IInventoryService
    {
        private readonly IInventoryService _inner;
        private readonly TimeSpan _timeout;

        private readonly Dictionary<string, (Product Product, DateTime CachedAt)> _cache
            = new Dictionary<string, (Product, DateTime)>();

        public CachedInventoryService(IInventoryService inner, TimeSpan? timeout = null)
        {
            _inner = inner;
            _timeout = timeout ?? TimeSpan.FromMinutes(5);
        }

        public List<Product> GetAllProducts()
        {
            // No caching for getting all products.
            return _inner.GetAllProducts();
        }

        public Product? GetProduct(string id)
        {
            if (_cache.TryGetValue(id, out var entry))
            {
                if (DateTime.UtcNow - entry.CachedAt < _timeout)
                    return entry.Product;
            }

            var p = _inner.GetProduct(id);

            if (p != null)
                _cache[id] = (p, DateTime.UtcNow);

            return p;
        }

        public void AddProduct(Product product)
        {
            _inner.AddProduct(product);
            _cache[product.Id] = (product, DateTime.UtcNow);
        }

        public bool UpdateProduct(Product product)
        {
            var updated = _inner.UpdateProduct(product);

            if (updated)
                _cache[product.Id] = (product, DateTime.UtcNow);

            return updated;
        }

        public List<Product> SearchProducts(string searchString, Func<string, Product, bool> searchCallback)
        {
            // No caching for search
            return _inner.SearchProducts(searchString, searchCallback);
        }
    }
}
