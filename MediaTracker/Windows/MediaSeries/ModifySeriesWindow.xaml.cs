using Extensions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Extensions.Enums;

namespace MediaTracker.Windows.MediaSeries
{
    /// <summary>Interaction logic for ModifySeriesWindow.xaml</summary>
    public partial class ModifySeriesWindow
    {
        public ModifySeriesWindow()
        {
            InitializeComponent();
        }

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        private void WindowModifySeries_Closed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #region Text/Selection Changed

        private void TxtTextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void DecimalTextChanged(object sender, TextChangedEventArgs e)
        {
            Functions.TextBoxTextChanged(sender, KeyType.Decimals);
        }

        private void IntTextChanged(object sender, TextChangedEventArgs e)
        {
            Functions.TextBoxTextChanged(sender, KeyType.Integers);
        }

        private void CmbSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        #endregion Text/Selection Changed

        #region Click

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnSaveNew_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnSaveExit_Click(object sender, RoutedEventArgs e)
        {
        }

        #endregion Click

        #region PreviewKeyDown

        private void Decimal_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Functions.PreviewKeyDown(e, KeyType.Decimals);
        }

        private void Integer_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Functions.PreviewKeyDown(e, KeyType.Integers);
        }

        private void Date_PreviewKeyDown(object sender, KeyEventArgs e)
        {
        }

        #endregion PreviewKeyDown

        #region GotFocus

        private void Txt_GotFocus(object sender, RoutedEventArgs e)
        {
            Functions.TextBoxGotFocus(sender);
        }

        #endregion GotFocus
    }
}