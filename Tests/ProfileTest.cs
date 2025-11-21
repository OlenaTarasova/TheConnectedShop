using Microsoft.Playwright;
using System.Threading.Tasks;
using TheConnectedShop.Pages;
using Header = TheConnectedShop.Pages.Header;

namespace TheConnectedShop.Tests

{
   
    [TestFixture]
    public class ProfileTest
    {
        protected IPlaywright _playwright;
        protected IBrowser _browser;
        protected IPage _page;
        private HomePage _homePage;
        private ProfilePage _profilePage;
        [SetUp]

        public async Task Setup()
        {

            _playwright = await Playwright.CreateAsync();

            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions

            {

                Headless = false,

                SlowMo = 200

            });
            _page = await _browser.NewPageAsync();
            _homePage = new HomePage(_page);

            _profilePage = new ProfilePage(_page);

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

        [Description("Перевірка наявності елементів та атрибутів у Profile Sign In")]
        public async Task VerifyProfileSignInAsync()
        {
            await _homePage.VerifyHomePageAsync();
          await _profilePage.Verify_Click_Profile_IconAsync();
          //await _profilePage.Verify_SignIn_With_EmailAsync("(string email)");
          await _profilePage.Verify_Sign_In_To_ShopAsync("string email)");
        }
    }
}