# MSTest-playwright-browserstack

This sample elaborates the [MSTest](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest) Integration with BrowserStack.

<img src="assets/browserstack.png" width=30 height=25> <img src="assets/MSTest.png" width=50 height=25> 

> To perform tests using SDK, please checkout the sdk branch

## Setup

### Installation Steps

1. Clone the repository.
2. Open the solution `MSTest-Playwright-BrowserStack.sln` in Visual Studio.
3. Install dependencies using NuGet Package Manager:
    ```bash
    dotnet restore
    ```
4. Build the solution

### Adding Credentials

1. Add your BrowserStack Username and Access Key to the `*.conf.json` files in the project. 
   
    ```json
    {
        "user": "BROWSERSTACK_USERNAME",
        "key": "BROWSERSTACK_ACCESS_KEY",
    }
    ```

2. Alternatively, you can set environment variables:
    - `BROWSERSTACK_USERNAME`
    - `BROWSERSTACK_ACCESS_KEY`

You can get your browserstack credentials from [here](https://www.browserstack.com/accounts/profile/details)

### Running Tests

- To run tests, execute the following command:
    ```bash
    dotnet test
    ```

- To run the single test, execute the following command:
    ```bash
    dotnet test --filter TestCategory=sample-test
    ```

- To run tests in parallel, execute the following command:
    ```bash
    dotnet test --filter TestCategory=sample-parallel-test
    ```

- To run local tests, execute the following command:
    ```bash
    dotnet test --filter TestCategory=sample-local-test
    ```

Understand how many parallel sessions you need by using our [Parallel Test Calculator](https://www.browserstack.com/automate/parallel-calculator?ref=github)

## Notes
* You can view your test results on the [BrowserStack automate dashboard](https://www.browserstack.com/automate)
* To test on a different set of browsers, check out our [platform configurator](https://www.browserstack.com/automate/c-sharp#setting-os-and-browser)
* You can export the environment variables for the Username and Access Key of your BrowserStack account

  * For Unix-like or Mac machines:
  ```
  export BROWSERSTACK_USERNAME=<browserstack-username> &&
  export BROWSERSTACK_ACCESS_KEY=<browserstack-access-key>
  ```

  * For Windows Cmd:
  ```
  set BROWSERSTACK_USERNAME=<browserstack-username>
  set BROWSERSTACK_ACCESS_KEY=<browserstack-access-key>
  ```

  * For Windows Powershell:
  ```
  $env:BROWSERSTACK_USERNAME=<browserstack-username>
  $env:BROWSERSTACK_ACCESS_KEY=<browserstack-access-key>
  ```

## Additional Resources
* [Documentation for writing automate playwright test scripts in C#](https://www.browserstack.com/docs/automate/playwright/getting-started/c-sharp)
* [Customizing your tests on BrowserStack](https://www.browserstack.com/automate/capabilities)
* [Browsers & mobile devices for selenium testing on BrowserStack](https://www.browserstack.com/list-of-browsers-and-platforms?product=automate)
* [Using REST API to access information about your tests via the command-line interface](https://www.browserstack.com/automate/rest-api)
