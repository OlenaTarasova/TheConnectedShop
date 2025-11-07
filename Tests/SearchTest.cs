using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;
using TheConnectedShop.Pages;

namespace TheConnectedShop.Tests
{

    [TestFixture]
    public class SearchTest : BaseTest
    {

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
