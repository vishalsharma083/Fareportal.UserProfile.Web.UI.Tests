using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using UserProfileSPA.Library;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;
using System.Configuration;

namespace UserProfileSPA.TestCases
{
    [TestClass]
    public class FPIntegration
    {

        string SignInUrl = ConfigurationManager.AppSettings["URL"];
        string Prefix = ConfigurationManager.AppSettings["UrlPrefix"];

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
        public void SignInToViewYourBookingAllValidations()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            string _signInToViewYourBookingAllValidations = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("SignInToViewYourBookingAllValidations");
            string[] ToViewYourBookingAllValidations = _signInToViewYourBookingAllValidations.Split(",".ToCharArray());

            Utility.ByLinkText("ClickOnMyBookingLink", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
            Utility.Sleep(4);
            Utility.ByLinkText("ClickOnSignInToViewYourBooking", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
            Utility.Sleep(4);

            Utility.CssToSetText("TextInEmailAddess", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
            Utility.CssToSetText("TextInPassword", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);

            Utility.CsstoClick("ClickOnSignInButton", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
            if (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextCity", "value", 2)) && string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextCity", "value", 2)))
            {

                string _actualerrorForEmailAddress = Utility.ByXpath("ValidationForEmailAddress", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                string _actualerrorForPassword = Utility.ByXpath("ValidationForPassword", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);

                Assert.AreEqual(ToViewYourBookingAllValidations[0], _actualerrorForEmailAddress);
                Assert.AreEqual(ToViewYourBookingAllValidations[1], _actualerrorForPassword);
            }
            else if ((string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextCity", "value", 2)) && (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextCity", "value", 2)))))
            {
                string _actualerrorForEmailAddress = Utility.ByXpath("ValidationForEmailAddress", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Assert.AreEqual(ToViewYourBookingAllValidations[0], _actualerrorForEmailAddress);
            }
            else if ((!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextCity", "value", 2)) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextCity", "value", 2)))))
            {
                string _actualerrorForEmailAddress = Utility.ByXpath("ValidationForEmailAddress", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Assert.AreEqual(ToViewYourBookingAllValidations[2], _actualerrorForEmailAddress);

                string _actualerrorForPassword = Utility.ByXpath("ValidationForPassword", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Assert.AreEqual(ToViewYourBookingAllValidations[1], _actualerrorForPassword);
            }
            else if ((!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextCity", "value", 2)) && (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextCity", "value", 2)))))
            {
                string _actualerrorForEmailAddress = Utility.ByXpath("ValidationForEmailAddress", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Assert.AreEqual(ToViewYourBookingAllValidations[2], _actualerrorForEmailAddress);

                string _actualerrorForPassword = Utility.ByXpath("ValidationForPassword", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Assert.AreEqual(ToViewYourBookingAllValidations[1], _actualerrorForPassword);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            UserProfileSPA.Library.TestEnvironment.Dispose();
        }  
    }
}
