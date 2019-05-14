using OpenQA.Selenium;
using TestExample.Helpers;

namespace TestExample.Pages
{
    class EnterNewPasswordPage : Settings
    {
        // Initializes the WebElements of the current page.
        private IWebElement SaveButton => webDriver.FindElement(By.ClassName("btn-primary"));

        private IWebElement NewPasswordInput => webDriver.FindElement(By.Id("NewPassword"));
        private IWebElement ConfirmPasswordInput => webDriver.FindElement(By.Id("ConfirmPassword"));


        // Methods for current page.
        public void InputNewPassword(string newPassword)
        {
            Utils.WaitElement(NewPasswordInput);

            NewPasswordInput.SendKeys(newPassword);
            ConfirmPasswordInput.SendKeys(newPassword);

            SaveButton.Click();
        }
    }
}
