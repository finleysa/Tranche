using System.Collections.ObjectModel;
using System.Globalization;
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

        public ObservableCollection<Target> ExecuteSqlQueryTarget(string connectionString, string query)
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
                        var targets = new ObservableCollection<Target>();
                        while (reader.Read())
                        {
                            targets.Add(new Target { ID = reader.GetInt16(0), Alias = reader.GetString(1)});
                        }
                        return targets;
                    }
                }
            }
        }

        public ObservableCollection<TargetMedia> ExecuteSqlQueryMedia(string connectionString, string query)
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
                        var targets = new ObservableCollection<TargetMedia>();
                        while (reader.Read())
                        {
                            var tm = new TargetMedia
                            {
                                target_id = reader.GetInt16(0),
                                alias = reader.GetString(1),
                                content = reader.GetString(2),
                                direction = reader.GetInt16(3),
                                start_time = reader.GetDateTime(4),
                                end_time = reader.GetDateTime(5)
                            };

                            if (string.IsNullOrEmpty(tm.content))
                            {
                                tm.content = @"{{{ NOT INTERCEPTED }}}";
                            }
                            targets.Add(tm);
                        }
                        return targets;
                    }
                }
            }
        }

        public ObservableCollection<TargetMedia> ExecuteSqlQueryAllMedia(string connectionString, string query)
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
                        var targets = new ObservableCollection<TargetMedia>();
                        while (reader.Read())
                        {
                            var tm = new TargetMedia
                            {
                                content = reader.GetString(0),
                                direction = reader.GetInt16(1),
                                start_time = reader.GetDateTime(2),
                                end_time = reader.GetDateTime(3)
                            };

                            if (string.IsNullOrEmpty(tm.content))
                            {
                                tm.content = @"{{{ NOT INTERCEPTED }}}";
                            }
                            targets.Add(tm);
                        }
                        return targets;
                    }
                }
            }


        }


    }
}
