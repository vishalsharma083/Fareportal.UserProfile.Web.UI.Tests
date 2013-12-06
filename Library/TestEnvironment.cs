using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using System.Drawing.Imaging;
using OpenQA.Selenium.Safari;

using System.Xml;
using System.Threading;
using System.Diagnostics;

using System.IO;
using System.Net.Mail;
using System.Xml.Linq;
using System.Data;
using System.Data.OleDb;


namespace UserProfileSPA.Library
{
    public static class TestEnvironment
    {
        public static BrowserType BrowserName
        {
            get
            {
                return (BrowserType)Enum.Parse(typeof(BrowserType), ConfigurationManager.AppSettings["Browser"]);
            }
        }


        public static IWebDriver Driver;

        public static void Dispose()
        {

            Driver.Quit();
            Driver = null;

        }
        public enum EngineType
        {
            COA,
            CA,
            OT,
            UK,
            Eclipse
        }

        public static string XMLFileName()
        {
            switch (FlightEngine)
            {
                case EngineType.COA:
                    return "COA_Sp.xml";
                case EngineType.CA:
                    return "CA_Sp.xml";
                case EngineType.OT:
                    return "OT_SP.xml";               
                default:
                    break;
            }
            return null;
        }

        private static Dictionary<EngineType, XmlDocument> XmlDocuments = new Dictionary<EngineType, XmlDocument>();
        public static XmlDocument GetXml(EngineType engine_, string fileName_)
        {
            XmlDocument myXml = new XmlDocument();
            if (XmlDocuments.ContainsKey(engine_))
            {
                myXml = XmlDocuments[engine_];
            }
            else
            {
                myXml.Load(fileName_);
                XmlDocuments.Add(engine_, myXml);
            }
            return myXml;
        }

        //public static string LoadXML(string tagNameToFindd_)
        //{

        //    XmlDocument xmlDoc = new XmlDocument();

        //    if (Environment.FlightEngine == EngineType.COA)
        //    { xmlDoc.Load("Dms.xml"); }

        //    if (Environment.FlightEngine == EngineType.OT)
        //    { xmlDoc.Load("OT.xml"); }

        //    if (Environment.FlightEngine == EngineType.CA)
        //    { xmlDoc.Load("CA.xml"); }

        //    if (Environment.FlightEngine == EngineType.Eclipse)
        //    { xmlDoc.Load("Dms.xml"); }

        //    if (Environment.FlightEngine == EngineType.Eclipse)
        //    { xmlDoc.Load("GCMSadmin.xml"); }

        //    string  = xmlDoc.GetElementsByTagName(tagNameToFindd_)[0].InnerText;
        //    return Attribute;
        //}

        //public static string LoadXML(string tagNameToFind_)
        //{
        //    XmlDocument myXml = new XmlDocument();
        //    switch (FlightEngine)
        //    {
        //        case EngineType.COA:
        //            myXml = GetXml(EngineType.COA, "COA_SP.xml");
        //            break;
        //        case EngineType.CA:
        //            myXml = GetXml(EngineType.CA, "CA_SP.xml");
        //            break;
        //        case EngineType.OT:
        //            myXml = GetXml(EngineType.OT, "OT_SP.xml");
        //            break;                       
        //        default:
        //            break;
        //    }
        //     return myXml.GetElementsByTagName(tagNameToFind_)[0].InnerText;
        //}

        public static string LoadXML(string tagNameToFindd_)
        {

            XmlDocument myXml = new XmlDocument();
            if (TestEnvironment.FlightEngine == EngineType.COA)
            { myXml.Load("COA_SP.xml"); }

            if (TestEnvironment.FlightEngine == EngineType.CA)
            { myXml.Load("CA_SP.xml"); }

            if (TestEnvironment.FlightEngine == EngineType.OT)
            { myXml.Load("OT_SP.xml"); }

             return myXml.GetElementsByTagName(tagNameToFindd_)[0].InnerText;
           
        }



        public static void Init()
        {
            if (TestEnvironment.BrowserName == BrowserType.FireFox & Driver == null)
            {
                Driver = new FirefoxDriver();
                Driver.Manage().Window.Maximize();

                //LoadPage();
            }
            if (TestEnvironment.BrowserName == BrowserType.Chrome & Driver == null)
            {
                Driver = new ChromeDriver(@"D:\Projects\Solution1\Eclips\Lib\BrowserExe");
                Driver.Manage().Window.Maximize();
                //LoadPage();
            }
            if (TestEnvironment.BrowserName == BrowserType.IE & Driver == null)
            {
                Driver = new InternetExplorerDriver();
                Driver.Manage().Window.Maximize();
                //LoadPage();
            }
            if (TestEnvironment.BrowserName == BrowserType.Safari & Driver == null)
            {
                Driver = new SafariDriver();
                Driver.Manage().Window.Maximize();
                //LoadPage();
            }
            LoadPage();
           // UserProfileBase.clearBrowserCache();

            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
        }

        public static EngineType FlightEngine
        {
            get
            {
                return (EngineType)Enum.Parse(typeof(EngineType), ConfigurationManager.AppSettings["site"]);
            }
        }

        public enum BrowserType
        {
            FireFox,
            IE,
            Chrome,
            Safari
        }

        public static void LoadPage()
        {
            if (TestEnvironment.FlightEngine == EngineType.COA)
            { Driver.Url = "http://www.cheapoair.com/profiles/#/user-signin"; }

            if (TestEnvironment.FlightEngine == EngineType.CA)
            { Driver.Url = "http://www.cheapoair.ca"; }

            if (TestEnvironment.FlightEngine == EngineType.OT)
            { Driver.Url = "http://www.onetravel.com"; }

            //if (Environment.FlightEngine == EngineType.Gmail)
            //{ Driver.Url = "http://www.gmail.com"; }

        }
        public static void clearBrowserCache()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Manage().Cookies.DeleteAllCookies();

        }



