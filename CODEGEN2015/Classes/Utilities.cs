using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
namespace CodeGenerator2005
{
   public class Utilities
    {


       public static string GetFrienlyName(string strName)
       {
           string newString = "";
           char[] arr=strName.ToCharArray();
           for (int i = 0; i < arr.Length; i++)
           {
               char ch = arr[i];
               char nextChar = ((i + 1) < arr.Length) ? arr[i + 1] : char.MinValue;

               newString += ch;
               if (!Char.IsUpper(ch))
               {
                   if (Char.IsUpper(nextChar))
                   {
                       newString += " ";
                   }
               }
           }
           return newString;
       
       }

       //Conversion de tipo de motor SQL Server a numeracion .NET Framework
       public static string GetSystemType(string strDBType, CodeLanguage objCodeLanguage)
       {

           string CSType = "";

           switch (strDBType.ToLower())
           {
               case "bigint":
                   CSType = "Int64";
                   break;
               case "binary":
                   CSType = "Byte[]";
                   break;
               case "bit":
                   CSType = "Boolean";
                   break;
               case "char":
                   CSType = "String";
                   break;
               case "date":
                   CSType = "DateTime";
                   break;
               case "datetime":
                   CSType = "DateTime";
                   break;
               case "datetime2":
                   CSType = "DateTime2";
                   break;
               case "datetimeoffset":
                   CSType = "DateTimeOffset";
                   break;
               case "decimal":
                   CSType = "Decimal";
                   break;
               case "float":
                   CSType = "Double";
                   break;
               case "image":
                   CSType = "Byte[]";
                   break;
               case "int":
                   CSType = "Int32";
                   break;
               case "money":
                   CSType = "Decimal";
                   break;
               case "nchar":
                   CSType = "String";
                   break;
               case "ntext":
                   CSType = "String";
                   break;
               case "numeric":
                   CSType = "Decimal";
                   break;
               case "nvarchar":
                   CSType = "String";
                   break;
               case "real":
                   CSType = "Single";
                   break;
               case "rowversion":
                   CSType = "Byte[]";
                   break;
               case "smalldatetime":
                   CSType = "DateTime";
                   break;
               case "smallint":
                   CSType = "Int16";
                   break;
               case "smallmoney":
                   CSType = "Decimal";
                   break;
               case "sql_variant":
                   CSType = "Object *";
                   break;
               case "text":
                   CSType = "String";
                   break;
               case "time":
                   CSType = "TimeSpan";
                   break;
               case "timestamp":
                   CSType = "Byte[]";
                   break;
               case "tinyint":
                   CSType = "Byte";
                   break;
               case "uniqueidentifier":
                   CSType = "Guid";
                   break;
               case "varbinary":
                   CSType = "Byte[]";
                   break;
               case "varchar":
                   CSType = "String";
                   break;
               case "xml":
                   CSType = "Xml";
                   break;
               default:
                   CSType = "object";
                   break;
           }
           return CSType;
       }

       //Conversion de tipo de motor SQL Server a numeracion SqlSBType 
       public static string GetParamsType(string strDBType, CodeLanguage objCodeLanguage)
       {

           string CSType = "";

           switch (strDBType.ToLower())
           {
               case "bigint":
                   CSType = "BigInt";
                   break;
               case "binary":
                   CSType = "VarBinary";
                   break;
               case "bit":
                   CSType = "Bit";
                   break;
               case "char":
                   CSType = "Char";
                   break;
               case "date":
                   CSType = "Date";
                   break;
               case "datetime":
                   CSType = "DateTime";
                   break;
               case "datetime2":
                   CSType = "DateTime2";
                   break;
               case "datetimeoffset":
                   CSType = "DateTimeOffset";
                   break;
               case "decimal":
                   CSType = "Decimal";
                   break;
               case "float":
                   CSType = "Float";
                   break;
               case "image":
                   CSType = "Binary";
                   break;
               case "int":
                   CSType = "Int";
                   break;
               case "money":
                   CSType = "Money";
                   break;
               case "nchar":
                   CSType = "NChar";
                   break;
               case "ntext":
                   CSType = "NText";
                   break;
               case "numeric":
                   CSType = "Decimal";
                   break;
               case "nvarchar":
                   CSType = "NVarChar";
                   break;
               case "real":
                   CSType = "Real";
                   break;
               case "rowversion":
                   CSType = "Timestamp";
                   break;
               case "smalldatetime":
                   CSType = "DateTime";
                   break;
               case "smallint":
                   CSType = "SmallInt";
                   break;
               case "smallmoney":
                   CSType = "SmallMoney";
                   break;
               case "sql_variant":
                   CSType = "Variant";
                   break;
               case "text":
                   CSType = "Text";
                   break;
               case "time":
                   CSType = "Time";
                   break;
               case "timestamp":
                   CSType = "Timestamp";
                   break;
               case "tinyint":
                   CSType = "TinyInt";
                   break;
               case "uniqueidentifier":
                   CSType = "UniqueIdentifier";
                   break;
               case "varbinary":
                   CSType = "VarBinary";
                   break;
               case "varchar":
                   CSType = "VarChar";
                   break;
               case "xml":
                   CSType = "Xml";
                   break;
               default:
                   CSType = "object";
                   break;
           }

           return CSType;
       }

