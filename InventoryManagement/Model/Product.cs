using System.Text.Json;
using System.Text.Json.Serialization;

namespace InventoryManagement.Model
{
    internal class Product
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("size")]
        public JsonElement Size { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("color")]
        public string? Color { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }
    }
}