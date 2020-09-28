using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PCLStorage;
using Countr.Core.Models;
using SQLite;

namespace Countr.Core.Repositories
{
    public class CounterRepository : ICounterRepository
    {
        readonly SQLiteAsyncConnection _connection;

        public CounterRepository ()
        {
            var local = FileSystem.Current.LocalStorage.Path;
            var datafile = Path.Combine (local, "counters.db3");
            _connection = new SQLiteAsyncConnection (datafile);
            _connection.GetConnection ().CreateTable<Counter> ();
        }
        
        public Task Save (Counter counter)
        {
            return _connection.InsertOrReplaceAsync (counter);
        }

        public Task<List<Counter>> GetAll ()
        {
            return _connection.Table<Counter> ().ToListAsync ();
        }

        public Task Delete (Counter counter)
        {
            return _connection.DeleteAsync (counter);
        }
    }
}