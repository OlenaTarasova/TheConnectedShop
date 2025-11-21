using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;
 
namespace TheConnectedShop.Pages

{

    public abstract class BasePage

    {

        protected readonly IPage Page;

        protected readonly string BaseUrl = "https://theconnectedshop.com/";
 
        protected BasePage(IPage page)//конструктор

        {

            Page = page;

        }
 
        public async Task NavigateAsync(string relativeUrl = "")

        {

            await Page.GotoAsync($"{BaseUrl}{relativeUrl}", new PageGotoOptions

            {

                WaitUntil = WaitUntilState.DOMContentLoaded //перевірка завантаження

            });

        }
 
        public async Task WaitForNetworkIdleAsync()// метод відповідає за очікування

        {

             await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        }

    }

}

 