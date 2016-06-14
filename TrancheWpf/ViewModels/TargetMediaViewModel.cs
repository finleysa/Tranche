using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Prism.Events;
using TrancheWpf.DatabaseManager;
using TrancheWpf.Models;

namespace TrancheWpf.ViewModels
{
    public class TargetMediaViewModel
    {
        private const string PgConnectString =
            "Host=localhost;Username=postgres;Password=password;Database=RoadRunner";
        private const string TargetsQuery =
            "Select * FROM t_sms";

        private readonly IEventAggregator _iEventAggregator;
        private Target _selectedTarget;
        public ICommand Subscribe { get; set; }
        public IEnumerable<TargetMedia> TargetMediaData { get; set; }
        public IEnumerable<TargetMedia> DisplayedMediaData { get; set; }

        public Target SelectedTarget
        {
            get { return _selectedTarget; }
            set
            {
                if (_selectedTarget != value)
                {
                    _selectedTarget = value;
                    if (value != null)
                    {
                        DisplayedMediaData = from data in TargetMediaData where data.ID == value.ID select data;
                    }
                }
            }
        }

        public TargetMediaViewModel(IEventAggregator iEventAggregator)
        {
            TargetMediaData = DBManager.Instance.ExecuteSqlQuerySms(PgConnectString, TargetsQuery);
            this._iEventAggregator = iEventAggregator;
            SubscriptionToken subscriptionToken =
                this
                    ._iEventAggregator
                    .GetEvent<PubSubEvent<Target>>()
                    .Subscribe((details) => { this._selectedTarget = details; });

            this.Subscribe = new DelegateCommand(
            () =>
            {
                subscriptionToken =
                    this
                        ._iEventAggregator
                        .GetEvent<PubSubEvent<Target>>()
                        .Subscribe((details) =>
                        {
                            this.SelectedTarget = details;
                        });
            });
        }

        private void GetSms(int id)
        {
            //TargetMediaData = DBManager.Instance.ExecuteSqlQuerySms(PgConnectString, TargetsQuery);
            //Console.WriteLine(_targetMediaData[0].Sms + " " + _targetMediaData[0].Encoding + " " + _targetMediaData[0].ID);
        }
    }
}
