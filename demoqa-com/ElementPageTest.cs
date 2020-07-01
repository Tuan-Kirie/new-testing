using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace demoqa_com.pages
{
    [TestFixture]
    public class ElementPageTest
    {
        public IWebDriver driver = null;

        public ChromeOptions ChromdeDriverOptions
        {
            get
            {
                string project_path = Path.GetDirectoryName(Path.GetDirectoryName(
                    System.IO.Path.GetDirectoryName(
                        System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
                string download_path =
                    (project_path.Replace("\\", Path.DirectorySeparatorChar.ToString()) + "\\Download").Substring(6);
                ChromeOptions options = new ChromeOptions();
                options.AddUserProfilePreference("download.default_directory", download_path);
                options.AddUserProfilePreference("download.prompt_for_download", "false");
                options.AddUserProfilePreference("directory_upgrade", true);
                options.AddUserProfilePreference("safebrowsing.enabled", true);
                options.AddUserProfilePreference("safebrowsing_for_trusted_sources_enabled", false);
                return options;
            }
        }

        private string elements_page_url = "https://demoqa.com/elements";
        private string text_box_url = "https://demoqa.com/text-box";
        private string radio_btn_url = "https://demoqa.com/radio-button";
        private string web_tables_btn_url = "https://demoqa.com/webtables";
        private string buttons_btn_url = "https://demoqa.com/buttons";
        private string uplodown_btn_url = "https://demoqa.com/upload-download";
        private string links_btn_url = "https://demoqa.com/links";
        private string dynamicProp_btn_url = "https://demoqa.com/dynamic-properties";
        private string base_url = "https://demoqa.com/";

        [SetUp]
        public void initChromeDriver()
        {
            this.driver = new ChromeDriver(ChromdeDriverOptions);

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
            [Values("testuser@testmail.com", "testuser@testmailcom", "testuser.com")]
            string email,
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
        public void test_guest_can_select_radio(
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

        [TestCase(new object[] {new string[] {"FirstName", "LastName", "testmail@gmail.com", "11", "12222", "Legal"}})]
        public void test_guest_can_add_new_row(string[] data)
        {
            ElementsPage page = new ElementsPage(driver, web_tables_btn_url);
            page.open_page();
            page.add_new_table_row(data);
            Boolean check_status = page.check_new_raw_exist(data);
            Assert.AreEqual(true, check_status);

        }

        [TestCase(new object[] {new string[] {"FirstName", "LastName", "testmail@gmail.com", "11", "12222", "Legal"}})]
        public void test_guest_can_edit_existing_row(string[] data)
        {
            ElementsPage page = new ElementsPage(driver, web_tables_btn_url);
            page.open_page();
            page.edit_existing_table(data);
            Boolean check_status = page.check_new_raw_exist(data);
            Assert.AreEqual(true, check_status);
        }

        [Test]
        public void test_guest_can_delete_row()
        {
            ElementsPage page = new ElementsPage(driver, web_tables_btn_url);
            page.open_page();
            string[] data = page.delete_existing_row();
            Boolean check_status = page.check_row_deleting(data);
            Assert.AreEqual(true, check_status);
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
        public void test_guest_can_click_to_btns(string click_type)
        {
            ElementsPage page = new ElementsPage(driver, buttons_btn_url);
            page.open_page();
            switch (click_type)
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

            Boolean click_res = page.check_btn_click(click_type);
            Assert.True(click_res);
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
            Boolean check_res = page.check_file_upload("test_file.txt");
            Assert.True(check_res);
        }

        [Test]
        public void test_guest_can_download_file()
        {
            ElementsPage page = new ElementsPage(this.driver, uplodown_btn_url);
            page.open_page();
            page.uplodown_download_file();
            Boolean check_res = page.check_file_download("sampleFile.jpeg");
            Assert.True(check_res);
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
        public void test_links_with_new_tab(string link_type)
        {
            ElementsPage page = new ElementsPage(this.driver, links_btn_url);
            page.open_page();
            string opened_link = page.open_link_in_new_tab(link_type);
            Assert.AreEqual(base_url, opened_link);
        }

        [Test, Sequential]
        public void test_links_with_api_call(
            [Values("created", "no-content", "moved", "bad-request", "unauthorized", "forbidden", "not-found")]
            string link_type)
        {
            ElementsPage page = new ElementsPage(this.driver, links_btn_url);
            page.open_page();
            page.click_link_with_api_call(link_type);
            bool check_res = page.check_click_res(link_type);
            Assert.True(check_res);
        }

        //Element not cickable with standard method, this test will be fallen down
        [Test]
        public void test_guest_can_go_to_dynamic_proprties_page()
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
            bool check_res = page.check_dynamic_button_clickable();
            Assert.True(check_res);
        }

        [TearDown]
        public void close_driver()
        {
            this.driver.Close();
        }
    }
}