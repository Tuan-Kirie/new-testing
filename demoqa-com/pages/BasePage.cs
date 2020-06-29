using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

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
        
        public void scroll_window_to_elem(string elem_id)
        {
            string js_script;
            js_script = String.Format("document.getElementById(\"{0}\").scrollIntoView();", elem_id);
            this.driver.ExecuteJavaScript(js_script);
        }

        public IWebElement check_element_on_DOM(string locator)
        {
            WebDriverWait wait = new WebDriverWait(this.driver, new TimeSpan(0, 0, this.timeout));
            try
            {
                IWebElement elem = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(locator)));
                return elem;
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
        }
    }
}