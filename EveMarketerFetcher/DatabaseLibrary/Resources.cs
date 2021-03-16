using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary {
    public static class Resources {

        public static string PRICE_DB_PATH = @"C:\Users\Lord\Documents\evePriceQueries.db";
        public static string ITEMS_TO_CHECK = @"C:\Users\Lord\Documents\eve_items_to_track.txt";

        public enum SystemId {
            Jita = 30000142,
            Perimeter = 30000144
        }
    }
}
