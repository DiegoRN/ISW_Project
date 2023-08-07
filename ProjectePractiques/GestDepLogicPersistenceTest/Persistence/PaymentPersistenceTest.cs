using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GestDep.Entities;
using System.Linq;

namespace GestDepLogicDesignTest
{
    [TestClass]
    public class PaymentPersistenceTest : BaseTest
    {
        [TestMethod]
        public void StoresInitialData()
        {
            Payment payment = new Payment(TestData.EXPECTED_PAYMENT_DATE, TestData.EXPECTED_PAYMENT_DESCRIPCION, TestData.EXPECTED_PAYMENT_QUANTITY);
            dal.Insert(payment);
            dal.Commit();

            Payment paymentDAL = dal.GetAll<Payment>().First();
            Assert.AreEqual(TestData.EXPECTED_PAYMENT_DATE, paymentDAL.Date, "Date was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_PAYMENT_DESCRIPCION, paymentDAL.Description, "Description was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_PAYMENT_QUANTITY, paymentDAL.Quantity, "Quantity was not stored properly.");
        }
    }
}
