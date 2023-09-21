using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Net.Http;
using System.Windows;

namespace easyquoteDesktop
{
    /// <summary>
    /// Interaction logic for AddNewProduct.xaml
    /// </summary>

    /**
    * In this file:
    *    
    * $selectedFilePath
    * @LoadComboboxes
    * @SaveNewProductButton
    * @ImageUpload
    * @SaveNewProduct
    *
	**/

    public partial class AddNewProduct : Window
    {
        public AddNewProduct()
        {
            InitializeComponent();
            LoadComboboxes();
        }

        /*--Used for image path and image URL.--*/
        string selectedFilePath = "";


        /*--##----LOAD-SECTION----##--*/
        /**
         * Fills the corresponding ComboBoxes with the categories.
         **/
        private void LoadComboboxes()
        {
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
        /**--------------------------------------------------------------------------------------------------------**/


        /*--##----SAVE-NEW-PRODUCT----##--*/
        /**
         * Pressing a button activates the function.
         * You are offered the option of choosing a photo.
         **/
        private void SaveNewProductButton(object sender, RoutedEventArgs e)
        {
            string productName = textbox_productName.Text;
            string description = textbox_description.Text;
            string price = textbox_price.Text;
            string category = combobox_category.Text;
            string subCategory = combobox_subCategory.Text;
            string active = combobox_active.Text;
            if (InputTest.InputStringTest(productName, description, price, category, subCategory, active))
            {
                active = (active == "Igen") ? "1" : "0";

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Images|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.ShowDialog();
                selectedFilePath = openFileDialog.FileName;

                if (selectedFilePath != "")
                {
                    ImageUpload(selectedFilePath);
                    SaveNewProduct(productName, description, price, category, subCategory, active, selectedFilePath);
                }
                else
                {
                    SaveNewProduct(productName, description, price, category, subCategory, active, selectedFilePath);
                }

            }
        }
        /**--------------------------------------------------------------------------------------------------------**/


        /*--##----IMG-UPLOAD----##--*/
        /**
         * Opens the upload new photo window for the new product.
         * Appends the current time to the file name.
         **/
        private async void ImageUpload(string filePath)
        {
            string currentDateTime = DateTime.Now.ToString("yyyyMMdd.HHmmss");
            string originalFileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            string fileExtension = Path.GetExtension(filePath);
            string newFileName = $"{originalFileNameWithoutExtension}_{currentDateTime}{fileExtension}";
            selectedFilePath = newFileName;

            // sends the file to the API
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                // the URL of the API
                string apiUrl = "http://localhost:8000/api/upload";

                // add file to HTTP request
                var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
                formData.Add(fileContent, "photo", newFileName);

                // send the HTTP POST request
                var response = await client.PostAsync(apiUrl, formData);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Fájl feltöltése sikertelen: " + await response.Content.ReadAsStringAsync(), "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        /**--------------------------------------------------------------------------------------------------------**/


        /*--##----IMG-UPLOAD----##--*/
        /**
         * It saves the data of the new product in the "products" table.
         **/
        private void SaveNewProduct(string productName, string description, string price, string category, string subCategory, string active, string selectedFilePath)
        {
            string query = "INSERT INTO products SET " +
                        "name ='" + productName + "', " +
                        "description ='" + description + "', " +
                        "price ='" + price + "', " +
                        "category ='" + category + "', " +
                        "sub_category ='" + subCategory + "', ";
            if (selectedFilePath != "")
            {
                query += "img_url ='" + selectedFilePath + "', ";
            }
            query += "active ='" + active + "';";

            MySqlCommand command = new MySqlCommand(query, DataStorage.databaseConnectionString);
            try
            {
                DataStorage.databaseConnectionString.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("A termék sikeresen létrehozva!", "Frissítve", MessageBoxButton.OK, MessageBoxImage.Information);
                DataStorage.databaseConnectionString.Close();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("A termék létrehozása nem sikerült: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                DataStorage.databaseConnectionString.Close();
            }
        }
        /**--------------------------------------------------------------------------------------------------------**/
    }
}
