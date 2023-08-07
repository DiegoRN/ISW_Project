
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GestDep.Persistence;


namespace GestDepLogicDesignTest
{
    [TestClass]
    public class BaseTest
    {
        protected private EntityFrameworkDAL dal;
   
        [TestInitialize]
        public void IniTests()
        {
            dal = new EntityFrameworkDAL(new GestDepDbContext());
            dal.RemoveAllData();


        }
        [TestCleanup]
        public void CleanTests()
        {
            dal.RemoveAllData();
        }
    }
}
