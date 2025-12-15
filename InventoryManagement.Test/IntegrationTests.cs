using InventoryManagement.Model;
using InventoryManagement.Service;
using InventoryManagement.Source;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static InventoryManagement.Test.TestUtils;


namespace InventoryManagement.Test
{
    internal class IntegrationTests
    {
        [Test]
        public void SearchProducts_FiltersCorrectly()
        {
            var path = Path.GetTempFileName();
            try
            {
                var source = new DiskInventorySource(path);

                // add two products
                var p1 = new Product
                {
                    Id = "1",
                    Name = "Slim fit jeans",
                    Category = "Pants",
                    Color = "Washed",
                    Size = MakeJsonElement("30")
                };
                var p2 = new Product
                {
                    Id = "2",
                    Name = "Summer heels",
                    Category = "Shoes",
                    Color = "White",
                    Size = MakeJsonElement("{ \"h\": 10, \"w\": 5 }")
                };

                source.Add(p1);
                source.Add(p2);

                var service = new InventoryService(source);

                var results = service.SearchProducts(
                    "jeans",
                    (search, product) =>
                        !string.IsNullOrEmpty(product.Name) &&
                        product.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
                );

                Assert.That(results.Count, Is.EqualTo(1));
                Assert.That(results[0].Id, Is.EqualTo("1"));
            }
            finally
            {
                File.Delete(path);
            }
        }
        
    }
}
