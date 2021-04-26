using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data.SqlClient;
//to add Microsoft Data Access Service Components Module  (Msdasc.dll in v2.0  and  Oledb32.dll in V2.1)
//set two references in your project (under COM):
//"Microsoft OLE DB Service Component 1.0 Type Library" 
//"Microsoft ActiveX Data Objects 2.8 Library"
using MSDASC;
using ADODB;
using System.Xml;
using System.Xml.Schema;
using System.IO;
namespace CodeGenerator2005
{
    public enum RelationType
    {
        Child = 1,
        Parent = 2,
        Both = 3
    }


    public class DBManager
    {
        #region Private Variables
        private string _ConnectionString = "";
      
        #endregion

        #region Public Properties
        public string ConnectionString
        {
            get
            {
                return _ConnectionString;

            }
            set
            {
                _ConnectionString = value;
            }
        }
        #endregion

        #region Constructors
        public DBManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DBManager(string strConnection)
        {
            //
            // TODO: Add constructor logic here
            //
            _ConnectionString = strConnection;

        }
        #endregion
        
        #region Methods
        
        //TablesSchema fields : TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE, TABLE_GUID, DESCRIPTION, TABLE_PROPID, DATE_CREATED, DATE_MODIFIED 
        //ColumnsSchema fields : TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME, COLUMN_GUID, COLUMN_PROPID, ORDINAL_POSITION, COLUMN_HASDEFAULT, COLUMN_DEFAULT, COLUMN_FLAGS, IS_NULLABLE, DATA_TYPE, TYPE_GUID, CHARACTER_MAXIMUM_LENGTH, CHARACTER_OCTET_LENGTH, NUMERIC_PRECISION, NUMERIC_SCALE, DATETIME_PRECISION, CHARACTER_SET_CATALOG, CHARACTER_SET_SCHEMA, CHARACTER_SET_NAME, COLLATION_CATALOG, COLLATION_SCHEMA, COLLATION_NAME, DOMAIN_CATALOG, DOMAIN_SCHEMA, DOMAIN_NAME, DESCRIPTION, SS_DATA_TYPE
        #region private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strQuery"></param>
        /// <returns></returns>
        private DataTable GetDataTable(string strQuery)
        {
            OleDbCommand oo = null;
            OleDbConnection con = new OleDbConnection(_ConnectionString);
            OleDbDataAdapter adp = new OleDbDataAdapter(strQuery, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            return dt;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objOleDbSchemaGuid"></param>
        /// <param name="arrbject">
        /// the array of restrictions is as follows: {TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE}
        /// ex: {"Pubs", "dbo", "Employee","TABLE"})
        /// </param>
        /// <returns></returns>
        private DataTable GetDataTable(Guid objOleDbSchemaGuid, object[] arrbject)
        {
            #region Help
            // • Columns 
            //• Foreign keys 
            //• Indexes 
            //• Primary keys 
            //• Tables 
            //• Views 
            //Query for getting the list of all tables from the schema, no restriction
            //dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            //Query for getting only the user tables
            // dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

            //Query for getting stored prodedures
            // dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Procedures, null);

            //Query for getting all primary keys
            //dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, null);

            //Query for getting all supported types
            //dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Provider_Types, null);

            //Query for getting Table Column
            //dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tableName });
            #endregion
            DataTable dt = null;
            //Open a connection object
            OleDbConnection con = new OleDbConnection(_ConnectionString);
            con.Open();
            //Declare a Datatable object
            dt = con.GetOleDbSchemaTable(objOleDbSchemaGuid, arrbject);
            con.Close();
            return dt;
        }


        /// <summary>
        /// Create New Connection Using Data Links 
        /// </summary>
        /// <returns>Connection String</returns>
        private string CreateNewConnection()
        {
            DataLinksClass DLC;
            Connection conn;
            try
            {
                DLC = new DataLinksClass();
                conn = (Connection)DLC.PromptNew();
                if (conn == null)
                {
                    _ConnectionString = "";
                }
                else
                {
                    _ConnectionString = conn.ConnectionString;

                }
                return _ConnectionString;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                DLC = null;
                conn = null;
            }
        }
        /// <summary>
        /// Edit Existing Connection Using Data Links
        /// </summary>
        /// <param name="connStr">Connection string to edit</param>
        /// <returns>if success return true else return false</returns>
        private bool EditExistingConnection(ref string connStr)
        {
            //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Test.mdb;Persist Security Info=False"
            DataLinksClass DLC;
            Connection conn;
            try
            {
                if (connStr != null && connStr != "")
                {
                    DLC = new DataLinksClass();
                    conn = new Connection();
                    conn.ConnectionString = connStr;
                    object objConn = (object)conn;
                    if (DLC.PromptEdit(ref objConn))
                    {
                        connStr = conn.ConnectionString;
                        _ConnectionString = connStr;
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

                throw ex;
            }
            finally
            {
                DLC = null;
                conn = null;
            }

        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable GetAllTablesByQuery()
        {
            //Get Only table_Catalog= 'Northwind',table_name= 'Categories',table_type ='BASE TABLE||VIEW',table_schema ='dbo;
            // string strQuery = "select table_Catalog,table_name,table_schema,table_type from  INFORMATION_SCHEMA.Tables where TABLE_TYPE ='BASE TABLE'";
            // Get All Tables in a database
            string strQuery = "select id as TABLE_ID, name as TABLE_NAME, 0 as IS_CHILD, '' as CHILD_TABLES from sysobjects where xtype = 'U' order by name";
            return GetDataTable(strQuery);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="DBName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private DataTable GetAllColumnsByQuery(string tableName)
        {
            #region By Table ID
            string strQueryByID = "";
            strQueryByID = @"select 
								t2.id TABLE_ID, 
								t1.TABLE_NAME, 
								t1.COLUMN_NAME, 
								t1.DATA_TYPE, 
								t1.CHARACTER_MAXIMUM_LENGTH, 
								t1.COLUMN_DEFAULT, 
								t1.IS_NULLABLE, 
								0 as IS_CHILD_TABLE, 
								(select 1 
								from INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
								where (TABLE_NAME = t1.TABLE_NAME) and (COLUMN_NAME = t1.COLUMN_NAME) and (constraint_name LIKE 'PK_%')) as Is_PK,
								'' as PARENT_COL_NAME,
								'' as CHILD_COL_NAME
						from 
								INFORMATION_SCHEMA.COLUMNS t1 join sysobjects t2 on t1.TABLE_NAME = t2.NAME
						where 
								Table_Catalog = '{0}' and id = {1}";
            #endregion

            #region  help
            string sSQL = @"SELECT INFORMATION_SCHEMA.COLUMNS.*, 
(SELECT COLUMNPROPERTY (OBJECT_ID (@sTableName), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsComputed')) AS IsComputed,
(SELECT COL_LENGTH(@sTableName, INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME )) AS ColumnLength,
(SELECT COLUMNPROPERTY (OBJECT_ID(@sTableName), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsIdentity')) AS IsIdentity,
(SELECT COLUMNPROPERTY(OBJECT_ID(@sTableName), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsRowGuidCol')) AS IsRowGuidColumn,
(ISNULL(
	(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
		WHERE TABLE_NAME=@sTableName
	 	AND TABLE_SCHEMA='dbo' 
		AND COLUMN_NAME=INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME 
		AND EXISTS (
			    SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
				 WHERE TABLE_NAME=@sTableName AND TABLE_SCHEMA='dbo' 
				 AND CONSTRAINT_TYPE = 'PRIMARY KEY' 
				 AND CONSTRAINT_NAME = INFORMATION_SCHEMA.KEY_COLUMN_USAGE.CONSTRAINT_NAME
			    )
       ), 0)) AS IsPrimaryKey,
(ISNULL((SELECT TOP 1 1 FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
	WHERE TABLE_NAME = @sTableName AND TABLE_SCHEMA='dbo'
	AND COLUMN_NAME = INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME
	 AND EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS  
	 WHERE TABLE_NAME = TABLE_NAME=@sTableName AND TABLE_SCHEMA = 'dbo'
	AND COLUMN_NAME = INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME 
	 AND EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE 
			TABLE_NAME = @sTableName AND TABLE_SCHEMA='dbo' 
			 AND CONSTRAINT_TYPE = 'UNIQUE' 
			 AND CONSTRAINT_NAME = INFORMATION_SCHEMA.KEY_COLUMN_USAGE.CONSTRAINT_NAME)
	 ), 0)) AS HasUniqueConstraint 
FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @sTableName AND TABLE_SCHEMA='dbo'";

            #endregion
            string strQueryByName = "";
            strQueryByName = @"select 
								t2.id TABLE_ID, 
								t1.TABLE_NAME, 
								t1.COLUMN_NAME, 
								t1.DATA_TYPE, 
								t1.CHARACTER_MAXIMUM_LENGTH, 
								t1.COLUMN_DEFAULT, 
								t1.IS_NULLABLE, 
								0 as IS_CHILD_TABLE, 
								(select 1 
								from INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
								where (TABLE_NAME = t1.TABLE_NAME) and (COLUMN_NAME = t1.COLUMN_NAME) and (constraint_name LIKE 'PK_%')) as Is_PK,
								'' as PARENT_COL_NAME,
								'' as CHILD_COL_NAME
						from 
								INFORMATION_SCHEMA.COLUMNS t1 join sysobjects t2 on t1.TABLE_NAME = t2.NAME
						where 
                       TABLE_NAME = '" + tableName + "'";//and Table_Catalog = '" + dbName + "'  
            return GetDataTable(strQueryByName);

        }

        /// <summary>
        /// Get related tables
        /// </summary>
        /// <param name="tableName">source table name</param>
        /// <param name="relationType">1 for Child tables only, 2 for Parent tables only, 3 for both</param>
        /// <returns></returns>
        private DataTable GetRelatedTablesByQuery(string tableName, int relationType)
        {
            #region By Table ID
            int tableId = 0;
            string strQueryByID = "";
            if (relationType == 1) // Get referenced (child) tables of this table
            {
                //strQuery = String.Format("select id TABLE_ID, name TABLE_NAME, 1 as IS_CHILD, '' as CHILD_TABLES from sysobjects Where id in (select fkeyid from sysforeignkeys Where rkeyid = {0}) order by name", tableId );
                strQueryByID = String.Format(
                                @"select 
										ctbl.id TABLE_ID, 
										ctbl.name TABLE_NAME, 
										1 as IS_CHILD, 
										'' as CHILD_TABLES,
										(select t1.name from syscolumns t1 where (t1.id = rtbl.rkeyid and t1.colid = rtbl.rkey)) as PARENT_COL_NAME,
										(select t1.name from syscolumns t1 where (t1.id = rtbl.fkeyid and t1.colid = rtbl.fkey)) as CHILD_COL_NAME
									from
										sysobjects ptbl 
										join sysforeignkeys rtbl on ptbl.id = rtbl.rkeyid
										join sysobjects ctbl on ctbl.id = rtbl.fkeyid
									where 
										(ptbl.id = {0}) 
									order by 
										ptbl.name", tableId);
            }
            else if (relationType == 2) // Get referenced (parent) tables of this table
            {
                strQueryByID = String.Format("select id TABLE_ID, name TABLE_NAME, 0 as IS_CHILD, '' as CHILD_TABLES from sysobjects Where id in (select rkeyid from sysforeignkeys Where fkeyid = {0}) order by name", tableId);
            }
            else if (relationType == 3) // Get all related tables
            {
                strQueryByID = String.Format(@"select id TABLE_ID, name TABLE_NAME, 1 as IS_CHILD, '' as CHILD_TABLES from sysobjects Where id in (select fkeyid from sysforeignkeys Where rkeyid = {0})
											union
											select id TABLE_ID, name TABLE_NAME, 0 as IS_CHILD, '' as CHILD_TABLES from sysobjects Where id in (select rkeyid from sysforeignkeys Where fkeyid = {0}) order by name", tableId);
            }

            #endregion


            string strQuery = "";
            if (relationType == 1) // Get referenced (child) tables of this table
            {
                strQuery = String.Format(
                                        @"select 
										ctbl.id TABLE_ID, 
										ctbl.name TABLE_NAME, 
										1 as IS_CHILD, 
										'' as CHILD_TABLES,
										(select t1.name from syscolumns t1 
                                        where (t1.id = rtbl.rkeyid and t1.colid = rtbl.rkey)) as PARENT_COL_NAME,
										(select t1.name from syscolumns t1 
                                        where (t1.id = rtbl.fkeyid and t1.colid = rtbl.fkey)) as CHILD_COL_NAME
									from
										sysobjects ptbl 
										join sysforeignkeys rtbl on ptbl.id = rtbl.rkeyid
										join sysobjects ctbl on ctbl.id = rtbl.fkeyid
									where 
										(ptbl.name = '{0}') 
									order by 
										ptbl.name", tableName);
            }
            else if (relationType == 2) // Get referenced (parent) tables of this table
            {
                strQuery = String.Format(
                    @"select id TABLE_ID, name TABLE_NAME,
                        0 as IS_CHILD, '' as CHILD_TABLES 
                        from sysobjects 
                        Where id in (
                                    select rkeyid from sysforeignkeys 
                                    Where fkeyid = (select id from sysobjects where name = '{0}')
                                    ) 
                                    order by name", tableName);
            }
            else if (relationType == 3) // Get all related tables
            {
                strQuery = String.Format(
                                           @"select id TABLE_ID, name TABLE_NAME, 1 as IS_CHILD, '' as CHILD_TABLES 
                                            from sysobjects 
                                            Where id in (
                                                         select fkeyid from sysforeignkeys 
                                                         Where rkeyid = (select id from sysobjects where name = '{0}')
                                                         )
											union
											select id TABLE_ID, name TABLE_NAME, 0 as IS_CHILD, '' as CHILD_TABLES
                                            from sysobjects 
                                            Where id in (
                                                         select rkeyid from sysforeignkeys 
                                                         Where fkeyid = (select id from sysobjects where name = '{0}')
                                                         ) 
                                            order by name", tableName);
            }
            return GetDataTable(strQuery);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="relationType"></param>
        /// <returns></returns>
        private DataTable GetRelatedTablesBySchema(string tableName, RelationType relationType)
        {
            //Get Foreign Keys For All Tables and check if this table Foreign Keys to other Tables 
            DataTable dt = GetDataTable(OleDbSchemaGuid.Foreign_Keys, new object[] { null, null, null });
            DataView dv = new DataView(dt);
            if (relationType == RelationType.Child)
            {
                dv.RowFilter = "PK_TABLE_NAME='" + tableName + "'";
            }
            else if (relationType == RelationType.Parent)
            {
                dv.RowFilter = "FK_TABLE_NAME='" + tableName + "'";

            }
            else if (relationType == RelationType.Both)
            {
                dv.RowFilter = "FK_TABLE_NAME='" + tableName + "' or PK_TABLE_NAME='" + tableName + "'";
            }
            dt = dv.ToTable();
            return dt;
        }


        /// <summary>
        /// Get table details properties
        /// </summary>
        /// <param name="TableName">table Name</param>
        /// <returns></returns>
        private DataTable GetTablePropertiesByQuery(string tableName)
        {
            string strQuery = @"SELECT *, 
                             (SELECT value FROM sysproperties 
                              WHERE id = OBJECT_ID(INFORMATION_SCHEMA.Columns.TABLE_SCHEMA+'.'+INFORMATION_SCHEMA.Columns.TABLE_NAME)
                              AND smallid = INFORMATION_SCHEMA.Columns.ORDINAL_POSITION 
                              AND name = 'MS_Description'
                             )AS DESCRIPTION, 
                              (SELECT IDENT_SEED(TABLE_NAME)) AS IDENT_SEED, 
                              (SELECT IDENT_INCR(TABLE_NAME)) AS IDENT_INCR, 
                              (SELECT COLUMNPROPERTY( OBJECT_ID('{0}'),COLUMN_NAME,'IsIdentity')) As IS_IDENTITY
                              FROM INFORMATION_SCHEMA.Columns 
                              WHERE table_name = '{0}' ";//AND TABLE_CATALOG = '{1}'";
            strQuery = string.Format(strQuery, tableName);
            return GetDataTable(strQuery);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrTableName"></param>
        /// <returns>Array List Of TableInfo</returns>
        private ArrayList GetMapedTables(string[] arrTableName)
        {
            DataTable dtTables = null;
            TableInfo tblInfo = null;
            ArrayList arrTablesInfo = new ArrayList();
            string strTableName = null;

            if (arrTableName == null || arrTableName.Length == 0)
            {
                //1-Get All Tables 
                dtTables = GetAllTables();
                foreach (DataRow drTableInfo in dtTables.Rows)
                {
                    strTableName = Convert.IsDBNull(drTableInfo["Table_Name"]) ? "" : Convert.ToString(drTableInfo["Table_Name"]);
                    if (strTableName != null)
                    {
                        tblInfo = new TableInfo();
                        tblInfo.Name = strTableName;
                        tblInfo.DBName = Convert.IsDBNull(drTableInfo["Table_Catalog"]) ? "" : Convert.ToString(drTableInfo["Table_Catalog"]); ;

                        tblInfo.arrColumns = GetMapedColumns(strTableName);
                        arrTablesInfo.Add(tblInfo);

                    }
                }
            }
            else if (arrTableName.Length == 1)
            {
                strTableName = arrTableName[0];
                if (strTableName != null)
                {
                    tblInfo = new TableInfo();
                    tblInfo.Name = strTableName;
                    tblInfo.arrColumns = GetMapedColumns(strTableName);
                    arrTablesInfo.Add(tblInfo);
                }
            }   
            else if (arrTableName.Length > 1) //Botón que carga una tabla cada vez
            {
                int count = 0;
                while (count < arrTableName.Length) {
                    if (arrTableName[count] != null) {
                        strTableName = arrTableName[count];
                        tblInfo = new TableInfo();
                        tblInfo.Name = strTableName;
                        tblInfo.arrColumns = GetMapedColumns(strTableName);
                        if (tblInfo.arrColumns.Count > 0)
                            arrTablesInfo.Add(tblInfo);

                    }
                    count++;
                }
            }


            return arrTablesInfo;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTableName"></param>
        /// <returns>Array List Of TableInfo</returns>
        private ArrayList GetMapedColumns(string strTableName)
        {
            //1-Get Column for table
            DataTable dtColumn = GetAllColumns(strTableName);
            DataTable dtPrimaryKeys = GetPrimaryKeys(strTableName);
            //DataTable dtRelatedTables = GetRelatedTables(strTableName, RelationType.Both);

            ArrayList arrColumnsInfo = new ArrayList();
            ColumnInfo colInfo = null;

            foreach (DataRow drColomnInfo in dtColumn.Rows)
            {
                colInfo = new ColumnInfo();
                colInfo.TableName = Convert.IsDBNull(drColomnInfo["Table_Name"]) ? "" : Convert.ToString(drColomnInfo["Table_Name"]);
                colInfo.Name = Convert.IsDBNull(drColomnInfo["Column_Name"]) ? "" : Convert.ToString(drColomnInfo["Column_Name"]);
                colInfo.IsAllowNull = Convert.IsDBNull(drColomnInfo["IS_NULLABLE"]) ? false : (drColomnInfo["IS_NULLABLE"].ToString().ToLower() == "yes" ? true : false);
                colInfo.DBType = Convert.IsDBNull(drColomnInfo["DATA_TYPE"]) ? "" : Convert.ToString(drColomnInfo["DATA_TYPE"]);
                colInfo.Length = Convert.IsDBNull(drColomnInfo["CHARACTER_MAXIMUM_LENGTH"]) ? 0 : Convert.ToInt64(drColomnInfo["CHARACTER_MAXIMUM_LENGTH"]);
                colInfo.Precision = Convert.IsDBNull(drColomnInfo["NUMERIC_PRECISION"]) ? 0 : Convert.ToInt32(drColomnInfo["NUMERIC_PRECISION"]);
                colInfo.Scale = Convert.IsDBNull(drColomnInfo["NUMERIC_SCALE"]) ? 0 : Convert.ToInt32(drColomnInfo["NUMERIC_SCALE"]);
                colInfo.Ordinal = Convert.IsDBNull(drColomnInfo["ORDINAL_POSITION"]) ? 0 : Convert.ToInt32(drColomnInfo["ORDINAL_POSITION"]);
                colInfo.IsIdentity = Convert.IsDBNull(drColomnInfo["IsIdentity"]) ? false : Convert.ToBoolean(drColomnInfo["IsIdentity"]);

                //data_type
                /*foreach (DataRow drRelatedTablesInfo in dtRelatedTables.Rows)
                {
                    string strPKTableName = Convert.IsDBNull(drRelatedTablesInfo["PK_Table_Name"]) ? "" : Convert.ToString(drRelatedTablesInfo["PK_Table_Name"]);
                    string strPKColumnName = Convert.IsDBNull(drRelatedTablesInfo["PK_Column_Name"]) ? "" : Convert.ToString(drRelatedTablesInfo["PK_Column_Name"]);
                    string strFKTableName = Convert.IsDBNull(drRelatedTablesInfo["FK_Table_Name"]) ? "" : Convert.ToString(drRelatedTablesInfo["FK_Table_Name"]);
                    string strFKColumnName = Convert.IsDBNull(drRelatedTablesInfo["FK_Column_Name"]) ? "" : Convert.ToString(drRelatedTablesInfo["FK_Column_Name"]);

                    if (colInfo.TableName == strPKTableName && colInfo.Name == strPKColumnName)
                    {
                        colInfo.IsParent = true;
                        colInfo.IsPrimaryKey = true;
                        //dead lock child get parent thin parent get child
                        // colInfo.arrChildTables.Add(GetTableInfo(strFKTableName,  strPKTableName));

                    }
                    else if (colInfo.TableName == strFKTableName && colInfo.Name == strFKColumnName)
                    {
                        colInfo.IsChild = true;
                        colInfo.IsForeignKey = true;
                        colInfo.ParentColumnName = strPKColumnName;
                        colInfo.ParentTableName = strPKTableName;
                        if (strFKTableName != strPKTableName)//not uniry
                        {
                        colInfo.ParentTable = GetTableInfo(strPKTableName);
                        }
                    }
                }*/

                foreach (DataRow drPrimaryKeysInfo in dtPrimaryKeys.Rows)
                {
                    string strPKTableName = Convert.IsDBNull(drPrimaryKeysInfo["Table_Name"]) ? "" : Convert.ToString(drPrimaryKeysInfo["Table_Name"]);
                    string strPKColumnName = Convert.IsDBNull(drPrimaryKeysInfo["Column_Name"]) ? "" : Convert.ToString(drPrimaryKeysInfo["Column_Name"]);
                    if (colInfo.TableName == strPKTableName && colInfo.Name == strPKColumnName)
                    {
                        colInfo.IsPrimaryKey = true;
                    }

                }

                arrColumnsInfo.Add(colInfo);
            }

            return arrColumnsInfo;
        }

     
        #endregion

        #region Public Methods

        /// <summary>
        /// Open Data Link Connection
        /// </summary>
        /// <returns></returns>
        public bool OpenDataLinkConnection()
        {
            string strConn = "";
            return OpenDataLinkConnection(ref strConn);
        }
        /// <summary>
        /// Open Data Link Connection for existing connection
        /// </summary>
        /// <param name="connStr">Connection string to edit, other wise empty string to create new connection</param>
        /// <returns>if success return true else return false</returns>
        public bool OpenDataLinkConnection(ref string connStr)
        {
            //if conString parameter empty open prompt to user to create new connection
            if (connStr == null || connStr == "")
            {
                if (CreateNewConnection() != "") { return true; } else { return false; }
            }
            //Edit Connection String
            else
            {
                return EditExistingConnection(ref connStr);
            }
        }


        /// <summary>
        /// Get all tables in a database
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllTables()
        {
            //Query for getting only the user tables
            return GetDataTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetAllColumns(string tableName)
        {
            DataTable dtColumn = null;
            //there is a problem in column data type when get it using schema on sql db
            if (_ConnectionString.Contains("Provider=SQLOLEDB"))
            {
                string query = @"select t1.*, COLUMNPROPERTY (OBJECT_ID(t1.Table_Name), t1.Column_Name, 'IsIdentity') as IsIdentity
                                from  INFORMATION_SCHEMA.COLUMNS   as t1
                                where TABLE_NAME = '" + tableName + "'";

                dtColumn = GetDataTable(query);
            }
            else
            {
                dtColumn = GetDataTable(OleDbSchemaGuid.Columns, new object[] { null, null, tableName });
                dtColumn.Columns.Add(new DataColumn("IsIdentity", typeof(System.Boolean)));
            }
            return dtColumn;
        }

        /// <summary>
        /// other tables which used table primary key 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetChildTables(string tableName)
        {
            return GetDataTable(OleDbSchemaGuid.Foreign_Keys, new object[] { null, null, tableName });


        }

        /// <summary>
        /// get tables which represented to foreign keys in table  
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetParentTables(string tableName)
        {

            //Get Foreign Keys For All Tables and check if this table Foreign Keys to other Tables 
            DataTable dt = GetDataTable(OleDbSchemaGuid.Foreign_Keys, new object[] { null, null, null });
            DataView dv = new DataView(dt);
            dv.RowFilter = "FK_TABLE_NAME='" + tableName + "'";
            dt = dv.ToTable();
            //go through all the foreign keys displayed
            //foreach (DataRow row in dt.Rows)
            //{

            //    string strTable = Convert.ToString( row["PK_TABLE_NAME"]);
            //    string strChild = Convert.ToString(row["FK_TABLE_NAME"]);
            //    string strParentColName = Convert.ToString(row["PK_COLUMN_NAME"]);
            //    string strChildColName = Convert.ToString(row["FK_COLUMN_NAME"]);
            //    //the relation name that will be created
            //    string strRelationName = strTable + "_" + strParentColName + "_" + strChild + "_" + strChildColName;
            //}
            return dt;
        }

        /// <summary>
        /// get tables represented to table Foreign   
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetPrimaryKeys(string tableName)
        {
            return GetDataTable(OleDbSchemaGuid.Primary_Keys, new object[] { null, null, tableName });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetTableProperties(string tableName)
        {
            return GetDataTable(OleDbSchemaGuid.Tables_Info, new object[] { null, null, tableName });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="relationType">1 for Child tables only, 2 for Parent tables only, 3 for both</param>
        /// <returns></returns>
        public DataTable GetRelatedTables(string tableName, RelationType relationType)
        {
            return GetRelatedTablesBySchema(tableName, relationType);
        }

        /// <summary>
        /// Get TableInfo object
        /// </summary>
        /// <param name="strTableName"></param>
        /// <returns>TableInfo</returns>
        public TableInfo GetTableInfo(string strTableName)
        {
            string[] arrTableNames = new string[1];
            arrTableNames[0] = strTableName;
            return (TableInfo)GetMapedTables(arrTableNames)[0];
        }

        /// <summary>
        /// Get array list of TablesInfo object 
        /// </summary>
        /// <returns>ArrayList of TableInfo</returns>
        public ArrayList GetAllTablesInfo()
        {
            return GetMapedTables(null);
        }

        public ArrayList GetOneTableInfo()
        {
            string[] arrTableNames = new string[1];
            arrTableNames[0] = "T866_RHP_PEDIDOPROV";
            return GetMapedTables(arrTableNames);
        }

        /// <summary>
        /// Añade una tabla a la lista de tablas y obtiene la información de bd de todas ellas
        /// </summary>
        /// <returns></returns>
        private string[] arrTableNames = new string[10];
        private int nTableNames = 0;
        public ArrayList AddOneTableInfo(string newTableName)
        {
            arrTableNames[nTableNames++] = newTableName;
            return GetMapedTables(arrTableNames);

        }

        public ArrayList UpdateMappedTables(string[] arrTableNamesUpdate) {
            arrTableNames = arrTableNamesUpdate;
            int nNames = 0;
            foreach (string name in arrTableNames)
                if (name != null) nNames++;
            nTableNames = nNames;
            return GetMapedTables(arrTableNamesUpdate);
        }


        #endregion
        #endregion


        #region UnUsed ColumnType Proplem

        private ColumnInfo ConvertToColumnInfo(DataRow drColumnInfo)
        {
            ColumnInfo objColumnInfo = new ColumnInfo();
            // Retrieve the data type of the field.
            // FieldDataType dataType = FieldDataType.Unknown;

            // These values could not be retrieved this way.
            bool isRowGuid = false;
            bool isPrimaryKey = false;

            // Retrieve the name of the field.
            string name = string.Empty;
            if (drColumnInfo["COLUMN_NAME"] != DBNull.Value)
            {
                objColumnInfo.Name = drColumnInfo["COLUMN_NAME"] as String;
            }


            if (drColumnInfo["DATA_TYPE"] != DBNull.Value)
            {
                // When it is an Integer (DATA_TYPE == 3) and COLUMN_FLAGS == 122 it is an AutoNumberField; otherwise
                // it is a normal integer.
                // DATA_TYPE is and Int32 and COLUMN_FLAGS is an Int64.
                if ((int)drColumnInfo["DATA_TYPE"] == 3 && (long)drColumnInfo["COLUMN_FLAGS"] == 90)
                {
                    objColumnInfo.DBType = SqlDbType.Int.ToString();//FieldDataType.AutoNumber;
                }
                // Lookup of it isn't a Memo type.
                else if ((int)drColumnInfo["DATA_TYPE"] == 130 && (long)drColumnInfo["COLUMN_FLAGS"] == 234)
                {
                    // objColumnInfo.ColumnDBType = FieldDataType.Text;
                }
                else
                {
                    objColumnInfo.DBType = ((ColumnDataType)drColumnInfo["DATA_TYPE"]).ToString();
                    //objColumnInfo.ColumnDBType = FieldDataTypeConverter.Parse(oleDbType);
                }


                // Retrieve the size of the field.
                //int size = Field.DEFAULT_SIZE;
                if (drColumnInfo["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value)
                {
                    objColumnInfo.Length = Convert.ToInt32(drColumnInfo["CHARACTER_MAXIMUM_LENGTH"]);

                    // if (objColumnInfo.ColumnDBType == FieldDataType.VarChar && size == 0)
                    // objColumnInfo.ColumnDBType = FieldDataType.Text;
                }

                // Retrieve the default value of the field, if it has one.
                string defaultValue = string.Empty;
                if ((bool)drColumnInfo["COLUMN_HASDEFAULT"])
                {
                    objColumnInfo.DefaultValue = Convert.ToString(drColumnInfo["COLUMN_DEFAULT"]);
                }

                // Retrieve the numeric presision of the field.
                int precision = -1;
                if (drColumnInfo["NUMERIC_PRECISION"] != DBNull.Value)
                {
                    objColumnInfo.Precision = Convert.ToInt32(drColumnInfo["NUMERIC_PRECISION"]);
                }

                // Retrieve the numeric scale of the field.
                int scale = -1;
                if (drColumnInfo["NUMERIC_SCALE"] != DBNull.Value)
                {
                    objColumnInfo.Scale = Convert.ToInt32(drColumnInfo["NUMERIC_SCALE"]);
                }

                // Retrieve the name of the field.
                bool allowsNull = true;
                if (drColumnInfo["IS_NULLABLE"] != DBNull.Value)
                {
                    objColumnInfo.IsAllowNull = Convert.ToBoolean(drColumnInfo["IS_NULLABLE"]);
                }

                // Retrieve the ordinal position of the field.
                int ordinal = -1;
                if (drColumnInfo["ORDINAL_POSITION"] != DBNull.Value)
                {
                    objColumnInfo.Ordinal = Convert.ToInt32(drColumnInfo["ORDINAL_POSITION"]);
                }

            }
            return objColumnInfo;

            /*
            'Short	2	5
            'Long	3	10
            'Single	4	7
            'Double	5	15
            'Currency	6	19
            'DateTime	7	8
            'Bit	11	2
            'Byte	17	3
            'GUID	72	16
            'BigBinary	128	4000
            'LongBinary	128	1073741823
            'VarBinary	128	510
            'LongText	130	536870910
            'VarChar	130	255
            'Decimal	131	28  */


        }
        //private static string TypeConverter(string srcType, out string nullDefaultValue, out string convertTo)
        //{

        //    string targetType = "";

        //    switch (srcType)
        //    {
        //        case "bit":
        //            targetType = "Boolean";
        //            nullDefaultValue = "false";
        //            convertTo = "ToBoolean";
        //            break;
        //        case "int":
        //            targetType = "Int32";
        //            nullDefaultValue = "-1";
        //            convertTo = "ToInt32";
        //            break;
        //        case "smallint":
        //            targetType = "Int16";
        //            nullDefaultValue = "-1";
        //            convertTo = "ToInt16";
        //            break;
        //        case "bigint":
        //            targetType = "Int64";
        //            nullDefaultValue = "-1";
        //            convertTo = "ToInt64";
        //            break;
        //        case "float":
        //            targetType = "float";
        //            nullDefaultValue = "-1";
        //            convertTo = "ToSingle";
        //            break;
        //        case "decimal":
        //        case "money":
        //            targetType = "Double";
        //            nullDefaultValue = "-1";
        //            convertTo = "ToDouble";
        //            break;
        //        case "char(1)":
        //        case "nchar(1)":
        //            targetType = "Char";
        //            nullDefaultValue = "String.Empty";
        //            convertTo = "ToChar";
        //            break;
        //        case "nvarchar":
        //        case "varchar":
        //        case "char(n)":
        //        case "nchar(n)":
        //        case "text":
        //        case "ntext":
        //            targetType = "String";
        //            nullDefaultValue = "String.Empty";
        //            convertTo = "ToString";
        //            break;
        //        case "datetime":
        //        case "smalldatetime":
        //            targetType = "DateTime";
        //            nullDefaultValue = "DateTime.MinValue";
        //            convertTo = "ToDateTime";
        //            break;
        //        default:
        //            targetType = "NOT_KNOWN";
        //            nullDefaultValue = "String.Empty";
        //            convertTo = "ToString";
        //            break;
        //    }

        //    return targetType;
        //}

        private static string TypeConverter(string srcType, out string nullDefaultValue, out string convertTo)
        {

            string targetType = "";

            switch (srcType)
            {
                case "bit":
                    targetType = "Boolean";
                    nullDefaultValue = "false";
                    convertTo = "ToBoolean";
                    break;
                case "int":
                    targetType = "Int32";
                    nullDefaultValue = "-1";
                    convertTo = "ToInt32";
                    break;
                case "smallint":
                    targetType = "Int16";
                    nullDefaultValue = "-1";
                    convertTo = "ToInt16";
                    break;
                case "bigint":
                    targetType = "Int64";
                    nullDefaultValue = "-1";
                    convertTo = "ToInt64";
                    break;
                case "float":
                    targetType = "float";
                    nullDefaultValue = "-1";
                    convertTo = "ToSingle";
                    break;
                case "decimal":
                case "money":
                    targetType = "Double";
                    nullDefaultValue = "-1";
                    convertTo = "ToDouble";
                    break;
                case "char(1)":
                case "nchar(1)":
                    targetType = "Char";
                    nullDefaultValue = "String.Empty";
                    convertTo = "ToChar";
                    break;
                case "nvarchar":
                case "varchar":
                case "char(n)":
                case "nchar(n)":
                case "text":
                case "ntext":
                    targetType = "String";
                    nullDefaultValue = "String.Empty";
                    convertTo = "ToString";
                    break;
                case "datetime":
                case "smalldatetime":
                    targetType = "DateTime";
                    nullDefaultValue = "DateTime.MinValue";
                    convertTo = "ToDateTime";
                    break;
                default:
                    targetType = "NOT_KNOWN";
                    nullDefaultValue = "String.Empty";
                    convertTo = "ToString";
                    break;
            }

            return targetType;
        }



               public enum ColumnDataType
        {
            Short = 2,//5
            Long = 3,//10
            Single = 4,//7
            Double = 5,//15
            Currency = 6,//	19
            DateTime = 7,//	8
            Bit = 11,//2
            Byte = 17,//3
            GUID = 72,//16
            BigBinary = 128,//4000
            LongBinary = 128,//1073741823
            VarBinary = 128,//510
            LongText = 130,//536870910
            VarChar = 130,//255
            Decimal = 131,//28
        }
        public enum ColumnType1
        {
            Empty = 0,
            TinyInt = 16,
            SmallInt = 2,
            Integer = 3,
            BigInt = 20,
            UnsignedTinyInt = 17,
            UnsignedSmallInt = 18,
            UnsignedInt = 19,
            UnsignedBigInt = 21,
            Single = 4,
            Double = 5,
            Currency = 6,
            Decimal = 14,
            Numeric = 131,
            Boolean = 11,
            Error = 10,
            UserDefined = 132,
            Variant = 12,
            IDispatch = 9,
            IUnknown = 13,
            GUID = 72,
            Date = 7,
            DBDate = 133,
            DBTime = 134,
            DBTimeStamp = 135,
            BSTR = 8,
            Char = 129,
            VarChar = 200,
            LongVarChar = 201,
            WChar = 130,
            VarWChar = 202,
            LongVarWChar = 203,
            Binary = 128,
            VarBinary = 204,
            LongVarBinary = 205,
            Chapter = 136,
            FileTime = 64,
            PropVariant = 138,
            VarNumeric = 139,
            //Array = &H2000
        }
        #endregion

        #region new solution
        //Step 1:

        //Add references to:
        //   Microsoft.Data.ConnectionUI.dll
        //   Microsoft.Data.ConnectionUI.Dialog.dll
        //   You will need to browse for them at C:\Program Files\Microsoft Visual 
        //   Studio 8\Common7\IDE(assuming that your VS2005 is installed to the default location)

        //Step 2:
        //    Microsoft.Data.ConnectionUI.Dialog.DataConnectionDialog _dialog = new 
        //    Microsoft.Data.ConnectionUI.Dialog.DataConnectionDialog();
        //    Microsoft.Data.ConnectionUI.Dialog.DataSource.AddStandardDataSources(_dialog);
        //    Microsoft.Data.ConnectionUI.Dialog.DataConnectionDialog.Show(_dialog);


        public DataSet GetMetaData(DataSet ds)
        {
            DataSet metaData = new DataSet();
            DataTable dtTable = new DataTable("Table");
            dtTable.Columns.Add(new DataColumn("Table_Name"));
            dtTable.Columns.Add(new DataColumn("Column"));
            metaData.Tables.Add(dtTable);

            DataTable dtRelations = new DataTable("Relations");
            dtRelations.Columns.Add(new DataColumn("ParentTable"));
            dtRelations.Columns.Add(new DataColumn("ParentColumn"));
            dtRelations.Columns.Add(new DataColumn("ChildTable"));
            dtRelations.Columns.Add(new DataColumn("ChildColumn"));
            metaData.Tables.Add(dtRelations);
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                for (int j = 0; j < ds.Tables[i].Columns.Count; j++)
                {
                    DataRow dr = metaData.Tables["Table"].NewRow();
                    dr["Table_Name"] = ds.Tables[i].TableName;
                    dr["Column"] = ds.Tables[i].Columns[j].ColumnName;
                    metaData.Tables["Table"].Rows.Add(dr);
                }
            }

            for (int i = 0; i < ds.Relations.Count; i++)
            {
                for (int j = 0; j < ds.Relations[i].ParentColumns.Length; j++)
                {
                    for (int k = 0; k < ds.Relations[i].ChildColumns.Length; k++)
                    {
                        DataRow dr = metaData.Tables["Relations"].NewRow();
                        dr["ParentTable"] = ds.Relations[i].ParentTable;
                        dr["ParentColumn"] = ds.Relations[i].ParentColumns[j].ColumnName;
                        dr["ChildTable"] = ds.Relations[i].ChildTable;
                        dr["ChildColumn"] = ds.Relations[i].ChildColumns[j].ColumnName;
                        metaData.Tables["Relations"].Rows.Add(dr);
                    }
                }
            }
            return metaData;
        }


        public static string DbTypeLenght(string srcType)
        {
            string dbLenght = "";
            switch (srcType)
            {
                case "bigint":
                    dbLenght = "8";
                    break;
                case "binary":
                    dbLenght = "8000";
                    break;
                case "bit":
                    dbLenght = "1";
                    break;
                case "char":
                    dbLenght = "8000";
                    break;
                case "date":
                    dbLenght = "3";
                    break;
                case "datetime":
                    dbLenght = "8";
                    break;
                case "datetime2":
                    dbLenght = "8";
                    break;
                case "datetimeoffset":
                    dbLenght = "10";
                    break;
                case "decimal":
                    dbLenght = "38";
                    break;
                case "float":
                    dbLenght = "8";
                    break;
                case "image":
                    dbLenght = "2147483647";
                    break;
                case "int":
                    dbLenght = "4";
                    break;
                case "money":
                    dbLenght = "8";
                    break;
                case "nchar":
                    dbLenght = "8000";
                    break;
                case "ntext":
                    dbLenght = "2147483647";
                    break;
                case "numeric":
                    dbLenght = "Decimal";
                    break;
                case "nvarchar":
                    dbLenght = "2147483647";
                    break;
                case "real":
                    dbLenght = "8";
                    break;
                case "rowversion":
                    dbLenght = "8";
                    break;
                case "smalldatetime":
                    dbLenght = "4";
                    break;
                case "smallint":
                    dbLenght = "2";
                    break;
                case "smallmoney":
                    dbLenght = "4";
                    break;
                case "sql_variant":
                    dbLenght = "8000";
                    break;
                case "text":
                    dbLenght = "2147483647";
                    break;
                case "time":
                    dbLenght = "5";
                    break;
                case "timestamp":
                    dbLenght = "8";
                    break;
                case "tinyint":
                    dbLenght = "1";
                    break;
                case "uniqueidentifier":
                    dbLenght = "16";
                    break;
                case "varbinary":
                    dbLenght = "2147483647";
                    break;
                case "varchar":
                    dbLenght = "2147483647";
                    break;
                case "xml":
                    dbLenght = "2147483647";
                    break;

            }

            return dbLenght;
        }

        //public static string DbTypeLenght(string srcType)
        //{
        //    string dbLenght = "";
        //    switch (srcType)
        //    {
        //        case "bit":
        //            dbLenght = "1";
        //            break;
        //        case "int":
        //            dbLenght = "4";
        //            break;
        //        case "smallint":
        //            dbLenght = "2";
        //            break;
        //        case "float":
        //            dbLenght = "8";
        //            break;
        //        case "datetime":
        //            dbLenght = "8";
        //            break;
        //        case "smalldatetime":
        //            dbLenght = "4";
        //            break;
        //        case "tinyint":
        //            dbLenght = "1";
        //            break;
        //        case "money":
        //            dbLenght = "8";
        //            break;
        //        case "date":
        //            dbLenght = "3";
        //            break;
        //        case "smallmoney":
        //            dbLenght = "4";
        //            break;
        //        case "bigint":
        //            dbLenght = "8";
        //            break;

        //    }

        //    return dbLenght;
        //}
        #endregion

    }
}

