using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace easyquoteDesktop.pages
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>

    /**
    * In this file:
    *    
    * @LoadGrid
    * @LoadComboboxes
    * @ClearBoxes
    * @ClearButton
    * @UpdateButton
    * @UpdateExecute
    * @SearchTextChanged
    * @SearchResult
    * @DataGridDoubleClick
    * @ImageUploadButton
    * @ChangeImageUrl 
    * @AddNewProductButton
    *
	**/

    public partial class Products : Page
    {
        public Products()
        {
            InitializeComponent();
            LoadGrid();
            LoadComboboxes();
        }

        /*--##----LOAD----##--*/
        /**
         * Gets the list of "products" from the database.
         * and loads it into the table.
         **/
        private void LoadGrid()
        {
            try
            {
                DataStorage.databaseConnectionString.Open();
                DataTable table = new DataTable();
                string query = "SELECT id, name, active, category, sub_category, img_url, description, price FROM products";
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
        * It retrieves the "ID" fields from the list of products in the database and loads them into the ComboBox.
        * Fills the corresponding ComboBoxes with the words "Igen", "Nem" and categories.
        **/
        private void LoadComboboxes()
        {
            try
            {
                DataStorage.databaseConnectionString.Open();
                string query = "SELECT id FROM products";
                MySqlCommand command = new MySqlCommand(query, DataStorage.databaseConnectionString);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string id = reader.GetString(0);
                    combobox_id.Items.Add(id);
                }
                DataStorage.databaseConnectionString.Close();

                string[] activeItems = DataStorage.GetActiveItems();
                string[] categoryItems = DataStorage.GetCategoryItems();                
                string[] subCategoryItems = DataStorage.GetSubCategoryItems();

                foreach (string item in activeItems)
                    combobox_active.Items.Add(item);
                foreach (string item in categoryItems)
                    combobox_category.Items.Add(item);
                foreach (string item in subCategoryItems)
                    combobox_subCategory.Items.Add(item);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                DataStorage.databaseConnectionString.Close();
            }
        }
        /**--------------------------------------------------------------------------------------------------------**/


        /*--##----CLEAR----##--*/
        /**
         * Empty TextBoxes and ComboBoxes.
         **/
        private void ClearBoxes()
        {
            textbox_name.Clear();
            textbox_description.Clear();
            textbox_price.Clear();
            combobox_id.Items.Clear();
            combobox_active.Items.Clear();
            combobox_category.Items.Clear();
            combobox_subCategory.Items.Clear();
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


        /*--##----UPDATE----##--*/
        /**
        * Pressing a button activates the function.
        * Starts the updating process.
        * Converts the string (Igen/Nem) to a bool value (1/0).
        **/
        private void UpdateButton(object sender, RoutedEventArgs e)
        {
            string name = textbox_name.Text;
            string description = textbox_description.Text;
            string price = textbox_price.Text;
            string category = combobox_category.Text;
            string subCategory = combobox_subCategory.Text;
            string active = combobox_active.Text;
            string productId = combobox_id.Text;

            if (InputTest.InputStringTest(name, description, price, category, subCategory, active, productId))
            {
                if (MessageBox.Show("Biztosan szeretnéd felülírni?", "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    active = (active == "Igen") ? "1" : "0";

                    UpdateExecute(name, description, price, category, subCategory, active, productId);
                }
            }
        }

        /**
         * Updates the selected record in the "products" database by SQL query.
         **/
        private void UpdateExecute(string name, string description, string price, string category, string subCategory, string active, string productId)
        {
            string query = "UPDATE products SET " +
                        "name ='" + name + "', " +
                        "description ='" + description + "', " +
                        "price ='" + price + "', " +
                        "category ='" + category + "', " +
                        "sub_category ='" + subCategory + "', " +
                        "active ='" + active + "' " +
                        "WHERE id = '" + productId + "';";
            MySqlCommand command = new MySqlCommand(query, DataStorage.databaseConnectionString);
            try
            {
                DataStorage.databaseConnectionString.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("A termék sikeresen frissítve!", "Frissítve", MessageBoxButton.OK, MessageBoxImage.Information);
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
                string query = "SELECT id, name, active, category, sub_category, img_url, description, price " +
                    "FROM products " +
                    "WHERE name LIKE '" + searchKeyword + "' " +
                    "OR category LIKE '" + searchKeyword + "' " +
                    "OR sub_category LIKE '" + searchKeyword + "' " +
                    "OR description LIKE '" + searchKeyword + "';";
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
                textbox_description.Text = row["description"].ToString();
                textbox_price.Text = row["price"].ToString();
                combobox_id.Text = row["ID"].ToString();
                combobox_category.Text = row["category"].ToString();
                combobox_subCategory.Text = row["sub_category"].ToString();

                bool active = Convert.ToBoolean(row["active"]);
                combobox_active.Text = active ? "Igen" : "Nem";
            }
        }

        /**--------------------------------------------------------------------------------------------------------**/


        /*--##----IMG-UPLOAD----##--*/
        /**
         * Opens the upload new photo window for the selected product.
         * Appends the current time to the file name.
         **/
        private async void ImageUploadButton(object sender, RoutedEventArgs e)
        {
            if (combobox_id.Text != "")
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Images|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

                if (openFileDialog.ShowDialog() == true)
                {
                    // path of the selected file
                    string selectedFilePath = openFileDialog.FileName;

                    string currentDateTime = DateTime.Now.ToString("yyyyMMdd.HHmmss");
                    string originalFileNameWithoutExtension = Path.GetFileNameWithoutExtension(selectedFilePath);
                    string fileExtension = Path.GetExtension(selectedFilePath);
                    string newFileName = $"{originalFileNameWithoutExtension}_{currentDateTime}{fileExtension}";

                    // sends the file to the API
                    using (var client = new HttpClient())
                    using (var formData = new MultipartFormDataContent())
                    {
                        // the URL of the API
                        string apiUrl = "http://localhost:8000/api/upload";

                        // add file to HTTP request
                        var fileContent = new ByteArrayContent(File.ReadAllBytes(selectedFilePath));
                        formData.Add(fileContent, "photo", newFileName);

                        // send the HTTP POST request
                        var response = await client.PostAsync(apiUrl, formData);

                        if (response.IsSuccessStatusCode)
                        {
                            // if the upload is successful, we send the file name to the function that manages the database
                            string id = combobox_id.Text;
                            string selectedFileName = newFileName;

                            ChangeImageUrl(id, selectedFileName);
                        }
                        else
                        {
                            MessageBox.Show("Fájl feltöltése sikertelen: " + await response.Content.ReadAsStringAsync(), "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Termékfotó módosításhoz a termék ID kiválasztása kötelező!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /*
         * Replaces the "img_url" value in the database with the new value.
         **/
        private void ChangeImageUrl(string id, string fileName)
        {
            string img_url = fileName;
            string query = "UPDATE products SET img_url ='" + img_url + "' WHERE id = '" + id + "';";
            MySqlCommand command = new MySqlCommand(query, DataStorage.databaseConnectionString);
            try
            {
                DataStorage.databaseConnectionString.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("A termék sikeresen frissítve!", "Frissítve", MessageBoxButton.OK, MessageBoxImage.Information);
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


        /*--##----OPEN-ADD-NEW-PRODUCT-WINDOW----##--*/
        /**
         * It opens the add new product window.
         **/
        private void AddNewProductButton(object sender, RoutedEventArgs e)
        {
            AddNewProduct openAddNewProductWindow = new AddNewProduct();
            openAddNewProductWindow.Show();
        }
        /**--------------------------------------------------------------------------------------------------------**/
    }
}
