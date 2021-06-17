using System;
using System.IO;
using System.Threading.Tasks;
using Couchbase.Lite;
using Couchbase.Lite.Query;

namespace ReactiveTest.Repositories
{
    internal class DatabaseManager
    {
        readonly string _databaseName;

        Database _database;

        internal DatabaseManager(string databaseName)
        {
            _databaseName = databaseName;
        }

        public async Task<Database> GetDatabaseAsync()
        {
            if (_database == null)
            {
                if (_databaseName == "userprofile")
                {
                    var databaseConfig = new DatabaseConfiguration
                    {
                        Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                        "nichojo")
                    };

                    _database = new Database(_databaseName, databaseConfig);
                }
            }

            return _database;
        }

        // tag::createUniversityDatabaseIndexes[]
        void CreateUniversitiesDatabaseIndexes()
        {
            _database.CreateIndex("NameLocationIndex",
                                  IndexBuilder.ValueIndex(ValueIndexItem.Expression(Expression.Property("name")),
                                                          ValueIndexItem.Expression(Expression.Property("location"))));
        }
        // end::createUniversityDatabaseIndexes[]
    }
}
