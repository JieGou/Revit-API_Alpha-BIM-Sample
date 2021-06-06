using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace AlphaBIM
{
    public partial class ElevationFloorWindow
    {
        private ElevationFloorViewModel _viewModel;
        public ElevationFloorWindow(ElevationFloorViewModel viewModel)
        {
            InitializeComponent();
            _viewModel= viewModel;
            DataContext = viewModel;
            TextBox.Focus();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SetTopElevation();
            DialogResult = true;
            Close();
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Height = MainPanel.ActualHeight + 60;
            MinHeight = MainPanel.ActualHeight + 60;
        }
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                _viewModel.SetTopElevation();
                DialogResult = true;
                Close();
            }
            if (e.Key == Key.Escape)
            {
                DialogResult = false;
                Close();
            }
        }

        #region Copy Title bar

        private void OpenWebSite(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("https://alphabimvn.com/vi");
            }
            catch (Exception)
            {
            }
        }

        private void CustomDevelopment(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("http://bit.ly/3bNeJek");
            }
            catch (Exception)
            {
            }
        }

        private void Feedback(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("mailto:contact@alphabimvn.com");
            }
            catch (Exception)
            {
            }
        }

        #endregion Copy Title bar
    }
}
