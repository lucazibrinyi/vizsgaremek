using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace easyquoteDesktop.pages
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>

    /**
    * In this file:
    *
    * @IfElse
    * @LoadGrid
    * @LoadComboboxes
    * @ClearBoxes
    * @ClearButton
    * @DeleteButton
    * @DeleteExecute
    * @ShowConfirmationDialog
    * @UpdateButton
    * @UpdateExecute
    * @SearchTextChanged
    * @SearchResult
    * @DataGridDoubleClick
    * @AddNewUSerButton
    *
	**/

    public partial class Users : Page
    {
        public Users()
        {
            InitializeComponent();
            LoadGrid();
            LoadComboboxes();
            BossOrNot();
        }


        /*--##----LOAD-SECTION----##--*/
        /**
         * Changes the visibility of elements.
         **/
        private void BossOrNot()
        {
            if (!MainWindow.bossOrNot)
            {
                textbox_name.IsEnabled = false;
                textbox_email.IsEnabled = false;
                combobox_employee.IsEnabled = false;
                combobox_id.IsEnabled = false;
                clear_button.SetValue(Grid.RowProperty, 4);

                label_newPassword.Visibility = Visibility.Hidden;
                label_newPasswordRepeat.Visibility = Visibility.Hidden;
                passwordbox_password.Visibility = Visibility.Hidden;
                passwordbox_passwordRepeat.Visibility = Visibility.Hidden;
                update_button.Visibility = Visibility.Hidden;
                delete_button.Visibility = Visibility.Hidden;
                addNewUser_button.Visibility = Visibility.Hidden;
            }
        }

        /**
         * It retrieves the "ID" fields from the list of orders in the database and loads them into the ComboBox.
         * Fills the corresponding ComboBoxes with the words "Igen" and "Nem".
        **/
        private void LoadGrid()
        {
            try
            {
                DataStorage.databaseConnectionString.Open();
                DataTable table = new DataTable();
                string query = "";
                query = "SELECT id, name, email, employee, created_at, updated_at FROM users";                
                MySqlCommand command = new MySqlCommand(query, DataStorage.databaseConnectionString);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);
                usersDataGrid.ItemsSource = table.DefaultView;
                DataStorage.databaseConnectionString.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                DataStorage.databaseConnectionString.Close();
            }
        }
        /**
        * It retrieves the "ID" fields from the list of users in the database and loads them into the ComboBox.
        * Fills the corresponding ComboBoxes with the words "Igen" and "Nem".
       **/
        private void LoadComboboxes()
        {
            try
            {

                DataStorage.databaseConnectionString.Open();
                string query = "SELECT id, name, email, employee FROM users";
                MySqlCommand command = new MySqlCommand(query, DataStorage.databaseConnectionString);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string id = reader.GetString(0);
                    combobox_id.Items.Add(id);
                }
                combobox_employee.Items.Add("Nem");
                combobox_employee.Items.Add("Igen");
                DataStorage.databaseConnectionString.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                DataStorage.databaseConnectionString.Close();
            }
        }
        /**--------------------------------------------------------------------------------------------------------**/


        /*--##----CLEAR-SECTION----##--*/
        /**
         * Empty TextBoxes and ComboBoxes.
         **/
        private void ClearBoxes()
        {
            textbox_name.Clear();
            textbox_email.Clear();
            combobox_id.Items.Clear();
            combobox_employee.Items.Clear();
            passwordbox_password.Clear();
            passwordbox_passwordRepeat.Clear();

            BossOrNot();
        }

        /**
         * Pressing a button activates the function.
         **/
        private void ClearButton(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
            LoadGrid();
            LoadComboboxes();
        }
        /**--------------------------------------------------------------------------------------------------------**/


        /*--##----DELETE----##--*/
        /**
         * Pressing a button activates the function.
         * Starts the deletion process.
         **/
        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            string userId = combobox_id.Text;
            if (InputTest.InputIdForDelete(userId))
            {
                if (ShowConfirmationDialog("Biztosan törölni szeretné?", "Megerősítés"))
                {
                    DeleteExecute(userId);
                }
            }
        }

        /**
         * Deletes the selected record from the "users" table by SQL query.
         **/
        private void DeleteExecute(string userId)
        {
            try
            {
                string query = "DELETE from users WHERE id ='" + userId + "';";
                MySqlCommand command = new MySqlCommand(query, DataStorage.databaseConnectionString);
                DataStorage.databaseConnectionString.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Sikeres törlés!", "Törölve", MessageBoxButton.OK, MessageBoxImage.Information);
                DataStorage.databaseConnectionString.Close();
                ClearBoxes();
                LoadGrid();
                LoadComboboxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("A törlés sikertelen! " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                DataStorage.databaseConnectionString.Close();
            }
        }
        /**--------------------------------------------------------------------------------------------------------**/


        /*--##----CONFIRMATION-DIALOG----##--*/
        private bool ShowConfirmationDialog(string message, string title)
        {
            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }
        /**--------------------------------------------------------------------------------------------------------**/


        /*--##----UPDATE----##--*/
        /**
        * Pressing a button activates the function.
        * Starts the updating process.
        * Converts the string (Igen/Nem) to a bool value (1/0).
        * If a password is set, it will be sent for verification.
        **/
        private void UpdateButton(object sender, RoutedEventArgs e)
        {
            string name = textbox_name.Text;
            string email = textbox_email.Text;
            string employee = combobox_employee.Text;
            string userId = combobox_id.Text;
            string password = passwordbox_password.Password;
            string passwordRepeat = passwordbox_passwordRepeat.Password;

            if (InputTest.InputStringTest(name, email, employee, userId))
            {
                if (password != "" || passwordRepeat != "")
                {
                    if (!InputTest.InputPasswordEquals(password, passwordRepeat) || !InputTest.InputPasswordMinimumLongTest(password, 8))
                    {
                        return;
                    }
                }
                if (ShowConfirmationDialog("Biztosan módosítani szeretné?", "Megerősítés"))
                {
                    employee = (employee == "Igen") ? "1" : "0";

                    UpdateExecute(name, email, employee, userId, password);
                }
            }   
        }
        
        /**
         * Updates the selected record in the "users" database by SQL query.
         **/
        private void UpdateExecute(string name, string email, string employee, string userId, string password)
        {
            try
            {
                DateTime currentDateTime = DateTime.Now;
                string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                string query = "UPDATE users SET " +
                    "name ='" + name + "', " +
                    "email ='" + email + "', " +
                    "employee ='" + employee + "', ";
                if (password != "")
                {
                    string hashedPassword = HashStringMd5.Md5hash(password);
                    query += "password = '" + hashedPassword + "', ";
                }
                query += "updated_at = '" + formattedDateTime + "' " +
                    "WHERE id = '" + userId + "';";
                MySqlCommand command = new MySqlCommand(query, DataStorage.databaseConnectionString);
                DataStorage.databaseConnectionString.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("A bejegyzés sikeresen frissítve!", "Frissítve", MessageBoxButton.OK, MessageBoxImage.Information);
                DataStorage.databaseConnectionString.Close();
                ClearBoxes();
                LoadGrid();
                LoadComboboxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("A frissítés nem sikerült! " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                DataStorage.databaseConnectionString.Close();
            }
        }
        /**--------------------------------------------------------------------------------------------------------**/


        /*--##----SEARCH----##--*/
        /**
         * Retrieves records from the database that match the value entered in the search field.
         **/
        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchKeyword = "%" + txt_search.Text + "%";
            SearchResult(searchKeyword);
        }

        private void SearchResult(string searchKeyword)
        {
            try
            {
                DataTable dataTable = new DataTable();
                string query = "SELECT id, name, email, employee, created_at, updated_at " +
                    "FROM users " +
                    "WHERE name LIKE '" + searchKeyword + "' " +
                    "OR email LIKE '" + searchKeyword + "';";
                MySqlCommand command = new MySqlCommand(query, DataStorage.databaseConnectionString);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);
                usersDataGrid.ItemsSource = dataTable.DefaultView;
                DataStorage.databaseConnectionString.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                DataStorage.databaseConnectionString.Close();
            }
        }
        /**--------------------------------------------------------------------------------------------------------**/


        /*--##----DOUBLE-CLICK----##--*/
        /**
         * The function runs when a row in the table is double-clicked.
         * It copies the data in the row of the table into the TextBoxes and ComboBoxes.
         **/
        private void DataGridDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (usersDataGrid.SelectedItem != null && usersDataGrid.SelectedItems.Count == 1)
            {
                DataRowView row = usersDataGrid.SelectedItem as DataRowView;
                textbox_name.Text = row["Name"].ToString();
                textbox_email.Text = row["Email"].ToString();
                combobox_id.Text = row["ID"].ToString();

                bool employee = Convert.ToBoolean(row["Employee"]);
                combobox_employee.Text = employee ? "Igen" : "Nem";
                                
                if (MainWindow.bossOrNot)
                {
                    combobox_employee.IsEnabled = !textbox_email.Text.StartsWith("boss@");
                    textbox_email.IsEnabled = !textbox_email.Text.StartsWith("boss@");

                    bool isBossOrIrodaEmail = textbox_email.Text.StartsWith("boss@") || textbox_email.Text.StartsWith("iroda");
                    passwordbox_password.IsEnabled = isBossOrIrodaEmail;
                    passwordbox_passwordRepeat.IsEnabled = isBossOrIrodaEmail;
                }                
            }
        }
        /**--------------------------------------------------------------------------------------------------------**/


        /*--##----OPEN-NEW-USER-WINDOW----##--*/
        /**
         * It opens the add new user window.
         **/
        private void AddNewUSerButton(object sender, RoutedEventArgs e)
        {
            NewUser openAddNewUserWindow = new NewUser();
            openAddNewUserWindow.Show();

        }
        /**--------------------------------------------------------------------------------------------------------**/
    }
}
