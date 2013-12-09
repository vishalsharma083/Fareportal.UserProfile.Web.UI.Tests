using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using UserProfileSPA.Library;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace UserProfileSPA.TestCases
{
    [TestClass]
    public class MyDeals
    {

        [TestInitialize]
        public void Initialize()
        {
            UserProfileSPA.Library.TestEnvironment.Init();
        }
        private TestContext testContextInstance;
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

        [TestMethod]
        public void VerifyingTheSourceAndDestinationAndPrice()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
          
            string _baseUrl = Record("SignInUrl");
            if (_baseUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(2);
                Utility.CsstoClick("MyDealPage", 4);

                IWebElement lDeal = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("Image")));
                new Actions(Driver).MoveToElement(lDeal).Build().Perform();

                string Deals = Utility.ByXpath("MouseHoverOnFirstDealInFligts", 3);
            }

        }

        [TestCleanup]
        public void Cleanup()
        {
            UserProfileSPA.Library.TestEnvironment.Dispose();
        }    
    }
}
