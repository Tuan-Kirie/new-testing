using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Linq;

namespace demoqa_com.pages
{
    public class BasePage
    {
        public IWebDriver driver;
        public string url;
        public int timeout;
        
        //Uploading files directory
        public string project_path = Path.GetDirectoryName(Path.GetDirectoryName(
            System.IO.Path.GetDirectoryName( 
                System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase )));
        public string UploadFilesPath
        {
            get => (project_path.Replace("\\", "/") + "/files/").Substring(6);
        }

        public string DownLoadFilesPath
        {
            get => (project_path.Replace("\\", "/") + "/download/").Substring(6);
        }
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

        public IWebElement check_element_is_clickable(string locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(this.driver, new TimeSpan(0, 0, this.timeout));
                IWebElement elem = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(locator)));
                return elem;
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
        }

        public Boolean check_element_in_Dom_XPATH(string element_text)
        {
            try
            {
                string locator = $"//*[text()='{element_text}']";
                IWebElement elem = this.driver.FindElement(By.XPath(locator));
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
            this.driver.ExecuteJavaScript(js);
        }

        public Boolean check_file_download(string file_name, int download_time=3000)
        {   
            // Await file download (3 sec auto or manually add)
            System.Threading.Thread.Sleep(download_time);
            string download_directory = DownLoadFilesPath;
            string[] filePaths = Directory.GetFiles(download_directory);
            foreach (string file in filePaths)
            {
                if (file.Contains(file_name))
                {
                    FileInfo exis_file = new FileInfo(file);
                    exis_file.Delete();
                    return true;
                }
            }

            return false;
        }

        
    }
}