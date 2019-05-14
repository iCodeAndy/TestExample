using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestExample.Pages;

namespace TestExample.Helpers
{
    class Utils : Settings
    {
        public static void WaitChangedURL(string newURL)
        {
            webDriverWait.Until(webDriver => webDriver.Url.Equals(newURL));
        }

        public static void WaitElement(IWebElement webElement)
        {
            webDriverWait.Until(webDriver => webElement.Displayed);
        }

        public static void NavigateTo(string newURL)
        {
            webDriver.Navigate().GoToUrl(newURL);
        }

        public static void ScrollToElement(IWebElement webElement)
        {
            Actions actions = new Actions(webDriver);
            actions.MoveToElement(webElement).Perform();
        }

        public static string GetTitlePage()
        {
            return webDriver.Title;
        }

        public static void AuthorizationAsAdmin()
        {
            MainPage mainPage = new MainPage();
            mainPage.Authorization(phoneAdmin, passwordAdmin);
            mainPage.SetPinCode(pinCode);
        }
    }
}
