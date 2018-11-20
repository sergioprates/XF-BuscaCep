using System;
using System.Collections.Generic;
using System.Text;
using BuscaCep.Data.Entidades;
using BuscaCep.Providers;
using SQLite;

namespace BuscaCep.Data
{
    class DatabaseService
    {
        private static Lazy<DatabaseService> _lazy = new Lazy<DatabaseService>(()=> new DatabaseService());
        private readonly SQLiteConnection _connection;

        public static DatabaseService Current { get => _lazy.Value; }

        private DatabaseService()
        {
            var dbPath = Xamarin.Forms.DependencyService.Get<ISQLiteDatabasePathProvider>().GetDatabasePath();

            _connection = new SQLite.SQLiteConnection(dbPath);

            _connection.CreateTable<Cep>();
        }

        public Cep Salvar(Cep entidade)
        {
            _connection.InsertOrReplace(entidade);
            return entidade;
        }

        public List<Cep> Listar() => _connection.Table<Cep>().ToList();

        public Cep Obter(Guid id) => _connection.Find<Cep>(id);
    }
}
