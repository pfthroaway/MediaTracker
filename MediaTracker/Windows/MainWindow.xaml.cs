using MediaTracker.Classes;
using MediaTracker.Windows.MediaSeries;
using System.Windows;

namespace MediaTracker.Windows
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            AppState.VerifyDatabaseIntegrity();
        }

        #region Click

        private void BtnCurrentSeries_Click(object sender, RoutedEventArgs e)
        {
            AiringWindow window = new AiringWindow { PreviousWindow = this };
            window.RefreshItemsSource(true);
            window.Show();
            Visibility = Visibility.Hidden;
        }

        private void BtnHiatusSeries_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnEnded_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnReleased_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnUpcoming_Click(object sender, RoutedEventArgs e)
        {
        }

        #endregion Click

        private async void WindowMain_Loaded(object sender, RoutedEventArgs e)
        {
            await AppState.LoadAll();
        }
    }
}