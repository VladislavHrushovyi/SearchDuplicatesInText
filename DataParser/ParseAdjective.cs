using System.Drawing;
using DataParser.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DataParser;

public class ParseAdjective
{
    private readonly IWebDriver _chromeDriver;
    List<string> list = new();

    private readonly string[] _urls = new[]
    {
        "https://uk.wiktionary.org/w/index.php?title=%D0%9A%D0%B0%D1%82%D0%B5%D0%B3%D0%BE%D1%80%D1%96%D1%8F:%D0%A3%D0%BA%D1%80%D0%B0%D1%97%D0%BD%D1%81%D1%8C%D0%BA%D1%96_%D0%BF%D1%80%D0%B8%D0%BA%D0%BC%D0%B5%D1%82%D0%BD%D0%B8%D0%BA%D0%B8",
        "https://uk.wiktionary.org/wiki/%D0%9A%D0%B0%D1%82%D0%B5%D0%B3%D0%BE%D1%80%D1%96%D1%8F:%D0%A3%D0%BA%D1%80%D0%B0%D1%97%D0%BD%D1%81%D1%8C%D0%BA%D1%96_%D0%BF%D1%80%D0%B8%D0%BA%D0%BC%D0%B5%D1%82%D0%BD%D0%B8%D0%BA%D0%B8,_%D0%BF%D1%80%D0%B8%D0%BA%D0%BC%D0%B5%D1%82%D0%BD%D0%B8%D0%BA%D0%BE%D0%B2%D0%B5_%D0%B2%D1%96%D0%B4%D0%BC%D1%96%D0%BD%D1%8E%D0%B2%D0%B0%D0%BD%D0%BD%D1%8F_1a"
    };

    public ParseAdjective()
    {
        this._chromeDriver = new ChromeDriver(@"C:/bin/WebDriverChrome");
        this._chromeDriver.Manage().Window.Size = new Size(500, 600);
    }

    public void StartParse()
    {

        var pathItem = By.XPath(@"//*[@id='mw-pages']/div/div/div/ul/*/a"); 
        var pathNextBtn = By.XPath(@"//a[contains(text(), 'наступна')]");
        foreach (var url in _urls)
        {
            _chromeDriver.Navigate().GoToUrl(url);
            while (true)
            {
                try
                {
                    WaiterElement.WaitElement(_chromeDriver, pathItem);
                    var items = _chromeDriver.FindElements(pathItem);
                    foreach (var word in items)
                    {
                        list.Add(word.Text);
                    }
                    WaiterElement.WaitUntilElementClickable(_chromeDriver, pathNextBtn);
                    //WaiterElement.WaitElement(_chromeDriver, pathNextBtn);
                    //var btn = Helpers.FindElement(_chromeDriver, pathNextBtn);
                    Helpers.FindElementAndClick(_chromeDriver, pathNextBtn);
                }
                catch (Exception e)
                {
                    //_chromeDriver.Close();
                    Console.WriteLine(e.Message);
                    break;
                }
            }
        }
        _chromeDriver.Close();
        using TextWriter tw = new StreamWriter("Adjective.txt", true);
        tw.WriteLine(list.Count);
        foreach (String s in list)
            tw.WriteLine(s.Substring(s.Length - 2) == "ий"
                ? s.ToLower().Remove(s.Length - 2)
                : s.ToLower().Remove(s.Length - 1));
    }
}