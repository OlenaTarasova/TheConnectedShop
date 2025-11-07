using Microsoft.Playwright;

using Microsoft.Playwright.NUnit;

using NUnit.Framework;

using System;

using System.Threading.Tasks;
 
namespace TheConnectedShop

{

    [TestFixture]

    public class OpenTheConnectedShop

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

                Headless = false,

                SlowMo = 200

            });

            _page = await _browser.NewPageAsync();

            await _page.GotoAsync(BaseUrl, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });

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

            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            Assert.That(_page.Url, Does.StartWith(BaseUrl), $"Unexpected URL after opening site: {_page.Url}");
 
            var title = await _page.TitleAsync();

            Console.WriteLine($"Page title: {title}");

            Assert.That(title, Does.Contain("The Connected Shop").IgnoreCase,

                $"Unexpected page title: {title}");

        }
 
        [Test]

        public async Task CheckLogo()

        {

            var headerLink = _page.Locator("a.header__heading-link");

            Assert.That(await headerLink.CountAsync(), Is.GreaterThan(0), "Header link not found on the page");
 
            var hrefValue = await headerLink.First.GetAttributeAsync("href");

            Console.WriteLine($"Header link href: {hrefValue}");

            Assert.That(hrefValue, Does.Contain("/").IgnoreCase, $"Unexpected href value: {hrefValue}");
 
            var logoImage = _page.Locator("img[alt='The Connected Shop']");

            Assert.That(await logoImage.CountAsync(), Is.GreaterThan(0), "Logo image not found on the page");
 
            var srcValue = await logoImage.First.GetAttributeAsync("src");

            Console.WriteLine($"Logo image src: {srcValue}");

            Assert.That(srcValue, Does.Contain("The_Connected_Shop_Logo").IgnoreCase, $"Unexpected src value: {srcValue}");

        }
 
        [Test]

        public async Task CheckPhoneNumber()

        {

            var phoneNumberLocator = _page.Locator("a.header__customer-support-region__button", new() { HasText = "(305) 330-3424" });

            Assert.That(await phoneNumberLocator.CountAsync(), Is.GreaterThan(0), "Phone number not found on the page");

        }
 
        [Test]

        public async Task CheckProfileButton()

        {

            var profileButton = _page.Locator("svg.icon.icon-account");

            Assert.That(await profileButton.CountAsync(), Is.GreaterThan(0), "Profile button not found on the page");

        }
 
        [Test]

        public async Task CheckCartValue()

        {

            var cartIcon = _page.Locator("#cart-icon-bubble");

            Assert.That(await cartIcon.CountAsync(), Is.GreaterThan(0), "Cart icon not found on the page");

        }
 
        [Test]

        public async Task CheckCartClick()

        {

            var cartIcon = _page.Locator("#cart-icon-bubble");

            Assert.That(await cartIcon.CountAsync(), Is.GreaterThan(0), "Cart icon not found on the page");

            await cartIcon.First.ClickAsync();

            await _page.WaitForTimeoutAsync(1000);

        }
 
        [Test]

        public async Task CheckSearchFullNameProduct()

        {

            var searchField = _page.Locator("#Search-In-Inline");

            var placeholder = await searchField.GetAttributeAsync("placeholder");

            Assert.That(placeholder, Is.EqualTo("Search"), $"Unexpected placeholder: {placeholder}");
 
            await searchField.FillAsync("ADA Smart Door Lock");

            Assert.That(await searchField.InputValueAsync(), Is.EqualTo("ADA Smart Door Lock"));
 
            var searchButton = _page.GetByRole(AriaRole.Button, new() { Name = "Search" });

            await searchButton.ClickAsync();
 
            var result = _page.Locator("a[href*='/products/ada-smart-door-lock']");

            await result.First.WaitForAsync(new() { State = WaitForSelectorState.Visible });

            Assert.That(await result.First.IsVisibleAsync(), Is.True, "Product not found in search results");

        }
 
        [Test]

        public async Task CheckSearchPartialName()

        {

            var searchField = _page.Locator("#Search-In-Inline");

            var placeholder = await searchField.GetAttributeAsync("placeholder");

            Assert.That(placeholder, Is.EqualTo("Search"));
 
            await searchField.FillAsync("ada smart");

            var searchButton = _page.GetByRole(AriaRole.Button, new() { Name = "Search" });

            await searchButton.ClickAsync();
 
            var result = _page.Locator("a[href*='/products/ada-smart-door-lock']");

            await result.First.WaitForAsync(new() { State = WaitForSelectorState.Visible });

            Assert.That(await result.First.IsVisibleAsync(), Is.True, "Product not found for partial name search");

        }
 
        [Test]

        public async Task Search_EmptyField_ShouldRemainEmpty()

        {

            var searchField = _page.Locator("#Search-In-Inline");

            await searchField.FillAsync("");
 
            var searchButton = _page.GetByRole(AriaRole.Button, new() { Name = "Search" });

            await searchButton.ClickAsync();
 
            var inputValue = await searchField.InputValueAsync();

            Assert.That(inputValue, Is.EqualTo(""), "Search input should remain empty");

        }
 
        [Test]

        public async Task Search_NonExistentProduct_ShowsAlert()

        {

            string nonExistingProduct = "maincraft";

            var searchField = _page.Locator("#Search-In-Inline");

            await searchField.FillAsync(nonExistingProduct);
 
            var searchButton = _page.GetByRole(AriaRole.Button, new() { Name = "Search" });

            await searchButton.ClickAsync();
 
            var alertMessage = _page.Locator(".alert--warning[role='status']");

            await alertMessage.WaitForAsync(new() { State = WaitForSelectorState.Visible });
 
            var alertText = await alertMessage.InnerTextAsync();

            Console.WriteLine($"Alert message: {alertText}");
 
            Assert.That(alertText, Does.Contain($"No results found for {nonExistingProduct}").IgnoreCase,

                "No results message not displayed or incorrect");

        }

    }

}

 