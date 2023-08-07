
using System.Linq;
using GestDep.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace GestDepLogicDesignTest
{
    [TestClass]
    public class PersonPersistenceTest : BaseTest
    {  
        
        [TestMethod]
        public void StoresInitialData()
        {   /*Arrange

             - BaseTest.IniTests() is run before this code
             - Static Objects and variables used to create the object are inicialized in the class TestData 
             */

            //Act
            Person person = new Person(TestData.EXPECTED_PERSON_ADDRESS, TestData.EXPECTED_PERSON_IBAN, TestData.EXPECTED_PERSON_ID, 
                TestData.EXPECTED_PERSON_NAME, TestData.EXPECTED_PERSON_ZIP_CODE);
            dal.Insert(person);
            dal.Commit();
            //Assert
            Person personDAL = dal.GetAll<Person>().First();
            Assert.AreEqual(TestData.EXPECTED_PERSON_ADDRESS, personDAL.Address, "Address not properly stored.");
            Assert.AreEqual(TestData.EXPECTED_PERSON_ID, personDAL.Id, "Id not properly stored. ");
            Assert.AreEqual(TestData.EXPECTED_PERSON_IBAN, personDAL.IBAN, "IBAN not properly stored. ");
            Assert.AreEqual(TestData.EXPECTED_PERSON_NAME, personDAL.Name, "Name not properly stored. ");
            Assert.AreEqual(TestData.EXPECTED_PERSON_ZIP_CODE, personDAL.ZipCode, "Zip code not properly stored.");
            //After assert: BaseTest.CleanTests() is run
        }
    }
}
