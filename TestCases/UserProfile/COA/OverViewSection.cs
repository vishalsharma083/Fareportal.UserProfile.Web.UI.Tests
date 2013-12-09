using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using UserProfileSPA.Library;


namespace UserProfileSPA.TestCases
{
    [TestClass]
    public class OverViewSection 
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


        [DeploymentItem("FareAlerts.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\FareAlerts.csv", "FareAlerts#csv", DataAccessMethod.Sequential), TestMethod]
        public void FareAlerts()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;         
            string Error = Record("Error");
            Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
            Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);        
            string _fromCity = Record("FromCity");
            string _toCity = Record("ToCity");
            string _baseUrl = Record("URL");
            string _overViewUrl = Record("OverviewUrl");
            if (_baseUrl == Driver.Url)
            {              
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(5);
                if (_overViewUrl == Driver.Url)
                {
                    string _fareAlert = Utility.ByXpath("NoFare", 3);
                    string expected_fares = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("NoFares");
                    if (_fareAlert == expected_fares)
                    {
                        Utility.XPathtoClick("ClickOnFareAlerts", 3);

                        string _settingUrl = Record("SettingPageURL");
                        if (_settingUrl == Driver.Url)
                        {
                            string _departDate = Record("DepartDate");
                            string _retunDate = Record("ReturnDate");
                            Utility.CsstoClick("ClickFareAlertCheckBox", 3);                           
                            Utility.CsstoClick("ClickOnFareAlertDiv", 3);
                            Utility.CsstoClick("ClickOnAddFareAlertBtton", 3);
                            Utility.Sleep(2);
                            Utility.CsstoClick("UpdateSettingsButton", 3);
                            Utility.ByLinkTexttoClick("ClickOnOverViewMenu", 4);
                            if (_overViewUrl == Driver.Url)
                            {
                                string _farefromcity = Utility.ByXpath("FareAlertFromLable", 3);
                                string _faretocity = Utility.ByXpath("FareAlertToLabe", 3);
                                Assert.AreEqual(_fromCity, _farefromcity);
                                Assert.AreEqual(_toCity, _faretocity);
                            }
                            else
                            {
                                throw new Exception("Home page is not loaded properly.");
                            }

                        }
                        else
                        {
                            throw new Exception(Error);
                        }
                    }
                    else
                    {
                        string _farefromcity = Utility.ByXpath("FareAlertFromLable", 3);
                        string _faretocity = Utility.ByXpath("FareAlertToLabe", 3);
                        Assert.AreEqual(_fromCity, _farefromcity);
                        Assert.AreEqual(_toCity, _faretocity);
                    }
                }
                else
                {
                    throw new Exception("Overview page is not loaded properly.");
                }
            }
            else
            {
                throw new Exception("Home page is not opened.");
            }
        }


        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\VerifySectionOnOverViewPage.csv", "VerifySectionOnOverViewPage#csv", DataAccessMethod.Sequential), DeploymentItem("VerifySectionOnOverViewPage.csv"), TestMethod]
        public void VerifySectionOnPage()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;           
            Utility.CssToSetText("Email", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
            Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);          
            string signinUrl = Record("SignInUrl");
            string CommentUTinOverviewPage = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("CommentUTinOverviewPage");
            string StartSearchNowUT = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("StartSearchNowUT");
            string CommentFTinOverViewpage = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("CommentFTinOverViewpage");
            string CreateFareAlerFTbtn = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("CreateFareAlerFTbtn");
            string CommrntRSinOverViewpage = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("CommrntRSinOverViewpage");
            string StartSearchNowBtnRS = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("StartSearchNowBtnRS");
            string TravelTools = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("TravelTools");

            Utility.Sleep(4);
            if (signinUrl == Driver.Url)
            {                
                Utility.CsstoClick("SignInBtn", 4);
                Utility.Sleep(10);
                string fareportalOverviewUrl = Record("FareportalOverviewUrl");
                Utility.Sleep(4);
                if (fareportalOverviewUrl == Driver.Url)
                {

                    if (Utility.IsDisplayedUsingXpath("UpCommingTripsTitle"))
                    {
                        string _upcommingTrips = Utility.ByCss("UpCommingTrips", 4);
                        string[] UpcommingTrips = _upcommingTrips.Replace("\r\n", ",").Split(",".ToCharArray());
                        Assert.AreEqual(CommentUTinOverviewPage, UpcommingTrips[1]);
                        Assert.AreEqual(StartSearchNowUT, UpcommingTrips[2]);
                    }
                    else
                    {
                        throw new Exception("Upcomming Trip title is not displayed.");
                    }


                    if (Utility.IsDisplayedUsingXpath("FareAlertTitle"))
                    {
                        string _fareAlert = Utility.ByCss("FareAlert", 4);
                        string[] FareAlert = _fareAlert.Replace("\r\n", ",").Split(",".ToCharArray());
                        Assert.AreEqual(CommentFTinOverViewpage, FareAlert[1]);
                        Assert.AreEqual(CreateFareAlerFTbtn, FareAlert[2]);
                    }
                    else
                    {
                        throw new Exception("Fare Alert title is not displayed.");
                    }


                    if (Utility.IsDisplayedUsingXpath("RecentSearchesTitle"))
                    {
                        string _recentSearches = Utility.ByCss("RecentSearches", 4);
                        string[] RecentSearches = _recentSearches.Replace("\r\n", ",").Split(",".ToCharArray());
                        Assert.AreEqual(CommrntRSinOverViewpage, RecentSearches[1]);
                        Assert.AreEqual(StartSearchNowBtnRS, RecentSearches[2]);
                    }
                    else
                    {
                        throw new Exception("Recent Searches title is not displayed.");
                    }

                    if (Utility.IsDisplayedUsingXpath("TravelTools"))
                    {
                        string _diffTravelTools = Utility.ByXpath("DifferentTravelTools", 4);
                        string[] DiffTravelTools = _diffTravelTools.Replace("\r\n", ",").Split(",".ToCharArray());
                        string[] expectedTravelTools = TravelTools.Replace(",", "_").Split("_".ToCharArray());
                        int i = 0;
                        foreach (var items in DiffTravelTools)
                        {
                            Assert.AreEqual(expectedTravelTools[i], items);
                            i++;
                        }
                    }
                    else
                    {
                        throw new Exception("Travel Tools title is not displayed.");
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















