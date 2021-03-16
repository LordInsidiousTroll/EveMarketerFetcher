using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace PriceFetcher.Handlers {
    public static class ItemHandler {

        public static string GetItemIdByName(string name) {
            string a = $"https://www.fuzzwork.co.uk/api/typeid.php?typename={name}&format=xml";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(WebQueryHandler.DoQuery(a));
            var itemId = doc.SelectSingleNode("//row/@typeID").InnerText;
            return itemId;
        }
    }
}
