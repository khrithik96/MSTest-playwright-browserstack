using Microsoft.Playwright;

namespace MSTest_Playwright_BrowserStack
{
    [TestClass]
    public class PageTest
    {
        protected IBrowser? browser;
        protected IPage? page;

        [TestInitialize]
        public async Task Initialize()
        {
            var playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync();
            page = await browser.NewPageAsync();
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            if (browser != null)
            {
                await browser.CloseAsync();
            }
        }
        public static async Task SetStatus(IPage browserPage, bool passed)
        {
            if (browserPage is not null)
            {
                if (passed)
                    await browserPage.EvaluateAsync("_ => {}", "browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \"Test Passed!\"}}");
                else
                    await browserPage.EvaluateAsync("_ => {}", "browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"Test Failed!\"}}");
            }
        }
    }
}
