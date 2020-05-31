using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace seleniumInstagramFollowers
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.instagram.com");
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);

            IWebElement userName = driver.FindElement(By.Name("username"));
            IWebElement password = driver.FindElement(By.Name("password"));
            IWebElement loginBtn = driver.FindElement(By.CssSelector(".sqdOP.L3NKy.y3zKF"));

            bilgiler bilgi = new bilgiler();
            userName.SendKeys(bilgi.kullaniciAdi());
            password.SendKeys(bilgi.sifre());
            loginBtn.Click();
            Console.WriteLine("Hesap Bilgileri Girildi!");
            Thread.Sleep(2500);

            driver.Navigate().GoToUrl($"https://www.instagram.com/{bilgi.kullaniciAdi()}");
            Console.WriteLine("Profile Yönlendirildi!");
            Thread.Sleep(2500);

            IWebElement followerLink = driver.FindElement(By.CssSelector("#react-root > section > main > div > header > section > ul > li:nth-child(2) > a"));
            followerLink.Click();
            Thread.Sleep(2500);


            //ScrollDown Start
            //isgrP
            string jsCommand = "" +
                "sayfa = document.querySelector('.isgrP');" +
                "sayfa.scrollTo(0,sayfa.scrollHeight);" +
                "var sayfaSonu = sayfa.scrollHeight;" +
                "return sayfaSonu;";

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            var sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand));

            while (true)
            {
                var son = sayfaSonu;
                Thread.Sleep(750);
                sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand));
                if (son == sayfaSonu)
                    break;
            }

            //ScrollDown End


            //Takipçi Listeleme Start

            int sayac = 1;
            IReadOnlyCollection<IWebElement> follwers = driver.FindElements(By.CssSelector(".FPmhX.notranslate._0imsa"));
            foreach (IWebElement follower in follwers)
            {
                Console.WriteLine(sayac + " ==> " + follower.Text);
                sayac++;
            }

            //Takipçi Listeleme END

        }
    }
}
