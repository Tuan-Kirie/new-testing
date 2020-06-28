using OpenQA.Selenium;

namespace demoqa_com.pages
{
    public class BasePage
    {
        public IWebDriver driver;
        public string url;
        public int timeout;

        public BasePage(IWebDriver driver, string url, int timeout = 4)
        {
            this.driver = driver;
            this.url = url;
            this.timeout = timeout;
        }
        
        public void open_page() {this.driver.Navigate().GoToUrl(this.url);}
        
        public void close_driver() {this.driver.Close();}
    }
}