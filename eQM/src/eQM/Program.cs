using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
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
            
            foreach(var td in tds)
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
                for(var i =0; i < 2; i++)
                {
                     var htmlUrl = htmlFiles[1].Descendants("a")?.ElementAt(0).ChildAttributes("href")?.FirstOrDefault().Value;
                    await Task.Delay(2000);
                     var htmlHttpClient = new HttpClient();
                     var newHtml2 = await htmlHttpClient.GetStringAsync(htmlUrl);
                     
                    var xmlDoc = XDocument.Parse(newHtml2);
                    xmlDoc.CreateReader();

                    var title = xmlDoc.Root.Elements().First(node => node.Name.LocalName == "title").Attribute("value").Value;
                    
                }
                
                //    //add breakpoints and check. also add how you'd want to save the downloaded page.
               
            }

        }
    }
    
}
