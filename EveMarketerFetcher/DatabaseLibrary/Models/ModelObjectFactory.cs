using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace DatabaseLibrary.Models {
    public class ModelObjectFactory {


        public static TResult CreateObject<TResult>(object data) {
            if (data.GetType() == typeof(SQLiteDataReader)) {
                var reader = (SQLiteDataReader)data;
                return _createObject<TResult>(reader);
            }
            //else if (data.GetType() == typeof(System.Data.DataRow)) {
            //    var dataRow = (System.Data.DataRow)data;
            //    return _createObject<TResult>(dataRow);
            //}
            return default(TResult);
        }

        public static TResult _createObject<TResult>(SQLiteDataReader reader) {
            if (typeof(TResult) == typeof(ItemPriceData))
                return (TResult)CreateItemPriceDataObject(reader);
            else if (typeof(TResult) == typeof(Item))
                return (TResult)CreateItemObject(reader);
            else
                return default(TResult);
        }

        public static object CreateItemPriceDataObject(SQLiteDataReader reader) {
            var ipd = new ItemPriceData {
                Id = reader.GetValue(reader.GetOrdinal("Id")).ToString(),
                ItemName = reader.GetValue(reader.GetOrdinal("ItemName")).ToString(),
                SystemId = reader.GetValue(reader.GetOrdinal("SystemId")).ToString(),
                SellPrice = reader.GetValue(reader.GetOrdinal("SellPrice")).ToString(),
                BuyPrice = reader.GetValue(reader.GetOrdinal("BuyPrice")).ToString(),
                SellVolume = reader.GetValue(reader.GetOrdinal("SellVolume")).ToString(),
                BuyVolume = reader.GetValue(reader.GetOrdinal("BuyVolume")).ToString()
            };
            return ipd;
        }

        public static object CreateItemObject(SQLiteDataReader reader) {
            var i = new Item {
                Id = reader.GetValue(reader.GetOrdinal("Id")).ToString(),
                ItemName = reader.GetValue(reader.GetOrdinal("ItemName")).ToString()
            };
            return i;
        }
    }
}
