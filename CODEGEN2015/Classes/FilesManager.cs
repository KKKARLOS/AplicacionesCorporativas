using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;
namespace CodeGenerator2005
{
    public class FilesManager
    {
        #region Private Variables
        private FilesSettingInfo _FilesSettingInfo;
        #endregion

        #region Constructors

        public FilesManager(FilesSettingInfo objFilesSettingInfo)
        {
            _FilesSettingInfo = objFilesSettingInfo;
        }
        #endregion

        #region Methods

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into it's new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }


        #region Code DO /DBFactory/DA/Bus
        #region Helper Methods
        private string GetDataNameSpace(string strProvider, CodeLanguage objCodeLanguage)
        {
            string strUsingNameSpace = "";
            if (strProvider.ToLower().Trim() == "sql") { strProvider += "Client"; }

            if (objCodeLanguage == CodeLanguage.CS)
            {
                strUsingNameSpace += "using System.Data." + strProvider + ";";
            }
            else if (objCodeLanguage == CodeLanguage.VB)
            {
                strUsingNameSpace += "Imports System.Data." + strProvider;
            }
            return strUsingNameSpace;

        }
        private string GetNameSpace(string NameSpace, CodeLanguage objCodeLanguage)
        {
            string strUsingNameSpace = "";
        
            if (objCodeLanguage == CodeLanguage.CS)
            {
                strUsingNameSpace += "using " + NameSpace + ";";
            }
            else if (objCodeLanguage == CodeLanguage.VB)
            {
                strUsingNameSpace += "Imports " + NameSpace;
            }
            return strUsingNameSpace;

        }
        private string GetVariable(ColumnInfo objColumnInfo, CodeLanguage objCodeLanguage)
        {
            string strPrivateVariable = "";
            if (objCodeLanguage == CodeLanguage.CS)
            {
                strPrivateVariable += "\t\t" + "private " + Utilities.GetSystemType(objColumnInfo.DBType.ToString(), objCodeLanguage) + " _" + objColumnInfo.Name + ";\n";
            }
            else if (objCodeLanguage == CodeLanguage.VB)
            {
                strPrivateVariable += "\t\t" + "Private " + " _" + objColumnInfo.Name + " As " + Utilities.GetSystemType(objColumnInfo.DBType.ToString(), objCodeLanguage) + "\n";
            }
            return strPrivateVariable;

        }
        private string GetProperty(ColumnInfo objColumnInfo, CodeLanguage objCodeLanguage)
        {

            string strPublicProperty = "";
            if (objCodeLanguage == CodeLanguage.CS)
            {
                strPublicProperty += "\t\t" + "public " + Utilities.GetSystemType(objColumnInfo.DBType.ToString(), objCodeLanguage) + " " + objColumnInfo.Name + "\n";
                strPublicProperty += "\t\t" + "{" + "\n";

                strPublicProperty += "\t\t\t" + "get";
                strPublicProperty += "{";
                strPublicProperty += "return" + " _" + objColumnInfo.Name + ";";
                strPublicProperty += "}" + "\n";

                strPublicProperty += "\t\t\t" + "set";
                strPublicProperty += "{";
                strPublicProperty += "_" + objColumnInfo.Name + " = value;";
                strPublicProperty += "}" + "\n";

                strPublicProperty += "\t\t" + "}" + "\n\n";
            }

            else if (objCodeLanguage == CodeLanguage.VB)
            {
                strPublicProperty += "\t\t" + "Public Property " + objColumnInfo.Name + "() As " + Utilities.GetSystemType(objColumnInfo.DBType.ToString(), objCodeLanguage) + "\n";
                strPublicProperty += "\t\t\t" + "Get" + "\n";
                strPublicProperty += "\t\t\t\t" + "Return" + " _" + objColumnInfo.Name + "\n";
                strPublicProperty += "\t\t\t" + "End Get" + "\n";
                strPublicProperty += "\t\t\t" + "Set(ByVal value As " + Utilities.GetSystemType(objColumnInfo.DBType.ToString(), objCodeLanguage) + ")" + "\n";
                strPublicProperty += "\t\t\t\t" + "_" + objColumnInfo.Name + " = value" + "\n";
                strPublicProperty += "\t\t\t" + "End Set" + "\n";
                strPublicProperty += "\t\t" + "End Property" + "\n";
            }
            return strPublicProperty;

        }
        private string GetVariable(string VariableName, string VariableType, string VariableValue, CodeLanguage objCodeLanguage)
        {
            string VS = ""; // C# >> Query Variable ="string strQuery=\"\";"  ,VB Query Variable ="Dim  strQuery As string=\"\"" 
              if (objCodeLanguage == CodeLanguage.CS)
            {
                VS = VariableType + " " + VariableName + " = " + VariableValue + ";";
             }
             else if (objCodeLanguage == CodeLanguage.VB)
            {
                 VS = "Dim " + VariableName + " As " + VariableType;
                 if(VariableValue !=null&& VariableValue.Length>0)
                 VS += " = "+ VariableValue;
             }
            return VS;
        }
        private string GetVariableDefinition(string VariableName, string VariableType, string VariableValue, CodeLanguage objCodeLanguage)
        {
            string VS = ""; // C# >> Query Variable ="string strQuery=\"\";"  ,VB Query Variable ="Dim  strQuery As string=\"\"" 
            if (objCodeLanguage == CodeLanguage.CS)
            {
                VS = VariableType + " " + VariableName;
                if (VariableValue != null && VariableValue.Length > 0)
                    VS += " = " + VariableValue + ";";
                else
                    VS += ";";
            }
            else if (objCodeLanguage == CodeLanguage.VB)
            {
                VS = "Dim " + VariableName + " As " + VariableType;
                if (VariableValue != null && VariableValue.Length > 0)
                    VS += " = " + VariableValue;
            }
            return VS;
        }
        private string GetVariableInitiation(string VariableName, string VariableValue, CodeLanguage objCodeLanguage)
        {
            string VS = ""; 
            if (objCodeLanguage == CodeLanguage.CS)
            {
                VS = VariableName + " = " + VariableValue + ";";
            }
            else if (objCodeLanguage == CodeLanguage.VB)
            {
                if (VariableValue != null && VariableValue.Length > 0)
                    VS += VariableName +" = " + VariableValue;
            }
            return VS;
        }
        private string GetMethodParameter(string ParamName, string ParamType, string ParamPathType, CodeLanguage objCodeLanguage)
        {
            string P = ""; 
            if (objCodeLanguage == CodeLanguage.CS)
            {
                P = ParamType + " " + ParamName;
            }
            else if (objCodeLanguage == CodeLanguage.VB)
            {
                P = ParamPathType + " " + ParamName + " As " + ParamType;
                
            }
            return P;
        }
        private void   SetDAMethods(ref string strTemplateDA, TableInfo objTableInfo, FilesSettingInfo objFilesSettingInfo)
        {
            #region Define Variables
            string strPrivateEnumDBFields = "";
            string strCommandType = "";
            string strFields = "";
            string strPKWhere = "";
            string strPKCommandParameters = "";
            string strPKDBParameters = "";
            string strPKParameters = "";
            string strFKParameters = ""; //se usan en IB_DA_TEMPLATE para sobrecarga de catalogo
            string strInitiateParameter = "";
            
            byte bEnumerator = 1;
            byte bPKEnumerator = 0;
            byte bFKEnumerator = 0;  

            
            string strDOName = objTableInfo.Name.Substring(objTableInfo.Name.IndexOf("_") + 1) ;
            string strParamObj ="";
            
            if (objFilesSettingInfo.UseDataObject) strParamObj = "o" + strDOName + ".";
            
            string ES = ""; // C# >> End Of Statment=";" , Vb >> End Of Statment=""
            string CO = "";//+ &
            string SNL = "";//\n _ seperate statement

            #region General
            string strEnumDbInf = ""; //Se usa en la plantilla IB_DA_TEMPLATE
            string strLength = "";
            #endregion

            #region Insert
            string strInsertQuery = "";
            string strInsertParameters = "";
            string strInsertCommandText = "";
            string strInsertCommandParameters = "";
            string strInsertEnumParameters = ""; //Se usa en la plantilla IB_DA_TEMPLATE
            string strInsertReturnParameter = "";

            string strInsertFields = "";
            string strInsertValues = "";

            #endregion

            #region Update
            string strUpdateQuery = "";
            string strUpdateParameters = "";
            string strUpdateEnumParameters = "";
            string strUpdateCommandText = "";
            string strUpdateCommandParameters = "";

            string strUpdateFields = "";
            #endregion

            #region Select
            string strSelectQuery = "";
            string strSelectCommandText = "";
            string strSelectReturnType = "";
            string strSelectMapping = "";
            string strSelectReturn = "";
            string strSelectDBMapping = "";//Se usa en la plantilla IB_DA_TEMPLATE
            string strSelectDBReturn = "";//Se usa en la plantilla IB_DA_TEMPLATE

            #endregion

            #region Select All
            string strSelectAllQuery = "";
            string strSelectAllCommandText = "";
            string strSelectAllReturnType = "";
            string strSelectAllReturnObject = "";
            string strSelectAllMapping = "";
            string strSelectAllReturn = "";
            string strSelectAllParameters = ""; //Se usa en la plantilla IB_DA_TEMPLATE
            string strSelectAllDBParameters = ""; //Se usa en la plantilla IB_DA_TEMPLATE
            
            #endregion

            #region Delete
            string strDeleteQuery = "";
            string strDeleteCommandText = "";
            #endregion

            #region Delete All
            string strDeleteAllQuery = "";
            string strDeleteAllCommandText = "";
            #endregion

            #endregion

            #region Code C#
            if (objFilesSettingInfo.CodeLanguage == CodeLanguage.CS)
            {
                ES = ";";
                CO = " + ";
                strSelectAllMapping += "foreach(DataRow dr in dt.Rows)\n";
                strSelectAllMapping += "{\n";
                strSelectAllMapping += strDOName + " obj" + strDOName + "= new " + strDOName + "();\n";
                strEnumDbInf = "switch (dbField)\n\t\t\t{\n";

                #region Get Column Data
                string strNameVar = "o" + objTableInfo.Name + "Filter.";
                foreach (ColumnInfo objColumnInfo in objTableInfo.arrColumns)
                {
                    
                    #region General
                    string separated = Utilities.GetSingleQuote(objColumnInfo.DBType);
                    strFields += objColumnInfo.Name + ",\" " + CO + " \n\"";
                    strLength = (objColumnInfo.Length > 0 && objColumnInfo.Length <= 2147483647) ? objColumnInfo.Length.ToString() : DBManager.DbTypeLenght(objColumnInfo.DBType);

                    if (objColumnInfo.IsPrimaryKey)
                    {
                        //PK Where Query
                        strPKWhere += objColumnInfo.Name + "=" + separated + "\" " + CO + " " + strParamObj + objColumnInfo.Name + " " + CO + " \"" + separated + "\"  and ";


                        //PK Method Parameters
                        strPKParameters += Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage) + " " + objColumnInfo.Name;
                        strPKParameters += ",";

                        //PK Command Parameter
                        strPKCommandParameters += objFilesSettingInfo.DAProviderType + "param = new " + objFilesSettingInfo.DAProviderType + "Parameter(\"@" + objColumnInfo.Name + "\", " + Utilities.GetDBTypeEnum(objColumnInfo.DBType, objFilesSettingInfo.DAProviderType) + ");" + "\n";
                        strPKCommandParameters += objFilesSettingInfo.DAProviderType + "param.Value = " + strParamObj + objColumnInfo.Name + ";" + "\n";
                        strPKCommandParameters += objFilesSettingInfo.DAProviderType + "comm.Parameters.Add(" + objFilesSettingInfo.DAProviderType + "param);" + "\n\n";
                                               
                        strPKDBParameters += "\t\t\t\t\tParam(enumDBFields." + objColumnInfo.Name + ", " + objColumnInfo.Name + "),\n";
                        bPKEnumerator++;
                    }
                    

                    strPrivateEnumDBFields += objColumnInfo.Name + " = " + bEnumerator++.ToString() + ",\n\t\t\t";
                    strEnumDbInf += "\t\t\t\tcase enumDBFields." + objColumnInfo.Name + ":\n";
                    strEnumDbInf += "\t\t\t\t\tparamName = \"@" + objColumnInfo.Name + "\";\n";
                    strEnumDbInf += "\t\t\t\t\tparamType = SqlDbType." + Utilities.GetParamsType(objColumnInfo.DBType.ToString(), CodeLanguage.CS) + ";\n";
                    strEnumDbInf += "\t\t\t\t\tparamSize = " + strLength + ";\n";
                    strEnumDbInf += "\t\t\t\t\tbreak;\n";

                    #endregion

                    #region Insert

                    if (!objColumnInfo.IsIdentity)
                    {
                        //Insert Query
                        strInsertFields += objColumnInfo.Name + ",";

                        strInsertValues += separated + "\" " + CO + " " + strParamObj + objColumnInfo.Name + " " + CO + " \"" + separated + ",\" " + CO + " \n\"";

                        //Insert Method Parameters
                        strInsertParameters += Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage) + " " + objColumnInfo.Name;
                        strInsertParameters += ",";

                        //insert Command Parameter
                        strInsertCommandParameters += objFilesSettingInfo.DAProviderType + "param = new " + objFilesSettingInfo.DAProviderType + "Parameter(\"@" + objColumnInfo.Name + "\", " + Utilities.GetDBTypeEnum(objColumnInfo.DBType, objFilesSettingInfo.DAProviderType) + ");" + "\n";
                        strInsertCommandParameters += objFilesSettingInfo.DAProviderType + "param.Value = " + strParamObj + objColumnInfo.Name + ";" + "\n";
                        strInsertCommandParameters += objFilesSettingInfo.DAProviderType + "comm.Parameters.Add(" + objFilesSettingInfo.DAProviderType + "param);" + "\n\n";
                        
                        //insert Command Parameter with Param Function
                        strInsertEnumParameters += "\t\t\t\t\tParam(enumDBFields." + objColumnInfo.Name + ", " + strParamObj + objColumnInfo.Name + "),\n";
                    }

                    #endregion

                    #region Update

                    //Update Method Parameters
                    strUpdateParameters += Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage) + " " + objColumnInfo.Name;
                    strUpdateParameters += ",";

