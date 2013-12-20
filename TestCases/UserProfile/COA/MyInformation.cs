using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using UserProfileSPA.Library;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Firefox;
using System.Configuration;

namespace UserProfileSPA.TestCases
{
    [TestClass]
    public class MyInformation
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

        [DeploymentItem("CheckBoxUseForBillingDetails.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\CheckBoxUseForBillingDetails.csv", "CheckBoxUseForBillingDetails#csv", DataAccessMethod.Sequential), TestMethod]
        public void CheckBoxUseForBillingDetails()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
            Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);

            string _billingUrl = Record("MyBillingUrl");

            string signinUrl = Record("SignInUrl");
            if (signinUrl == Driver.Url)
            {
                Utility.CsstoClick("SignInBtn", 4);
                Utility.Sleep(3);

                string _overViewUrl = Record("OverviewUrl");
                if (_overViewUrl == Driver.Url)
                {
                    Utility.CsstoClick("clickOnMyInformation", 4);
                    Utility.Sleep(2);

                    string CheapoairMyDetailsUrl = Record("CheapoairMyDetailsUrl");
                    if (CheapoairMyDetailsUrl == Driver.Url)
                    {
                        string IscheckedMyInfoPage = Utility.GrabAttributeValueByCss("UseforBillingDetailsCheckBoxInMyDetailsPage", "Checked", 4);
                        if (IscheckedMyInfoPage == "true")
                        {
                            string actualAddressLine1 = Utility.GrabAttributeValueByCss("TextAddressOne", "value", 4);
                            string actualCity = Utility.GrabAttributeValueByCss("TextCity", "value", 4);
                            string actualCountryCode = Utility.GrabAttributeValueByCss("Country", "value", 4);

                            Utility.ByLinkTexttoClick("ClickOnMyBillingDetails", 3);
                            if (_billingUrl == Driver.Url)
                            {

                                string ExistingAddressCheckBoxInBillingPage = Utility.GrabAttributeValueByCss("ExistingAddressCheckBoxInBillingPage", "Checked", 4);
                                if (ExistingAddressCheckBoxInBillingPage == "true")
                                {

                                    string AddresssShowingAfterAddingCard = Utility.ByXpath("AddresssShowingAfterAddingCard", 4);
                                    string[] ExistingAddressInPersonalInfo = AddresssShowingAfterAddingCard.Split(",".ToCharArray());
                                    string[] existingCity = ExistingAddressInPersonalInfo[1].Remove(0, 0).Split("".ToCharArray());
                                    string[] existingCode = ExistingAddressInPersonalInfo[2].Remove(0, 0).Split("".ToCharArray());
                                    Assert.AreEqual(actualAddressLine1, ExistingAddressInPersonalInfo[0]);
                                    Assert.AreEqual(actualCity, existingCity[1]);
                                    Assert.AreEqual(actualCountryCode, existingCode[1]);
                                }
                            }
                        }
                        else if (IscheckedMyInfoPage == null)
                        {
                            Utility.ByLinkTexttoClick("ClickOnMyBillingDetails", 3);
                            string ExistingAddressCheckBoxInBillingPage = Utility.GrabAttributeValueByCss("ExistingAddressCheckBoxInBillingPage", "Checked", 4);
                            if (ExistingAddressCheckBoxInBillingPage == null)
                            {
                                if ((string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("textaddressLine1InBillingDetails", "value", 2))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("textcityInBillingdetails", "value", 2))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("textzipInBillingDetails", "value", 2)))) //&& (string.IsNullOrEmpty(GrabAttributeValueByCss("textBillingphoneNumbers", "value", 2))))
                                {
                                    string expectedCountryValue = Record("ExpectedCountryValue");
                                    string expectedStateValue = Record("ExpectedStateValue");
                                    var element1 = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("BillingAddressCountry")));
                                    var existingValue = new SelectElement(element1);
                                    string CurrentlySelectedCountryValue = existingValue.SelectedOption.Text;

                                    var element2 = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("BillingAddressState")));
                                    var existingValueInState = new SelectElement(element2);
                                    string CurrentlySelectedStateValue = existingValueInState.SelectedOption.Text;

                                    Assert.AreEqual(expectedCountryValue, CurrentlySelectedCountryValue);
                                    Assert.AreEqual(expectedStateValue, CurrentlySelectedStateValue);
                                }
                            }
                        }
                    }
                }
            }
        }

        [DeploymentItem("CheckForZipCodesValidation.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\CheckForZipCodesValidation.csv", "CheckForZipCodesValidation#csv", DataAccessMethod.Sequential), TestMethod]
        public void CheckForZipCodesValidation()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            string _baseUrl = Record("SignInUrl");
            if (_baseUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CsstoClick("SignInBtn", 3);
                string _zipCode = Record("ZipCode");
                string Country = Record("Country");

                Utility.Sleep(2);
                Utility.CsstoClick("clickOnMyInformation", 4);
                Utility.Sleep(2);
                var element1 = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("Country")));
                var selectElement1 = new SelectElement(element1);
                selectElement1.SelectByText(Country);
                if ((Country == "Canada") || (Country == "United Kingdom"))
                {
                    Utility.CsstoClear("Zip", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                    Utility.CssToSetText("Zip", _zipCode, 3);
                    string zipText = Utility.GrabAttributeValueByCss("Zip", "value", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                    if (!string.IsNullOrEmpty(zipText))
                    {
                        Regex r = new Regex("^[a-zA-Z0-9 ]{7}$");
                        if (r.IsMatch(zipText))
                        {
                            Regex r1 = new Regex("^[a-zA-Z0-9 ]+$");
                            if (r1.IsMatch(zipText))
                            {
                                Assert.IsTrue(true);
                                //Utility.CssToSetText("Zip", _zipCode, 3);
                            }
                        }
                        else
                        {
                            string expectedErrMgs = Record("ExpectedErrMgs");
                            if (expectedErrMgs != "No Error")
                            {
                                Utility.CsstoClick("SaveInformationBtn", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                                string actualErrMsg = Utility.ByXpath("MyInfoZipCodeErrorMessage", 3);
                                Assert.AreEqual(expectedErrMgs, actualErrMsg);
                            }
                        }
                    }
                    else
                    {
                        string expectedErrMgs = Record("ExpectedErrMgs");
                        if (expectedErrMgs != "No Error")
                        {
                            Utility.CsstoClick("SaveInformationBtn", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                            string actualErrMsg = Utility.ByXpath("MyInfoZipCodeErrorMessage", 3);
                            Assert.AreEqual(expectedErrMgs, actualErrMsg);
                        }
                    }
                }
                //This if is for India or for other remaining countries.
                else if (Country == "India")
                {
                    Regex r = new Regex("^[0-9]*$");
                    if (!string.IsNullOrEmpty(_zipCode))
                    {
                        if (r.IsMatch(_zipCode))
                        {
                            //int otherZip = _zipCode.Length;//Convert.ToInt32(_zipCode);
                            if (_zipCode.Length > 4)
                            {
                                Utility.CssToSetText("Zip", _zipCode, 3);
                            }
                            else
                            {
                                string expectedErrMgs = Record("ExpectedErrMgs");
                                if (expectedErrMgs != "No Error")
                                {
                                    Utility.CsstoClick("SaveInformationBtn", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                                    string actualErrMsg = Utility.ByXpath("MyInfoZipCodeErrorMessage", 3);
                                    Assert.AreEqual(expectedErrMgs, actualErrMsg);
                                }
                            }
                        }
                    }
                    else
                    {
                        string expectedErrMgs = Record("ExpectedErrMgs");
                        if (expectedErrMgs != "No Error")
                        {
                            Utility.CsstoClick("SaveInformationBtn", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                            string actualErrMsg = Utility.ByXpath("MyInfoZipCodeErrorMessage", 3);
                            Assert.AreEqual(expectedErrMgs, actualErrMsg);
                        }
                    }
                }

                // Utility.CsstoClick("SaveInformationBtn", 3);
            }
        }


        [DeploymentItem("VerifyTheValuesInFirstNameAndLastName.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\VerifyTheValuesInFirstNameAndLastName.csv", "VerifyTheValuesInFirstNameAndLastName#csv", DataAccessMethod.Sequential), TestMethod]
        public void CheckTheValuesInFirstNameAndLastName()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            string _baseUrl = Record("SignInUrl");
            if (_baseUrl == Driver.Url)
            {
                Utility.XPathtoClick("ClickOnCreateAnAccount", 4);
                string _signUpUrl = Record("SignUpUrl");
                if (_signUpUrl == Driver.Url)
                {
                    Random randomNum = new Random();
                    int num = randomNum.Next(5, 100);

                    string emailAddress = num + Record("EmailAddress");
                    string password = num + Record("Password");
                    Utility.CssToSetText("TextInEmail", emailAddress, UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                    Utility.CssToSetText("TextInPassword", password, UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                    string confrmPassword = Record("ConfrmPassword");
                    string _confrmPassword = num + confrmPassword;
                    string _firstName = Record("FirstName");
                    string _lastName = Record("LastName");

                    Utility.CssToSetText("TextInConfrmPassword", _confrmPassword, 4);
                    Utility.CssToSetText("CreateAnAccountTextInFirstName", _firstName, 4);
                    Utility.CssToSetText("CreateAnAccountTextInLastName", _lastName, 4);

                    string dobMonth = Record("DobMonth");
                    var element1 = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("DobMonth")));
                    var selectElement1 = new SelectElement(element1);
                    selectElement1.SelectByText(dobMonth);

                    string dobDay = Record("DobDay");
                    var element2 = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DobDay")));
                    var selectElement2 = new SelectElement(element2);
                    selectElement2.SelectByText(dobDay);

                    string dobyear = Record("Dobyear");
                    var element3 = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DobYear")));
                    var selectElement3 = new SelectElement(element3);
                    selectElement3.SelectByText(dobyear);

                    string gender = Record("Gender");
                    var element4 = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("SelectGender")));
                    var selectElement4 = new SelectElement(element4);
                    selectElement4.SelectByText(gender);

                    Utility.CsstoClick("ClickOnSignUpBtn", 3);

                    if (Utility.IsDisplayedUsingXpath("EmailAlreadyExistMessage"))
                    {
                        string _emailAddressUnique = num + emailAddress + num;

                        string _overViewPage = Record("OverViewPage");
                        if (_overViewPage == Driver.Url)
                        {
                            Utility.CsstoClick("clickOnMyInformation", 4);
                            Utility.Sleep(2);
                            string _verifyFirstName = Utility.GrabAttributeValueByCss("FirstName", "value", 4);
                            Assert.AreEqual(_firstName, _verifyFirstName);
                            string _verifyLastName = Utility.GrabAttributeValueByCss("LastName", "value", 4);
                            Assert.AreEqual(_lastName, _verifyLastName);
                            string _veyfyEmail = Utility.GrabAttributeValueByCss("SignInInformationEmailAddress", "value", 4);
                            Assert.AreEqual(_emailAddressUnique, _veyfyEmail);


                            IWebElement _verifyDobMonth = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DobMonth")));
                            var selectElement = new SelectElement(_verifyDobMonth);
                            string actualDobMonth = selectElement.SelectedOption.Text;
                            Assert.AreEqual(dobMonth, actualDobMonth);


                            IWebElement _verifyDobDay = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DobDay")));
                            var selectElementOfDay = new SelectElement(_verifyDobDay);
                            string actualDobDay = selectElementOfDay.SelectedOption.Text;
                            Assert.AreEqual(dobDay, actualDobDay);


                            IWebElement _verifyDobyear = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DobDay")));
                            var selectElementOfYear = new SelectElement(_verifyDobyear);
                            string actualDobYear = selectElementOfYear.SelectedOption.Text;
                            Assert.AreEqual(dobyear, actualDobYear);
                            Utility.Sleep(10);
                            Utility.CsstoClick("ClickOnWelcomeDropdown", 4);
                            Utility.Sleep(2);
                            Utility.CsstoClick("ClickOnSignOut", 4);

                            Driver.Close();
                            Driver.Navigate().GoToUrl(_baseUrl);
                            Utility.CssToSetText("Email", emailAddress, UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                            Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);

                            Utility.CsstoClick("SignInBtn", 2);
                            Assert.AreEqual(emailAddress, _veyfyEmail);
                        }
                        else
                        {
                            Assert.IsTrue(false, "OverView Page is not loaded.");
                        }
                    }
                    else
                    {
                        Utility.Sleep(5);
                        string _overViewPage = Record("OverViewPage");
                        Utility.Sleep(6);
                        if (_overViewPage == Driver.Url)
                        {
                            Utility.CsstoClick("clickOnMyInformation", 4);
                            Utility.Sleep(2);
                            string _verifyFirstName = Utility.GrabAttributeValueByCss("FirstName", "value", 4);
                            Assert.AreEqual(_firstName, _verifyFirstName);
                            string _verifyLastName = Utility.GrabAttributeValueByCss("LastName", "value", 4);
                            Assert.AreEqual(_lastName, _verifyLastName);
                            string _veyfyEmail = Utility.GrabAttributeValueByCss("SignInInformationEmailAddress", "value", 4);
                            Assert.AreEqual(emailAddress, _veyfyEmail);


                            IWebElement _verifyDobMonth = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DobMonth")));
                            var selectElement = new SelectElement(_verifyDobMonth);
                            string actualDobMonth = selectElement.SelectedOption.Text;
                            Assert.AreEqual(dobMonth, actualDobMonth);


                            IWebElement _verifyDobDay = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DobDay")));
                            var selectElementOfDay = new SelectElement(_verifyDobDay);
                            string actualDobDay = selectElementOfDay.SelectedOption.Text;
                            Assert.AreEqual(dobDay, actualDobDay);


                            IWebElement _verifyDobyear = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DobYear")));
                            var selectElementOfYear = new SelectElement(_verifyDobyear);
                            string actualDobYear = selectElementOfYear.SelectedOption.Text;
                            Assert.AreEqual(dobyear, actualDobYear);
                            Utility.Sleep(10);
                            Utility.CsstoClick("ClickOnWelcomeDropdown", 4);
                            Utility.Sleep(2);
                            Utility.CsstoClick("ClickOnSignOut", 4);

                            string prev = Driver.Url;
                            Driver.Close();
                            Driver = new FirefoxDriver();
                            Driver.Navigate().GoToUrl(_baseUrl);
                            Driver.Manage().Window.Maximize();
                            Utility.Sleep(6);
                            Driver.FindElement(By.CssSelector("input[id='txtUserName']")).SendKeys(emailAddress);
                            Driver.FindElement(By.CssSelector("input[id='txtPassword']")).SendKeys(password);
                            Driver.FindElement(By.CssSelector("input[id='btnSignIn']")).Click();
                            //Utility.CssToSetText("Email", emailAddress, UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                            //Utility.CssToSetText("Password", password, UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);                           
                            //Utility.CsstoClick("SignInBtn", 2);
                            Assert.AreEqual(emailAddress, _veyfyEmail);
                        }
                        else { Assert.IsTrue(false, "OverViewUrl is not opened."); }
                    }
                }
                else { Assert.IsTrue(false, "SignUpUrl is not opened."); }
            }
            else { Assert.IsTrue(false, "SignInUrl is not opened."); }
        }


        [DeploymentItem("MyInformationsAllValidations.csv"), DeploymentItem("AppData\\MyInformationsAllValidations.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MyInformationsAllValidations.csv", "MyInformationsAllValidations#csv", DataAccessMethod.Sequential), TestMethod]
        public void MyInformationsAllValidations()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
           
            if (Prefix+SignInUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
           
                Utility.CsstoClick("SignInBtn", 4);
                Utility.Sleep(3);
                if (Prefix+Record("verViewUrl") == Driver.Url)
                {
                    Utility.CsstoClick("clickOnMyInformation", 4);
                    Utility.Sleep(2);
                    if (Prefix+Record("MyInformationUrl") == Driver.Url)
                    {
                        Utility.CsstoClick("SaveInformationBtn", 4);
                        Utility.Sleep(1);
                        string expectedValidationsIfMyInfoisEmpty = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("expectedValidationsIfMyInfoisEmpty");
                        string[] ValidationsIfMyInfoisEmpty = expectedValidationsIfMyInfoisEmpty.Split(".".ToCharArray());

                        if (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextAddressOne", "value", 2)) && string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextCity", "value", 2)) && string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextZipcode", "value", 2)) && string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextContactNumaber", "value", 2)))
                        {
                            string AirLine1Validation = Utility.ByXpath("AirLine1Validation", 4);
                            Assert.AreEqual(ValidationsIfMyInfoisEmpty[1], AirLine1Validation);

                            string CityNamemissingValidation = Utility.ByXpath("CityNamemissingValidation", 4);
                            Assert.AreEqual(ValidationsIfMyInfoisEmpty[2], CityNamemissingValidation);

                            string ZipCodeIsMissingValidation = Utility.ByXpath("ZipCodeIsMissingValidation", 4);
                            Assert.AreEqual(ValidationsIfMyInfoisEmpty[3], ZipCodeIsMissingValidation);

                            string ContactNoIsMissingValidation = Utility.ByXpath("ContactNoIsMissingValidation", 4);
                            Assert.AreEqual(ValidationsIfMyInfoisEmpty[4], ContactNoIsMissingValidation);
                        }
                        else
                        {
                            throw new Exception("May be all text boxes are not empty.");
                        }
                    }
                    else
                    {
                        Assert.IsTrue(false, "MyInformation url is not opened.");
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


       [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\SignInInformationInMyInformation.csv", "SignInInformationInMyInformation#csv", DataAccessMethod.Sequential), DeploymentItem("AppData\\SignInInformationInMyInformation.csv"), TestMethod]
        public void SignInInformation()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            if (Prefix+SignInUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(5);
                if (Prefix+Record("OverViewUrl") == Driver.Url)
                {
                    Utility.CsstoClick("clickOnMyInformation", 4);
                    Utility.Sleep(2);
                    if (Prefix+Record("MyInformationUrl") == Driver.Url)
                    {
                        string myInformationEmail = Utility.GrabAttributeValueByCss("SignInInformationEmailAddress", "value", 4);
                        Assert.AreEqual(Record("Email"), myInformationEmail, "Email address is matched");
                        Utility.Sleep(2);
                        string _newpassword = Record("NewPassword");
                        string _confrmpassword = Record("ConfrmPassword");
                        Utility.CsstoClick("ClickOnEmailGrey", 3);
                        Utility.CssToSetText("NewPassword", _newpassword, 4);
                        Utility.CssToSetText("ConfrmPassword", _confrmpassword, 4);
                        Utility.CsstoClick("ClickOnConfrmPasswordCheckBox", 4);
                        Utility.XPathtoClick("ClickOnDismissLink", 4);
                        Utility.Sleep(2);
                        Utility.CsstoClick("ClickOnWelcomeDropdown", 4);
                        Utility.Sleep(2);
                        Utility.CsstoClick("ClickOnSignOut", 4);
                        Utility.Sleep(5);
                        Driver.Close();
                        Driver = new FirefoxDriver();
                        Driver.Navigate().GoToUrl(Prefix+SignInUrl);
                        Driver.Manage().Window.Maximize();
                        Utility.Sleep(6);
                        if (Prefix+SignInUrl == Driver.Url)
                        {
                            Driver.FindElement(By.CssSelector("input[id='txtUserName']")).SendKeys(Record("Email"));
                            Driver.FindElement(By.CssSelector("input[id='txtPassword']")).SendKeys(_newpassword);
                            Driver.FindElement(By.CssSelector("input[id='btnSignIn']")).Click();
                            Utility.Sleep(4);
                            Assert.AreEqual(Prefix+Record("OverviewUrl"), Driver.Url);
                            Driver.FindElement(By.CssSelector("a[class='myAccount']")).Click();
                            Utility.Sleep(2);
                            if (Prefix+Record("MyInformationUrl") == Driver.Url)
                            {
                                Assert.AreEqual(Record("Email"), myInformationEmail, "Email address is matched");
                                Utility.Sleep(2);
                                string _prevpassword = Record("Password");
                                string _confrmPrevpassword = Record("Password");
                                Driver.FindElement(By.CssSelector("div[class='email_grey']")).Click();
                                Driver.FindElement(By.CssSelector("input[id='newPassword']")).SendKeys(_prevpassword);
                                Driver.FindElement(By.CssSelector("input[id='newConfirmPassword']")).SendKeys(_confrmPrevpassword);

                                Driver.FindElement(By.CssSelector("span[class='tick_icon saveEditedPrefBtn']")).Click();
                                Driver.FindElement(By.CssSelector("a[class='dropdown-toggle mgLft5']")).Click();
                                Driver.FindElement(By.CssSelector("a[id='btnLogOut']")).Click();

                            }

                            //Utility.CssToSetText("Email", Record("Email"), 4);
                            //Utility.CssToSetText("CreateAccountEmailAddress", newpassword, 4);
                            //Utility.CsstoClick("SignInBtn", 2);
                        }
                        else
                        {
                            Assert.IsTrue(false, "SignInUrl is not opened for second time.");
                        }
                    }
                    else
                    {
                        Assert.IsTrue(false, "MyDetaisUrl is not opened.");
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


        [DeploymentItem("SignInValidationInMyInformation.csv"), DeploymentItem("AppData\\SignInValidationInMyInformation.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\SignInValidationInMyInformation.csv", "SignInValidationInMyInformation#csv", DataAccessMethod.Sequential), TestMethod]
        public void SignInInformationValidation()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            if (Prefix + SignInUrl == Driver.Url)
            {
                Utility.Sleep(5);
                Utility.CssToSetText("Email", Record("Email"), 4);
                Utility.CssToSetText("Password", Record("Password"), 4);
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(3);
               
                    if (Prefix + Record("OverviewUrl") == Driver.Url)
                    {
                        Utility.CsstoClick("clickOnMyInformation", 4);
                        Utility.Sleep(2);
                        if (Prefix + Record("MyInformationUrl") == Driver.Url)
                        {
                            string myInformationEmail = Utility.GrabAttributeValueByCss("SignInInformationEmailAddress", "value", 4);
                            Assert.AreEqual(Record("Email"), myInformationEmail, "Email address is matched");
                            string _newpasswordvalidation = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("newpassworderror");
                            string _confrmpasswordvalidation = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("confrmpassworderror");
                            string[] confrmpasswordvalidation = _confrmpasswordvalidation.Split(",".ToCharArray());
                            Utility.CsstoClick("ClickOnEmailGrey", 3);
                            string _newpassword = Record("NewPassword");
                            string _confrmpassword = Record("ConfrmPassword");
                            Utility.CssToSetText("NewPassword", _newpassword, 4);
                            Utility.CssToSetText("ConfrmPassword", _confrmpassword, 4);

                            Utility.CsstoClick("ClickOnConfrmPasswordCheckBox", 4);
                            if ((string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("NewPassword", "value", 4))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("ConfrmPassword", "value", 4))))
                            {
                                string _blankpassworderror = Utility.ByXpath("PasswordErrorWhenBlank", 3);
                                Assert.AreEqual(_newpasswordvalidation, _blankpassworderror);
                                string _blankconfrmpassword = Utility.ByXpath("ConfrmPasswordWhenBlank", 3);
                                Assert.AreEqual(confrmpasswordvalidation[0], _blankconfrmpassword);
                            }
                            else if ((string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("NewPassword", "value", 4))) && (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("ConfrmPassword", "value", 4))))
                            {
                                string _blankpassworderror = Utility.ByXpath("PasswordErrorWhenBlank", 3);
                                Assert.AreEqual(_newpasswordvalidation, _blankpassworderror);
                                string _confrmpassworderror = Utility.ByXpath("ConfrmPasswordWhenBlank", 3);
                                Assert.AreEqual(confrmpasswordvalidation[1], _confrmpassworderror);
                            }
                            else if ((!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("NewPassword", "value", 4))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("ConfrmPassword", "value", 4))))
                            {
                                //string _blankpassworderror = Utility.ByXpath("BlankPasswordError", 3);
                                //Assert.AreEqual(_newpasswordvalidation, _blankpassworderror);
                                string _blankconfrmpassword = Utility.ByXpath("ConfrmPaswordErrorMsgWhenPasswordIsNotBlankOrBoth", 3);
                                Assert.AreEqual(confrmpasswordvalidation[0], _blankconfrmpassword);
                            }
                            else if ((!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("NewPassword", "value", 4))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("ConfrmPassword", "value", 4))))
                            {
                                //string _blankpassworderror = Utility.ByXpath("BlankPasswordError", 3);
                                //Assert.AreEqual(_newpasswordvalidation, _blankpassworderror);
                                string _blankconfrmpassword = Utility.ByXpath("ConfrmPaswordErrorMsgWhenPasswordIsNotBlankOrBoth", 3);
                                Assert.AreEqual(confrmpasswordvalidation[0], _blankconfrmpassword);
                            }

                        }
                        else
                        {
                            Assert.IsTrue(false, "MyInformationUrl is not opened.");
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


        [DeploymentItem("AppData\\StateMustBeDropDownAfterSelectingUSAndCanada.csv"), DeploymentItem("StateMustBeDropDownAfterSelectingUSAndCanada.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\StateMustBeDropDownAfterSelectingUSAndCanada.csv", "StateMustBeDropDownAfterSelectingUSAndCanada#csv", DataAccessMethod.Sequential), TestMethod]
        public void StateMustBeDropDownAfterSelectingUSAndCanada()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            if (Prefix+SignInUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(3);
                if (Prefix + Record("OverViewUrl") == Driver.Url)
                {
                    
                    Utility.Sleep(6);
                    Utility.CsstoClick("clickOnMyInformation", 4);
                    Utility.Sleep(2);
                    if (Prefix + Record("MyInformationUrl") == Driver.Url)
                    {
                        string Country = Record("Country");
                        string state = Record("State");
                        Utility.CsstoClear("City", 2);
                        if ((Country == "United States") || (Country == "Canada"))
                        {
                            var element1 = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("Country")));
                            var selectElement1 = new SelectElement(element1);
                            selectElement1.SelectByText(Country);

                            var element2 = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("SelectState")));
                            var selectElement2 = new SelectElement(element2);
                            selectElement2.SelectByText(state);
                        }
                        else
                        {
                            if (Utility.GrabAttributeValueByCss("State", "type", 2) == "text")
                            {

                                var element = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("Country")));
                                var selectElement = new SelectElement(element);
                                selectElement.SelectByText(Country);
                                Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("State"))).SendKeys(state);

                            }
                            else
                            {
                                throw new Exception("Still it containig dropdown.");
                            }
                        }
                        Utility.CsstoClick("SaveInformationBtn", 7);
                    }
                    else
                    {
                        Assert.IsTrue(false, "MyInformation url is not opened.");
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


        [DeploymentItem("VerifyTheEmailAddressField.csv"), DeploymentItem("AppData\\VerifyTheEmailAddressField.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\VerifyTheEmailAddressField.csv", "VerifyTheEmailAddressField#csv", DataAccessMethod.Sequential), TestMethod]
        public void VerifingTheEmailAddress()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            if (Prefix+SignInUrl == Driver.Url)
            {               
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(2);
                
                if (Prefix + Record("OverViewUrl") == Driver.Url)
                {
                    Utility.CsstoClick("clickOnMyInformation", 4);
                    Utility.Sleep(2);
                    if (Prefix + Record("MyInformationUrl") == Driver.Url)
                    {
                        string email = Record("Email");
                        string myInformationEmail = Utility.GrabAttributeValueByCss("SignInInformationEmailAddress", "value", 4);
                        Assert.AreEqual(email, myInformationEmail, "Email address is matched");
                    }
                    else
                    {
                        Assert.IsTrue(false, "MyInformation Url is not opened.");
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


        [DeploymentItem("AppData\\VerifyContactAndMobilePhoneFields.csv"), DeploymentItem("VerifyContactAndMobilePhoneFields.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\VerifyContactAndMobilePhoneFields.csv", "VerifyContactAndMobilePhoneFields#csv", DataAccessMethod.Sequential), TestMethod]
        public void VerifyContactAndMobilePhoneFields()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;           
            if (Prefix + SignInUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
               
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(6);
                string _overViewUrl = Record("OverViewUrl");
                if (Prefix + _overViewUrl == Driver.Url)
                {
                    Utility.CsstoClick("clickOnMyInformation", 4);
                    Utility.Sleep(2);
                    string _myInfoUrl = Record("MyInformationUrl");
                    if (Prefix + _myInfoUrl == Driver.Url)
                    {
                        string _contactPhone = Record("ContactPhone");
                        string _mobilePhone = Record("MobilePhone");
                        Utility.CsstoClear("TextContactNumaber", 6);
                        Utility.CsstoClear("MobilePhone", 6);
                        Utility.CssToSetText("TextContactNumaber", _contactPhone, 3);
                        Utility.XPathtoClick("CliclOnMoreinformation", 3);
                        Utility.CssToSetText("MobilePhone", _mobilePhone, 3);
                        Utility.XPathtoClick("CliclOnMoreinformation", 3);
                        string errorMsgForContact = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("errorMsgForContact");
                        string errorMsgForMobile = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("errorMsgForMobile");
                        string MobileNo = Utility.GrabAttributeValueByCss("MobilePhone", "value", 3);
                        string ContactNo = Utility.GrabAttributeValueByCss("TextContactNumaber", "value", 3);
                        string[] MsgForContact = errorMsgForContact.Split(",".ToCharArray());
                        
                        if ((Regex.IsMatch(ContactNo, @"^(?!\s*$).+") && (Regex.IsMatch(MobileNo, @"^(?!\s*$).+"))))
                        {
                            if ((Regex.IsMatch(ContactNo, @"^\d{7,15}$")) && (Regex.IsMatch(MobileNo, @"^\d{7,15}$"))) //It checks upto 15 digits - Not allowed Alphanumeric
                            {
                                Assert.IsTrue(true);
                            }
                            else
                            {
                                string actualContactMsg = Utility.ByXpath("actualContactMsg", 3);
                                string actualMobileMsg = Utility.ByXpath("actualMobileMsg", 3);
                                Assert.AreEqual(MsgForContact[0], actualContactMsg, "Please enter a valid contact phone");
                                Assert.AreEqual(errorMsgForMobile, actualMobileMsg, "Please enter a valid mobile phone");
                            }
                        }
                        else if ((Regex.IsMatch(ContactNo, @"^$")) && (Regex.IsMatch(MobileNo, @"^$")))
                        {
                            Utility.CsstoClick("SaveInformationBtn", 3);
                            string actualContactMsg = Utility.ByXpath("actualContactMsg", 3);
                            Assert.AreEqual(MsgForContact[1], actualContactMsg);

                        }
                        else if ((Regex.IsMatch(ContactNo, @"^(?!\s*$).+")) && (Regex.IsMatch(MobileNo, @"^$")))
                        {
                            if ((Regex.IsMatch(ContactNo, @"^\d{7,15}$")))
                            {
                                Assert.IsTrue(true);
                            }
                            else
                            {
                                string actualContactMsg = Utility.ByXpath("actualContactMsg", 3);
                                Assert.AreEqual(MsgForContact[0], actualContactMsg, "Please enter a valid contact phone");

                            }
                        }
                        else if ((Regex.IsMatch(ContactNo, @"^$")) && (Regex.IsMatch(MobileNo, @"^(?!\s*$).+")))
                        {
                            if ((Regex.IsMatch(MobileNo, @"^\d{7,15}$")))
                            {
                                Assert.IsTrue(true);
                            }
                            else
                            {
                                string actualMobileMsg = Utility.ByXpath("actualMobileMsg", 3);
                                Assert.AreEqual(errorMsgForMobile, actualMobileMsg);
                            }
                        }
                    }
                    else
                    {
                        Assert.IsTrue(false, "MyInformation Url is not opened.");
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


        [DeploymentItem("VerifyTheCityAndStateFields.csv"), DeploymentItem("AppData\\VerifyTheCityAndStateFields.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\VerifyTheCityAndStateFields.csv", "VerifyTheCityAndStateFields#csv", DataAccessMethod.Sequential), TestMethod]
        public void VerifyTheCityAndStateFields()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
           
            if (Prefix+SignInUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(5);
                string _overView = Record("OverView");
                if (Prefix + _overView == Driver.Url)
                {
                    
                    Utility.CsstoClick("clickOnMyInformation", 4);
                    string _myInformationUrl = Record("MyInformationUrl");
                    if (Prefix + _myInformationUrl == Driver.Url)
                    {
                        string city = Record("City");
                        string state = Record("State");
                        Utility.Sleep(2);
                        Utility.CsstoClear("City", 4);
                        //Utility.CsstoClear("State", 4);
                        Utility.CssToSetText("City", city, 2);
                        Utility.CssToSetText("State", state, 2);
                        Utility.CsstoClick("SaveInformationBtn", 3);
                        string expectedValidationsOfCityAndState = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("expectedValidationsOfCityAndState");
                        string[] ValidationsOfCityAndState = expectedValidationsOfCityAndState.Split(",".ToCharArray());


                        //string checkType = ;
                        if (Utility.GrabAttributeValueByCss("TextAddressOne", "type", 2) == "text")
                        {

                            if (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextCity", "value", 2)) && string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextCity", "value", 2)))
                            {

                                string CityNamemissingValidation = Utility.ByXpath("CityNamemissingValidation", 4);
                                Assert.AreEqual(ValidationsOfCityAndState[0], CityNamemissingValidation);

                                string StateNamemissingValidation = Utility.ByXpath("StateNamemissingValidation", 4);
                                Assert.AreEqual(ValidationsOfCityAndState[1], StateNamemissingValidation);

                            }
                            else
                            {
                                Assert.IsTrue(true, "May City or State text boxes are not empty.");
                            }
                        }
                        else
                        {
                            Assert.IsTrue(false, "CheckType is not text type.");
                        }
                    }
                    else
                    {
                        Assert.IsTrue(false, "MyInformation url is not opened.");
                    }
                }
                else
                {
                    Assert.IsTrue(false, "OverView url is not opened.");
                }
            }
            else
            {
                Assert.IsTrue(false, "SignInUrl is not opened.");
            }
        }


        [DeploymentItem("VerifyTheNameFieldValidations.csv"), DeploymentItem("AppData\\VerifyTheNameFieldValidations.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\VerifyTheNameFieldValidations.csv", "VerifyTheNameFieldValidations#csv", DataAccessMethod.Sequential), TestMethod]
        public void VerifyTheNameFieldValidations()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;

            if (Prefix + SignInUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(5);
                string _overViewUrl = Record("OverViewUrl");
                if (Prefix + _overViewUrl == Driver.Url)
                {
                    Utility.CsstoClick("clickOnMyInformation", 4);
                    Utility.Sleep(2);
                    string _myInfoUrl = Record("MyInformationUrl");
                    if (Prefix + _myInfoUrl == Driver.Url)
                    {
                        string _firstNametxt = Record("FirstName");
                        // char[] firstChar = _firstNametxt.ToCharArray();
                        string _lastNametxt = Record("LastName");
                        Utility.CsstoClear("FirstName", 3);
                        Utility.CssToSetText("FirstName", _firstNametxt, 3);
                        Utility.CsstoClear("LastName", 3);
                        Utility.CssToSetText("LastName", _lastNametxt, 3);
                        Utility.CsstoClick("SaveInformationBtn", 4);

                        if (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("FirstName", "value", 3)))
                        {
                            if ((_firstNametxt.Length > 2) && (_firstNametxt.Length < 25))
                            {
                                Regex regex = new Regex(@"^[a-zA-Z]");
                                if (regex.IsMatch(_firstNametxt))
                                {
                                    if ((_firstNametxt.Contains("!") || (_firstNametxt.Contains("@")) || (_firstNametxt.Contains("~")) || (_firstNametxt.Contains("$")) || (_firstNametxt.Contains("%")) || (_firstNametxt.Contains("^")) || (_firstNametxt.Contains("&")) || (_firstNametxt.Contains("*")) || (_firstNametxt.Contains("`")) || (_firstNametxt.Contains("+")) || (_firstNametxt.Contains("-")) || (_firstNametxt.Contains(":")) || (_firstNametxt.Contains(".")) || (_firstNametxt.Contains(",")) || (_firstNametxt.Contains("(")) || (_firstNametxt.Contains(")")) || (_firstNametxt.Contains("="))))
                                    {
                                        string expectedErrorMsg = Record("ExpectedErrorMsgForFirstname");
                                        if (expectedErrorMsg != "No Error")
                                        {
                                            string actualErrorMsg = Utility.ByXpath("ErrorMsgForFirstNameInMyInformation", 4);
                                            Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                                        }
                                        //throw new Exception("Name can only contain apostrophe, space or hyphen.");
                                    }
                                    else { Assert.IsTrue(true); }
                                    if ((_firstNametxt.Contains("'s") || (!_firstNametxt.Contains(" ")) || (!_firstNametxt.Contains("-"))))
                                    { Assert.IsTrue(true); }
                                    else { Assert.IsTrue(false); }
                                }
                                else
                                {
                                    string expectedErrorMsg = Record("ExpectedErrorMsgForFirstname");
                                    if (expectedErrorMsg != "No Error")
                                    {
                                        string actualErrorMsg = Utility.ByXpath("ErrorMsgForFirstNameInMyInformation", 4);
                                        Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                                    }

                                    //throw new Exception("Name must begin with a letter.");
                                }
                            }
                            else
                            {
                                string expectedErrorMsg = Record("ExpectedErrorMsgForFirstname");
                                if (expectedErrorMsg != "No Error")
                                {
                                    string actualErrorMsg = Utility.ByXpath("ErrorMsgForFirstNameInMyInformation", 4);
                                    Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                                }
                                //throw new Exception("FirstName should starts with big letter");
                            }
                        }
                        else
                        {
                            string expectedErrorMsg = Record("ExpectedErrorMsgForFirstname");
                            if (expectedErrorMsg != "No Error")
                            {
                                string actualErrorMsg = Utility.ByXpath("ErrorMsgForFirstNameInMyInformation", 4);
                                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                            }
                            // Assert.IsTrue(false, "Please enter first name");
                        }

                        if (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("LastName", "value", 3)))
                        {
                            if ((_lastNametxt.Length > 2) && (_lastNametxt.Length < 25))
                            {
                                Regex regex = new Regex(@"^[a-zA-Z]");
                                if (regex.IsMatch(_firstNametxt))
                                {
                                    if ((_lastNametxt.Contains("!") || (_lastNametxt.Contains("@")) || (_lastNametxt.Contains("~")) || (_lastNametxt.Contains("$")) || (_lastNametxt.Contains("%")) || (_lastNametxt.Contains("^")) || (_lastNametxt.Contains("&")) || (_lastNametxt.Contains("*")) || (_lastNametxt.Contains("`")) || (_lastNametxt.Contains("+")) || (_lastNametxt.Contains("_")) || (_lastNametxt.Contains(":")) || (_lastNametxt.Contains(".")) || (_lastNametxt.Contains(",")) || (_lastNametxt.Contains("(")) || (_lastNametxt.Contains(")")) || (_lastNametxt.Contains("="))))
                                    {
                                        string expectedErrorMsg = Record("ExpectedErrorMsgForLastname");
                                        string actualErrorMsg = Utility.ByXpath("ErrorMsgForLastNameInMyInformation", 4);
                                        Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                                        //throw new Exception("Name can only contain apostrophe, space or hyphen.");
                                    }
                                    else { Assert.IsTrue(true); }
                                    if ((_lastNametxt.Contains("'s") || (!_lastNametxt.Contains(" ")) || (!_lastNametxt.Contains("-"))))
                                    { Assert.IsTrue(true); }
                                }
                                else
                                {
                                    string expectedErrorMsg = Record("ExpectedErrorMsgForLastname");
                                    if (expectedErrorMsg != "No Error")
                                    {
                                        string actualErrorMsg = Utility.ByXpath("ErrorMsgForLastNameInMyInformation", 4);
                                        Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                                    }
                                    // throw new Exception("Name must begin with a letter.");
                                }
                            }
                            else
                            {
                                string expectedErrorMsg = Record("ExpectedErrorMsgForLastname");
                                if (expectedErrorMsg != "No Error")
                                {
                                    string actualErrorMsg = Utility.ByXpath("ErrorMsgForLastNameInMyInformation", 4);
                                    Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                                }
                                // throw new Exception("FirstName should starts with big letter");
                            }

                        }
                        else
                        {
                            string expectedErrorMsg = Record("ExpectedErrorMsgForLastname");
                            if (expectedErrorMsg != "No Error")
                            {
                                string actualErrorMsg = Utility.ByXpath("ErrorMsgForLastNameInMyInformation", 4);
                                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                            }
                            //Assert.IsTrue(false, "Please enter last name");
                        }
                    }
                    else
                    {
                        Assert.IsTrue(false, "MyIformation Url is not opened.");
                    }
                }
                else
                {
                    Assert.IsTrue(false, "OverView Url is not opened.");
                }
            }
            else
            {
                Assert.IsTrue(false, "SignInUrl is not opened.");
            }
        }


        [DeploymentItem("AppData\\VerifyTheTitleField.csv"), DeploymentItem("VerifyTheTitleField.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\VerifyTheTitleField.csv", "VerifyTheTitleField#csv", DataAccessMethod.Sequential), TestMethod]
        public void VerifyTheTitleField()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            //string _baseUrl = Record("SignInUrl");
            if (Prefix + SignInUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(2);
                string _overViewUrl = Record("OverViewUrl");
                if (Prefix + _overViewUrl == Driver.Url)
                {
                    string _myinfoUrl = Record("MyInformationUrl");
                    Utility.CsstoClick("clickOnMyInformation", 4);
                    Utility.Sleep(2);
                    if (Prefix + _myinfoUrl == Driver.Url)
                    {
                        string _selectedGender = Record("SelectedGender");
                        var _gender = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("Gender")));
                        var selectelementGender = new SelectElement(_gender);
                        selectelementGender.SelectByText(_selectedGender);

                        string _selectedTitle = Record("SelectedTitle");
                        var _title = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("Title")));
                        var selectelementTitle = new SelectElement(_title);
                        selectelementTitle.SelectByText(_selectedTitle);


                        string _dropdownTitleValue = Utility.GrabAttributeValueByCss("Title", "value", 2);
                        string _dropdownGenderValue = Utility.GrabAttributeValueByCss("Gender", "value", 2);

                        if ((_dropdownTitleValue == "1") && (_dropdownGenderValue == "1"))
                        {
                        }
                        else
                        {
                            string expectedErrorMsg = Record("ExpectedErrorMsg");
                            if (expectedErrorMsg != "No Error")
                            {
                                string actualErrorMsg = Utility.ByXpath("GenderErrorMsg", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                            }
                        }
                    }
                    else
                    {
                        Assert.IsTrue(false, "MyInformation Url is not opened.");
                    }
                }
                else
                {
                    Assert.IsTrue(false, "OverView Url is not opened.");
                }
            }
            else
            {
                Assert.IsTrue(false, "SignInUrl is not opened.");
            }
        }


        [DeploymentItem("AppData\\VerifyTheValidationsOf_TheLeapYear.csv"), DeploymentItem("VerifyTheValidationsOf_TheLeapYear.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\VerifyTheValidationsOf_TheLeapYear.csv", "VerifyTheValidationsOf_TheLeapYear#csv", DataAccessMethod.Sequential), TestMethod]
        public void VerifyTheValidationsOf_TheLeapYear()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            Utility.Sleep(4);
            if (Prefix + SignInUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(5);
                string _overViewUrl = Record("OverViewUrl");
                if (Prefix + _overViewUrl == Driver.Url)
                {
                    string _year = Record("DobYear");
                    string month = Record("DobMonth");
                    string _day = Record("DobDay");
                    int year = Convert.ToInt32(_year);
                    int day = Convert.ToInt32(_day);

                    Utility.Sleep(2);
                    Utility.CsstoClick("clickOnMyInformation", 4);

                    string _myInfoUrl = Record("MyInformationUrl");
                    if (Prefix + _myInfoUrl == Driver.Url)
                    {
                        Utility.Sleep(2);

                        var elementMonth = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DobMonth")));
                        var selectElementMonth = new SelectElement(elementMonth);
                        selectElementMonth.SelectByText(month);
                        Utility.Sleep(4);
                        var elementDay = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DobDay")));
                        var selectElementDay = new SelectElement(elementDay);
                        selectElementDay.SelectByText(_day);
                        Utility.Sleep(4);
                        IWebElement _verifyDobyear = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("DobYear")));
                        var selectElementOfYear = new SelectElement(_verifyDobyear);
                        selectElementOfYear.SelectByText(_year);
                        string actualDobYear = selectElementOfYear.SelectedOption.Text;
                        int DobYear = Convert.ToInt32(actualDobYear);
                        Utility.Sleep(4);
                        if (((Utility.GrabAttributeValueByCss("DobMonth", "value", 4)) == "0") && ((Utility.GrabAttributeValueByCss("DobDay", "value", 4)) == "0") && (Utility.GrabAttributeValueByCss("DobYear", "value", 4)) == "0")
                        {
                            Assert.IsTrue(false, "Please provide a date of birth");
                        }
                        else if (((DobYear % 4 == 0) && (DobYear % 100 != 0)) || (DobYear % 400 == 0))
                        {
                            if (month == "Feb")
                            {
                                Assert.AreEqual(29, day);
                            }
                        }
                        else if (month == "Feb")
                        {
                            if (day == 29)
                            {
                                string expectedErrorMsg = Record("ExpectedErrorMsg");
                                if (expectedErrorMsg != "No Error")
                                {
                                    Utility.CsstoClick("SaveInformationBtn", 5);
                                    Utility.Sleep(7);
                                    string actualErrorMsg = Utility.ByXpath("LeapYearErrorMsgInMyDetails", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                                    Utility.Sleep(3);
                                    Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                                }
                            }
                        }
                    }
                    else
                    {
                        Assert.IsTrue(false, "MyInformation Url is not opened.");
                    }
                }
                else
                {
                    Assert.IsTrue(false, "OverView Url is not opened.");
                }
            }
            else
            {
                Assert.IsTrue(false, "SignIn Url is not opened.");
            }
        }


        [DeploymentItem("verifyGenderAgainstTitle.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\verifyGenderAgainstTitle.csv", "verifyGenderAgainstTitle#csv", DataAccessMethod.Sequential), TestMethod]
        public void verifyGenderAgainstTitle()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;

            if (Prefix+SignInUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(5);

                string _overViewUrl = Record("OverViewUrl");
                if (Prefix + _overViewUrl == Driver.Url)
                {
                    Utility.Sleep(2);
                    Utility.CsstoClick("clickOnMyInformation", 4);
                    Utility.Sleep(2);
                    string _myInfoUrl = Record("MyInformationUrl");
                    if (Prefix+_myInfoUrl == Driver.Url)
                    {
                        string titlesWhenGenderIsNoSpecified = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("titlesWhenGenderIsNoSpecified");
                        string[] _titlesWhenGenderIsNoSpecified = titlesWhenGenderIsNoSpecified.Split(",".ToCharArray());
                        string titleForMales = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("titleForMales");
                        string[] _titleForMales = titleForMales.Split(",".ToCharArray());
                        string titleForFemales = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("titleForFemales");
                        string[] _titleForFemales = titleForFemales.Split(",".ToCharArray());

                        string _selectedGender = Record("SelectedGender");
                        var _gender = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("Gender")));
                        var selectelementGender = new SelectElement(_gender);
                        selectelementGender.SelectByText(_selectedGender);

                        if (((Utility.GrabAttributeValueByCss("CoTravellerGender", "value", 4)) == "1"))
                        {
                            string elements = Utility.ByCss("SelectCoTravellerTitle", 4);
                            string[] title = elements.Replace("\r\n", "_").Split("_".ToCharArray());

                            int i = 0;
                            foreach (var element in title)
                            {
                                Assert.AreEqual(_titleForMales[i], element);
                                i++;
                            }
                        }
                        else if (((Utility.GrabAttributeValueByCss("CoTravellerGender", "value", 4)) == "2"))
                        {
                            string elements = Utility.ByCss("SelectCoTravellerTitle", 4);
                            string[] title = elements.Replace("\r\n", "_").Split("_".ToCharArray());

                            int i = 0;
                            foreach (var element in title)
                            {
                                Assert.AreEqual(_titleForFemales[i], element);
                                i++;
                            }
                        }
                        else
                        {
                            string elements = Utility.ByCss("SelectCoTravellerTitle", 4);
                            string[] title = elements.Replace("\r\n", "_").Split("_".ToCharArray());

                            int i = 0;
                            foreach (var element in title)
                            {
                                Assert.AreEqual(_titlesWhenGenderIsNoSpecified[i], element);
                                i++;
                            }
                        }

                    }
                    else
                    {
                        Assert.IsTrue(true, "MyInformation Url is not opened.");
                    }
                }
                else
                {
                    Assert.IsTrue(true,"OverView Url is not opened.");
                }
            }
            else
            {
                Assert.IsTrue(true, "SignIn Url is not opened.");
            }
        }


        [TestCleanup]
        public void Cleanup()
        {
            UserProfileSPA.Library.TestEnvironment.Dispose();
        }
    }
}
