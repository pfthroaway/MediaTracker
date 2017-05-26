using Extensions;
using Extensions.ListViewHelp;
using MediaTracker.Classes;
using MediaTracker.Classes.Enums;
using MediaTracker.Classes.MediaTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MediaTracker.Windows.MediaSeries
{
    /// <summary>Interaction logic for AiringWindow.xaml</summary>
    public partial class AiringWindow : INotifyPropertyChanged
    {
        private ListViewSort _sort = new ListViewSort();
        private List<Series> _series = new List<Series>();
        private Series _selectedSeries = new Series();

        internal MainWindow PreviousWindow { get; set; }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        /// <summary>Loads all series from the database.</summary>
        private void LoadSeries()
        {
            _series.Clear();
            List<Series> allSeries = AppState.AllSeries;
            _series.AddRange(allSeries.FindAll(series => series.Status == SeriesStatus.Airing));
            _series = new List<Series>(_series.OrderBy(series => series.Day).ThenBy(series => series.Time).ToList());
        }

        /// <summary>Refreshes the ListView's ItemsSource.</summary>
        internal void RefreshItemsSource(bool reload = false)
        {
            if (reload)
                LoadSeries();
            LVSeries.ItemsSource = _series;
            LVSeries.Items.Refresh();
            DataContext = _selectedSeries;
        }

        /// <summary>Toggles Buttons on the Window.</summary>
        /// <param name="toggle">Toggle</param>
        private void ToggleButtons(bool toggle)
        {
            BtnModifySeries.IsEnabled = toggle;
            BtnDeleteSeries.IsEnabled = toggle;
        }

        #region Click Methods

        private void LVSeriesColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            _sort = Functions.ListViewColumnHeaderClick(sender, _sort, LVSeries);
        }

        private void BtnNewSeries_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnModifySeries_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnDeleteSeries_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Click Methods

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        public AiringWindow()
        {
            InitializeComponent();
            LVSeries.ItemsSource = _series;
        }

        private void LVSeries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LVSeries.SelectedIndex >= 0)
                _selectedSeries = (Series)LVSeries.SelectedItem;
            else
                _selectedSeries = new Series();

            ToggleButtons(LVSeries.SelectedIndex >= 0);
            RefreshItemsSource();
        }

        #endregion Window-Manipulation Methods

        private void WindowAiring_Closed(object sender, EventArgs e)
        {
            PreviousWindow.Show();
        }
    }
}