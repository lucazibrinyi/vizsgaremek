using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyquoteDesktop
{

    /**
    * In this file:
    * 
    * $databaseConnectionString
    * @GetActiveItems
    * @GetCategoryItems
    * @GetSubCategoryItems
    * 
    * **/

    public static class DataStorage
    {
        /*--A variable, available to other parts of the program, containing the database connection details.--*/
        public static MySqlConnection databaseConnectionString = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;database=easyquote_web");


        /*--For "Nem" => "false", "Igen" => "true" for database--*/
        public static string[] GetActiveItems()
        {
            return new string[] { "Nem", "Igen" };
        }

        /*--Prouduct categories--*/
        public static string[] GetCategoryItems()
        {
            return new string[] { "egyeb", "kamera", "riaszto" };
        }

        /*--Prouduct subcategories--*/
        public static string[] GetSubCategoryItems()
        {
            return new string[] { "egyeb", "kezelo", "kamera", "kozpont", "szirena", "rogzito", "hattertarak", "mozgaserzekelo", "kamerakiegeszito", "riasztokiegészíto" };
        }
    }
}