                    //Update Command Parameter
                    strUpdateCommandParameters += objFilesSettingInfo.DAProviderType + "param = new " + objFilesSettingInfo.DAProviderType + "Parameter(\"@" + objColumnInfo.Name + "\", " + Utilities.GetDBTypeEnum(objColumnInfo.DBType, objFilesSettingInfo.DAProviderType) + ");" + "\n";
                    strUpdateCommandParameters += objFilesSettingInfo.DAProviderType + "param.Value = " + strParamObj + objColumnInfo.Name + ";" + "\n";
                    strUpdateCommandParameters += objFilesSettingInfo.DAProviderType + "comm.Parameters.Add(" + objFilesSettingInfo.DAProviderType + "param);" + "\n\n";

                    //Update Query
                    if (!objColumnInfo.IsIdentity)
                    {
                        strUpdateFields += objColumnInfo.Name + "=" + separated + "\" " + CO + " " + strParamObj + objColumnInfo.Name + " " + CO + " \"" + separated + ",\" " + CO + " \n\"";
                    }

                    //Update Command Parameter with Param Function
                    strUpdateEnumParameters += "\t\t\t\t\tParam(enumDBFields." + objColumnInfo.Name + ", " + strParamObj + objColumnInfo.Name + "),\n";
                                    
                    #endregion

