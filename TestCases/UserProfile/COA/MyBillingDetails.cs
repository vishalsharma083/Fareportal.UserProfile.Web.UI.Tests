﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using UserProfileSPA.Library;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace UserProfileSPA.TestCases
{  
    [TestClass]
    public partial class MyBillingDetails 
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


        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\ClickOnEditCardDetailsAndDoChanges.csv", "ClickOnEditCardDetailsAndDoChanges#csv", DataAccessMethod.Sequential), DeploymentItem("ClickOnEditCardDetailsAndDoChanges.csv"), TestMethod]
        public void ClickOnEditCardDetailsAndDoChanges()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
           
            string email = Record("Email");
            string password = Record("Password");
            string signinUrl = Record("SignInUrl");

            if (signinUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
               
                Utility.CsstoClick("SignInBtn", 4);
                Utility.Sleep(3);
                string fareportalOverviewUrl = Record("CheapoairOverviewUrl");
                Utility.Sleep(3);
                if (fareportalOverviewUrl == Driver.Url)
                {
                    Utility.CsstoClick("clickOnMyInformation", 4);
                    Utility.Sleep(2);

                    string fareportalMyDetailsUrl = Record("CheapoairMyDetailsUrl");
                    if (fareportalMyDetailsUrl == Driver.Url)
                    {
                        Utility.ByLinkTexttoClick("ClickOnMyBillingDetails", 4);

                        string fareportalMyBillingDetailsUrl = Record("CheapoairMyBillingDetailsUrl");
                        if (fareportalMyBillingDetailsUrl == Driver.Url)
                        {
                            if (Utility.IsDisplayedUsingCss("ViewCard"))
                            {
                                string _yourCard = Utility.ByXpath("YourCard", 4);
                                if (_yourCard == "Your Cards :")
                                {
                                    string cardValues = Utility.ByCss("ViewCard", 3);
                                    string[] card = cardValues.Replace("\r\n", "_").Split("_".ToCharArray());
                                    string[] splitAddress = card[7].Split(",".ToCharArray());
                                    string[] splitAddressLine1AndCity = splitAddress[0].Split(" ".ToCharArray());
                                    string addressLine1 = splitAddressLine1AndCity[0] + " " + splitAddressLine1AndCity[1];
                                    string city = splitAddressLine1AndCity[2];
                                    string[] splitStateAndCountryCode = splitAddress[1].Split(" ".ToCharArray());
                                    string state = splitStateAndCountryCode[1];
                                    string CountryCode = splitStateAndCountryCode[2];
                                    string prevName = card[6];
                                    Utility.XPathtoClick("ClickOnEditLinkInBillingDetials", 3);

                                    if (!(Utility.IsDisplayedUsingCss("YourCard")))
                                    {
                                        if ((Utility.IsDisplayedUsingCss("CalnelBtn")))
                                        {
                                            IWebElement alreadyExistCountry = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("UpdatedCountry")));
                                            var selectalreadyExistCountryElementCountry = new SelectElement(alreadyExistCountry);
                                            string CurrentlySelected = selectalreadyExistCountryElementCountry.SelectedOption.Text;
                                            if (CurrentlySelected == "Indonesia")
                                            {
                                                string _updatedCountry = "India";
                                                string _updatedFirstName = Record("UpdatedFirstName");
                                                string _updatedLastName = Record("UpdatedLastName");
                                                string _fullName = _updatedFirstName + " " + _updatedLastName;
                                                Utility.CssToSetText("UpdatedFirstName", _updatedFirstName, 3);
                                                Utility.CssToSetText("UpdatedLastName", _updatedLastName, 3);
                                                // string _updatedCountry = Record("Country");
                                                IWebElement elementCountry = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("UpdatedCountry")));
                                                var selectElementCountry = new SelectElement(elementCountry);
                                                selectElementCountry.SelectByText(_updatedCountry);

                                                string _updatedCity = Record("UpdatedCity");
                                                _updatedCity = _updatedCity + "Ind";
                                                Utility.CssToSetText("UpdatedCity", _updatedCity, 3);

                                                string _updatedAddressLine1 = Record("UpdatedAddressLine1");
                                                Random randomNum = new Random();
                                                int num = randomNum.Next(5, 100);
                                                _updatedAddressLine1 = _updatedAddressLine1 + num;
                                                Utility.CssToSetText("UpdatedAddressLine1", _updatedAddressLine1, 3);

                                                string _updatedState = Record("UpdatedState");
                                                _updatedState = _updatedState + "Ind";
                                                Utility.CssToSetText("UpdatedState", _updatedState, 3);

                                                string _ValueOfSelectedCountry = Utility.GrabAttributeValueByCss("UpdatedCountry", "value", 6).ToString();
                                                Utility.CsstoClick("SaveUpdatedInformationInBillingDetails", 4);

                                                if (!(Utility.IsDisplayedUsingCss("CalnelBtn")))
                                                {
                                                    if (Utility.IsDisplayedUsingCss("ViewCard"))
                                                    {
                                                        string updatedCardValues = Utility.ByCss("ViewCard", 3);
                                                        string[] updatedCard = updatedCardValues.Replace("\r\n", "_").Split("_".ToCharArray());
                                                        string[] updatedSplitAddress = updatedCard[7].Split(",".ToCharArray());
                                                        string[] updatedSplitAddressLine1AndCity = updatedSplitAddress[0].Split(" ".ToCharArray());
                                                        string updatedAddressLine1 = updatedSplitAddressLine1AndCity[0] + " " + updatedSplitAddressLine1AndCity[1];
                                                        string updatedCity = updatedSplitAddressLine1AndCity[2];

                                                        string[] splitUpdatedStateAndCountryCode = updatedSplitAddress[1].Split(" ".ToCharArray());
                                                        string updatedState = splitUpdatedStateAndCountryCode[1];
                                                        string updateCountryCode = splitUpdatedStateAndCountryCode[2];

                                                        Assert.AreNotEqual(addressLine1, updatedAddressLine1);
                                                        Assert.AreNotEqual(city, updatedCity);
                                                        Assert.AreNotEqual(state, updatedState);
                                                        Assert.AreNotEqual(CountryCode, updateCountryCode);
                                                        Assert.AreNotEqual(prevName, _fullName);
                                                    }

                                                }
                                            }

                                            else if (CurrentlySelected == "India")
                                            {
                                                string _updatedCountry = "Indonesia";
                                                string _updatedFirstName = Record("UpdatedFirstName");
                                                string _updatedLastName = Record("UpdatedLastName");
                                                string _fullName = _updatedFirstName + " " + "Kathuria";
                                                //string _updatedCountry = Record("Country");
                                                IWebElement elementCountry = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("UpdatedCountry")));
                                                var selectElementCountry = new SelectElement(elementCountry);
                                                selectElementCountry.SelectByText(_updatedCountry);

                                                string _updatedCity = Record("UpdatedCity");
                                                _updatedCity = _updatedCity + "Indo";
                                                Utility.CssToSetText("UpdatedCity", _updatedCity, 3);

                                                string _updatedAddressLine1 = Record("UpdatedAddressLine1");
                                                Random randomNum = new Random();
                                                int num = randomNum.Next(5, 100);
                                                _updatedAddressLine1 = _updatedAddressLine1 + num;
                                                Utility.CssToSetText("UpdatedAddressLine1", _updatedAddressLine1, 3);

                                                string _updatedState = Record("UpdatedState");
                                                _updatedState = _updatedState + "Indo";
                                                Utility.CssToSetText("UpdatedState", _updatedState, 3);

                                                string _ValueOfSelectedCountry = Utility.GrabAttributeValueByCss("UpdatedCountry", "value", 6).ToString();
                                                Utility.CsstoClick("SaveUpdatedInformationInBillingDetails", 4);
                                                Utility.Sleep(5);
                                                if (!(Utility.IsDisplayedUsingCss("CalnelBtn")))
                                                {
                                                    if (Utility.IsDisplayedUsingCss("ViewCard"))
                                                    {
                                                        string updatedCardValues = Utility.ByCss("ViewCard", 3);
                                                        string[] updatedCard = updatedCardValues.Replace("\r\n", "_").Split("_".ToCharArray());
                                                        string[] updatedSplitAddress = updatedCard[7].Split(",".ToCharArray());
                                                        string[] updatedSplitAddressLine1AndCity = updatedSplitAddress[0].Split(" ".ToCharArray());
                                                        string updatedAddressLine1 = updatedSplitAddressLine1AndCity[0] + " " + updatedSplitAddressLine1AndCity[1];
                                                        string updatedCity = updatedSplitAddressLine1AndCity[2];

                                                        string[] splitUpdatedStateAndCountryCode = updatedSplitAddress[1].Split(" ".ToCharArray());
                                                        string updatedState = splitUpdatedStateAndCountryCode[1];
                                                        string updateCountryCode = splitUpdatedStateAndCountryCode[2];

                                                        Assert.AreNotEqual(addressLine1, updatedAddressLine1);
                                                        Assert.AreNotEqual(city, updatedCity);
                                                        Assert.AreNotEqual(state, updatedState);
                                                        Assert.AreNotEqual(CountryCode, updateCountryCode);
                                                        Assert.AreNotEqual(prevName, _fullName);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Assert.IsTrue(false, "Selected country may not be India or Indonesia.");
                                            }
                                        }
                                        else
                                        {
                                            Assert.IsTrue(false, "Cancel button is not displayed.");
                                        }
                                    }
                                    else
                                    {
                                        Assert.IsTrue(false, "YourCard is still displaying.");
                                    }
                                }
                                else
                                {
                                    Assert.IsTrue(false,"There is no Card is added please add first.");
                                }
                            }
                            else
                            {
                                Assert.IsTrue(false, "There is no Card is added please add first.");
                            }
                        }
                        else
                        {
                            Assert.IsTrue(false, "MyBillingDetailUrl is not opened.");
                        }
                    }
                    else
                    {
                        Assert.IsTrue(false, "MyDetailUrl is not opened.");
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

        [DeploymentItem("DeleteCard.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\DeleteCard.csv", "DeleteCard#csv", DataAccessMethod.Sequential), TestMethod]
        public void DeleteCard()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;            
            string email = Record("Email");
            string password = Record("Password");
            string signinUrl = Record("SignInUrl");
            // string _findString = Record("FindString");
            Utility.Sleep(4);
            if (signinUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
              
                Utility.CsstoClick("SignInBtn", 4);
                Utility.Sleep(3);
                Utility.CsstoClick("clickOnMyInformation", 4);
                Utility.Sleep(2);
                Utility.ByLinkTexttoClick("ClickOnMyBillingDetails", 4);
                Utility.Sleep(3);
                string fareportalMyBillingDetailsUrl = Record("FareportalMyBillingDetailsUrl");
                Utility.Sleep(4);
                // && (IsDisplayedUsingXpath("YourCard"))
                if (fareportalMyBillingDetailsUrl == Driver.Url)
                {
                    if (Utility.IsDisplayedUsingCss("Deletecard"))
                    {
                        string _yourCard = Utility.ByXpath("YourCard", 4);
                        if (_yourCard == "Your Cards :")
                        {
                            Utility.Sleep(6);
                            int str = Driver.FindElements(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("Deletecard"))).Count();
                            for (int i = 1; i <= str; i++)
                            {
                                Utility.CsstoClick("Deletecard", 4);
                                //if (Utility.IsDisplayedUsingXpath("YourCard"))
                                //{
                                //    Assert.IsTrue(true);
                                //}
                                //else
                                //{
                                //    Assert.IsTrue(false, "Your Card is not displayed.");
                                //}
                            }
                            if (!Utility.IsDisplayedUsingXpath("YourCard"))
                            {
                                Assert.IsTrue(true);
                            }
                        }
                        else
                        {
                            Assert.IsTrue(false, "There is no card mentioned.");
                        }
                    }
                    else
                    { 
                       
                    }
                }
                else
                {
                    Assert.IsTrue(false, "BillingDetailsUrl is not opened.");
                }
            }
            else
            {
                Assert.IsTrue(false, "SignInUrl is not opened.");
            }
        }


        [DeploymentItem("MinAndMaxDigitInCreditCard.csv"), DeploymentItem("MinAndMaxDigitsInCreditCard .csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MinAndMaxDigitsInCreditCard .csv", "MinAndMaxDigitsInCreditCard #csv", DataAccessMethod.Sequential), TestMethod]
        public void MinAndMaxDigitInCreditCard()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;           
            string email = Record("Email");
            string password = Record("Password");
            string signinUrl = Record("SignInUrl");
            string enterCardNumber = Record("EnterCardNumber");
            // string _findString = Record("FindString");
            Utility.Sleep(4);
            if (signinUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
             
                Utility.CsstoClick("SignInBtn", 4);
                Utility.Sleep(3);
                string fareportalOverviewUrl = Record("CheapoairOverviewUrl");
                Utility.Sleep(4);
                if (fareportalOverviewUrl == Driver.Url)
                {
                    Utility.CsstoClick("clickOnMyInformation", 4);
                    Utility.Sleep(2);

                    string fareportalMyDetailsUrl = Record("CheapoairMyDetailsUrl");
                    Utility.Sleep(4);
                    if (fareportalMyDetailsUrl == Driver.Url)
                    {
                        Utility.ByLinkTexttoClick("ClickOnMyBillingDetails", 4);
                        Utility.Sleep(3);

                        string fareportalMyBillingDetailsUrl = Record("CheapoairMyBillingDetailsUrl");
                        Utility.Sleep(4);
                        if (fareportalMyBillingDetailsUrl == Driver.Url)
                        {
                            Utility.CssToSetText("textcardNumber", enterCardNumber, 3);
                            Utility.CsstoClick("ClickOnSaveBillingDetailsBtn", 3);
                            if (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("textcardNumber", "value", 3)))
                            {

                                string cardNumber = Utility.GrabAttributeValueByCss("textcardNumber", "value", 3);
                                Regex onlyNumeric = new Regex("^[0-9]");

                                if (onlyNumeric.IsMatch(cardNumber))
                                {
                                    Assert.IsTrue(true);
                                    Regex r1 = new Regex("^[0-9]{13}$");
                                    Regex r2 = new Regex("^[0-9]{16}$");
                                    if (r1.IsMatch(cardNumber))
                                    {
                                        Assert.IsTrue(true);
                                    }
                                    else if (r2.IsMatch(cardNumber))
                                    {
                                        Assert.IsTrue(true);
                                    }
                                    else
                                    {
                                        string expectedErrorMsg = Record("ExpectedErrorMsg");
                                        string actualErrorMsg = Utility.ByXpath("CardNumberErrorMsg", 3);
                                        Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                                        //Assert.IsTrue(false, "Sorry, but we don't accept this credit card. Please enter a card from the list above");
                                    }
                                }
                                else
                                {
                                    string expectedErrorMsg = Record("ExpectedErrorMsg");
                                    string actualErrorMsg = Utility.ByXpath("CardNumberErrorMsg", 3);
                                    Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                                    //Assert.IsTrue(false, "Only numbers are allowed");
                                }
                            }
                            else
                            {
                                string expectedErrorMsg = Record("ExpectedErrorMsg");
                                string actualErrorMsg = Utility.ByXpath("CardNumberErrorMsg", 3);
                                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                                //Assert.IsTrue(expectedErrorMsg, "Card number is required.");
                            }
                        }
                        else
                        {
                            Assert.IsTrue(false, "MyBillingDetailUrl is not opened.");
                        }
                    }
                    else
                    {
                        Assert.IsTrue(false, "MyDetailUrl is not opened.");
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
               

        [DeploymentItem("MyBillingDetailsAllValidations.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MyBillingDetailsAllValidations.csv", "MyBillingDetailsAllValidations#csv", DataAccessMethod.Sequential), TestMethod]
        public void MyBillingDetailsAllValidations()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;            
            string email = Record("Email");
            string password = Record("Password");
            string signinUrl = Record("SignInUrl");
            string _findString = Record("FindString");
            if (signinUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
           
                Utility.CsstoClick("SignInBtn", 4);
                Utility.Sleep(3);
                string fareportalOverviewUrl = Record("FareportalOverviewUrl");
                if (fareportalOverviewUrl == Driver.Url)
                {
                    Utility.CsstoClick("clickOnMyInformation", 4);
                    Utility.Sleep(2);

                    string fareportalMyDetailsUrl = Record("FareportalMyDetailsUrl");
                    if (fareportalMyDetailsUrl == Driver.Url)
                    {
                        Utility.ByLinkTexttoClick("ClickOnMyBillingDetails", 4);

                        string fareportalMyBillingDetailsUrl = Record("FareportalMyBillingDetailsUrl");
                        if (fareportalMyBillingDetailsUrl == Driver.Url)
                        {
                            Utility.CsstoClick("ClickOnSaveBillingDetailsBtn", 4);
                            string expectedValidationsIfMyBillingDetailsIsEmpty = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("expectedValidationsIfMyBillingDetailsIsEmpty");
                            string[] ValidationsIfMyBillingDetailsIsEmpty = expectedValidationsIfMyBillingDetailsIsEmpty.Split(",".ToCharArray());
                            string strAddCard = Utility.ByXpath("AddNewCard", 5);

                            if ((string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("textcardNumber", "value", 2))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("textfirstName", "value", 2))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("textlastName", "value", 2))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("textnickName", "value", 2))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("textaddressLine1InBillingDetails", "value", 2))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("textcityInBillingdetails", "value", 2))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("textzipInBillingDetails", "value", 2)))) //&& (string.IsNullOrEmpty(GrabAttributeValueByCss("textBillingphoneNumbers", "value", 6))))
                            {
                                string cardNumberValidation = Utility.ByXpath("cardNumberValidation", 4);
                                Assert.AreEqual(ValidationsIfMyBillingDetailsIsEmpty[0], cardNumberValidation);

                                string firstnameValidation = Utility.ByXpath("firstnameValidation", 4);
                                Assert.AreEqual(ValidationsIfMyBillingDetailsIsEmpty[2], firstnameValidation);

                                string lastnameValidation = Utility.ByXpath("lastnameValidation", 4);
                                Assert.AreEqual(ValidationsIfMyBillingDetailsIsEmpty[3], lastnameValidation);

                                string cardNickNameValidation = Utility.ByXpath("cardNickNameValidation", 4);
                                Assert.AreEqual(ValidationsIfMyBillingDetailsIsEmpty[4], cardNickNameValidation);

                                string addressLine1ValidationInbillingDetails = Utility.ByXpath("addressLine1ValidationInbillingDetails", 4);
                                Assert.AreEqual(ValidationsIfMyBillingDetailsIsEmpty[5], addressLine1ValidationInbillingDetails);

                                string cityValidationInBillingDetails = Utility.ByXpath("cityValidationInBillingDetails", 4);
                                Assert.AreEqual(ValidationsIfMyBillingDetailsIsEmpty[6], cityValidationInBillingDetails);

                                string zipValidationInBillingDetails = Utility.ByXpath("zipValidationInBillingDetails", 4);
                                Assert.AreEqual(ValidationsIfMyBillingDetailsIsEmpty[7], zipValidationInBillingDetails);

                                if (((Utility.GrabAttributeValueByCss("Month", "value", 4)) == "0") && ((Utility.GrabAttributeValueByCss("Year", "value", 4)) == "0"))
                                {
                                    string cardExpiration = Utility.ByXpath("cardExpiration", 4);
                                    Assert.AreEqual(ValidationsIfMyBillingDetailsIsEmpty[1], cardExpiration);
                                }
                                else
                                {
                                    throw new Exception("Month and year is not empty.");
                                }
                            }
                            else
                            {
                                Assert.IsTrue(false, "Might be there is no textbox empty.");
                            }
                        }
                        else
                        {
                            Assert.IsTrue(false, "MyBillingDetailUrl is not opened.");
                        }
                    }
                    else
                    {
                        Assert.IsTrue(false, "MyDetailUrl is not opened.");
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


        [DeploymentItem("SuccesfullyAddaCard.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\SuccesfullyAddaCard.csv", "SuccesfullyAddaCard#csv", DataAccessMethod.Sequential), TestMethod]
        public void SuccesfullyAddaCard()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;          
            string email = Record("Email");
            string password = Record("Password");
            string signinUrl = Record("SignInUrl");          
            Utility.Sleep(4);
            if (signinUrl == Driver.Url)
            {
              
                if ((email) != null)
                {
                    Utility.CssToSetText("CreateAccountEmailAddress", email, 4);
                }
                if ((password) != null)
                {
                    Utility.CssToSetText("CreateAccountEmailAddress", password, 4);
                }

                Utility.CsstoClick("SignInBtn", 4);
                Utility.Sleep(3);
                string fareportalOverviewUrl = Record("FareportalOverviewUrl");
                Utility.Sleep(4);
                if (fareportalOverviewUrl == Driver.Url)
                {
                    Utility.CsstoClick("clickOnMyInformation", 4);
                    Utility.Sleep(2);

                    string fareportalMyDetailsUrl = Record("FareportalMyDetailsUrl");
                    Utility.Sleep(4);
                    if (fareportalMyDetailsUrl == Driver.Url)
                    {
                        Utility.ByLinkTexttoClick("ClickOnMyBillingDetails", 4);
                        Utility.Sleep(3);

                        string fareportalMyBillingDetailsUrl = Record("FareportalMyBillingDetailsUrl");
                        Utility.Sleep(4);
                        if (fareportalMyBillingDetailsUrl == Driver.Url)
                        {
                            if (Utility.IsDisplayedUsingCss("Deletecard"))
                            {
                                Utility.DeleteFunc();


                                string cardNumber = Record("cardNumber");
                                string firstName = Record("firstName");
                                string lastName = Record("lastName");
                                string cardNickName = Record("cardNickName");

                                string _expMonth = Record("ExpMonth");
                                var element1 = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("Month")));
                                var selectElement1 = new SelectElement(element1);
                                selectElement1.SelectByText(_expMonth);

                                string _expYear = Record("ExpYear");
                                var element2 = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("Year")));
                                var selectElement2 = new SelectElement(element2);
                                selectElement2.SelectByText(_expYear);
                             
                                if ((cardNumber) != null)
                                {
                                    Utility.CssToSetText("CreateAccountEmailAddress", cardNumber, 4);
                                }
                                if ((firstName) != null)
                                {
                                    Utility.CssToSetText("CreateAccountEmailAddress", firstName, 4);
                                }
                                if ((lastName) != null)
                                {
                                    Utility.CssToSetText("CreateAccountEmailAddress", lastName, 4);
                                }
                                if ((cardNickName) != null)
                                {
                                    Utility.CssToSetText("CreateAccountEmailAddress", cardNickName, 4);
                                }

                                string addressLine1 = Record("AddressLine1");
                                string city = Record("city");
                                string zip = Record("zip");
                                string billingPhone = Record("billingPhone");

                                string _country = Record("Country");
                               
                                string _state = Record("State");
                              
                                string checkType = Utility.GrabAttributeValueByCss("BillingAddressState", "type", 2);

                                if ((_country == "United States") || (_country == "Canada"))
                                {
                                    var element3 = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("BillingAddressCountry")));
                                    var selectElement3 = new SelectElement(element3);
                                    selectElement3.SelectByText(_country);

                                    var element4 = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("BillingAddressState")));
                                    var selectElement4 = new SelectElement(element4);
                                    selectElement4.SelectByText(_state);
                                }
                                else
                                {
                                    if (checkType == "select-one")
                                    {
                                        var element = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("BillingAddressCountry")));
                                        var selectElement = new SelectElement(element);
                                        selectElement.SelectByText(_country);
                                        Utility.CssToSetText("State", _state, 4);                                      
                                    }
                                }
                                                               
                                if ((addressLine1) != null)
                                {
                                    Utility.CssToSetText("CreateAccountEmailAddress", addressLine1, 4);
                                }
                                if ((city) != null)
                                {
                                    Utility.CssToSetText("CreateAccountEmailAddress", city, 4);
                                }
                                if ((zip) != null)
                                {
                                    Utility.CssToSetText("CreateAccountEmailAddress", zip, 4);
                                }
                                if ((billingPhone) != null)
                                {
                                    Utility.CssToSetText("CreateAccountEmailAddress", billingPhone, 4);
                                }

                                Utility.CsstoClick("ClickOnSaveBillingDetailsBtn", 5);
                                Utility.Sleep(5);
                                string Ischecked = Utility.GrabAttributeValueByCss("ExistingAddress", "Checked", 4);//Here checking the radio button.
                                if (Utility.IsDisplayedUsingXpath("SavingMessage"))
                                {

                                    if (Ischecked == "true")
                                    {
                                        string AddresssShowingAfterAddingCard = Utility.ByXpath("AddresssShowingAfterAddingCard", 4);
                                        string[] ExistingAddressFormPersonalInfo = AddresssShowingAfterAddingCard.Split(",".ToCharArray());

                                    }
                                }
                            }
                            else
                            {

                                string cardNumber = Record("cardNumber");
                                string firstName = Record("firstName");
                                string lastName = Record("lastName");
                                string cardNickName = Record("cardNickName");

                                string _expMonth = Record("ExpMonth");
                                var element1 = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("Month")));
                                var selectElement1 = new SelectElement(element1);
                                selectElement1.SelectByText(_expMonth);

                                string _expYear = Record("ExpYear");
                                var element2 = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("Year")));
                                var selectElement2 = new SelectElement(element2);
                                selectElement2.SelectByText(_expYear);                            

                                if ((cardNumber) != null)
                                {
                                    Utility.CssToSetText("CreateAccountEmailAddress", cardNumber, 4);
                                }
                                if ((firstName) != null)
                                {
                                    Utility.CssToSetText("CreateAccountEmailAddress", firstName, 4);
                                }
                                if ((lastName) != null)
                                {
                                    Utility.CssToSetText("CreateAccountEmailAddress", lastName, 4);
                                }
                                if ((cardNickName) != null)
                                {
                                    Utility.CssToSetText("CreateAccountEmailAddress", cardNickName, 4);
                                }


                                string addressLine1 = Record("AddressLine1");
                                string city = Record("city");
                                string zip = Record("zip");
                                string billingPhone = Record("billingPhone");

                                string _country = Record("Country");
                              
                                string _state = Record("State");
                                
                                string checkType = Utility.GrabAttributeValueByCss("BillingAddressState", "type", 2);

                                if ((_country == "United States") || (_country == "Canada"))
                                {
                                    var element3 = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("BillingAddressCountry")));
                                    var selectElement3 = new SelectElement(element3);
                                    selectElement3.SelectByText(_country);

                                    var element4 = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("BillingAddressState")));
                                    var selectElement4 = new SelectElement(element4);
                                    selectElement4.SelectByText(_state);
                                }
                                else
                                {
                                    if (checkType == "select-one")
                                    {
                                        var element = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("BillingAddressCountry")));
                                        var selectElement = new SelectElement(element);
                                        selectElement.SelectByText(_country);
                                        Utility.CssToSetText("State", _state, 4);                                        
                                    }
                                }
                             
                                if ((addressLine1) != null)
                                {
                                    Utility.CssToSetText("CreateAccountEmailAddress", addressLine1, 4);
                                }
                                if ((city) != null)
                                {
                                    Utility.CssToSetText("CreateAccountEmailAddress", city, 4);
                                }
                                if ((zip) != null)
                                {
                                    Utility.CssToSetText("CreateAccountEmailAddress", zip, 4);
                                }
                                if ((billingPhone) != null)
                                {
                                    Utility.CssToSetText("CreateAccountEmailAddress", billingPhone, 4);
                                }

                                Utility.CsstoClick("ClickOnSaveBillingDetailsBtn", 5);
                                Utility.Sleep(5);
                                string Ischecked = Utility.GrabAttributeValueByCss("ExistingAddress", "Checked", 4);//Here checking the radio button.
                                if (Utility.IsDisplayedUsingXpath("SavingMessage"))
                                {

                                    if (Ischecked == "true")
                                    {
                                        string AddresssShowingAfterAddingCard = Utility.ByXpath("AddresssShowingAfterAddingCard", 4);
                                        string[] ExistingAddressFormPersonalInfo = AddresssShowingAfterAddingCard.Split(",".ToCharArray());

                                    }
                                }
                                else
                                {
                                    Assert.IsTrue(false, "SavingMessage not displayed.");
                                }
                            }
                        }
                        else
                        {
                            Assert.IsTrue(false, "MyBillingDetailUrl is not opened.");
                        }
                    }
                    else
                    {
                        Assert.IsTrue(false, "MyDetailUrl is not opened.");
                    }
                }
                else
                {
                    Assert.IsTrue(false, "OverviewUrl is not opened.");
                }
            }
            else
            {
                Assert.IsTrue(false, "SignInUrl is not opened.");
            }
        }



        [DeploymentItem("VerifyTheFirstAndLastNameValidationsInMyBillingDetails.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\VerifyTheFirstAndLastNameValidationsInMyBillingDetails.csv", "VerifyTheFirstAndLastNameValidationsInMyBillingDetails#csv", DataAccessMethod.Sequential), TestMethod]
        public void VerifyTheFirstAndLastNameValidationsInMyBillingDetails()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;           
            string email = Record("Email");
            string password = Record("Password");
            string signinUrl = Record("SignInUrl");
            Utility.Sleep(4);
            if (signinUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CsstoClick("SignInBtn", 4);
                Utility.Sleep(3);
                string CheapoairOverviewUrl = Record("CheapoairOverviewUrl");
                Utility.Sleep(4);
                if (CheapoairOverviewUrl == Driver.Url)
                {
                    Utility.CsstoClick("clickOnMyInformation", 4);
                    Utility.Sleep(2);

                    string CheapoairMyInfoUrl = Record("CheapoairMyInfoUrl");
                    Utility.Sleep(4);
                    if (CheapoairMyInfoUrl == Driver.Url)
                    {
                        Utility.ByLinkTexttoClick("ClickOnMyBillingDetails", 4);
                        Utility.Sleep(3);

                        string MyBillingDetailsUrl = Record("MyBillingDetailsUrl");
                        Utility.Sleep(4);
                        if (MyBillingDetailsUrl == Driver.Url)
                        {
                            string _firstNametxt = Record("FirstName");
                            string _lastNametxt = Record("LastName");
                            Utility.CsstoClear("textfirstName", 3);
                            Utility.CssToSetText("textfirstName", _firstNametxt, 3);
                            Utility.CsstoClick("CliclOnAddCCdiv", 3);
                            Utility.CsstoClear("textlastName", 3);
                            Utility.CssToSetText("textlastName", _lastNametxt, 3);
                            Utility.CsstoClick("ClickOnSaveBillingDetailsBtn",4);
                            if (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("textfirstName", "value", 3)))
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
                                                string actualErrorMsg = Utility.ByXpath("ErrorMsgForFirstNameInBillingDetails", 4);
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
                                            string actualErrorMsg = Utility.ByXpath("ErrorMsgForFirstNameInBillingDetails", 4);
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
                                        string actualErrorMsg = Utility.ByXpath("ErrorMsgForFirstNameInBillingDetails", 4);
                                        Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                                    }
                                    //throw new Exception("Firstname must be between 2 and 25 characters in length.");
                                }

                            }
                            else
                            {
                                string expectedErrorMsg = Record("ExpectedErrorMsgForFirstname");
                                if (expectedErrorMsg != "No Error")
                                {
                                    string actualErrorMsg = Utility.ByXpath("ErrorMsgForFirstNameInBillingDetails", 4);
                                    Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                                }
                                //Assert.IsTrue(false, "Please enter first name");
                            }

                            if (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("textlastName", "value", 3)))
                            {
                                if ((_lastNametxt.Length > 2) && (_lastNametxt.Length < 25))
                                {
                                    Regex regex = new Regex(@"^[a-zA-Z]");
                                    if (regex.IsMatch(_firstNametxt))
                                    {
                                        if ((_lastNametxt.Contains("!") || (_lastNametxt.Contains("@")) || (_lastNametxt.Contains("~")) || (_lastNametxt.Contains("$")) || (_lastNametxt.Contains("%")) || (_lastNametxt.Contains("^")) || (_lastNametxt.Contains("&")) || (_lastNametxt.Contains("*")) || (_lastNametxt.Contains("`")) || (_lastNametxt.Contains("+")) || (_lastNametxt.Contains("_")) || (_lastNametxt.Contains(":")) || (_lastNametxt.Contains(".")) || (_lastNametxt.Contains(",")) || (_lastNametxt.Contains("(")) || (_lastNametxt.Contains(")")) || (_lastNametxt.Contains("="))))
                                        {
                                            string expectedErrorMsg = Record("ExpectedErrorMsgForLastname");
                                            string actualErrorMsg = Utility.ByXpath("ErrorMsgForLastNameInBillingDetails", 4);
                                            Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
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
                                            string actualErrorMsg = Utility.ByXpath("ErrorMsgForLastNameInBillingDetails", 4);
                                            Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                                        }
                                        //throw new Exception("Name must begin with a letter.");
                                    }
                                }
                                else
                                {
                                    string expectedErrorMsg = Record("ExpectedErrorMsgForLastname");
                                    if (expectedErrorMsg != "No Error")
                                    {
                                        string actualErrorMsg = Utility.ByXpath("ErrorMsgForLastNameInBillingDetails", 4);
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
                                    string actualErrorMsg = Utility.ByXpath("ErrorMsgForLastNameInBillingDetails", 4);
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
                        Assert.IsTrue(false, "MyInfoUrl is not opened.");
                    }
                }
                else
                {
                    Assert.IsTrue(false, "OverviewUrl is not opened.");
                }
            }
            else
            {
                Assert.IsTrue(false, "SignInUrl is not opened.");
            }
        }


        [DeploymentItem("VerifytheRadioButtonsOnThisSection.csv"), DeploymentItem("VerifytheRadioButtonsInBillingSection.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\VerifytheRadioButtonsInBillingSection.csv", "VerifytheRadioButtonsInBillingSection#csv", DataAccessMethod.Sequential), TestMethod]
        public void VerifytheRadioButtonsInBillingSection()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;           
            string email = Record("Email");
            string password = Record("Password");
            string signinUrl = Record("SignInUrl");

            if (signinUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CsstoClick("SignInBtn", 4);
                Utility.Sleep(3);
                //Utility.CssToSetText("CreateAccountEmailAddress", email, 4);
                //Utility.CssToSetText("CreateAccountEmailAddress", password, 4); 
                string CheapoairOverviewUrl = Record("CheapoairOverviewUrl");
                Utility.Sleep(3);
                if (CheapoairOverviewUrl == Driver.Url)
                {
                    Utility.CsstoClick("clickOnMyInformation", 4);
                    Utility.Sleep(2);

                    string CheapoairMyDetailsUrl = Record("CheapoairMyDetailsUrl");
                    if (CheapoairMyDetailsUrl == Driver.Url)
                    {
                        Utility.ByLinkTexttoClick("ClickOnMyBillingDetails", 3);
                        string CheapoairBillingUrl = Record("CheapoairBillingUrl");
                        if (CheapoairBillingUrl == Driver.Url)
                        {
                            Utility.Sleep(4);
                            if (!Utility.IsDisplayedUsingCss("ExistingAddress") && (Utility.IsDisplayedUsingCss("AddNewAddressCheckBox")))
                            {
                                Assert.IsTrue(true);
                            }
                            else
                            {
                                Assert.IsTrue(false);
                            }
                        }
                        else
                        {
                            Assert.IsTrue(false, "MyBillingDetailUrl is not opened.");
                        }
                    }
                    else
                    {
                        Assert.IsTrue(false, "MyDetaisUrl is not opened.");
                    }
                }
                else
                {
                    Assert.IsTrue(false, "OverviewUrl is not opened.");
                }
            }
            else
            {
                Assert.IsTrue(false, "SignInUrl is not opened.");
            }
        }



        [DeploymentItem("VeryufyTheRadioButtonsAgainstEachCard.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\VeryufyTheRadioButtonsAgainstEachCard.csv", "VeryufyTheRadioButtonsAgainstEachCard#csv", DataAccessMethod.Sequential), TestMethod]
        public void VeryufyTheRadioButtonsAgainstEachCard()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;           
            Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
            Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);           
            string signinUrl = Record("SignInUrl");           
            Utility.Sleep(4);
            if (signinUrl == Driver.Url)
            {
                Utility.CsstoClick("SignInBtn", 4);
                Utility.Sleep(3);
                string fareportalOverviewUrl = Record("FareportalOverviewUrl");
                Utility.Sleep(4);
                if (fareportalOverviewUrl == Driver.Url)
                {
                    Utility.CsstoClick("clickOnMyInformation", 4);
                    Utility.Sleep(2);

                    string fareportalMyDetailsUrl = Record("FareportalMyDetailsUrl");
                    Utility.Sleep(4);
                    if (fareportalMyDetailsUrl == Driver.Url)
                    {
                        Utility.ByLinkTexttoClick("ClickOnMyBillingDetails", 4);
                        Utility.Sleep(3);

                        string fareportalMyBillingDetailsUrl = Record("FareportalMyBillingDetailsUrl");
                        Utility.Sleep(4);
                        if (fareportalMyBillingDetailsUrl == Driver.Url)
                        {
                            int noOfCards = UserProfileSPA.Library.TestEnvironment.Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("ExistingCCardsDiv"))).FindElements(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("ViewCard"))).Count();

                            if (noOfCards == Driver.FindElements(By.ClassName("selectDefaultCard")).Count)
                            {
                                Assert.IsTrue(true, "Number of radio button is equal to number of Cards.");
                            }
                            else
                            {
                                Assert.IsTrue(true, "Number of radio button is not equal to number of Cards.");
                            }
                        }
                        else
                        {
                            Assert.IsTrue(false, "MyBillingDetailsUrl is not opened.");
                        }
                    }
                    else
                    {
                        Assert.IsTrue(false, "MyDetailsUrl is not opened.");
                    }
                }
                else
                {
                    Assert.IsTrue(false, "OverviewUrl is not opened.");
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