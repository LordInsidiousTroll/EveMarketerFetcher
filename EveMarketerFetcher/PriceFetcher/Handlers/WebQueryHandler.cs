using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using DatabaseLibrary;
using static DatabaseLibrary.Resources;

namespace PriceFetcher.Handlers {
    public class WebQueryHandler {


        public static string DoQuery(string url) {
            string rt;
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            rt = reader.ReadToEnd();
            Console.WriteLine(rt);

            reader.Close();
            response.Close();

            return rt;
        }

        public static string GetUrl(string itemId, string systemId) {
            string a = $"https://api.evemarketer.com/ec/marketstat?typeid={itemId}&usesystem={systemId}";
            return DoQuery(a);
        }

        public static string GetUrl(string itemId, SystemId system) {
            string a = $"https://api.evemarketer.com/ec/marketstat?typeid={itemId}&usesystem={system.ToString()}";
            return DoQuery(a);
        }
    }
}
