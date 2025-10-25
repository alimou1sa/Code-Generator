using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerater_DataAcessLayer
{
    public class clsTblesInfoData
    {

        public static List<string> GetTables()
        {
            List<string> TableNames = new List<string>();

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {

                    string query = @"


                        SELECT DISTINCT    

	                        c.TABLE_NAME 

                        FROM  
	                        INFORMATION_SCHEMA.COLUMNS  c
	                        join

	                        INFORMATION_SCHEMA.TABLES  t

	                        ON c.TABLE_NAME = t.TABLE_NAME 

	                        WHERE t.TABLE_TYPE = 'BASE TABLE' --include only basetable 


	                        AND t.TABLE_CATALOG  = @Database -- filtering by databse name 
	                        AND t.TABLE_NAME NOT IN ('sysdiagrams') -- and exclude sysdiagrams tables ";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        connection.Open();

                        command.Parameters.AddWithValue("@Database", clsDataAccessSettings.DataBase);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                TableNames.Add((string)reader["TABLE_NAME"]);

                            }


                        }
                    }

                }
            }
            catch
            {

            }


            return TableNames;

        }

        public static string GetPrimaryKey(string TableName)
        {

            string PrimaryKey = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"

                       
                                
				   
				   
			        SELECT c.COLUMN_NAME

                            FROM 

	                            INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu
				                            join
	                            INFORMATION_SCHEMA.COLUMNS c

				                            ON
					                            kcu .TABLE_NAME = c .TABLE_NAME 

				                            AND 

					                            kcu.COLUMN_NAME = c.COLUMN_NAME 

			                            WHERE kcu .TABLE_NAME = @TableName and kcu.CONSTRAINT_NAME like 'PK%' and kcu.TABLE_CATALOG = @DataBase
		
                    ;";


                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    connection.Open();


                    command.Parameters.AddWithValue("@TableName", TableName);
                    command.Parameters.AddWithValue("@DataBase", clsDataAccessSettings.DataBase);


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PrimaryKey = (string)reader["COLUMN_NAME"];


                        }
                    }

                }

            }

            return PrimaryKey;
        }

        public static DataTable GetTableInfo(string tableName)
        {
            DataTable dtTableInfo = new DataTable();


            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = @"




                         SELECT  

	                            c.table_name,
	                            c.COLUMN_NAME ,
	                            c.DATA_TYPE,
	                            c.IS_NULLABLE 
							
                            FROM  
	                            INFORMATION_SCHEMA.COLUMNS  c
	                         
							      join

	                            INFORMATION_SCHEMA.TABLES  t
						  
                                         ON
	                             c.TABLE_NAME   = t.TABLE_NAME  
							 
							 
                                WHERE
	                                 t.TABLE_CATALOG  =  @Database -- filtering by databse name 
	                                AND t.TABLE_NAME =  @TableName
							
							    order by ORDINAL_POSITION	";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {


                        connection.Open();


                        command.Parameters.AddWithValue("@Database", clsDataAccessSettings.DataBase);
                        command.Parameters.AddWithValue("@TableName", tableName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            dtTableInfo.Load(reader);

                        }
                    }

                }
            }
            catch
            {

            }


            return dtTableInfo;



        }

        public static  DataTable GetForeigKeys(string TableName)
        {
            DataTable dtForeigns = new DataTable();


            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = @"


                        select 

	
	                        tr.name as PARENT_TABLE_NAME,
	                        c.name FOREIGN_NAME,
	                        case c.is_nullable when 0 then 'NO'ELse 'YES' end as  Is_Nullabel,
	                        ty.name as DATA_TYPE

                        from 

	                        sys.foreign_keys fk

	                        join sys.tables t on fk.parent_object_id = t.object_id


	                        join sys.tables tr on fk.referenced_object_id = tr.object_id

	
	                        inner join sys.foreign_key_columns fkc ON 
	                        fk.object_id = fkc.constraint_object_id 
	
	                        inner join sys.columns c ON fkc.parent_object_id = c.object_id AND fkc.parent_column_id = c.column_id 

	                        inner join sys.types ty on c.user_type_id  = ty.user_type_id

	                        where t.name = @TableName


                         
 ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {


                        connection.Open();

                        command.Parameters.AddWithValue("@TableName", TableName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            dtForeigns.Load(reader);

                        }
                    }

                }
            }
            catch
            {

            }


            return dtForeigns;

        }

        public static bool IsCredentialsCorrect()
        {
            bool Correct = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    conn.Open();

                    conn.Close();

                    Correct = true;

                }

            }
            catch
            {
                Correct = false;

            }

            return Correct;

        }

        public static List<string> GetTableColumns(string TablName)
        {
            List<string> TableNames = new List<string>();

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {

                    string query = @"

Select Column_Name from INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME =@TablName ";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        connection.Open();

                        command.Parameters.AddWithValue("@Database", clsDataAccessSettings.DataBase);
                        command.Parameters.AddWithValue("@TablName", TablName);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                TableNames.Add((string)reader["TABLE_NAME"]);

                            }


                        }
                    }

                }
            }
            catch
            {

            }


            return TableNames;

        }

        public static DataTable GetTableInfoWhithoutPrimarykey(string TableName)
        {
            DataTable dtForeigns = new DataTable();


            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = @"



SELECT
    c.TABLE_NAME,
    c.COLUMN_NAME,
    c.DATA_TYPE,
    c.IS_NULLABLE
FROM
    INFORMATION_SCHEMA.COLUMNS AS c
    INNER JOIN INFORMATION_SCHEMA.TABLES AS t
        ON c.TABLE_NAME = t.TABLE_NAME
        AND c.TABLE_SCHEMA = t.TABLE_SCHEMA
WHERE
    t.TABLE_CATALOG = @Database
    AND t.TABLE_NAME = @TableName
    AND c.COLUMN_NAME NOT IN (
        SELECT kcu.COLUMN_NAME
        FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS kcu
        WHERE kcu.TABLE_NAME = @TableName
          AND kcu.TABLE_SCHEMA = (SELECT ts.TABLE_SCHEMA
                                FROM INFORMATION_SCHEMA.TABLES AS ts
                                WHERE ts.TABLE_NAME =@TableName
                                  AND ts.TABLE_CATALOG = @Database)
          AND kcu.CONSTRAINT_NAME LIKE 'PK%' -- PK constraints
    )
ORDER BY
    c.ORDINAL_POSITION;

";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {


                        connection.Open();


                        command.Parameters.AddWithValue("@Database", clsDataAccessSettings.DataBase);
                        command.Parameters.AddWithValue("@TableName", TableName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            dtForeigns.Load(reader);

                        }
                    }

                }
            }
            catch
            {

            }


            return dtForeigns;

        }

        public static bool GePrimaryKeyInfoByTableName(string  TablName, ref string ColumnName, ref string sqlDataType)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {      
                connection.Open();
                string query = @"   SELECT c.COLUMN_NAME
   ,c.DATA_TYPE
FROM 

    INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu
                join
    INFORMATION_SCHEMA.COLUMNS c

                ON
                    kcu .TABLE_NAME = c .TABLE_NAME 

                AND 

                    kcu.COLUMN_NAME = c.COLUMN_NAME 

            WHERE kcu .TABLE_NAME = @TablName and kcu.CONSTRAINT_NAME like 'PK%'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TablName", TablName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                // The record was found
                                isFound = true;

                                ColumnName = (string)reader["COLUMN_NAME"];

                                sqlDataType = (string)reader["DATA_TYPE"];

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
            catch (Exception ex)
            {
                isFound = false;
            }

            return isFound;
        }

        public static bool GeColumnInfoByTableNameandColumnName(string TablName,  string ColumnName, ref string sqlDataType,ref bool IsNullable)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();
                    string query = @" SELECT
    c.name AS ColumnName,
    t.name AS DataType,
    c.is_nullable AS IsNullable 
