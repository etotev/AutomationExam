using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace TaskBoard_WebApp_Testing
{
    public class WebApp_Selenium_Tests
    {
        private const string url = "https://shorturl.etotev.repl.co/";
        private WebDriver driver;

        [SetUp]
        public void OpenBrowser()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void CloseBrowser()
        {
            this.driver.Quit();
        }


        [Test]
        public void CheckFirstDoneTask()
        {
            driver.Navigate().GoToUrl(url);
            var shortUrlsButton = driver.FindElement(By.LinkText("Short URLs"));
            shortUrlsButton.Click();

            string expected = "https://nakov.com";

            var firstLinkInTable = driver.FindElement(By.CssSelector("body > main > table > tbody > tr:nth-child(1) > td:nth-child(1) > a"));

            Assert.That(firstLinkInTable.Text, Is.EqualTo(expected));

        }


        [Test]
        public void Add_New_ShortUrl()
        {
            driver.Navigate().GoToUrl(url);
            var addUrlButton = driver.FindElement(By.LinkText("Add URL"));
            addUrlButton.Click();
            string urlToAdd = "https://gong.bg";
            string shortCode = "gong";

            driver.FindElement(By.Id("url")).SendKeys($"{urlToAdd}");
            driver.FindElement(By.Id("code")).SendKeys($"{shortCode}");
            driver.FindElement(By.CssSelector("td > button")).Click();


            var lastAddedLink = driver.FindElement(By.CssSelector("table > tbody > tr:last-child > td:nth-child(1) > a"));

            Assert.That(lastAddedLink.Text, Is.EqualTo($"{urlToAdd}"));

        }

        [Test]
        public void Add_New_Invalid_Url()
        {
            driver.Navigate().GoToUrl(url);
            var addUrlButton = driver.FindElement(By.LinkText("Add URL"));
            addUrlButton.Click();
            string urlToAdd = "www.gong.bg";
            string shortCode = "gong";

            driver.FindElement(By.Id("url")).SendKeys($"{urlToAdd}");
            driver.FindElement(By.Id("code")).SendKeys($"{shortCode}");
            driver.FindElement(By.CssSelector("td > button")).Click();

            var erorMsg = driver.FindElement(By.CssSelector("body > div.err"));

            Assert.That(erorMsg.Text, Is.EqualTo("Invalid URL!"));

        }

        [Test]
        public void Navigate_To_Invalid_Url()
        {
            var invalidUrl = "invalidLinkdsds";
            driver.Navigate().GoToUrl(url + "go/" + invalidUrl);

            var erorMsg = driver.FindElement(By.CssSelector("body > div.err"));
            var errorHeader = driver.FindElement(By.CssSelector("body > main > h1"));
            var errorInvalidUrl = driver.FindElement(By.CssSelector("body > main > p"));

            Assert.That(erorMsg.Text, Is.EqualTo("Cannot navigate to given short URL"));
            Assert.That(errorHeader.Text, Is.EqualTo("Error: Cannot navigate to given short URL"));
            Assert.That(errorInvalidUrl.Text, Is.EqualTo($"Invalid short URL code: {invalidUrl}"));
        }


        [Test]
        public void Navigate_To_ValidUrl_Validate_Counter()
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.LinkText("Short URLs")).Click();

            var countOfVisits = driver.FindElement(By.CssSelector("table > tbody > tr:nth-child(2) > td:nth-child(4)"));
            var previousCount = countOfVisits.Text;
            
            driver.Navigate().GoToUrl(url + "go/seldev");

            driver.Navigate().GoToUrl(url + "/urls");
          

            var newCount = driver.FindElement(By.CssSelector("table > tbody > tr:nth-child(2) > td:nth-child(4)")).Text;

            Assert.That(int.Parse(newCount), Is.GreaterThan(int.Parse(previousCount)));

        }

    }
}