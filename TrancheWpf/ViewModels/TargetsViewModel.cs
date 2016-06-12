using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrancheWpf.DatabaseManager;
using TrancheWpf.Models;

namespace TrancheWpf.ViewModels
{
    class TargetsViewModel
    {
        private const string PgConnectString =
            "Host=localhost;Username=postgres;Password=password;Database=RoadRunner";
        private const string TargetsQuery =
            "Select * FROM t_intercepted_target";

        public ObservableCollection<Target> Targets
        {
            get;
            set;
        }

        public TargetsViewModel()
        {
            Targets = DBManager.Instance.ExecuteSqlQuery(PgConnectString, TargetsQuery);
        }
    }
}
