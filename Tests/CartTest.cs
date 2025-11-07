using NUnit.Framework;
using System.Threading.Tasks;
using TheConnectedShop.Pages;
using Microsoft.Playwright;

namespace TheConnectedShop.Tests
{

    [TestFixture]
    public class CartTest : BaseTest
    {

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

    }
}