using Microsoft.Playwright;

using System.Threading.Tasks;

using NUnit.Framework;
 
namespace TheConnectedShop.Pages

{

    public class ProfilePage : BasePage

    {
          
         private readonly ILocator _profileIcon;

        private readonly ILocator _profileLink;
       private  readonly ILocator _singInURL;
        protected readonly IPage _page;
         private const string ExpectedSignInTitle = "Sign in - The Connected Shop";
         // private const string ExpectedSignInUrl = "https://account.theconnectedshop.com/authentication/login?client_id=bab453b6-fd6a-4aec-ac3f-142ceb01bcd4&locale=en&redirect_uri=%2Fauthentication%2Foauth%2Fauthorize%3Fclient_id%3Dbab453b6-fd6a-4aec-ac3f-142ceb01bcd4%26locale%3Den%26nonce%3D6bd9d80b-7f4f-409f-819e-9d463fde7cc8%26redirect_uri%3Dhttps%253A%252F%252Faccount.theconnectedshop.com%252Fcallback%253Fsource%253Dcore%26region_country%3DUA%26response_type%3Dcode%26scope%3Dopenid%2Bemail%2Bcustomer-account-api%253Afull%26state%3DhWN5VjtTO0XcPSHKTq2R1CzZ&region_country=UA";
         // не подобається url
        public ProfilePage(IPage page) : base(page)

        {  
            _page = page;

            _profileIcon = _page.Locator("svg.icon-account").Nth(1);

            _profileLink = Page.Locator("a[href*='customer_authentication/redirect']").Nth(1);
            _singInURL = Page.Locator("a[href*='authentication/login?client_id']");
           
        }
          public async Task Verify_Click_Profile_IconAsync()
          {
           
             Assert.That(await _profileLink.IsVisibleAsync(), Is.True, "Посилання профілю не відображається");
         
          await _profileIcon.ClickAsync();

            await WaitForNetworkIdleAsync();

            // Перевірка URL
            var currentUrlSignIn = Page.Url;
            Assert.That(currentUrlSignIn, Does.Contain("authentication/login?client_id"), $"Incorrect URL. Expected to contain: authentication/login?client_id, Actual: {currentUrlSignIn}");

            var pageTitleSignIn = await Page.TitleAsync();
            Assert.That(pageTitleSignIn, Is.EqualTo(ExpectedSignInTitle), $"Incorrect page title. Expected: {ExpectedSignInTitle}, Actual: {pageTitleSignIn}");

          } 
          // провірка атрибутів кнопок і полів профілю
    }
}