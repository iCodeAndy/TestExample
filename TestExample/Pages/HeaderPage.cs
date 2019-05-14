using OpenQA.Selenium;
using TestExample.Helpers;

namespace TestExample.Pages
{
    class HeaderPage : Settings
    {
        // Initializes the WebElements of the current page.
        private IWebElement ExitButton => webDriver.FindElement(By.LinkText("Выйти"));

        // Methods for current page.
        public void ClickExit()
        {
            Utils.WaitElement(ExitButton);
            ExitButton.Click();
        }

        public bool ExitIsDisplayed()
        {
            Utils.WaitElement(ExitButton);
            return ExitButton.Displayed;
        }
    }
}
