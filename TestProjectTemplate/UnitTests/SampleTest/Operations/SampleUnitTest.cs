using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProjectTemplate.UnitTests.SampleTest.Operations
{
    [TestClass]
    public class SampleUnitTest : TestBase
    {
        #region Fields

        //private IService _service; OR //private Service _service;

        #endregion

        #region Constructor

        public SampleUnitTest()
        {
            // _service = GetService<IService>(); OR _service = GetService<Service>();
        }

        #endregion

        #region Test Initialization

        [TestInitialize]
        public override void TestInitialization()
        {
            //DB or state preparing for the test should do here.
        }

        #endregion

        #region Test Methods

        [TestMethod]
        public void SampleTestMethod()
        {
            //var result  = _service.Process();
        }

        #endregion

        #region Private Methods

        #endregion

        #region Test Cleanup

        [TestCleanup]
        public override void TestCleanup()
        {
            //DB or state reverting should do here.
        }

        #endregion
    }
}
