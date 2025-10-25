using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerater_DataAcessLayer
{
    public  class clsDataType
    {

        public static string ConvertDataTypeFromSql(string sqlDataType)
        {
            switch (sqlDataType)
            {
                case "int":
                    return "int";

                case "tinyint":
                    return "byte";

                case "smallint":
                    return "int";

                case "Decimal":
                    return "double";

                case "smallmoney":
                    return "double";

                case "money":
                    return "double";

                case "varchar":
                    return "string";

                case "nvarchar":
                    return "string";

                case "datetime":
                    return "DateTime";

                case "smallDateTime":
                    return "DateTime";

                case "date":
                    return "DateTime";

                case "bit":
                    return "bool";

                default:
                    return "int";

            }
        }

        public static string GetNulleblData(string DataType)
        {
            switch (DataType)
            {
                case "int":
                    return "-1";

                case "byte":
                    return "0";

                case "double":
                    return "0";

                case "string":
                    return "\"\"";

                case "DateTime":
                    return "DateTime.MinValue";

                case "bool":
                    return "false";

                default:
                    return "-1";

            }
        }



    }
}
