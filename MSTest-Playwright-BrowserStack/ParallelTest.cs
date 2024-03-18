namespace MSTest_Playwright_BrowserStack
{
    [TestClass]
    [TestCategory("sample-parallel-test")]
    public class ParallelTest
    {
        [TestMethod]
        public async Task Chrome()
        {
            var singleTest = new SingleTest("parallel", "chrome", "parallel.conf.json");
            await singleTest.Init();
            await singleTest.SearchBstackDemo();
            await singleTest.Cleanup();
        }

        [TestMethod]
        public async Task PlaywrightFirefox()
        {
            var singleTest = new SingleTest("parallel", "playwright-firefox", "parallel.conf.json");
            await singleTest.Init();
            await singleTest.SearchBstackDemo();
            await singleTest.Cleanup();
        }

        [TestMethod]
        public async Task PlaywrightWebkit()
        {
            var singleTest = new SingleTest("parallel", "playwright-webkit", "parallel.conf.json");
            await singleTest.Init();
            await singleTest.SearchBstackDemo();
            await singleTest.Cleanup();
        }
    }
}


