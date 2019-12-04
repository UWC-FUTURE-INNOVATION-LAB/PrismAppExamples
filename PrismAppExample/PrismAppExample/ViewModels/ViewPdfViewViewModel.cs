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
    public class ViewPdfViewViewModel : ViewModelBase
    {
        private string _pdfPath;

        private DelegateCommand _openGoogleDocsPdfCommand;
        public DelegateCommand OpenGoogleDocsPdfCommand =>
            _openGoogleDocsPdfCommand ?? (_openGoogleDocsPdfCommand = new DelegateCommand(ExecuteOpenGoogleDocsPdfCommand));

        void ExecuteOpenGoogleDocsPdfCommand()
        {
            NavigationService.NavigateAsync("ViewPdfOnlineView");
        }

        private IDocumentViewer _documentViewer;

        private DelegateCommand _openPdfCommand;
        public DelegateCommand OpenPdfCommand =>
            _openPdfCommand ?? (_openPdfCommand = new DelegateCommand(ExecuteOpenPdfCommand));

        public ViewPdfViewViewModel(INavigationService navigationService, IDocumentViewer documentViewer) : base(navigationService)
        {
            _pdfPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            _documentViewer = documentViewer;
        }

        void ExecuteOpenPdfCommand()
        {
            CopyEmbeddedContent("PrismAppExample.pdffile.MagPi78.pdf", "MagPi78.pdf");

            _documentViewer.ViewDocument(_pdfPath, "MagPi78.pdf");
        }

        private void CopyEmbeddedContent(string resourceName, string outputFileName)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;

            using (Stream resFilestream = assembly.GetManifestResourceStream(resourceName))
            {
                if (resFilestream == null) return;
                byte[] ba = new byte[resFilestream.Length];
                resFilestream.Read(ba, 0, ba.Length);

                File.WriteAllBytes(Path.Combine(_pdfPath,outputFileName), ba);
            }
        }





    }
}