       //Conversion de tipo de motor SQL Server a Enumeracion SqlDbType
       public static string GetDBTypeEnum(string strDBType,string strProvider)
       {

           string DBType = "";
           
           switch (strDBType.ToLower())
           {
               case "bigint":
                   DBType = "BigInt";
                   break;
               case "binary":
                   DBType = "VarBinary";
                   break;
               case "bit":
                   DBType = "Bit";
                   break;
               case "char":
                   DBType = "Char";
                   break;
               case "date":
                   DBType = "Date";
                   break;
               case "datetime":
                   DBType = "DateTime";
                   break;
               case "datetime2":
                   DBType = "DateTime2";
                   break;
               case "datetimeoffset":
                   DBType = "DateTimeOffset";
                   break;
               case "decimal":
                   DBType = "Decimal";
                   break;
               case "FILESTREAM":
                   DBType = "VarBinary";
                   break;
               case "float":
                   DBType = "Float";
                   break;
               case "image":
                   DBType = "Bynary";
                   break;
               case "int":
                   DBType = "Int";
                   break;
               case "money":
                   DBType = "Money";
                   break;
               case "nchar":
                   DBType = "NChar";
                   break;
               case "ntext":
                   DBType = "NText";
                   break;
               case "numeric":
                   DBType = "Decimal";
                   break;
               case "nvarchar":
                   DBType = "NVarChar";
                   break;
               case "real":
                   DBType = "Real";
                   break;
               case "rowversion":
                   DBType = "Timestamp";
                   break;
               case "smalldatetime":
                   DBType = "DateTime";
                   break;
               case "smallint":
                   DBType = "SmallInt";
                   break;
               case "smallmoney":
                   DBType = "SmallMoney";
                   break;
               case "sql_variant":
                   DBType = "Variant";
                   break;
               case "text":
                   DBType = "Text";
                   break;
               case "time":
                   DBType = "Time";
                   break;
               case "timestamp":
                   DBType = "Timestamp";
                   break;
               case "tinyint":
                   DBType = "TinyInt";
                   break;
               case "uniqueidentifier":
                   DBType = "UniqueIdentifier";
                   break;
               case "varbinary":
                   DBType = "VarBinary";
                   break;
               case "varchar":
                   DBType = "VarChar";
                   break;
               case "xml":
                   DBType = "Xml";
                   break;
               default:
                   DBType = strDBType;
                   break;
           }
           return "SqlDbType." + DBType;
       }

