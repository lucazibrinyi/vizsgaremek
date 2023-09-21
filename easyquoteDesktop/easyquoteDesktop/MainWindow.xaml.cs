using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace easyquoteDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    /**
    * In this file:
    * 
    * $databaseConnectionString
    * $bossOrNot
    * @Md5hash
    * @LoginButton
    * 
    **/

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }        

        /*--Variable available to other parts of the program, needed for privilege management.--*/
        public static bool bossOrNot;

        /*--##----LOGIN----##--*/
        /**
         * The function is executed at the press of a button. It compares the entered email address and password with data stored in the database. It hashes the password.
         **/
        private void LoginButton(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = textbox_email.Text;
                DataStorage.databaseConnectionString.Open();
                string query = "SELECT password FROM users WHERE email='" + email + "';";
                MySqlCommand adat = new MySqlCommand(query, DataStorage.databaseConnectionString);
                MySqlDataReader reader = adat.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    string hashedPassword = HashStringMd5.Md5hash(passwordbox_password.Password);
                    if (reader.GetString(0) == hashedPassword)
                    {
                        EasyquoteDashboard easyquoteDashboardOpen = new EasyquoteDashboard();
                        easyquoteDashboardOpen.Show();
                        DataStorage.databaseConnectionString.Close();
                        bossOrNot = email.StartsWith("boss@");
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Hibás felhasználónév, vagy jelszó!", "Figyelmeztetés!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        DataStorage.databaseConnectionString.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Hibás felhasználónév, vagy jelszó!", "Figyelmeztetés!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    DataStorage.databaseConnectionString.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                DataStorage.databaseConnectionString.Close();
            }
        }
        /**--------------------------------------------------------------------------------------------------------**/
    }
}
