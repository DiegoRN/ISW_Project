using System;
using System.Linq;
using GestDep.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestDepLogicDesignTest
{
    [TestClass]
    public class ActivityPersistenceTest : BaseTest
    {
        [TestMethod]
        public void StoresInitialData()
        {
            Activity activity = new Activity(TestData.EXPECTED_ACTIVITY_DAYS, TestData.EXPECTED_ACTIVITY_DESCRIPTION, TestData.EXPECTED_ACTIVITY_DURATION,
                TestData.EXPECTED_ACTIVITY_FINISH_DATE, TestData.EXPECTED_MAX_ENROLLMENTS, TestData.EXPECTED_MIN_ENROLLMENTS, TestData.EXPECTED_ACTIVITY_PRICE,
                TestData.EXPECTED_ACTIVITY_START_DATE, TestData.EXPECTED_ACTIVITY_START_HOUR);
            dal.Insert(activity);
            dal.Commit();
            Activity activityDAL = dal.GetAll<Activity>().First();
            Assert.AreEqual(TestData.EXPECTED_ACTIVITY_DAYS, activityDAL.ActivityDays, "ActivityDays was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_ACTIVITY_DESCRIPTION, activityDAL.Description, "Description was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_ACTIVITY_DURATION, activityDAL.Duration, "Duration was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_ACTIVITY_FINISH_DATE, activityDAL.FinishDate, "FinishDate was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_MAX_ENROLLMENTS, activityDAL.MaximumEnrollments, "MaximumEnrollments was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_MIN_ENROLLMENTS, activityDAL.MinimumEnrollments, "MinimumEnrollments was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_ACTIVITY_PRICE, activityDAL.Price, "Price was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_ACTIVITY_START_DATE, activityDAL.StartDate, "StartDate was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_ACTIVITY_START_HOUR, activityDAL.StartHour, "StartHour was not initialized properly.");
            Assert.IsNotNull(activityDAL.Cancelled, "Cancelled was not stored properly.");

            Assert.IsNotNull(activityDAL.Enrollments, "The collection of Enrollments was not stored properly.");
            Assert.IsNotNull(activityDAL.Rooms, "The collection of Rooms was not stored properly.");
            Assert.AreEqual(TestData.EXPECTED_EMPTY_LIST_COUNT, activityDAL.Rooms.Count, "The collection of Rooms was not stored properly. The number of elements is higher than expected.");
            Assert.AreEqual(TestData.EXPECTED_EMPTY_LIST_COUNT, activityDAL.Enrollments.Count, "The collection of Enrollments was not stored properly. The number of elements is higher than expected.");
        }
        [TestMethod]
        public void StoresRoom()
        {
            Activity activity = new Activity(TestData.EXPECTED_ACTIVITY_DAYS, TestData.EXPECTED_ACTIVITY_DESCRIPTION, TestData.EXPECTED_ACTIVITY_DURATION,
               TestData.EXPECTED_ACTIVITY_FINISH_DATE, TestData.EXPECTED_MAX_ENROLLMENTS, TestData.EXPECTED_MIN_ENROLLMENTS, TestData.EXPECTED_ACTIVITY_PRICE,
               TestData.EXPECTED_ACTIVITY_START_DATE, TestData.EXPECTED_ACTIVITY_START_HOUR);
            Room room = new Room(TestData.EXPECTED_ROOM_NUMBER);
            activity.Rooms.Add(room); //Inverse navigation will be added by EE whether is well implemented
            dal.Insert(activity);
            dal.Commit();

            Activity activityDAL = dal.GetAll<Activity>().First();
            Assert.IsNotNull(activityDAL.Rooms, "The collection of Rooms was not properly stored .");
            Assert.AreEqual(TestData.EXPECTED_ONE_ELEMENT_LIST_COUNT, activityDAL.Rooms.Count, "The collection of Rooms was notproperly stored.");
            Assert.AreEqual(TestData.EXPECTED_ROOM_NUMBER, activityDAL.Rooms.First().Number, "The associated Room is not properly stored ");
            Assert.AreEqual(TestData.EXPECTED_ACTIVITY_DESCRIPTION, activityDAL.Rooms.First().Activities.First().Description, "The associated Room is not properly stored. Inverse navigation is not well implemented \n");

        }
        [TestMethod]
        public void StoresEnrollment()
        {
            Activity activity = new Activity(TestData.EXPECTED_ACTIVITY_DAYS, TestData.EXPECTED_ACTIVITY_DESCRIPTION, TestData.EXPECTED_ACTIVITY_DURATION,
                TestData.EXPECTED_ACTIVITY_FINISH_DATE, TestData.EXPECTED_MAX_ENROLLMENTS, TestData.EXPECTED_MIN_ENROLLMENTS, TestData.EXPECTED_ACTIVITY_PRICE,
                TestData.EXPECTED_ACTIVITY_START_DATE, TestData.EXPECTED_ACTIVITY_START_HOUR);
            Enrollment enrollment = new Enrollment(TestData.EXPECTED_ENROLLMENT_DATE, activity, TestData.DEFAULT_PAYMENT, TestData.DEFAULT_USER);
            activity.Enrollments.Add(enrollment);
            dal.Insert(activity);
            dal.Commit();

            Activity activityDAL = dal.GetAll<Activity>().First();
            Assert.IsNotNull(activityDAL.Enrollments, "The collection of Enrollments was not properly stored.");
            Assert.AreEqual(TestData.EXPECTED_ONE_ELEMENT_LIST_COUNT, activityDAL.Enrollments.Count, "The collection of Enrollments was not properly stored.");

        }
        [TestMethod]
        public void StoresInstructor()
        {
            Activity activity = new Activity(TestData.EXPECTED_ACTIVITY_DAYS, TestData.EXPECTED_ACTIVITY_DESCRIPTION, TestData.EXPECTED_ACTIVITY_DURATION,
                TestData.EXPECTED_ACTIVITY_FINISH_DATE, TestData.EXPECTED_MAX_ENROLLMENTS, TestData.EXPECTED_MIN_ENROLLMENTS, TestData.EXPECTED_ACTIVITY_PRICE,
                TestData.EXPECTED_ACTIVITY_START_DATE, TestData.EXPECTED_ACTIVITY_START_HOUR);
            Instructor instructor = new Instructor(TestData.EXPECTED_PERSON_ADDRESS, TestData.EXPECTED_PERSON_IBAN, TestData.EXPECTED_PERSON_ID,
                TestData.EXPECTED_PERSON_NAME, TestData.EXPECTED_PERSON_ZIP_CODE, TestData.EXPECTED_SSN);
            activity.Instructor = instructor;//Inverse navigation will be added by EE whether is well implemented
            dal.Insert(activity);
            dal.Commit();

            Activity activityDAL = dal.GetAll<Activity>().First();
            Assert.IsNotNull(activityDAL.Instructor, "The associated instructor is not properly stored.\n");
            Assert.AreEqual(TestData.EXPECTED_PERSON_ID, activityDAL.Instructor.Id, "The associated instructor is not properly stored.\n");
        }
    }
}
