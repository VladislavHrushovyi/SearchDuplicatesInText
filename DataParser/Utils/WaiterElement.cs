using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace DataParser.Utils;

public static class WaiterElement
{
    public static void WaitElement(IWebDriver driver, By locator, int seconds = 10)
    {
        Console.WriteLine("Allo");
        new WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementIsVisible(locator));
        new WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementToBeClickable(locator));
    }
    
    public static void WaitUntilElementClickable(IWebDriver driver, By elementLocator, int timeout = 10)
    {
        try
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(ExpectedConditions.ElementToBeClickable(elementLocator));
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
            throw;
        }
    }

    public static void ShouldLocate(IWebDriver webDriver, string location)
    {
        try
        {
            new WebDriverWait(webDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.UrlContains(location));
        }
        catch(WebDriverTimeoutException e)
        {
            throw new NotFoundException($"Not found {location}, timeout", e);
        }
    }
}