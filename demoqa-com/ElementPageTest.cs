using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace demoqa_com.pages
{
    [TestFixture]
    public class ElementPageTest
    {
        public IWebDriver driver = null;
        private string elements_page_url = "https://demoqa.com/elements";
        private string text_box_url = "https://demoqa.com/text-box";
        private string radio_btn_url = "https://demoqa.com/radio-button";
        private string web_tables_btn_url = "https://demoqa.com/webtables";
        
        [SetUp]
        public void initChromeDriver()
        {
            this.driver = new ChromeDriver();
        }
        
        [Test]
        public void test_guest_can_go_to_text_box_page()
        {
            ElementsPage page = new  ElementsPage(driver, elements_page_url);
            page.open_page();
            page.open_test_box();
            Assert.AreEqual("https://demoqa.com/text-box", this.driver.Url, "Not Right Url");
        }

        [Test, Combinatorial]
        public void test_full_name_input_text_box_page(
            [Values("testuser", "Testuser", "@Testuser", "_Testuser")]
            string full_name)
        {
            ElementsPage page = new ElementsPage(driver, text_box_url);
            page.open_page();
            page.input_data_to_full_name(full_name);
            page.submit_form();
            IWebElement res = page.get_form_send_result();
            IWebElement res_name = res.FindElement(By.Id("name"));
            string name = res_name.Text.Substring(5);
            Assert.AreEqual(full_name, name, "Input names are not equal");
        }

        [Test, Sequential]
        public void test_email_input_test_box_page(
            [Values("testuser@testmail.com", "testuser@testmailcom", "testuser.com")] string email,
            [Values(true, false, false)] Boolean expected_result
            )
        {
            ElementsPage page = new ElementsPage(driver, text_box_url);
            page.open_page();
            page.input_data_to_email(email);
            page.submit_form();
            bool email_res = page.check_email_send_res(email);
            Assert.AreEqual(expected_result, email_res, "Input email are not equal");
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
        public void test_guest_can_select_yes_radio(
            [Values("Yes", "Impressive", "No")] string radio_type,
            [Values(true, true, false)] Boolean expected_result
            )
        {
            ElementsPage page = new ElementsPage(driver, radio_btn_url);
            page.open_page();
            page.click_radio(radio_type);
            Boolean res = page.check_radio_select_res(radio_type);
            Assert.AreEqual(expected_result, res, "Error with radio btn selection");
        }

        [Test]
        public void test_guest_can_go_to_web_tables_page()
        {
            ElementsPage page = new ElementsPage(driver, elements_page_url);
            page.open_page();
            page.open_web_tables();
            Assert.AreEqual(web_tables_btn_url, page.driver.Url, "Not correct page");
        }

        [TestCase(new object[] {new string[] {"FirstName", "LastName", "11", "testmail@gmail.com", "12222", "Legal"}})]
        public void test_guest_can_add_new_row(string[] data)
        {
            ElementsPage page = new ElementsPage(driver, web_tables_btn_url);
            page.open_page();
            page.add_new_table_row(data);
            
        }
        
        [TearDown]
        public void close_driver()
        {
            this.driver.Close();
        }
    }
}