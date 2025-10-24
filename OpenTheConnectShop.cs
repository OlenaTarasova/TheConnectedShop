using System.ComponentModel.Design;
using Microsoft.Playwright;

namespace TheConnectShop
{

    [TestFixture]
    public class OpenTheConnectShop
    {

    private IPlaywright _playwright;
    private IBrowser _browser;
    private IPage _page;
        private const string BaseUrl = "https://theconnectedshop.com/";
        [SetUp]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            _page = await _browser.NewPageAsync();

        }
        [TearDown]
        public async Task Teardown()
        {

            await _browser.CloseAsync();
            _playwright.Dispose();
        }
        [Test]
        public async Task OpenSite()
        {

            await _page.GotoAsync(BaseUrl);
            Assert.That(_page.Url, Does.StartWith(BaseUrl),$"Unexpected URL after opening site: {_page.Url}");
        }  
                       }
}
