using InventoryManagement.Model;
using System.Text.Json;

namespace InventoryManagement.Source
{
    /// <summary>
    /// Disk storage implementation of IInventoryStorage.
    /// Instantiated with a JSON file name and performs CRUD operations to that file.
    /// </summary>
    internal class DiskInventorySource : IInventorySource
    {
        private readonly string _filePath;

        public DiskInventorySource(string filePath)
        {
            _filePath = filePath;
        }

        private List<Product> ReadAllFromFile()
        {
            if (!File.Exists(_filePath))
                return new List<Product>();

            var text = File.ReadAllText(_filePath);

            if (string.IsNullOrWhiteSpace(text))
                return new List<Product>();

            return JsonSerializer.Deserialize<List<Product>>(text)
                   ?? new List<Product>();
        }

        private void WriteAllToFile(List<Product> products)
        {
            var json = JsonSerializer.Serialize(
                products,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(_filePath, json);
        }

        public List<Product> GetAll()
        {
            return ReadAllFromFile();
        }

        public Product? Get(string id)
        {
            return ReadAllFromFile().FirstOrDefault(p => p.Id == id);
        }

        public void Add(Product product)
        {
            var all = ReadAllFromFile();
            all.Add(product);
            WriteAllToFile(all);
        }

        public bool Update(Product product)
        {
            var all = ReadAllFromFile();
            var index = all.FindIndex(p => p.Id == product.Id);

            if (index < 0)
                return false;

            all[index] = product;
            WriteAllToFile(all);
            return true;
        }
    }
}
