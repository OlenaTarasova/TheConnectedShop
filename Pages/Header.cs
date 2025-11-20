using Microsoft.Playwright;

using System.Threading.Tasks;

using NUnit.Framework;
 
namespace TheConnectedShop.Pages

{

    public class Header : BasePage

    {

        private readonly ILocator _logoLink;

        private readonly ILocator _logoImage;

        private readonly ILocator _phoneNumber;

        private readonly ILocator _profileIcon;

        private readonly ILocator _profileLink;

        private readonly ILocator _cartIcon;

        private readonly ILocator _cartLink;

        private readonly ILocator _searchButton;
 
       protected readonly IPage _page;
 
        public Header(IPage page) : base(page)

        {

        
              _page = page;   

            _logoLink = _page.Locator("a.header__heading-link");

            _logoImage = _page.Locator("a.header__heading-link img");

            _phoneNumber = _page.Locator("a[href^=\"tel:\"]").Nth(1);

            _profileIcon = _page.Locator("svg.icon-account").Nth(1);

            _profileLink = _page.Locator("a[href*='customer_authentication/redirect']").Nth(1);

            _cartIcon = _page.Locator("#cart-icon-bubble");

           _cartLink = _page.Locator("a[href='/cart']").Nth(0);
           
            _searchButton = _page.GetByRole(AriaRole.Button, new() { Name = "Search" });
         
          
              

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

        }
        public async Task VerifyProfileLinkVisibleAsync() // тыльки наявнысть href, пізніше добавити тести для перевірки профілю

        {

            Assert.That(await _profileLink.IsVisibleAsync(), Is.True, "Посилання профілю не відображається");

        }
 
        public async Task VerifyLogoAttributesAsync()

        {

            string href = await _logoLink.GetAttributeAsync("href");

            string src = await _logoImage.GetAttributeAsync("src");

            string alt = await _logoImage.GetAttributeAsync("alt");
 
            Assert.Multiple(() =>

            {

                Assert.That(href, Is.EqualTo("/"), "Атрибут href логотипа має бути '/'");

                Assert.That(src, Does.Contain("The_Connected_Shop_Logo"), "src логотипа не містить правильного файлу");

                Assert.That(alt, Is.EqualTo("The Connected Shop"), "Атрибут alt логотипа некоректний");

            });

        }
 
        public async Task VerifyPhoneNumberAttributesAsync()

        {

            string href = await _phoneNumber.GetAttributeAsync("href");

            string text = await _phoneNumber.InnerTextAsync();
 
            Assert.Multiple(() =>

            {

                Assert.That(href, Does.StartWith("tel:"), "href має починатись з 'tel:'");

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
 
                if (role != null)

                    Assert.That(role, Is.EqualTo("img").Or.EqualTo("presentation"), "role повинен бути 'img' або 'presentation'");

            });

        }
 
        public async Task VerifyCartIconAttributesAsync() //тут href атрибут перувфряла в Multiply , ркуа профіля провіряла окремим методом - який сосіб краащий 

        {

            string id = await _cartIcon.GetAttributeAsync("id");

            string classAttr = await _cartIcon.GetAttributeAsync("class");
            string hrefValue = await _cartLink.GetAttributeAsync("href");
 
            Assert.Multiple(() =>

            {

                Assert.That(id, Is.EqualTo("cart-icon-bubble"), "ID кошика некоректний");

                Assert.That(classAttr, Does.Contain("header__icon--cart").Or.Contain("cart-count-bubble"), 

                    "Клас кошика не відповідає очікуваному");
                Assert.That(hrefValue, Does.Contain("/cart").IgnoreCase, $"Unexpected href value: {hrefValue}");

            });

        }

        public async Task VerifySearchButtonAttributesAsync()

        {

            string type = await _searchButton.GetAttributeAsync("type");

            string ariaLabel = await _searchButton.GetAttributeAsync("aria-label");

            Assert.Multiple(() =>

            {

                Assert.That(type, Is.Null.Or.EqualTo("button"));

                Assert.That(ariaLabel, Is.EqualTo("Search").IgnoreCase);

            });

        }
        

    }

}

 