       public static string GetSingleQuote(string strDBType)
       {

           string CSType = "";
           string VBType = "";
           string addSingleQuote = string.Empty;

           switch (strDBType.ToLower())
           {
               case "bit":
                   CSType = "Boolean";
                   VBType = "Boolean";
                    break;
               case "int":
                   CSType = "Int32";
                   VBType = "Int32";
                   break;
               case "smallint":
               case "tinyint":
                   CSType = "Int16";
                   VBType = "Int16";
                   break;
               case "bigint":
                   CSType = "Int64";
                   VBType = "Int64";
                   break;
               case "float":
               case "decimal":
               case "money":
               case "real":
                   CSType = "Double";
                   VBType = "Double";
                   break;
               case "nvarchar":
               case "varchar":
               case "char":
               case "nchar":
               case "text":
               case "ntext":
                   CSType = "String";
                   VBType = "String";
                   addSingleQuote = "'";
                   break;
               case "datetime":
               case "smalldatetime":
                   CSType = "DateTime";
                   VBType = "DateTime";
                   addSingleQuote = "'";
                   break;
               case "varbinary":
               case "binary":
               case "image":
                   CSType = "byte[]";
                   VBType = "byte()";
                   break;
               default:
                   CSType = "object";
                   VBType = "object";
                   break;
           }

           return addSingleQuote;
       }



       /// <summary>
       /// 
       /// </summary>
       /// <param name="anOleDbType"></param>
       /// <returns></returns>
       public Type MapOleDbTypeToClrType(OleDbType anOleDbType)
       {

           if (anOleDbType == OleDbType.BigInt)
               return typeof(Int64);
           if (anOleDbType == OleDbType.Binary)
               return typeof(Byte[]);
           if (anOleDbType == OleDbType.Boolean)
               return typeof(Boolean);
           if (anOleDbType == OleDbType.BSTR)
               return typeof(String);
           if (anOleDbType == OleDbType.Char)
               return typeof(String);
           if (anOleDbType == OleDbType.Currency)
               return typeof(Decimal);
           if (anOleDbType == OleDbType.Date)
               return typeof(DateTime);
           if (anOleDbType == OleDbType.DBDate)
               return typeof(DateTime);
           if (anOleDbType == OleDbType.DBTime)
               return typeof(TimeSpan);
           if (anOleDbType == OleDbType.DBTimeStamp)
               return typeof(DateTime);
           if (anOleDbType == OleDbType.Decimal)
               return typeof(Decimal);
           if (anOleDbType == OleDbType.Double)
               return typeof(Double);
           if (anOleDbType == OleDbType.Empty)
               return typeof(DBNull);
           if (anOleDbType == OleDbType.Guid)
               return typeof(Guid);
           if (anOleDbType == OleDbType.IDispatch)
               return typeof(Object);
           if (anOleDbType == OleDbType.Integer)
               return typeof(Int32);
           if (anOleDbType == OleDbType.LongVarBinary)
               return typeof(String);
           if (anOleDbType == OleDbType.LongVarChar)
               return typeof(String);
           if (anOleDbType == OleDbType.LongVarWChar)
               return typeof(String);
           if (anOleDbType == OleDbType.Numeric)
               return typeof(Decimal);
           if (anOleDbType == OleDbType.PropVariant)
               return typeof(Object);
           if (anOleDbType == OleDbType.Single)
               return typeof(Single);
           if (anOleDbType == OleDbType.SmallInt)
               return typeof(Int16);
           if (anOleDbType == OleDbType.TinyInt)
               return typeof(SByte);
           if (anOleDbType == OleDbType.UnsignedBigInt)
               return typeof(UInt64);
           if (anOleDbType == OleDbType.UnsignedInt)
               return typeof(UInt32);
           if (anOleDbType == OleDbType.UnsignedSmallInt)
               return typeof(UInt16);
           if (anOleDbType == OleDbType.UnsignedTinyInt)
               return typeof(Byte);
           if (anOleDbType == OleDbType.VarBinary)
               return typeof(Byte[]);
           if (anOleDbType == OleDbType.VarChar)
               return typeof(String);
           if (anOleDbType == OleDbType.Variant)
               return typeof(Object);
           if (anOleDbType == OleDbType.VarNumeric)
               return typeof(Decimal);
           if (anOleDbType == OleDbType.VarWChar)
               return typeof(String);
           if (anOleDbType == OleDbType.WChar)
               return typeof(String);

           return typeof(Object);
       }

       //Pascal case: GreenButtonType. 
       //Camel case: myInt 
       public static string ConvertToPascalCase(string strKeyWord)
       {

           return strKeyWord.Substring(0, 1).ToUpper() + strKeyWord.Substring(1);
       }


       public static string ConvertToCamelCase(string strKeyWord)
       {

           return strKeyWord.Substring(0, 1).ToLower();
       }

    
    }
}
