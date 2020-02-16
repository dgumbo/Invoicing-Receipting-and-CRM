using invoice_app.Services;
using invoice_app.Services.impl;
using invoice_demo_app.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace invoice_app.Tests
{
    [TestFixture]
    class ProductServiceTest
    {
        // IProductService productService ;

        [SetUp]
        public void Setup()
        {


    }

        [Test]
        public void CreateMethodTest()
        {
            //Mock<IProductService> mockProductService = new Mock<IProductService>();
            Mock<DbSet<Product>> mockProductDbSet = new Mock<DbSet<Product>>();
            mockProductDbSet.Setup(productDbSet => productDbSet.Find(It.IsAny<int>()))
                .Returns(new Product() { Id = 1 }); 
            // WOW! No record/replay weirdness?! :)

            Mock<IProductService> mockProductService = new Mock<IProductService>();
            mockProductService.Setup(productService => productService.GetDbSet())
                .Returns(mockProductDbSet.Object);
            //Mock<DbSet<Product>> mockProductDbSet = new Mock<DbSet<Product>>();

            // productService = new ProductServiceImpl(mockAppDbContext.Object);

            mockProductService.Verify(ps=> ps.Find(152452), Times.AtMostOnce()) ;

            var productService = mockProductService.Object;
            var product = productService.Create(new Product());

            Assert.That(product , Is.Null);

            // Verify that the given method was indeed called with the expected value at most once
            // mock.Verify(library => library.Create(product), Times.AtMostOnce());
            // mock.Verify(library => product.ValidateBaseEntityProperties( ), Times.AtMostOnce()); 
        }
    }
}
