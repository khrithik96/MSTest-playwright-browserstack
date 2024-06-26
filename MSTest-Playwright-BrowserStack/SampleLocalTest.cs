using Microsoft.Playwright.MSTest;

namespace MSTest_Playwright_BrowserStack
{
    [TestClass]
    [TestCategory("sample-local-test")]
    public class SampleLocalTest : PageTest
    {
        public SampleLocalTest() : base() { }

        [TestMethod]
        public async Task SearchBstackDemo()
        {
            if (Page != null)
            {
                // Navigate to the base url
                await Page.GotoAsync("http://bs-local.com:45454/");

                // Verify if BrowserStackLocal running
                var title = await Page.TitleAsync();
                StringAssert.Contains("BrowserStack Local", title);
            }
        }
    }
}
