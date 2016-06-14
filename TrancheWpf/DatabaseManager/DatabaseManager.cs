using System;
using System.Collections.ObjectModel;
using Npgsql;
using TrancheWpf.Models;

namespace TrancheWpf.DatabaseManager
{
    public class DBManager
    {
        private static DBManager instance = null;
        private static readonly object padlock = new object();
        private const string PgConnectString =
            "Host=localhost;Username=postgres;Password=password;Database=RoadRunner";
        private const string TcomQuery =
            "Select * FROM t_com";

        public ObservableCollection<TCom> Tcom;

        private DBManager()
        {
            Tcom = this.ExecuteSqlQueryTcom(PgConnectString, TcomQuery);
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

        public ObservableCollection<TCom> ExecuteSqlQueryTcom(string connectionString, string query)
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
                        var TComs = new ObservableCollection<TCom>();
                        while (reader.Read())
                        {
                            TComs.Add(new TCom
                            {
                                id_source_com_info = reader.GetInt16(0),
                                //id_appli = reader.GetInt16(1),
                                //num_com = reader.GetInt16(2),
                                //start_time = reader.GetTimeStamp(3).ToString(),
                                //end_time = reader.GetTimeStamp(4).ToString(),
                                //end_reason = reader.GetInt16(5),
                                //is_source = reader.GetBoolean(6),
                                //gps_localization = reader.GetString(7),
                                //is_running = reader.GetBoolean(8),
                                //timestamp = reader.GetInt16(9),
                                //is_emergency = reader.GetBoolean(10),
                                //is_ccbs = reader.GetBoolean(11),
                                //is_exported = reader.GetBoolean(12),
                                //switch_type = reader.GetInt16(13),
                                //end_info = reader.GetInt16(14),
                                //power = reader.GetInt16(15),
                                //has_ciphering = reader.GetBoolean(16),
                                //networkshortname = reader.GetString(17),
                                //networkfullname = reader.GetString(18),
                                //import_status = reader.GetInt16(19),
                                //exploitation_state = reader.GetInt16(20),
                                //to_be_deleted = reader.GetBoolean(21)
                            });
                        }
                        return TComs;
                    }
                }
            }
        }

        public ObservableCollection<TargetMedia> ExecuteSqlQuerySms(string connectionString, string query)
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
                        var media = new ObservableCollection<TargetMedia>();
                        while (reader.Read())
                        {
                            if (!string.IsNullOrEmpty(reader.GetString(2)))
                            {
                                // Check if SMS content is empty
                                Console.WriteLine(reader.GetInt32(1));                                      
                                media.Add(new TargetMedia
                                {
                                    ID = reader.GetInt32(1),
                                    Sms = reader.GetString(2),
                                    Encoding = reader.GetInt16(5)
                                });
                            }
                        }
                        return media;
                    }
                }
            }
        }
    }
}
