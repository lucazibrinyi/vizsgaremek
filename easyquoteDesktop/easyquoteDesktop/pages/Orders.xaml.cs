using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace easyquoteDesktop.pages
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>

    /**
    * In this file:
    * 
    * @LoadGrid
    * @LoadComboboxes
    * @ClearBoxes
    * @ClearButton
    * @DeleteButton
    * @DeleteExecute
    * @UpdateButton
    * @UpdateExecute
    * @ShowConfirmationDialog
    * @SearchTextChanged
    * @DataGridDoubleClick
    * @DataGridSelectionChanged
    * @OpenNAVLink
    *
	**/
    public partial class Orders : Page
    {
        public Orders()
        {
            InitializeComponent();
            LoadGrid();
            LoadComboboxes();
        }

        /*--##----LOAD-SECTION----##--*/
        /**
         * Gets the list of "orders" from the database.
         * and loads it into the table.
         **/
        private void LoadGrid()
        {
            try
            {
                DataStorage.databaseConnectionString.Open();
                DataTable table = new DataTable();
                string query = "SELECT * FROM orders";
                MySqlCommand command = new MySqlCommand(query, DataStorage.databaseConnectionString);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);
                ordersDataGrid.ItemsSource = table.DefaultView;
                DataStorage.databaseConnectionString.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                DataStorage.databaseConnectionString.Close();
            }
        }


        /**
         * It retrieves the "ID" fields from the list of orders in the database and loads them into the ComboBox.
         * Fills the corresponding ComboBoxes with the words "Igen" and "Nem".
         **/
        private void LoadComboboxes()
        {
            try
            {
                DataStorage.databaseConnectionString.Open();
                string query = "SELECT id FROM orders";
                MySqlCommand command = new MySqlCommand(query, DataStorage.databaseConnectionString);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string id = reader.GetString(0);
                    combobox_orderId.Items.Add(id);
                }
                string[] yesOrNoList = DataStorage.GetActiveItems();
                foreach (string yesNo in yesOrNoList)
                {
                    combobox_accepted.Items.Add(yesNo);
                    combobox_survey.Items.Add(yesNo);
                    combobox_completed.Items.Add(yesNo);
                }
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
            textbox_city.Clear();
            textbox_address.Clear();
            textbox_postalCode.Clear();
            textbox_phoneNumber.Clear();
            combobox_orderId.Items.Clear();
            combobox_survey.Items.Clear();
            combobox_accepted.Items.Clear();
            combobox_completed.Items.Clear();
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
            string orderId = combobox_orderId.Text;
            if (InputTest.InputIdForDelete(orderId))
            {
                if (ShowConfirmationDialog("Biztosan törölni szeretné?", "Megerősítés"))
                {
                    DeleteExecute(orderId);
                }
            }
        }

        /**
         * Deletes the selected record from the "orders" table by SQL query.
         **/
        private void DeleteExecute(string orderId)
        {
            try
            {
                string query = "DELETE from orders WHERE id ='" + orderId + "';";
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
                MessageBox.Show("A törlés sikertelen: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                DataStorage.databaseConnectionString.Close();
            }
        }
        /**--------------------------------------------------------------------------------------------------------**/


        /*--##----UPDATE----##--*/
        /**
        * Pressing a button activates the function.
        * Starts the updating process.
        * Converts the string (Igen/Nem) to a bool value (1/0).
        **/
        private void UpdateButton(object sender, RoutedEventArgs e)
        {
            string city = textbox_city.Text;
            string address = textbox_address.Text;
            string postalCode = textbox_postalCode.Text;
            string phoneNumber = textbox_phoneNumber.Text;
            string orderId = combobox_orderId.Text;
            string survey = combobox_survey.Text;
            string accepted = combobox_accepted.Text;
            string completed = combobox_completed.Text;

            if (InputTest.InputStringTest(city, address, postalCode, phoneNumber, orderId, survey, accepted, completed))
            {
                if (MessageBox.Show("Biztosan szeretnéd felülírni?", "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    survey = (survey == "Igen") ? "1" : "0";
                    accepted = (accepted == "Igen") ? "1" : "0";
                    completed = (completed == "Igen") ? "1" : "0";

                    UpdateExecute(city, address, postalCode, phoneNumber, survey, accepted, completed, orderId);
                }
            }
        }

        /**
         * Updates the selected record in the "order" database by SQL query.
         * If "completed" is true, it adds the current date plus 15 days to the SQL query.
         **/
        private void UpdateExecute(string city, string address, string postalCode, string phoneNumber, string survey, string accepted, string completed, string orderId)
        {
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "";
            DateTime currentDate = DateTime.Now;
            DateTime modifiedDate = currentDate.AddDays(15);

            string formattedDatePlus15 = modifiedDate.ToString("yyyy-MM-dd");
            query += "UPDATE orders SET " +
                "city ='" + city + "', " +
                "address ='" + address + "', " +
                "postal_code ='" + postalCode + "', " +
                "phone_number ='" + phoneNumber + "', " +
                "survey ='" + survey + "', " +
                "accepted ='" + accepted + "', ";
            if (completed == "1")
            {
                query += "payment_deadline = '" + formattedDatePlus15 + "', ";
            }
            query += "completed ='" + completed + "', " +
                "updated_at = '" + formattedDateTime + "' " +
                "WHERE id = '" + orderId + "';";

            try
            {
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
                MessageBox.Show("A frissítés sikertelen: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                DataStorage.databaseConnectionString.Close();
            }
        }
        /**--------------------------------------------------------------------------------------------------------**/


        /*--##----CONFIRMATION-DIALOG----##--*/
        /**
         * Requests confirmation to perform the operation.
         **/
        private bool ShowConfirmationDialog(string message, string title)
        {
            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
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
                string query = "SELECT * " +
                    "FROM orders " +
                    "WHERE postal_code LIKE '" + searchKeyword + "' " +
                    "OR city LIKE '" + searchKeyword + "' " +
                    "OR address LIKE '" + searchKeyword + "' " +
                    "OR phone_number LIKE '" + searchKeyword + "' " +
                    "OR created_at LIKE '" + searchKeyword + "';";
                MySqlCommand command = new MySqlCommand(query, DataStorage.databaseConnectionString);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);
                ordersDataGrid.ItemsSource = dataTable.DefaultView;
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
            if (ordersDataGrid.SelectedItem != null && ordersDataGrid.SelectedItems.Count == 1)
            {
                DataRowView row = ordersDataGrid.SelectedItem as DataRowView;
                textbox_postalCode.Text = row["postal_code"].ToString();
                textbox_city.Text = row["city"].ToString();
                textbox_address.Text = row["address"].ToString();
                textbox_phoneNumber.Text = row["phone_number"].ToString();
                combobox_orderId.Text = row["id"].ToString();

                bool accepted = Convert.ToBoolean(row["accepted"]);
                bool survey = Convert.ToBoolean(row["survey"]);
                bool completed = Convert.ToBoolean(row["completed"]);

                combobox_accepted.Text = accepted ? "Igen" : "Nem";
                combobox_survey.Text = survey ? "Igen" : "Nem";
                combobox_completed.Text = completed ? "Igen" : "Nem";
            }
        }

        /**
         * The function runs when a row in the table is clicked once.
         * Queries the list of products for the selected order and loads it into the table below.
         **/
        private void DataGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ordersDataGrid.SelectedIndex >= 0)
            {
                DataRowView row = ordersDataGrid.SelectedItem as DataRowView;
                string orderId = row["id"].ToString();
                try
                {
                    DataStorage.databaseConnectionString.Open();
                    DataTable table = new DataTable();
                    string query = "SELECT op.id, op.order_id, op.product_id, op.quantity, p.name, p.category, p.price " +
                                   "FROM ordered_products AS op " +
                                   "INNER JOIN products AS p ON op.product_id = p.id " +
                                   "WHERE op.order_id = '" + orderId + "'";

                    MySqlCommand command = new MySqlCommand(query, DataStorage.databaseConnectionString);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(table);
                    table.Columns.Add("totalPrice", typeof(decimal));
                    foreach (DataRow rowB in table.Rows)
                    {
                        decimal quantity = Convert.ToDecimal(rowB["quantity"]);
                        decimal price = Convert.ToDecimal(rowB["price"]);
                        decimal totalPrice = quantity * price;
                        rowB["totalPrice"] = totalPrice;
                    }
                    orderedProductsDataGrid.ItemsSource = table.DefaultView;
                    DataStorage.databaseConnectionString.Close();

                    decimal finalPrice = 0;
                    foreach (DataRow rowC in table.Rows)
                    {
                        decimal totalPrice = Convert.ToDecimal(rowC["totalPrice"]);
                        finalPrice += totalPrice;
                    }
                    textbox_finalPrice.Text = finalPrice.ToString() + " Ft";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    DataStorage.databaseConnectionString.Close();
                }
            }
        }
        /**--------------------------------------------------------------------------------------------------------**/


        /*--##----OPEN-LINK----##--*/
        private void OpenNAVLink(object sender, RoutedEventArgs e)
        {
            LinkOpener.OpenNavLink();
        }
        /**--------------------------------------------------------------------------------------------------------**/
    }
}
