using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using UserProfileSPA.Library;
using OpenQA.Selenium.Firefox;
using System.Configuration;



namespace UserProfileSPA.TestCases
{
    [TestClass]
    public class SignIn 
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

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\PasswordNotMatchingValidation.csv", "PasswordNotMatchingValidation#csv", DataAccessMethod.Sequential), DeploymentItem("UserProfileSPA\\PasswordNotMatchingValidation.csv"), TestMethod]

        public void PasswordNotMatchingValidation()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            
            if (Prefix + SignInUrl == Driver.Url)
            {
                Utility.Sleep(2);
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CsstoClick("SignInBtn", 3);
                string[] expextedPasswordNotMatchingValidation = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("expectedPasswordNotMatchingValidationError").Split("\r\n".ToCharArray());
                string actualErr = Utility.ByCss("PasswordNotMatchingValidation", 3);
                string[] actualPasswordNotMatchingValidation = actualErr.Split("\r\n".ToCharArray());
                int i = 0;
                foreach (var item in actualPasswordNotMatchingValidation)
                {
                    Utility.Sleep(5);
                    Assert.AreEqual(expextedPasswordNotMatchingValidation[i], item);
                    i++;
                }
            }
            else
            {
                Assert.IsTrue(false,"SignIn url is not opened.");
            }

        }

        [DeploymentItem("SignIn.csv"), DeploymentItem("AppData\\SignIn.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\SignIn.csv", "SignIn#csv", DataAccessMethod.Sequential), TestMethod]
        public void SignInSucessfully()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
            Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
            if (Prefix + SignInUrl == Driver.Url)
            {
                Utility.CsstoClick("SignInBtn", 2);
                string _overViewUrl = Record("OverViewUrl");
                Utility.Sleep(4);
                if (Prefix + _overViewUrl == Driver.Url)
                {
                    string expectedName = Utility.ByXpath("TravelerNameAccount", 3);
                    string[] _expectedName = expectedName.Split(" ".ToCharArray());
                    string actualName = Utility.ByXpath("OverViewWelcome", 3);
                    string[] _actualName = actualName.Split(" ".ToCharArray());
                    Assert.AreEqual(_expectedName[0], _actualName[1]);
                }
                else
                {
                    Assert.IsTrue(false,"OverView Url is not opened.");
                }
            }
            else
            {
                Assert.IsTrue(false,"Problem seems in SignIn page.");
            }
        }


        [DeploymentItem("AppData\\FaceBookSignIn.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\FaceBookSignIn.csv", "FaceBookSignIn#csv", DataAccessMethod.Sequential), TestMethod]
        public void FaceBookSignIn()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            //string UserProfileURL = Record("UserProfileURL");
            if (Prefix + SignInUrl  == Driver.Url)
            {
                Utility.XPathtoClick("SignInWithFaceBook", 3);
                if (Driver.WindowHandles.Count > 1)
                {
                    Driver.SwitchTo().Window(Driver.WindowHandles[1]);
                    Driver.Manage().Window.Maximize();
                    string FbEmailorPhoneNo = Record("FbEmailorPhoneNo");
                    string Password = Record("Password");
                    Utility.CssToSetText("FbEmailorPhoneNo", Record("FbEmailorPhoneNo"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                    Utility.CssToSetText("FbPassword", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                    Utility.XPathtoClick("FaceBookLoginBtn", 3);
                    Driver.SwitchTo().Window(Driver.WindowHandles[0]);

                    Utility.Sleep(4);
                    string fareportalOverviewUrl = Record("OverviewUrl");
                    Utility.Sleep(7);
                    if (Prefix + fareportalOverviewUrl == Driver.Url)
                    {
                        string actualCompleteFbName = Utility.ByCss("FbAccountsCompleteName", 5);
                        string expectedCompleteFbName = Record("ExpectedCompleteFbName");
                        Assert.AreEqual(expectedCompleteFbName, actualCompleteFbName);
                    }
                    else
                    {
                        throw new Exception("Overview page is not opened.");
                    }
                }
                else
                {
                    throw new Exception("facebook window is not opened.");
                }
            }
            else
            {
                throw new Exception("Signin page is not opened.");
            }
        }


        [DeploymentItem("AppData\\SignInWithGoogle.csv"), DeploymentItem("SignInWithGoogle.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\SignInWithGoogle.csv", "SignInWithGoogle#csv", DataAccessMethod.Sequential), TestMethod]
        public void SignInWithGoogle()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            //string UserProfileURL = Record("UserProfileURL");
            if (SignInUrl + Prefix == Driver.Url)
            {
                Utility.XPathtoClick("signInWithGoogle", 6);
                if (Driver.WindowHandles.Count > 1)
                {
                    Driver.SwitchTo().Window(Driver.WindowHandles[1]);
                    Driver.Manage().Window.Maximize();
                    string GoogleEmail = Record("GoogleEmail");
                    string GooglePassword = Record("GooglePassword");

                    Utility.CssToSetText("GoogleEmail", Record("GoogleEmail"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                    Utility.CssToSetText("GooglePassword", Record("GooglePassword"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                    Utility.CsstoClick("GoogleLoginBtn", 6);
                    Driver.SwitchTo().Window(Driver.WindowHandles[0]);
                    Utility.Sleep(4);
                    string fareportalOverviewUrl = Record("OverviewUrl");
                    Utility.Sleep(7);
                    if (Prefix + fareportalOverviewUrl == Driver.Url)
                    {
                        string actualCompleteGoogleName = Utility.ByCss("GoogleAccountsCompleteName", 5);
                        string expectedCompleteGoogleName = Record("ExpectedCompleteGoogleName");
                        Assert.AreEqual(expectedCompleteGoogleName, actualCompleteGoogleName);
                    }
                    else
                    {
                        throw new Exception("Overview page is not opened.");
                    }
                }
                else
                {
                    throw new Exception("Google SignIn window is not opened.");
                }
            }
            else
            {
                throw new Exception("Signin page is not opened.");
            }
        }


        public void ValidatingForgetPassword()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            Utility.ByLinkTexttoClick("ClickOnForgotyourpassword", 5);
            Utility.CsstoClick("ClickOnRecoverMyPassword", 5);
            Utility.Sleep(4);

            string expectederror = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("ForgetPasswordValidation");
            if (Utility.IsDisplayedUsingXpath("ForgetPasswordValidation"))
            {
                string actualerror = Utility.ByXpath("ForgetPasswordValidation", 5);
                Assert.AreEqual(expectederror, actualerror);
            }
        }


        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\ValidatingSignIn.csv", "ValidatingSignIn#csv", DataAccessMethod.Sequential), DeploymentItem("ValidatingSignIn.csv"), TestMethod]
        public void ValidatingSignIn()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            //string email = Record("Email");
            //string pass = Record("Password");
            Utility.CssToSetText("Email", Record("Email"), 4);
            Utility.CssToSetText("Password", Record("Password"), 4);
            Utility.CsstoClick("SignInBtn", 9);
            Utility.Sleep(3);

            if (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("Email", "value", 2)) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("Password", "value", 2))))
            {
                string expectedEmailErrorWhenBlank = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("expectedEmailErrorWhenBlank");
                string expectedPasswordErrorWhenBlank = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("expectedPasswordErrorWhenBlank");

                string actualEmailError = (Utility.ByXpath("SignInValidationErrors", 2));
                Assert.AreEqual(expectedEmailErrorWhenBlank, actualEmailError);
                string actualPasswordError = (Utility.ByXpath("PasswordValidationError", 2));
                Assert.AreEqual(expectedPasswordErrorWhenBlank, actualPasswordError);
            }
            else if (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("Email", "value", 2)) && (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("Password", "value", 2))))
            {
                string expectedEmailErrorWhenWrong = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("expectedEmailErrorWhenWrong");
                string actualEmailError = (Utility.ByXpath("SignInValidationErrors", 2));
                Assert.AreEqual(expectedEmailErrorWhenWrong, actualEmailError);
            }
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\VerifyingRememberMe.csv", "VerifyingRememberMe#csv", DataAccessMethod.Sequential), DeploymentItem("VerifyingRememberMe.csv"), TestMethod]
        public void VerifyingRememberMe()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            //string _baseUrl = Record("SignInUrl");
            Utility.Sleep(7);
            if (SignInUrl + Prefix == Driver.Url)
            {               
                Utility.CssToSetText("Email", Record("Email"), 4);
                Utility.CssToSetText("Password", Record("Password"), 4);
                Utility.CsstoClick("rememberMe", 3);
                Utility.CsstoClick("SignInBtn", 9);
                Utility.Sleep(7);

                string _overViewUrl = Record("OverViewUrl");
                if (Prefix + _overViewUrl == Driver.Url)
                {
                    string expectedName = Utility.ByXpath("TravelerNameAccount", 3);
                    string[] _expectedName = expectedName.Split(" ".ToCharArray());
                    string actualName = Utility.ByCss("OverViewWelcomeName", 6);
                    string[] _actualName = actualName.Split(" ".ToCharArray());
                    Assert.AreEqual(_expectedName[0], _actualName[1]);
                    Utility.Sleep(6);
                    string preUrlValue = Driver.Url;                   
                    Driver.Close();                    
                    Driver = new FirefoxDriver();
                    Driver.Navigate().GoToUrl(SignInUrl + Prefix);
                    Driver.Manage().Window.Maximize();
                     Utility.Sleep(6);
                     if (SignInUrl + Prefix != preUrlValue)
                     {
                         if (Prefix + _overViewUrl == Driver.Url)
                         {
                             Assert.AreEqual(_expectedName[0], _actualName[1]);
                         }
                         else
                         {
                             Assert.IsTrue(false, "OverViewUrl is not opened.");
                         }
                     }
                     else
                     {
                         Assert.IsTrue(false,"Should not open SignIn page.");
                     }
                }
                else
                {
                    Assert.IsTrue(false, "OverViewUrl is not opened.");
                }

            }
            else
            {
                Assert.IsTrue(false, "SignInUrl is not opened.");
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            UserProfileSPA.Library.TestEnvironment.Dispose();
        }    
    }
}
