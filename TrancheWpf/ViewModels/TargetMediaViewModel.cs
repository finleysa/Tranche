using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EventAggregator;
using TrancheWpf.Annotations;
using TrancheWpf.DatabaseManager;
using TrancheWpf.Events;
using TrancheWpf.Models;
using IEventAggregator = EventAggregator.IEventAggregator;

namespace TrancheWpf.ViewModels
{
    public class TargetMediaViewModel : INotifyPropertyChanged, ISubscriber<TargetSelected>
    {
        private const string PgConnectString =
            "Host=localhost;Username=postgres;Password=password;Database=RoadRunner";
        private const string TargetsQuery =
            @"select t_intercepted_com.id as target_id, t_intercepted_target.alias, t_sms.content, t_com_media.direction from t_sms
            join t_com_media
            on t_com_media.id_com_media = t_sms.id_com_media
            join t_intercepted_com
            on t_intercepted_com.id_com = t_com_media.id_com
            join t_intercepted_target
            on t_intercepted_target.id = t_intercepted_com.id";

        private readonly IEventAggregator _iEventAggregator;
        public IEnumerable<TargetMedia> DisplayedMediaData { get; set; }
        public IEnumerable<TargetMedia> InternalMediaData { get; set; }
        private TargetMedia _selectedMedia;

        public TargetMedia SelectedMedia {
            get { return _selectedMedia; }
            set
            {
                _selectedMedia = value;
                OnPropertyChanged(nameof(value));
                this._iEventAggregator.PublishEvent(new MediaSelected { TargetMedia = value });
            }
        }

        public TargetMediaViewModel(IEventAggregator iEventAggregator)
        {
            InternalMediaData = DBManager.Instance.ExecuteSqlQueryMedia(PgConnectString, TargetsQuery);
            DisplayedMediaData = InternalMediaData;

            this._iEventAggregator = iEventAggregator;
            this._iEventAggregator.SubsribeEvent(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnEventHandler(TargetSelected e)
        {
            DisplayedMediaData = from data in InternalMediaData where data.target_id == e.Target.ID select data;
            OnPropertyChanged(nameof(DisplayedMediaData));
        }
    }
}
