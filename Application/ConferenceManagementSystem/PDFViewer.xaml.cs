using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ConferenceManagementSystem
{
    /// <summary>
    /// Interaction logic for PDFViewer.xaml
    /// </summary>
    public partial class PDFViewer : Window
    {
        private string pdfpath;

        public PDFViewer(string pdfpath)
        {
            this.pdfpath = pdfpath;
            InitializeComponent();
            pdfWebBrowser.Navigate(new Uri("about:blank"));
            pdfWebBrowser.Navigate(this.pdfpath);
        }
    }
}
