using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EventAggregator;
using TrancheWpf.Annotations;
using TrancheWpf.Events;
using TrancheWpf.Models;

namespace TrancheWpf.ViewModels
{
    class TargetMediaDetailViewModel :  INotifyPropertyChanged, ISubscriber<MediaSelected>
    {
        private readonly IEventAggregator _iEventAggregator;

        private TargetMedia _selectedMedia;

        public TargetMedia SelectedMedia
        {
            get
            {
                if (_selectedMedia == null)
                {
                    _selectedMedia = new TargetMedia();
                }
                return _selectedMedia;
            }
            set
            {
                _selectedMedia = value;
                OnPropertyChanged(nameof(SelectedMedia));
            }
        }

        public TargetMediaDetailViewModel(IEventAggregator iEventAggregator)
        {
            this._iEventAggregator = iEventAggregator;
            this._iEventAggregator.SubsribeEvent(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnEventHandler(MediaSelected e)
        {
            SelectedMedia = e.TargetMedia;
        }
    }
}
