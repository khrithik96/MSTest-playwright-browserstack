namespace MSTest_Playwright_BrowserStack
{
    [TestClass]
    [TestCategory("sample-local-test")]
    public class LocalTest : BrowserStackMSTest
    {
        public LocalTest() : base("local", "chrome", "local.conf.json") { }

        public LocalTest(string profile, string environment, string configFile) : base(profile, environment, configFile) { }

        [TestMethod]
        public async Task SearchBstackDemo()
        {
            if (page != null)
            {
                try
                {
                    // Navigate to the base url
                    await page.GotoAsync("http://bs-local.com:45454/");

                    // Verify if BrowserStackLocal running
                    var title = await page.TitleAsync();
                    StringAssert.Contains("BrowserStack Local", title);
                    await SetStatus(page, title.Contains("BrowserStack Local"));
                }
                catch (Exception)
                {
                    await SetStatus(page, false);
                    throw;
                }
            }
        }
    }
}

