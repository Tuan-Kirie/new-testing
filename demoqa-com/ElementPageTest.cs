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
        private string url = "https://demoqa.com/elements";

        [SetUp]
        public void initChromeDriver()
        {
            this.driver = new ChromeDriver();
        }
        
        [Test]
        public void test_guest_can_go_to_text_box_page()
        {
            ElementsPage page = new  ElementsPage(driver, url);
            page.open_page();
            page.open_test_box();
            Assert.AreEqual("https://demoqa.com/text-box", this.driver.Url, "Not Right Url");
        }

        [Test, Combinatorial]
        public void test_guest_can_send_full_name(
            [Values("testuser", "Testuser", "@Testuser", "_Testuser")]
            string full_name)
        {
            ElementsPage page = new ElementsPage(driver, url);
            page.open_page();
            page.open_test_box();
            page.input_data_to_full_name(full_name);
            page.submit_form();
            IWebElement res = page.get_form_send_result();
            IWebElement res_name = res.FindElement(By.Id("name"));
            string name = res_name.Text.Substring(5);
            Assert.AreEqual(full_name, name, "Input names are not equal");
        }
        
        
        [TearDown]
        public void close_driver()
        {
            this.driver.Close();
        }
    }
}