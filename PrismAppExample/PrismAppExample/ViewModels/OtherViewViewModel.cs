using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismMapsExample.ViewModels
{
    public class OtherViewViewModel : ViewModelBase
    {
        public OtherViewViewModel(INavigationService navigation) : base(navigation)
        {
            Title = "Other View";
        }
    }
}
