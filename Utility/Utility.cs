using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.IO;
using UserProfileSPA.TestCases;
using System.Reflection;
using OpenQA.Selenium;
using System.Threading;
using System.Diagnostics;
using OpenQA.Selenium.Support.UI;
using UserProfileSPA.Library;

namespace UserProfileSPA
{
    public class Utility 
    {

       public static ArrayList GetElementsByTagName(string FileName, string ParentTag)
        {
            XmlDocument myXML = TestEnvironment.GetXml(TestEnvironment.FlightEngine, FileName);
            System.Xml.XmlElement root = myXML.DocumentElement;
            System.Xml.XmlNodeList lst = root.GetElementsByTagName(ParentTag)[0].ChildNodes;
            ArrayList arrList = new ArrayList(lst.Count);
            ArrayList items = new ArrayList();
            foreach (System.Xml.XmlNode node in lst)
            {
                arrList.Add(node);
            }
            foreach (XmlElement item in arrList)
            {
                items.Add(item.InnerXml.ToString());
            }
            return items;
        }
        public static void CssToSetText(string xmlTagName_, string testCaseValue_, int elementSearchTimeOut_)
        {

            if (String.IsNullOrEmpty(testCaseValue_))
            {
                return;
            }

            string XMLValue = UserProfileSPA.Library.TestEnvironment.LoadXML(xmlTagName_);
            try
            {
                TestEnvironment.WaitForElementPresent(By.CssSelector(XMLValue), elementSearchTimeOut_);

                var element = TestEnvironment.Driver.FindElement(By.CssSelector(XMLValue));
                if (!String.IsNullOrEmpty(testCaseValue_))
                {
                    element.Clear();
                    element.SendKeys(testCaseValue_);
                }
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Not able to locate the element <" + xmlTagName_ + "> even after " + elementSearchTimeOut_ + " secs. " + "EXCEPTION: " + ex);
            }
        }

        public static void CsstoClick(string xmlTagName_, int Time_WaitForElementPresent)
        {
            try
            {
                string XMLValue = UserProfileSPA.Library.TestEnvironment.LoadXML(xmlTagName_);
                UserProfileSPA.Library.TestEnvironment.WaitForElementPresent(By.CssSelector(XMLValue), Time_WaitForElementPresent);
                var element = TestEnvironment.Driver.FindElement(By.CssSelector(XMLValue));
                element.Click();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Not able to locate/click the element <" + xmlTagName_ + "> even after " + Time_WaitForElementPresent + " secs. " + "EXCEPTION: " + ex);
            }
        }

        public static void ByClasstoClick(string xmlTagName_, int Time_WaitForElementPresent)
        {
            try
            {
                string XMLValue = UserProfileSPA.Library.TestEnvironment.LoadXML(xmlTagName_);
                UserProfileSPA.Library.TestEnvironment.WaitForElementPresent(By.CssSelector(XMLValue), Time_WaitForElementPresent);
                var element = TestEnvironment.Driver.FindElement(By.ClassName(XMLValue));
                element.Click();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Not able to locate/click the element <" + xmlTagName_ + "> even after " + Time_WaitForElementPresent + " secs. " + "EXCEPTION: " + ex);
            }
        }

        public static void XPathtoClick(string xmlTagName_, int Time_WaitForElementPresent)
        {
            try
            {
                string XMLValue = UserProfileSPA.Library.TestEnvironment.LoadXML(xmlTagName_);
                UserProfileSPA.Library.TestEnvironment.WaitForElementPresent(By.XPath(XMLValue), Time_WaitForElementPresent);
                var element = TestEnvironment.Driver.FindElement(By.XPath(XMLValue));
                element.Click();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Not able to locate/click the element <" + xmlTagName_ + "> even after " + Time_WaitForElementPresent + " secs. " + "EXCEPTION: " + ex);
            }
        }


        public static void IDtoClick(string xmlTagName_, int Time_WaitForElementPresent)
        {
            try
            {
                string XMLValue = UserProfileSPA.Library.TestEnvironment.LoadXML(xmlTagName_);
                UserProfileSPA.Library.TestEnvironment.WaitForElementPresent(By.Id(XMLValue), Time_WaitForElementPresent);
                var element = TestEnvironment.Driver.FindElement(By.Id(XMLValue));
                element.Click();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Not able to locate/click the element <" + xmlTagName_ + "> even after " + Time_WaitForElementPresent + " secs. " + "EXCEPTION: " + ex);
            }
        }

