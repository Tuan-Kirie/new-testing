using OpenQA.Selenium;

namespace demoqa_com.pages

{
    public class ElementsPage : BasePage
    {
        private string textBoxPageBtn = "//span[text()='Text Box']//ancestor::li";
        
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
    }
}