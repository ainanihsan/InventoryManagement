using InventoryManagement.Model;
using InventoryManagement.Service;
using NSubstitute;
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
    internal class CachedInventoryServiceTest
    {        
        [Test]
        public void GetProduct_AfterExpiry_CallsInnerAgain()
        {
            var inner = Substitute.For<IInventoryService>();
            var product = new Product { Id = "1", Name = "Test", Size = MakeJsonElement("37") };
            inner.GetProduct("1").Returns(product);

            var cached = new CachedInventoryService(inner, TimeSpan.FromMilliseconds(50));

            var p1 = cached.GetProduct("1");
            Thread.Sleep(100); // allow expiry
            var p2 = cached.GetProduct("1");

            inner.Received(2).GetProduct("1");
        }
        [Test]
        public void GetProduct_Twice_UsesCache_InnerCalledOnce()
        {
            var inner = Substitute.For<IInventoryService>();
            var product = new Product { Id = "1", Name = "Test", Size = MakeJsonElement("37") };
            inner.GetProduct("1").Returns(product);

            var cached = new CachedInventoryService(inner, TimeSpan.FromMinutes(5));
            var a = cached.GetProduct("1");
            var b = cached.GetProduct("1");

            Assert.That(a, Is.Not.Null);
            Assert.That(b, Is.Not.Null);
            inner.Received(1).GetProduct("1");
        }


    }
}
