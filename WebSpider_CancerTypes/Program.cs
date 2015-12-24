using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;


namespace WebSpider_CancerTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            string htmlDoc = GetUrltoHtml("http://www.cancer.gov/types", "utf-8");


            //var uri = new Uri("http://www.cancer.gov/types");
            //var browser = new ScrapingBrowser();
            //var htmlDoc = browser.DownloadString(uri);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlDoc);

            var htmlNode = htmlDocument.DocumentNode;
            var title = htmlNode.SelectNodes("//ul[@class='cancer-list']");

            foreach (var ul in title)
            {
                string test = ul.InnerHtml;

                //var child = ul.SelectNodes("child::li");
                var child = ul.ChildNodes;
                //Console.WriteLine(ul.InnerText);
                foreach (var ulchild in child)
                {
                    if(ulchild.Name != "#text")
                    {
                        var grandchild = ulchild.SelectNodes("child::ul");
                        if (grandchild != null)
                        {
                            foreach(var sub in grandchild[0].ChildNodes)
                            {
                                if (sub.Name != "#text")
                                    Console.WriteLine(sub.InnerText);
                            }
                        }

                    }
                }
            }
            Console.ReadLine();
        }



        public static string GetUrltoHtml(string Url, string type)
        {
            try
            {
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                // Get the response instance.
                System.Net.WebResponse wResp = wReq.GetResponse();
                System.IO.Stream respStream = wResp.GetResponseStream();
                // Dim reader As StreamReader = New StreamReader(respStream)
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding(type)))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
