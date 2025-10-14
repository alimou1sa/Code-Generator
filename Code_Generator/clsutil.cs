using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Classes
{
    public class clsUtil
    {


        public static string GenerateGUID()
        {

            // Generate a new GUID
            Guid newGuid = Guid.NewGuid();

            // convert the GUID to a string
            return newGuid.ToString();
            
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
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }

            return true;
            
        }
     
        public static string ReplaceFileNameWithGUID(string sourceFile)
        {
            // Full file name. Change your file name   
            string fileName = sourceFile;
            FileInfo fi = new FileInfo(fileName);
            string extn = fi.Extension;
            return GenerateGUID() + extn;

        }
       
        public static  bool CopyImageToProjectImagesFolder(ref string  sourceFile)
        {
            // this funciton will copy the image to the
            // project images foldr after renaming it
            // with GUID with the same extention, then it will update the sourceFileName with the new name.

            string DestinationFolder = @"C:\DVLD-People-Images\";
            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }

            string destinationFile = DestinationFolder + ReplaceFileNameWithGUID(sourceFile);
            try
            {
                File.Copy(sourceFile, destinationFile, true);
                

            }
            catch (IOException iox)
            {
                MessageBox.Show (iox.Message,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            sourceFile= destinationFile;
            return true;
        }



        static string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\GodeGenerater";
        public static bool RememberUsernameAndPasswordByRegjistry(string UserID, string Password)
        {

            string valueName1 = "UserID";
            string valueData1 = UserID;

            string valueName2 = "Password";
            string valueData2 = Password;

            try
            {
                // Write the value to the Registry
                Registry.SetValue(keyPath, valueName1, valueData1, RegistryValueKind.String);

                Registry.SetValue(keyPath, valueName2, valueData2, RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }


        }
        public static bool GetStoredCredentialByRegjistry(ref string UserID, ref string Password)
        {

            string valueName1 = "UserID";
            string valueName2 = "Password";

            try
            {
                // Read the value from the Registry
                string value1 = Registry.GetValue(keyPath, valueName1, null) as string;
                string value2 = Registry.GetValue(keyPath, valueName2, null) as string;

                if (value1 != null)
                {
                    UserID = value1;

                    if (value2 != null)
                    {
                        Password = value2;

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
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
