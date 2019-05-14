using OpenQA.Selenium;
using TestExample.Helpers;

namespace TestExample.Pages
{
    class MainPage : Settings
    {
        // Initializes the WebElements of the current page.
        private IWebElement CloseWarningPopupButton => webDriver.FindElement(By.XPath("/html[1]/body[1]/div[3]/div[1]/p[1]/span[1]"));
        private IWebElement PersonCabinetButton => webDriver.FindElement(By.Id("loginLink"));
        private IWebElement LoginButton => webDriver.FindElement(By.XPath("//*[@id=\"loginForm\"]/div[3]/span"));
        private IWebElement ConfirmButton => webDriver.FindElement(By.XPath("//*[@id=\"loginConfirmCodeForm\"]/div[5]/span[1]"));

        private IWebElement PhoneInput => webDriver.FindElement(By.Id("UserName"));
        private IWebElement PasswordInput => webDriver.FindElement(By.Id("Password"));
        private IWebElement PinCodeInput => webDriver.FindElement(By.Id("ConfirmationCode"));

        private IWebElement WarningPopup => webDriver.FindElement(By.Id("paymentsModalArea"));
        private IWebElement AuthorizationForm => webDriver.FindElement(By.Id("loginForm"));
        private IWebElement PinCodeForm => webDriver.FindElement(By.Id("loginConfirmCodeForm"));


        // Methods for current page.
        public void CloseWarningPopup()
        {
            Utils.WaitElement(WarningPopup);
            CloseWarningPopupButton.Click();
        }

        public void Authorization(string phone, string password)
        {
            ClickPersonCabinet();
            Utils.WaitElement(AuthorizationForm);

            PhoneInput.SendKeys(phone);
            PasswordInput.SendKeys(password);

            LoginButton.Click();
        }

        public void ClickPersonCabinet()
        {
            Utils.WaitElement(PersonCabinetButton);
            PersonCabinetButton.Click();
        }

        public void SetPinCode(string pinCode)
        {
            Utils.WaitElement(PinCodeForm);
            PinCodeInput.SendKeys(pinCode);

            ConfirmButton.Click();
        }
    }
}
