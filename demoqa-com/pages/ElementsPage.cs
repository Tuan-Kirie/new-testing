using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace demoqa_com.pages

{
    public class ElementsPage : BasePage
    {
        private const string textBoxPageBtn = "//span[text()='Text Box']//ancestor::li";
        private const string textBoxFullNameInp = "#userName-wrapper input";
        private const string textBoxSubmitBtn = "#submit";
        private const string textBoxResult = "#output";
        private const string textBoxResultFullname = "#output #name";
        private const string textBoxEmailInp = "#userEmail";
        private const string textBoxResultEmail = "#output #email";
        public ElementsPage(IWebDriver driver, string url, int timeout = 4) : base(driver, url, timeout)
        {
            this.driver = driver;
            this.url = url;
            this.timeout = timeout;
        }

        public void open_test_box()
        {
            IWebElement elem = this.driver.FindElement(By.XPath(textBoxPageBtn));
            elem.Click();
        }
        
        public void input_data_to_full_name(string data)
        {
            IWebElement elem = this.driver.FindElement(By.CssSelector(textBoxFullNameInp));
            elem.SendKeys(data);
        }

        public void submit_form()
        {
            scroll_window_to_elem(textBoxSubmitBtn.Substring(1));
            IWebElement submit = this.driver.FindElement(By.CssSelector(textBoxSubmitBtn));
            submit.Click();
        }

        public IWebElement get_form_send_result()
        {
            return this.driver.FindElement(By.CssSelector(textBoxResult));
        }

        public void input_data_to_email(string data)
        {
            IWebElement elem = this.driver.FindElement(By.CssSelector(textBoxEmailInp));
            elem.SendKeys(data);
        }

        public Boolean check_email_send_res(string email)
        {
            if (check_element_on_DOM(textBoxResultEmail) != null)
            {
                IWebElement res_email = this.driver.FindElement(By.CssSelector(textBoxResultEmail));
                string result = res_email.Text.Substring(6);
                return email == result;
            }
            else
            {
                return false;
            }
        }
    }
}