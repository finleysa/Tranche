using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TrancheWpf.Annotations;
using TrancheWpf.DatabaseManager;
using TrancheWpf.Events;
using TrancheWpf.Models;
using IEventAggregator = EventAggregator.IEventAggregator;

namespace TrancheWpf.ViewModels
{
    public class TargetsViewModel : INotifyPropertyChanged
    {
        private const string PgConnectString =
            "Host=localhost;Username=postgres;Password=password;Database=RoadRunner";
        private const string TargetsQuery =
            "Select * FROM t_intercepted_target";
        private Target _selectedTarget;
        private readonly IEventAggregator _iEventAggregator;


        public MyICommand DeleteCommand { get; set; }
        public MyICommand ShowAllMediaCommand { get; set; }
        public ObservableCollection<Target> Targets { get; set; }


        public TargetsViewModel(IEventAggregator iEventAggregator)
        {
            this._iEventAggregator = iEventAggregator;
            Targets = DBManager.Instance.ExecuteSqlQueryTarget(PgConnectString, TargetsQuery);
            ShowAllMediaCommand = new MyICommand(OnShow, CanShow);
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
        }

        private bool CanShow()
        {
            return true;
        }

        private void OnShow()
        {
            this._iEventAggregator.PublishEvent(new ShowAllMedia { Show = true });
        }

        public ObservableCollection<Target> Target { get; set; }

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
                    this._iEventAggregator.PublishEvent(new TargetSelected {Target = value});
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
