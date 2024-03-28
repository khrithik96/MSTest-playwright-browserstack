namespace MSTest_Playwright_BrowserStack
{
    [TestClass]
    [TestCategory("sample-parallel-test")]
    public class ParallelTest
    {
        [TestMethod]
        [DataRow("parallel", "chrome", "parallel.conf.json")]
        [DataRow("parallel", "playwright-firefox", "parallel.conf.json")]
        [DataRow("parallel", "playwright-webkit", "parallel.conf.json")]
        public async Task SearchBstackDemo(string profile, string environment, string configFile)
        {
            var singleTest = new SingleTest(profile, environment, configFile);
            await singleTest.Init();
            await singleTest.SearchBstackDemo();
            await singleTest.Cleanup();
        }
    }
}


