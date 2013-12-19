using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using UserProfileSPA.Library;
using System.Text.RegularExpressions;


namespace UserProfileSPA.TestCases
{
    [TestClass]
    public class Setting 
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


        [DeploymentItem("AddFareAllAlertValidationsInSettingsPage.csv"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\AddFareAllAlertValidationsInSettingsPage.csv", "AddFareAllAlertValidationsInSettingsPage#csv", DataAccessMethod.Sequential), TestMethod]
        public void AddFareAllAlertValidationsInSettingsPage()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;
            string _addFareAlertValidationsInSettingsPage = UserProfileSPA.TestCases.Resource.COA_SP.ResourceManager.GetString("AddFareAlertValidationsInSettingsPage");
            string[] addFareAlertValidationsInSettingsPage = _addFareAlertValidationsInSettingsPage.Split(",".ToCharArray());

            string _baseUrl = Record("URL");

            if (_baseUrl == Driver.Url)
            {
                Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(5);
                string _signInUrl = Record("SignInUrl");
                string _overViewUrl = Record("OverviewUrl");
                if (_overViewUrl == Driver.Url)
                {
                    Utility.XPathtoClick("ClickOnSettingMenu", 4);
                    string _settingPageUrl = Record("SettingPageUrl");
                    if (_settingPageUrl == Driver.Url)
                    {
                        Utility.CsstoClick("ClickOnAddFareAlertBtton", 4);
                        if ((string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInFromCityInSetting", "value", 2))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("TextInToCityInSetting", "value", 2))))// && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("DateInFromCityInSetting", "value", 2))) && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("DateInToCityInSetting", "value", 2))))
                        {
                            string ValidationInFromCityInSetting = Utility.ByXpath("ValidationInFromCityInSetting", 4);
                            Assert.AreEqual(addFareAlertValidationsInSettingsPage[0], ValidationInFromCityInSetting);

                            string ValidationInToCityInSetting = Utility.ByXpath("ValidationInToCityInSetting", 4);
                            Assert.AreEqual(addFareAlertValidationsInSettingsPage[1], ValidationInToCityInSetting);

                            Regex r = new Regex(@"^(0?[1-9]|[12][0-9]|3[01])[ \/.-](0?[1-9]|1[012])[ \/.-](19|20)\d\d$");

                           
                            string _Ddate = Record("DepDate");
                            Utility.CssToSetText("DateInFromCityInSetting", _Ddate, 4);
                            string _Rdate = Record("Rdate");
                            Utility.CssToSetText("DateInToCityInSetting", _Rdate, 4);
                            if (!r.IsMatch(Utility.GrabAttributeValueByCss("DateInFromCityInSetting", "value", 2)))// && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("DateInToCityInSetting", "value", 2))))
                            {
                                string DateValidationInFromCityInSetting = Utility.ByXpath("DateValidationInFromCityInSetting", 4);
                                Assert.AreEqual(addFareAlertValidationsInSettingsPage[2], DateValidationInFromCityInSetting);

                                string DateValidationInToCityInSetting = Utility.ByXpath("DateValidationInToCityInSetting", 4);
                                Assert.AreEqual(addFareAlertValidationsInSettingsPage[3], DateValidationInToCityInSetting);
                            }
                            
                            if (!r.IsMatch(Utility.GrabAttributeValueByCss("DateInToCityInSetting", "value", 2)))// && (string.IsNullOrEmpty(Utility.GrabAttributeValueByCss("DateInToCityInSetting", "value", 2))))
                            {
                                string DateValidationInFromCityInSetting = Utility.ByXpath("DateValidationInFromCityInSetting", 4);
                                Assert.AreEqual(addFareAlertValidationsInSettingsPage[2], DateValidationInFromCityInSetting);

                                string DateValidationInToCityInSetting = Utility.ByXpath("DateValidationInToCityInSetting", 4);
                                Assert.AreEqual(addFareAlertValidationsInSettingsPage[3], DateValidationInToCityInSetting);
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
