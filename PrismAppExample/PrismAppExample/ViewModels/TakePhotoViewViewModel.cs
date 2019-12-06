using Plugin.Media;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PrismMapsExample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace PrismAppExample.ViewModels
{
    public class TakePhotoViewViewModel : ViewModelBase
    {
        private IPageDialogService _dialogService;
        private DelegateCommand _takePhotoCommand;
        public DelegateCommand TakePhotoCommand =>
            _takePhotoCommand ?? (_takePhotoCommand = new DelegateCommand(ExecuteTakePhotoCommand));

        private ImageSource _cameraImage;

        public ImageSource CameraImage
        {
            get { return _cameraImage; }
            set { SetProperty(ref _cameraImage, value); }
        }


        private async void ExecuteTakePhotoCommand()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await _dialogService.DisplayAlertAsync("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            await _dialogService.DisplayAlertAsync("File Location", file.Path, "OK");



            CameraImage = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

        }

        public TakePhotoViewViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base (navigationService)
        {
            Title = "Take Photo";
            _dialogService = pageDialogService;
        }


    }
}
