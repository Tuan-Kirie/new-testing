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
        
        private static ChromeOptions ConfigureOptionsChrome(string browserName) {
            ChromeOptions chromeOption = new ChromeOptions();
            chromeOption.AddUserProfilePreference("download.default_directory", DownloadPath);
            chromeOption.AddUserProfilePreference("download.prompt_for_download", "false");
            chromeOption.AddUserProfilePreference("directory_upgrade", true);
            chromeOption.AddUserProfilePreference("safebrowsing.enabled", true);
            chromeOption.AddUserProfilePreference("safebrowsing_for_trusted_sources_enabled", false);
            return chromeOption;
        }
        
        public IWebDriver InitDriver(string browserName)
        {
            switch (browserName)
            {
                case "Chrome":
                    driver = new ChromeDriver(ConfigureOptionsChrome(browserName));
                    return driver;
                case "Firefox":
                    driver = new FirefoxDriver();
                    return driver;
                case "Edge":
                    driver = new EdgeDriver();
                    return driver;
            }
            return null;
        }
        
    }
}