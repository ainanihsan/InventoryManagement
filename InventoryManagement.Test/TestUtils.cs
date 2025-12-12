using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventoryManagement.Test
{
    static class TestUtils
    {
        public static JsonElement MakeJsonElement(string json)
        {
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.Clone(); // clone so element outlives document
        }
    }
}
