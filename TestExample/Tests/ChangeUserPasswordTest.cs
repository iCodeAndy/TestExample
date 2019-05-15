using NUnit.Framework;
using System.Text.RegularExpressions;
using TestExample.Helpers;
using TestExample.Pages;

namespace TestExample.Tests
{
    class ChangeUserPasswordTest : Settings
    {
        // Required variables for tests.
        private MainPage mainPage;
        private EditUserPage editUserPage;
        private HeaderPage headerPage;
        private EnterNewPasswordPage enterNewPasswordPage;

        
        // Initializes required variables.
        public override void Initialize()
        {
            currentURL = "http://test.greenmoney.ru/";

            mainPage = new MainPage();
            editUserPage = new EditUserPage();
            headerPage = new HeaderPage();
            enterNewPasswordPage = new EnterNewPasswordPage();
        }


        // Tests.

        [Test(Description = "Change the user password through the admin panel.")]
        public void ChangeUserPasswordThroughAdminPanelTest()
        {
            mainPage.CloseWarningPopup();
            mainPage.Authorization(phoneAdmin, passwordAdmin);
            mainPage.SetPinCode(pinCode);

            Utils.WaitChangedURL(managerLoans);
            Utils.NavigateTo(adminEditUser + idClient);

            string clientPhone = "+" + Regex.Replace(editUserPage.GetUserPhone(), "[^0-9.]", "");
            editUserPage.ChangePassword(defaultPassword);
            editUserPage.ClickSave();

            headerPage.ClickExit();

            mainPage.CloseWarningPopup();
            mainPage.Authorization(clientPhone, defaultPassword);

            enterNewPasswordPage.InputNewPassword(defaultPassword);

            mainPage.CloseWarningPopup();
            mainPage.Authorization(clientPhone, defaultPassword);

            Assert.IsTrue(headerPage.ExitIsDisplayed());
        }

        [Test(Description = "Second test for Demonstration of running multiple tests.")]
        public void SuccessAuthorizationTest()
        {
            mainPage.CloseWarningPopup();
            mainPage.Authorization(phoneAdmin, passwordAdmin);
            mainPage.SetPinCode(pinCode);

            Assert.IsTrue(headerPage.ExitIsDisplayed());
        }

        [Test(Description = "Test for Demostration failed result.")]
        public void AuthorizationWithoutPasswordFailTest()
        {
            mainPage.CloseWarningPopup();
            mainPage.Authorization(phoneAdmin, string.Empty);
            mainPage.SetPinCode(pinCode);

            Assert.IsFalse(headerPage.ExitIsDisplayed());
        }
    }
}
