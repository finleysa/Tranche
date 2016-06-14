using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrancheWpf.Models
{
    public class TargetMedia : INotifyPropertyChanged
    {
        public int Encoding { get; set; }
        public string Sms { get; set; }
        public int ID { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
