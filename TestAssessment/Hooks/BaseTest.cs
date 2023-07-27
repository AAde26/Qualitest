using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using System.Reflection;
using TechTalk.SpecFlow;

namespace TestAssessment.Hooks
{
    [Binding]
    public class BaseTest
    {
           public static IWebDriver driver;
        [BeforeScenario]
        public void BeforeScenario()
        {
                driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)); 
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000);
        }

        

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();
            Kill("chromedriver");
        }
        public static void Kill(string processName) 
        {
            try
            {
                Process[] runningProcesses = Process.GetProcesses();
                foreach (var process in runningProcesses)
                {
                    if (process.ProcessName.Contains(processName))
                    {
                        process.Kill();
                    }

                }
           
           }
            catch(Exception) {
            }
        }
    }
}