        public static void ByLinkTexttoClick(string xmlTagName_, int Time_WaitForElementPresent)
        {
            try
            {
                string XMLValue = UserProfileSPA.Library.TestEnvironment.LoadXML(xmlTagName_);
                TestEnvironment.WaitForElementPresent(By.LinkText(XMLValue), Time_WaitForElementPresent);
                var element = TestEnvironment.Driver.FindElement(By.LinkText(XMLValue));
                element.Click();
            }
            catch (Exception ex)
            {

                throw new NotFoundException("Not able to locate/click the element <" + xmlTagName_ + "> even after " + Time_WaitForElementPresent + " secs. " + "EXCEPTION: " + ex);
            }
        }

        public static bool IsDisplayedUsingXpath(string XMLValue)
        {
            try
            {
                return TestEnvironment.Driver.FindElement(By.XPath(TestEnvironment.LoadXML(XMLValue))).Displayed;
            }
            catch { return false; }
        }


        public static string ValidationMessageByCss(string xmlTageName_, int Time_WaitForElementPresent)
        {
            string XMLValue = TestEnvironment.LoadXML(xmlTageName_);
            try
            {
                TestEnvironment.WaitForElementPresent(By.CssSelector(XMLValue), Time_WaitForElementPresent);
                return TestEnvironment.Driver.FindElement(By.CssSelector(XMLValue)).Text;
            }
            catch (Exception ex)
            {

                throw new Exception("Not able to Grab the validation for the element<" + xmlTageName_ + "> even after " + Time_WaitForElementPresent + " secs. EXCEPTION: " + ex);
            }
        }

        public static string ByXpath(string xmlTagName_, int Time_WaitForElementPresent)
        {
            string XMLValue = TestEnvironment.LoadXML(xmlTagName_);
            try
            {
                TestEnvironment.WaitForElementPresent(By.XPath(XMLValue), Time_WaitForElementPresent);
                return TestEnvironment.Driver.FindElement(By.XPath(XMLValue)).Text;
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Not able to locate/click the element <" + xmlTagName_ + "> even after " + Time_WaitForElementPresent + " secs. " + "EXCEPTION: " + ex);
            }
        }

        public static string ByClassName(string xmlTagName_, int Time_WaitForElementPresent)   
        {
            string XMLValue = TestEnvironment.LoadXML(xmlTagName_);
            try
            {
                TestEnvironment.WaitForElementPresent(By.ClassName(XMLValue), Time_WaitForElementPresent);
                return TestEnvironment.Driver.FindElement(By.ClassName(XMLValue)).Text;
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Not able to locate/click the element <" + xmlTagName_ + "> even after " + Time_WaitForElementPresent + " secs. " + "EXCEPTION: " + ex);
            }
        }

        public static string ByCss(string xmlTagName_, int Time_WaitForElementPresent)
        {
            string XMLValue = TestEnvironment.LoadXML(xmlTagName_);
            try
            {
                TestEnvironment.WaitForElementPresent(By.CssSelector(XMLValue), Time_WaitForElementPresent);
                return TestEnvironment.Driver.FindElement(By.CssSelector(XMLValue)).Text;
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Not able to locate/click the element <" + xmlTagName_ + "> even after " + Time_WaitForElementPresent + " secs. " + "EXCEPTION: " + ex);
            }

        }

        public static string ByLinkText(string xmlTagName_, int Time_WaitForElementPresent)
        {
            string XMLValue = TestEnvironment.LoadXML(xmlTagName_);
            try
            {
                TestEnvironment.WaitForElementPresent(By.CssSelector(XMLValue), Time_WaitForElementPresent);
                return TestEnvironment.Driver.FindElement(By.LinkText(XMLValue)).Text;
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Not able to locate/click the element <" + xmlTagName_ + "> even after " + Time_WaitForElementPresent + " secs. " + "EXCEPTION: " + ex);
            }

        }
      


        public static string GrabAttributeValueByCss(string xmlTagName_, string attribute_, int Time_WaitForElementPresent)
        {
            string XMLValue = TestEnvironment.LoadXML(xmlTagName_);
            try
            {
                string attribute = attribute_;
                // LoadAndWaitID(xmlTagName_ 20);
                Sleep(1);
                string AttributeValue = TestEnvironment.Driver.FindElement(By.CssSelector(XMLValue)).GetAttribute(attribute);
                return AttributeValue;
            }
            catch (Exception ex)
            {

                throw new NotFoundException(xmlTagName_ + "Element found on the page, attribute not found <" + "> even after " + Time_WaitForElementPresent + " secs. " + "EXCEPTION: " + ex);
            }
        }

