using CodeGenerater_DataAcessLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace CodeGenerator_BusinessLayer
{
    public class clsGenerator
    {


        public static clsTable table;

        private static string _Path = "C:\\Users\\TOPTECH\\source\\C#\\";

        public static string Path {
            get
            {
              return  _Path;
            }
            set { _Path = value; }
        }
       
        private static clsTable _tablesInfo;

        private static List<clsTable> _TablesInfos;

        public static void SetConnactionInfo(string dataBase, string userID, string password)
        {
            clsDataAccessSettings.SetCredetials(dataBase, userID, password);
        }

        public static bool IsCredetialsCorrect()
        {
            return clsDataAccessSettings.IsConnationInfoCurrect();
        }

        private static string _CreatFormatDataClas(string ProjrctName, string ClassType,string TableName)
        {
            string FormatClass;



            FormatClass= @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace " + ProjrctName + "_" + ClassType + @"
{
    public class cls" + TableName+@"
    {    ";
            
                        return FormatClass;

        }

        //-------------------------Data Access Layer ---------------------------------


        private static string _CreatFuncShowListTableInfoData(string TableName)
        {
            string CreateShowListTables = "";
  
            CreateShowListTables =
        @"           public static async Task<DataTable> GetAll" + TableName+ @"()
                      {

             DataTable dt = new DataTable();

              try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {             
                       await connection.OpenAsync();

             " + $@" string query = ""select * from {TableName}" + " \" ;"  + @"

                 using (  SqlCommand command = new SqlCommand(query, connection)){

      
               
   
           
                  SqlDataReader reader = await command.ExecuteReaderAsync();
           
                  if (reader.HasRows)
           
                  {
                      dt.Load(reader);
                  }
           
                  reader.Close();
           


                    }

                }

            }

            catch (SqlException ex)
            {

                clsErrorEventLog.LogError(ex.Message);
                dt = null;
            }
            return dt;

        } ";
            
            return CreateShowListTables;
        }

        private static string _CreatFuncAddNewInfoToTableData(string TableName )
        {
            StringBuilder CreateAddNew= new StringBuilder();
            StringBuilder CreateQuery=new StringBuilder();
            StringBuilder CreatParameters=new StringBuilder();
            StringBuilder CreateSQLCommand=new StringBuilder();
            StringBuilder CreateQueryValue = new StringBuilder();


            CreateQuery.Append( $@"
             string query= @""INSERT INTO [dbo].[{TableName}]  (");

            CreatParameters.Append("(");
            CreateSQLCommand.Append(" \n       using(  SqlCommand command = new SqlCommand(query, connection)){\n");




            foreach (clsProperty Property in table.NormalProperties)
            {

                CreatParameters.Append(" " + Property.CSharpDataType + " " + Property.name + " ,");
                CreateQuery.Append($"              \n [{Property.name}],");
                CreateQueryValue.Append($"              \n {Property.name} ,@");

                if (!Property.IsNullabel)
                {
                    CreateSQLCommand.Append("\n" + $@"              command.Parameters.AddWithValue(@""{Property.name}"" , " + Property.name + ");\n ");
                }
                else
                {
                    CreateSQLCommand.Append(";\n" + $@" if (" + Property.name + @" != """" &&" + Property.name + $@" != null)
                                 command.Parameters.AddWithValue(@""{Property.name}"" , " + Property.name + $@"); 
                            else
                                  command.Parameters.AddWithValue(@""{Property.name}"",System.DBNull.Value);");
                }

            };

            CreatParameters.Remove(CreatParameters.Length - 1,1);
            CreatParameters.Append(")\n    { \n  int ID = -1;\r\n\r\n    try\r\n            {\r\n\r\n                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))\r\n\r\n                {              \n \r\n                       await connection.OpenAsync();");
            CreateQuery.Remove(CreateQuery.Length - 1,1);
            CreateQuery = CreateQuery.Append(")\n  VALUES \n(@" + CreateQueryValue.Remove(CreateQueryValue.Length - 2,2));
            CreateQuery.Append("\n);  SELECT SCOPE_IDENTITY();\" ;");


            CreateAddNew.Append( $@"
        public static async Task<int> AddNew" + TableName + @" " + CreatParameters + CreateQuery +CreateSQLCommand+ @"


                object result =    await command.ExecuteScalarAsync();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ID = insertedID;
                }

                       }

                }

            }

            catch (SqlException ex)
            {

                clsErrorEventLog.LogError(ex.Message);
                return 0;
            }

            return ID;
        }");


            return CreateAddNew.ToString();


        }

        private static string _CreatFuncUpdateInFoInTableData(string TableName)
        {
            StringBuilder CreatUpdate = new StringBuilder();
            StringBuilder CreateQuery = new StringBuilder();
            StringBuilder CreatParameters = new StringBuilder();
            StringBuilder CreateSQLCommand = new StringBuilder();
            StringBuilder CreateQueryValue = new StringBuilder();

            CreatParameters.Append($"( {table.PrimaryProperty.CSharpDataType}" + " " + $" {table.PrimaryProperty.name}" + ",");


            CreateQuery .Append("\n"+ $@"
             string query= @""UPDATE [dbo].[{TableName}]  set ");

           CreateSQLCommand.Append(@"       using (SqlCommand command = new SqlCommand(query, connection))
                    {"+$@"
                                  command.Parameters.AddWithValue(@""{table.PrimaryProperty.name}"" , " +table.PrimaryProperty.name + ");       \n   ");




            foreach(clsProperty Property in table.NormalProperties)
            {

                CreatParameters.Append(" " + Property.CSharpDataType + " " + Property.name + " ,");
                CreateQuery.Append($"                  \n [{Property.name}] =@" + $"{Property.name}" + ",");

                if (!Property.IsNullabel)
                {
                    CreateSQLCommand.Append("\n" + $@"                 command.Parameters.AddWithValue(@""{Property.name}"" , " + Property.name + ");");
                }
                else
                {
                    CreateSQLCommand.Append("\n" + $@"       if (" + Property.name + @" != """" &&" + Property.name + $@" != null)
                                              command.Parameters.AddWithValue(@""{Property.name}"" , " + Property.name + $@"); 
                            else
                                             command.Parameters.AddWithValue(@""{Property.name}"",System.DBNull.Value);");
                }

            };

            CreatParameters.Remove(CreatParameters.Length - 1, 1);


            CreateQuery.Remove(CreateQuery.Length - 1, 1);
            CreateQuery.Append("\n Where " + table.PrimaryProperty.name + "=@" + table.PrimaryProperty.name + " \" ; \n ");


            CreatUpdate.Append( $@"
           public static  async Task<bool> Update" + TableName + @"" + CreatParameters + @")
           { 
              int rowsAffected = 0;
               try
              {
              using( SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)){
                              
                       await connection.OpenAsync();

           " + CreateQuery+ CreateSQLCommand + @"


                   rowsAffected =   await command.ExecuteNonQueryAsync();



                    }
                }
           }
            catch (SqlException ex)
            {

                clsErrorEventLog.LogError(ex.Message);
                rowsAffected = -1;
            }
            return (rowsAffected > 0);
        }");


            return CreatUpdate.ToString();


        }

        private static string _CreatFuncGetInFoFromTableData(string TableName)
        {
            StringBuilder CreatFind = new StringBuilder();
            StringBuilder CreateQuery = new StringBuilder();
            StringBuilder Creatreader = new StringBuilder();
            StringBuilder CreateParameters = new StringBuilder();



            CreateQuery.Append( "\n" + @"        
                bool isFound = false;
            try
            {
            
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {                     
                       await connection.OpenAsync();

               " + $@"

                string query=@""select * from {TableName}  where {table.PrimaryProperty.name}=@{table.PrimaryProperty.name}" + " \" ;"  + @" 
                using (SqlCommand command = new SqlCommand(query, connection))
                { "+$@"
                     command.Parameters.AddWithValue(@""{table.PrimaryProperty.name}"", {table.PrimaryProperty.name});

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())" + @"
                    {
                        if (reader.Read())
                        {   
                          // The record was found
                             isFound = true;
                                ");


   

         CreateParameters.Append( " ( "+ table.PrimaryProperty.CSharpDataType +" " +table.PrimaryProperty.name+",ref ");





            foreach (clsProperty Property in table.NormalProperties)
            {

                CreateParameters.Append(Property.CSharpDataType + " " + Property.name + " , ref ");

                if (!Property.IsNullabel)
                {
                    Creatreader.Append($@"                   {Property.name}=({Property.CSharpDataType})reader[""{Property.name}""] ;" + "\n");
                }
                else
                {
                    Creatreader.Append("\n     " + $@" if (reader[""{Property.name}""] != DBNull.Value)" + @"
                        {" + $@"
                            {Property.name} = ({Property.CSharpDataType})reader[""{Property.name}""];" + @"
                        }
                        else
                        {" + $@"
                          {Property.name} = "" "";" + @"
                        }");


                }
            };
      
            CreateParameters.Remove(CreateParameters.Length - 7,7);
            CreateParameters.Append(")\n    { \n");
            CreateQuery.Remove(CreateQuery.Length - 1, 1);




            CreatFind.Append( $@"
        public static  bool Get" + TableName + "InfoByID " + CreateParameters + CreateQuery + Creatreader+ @"


                         }
                        else
                        {
                          // The record was not found
                           isFound = false;
                         }

                      
                        }
                    }
                   }
            }
            catch (SqlException ex)
            {

                clsErrorEventLog.LogError(ex.Message);
                isFound = false;
            }


            return isFound;
        }");

            return CreatFind.ToString();
        }

        private static string _CreatFuncDeleteInfofromTableData(string TableName)
        {
            StringBuilder CreatDelete=new StringBuilder();
            StringBuilder CreateParameters = new StringBuilder();
            CreateParameters.Append($@"({table.PrimaryProperty.CSharpDataType} {table.PrimaryProperty.name})"+"\n");


            CreatDelete.Append($@"
                public static async Task<bool> Delete" + TableName + CreateParameters  + @"        
                {

            int rowsAffected = 0;

          using( SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {                
                       await connection.OpenAsync();


            string query" + $@"= @""Delete {TableName}  where {table.PrimaryProperty.name}=@{table.PrimaryProperty.name}" + " \" ;" + @" 
                using (SqlCommand command = new SqlCommand(query, connection))
                { " + $@"
                  command.Parameters.AddWithValue(@""{table.PrimaryProperty.name}"", {table.PrimaryProperty.name});
                  " + @"
                    try
                    {
                        rowsAffected =    await command.ExecuteNonQueryAsync();                  
                    }
         catch (SqlException ex)
            {

                clsErrorEventLog.LogError(ex.Message);
                rowsAffected = -1;
            }
        
                }

            return (rowsAffected > 0);

        }
}
");



            return CreatDelete.ToString();
        }

        private static string _CreatDataAccessSettingsClass()
        {
            string Class = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Library_DataAccess
{
    public class clsDataAccessSettings
    {
      " + $@"
       public static string ConnectionString = ""Server=.;Database={clsDataAccessSettings.DataBase};User Id={clsDataAccessSettings.UserID};Password={clsDataAccessSettings.Password};"";
  








      // public static string ConnectionString = ConfigurationManager.AppSettings[""ConnectionString""];

       " +$@"

//You Have to Add ConnectionString in ""App.confg - File"" 
//Go to References And Add ""System.Configuration""



          /*  <?xml version = ""1.0"" encoding=""utf-8"" ?>
<configuration>


    <startup>

        <supportedRuntime version = ""v4.0"" sku = "".NETFramework,Version=v4.8""/>

    </startup>


    <appSettings>

        <add key = ""ConnectionString"" value = ""Server=.;Database={clsDataAccessSettings.DataBase };User id= { clsDataAccessSettings.UserID};Password={ clsDataAccessSettings.Password};""/>

    </appSettings>

</configuration>*/



    " +@"}
}";



//
          /*  <? xml version = "1.0" encoding = "utf-8" ?>
< configuration >


    < startup >

        < supportedRuntime version = "v4.0" sku = ".NETFramework,Version=v4.8" />

    </ startup >


    < appSettings >

        < add key = "ConnectionString" value = "Server=.;Database={clsDataAccessSettings.DataBase };User id= { clsDataAccessSettings.UserID};Password={ clsDataAccessSettings.Password};" />

    </ appSettings >

</ configuration >*/



            return Class;
        }

        private static string _CreatErrorEventLogClass(string ProjrctName)
        {

            string Class= @"using System;
using System.Diagnostics;
namespace Library_DB_DataAccess

{

    public class clsErrorEventLog
    {


        private static readonly string SourceName" +$@"= ""{ProjrctName}"" ;"+ @"
        static clsErrorEventLog()
        {

            if (!EventLog.Exists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, ""Application"");
            }
        }

        public static void LogError(string ErrorMessage, EventLogEntryType entryType = EventLogEntryType.Error)
        {
            EventLog.WriteEntry(SourceName, ErrorMessage, entryType);
        }

    }

}";
            return Class;
        }

        //-------------------------Bussiness Layer --------------------------------------
        private static string _CreatProperties(string TableName)
        {
            StringBuilder CreateProperty = new StringBuilder();
            StringBuilder PrivateConstracter = new StringBuilder();
            StringBuilder PublicConstracter = new StringBuilder();
            StringBuilder CreateParametreConstracter = new StringBuilder();

            CreateParametreConstracter.Append($"this.{table.PrimaryProperty.name}={table.PrimaryProperty.name};");

            PrivateConstracter.Append($@" 
         private cls{TableName} ( " + table.PrimaryProperty.CSharpDataType + " " + table.PrimaryProperty.name + " ,");

            PublicConstracter.Append($@"
         public cls{TableName}() 
                " + "        { " + $"  \n      this.{table.PrimaryProperty.name}= {clsDataType.GetNulleblData(table.PrimaryProperty.CSharpDataType.ToString())};\n");
                         

            CreateProperty.Append(@"
           public enum enMode { AddNew = 0, Update = 1 };
           public enMode Mode = enMode.AddNew; 

          " +$"    \n           public {table.PrimaryProperty.CSharpDataType}"+$" {table.PrimaryProperty.name}" + "{ set; get; }\n ");

            foreach (clsProperty Property in table.NormalProperties)
            {


                CreateProperty.Append("\n" + $"        public {Property.CSharpDataType}" + $" {Property.name}" + "{ set; get; }\n");
                PublicConstracter.Append($"   \n       this.{Property.name} ={clsDataType.GetNulleblData(Property.CSharpDataType.ToString())};\n");
                PrivateConstracter.Append(" " + Property.CSharpDataType + " " + Property.name + " ,");
                CreateParametreConstracter.Append($" \n          this.{Property.name} ={Property.name};\n");

            }
            PrivateConstracter.Remove(PrivateConstracter.Length - 2, 2);
            PrivateConstracter.Append(") \n           {\n");


            foreach (clsForeignProperty ForgeinProperty in table.ForeignProperties)
            {
                CreateProperty.Append("\n        Public cls" + ForgeinProperty.ParentTable + " " + ForgeinProperty.ParentTable + "Info  { set; get; }\n");
                PrivateConstracter.Append(ForgeinProperty.ParentTable + "Info=cls" + ForgeinProperty.ParentTable + ".Find(" + ForgeinProperty.name + ");\n");
            }

            CreateParametreConstracter.Append( "            Mode = enMode.Update;\n          }\n\n");
            PublicConstracter.Append("         Mode= enMode.AddNew;\n          }\n\n");

      

          
            PrivateConstracter.Append(CreateParametreConstracter);

                return (CreateProperty.Append(PrivateConstracter)).Append(PublicConstracter).ToString();
        }

        private static string _CreatGetAllFunc(string TableName)
        {
            StringBuilder stclass = new StringBuilder();
                stclass.Append(
             $@"        public static async Task<DataTable>  GetAll{TableName}()"+ 
        "\n             {\n"
           +$@"            return await cls{TableName}Data.GetAll{TableName}();"+
    " \n                }\n\n");

            return stclass.ToString();
        }

        private static string _CreatAddInfoFunc(string TableName)
        {
            StringBuilder stclass =new StringBuilder();

            stclass.Append($@"        private async Task<bool> _AddNew{TableName}()" +
        "\n          {\n"
           + $@"       this.{table.PrimaryProperty.name}=await cls{TableName}Data.AddNew{TableName}(");

            foreach (clsProperty Property in table.NormalProperties)
                stclass.Append("this." + Property.name + " ,");

            
            stclass.Remove(stclass.Length-1,1);

            stclass.Append($@"); 

            return  (this.{table.PrimaryProperty.name}!= -1);
"+" \n        }\n");
                    return stclass.ToString();
        }

        private static string _CreatUpdateInfoFunc(string TableName)
        {
            StringBuilder stclass =new StringBuilder();



            stclass.Append($@"        private async Task<bool> _Update{TableName}()" +
        "\n          {\n"
           + $@"       return await cls{TableName}Data.Update{TableName}(this."+table.PrimaryProperty.name+",");

            foreach (clsProperty Property in table.NormalProperties)
                stclass.Append("this." + Property.name + " ,");
               
            
            stclass.Remove(stclass.Length-1,1);

            stclass.Append($@"); 
             "+"\n     }\n");


            return stclass.ToString();
        }

        private static string _CreatFindInfoFunc(string TableName)
        {
            StringBuilder Return = new StringBuilder();
            StringBuilder Parametars2 =new StringBuilder();
            StringBuilder stclass=new StringBuilder();

               stclass .Append( $@"        public static cls{TableName} Find(int {table.PrimaryProperty.name})" +
        "\n          {\n");

            Parametars2.Append( $"\n         if ( cls{TableName}Data.Get{TableName}InfoByID({table.PrimaryProperty.name}");
    
            Return.Append(  $"        return new cls{TableName}("+table.PrimaryProperty.name +", ");

            foreach (clsProperty Property in table.NormalProperties)
            {
        
                Return.Append($"{Property.name},");
                stclass.Append( $"       {Property.CSharpDataType} {Property.name}= {clsDataType.GetNulleblData(Property.CSharpDataType)} ;\n");
                Parametars2.Append($",ref {Property.name}");
                    
                
            }

            Parametars2.Append(@"))
            ");


            stclass.Remove(stclass.Length - 1, 1);
            stclass.Append(Parametars2);
            Return.Remove(Return.Length - 1,1);
            Return.Append( ");  \n      else \n         return null;\n     }\n");
            stclass.Append(Return);


            return stclass.ToString();
        }
    
        private static string _CreatSaveInfoFunc(string TableName)
        {
            string stclass = $@"       
            public async Task<bool> Save()
        {{


            switch (Mode)
            {{
                case enMode.AddNew:
                    if (await _AddNew{TableName}())
                    {{

                        Mode = enMode.Update;
                        return true;
                    }}
                    else
                    {{
                        return false;
                    }}

                case enMode.Update:

                    return await _Update{TableName}();

            }}

            return false;
        }}
";




            return stclass;
        }
     
        private static string _CreatDeleteInfoFunc(string TableName)
        {

            string stclass = $@"
        public async Task<bool> Delete()
        {{
            return await cls{TableName}Data.Delete{TableName}(this.{table.PrimaryProperty.name});
        }}

";


            return stclass;
        }
        //-----------------------------------------------------------------------------------
        private static string _CreatBusinessLayerClass(string ProjrctName,string  Tabl)
        {
            StringBuilder sb = new StringBuilder();


       
            string end = "  \n\n\n  }   \n\n}  ";


            sb.Append(_CreatFormatDataClas(ProjrctName, "_BusinessLayer", Tabl ) + "\n\n");
            sb.Append(_CreatProperties(Tabl) + "\n\n");
            sb.Append(_CreatGetAllFunc(Tabl) + "\n\n");
            sb.Append(_CreatAddInfoFunc(Tabl) + "\n\n");
            sb.Append(_CreatUpdateInfoFunc(Tabl) + "\n\n");
            sb.Append(_CreatFindInfoFunc(Tabl) + "\n\n");
            sb.Append(_CreatSaveInfoFunc(Tabl) + "\n\n");
            sb.Append(_CreatDeleteInfoFunc(Tabl) + "\n\n");
            sb.Append(end);



            clsData.CreatFileCs(Path+"\\"+
                ProjrctName + "BusinessLayer" + "\\cls" + Tabl + ".cs", sb.ToString());


            return sb.ToString();
        }

        private static string _CreatDataAccessLayerClass(string ProjrctName,string Tabl)
        {

            StringBuilder sb = new StringBuilder();
            string end = "  \n\n\n        \n\n}  \n \n   }";

            sb.Append(_CreatFormatDataClas(ProjrctName, "_DataAccessLayer", Tabl + "Data")+"\n\n");
            sb.Append(_CreatFuncShowListTableInfoData(Tabl) + "\n\n");
            sb.Append(_CreatFuncAddNewInfoToTableData(Tabl) + "\n\n");
            sb.Append(_CreatFuncUpdateInFoInTableData(Tabl) + "\n\n");
            sb.Append(_CreatFuncGetInFoFromTableData(Tabl) + "\n\n");
            sb.Append(_CreatFuncDeleteInfofromTableData(Tabl) + "\n\n");
            sb.Append(end);

            clsData.CreatFileCs(Path + "\\" + ProjrctName  +"DataAccessLayer" + "\\cls" + Tabl + "Data.cs", sb.ToString());



            clsData.CreatFileCs(Path+"\\" + ProjrctName + "DataAccessLayer"
                + "\\clsDataAccessSetting.cs"
                   , _CreatDataAccessSettingsClass());

            clsData.CreatFileCs(Path + "\\" + ProjrctName + "DataAccessLayer"
                + "\\clsErrorEventLog.cs"
                   , _CreatErrorEventLogClass(ProjrctName));

            return sb.ToString();

        }

        private static void _CreatDataAccessAndBusinessLayer(string TableName,string ProjrctName)
        {
            DataTable _NormalProperties = new DataTable();
            DataTable _ForeignInfo = new DataTable();
            table = new clsTable();


            _NormalProperties =  clsTable.GetTableInfoWhithoutPrimary(TableName);
            table.PrimaryProperty = clsTable.FindPrimaryKey(TableName);
             _ForeignInfo = clsTable.GetForeignInfo(TableName);



            if (table.PrimaryProperty == null)
                return ;

            table.TableName = TableName;


            Parallel.ForEach(_NormalProperties.AsEnumerable() ,row=>
            {
                clsProperty property = new clsProperty();

                property.FillProperty((string)row["COLUMN_NAME"], (string)row["DATA_TYPE"], ((string)row["IS_NULLABLE"] == "YES") ? true : false);
                table.NormalProperties.Add(property);

            });


            Parallel.ForEach(_ForeignInfo.AsEnumerable(), row =>
            {
                clsForeignProperty Foreignproperty = new clsForeignProperty();

                Foreignproperty.FillFogienProperty((string)row["FOREIGN_NAME"], (string)row["DATA_TYPE"],
                    ((string)row["Is_Nullabel"] == "YES") ? true : false, (string)row["PARENT_TABLE_NAME"]);
                table.ForeignProperties.Add(Foreignproperty);

            });

            Parallel.Invoke(
                () => _CreatBusinessLayerClass(ProjrctName, TableName),
                () => _CreatDataAccessLayerClass(ProjrctName, TableName));

        }

        public static bool Generate(string ProjrctName)
        {
            Path = "";
            Path = "C:\\Users\\TOPTECH\\source\\C#\\" + ProjrctName + "Cs";

            clsData.CreateFolderIfDoesNotExist(Path);


            Parallel.Invoke(
               () => clsData.CreateFolderIfDoesNotExist(Path +"\\"+ProjrctName+"BusinessLayer"),
               () => clsData.CreateFolderIfDoesNotExist(Path +"\\"+ ProjrctName + "DataAccessLayer")
               );




            List<string> Tabls;
             Tabls = clsTable.GetTables();

            foreach (string TableName in Tabls)
            {
               Parallel.Invoke(()=>_CreatDataAccessAndBusinessLayer(TableName, ProjrctName));

            }


            //Parallel.ForEach(Tabls, TableName =>
            //{

            //    _CreatDataAccessAndBusinessLayer(TableName, ProjrctName);

            //});

            return true;

        }


    }
}
