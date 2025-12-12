using InventoryManagement.Model;
using InventoryManagement.Service;
using InventoryManagement.Source;
using NSubstitute;
using NUnit.Framework;
using System.Text.Json;

namespace InventoryManagement.Test
{
    [TestFixture]
    internal class DiskInventorySourceTests
    {
        [Test]
        public void Add_Then_GetAll_ReturnsProduct()
        {
            var path = Path.GetTempFileName();
            var source = new DiskInventorySource(path);

            var product = new Product
            {
                Id = "100",
                Name = "Slim fit jeans",
                Category = "Jeans",
                Size = MakeJsonElement("36"),
                Color = "Blue"
            };
            source.Add(product);

            var all = source.GetAll();

            Assert.That(all.Count, Is.EqualTo(1));
            Assert.That(all[0].Id, Is.EqualTo("100"));
        }

        [Test]
        public void Update_ModifiesExistingProduct()
        {
            var path = Path.GetTempFileName();
            var source = new DiskInventorySource(path);

            var product = new Product 
            { 
                Id = "200",
                Name = "Old",
                Category = "Old Category",
                Color = "Old Color",
                Size = MakeJsonElement("36")
            };
            source.Add(product);

            var updated = new Product
            {
                Id = "200",
                Name = "New",
                Category = "New Category",
                Color = "New Color",
                Size = MakeJsonElement("37")
            };
            var success = source.Update(updated);

            var reloaded = source.Get("200");

            Assert.That(success, Is.True);
            Assert.That(reloaded!.Name, Is.EqualTo("New"));
            Assert.That(reloaded!.Category, Is.EqualTo("New Category"));
            Assert.That(reloaded!.Color, Is.EqualTo("New Color"));
            Assert.That(reloaded.Size.ValueKind, Is.EqualTo(JsonValueKind.Number));
            Assert.That(reloaded.Size.GetInt32(), Is.EqualTo(37));
        }

        private static JsonElement MakeJsonElement(string json)
        {
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.Clone(); // clone so element outlives document
        }

        [Test]
        public void Add_And_Get_WithObjectSize()
        {
            var path = Path.GetTempFileName();
            try
            {
                var source = new DiskInventorySource(path);

                var product = new Product
                {
                    Id = "100",
                    Name = "Slim fit jeans",
                    Category = "Pants",
                    Color = "Washed",
                    Size = MakeJsonElement("{ \"h\": 30, \"w\": 28 }") // object
                };

                source.Add(product);

                var all = source.GetAll();
                Assert.That(all.Count, Is.EqualTo(1));

                var loaded = all[0];
                Assert.That(loaded.Id, Is.EqualTo("100"));

                // assert object size
                Assert.That(loaded.Size.ValueKind, Is.EqualTo(JsonValueKind.Object));
                Assert.That(loaded.Size.TryGetProperty("h", out var hProp), Is.True);
                Assert.That(hProp.GetInt32(), Is.EqualTo(30));
                Assert.That(loaded.Size.TryGetProperty("w", out var wProp), Is.True);
                Assert.That(wProp.GetInt32(), Is.EqualTo(28));
            }
            finally
            {
                File.Delete(path);
            }
        }

    }
}
