﻿using System;
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

namespace UserProfileSPA.TestCases
{
    [TestClass]
    public partial class CreateAnAccount : UserProfileTestBase
    {
        [TestInitialize]
        public void Initialize()
        {
            UserProfileSPA.Library.TestEnvironment.Init();
        }


        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\CreateAnAccountAllValidations.csv", "CreateAnAccountAllValidations#csv", DataAccessMethod.Sequential), DeploymentItem("CreateAnAccountAllValidations.csv"), TestMethod]
        public void CreateAnAccountAllValidationsAllFieldsAreBlank()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;

            string signinUrl = Record("SignInUrl");

            if (signinUrl == Driver.Url)
            {
                UserProfileSPA.Utility.XPathtoClick("ClickOnCreateAnAccountBtn", 4);
                Utility.Sleep(2);
                string signUpUrl = Record("SignUpUrl");
                if (signUpUrl == Driver.Url)
                {
                    Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                    string firstName = Record("FirstName");
                    string lastName = Record("LastName");
                    Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                    string cnfrmPassword = Record("ConfrmPassword");

                    if ((firstName) != null)
                    {
                        Utility.CssToSetText("CreateAccountEmailAddress", firstName, 4);
                    }
                    if ((lastName) != null)
                    {
                        Utility.CssToSetText("CreateAccountEmailAddress", lastName, 4);
                    }

                    Utility.CsstoClick("ClickOnCreateAnAccountBtnSignUpFree", 4);
                    Utility.Sleep(2);
                    string expectedValidationsIfCreateMyAccuntIsEmpty = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("expectedValidationsIfCreateMyCaaountIsEmpty");
                    string[] ValidationsIfCreateMyAccountIsEmpty = expectedValidationsIfCreateMyAccuntIsEmpty.Split(",".ToCharArray());

                    if ((string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInEmail", "value", 2))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInFirstName", "value", 2))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInLastName", "value", 2))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInPassword", "value", 2)) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInConfrmPassword", "value", 2)))))
                    {
                        string TextInEmailValidattion = Utility.ByXpath("TextInEmailValidattion", 4);
                        Assert.AreEqual(ValidationsIfCreateMyAccountIsEmpty[0], TextInEmailValidattion);

                        string TextInFirstNamelValidattion = Utility.ByXpath("TextInFirstNamelValidattion", 4);
                        Assert.AreEqual(ValidationsIfCreateMyAccountIsEmpty[1], TextInFirstNamelValidattion);

                        string TextInLastNameValidattion = Utility.ByXpath("TextInLastNameValidattion", 4);
                        Assert.AreEqual(ValidationsIfCreateMyAccountIsEmpty[2], TextInLastNameValidattion);

                        string TextInPasswordValidattion = Utility.ByXpath("TextInPasswordValidattion", 4);
                        Assert.AreEqual(ValidationsIfCreateMyAccountIsEmpty[3], TextInPasswordValidattion);

                        string TextInConfrmPasswordValidattion = Utility.ByXpath("TextInPasswordValidattion", 4);
                        Assert.AreEqual(ValidationsIfCreateMyAccountIsEmpty[3], TextInConfrmPasswordValidattion);
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
                        Assert.IsTrue(false, "Gender is selected.");
                    }
                }
                else
                {
                    throw new Exception("SignIn url is not open ");
                }
            }
            else
            {
                Assert.IsTrue(false, "SignInUrl is not opened.");
            }
        }

        [DeploymentItem("CreateAnAccountFirstAndLastNameValidations.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\CreateAnAccountFirstAndLastNameValidations.csv", "CreateAnAccountFirstAndLastNameValidations#csv", DataAccessMethod.Sequential), TestMethod]
        public void CreateAnAccountFirstAndLastNameValidations()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;

            string _baseUrl = Record("SignInUrl");
            if (_baseUrl == Driver.Url)
            {
                Utility.XPathtoClick("ClickOnCreateAnAccountBtn", 4);
                Utility.Sleep(2);
                string signUpUrl = Record("SignUpUrl");
                if (signUpUrl == Driver.Url)
                {
                    string _firstNametxt = Record("FirstName");
                    string _lastNametxt = Record("LastName");
                    Utility.CsstoClear("FirstName", 3);
                    Utility.CssToSetText("FirstName", _firstNametxt, 3);
                    Utility.CsstoClick("ClickOnDivOfSignUpFreeToCheckYourBooking", 4);
                    Utility.CsstoClear("LastName", 3);
                    Utility.CssToSetText("LastName", _lastNametxt, 3);
                    Utility.CsstoClick("ClickOnDivOfSignUpFreeToCheckYourBooking", 4);


                    if (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("CreateAnAccountTextInFirstName", "value", 3)))
                    {
                        if ((_firstNametxt.Length > 2) && (_firstNametxt.Length < 25))
                        {
                            Regex regex = new Regex(@"^[a-zA-Z]");
                            if (regex.IsMatch(_firstNametxt))
                            {
                                if ((_firstNametxt.Contains("!") || (_firstNametxt.Contains("@")) || (_firstNametxt.Contains("~")) || (_firstNametxt.Contains("$")) || (_firstNametxt.Contains("%")) || (_firstNametxt.Contains("^")) || (_firstNametxt.Contains("&")) || (_firstNametxt.Contains("*")) || (_firstNametxt.Contains("`")) || (_firstNametxt.Contains("+")) || (_firstNametxt.Contains("-")) || (_firstNametxt.Contains(":")) || (_firstNametxt.Contains(".")) || (_firstNametxt.Contains(",")) || (_firstNametxt.Contains("(")) || (_firstNametxt.Contains(")")) || (_firstNametxt.Contains("="))))
                                {
                                    throw new Exception("Name can only contain apostrophe, space or hyphen.");
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
                                throw new Exception("Name must begin with a letter.");
                            }
                        }
                        else
                        {
                            throw new Exception("FirstName should starts with big letter");
                        }

                    }
                    else
                    {
                        Assert.IsTrue(false, "Please enter first name");
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
                                    throw new Exception("Name can only contain apostrophe, space or hyphen.");
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
                                throw new Exception("Name must begin with a letter.");
                            }
                        }
                        else
                        {
                            throw new Exception("FirstName should starts with big letter");
                        }

                    }
                    else
                    {
                        Assert.IsTrue(false, "Please enter last name");
                    }

                }
                else
                {
                    Assert.IsTrue(false, "SignUp page is not open");
                }
            }
        }


        [DeploymentItem("ValidateEmailAddressInCreateAnAccount.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\ValidateEmailAddressInCreateAnAccount.csv", "ValidateEmailAddressInCreateAnAccount#csv", DataAccessMethod.Sequential), TestMethod]
        public void ValidateEmailAddressInCreateAnAccount()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;

            string _baseUrl = Record("SignInUrl");
            if (_baseUrl == Driver.Url)
            {
                Utility.XPathtoClick("ClickOnCreateAnAccountBtn", 4);
                Utility.Sleep(2);
                string signUpUrl = Record("SignUpUrl");
                if (signUpUrl == Driver.Url)
                {
                    string _email = Record("Email");
                    Utility.CssToSetText("TextInEmail", _email, 3);
                    Utility.CsstoClear("FirstName", 3);
                    if (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInEmail", "value", 3)))
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
                            Assert.IsTrue(false, "Please enter a valid email address.");
                        }
                    }
                    else
                    {
                        Assert.IsTrue(false, "Your email is required");
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

        [DeploymentItem("ValidatingPasswordForCreateAnAccount.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\ValidatingPasswordForCreateAnAccount.csv", "ValidatingPasswordForCreateAnAccount#csv", DataAccessMethod.Sequential), TestMethod]
        public void ValidatingPasswordForCreateAnAccount()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;

            string signinUrl = Record("SignInUrl");
            string password = Record("Password");
            string confrmPassword = Record("ConfrmPassword");
            Driver.Navigate().GoToUrl(signinUrl);

            if (signinUrl == Driver.Url)
            {
                Utility.XPathtoClick("ClickOnCreateAnAccountBtn", 4);
                Utility.Sleep(2);
                string signUpUrl = Record("SignUpUrl");
                if (signUpUrl == Driver.Url)
                {
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
                                string expectedWhenBothNotSame = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("ecpectedWhenBothNotSame");
                                List<IWebElement> actualWhenBothNotSame = Driver.FindElements(By.ClassName("val_error")).ToList();
                                Assert.AreEqual(expectedWhenBothNotSame, actualWhenBothNotSame[3].Text);
                            }
                        }
                        else
                        {
                            string expectedWhenLengthNotExpected = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("expectedWhenBothLengthNotExpected");
                            List<IWebElement> actualWhenLengthNotExpected = Driver.FindElements(By.ClassName("val_error")).ToList();
                            Assert.AreEqual(expectedWhenLengthNotExpected, actualWhenLengthNotExpected[3].Text);
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


        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\VerifyTheValidationsOfTheLeapYearInRegisterPage.csv", "VerifyTheValidationsOfTheLeapYearInRegisterPage#csv", DataAccessMethod.Sequential), DeploymentItem("VerifyTheValidationsOfTheLeapYearInRegisterPage.csv"), TestMethod]
        public void VerifyTheValidationsOfTheLeapYearInRegisterPage()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;

            string _baseUrl = Record("SignInUrl");
            if (_baseUrl == Driver.Url)
            {
                string _year = Record("DobYear");
                string month = Record("DobMonth");
                string _day = Record("DobDay");
                int year = Convert.ToInt32(_year);
                int day = Convert.ToInt32(_day);

                Utility.XPathtoClick("ClickOnCreateAnAccountBtn", 4);
                Utility.Sleep(2);
                string signUpUrl = Record("SignUpUrl");
                if (signUpUrl == Driver.Url)
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
                                Assert.IsTrue(true);
                            }
                        }
                    }
                    else if (month == "Feb")
                    {
                        if (day == 29)
                        {
                            throw new Exception("Please enter a valid date of birth");
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

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\VerifyingEmailAddressAlreadyExistInCreateAnAccount.csv", "VerifyingEmailAddressAlreadyExistInCreateAnAccount#csv", DataAccessMethod.Sequential), DeploymentItem("VerifyingEmailAddressAlreadyExistInCreateAnAccount.csv"), TestMethod]
        public void VerifyingEmailAddressAlreadyExistInCreateAnAccount()
        {
            IWebDriver Driver = TestEnvironment.Driver;
            string signinUrl = Record("SignInUrl");  
            if (signinUrl == Driver.Url)
            {
                Utility.XPathtoClick("ClickOnCreateAnAccountBtn", 4);
                Utility.Sleep(2);
                string signUpUrl = Record("SignUpUrl");
                if (signUpUrl == Driver.Url)
                {
                    Utility.CssToSetText("TextInEmail", Record("EnterAlreadyExistEmail"), 3);
                    Utility.CsstoClick("ClickOnCreateAnAccountBtnSignUpFree", 4);                   
                    string actualEmailAlreadyExist = Utility.ByClassName("AlreadyExistEmail",4);
                    string expectedWhenEmailIsAlreadyExist = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("expectedWhenEmailIsAlreadyExist");
                    Assert.AreEqual(expectedWhenEmailIsAlreadyExist,actualEmailAlreadyExist);
                }
            }
        }
    }
}
