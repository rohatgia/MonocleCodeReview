using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Pdf;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Monocle.UWP;
using Xamarin.Forms;


[assembly: Dependency(typeof(PDFLoad))]
namespace Monocle.UWP
{
    public class PDFLoad : IPDFLoad
    {
        public PDFLoad() { }

        public ObservableCollection<BitmapImage> PdfPages
        {
            get;
            set;
        } = new ObservableCollection<BitmapImage>();

        public async void OpenLocal(String URI)
        {
            try
            {
                StorageFile storageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(URI, UriKind.Absolute));
                PdfDocument pdfDoc = await PdfDocument.LoadFromFileAsync(storageFile);

                Load(pdfDoc);
            }
            catch (FileNotFoundException error) {  }
            //ResumeCount--; DisplayEndOfListDialog();
        }

        public async void Load(PdfDocument pdfDoc)
        {
            PdfPages.Clear();

            for (uint i = 0; i < pdfDoc.PageCount; i++)
            {
                BitmapImage image = new BitmapImage();

                var page = pdfDoc.GetPage(i);

                using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                {
                    await page.RenderToStreamAsync(stream);
                    await image.SetSourceAsync(stream);
                }

                PdfPages.Add(image);
            }
        }
    }
}
