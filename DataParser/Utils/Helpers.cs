using OpenQA.Selenium;

namespace DataParser.Utils;

public class Helpers
{
    public static void FindElementAndClick(IWebDriver driver, By xPath)
    {
        driver.FindElement(xPath).Click();
    }

    public static IWebElement FindElement(IWebDriver driver, By xPath)
    {
        return driver.FindElement(xPath);
    }

    public static string GetTextFromElement(IWebDriver driver, By xPath)
    {
        return driver.FindElement(xPath).Text;
    }

    public static void SendKeysToElement(IWebElement element, string keys)
    {
        element.SendKeys(keys);
    }
}