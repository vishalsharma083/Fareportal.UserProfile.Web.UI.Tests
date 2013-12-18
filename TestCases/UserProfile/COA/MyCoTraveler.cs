using System;
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
    public class MyCoTraveler 
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



        [DeploymentItem("AddNewCoTraveler.csv"), DeploymentItem("AppData\\AddNewCoTraveler.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\AddNewCoTraveler.csv", "AddNewCoTraveler#csv", DataAccessMethod.Sequential), TestMethod]
        public void AddCoTraveler()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            string _baseUrl = Record("SignInUrl");
            if (_baseUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(2);
                Utility.CsstoClick("clickOnMyInformation", 4);
                Utility.Sleep(2);
                string cheapoairMyInfoUrl = Record("CheapoairMyInfoUrl");
                if (cheapoairMyInfoUrl == Driver.Url)
                {
                    Utility.ByLinkTexttoClick("clickOnMyCoTravelerMenu", 4);

                    string myCoTravelerUrl = Record("MyCoTravelerUrl");
                    if (myCoTravelerUrl == Driver.Url)
                    {
                        Utility.CsstoClick("ClickOnAddCoTravellerBtn", 3);
                        Utility.Sleep(7);
                        if (!Utility.IsDisplayedUsingCss("ClickOnAddCoTravellerBtn"))
                        {
                            if ((Utility.IsDisplayedUsingCss("CoTravelerFlightPreference")) && (Utility.IsDisplayedUsingCss("CotravelerHotelPreference")) && (Utility.IsDisplayedUsingCss("CoTravelerCarPreference")) && (Utility.IsDisplayedUsingCss("AddCoTravelerSection")))
                            {
                                string selectAgeGroupForFirstCotraveler = Record("selectAgeGroupForFirstCotraveler");
                                var enterAgeGroupForFirstCoTraveler = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("SelectCoTravellerAgeGroup")));
                                var selectElementForFirstCotraveler = new SelectElement(enterAgeGroupForFirstCoTraveler);
                                selectElementForFirstCotraveler.SelectByText(selectAgeGroupForFirstCotraveler);                               

                                string _firstNameForFirstCotraveler = Record("FirstNameForFirstCotraveler");
                                string _lastNameForFirstCotraveler = Record("LastNameForFirstCotraveler");
                                Utility.CssToSetText("textInFirstname", _firstNameForFirstCotraveler, 3);
                                Utility.CssToSetText("textInlastName", _lastNameForFirstCotraveler, 3);
                                string _completeName = _firstNameForFirstCotraveler + " " + _lastNameForFirstCotraveler;


                                string _monthForFirstCotraveler = Record("MonthForFirstCotraveler");
                                var selectMonthForFirstCotraveler = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerMonth")));
                                var selectElementMonthForFirstCotraveler = new SelectElement(selectMonthForFirstCotraveler);
                                selectElementMonthForFirstCotraveler.SelectByText(_monthForFirstCotraveler);

                                string _dayForFirstCotraveler = Record("DayForFirstCotraveler");
                                var selectDayFirstCotraveler = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerDay")));
                                var selectElementDayForFirstCotraveler = new SelectElement(selectDayFirstCotraveler);
                                selectElementDayForFirstCotraveler.SelectByText(_dayForFirstCotraveler);

                                string _yearForFirstCotraveler = Record("YearForFirstCotraveler");
                                var selectYearFirstCotraveler = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerYear")));
                                var selectElementYearForFirstCotraveler = new SelectElement(selectYearFirstCotraveler);
                                selectElementYearForFirstCotraveler.SelectByText(_yearForFirstCotraveler);

                                string _genderForFirstCotraveler = Record("GenderForFirstCotraveler");
                                var selectGenderForFirstCotraveler = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerGender")));
                                var selectElementGenderForFirstCotraveler = new SelectElement(selectGenderForFirstCotraveler);
                                selectElementGenderForFirstCotraveler.SelectByText(_genderForFirstCotraveler);

                                string SelectTitleForFirstCotraveler = Record("SelectTitleForFirstCotraveler");
                                var enterTitleForFirstCotraveler = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("SelectCoTravellerTitle")));
                                var selectElementTitleForFirstCotraveler = new SelectElement(enterTitleForFirstCotraveler);
                                selectElementTitleForFirstCotraveler.SelectByText(SelectTitleForFirstCotraveler);

                                Utility.XPathtoClick("ClickOnFlightPreferecne", 3);
                                Utility.Sleep(3);
                                string _homeAirportForFirstCotraveler = Record("HomeAirportForFirstCotraveler");
                                Utility.CssToSetText("CoTravelerHomeAirPort", _homeAirportForFirstCotraveler, 3);
                                Utility.CsstoClick("ClickOnSaveCoTravelerBtn", 3);
                                 Utility.Sleep(7);
                                if (Utility.IsDisplayedUsingCss("ClickOnAddCoTravellerBtn"))
                                {
                                    Utility.CsstoClick("ClickOnAddCoTravellerBtn", 3);
                                    Utility.Sleep(2);
                                    if (!Utility.IsDisplayedUsingCss("ClickOnAddCoTravellerBtn"))
                                    {

                                        if ((Utility.IsDisplayedUsingCss("CoTravelerFlightPreference")) && (Utility.IsDisplayedUsingCss("CotravelerHotelPreference")) && (Utility.IsDisplayedUsingCss("CoTravelerCarPreference")) && (Utility.IsDisplayedUsingCss("AddCoTravelerSection")))
                                        {

                                            string selectAgeGroupForSecondCotraveler = Record("selectAgeGroupForSecondCotraveler");
                                            var enterAgeGroupForSecondCoTraveler = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("SelectCoTravellerAgeGroup")));
                                            var selectElementForSecondCotraveler = new SelectElement(enterAgeGroupForSecondCoTraveler);
                                            selectElementForSecondCotraveler.SelectByText(selectAgeGroupForSecondCotraveler);                                           

                                            string _firstNameForSecondCotraveler = Record("FirstNameForSecondCotraveler");
                                            string _lastNameForSecondCotraveler = Record("LastNameForSecondCotraveler");
                                            Utility.CssToSetText("textInFirstname", _firstNameForSecondCotraveler, 3);
                                            Utility.CssToSetText("textInlastName", _lastNameForSecondCotraveler, 3);
                                            string _completeNameForSecondCoTraveler = _firstNameForSecondCotraveler + " " + _lastNameForSecondCotraveler;


                                            string _monthForSecondCotraveler = Record("MonthForSecondCotraveler");
                                            var selectMonthForSecondCotraveler = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerMonth")));
                                            var selectElementMonthForSecondCotraveler = new SelectElement(selectMonthForSecondCotraveler);
                                            selectElementMonthForSecondCotraveler.SelectByText(_monthForSecondCotraveler);

                                            string _dayForSecondCotraveler = Record("DayForSecondCotraveler");
                                            var selectDaySecondCotraveler = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerDay")));
                                            var selectElementDayForSecondCotraveler = new SelectElement(selectDaySecondCotraveler);
                                            selectElementDayForSecondCotraveler.SelectByText(_dayForSecondCotraveler);

                                            string _yearForSecondCotraveler = Record("YearForSecondCotraveler");
                                            var selectYearSecondCotraveler = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerYear")));
                                            var selectElementYearForSecondCotraveler = new SelectElement(selectYearSecondCotraveler);
                                            selectElementYearForSecondCotraveler.SelectByText(_yearForSecondCotraveler);

                                            string _genderForSecondCotraveler = Record("GenderForSecondCotraveler");
                                            var selectGenderForSecondCotraveler = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerGender")));
                                            var selectElementGenderForSecondCotraveler = new SelectElement(selectGenderForSecondCotraveler);
                                            selectElementGenderForSecondCotraveler.SelectByText(_genderForSecondCotraveler);

                                            string SelectTitleForSecondCotraveler = Record("SelectTitleForSecondCotraveler");
                                            var enterTitleForSecondCotraveler = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("SelectCoTravellerTitle")));
                                            var selectElementTitleForSecondCotraveler = new SelectElement(enterTitleForSecondCotraveler);
                                            selectElementTitleForSecondCotraveler.SelectByText(SelectTitleForSecondCotraveler);

                                            Utility.XPathtoClick("ClickOnFlightPreferecne", 3);
                                            Utility.Sleep(3);
                                            string _homeAirportForSecondCotraveler = Record("HomeAirportForSecondCotraveler");
                                            Utility.CssToSetText("CoTravelerHomeAirPort", _homeAirportForSecondCotraveler, 3);
                                            Utility.CsstoClick("ClickOnSaveCoTravelerBtn", 3);
                                            Utility.Sleep(3);


                                            string xpath = "html/body/div[1]/div[2]/div[2]/div/div/div[4]/div/div/div[2]/div/div/div/form/div[1]/div/div[2]";
                                            if (Utility.IsDisplayedUsingXpathForMoltingInnerText(xpath))
                                            {
                                                string getDetails = Driver.FindElement(By.XPath(xpath)).Text;
                                                string[] detailsforFirstCoTraveler = getDetails.Replace("\r\n", "_").Split("_".ToCharArray());
                                                Assert.AreEqual(_completeName, detailsforFirstCoTraveler[0]);

                                                int str = Driver.FindElements(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DeleteCoTraveler"))).Count();
                                                int num = Convert.ToInt32(2);
                                                for (int i = 1; i <= str; i++)
                                                {
                                                    xpath = "html/body/div[1]/div[2]/div[2]/div/div/div[4]/div/div/div[2]/div/div/div/form/div[1]/div/div[" + (num + i) + "]";
                                                    if (Utility.IsDisplayedUsingXpathForMoltingInnerText(xpath))
                                                    {
                                                        getDetails = Driver.FindElement(By.XPath(xpath)).Text;
                                                        string[] detailsForOthers = getDetails.Replace("\r\n", "_").Split("_".ToCharArray());
                                                        Assert.AreEqual(_completeNameForSecondCoTraveler, detailsForOthers[0]);

                                                    }
                                                }
                                                for (int i = 1; i <= str; i++)
                                                {
                                                    Utility.CsstoClick("DeleteCoTraveler", 3);
                                                }
                                                Utility.CsstoClick("ClickOnWelcomeDropdown", 4);
                                                Utility.Sleep(2);
                                                Utility.CsstoClick("ClickOnSignOut", 4);
                                            }
                                            else
                                            {
                                                //there is no cotraveller at the moment.
                                            }
                                        }
                                        else
                                        {
                                            Assert.IsTrue(false, "Add CoTraveler Button still there.");
                                        }
                                    }
                                    else
                                    {
                                        Assert.IsTrue(false, "AddCoTraveler Button still displayed.");
                                    }
                                }
                                else
                                {
                                    Assert.IsTrue(false, "Might be sections not displayed");
                                }
                            }
                            else
                            {
                                Assert.IsTrue(false, "Add CoTraveler Button still there.");
                            }
                        }
                    }
                }
            }
        }


        //[DeploymentItem("AddNewCoTraveler.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\AddNewCoTraveler.csv", "AddNewCoTraveler#csv", DataAccessMethod.Sequential), TestMethod]
        //public void AddNewCoTraveler()
        //{
        //    IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;           
        //    string _baseUrl = Record("SignInUrl");
        //    if (_baseUrl == Driver.Url)
        //    {
        //        Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
        //        Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);               
        //        Utility.CsstoClick("SignInBtn", 3);
        //        Utility.Sleep(2);
        //        Utility.CsstoClick("clickOnMyInformation", 4);
        //        Utility.Sleep(2);
        //        string cheapoairMyInfoUrl = Record("CheapoairMyInfoUrl");
        //        if (cheapoairMyInfoUrl == Driver.Url)
        //        {
        //            Utility.ByLinkTexttoClick("clickOnMyCoTravelerMenu", 4);

        //            string myCoTravelerUrl = Record("MyCoTravelerUrl");
        //            if (myCoTravelerUrl == Driver.Url)
        //            {

        //                if (Utility.IsDisplayedUsingXpath("AddCoTravelerDiv"))
        //                {
        //                    int str = Driver.FindElements(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DeleteCoTraveler"))).Count();

        //                    for (int i = 1; i <= str; i++)
        //                    {
        //                        Utility.CsstoClick("DeleteCoTraveler", 3);
        //                        if (!Utility.IsDisplayedUsingXpath("AddCoTravelerDiv"))
        //                        {
        //                            Assert.IsTrue(true);
        //                        }
        //                    }

        //                    if (Utility.IsDisplayedUsingCss("ClickOnAddCoTravellerBtn"))
        //                    {
        //                        Utility.CsstoClick("ClickOnAddCoTravellerBtn", 3);
        //                        Utility.Sleep(7);
        //                        if ((Utility.IsDisplayedUsingCss("CoTravelerFlightPreference")) && (Utility.IsDisplayedUsingCss("CotravelerHotelPreference")) && (Utility.IsDisplayedUsingCss("CoTravelerCarPreference")) && (Utility.IsDisplayedUsingCss("AddCoTravelerSection")))
        //                        {

        //                            string selectAgeGroup = Record("selectAgeGroup");
        //                            var enterAgeGroup = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("SelectCoTravellerAgeGroup")));
        //                            var selectElement = new SelectElement(enterAgeGroup);
        //                            selectElement.SelectByText(selectAgeGroup);

        //                            string selectTitle = Record("SelectTitle");
        //                            var enterTitle = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("SelectCoTravellerAgeGroup")));
        //                            var selectElementTitle = new SelectElement(enterTitle);
        //                            selectElementTitle.SelectByText(selectTitle);

        //                            string _firstNametxt = Record("FirstName");
        //                            char[] firstChar = _firstNametxt.ToCharArray();
        //                            string _lastNametxt = Record("LastName");
        //                            Utility.CssToSetText("textInFirstname", _firstNametxt, 3);
        //                            Utility.CssToSetText("textInlastName", _lastNametxt, 3);


        //                            string _month = Record("Month");
        //                            var selectMonth = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerMonth")));
        //                            var selectElementMonth = new SelectElement(selectMonth);
        //                            selectElementMonth.SelectByText(_month);

        //                            string _day = Record("Day");
        //                            var selectDay = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerMonth")));
        //                            var selectElementDay = new SelectElement(selectDay);
        //                            selectElementDay.SelectByText(_day);

        //                            string _year = Record("Year");
        //                            var selectYear = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerMonth")));
        //                            var selectElementYear = new SelectElement(selectYear);
        //                            selectElementYear.SelectByText(_year);

        //                            string _gender = Record("Gender");
        //                            var selectGender = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerMonth")));
        //                            var selectElementGender = new SelectElement(selectGender);
        //                            selectElementGender.SelectByText(_gender);

        //                            Utility.CsstoClick("ClickOnFlightPreferecne", 3);
        //                            Utility.Sleep(3);
        //                            string _homeAirport = Record("HomeAirport");
        //                            Utility.CssToSetText("CoTravelerHomeAirPort", _homeAirport, 3);
        //                            Utility.CsstoClick("ClickOnSaveCoTravelerBtn", 3);

        //                            Utility.Sleep(3);
        //                            if (Utility.IsDisplayedUsingCss("ClickOnAddCoTravellerBtn"))
        //                            {
        //                                string getDetails = Driver.FindElement(By.XPath("html/body/div[1]/div[2]/div[2]/div/div/div[4]/div/div/div[2]/div/div/div/form/div[1]/div/div[2]/div[1]")).Text;//CssSelector(TestEnvironment.LoadXML("AddCoTravelerDiv"))).FindElements(By.CssSelector(TestEnvironment.LoadXML("CotravelerDetailsDiv"))).ToList();

        //                            }

        //                        }
        //                        else
        //                        {
        //                            Assert.IsTrue(false, "Might be sections not displayed");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Assert.IsTrue(false, "Add CoTraveler Button still there.");
        //                    }
        //                }
        //                else
        //                {

        //                    if (Utility.IsDisplayedUsingCss("ClickOnAddCoTravellerBtn"))
        //                    {
        //                        Utility.CsstoClick("ClickOnAddCoTravellerBtn", 3);
        //                        Utility.Sleep(2);
        //                        if (!Utility.IsDisplayedUsingCss("ClickOnAddCoTravellerBtn"))
        //                        {
        //                            if ((Utility.IsDisplayedUsingCss("CoTravelerFlightPreference")) && (Utility.IsDisplayedUsingCss("CotravelerHotelPreference")) && (Utility.IsDisplayedUsingCss("CoTravelerCarPreference")) && (Utility.IsDisplayedUsingCss("AddCoTravelerSection")))
        //                            {

        //                                string selectAgeGroup = Record("selectAgeGroup");
        //                                var enterAgeGroup = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("SelectCoTravellerAgeGroup")));
        //                                var selectElement = new SelectElement(enterAgeGroup);
        //                                selectElement.SelectByText(selectAgeGroup);

        //                                string selectTitle = Record("SelectTitle");
        //                                var enterTitle = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("SelectCoTravellerTitle")));
        //                                var selectElementTitle = new SelectElement(enterTitle);
        //                                selectElementTitle.SelectByText(selectTitle);

        //                                string _firstNametxt = Record("FirstName");
        //                                char[] firstChar = _firstNametxt.ToCharArray();
        //                                string _lastNametxt = Record("LastName");
        //                                Utility.CssToSetText("textInFirstname", _firstNametxt, 3);
        //                                Utility.CssToSetText("textInlastName", _lastNametxt, 3);


        //                                string _month = Record("Month");
        //                                var selectMonth = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerMonth")));
        //                                var selectElementMonth = new SelectElement(selectMonth);
        //                                selectElementMonth.SelectByText(_month);

        //                                string _day = Record("Day");
        //                                var selectDay = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerDay")));
        //                                var selectElementDay = new SelectElement(selectDay);
        //                                selectElementDay.SelectByText(_day);

        //                                string _year = Record("Year");
        //                                var selectYear = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerYear")));
        //                                var selectElementYear = new SelectElement(selectYear);
        //                                selectElementYear.SelectByText(_year);

        //                                string _gender = Record("Gender");
        //                                var selectGender = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerGender")));
        //                                var selectElementGender = new SelectElement(selectGender);
        //                                selectElementGender.SelectByText(_gender);

        //                                Utility.XPathtoClick("ClickOnFlightPreferecne", 3);
        //                                Utility.Sleep(3);
        //                                string _homeAirport = Record("HomeAirport");
        //                                Utility.CssToSetText("CoTravelerHomeAirPort", _homeAirport, 3);
        //                                Utility.CsstoClick("ClickOnSaveCoTravelerBtn", 3);

        //                                string xpath = "html/body/div[1]/div[2]/div[2]/div/div/div[4]/div/div/div[2]/div/div/div/form/div[1]/div/div[2]";
        //                                if (Utility.IsDisplayedUsingXpath(xpath))
        //                                { }
        //                                string getDetails = Driver.FindElement(By.XPath(xpath)).Text;
        //                                int str = Driver.FindElements(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DeleteCoTraveler"))).Count();
        //                                int num = Convert.ToInt32(2);
        //                                for (int i = 1; i <= str; i++)
        //                                {
        //                                    xpath = "html/body/div[1]/div[2]/div[2]/div/div/div[4]/div/div/div[2]/div/div/div/form/div[1]/div/div[" + (num + i) + "]";

        //                                    if (Utility.IsDisplayedUsingXpath(xpath))
        //                                    {
        //                                        getDetails = Driver.FindElement(By.XPath(xpath)).Text;
        //                                    }
        //                                }
        //                            }
        //                            else
        //                            {
        //                                Assert.IsTrue(false, "Might be sections not displayed");
        //                            }
        //                        }
        //                        else
        //                        {
        //                            Assert.IsTrue(false, "Add CoTraveler Button still there.");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Assert.IsTrue(false, "Add CoTraveler Button not displayed.");
        //                    }

        //                }                    

        //            }
        //        }
        //    }
        //}


        [DeploymentItem("CheckTitleForMyCotraveler.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\CheckTitleForMyCotraveler.csv", "CheckTitleForMyCotraveler#csv", DataAccessMethod.Sequential), TestMethod]
        public void CheckTitleForMyCotraveler()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            string _baseUrl = Record("SignInUrl");
            if (_baseUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);               
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(2);
                Utility.CsstoClick("clickOnMyInformation", 4);
                Utility.Sleep(2);
                string cheapoairMyInfoUrl = Record("CheapoairMyInfoUrl");
                if (cheapoairMyInfoUrl == Driver.Url)
                {
                    Utility.ByLinkTexttoClick("clickOnMyCoTravelerMenu", 4);

                    string myCoTravelerUrl = Record("MyCoTravelerUrl");
                    if (myCoTravelerUrl == Driver.Url)
                    {
                        Utility.CsstoClick("ClickOnAddCoTravelerBtn", 4);

                        string SelectFromAgeGroup = Record("SelectFromAgeGroup");
                        var enterOneOfAgeGroup = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("SelectCoTravellerAgeGroup")));
                        var selectElement = new SelectElement(enterOneOfAgeGroup);
                        selectElement.SelectByText(SelectFromAgeGroup);
                        Utility.Sleep(4);

                        string titlesForAdultAndSenior = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("titlesForAdultAndSenior");
                        string[] _titlesForAdultAndSenior = titlesForAdultAndSenior.Split(",".ToCharArray());
                        string titleForChildAndInfant = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("titleForChildAndInfant");
                        string[] _titleForChildAndInfant = titleForChildAndInfant.Split(",".ToCharArray());
                        if (((Utility.GrabAttributeValueByCss("SelectCoTravellerAgeGroup", "value", 4)) == "1") || ((Utility.GrabAttributeValueByCss("SelectCoTravellerAgeGroup", "value", 4)) == "2"))
                        {
                            string elements = Utility.ByCss("SelectCoTravellerTitle", 4);
                            string[] title = elements.Replace("\r\n", "_").Split("_".ToCharArray());

                            int i = 0;
                            foreach (var element in title)
                            {
                                Assert.AreEqual(_titlesForAdultAndSenior[i], element);
                                i++;
                            }
                        }
                        else if (((Utility.GrabAttributeValueByCss("SelectCoTravellerAgeGroup", "value", 4)) == "3") || ((Utility.GrabAttributeValueByCss("SelectCoTravellerAgeGroup", "value", 4)) == "4"))
                        {
                            string elements = Utility.ByCss("SelectCoTravellerTitle", 4);
                            string[] title = elements.Replace("\r\n", "_").Split("_".ToCharArray());

                            int i = 0;
                            foreach (var element in title)
                            {
                                Assert.AreEqual(_titleForChildAndInfant[i], element);
                                i++;
                            }
                        }
                    }
                }
            }
        }



        [DeploymentItem("CoTravelerFirstAndLastNameValidations.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\CoTravelerFirstAndLastNameValidations.csv", "CoTravelerFirstAndLastNameValidations#csv", DataAccessMethod.Sequential), TestMethod]
        public void CoTravelerFirstAndLastNameValidations()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;            
            string _baseUrl = Record("SignInUrl");
            if (_baseUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
               
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(2);
                Utility.CsstoClick("clickOnMyInformation", 4);
                Utility.Sleep(2);
                string cheapoairMyInfoUrl = Record("CheapoairMyInfoUrl");
                if (cheapoairMyInfoUrl == Driver.Url)
                {
                    Utility.ByLinkTexttoClick("clickOnMyCoTravelerMenu", 4);

                    string myCoTravelerUrl = Record("MyCoTravelerUrl");
                    if (myCoTravelerUrl == Driver.Url)
                    {
                        Utility.CsstoClick("ClickOnAddCoTravelerBtn", 4);

                        string _firstNametxt = Record("FirstName");
                       // char[] firstChar = _firstNametxt.ToCharArray();
                        string _lastNametxt = Record("LastName");
                        Utility.CsstoClear("textInFirstname",3);
                        Utility.CssToSetText("textInFirstname", _firstNametxt, 3);
                        Utility.CsstoClick("ClickOnTsaReaders",3);
                        Utility.CsstoClear("textInlastName", 3);
                        Utility.CssToSetText("textInlastName", _lastNametxt, 3);
                        Utility.CsstoClick("ClickOnTsaReaders", 3);
                        Utility.CsstoClick("ClickOnSaveCoTravelerBtn",3);
                        if (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("textInFirstname", "value", 3)))
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
                                            string actualErrorMsg = Utility.ByXpath("ErrorMsgForFirstNameInCoTraveler", 4);
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
                                        string actualErrorMsg = Utility.ByXpath("ErrorMsgForFirstNameInCoTraveler", 4);
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
                                    string actualErrorMsg = Utility.ByXpath("ErrorMsgForFirstNameInCoTraveler", 4);
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
                                string actualErrorMsg = Utility.ByXpath("ErrorMsgForFirstNameInCoTraveler", 4);
                                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                            }
                            //Assert.IsTrue(false, "Please enter first name");
                        }

                        if (!string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("textInlastName", "value", 3)))
                        {
                            if ((_lastNametxt.Length > 2) && (_lastNametxt.Length < 25))
                            {
                                Regex regex = new Regex(@"^[a-zA-Z]");
                                if (regex.IsMatch(_firstNametxt))
                                {
                                    if ((_lastNametxt.Contains("!") || (_lastNametxt.Contains("@")) || (_lastNametxt.Contains("~")) || (_lastNametxt.Contains("$")) || (_lastNametxt.Contains("%")) || (_lastNametxt.Contains("^")) || (_lastNametxt.Contains("&")) || (_lastNametxt.Contains("*")) || (_lastNametxt.Contains("`")) || (_lastNametxt.Contains("+")) || (_lastNametxt.Contains("_")) || (_lastNametxt.Contains(":")) || (_lastNametxt.Contains(".")) || (_lastNametxt.Contains(",")) || (_lastNametxt.Contains("(")) || (_lastNametxt.Contains(")")) || (_lastNametxt.Contains("="))))
                                    {
                                        string expectedErrorMsg = Record("ExpectedErrorMsgForLastname");
                                        string actualErrorMsg = Utility.ByXpath("ErrorMsgForLastNameInCoTraveler", 4);
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
                                        string actualErrorMsg = Utility.ByXpath("ErrorMsgForLastNameInCoTraveler", 4);
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
                                    string actualErrorMsg = Utility.ByXpath("ErrorMsgForLastNameInCoTraveler", 4);
                                    Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                                }
                                //throw new Exception("FirstName should starts with big letter");
                            }
                        }
                        else
                        {
                            string expectedErrorMsg = Record("ExpectedErrorMsgForLastname");
                            if (expectedErrorMsg != "No Error")
                            {
                                string actualErrorMsg = Utility.ByXpath("ErrorMsgForLastNameInCoTraveler", 4);
                                Assert.AreEqual(expectedErrorMsg, actualErrorMsg);
                            }
                            //Assert.IsTrue(false, "Please enter last name");
                        }
                    }
                    else
                    {
                        Assert.IsTrue(false, "MyCoTraveler page is not open");
                    }
                }
            }
        }


        [DeploymentItem("DeleteLastCoTraveler.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\DeleteLastCoTraveler.csv", "DeleteLastCoTraveler#csv", DataAccessMethod.Sequential), TestMethod]
        public void DeleteLastCoTraveler()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;          
            string _baseUrl = Record("SignInUrl");
            if (_baseUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);               

                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(2);
                Utility.CsstoClick("clickOnMyInformation", 4);
                Utility.Sleep(2);
                string cheapoairMyInfoUrl = Record("CheapoairMyInfoUrl");
                if (cheapoairMyInfoUrl == Driver.Url)
                {
                    Utility.ByLinkTexttoClick("clickOnMyCoTravelerMenu", 4);

                    string myCoTravelerUrl = Record("MyCoTravelerUrl");
                    if (myCoTravelerUrl == Driver.Url)
                    {
                        string xpath = "html/body/div[1]/div[2]/div[2]/div/div/div[4]/div/div/div[2]/div/div/div/form/div[1]/div/div[2]";
                        if (Utility.IsDisplayedUsingXpathForMoltingInnerText(xpath))
                        {
                            int _countDelete = UserProfileSPA.Library.TestEnvironment.Driver.FindElements(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DeleteCoTraveler"))).Count();
                          
                            if (Utility.IsDisplayedUsingCss("ClickOnAddCoTravellerBtn"))
                            {
                                Utility.CsstoClick("ClickOnAddCoTravellerBtn", 3);

                                if (!Utility.IsDisplayedUsingCss("ClickOnAddCoTravellerBtn"))
                                {
                                    if ((Utility.IsDisplayedUsingCss("CoTravelerFlightPreference")) && (Utility.IsDisplayedUsingCss("CotravelerHotelPreference")) && (Utility.IsDisplayedUsingCss("CoTravelerCarPreference")) && (Utility.IsDisplayedUsingCss("AddCoTravelerSection")))
                                    {
                                        
                                        string selectAgeGroupForSecondCotraveler = Record("selectAgeGroupForCotraveler");
                                        var enterAgeGroupForSecondCoTraveler = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("SelectCoTravellerAgeGroup")));
                                        var selectElementForSecondCotraveler = new SelectElement(enterAgeGroupForSecondCoTraveler);
                                        selectElementForSecondCotraveler.SelectByText(selectAgeGroupForSecondCotraveler);                             


                                        string _firstNameForSecondCotraveler = Record("FirstNameForCotraveler");
                                        string _lastNameForSecondCotraveler = Record("LastNameForCotraveler");
                                        Utility.CssToSetText("textInFirstname", _firstNameForSecondCotraveler, 3);
                                        Utility.CssToSetText("textInlastName", _lastNameForSecondCotraveler, 3);
                                        string _completeNameForSecondCoTraveler = _firstNameForSecondCotraveler + " " + _lastNameForSecondCotraveler;


                                        string _monthForSecondCotraveler = Record("MonthForCotraveler");
                                        var selectMonthForSecondCotraveler = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerMonth")));
                                        var selectElementMonthForSecondCotraveler = new SelectElement(selectMonthForSecondCotraveler);
                                        selectElementMonthForSecondCotraveler.SelectByText(_monthForSecondCotraveler);

                                        string _dayForSecondCotraveler = Record("DayForCotraveler");
                                        var selectDaySecondCotraveler = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerDay")));
                                        var selectElementDayForSecondCotraveler = new SelectElement(selectDaySecondCotraveler);
                                        selectElementDayForSecondCotraveler.SelectByText(_dayForSecondCotraveler);

                                        string _yearForSecondCotraveler = Record("YearForCotraveler");
                                        var selectYearSecondCotraveler = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerYear")));
                                        var selectElementYearForSecondCotraveler = new SelectElement(selectYearSecondCotraveler);
                                        selectElementYearForSecondCotraveler.SelectByText(_yearForSecondCotraveler);

                                        string _genderForSecondCotraveler = Record("GenderForCotraveler");
                                        var selectGenderForSecondCotraveler = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerGender")));
                                        var selectElementGenderForSecondCotraveler = new SelectElement(selectGenderForSecondCotraveler);
                                        selectElementGenderForSecondCotraveler.SelectByText(_genderForSecondCotraveler);

                                        string SelectTitleForSecondCotraveler = Record("SelectTitleForCotraveler");
                                        var enterTitleForSecondCotraveler = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("SelectCoTravellerTitle")));
                                        var selectElementTitleForSecondCotraveler = new SelectElement(enterTitleForSecondCotraveler);
                                        selectElementTitleForSecondCotraveler.SelectByText(SelectTitleForSecondCotraveler);

                                        Utility.XPathtoClick("ClickOnFlightPreferecne", 3);
                                        Utility.Sleep(3);
                                        string _homeAirportForSecondCotraveler = Record("HomeAirportForCotraveler");
                                        Utility.CssToSetText("CoTravelerHomeAirPort", _homeAirportForSecondCotraveler, 3);
                                        Utility.CsstoClick("ClickOnSaveCoTravelerBtn", 3);
                                        Utility.Sleep(3);

                                        int _countDeleteNew = UserProfileSPA.Library.TestEnvironment.Driver.FindElements(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DeleteCoTraveler"))).Count();
                                        if (_countDelete != _countDeleteNew)
                                        {
                                            UserProfileSPA.Library.TestEnvironment.Driver.FindElements(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DeleteCoTraveler"))).LastOrDefault().Click();

                                            _countDeleteNew = UserProfileSPA.Library.TestEnvironment.Driver.FindElements(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("DeleteCoTraveler"))).Count();

                                            if (_countDelete == _countDeleteNew)
                                            {
                                                Assert.IsTrue(true,"Newly added cotraveler is deleted.");
                                            }                                            
                                        }
                                        else
                                        {
                                            Assert.IsTrue(false, "Please add new CoTraveler for delete.");
                                        }
                                    }
                                    else
                                    {
                                        Assert.IsTrue(false, "AddCoTravelerSection is not found.");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        [DeploymentItem("MyCoTravelerAllValidations.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\MyCoTravelerAllValidations.csv", "MyCoTravelerAllValidations#csv", DataAccessMethod.Sequential), TestMethod]
        public void MyCoTravellerValidations()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
           
            Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
            Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
            
            string signinUrl = Record("SignInUrl");
            if (signinUrl == Driver.Url)
            {
                
                Utility.CsstoClick("SignInBtn", 4);
                Utility.Sleep(3);
                string fareportalOverviewUrl = Record("CheapoairOverviewUrl");
                if (fareportalOverviewUrl == Driver.Url)
                {
                    Utility.CsstoClick("clickOnMyInformation", 4);
                    Utility.Sleep(2);

                    string fareportalMyDetailsUrl = Record("CheapoairMyDetailsUrl");
                    if (fareportalMyDetailsUrl == Driver.Url)
                    {
                        Utility.ByLinkTexttoClick("clickOnMyCoTravelerMenu", 4);

                        string myCoTravelerUrl = Record("CheapoairMyCoTravelerDetailsUrl");
                        if (myCoTravelerUrl == Driver.Url)
                        {
                            Utility.CsstoClick("ClickOnAddCoTravelerBtn", 4);
                            Utility.CsstoClick("SaveCoTraveler", 4);



                            if ((string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("textInFirstname", "value", 2))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("textInlastName", "value", 2))))
                            {
                                string expectedValidationsIfMyCoTravelerIsNameFieldIsEmpty = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("expectedValidationsIfMyCoTravelerIsNameFieldIsEmpty");
                                string[] ValidationsIfMyCoTravelerNameFieldIsEmpty = expectedValidationsIfMyCoTravelerIsNameFieldIsEmpty.Split(",".ToCharArray());

                                string firstnameValidation = Utility.ByXpath("CoTravelerfirstnameValidation", 4);
                                Assert.AreEqual(ValidationsIfMyCoTravelerNameFieldIsEmpty[0], firstnameValidation);

                                string lastnameValidation = Utility.ByXpath("CoTravelerLastnameValidation", 4);
                                Assert.AreEqual(ValidationsIfMyCoTravelerNameFieldIsEmpty[1], lastnameValidation);
                                string expectedValidationsInDobDropDownWhileAddingCoTraveler = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("expectedValidationsInDobDropDownWhileAddingCoTraveler");
                                string[] ValidationsIfDobDropDown = expectedValidationsInDobDropDownWhileAddingCoTraveler.Split(",".ToCharArray());

                                if (((Utility.GrabAttributeValueByCss("CoTravellerMonth", "value", 4)) == "0") && ((Utility.GrabAttributeValueByCss("CoTravellerDay", "value", 4)) == "0") && (Utility.GrabAttributeValueByCss("CoTravellerYear", "value", 4)) == "0")
                                {
                                   
                                    string actualErrorMsg = Utility.ByXpath("CoTravelerDobErrorMsg", 4);
                                    Assert.AreEqual(ValidationsIfDobDropDown[0], actualErrorMsg);                                    
                                }
                                else if (((Utility.GrabAttributeValueByCss("CoTravellerMonth", "value", 4)) != "0") || ((Utility.GrabAttributeValueByCss("CoTravellerDay", "value", 4)) != "0") || (Utility.GrabAttributeValueByCss("CoTravellerYear", "value", 4)) != "0")
                                {
                                    string actualErrorMsg = Utility.ByXpath("CoTravelerDobErrorMsg", 4);
                                    Assert.AreEqual(ValidationsIfDobDropDown[1], actualErrorMsg);
                                }                                

                                if (((Utility.GrabAttributeValueByCss("SelectGenderInCoTraveler", "value", 4)) == "0"))
                                {
                                    string expectedValidationInCotravelerGender = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("expectedValidationInCotravelerGender");
                                    string actualValidationInCotravelerGender = Utility.ByXpath("CoTravelerGenderIsNotSelected", 4);
                                    Assert.AreEqual(expectedValidationInCotravelerGender, actualValidationInCotravelerGender);
                                }                               
                            }
                        }
                    }
                }
            }   
        }


        [DeploymentItem("UpdateDataUsingEditButton.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\UpdateDataUsingEditButton.csv", "UpdateDataUsingEditButton#csv", DataAccessMethod.Sequential), TestMethod]
        // should update the firstName and month in table to check changes.
        public void UpdateDataUsingEditButton()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
          
            string _baseUrl = Record("SignInUrl");
            if (_baseUrl == Driver.Url)
            {
                string email = Record("Email");
                string password = Record("Password");
                Utility.CssToSetText("Email", email, 4);               
                Utility.CssToSetText("Password", password, 4);
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(2);
                Utility.CsstoClick("clickOnMyInformation", 4);
                Utility.Sleep(2);
                string cheapoairMyInfoUrl = Record("CheapoairMyInfoUrl");
                if (cheapoairMyInfoUrl == Driver.Url)
                {
                    Utility.ByLinkTexttoClick("clickOnMyCoTravelerMenu", 4);

                    string myCoTravelerUrl = Record("MyCoTravelerUrl");
                    if (myCoTravelerUrl == Driver.Url)
                    {
                        if (Utility.IsDisplayedUsingCss("ClickOnAddCoTravellerBtn"))
                        {
                            string xpath = "html/body/div[1]/div[2]/div[2]/div/div/div[4]/div/div/div[2]/div/div/div/form/div[1]/div/div[2]";
                            if (Utility.IsDisplayedUsingXpathForMoltingInnerText(xpath))
                            {

                                string getDetails = Driver.FindElement(By.XPath(xpath)).Text;
                                string[] details = getDetails.Replace("\r\n", "_").Split("_".ToCharArray());
                                string previousName = details[0];
                                string PreviousDob = details[2];
                                string[] firstName = previousName.Replace(" ", "_").Split("_".ToCharArray());
                                string[] DobMonth = PreviousDob.Split(":".ToCharArray());
                                Utility.XPathtoClick("ClickOnEditLinkForFirstCotraveler", 3);
                                string EditProfile = Utility.ByXpath("EditCoTravelerHeader", 3);
                                if (!(Utility.IsDisplayedUsingCss("ClickOnAddCoTravellerBtn")))
                                {
                                    if ((EditProfile == "Edit Co-Traveler " + firstName[0].ToLower()) || (EditProfile == "Edit Co-Traveler " + firstName[0]))
                                    {

                                       
                                        string updatedNameFromTable = Record("UpdatedName");
                                        string updatedMonth = Record("UpdatedMonth");

                                        Utility.CssToSetText("textInFirstname", updatedNameFromTable, 3);

                                        var selectMonth = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("CoTravellerMonth")));
                                        var selectElementMonth = new SelectElement(selectMonth);
                                        selectElementMonth.SelectByText(updatedMonth);
                                        Utility.CsstoClick("ClickOnSaveCoTravelerBtn", 3);

                                        if (Utility.IsDisplayedUsingXpathForMoltingInnerText(xpath))
                                        {
                                            string getNewDetails = Driver.FindElement(By.XPath(xpath)).Text;
                                            string[] Updateddetails = getNewDetails.Replace("\r\n", "_").Split("_".ToCharArray());
                                            string NewUpdatedName = Updateddetails[0];
                                            string NewUpdatedDob = Updateddetails[2];
                                            string[] updatedfirstName = NewUpdatedName.Replace(" ", "_").Split("_".ToCharArray());
                                            string[] updatedDobMonth = NewUpdatedDob.Split(":".ToCharArray());
                                            Assert.AreEqual(updatedNameFromTable, updatedfirstName[0]);
                                            Assert.AreNotEqual(DobMonth[1], updatedDobMonth[1]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        [DeploymentItem("VerifyTheTitleFieldWithGenderInCoTraveler.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\VerifyTheTitleFieldWithGenderInCoTraveler.csv", "VerifyTheTitleFieldWithGenderInCoTraveler#csv", DataAccessMethod.Sequential), TestMethod]
        public void VerifyTheTitleFieldWithGenderInCoTraveler()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
           
            string _baseUrl = Record("SignInUrl");
            if (_baseUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(2);
                Utility.CsstoClick("clickOnMyInformation", 4);
                Utility.Sleep(2);
                string cheapoairMyInfoUrl = Record("CheapoairMyInfoUrl");
                if (cheapoairMyInfoUrl == Driver.Url)
                {
                    Utility.ByLinkTexttoClick("clickOnMyCoTravelerMenu", 4);

                    string myCoTravelerUrl = Record("MyCoTravelerUrl");
                    if (myCoTravelerUrl == Driver.Url)
                    {
                        Utility.CsstoClick("ClickOnAddCoTravelerBtn", 4);

                        string SelectFromAgeGroup = Record("SelectFromAgeGroup");
                        var enterOneOfAgeGroup = Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML("SelectCoTravellerAgeGroup")));
                        var selectElement = new SelectElement(enterOneOfAgeGroup);
                        selectElement.SelectByText(SelectFromAgeGroup);
                        Utility.Sleep(4);

                       
                        if (((Utility.GrabAttributeValueByCss("SelectCoTravellerAgeGroup", "value", 4)) == "1") || ((Utility.GrabAttributeValueByCss("SelectCoTravellerAgeGroup", "value", 4)) == "2"))
                        {
                            string _selectedTitle = Record("SelectedTitle");
                            var _title = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("Title")));
                            var selectelementTitle = new SelectElement(_title);
                            selectelementTitle.SelectByText(_selectedTitle);

                            string _selectedGender = Record("SelectedGender");
                            var _gender = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("Gender")));
                            var selectelementGender = new SelectElement(_gender);
                            selectelementGender.SelectByText(_selectedGender);                           
                           

                            if (((Utility.GrabAttributeValueByCss("SelectCoTravellerTitle", "value", 4)) == "1") && ((Utility.GrabAttributeValueByCss("CoTravellerGender", "value", 4)) == "1"))
                            {
                                Assert.IsTrue(true);
                            }
                            else
                            {
                                string expectedErrorMsg = Record("ExpectedErrorMsg");
                                if (expectedErrorMsg != "No Error")
                                {
                                    string actualErrorMsg = Utility.ByXpath("GenderErrorMsg", 4);
                                    Assert.IsTrue(false, "Please select proper gender");
                                }
                               // throw new Exception("Please select proper gender");
                            }
                           
                        }
                        else if (((Utility.GrabAttributeValueByCss("SelectCoTravellerAgeGroup", "value", 4)) == "3") || ((Utility.GrabAttributeValueByCss("SelectCoTravellerAgeGroup", "value", 4)) == "4"))
                        {
                            string _selectedGender = Record("SelectedGender");
                            var _gender = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("Gender")));
                            var selectelementGender = new SelectElement(_gender);
                            selectelementGender.SelectByText(_selectedGender);                           
                           

                            string _selectedTitle = Record("SelectedTitle");
                            var _title = Driver.FindElement(By.CssSelector(UserProfileSPA.Library.TestEnvironment.LoadXML("Title")));
                            var selectelementTitle = new SelectElement(_title);
                            selectelementTitle.SelectByText(_selectedTitle);

                           

                            if (((Utility.GrabAttributeValueByCss("SelectCoTravellerTitle", "value", 4)) == "1") && ((Utility.GrabAttributeValueByCss("CoTravellerGender", "value", 4)) == "1"))
                            {
                                Assert.IsTrue(true);
                            }
                            else
                            {
                                string expectedErrorMsg = Record("ExpectedErrorMsg");
                                if(expectedErrorMsg != "No Error")
                                {
                                    string actualErrorMsg = Utility.ByXpath("GenderErrorMsg", 4);
                                    Assert.IsTrue(false, "Please select proper gender");
                                }
                                
                            }
                        }
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