        public static string GrabAttributeValueByXpath(string xmlTagName_, string attribute_, int Time_WaitForElementPresent)
        {
            string XMLValue = TestEnvironment.LoadXML(xmlTagName_);
            try
            {
                string attribute = attribute_;
                // LoadAndWaitID(xmlTagName_ 20);
                Sleep(1);
                string AttributeValue = TestEnvironment.Driver.FindElement(By.XPath(XMLValue)).GetAttribute(attribute);
                return AttributeValue;
            }
            catch (Exception ex)
            {

                throw new NotFoundException(xmlTagName_ + "Element found on the page, attribute not found <" + "> even after " + Time_WaitForElementPresent + " secs. " + "EXCEPTION: " + ex);
            }
        }

        public static  void CsstoClear(string xmlTagName_, int Time_WaitForElementPresent)
        {
            try
            {
                string XMLValue = UserProfileSPA.Library.TestEnvironment.LoadXML(xmlTagName_);
                UserProfileSPA.Library.TestEnvironment.WaitForElementPresent(By.CssSelector(XMLValue), Time_WaitForElementPresent);
                var element = TestEnvironment.Driver.FindElement(By.CssSelector(XMLValue));
                element.Clear();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Not able to locate/click the element <" + xmlTagName_ + "> even after " + Time_WaitForElementPresent + " secs. " + "EXCEPTION: " + ex);
            }
        }


        public static bool IsDisplayedUsingCss(string XMLValue)
        {
            try
            {
                return TestEnvironment.Driver.FindElement(By.CssSelector(TestEnvironment.LoadXML(XMLValue))).Displayed;
            }
            catch { return false; }
        }

        public static void DeleteFunc()
        {
            CsstoClick("Deletecard", 4);
        }


        public static bool IsDisplayedUsingId(string XMLValue)
        {
            try
            {
                return TestEnvironment.Driver.FindElement(By.Id((XMLValue))).Displayed;
            }
            catch { return false; }
        }

       
        
      
        //public void GetMonth(string xmlTagName_, string attribute_, int Time_waitforElement, string pickdobDay)
        //{
        //    string XMLValue = Environment.LoadXML(xmlTagName_);
        //    try
        //    {
        //        string attribute = attribute_;
        //        var dobdaydropdown = Environment.Driver.FindElement(By.XPath(XMLValue)).GetAttribute(attribute);
        //        var selectelementdobday = new SelectElement(dobdaydropdown);
        //        selectelementdobday.SelectByText(pickdobDay);
        //    }
        //    catch
        //    {
        //        throw new NotFoundException(xmlTagName_ + "Element found on the page, attribute not found <" + "> even after " + Time_WaitForElementPresent + " secs. " + "EXCEPTION: " + ex);
        //    }

        //}
        //public string Email
        //{
        //    set
        //    {
        //        SetTextByCss("Email", value, 20);
        //        System.Threading.Thread.Sleep(500);
        //    }
        //}

        public static void Sleep(int _Seconds)
        {
            int newtime_sleep = _Seconds * 1000;
            Thread.Sleep(newtime_sleep);
        }

        public static void clearBrowserCache()
        {

            TestEnvironment.Driver.Manage().Cookies.DeleteAllCookies();
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            if (TestEnvironment.BrowserName == TestEnvironment.BrowserType.IE)
                p.StartInfo.FileName = @"D:\BatchFile\IE.bat";
            else if (TestEnvironment.BrowserName == TestEnvironment.BrowserType.FireFox)
                p.StartInfo.FileName = @"D:\BatchFile\Firefox.bat";
            else if (TestEnvironment.BrowserName == TestEnvironment.BrowserType.Chrome)
                p.StartInfo.FileName = @"D:\BatchFile\Chrome.bat";
            else if (TestEnvironment.BrowserName == TestEnvironment.BrowserType.Safari)
                p.StartInfo.FileName = @"D:\BatchFile\Safari.bat";
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
        }

        public static bool IsDisplayedUsingXpathForMoltingInnerText(string XMLValue) 
        {
            try
            {
                return TestEnvironment.Driver.FindElement(By.XPath(XMLValue)).Displayed;
            }
            catch { return false; }
        }


        //private bool acceptNextAlert = true;
        //private string CloseAlertAndGetItsText()
        //{
        //    try
        //    {
        //        IAlert alert = Environment.Driver.SwitchTo().Alert();
        //        if (acceptNextAlert)
        //        {
        //            alert.Accept();
        //        }
        //        else
        //        {
        //            alert.Dismiss();
        //        }
        //        return alert.Text;
        //    }
        //    finally
        //    {
        //        acceptNextAlert = true;
        //    }
        //}
    }
}

