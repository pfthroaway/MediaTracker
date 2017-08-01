﻿using MediaTracker.Classes;
using MediaTracker.Classes.Enums;
using MediaTracker.Classes.MediaTypes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MediaTracker.Pages.MediaSeries
{
    /// <summary>Interaction logic for HiatusPage.xaml</summary>
    public partial class HiatusPage
    {
        private ListViewSort _sort = new ListViewSort();
        private List<Series> _series = new List<Series>();
        private Series _selectedSeries = new Series();

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        /// <summary>Refreshes the ListView's ItemsSource.</summary>
        internal void RefreshItemsSource()
        {
            _series = AppState.AllSeries.FindAll(series => series.Status == SeriesStatus.Hiatus)
                .OrderBy(series => series.Day).ThenBy(series => series.Time).ThenBy(series => series.Name).ToList();
            LVSeries.ItemsSource = _series;
            LVSeries.Items.Refresh();
            DataContext = _selectedSeries;
        }

        /// <summary>Toggles Buttons on the Page.</summary>
        /// <param name="toggle">Toggle</param>
        private void ToggleButtons(bool toggle)
        {
            BtnModifySeries.IsEnabled = toggle;
            BtnDeleteSeries.IsEnabled = toggle;
        }

        #region Click Methods

        private void LVSeriesColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            _sort = Functions.ListViewColumnHeaderClick(sender, _sort, LVSeries, "#CCCCCC");
        }

        private void BtnNewSeries_Click(object sender, RoutedEventArgs e)
        {
            AppState.Navigate(new NewSeriesPage());
        }

        private void BtnModifySeries_Click(object sender, RoutedEventArgs e)
        {
            AppState.Navigate(new ModifySeriesPage { SelectedSeries = _selectedSeries });
        }

        private async void BtnDeleteSeries_Click(object sender, RoutedEventArgs e)
        {
            if (await AppState.DeleteSeries(_selectedSeries))
                RefreshItemsSource();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }

        #endregion Click Methods

        #region Window-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage()
        {
            AppState.GoBack();
        }

        public HiatusPage()
        {
            InitializeComponent();
        }

        private void LVSeries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedSeries = LVSeries.SelectedIndex >= 0 ? (Series)LVSeries.SelectedItem : new Series();

            ToggleButtons(LVSeries.SelectedIndex >= 0);
            DataContext = _selectedSeries;
        }

        private void HiatusPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            AppState.CalculateScale(Grid);
            RefreshItemsSource();
        }

        #endregion Window-Manipulation Methods
    }
}