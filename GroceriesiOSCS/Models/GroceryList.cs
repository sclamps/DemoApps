using System.Collections.Generic;

namespace GroceriesiOSCS.Models
{
    public class GroceryList
    {
        public string ListName { get; set; }
        public User ListOwner { get; set; }
        public List<Item> ListItems { get; set; }
    }
}