        public static bool IsElementPresent(By by)
        {

            try
            {
                Driver.FindElement(by);
                return true;
            }
            catch (InvalidSelectorException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static bool IsElementDisplayed(By by)
        {
            try
            {
                return Driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


        public static void DoNavigate(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(50));
        }
        public static void TakeScreenshot(IWebDriver driver, string saveLocation)
        {
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(saveLocation, ImageFormat.Png);
        }

       

        public static bool IsPresentInArray(string[] firstArray, string[] secondArray)
        {
            foreach (var itemA in firstArray)
            {
                foreach (var itemB in secondArray)
                {
                    if (itemB != itemA)
                    {

                    }

                }
            }
            return true;
        }
        
        public static bool WaitForElementPresent(string id_)
        {
            string XMLValues = LoadXML(id_);
            return WaitForElementPresent(By.Id(XMLValues), 20);
        }

        public static bool WaitForElementPresent(By by, int waitInSeconds)
        {
            var waitinSecs = waitInSeconds * 1000;
            var timeDifference = (waitinSecs / 250);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var startvalue = 0; startvalue < timeDifference; startvalue++)
            {
                if (stopwatch.ElapsedMilliseconds > waitinSecs)
                    return false;
                //var elements = Driver.FindElements(by);
                //if (elements != null && elements.Count > 0)
                // return true;
                if (CheckElementPresence(by))
                {
                    return true;
                }
                Thread.Sleep(250);
            }
            return false;
        }

        public static bool CheckElementPresence(By by)
        {
            try
            {
                var elements = Driver.FindElements(by);
                if (elements != null && elements.Count > 0)
                { return true; }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //public static UserProfileSPA.Library.COA.Page.UserProfileLoadPage userprofilePage;

        //public static UserProfileSPA.Library.COA.Page.UserProfileLoadPage UserProfilepage
        //{
        //    get
        //    {
        //        if (userprofilePage != null)
        //            return userprofilePage;
        //        if (FlightEngine == EngineType.COA)
        //        {
        //            userprofilePage = new COAuserprofileLoadPage();
        //        }
        //        else if (FlightEngine == EngineType.OT)
        //        {
        //            // userprofilePage = new OTAirLoadPage();
        //        }
        //        else if (FlightEngine == EngineType.CA)
        //        {
        //            // userprofilePage = new CAAirLoadPage();
        //        }

        //        return userprofilePage;
        //    }
        //    set
        //    {
        //        userprofilePage = value;
        //    }
        //}






        //public static void ConvertCsvToXML(string path,string TestKey,string UserName,string Password)
        //{
        //    var lines = File.ReadAllLines(path);

        //    var xml = new XElement("TopElement",
        //       lines.Select(line => new XElement(TestKey,
        //          line.Split(';')
        //              .Select((column, index) => new XElement("Column" + index, column)))));

        //    xml.Save(@"C:\xmlout.xml");

        //}



        //public static void ConvertCsvToXML(string path, string TestKey, string UserName, string Password)
        //{
        //    XmlTextWriter writer = new XmlTextWriter("users.xml", Encoding.UTF8);

        //    writer.WriteStartDocument();
        //    writer.WriteStartElement("users");

        //    using (CsvReader reader = new CsvReader("users.csv"))
        //    {
        //        reader.ReadHeaders();

        //        while (reader.ReadRecord())
        //        {
        //            writer.WriteStartElement("user");

        //            writer.WriteElementString("user_id", reader["user_id"]);
        //            writer.WriteElementString("first_name", reader["first_name"]);
        //            writer.WriteElementString("last_name", reader["last_name"]);

        //            writer.WriteEndElement();
        //        }

        //        reader.Close();
        //    }

        //    writer.WriteEndElement();
        //    writer.WriteEndDocument();
        //    writer.Close();
        //}







       // public static DataTable ReadDataFromCSVFile(string filePath_, string fileName_, string mandatoryColumn_, bool isWriteSchema)
       //{
       // DataTable dataTable = null;
       // string query;


       // string csvConnectionString =  filePath_;

       // using (OleDbConnection oleDbConnection = new OleDbConnection(csvConnectionString))
       // {
       // if (!string.IsNullOrEmpty(mandatoryColumn_))
       // {
       // query = "SELECT * FROM [" + fileName_ + "] WHERE " + mandatoryColumn_ + " IS NOT NULL";
       // }
       // else
       // {
       // query = "SELECT * FROM [" + fileName_ + "]";
       // }

       // oleDbConnection.Open();
       // // Added on 4/April/2013
       // using (OleDbCommand cmd = new OleDbCommand(query, oleDbConnection))
       // {
       // // Added on 4/April/2013
       // using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(cmd))
       // {
       // // Added on 4/April/2013
       // using (DataTable dtSchema = new DataTable())
       // {
       // oleDbDataAdapter.FillSchema(dtSchema, SchemaType.Source);

       // //if (isWriteSchema) //If csv file does not contain header then write schema for that file
       // //{
       // //WriteSchema(filePath_, fileName_, mandatoryColumn_);
       // //}

       // }

       // }


        public static void ConvertCsvToXML(string path)
        {
            string[] source = File.ReadAllLines(path);
            XElement xml = new XElement("SignIn",
                from str in source
                let fields = str.Split(',')
                select new XElement("Sucess",
                    new XElement("UserName", fields[0]),
                    new XElement("Password", fields[1])                   
                    )
                );

           xml.Save(@"C:\xmlout.xml"); 
        }
    }
}
        
