using MySql.Data.MySqlClient;
using System;
using System.Net.Mail;
using System.Windows;

namespace easyquoteDesktop
{
    /// <summary>
    /// Interaction logic for NewUser.xaml
    /// </summary>

    /**
     * In this file:
     * 
     * @Md5hash
     * @SaveNewUserButton
     * @InputTest
     * @IsValidEmail
     * 
     * **/

    public partial class NewUser : Window
    {
        public NewUser()
        {
            InitializeComponent();
        }


        /*--##----SAVE-NEW-USER----##--*/
        /** 
         * Pressing a button activates the function
         * Creates a new record in the database from the data received from the user.
         * Adds the current time to the SQL command. 
         **/
        private void SaveNewUserButton(object sender, RoutedEventArgs e)
        {
            string name = textbox_email.Text;
            string email = textbox_email.Text;
            string password = passwordbox_password.Password;
            string passwordRepeat = passwordbox_passwordRepeat.Password;

            if (InputTesting(name, email, password, passwordRepeat))
            {
                try
                {
                    DateTime currentDateTime = DateTime.Now;
                    string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    string hashedPassword = HashStringMd5.Md5hash(passwordbox_password.Password);
                    string query = "INSERT INTO users SET " +
                        "name ='" + name + "', " +
                        "email ='" + email + "', " +
                        "password ='" + hashedPassword + "', " +
                        "updated_at = '" + formattedDateTime + "', " +
                        "created_at = '" + formattedDateTime + "';";
                    MySqlCommand command = new MySqlCommand(query, DataStorage.databaseConnectionString);
                    DataStorage.databaseConnectionString.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("A felhasználó sikeresen létrehozva!", "Frissítve", MessageBoxButton.OK, MessageBoxImage.Information);
                    DataStorage.databaseConnectionString.Close();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    DataStorage.databaseConnectionString.Close();
                }
            }
        }
        /**--------------------------------------------------------------------------------------------------------**/


        /*--##----INPUT-TESTING----##--*/
        /**
         * Checks that input fields are correctly filled
         **/
        private bool InputTesting(string name, string email, string password, string passwordRepeat)
        {
            string requiredPrefix = "iroda";
            if (!(InputTest.InputStringTest(name, email, password, passwordRepeat)))
            {                
                return false;
            }
            if (!email.StartsWith(requiredPrefix))
            {
                MessageBox.Show("Az email címnek az 'iroda' előtaggal kell kezdődnie.", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!InputTest.InputEmailIsValid(email))
            {                
                return false;
            }
            if (!InputTest.InputPasswordMinimumLongTest(password, 8))
            {                
                return false;
            }
            if (!InputTest.InputPasswordEquals(password, passwordRepeat))
            {                
                return false;
            }
            return true;
        }
        
        /**--------------------------------------------------------------------------------------------------------**/
    }
}
