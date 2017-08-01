using Extensions;
using Extensions.Enums;
using MediaTracker.Classes.Database;
using MediaTracker.Classes.MediaTypes;
using MediaTracker.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MediaTracker.Classes
{
    internal static class AppState
    {
        private static readonly SQLiteDatabaseInteraction DatabaseInteraction = new SQLiteDatabaseInteraction();
        internal static List<Series> AllSeries = new List<Series>();

        internal static MainWindow MainWindow { get; set; }

        #region Navigation

        internal static double CurrentPageWidth { get; set; }
        internal static double CurrentPageHeight { get; set; }

        /// <summary>Calculates the scale needed for the MainWindow.</summary>
        /// <param name="grid">Grid of current Page</param>
        internal static void CalculateScale(Grid grid)
        {
            CurrentPageHeight = grid.ActualHeight;
            CurrentPageWidth = grid.ActualWidth;
            MainWindow.CalculateScale();

            Page newPage = MainWindow.MainFrame.Content as Page;
            if (newPage != null)
                newPage.Style = (Style)MainWindow.FindResource("PageStyle");
        }

        /// <summary>Navigates to selected Page.</summary>
        /// <param name="newPage">Page to navigate to.</param>
        internal static void Navigate(Page newPage)
        {
            MainWindow.MainFrame.Navigate(newPage);
        }

        /// <summary>Navigates to the previous Page.</summary>
        internal static void GoBack()
        {
            if (MainWindow.MainFrame.CanGoBack)
                MainWindow.MainFrame.GoBack();
        }

        #endregion Navigation

        #region Database Interaction

        /// <summary>Verifies that the requested database exists and that its file size is greater than zero. If not, it extracts the embedded database file to the local output folder.</summary>
        public static void VerifyDatabaseIntegrity()
        {
            DatabaseInteraction.VerifyDatabaseIntegrity();
        }

        #endregion Database Interaction

        #region Television

        #region Delete

        /// <summary>Deletes a Series from the database.</summary>
        /// <param name="deleteSeries">Series to be deleted</param>
        /// <returns>True if successful</returns>
        public static async Task<bool> DeleteSeries(Series deleteSeries)
        {
            if (YesNoNotification($"Are you sure you want to delete {deleteSeries.Name}? This action cannot be undone.",
              "Media Tracker"))
            {
                if (await DatabaseInteraction.DeleteSeries(deleteSeries))
                {
                    AllSeries.Remove(deleteSeries);
                    return true;
                }
                DisplayNotification($"Unable to delete {deleteSeries.Name}.", "Media Tracker");
            }
            return false;
        }

        #endregion Delete

        #region Load

        internal static async Task LoadAll()
        {
            await LoadSeries();
        }

        /// <summary>Loads all Series from the database.</summary>
        /// <returns>All Series</returns>
        public static async Task LoadSeries()
        {
            AllSeries = await DatabaseInteraction.LoadSeries();
        }

        #endregion Load

        #region Save

        /// <summary>Modifies a Series in the database.</summary>
        /// <param name="oldSeries">Original series</param>
        /// <param name="newSeries">Series to replace original</param>
        /// <returns>True if successful</returns>
        internal static async Task<bool> ModifySeries(Series oldSeries, Series newSeries)
        {
            if (await DatabaseInteraction.ModifySeries(oldSeries, newSeries))
            {
                AllSeries.Replace(oldSeries, newSeries);
                AllSeries = AllSeries.OrderBy(series => series.Name).ToList();
                return true;
            }
            DisplayNotification("Unable to modify television series.", "Media Tracker");
            return false;
        }

        /// <summary>Saves a new Series to the database.</summary>
        /// <param name="newSeries">Series to be saved</param>
        /// <returns>True if successful</returns>
        internal static async Task<bool> NewSeries(Series newSeries)
        {
            if (await DatabaseInteraction.NewSeries(newSeries))
            {
                AllSeries.Add(newSeries);
                AllSeries = AllSeries.OrderBy(series => series.Name).ToList();
                return true;
            }
            DisplayNotification("Unable to add new television series.", "Media Tracker");
            return false;
        }

        #endregion Save

        #endregion Television

        #region Notification Management

        /// <summary>Displays a new Notification in a thread-safe way.</summary>
        /// <param name="message">Message to be displayed</param>
        /// <param name="title">Title of the Notification window</param>
        internal static void DisplayNotification(string message, string title)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                new Notification(message, title, NotificationButtons.OK, MainWindow).ShowDialog();
            });
        }

        /// <summary>Displays a new Notification in a thread-safe way and retrieves a boolean result upon its closing.</summary>
        /// <param name="message">Message to be displayed</param>
        /// <param name="title">Title of the Notification window</param>
        /// <returns>Returns value of clicked button on Notification.</returns>
        internal static bool YesNoNotification(string message, string title)
        {
            bool result = false;
            Application.Current.Dispatcher.Invoke(delegate
            {
                if (new Notification(message, title, NotificationButtons.YesNo, MainWindow).ShowDialog() == true)
                    result = true;
            });
            return result;
        }

        #endregion Notification Management
    }
}