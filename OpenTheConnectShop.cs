using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
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
            var phoneNumberValue = _page.Locator("a[href='tel:(305) 330-3424']"); //пошук якщо номер телефону не змінний
            Assert.That(await phoneNumberValue.CountAsync(), Is.GreaterThan(0), "Phone number not found on the page");
            /* 2 результаты за тегом document.querySelectorAll('a .header__customer-support-region__global-info');
NodeList(2) [span.header__customer-support-region__global-info, span.header__customer-support-region__global-info] */
            var phoneNumberLocator = _page.Locator("a.header__customer-support-region__button", new() { HasText = "(305) 330-3424" });
            Assert.That(phoneNumberLocator, Is.Not.Null, "Phone number is not found on the page");
            // /var phoneNumberLocator = _page.Locator("span.header__customer-support-region__global-info >> span.font-body-bold");


            //     await Expect(phoneNumberLocator).ToBeVisibleAsync();


            //     var phoneText = await phoneNumberLocator.InnerTextAsync();
            //     Console.WriteLine($"Phone number text: {phoneText}");


            //     Assert.That(phoneText.Trim()(прибирає всі додаткові символи), Is.EqualTo("(305) 330-3424"), "Phone number text does not match expected value");
        }

        [Test] //Профіль ( кнопка)
        public async Task CheckProfileButton()
        {
            await _page.GotoAsync(BaseUrl);
            var profileButtonValue = _page.Locator("svg.icon.icon-account").First;
            // Assert.That(ProfileButtonValue, Is.Not.Null, "Profile value is not found on the page");
            Assert.That(await profileButtonValue.CountAsync(), Is.GreaterThan(0)); //цей варыант кращий

        }

        [Test] //корзина
        public async Task CheckCartVulue()
        {
            await _page.GotoAsync(BaseUrl);
            var CartValue = _page.Locator("#cart-icon-bubble");
            Assert.That(CartValue, Is.Not.Null, "Cart value is not found on the page");

        }

        [Test] //корзина клік
        public async Task CheckCartClick()
        {
            await _page.GotoAsync(BaseUrl);
            var CartValue = _page.Locator("#cart-icon-bubble").ClickAsync;
            Assert.That(CartValue, Is.Not.Null, "Cart value is not found on the page");

        }

        [Test]
        public async Task CheckSearchFullNameProduct()
        {
            await _page.GotoAsync(BaseUrl);
            var searchvalue = _page.Locator("#Search-In-Inline");
            var placeholderSearch = await searchvalue.GetAttributeAsync("placeholder");
            Assert.That(placeholderSearch, Is.EqualTo("Search"), $"error:{placeholderSearch}");
            await searchvalue.FillAsync("ADA Smart Door Lock");// просимо заповнити
            var inputSearchValue = await searchvalue.InputValueAsync(); //витягуємо значення
            Assert.That(inputSearchValue, Is.EqualTo("ADA Smart Door Lock"), "ADA Smart Door Lock product not found"); // порівнюємо 
            var searchButton = _page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Search" });// чи можна просто через .Click();? як краще?
            await searchButton.ClickAsync();
            await _page.WaitForSelectorAsync("text=ADA Smart Door Lock");// чекаэм на результати пошуку
            var resultFirstSearch = _page.Locator("a[href*='/products/ada-smart-door-lock']");// шукаєм карту продукту
            Assert.That(await resultFirstSearch.IsVisibleAsync(), Is.True, "Product not found in search results");
            await resultFirstSearch.ClickAsync(); //треба чи ні? хз

        }
        [Test]
        public async Task CheckSearchOnePartOfNAme()//не повна назва та нижній регістр

        {
            await _page.GotoAsync(BaseUrl);
            var searchvalue = _page.Locator("#Search-In-Inline");
            var placeholderSearch = await searchvalue.GetAttributeAsync("placeholder");
            Assert.That(placeholderSearch, Is.EqualTo("Search"), $"error:{placeholderSearch}");
            await searchvalue.FillAsync("ada smart");
            var inputSearchValue = await searchvalue.InputValueAsync();
            Assert.That(inputSearchValue, Is.EqualTo("ada smart"), "ada smart not found");
            var searchButton = _page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Search" });
            await searchButton.ClickAsync();
            await _page.WaitForSelectorAsync("text=ada smart");// чекаэм на результати пошуку
            var resultFirstSearch = _page.Locator("a[href*='/products/ada-smart-door-lock']");// шукаєм карту продукту
            Assert.That(await resultFirstSearch.IsVisibleAsync(), Is.True, "Product not found in search results");
            await resultFirstSearch.ClickAsync();
        }

        [Test] //негативний
        public async Task Search_WithEmptyFild()
        {
            await _page.GotoAsync(BaseUrl);
            var searchvalue = _page.Locator("#Search-In-Inline");
            var placeholderSearch = await searchvalue.GetAttributeAsync("placeholder");
            Assert.That(placeholderSearch, Is.EqualTo("Search"), $"error:{placeholderSearch}");
            await searchvalue.FillAsync("");
            var searchButton = _page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Search" });
            await searchButton.ClickAsync();
            var inputValue = await searchvalue.InputValueAsync();// яку перевірку поставити?
            Assert.That(inputValue, Is.EqualTo(""), "Search input should remain empty");

        }
        [Test]//перевірка неіснуючого товару
        public async Task Search_non_ExistentProduct_AlertMessage()
        {
            await _page.GotoAsync(BaseUrl);
            var searchvalue = _page.Locator("#Search-In-Inline");
            var placeholderSearch = await searchvalue.GetAttributeAsync("placeholder");
            Assert.That(placeholderSearch, Is.EqualTo("Search"), $"error:{placeholderSearch}");
            string nonExistingProduct = "maincraft";//щоб можна було добавити різні слова для перевірки і перевірити введено слово в повідомлені
            await searchvalue.FillAsync(nonExistingProduct);
            var searchButton = _page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Search" });
            await searchButton.ClickAsync();
            await _page.WaitForSelectorAsync("text=maincraft");
            var alertMessage = _page.Locator("p[role='status'].alert--warning");
            await alertMessage.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });//чекаємо поки появиться повідомлення
            var alertMessageText = await alertMessage.InnerTextAsync();
            Assert.That(alertMessageText, Does.Contain($"No results found for {nonExistingProduct}. Check the spelling or use a different word or phrase"), " No results message not displayed or incorrect");
        }
    }
}

