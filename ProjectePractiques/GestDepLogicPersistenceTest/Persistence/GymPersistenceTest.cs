using System;
using System.Linq;
using GestDep.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestDepLogicDesignTest
{
    [TestClass]
    public class GymPersistenceTest : BaseTest
    {
        [TestMethod]
        public void StoresInitialData()
        {
            Gym gym = new Gym(TestData.EXPECTED_GYM_CLOSING_HOUR, TestData.EXPECTED_GYM_DISCOUNT_LOCAL, TestData.EXPECTED_GYM_DISCOUNT_RETIRED,
                TestData.EXPECTED_GYM_FREE_USER_PRICE, TestData.EXPECTED_GYM_NAME, TestData.EXPECTED_GYM_OPENING_HOUR,
                TestData.EXPECTED_GYM_ZIP_CODE);
            dal.Insert(gym);
            dal.Commit();

            Gym gymDAL = dal.GetAll<Gym>().First();
            Assert.AreEqual(TestData.EXPECTED_GYM_CLOSING_HOUR, gymDAL.ClosingHour, "ClosingHour was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_GYM_DISCOUNT_LOCAL, gymDAL.DiscountLocal, "DiscountLocal was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_GYM_DISCOUNT_RETIRED, gymDAL.DiscountRetired, "DiscountRetired was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_GYM_FREE_USER_PRICE, gymDAL.FreeUserPrice, "FreeUserPrice was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_GYM_NAME, gymDAL.Name, "Name was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_GYM_OPENING_HOUR, gymDAL.OpeningHour, "OpeningHour was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_GYM_ZIP_CODE, gymDAL.ZipCode, "ZipCode was not stored properly.");

            Assert.IsNotNull(gymDAL.Activities, "The collection of Activites was not stored properly.");
            Assert.IsNotNull(gymDAL.Rooms, "The collection of Rooms was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_EMPTY_LIST_COUNT, gymDAL.Activities.Count, "The collection of Activites was not stored properly. The number of elements is higher than expected.");
            Assert.AreEqual(TestData.EXPECTED_EMPTY_LIST_COUNT, gymDAL.Rooms.Count, "The collection of Rooms was not stored properly. The number of elements is higher than expected.");
        }
        [TestMethod]
        public void StoresActivity()
        {
            Gym gym = new Gym(TestData.EXPECTED_GYM_CLOSING_HOUR, TestData.EXPECTED_GYM_DISCOUNT_LOCAL, TestData.EXPECTED_GYM_DISCOUNT_RETIRED,
                  TestData.EXPECTED_GYM_FREE_USER_PRICE, TestData.EXPECTED_GYM_NAME, TestData.EXPECTED_GYM_OPENING_HOUR,
                  TestData.EXPECTED_GYM_ZIP_CODE);

            Activity activity = new Activity(TestData.EXPECTED_ACTIVITY_DAYS, TestData.EXPECTED_ACTIVITY_DESCRIPTION, TestData.EXPECTED_ACTIVITY_DURATION,
            TestData.EXPECTED_ACTIVITY_FINISH_DATE, TestData.EXPECTED_MAX_ENROLLMENTS, TestData.EXPECTED_MIN_ENROLLMENTS, TestData.EXPECTED_ACTIVITY_PRICE,
            TestData.EXPECTED_ACTIVITY_START_DATE, TestData.EXPECTED_ACTIVITY_START_HOUR);

            gym.Activities.Add(activity);
            dal.Insert(gym);
            dal.Commit();

            Gym gymDAL = dal.GetAll<Gym>().First();
            Assert.IsNotNull(gymDAL.Activities, "The collection of Activites is not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_ONE_ELEMENT_LIST_COUNT, gymDAL.Activities.Count, "The collection of Activites is not properly stored .");
            Assert.AreEqual(TestData.EXPECTED_ACTIVITY_DESCRIPTION, gymDAL.Activities.First().Description, "The associated activity is not properly stored");
        }
        [TestMethod]
        public void StoresRoom()
        {
            Gym gym = new Gym(TestData.EXPECTED_GYM_CLOSING_HOUR, TestData.EXPECTED_GYM_DISCOUNT_LOCAL, TestData.EXPECTED_GYM_DISCOUNT_RETIRED,
                TestData.EXPECTED_GYM_FREE_USER_PRICE, TestData.EXPECTED_GYM_NAME, TestData.EXPECTED_GYM_OPENING_HOUR,
                TestData.EXPECTED_GYM_ZIP_CODE);

            Room room = new Room(TestData.EXPECTED_ROOM_NUMBER);

            gym.Rooms.Add(room);
            dal.Insert(gym);
            dal.Commit();

            Gym gymDAL = dal.GetAll<Gym>().First();
            Assert.IsNotNull(gymDAL.Rooms, "The collection of Rooms was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_ONE_ELEMENT_LIST_COUNT, gymDAL.Rooms.Count, "The collection of Rooms was not properly stored ");
            Assert.AreEqual(TestData.EXPECTED_ROOM_NUMBER, gymDAL.Rooms.First().Number, "The associated Room is not properly stored ");
        }


    }
}
