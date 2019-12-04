using System;
using System.Collections.Generic;
using System.Text;

namespace PrismAppExample.Services.Interfaces
{
    public interface IDocumentViewer
    {
        void ViewDocument(string path, string documentName);
    }
}
