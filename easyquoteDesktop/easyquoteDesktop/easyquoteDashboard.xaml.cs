using easyquoteDesktop.pages;
using System;
using System.Diagnostics;
using System.Windows;

namespace easyquoteDesktop
{
    /// <summary>
    /// Interaction logic for easyquoteDashboard.xaml
    /// </summary>

    /**
    * In this file:
    * 
    * CloseAppButton
    * OpenAboutWindow
    * @OpenNAVLink
    * @OpenOrdersPage
    * OpenUsersPage
    * OpenProductsPage
    * 
    **/

    public partial class EasyquoteDashboard : Window
    {
        public EasyquoteDashboard()
        {
            InitializeComponent();
        }

        /*--##----MENU-BUTTONS----##--*/
        /**
         * Functions are executed at the press of a menu button.
         **/
        private void OpenOrdersPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Orders();
        }

        private void OpenNAVLink(object sender, RoutedEventArgs e)
        {
            LinkOpener.OpenNavLink();
        }

        private void OpenProductsPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Products();
        }

        private void OpenUsersPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Users();
        }

        private void CloseAppButton(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void OpenAboutWindow(object sender, RoutedEventArgs e)
        {
            AboutWindow OpenAboutWindow = new AboutWindow();
            OpenAboutWindow.Show();
        }
        /**--------------------------------------------------------------------------------------------------------**/
    }
}
