using System;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace demoqa_com.pages
{
    public class BasePage
    {
        public IWebDriver Driver;
        public string url;
        public int timeout;
        
        //Uploading files directory
        public string UploadFilesPath
        {
            get => DriverInitializer.ProjectPath + "files\\";
        }

        public string DownLoadFilesPath
        {
            get =>  DriverInitializer.DownloadPath;
        }

        public BasePage(IWebDriver driver, string url, int timeout = 4)
        {
            Driver = driver;
            this.url = url;
            this.timeout = timeout;
        }
        
        public void open_page() {Driver.Navigate().GoToUrl(url);}
        
        public void close_driver() {Driver.Close();}

        public void scroll_window_to_elem(string elemId)
        {
            var jsScript = $"document.getElementById(\"{elemId}\").scrollIntoView();";
            Driver.ExecuteJavaScript(jsScript);
        }

        public IWebElement check_element_on_DOM(string locator)
        {
            WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, timeout));
            try
            {
                IWebElement elem = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(locator)));
                return elem;
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
        }

        public IWebElement check_element_is_visible(string locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, timeout));
                IWebElement elem = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(locator)));
                return elem;
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
        }

        public bool check_element_is_clickable(string locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 6));
                IWebElement elem = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(locator)));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        protected bool check_element_in_Dom_XPATH(string elementText)
        {
            try
            {
                string locator = $"//*[text()='{elementText}']";
                IWebElement elem = Driver.FindElement(By.XPath(locator));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void click_to_element_with_js(string locator)
        {
            string js = $"document.getElementById(\"{locator.TrimStart('#')}\").click();";
            Driver.ExecuteJavaScript(js);
        }

        public bool check_file_download(string fileName, int downloadTime=3000)
        {   
            // Await file download (3 sec auto or manually add)
            Thread.Sleep(downloadTime);
            string downloadDirectory = DownLoadFilesPath;
            string[] filePaths = Directory.GetFiles(downloadDirectory);
            foreach (string file in filePaths)
            {
                if (!file.Contains(fileName)) continue;
                FileInfo exisFile = new FileInfo(file);
                exisFile.Delete();
                return true;
            }

            return false;
        }
        
        
    }
}