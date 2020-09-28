using SQLite;

namespace Countr.Core.Models
{
    public class Counter
    {
        [PrimaryKey, AutoIncrement]
        public int? id { get; set; }   
        public string Name { get; set; }
        public int Count { get; set; }
        
    }
}