using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models {
    public class Item : IEquatable<Item>{
        public string Id;
        public string ItemName;

        public bool Equals(Item other) {
            return Id == other.Id &&
                   ItemName == other.ItemName;
        }

        public override int GetHashCode() {
            int hashCode = -459046523;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ItemName);
            return hashCode;
        }
    }
}
