using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestClass]
    public class OnlineOrderUitTest
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
        public void TestQueryMemberOrder()
        {
            // 2. 取得網頁
            driver.Navigate().GoToUrl("http://localhost:49358/Orders/QueryMemberOrder");
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnQuery")));
            IWebElement submit = driver.FindElement(By.Id("btnQuery"));
            submit.Click();
            // 若是出現 查詢明細 的 Link 則代表Ajax Finished
            wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("查詢明細")));
            // 比對查詢訂單
            IWebElement data = null;
            data = driver.FindElement(By
                    .XPath("//*[@id=\"orderList\"]/div/table/tbody/tr[1]/td[1]"));
            Assert.AreEqual("ORD0001", data.Text.Trim());
            data = driver.FindElement(By
                    .XPath("//*[@id=\"orderList\"]/div/table/tbody/tr[2]/td[1]"));
            Assert.AreEqual("ORD0011", data.Text.Trim());
            data = driver.FindElement(By
                    .XPath("//*[@id=\"orderList\"]/div/table/tbody/tr[3]/td[1]"));
            Assert.AreEqual("ORD0033", data.Text.Trim());
            data = driver.FindElement(By
                    .XPath("//*[@id=\"orderList\"]/div/table/tbody/tr[4]/td[1]"));
            Assert.AreEqual("ORD0034", data.Text.Trim());
            data = driver.FindElement(By
                    .XPath("//*[@id=\"orderList\"]/div/table/tbody/tr[5]/td[1]"));
            Assert.AreEqual("ORD0101", data.Text.Trim());
            IWebElement link = driver.FindElement(By
                    .XPath("//*[@id=\"orderList\"]/div/table/tbody/tr[3]/td[4]/a"));
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
            link.Click();
            data = driver.FindElement(By.Id("orderId"));
            Assert.AreEqual("ORD0033", data.Text.Trim());
            data = driver.FindElement(By.Id("totalAmount"));
            Assert.AreEqual("總計價格：650", data.Text.Trim());
            data = driver
                    .FindElement(By
                            .XPath("/html/body/div[2]/div/div/div/div/table/tbody/tr[1]/td[1]"));
            Assert.AreEqual("UML團隊流程開發與管理", data.Text.Trim());
            data = driver
                    .FindElement(By
                            .XPath("/html/body/div[2]/div/div/div/div/table/tbody/tr[2]/td[1]"));
            Assert.AreEqual("數讀5", data.Text.Trim());
        }
    }
}
