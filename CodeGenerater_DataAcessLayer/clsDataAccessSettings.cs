using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerater_DataAcessLayer
{
    public class clsDataAccessSettings
    {

        public static string DataBase = "DVLD", UserID = "", Password = "";


        public static string connectionString;

        public static void SetCredetials(string dataBase, string userID, string password)
        {
            DataBase = dataBase;
            UserID = userID;
            Password = password;

            connectionString = @"Server =.;DataBase =" + DataBase + ";User ID =" + UserID + ";Password =" + Password;
        }


        public static bool IsConnationInfoCurrect()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {

                    connection.Open();
                    //var Result = command.ExecuteNonQuery();
                    using (SqlCommand d = new SqlCommand("Select 1 AS Connaction", connection))
                    {
                        var result = d.ExecuteScalar();
                        if (result != null)
                            return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
