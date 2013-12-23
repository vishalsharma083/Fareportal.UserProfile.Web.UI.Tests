using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using UserProfileSPA.Library;
using System.Data;
using System.Xml;
using UserProfileSPA;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Support.UI;
using System.Configuration;

namespace UserProfileSPA.TestCases
{
    [TestClass]
    public class CreateAnAccount 
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


        [DeploymentItem("CreateAnAccountAllValidations.csv"), DeploymentItem("AppData\\CreateAnAccountAllValidations.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\CreateAnAccountAllValidations.csv", "CreateAnAccountAllValidations#csv", DataAccessMethod.Sequential), TestMethod]
        public void CreateAnAccountAllValidationsAllFieldsAreBlank()
        {
           IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;

            Utility.Sleep(5);
            if (Prefix+SignInUrl == Driver.Url)
            {
                UserProfileSPA.Utility.XPathtoClick("ClickOnCreateAnAccountBtn", 4);
                Utility.Sleep(2);
               
                if (Prefix+Record("SignUpUrl") == Driver.Url)
                {                      
                    Utility.CssToSetText("CreateAccountEmailAddress", Record("EmailAddress"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                    Utility.CssToSetText("CreateAccountPassword", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                    Utility.CssToSetText("CreateAccountFirstName", Record("FirstName"), 4);
                    Utility.CssToSetText("CreateAccountLastName", Record("LastName"), 4);
                    Utility.CssToSetText("CreateAccountConfrmPassword", Record("ConfrmPassword"), 4);
                    

                    Utility.CsstoClick("ClickOnCreateAnAccountBtnSignUpFree", 4);
                    Utility.Sleep(2);
                    string expectedValidationsIfCreateMyAccuntIsEmpty = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("expectedValidationsIfCreateMyAccountIsEmpty");
                    string[] ValidationsIfCreateMyAccountIsEmpty = expectedValidationsIfCreateMyAccuntIsEmpty.Split(",".ToCharArray());

                    if ((string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInEmail", "value", 2))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("CreateAnAccountTextInFirstName", "value", 2))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("CreateAnAccountTextInLastName", "value", 2))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInPassword", "value", 2)) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInConfrmPassword", "value", 2)))))
                    {
                        string TextInEmailValidattion = Utility.ByXpath("EmailExpectedError", 4);
                        Assert.AreEqual(ValidationsIfCreateMyAccountIsEmpty[0], TextInEmailValidattion);

                        string TextInFirstNamelValidattion = Utility.ByXpath("ErrorMsgForFirstName", 4);
                        Assert.AreEqual(ValidationsIfCreateMyAccountIsEmpty[3], TextInFirstNamelValidattion);

                        string TextInLastNameValidattion = Utility.ByXpath("ErrorMsgForLastName", 4);
                        Assert.AreEqual(ValidationsIfCreateMyAccountIsEmpty[4], TextInLastNameValidattion);

                        string TextInPasswordValidattion = Utility.ByXpath("ExpectedErrorMsgForPassword", 4);
                        Assert.AreEqual(ValidationsIfCreateMyAccountIsEmpty[1], TextInPasswordValidattion);

                        string TextInConfrmPasswordValidattion = Utility.ByXpath("ExpectedErrorMsgForConfrmPassword", 4);
                        Assert.AreEqual(ValidationsIfCreateMyAccountIsEmpty[2], TextInConfrmPasswordValidattion);
                    }
                    if (((Utility.GrabAttributeValueByCss("CreateAnAccountMonth", "value", 4)) == "0") && ((Utility.GrabAttributeValueByCss("CreateAnAccountDay", "value", 4)) == "0") && (Utility.GrabAttributeValueByCss("CreateAnAccountYear", "value", 4)) == "0")
                    {
                        string expectedValidationsInDObWhileCreateAnAccountIsEmpty = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("expectedValidationsInDObWhileCreateAnAccountIsEmpty");
                        string _actualErrorWhileDobIsNotSelected = Utility.ByXpath("ActualErrorWhileDobIsNotSelected", 4);
                        Assert.AreEqual(expectedValidationsInDObWhileCreateAnAccountIsEmpty, _actualErrorWhileDobIsNotSelected);
                    }
                    else
                    {
                        Assert.IsTrue(false, "Month,Day,Year is not empty.");
                    }
                    if (((Utility.GrabAttributeValueByCss("SelectGender", "value", 4)) == "0"))
                    {
                        string expectedValidationsInSelectGenderWhileCreateAnAccountIsEmpty = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("expectedValidationsInSelectGenderWhileCreateAnAccountIsEmpty");
                        string actualValidationsInSelectGenderWhileCreateAnAccountIsEmpty = Utility.ByXpath("SelectGenderValidation", 4);
                        Assert.AreEqual(actualValidationsInSelectGenderWhileCreateAnAccountIsEmpty, expectedValidationsInSelectGenderWhileCreateAnAccountIsEmpty);
                    }
                    else
                    {
                        if (!Utility.IsDisplayedUsingXpath("SelectGenderValidation"))
                        { 
                           if((Utility.GrabAttributeValueByCss("SelectGender", "value", 4)) != "0")
                            {
                              Assert.IsTrue(true);
                            }
                        }                   
                       
                    }
                }
                else
                {
                    throw new Exception("SignUpUrl is not open ");
                }
            }
            else
            {
                Assert.IsTrue(false, "SignInUrl is not opened.");
            }
        }

       [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\CreateAnAccountFirstAndLastNameValidations.csv", "CreateAnAccountFirstAndLastNameValidations#csv", DataAccessMethod.Sequential), DeploymentItem("AppData\\CreateAnAccountFirstAndLastNameValidations.csv"), DeploymentItem("CoTravelerFirstAndLastNameValidations.csv"), DeploymentItem("CreateAnAccountFirstAndLastNameValidations.csv"), TestMethod]
        public void CreateAnAccountFirstAndLastNameValidations() 
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            if (Prefix+SignInUrl == Driver.Url)
            {
                Utility.XPathtoClick("ClickOnCreateAnAccountBtn", 4);
                Utility.Sleep(2);
                if (Prefix+Record("SignUpUrl") == Driver.Url)
                {
                    string _firstNametxt = Record("FirstName");
                    string _lastNametxt = Record("LastName");
                    Utility.CsstoClear("FirstName", 3);
                    Utility.CssToSetText("FirstName", _firstNametxt, 3);
                    Utility.CsstoClick("ClickOnDivOfSignUpFreeToCheckYourBooking", 4);
                    Utility.CsstoClear("LastName", 3);
                    Utility.CssToSetText("LastName", _lastNametxt, 3);
                    Utility.CsstoClick("ClickOnDivOfSignUpFreeToCheckYourBooking", 4);
                    Utility.CsstoClick("ClickOnCreateAnAccountBtnSignUpFree", 4);


                    if (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("CreateAnAccountTextInFirstName", "value", 3)))
                    {
                        if ((_firstNametxt.Length > 2) && (_firstNametxt.Length < 25))
                        {
                            Regex regex = new Regex(@"^[a-zA-Z]");
                            if (regex.IsMatch(_firstNametxt))
                            {
                                if ((_firstNametxt.Contains("!") || (_firstNametxt.Contains("@")) || (_firstNametxt.Contains("~")) || (_firstNametxt.Contains("$")) || (_firstNametxt.Contains("%")) || (_firstNametxt.Contains("^")) || (_firstNametxt.Contains("&")) || (_firstNametxt.Contains("*")) || (_firstNametxt.Contains("`")) || (_firstNametxt.Contains("+")) || (_firstNametxt.Contains("-")) || (_firstNametxt.Contains(":")) || (_firstNametxt.Contains(".")) || (_firstNametxt.Contains(",")) || (_firstNametxt.Contains("(")) || (_firstNametxt.Contains(")")) || (_firstNametxt.Contains("=") || (_firstNametxt.Contains("#")))))
                                {
                                    string expectedErrorMsg = Record("ExpectedErrorMsgForFirstname");
                                    if (expectedErrorMsg != "No Error")
                                    {
                                        string actualErrorMsg = Utility.ByXpath("ErrorMsgForFirstName", 4);
                                        Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                                    }
                                        //throw new Exception("Name can only contain apostrophe, space or hyphen.");
                                }
                                else { Assert.IsTrue(true); }
                                if ((_firstNametxt.Contains("'s") || (!_firstNametxt.Contains(" ")) || (!_firstNametxt.Contains("-"))))
                                { Assert.IsTrue(true); }
                                else
                                {
                                    Assert.IsTrue(false);
                                }
                            }
                            else
                            {
                                string expectedErrorMsg = Record("ExpectedErrorMsgForFirstname");
                                if (expectedErrorMsg != "No Error")
                                {
                                    string actualErrorMsg = Utility.ByXpath("ErrorMsgForFirstName", 4);
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
                                string actualErrorMsg = Utility.ByXpath("ErrorMsgForFirstName", 4);
                                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                            }
                              // throw new Exception("Firstname must be between 2 and 25 characters in length.");
                        }

                    }
                    else
                    {
                        string expectedErrorMsg = Record("ExpectedErrorMsgForFirstname");
                        if (expectedErrorMsg != "No Error")
                        {
                            string actualErrorMsg = Utility.ByXpath("ErrorMsgForFirstName", 4);
                            Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                        }
                        //Assert.IsTrue(false, "Please enter first name");
                    }

                    if (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("CreateAnAccountTextInLastName", "value", 3)))
                    {
                        if ((_lastNametxt.Length > 2) && (_lastNametxt.Length < 25))
                        {
                            Regex regex = new Regex(@"^[a-zA-Z]");
                            if (regex.IsMatch(_firstNametxt))
                            {
                                if ((_lastNametxt.Contains("!") || (_lastNametxt.Contains("@")) || (_lastNametxt.Contains("~")) || (_lastNametxt.Contains("$")) || (_lastNametxt.Contains("%")) || (_lastNametxt.Contains("^")) || (_lastNametxt.Contains("&")) || (_lastNametxt.Contains("*")) || (_lastNametxt.Contains("`")) || (_lastNametxt.Contains("+")) || (_lastNametxt.Contains("_")) || (_lastNametxt.Contains(":")) || (_lastNametxt.Contains(".")) || (_lastNametxt.Contains(",")) || (_lastNametxt.Contains("(")) || (_lastNametxt.Contains(")")) || (_lastNametxt.Contains("="))))
                                {
                                    string expectedErrorMsg = Record("ExpectedErrorMsgForLastname");
                                    string actualErrorMsg = Utility.ByXpath("ErrorMsgForLastName", 4);
                                    Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                                    //throw new Exception("Firstname must be between 2 and 25 characters in length.");
                                    //throw new Exception("Name can only contain apostrophe, space or hyphen.");
                                }
                                else
                                {
                                    Assert.IsTrue(true);
                                }
                                if ((_lastNametxt.Contains("'s") || (!_lastNametxt.Contains(" ")) || (!_lastNametxt.Contains("-"))))
                                {
                                    Assert.IsTrue(true);
                                }
                            }
                            else
                            {
                                string expectedErrorMsg = Record("ExpectedErrorMsgForLastname");
                                if (expectedErrorMsg != "No Error")
                                {
                                    string actualErrorMsg = Utility.ByXpath("ErrorMsgForLastName", 4);
                                    Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                                }
                               /// throw new Exception("Name must begin with a letter.");
                            }
                        }
                        else
                        {
                            string expectedErrorMsg = Record("ExpectedErrorMsgForLastname");
                            if (expectedErrorMsg != "No Error")
                            {
                                string actualErrorMsg = Utility.ByXpath("ErrorMsgForLastName", 4);
                                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                            }
                            //throw new Exception("Firstname must be between 2 and 25 characters in length.");
                        }

                    }
                    else
                    {
                        string expectedErrorMsg = Record("ExpectedErrorMsgForLastname");
                        if (expectedErrorMsg != "No Error")
                        {
                            string actualErrorMsg = Utility.ByXpath("ErrorMsgForLastName", 4);
                            Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                        }
                        //Assert.IsTrue(false, "Please enter last name");
                    }

                }
                else
                {
                    Assert.IsTrue(false, "SignUp page is not open");
                }
            }
            else
            {
                Assert.IsTrue(false, "SignIn page is not open");
            }
        }


        [DeploymentItem("ValidateEmailAddressInCreateAnAccount.csv"), DeploymentItem("AppData\\ValidateEmailAddressInCreateAnAccount.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\ValidateEmailAddressInCreateAnAccount.csv", "ValidateEmailAddressInCreateAnAccount#csv", DataAccessMethod.Sequential), TestMethod]
        public void ValidateEmailAddressInCreateAnAccount()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            if (Prefix+SignInUrl == Driver.Url)
            {
                Utility.XPathtoClick("ClickOnCreateAnAccountBtn", 4);
                Utility.Sleep(4);
                if (Prefix+Record("SignUpUrl") == Driver.Url)
                {
                    string _email = Record("Email");
                    Utility.CsstoClick("CreateAccountEmailAddress", 4);
                    Utility.CssToSetText("TextInEmail", _email, 3);
                    Utility.CsstoClear("FirstName", 3);
                    if (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInEmail", "value", 3)))
                    {
                        Utility.CsstoClick("ClickOnCreateAnAccountBtnSignUpFree", 4);
                        if (!Utility.IsDisplayedUsingXpath("EmailAlreadyExistInCreateAnAccount"))
                        {
                            //string MatchEmailExpr = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.(?:[A-Z]{2}|com";
                            string MatchEmailExpr = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                            if (Regex.IsMatch(_email, MatchEmailExpr))
                            {
                                if (!Utility.IsDisplayedUsingXpath("ErrorMessageWhileEmailIsWrongOrEmpty"))
                                {
                                    Assert.IsTrue(true);
                                }
                            }
                            else
                            {
                                string expectedErrorMsg = Record("ExpectedErrorMsg");
                                if (expectedErrorMsg != "No Error")
                                {
                                    Utility.Sleep(4);
                                    string actualErrorMsg = Utility.ByXpath("EmailExpectedError", 7);
                                    Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                                }
                            }
                        }
                        else
                        {
                            string expectedErrorMsg = Record("ExpectedErrorMsg");
                            if (expectedErrorMsg != "No Error")
                            {
                                string actualErrorMsg = Utility.ByXpath("EmailAlreadyExistInCreateAnAccount", 4);
                                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                            }
                        }
                    }
                    else
                    {
                        string expectedErrorMsg = Record("ExpectedErrorMsg");
                        if (expectedErrorMsg != "No Error")
                        {
                            string actualErrorMsg = Utility.ByXpath("EmailExpectedError", 22);
                            Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                        }
                    }
                }
                else
                {
                    Assert.IsTrue(false, "SignUP page is not opened");
                }
            }
            else
            {
                Assert.IsTrue(false, "SignIn page is not opened");
            }
        }

        [DeploymentItem("AppData\\ValidatingPasswordForCreateAnAccount.csv"), DeploymentItem("ValidatingPasswordForCreateAnAccount.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\ValidatingPasswordForCreateAnAccount.csv", "ValidatingPasswordForCreateAnAccount#csv", DataAccessMethod.Sequential), TestMethod]
        public void ValidatingPasswordForCreateAnAccount()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            if (Prefix+SignInUrl == Driver.Url)
            {
                Utility.XPathtoClick("ClickOnCreateAnAccountBtn", 4);
                Utility.Sleep(3);               
                if (Prefix+Record("SignUpUrl") == Driver.Url)
                {
                    string password = Record("Password");
                    string confrmPassword = Record("ConfrmPassword");
                    Utility.CssToSetText("TextInPassword", password, 3);
                    Utility.CsstoClick("ClickOnCreateAnAccountDiv", 3);
                    Utility.CssToSetText("TextInConfrmPassword", confrmPassword, 3);
                    Utility.CsstoClick("ClickOnCreateAnAccountDiv", 3);
                    Utility.CsstoClick("ClickOnCreateAnAccountBtnSignUpFree", 4);
                    if (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInPassword", "value", 3)) && (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInPassword", "value", 3))))
                    {
                        if ((password.Length > 5) && (password.Length < 32))
                        {
                            string _pass = Utility.GrabAttributeValueByCss("TextInPassword", "value", 4);
                            string _confrmPass = Utility.GrabAttributeValueByCss("TextInConfrmPassword", "value", 4);
                            if (_pass == _confrmPass)
                            {
                                Assert.IsTrue(true);
                            }
                            else
                            {
                                string expectedErrorMsgForpassword = Record("ExpextedErrorMessageForPassword");
                                if (expectedErrorMsgForpassword != "No Error")
                                {
                                    string actualerrorMsgForPassword = Utility.ByXpath("ExpectedErrorMsgForPassword", 6);
                                    Assert.AreEqual(expectedErrorMsgForpassword, actualerrorMsgForPassword);
                                }
                                string expextedErrorMessageForConfrmPassword = Record("ExpextedErrorMessageForConfrmPassword");
                                if (expextedErrorMessageForConfrmPassword != "No Error")
                                {
                                    string actualerrorMsgForConfrmPassword = Utility.ByXpath("ExpectedErrorMsgForConfrmPassword", 3);
                                    Assert.AreEqual(expextedErrorMessageForConfrmPassword, actualerrorMsgForConfrmPassword);
                                }                                
                            }
                        }
                        else
                        {
                            string expectedErrorMsgForpassword = Record("ExpextedErrorMessageForPassword");
                            if (expectedErrorMsgForpassword != "No Error")
                            {
                                string actualerrorMsgForPassword = Utility.ByXpath("ExpectedErrorMsgForPassword", 3);
                                Assert.AreEqual(expectedErrorMsgForpassword, actualerrorMsgForPassword);
                            }
                            string expextedErrorMessageForConfrmPassword = Record("ExpextedErrorMessageForConfrmPassword");
                            if (expextedErrorMessageForConfrmPassword != "No Error")
                            {
                                string actualerrorMsgForConfrmPassword = Utility.ByXpath("ExpectedErrorMsgForConfrmPassword", 3);
                                Assert.AreEqual(expextedErrorMessageForConfrmPassword, actualerrorMsgForConfrmPassword);
                            }                            
                        }
                    }
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


        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\VerifyTheValidationsOfTheLeapYearInRegisterPage.csv", "VerifyTheValidationsOfTheLeapYearInRegisterPage#csv", DataAccessMethod.Sequential), DeploymentItem("VerifyTheValidationsOfTheLeapYearInRegisterPage.csv"), DeploymentItem("AppData\\VerifyTheValidationsOfTheLeapYearInRegisterPage.csv"), TestMethod]
        public void VerifyTheValidationsOfTheLeapYearInRegisterPage()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            if (Prefix+SignInUrl == Driver.Url)
            {
                string _year = Record("DobYear");
                string month = Record("DobMonth");
                string _day = Record("DobDay");
                int year = Convert.ToInt32(_year);
                int day = Convert.ToInt32(_day);

                Utility.XPathtoClick("ClickOnCreateAnAccountBtn", 4);
                Utility.Sleep(3);               
                if (Prefix+Record("SignUpUrl") == Driver.Url)
                {
                    var elementMonth = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DobMonth")));
                    var selectElementMonth = new SelectElement(elementMonth);
                    selectElementMonth.SelectByText(month);

                    var elementDay = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DobDay")));
                    var selectElementDay = new SelectElement(elementDay);
                    selectElementDay.SelectByText(_day);

                    IWebElement _verifyDobyear = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("DobYear")));
                    var selectElementOfYear = new SelectElement(_verifyDobyear);
                    selectElementOfYear.SelectByText(_year);
                    string actualDobYear = selectElementOfYear.SelectedOption.Text;
                    int DobYear = Convert.ToInt32(actualDobYear);

                    if (((Utility.GrabAttributeValueByCss("DobMonth", "value", 4)) == "0") && ((Utility.GrabAttributeValueByCss("DobDay", "value", 4)) == "0") && (Utility.GrabAttributeValueByCss("DobYear", "value", 4)) == "0")
                    {
                        Assert.IsTrue(false, "Please provide a date of birth");
                    }
                    else if (((DobYear % 4 == 0) && (DobYear % 100 != 0)) || (DobYear % 400 == 0))
                    {
                        if (month == "Feb")
                        {
                            if (day == 29)
                            {
                                Assert.IsTrue(true, "it is a leap year.");
                            }
                            // Assert.AreEqual(29, day);
                        }
                    }
                    else if (month == "Feb")
                    {
                        if (day == 29)
                        {
                            string expectedErrorMsg = Record("ExpectedErrorMsg");
                            if (expectedErrorMsg != "No Error")
                            {
                                Utility.CsstoClick("ClickOnCreateAnAccountBtnSignUpFree", 4);
                                string actualErrorMsg = Utility.ByXpath("LeapYearErrorMsgInCreateAnAccount", UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                            }
                        }
                    }
                    else
                    {
                        Assert.IsTrue(false, "Please select Month Feb to check leap year.");
                    }
                    
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

        [DeploymentItem("AppData\\VerifyingEmailAddressAlreadyExistInCreateAnAccount.csv"), DeploymentItem("VerifyingEmailAddressAlreadyExistInCreateAnAccount.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\VerifyingEmailAddressAlreadyExistInCreateAnAccount.csv", "VerifyingEmailAddressAlreadyExistInCreateAnAccount#csv", DataAccessMethod.Sequential), TestMethod]
        public void VerifyingEmailAddressAlreadyExistInCreateAnAccount()
        {
            IWebDriver Driver = TestEnvironment.Driver;
            Utility.Sleep(5);
            if (Prefix+SignInUrl == Driver.Url)
            {               
                Utility.XPathtoClick("ClickOnCreateAnAccountBtn", 4);
                Utility.Sleep(4);
                if (Prefix+Record("SignUpUrl") == Driver.Url)
                {
                    Utility.CssToSetText("TextInEmail", Record("EnterAlreadyExistEmail"), 3);
                    Utility.CsstoClick("ClickOnCreateAnAccountBtnSignUpFree", 4);
                    Utility.Sleep(15);
                    string actualEmailAlreadyExist = Utility.ByXpath("AlreadyExistEmail",4);
                    string expectedWhenEmailIsAlreadyExist = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("expectedWhenEmailIsAlreadyExist");
                    Assert.AreEqual(expectedWhenEmailIsAlreadyExist,actualEmailAlreadyExist);
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

      

        [TestCleanup]
        public void Cleanup()
        {
            UserProfileSPA.Library.TestEnvironment.Dispose();
        }     
    }

}
