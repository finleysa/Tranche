using System;
using System.ComponentModel;

namespace TrancheWpf.Models
{
    public class TargetModel { }

    public class Target : INotifyPropertyChanged
    {
        #region Fields
        private string _alias;
        private int _id;
        #endregion

        #region Properties
        public string Alias
        {
            get { return _alias; }
            set
            {
                if (_alias != value)
                {
                    _alias = value;
                    RaisePropertyChanged("Alias");
                }
            }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
