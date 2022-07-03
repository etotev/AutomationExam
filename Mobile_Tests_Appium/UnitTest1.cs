using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Threading;

namespace Mobile_Tests_Appium
{
    public class MobileAppBaseTest
    {
        private const string AppiumServerUri = "http://127.0.0.1:4723/wd/hub";
        private const string AppPath = @"C:\Exam\com.android.example.github.apk";
        protected AndroidDriver<AndroidElement> driver;
        private const string apiUrl = "https://shorturl.etotev.repl.co/api";


        [SetUp]
        public void Setup()
        {
            var options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", AppPath);
            driver = new AndroidDriver<AndroidElement>(
                new Uri(AppiumServerUri), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void ShutDown()
        {
            driver.Quit();
        }


        [Test]
        public void TestSearchForSeleniumAndAssertResult()
        {
            var searchField = driver.FindElementById("com.android.example.github:id/input");
            searchField.SendKeys("Selenium");

            driver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);

            Thread.Sleep(5000);

            var SeleniumHQResult = driver.FindElementByXPath("/hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.view.ViewGroup/android.widget.FrameLayout[2]/android.widget.FrameLayout/android.widget.FrameLayout/android.view.ViewGroup/androidx.recyclerview.widget.RecyclerView/android.widget.FrameLayout[1]/android.view.ViewGroup/android.widget.TextView[2]");

            Assert.That(SeleniumHQResult.Text, Is.EqualTo("SeleniumHQ/selenium"));
        }

    }
}