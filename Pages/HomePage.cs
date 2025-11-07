using Microsoft.Playwright;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TheConnectedShop.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IPage page) : base(page) { }


        public async Task OpenAsync() => await NavigateAsync();

        private const string ExpectedTitle = "The Connected Shop - Smart Locks, Smart Sensors, Smart Home &amp; Office";
        private const string ExpectedUrl = "https://theconnectedshop.com/";
       [Test]
          public async Task VerifyHomePageAsync()
        {
            // Перевірка URL
            var currentUrl = Page.Url;
            Assert.That(currentUrl, Is.EqualTo(ExpectedUrl), $"Incorrect URL. Expected: {ExpectedUrl}, Actual: {currentUrl}");

            // Перевірка Title
            var pageTitle = await Page.TitleAsync();
            Assert.That(pageTitle, Is.EqualTo(ExpectedTitle), $"Incorrect page title. Expected: {ExpectedTitle}, Actual: {pageTitle}");
        }

       //д/з перевірка title. corectURL - done
    }
}

