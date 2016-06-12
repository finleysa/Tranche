using System.ComponentModel;

namespace TrancheWpf.Models
{
    public class TargetModel { }

    public class Target : INotifyPropertyChanged
    {
        private string alias;
        private int id;

        public string Alias
        {
            get { return alias; }
            set
            {
                if (alias != value)
                {
                    alias = value;
                    RaisePropertyChanged("Alias");
                }
            }
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
