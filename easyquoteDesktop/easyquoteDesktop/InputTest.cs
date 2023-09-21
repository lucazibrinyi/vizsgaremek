using System;
using System.Net.Mail;
using System.Windows;

namespace easyquoteDesktop
{

    /**
    * In this file:
    * 
    * @InputStringTest
    * @InputPasswordMinimumLongTest
    * @InputEmailIsValid
    * @InputIdForDelete
    * 
    * **/

    public static class InputTest
    {


        /*--The input strings are checked whether they are empty or not. If empty, a message pops up.--*/
        public static bool InputStringTest(params string[] inputs)
        {
            foreach (string input in inputs)
            {
                if (string.IsNullOrEmpty(input))
                {
                    MessageBox.Show("Minden mező kitöltése kötelező!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            return true;
        }

        public static bool InputPasswordMinimumLongTest(string password, int minLong)
        {
            if (password.Length < minLong)
            {
                MessageBox.Show($"A jelszónak legalább {minLong} karakter hosszúnak kell lenni!", "Figyelmeztetés!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        public static bool InputPasswordEquals(string password1, string password2)
        {
            if (password1 != password2)
            {
                MessageBox.Show("A jelszavak nem egyeznek!", "Figyelmeztetés!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        /*--Checks the text to see if it meets the general rules for email addresses--*/
        public static bool InputEmailIsValid(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Nem valid email cím!", "Figyelmeztetés!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }


        public static bool InputIdForDelete(string id)
        {
            if (id == "")
            {
                MessageBox.Show("Az ID mező kitöltése kötelező!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
    }
}
