using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using TrancheWpf.Annotations;
using TrancheWpf.DatabaseManager;
using TrancheWpf.Models;

namespace TrancheWpf.ViewModels
{
    public class TargetsViewModel : INotifyPropertyChanged
    {
        private const string PgConnectString =
            "Host=localhost;Username=postgres;Password=password;Database=RoadRunner";
        private const string TargetsQuery =
            "Select * FROM t_intercepted_target";
        private Target _selectedTarget;
        public MyICommand DeleteCommand { get; set; }

        public TargetsViewModel(IEventAggregator iEventAggregator)
        {
            this.iEventAggregator = iEventAggregator;
            Targets = DBManager.Instance.ExecuteSqlQuery(PgConnectString, TargetsQuery);
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
        }

        public ObservableCollection<Target> Targets
        {
            get;
            set;
        }

        public Target SelectedTarget
        {
            get { return _selectedTarget; }
            set
            {
                if (_selectedTarget != value)
                {
                    _selectedTarget = value;
                    OnPropertyChanged("SelectedTarget");
                    DeleteCommand.RaiseCanExecuteChanged();
                    this.iEventAggregator
                        .GetEvent<PubSubEvent<Target>>()
                        .Publish(this.SelectedTarget);
                }
            }
        }

        private void OnDelete()
        {
            Targets.Remove(SelectedTarget);
        }

        private bool CanDelete()
        {
            return SelectedTarget != null;
        }

        private IEventAggregator iEventAggregator;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
