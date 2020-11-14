using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MagnusProj
{
    class AutomationAssignment
    {
        public static IWebDriver driver { get; set; }

       
        public void startBrowser()
        {
           driver = new ChromeDriver("C:\\Users\\kwaku\\OneDrive\\Desktop\\C#\\AutoFramework\\packages\\Selenium.WebDriver.ChromeDriver.85.0.4183.8700\\driver\\win32\\");// start instance of chrome browser
        }

        [Test]
        public void TestFormAuthentication()
        {
            startBrowser();
            driver.Navigate().GoToUrl("http://the-internet.herokuapp.com");//we navigate to website
            driver.Manage().Window.Maximize();
            driver.FindElement(By.CssSelector("#content > ul:nth-child(4) > li:nth-child(21) > a:nth-child(1)")).Click(); //click on authentication link
            Thread.Sleep(200);
            driver.FindElement(By.Id("username")).SendKeys("ABCDERER"); //enter username
            driver.FindElement(By.Id("password")).SendKeys("magnus1");//enter password
            driver.FindElement(By.CssSelector(".fa")).Click();
            Thread.Sleep(200);
            IWebElement errormessage = driver.FindElement(By.Id("flash")); //This is the error message displayed on the screen
            Assert.AreEqual(errormessage.Text , "Your username is invalid!\r\n×");

        }

        [Test]
        public void TestInfiniteScroll()
        {
            startBrowser();
            driver.Navigate().GoToUrl("http://the-internet.herokuapp.com");
            driver.Manage().Window.Maximize();
            driver.FindElement(By.CssSelector("#content > ul:nth-child(4) > li:nth-child(26) > a:nth-child(1)")).Click();
            IWebElement endofpage = driver.FindElement(By.ClassName("jscroll-added"));
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);",endofpage); //scroll to the end of page 
            Thread.Sleep(2000);
            IWebElement InfiniteScroll = driver.FindElement(By.CssSelector(".example > h3:nth-child(1)"));
            Assert.IsTrue(InfiniteScroll.Text == "Infinite Scroll");

        }

        [Test]
        public void keypresses()
        {
            startBrowser();
            driver.Navigate().GoToUrl("http://the-internet.herokuapp.com");
            driver.Manage().Window.Maximize();
            driver.FindElement(By.CssSelector("#content > ul:nth-child(4) > li:nth-child(31) > a:nth-child(1)")).Click();
            driver.FindElement(By.Id("target")).SendKeys("W");
            Assert.AreEqual(driver.FindElement(By.Id("result")).Text, "You entered:" + " W");
            driver.Navigate().Refresh();
            driver.FindElement(By.Id("target")).SendKeys("M");
            Assert.AreEqual(driver.FindElement(By.Id("result")).Text, "You entered:" + " M");
            driver.Navigate().Refresh();
            driver.FindElement(By.Id("target")).SendKeys("R");
            Assert.AreEqual(driver.FindElement(By.Id("result")).Text, "You entered:" + " R");
            driver.Navigate().Refresh();
            driver.FindElement(By.Id("target")).SendKeys("P");
            Assert.AreEqual(driver.FindElement(By.Id("result")).Text, "You entered:" + " P");

            // script can be upgraded with a for loop
            

        }

        [Test]
        public void useforloopkeypress()
        {
            startBrowser();
            driver.Navigate().GoToUrl("http://the-internet.herokuapp.com");
            driver.Manage().Window.Maximize();
            driver.FindElement(By.CssSelector("#content > ul:nth-child(4) > li:nth-child(31) > a:nth-child(1)")).Click();
            var keypresses = new List<string> { " W", " M", " R"," P" };
            foreach (string key in keypresses)
            {
                driver.FindElement(By.Id("target")).SendKeys(key);
                Assert.AreEqual(driver.FindElement(By.Id("result")).Text, "You entered:" +""+key);
                driver.Navigate().Refresh();

            }
               
                
        }


        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}
