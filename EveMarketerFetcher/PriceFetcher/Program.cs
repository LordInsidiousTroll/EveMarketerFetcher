using PriceFetcher.Handlers;
using System;
using System.IO;
using DatabaseLibrary;
using DatabaseLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace PriceFetcher {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            //var id = ItemHandler.GetItemIdByName("Tritanium");

            //ImportItemsFromFile();

            //ImportItemsFromTextFile();


        }

        public static void ImportItemsFromFile() {
            var input = File.ReadAllLines(Resources.ITEMS_TO_CHECK);

            List<Item> items = new List<Item>();
            foreach (var name in input) {
                if (name == null || name.Trim() == "")
                    continue;
                var id = ItemHandler.GetItemIdByName(name);
                items.Add(new Item { Id = id, ItemName = name });
            }

            string path = @"C:\Users\Lord\Documents\items.txt";
            var itemList = items.ConvertAll(i => $"{i.Id},{i.ItemName}");
            File.WriteAllLines(path, itemList);

            foreach (var item in items) {
                PriceDb.InsertNewItem(item);
            }
        }

        public static void ImportItemsFromTextFile() {
            string path = @"C:\Users\Lord\Documents\items.txt";

            var list = File.ReadAllLines(path);
            var items = new List<Item>();
            var ids = new List<string>();
            foreach (var line in list) {
                var ls = line.Split(',');
                var id = ls[0].Trim();
                var name = ls[1].Trim();
                ids.Add(id);
                items.Add(new Item { Id = id, ItemName = name });
            }

            var distinctItems = items.Distinct().ToList();

            //var distinctIds = ids.Distinct().ToList();
            //var duplicates = ids.GroupBy(x => x).Where(group => group.Count() > 1).Select(group => group.Key).ToList();
            //List<string> duplicateIndexes = new List<string>();
            //for (int i = 0; i < items.Count; i++) {
            //    Item item = items[i];
            //    if (duplicates.Contains(item.Id)) {
            //        duplicateIndexes.Add(i.ToString());
            //    }
            //}
            string s = "ayy";

            foreach (var item in distinctItems) {
                PriceDb.InsertNewItem(item);
            }
        }
    }
}
