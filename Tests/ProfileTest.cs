using NUnit.Framework;
using System.Threading.Tasks;
using TheConnectedShop.Pages;
using Microsoft.Playwright;

namespace TheConnectedShop.Tests
{

    [TestFixture]
    public class ProfileTest : BaseTest
    {

        [Test]
        public async Task CheckProfileButton()

        {

            var profileButton = _page.Locator("svg.icon.icon-account");

            Assert.That(await profileButton.CountAsync(), Is.GreaterThan(0), "Profile button not found on the page");

        }
    }
}