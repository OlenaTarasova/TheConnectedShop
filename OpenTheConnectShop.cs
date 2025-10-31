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
            Assert.That(_page.Url, Does.StartWith(BaseUrl), $"Unexpected URL after opening site: {_page.Url}");

            //перевірка title
            var title = await _page.TitleAsync();
            Console.WriteLine($"Page title: {title}");
            Assert.That(title, Does.Contain("The Connected Shop - Smart Locks, Smart Sensors, Smart Home & Office")
                .IgnoreCase, $"Unexpected page title: {title}");
        }
        [Test]
        public async Task CheckLogo()
        {

            await _page.GotoAsync(BaseUrl);
            var headerLink = await _page.QuerySelectorAsync("a.header__heading-link");// пошук за локатором а...
            Assert.That(headerLink, Is.Not.Null, "Header link not found on the page");

            var hrefValue = await headerLink.GetAttributeAsync("href"); //пошук за атрибутом href
            Console.WriteLine($"Header link href: {hrefValue}");
            Assert.That(hrefValue, Is.Not.Null.And.Not.Empty, "Header link has no href attribute");
            Assert.That(hrefValue, Does.Contain("/").IgnoreCase, $"Unexpected href value: {hrefValue}");

            var logoImage = _page.Locator("img[alt='The Connected Shop']"); //пошук зображення лого
            Assert.That(headerLink, Is.Not.Null, "logo image not found on the page");

            var srcValue = await logoImage.GetAttributeAsync("src");//пошук за атрибутом scr
            Console.WriteLine($"Logo image scr: {srcValue}");
            Assert.That(srcValue, Is.Not.Null.And.Not.Empty, "Logo image has no scr attribute");
            Assert.That(srcValue, Does.Contain("The_Connected_Shop_Logo").IgnoreCase, $"Unexpected scr value: {srcValue}");

        }
        // зрозуміти різницю між _page.QuerySelectorAsync and _page.Locator
        [Test]
        public async Task CheckPhoneNumber()
        {
            await _page.GotoAsync(BaseUrl);
            var PhoneNumberValue = _page.Locator("a[href='tel:(305) 330-3424']"); //пошук якщо номер телефону не змінний
            Assert.That(PhoneNumberValue, Is.Not.Null, "Phone number not found on the page");
            /* 2 результаты за тегом document.querySelectorAll('a .header__customer-support-region__global-info');
NodeList(2) [span.header__customer-support-region__global-info, span.header__customer-support-region__global-info] */
            var phoneNumberLocator = _page.Locator("a.header__customer-support-region__button", new() { HasText = "(305) 330-3424" });
            Assert.That(phoneNumberLocator, Is.Not.Null, "Phone number is not found on the page");

        }
        [Test] //Профіль ( кнопка)
        public async Task CheckProfileButton()
        {
            await _page.GotoAsync(BaseUrl);
            var ProfilyButtonValue = _page.Locator("svg.icon.icon-account").First;
            Assert.That(ProfilyButtonValue, Is.Not.Null, "Profile value is not found on the page");

        }

        [Test] //корзина
        public async Task CheckCartVulue()
        {
            await _page.GotoAsync(BaseUrl);
            var CartValue = _page.Locator("#cart-icon-bubble");
            Assert.That(CartValue, Is.Not.Null, "Cart value is not found on the page");
            
        }

                       }
}
