using Microsoft.Playwright;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TheConnectedShop.Pages
{
    public class ProfilePage : BasePage
    {
        private readonly ILocator _profileLink;
        private readonly ILocator _signInWithButton;
        private readonly ILocator _emailField;
        private readonly ILocator _continueButton;
        private readonly ILocator _emailFieldSignInAccount;
        private readonly string _testEmail;
        private readonly ILocator _continueSignInAccount;


        private const string ExpectedSignInTitle = "Sign in - The Connected Shop";
        private const string ExpectedSignInAccountTitle = "Sign in – Shop account";

        public ProfilePage(IPage page) : base(page)
        {
            _profileLink = Page.Locator("a[href*='customer_authentication/redirect']").Nth(1);
            _signInWithButton = Page.Locator("#standalone-button-core-idp");
            _emailField = Page.Locator("#email");
            _emailFieldSignInAccount = Page.Locator("#IdentityEmailForm-input");
            _continueButton = Page.Locator("button[name='commit'][type='submit']");
            _testEmail = "tarasova.e.pe@gmail.com";
            _continueSignInAccount =  Page.GetByRole(AriaRole.Button, new() { Name = "Continue" });
        }

       public async Task Verify_Click_Profile_IconAsync()
{
    //перевырка видимості
    Assert.That(await _profileLink.IsVisibleAsync(), Is.True, "Посилання профілю не відображається");

    await _profileLink.ClickAsync();

   // await Page.WaitForURLAsync(new Regex("authentication/login"));// типу регулярний вираз
await _emailField.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

    var currentUrlSignIn = Page.Url;// перевіряєм чи ми на првильній сторінці після кліку
    Assert.That( currentUrlSignIn,Does.Contain("authentication/login?client_id"),
        $"Incorrect URL. Expected to contain: authentication/login?client_id, Actual: {currentUrlSignIn}");

    var pageTitleSignIn = await Page.TitleAsync();
    Assert.That(pageTitleSignIn, Is.EqualTo(ExpectedSignInTitle),
        $"Incorrect page title. Expected: {ExpectedSignInTitle}, Actual: {pageTitleSignIn}");
}
// public async Task Verify_SignIn_With_EmailAsync(string email)
//         {
//             await _emailField.IsVisibleAsync();
//             await _emailField.FillAsync("tarasova.e.pe@gmail.com");
//            await _continueButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible }); 
//             await _continueButton.ClickAsync();
    //          await Page.WaitForURLAsync(new Regex("authentication/code")); //перехыд на ыншу сторынку підтвердження email
    //           var currentUrlEnterCode = Page.Url;// перевіряєм чи ми на првильній сторінці після кліку
    // Assert.That( currentUrlEnterCode,Does.Contain("authentication/code"),
    //     $"Incorrect URL. Expected to contain:authentication/code, Actual: {currentUrlEnterCode}");

    // var pageTitleEnterCode = await Page.TitleAsync();
    // Assert.That(pageTitleEnterCode, Is.EqualTo(ExpectedSignInTitle),
    //     $"Incorrect page title. Expected: {ExpectedSignInTitle}, Actual: {pageTitleEnterCode}");}
    

public async Task Verify_Sign_In_To_ShopAsync(string email)
{
    await _signInWithButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
    
    // Готуємо таск на нове вікно
    var popupTask = Page.WaitForPopupAsync();

    await _signInWithButton.ClickAsync();

    var popup = await popupTask;

    await popup.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

    
    var pageTitleSignInToShop = await popup.TitleAsync();
    Assert.That(pageTitleSignInToShop, Is.EqualTo(ExpectedSignInAccountTitle),
        $"Incorrect page title. Expected: {ExpectedSignInAccountTitle}, Actual: {pageTitleSignInToShop}");

    var emailField = popup.Locator("#IdentityEmailForm-input");
    var continueButton = popup.GetByRole(AriaRole.Button, new() { Name = "Continue" });

    await emailField.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
    await emailField.FillAsync(_testEmail);

    
    await continueButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
    await continueButton.ClickAsync();


}
    }
}