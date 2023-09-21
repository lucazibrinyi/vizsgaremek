using System;
using System.Diagnostics;
using System.Windows;

namespace easyquoteDesktop
{
    /**
    * In this file:
    * 
    * @OpenNavLink
    * 
    * **/

    public static class LinkOpener
    {
        /**
         * Opens the link in the default browser.
        **/
        public static void OpenNavLink()
        {
            try
            {
                string url = "https://onlineszamla.nav.gov.hu/home";

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
