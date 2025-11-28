using Microsoft.Playwright;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TheConnectedShop.Pages
{
    public class SearchPage : BasePage
    {
         private readonly ILocator _searchField;
        private readonly ILocator _searchButton;

        public SearchPage(IPage page) : base(page)
        {
            _searchField = Page.Locator("#Search-In-Inline");
           // _searchButton = Page.GetByRole(AriaRole.Button, new() { Name = "Search" });
          //  _searchButton = Page.Locator("button[type='submit'][aria-label='Search']");
          _searchButton = Page.Locator("button[onclick*='Search-In-Inline']");
          

        }

        public async Task Verify_Search_VAlid_ProductAsync(string VALID_SEARCH_QUERY)
        {
            await _searchField.FillAsync(VALID_SEARCH_QUERY);
            await _searchButton.ClickAsync();
            var result = Page.Locator("a[href*='/products/ada-smart-door-lock']");

            await result.First.WaitForAsync(new() { State = WaitForSelectorState.Visible });

            Assert.That(await result.First.IsVisibleAsync(), Is.True, "Product not found in search results");
                      
                   }

        public async Task Verify_Search_Invalid_ProductAsync(string INVALID_SEARCH_QUERY)
        {
            await _searchField.FillAsync(INVALID_SEARCH_QUERY);
            await _searchButton.ClickAsync();
            var alertMessage = Page.Locator(".alert--warning[role='status']");

            await alertMessage.WaitForAsync(new() { State = WaitForSelectorState.Visible });
 
            var alertText = await alertMessage.InnerTextAsync();

            Console.WriteLine($"Alert message: {alertText}");
 
           Assert.Multiple(() =>
{
    Assert.That(alertText, Does.Contain("No results found for").IgnoreCase,
        "Не знайдено фрагмент 'No results found for'");

    Assert.That(alertText, Does.Contain(INVALID_SEARCH_QUERY).IgnoreCase,
        $"Не знайдено значення пошукового запиту: {INVALID_SEARCH_QUERY}");
});
            }
        public async Task Verify_Search_Empty_ProductAsync(string EMPTY_SEARCH_QUERY)
        {
            await _searchField.FillAsync(EMPTY_SEARCH_QUERY);
            await _searchButton.ClickAsync();
            var inputValue = await _searchField.InputValueAsync();

            Assert.That(inputValue, Is.EqualTo(""), "Search input should remain empty");
        }
        public async Task Verify_Search_Special_Chars_ProductAsync(string SPECIAL_CHARS_SEARCH_QUERY)
        {
            await _searchField.FillAsync(SPECIAL_CHARS_SEARCH_QUERY);
            await _searchButton.ClickAsync();
             var alertMessage = Page.Locator("p.alert--warning[role='status']");

     await alertMessage.WaitForAsync(new() { State = WaitForSelectorState.Visible });
 
            var alertText = await alertMessage.InnerTextAsync();

            Console.WriteLine($"Alert message: {alertText}");
 
               Assert.Multiple(() =>
{
    Assert.That(alertText, Does.Contain("No results found for").IgnoreCase,
        "Не знайдено фрагмент 'No results found for'");

    Assert.That(alertText, Does.Contain(SPECIAL_CHARS_SEARCH_QUERY).IgnoreCase,
        $"Не знайдено значення пошукового запиту: {SPECIAL_CHARS_SEARCH_QUERY}");
});
        }
    }
    }