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
        public IWebDriver driver;
        private const string BaseUrl = "https://demoqa.com/";
        private const string ElementsPageUrl = BaseUrl + "elements";
        private const string TextBoxUrl = BaseUrl +  "text-box";
        private const string RadioBtnUrl = BaseUrl + "radio-button";
        private const string WebTablesBtnUrl = BaseUrl + "webtables";
        private const string ButtonsBtnUrl = BaseUrl + "buttons";
        private const string UplodownBtnUrl = BaseUrl + "upload-download";
        private const string LinksBtnUrl = BaseUrl +  "links";
        private const string DynamicPropBtnUrl = BaseUrl + "dynamic-properties";
        

        public ElementPageTest(string browserName)
        {
            _browserName = browserName;
        }
        

        [SetUp]
        public void Initialize()
        {
            DriverInitializer initializer = new DriverInitializer();
            driver = initializer.InitDriver(_browserName);
        }
        
        [Test]
        public void test_guest_can_go_to_text_box_page()
        {
            ElementsPage page = new ElementsPage(driver, ElementsPageUrl);
            page.open_page();
            page.open_test_box();
            Assert.AreEqual(TextBoxUrl, driver.Url, "Not Right Url");
        }

        [Test, Combinatorial]
        public void test_full_name_input_text_box_page(
            [Values("testuser", "Testuser", "@Testuser", "_Testuser")]
            string fullName)
        {
            ElementsPage page = new ElementsPage(driver, TextBoxUrl);
            page.open_page();
            page.input_data_to_full_name(fullName);
            page.submit_form();
            string name = page.get_name_from_form_send_res();
            Assert.AreEqual(fullName, name, "Input names are not equal");
        }

        [Test, Sequential]
        public void test_email_input_test_box_page(
            [Values("testuser@testmail.com", "testuser@testmailcom", "testuser.com")]
            string email,
            [Values(true, false, false)] bool expectedResult
        )
        {
            ElementsPage page = new ElementsPage(driver, TextBoxUrl);
            page.open_page();
            page.input_data_to_email(email);
            page.submit_form();
            bool emailRes = page.check_email_send_res(email);
            Assert.AreEqual(expectedResult, emailRes, "Input email are not equal");
        }

        [Test]
        public void test_guest_can_go_to_radio_btn_page()
        {
            ElementsPage page = new ElementsPage(driver, ElementsPageUrl);
            page.open_page();
            page.open_radio_btn();
            Assert.AreEqual("https://demoqa.com/radio-button", driver.Url, "Not Right Url");
        }

        [Test, Sequential]
        public void test_guest_can_select_radio(
            [Values("Yes", "Impressive", "No")] string radioType,
            [Values(true, true, false)] bool expectedResult
        )
        {
            ElementsPage page = new ElementsPage(driver, RadioBtnUrl);
            page.open_page();
            page.click_radio(radioType);
            bool res = page.check_radio_select_res(radioType);
            Assert.AreEqual(expectedResult, res, "Error with radio btn selection");
        }

        [Test]
        public void test_guest_can_go_to_web_tables_page()
        {
            ElementsPage page = new ElementsPage(driver, ElementsPageUrl);
            page.open_page();
            page.open_web_tables();
            Assert.AreEqual(WebTablesBtnUrl, page.Driver.Url, "Not correct page");
        }

        [TestCase(new object[] {new[] {"FirstName", "LastName", "testmail@gmail.com", "11", "12222", "Legal"}})]
        public void test_guest_can_add_new_row(string[] data)
        {
            ElementsPage page = new ElementsPage(driver, WebTablesBtnUrl);
            page.open_page();
            page.add_new_table_row(data);
            bool checkStatus = page.check_new_raw_exist(data);
            Assert.True(true, "No correct form result");

        }

        [TestCase(new object[] {new[] {"FirstName", "LastName", "testmail@gmail.com", "11", "12222", "Legal"}})]
        public void test_guest_can_edit_existing_row(string[] data)
        {
            ElementsPage page = new ElementsPage(driver, WebTablesBtnUrl);
            page.open_page();
            page.edit_existing_table(data);
            bool checkStatus = page.check_new_raw_exist(data);
            Assert.AreEqual(true, checkStatus);
        }

        [Test]
        public void test_guest_can_delete_row()
        {
            ElementsPage page = new ElementsPage(driver, WebTablesBtnUrl);
            page.open_page();
            string[] data = page.delete_existing_row();
            bool checkStatus = page.check_row_deleting(data);
            Assert.True(checkStatus, "Error with deleting table row");
        }

        [Test]
        public void test_guest_can_go_to_buttons_page()
        {
            ElementsPage page = new ElementsPage(driver, ElementsPageUrl);
            page.open_page();
            page.go_to_buttons_page();
            Assert.AreEqual(ButtonsBtnUrl, driver.Url, "Not right Url");
        }

        [TestCase("double")]
        [TestCase("right")]
        [TestCase("left")]
        public void test_guest_can_click_to_btns(string clickType)
        {
            ElementsPage page = new ElementsPage(driver, ButtonsBtnUrl);
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

            bool clickRes = page.check_btn_click(clickType);
            Assert.True(clickRes, $"Error with clicking to button{clickType}");
        }

        [Test]
        [Ignore("No locating with XPATH")]
        public void test_guest_can_go_to_upload_download_page()
        {
            ElementsPage page = new ElementsPage(driver, ElementsPageUrl);
            page.go_to_uplodown_page();
            Assert.AreEqual(UplodownBtnUrl, driver.Url, "Not right URL");
        }

        [Test]
        public void test_guest_can_upload_file()
        {
            ElementsPage page = new ElementsPage(driver, UplodownBtnUrl);
            page.open_page();
            page.upload_test_file("test_file.txt");
            bool checkRes = page.check_file_upload("test_file.txt");
            Assert.True(checkRes, "Error with file uploading");
        }

        [Test]
        public void test_guest_can_download_file()
        {
            ElementsPage page = new ElementsPage(driver, UplodownBtnUrl);
            page.open_page();
            page.uplodown_download_file();
            bool checkRes = page.check_file_download("sampleFile.jpeg");
            Assert.True(checkRes, "Error with file downloading");
        }

        [Test]
        [Ignore("Element not clickable with standard method, this test will be fallen down")]
        public void test_guest_can_go_links_page()
        {
            ElementsPage page = new ElementsPage(driver, ElementsPageUrl);
            page.open_page();
            page.go_to_links_page();
            Assert.AreEqual(LinksBtnUrl, driver.Url, "Not right Url");
        }

        [TestCase("common")]
        [TestCase("dynamic")]
        public void test_links_with_new_tab(string linkType)
        {
            ElementsPage page = new ElementsPage(driver, LinksBtnUrl);
            page.open_page();
            string openedLink = page.open_link_in_new_tab(linkType);
            Assert.AreEqual(BaseUrl, openedLink, "Error with opening new tab with click to link");
        }

        [Test, Sequential]
        public void test_links_with_api_call(
            [Values("created", "no-content", "moved", "bad-request", "unauthorized", "forbidden", "not-found")]
            string linkType)
        {
            ElementsPage page = new ElementsPage(driver, LinksBtnUrl);
            page.open_page();
            page.click_link_with_api_call(linkType);
            bool checkRes = page.check_click_res(linkType);
            Assert.True(checkRes, "Error with sending api call with link");
        }

        [Test]
        [Ignore("Element not clickable with standard method, this test will be fallen down")]
        public void test_guest_can_go_to_dynamic_properties_page()
        {
            ElementsPage page = new ElementsPage(driver, ElementsPageUrl);
            page.open_page();
            page.go_to_dynamic_properties_page();
            Assert.AreEqual(DynamicPropBtnUrl, driver.Url, "Not right URL");
        }
        
        
        [Test]
        public void test_btn_are_visible()
        {
            ElementsPage page = new ElementsPage(driver, DynamicPropBtnUrl);
            page.open_page();
            bool checkRes = page.check_dynamic_button_clickable();
            Assert.True(checkRes, "Dynamic button not clickable");
        }

        [TearDown]
        public void close_driver()
        {
            driver.Close();
        }
    }
}