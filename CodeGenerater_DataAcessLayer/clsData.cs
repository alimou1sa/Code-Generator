using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerater_DataAcessLayer
{
    public class clsData
    {

        public static void CreatFileCs(string bath, string Data)
        {
            File.WriteAllText(bath, Data);
        }

        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {

            // Check if the folder exists
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    // If it doesn't exist, create the folder
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {

                    return false;
                }
            }

            return true;

        }

        public static string CreateProjectDirectory(string Projectpath)
        {

            if (!Directory.Exists(Projectpath))
            {
                Directory.CreateDirectory(Projectpath);

            }


            string path = Path.Combine( Projectpath);


            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

            }


            return path;



        }


    }
}
