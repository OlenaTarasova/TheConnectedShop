using Microsoft.Playwright;
using System.Threading.Tasks;
using NUnit.Framework;
using TheConnectedShop.Pages;
using System.Security.Cryptography.X509Certificates;

namespace TheConnectedShop.Pages
{
    public class Header : BasePage
    {
        private readonly ILocator _logoLink;
        private readonly ILocator _logoImage;
        private readonly ILocator _phoneNumber;
        private readonly ILocator _profileIcon;
        private readonly ILocator _cartIcon;
       private readonly ILocator _searchButton;
        private readonly ILocator _title;
        protected IPage _page;
              

        public Header(IPage page) : base(page)
        {
            _logoLink = Page.Locator("a.header__heading-link");
            _logoImage = Page.Locator("a.header__heading-link img");
            _phoneNumber = Page.Locator("a.header__customer-support-region__button");
            _profileIcon = Page.Locator("svg.icon.icon-account");
            _cartIcon = Page.Locator("#cart-icon-bubble");
            _searchButton = Page.GetByRole(AriaRole.Button, new() { Name = "Search" });
            _title = Page.Locator("The Connected Shop - Smart Locks, Smart Sensors, Smart Home &amp; Office");
        }
        public async Task VerifyLogoVisibleAsync()
        {
            Assert.That(await _logoImage.IsVisibleAsync(), Is.True, "Логотип не відображається");
        }
        public async Task VerifyPhoneNumberVisibleAsync()
        {
            Assert.That(await _phoneNumber.IsVisibleAsync(), Is.True, "Телефон не відображається у хедері");
        }
        public async Task VerifyIconsVisibleAsync()
        {
            Assert.That(await _profileIcon.IsVisibleAsync(), Is.True, "Іконка профілю не відображається");
            Assert.That(await _cartIcon.IsVisibleAsync(), Is.True, "Іконка кошика не відображається");
            // Assert.That(await _searchButton.IsVisibleAsync(), Is.True, "Кнопка пошуку не відображається");
        }
        public async Task VerifyLogoAttributesAsync() //множинна перевірка всіх атрибутів
        {
            string href = await _logoLink.GetAttributeAsync("href");
            string src = await _logoImage.GetAttributeAsync("src");
            string alt = await _logoImage.GetAttributeAsync("alt");

            Assert.Multiple(() =>
            {
                Assert.That(href, Is.EqualTo("/"), "Атрибут href логотипа має бути '/'");
                Assert.That(src, Does.Contain("The_Connected_Shop_Logo"), "Атрибут src логотипа не містить правильного файлу");
                Assert.That(alt, Is.EqualTo("The Connected Shop"), "Атрибут alt логотипа некоректний");
            });
        }
        public async Task VerifyPhoneNumberAttributesAsync()
        {
            string href = await _phoneNumber.GetAttributeAsync("href");
            string text = await _phoneNumber.InnerTextAsync();
 
            Assert.Multiple(() =>
            {
                Assert.That(href, Does.StartWith("tel:"), "Атрибут href має починатись з 'tel:'");
                Assert.That(text.Trim(), Is.Not.Empty, "Телефон не має текстового значення");
            });
        }
 
        public async Task VerifyProfileIconAttributesAsync()
        {
            string role = await _profileIcon.GetAttributeAsync("role");
            string classAttr = await _profileIcon.GetAttributeAsync("class");
 
            Assert.Multiple(() =>
            {
                Assert.That(classAttr, Does.Contain("icon-account"), "Іконка профілю не має класу 'icon-account'");
                // не всі SVG мають role, тому перевіряємо обережно:
                if (role != null)
                    Assert.That(role, Is.EqualTo("img").Or.EqualTo("presentation"), " role атрибут має бути 'img' або 'presentation'");
            });
        }
 
        public async Task VerifyCartIconAttributesAsync()
        {
            string id = await _cartIcon.GetAttributeAsync("id");
            string classAttr = await _cartIcon.GetAttributeAsync("class");
 
            Assert.Multiple(() =>
            {
                Assert.That(id, Is.EqualTo("cart-icon-bubble"), " id кошика некоректний");
                Assert.That(classAttr, Does.Contain("header__icon--cart").Or.Contain("cart-count-bubble"), " клас кошика не відповідає очікуваному");
            });
        }

        public async Task VerifySearchButtonAttributesAsync()
        {
            string type = await _searchButton.GetAttributeAsync("type");
            string ariaLabel = await _searchButton.GetAttributeAsync("aria-label");

            Assert.Multiple(() =>
            {
                Assert.That(type, Is.Null.Or.EqualTo("button"), " type пошукової кнопки має бути 'button'");
                Assert.That(ariaLabel, Is.EqualTo("Search").IgnoreCase, " aria-label має бути 'Search'");
            });
        }
      
    }
}