using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

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

    }
}