FROM
    sys.tables AS tbl
    INNER JOIN sys.schemas AS sch ON tbl.schema_id = sch.schema_id
    INNER JOIN sys.columns AS c ON tbl.object_id = c.object_id
    INNER JOIN sys.types AS t ON c.user_type_id = t.user_type_id
WHERE
    tbl.name = @TablName
      AND c.name = @ColumnName;
    INFORMATION_SCHEMA.COLUMNS c
";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TablName", TablName);
                        command.Parameters.AddWithValue("@ColumnName", ColumnName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                // The record was found
                                isFound = true;

                                IsNullable = (bool)reader["IsNullable"];

                                sqlDataType = (string)reader["sqlDataType"];

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
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }

            return isFound;
        }

        public static   bool GeColumnsInfoByTableName(string TablName, ref string ColumnName, ref string sqlDataType, ref bool IsNullable)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();
                    string query = @" 
SELECT
    c.name AS ColumnName,
    t.name AS DataType,
    c.is_nullable AS IsNullable
FROM
    sys.tables AS tbl
    INNER JOIN sys.schemas AS sch ON tbl.schema_id = sch.schema_id
    INNER JOIN sys.columns AS c ON tbl.object_id = c.object_id
    INNER JOIN sys.types AS t ON c.user_type_id = t.user_type_id
WHERE
    tbl.name =@TablName
";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        
                        command.Parameters.AddWithValue("@TablName", TablName);
          

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                                                 
                            if (reader.Read())
                            {

                                // The record was found
                                isFound = true;

                                IsNullable = (bool)reader["IsNullable"];

                                ColumnName = (string )reader["ColumnName"];
                                sqlDataType = (string)reader["sqlDataType"];

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
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }

            return isFound;
        }

        public static List<string> GetAllDatabasesNames()
        {
            List<string> DBlist = new List<string>();

            try
            {


                using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=master;Integrated Security=True"))
                {
                    string query = @"SELECT Name FROM sys.databases Where database_id > 4
	                                    Order By Name ASC;";

                    using (SqlCommand cmd = new SqlCommand(query,connection))
                    {

                        connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DBlist.Add((string)reader["Name"]);
                            }
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }


            return DBlist;
        }



    }
}
