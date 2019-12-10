using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PrismMapsExample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace PrismAppExample.ViewModels
{
    public class TimerViewViewModel : ViewModelBase
    {
        private string _currentTime;
        public string CurrentTime
        {
            get { return _currentTime; }
            set { SetProperty(ref _currentTime, value); }
        }


        public TimerViewViewModel(INavigationService navigationService) : base(navigationService)
        {

            Device.StartTimer(new TimeSpan(0, 0, 1), TimerTick);

        }

        private bool TimerTick()
        {
            CurrentTime = DateTime.Now.ToString();

            return true;
        }
    }
}
