using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace demoqa_com.pages

{
    public class ElementsPage : BasePage
    {
        //ElementsPage locators
        private const string textBoxPageBtn = "//span[text()='Text Box']//ancestor::li";
        private const string radioBtnPageBtn = "//span[text()='Radio Button']//ancestor::li";
        // TextBoxPage locators
        private const string textBoxFullNameInp = "#userName-wrapper input";
        private const string textBoxSubmitBtn = "#submit";
        private const string textBoxResult = "#output";
        private const string textBoxResultFullname = "#output #name";
        private const string textBoxEmailInp = "#userEmail";
        private const string textBoxResultEmail = "#output #email";
       
        //RadioBtnPage Locators
        private const string radioBtnYesRadio = "#yesRadio";
        private const string radioBtnImpressiveRadio = "#impressiveRadio";
        private const string radioBtnNoRadio = "#noRadio";
        private const string radioBtnOutput = ".text-success";
        
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

        public void open_radio_btn()
        {
            IWebElement elem = this.driver.FindElement(By.XPath(radioBtnPageBtn));
            elem.Click();
        }

        public void click_radio(string radio_type)
        {
            if (radio_type == "Yes")
            {
                IWebElement elem = check_element_is_clickable(radioBtnYesRadio);
                elem.Click();
            } else if (radio_type == "Impressive")
            {
                IWebElement elem = check_element_is_clickable(radioBtnImpressiveRadio);
                elem.Click();
            }
            else
            {
                IWebElement elem = check_element_is_clickable(radioBtnNoRadio);
                elem.Click();
            }
        }

        private Boolean check_radio_output(string selected_radio)
        {
            IWebElement elem = this.driver.FindElement(By.CssSelector(radioBtnOutput));
            return selected_radio == elem.Text;
        }
        
        public Boolean check_radio_select_res(string radio_btn_type)
        {
            switch (radio_btn_type)
            {
                case "Yes":
                    return check_radio_output("Yes"); 
                case "Impressive":
                    return check_radio_output("Impressive");
                case "No":
                    return check_radio_output("No");
            }
            return false;
        }
        
        
    }
}