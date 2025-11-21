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
            _searchButton = Page.GetByRole(AriaRole.Button, new() { Name = "Search" });
        }

        public async Task Verify_Search_VAlid_ProductAsync(string VALID_SEARCH_QUERY)
        {
            await _searchField.FillAsync(VALID_SEARCH_QUERY);
                      
                   }
        public async Task Verify_Search_Invalid_ProductAsync(string INVALID_SEARCH_QUERY)
        {
            await _searchField.FillAsync(INVALID_SEARCH_QUERY);
            }
        public async Task Verify_Search_Empty_ProductAsync(string EMPTY_SEARCH_QUERY)
        {
            await _searchField.FillAsync(EMPTY_SEARCH_QUERY);
        }
        public async Task Verify_Search_Special_Chars_ProductAsync(string SPECIAL_CHARS_SEARCH_QUERY)
        {
            await _searchField.FillAsync(SPECIAL_CHARS_SEARCH_QUERY);
        }
    }
    }