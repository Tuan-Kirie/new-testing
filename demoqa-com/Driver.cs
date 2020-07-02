using System;
using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace demoqa_com.pages
{
    public class DriverInitializer
    {
        private static IWebDriver driver;
        private static string ProjectPath
        {
            get
            {
                DirectoryInfo currentDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                string projectDirectory = currentDirectory.Parent.Parent.FullName + "\\";
                return projectDirectory;
            }
        }

        private static string DownloadPath
        {
            get => ProjectPath + "Download";
        }
        
        private static ChromeOptions ConfigureOptionsChrome() {
            ChromeOptions chromeOption = new ChromeOptions();
            chromeOption.AddUserProfilePreference("download.default_directory", DownloadPath);
            chromeOption.AddUserProfilePreference("download.prompt_for_download", "false");
            chromeOption.AddUserProfilePreference("directory_upgrade", true);
            chromeOption.AddUserProfilePreference("safebrowsing.enabled", true);
            chromeOption.AddUserProfilePreference("safebrowsing_for_trusted_sources_enabled", false);
            return chromeOption;
        }

        private static FirefoxOptions ConfigureOptionsFirefox()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.SetPreference("browser.download.dir", DownloadPath);
            options.SetPreference("browser.download.folderList", 2);
            options.SetPreference("browser.download.useDownloadDir", true);
            options.SetPreference("browser.helperApps.alwaysAsk.force", false);
            options.SetPreference("browser.download.manager.showWhenStarting",false);
            options.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/zip,application/octet-stream,image/jpeg,application/vnd.ms-outlook,text/html,application/pdf");
            return options;
        }
        
        public IWebDriver InitDriver(string browserName)
        {
            switch (browserName)
            {
                case "Chrome":
                    driver = new ChromeDriver(ConfigureOptionsChrome());
                    return driver;
                case "Firefox":
                    driver = new FirefoxDriver(ConfigureOptionsFirefox());
                    return driver;
                case "Edge":
                    driver = new EdgeDriver();
                    return driver;
            }
            return null;
        }
        
    }
}