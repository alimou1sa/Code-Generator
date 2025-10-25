using CodeGenerater_DataAcessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator_BusinessLayer
{
    public  class clsTable
    {


        public string TableName { set; get; }
        public List<clsProperty> NormalProperties { set; get; }
        public clsProperty PrimaryProperty { set; get; }
        public List<clsForeignProperty> ForeignProperties { set; get; }

        public clsTable()
        {
            this.TableName = "";
            this.NormalProperties = new List<clsProperty>();
            this.PrimaryProperty = new clsProperty();
            this.ForeignProperties = new List<clsForeignProperty>();

        }

        public static string GetPrimaryKeyName(string TableName)
        {
            return clsTblesInfoData.GetPrimaryKey(TableName);
        }
        public static DataTable GetTableInfo(string TableName)
        {
            return clsTblesInfoData.GetTableInfo(TableName);

        }
        public static DataTable GetForeignInfo(string tableName)
        {
            return clsTblesInfoData.GetForeigKeys(tableName);
        }
        public static List<string> GetTables()
        {
            return clsTblesInfoData.GetTables();

        }
        public static List<string> GetTablesColumns(string TableName)
        {
            return clsTblesInfoData.GetTableColumns(TableName);

        }
        public static clsTable FindTable(string TableName)
        {
            string PrimaryKey = GetPrimaryKeyName(TableName);

            DataTable dtTableInfo = GetTableInfo(TableName);


            clsTable table = new clsTable();

            table.TableName = TableName;


            foreach (DataRow row in dtTableInfo.Rows)
            {
                clsProperty property = new clsProperty();


                property.FillProperty((string)row["COLUMN_NAME"], (string)row["DATA_TYPE"], ((string)row["IS_NULLABLE"] == "YES") ? true : false);

                if (property.name == PrimaryKey)
                    table.PrimaryProperty = property;
                else
                    table.NormalProperties.Add(property);

            }

            DataTable dtForeignKeysInfo = GetForeignInfo(TableName);



            foreach (DataRow row in dtForeignKeysInfo.Rows)
            {
                clsForeignProperty Foreignproperty = new clsForeignProperty();



                Foreignproperty.FillFogienProperty((string)row["FOREIGN_NAME"], (string)row["DATA_TYPE"], ((string)row["Is_Nullabel"] == "YES") ? true : false, (string)row["PARENT_TABLE_NAME"]);

                table.ForeignProperties.Add(Foreignproperty);


            }



            return table;


        }
        public static List<clsTable> FindTables()
        {

            List<clsTable> Tables = new List<clsTable>();


            List<string> TableNames = GetTables();


            foreach (string tableName in TableNames)
            {
                clsTable table = FindTable(tableName);

                Tables.Add(table);

            }


            return Tables;

        }

        public static DataTable GetTableInfoWhithoutPrimary( string TableName)
        {

            return
            clsTblesInfoData.GetTableInfoWhithoutPrimarykey(TableName);
        }

        public static clsProperty FindPrimaryKey(string TableName)
        {
            string PrimayName = "";
            string sqlDataType = "";

            bool _IsGet  =clsTblesInfoData.GePrimaryKeyInfoByTableName(TableName, ref PrimayName, ref sqlDataType);
            if (_IsGet)
           
                return  new clsProperty(PrimayName, sqlDataType,false);
            else
                return null;

        }

        public static List<string> GetAllDatabasesNames()
        {
            return clsTblesInfoData.GetAllDatabasesNames();
        }


    }
}
