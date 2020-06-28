using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace demoqa_com.pages
{
    public class ElementPageTest
    {
        IWebDriver driver = new ChromeDriver();
        private string url = "https://demoqa.com/elements";
            
        [Test]
        public void test_guest_can_go_to_text_box_page()
        {
            ElementsPage page = new  ElementsPage(driver, url);
            page.open_page();
            page.open_test_box();
            Assert.AreEqual("https://demoqa.com/text-box", this.driver.Url, "Not Right Url");
        }

        [TearDown]
        public void close_driver()
        {
            this.driver.Close();
        }
    }
}