using System;
using NUnit.Framework;
using OpenQA.Selenium;

namespace demoqa_com.pages
{
    [TestFixture("Chrome")]
    [TestFixture("Firefox")]
    //[TestFixture("Edge")]
    public class ElementPageTest
    {
        private readonly string _browserName;
        private IWebDriver driver = null;
        private const string elements_page_url = "https://demoqa.com/elements";
        private const string text_box_url = "https://demoqa.com/text-box";
        private const string radio_btn_url = "https://demoqa.com/radio-button";
        private const string web_tables_btn_url = "https://demoqa.com/webtables";
        private const string buttons_btn_url = "https://demoqa.com/buttons";
        private const string uplodown_btn_url = "https://demoqa.com/upload-download";
        private const string links_btn_url = "https://demoqa.com/links";
        private const string dynamicProp_btn_url = "https://demoqa.com/dynamic-properties";
        private const string base_url = "https://demoqa.com/";

        public ElementPageTest(string browserName)
        {
            this._browserName = browserName;
        }
        

        [SetUp]
        public void Initialize()
        {
            DriverInitializer initializer = new DriverInitializer();
            this.driver = initializer.InitDriver(this._browserName);
        }
        
        [Test]
        public void test_guest_can_go_to_text_box_page()
        {
            ElementsPage page = new ElementsPage(driver, elements_page_url);
            page.open_page();
            page.open_test_box();
            Assert.AreEqual("https://demoqa.com/text-box", this.driver.Url, "Not Right Url");
        }

        [Test, Combinatorial]
        public void test_full_name_input_text_box_page(
            [Values("testuser", "Testuser", "@Testuser", "_Testuser")]
            string fullName)
        {
            ElementsPage page = new ElementsPage(driver, text_box_url);
            page.open_page();
            page.input_data_to_full_name(fullName);
            page.submit_form();
            IWebElement res = page.get_form_send_result();
            IWebElement resName = res.FindElement(By.Id("name"));
            string name = resName.Text.Substring(5);
            Assert.AreEqual(fullName, name, "Input names are not equal");
        }

        [Test, Sequential]
        public void test_email_input_test_box_page(
            [Values("testuser@testmail.com", "testuser@testmailcom", "testuser.com")]
            string email,
            [Values(true, false, false)] Boolean expectedResult
        )
        {
            ElementsPage page = new ElementsPage(driver, text_box_url);
            page.open_page();
            page.input_data_to_email(email);
            page.submit_form();
            bool emailRes = page.check_email_send_res(email);
            Assert.AreEqual(expectedResult, emailRes, "Input email are not equal");
        }

        [Test]
        public void test_guest_can_go_to_radio_btn_page()
        {
            ElementsPage page = new ElementsPage(driver, elements_page_url);
            page.open_page();
            page.open_radio_btn();
            Assert.AreEqual("https://demoqa.com/radio-button", this.driver.Url, "Not Right Url");
        }

        [Test, Sequential]
        public void test_guest_can_select_radio(
            [Values("Yes", "Impressive", "No")] string radioType,
            [Values(true, true, false)] Boolean expectedResult
        )
        {
            ElementsPage page = new ElementsPage(driver, radio_btn_url);
            page.open_page();
            page.click_radio(radioType);
            Boolean res = page.check_radio_select_res(radioType);
            Assert.AreEqual(expectedResult, res, "Error with radio btn selection");
        }

        [Test]
        public void test_guest_can_go_to_web_tables_page()
        {
            ElementsPage page = new ElementsPage(driver, elements_page_url);
            page.open_page();
            page.open_web_tables();
            Assert.AreEqual(web_tables_btn_url, page.driver.Url, "Not correct page");
        }

        [TestCase(new object[] {new[] {"FirstName", "LastName", "testmail@gmail.com", "11", "12222", "Legal"}})]
        public void test_guest_can_add_new_row(string[] data)
        {
            ElementsPage page = new ElementsPage(driver, web_tables_btn_url);
            page.open_page();
            page.add_new_table_row(data);
            Boolean checkStatus = page.check_new_raw_exist(data);
            Assert.AreEqual(true, checkStatus);

        }

