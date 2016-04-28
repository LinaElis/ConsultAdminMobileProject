using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultAdminMobileProject.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _enableButton;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, e);
        }

        protected void SetPropertyField<T>(string propertyName, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue)) return;
            field = newValue;
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public bool EnableButton
        {
            get
            {
                return _enableButton;
            }
            set { SetPropertyField(nameof(EnableButton), ref _enableButton, value); }
        }
    }
}
