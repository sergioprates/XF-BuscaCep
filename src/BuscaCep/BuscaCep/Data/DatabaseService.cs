using System;
using System.Collections.Generic;
using System.Text;

namespace BuscaCep.Data
{
    class DatabaseService
    {
        private static Lazy<DatabaseService> _lazy = new Lazy<DatabaseService>(()=> new DatabaseService());

        public static DatabaseService Current { get => _lazy.Value; }

        private DatabaseService()
        {
            //_sqlLiteConnection
        }
    }
}
