using MediaTracker.Classes;
using MediaTracker.Pages.MediaSeries;
using System.Windows;

namespace MediaTracker.Pages
{
    /// <summary> Interaction logic for MainPage.xaml </summary>
    public partial class MainPage
    {
        #region Click

        #region Television

        private void BtnTelevisionAll_Click(object sender, RoutedEventArgs e) => AppState.Navigate(new AllTelevisionPage());

        private void BtnTelevisionAiring_Click(object sender, RoutedEventArgs e) => AppState.Navigate(new AiringPage());

        private void BtnTelevisionHiatus_Click(object sender, RoutedEventArgs e) => AppState.Navigate(new HiatusPage());

        private void BtnTelevisionEnded_Click(object sender, RoutedEventArgs e) => AppState.Navigate(new EndedPage());

        private void BtnTelevisionAddNew_Click(object sender, RoutedEventArgs e) => AppState.Navigate(new NewSeriesPage());

        #endregion Television

        #region Film

        private void BtnFilmReleased_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnFilmUpcoming_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnFilmAddNew_Click(object sender, RoutedEventArgs e)
        {
        }

        #endregion Film

        #endregion Click

        public MainPage() => InitializeComponent();

        private void MainPage_OnLoaded(object sender, RoutedEventArgs e) => AppState.CalculateScale(Grid);
    }
}