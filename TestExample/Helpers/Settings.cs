using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;

namespace TestExample.Helpers
{
    [TestFixture]
    class Settings
    {
        // Base variables.
        protected static IWebDriver webDriver;
        protected static WebDriverWait webDriverWait;

        protected static ExtentReports extentReports;
        protected static ExtentTest extentTest;

        protected string currentURL;
        protected string timeToRunTests;


        // Data for Tests.
        protected static string phoneAdmin = "+79515772917";
        protected static string passwordAdmin = "Maximum007";
        protected static string pinCode = "123456";
        protected static string defaultPassword = "qwe123";
        protected static string idClient = "903544";

        // Initialization of pages and parameters in a child class.
        public virtual void Initialize()
        {

        }


        // Settings for Tests.

        // Default setup before all tests.
        [OneTimeSetUp]
        public virtual void BeforeAll()
        {
            SetBrowserChrome();
            SetExternReport();

            Initialize();
        }

        // Default setup before each test.
        [SetUp]
        public virtual void Before()
        {
            // Initialize the test in the report.
            extentTest = extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
            Utils.NavigateTo(currentURL);
        }


        // Default setup after each test.
        [TearDown]
        public virtual void After() {
            MakeReportForCurrentTest();
            PreparingForTheNextTest();
        }

        // Default setup after all tests.
        [OneTimeTearDown]
        public virtual void AfterAll() {
            // Save the test report.
            extentReports.Flush();
            CloseBrowser();
        }


        // Settings for Browser.

        // Browser settings for Google Chrome.
        protected static void SetBrowserChrome()
        {
            ChromeOptions options = new ChromeOptions();

            // Configure the page load strategy. Normal - Wait until the page is fully loaded.
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            options.AddArguments("start-maximized");
            options.AddArgument("incognito");

            webDriver = new ChromeDriver(options);
            webDriverWait = new WebDriverWait(new SystemClock(), webDriver, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(3));

            webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        // Close the current window and open a new.
        private void PreparingForTheNextTest()
        {
            ((IJavaScriptExecutor)webDriver).ExecuteScript("window.open();");

            webDriver.SwitchTo().Window(webDriver.WindowHandles.First()).Close();
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
        }

        // Close all browser Windows. Not only window is currently in focus.
        private void CloseBrowser()
        {
            if (webDriver != null)
            {
                webDriver.Quit();
                webDriver = null;
            }
        }


        // Settings for Report.

        // Settings for Extern Report.
        private void SetExternReport()
        {
            extentReports = new ExtentReports();

            // Set up directories for the Report.
            string directory = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            DirectoryInfo directoryInfo = Directory.CreateDirectory(directory + "\\Reports");
            ExtentHtmlReporter extentHtmlReporter = new ExtentHtmlReporter(directory + "\\Reports\\");

            // The Declaration of environment variables. 
            // Configure the report type and view.
            extentReports.AddSystemInfo("Test environment", "http://test.greenmoney.ru/");
            extentReports.AttachReporter(extentHtmlReporter);
            extentHtmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

            timeToRunTests = DateTime.Now.DayOfWeek + "_" + DateTime.Now.Hour;
        }

        // Generate a report for the current test.
        private void MakeReportForCurrentTest()
        {
            // Getting information about the test.
            TestStatus status = TestContext.CurrentContext.Result.Outcome.Status;
            string stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            string textMessage = "<pre>" + TestContext.CurrentContext.Result.Message + "</pre>";
            string descriptionTest = TestContext.CurrentContext.Test.Properties.Get("Description").ToString();

            // Record the test result.
            switch (status)
            {
                case TestStatus.Failed:
                    string screenShotPath = MakeScreenShot(TestContext.CurrentContext.Test.Name);
                    extentTest.Log(Status.Fail, "Description:<br>" + descriptionTest);
                    extentTest.Log(Status.Fail, "Message:<br>" + textMessage);
                    extentTest.Log(Status.Fail, "StackTrace<br>" + stackTrace);
                    extentTest.Log(Status.Fail, "" + extentTest.AddScreenCaptureFromPath(screenShotPath));
                    break;

                case TestStatus.Skipped:
                    extentTest.Log(Status.Skip, "SKIPPED");
                    extentTest.Log(Status.Skip, "Description:<br>" + descriptionTest);
                    break;

                default:
                    extentTest.Log(Status.Pass, "PASS");
                    extentTest.Log(Status.Pass, "Description:<br>" + descriptionTest);
                    break;
            }
        }

        // Save a screenshot of the error.
        private string MakeScreenShot(string screenShotName)
        {
            string localPath;

            ITakesScreenshot takesScreenshot = (ITakesScreenshot)webDriver;
            Screenshot screenShot = takesScreenshot.GetScreenshot();

            // Set up directories for the ScreenShot.
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string directory = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            DirectoryInfo directoryInfo = Directory.CreateDirectory(directory + "\\ScreenShots\\" + timeToRunTests + "\\");

            // To take a screenshot.
            string finalpth = path.Substring(0, path.LastIndexOf("bin")) + "ScreenShots/" + timeToRunTests + "/" + screenShotName + ".png";
            localPath = new Uri(finalpth).LocalPath;
            screenShot.SaveAsFile(localPath, ScreenshotImageFormat.Png);

            return localPath;
        }
    }
}
