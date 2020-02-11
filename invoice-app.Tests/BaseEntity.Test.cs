using invoice_demo_app.Models;
using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/**
*
* @author denzil
*/
namespace invoice_demo_app.Tests.Basic.models
{
    [TestFixture]
    public class BaseEntityTest
    {
        Product product;

        [SetUp]
        public void Setup()
        {
            product = new Product();
        }

        [Test]
        public void TestValidateBaseEntityProperties()
        {
            product.ValidateBaseEntityProperties();

            Assert.IsNull(product.Id);
            Assert.IsNotNull(product.CreatedByUser);
            Assert.AreEqual(product.CreatedByUser, "dgumbo");
            Assert.AreEqual(product.ModifiedByUser, "dgumbo");
            Assert.IsTrue( product.ActiveStatus );

            Assert.AreEqual(product.CreationTime.Year, DateTime.Now.Year);
            Assert.AreEqual(product.ModificationTime.Year, DateTime.Now.Year);
        }

        //[TestCase(-1)]
        //[TestCase(0)]
        //[TestCase(1)]
        //public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
        //{
        //    var result = _primeService.IsPrime(value);

        //    Assert.IsFalse(result, $"{value} should not be prime");
        //}
    }
}
