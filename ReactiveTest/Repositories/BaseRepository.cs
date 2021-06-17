using System;
using System.IO;
using System.Threading.Tasks;
using Couchbase.Lite;

namespace ReactiveTest.Repositories
{
    public abstract class BaseRepository : IDisposable
    {
        private readonly string _databaseName;
        ListenerToken DatabaseListenerToken { get; set; }

        DatabaseConfiguration _databaseConfig;
        protected DatabaseConfiguration DatabaseConfig
        {
            get
            {
                if (_databaseConfig == null)
                {

                    _databaseConfig = new DatabaseConfiguration
                    {
                        Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                        "reactivedb")
                    };
                }

                return _databaseConfig;
            }
            set => _databaseConfig = value;
        }

        private Database _database;
        protected Database Database
        {
            get
            {
                if (_database == null)
                    _database = new Database(_databaseName, DatabaseConfig);

                return _database;
            }
            private set => _database = value;
        }

        public BaseRepository(string databaseName)
        {
            _databaseName = databaseName;

            DatabaseListenerToken = Database.AddChangeListener(OnDatabaseChangeEvent);
        }

        protected virtual async Task<Database> GetDatabaseAsync()
        {
            if (_database == null)
            {
                var databaseManager = new DatabaseManager(_databaseName);

                if (databaseManager != null)
                {
                    _database = await databaseManager.GetDatabaseAsync();
                }
            }

            return _database;
        }

        //public abstract T Get(K id);
        //public abstract bool Save(T obj);

        /// <summary>
        /// Logs db changes to console
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDatabaseChangeEvent(object sender, DatabaseChangedEventArgs e)
        {
            foreach (var documentId in e.DocumentIDs)
            {
                var document = Database?.GetDocument(documentId);

                string message = $"Document (id={documentId}) was ";

                if (document == null)
                {
                    message += "deleted";
                }
                else
                {
                    message += "added/updaAted";
                }

                Console.WriteLine(message);
            }
        }

        public void Dispose()
        {
            if(_database != null)
            {
                DatabaseConfig = null;

                Database.RemoveChangeListener(DatabaseListenerToken);
                _database.Close();
                _database = null;
            }
        }
    }
}
