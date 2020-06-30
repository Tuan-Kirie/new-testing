using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;

namespace demoqa_com.pages

{
    public class ElementsPage : BasePage
    {
        //ElementsPage locators
        private const string textBoxPageBtn = "//span[text()='Text Box']//ancestor::li";
        private const string radioBtnPageBtn = "//span[text()='Radio Button']//ancestor::li";
        private const string webTablesPageBtn = "//span[text()='Web Tables']//ancestor::li";
        private const string buttonsPageBtn = "//span[text()='Buttons']//ancestor::li";
        private const string upldowPageBtn = "//span[text()='Upload and Download']//ancestor::li";
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
        
        //WebTablesPage Locators
        private const string webTablesTable = ".rt-table";
        private const string webTablesAddNewBtn = "#addNewRecordButton";
        private const string webTablesEditBtn = ".action-buttons > span[title=\"Edit\"]";
        private const string webTablesRemoveBtn = ".action-buttons > span[title=\"Delete\"]";
        private const string webTablesFormFirstNameInput = "#firstName";
        private const string webTablesFormLastNameInput = "#lastName";
        private const string webTablesFormEmailInput = "#userEmail";
        private const string webTablesFormAgeInput = "#age";
        private const string webTablesFormSalaryInput = "salary";
        private const string webTablesFormDepartmentInput = "#department";
        private const string webTablesFormSendBtn = "#submit";
        private const string webTablesUserForm = "#userForm";
        private const string webTablesUserFormInputs = "#userForm input";
        private const string webTablesRow = ".rt-tr[role=row].-odd";
        
        //ButtonsPage Locators
        private const string btnDoubleClickBtn = "#doubleClickBtn";
        private const string btnRightClickBtn = "#rightClickBtn";
        private const string btnLeftClickBtn = "//button[text()='Click Me']";
        
        //Upload and Download page Locators;
        private const string uplodownSelectBtn = "#uploadFile";
        private const string uplodownDownloadBtn = "#downloadButton";
        private const string uplodownResult = "#uploadedFilePath";
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
                click_to_element_with_js(radioBtnYesRadio);
            } else if (radio_type == "Impressive")
            {
                click_to_element_with_js(radioBtnImpressiveRadio);
            }
            else
            {
                click_to_element_with_js(radioBtnNoRadio);
            }
        }

        private Boolean check_radio_output(string selected_radio)
        {
            try
            {
                IWebElement elem = this.driver.FindElement(By.CssSelector(radioBtnOutput));
                return selected_radio == elem.Text;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
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

        public void open_web_tables()
        {
            IWebElement elem = this.driver.FindElement(By.XPath(webTablesPageBtn));
            elem.Click();
        }

        public void add_new_table_row(string[] data)
        {
            IWebElement add_btn = this.driver.FindElement(By.CssSelector(webTablesAddNewBtn));
            add_btn.Click();
            List<IWebElement> form_inputs = new List<IWebElement>(this.driver.FindElements(By.CssSelector(webTablesUserFormInputs)));
            for (int i = 0; i < form_inputs.Count; i++)
            {
                form_inputs[i].SendKeys(data[i]);
            }

            IWebElement sbm_btn = this.driver.FindElement(By.CssSelector(webTablesFormSendBtn));
            sbm_btn.Click();
        }

        public void edit_existing_table(string[] data)
        {
            IWebElement edit_btn = this.driver.FindElement(By.CssSelector(webTablesEditBtn));
            edit_btn.Click();
            List<IWebElement> form_inputs = new List<IWebElement>(this.driver.FindElements(By.CssSelector(webTablesUserFormInputs)));
            for (int i = 0; i < form_inputs.Count; i++)
            {
                form_inputs[i].SendKeys(Keys.Control + "a");
                form_inputs[i].SendKeys(Keys.Delete);
                form_inputs[i].SendKeys(data[i]);
            }

            IWebElement sbm_btn = this.driver.FindElement(By.CssSelector(webTablesFormSendBtn));
            sbm_btn.Click();
        }
        
        public Boolean check_new_raw_exist(string[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (check_element_in_Dom_XPATH(data[i]))
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public string[] delete_existing_row()
        {
            IWebElement existing_row = this.driver.FindElement(By.CssSelector(webTablesRow));
            List<IWebElement> row_elems_B = new List<IWebElement>(existing_row.FindElements(By.CssSelector("div")));
            row_elems_B.RemoveAt(row_elems_B.Count - 1);
            string[] row_content = new string[row_elems_B.Count];
            for (int i = 0; i < row_elems_B.Count; i++)
            {
                row_content[i] = row_elems_B[i].Text;
            }
            IWebElement delete_btn = this.driver.FindElement(By.CssSelector(webTablesRemoveBtn));
            delete_btn.Click();
            return row_content;
        }

        public Boolean check_row_deleting(string[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (!check_element_in_Dom_XPATH(data[i]))
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public void go_to_buttons_page()
        {
            IWebElement buttons_page_btn = this.driver.FindElement(By.XPath(buttonsPageBtn));
            buttons_page_btn.Click();
        }

        public void click_to_double_click_btn()
        {
            Actions action = new Actions(this.driver);
            IWebElement btn = this.driver.FindElement(By.CssSelector(btnDoubleClickBtn));
            action.DoubleClick(btn).Perform();
        }

        public void click_to_right_click_btn()
        {    
            Actions action = new Actions(this.driver);
            IWebElement btn = this.driver.FindElement(By.CssSelector(btnRightClickBtn));
            action.ContextClick(btn).Perform();
        }

        public void click_to_common_click_btn()
        {
            IWebElement btn = this.driver.FindElement(By.XPath(btnLeftClickBtn));
            btn.Click();
        }

        public Boolean check_btn_click(string btn_type)
        {
            try
            {
                switch (btn_type)
                {
                    case "double":
                        this.driver.FindElement(By.CssSelector("#doubleClickMessage"));
                        return true;
                    case "left":
                        this.driver.FindElement(By.CssSelector("#dynamicClickMessage"));
                        return true;
                    case "right":
                        this.driver.FindElement(By.CssSelector("#rightClickMessage"));
                        return true;
                }
            }
            catch (NoSuchElementException)
            {
                return false;
            }

            return false;
        }
        
        public void go_to_uplodown_page()
        {
            IWebElement elem  = this.driver.FindElement(By.XPath(upldowPageBtn));
            elem.Click();
        }

        public void upload_test_file(string file_name)
        {
            IWebElement upload_inp = this.driver.FindElement(By.CssSelector(uplodownSelectBtn));
            string upload_file = UploadFilesPath + file_name;
            upload_inp.SendKeys(upload_file);
            
        }

        public Boolean check_file_upload(string file_name)
        {
            IWebElement res = this.driver.FindElement(By.CssSelector(uplodownResult));
            string result = res.Text;
            if (result == "C:\\fakepath\\" + file_name)
            {
                return true;
            }

            return false;
        }

        public void uplodown_download_file()
        {
            IWebElement elem = this.driver.FindElement(By.CssSelector(uplodownDownloadBtn));
            elem.Click();
        }
    }
}