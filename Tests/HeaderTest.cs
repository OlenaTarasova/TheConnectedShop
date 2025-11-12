using Microsoft.Playwright;
using System.Threading.Tasks;
using TheConnectedShop.Pages;
using Header = TheConnectedShop.Pages.Header;

namespace TheConnectedShop.Tests

{
   
    [TestFixture]
    public class HeaderTest
    {
        protected IPlaywright _playwright;
        protected IBrowser _browser;
        protected IPage _page;
        private HomePage _homePage;
        private Header _header;
        [SetUp]

        public async Task Setup()
        {

            _playwright = await Playwright.CreateAsync();

            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions

            {

                Headless = false,

                SlowMo = 200

            });
            _homePage = new HomePage(_page);

            _header = new Header(_page);

            _page = await _browser.NewPageAsync();
            await _homePage.OpenAsync();

        }

        [TearDown]

        public async Task Teardown()

        {

            await _browser.CloseAsync();

            _playwright.Dispose();

        }

        [Test]

        [Category("UI")]

        [Description("Перевірка наявності елементів та атрибутів у Header")]

        public async Task Verify_Header_Elements_And_Attributes()

        {
            await _homePage.VerifyHomePageAsync();

            await _header.VerifyIconsVisibleAsync();

            await _header.VerifyLogoAttributesAsync();

            await _header.VerifyLogoVisibleAsync();

            await _header.VerifyPhoneNumberVisibleAsync();

            await _header.VerifyPhoneNumberAttributesAsync();

            await _header.VerifyProfileIconAttributesAsync();

            await _header.VerifyCartIconAttributesAsync();

            await _header.VerifySearchButtonAttributesAsync();


        }
                 
 
    }
   
 

}