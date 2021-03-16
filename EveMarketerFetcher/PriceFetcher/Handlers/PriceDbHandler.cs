using DatabaseLibrary;
using DatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PriceFetcher.Handlers {
    public class PriceDbHandler {

        

        public void InsertIfNotPresent(Item item) {
            InsertIfNotPresent(new List<Item> { item });
        }

        public void InsertIfNotPresent(List<Item> items) {

            List<int> existingIds = PriceDb.GetAllIds();
            List<Item> newItems = items.Where(i => !existingIds.Contains(int.Parse(i.Id))).ToList();

            foreach (var newItem in newItems) {
                //InsertIfNotPresent(newItem);
                PriceDb.InsertNewItem(newItem);
            }
        }
    }
}
