using System;
using System.Linq;
using GestDep.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestDepLogicDesignTest
{
    [TestClass]
    public class EnrollmentPersistenceTest : BaseTest
    {
        [TestMethod]
        public void StoresInitialData()
        {
            Enrollment enrollment = new Enrollment(TestData.EXPECTED_ENROLLMENT_DATE, TestData.DEFAULT_ACTIVITY, TestData.DEFAULT_PAYMENT, TestData.DEFAULT_USER);
            dal.Insert(enrollment);
            dal.Commit();

            Enrollment enrollmentDAL = dal.GetAll<Enrollment>().First();
            Assert.AreEqual(TestData.EXPECTED_ENROLLMENT_DATE, enrollmentDAL.EnrollmentDate, "EnrollmentDate not properly stored.");
            Assert.AreEqual(TestData.DEFAULT_ACTIVITY, enrollmentDAL.Activity, "Activity not properly stored. Please, check that the attribute is marked as virtual.");
            Assert.AreEqual(TestData.DEFAULT_USER, enrollmentDAL.User, "User not properly not properly stored. Please, check that the attribute is marked as virtual.");

            Assert.IsNull(enrollmentDAL.CancellationDate, "CancellationDate not properly stored.");
            Assert.IsNull(enrollmentDAL.ReturnedFirstCuotaIfCancelledActivity, "ReturnedFirstCuotaIfCancelledActivity not properly stored.");

            Assert.IsNotNull(enrollmentDAL.Payments, "Collection of Payments not properly stored.");
            Assert.AreEqual(TestData.EXPECTED_ONE_ELEMENT_LIST_COUNT, enrollmentDAL.Payments.Count, "Payment not added to the list of Payments.");
            Assert.AreEqual(TestData.DEFAULT_PAYMENT, enrollmentDAL.Payments.First(), "There should be one payment stored. Please, check that the relationship is marked as virtual and the minimum cardinality is kept in the constructor.");
        }

        [TestMethod]
        public void TestVirtualKeyword() {
            Enrollment enrollment = new Enrollment(TestData.EXPECTED_ENROLLMENT_DATE, TestData.DEFAULT_ACTIVITY, TestData.DEFAULT_PAYMENT, TestData.DEFAULT_USER);
            dal.Insert(enrollment);
            dal.Commit();

            Enrollment enrollmentDAL = dal.GetAll<Enrollment>().First();
            Assert.AreEqual(enrollmentDAL, enrollmentDAL.User.Enrollments.First(), "The first enrollment in the User's enrollment list should be the same as this enrollment.");
        }
    }
}