                    #region Select
                    strSelectMapping += "if(!Convert.IsDBNull(dr[\"" + objColumnInfo.Name + "\"]" + "))" + "\n";
                    strSelectMapping += "obj" + strDOName + "." + objColumnInfo.Name + "=";
                    //Cambio para tratar los tipos Guid
                    if (Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage) == "Guid")
                    {
                        strSelectMapping += "new Guid(dr[\"" + objColumnInfo.Name + "\"].ToString())" + ";\n";
                    }
                    else
                    {
                        strSelectMapping += "Convert.To" + Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage) + "(dr[\"" + objColumnInfo.Name + "\"])" + ";\n";
                    }    
                    //
                    
                    if(objColumnInfo.IsAllowNull)
                        strSelectDBMapping += "\t\t\t\t\tif(!Convert.IsDBNull(dr[\"" + objColumnInfo.Name + "\"]" + "))" + "\n\t";
                    strSelectDBMapping += "\t\t\t\t\to" + strDOName + "." + objColumnInfo.Name + "=";
                    //Cambio para tratar los tipos Guid
                    if (Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage) == "Guid")
                    {
                        strSelectDBMapping += "new Guid(dr[\"" + objColumnInfo.Name + "\"].ToString())" + ";\n";
                    }
                    else
                    {
                        strSelectDBMapping += "Convert.To" + Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage) + "(dr[\"" + objColumnInfo.Name + "\"])" + ";\n";
                    }
                    //
                    #endregion

                    #region Select All
                    strSelectAllMapping += "if(!Convert.IsDBNull(dr[\"" + objColumnInfo.Name + "\"]" + "))" + "\n";
                    strSelectAllMapping += "obj" + strDOName + "." + objColumnInfo.Name + "=";
                    strSelectAllMapping += "Convert.To" + Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage) + "(dr[\"" + objColumnInfo.Name + "\"])" + ";\n";

                    if (objColumnInfo.IsForeignKey) //solución de varias sobrecargas PENDIENTE USAR
                    {
                        strFKParameters += "\t\t\t\t\tParam(enumDBFields." + objColumnInfo.Name + ", " + strParamObj + objColumnInfo.Name + ((objColumnInfo.Length == 0) ? ".ToString()" : "") + "),\n";
                        bFKEnumerator++;
                    }

                    if (!objColumnInfo.IsPrimaryKey)
                    {
                        //SelectAll Method Parameters 
                        strSelectAllParameters += Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage) + " " + objColumnInfo.Name;
                        strSelectAllParameters += ",";

                        //SelectAll Command Parameter with Param Function
                        //strSelectAllDBParameters += "\t\t\t\t\tParam(enumDBFields." + objColumnInfo.Name + ", " + strNameVar + objColumnInfo.Name + "),\n";
                        strSelectAllDBParameters += "\t\t\t\t\tParam(enumDBFields." + objColumnInfo.Name + ", " + strParamObj.Replace(".","") + "Filter." + objColumnInfo.Name + "),\n";

                    }
                    #endregion


                }
               
                #endregion

                strSelectAllMapping += "arr" + strDOName + ".Add(obj" + strDOName + ");\n";
                strSelectAllMapping += "}";
                strInsertReturnParameter += "if(!Convert.IsDBNull(" + objFilesSettingInfo.DAProviderType + "comm.Parameters[\"@ID\"]))" + "\n";
                strInsertReturnParameter += "\t\t\t\tresult = Convert.ToInt32(" + objFilesSettingInfo.DAProviderType + "comm.Parameters[\"@ID\"].Value)" + ES + "\n";

                strEnumDbInf += "\t\t\t}\n";
            }
            #endregion

            #region Code VB
            else if (objFilesSettingInfo.CodeLanguage == CodeLanguage.VB)
            {
                ES = "";
                CO = " & ";
                strSelectAllMapping += " For Each dr As DataRow In ds.Tables(0).Rows\n";
                strSelectAllMapping += " Dim obj" + strDOName + " As New " + strDOName + "()\n";

                #region Get Column Data
                foreach (ColumnInfo objColumnInfo in objTableInfo.arrColumns)
                {
                    #region General
                    string separated = Utilities.GetSingleQuote(objColumnInfo.DBType);
                    strFields += objColumnInfo.Name + ",\" " + CO + " \"";

                    if (objColumnInfo.IsPrimaryKey)
                    {
                        //PK Where Query

                        strPKWhere += objColumnInfo.Name + "=" + separated + "\" " + CO + " " + strParamObj + objColumnInfo.Name + " " + CO + " \"" + separated + "\"  and ";

                        //PK Method Parameters
                        strPKParameters += Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage) + " " + objColumnInfo.Name;
                        strPKParameters += ",";

                        //PK Command Parameter
                        strPKCommandParameters += objFilesSettingInfo.DAProviderType + "param = new " + objFilesSettingInfo.DAProviderType + "Parameter(\"@" + objColumnInfo.Name + "\", " + Utilities.GetDBTypeEnum(objColumnInfo.DBType, objFilesSettingInfo.DAProviderType) + ")" + "\n";
                        strPKCommandParameters += objFilesSettingInfo.DAProviderType + "param.Value = " + strParamObj + objColumnInfo.Name  + "\n";
                        strPKCommandParameters += objFilesSettingInfo.DAProviderType + "comm.Parameters.Add(" + objFilesSettingInfo.DAProviderType + "param)" + "\n\n";

                    }
                    #endregion

                    #region Insert

                    if (!objColumnInfo.IsIdentity)
                    {
                        //Insert Query

                        strInsertFields += objColumnInfo.Name + ",";

                        strInsertValues += separated + "\" " + CO + " " + strParamObj + objColumnInfo.Name + " " + CO + " \"" + separated + ",\" " + CO + " \"";


                        //Insert Method Parameters
                        strInsertParameters += Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage) + " " + objColumnInfo.Name;
                        strInsertParameters += ",";

                        //insert Command Parameter
                        strInsertCommandParameters += objFilesSettingInfo.DAProviderType + "param = new " + objFilesSettingInfo.DAProviderType + "Parameter(\"@" + objColumnInfo.Name + "\", " + Utilities.GetDBTypeEnum(objColumnInfo.DBType, objFilesSettingInfo.DAProviderType) + ")" + "\n";
                        strInsertCommandParameters += objFilesSettingInfo.DAProviderType + "param.Value = " + strParamObj + objColumnInfo.Name + "\n";
                        strInsertCommandParameters += objFilesSettingInfo.DAProviderType + "comm.Parameters.Add(" + objFilesSettingInfo.DAProviderType + "param)" + "\n\n";
                    }

                    #endregion

                    #region Update

                    //Update Method Parameters
                    strUpdateParameters += Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage) + " " + objColumnInfo.Name;
                    strUpdateParameters += ",";

                    //Update Command Parameter
                    strUpdateCommandParameters += objFilesSettingInfo.DAProviderType + "param = new " + objFilesSettingInfo.DAProviderType + "Parameter(\"@" + objColumnInfo.Name + "\", " + Utilities.GetDBTypeEnum(objColumnInfo.DBType, objFilesSettingInfo.DAProviderType) + ")" + "\n";
                    strUpdateCommandParameters += objFilesSettingInfo.DAProviderType + "param.Value = " + strParamObj + objColumnInfo.Name + "\n";
                    strUpdateCommandParameters += objFilesSettingInfo.DAProviderType + "comm.Parameters.Add(" + objFilesSettingInfo.DAProviderType + "param)" + "\n\n";

                    //Update Query
                    if (!objColumnInfo.IsIdentity)
                    {
                        strUpdateFields += objColumnInfo.Name + "=" + separated + "\" " + CO + " " + strParamObj + objColumnInfo.Name + " + \"" + separated + ",\" " + CO + " \"";
                    }


                    #endregion

                    #region Select
                    strSelectMapping += "If Not IsDBNull(dr(\"" + objColumnInfo.Name + "\")) Then" + "\n";
                    strSelectMapping += "obj" + strDOName + "." + objColumnInfo.Name + "=";
                    strSelectMapping += "Convert.To" + Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage) + "(dr(\"" + objColumnInfo.Name + "\"))" + "\n";
                    strSelectMapping += "End If"+"\n";
                    #endregion

                    #region Select All
                    strSelectAllMapping += "If Not IsDBNull(dr(\"" + objColumnInfo.Name + "\")) Then" + "\n";
                    strSelectAllMapping += "obj" + strDOName + "." + objColumnInfo.Name + "=";
                    strSelectAllMapping += "Convert.To" + Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage) + "(dr(\"" + objColumnInfo.Name + "\"))" + "\n";
                    strSelectAllMapping += "End If" + "\n";
                    #endregion

                }

                #endregion

                strSelectAllMapping += "arr" + strDOName + ".Add(obj" + strDOName + ")\n";
                strSelectAllMapping += "Next";
                strInsertReturnParameter += "IF Not IsDBNull(" + objFilesSettingInfo.DAProviderType + "comm.Parameters(\"@ID\")) Then" + "\n";
                strInsertReturnParameter += "result = Convert.ToInt32(" + objFilesSettingInfo.DAProviderType + "comm.Parameters(\"@ID\").Value)" + ES + "\n";
                strInsertReturnParameter += "End IF";

            }
            #endregion

            #region   Methods  Parameter and Return

            if (strInsertEnumParameters.LastIndexOf(',') > -1) strInsertEnumParameters = "SqlParameter[] dbparams = new SqlParameter[" + (objTableInfo.arrColumns.Count - bPKEnumerator) + "] {\n" + (strInsertEnumParameters.Remove(strInsertEnumParameters.LastIndexOf(','))) + "\n\t\t\t\t};";
            if (strPKDBParameters.LastIndexOf(',') > -1) strPKDBParameters = "SqlParameter[] dbparams = new SqlParameter[" + bPKEnumerator + "] {\n" + (strPKDBParameters.Remove(strPKDBParameters.LastIndexOf(','))) + "\n\t\t\t\t};";
            if (strPKParameters.LastIndexOf(',') > -1) strPKParameters = strPKParameters.Remove(strPKParameters.LastIndexOf(','));
            if (strUpdateEnumParameters.LastIndexOf(',') > -1) strUpdateEnumParameters = "SqlParameter[] dbparams = new SqlParameter[" + (objTableInfo.arrColumns.Count) + "] {\n" + (strUpdateEnumParameters.Remove(strUpdateEnumParameters.LastIndexOf(','))) + "\n\t\t\t\t};";
            if (strSelectAllParameters.LastIndexOf(',') > -1) strSelectAllParameters = strSelectAllParameters.Remove(strSelectAllParameters.LastIndexOf(','));
            if (strSelectAllDBParameters.LastIndexOf(',') > -1) strSelectAllDBParameters = "SqlParameter[] dbparams = new SqlParameter[" + (objTableInfo.arrColumns.Count - bPKEnumerator) + "] {\n" + (strSelectAllDBParameters.Remove(strSelectAllDBParameters.LastIndexOf(','))) + "\n\t\t\t\t};";
            if (strFKParameters.LastIndexOf(',') > -1) strFKParameters = "DbParameter[] dbparams = new DbParameter[" + bFKEnumerator + "] {\n" + (strFKParameters.Remove(strPKDBParameters.LastIndexOf(','))) + "\n\t\t\t\t};";


            if (objFilesSettingInfo.UseDataObject)
            {
                strInsertParameters = GetMethodParameter("o" + strDOName, "Models." + strDOName, "ByRef", objFilesSettingInfo.CodeLanguage);
                strUpdateParameters = GetMethodParameter("o" + strDOName, "Models." + strDOName, "ByRef", objFilesSettingInfo.CodeLanguage);
                
                // se usa la de arriba (línea antes del UseDataObject)
                //strPKParameters     = GetMethodParameter("obj" + strDOName,strDOName,"ByRef", objFilesSettingInfo.CodeLanguage);

                strSelectDBMapping = "o" + strDOName + " = new Models." + strDOName + "();\n" + strSelectDBMapping;

                strSelectReturnType = strDOName;
                strSelectReturn += "return " + "obj" + strDOName + ES;
                strSelectDBReturn += "o" + strDOName;

                strSelectAllReturnType = "ArrayList";
                strSelectAllReturnObject += GetVariable("arr" + strDOName, "ArrayList", "new ArrayList()",objFilesSettingInfo.CodeLanguage);
                strSelectAllReturn += "return " + "arr" + strDOName + ES;
            }
            else
            {
                if (strInsertParameters.LastIndexOf(',') > -1) strInsertParameters = (strInsertParameters.Remove(strInsertParameters.LastIndexOf(',')));
                if (strUpdateParameters.LastIndexOf(',') > -1) strUpdateParameters = (strUpdateParameters.Remove(strUpdateParameters.LastIndexOf(',')));
                if (strPKParameters.LastIndexOf(',') > -1) strPKParameters = (strPKParameters.Remove(strPKParameters.LastIndexOf(',')));

                strParamObj = "";
                strSelectReturnType = "DataRow";
                strSelectMapping = "";
                strSelectReturn = "return dr"+ES;


                strSelectAllReturnType = "DataTable";
                strSelectAllReturnObject = "";
                strSelectAllMapping = "";
                strSelectAllReturn = "return dt"+ES;
            }

            strInsertCommandParameters += objFilesSettingInfo.DAProviderType + "param = new " + objFilesSettingInfo.DAProviderType + "Parameter(\"@ID\", " + objFilesSettingInfo.DAProviderType + "DbType.Int)"+ES + "\n";
            strInsertCommandParameters += objFilesSettingInfo.DAProviderType + "param.Direction = ParameterDirection.ReturnValue" + ES + "\n";
            strInsertCommandParameters += objFilesSettingInfo.DAProviderType + "comm.Parameters.Add(" + objFilesSettingInfo.DAProviderType + "param)" + ES + "\n";

            #endregion

            #region   Command Type And Text
            if (objFilesSettingInfo.DACommandType == "StoredProcedure")
            {
                string spName = objFilesSettingInfo.AppName + "_" + (objTableInfo.Name.Substring(objTableInfo.Name.IndexOf("_") + 1)); //En código origintal es --> objTableInfo.Name
                strCommandType = "CommandType.StoredProcedure";
                strInsertCommandText = "\"" + objFilesSettingInfo.SPBeforeInsert + spName + objFilesSettingInfo.SPAfterInsert + "\"";
                strUpdateCommandText = "\"" + objFilesSettingInfo.SPBeforeUpdateByPK + spName + objFilesSettingInfo.SPAfterUpdateByPK + "\"";
                strSelectCommandText = "\"" + objFilesSettingInfo.SPBeforeSelectByPK + spName + objFilesSettingInfo.SPAfterSelectByPK + "\"";
                strDeleteAllCommandText = "\"" + objFilesSettingInfo.SPBeforeDeleteAll + spName + objFilesSettingInfo.SPAfterDeleteAll + "\"";
                strDeleteCommandText = "\"" + objFilesSettingInfo.SPBeforeDeleteByPK + spName + objFilesSettingInfo.SPAfterDeleteByPK + "\"";
                strSelectAllCommandText = "\"" + objFilesSettingInfo.SPBeforeSelectAll + spName + objFilesSettingInfo.SPAfterSelectAll + "\"";
                strInitiateParameter = GetVariableDefinition(objFilesSettingInfo.DAProviderType + "param", objFilesSettingInfo.DAProviderType + "Parameter","", objFilesSettingInfo.CodeLanguage);
            }

            else
            {
                strCommandType = "CommandType.Text";
                if (strFields.LastIndexOf(',') > -1) strFields = (strFields.Remove(strFields.LastIndexOf(',')));
                if (strPKWhere.LastIndexOf(" and ") > -1) strPKWhere = (strPKWhere.Remove(strPKWhere.LastIndexOf(" and ")));


                if (strInsertFields.LastIndexOf(',') > -1) strInsertFields = (strInsertFields.Remove(strInsertFields.LastIndexOf(',')));
                if (strInsertValues.LastIndexOf(',') > -1) strInsertValues = (strInsertValues.Remove(strInsertValues.LastIndexOf(',')));

                strInsertQuery = GetVariable("strOuery", "string", "\"\"",objFilesSettingInfo.CodeLanguage)+"\n";
                strInsertQuery += "strOuery+=\"Insert Into " + "\""+ES+"\n";
                strInsertQuery += "strOuery+=\" " + objTableInfo.Name + " \""+ES+"\n";
                strInsertQuery += "strOuery+=\"" + " ( " + "\"" + ES + "\n";
                strInsertQuery += "strOuery+=\"" + strInsertFields + "\"" + ES + "\n";
                strInsertQuery += "strOuery+=\"" + " ) " + "\"" + ES + "\n";
                strInsertQuery += "strOuery+=\"" + " Values " + "\"" + ES + "\n";
                strInsertQuery += "strOuery+=\"" + " ( " + "\"" + ES + "\n";
                strInsertQuery += "strOuery+=\"" + strInsertValues + "\"" + ES + "\n";
                strInsertQuery += "strOuery+=\"" + " ) " + "\"" + ES + "\n";
                strInsertCommandText = "strOuery";

                if (strUpdateFields.LastIndexOf(',') > -1) strUpdateFields = (strUpdateFields.Remove(strUpdateFields.LastIndexOf(',')));
                strUpdateQuery = GetVariable("strOuery", "string", "\"\"", objFilesSettingInfo.CodeLanguage) + "\n";
                strUpdateQuery += "strOuery+=\"Update  " + "\"" + ES + "\n";
                strUpdateQuery += "strOuery+=\" " + objTableInfo.Name + " \"" + ES + "\n";
                strUpdateQuery += "strOuery+=\"" + " Set " + "\"" + ES + "\n";
                strUpdateQuery += "strOuery+=\"" + strUpdateFields + "\"" + ES + "\n";
                strUpdateQuery += "strOuery+=\"" + " where " + "\"" + ES + "\n";
                strUpdateQuery += "strOuery+=\"" + strPKWhere + "" + ES + "";
                strUpdateCommandText = "strOuery";

                strSelectQuery = GetVariable("strOuery", "string", "\"\"", objFilesSettingInfo.CodeLanguage) + "\n";
                strSelectQuery += "strOuery+=\"Select  " + "\"" + ES + "\n";
                strSelectQuery += "strOuery+=\"" + strFields + "\"" + ES + "\n";
                strSelectQuery += "strOuery+=\" From " + objTableInfo.Name + "\"" + ES + "\n";
                strSelectQuery += "strOuery+=\"" + " where " + "\"" + ES + "\n";
                strSelectQuery += "strOuery+=\"" + strPKWhere + "" + ES + "";
                strSelectCommandText = "strOuery";

                strSelectAllQuery = GetVariable("strOuery", "string", "\"\"", objFilesSettingInfo.CodeLanguage) + "\n";
                strSelectAllQuery += "strOuery +=\"Select  " + "\"" + ES + "\n";
                strSelectAllQuery += "strOuery+=\"" + strFields + "\"" + ES + "\n";
                strSelectAllQuery += "strOuery+=\" From " + objTableInfo.Name + "\"" + ES + "\n";
                strSelectAllCommandText = "strOuery";

                strDeleteQuery = GetVariable("strOuery", "string", "\"\"", objFilesSettingInfo.CodeLanguage) + "\n";
                strDeleteQuery += "strOuery+=\"Delete  " + "\"" + ES + "\n";
                strDeleteQuery += "strOuery+=\" From " + objTableInfo.Name + "\"" + ES + "\n";
                strDeleteQuery += "strOuery+=\"" + " where " + "\"" + ES + "\n";
                strDeleteQuery += "strOuery+=\"" + strPKWhere + "" + ES + "";
                strDeleteCommandText = "strOuery";

                strDeleteAllQuery = GetVariable("strOuery", "string", "\"\"", objFilesSettingInfo.CodeLanguage) + "\n";
                strDeleteAllQuery  += "strOuery+=\"Delete  " + "\"" + ES + "\n";
                strDeleteAllQuery  += "strOuery+=\" From " + objTableInfo.Name + "\"" + ES + "\n";
                strDeleteAllCommandText = "strOuery";

                strPKCommandParameters = "";

                strInsertCommandParameters = "";
                strInsertReturnParameter = "";

                strUpdateParameters = "";
                strUpdateCommandParameters = "";
            }

            if (strPrivateEnumDBFields.LastIndexOf(',') > -1) strPrivateEnumDBFields = (strPrivateEnumDBFields.Remove(strPrivateEnumDBFields.LastIndexOf(',')));
            #endregion

            #region Update Template

            strTemplateDA = strTemplateDA.Replace("#PrivateEnumDBFields#", strPrivateEnumDBFields);
            strTemplateDA = strTemplateDA.Replace("#CommandType#", strCommandType);
            strTemplateDA = strTemplateDA.Replace("#InitiateParameter#", strInitiateParameter);
            strTemplateDA = strTemplateDA.Replace("#PKParameters#", strPKParameters);
            strTemplateDA = strTemplateDA.Replace("#FKParameters#", strFKParameters);
            
            strTemplateDA = strTemplateDA.Replace("#PKCommandParameters#", strPKCommandParameters);
            strTemplateDA = strTemplateDA.Replace("#PKDBParameters#", strPKDBParameters);
            strTemplateDA = strTemplateDA.Replace("#EnumDBInf#", strEnumDbInf);

            strTemplateDA = strTemplateDA.Replace("#InsertQuery#", strInsertQuery);
            strTemplateDA = strTemplateDA.Replace("#InsertParameters#", strInsertParameters);
            strTemplateDA = strTemplateDA.Replace("#InsertEnumParameters#", strInsertEnumParameters);
            strTemplateDA = strTemplateDA.Replace("#InsertCommandText#", strInsertCommandText);
            strTemplateDA = strTemplateDA.Replace("#InsertCommandParameters#", strInsertCommandParameters);
            strTemplateDA = strTemplateDA.Replace("#InsertReturnParameter#", strInsertReturnParameter);

            strTemplateDA = strTemplateDA.Replace("#UpdateQuery#", strUpdateQuery);
            strTemplateDA = strTemplateDA.Replace("#UpdateCommandText#", strUpdateCommandText);
            strTemplateDA = strTemplateDA.Replace("#UpdateParameters#", strUpdateParameters);
            strTemplateDA = strTemplateDA.Replace("#UpdateCommandParameters#", strUpdateCommandParameters);
            strTemplateDA = strTemplateDA.Replace("#UpdateEnumParameters#", strUpdateEnumParameters);

            strTemplateDA = strTemplateDA.Replace("#SelectQuery#", strSelectQuery);
            strTemplateDA = strTemplateDA.Replace("#SelectCommandText#", strSelectCommandText);
            strTemplateDA = strTemplateDA.Replace("#SelectReturnType#", strSelectReturnType);
            strTemplateDA = strTemplateDA.Replace("#SelectReturn#", strSelectReturn);
            strTemplateDA = strTemplateDA.Replace("#SelectMapping#", strSelectMapping);
            strTemplateDA = strTemplateDA.Replace("#SelectDBMapping#", strSelectDBMapping);
            strTemplateDA = strTemplateDA.Replace("#SelectDBReturn#", strSelectDBReturn);

            strTemplateDA = strTemplateDA.Replace("#SelectAllQuery#", strSelectAllQuery);
            strTemplateDA = strTemplateDA.Replace("#SelectAllCommandText#", strSelectAllCommandText);
            strTemplateDA = strTemplateDA.Replace("#SelectAllReturnType#", strSelectAllReturnType);
            strTemplateDA = strTemplateDA.Replace("#SelectAllReturn#", strSelectAllReturn);
            strTemplateDA = strTemplateDA.Replace("#SelectAllMapping#", strSelectAllMapping);
            strTemplateDA = strTemplateDA.Replace("#SelectAllReturnObject#", strSelectAllReturnObject);
            strTemplateDA = strTemplateDA.Replace("#SelectAllParameters#", strSelectAllParameters);
            strTemplateDA = strTemplateDA.Replace("#SelectAllDBParameters#", strSelectAllDBParameters);
            
            strTemplateDA = strTemplateDA.Replace("#DeleteQuery#", strDeleteQuery);
            strTemplateDA = strTemplateDA.Replace("#DeleteCommandText#", strDeleteCommandText);
            strTemplateDA = strTemplateDA.Replace("#DeleteAllQuery#", strDeleteAllQuery);
            strTemplateDA = strTemplateDA.Replace("#DeleteAllCommandText#", strDeleteAllCommandText);
            #endregion
        }
        private void   SetBusMethods(ref string strTemplateBus, TableInfo objTableInfo, FilesSettingInfo objFilesSettingInfo)
        {
            #region Define Variables
            string strTableName = objTableInfo.Name.Substring(objTableInfo.Name.IndexOf("_")+1);
            string strDOName = objFilesSettingInfo.DOBeforeName + strTableName + objFilesSettingInfo.DOAfterName;
            string strPKParameters = "";
            string strInsertParameters = "";
            string strUpdateParameters = "";
            string strSelectReturnType = "";
            string strSelectAllReturnType = "";
            
            string strDAPKParameters = "";
            string strDAInsertParameters = "";
            string strDAUpdateParameters = "";
            string strDASelectAllParameters = "";
            string strSelectAllParameters = "";
            #endregion

            // Insert y Update con objetos (versión sin reemplazo de patrones, todo en la plantilla exceto nombre de la clase)
            // Select y Delete por PK
            // SelectAll con variables

            #region Get Method Parameters as Variables
            //if (! objFilesSettingInfo.UseDataObject)
            //{
                foreach (ColumnInfo objColumnInfo in objTableInfo.arrColumns)
                {
                    if (objColumnInfo.IsPrimaryKey)
                    {
                        //PK Method Parameters
                        strPKParameters += GetMethodParameter(objColumnInfo.Name, Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage), "ByVal", objFilesSettingInfo.CodeLanguage);
                        strPKParameters += ",";

                        //PK Method Parameters
                        strDAPKParameters += objColumnInfo.Name;
                        strDAPKParameters += ",";
                    }
                    else {
                        //SelectAll Method Parameters
                        strSelectAllParameters += GetMethodParameter(objColumnInfo.Name, Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage), "ByVal", objFilesSettingInfo.CodeLanguage);
                        strSelectAllParameters += ",";

                        //SelectAll Method Parameters
                        strDASelectAllParameters += objColumnInfo.Name;
                        strDASelectAllParameters += ",";
                    }

                    if (!objColumnInfo.IsIdentity)
                    {
                        //Insert Method Parameters
                        strInsertParameters += GetMethodParameter(objColumnInfo.Name, Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage), "ByVal", objFilesSettingInfo.CodeLanguage);
                        strInsertParameters += ",";

                        //Insert Method Parameters
                        strDAInsertParameters += objColumnInfo.Name;
                        strDAInsertParameters += ",";
                    }

                    //Update Method Parameters
                    strUpdateParameters += GetMethodParameter(objColumnInfo.Name, Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage), "ByVal", objFilesSettingInfo.CodeLanguage);
                    strUpdateParameters += ",";

                    //Update Method Parameters
                    strDAUpdateParameters += objColumnInfo.Name;
                    strDAUpdateParameters += ",";
                }

                if (strInsertParameters.LastIndexOf(',') > -1) strInsertParameters = (strInsertParameters.Remove(strInsertParameters.LastIndexOf(',')));
                if (strUpdateParameters.LastIndexOf(',') > -1) strUpdateParameters = (strUpdateParameters.Remove(strUpdateParameters.LastIndexOf(',')));
                if (strPKParameters.LastIndexOf(',') > -1) strPKParameters = (strPKParameters.Remove(strPKParameters.LastIndexOf(',')));
                if (strSelectAllParameters.LastIndexOf(',') > -1) strSelectAllParameters = (strSelectAllParameters.Remove(strSelectAllParameters.LastIndexOf(',')));

                if (strDAInsertParameters.LastIndexOf(',') > -1) strDAInsertParameters = (strDAInsertParameters.Remove(strDAInsertParameters.LastIndexOf(',')));
                if (strDAUpdateParameters.LastIndexOf(',') > -1) strDAUpdateParameters = (strDAUpdateParameters.Remove(strDAUpdateParameters.LastIndexOf(',')));
                if (strDAPKParameters.LastIndexOf(',') > -1) strDAPKParameters = (strDAPKParameters.Remove(strDAPKParameters.LastIndexOf(',')));
                if (strDASelectAllParameters.LastIndexOf(',') > -1) strDASelectAllParameters = (strDASelectAllParameters.Remove(strDASelectAllParameters.LastIndexOf(',')));

                strSelectReturnType = "DataRow";
                strSelectAllReturnType = "DataTable";
            //}
           #endregion

            #region  Get Method Parameters as Object
            //else 
            //{
                strInsertParameters = GetMethodParameter("o" + strDOName, "Models." + strDOName, "ByRef", objFilesSettingInfo.CodeLanguage);
                strUpdateParameters = GetMethodParameter("o" + strDOName, "Models." + strDOName, "ByRef", objFilesSettingInfo.CodeLanguage);
                //strPKParameters = GetMethodParameter("o" + strDOName, strDOName, "ByRef", objFilesSettingInfo.CodeLanguage);

                strDAInsertParameters = "obj" + strDOName;
                strDAUpdateParameters = "obj" + strDOName;
                //strDAPKParameters = "obj" + strDOName;

                strSelectReturnType = strDOName;
                strSelectAllReturnType = "ArrayList";
            //}
            #endregion

            #region Update Template
            strTemplateBus = strTemplateBus.Replace("#PKParameters#", strPKParameters);

            strTemplateBus = strTemplateBus.Replace("#InsertParameters#", strInsertParameters);

            strTemplateBus = strTemplateBus.Replace("#UpdateParameters#", strUpdateParameters);

            strTemplateBus = strTemplateBus.Replace("#DAPKParameters#", strDAPKParameters);

            strTemplateBus = strTemplateBus.Replace("#DAInsertParameters#", strDAInsertParameters);

            strTemplateBus = strTemplateBus.Replace("#DAUpdateParameters#", strDAUpdateParameters);

            strTemplateBus = strTemplateBus.Replace("#SelectReturnType#", strSelectReturnType);

            strTemplateBus = strTemplateBus.Replace("#SelectAllReturnType#", strSelectAllReturnType);

            strTemplateBus = strTemplateBus.Replace("#SelectAllParameters#", strSelectAllParameters);

            strTemplateBus = strTemplateBus.Replace("#DASelectAllParameters#", strDASelectAllParameters);

            strTemplateBus = ObtenerGuids(strTemplateBus);

            #endregion
        }
        #endregion

        private string ObtenerGuids(string strTemplate) { 
            int index = 0;
            while (strTemplate.IndexOf("#Guid#", index)>0){
                //reemplazar el primero
                strTemplate = ReplaceFirst(strTemplate, "#Guid#", Guid.NewGuid().ToString());
            }
            return strTemplate;
        }

        string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        public bool CreateDOFile(FilesSettingInfo objFilesSettingInfo)
        {
            if (
               objFilesSettingInfo.TablesInfo != null && objFilesSettingInfo.TablesInfo.Count > 0
               && objFilesSettingInfo.FolderPath != null && objFilesSettingInfo.FolderPath != ""
               )
            {
                foreach (TableInfo objTableInfo in objFilesSettingInfo.TablesInfo)
                {
                    #region Define Variables
                    string strParent = "";
                    string strWCFService = "";
                    string strClassName = "";
                    string strPrivateVariables = "";
                    string strPublicProperties = "";
                    string strUsingNameSpace = "";
                    #endregion

                    #region Fill Variables
                    strUsingNameSpace = objFilesSettingInfo.DOWCFService ? "using System.Runtime.Serialization;" : "";                   
                    strWCFService = objFilesSettingInfo.DOWCFService ? "[DataContract]" : "";                   
                    //strClassName = objFilesSettingInfo.DOBeforeName + objTableInfo.Name + objFilesSettingInfo.DOAfterName;
                    strClassName = objFilesSettingInfo.DOBeforeName + objTableInfo.Name.Substring(objTableInfo.Name.IndexOf("_") + 1) + objFilesSettingInfo.DOAfterName;

                    #region Get Parents
                    if (objFilesSettingInfo.DOParentClass.Length > 0)
                    {
                        if (objFilesSettingInfo.CodeLanguage == CodeLanguage.CS)
                        {
                            if (objFilesSettingInfo.DOParentClass.Length > 0)
                            {
                                strParent = " : " + objFilesSettingInfo.DOParentClass;
                            }
                            if (objFilesSettingInfo.DOParentInterface.Length > 0)
                            {
                                if (strParent.Length > 0) strParent += ",";
                                strParent += objFilesSettingInfo.DOParentInterface;
                            }
                        }
                        else if (objFilesSettingInfo.CodeLanguage == CodeLanguage.VB)
                        {
                            if (objFilesSettingInfo.DOParentClass.Length > 0)
                            {
                                strParent = " Inherits " + objFilesSettingInfo.DOParentClass;
                            }

                            if (objFilesSettingInfo.DOParentInterface.Length > 0)
                            {
                                if (strParent.Length > 0) strParent += "\n";
                                strParent += "\t\t Implements " + objFilesSettingInfo.DOParentInterface;
                            }

                        }
                    }
                    #endregion

                    foreach (ColumnInfo objColumnInfo in objTableInfo.arrColumns)
                    {
                        #region Create Private Variables
                        strPrivateVariables += GetVariable(objColumnInfo, objFilesSettingInfo.CodeLanguage);
                        #endregion

                        #region Create Public Properties
                        if (objFilesSettingInfo.DOWCFService)
                            strPublicProperties += "\t\t[DataMember]\n"; 
                        strPublicProperties += GetProperty(objColumnInfo, objFilesSettingInfo.CodeLanguage);
                        #endregion

                    }
                    #endregion

                    #region Update Template
                    string strTemplateDO = objFilesSettingInfo.DOTemplate;

                    strTemplateDO = strTemplateDO.Replace("#UsingNameSpace#", strUsingNameSpace);
                    strTemplateDO = strTemplateDO.Replace("#NameSpace#", objFilesSettingInfo.DONameSpace);
                    strTemplateDO = strTemplateDO.Replace("#DataContract#", strWCFService);
                    strTemplateDO = strTemplateDO.Replace("#ClassName#", strClassName);
                    strTemplateDO = strTemplateDO.Replace("#ClassParents#", strParent);
                    strTemplateDO = strTemplateDO.Replace("#PrivateVariables#", strPrivateVariables);
                    strTemplateDO = strTemplateDO.Replace("#PublicProperties#", strPublicProperties);
                    #endregion

                    #region Save File
                    //string folderpath = objFilesSettingInfo.FolderPath + "\\" + objTableInfo.DBName + "\\" + objTableInfo.Name;
                    //string filepath = folderpath + "\\" + objFilesSettingInfo.DOBeforeName + objTableInfo.Name + objFilesSettingInfo.DOAfterName + "." + objFilesSettingInfo.CodeLanguage.ToString().ToLower();
                    string folderpath = objFilesSettingInfo.FolderPath + "\\" + objTableInfo.DBName + "\\" + objTableInfo.Name + "\\";
                    string filename = "DO." + (objFilesSettingInfo.DOWCFService ? "WCF" : "" ) + objTableInfo.Name.Substring(objTableInfo.Name.IndexOf("_") + 1);
                    string filepath = folderpath + "\\" + filename + "." + objFilesSettingInfo.CodeLanguage.ToString().ToLower();
                    SaveFileData(filepath, strTemplateDO);
                    #endregion
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool CreateDBFactory(FilesSettingInfo objFilesSettingInfo)
        {
            if (
               objFilesSettingInfo.TablesInfo != null && objFilesSettingInfo.TablesInfo.Count > 0
               && objFilesSettingInfo.FolderPath != null && objFilesSettingInfo.FolderPath != ""
               )
            {
                #region Update Template
                string strTemplateDADBFactory = objFilesSettingInfo.DADBFactoryTemplate;
                string strUsingNameSpaceDBFactory = GetDataNameSpace( objFilesSettingInfo.DAProviderType,objFilesSettingInfo.CodeLanguage);

                strTemplateDADBFactory = strTemplateDADBFactory.Replace("#UsingNameSpace#", strUsingNameSpaceDBFactory);
                strTemplateDADBFactory = strTemplateDADBFactory.Replace("#NameSpace#", objFilesSettingInfo.DANameSpace);
                strTemplateDADBFactory = strTemplateDADBFactory.Replace("#ClassName#", objFilesSettingInfo.DADBFactoryClass);
                strTemplateDADBFactory = strTemplateDADBFactory.Replace("#ProviderType#", objFilesSettingInfo.DAProviderType);
                #endregion

                #region Save File
                string folderpathDBFactory = objFilesSettingInfo.FolderPath + "\\" + ((TableInfo)objFilesSettingInfo.TablesInfo[0]).DBName + "\\";
                string filepathDBFactory = folderpathDBFactory + "\\" + objFilesSettingInfo.DADBFactoryClass + "." + objFilesSettingInfo.CodeLanguage.ToString().ToLower();
                SaveFileData(filepathDBFactory, strTemplateDADBFactory);
                #endregion

                return true;
            }
            else
            {
                return false;
            }


        }

        private bool CreateWSAFile(FilesSettingInfo objFilesSettingInfo)
        {
            if (
               objFilesSettingInfo.TablesInfo != null && objFilesSettingInfo.TablesInfo.Count > 0
               && objFilesSettingInfo.FolderPath != null && objFilesSettingInfo.FolderPath != ""
               )
            {
                #region Declaración
                string strClassName;
                string strTemplateWSA = objFilesSettingInfo.WSATemplate;
                string strUsingNameSpaceWSA = GetDataNameSpace(objFilesSettingInfo.DAProviderType, objFilesSettingInfo.CodeLanguage);
                string strSelectAllParameters = "";
                #endregion

                foreach (TableInfo objTableInfo in objFilesSettingInfo.TablesInfo)
                {
                    #region Variables
                    strClassName = objFilesSettingInfo.DABeforeName + objTableInfo.Name.Substring(objTableInfo.Name.IndexOf("_") + 1) + objFilesSettingInfo.DAAfterName;
                    #endregion

                    #region SelectAll
                    foreach (ColumnInfo objColumnInfo in objTableInfo.arrColumns)
                    {
                        if (!objColumnInfo.IsPrimaryKey)
                        {
                            //SelectAll Method Parameters 
                            strSelectAllParameters += Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage) + " " + objColumnInfo.Name;
                            strSelectAllParameters += ",";

                        }
                    }
                    #endregion

                    #region Update Template
                    /*strTemplateWSA = strTemplateWSA.Replace("#UsingNameSpace#", strUsingNameSpaceWSA);
                    strTemplateWSA = strTemplateWSA.Replace("#NameSpace#", objFilesSettingInfo.DANameSpace);
                    strTemplateWSA = strTemplateWSA.Replace("#ClassName#", objFilesSettingInfo.DADBFactoryClass);
                    strTemplateWSA = strTemplateWSA.Replace("#ProviderType#", objFilesSettingInfo.DAProviderType);
                    */
                    strTemplateWSA = strTemplateWSA.Replace("#ClassName#", strClassName);
                    #endregion

                    #region Save File
                    string folderpathDBFactory = objFilesSettingInfo.FolderPath + "\\" + ((TableInfo)objFilesSettingInfo.TablesInfo[0]).DBName + "\\";
                    string filepathDBFactory = folderpathDBFactory + "\\" + objFilesSettingInfo.DADBFactoryClass + "." + objFilesSettingInfo.CodeLanguage.ToString().ToLower();
                    SaveFileData(filepathDBFactory, strTemplateWSA);
                    #endregion
                }

                return true;
            }
            else
            {
                return false;
            }


        }

        public bool CreateDAFile(FilesSettingInfo objFilesSettingInfo)
        {
            if (
               objFilesSettingInfo.TablesInfo != null && objFilesSettingInfo.TablesInfo.Count > 0
               && objFilesSettingInfo.FolderPath != null && objFilesSettingInfo.FolderPath != ""
               )
            {
                //Create DB Factory 
                #region Create DB Factory (NO LO USAMOS)
                //CreateDBFactory(objFilesSettingInfo);
                #endregion

                #region Create WSAL file (PENDIENTE DEFINIR si se usa CACHÉ o no)
                //CreateWSAFile(objFilesSettingInfo);
                #endregion
                foreach (TableInfo objTableInfo in objFilesSettingInfo.TablesInfo)
                {
                    #region Define Variables
                    string strParent = "";
                    string strClassName = "";
                    string strPrivateVariables = "";
                    string strConstructors = "";
                    string strUsingNameSpace = "";

                    #endregion

                    #region Fill Variables

                    //strUsingNameSpace = GetDataNameSpace(objFilesSettingInfo.DAProviderType,objFilesSettingInfo.CodeLanguage);
                    if (objFilesSettingInfo.UseDataObject) strUsingNameSpace += "\n" +GetNameSpace ( objFilesSettingInfo.DONameSpace  ,objFilesSettingInfo.CodeLanguage);
                    //strClassName = objFilesSettingInfo.DABeforeName + objTableInfo.Name + objFilesSettingInfo.DAAfterName;
                    strClassName = objFilesSettingInfo.DABeforeName + objTableInfo.Name.Substring(objTableInfo.Name.IndexOf("_") + 1) + objFilesSettingInfo.DAAfterName;
              

                    #region Get Parents

                    if (objFilesSettingInfo.CodeLanguage == CodeLanguage.CS)
                    {

                        strParent = " : " + objFilesSettingInfo.DADBFactoryClass;

                        if (objFilesSettingInfo.DAParentInterface.Length > 0)
                        {
                            if (strParent.Length > 0) strParent += ",";
                            strParent += objFilesSettingInfo.DAParentInterface;
                        }
                    }
                    else if (objFilesSettingInfo.CodeLanguage == CodeLanguage.VB)
                    {

                        strParent = " Inherits " + objFilesSettingInfo.DADBFactoryClass;


                        if (objFilesSettingInfo.DAParentInterface.Length > 0)
                        {
                            if (strParent.Length > 0) strParent += "\n";
                            strParent += "\t\t Implements " + objFilesSettingInfo.DAParentInterface;
                        }

                    }

                    #endregion


                    #endregion

                    #region Update Template
                    string strTemplateDA = objFilesSettingInfo.DATemplate;
                    strTemplateDA = strTemplateDA.Replace("#UsingNameSpace#", strUsingNameSpace);
                    strTemplateDA = strTemplateDA.Replace("#NameSpace#", objFilesSettingInfo.DANameSpace);
                    strTemplateDA = strTemplateDA.Replace("#ClassName#", strClassName);
                    strTemplateDA = strTemplateDA.Replace("#ClassParents#", strParent);
                    strTemplateDA = strTemplateDA.Replace("#PrivateVariables#", strPrivateVariables);
                    strTemplateDA = strTemplateDA.Replace("#ProviderType#", objFilesSettingInfo.DAProviderType);
                    strTemplateDA = strTemplateDA.Replace("#Constructor#", strConstructors);

                    SetDAMethods(ref strTemplateDA, objTableInfo, objFilesSettingInfo);
                    #endregion

                    #region Save File
                    //string folderpath = objFilesSettingInfo.FolderPath + "\\" + objTableInfo.DBName + "\\" + objTableInfo.Name;
                    //string filepath = folderpath + "\\" + objFilesSettingInfo.DABeforeName + objTableInfo.Name + objFilesSettingInfo.DAAfterName + "." + objFilesSettingInfo.CodeLanguage.ToString().ToLower();
                    string folderpath = objFilesSettingInfo.FolderPath + "\\" + objTableInfo.DBName + "\\" + objTableInfo.Name + "\\";
                    string filename = "DAL." + objTableInfo.Name.Substring(objTableInfo.Name.IndexOf("_") + 1);
                    string filepath = folderpath + "\\" + filename + "." + objFilesSettingInfo.CodeLanguage.ToString().ToLower();
                    SaveFileData(filepath, strTemplateDA);
                    #endregion
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool CreateBusFile(FilesSettingInfo objFilesSettingInfo)
        {
            if (
               objFilesSettingInfo.TablesInfo != null && objFilesSettingInfo.TablesInfo.Count > 0
               && objFilesSettingInfo.FolderPath != null && objFilesSettingInfo.FolderPath != ""
               )
            {
                foreach (TableInfo objTableInfo in objFilesSettingInfo.TablesInfo)
                {
                    #region Define Variables
                    string strParent = "";
                    string strClassName = "";
                    string strPrivateVariables = "";
                    string strConstructors = "";
                    string strUsingNameSpace = "";
                    string strDAName ="";
                    #endregion

                    #region Fill Variables
                    string DAName=objFilesSettingInfo.DABeforeName+objTableInfo.Name+objFilesSettingInfo.DAAfterName;
                    strPrivateVariables += GetVariable("obj" + DAName, DAName, null, objFilesSettingInfo.CodeLanguage);
                    strConstructors     += GetVariableInitiation("obj" + DAName,"new" + DAName +"()", objFilesSettingInfo.CodeLanguage);
                    strUsingNameSpace   += "\n" + GetNameSpace(objFilesSettingInfo.DANameSpace, objFilesSettingInfo.CodeLanguage);
                    if (objFilesSettingInfo.UseDataObject) strUsingNameSpace += "\n" + GetNameSpace(objFilesSettingInfo.DONameSpace, objFilesSettingInfo.CodeLanguage);
                    //strClassName = objFilesSettingInfo.BusBeforeName + objTableInfo.Name + objFilesSettingInfo.BusAfterName;
                    strClassName = objFilesSettingInfo.BusBeforeName + objTableInfo.Name.Substring(objTableInfo.Name.IndexOf("_") + 1) + objFilesSettingInfo.BusAfterName;
                    strDAName = objFilesSettingInfo.DABeforeName + objTableInfo.Name + objFilesSettingInfo.DAAfterName;

                    #region Get Parents

                    if (objFilesSettingInfo.CodeLanguage == CodeLanguage.CS)
                    {
                        if (objFilesSettingInfo.BusParentClass.Length > 0)
                        {
                            strParent = " : " + objFilesSettingInfo.BusParentClass;
                        }

                        if (objFilesSettingInfo.BusParentInterface.Length > 0)
                        {
                            if (strParent.Length > 0) strParent += ",";
                            strParent += objFilesSettingInfo.BusParentInterface;
                        }
                    }
                    else if (objFilesSettingInfo.CodeLanguage == CodeLanguage.VB)
                    {
                        if (objFilesSettingInfo.BusParentClass.Length > 0)
                        {
                            strParent = " Inherits " + objFilesSettingInfo.BusParentClass;
                        }


                        if (objFilesSettingInfo.BusParentInterface.Length > 0)
                        {
                            if (strParent.Length > 0) strParent += "\n";
                            strParent += "\t\t Implements " + objFilesSettingInfo.BusParentInterface;
                        }

                    }

                    #endregion


                    #endregion

                    #region Update Template
                    string strTemplateBus = objFilesSettingInfo.BusTemplate;
                    strTemplateBus = strTemplateBus.Replace("#UsingNameSpace#", strUsingNameSpace);
                    strTemplateBus = strTemplateBus.Replace("#NameSpace#", objFilesSettingInfo.BusNameSpace);
                    strTemplateBus = strTemplateBus.Replace("#ClassName#", strClassName);
                    strTemplateBus = strTemplateBus.Replace("#ClassParents#", strParent);
                    strTemplateBus = strTemplateBus.Replace("#PrivateVariables#", strPrivateVariables);
                    strTemplateBus = strTemplateBus.Replace("#Constructor#", strConstructors);
                    strTemplateBus = strTemplateBus.Replace("#DAName#", strDAName);
                    SetBusMethods(ref strTemplateBus, objTableInfo, objFilesSettingInfo);
                    #endregion

                    #region Save File
                    //string folderpath = objFilesSettingInfo.FolderPath + "\\" + objTableInfo.DBName + "\\" + objTableInfo.Name;
                    //string filepath = folderpath + "\\" + objFilesSettingInfo.BusBeforeName + objTableInfo.Name + objFilesSettingInfo.BusAfterName + "." + objFilesSettingInfo.CodeLanguage.ToString().ToLower();
                    string folderpath = objFilesSettingInfo.FolderPath + "\\" + objTableInfo.DBName + "\\" + objTableInfo.Name + "\\";
                    string filename = "BLL." + objTableInfo.Name.Substring(objTableInfo.Name.IndexOf("_") + 1);
                    string filepath = folderpath + "\\" + filename + "." + objFilesSettingInfo.CodeLanguage.ToString().ToLower();

                    SaveFileData(filepath, strTemplateBus);
                    #endregion
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

        #region GUI
        public bool CreateWebGUIFile(FilesSettingInfo objFilesSettingInfo)
        {
            CreateWebBaseWebFormAspxFile(objFilesSettingInfo);
            CreateWebBaseWebFormCodeFile(objFilesSettingInfo);

            CreateWebMasterAspxFile(objFilesSettingInfo);
            CreateWebMasterCodeFile(objFilesSettingInfo);

            CreateWebLoginAspxFile(objFilesSettingInfo);
            CreateWebLoginCodeFile(objFilesSettingInfo);

            CreateWebDetailsAspxFile(objFilesSettingInfo);
            CreateWebDetailsCodeFile(objFilesSettingInfo);

            CreateWebSearchAspxFile(objFilesSettingInfo);
            CreateWebSearchCodeFile(objFilesSettingInfo);

            string folderpathSource = Application.StartupPath + @"\VarietyClasses";
            string folderpathTarget = objFilesSettingInfo.FolderPath + "\\" + ((TableInfo)objFilesSettingInfo.TablesInfo[0]).DBName + "\\" + "VarietyClasses";

                  Copy(folderpathSource, folderpathTarget);

            return true;

        }
        #region  Base Web Form
        private bool CreateWebBaseWebFormAspxFile(FilesSettingInfo objFilesSettingInfo)
        {
            if (
                   objFilesSettingInfo.TablesInfo != null && objFilesSettingInfo.TablesInfo.Count > 0
                   && objFilesSettingInfo.FolderPath != null && objFilesSettingInfo.FolderPath != ""
                   )
            {

                #region Update Template
                string strTemplate = objFilesSettingInfo.WebBaseWebFormTemplateHtml;


                if (objFilesSettingInfo.CodeLanguage == CodeLanguage.CS)
                {
                    strTemplate = strTemplate.Replace("#Language#", "C#");
                }
                else
                {
                    strTemplate = strTemplate.Replace("#Language#", objFilesSettingInfo.CodeLanguage.ToString());
                }
                strTemplate = strTemplate.Replace("#Ext#", objFilesSettingInfo.CodeLanguage.ToString().ToLower());


                #endregion

                #region Save File
                string folderpath = objFilesSettingInfo.FolderPath + "\\"+ ((TableInfo)objFilesSettingInfo.TablesInfo[0]).DBName + "\\"; ;
                string filepath = folderpath + "\\" + "BaseWebForm" + ".aspx";
                SaveFileData(filepath, strTemplate);
                #endregion

                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CreateWebBaseWebFormCodeFile(FilesSettingInfo objFilesSettingInfo)
        {
            if (
                  objFilesSettingInfo.TablesInfo != null && objFilesSettingInfo.TablesInfo.Count > 0
                  && objFilesSettingInfo.FolderPath != null && objFilesSettingInfo.FolderPath != ""
                  )
            {

                #region Update Template
                string strTemplate = objFilesSettingInfo.WebBaseWebFormTemplate;
                #endregion

                #region Save File
                string folderpath = objFilesSettingInfo.FolderPath+ "\\" + ((TableInfo)objFilesSettingInfo.TablesInfo[0]).DBName + "\\"; ;
                string filepath = folderpath + "\\" + "BaseWebForm" + ".aspx." + objFilesSettingInfo.CodeLanguage.ToString().ToLower();
                SaveFileData(filepath, strTemplate);
                #endregion

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Master Page
        private bool CreateWebMasterAspxFile(FilesSettingInfo objFilesSettingInfo)
             {
                 if (
                   objFilesSettingInfo.TablesInfo != null && objFilesSettingInfo.TablesInfo.Count > 0
                   && objFilesSettingInfo.FolderPath != null && objFilesSettingInfo.FolderPath != ""
                   )
                 {

                         #region Update Template
                         string strTemplate = objFilesSettingInfo.WebMasterTemplateHtml;
                         if (objFilesSettingInfo.CodeLanguage == CodeLanguage.CS)
                         {
                             strTemplate = strTemplate.Replace("#Language#", "C#");
                         }
                         else
                         {
                             strTemplate = strTemplate.Replace("#Language#", objFilesSettingInfo.CodeLanguage.ToString());
                         }
                         strTemplate = strTemplate.Replace("#MasterPageName#", objFilesSettingInfo.WebMasterName); ;
                         strTemplate = strTemplate.Replace("#Ext#", objFilesSettingInfo.CodeLanguage.ToString().ToLower());
                         strTemplate = strTemplate.Replace("#Title#", objFilesSettingInfo.WebMasterTitle);
                         
                         #endregion

                         #region Save File
                         string folderpath = objFilesSettingInfo.FolderPath + "\\" + ((TableInfo)objFilesSettingInfo.TablesInfo[0]).DBName + "\\"; ;
                         string filepath = folderpath + "\\" + objFilesSettingInfo.WebMasterName + ".master";
                         SaveFileData(filepath, strTemplate);
                         #endregion
                    
                     return true;
                 }
                 else
                 {
                     return false;
                 }
        }
        private bool CreateWebMasterCodeFile(FilesSettingInfo objFilesSettingInfo)
        {
            if (
                   objFilesSettingInfo.TablesInfo != null && objFilesSettingInfo.TablesInfo.Count > 0
                   && objFilesSettingInfo.FolderPath != null && objFilesSettingInfo.FolderPath != ""
                   )
            {

                #region Update Template
                string strTemplate = objFilesSettingInfo.WebMasterTemplate;
                strTemplate = strTemplate.Replace("#MasterPageName#", objFilesSettingInfo.WebMasterName );;
                strTemplate = strTemplate.Replace("#HomePage#", objFilesSettingInfo.WebHomePageName);
                strTemplate = strTemplate.Replace("#UserName#", objFilesSettingInfo.WebLoginSessionName);
                #endregion

                #region Save File
                string folderpath = objFilesSettingInfo.FolderPath + "\\" + ((TableInfo)objFilesSettingInfo.TablesInfo[0]).DBName + "\\"; ;
                string filepath = folderpath + "\\" + objFilesSettingInfo.WebMasterName + ".master." + objFilesSettingInfo.CodeLanguage.ToString().ToLower();
                SaveFileData(filepath, strTemplate);
                #endregion

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Login 
        private bool CreateWebLoginAspxFile(FilesSettingInfo objFilesSettingInfo)
        {
            if (
                   objFilesSettingInfo.TablesInfo != null && objFilesSettingInfo.TablesInfo.Count > 0
                   && objFilesSettingInfo.FolderPath != null && objFilesSettingInfo.FolderPath != ""
                   )
            {

                #region Update Template
                string strTemplate = objFilesSettingInfo.WebLoginTemplateHtml;
        

                if (objFilesSettingInfo.CodeLanguage == CodeLanguage.CS)
                         {
                             strTemplate = strTemplate.Replace("#Langauge#", "C#");
                         }
                         else
                         {
                             strTemplate = strTemplate.Replace("#Langauge#", objFilesSettingInfo.CodeLanguage.ToString());
                         }
                         strTemplate = strTemplate.Replace("#Ext#", objFilesSettingInfo.CodeLanguage.ToString().ToLower());

                                     
                #endregion

                #region Save File
                         string folderpath = objFilesSettingInfo.FolderPath + "\\" + ((TableInfo)objFilesSettingInfo.TablesInfo[0]).DBName + "\\"; ;
                string filepath = folderpath + "\\" + "Login" + ".aspx";
                SaveFileData(filepath, strTemplate);
                #endregion

                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CreateWebLoginCodeFile(FilesSettingInfo objFilesSettingInfo)
        {
            if (
                  objFilesSettingInfo.TablesInfo != null && objFilesSettingInfo.TablesInfo.Count > 0
                  && objFilesSettingInfo.FolderPath != null && objFilesSettingInfo.FolderPath != ""
                  )
            {

                #region Update Template
                string strTemplate =  objFilesSettingInfo.WebLoginTemplate;
                #endregion

                #region Save File
                string folderpath = objFilesSettingInfo.FolderPath + "\\" + ((TableInfo)objFilesSettingInfo.TablesInfo[0]).DBName + "\\"; ;
                string filepath = folderpath + "\\" + "Login" + ".aspx." + objFilesSettingInfo.CodeLanguage.ToString().ToLower();
                SaveFileData(filepath, strTemplate);
                #endregion

                return true;
            }
            else
            {
                return false;
            }
        }     
        #endregion

        #region Search
        private bool CreateWebSearchAspxFile(FilesSettingInfo objFilesSettingInfo)
        {
            if (
               objFilesSettingInfo.TablesInfo != null && objFilesSettingInfo.TablesInfo.Count > 0
               && objFilesSettingInfo.FolderPath != null && objFilesSettingInfo.FolderPath != ""
               )
            {
                foreach (TableInfo objTableInfo in objFilesSettingInfo.TablesInfo)
                {
                    #region Update Template
                    string strTemplate = objFilesSettingInfo.WebSearchTemplateHtml;

                    if (objFilesSettingInfo.CodeLanguage == CodeLanguage.CS)
                    {
                        strTemplate = strTemplate.Replace("#Language#", "C#");
                    }
                    else
                    {
                        strTemplate = strTemplate.Replace("#Language#", objFilesSettingInfo.CodeLanguage.ToString());
                    }

                    strTemplate = strTemplate.Replace("#MasterPageName#", objFilesSettingInfo.WebMasterName);
                    strTemplate = strTemplate.Replace("#TableName#", objTableInfo.Name);
                    strTemplate = strTemplate.Replace("#Ext#", objFilesSettingInfo.CodeLanguage.ToString().ToLower());

                    #endregion

                    #region Save File
                    string folderpath = objFilesSettingInfo.FolderPath + "\\" + objTableInfo.DBName + "\\" + objTableInfo.Name;
                    string filepath = folderpath + "\\" + objFilesSettingInfo.WebSearchBeforeName + objTableInfo.Name + objFilesSettingInfo.WebSearchAfterName + ".aspx" ;
                    SaveFileData(filepath, strTemplate);
                    #endregion
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CreateWebSearchCodeFile(FilesSettingInfo objFilesSettingInfo)
        {

            if (
              objFilesSettingInfo.TablesInfo != null && objFilesSettingInfo.TablesInfo.Count > 0
              && objFilesSettingInfo.FolderPath != null && objFilesSettingInfo.FolderPath != ""
              )
            {
                foreach (TableInfo objTableInfo in objFilesSettingInfo.TablesInfo)
                {
                    #region Define Variables
                    string strUsingNameSpace = "";
                    string strBusName = "";
                    #endregion

                    #region Fill Variables
                    strUsingNameSpace += "\n" + GetNameSpace(objFilesSettingInfo.BusNameSpace, objFilesSettingInfo.CodeLanguage);
                    strUsingNameSpace += "\n" + GetNameSpace(objFilesSettingInfo.DONameSpace, objFilesSettingInfo.CodeLanguage);
                    //if (objFilesSettingInfo.UseDataObject) strUsingNameSpace += "\n" + GetNameSpace(objFilesSettingInfo.DONameSpace, objFilesSettingInfo.CodeLanguage);
                    strBusName = objFilesSettingInfo.BusBeforeName + objTableInfo.Name + objFilesSettingInfo.BusAfterName;
                    #endregion

                    #region Update Template
                    string strTemplate = objFilesSettingInfo.WebSearchTemplate;
                    strTemplate = strTemplate.Replace("#UsingNameSpace#", strUsingNameSpace);
                    strTemplate = strTemplate.Replace("#TableName#", objTableInfo.Name);
                    strTemplate = strTemplate.Replace("#BusClass#", strBusName);
                    #endregion

                    #region Save File
                    string folderpath = objFilesSettingInfo.FolderPath + "\\" + objTableInfo.DBName + "\\" + objTableInfo.Name;
                    string filepath = folderpath + "\\" + objFilesSettingInfo.WebSearchBeforeName + objTableInfo.Name + objFilesSettingInfo.WebSearchAfterName + ".aspx." + objFilesSettingInfo.CodeLanguage.ToString().ToLower();
                    SaveFileData(filepath, strTemplate);
                    #endregion
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Details
        private bool CreateWebDetailsAspxFile(FilesSettingInfo objFilesSettingInfo)
        {
            if (
               objFilesSettingInfo.TablesInfo != null && objFilesSettingInfo.TablesInfo.Count > 0
               && objFilesSettingInfo.FolderPath != null && objFilesSettingInfo.FolderPath != ""
               )
            {
                foreach (TableInfo objTableInfo in objFilesSettingInfo.TablesInfo)
                {
                    string strContent = "<table align=\"center\" width =\"100%\" style=\"text-align:center\" >";

                    #region Update Template
                    string strTemplate = objFilesSettingInfo.WebDetailsTemplateHtml;

                    if (objFilesSettingInfo.CodeLanguage == CodeLanguage.CS)
                    {
                        strTemplate = strTemplate.Replace("#Language#", "C#");
                    }
                    else
                    {
                        strTemplate = strTemplate.Replace("#Language#", objFilesSettingInfo.CodeLanguage.ToString());
                    }

                    strTemplate = strTemplate.Replace("#MasterPageName#", objFilesSettingInfo.WebMasterName);
                    strTemplate = strTemplate.Replace("#TableName#", objTableInfo.Name);
                    strTemplate = strTemplate.Replace("#Ext#", objFilesSettingInfo.CodeLanguage.ToString().ToLower());

                    foreach (ColumnInfo objColumnInfo in objTableInfo.arrColumns)
                    {

                        strContent += "\n";
                        strContent += "<tr>";

                        strContent += "<td>";
                        strContent += "\n";
                        strContent += "<asp:Label ID=\"lbl" + objColumnInfo.Name+ "\" runat=\"server\" Text=\"" +Utilities.GetFrienlyName(objColumnInfo.Name) + "\"></asp:Label>";
                        strContent += "\n";
                        strContent += "</td>";
                        strContent += "\n";

                        strContent += "<td>";
                        strContent += "\n";
                        string strTxtLst = "";
                        if (objColumnInfo.IsForeignKey)
                        {
                            strContent += "<asp:DropDownList ID=\"lst" + objColumnInfo.Name + "\" runat=\"server\"></asp:DropDownList>";
                            strTxtLst = "lst";
                        }
                        else if (objColumnInfo.DBType=="bit")
                        {
                            strContent += "<asp:CheckBox ID=\"chk" + objColumnInfo.Name + "\" runat=\"server\" Text=\"" + Utilities.GetFrienlyName(objColumnInfo.Name) + "\" Checked=\"False\" />";
                        strTxtLst = "";
                        }
                        else
                        {
                            string maxlen = "";
                            if (objColumnInfo.Length > 0)
                                maxlen = " MaxLength=\"" + objColumnInfo.Length + "\" " ;

                            strContent += "<asp:TextBox ID=\"txt" + objColumnInfo.Name + "\" runat=\"server\"" + maxlen + "> </asp:TextBox>";
                            strTxtLst = "txt"; 
                        }
                        strContent += "\n";
                        strContent += "</td>";
                        strContent += "\n";

                        strContent += "<td>";
                        strContent += "\n";

                        if (!objColumnInfo.IsAllowNull)
                        {
                            if (strTxtLst.Length>0)
                            strContent += "<asp:RequiredFieldValidator ID=\"rfv" + objColumnInfo.Name + "\" runat=\"server\" ControlToValidate=\"" + strTxtLst + objColumnInfo.Name + "\" Display=\"Dynamic\" ErrorMessage=\"*\"></asp:RequiredFieldValidator>";
                        }
                        strContent += "\n";
                        strContent += "</td>";
                        strContent += "</tr>";
                        strContent += "\n";

                    }
                    strContent += "</table>";

                    strTemplate = strTemplate.Replace("#Content#", strContent);

                    #endregion

                    #region Save File
                    string folderpath = objFilesSettingInfo.FolderPath + "\\" + objTableInfo.DBName + "\\" + objTableInfo.Name;
                    string filepath = folderpath + "\\" + objFilesSettingInfo.WebDetailsBeforeName + objTableInfo.Name + objFilesSettingInfo.WebDetailsAfterName + ".aspx";
                    SaveFileData(filepath, strTemplate);
                    #endregion
                }
                return true;
            }
            else
            {
                return false;
            }
        }

       
        private bool CreateWebDetailsCodeFile(FilesSettingInfo objFilesSettingInfo)
        {
            if (
             objFilesSettingInfo.TablesInfo != null && objFilesSettingInfo.TablesInfo.Count > 0
             && objFilesSettingInfo.FolderPath != null && objFilesSettingInfo.FolderPath != ""
             )
            {
                foreach (TableInfo objTableInfo in objFilesSettingInfo.TablesInfo)
                {
                    #region Define Variables
                    string strUsingNameSpace = "";
                    string strBusName = "";
                    string strDOName = "";
                    string strGetControlsValues = "";
                    string strSetControlsValues = "";
                    #endregion

                    #region Fill Variables
                    strUsingNameSpace += "\n" + GetNameSpace(objFilesSettingInfo.BusNameSpace, objFilesSettingInfo.CodeLanguage);
                    strUsingNameSpace += "\n" + GetNameSpace(objFilesSettingInfo.DONameSpace, objFilesSettingInfo.CodeLanguage);
                   // if (objFilesSettingInfo.UseDataObject) strUsingNameSpace += "\n" + GetNameSpace(objFilesSettingInfo.DONameSpace, objFilesSettingInfo.CodeLanguage);

                    strDOName = objFilesSettingInfo.DOBeforeName   + objTableInfo.Name + objFilesSettingInfo.DOAfterName;
                    strBusName = objFilesSettingInfo.BusBeforeName + objTableInfo.Name + objFilesSettingInfo.BusAfterName;



                    foreach (ColumnInfo objColumnInfo in objTableInfo.arrColumns)
                    {
                        string systemType = Utilities.GetSystemType(objColumnInfo.DBType, objFilesSettingInfo.CodeLanguage);

                        strGetControlsValues += "obj" + strDOName + "." + objColumnInfo.Name;
                        strGetControlsValues += "=";
                        strGetControlsValues += "Convert.To" + systemType + "(";
                        if (objColumnInfo.IsForeignKey)
                        {
                           
                            strGetControlsValues += "lst" + objColumnInfo.Name + ".SelectedValue";
                            strSetControlsValues += "lst" + objColumnInfo.Name + ".SelectedValue";

                        }

                        else if (objColumnInfo.DBType == "bit")
                        {
                            strGetControlsValues += "chk" + objColumnInfo.Name + ".Checked";
                            strSetControlsValues += "chk" + objColumnInfo.Name + ".Checked";
                        }
                        else
                        {
                            strGetControlsValues += "txt" + objColumnInfo.Name + ".Text";
                            strSetControlsValues += "txt" + objColumnInfo.Name + ".Text";
                        }
                        strGetControlsValues += ");";


                        strSetControlsValues += "=";
                        if (objColumnInfo.DBType == "bit")
                        {
                            strSetControlsValues += "Convert.ToBoolean" + "(";
                        }
                        else
                        {
                            strSetControlsValues += "Convert.ToString" + "(";
                        }
                        strSetControlsValues += "obj" + strDOName + "." + objColumnInfo.Name;
                        strSetControlsValues += ");";

                        strGetControlsValues += "\n";
                        strSetControlsValues += "\n";

                        
                       
                    }
                    #endregion

                    #region Update Template
                    string strTemplate = objFilesSettingInfo.WebDetailsTemplate;
                    strTemplate = strTemplate.Replace("#UsingNameSpace#", strUsingNameSpace);
                    strTemplate = strTemplate.Replace("#TableName#", objTableInfo.Name);
                    strTemplate = strTemplate.Replace("#BusClass#", strBusName);
                    strTemplate = strTemplate.Replace("#DOClass#", strDOName);
                    strTemplate = strTemplate.Replace("#GetControlsValues#", strGetControlsValues);
                    strTemplate = strTemplate.Replace("#SetControlsValues#", strSetControlsValues);

                    
                    #endregion

                    #region Save File
                    string folderpath = objFilesSettingInfo.FolderPath + "\\" + objTableInfo.DBName + "\\" + objTableInfo.Name;
                    string filepath = folderpath + "\\" + objFilesSettingInfo.WebDetailsBeforeName + objTableInfo.Name + objFilesSettingInfo.WebDetailsAfterName + ".aspx." + objFilesSettingInfo.CodeLanguage.ToString().ToLower();
                    SaveFileData(filepath, strTemplate);
                    #endregion
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
        #endregion

        #region SQL Stored Procedure
        public bool CreateSPFile(FilesSettingInfo objFilesSettingInfo)
        {
            if (
                objFilesSettingInfo.TablesInfo != null && objFilesSettingInfo.TablesInfo.Count > 0
                && objFilesSettingInfo.FolderPath != null && objFilesSettingInfo.FolderPath != ""
                )
            {
                foreach (TableInfo objTableInfo in objFilesSettingInfo.TablesInfo)
                {
                    #region Define Variables
                    string strSelectAllParameters = "";
                    string strSelectAllWhere = "";

                    string strSelectParameters = "";
                    string strSelectFields = "";
                    string strSelectWhere = "";

                    string strInsertParameters = "";
                    string strInsertFields = "";
                    string strInsertValues = "";

                    string strUpdateParameters = "";
                    string strUpdateStatement = "";
                    string strUpdateWhere = "";

                    string strDeleteParameters = "";
                    string strDeleteWhere = "";
                    #endregion

                    #region Fill Variables
                    foreach (ColumnInfo objColumnInfo in objTableInfo.arrColumns)
                    {
                        #region Select
                        // @UserName nvarchar(20)=null,
                        string strNull = (objColumnInfo.IsAllowNull) ? "=null" : "";
                        //bit,text,image
                        string strLength = (objColumnInfo.Length > 0 && objColumnInfo.Length < 1073741823 && objColumnInfo.Length < 2147483647) ? "(" + objColumnInfo.Length + ")" : "";
                        strSelectFields += "[" + objColumnInfo.Name + "]" + ",\n";
                        if (objColumnInfo.IsPrimaryKey)
                        {
                            strSelectParameters += "@" + objColumnInfo.Name + " " + objColumnInfo.DBType + strLength + strNull + ",\n";
                            strSelectWhere += "[" + objColumnInfo.Name + "]" + "=@" + objColumnInfo.Name + "\n and ";
                        }
                        else {
                            strSelectAllParameters += "@" + objColumnInfo.Name + " " + objColumnInfo.DBType + strLength + strNull + ",\n"; ;
                            strSelectAllWhere += "[" + objColumnInfo.Name + "]" + "=@" + objColumnInfo.Name + "\n and ";
                        }
                        #endregion

                        #region Insert
                        if (!objColumnInfo.IsIdentity)
                        {
                            strInsertParameters += "@" + objColumnInfo.Name + " " + objColumnInfo.DBType + strLength + strNull + ",\n"; ;
                            strInsertFields += "[" + objColumnInfo.Name + "]" + ",\n";
                            strInsertValues += "@" + objColumnInfo.Name + ",\n";
                        }
                        #endregion

                        #region Update

                        if (!objColumnInfo.IsIdentity)
                        {
                            strUpdateStatement += "[" + objColumnInfo.Name + "]" + "=@" + objColumnInfo.Name + ",\n";
                        }
                        strUpdateParameters += "@" + objColumnInfo.Name + " " + objColumnInfo.DBType + strLength + strNull + ",\n"; ;

                        strUpdateWhere = strSelectWhere;
                        #endregion

                        #region Delete
                        strDeleteParameters = strSelectParameters;
                        strDeleteWhere = strSelectWhere;
                        #endregion

                    }


                    #endregion
                    
                    #region Update Template
                    string strTemplateSP = objFilesSettingInfo.SPTemplate;
                    strTemplateSP = strTemplateSP.Replace("#TableName#", objTableInfo.Name);
                    strTemplateSP = strTemplateSP.Replace("#Owner#", objFilesSettingInfo.SPOwner);
                    string spName = objFilesSettingInfo.AppName + "_" + (objTableInfo.Name.Substring(objTableInfo.Name.IndexOf("_") + 1)); //En código original es --> objTableInfo.Name

                    strTemplateSP = strTemplateSP.Replace("#SelectByPK#", objFilesSettingInfo.SPBeforeSelectByPK + spName + objFilesSettingInfo.SPAfterSelectByPK);
                    strTemplateSP = strTemplateSP.Replace("#SelectAll#", objFilesSettingInfo.SPBeforeSelectAll + spName + objFilesSettingInfo.SPAfterSelectAll);
                    strTemplateSP = strTemplateSP.Replace("#Insert#", objFilesSettingInfo.SPBeforeInsert + spName + objFilesSettingInfo.SPAfterInsert);
                    strTemplateSP = strTemplateSP.Replace("#UpdateByPK#", objFilesSettingInfo.SPBeforeUpdateByPK + spName + objFilesSettingInfo.SPAfterUpdateByPK);
                    strTemplateSP = strTemplateSP.Replace("#DeleteByPK#", objFilesSettingInfo.SPBeforeDeleteByPK + spName + objFilesSettingInfo.SPAfterDeleteByPK);
                    strTemplateSP = strTemplateSP.Replace("#DeleteAll#", objFilesSettingInfo.SPBeforeDeleteAll + spName + objFilesSettingInfo.SPAfterDeleteAll);

                    if (strSelectAllParameters.LastIndexOf(',') > -1) strSelectAllParameters = (strSelectAllParameters.Remove(strSelectAllParameters.LastIndexOf(',')) + "\n");
                    strTemplateSP = strTemplateSP.Replace("#SelectAll_Parameters#", strSelectAllParameters);

                    if (strSelectAllWhere.LastIndexOf(" and ") > -1) strSelectAllWhere = (strSelectAllWhere.Remove(strSelectAllWhere.LastIndexOf(" and ")) + "\n");
                    strTemplateSP = strTemplateSP.Replace("#SelectAll_Where#", strSelectAllWhere);


                    if (strSelectParameters.LastIndexOf(',') > -1) strSelectParameters = (strSelectParameters.Remove(strSelectParameters.LastIndexOf(',')) + "\n");
                    strTemplateSP = strTemplateSP.Replace("#Select_Parameters#", strSelectParameters);

                    if (strSelectFields.LastIndexOf(',') > -1) strSelectFields = (strSelectFields.Remove(strSelectFields.LastIndexOf(',')) + "\n");
                    strTemplateSP = strTemplateSP.Replace("#Select_Fields#", strSelectFields);

                    if (strSelectWhere.LastIndexOf(" and ") > -1) strSelectWhere = (strSelectWhere.Remove(strSelectWhere.LastIndexOf(" and ")) + "\n");
                    strTemplateSP = strTemplateSP.Replace("#Select_Where#", strSelectWhere);

                    if (strInsertParameters.LastIndexOf(',') > -1) strInsertParameters = (strInsertParameters.Remove(strInsertParameters.LastIndexOf(',')) + "\n");
                    strTemplateSP = strTemplateSP.Replace("#Insert_Parameters#", strInsertParameters);

                    if (strInsertFields.LastIndexOf(',') > -1) strInsertFields = (strInsertFields.Remove(strInsertFields.LastIndexOf(',')) + "\n");
                    strTemplateSP = strTemplateSP.Replace("#Insert_Fields#", strInsertFields);

                    if (strInsertValues.LastIndexOf(',') > -1) strInsertValues = (strInsertValues.Remove(strInsertValues.LastIndexOf(',')) + "\n");
                    strTemplateSP = strTemplateSP.Replace("#Insert_Values#", strInsertValues);

                    if (strUpdateParameters.LastIndexOf(',') > -1) strUpdateParameters = (strUpdateParameters.Remove(strUpdateParameters.LastIndexOf(',')) + "\n");
                    strTemplateSP = strTemplateSP.Replace("#Update_Parameters#", strUpdateParameters);

                    if (strUpdateStatement.LastIndexOf(',') > -1) strUpdateStatement = (strUpdateStatement.Remove(strUpdateStatement.LastIndexOf(',')) + "\n");
                    strTemplateSP = strTemplateSP.Replace("#Update_Statement#", strUpdateStatement);

                    if (strUpdateWhere.LastIndexOf(" and ") > -1) strUpdateWhere = (strUpdateWhere.Remove(strUpdateWhere.LastIndexOf(" and ")) + "\n");
                    strTemplateSP = strTemplateSP.Replace("#Update_Where#", strUpdateWhere);

                    if (strDeleteParameters.LastIndexOf(',') > -1) strDeleteParameters = (strDeleteParameters.Remove(strDeleteParameters.LastIndexOf(',')) + "\n");
                    strTemplateSP = strTemplateSP.Replace("#Delete_Parameters#", strDeleteParameters);

                    if (strDeleteWhere.LastIndexOf(" and ") > -1) strDeleteWhere = (strDeleteWhere.Remove(strDeleteWhere.LastIndexOf(" and ")) + "\n");
                    strTemplateSP = strTemplateSP.Replace("#Delete_Where#", strDeleteWhere);

                    #endregion

                    #region Save File
                    string folderpath = objFilesSettingInfo.FolderPath + "\\" + objTableInfo.DBName + "\\" + objTableInfo.Name;
                    string filepath = folderpath + "\\" + objTableInfo.Name + "." + objFilesSettingInfo.DBLanguage.ToString().ToLower();
                    SaveFileData(filepath, strTemplateSP);
                    #endregion

                }
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region IO Get / Save File Data
        public static string GetFileData(string filepath)
        {
            StreamReader sr = new StreamReader(filepath);
            string Template = sr.ReadToEnd();
            sr.Close();
            return Template;
        }
        public static bool SaveFileData(string strFilePath, string strFileData)
        {
            FileInfo fileinfo = new FileInfo(strFilePath);
            if (!fileinfo.Directory.Exists)
            {
                fileinfo.Directory.Create();
            }
            StreamWriter sw = new StreamWriter(strFilePath, false);
            sw.AutoFlush = true;
            sw.Write(strFileData);
            sw.Close();
            return true;
        }
        #endregion

        #endregion
    }
}