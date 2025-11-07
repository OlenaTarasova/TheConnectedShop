using Microsoft.Playwright;
using System.Threading.Tasks;
using TheConnectedShop.Pages;

namespace TheConnectedShop.Tests
{
    [TestFixture]
    public class LogoTest : BaseTest    
    {
    
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
    }
}
