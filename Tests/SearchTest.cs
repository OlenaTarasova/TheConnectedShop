using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;
using TheConnectedShop.Pages;
using System.Text.Json;


namespace TheConnectedShop.Tests
{
//  public static class TestConstants
// {
//     public const string VALID_SEARCH_QUERY = "valid_product_query";
//     public const string INVALID_SEARCH_QUERY = "invalid_product_query";
// }
public class ConfigData
{
    public string VALID_SEARCH_QUERY { get; set; }
    public string INVALID_SEARCH_QUERY { get; set; }
    public string EMPTY_SEARCH_QUERY { get; set; }
    public string SPECIAL_CHARS_SEARCH_QUERY { get; set; }
    
}
    [TestFixture]
  
    public class SearchTest
    {
        protected IPlaywright _playwright;
        protected IBrowser _browser;
        protected IPage _page;
        private HomePage _homePage;
        private SearchPage _searchPage;
        [SetUp]

        public async Task Setup()
        {

            _playwright = await Playwright.CreateAsync();

            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions

            {

                Headless = false,

                SlowMo = 200

            });
            _page = await _browser.NewPageAsync();
            _homePage = new HomePage(_page);

            _searchPage = new SearchPage(_page);

            await _homePage.OpenAsync();

        }

        [TearDown]

        public async Task Teardown()

        {

            await _browser.CloseAsync();

            _playwright.Dispose();

        }

        [Test]

        [Category("UI")]

        [Description("Перевірка пошуку товару")]

        public async Task VerifySearchFunctionalityAsync()
        {
          var json = await File.ReadAllTextAsync("new.json");
        var convertor = JsonSerializer.Deserialize<ConfigData>(json);
        
            await _searchPage.Verify_Search_VAlid_ProductAsync(convertor.VALID_SEARCH_QUERY);
            await _searchPage.Verify_Search_Invalid_ProductAsync(convertor.INVALID_SEARCH_QUERY);
            await _searchPage.Verify_Search_Empty_ProductAsync(convertor.EMPTY_SEARCH_QUERY);
            await _searchPage.Verify_Search_Special_Chars_ProductAsync(convertor.SPECIAL_CHARS_SEARCH_QUERY);
        

        }
}
}
