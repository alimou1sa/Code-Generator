using CodeGenerater_DataAcessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator_BusinessLayer
{
    public  class clsProperty
    {
        public string name { set; get; }
        public string sqlDataType { set; get; }
        public string CSharpDataType { set; get; }
        public bool IsNullabel { set; get; }

        public clsProperty()
        {
            this.name = "";
            this.sqlDataType = "";
            this.CSharpDataType = "";
            this.IsNullabel = false;
        }

        public clsProperty(string Name, string sqlDataType, bool IsNullabel)
        {
            this.name = Name;
            this.sqlDataType = sqlDataType;
            this.CSharpDataType = clsDataType.ConvertDataTypeFromSql(sqlDataType);
            this.IsNullabel = IsNullabel;
        }


        public void FillProperty(string Name, string sqlDataType, bool IsNullabel)
        {
            this.name = Name;
            this.sqlDataType = sqlDataType;
            this.CSharpDataType = clsDataType.ConvertDataTypeFromSql(sqlDataType);
            this.IsNullabel = IsNullabel;

        }


       /* private string GetDefaultValue()
        {
            if (CSharpDataType == "int" || CSharpDataType == "float" || CSharpDataType == "double")
                return "-1";
            else if (CSharpDataType == "string")
                return " ";
            else if (CSharpDataType == "DateTime")
                return "DateTime.Now";
            else if (CSharpDataType == "bool")
                return "false";
            else if (CSharpDataType == "byte")
                return "0";

            else
                return "";

        }*/






    }
}
