using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GestDep.Entities;
using System.Linq;

namespace GestDepLogicDesignTest
{
    [TestClass]
    public class RoomPersistenceTest : BaseTest
    {
        [TestMethod]
        public void StoresInitialData()
        {
            Room room = new Room(TestData.EXPECTED_ROOM_NUMBER);
            dal.Insert(room);
            dal.Commit();

            Room roomDAL = dal.GetAll<Room>().First();
            Assert.AreEqual(TestData.EXPECTED_ROOM_NUMBER, roomDAL.Number, "Number not properly stored.");
            Assert.IsNotNull(roomDAL.Activities, "Collection of Activities not properly stored.");
            Assert.AreEqual(TestData.EXPECTED_EMPTY_LIST_COUNT, roomDAL.Activities.Count, "Collection of Activities not properly stored. The number of elements is higher than expected.");
        }

        [TestMethod]
        public void StoresActivity()
        {
            Room room = new Room(TestData.EXPECTED_ROOM_NUMBER);

            Activity activity = new Activity(TestData.EXPECTED_ACTIVITY_DAYS, TestData.EXPECTED_ACTIVITY_DESCRIPTION, TestData.EXPECTED_ACTIVITY_DURATION,
              TestData.EXPECTED_ACTIVITY_FINISH_DATE, TestData.EXPECTED_MAX_ENROLLMENTS, TestData.EXPECTED_MIN_ENROLLMENTS, TestData.EXPECTED_ACTIVITY_PRICE,
              TestData.EXPECTED_ACTIVITY_START_DATE, TestData.EXPECTED_ACTIVITY_START_HOUR);

            room.Activities.Add(activity);
            dal.Insert(room);
            dal.Commit();
            Room roomDAL = dal.GetAll<Room>().First();
            Assert.IsNotNull(roomDAL.Activities, "Collection of Activities not properly stored.");
            Assert.AreEqual(TestData.EXPECTED_ONE_ELEMENT_LIST_COUNT, roomDAL.Activities.Count, "Collection of Activities not properly stored");
            Assert.AreEqual(TestData.EXPECTED_ROOM_NUMBER, roomDAL.Activities.First().Rooms.First().Number, "The associated activity is not properly stored");


        }


    }
}