        [TestCase(new object[] {new[] {"FirstName", "LastName", "testmail@gmail.com", "11", "12222", "Legal"}})]
        public void test_guest_can_edit_existing_row(string[] data)
        {
            ElementsPage page = new ElementsPage(driver, web_tables_btn_url);
            page.open_page();
            page.edit_existing_table(data);
            Boolean checkStatus = page.check_new_raw_exist(data);
            Assert.AreEqual(true, checkStatus);
        }

        [Test]
        public void test_guest_can_delete_row()
        {
            ElementsPage page = new ElementsPage(driver, web_tables_btn_url);
            page.open_page();
            string[] data = page.delete_existing_row();
            Boolean checkStatus = page.check_row_deleting(data);
            Assert.AreEqual(true, checkStatus);
        }

        [Test]
        public void test_guest_can_go_to_buttons_page()
        {
            ElementsPage page = new ElementsPage(driver, elements_page_url);
            page.open_page();
            page.go_to_buttons_page();
            Assert.AreEqual("https://demoqa.com/buttons", this.driver.Url, "Not right Url");
        }

        [TestCase("double")]
        [TestCase("right")]
        [TestCase("left")]
        public void test_guest_can_click_to_btns(string clickType)
        {
            ElementsPage page = new ElementsPage(driver, buttons_btn_url);
            page.open_page();
            switch (clickType)
            {
                case "double":
                    page.click_to_double_click_btn();
                    break;
                case "right":
                    page.click_to_right_click_btn();
                    break;
                case "left":
                    page.click_to_common_click_btn();
                    break;
            }

            Boolean clickRes = page.check_btn_click(clickType);
            Assert.True(clickRes);
        }

        [Test]
        public void test_guest_can_go_to_upload_download_page()
        {
            ElementsPage page = new ElementsPage(this.driver, elements_page_url);
            page.go_to_uplodown_page();
            Assert.AreEqual(uplodown_btn_url, this.driver.Url);
        }

        [Test]
        public void test_guest_can_upload_file()
        {
            ElementsPage page = new ElementsPage(this.driver, uplodown_btn_url);
            page.open_page();
            page.upload_test_file("test_file.txt");
            Boolean checkRes = page.check_file_upload("test_file.txt");
            Assert.True(checkRes);
        }

        [Test]
        public void test_guest_can_download_file()
        {
            ElementsPage page = new ElementsPage(this.driver, uplodown_btn_url);
            page.open_page();
            page.uplodown_download_file();
            Boolean checkRes = page.check_file_download("sampleFile.jpeg");
            Assert.True(checkRes);
        }

        [Test]
        public void test_guest_can_go_links_page()
        {
            ElementsPage page = new ElementsPage(this.driver, elements_page_url);
            page.open_page();
            page.go_to_links_page();
            Assert.AreEqual(links_btn_url, this.driver.Url);
        }

        [TestCase("common")]
        [TestCase("dynamic")]
        public void test_links_with_new_tab(string linkType)
        {
            ElementsPage page = new ElementsPage(this.driver, links_btn_url);
            page.open_page();
            string openedLink = page.open_link_in_new_tab(linkType);
            Assert.AreEqual(base_url, openedLink);
        }

        [Test, Sequential]
        public void test_links_with_api_call(
            [Values("created", "no-content", "moved", "bad-request", "unauthorized", "forbidden", "not-found")]
            string linkType)
        {
            ElementsPage page = new ElementsPage(this.driver, links_btn_url);
            page.open_page();
            page.click_link_with_api_call(linkType);
            bool checkRes = page.check_click_res(linkType);
            Assert.True(checkRes);
        }

        [Test]
        [Ignore("Element not clickable with standard method, this test will be fallen down")]
        public void test_guest_can_go_to_dynamic_properties_page()
        {
            ElementsPage page = new ElementsPage(this.driver, elements_page_url);
            page.open_page();
            page.go_to_dynamic_properties_page();
            Assert.AreEqual(dynamicProp_btn_url, this.driver.Url);
        }
        
        
        [Test]
        public void test_btn_are_visible()
        {
            ElementsPage page = new ElementsPage(this.driver, dynamicProp_btn_url);
            page.open_page();
            bool checkRes = page.check_dynamic_button_clickable();
            Assert.True(checkRes);
        }

        [TearDown]
        public void close_driver()
        {
            this.driver.Close();
        }
    }
}