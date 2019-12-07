using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PrismAppExample.Services.Interfaces;
using PrismMapsExample;
using PrismMapsExample.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PrismAppExample.ViewModels
{
    public class EmbeddedHtmlViewViewModel : ViewModelBase
    {
        private string _basePath;
        private IContentPackage _contentPackage;

        private string _contentUrl;
        public string ContentUrl
        {
            get { return _contentUrl; }
            set
            {
                SetProperty(ref _contentUrl, value);
            }
        }

        public EmbeddedHtmlViewViewModel(INavigationService navigationService, IContentPackage package) : base(navigationService)
        {
            _basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            _contentPackage = package;
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            ExtractContent();

            var contentPath = Path.Combine(_basePath, "content");
            ContentUrl = "file:///" + contentPath + "/index.html";
        }

        private void ExtractContent()
        {
            CopyEmbeddedContent(_basePath, "PrismAppExample.htmlzipfile.content.zip", "content.zip");

            var destination = Path.Combine(_basePath, "content");

            if (!Directory.Exists(destination))
                Directory.CreateDirectory(destination);

            _contentPackage.ExtractResourceFiles(_basePath, destination, "content.zip");
        }

        private void CopyEmbeddedContent(string basePath, string resourceName, string outputFileName)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;

            using (Stream resFilestream = assembly.GetManifestResourceStream(resourceName))
            {
                if (resFilestream == null) return;
                byte[] ba = new byte[resFilestream.Length];
                resFilestream.Read(ba, 0, ba.Length);

                File.WriteAllBytes(Path.Combine(basePath, outputFileName), ba);
            }
        }
    }
}
