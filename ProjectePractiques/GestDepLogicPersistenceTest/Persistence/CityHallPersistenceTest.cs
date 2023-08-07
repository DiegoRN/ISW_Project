using System;
using System.Linq;
using GestDep.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestDepLogicDesignTest
{
    [TestClass]
    public class CityHallPersistenceTest : BaseTest
    {
        [TestMethod]
        public void StoresInitialData()
        {
            CityHall cityHall = new CityHall(TestData.EXPECTED_CITY_HALL_NAME);
            dal.Insert(cityHall);
            dal.Commit();
            CityHall cityHallDAL = dal.GetAll<CityHall>().First();
            Assert.AreEqual(TestData.EXPECTED_CITY_HALL_NAME, cityHallDAL.Name, "Name was not properly stored .");
            Assert.IsNotNull(cityHallDAL.People, "The collection of People was not properly stored .");
            Assert.IsNotNull(cityHallDAL.Payments, "The collection of Payments was not properly stored .");
            Assert.IsNotNull(cityHallDAL.Gyms, "The collection of Gyms was not properly stored .");
            Assert.AreEqual(TestData.EXPECTED_EMPTY_LIST_COUNT, cityHallDAL.People.Count, "The collection of People was not properly stored .\nThe number of elements is higher than expected.");
            Assert.AreEqual(TestData.EXPECTED_EMPTY_LIST_COUNT, cityHallDAL.Payments.Count, "The collection of Payments was not stored properly.\nThe number of elements is higher than expected.");
            Assert.AreEqual(TestData.EXPECTED_EMPTY_LIST_COUNT, cityHallDAL.Gyms.Count, "The collection of Gyms was not properly stored .\nThe number of elements is higher than expected.");

        }
        [TestMethod]
        public void StoresPerson()
        {
            CityHall cityHall = new CityHall(TestData.EXPECTED_CITY_HALL_NAME);
            Person person = new Person(TestData.EXPECTED_PERSON_ADDRESS, TestData.EXPECTED_PERSON_IBAN, TestData.EXPECTED_PERSON_ID,
               TestData.EXPECTED_PERSON_NAME, TestData.EXPECTED_PERSON_ZIP_CODE);
            cityHall.People.Add(person);
            dal.Insert(cityHall);
            dal.Commit();

            CityHall cityHallDAL = dal.GetAll<CityHall>().First();
            Assert.IsNotNull(cityHallDAL.People, "The collection of People was not properly stored.");
            Assert.AreEqual(TestData.EXPECTED_ONE_ELEMENT_LIST_COUNT, cityHallDAL.People.Count, "The collection of People was not properly stored.\n");
            Assert.AreEqual(TestData.EXPECTED_PERSON_ID, cityHallDAL.People.First().Id, " Th associated Person is not properly stored\n");
        }
        [TestMethod]
        public void StoresPayment()
        {
            CityHall cityHall = new CityHall(TestData.EXPECTED_CITY_HALL_NAME);
            Payment payment = new Payment(TestData.EXPECTED_PAYMENT_DATE, TestData.EXPECTED_PAYMENT_DESCRIPCION, TestData.EXPECTED_PAYMENT_QUANTITY);
            cityHall.Payments.Add(payment);
            dal.Insert(cityHall);
            dal.Commit();

            CityHall cityHallDAL = dal.GetAll<CityHall>().First();
            Assert.IsNotNull(cityHallDAL.Payments, "The collection of Payments was not properly stored .");
            Assert.AreEqual(TestData.EXPECTED_ONE_ELEMENT_LIST_COUNT, cityHallDAL.Payments.Count, "The collection of Payments was not properly stored.\n");
            Assert.AreEqual(TestData.EXPECTED_PAYMENT_DESCRIPCION, cityHallDAL.Payments.First().Description, "The associated Payament is not properly stored\n"); 
        }

        [TestMethod]
        public void StoresGym()
        {
            CityHall cityHall = new CityHall(TestData.EXPECTED_CITY_HALL_NAME);
            Gym gym = new Gym(TestData.EXPECTED_GYM_CLOSING_HOUR, TestData.EXPECTED_GYM_DISCOUNT_LOCAL, TestData.EXPECTED_GYM_DISCOUNT_RETIRED,
                TestData.EXPECTED_GYM_FREE_USER_PRICE, TestData.EXPECTED_GYM_NAME, TestData.EXPECTED_GYM_OPENING_HOUR,
                TestData.EXPECTED_GYM_ZIP_CODE);
            cityHall.Gyms.Add(gym);
            dal.Insert(cityHall);
            dal.Commit();

            CityHall cityHallDAL = dal.GetAll<CityHall>().First();
            Assert.IsNotNull(cityHallDAL.Gyms, "The collection of Gyms was not properly stored .");
            Assert.AreEqual(TestData.EXPECTED_ONE_ELEMENT_LIST_COUNT, cityHallDAL.Gyms.Count, "The collection of Gyms was not properly stored .\n");
            Assert.AreEqual(TestData.EXPECTED_GYM_NAME, cityHallDAL.Gyms.First().Name, "The associated gym is not properly stored.\n");

        }
    }
}
