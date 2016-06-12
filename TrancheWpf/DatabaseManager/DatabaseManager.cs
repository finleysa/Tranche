using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using TrancheWpf.Models;

namespace TrancheWpf.DatabaseManager
{
    public class DBManager
    {
        private static DBManager instance = null;
        private static readonly object padlock = new object();

        private DBManager()
        {
        }

        public static DBManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DBManager();
                    }
                    return instance;
                }
            }
        }

        public ObservableCollection<Target> ExecuteSqlQuery(string connectionString, string query)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    using (var reader = cmd.ExecuteReader())
                    {
                        ObservableCollection<Target> targets = new ObservableCollection<Target>();
                        while (reader.Read())
                        {
                            targets.Add(new Target { ID = reader.GetInt16(0), Alias = reader.GetString(1)});
                        }
                        return targets;
                    }
                }
            }
        }
    }
}
