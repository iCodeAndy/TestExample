using NUnit.Framework;
using System.Text.RegularExpressions;
using TestExample.Helpers;
using TestExample.Pages;

namespace TestExample.Tests
{
    class ChangeUserPassword : Settings
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
            mainPage.Authorization("+79515772917", "Maximum007");
            mainPage.SetPinCode("123456");

            Utils.WaitChangedURL("http://test.greenmoney.ru/Manager/Loans");
            Utils.NavigateTo("http://test.greenmoney.ru/Admin/EditUser/" + "903544");

            string clientPhone = "+" + Regex.Replace(editUserPage.GetUserPhone(), "[^0-9.]", "");
            editUserPage.ChangePassword("qwe123");
            editUserPage.ClickSave();

            headerPage.ClickExit();

            mainPage.CloseWarningPopup();
            mainPage.Authorization(clientPhone, "qwe123");

            enterNewPasswordPage.InputNewPassword("qwe123");

            mainPage.CloseWarningPopup();
            mainPage.Authorization(clientPhone, "qwe123");

            Assert.IsTrue(headerPage.ExitIsDisplayed());
        }

        [Test(Description = "Second test for Demonstration of running multiple tests.")]
        public void DemoTest()
        {
            mainPage.CloseWarningPopup();
            mainPage.Authorization("+79515772917", "Maximum007");
            mainPage.SetPinCode("123456");

            Assert.IsTrue(headerPage.ExitIsDisplayed());
        }

        [Test(Description = "Test for Demostration failed result.")]
        public void FailedTest()
        {
            mainPage.CloseWarningPopup();
            mainPage.Authorization("+79515772917", "Maximum007");
            mainPage.SetPinCode("123456");

            Assert.IsFalse(headerPage.ExitIsDisplayed());
        }
    }
}
