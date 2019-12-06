﻿using Plugin.Media;
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
    public class PickPhotoViewViewModel : ViewModelBase
    {
        private IPageDialogService _dialogService;
        private DelegateCommand _pickPhotoCommand;
        public DelegateCommand PickPhotoCommand =>
            _pickPhotoCommand ?? (_pickPhotoCommand = new DelegateCommand(ExecutePickPhotoCommand));

        private ImageSource _pickImage;

        public ImageSource PickImage
        {
            get { return _pickImage; }
            set { SetProperty(ref _pickImage, value); }
        }


        private async void ExecutePickPhotoCommand()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {

            });


            if (file == null)
                return;

            await _dialogService.DisplayAlertAsync("File Location", file.Path, "OK");



            PickImage = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

        }

        public PickPhotoViewViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            Title = "Pick Photo";
            _dialogService = pageDialogService;
        }
    }

}
