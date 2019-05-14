using OpenQA.Selenium;
using TestExample.Helpers;

namespace TestExample.Pages
{
    class EditUserPage : Settings
    {
        // Initializes the WebElements of the current page.
        private IWebElement SaveButton => webDriver.FindElement(By.ClassName("btn-primary"));

        private IWebElement PhoneInput => webDriver.FindElement(By.Id("Phone"));
        private IWebElement PasswordInput => webDriver.FindElement(By.Id("Password"));


        // Methods for current page.
        public string GetUserPhone()
        {
            Utils.WaitElement(PhoneInput);
            return PhoneInput.GetAttribute("value");
        }

        public void ChangePassword(string password)
        {
            PasswordInput.Clear();
            PasswordInput.SendKeys(password);
        }

        public void ClickSave()
        {
            Utils.ScrollToElement(SaveButton);
            SaveButton.Click();
        }
    }
}
