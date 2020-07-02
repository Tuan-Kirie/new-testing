using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace demoqa_com.pages

{
    public class ElementsPage : BasePage
    {
        //ElementsPage locators
        private const string TextBoxPageBtn = "//span[text()='Text Box']//ancestor::li";
        private const string RadioBtnPageBtn = "//span[text()='Radio Button']//ancestor::li";
        private const string WebTablesPageBtn = "//span[text()='Web Tables']//ancestor::li";
        private const string ButtonsPageBtn = "//span[text()='Buttons']//ancestor::li";
        private const string UpldowPageBtn = "//span[text()='Upload and Download']//ancestor::li";
        private const string LinksPageBtn = "//span[text()='Links']//ancestor::li";
        private const string DynamicPageBtn = "//span[text()='Dynamic Properties']//ancestor::li";
        // TextBoxPage locators
        private const string TextBoxFullNameInp = "#userName-wrapper input";
        private const string TextBoxSubmitBtn = "#submit";
        private const string TextBoxResult = "#output";
        private const string TextBoxEmailInp = "#userEmail";
        private const string TextBoxResultEmail = "#output #email";
       
        //RadioBtnPage Locators
        private const string RadioBtnYesRadio = "#yesRadio";
        private const string RadioBtnImpressiveRadio = "#impressiveRadio";
        private const string RadioBtnNoRadio = "#noRadio";
        private const string RadioBtnOutput = ".text-success";
        
        //WebTablesPage Locators
        private const string WebTablesAddNewBtn = "#addNewRecordButton";
        private const string WebTablesEditBtn = ".action-buttons > span[title=\"Edit\"]";
        private const string WebTablesRemoveBtn = ".action-buttons > span[title=\"Delete\"]";
        private const string WebTablesFormSendBtn = "#submit";
        private const string WebTablesUserFormInputs = "#userForm input";
        private const string WebTablesRow = ".rt-tr[role=row].-odd";
        
        //ButtonsPage Locators
        private const string BtnDoubleClickBtn = "#doubleClickBtn";
        private const string BtnRightClickBtn = "#rightClickBtn";
        private const string BtnLeftClickBtn = "//button[text()='Click Me']";
        
        //Upload and Download page Locators;
        private const string UplodownSelectBtn = "#uploadFile";
        private const string UplodownDownloadBtn = "#downloadButton";
        private const string UplodownResult = "#uploadedFilePath";
        
        //Links page Locators
        private const string LinksCommonLink = "#simpleLink";
        private const string LinksDynamicLink = "#dynamicLink";
        private const string CreatedLink = "#created";
        private const string NoContentLink = "#no-content";
        private const string MovedLink = "#moved";
        private const string BadRequestLink = "#bad-request";
        private const string UnathorizedLink = "#unauthorized";
        private const string ForbiddenLink = "#forbidden";
        private const string NotFoundLink = "#invalid-url";
        private const string LinkResponse = "#linkResponse";

        //Dynamic Properties
        private const string DynamicEnableAfterbtn = "#enableAfter";
        private const string DynamicColorChangebtn = "#colorChange";
        private const string DynamicVisibleAfterbtn = "//*[text()='Visible After 5 Seconds']";


        public ElementsPage(IWebDriver driver, string url, int timeout = 4) : base(driver, url, timeout)
        {
            this.Driver = driver;
            this.url = url;
            this.timeout = timeout;
        }

        public void open_test_box()
        {
            IWebElement elem = this.Driver.FindElement(By.XPath(TextBoxPageBtn));
            elem.Click();
        }
        
        public void input_data_to_full_name(string data)
        {
            IWebElement elem = this.Driver.FindElement(By.CssSelector(TextBoxFullNameInp));
            elem.SendKeys(data);
        }

        public void submit_form()
        {
            scroll_window_to_elem(TextBoxSubmitBtn.Substring(1));
            IWebElement submit = this.Driver.FindElement(By.CssSelector(TextBoxSubmitBtn));
            submit.Click();
        }

        public IWebElement get_form_send_result()
        {
            return this.Driver.FindElement(By.CssSelector(TextBoxResult));
        }

        public void input_data_to_email(string data)
        {
            IWebElement elem = this.Driver.FindElement(By.CssSelector(TextBoxEmailInp));
            elem.SendKeys(data);
        }

        public Boolean check_email_send_res(string email)
        {
            if (check_element_on_DOM(TextBoxResultEmail) != null)
            {
                IWebElement resEmail = this.Driver.FindElement(By.CssSelector(TextBoxResultEmail));
                string result = resEmail.Text.Substring(6);
                return email == result;
            }
            else
            {
                return false;
            }
        }

        public void open_radio_btn()
        {
            IWebElement elem = this.Driver.FindElement(By.XPath(RadioBtnPageBtn));
            elem.Click();
        }

        public void click_radio(string radioType)
        {
            if (radioType == "Yes")
            {
                click_to_element_with_js(RadioBtnYesRadio);
            } else if (radioType == "Impressive")
            {
                click_to_element_with_js(RadioBtnImpressiveRadio);
            }
            else
            {
                click_to_element_with_js(RadioBtnNoRadio);
            }
        }

        private Boolean check_radio_output(string selectedRadio)
        {
            try
            {
                IWebElement elem = this.Driver.FindElement(By.CssSelector(RadioBtnOutput));
                return selectedRadio == elem.Text;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        public bool check_radio_select_res(string radioBtnType)
        {
            switch (radioBtnType)
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
            IWebElement elem = this.Driver.FindElement(By.XPath(WebTablesPageBtn));
            elem.Click();
        }

        public void add_new_table_row(string[] data)
        {
            IWebElement addBtn = this.Driver.FindElement(By.CssSelector(WebTablesAddNewBtn));
            addBtn.Click();
            List<IWebElement> formInputs = new List<IWebElement>(this.Driver.FindElements(By.CssSelector(WebTablesUserFormInputs)));
            for (int i = 0; i < formInputs.Count; i++)
            {
                formInputs[i].SendKeys(data[i]);
            }

            IWebElement sbmBtn = this.Driver.FindElement(By.CssSelector(WebTablesFormSendBtn));
            sbmBtn.Click();
        }

        public void edit_existing_table(string[] data)
        {
            IWebElement editBtn = this.Driver.FindElement(By.CssSelector(WebTablesEditBtn));
            editBtn.Click();
            List<IWebElement> formInputs = new List<IWebElement>(this.Driver.FindElements(By.CssSelector(WebTablesUserFormInputs)));
            for (int i = 0; i < formInputs.Count; i++)
            {
                formInputs[i].SendKeys(Keys.Control + "a");
                formInputs[i].SendKeys(Keys.Delete);
                formInputs[i].SendKeys(data[i]);
            }

            IWebElement sbmBtn = this.Driver.FindElement(By.CssSelector(WebTablesFormSendBtn));
            sbmBtn.Click();
        }
        
        public Boolean check_new_raw_exist(IEnumerable<string> data)
        {
            foreach (var t in data)
            {
                if (check_element_in_Dom_XPATH(t))
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
            IWebElement existingRow = this.Driver.FindElement(By.CssSelector(WebTablesRow));
            List<IWebElement> rowElemsB = new List<IWebElement>(existingRow.FindElements(By.CssSelector("div")));
            rowElemsB.RemoveAt(rowElemsB.Count - 1);
            string[] rowContent = new string[rowElemsB.Count];
            for (int i = 0; i < rowElemsB.Count; i++)
            {
                rowContent[i] = rowElemsB[i].Text;
            }
            IWebElement deleteBtn = this.Driver.FindElement(By.CssSelector(WebTablesRemoveBtn));
            deleteBtn.Click();
            return rowContent;
        }

        public bool check_row_deleting(IEnumerable<string> data)
        {
            foreach (var t in data)
            {
                if (!check_element_in_Dom_XPATH(t))
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
            IWebElement elem = this.Driver.FindElement(By.XPath(ButtonsPageBtn));
            elem.Click();
        }

        public void click_to_double_click_btn()
        {
            Actions action = new Actions(this.Driver);
            IWebElement btn = this.Driver.FindElement(By.CssSelector(BtnDoubleClickBtn));
            action.DoubleClick(btn).Perform();
        }

        public void click_to_right_click_btn()
        {    
            Actions action = new Actions(this.Driver);
            IWebElement btn = this.Driver.FindElement(By.CssSelector(BtnRightClickBtn));
            action.ContextClick(btn).Perform();
        }

        public void click_to_common_click_btn()
        {
            IWebElement btn = this.Driver.FindElement(By.XPath(BtnLeftClickBtn));
            btn.Click();
        }

        public bool check_btn_click(string btnType)
        {
            try
            {
                switch (btnType)
                {
                    case "double":
                        this.Driver.FindElement(By.CssSelector("#doubleClickMessage"));
                        return true;
                    case "left":
                        this.Driver.FindElement(By.CssSelector("#dynamicClickMessage"));
                        return true;
                    case "right":
                        this.Driver.FindElement(By.CssSelector("#rightClickMessage"));
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
            IWebElement elem  = this.Driver.FindElement(By.XPath(UpldowPageBtn));
            elem.Click();
        }

        public void upload_test_file(string fileName)
        {
            IWebElement uploadInp = this.Driver.FindElement(By.CssSelector(UplodownSelectBtn));
            string uploadFile = UploadFilesPath + fileName;
            uploadInp.SendKeys(uploadFile);
            
        }

        public Boolean check_file_upload(string fileName)
        {
            IWebElement res = this.Driver.FindElement(By.CssSelector(UplodownResult));
            string result = res.Text;
            if (result == "C:\\fakepath\\" + fileName)
            {
                return true;
            }

            return false;
        }

        public void uplodown_download_file()
        {
            IWebElement elem = this.Driver.FindElement(By.CssSelector(UplodownDownloadBtn));
            elem.Click();
        }
        
        public void go_to_links_page()
        {
            IWebElement elem  = this.Driver.FindElement(By.XPath(LinksPageBtn));
            elem.Click();
        }

        public string open_link_in_new_tab(string linkType)
        {
            if (linkType == "common")
            {
                IWebElement elem = this.Driver.FindElement(By.CssSelector(LinksCommonLink));
                elem.Click();
            }else if (linkType == "dynamic")
            {
                IWebElement elem = this.Driver.FindElement(By.CssSelector(LinksDynamicLink));
                elem.Click();
            }
    
            Collection<string> tabs = new Collection<string>(this.Driver.WindowHandles);
            this.Driver.SwitchTo().Window(tabs[tabs.Count - 1]);
            string openedLink = this.Driver.Url;
            this.Driver.Close();
            this.Driver.SwitchTo().Window(tabs[0]);
            return openedLink;
        }

        public void click_link_with_api_call(string linkType)
        {
            switch (linkType)
            {
                case "created":
                    IWebElement elem1 = this.Driver.FindElement(By.CssSelector(CreatedLink));
                    elem1.Click();
                    break;
                case "no-content":
                    IWebElement elem2 = this.Driver.FindElement(By.CssSelector(NoContentLink));
                    elem2.Click();
                    break;
                case "moved":
                    IWebElement elem3 = this.Driver.FindElement(By.CssSelector(MovedLink));
                    elem3.Click();
                    break;
                case "bad-request":
                    IWebElement elem4 = this.Driver.FindElement(By.CssSelector(BadRequestLink));
                    elem4.Click();
                    break;
                //Click with JS cause cant click with selenium common method
                case "unauthorized":
                    click_to_element_with_js(UnathorizedLink.TrimStart('#'));
                    break;
                case "forbidden":
                    click_to_element_with_js(ForbiddenLink.TrimStart('#'));
                    break;
                case "not-found":
                    click_to_element_with_js(NotFoundLink.TrimStart('#'));
                    break;
            }
        }

        public bool check_click_res(string linkType)
        {
            IWebElement clickRes = null;
            WebDriverWait wait = new WebDriverWait(this.Driver, new TimeSpan(0, 0, 3));
            try
            {
                clickRes = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(LinkResponse)));
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            string checkingRow = clickRes.Text;
            switch (linkType)
            {
                case "created":
                    return checkingRow.Contains("Created");
                case "no-content":
                    return checkingRow.Contains("No Content");
                case "moved":
                    return checkingRow.Contains("Moved Permanently");
                case "bad-request":
                    return checkingRow.Contains("Bad Request");
                case "unauthorized":
                    return checkingRow.Contains("Unauthorized");
                case "forbidden":
                    return checkingRow.Contains("Forbidden");
                case "not-found":
                    return checkingRow.Contains("Not Found");
            }

            return false;
        }

        public void go_to_dynamic_properties_page()
        {
            IWebElement elem = this.Driver.FindElement(By.XPath(DynamicPageBtn));
            elem.Click();
        }

        public bool check_dynamic_button_clickable()
        {
            return check_element_is_clickable(DynamicEnableAfterbtn);
        }

        public string get_name_from_form_send_res()
        {
            IWebElement res = get_form_send_result();
            IWebElement resName = res.FindElement(By.Id("name"));
            string name = resName.Text.Substring(5);
            return name;
        }

    }
}