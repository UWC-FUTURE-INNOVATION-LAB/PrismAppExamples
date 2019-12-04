using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PrismMapsExample.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PrismAppExample.ViewModels
{
    public class ViewPdfOnlineViewViewModel : ViewModelBase
    {
        private string _pdfPath;


        public HtmlWebViewSource WebViewSource
        {
            get
            {
                return new HtmlWebViewSource { Html = _pdfUrl };
            }
        }

        private string _pdfUrl;
        public string PdfUrl
        {
            get { return _pdfUrl; }
            set
            {
                SetProperty(ref _pdfUrl, value);
                RaisePropertyChanged("WebViewSource");
            }
        }

        public ViewPdfOnlineViewViewModel(INavigationService navigationService) : base(navigationService)
        {

        }

        public async override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            _pdfPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            await Download("https://www.raspberrypi.org/magpi-issues/MagPi85.pdf","MagPi85.pdf");

            var localPath = Path.Combine(_pdfPath, "MagPi85.pdf");

            PdfUrl = $"file:///android_asset/pdfjs/web/viewer.html?file={"file:///" + WebUtility.UrlEncode(localPath)}";
        }

        
        private async Task Download(string url, string fileName)
        {
            using (var httpClient = new HttpClient())
            {
                var pdfStream = Task.Run(() => httpClient.GetStreamAsync(url)).Result;

                var filePath = Path.Combine(_pdfPath, fileName);

                using (var memoryStream = new MemoryStream())
                {
                    await pdfStream.CopyToAsync(memoryStream);
                    File.WriteAllBytes(filePath, memoryStream.ToArray());
                }
            }
        }
    }
}
