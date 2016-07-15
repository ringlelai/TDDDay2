using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestClass]
    public class SeleniumTest
    {
        private IWebDriver driver;
        [TestInitialize]
        public void SetUp()
        {
            // 1. 取得一個WebDriver的Instance(FirefoxDriver)
            this.driver = new FirefoxDriver();
        }

        [TestCleanup]
        public void TearDown()
        {
            this.driver.Quit();
        }

        [TestMethod]
        public void TestGoogleSearch()
        {
            string input = "hsdc";
            string expectedResult = "www.hsdc.com.tw | Keeping Software Soft!";
            // 2. 取得網頁(www.google.com.tw)
            driver.Navigate().GoToUrl("http://www.google.com");
            // 3. 找到網頁中的元素(輸入的TextBox)
            IWebElement element = driver.FindElement(By.Id("lst-ib"));
            // 4. 填入值(hsdc)
            element.SendKeys(input);
            // 5. 根據名稱找到Button
            element = driver.FindElement(By.Name("btnK"));
            // 6. click
            element.Click();
            // 7. 等待回應
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
            // 8. 找到第一筆資料
            element = driver.FindElement(By.XPath("//*[@id=\"rso\"]/div/div[1]/div/h3/a"));
            // 9. 比對
            Assert.AreEqual(expectedResult, element.Text);
            // 10. click
            element.Click();
        }

        [TestMethod]
        public void TestJsAlert()
        {
            string expectedResult = "Hello from JavaScript!";
            driver.Navigate().GoToUrl("http://www.javascripter.net/faq/alert.htm");
            IWebElement element = driver.FindElement(By.XPath("/html/body/p[1]/table/tbody/tr/td[1]/form/input"));
            element.Click();
            IAlert alert = driver.SwitchTo().Alert();
            Assert.AreEqual(expectedResult, alert.Text);
            alert.Accept();
        }

        [TestMethod]
        public void TestAjaxAndNewWindows()
        {
            string expectedResult = "鑫概念科技有限公司";
            driver.Navigate().GoToUrl("http://www.104.com.tw");
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
            //「職務類別」Input
            IWebElement element = driver.FindElement(By.Id("ijob"));
            element.Click();
            Actions action = new Actions(driver);
            //[資訊軟體系統類]選項
            element = driver.FindElement(By.Id("e104menu2011_m_i_5"));
            //滑鼠移到[資訊軟體系統類]選項
            action.MoveToElement(element).Perform();
            //[軟體/工程類人員]選項
            action.MoveToElement(driver.FindElement(By.Id("e104menu2011_m_cb_5_0"))).Click().Perform();
            element = driver.FindElement(By.XPath("//*[@id=\"e104menu2011_box\"]/dl/dt[1]/span[1]/input"));
            element.Click();
            element = driver.FindElement(By.XPath("//*[@id=\"search_job\"]/div[4]/input"));
            element.Click();
            // 工作列表第一筆資料的職務名稱Link
            element = driver.FindElement(By.XPath("//*[@id=\"jl_page1\"]/div[1]/ul[1]/li[3]/div/a/span"));
            element.Click();
            string parentHandle = driver.CurrentWindowHandle;
            foreach (var winHandle in driver.WindowHandles)
            {
                // 切換視窗
                driver.SwitchTo().Window(winHandle);
            }
            // 找出第二個視窗中的公司名稱
            element = driver.FindElement(By.XPath("//*[@id=\"job\"]/article/header/div/h1/span/a[1]"));
            // 比對
            Assert.AreEqual(expectedResult, element.Text);
        }

        [TestMethod]
        public void TestWaitUserInput()
        {
            string expectedResult = "Thank you for submitting the form.";
            driver.Navigate().GoToUrl("http://www.snaphost.com/captcha/Demo.htm");
            IWebElement element = driver.FindElement(By.Id("Text1"));
            element.SendKeys("Test1");
            element = driver.FindElement(By.Id("DemoTextField2"));
            element.SendKeys("Test2");
            // 等待輸入驗證碼
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            // 若是出現 Home 的 Link 則代表已驗證成功
            wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Home")));
            element = driver.FindElement(By.XPath("//*[@id=\"form1\"]/div[3]/div"));
            // 比對
            Assert.AreEqual(expectedResult, element.Text);
        }
    }
}
