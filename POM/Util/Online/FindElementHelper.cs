using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using log4net.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using TechTalk.SpecFlow.Assist.ValueRetrievers;

namespace POM.Util.Online
{
    public static class FindElementHelper
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(FindElementHelper));

        public static IWebElement GetNthLinkWithExactSameLabel(this IWebDriver driver, string linkText, int linkNumber)
        {
            var itemCollection = driver.FindElements(By.LinkText(linkText));
            Log.Info($"Link Text: {linkText}, Link Number {linkNumber}");
            var link = itemCollection.ElementAt(linkNumber - 1);
            return link;
        }
        /// <summary>
        /// i.e driver.FindElement(By.Id("recommend_ selenium_link")).GetAttribute("style"));
        /// </summary>
        /// <param name="driver">driver instance</param>
        /// <param name="by">By</param>
        /// <param name="attribute">attribute name i.e. @id</param>
        /// <returns></returns>
        public static string GetAttributeValue(this IWebDriver driver, By by, string attribute)
        {
            var elem = driver.FindElement(by);
            return elem.GetAttribute(attribute);
        }

        public static void SwitchWindow(this IWebDriver driver, int windowNumber)
        {
            var handles = driver.WindowHandles;
            Log.Info($"There are {handles.Count} Windows");
            driver.SwitchTo().Window(driver.WindowHandles[windowNumber - 1]);
        }

        public static IWebElement GetButtonWithText(this IWebDriver driver, string btnText)
        {
            Log.Info("Trying to find a button with text " + btnText);
            var btn = driver.FindElement(By.XPath($"//button[contains(text(), '{btnText}')]"));
            return btn;
        }

        public static void ClickByJavaScript(this IWebDriver driver, By by)
        {
            var elm = driver.FindElement(by);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", elm);
        }

        public static IWebElement FindElementWithSequentialWait(this IWebDriver driver, By by, int totalSeconds, int waiting)
        {
            var d = driver.FindElement(by).Displayed;
            if (!d)
            {
                while (totalSeconds > 1 && totalSeconds > waiting && d == false)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(waiting));
                    d = driver.FindElement(by).Displayed;
                    totalSeconds -= waiting;
                }
            }
            return driver.FindElement(by);
        }

        public static bool WebElementIsDisabledByJavaScript(this IWebDriver driver, By by)
        {
            var elm = driver.FindElement(by);
            var result = (bool)((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].disabled = true;", elm);
            return result;
        }
    }
}
