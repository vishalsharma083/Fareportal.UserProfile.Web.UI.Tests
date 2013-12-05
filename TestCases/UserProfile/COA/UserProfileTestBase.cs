using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserProfileSPA.TestCases;
using OpenQA.Selenium;
using System.IO;
using System.Data;

using UserProfileSPA.Library;

namespace UserProfileSPA.TestCases
{
    /// <summary>
    /// Summary description for SPAUP
    /// </summary>
    [TestClass]
    public class UserProfileTestBase
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

        public string Record(string columnName_)
        {
            return TestContext.DataRow[columnName_].ToString();
        }

        [TestCleanup]
        public void Cleanup()
        {
           UserProfileSPA.Library.TestEnvironment.Dispose();
        }     

    }
}
