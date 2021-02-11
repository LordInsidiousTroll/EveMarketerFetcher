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

        public static void InsertNewItem(Item item) {
            string values = $"";
            string vars = $"";
            string s = $"Insert Info Items ({vars}) Values ({values});";
            DB.ExecuteNonQuery(new SQLiteCommand(s));
        }

        public static void InsertNewSystem(DatabaseLibrary.Models.System system) {
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

        public static void UpdateItemPriceData(ItemPriceData ipd) {
            string vars = $"SellPrice = '{ipd.SellPrice}', '{ipd.BuyPrice}', '{ipd.SellVolume}', '{ipd.BuyVolume}'";
            string s = $"Update ItemPrices ({vars}) Where Id = '{ipd.Id}';";
            DB.ExecuteNonQuery(new SQLiteCommand(s));
        }

        public static void UpdateItemPriceById(string itemId, SystemId systemId) {

        }
    }
}
