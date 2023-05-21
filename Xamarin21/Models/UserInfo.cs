using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Xamarin21.Models
{
    public class UserInfo : INotifyPropertyChanged
    {
        private Stack<int> _pin;
        //public Stack<int> Pin { get; set; }

        public Stack<int> Pin
        {
            get { return _pin; }
            set
            {
                if (_pin != value)
                {
                    _pin = new Stack<int>(value);
                    // Вызов уведомления при изменении
                    OnPropertyChanged("Pin");
                }
                //_pin = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
