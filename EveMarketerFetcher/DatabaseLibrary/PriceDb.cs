using DatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SQLite;
using static DatabaseLibrary.Resources;

namespace DatabaseLibrary {
    public static partial class PriceDb {

        internal static BaseDatabase DB = new BaseDatabase(Resources.PRICE_DB_PATH);

        #region Insert methods
        public static void InsertNewItem(Item item) {
            string values = $"";
            string vars = $"";
            string s = $"Insert Info Items ({vars}) Values ({values});";
            DB.ExecuteNonQuery(new SQLiteCommand(s));
        }

        public static void InsertNewSystem(DatabaseLibrary.Models.SolarSystem system) {
            string s = $"Insert Into Systems (Id, SystemName) Values ({system.Id}, {system.SystemName});";
            DB.ExecuteNonQuery(new SQLiteCommand(s));
        }

        public static void InsertNewItemPriceData(ItemPriceData ipd) {
            string vars = "(Id, SystemId, SellPrice, BuyPrice, SellVolume, BuyVolume)";
            var varray = new string[] { ipd.Id, ipd.SystemId, ipd.SellPrice, ipd.BuyPrice, ipd.SellVolume, ipd.BuyVolume};
            string values = $"({string.Join(",", varray)})";
            SQLiteCommand command = new SQLiteCommand($"Insert Into ItemPrice {vars} Values {values};");
            DB.ExecuteNonQuery(command);
        }
        #endregion


        #region Update methods
        public static void UpdateItemPriceData(ItemPriceData ipd) {
            string vars = $"SellPrice = '{ipd.SellPrice}', '{ipd.BuyPrice}', '{ipd.SellVolume}', '{ipd.BuyVolume}'";
            string s = $"Update ItemPrices ({vars}) Where Id = '{ipd.Id}';";
            DB.ExecuteNonQuery(new SQLiteCommand(s));
        }

        public static void UpdateItemPriceById(string itemId, SystemId systemId, string sellPrice, string buyPrice) {
            string vars = $"SellPrice = '{buyPrice}', BuyPrice = '{buyPrice}'";
            string s = $"Update ItemsPrices ({vars}) Where Id = '{itemId}' and SystemId = '{systemId}';";
            DB.ExecuteNonQuery(new SQLiteCommand(s));
        }
        #endregion

        #region Getter methods
        public static List<ItemPriceData> GetItemPriceDataById(string itemId) {
            string s = $"Select * From ItemPrices Where Id = '{itemId}';";
            var list = DB.ExecuteQueryMultipleReturn<ItemPriceData>(new SQLiteCommand(s), true);
            return list;
        }

        public static List<ItemPriceData> GetItemPriceDataByName(string itemName) {
            string s = $"Select * From ItemPrices Where ItemName = {itemName}';";
            var list = DB.ExecuteQueryMultipleReturn<ItemPriceData>(new SQLiteCommand(s), true);
            return list;
        }

        #region Find overall buy or sell price between Jita and Perimeter
        public static string GetItemSellPriceById(string itemId) {
            var list = GetItemPriceDataById(itemId).Select(i => i.SellPrice).ToList();
            return getSmallestSellPrice(list);
        }

        public static string GetItemSellPriceByName(string itemName) {
            var list = GetItemPriceDataByName(itemName).Select(i => i.BuyPrice).ToList();
            return getSmallestSellPrice(list);
        }



        public static string GetItemBuyPriceById(string itemId) {
            var list = GetItemPriceDataById(itemId).Select(i => i.BuyPrice).ToList();
            return getLargestBuyPrice(list);
        }

        public static string GetItemBuyPriceByName(string itemName) {
            var list = GetItemPriceDataByName(itemName).Select(i => i.BuyPrice).ToList();
            return getLargestBuyPrice(list);
        }
        #endregion


        private static string getSmallestSellPrice(List<string> prices) {
            int smallestIndex = 0;
            for (int i = 0; i < prices.Count; i++) {
                if (double.Parse(prices[i]) < double.Parse(prices[smallestIndex]))
                    smallestIndex = i;
            }
            return prices[smallestIndex];
        }

        private static string getLargestBuyPrice(List<string> prices) {
            int largestIndex = 0;
            for (int i = 0; i < prices.Count; i++) {
                if (double.Parse(prices[i]) > double.Parse(prices[largestIndex]))
                    largestIndex = i;
            }
            return prices[largestIndex];
        }

        #endregion
    }
}
