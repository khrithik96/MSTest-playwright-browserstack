using System;
using System.IO;
using BrowserStack;
using Microsoft.Playwright;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MSTest_Playwright_BrowserStack
{
    [TestClass]
    public class BrowserStackMSTest
    {
        protected IBrowser? browser;
        protected IPage? page;
        protected string? profile;
        protected string? environment;
        protected string? configFile;

        private Local? browserStackLocal;

        public BrowserStackMSTest(string profile, string environment, string configFile)
        {
            this.profile = profile;
            this.environment = environment;
            this.configFile = configFile;
        }

        [TestInitialize]
        public async Task Init()
        {
            // Get Configuration for correct profile
            string? currentDirectory = GoUpLevels(Directory.GetCurrentDirectory(), 3);

            if (configFile != null && profile != null && currentDirectory != null)
            {
                string path = Path.Combine(currentDirectory, configFile);

                JObject config = JObject.Parse(File.ReadAllText(path));
                if (config is null)
                    throw new Exception("Configuration not found!");

                // Get Environment specific capabilities
                JObject? capabilitiesJsonArr = config.GetValue("environments") as JObject;

                if(capabilitiesJsonArr is null)
                    throw new Exception("Environments not found!");

                JObject? capabilities = capabilitiesJsonArr.GetValue(environment) as JObject;

                if (capabilities is null)
                    throw new Exception("Capabilities not initialised!");

                // Get Common Capabilities
                JObject? commonCapabilities = config.GetValue("capabilities") as JObject;

                // Merge Capabilities
                capabilities.Merge(commonCapabilities);

                // Get username and accesskey
                string? username = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
                if (username is null)
                    username = (string?)config.GetValue("user");

                string? accessKey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
                if (accessKey is null)
                    accessKey = (string?)config.GetValue("key");

                capabilities["browserstack.user"] = username;
                capabilities["browserstack.key"] = accessKey;

                // Start Local if browserstack.local is set to true
                if (profile.Equals("local") && accessKey is not null)
                {
                    capabilities["browserstack.local"] = true;
                    browserStackLocal = new Local();
                    List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>() {
                            new KeyValuePair<string, string>("key", accessKey)
                        };
                    var localOptionsObject = config.GetValue("localOptions") as JObject;

                    if (localOptionsObject != null)
                    {
                        foreach (var localOption in localOptionsObject)
                        {
                            if (localOption.Value != null)
                            {
                                bsLocalArgs.Add(new KeyValuePair<string, string>(localOption.Key, localOption.Value.ToString()));
                            }
                        }
                    }
                    browserStackLocal.start(bsLocalArgs);
                }

                string capsJson = JsonConvert.SerializeObject(capabilities);
                string cdpUrl = "wss://cdp.browserstack.com/playwright?caps=" + Uri.EscapeDataString(capsJson);

                var playwright = await Playwright.CreateAsync();
                browser = await playwright.Chromium.ConnectAsync(cdpUrl);
                page = await browser.NewPageAsync();
            }
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            if (browser != null)
            {
                await browser.CloseAsync();
            }

            if (browserStackLocal != null)
            {
                browserStackLocal.stop();
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

        private static string GoUpLevels(string path, int levels)
        {
            // Combine with ".." for each level to go up
            string newPath = path;
            for (int i = 0; i < levels; i++)
            {
                newPath = Path.Combine(newPath, "..");
            }

            // Get the full path after going up the specified levels
            return Path.GetFullPath(newPath);
        }
    }
}

