using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Talana.Handlers
{
    public static class KanbanizeHandler
    {
        public static void StartHandler()
        {
            AbrirPagina();
        }
        static void AbrirPagina()
        {
            string email = "odelrio@acl.cl";
            string password = "Od3lr10%23";

            var url = "https://businessmap.io/user-login";
            var edgeOptions = new EdgeOptions();

            var driver = new EdgeDriver(edgeOptions);
            driver.Navigate().GoToUrl(url);
            IniciarSesion(driver, email, password);

        }

        static void IniciarSesion(EdgeDriver driver, string email, string password)
        {
            var usernameField = driver.FindElement(By.Id("email"));
            var passwordField = driver.FindElement(By.Id("password"));

            usernameField.SendKeys(email);
            passwordField.SendKeys(password);


            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//iframe[starts-with(@name,'a-')]")));
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.recaptcha-checkbox-checkmark")));
            element.Click();

            //var loginButton = driver.FindElement(By.Id("form-submit-btn"));
            //loginButton.Click();
        }
    }
}
