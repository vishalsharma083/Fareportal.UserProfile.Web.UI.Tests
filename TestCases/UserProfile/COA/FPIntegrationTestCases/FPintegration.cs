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

        [DeploymentItem("AppData\\SignInToViewYourBookingAllValidations.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\SignInToViewYourBookingAllValidations.csv", "SignInToViewYourBookingAllValidations#csv", DataAccessMethod.Sequential), TestMethod]
        public void SignInToViewYourBookingAllValidations()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            string _signInToViewYourBookingAllValidations = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("SignInToViewYourBookingAllValidations");
            string[] ToViewYourBookingAllValidations = _signInToViewYourBookingAllValidations.Split(",".ToCharArray());
            if (Prefix + SignInUrl == Driver.Url)
            {
                Utility.ByLinkTexttoClick("ClickOnMyBookingLink", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.Sleep(4);
                Utility.XPathtoClick("ClickOnSignInToViewYourBooking", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.Sleep(4);
                if (Prefix + Record("MyBookingUrl") == Driver.Url)
                {
                    Utility.CssToSetText("TextInEmailAddess", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                    Utility.CssToSetText("TextInPassword", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);

                    Utility.CsstoClick("ClickOnSignInButton", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                    //if ((string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInEmailAddess", "value", 6))) && 
                    if ((string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInPassword", "value", 6))))
                    {

                        string _actualerrorForEmailAddress = Utility.ByXpath("ValidationForEmailAddress", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                        string _actualerrorForPassword = Utility.ByXpath("ValidationForPassword", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);

                        Assert.AreEqual(ToViewYourBookingAllValidations[0], _actualerrorForEmailAddress);
                        Assert.AreEqual(ToViewYourBookingAllValidations[1], _actualerrorForPassword);
                    }
                    else if ((string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInEmailAddess", "value", 2)) && (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInPassword", "value", 2)))))
                    {
                        string _actualerrorForEmailAddress = Utility.ByXpath("ValidationForEmailAddress", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                        Assert.AreEqual(ToViewYourBookingAllValidations[0], _actualerrorForEmailAddress);
                    }
                    else if ((!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInEmailAddess", "value", 2)) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInPassword", "value", 2)))))
                    {
                        string _actualerrorForEmailAddress = Utility.ByXpath("ValidationForEmailAddress", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                        Assert.AreEqual(ToViewYourBookingAllValidations[2], _actualerrorForEmailAddress);

                        string _actualerrorForPassword = Utility.ByXpath("ValidationForPassword", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                        Assert.AreEqual(ToViewYourBookingAllValidations[1], _actualerrorForPassword);
                    }
                    else if ((!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInEmailAddess", "value", 2)) && (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInPassword", "value", 2)))))
                    {
                        string _actualerrorForEmailAddress = Utility.ByXpath("ValidationForEmailAddress", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                        Assert.AreEqual(ToViewYourBookingAllValidations[2], _actualerrorForEmailAddress);

                        string _actualerrorForPassword = Utility.ByXpath("ValidationForPassword", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                        Assert.AreEqual(ToViewYourBookingAllValidations[1], _actualerrorForPassword);
                    }
                }
                else
                {
                    Assert.IsTrue(false, "Cheapoair with Tabid 2885 is not opened.");
                }

            }
            else
            {
                Assert.IsTrue(false,"SignIn url is not opened.");
            }
        }

        [DeploymentItem("EmailAddressAlreadyExistInSignInToViewYourBooking.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\EmailAddressAlreadyExistInSignInToViewYourBooking.csv", "EmailAddressAlreadyExistInSignInToViewYourBooking#csv", DataAccessMethod.Sequential), TestMethod]
        public void EmailAddressAlreadyExist()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            string _signInToViewYourBookingAllValidations = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("SignInToViewYourBookingAllValidations");
            string[] ToViewYourBookingAllValidations = _signInToViewYourBookingAllValidations.Split(",".ToCharArray());
            if (Prefix + SignInUrl == Driver.Url)
            {
                Utility.ByLinkTexttoClick("ClickOnMyBookingLink", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.Sleep(4);
                Utility.XPathtoClick("ClickOnSignInToViewYourBooking", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.Sleep(4);
                if (Prefix + Record("MyBookingUrl") == Driver.Url)
                {
                    Utility.CssToSetText("TextInEmailAddess", Record("EnterAlreadyExistEmail"), 3);
                    Utility.XpathToSetText("TextInPassword",Record("Password"),9);
                    Utility.CsstoClick("ClickOnCreateAnAccountBtnSignUpFree", 4);
                    Utility.Sleep(15);
                    string _actualerrorForEmailAddress = Utility.ByXpath("ErrorInEmailAllreadyExist", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                 
                    string expectedWhenEmailIsAlreadyExist = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("expectedWhenEmailIsAlreadyExist");
                    Assert.AreEqual(ToViewYourBookingAllValidations[3], _actualerrorForEmailAddress);
                }
                else
                {
                    Assert.IsTrue(false, "SignUpUrl is not opened.");
                }
            }
            else
            {
                Assert.IsTrue(false, "SignInUrl is not opened.");
            }
        }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\RegisterForFreeAllvalidations.csv", "RegisterForFreeAllvalidations#csv", DataAccessMethod.Sequential), DeploymentItem("RegisterForFreeAllvalidations.csv"), TestMethod]
        public void RegisterForFreeAllvalidations()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            if (Prefix + SignInUrl == Driver.Url)
            {               
                Utility.ByLinkTexttoClick("ClickOnMyBookingLink", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.Sleep(4);
                if (Prefix + Record("MyBookingUrl") == Driver.Url)
                {
                    string _createMyAccountAllValidation = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("CreateMyAccountAllValidation");
                    string[] CreateMyAccountAllValidation = _createMyAccountAllValidation.Split(",".ToCharArray());

                    Utility.XPathtoClick("ClickOnSignInToViewYourBooking", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                    Utility.Sleep(4);

                    Utility.CssToSetText("TextEmailAddressInRegister", Record("Email"), 3);
                    Utility.CssToSetText("TextPasswordInRegister", Record("Password"), 3);
                    Utility.CssToSetText("TextInRetypePassword", Record("ConfrmPassword"), 3);
                   
                    Utility.CsstoClick("ClickOnCreateMyAcccountBtn", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);              


                    if (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextEmailAddressInRegister", "value", 3)) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextEmailAddressInRegister", "value", 3))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInRetypePassword", "value", 3)))) 
                    {
                        string _actualErrorInRegisterEmailAddress = Utility.ByXpath("ErrorInRegisteremailAddress", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                        Assert.AreEqual(CreateMyAccountAllValidation[0], _actualErrorInRegisterEmailAddress);

                        string _actualErrorInRegisterPassword = Utility.ByXpath("ErrorInRegisterPassword", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                        Assert.AreEqual(CreateMyAccountAllValidation[1], _actualErrorInRegisterPassword);

                        string _actualErrorInRegisterConfrmPassword = Utility.ByXpath("ErrorInRegistetConFrmPassword", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                        Assert.AreEqual(CreateMyAccountAllValidation[2], _actualErrorInRegisterConfrmPassword);
                    
                    }                                   
                }
            }
        }
        [TestCleanup]
        public void Cleanup()
        {
            UserProfileSPA.Library.TestEnvironment.Dispose();
        }  
    }
}
