using Alxf.BLL.Demo.Video;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Alxf.TestProject
{


    /// <summary>
    ///This is a test class for VideoModelResponsibilityTest and is intended
    ///to contain all VideoModelResponsibilityTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VideoModelResponsibilityTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Insert
        ///</summary>
        [TestMethod()]
        public void InsertTest()
        {
            VideoModelResponsibility target = new VideoModelResponsibility(); // TODO: Initialize to an appropriate value
            VideoModel model = null; // TODO: Initialize to an appropriate value
            model = new VideoModel();
            model.DownLink = "asdsadsa";
            
            model.Episodes = 12;
            model.ID = Guid.NewGuid();
            model.Name_cn = "测试中文";
            model.Name_en = "en name";
            model.Remark = "remark";
            model.TypeName = "动画:剧场版";
            model.UpdateTime = DateTime.Now;
            target.Insert(model);
            target=new VideoModelResponsibility();
            var cc = target.Query().Count(a => a.ID == model.ID);

            Assert.AreEqual<int>(1, cc);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteTest()
        {
            VideoModelResponsibility target = new VideoModelResponsibility(); // TODO: Initialize to an appropriate value
            VideoModel model = null; // TODO: Initialize to an appropriate value
            model = target.Query().First();
            target.Delete(model);
            target = new VideoModelResponsibility();
            var cc = target.Query().Count(a => a.ID == model.ID);
            Assert.AreEqual<int>(0, cc);
        }

        /// <summary>
        ///A test for Query
        ///</summary>
        [TestMethod()]
        public void QueryTest()
        {
            VideoModelResponsibility target = new VideoModelResponsibility(); // TODO: Initialize to an appropriate value
            IQueryable<VideoModel> expected = null; // TODO: Initialize to an appropriate value
            IQueryable<VideoModel> actual;
            actual = target.Query();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {
            VideoModelResponsibility target = new VideoModelResponsibility(); // TODO: Initialize to an appropriate value
            VideoModel model = null; // TODO: Initialize to an appropriate value
            target.Update(model);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
