using System.Linq;
using GestDep.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestDepLogicDesignTest
{
    [TestClass]
    public class InstructorPersistenceTest: BaseTest
    {
        
        [TestMethod]
        public void StoresInitialData()
        {
            Instructor instructor = new Instructor(TestData.EXPECTED_PERSON_ADDRESS, TestData.EXPECTED_PERSON_IBAN, TestData.EXPECTED_PERSON_ID,
                TestData.EXPECTED_PERSON_NAME, TestData.EXPECTED_PERSON_ZIP_CODE, TestData.EXPECTED_SSN);
            dal.Insert(instructor);
            dal.Commit();

            Instructor instructorDAL = dal.GetAll<Instructor>().First();


            Assert.AreEqual(TestData.EXPECTED_PERSON_ADDRESS, instructorDAL.Address, "Address is not properly stored..");
            Assert.AreEqual(TestData.EXPECTED_PERSON_ID, instructorDAL.Id, "Id  is not properly stored.");
            Assert.AreEqual(TestData.EXPECTED_PERSON_IBAN, instructorDAL.IBAN, "IBAN is not properly stored.");
            Assert.AreEqual(TestData.EXPECTED_PERSON_NAME, instructorDAL.Name, "Name is not properly stored.");
            Assert.AreEqual(TestData.EXPECTED_PERSON_ZIP_CODE, instructorDAL.ZipCode, "Zip code is not properly stored.");
            Assert.AreEqual(TestData.EXPECTED_SSN, instructorDAL.Ssn, "SSN is not properly stored.");

            Assert.IsNotNull(instructor.Activities, "The collection of Activities is not properly stored");
            Assert.AreEqual(TestData.EXPECTED_EMPTY_LIST_COUNT, instructor.Activities.Count, "The collection of Activities is not properly stored");
        }
        [TestMethod]
        public void StoresActivity()
        {
            Instructor instructor = new Instructor(TestData.EXPECTED_PERSON_ADDRESS, TestData.EXPECTED_PERSON_IBAN, TestData.EXPECTED_PERSON_ID,
                 TestData.EXPECTED_PERSON_NAME, TestData.EXPECTED_PERSON_ZIP_CODE, TestData.EXPECTED_SSN);
          
            Activity activity = new Activity(TestData.EXPECTED_ACTIVITY_DAYS, TestData.EXPECTED_ACTIVITY_DESCRIPTION, TestData.EXPECTED_ACTIVITY_DURATION,
               TestData.EXPECTED_ACTIVITY_FINISH_DATE, TestData.EXPECTED_MAX_ENROLLMENTS, TestData.EXPECTED_MIN_ENROLLMENTS, TestData.EXPECTED_ACTIVITY_PRICE,
               TestData.EXPECTED_ACTIVITY_START_DATE, TestData.EXPECTED_ACTIVITY_START_HOUR);

            instructor.Activities.Add(activity); //if associaton are well defined, EF will complete in the DB: activity.Instructor = instructor 
            dal.Insert(instructor);
            dal.Commit();

            Instructor instructorDAL = dal.GetAll<Instructor>().First();
            Assert.IsNotNull(instructorDAL.Activities, "The collection of Activities is not properly stored");
            Assert.AreEqual(TestData.EXPECTED_ONE_ELEMENT_LIST_COUNT, instructorDAL.Activities.Count, "The collection of Activities is not properly stored");
            Assert.AreEqual(TestData.EXPECTED_PERSON_ID, instructorDAL.Activities.First().Instructor.Id, "The associated activity is not properly stored");
        }

    }
}
