using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using Microsoft.Extensions.DependencyInjection;


namespace eQM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Crawler();
            Console.ReadLine();

        }
        private static async void Crawler()
        {
            var url = "https://ecqi.healthit.gov/ep/ecqms-2017-performance-period";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var tds = htmlDocument.DocumentNode.Descendants("td")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("views-field views-field-title active")).ToList();

            foreach (var td in tds)
            {
                var uri = "https://ecqi.healthit.gov" + td.Descendants("a")?.FirstOrDefault()?.ChildAttributes("href")?.FirstOrDefault().Value;
                await Task.Delay(5000);
                var newHttpClient = new HttpClient();
                var newHtml = await newHttpClient.GetStringAsync(uri);
                var newHtmlDocument = new HtmlDocument();
                newHtmlDocument.LoadHtml(newHtml);
                var htmlFiles = newHtmlDocument.DocumentNode.Descendants("span")
                    .Where(n => n.GetAttributeValue("class", "")
                    .Equals("file")).ToArray();
                for (var i = 0; i < 2; i++)
                {
                    var htmlUrl = htmlFiles[1].Descendants("a")?.ElementAt(0).ChildAttributes("href")?.FirstOrDefault().Value;
                    await Task.Delay(2000);
                    var htmlHttpClient = new HttpClient();
                    var newHtml2 = await htmlHttpClient.GetStringAsync(htmlUrl);
                    var newMeasure = new Measure();
                    var xmlDoc = XDocument.Parse(newHtml2);

                    
                    var measureNumber = xmlDoc.Root.Elements()?.Where(n => n.Name.LocalName == "subjectOf")?.ElementAt(0).Descendants()?.Where(n => n.Name.LocalName == "value")
                            ?.First(node => node.Name.LocalName == "value")?.Attribute("value").Value;
                    var rationale = xmlDoc.Root.Elements()?.Where(n => n.Name.LocalName == "subjectOf")?.ElementAt(9).Descendants()?.Where(n => n.Name.LocalName == "value")
                            ?.First(node => node.Name.LocalName == "value")?.Attribute("value").Value;
                    var clinicalRec = xmlDoc.Root.Elements()?.Where(n => n.Name.LocalName == "subjectOf")?.ElementAt(10).Descendants()?.Where(n => n.Name.LocalName == "value")
                            ?.First(node => node.Name.LocalName == "value")?.Attribute("value").Value;
                    var title = xmlDoc.Root.Elements()?.First(node => node.Name.LocalName == "title")?.Attribute("value").Value;
                    newMeasure.Title = title;
                    //var xmlDocPath = new XPathDocument(newHtml2);
                    //xmlDocPath.CreateNavigator();
                    var elements = xmlDoc.Root.Elements()?.Where(n => n.Name.LocalName == "subjectOf")
                       
                       ?.ToList();
                  
                  foreach(var element in elements)
                  {
                        var references = element.Descendants()?.Where(n => n.Name.LocalName == "measureAttribute")?.ToList();
                        
                        foreach(var reference in references)
                        {
                            var displayNames = reference?.Descendants()?.Where(n=>n.Name.LocalName == "displayName").ToList();
                            foreach(var displayName in displayNames)
                            {
                                var displayNameValue = displayName?.Attribute("value").Value;
                                if (displayNameValue == "Reference")
                                {
                                    var refValue = reference?.LastNode
                                        ?.ElementsAfterSelf()
                                        ?.First(n=>n.Name.LocalName == "value").Attribute("value")?.Value;
                                }
                            }
                           
                        }
                  }

                    //add breakpoints and check. also add how you'd want to save the downloaded page. 

                }

            }
        }
    }
}
