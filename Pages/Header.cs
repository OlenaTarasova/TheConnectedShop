using Microsoft.Playwright;
using System.Threading.Tasks;
using NUnit.Framework;
using TheConnectedShop.Pages;

namespace Locator_Header
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
    }
}