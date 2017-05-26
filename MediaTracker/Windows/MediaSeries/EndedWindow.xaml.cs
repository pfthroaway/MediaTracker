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

namespace MediaTracker.Windows.MediaSeries
{
    /// <summary>
    /// Interaction logic for EndedWindow.xaml
    /// </summary>
    public partial class EndedWindow
    {
        public EndedWindow()
        {
            InitializeComponent();
        }

        private void WindowEnded_Closed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}