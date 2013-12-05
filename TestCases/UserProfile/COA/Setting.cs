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
    public partial class Setting : UserProfileTestBase
    {

        [TestInitialize]
        public void Initialize()
        {
            UserProfileSPA.Library.TestEnvironment.Init();
        }


        [TestMethod]
        public void AddFareAlertValidationsInSettingsPage()
        {
            IWebDriver Driver = UserProfileSPA.Library.TestEnvironment.Driver;          
            string Error = Record("Error");
            Utility.CssToSetText("Email", Record("Email"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);
            Utility.CssToSetText("Password", Record("Password"), UserProfileSettings.ELEMENT_SEARCH_WAIT_TIMEOUT);           
            string _baseUrl = Record("URL");
            string _overViewUrl = Record("OverviewUrl");
            string _settingPageUrl = Record("SettingPageUrl");
            if (_baseUrl == Driver.Url)
            {                
                Utility.CsstoClick("SignInBtn", 3);
                Utility.Sleep(5);
                if (_overViewUrl == Driver.Url)
                {
                    Utility.XPathtoClick("SettingMenu", 4);

                    if (_settingPageUrl == Driver.Url)
                    {
                        Utility.CsstoClick("ClickOnAddFareAlertBtton", 4);
                    }
                }
            }
        }
    }
}
