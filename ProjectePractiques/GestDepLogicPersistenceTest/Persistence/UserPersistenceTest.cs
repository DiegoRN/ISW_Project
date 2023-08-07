using System;
using System.Linq;
using GestDep.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestDepLogicDesignTest
{
    [TestClass]
    public class UserPersistenceTest : BaseTest
    {
        [TestMethod]
        public void StoresInitialData()
        {
            User user = new User(TestData.EXPECTED_PERSON_ADDRESS, TestData.EXPECTED_PERSON_IBAN, TestData.EXPECTED_PERSON_ID, TestData.EXPECTED_PERSON_NAME, TestData.EXPECTED_PERSON_ZIP_CODE, TestData.EXPECTED_USER_BIRTHDATE, TestData.EXPECTED_USER_RETIRED);
            dal.Insert(user);
            dal.Commit();

            User userDAL = dal.GetAll<User>().First();
            Assert.AreEqual(TestData.EXPECTED_PERSON_ADDRESS, userDAL.Address, "Address not properly stored.");
            Assert.AreEqual(TestData.EXPECTED_PERSON_ID, userDAL.Id, "Id not properly stored.");
            Assert.AreEqual(TestData.EXPECTED_PERSON_IBAN, userDAL.IBAN, "IBAN not properly stored.");
            Assert.AreEqual(TestData.EXPECTED_PERSON_NAME, userDAL.Name, "Name not properly stored.");
            Assert.AreEqual(TestData.EXPECTED_PERSON_ZIP_CODE, userDAL.ZipCode, "Zip code not properly stored.");
            Assert.AreEqual(TestData.EXPECTED_USER_BIRTHDATE, userDAL.BirthDate, "Birth date not properly stored.");
            Assert.AreEqual(TestData.EXPECTED_USER_RETIRED, userDAL.Retired, "Retired not properly stored. ");

            Assert.IsNotNull(userDAL.Enrollments, "Collection of Enrollments no properly stored.");
            Assert.AreEqual(TestData.EXPECTED_EMPTY_LIST_COUNT, userDAL.Enrollments.Count, "Collection of Enrollments not properly stored.");
        }
        [TestMethod]
        public void StoresEnrollment()
        {   
            User user = new User(TestData.EXPECTED_PERSON_ADDRESS, TestData.EXPECTED_PERSON_IBAN, TestData.EXPECTED_PERSON_ID, TestData.EXPECTED_PERSON_NAME, TestData.EXPECTED_PERSON_ZIP_CODE, TestData.EXPECTED_USER_BIRTHDATE, TestData.EXPECTED_USER_RETIRED);
            Enrollment enrollment = new Enrollment(TestData.EXPECTED_ENROLLMENT_DATE, TestData.DEFAULT_ACTIVITY, TestData.DEFAULT_PAYMENT, user);
            user.Enrollments.Add(enrollment);
            dal.Insert(user);
            dal.Commit();

            User userDAL = dal.GetAll<User>().First();
            Assert.IsNotNull(userDAL.Enrollments, "Collection of Enrollments no properly stored.");
            Assert.AreEqual(TestData.EXPECTED_ONE_ELEMENT_LIST_COUNT, userDAL.Enrollments.Count, "Collection of Enrollments not properly stored.");
            Assert.AreEqual(TestData.EXPECTED_ENROLLMENT_DATE, userDAL.Enrollments.First().EnrollmentDate, "Collection of Enrollments not properly stored.");

            Assert.AreEqual(userDAL.Id, userDAL.Enrollments.First().User.Id, "Collection of Enrollments not properly stored. The enrollment is not associated to the user.");

        }
    }
}
