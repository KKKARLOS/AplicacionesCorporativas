// Decompiled with JetBrains decompiler
// Type: SII.Framework.LLDA.Gateway
// Assembly: SIFP.LLDA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 15EEB236-F5F9-4BF3-B448-BED12AC8DD3C
// Assembly location: D:\HCDIS\SIFP\SIFP.LLDA.dll

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SII.Framework.Common;
using SII.Framework.LLDA.Properties;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using System.Configuration;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace SII.Framework.LLDA
{
  public class Gateway
  {
    private const string ReturnParameter = "@Return";
    private Database _db;
    private DbConnection _dbConn;
    private DbTransaction _dbTrans;

    protected Gateway()
    {
    }

    public Gateway(string DatabaseName)
    {
      if (EMConfigurationManager.ConfigurationSource != null)
        this._db = ((ContainerBasedInstanceFactory<Database>) new DatabaseProviderFactory(EMConfigurationManager.ConfigurationSource)).Create(DatabaseName);
      else
        this._db = DatabaseFactory.CreateDatabase(DatabaseName);
    }

    public static Gateway GetInstance(string DatabaseName)
    {
      return new Gateway(DatabaseName);
    }

    public DataSet ExecuteStoredProcedureDataSet(string name, params StoredProcParam[] parameters)
    {
      DbCommand storedProcCommand = this._db.GetStoredProcCommand(name);
      if (parameters != null)
      {
        foreach (StoredProcParam storedProcParam in parameters)
          storedProcParam.Create(storedProcCommand, this._db);
      }
      if (this._dbTrans == null)
        return this._db.ExecuteDataSet(storedProcCommand);
      return this._db.ExecuteDataSet(storedProcCommand, this._dbTrans);
    }

    public DataSet ExecuteStoredProcedureDataSet(string name, string tableName, params StoredProcParam[] parameters)
    {
      DbCommand storedProcCommand = this._db.GetStoredProcCommand(name);
      if (parameters != null)
      {
        foreach (StoredProcParam storedProcParam in parameters)
          storedProcParam.Create(storedProcCommand, this._db);
      }
      DataSet dataSet1;
      DataSet dataSet2;
      if (this._dbTrans != null)
        dataSet2 = dataSet1 = this._db.ExecuteDataSet(storedProcCommand, this._dbTrans);
      else
        dataSet1 = dataSet2 = this._db.ExecuteDataSet(storedProcCommand);
      DataSet dataSet3 = dataSet2;
      dataSet3.Tables[0].TableName = tableName;
      return dataSet3;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="tableName"></param>
    /// <param name="timeout">Timeout in seconds</param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public DataSet ExecuteStoredProcedureDataSet(string name, string tableName, int timeout, params StoredProcParam[] parameters)
    {
        DbCommand storedProcCommand = this._db.GetStoredProcCommand(name);
        if (parameters != null)
        {
            foreach (StoredProcParam storedProcParam in parameters)
                storedProcParam.Create(storedProcCommand, this._db);
        }
        DataSet dataSet1;
        DataSet dataSet2;

        storedProcCommand.CommandTimeout = timeout;

        if (this._dbTrans != null)
            dataSet2 = dataSet1 = this._db.ExecuteDataSet(storedProcCommand, this._dbTrans);
        else
            dataSet1 = dataSet2 = this._db.ExecuteDataSet(storedProcCommand);
        DataSet dataSet3 = dataSet2;
        dataSet3.Tables[0].TableName = tableName;
        return dataSet3;
    }

    public DataTable ExecuteStoredProcedureDataTable(string name, params StoredProcParam[] parameters)
    {
      DbCommand storedProcCommand = this._db.GetStoredProcCommand(name);
      if (parameters != null)
      {
        foreach (StoredProcParam storedProcParam in parameters)
          storedProcParam.Create(storedProcCommand, this._db);
      }
      DataSet dataSet1;
      DataSet dataSet2;
      if (this._dbTrans != null)
        dataSet2 = dataSet1 = this._db.ExecuteDataSet(storedProcCommand, this._dbTrans);
      else
        dataSet1 = dataSet2 = this._db.ExecuteDataSet(storedProcCommand);
      DataSet dataSet3 = dataSet2;
      if (dataSet3.Tables.Count > 0)
        return dataSet3.Tables[0].Copy();
      return (DataTable) null;
    }

    public DataTable ExecuteStoredProcedureDataTable(string name, string tableName, params StoredProcParam[] parameters)
    {
      DbCommand storedProcCommand = this._db.GetStoredProcCommand(name);
      if (parameters != null)
      {
        foreach (StoredProcParam storedProcParam in parameters)
          storedProcParam.Create(storedProcCommand, this._db);
      }
      DataSet dataSet1;
      DataSet dataSet2;
      if (this._dbTrans != null)
        dataSet2 = dataSet1 = this._db.ExecuteDataSet(storedProcCommand, this._dbTrans);
      else
        dataSet1 = dataSet2 = this._db.ExecuteDataSet(storedProcCommand);
      DataSet dataSet3 = dataSet2;
      dataSet3.Tables[0].TableName = tableName;
      return dataSet3.Tables[tableName].Copy();
    }

    public IDataReader ExecuteStoredProcedureReader(string name, params StoredProcParam[] parameters)
    {
      DbCommand storedProcCommand = this._db.GetStoredProcCommand(name);
      if (parameters != null)
      {
        foreach (StoredProcParam storedProcParam in parameters)
          storedProcParam.Create(storedProcCommand, this._db);
      }
      if (this._dbTrans == null)
        return this._db.ExecuteReader(storedProcCommand);
      return this._db.ExecuteReader(storedProcCommand, this._dbTrans);
    }

    public object ExecuteStoredProcedureScalar(string name, params StoredProcParam[] parameters)
    {
      DbCommand storedProcCommand = this._db.GetStoredProcCommand(name);
      if (parameters != null)
      {
        foreach (StoredProcParam storedProcParam in parameters)
          storedProcParam.Create(storedProcCommand, this._db);
      }
      if (this._dbTrans == null)
        return this._db.ExecuteScalar(storedProcCommand);
      return this._db.ExecuteScalar(storedProcCommand, this._dbTrans);
    }

    public int ExecuteStoredProcedureNonQuery(string name, params StoredProcParam[] parameters)
    {
      DbCommand storedProcCommand = this._db.GetStoredProcCommand(name);
      if (parameters != null)
      {
        foreach (StoredProcParam storedProcParam in parameters)
          storedProcParam.Create(storedProcCommand, this._db);
      }
      if (this._dbTrans == null)
        return this._db.ExecuteNonQuery(storedProcCommand);
      return this._db.ExecuteNonQuery(storedProcCommand, this._dbTrans);
    }

    public int ExecuteStoredProcedureWithReturnValue(string name, params StoredProcParam[] parameters)
    {
      DbCommand storedProcCommand = this._db.GetStoredProcCommand(name);
      if (parameters != null)
      {
        foreach (StoredProcParam storedProcParam in parameters)
          storedProcParam.Create(storedProcCommand, this._db);
      }
      this._db.AddParameter(storedProcCommand, "@Return", DbType.Int32, ParameterDirection.ReturnValue, "", DataRowVersion.Current, (object) null);
      if (this._dbTrans == null)
        this._db.ExecuteNonQuery(storedProcCommand);
      else
        this._db.ExecuteNonQuery(storedProcCommand, this._dbTrans);
      int result = 0;
      int.TryParse(storedProcCommand.Parameters["@Return"].Value.ToString(), out result);
      return result;
    }

    public void ExecuteStoredProcedure(string name, params StoredProcParam[] parameters)
    {
      DbCommand storedProcCommand = this._db.GetStoredProcCommand(name);
      if (parameters != null)
      {
        foreach (StoredProcParam storedProcParam in parameters)
          storedProcParam.Create(storedProcCommand, this._db);
      }
      if (this._dbTrans == null)
        this._db.ExecuteNonQuery(storedProcCommand);
      else
        this._db.ExecuteNonQuery(storedProcCommand, this._dbTrans);
    }

    public DataSet ExecuteStoredProcedureDataSet(string name)
    {
      return this.ExecuteStoredProcedureDataSet(name, string.Empty, (StoredProcParam[]) null);
    }

    public DataSet ExecuteStoredProcedureDataSet(string name, string tableName)
    {
      return this.ExecuteStoredProcedureDataSet(name, tableName, (StoredProcParam[]) null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="tableName"></param>
    /// <param name="timeout">Timeout in seconds</param>
    /// <returns></returns>
    public DataSet ExecuteStoredProcedureDataSet(string name, string tableName, int timeout)
    {
        return this.ExecuteStoredProcedureDataSet(name, tableName, timeout, (StoredProcParam[])null);
    }

    public DataTable ExecuteStoredProcedureDataTable(string name)
    {
      return this.ExecuteStoredProcedureDataTable(name, string.Empty, (StoredProcParam[]) null);
    }

    public DataTable ExecuteStoredProcedureDataTable(string name, string tableName)
    {
      return this.ExecuteStoredProcedureDataTable(name, tableName, (StoredProcParam[]) null);
    }

    public IDataReader ExecuteStoredProcedureReader(string name)
    {
      return this.ExecuteStoredProcedureReader(name, (StoredProcParam[]) null);
    }

    public object ExecuteStoredProcedureScalar(string name)
    {
      return this.ExecuteStoredProcedureScalar(name, (StoredProcParam[]) null);
    }

    public int ExecuteStoredProcedureNonQuery(string name)
    {
      return this.ExecuteStoredProcedureNonQuery(name, (StoredProcParam[]) null);
    }

    public int ExecuteStoredProcedureWithReturnValue(string name)
    {
      return this.ExecuteStoredProcedureWithReturnValue(name, (StoredProcParam[]) null);
    }

    public void ExecuteStoredProcedure(string name)
    {
      this.ExecuteStoredProcedure(name, (StoredProcParam[]) null);
    }

    public DataSet ExecuteQueryDataSet(string query, string tableName, params StoredProcParam[] parameters)
    {
      DbCommand sqlStringCommand = this._db.GetSqlStringCommand(query);
      if (parameters != null)
      {
        foreach (StoredProcParam storedProcParam in parameters)
          storedProcParam.Create(sqlStringCommand, this._db);
      }
      DataSet dataSet1;
      DataSet dataSet2;
      if (this._dbTrans != null)
        dataSet2 = dataSet1 = this._db.ExecuteDataSet(sqlStringCommand, this._dbTrans);
      else
        dataSet1 = dataSet2 = this._db.ExecuteDataSet(sqlStringCommand);
      DataSet dataSet3 = dataSet2;
      if (!string.IsNullOrEmpty(tableName))
        dataSet3.Tables[0].TableName = tableName;
      return dataSet3;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="timeout">Timeout in seconds</param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public DataSet ExecuteQueryDataSet(string query, string tableName, int timeout, params StoredProcParam[] parameters)
    {
      DbCommand sqlStringCommand = this._db.GetSqlStringCommand(query);
      sqlStringCommand.CommandTimeout = timeout;
      if (parameters != null)
      {
        foreach (StoredProcParam storedProcParam in parameters)
          storedProcParam.Create(sqlStringCommand, this._db);
      }
      DataSet dataSet1;
      DataSet dataSet2;
      if (this._dbTrans != null)
        dataSet2 = dataSet1 = this._db.ExecuteDataSet(sqlStringCommand, this._dbTrans);
      else
        dataSet1 = dataSet2 = this._db.ExecuteDataSet(sqlStringCommand);
      DataSet dataSet3 = dataSet2;
      if (!string.IsNullOrEmpty(tableName))
        dataSet3.Tables[0].TableName = tableName;
      return dataSet3;
    }

    public DataSet ExecuteQueryDataSet(string query, params StoredProcParam[] parameters)
    {
      return this.ExecuteQueryDataSet(query, string.Empty, parameters);
    }

    public DataSet ExecuteQueryDataSet(string query, string tableName)
    {
      return this.ExecuteQueryDataSet(query, tableName, (StoredProcParam[]) null);
    }

    public DataSet ExecuteQueryDataSet(string query)
    {
      return this.ExecuteQueryDataSet(query, string.Empty, (StoredProcParam[]) null);
    }

    public DataTable ExecuteQueryDataTable(string query, string tableName, params StoredProcParam[] parameters)
    {
      DbCommand sqlStringCommand = this._db.GetSqlStringCommand(query);
      if (parameters != null)
      {
        foreach (StoredProcParam storedProcParam in parameters)
          storedProcParam.Create(sqlStringCommand, this._db);
      }
      DataSet dataSet1;
      DataSet dataSet2;
      if (this._dbTrans != null)
        dataSet2 = dataSet1 = this._db.ExecuteDataSet(sqlStringCommand, this._dbTrans);
      else
        dataSet1 = dataSet2 = this._db.ExecuteDataSet(sqlStringCommand);
      DataSet dataSet3 = dataSet2;
      if (!string.IsNullOrEmpty(tableName))
        dataSet3.Tables[0].TableName = tableName;
      return dataSet3.Tables[tableName].Copy();
    }

    public DataTable ExecuteQueryDataTable(string query, params StoredProcParam[] parameters)
    {
      return this.ExecuteQueryDataTable(query, string.Empty, parameters);
    }

    public DataTable ExecuteQueryDataTable(string query, string tableName)
    {
      return this.ExecuteQueryDataTable(query, tableName, (StoredProcParam[]) null);
    }

    public DataTable ExecuteQueryDataTable(string query)
    {
      return this.ExecuteQueryDataTable(query, string.Empty, (StoredProcParam[]) null);
    }

    public IDataReader ExecuteQueryReader(string query, params StoredProcParam[] parameters)
    {
      DbCommand sqlStringCommand = this._db.GetSqlStringCommand(query);
      if (parameters != null)
      {
        foreach (StoredProcParam storedProcParam in parameters)
          storedProcParam.Create(sqlStringCommand, this._db);
      }
      if (this._dbTrans != null)
        return this._db.ExecuteReader(sqlStringCommand, this._dbTrans);
      return this._db.ExecuteReader(sqlStringCommand);
    }

    public IDataReader ExecuteQueryReader(string query)
    {
      return this.ExecuteQueryReader(query, (StoredProcParam[]) null);
    }

    public object ExecuteQueryScalar(string query, params StoredProcParam[] parameters)
    {
      DbCommand sqlStringCommand = this._db.GetSqlStringCommand(query);
      if (parameters != null)
      {
        foreach (StoredProcParam storedProcParam in parameters)
          storedProcParam.Create(sqlStringCommand, this._db);
      }
      if (this._dbTrans != null)
        return this._db.ExecuteScalar(sqlStringCommand, this._dbTrans);
      return this._db.ExecuteScalar(sqlStringCommand);
    }

    public object ExecuteQueryScalar(string query)
    {
      return this.ExecuteQueryScalar(query, (StoredProcParam[]) null);
    }

    public int ExecuteQueryNonQuery(string query, params StoredProcParam[] parameters)
    {
      using (DbCommand sqlStringCommand = this._db.GetSqlStringCommand(query))
      {
        if (parameters != null)
        {
          foreach (StoredProcParam storedProcParam in parameters)
            storedProcParam.Create(sqlStringCommand, this._db);
        }
        return this._dbTrans == null ? this._db.ExecuteNonQuery(sqlStringCommand) : this._db.ExecuteNonQuery(sqlStringCommand, this._dbTrans);
      }
    }

    public int ExecuteQueryNonQuery(string query)
    {
      return this.ExecuteQueryNonQuery(query, (StoredProcParam[]) null);
    }

    public int ExecuteQueryWithReturnValue(string query, params StoredProcParam[] parameters)
    {
      DbCommand sqlStringCommand = this._db.GetSqlStringCommand(query);
      if (parameters != null)
      {
        foreach (StoredProcParam storedProcParam in parameters)
          storedProcParam.Create(sqlStringCommand, this._db);
      }
      this._db.AddParameter(sqlStringCommand, "@Return", DbType.Int32, ParameterDirection.ReturnValue, "", DataRowVersion.Current, (object) null);
      if (this._dbTrans == null)
        this._db.ExecuteNonQuery(sqlStringCommand);
      else
        this._db.ExecuteNonQuery(sqlStringCommand, this._dbTrans);
      int result = 0;
      int.TryParse(sqlStringCommand.Parameters["@Return"].Value.ToString(), out result);
      return result;
    }

    public int ExecuteQueryWithReturnValue(string query)
    {
      return this.ExecuteQueryWithReturnValue(query, (StoredProcParam[]) null);
    }

    public void ExecuteQuery(string query, params StoredProcParam[] parameters)
    {
      DbCommand sqlStringCommand = this._db.GetSqlStringCommand(query);
      if (parameters != null)
      {
        foreach (StoredProcParam storedProcParam in parameters)
          storedProcParam.Create(sqlStringCommand, this._db);
      }
      if (this._dbTrans == null)
        this._db.ExecuteNonQuery(sqlStringCommand);
      else
        this._db.ExecuteNonQuery(sqlStringCommand, this._dbTrans);
    }

    public void ExecuteQuery(string query)
    {
      this.ExecuteQuery(query, (StoredProcParam[]) null);
    }

    protected void CreateConnection()
    {
      if (this._dbTrans != null)
        throw new Exception(Resources.ERROR_TransactionInProgress);
      this._dbConn = this._db.CreateConnection();
      this._dbConn.Open();
    }

    public void BeginTransaction()
    {
      this.CreateConnection();
      this._dbTrans = this._dbConn.BeginTransaction();
    }

    public void BeginTransaction(IsolationLevel isolationLevel)
    {
      this.CreateConnection();
      this._dbTrans = this._dbConn.BeginTransaction(isolationLevel);
    }

    public void Commit()
    {
      try
      {
        this._dbTrans.Commit();
        this._dbConn.Close();
      }
      finally
      {
        this._dbTrans = (DbTransaction) null;
        this._dbConn = (DbConnection) null;
      }
    }

    public void Rollback()
    {
      try
      {
        this._dbTrans.Rollback();
        this._dbConn.Close();
      }
      finally
      {
        this._dbTrans = (DbTransaction) null;
        this._dbConn = (DbConnection) null;
      }
    }

    public bool IsInTransaction()
    {
      return this._dbTrans != null;
    }
  }

  //*********************************************************************
  //
  // The SqlHelper class is intended to encapsulate high performance, scalable best practices for 
  // common uses of SqlClient.
  //
  //*********************************************************************

  public class SqlHelper
  {
      //*********************************************************************
      //
      // Since this class provides only static methods, make the default constructor private to prevent 
      // instances from being created with "new SqlHelper()".
      //
      //*********************************************************************

      private SqlHelper() { }
      //*********************************************************************
      //
      // Nuevos métodos creados el 19/07/2006 para realizar los accesos a BD a través
      // de Procedimientos Almacenados a los que se le pasan SqlParameters.
      //
      //*********************************************************************
      #region Nuevos métodos
      //*********************************************************************
      //
      // Únicamente se establecerá la información del context_info cuando se abra una conexión
      // para asociarla a una transacción, o cuando no haya conexión.
      //
      //*********************************************************************

      /// <summary>
      /// Dada una cadena de códigos separada por ## devuelve un DataTable con el contenido
      /// </summary>
      /// <param name="sLista"></param>
      /// <returns></returns>
      public static DataTable GetDataTableCod(string sLista)
      {
          DataTable dt = new DataTable();
          dt.Columns.Add(new DataColumn("CODIGOINT", typeof(int)));
          if (sLista != "")
          {
              string[] aL = System.Text.RegularExpressions.Regex.Split(sLista, "##");
              for (int x = 0; x < aL.Length; x++)
              {
                  if (aL[x] != "")
                  {
                      DataRow row = dt.NewRow();
                      row["CODIGOINT"] = aL[x];
                      dt.Rows.Add(row);
                  }
              }
          }
          return dt;
      }
      /// <summary>
      /// Dada una cadena de denominaciones separada por ## devuelve un DataTable con el contenido
      /// </summary>
      /// <param name="sLista"></param>
      /// <returns></returns>
      public static DataTable GetDataTableDen(string sLista)
      {
          DataTable dt = new DataTable();
          dt.Columns.Add(new DataColumn("DENOMINACION", typeof(string)));
          if (sLista != "")
          {
              string[] aL = System.Text.RegularExpressions.Regex.Split(sLista, "##");
              for (int x = 0; x < aL.Length; x++)
              {
                  if (aL[x] != "")
                  {
                      DataRow row = dt.NewRow();
                      row["DENOMINACION"] = aL[x];
                      dt.Rows.Add(row);
                  }
              }
          }
          return dt;
      }

      public static DataTable GetDataTableFromArrayListCod(ArrayList aLista)
      {
          DataTable dt = new DataTable();
          dt.Columns.Add(new DataColumn("CODIGOINT", typeof(int)));

          for (int x = 0; x < aLista.Count; x++)
          {
              if (aLista[x].ToString() != "")
              {
                  DataRow row = dt.NewRow();
                  row["CODIGOINT"] = aLista[x];
                  dt.Rows.Add(row);
              }
          }

          return dt;
      }
      public static DataTable GetDataTableFromArrayListDen(ArrayList aLista)
      {
          DataTable dt = new DataTable();
          dt.Columns.Add(new DataColumn("DENOMINACION", typeof(string)));
          string sAux = "";
          for (int x = 0; x < aLista.Count; x++)
          {
              if (aLista[x].ToString() != "")
              {
                  DataRow row = dt.NewRow();
                  sAux = aLista[x].ToString().Replace("[", "|[");
                  sAux = sAux.Replace("]", "|]");
                  sAux = sAux.Replace("%", "|%");
                  sAux = sAux.Replace("_", "|_");
                  row["DENOMINACION"] = sAux;
                  dt.Rows.Add(row);
              }
          }
          return dt;
      }

      //public static void SetContextInfo(SqlConnection cn, SqlTransaction tr)
      //{
      //    if (HttpContext.Current != null)
      //    {
      //        if (HttpContext.Current.Session["IDFICEPI_ENTRADA"] != null)
      //        {
      //            string sIdFicepi = HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString();
      //            //sCadena = 128 espacios en blanco.
      //            string sCadena = "                                                                                                                                ";
      //            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(sCadena);
      //            byte[] bytesaux = System.Text.Encoding.ASCII.GetBytes(sIdFicepi);

      //            for (int i = 0; i < bytesaux.Length; i++)
      //            {
      //                bytes[i] = bytesaux[i];
      //            }

      //            SqlCommand cmd = new SqlCommand("SET CONTEXT_INFO @IdFicepiBinario", cn);
      //            cmd.Transaction = tr;
      //            //26/03/2015 Por indicación de Víctor subimos el timeout a media hora
      //            //cmd.CommandTimeout = 60;
      //            cmd.CommandTimeout = 1800;
      //            SqlParameter param = cmd.Parameters.Add("@IdFicepiBinario", System.Data.SqlDbType.VarBinary, 128);
      //            param.Value = bytes;
      //            cmd.ExecuteNonQuery();
      //            cmd.Parameters.Clear();
      //        }
      //    }
      //}

      public static int ExecuteNonQuery(string spName, params SqlParameter[] commandParameters)
      {
          SqlConnection cn = new SqlConnection(Utilidades.CadenaConexion);
          cn.Open();
          //SqlHelper.SetContextInfo(cn, null);

          //create a command and prepare it for execution
          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, cn, (SqlTransaction)null, CommandType.StoredProcedure, spName, commandParameters);

          //finally, execute the command.
          int retval = cmd.ExecuteNonQuery();

          // detach the SqlParameters from the command object, so they can be used again.
          cmd.Parameters.Clear();
          cn.Close();
          cn.Dispose();
          return retval;
      }
      public static int ExecuteNonQuery(string spName, int nTimeout, params SqlParameter[] commandParameters)
      {
          SqlConnection cn = new SqlConnection(Utilidades.CadenaConexion);
          cn.Open();
          //SqlHelper.SetContextInfo(cn, null);

          //create a command and prepare it for execution
          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, cn, (SqlTransaction)null, CommandType.StoredProcedure, spName, commandParameters);
          //después del PrepareCommand, que le pone un timeout de 60
          cmd.CommandTimeout = nTimeout;
          //finally, execute the command.
          int retval = cmd.ExecuteNonQuery();

          // detach the SqlParameters from the command object, so they can be used again.
          cmd.Parameters.Clear();
          cn.Close();
          cn.Dispose();
          return retval;
      }

      public static int ExecuteNonQueryTransaccion(SqlTransaction transaccion, string spName, params SqlParameter[] commandParameters)
      {
          if (transaccion == null) throw (new NullReferenceException("Transacción no existente"));
          //create a command and prepare it for execution
          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, transaccion.Connection, transaccion, CommandType.StoredProcedure, spName, commandParameters);

          //finally, execute the command.
          int retval = cmd.ExecuteNonQuery();

          // detach the SqlParameters from the command object, so they can be used again.
          cmd.Parameters.Clear();
          return retval;
      }
      public static int ExecuteNonQueryTransaccion(SqlTransaction transaccion, string spName, int nTimeout, params SqlParameter[] commandParameters)
      {
          if (transaccion == null) throw (new NullReferenceException("Transacción no existente"));
          //create a command and prepare it for execution
          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, transaccion.Connection, transaccion, CommandType.StoredProcedure, spName, commandParameters);
          //después del PrepareCommand, que le pone un timeout de 60
          cmd.CommandTimeout = nTimeout;

          //finally, execute the command.
          int retval = cmd.ExecuteNonQuery();

          // detach the SqlParameters from the command object, so they can be used again.
          cmd.Parameters.Clear();
          return retval;
      }

      public static DataSet ExecuteDatasetTransaccion(SqlTransaction transaccion, string spName, params SqlParameter[] commandParameters)
      {
          if (transaccion == null) throw (new NullReferenceException("Transacción no existente"));

          return ExecuteDataset(transaccion, CommandType.StoredProcedure, spName, commandParameters);
      }
      public static DataSet ExecuteDataset(string spName, params SqlParameter[] commandParameters)
      {
          //create & open a SqlConnection, and dispose of it after we are done.
          using (SqlConnection cn = new SqlConnection(Utilidades.CadenaConexion))
          {
              cn.Open();
              //La línea inferior es necesaria únicamente si se desea controlar la actividad de la aplicación
              //SqlHelper.SetContextInfo(cn, null);

              //call the overload that takes a connection in place of the connection string
              DataSet ds = ExecuteDataset(cn, CommandType.StoredProcedure, spName, commandParameters);

              cn.Close();
              cn.Dispose();
              return ds;
          }
      }

      public static DataSet ExecuteDatasetTransaccion(SqlTransaction transaccion, string spName, int nTimeout, params SqlParameter[] commandParameters)
      {
          if (transaccion == null) throw (new NullReferenceException("Transacción no existente"));

          return ExecuteDataset(transaccion, CommandType.StoredProcedure, spName, nTimeout, commandParameters);
      }
      public static DataSet ExecuteDataset(string spName, int nTimeout, params SqlParameter[] commandParameters)
      {
          //create & open a SqlConnection, and dispose of it after we are done.
          using (SqlConnection cn = new SqlConnection(Utilidades.CadenaConexion))
          {
              cn.Open();
              //La línea inferior es necesaria únicamente si se desea controlar la actividad de la aplicación
              //SqlHelper.SetContextInfo(cn, null);

              //call the overload that takes a connection in place of the connection string
              DataSet ds = ExecuteDataset(cn, CommandType.StoredProcedure, spName, nTimeout, commandParameters);

              cn.Close();
              cn.Dispose();
              return ds;
          }
      }
      #region para procedimiento almacenados que devuelven valor por return y no por select
      public static object ExecuteReturn(string spName, params SqlParameter[] commandParameters)
      {
          SqlConnection cn = new SqlConnection(Utilidades.CadenaConexion);
          cn.Open();
          //SqlHelper.SetContextInfo(cn, null);

          //create a command and prepare it for execution
          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, cn, (SqlTransaction)null, CommandType.StoredProcedure, spName, commandParameters);

          var returnParam = new SqlParameter
          {
              ParameterName = "@return",
              Direction = ParameterDirection.ReturnValue
          };
          cmd.Parameters.Add(returnParam);

          //execute the command & return the results
          cmd.ExecuteNonQuery();
          var retval = returnParam.Value;

          // detach the SqlParameters from the command object, so they can be used again.
          cmd.Parameters.Clear();
          cn.Close();
          cn.Dispose();
          return retval;
      }
      public static object ExecuteReturnTransaccion(SqlTransaction transaccion, string spName, params SqlParameter[] commandParameters)
      {
          if (transaccion == null) throw (new NullReferenceException("Transacción no existente"));

          //create a command and prepare it for execution
          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, transaccion.Connection, transaccion, CommandType.StoredProcedure, spName, commandParameters);
          var returnParam = new SqlParameter
          {
              ParameterName = "@return",
              Direction = ParameterDirection.ReturnValue
          };
          cmd.Parameters.Add(returnParam);
          //execute the command & return the results
          cmd.ExecuteNonQuery();
          var retval = returnParam.Value;

          // detach the SqlParameters from the command object, so they can be used again.
          cmd.Parameters.Clear();
          return retval;
      }
      #endregion
      public static object ExecuteScalar(string spName, params SqlParameter[] commandParameters)
      {
          SqlConnection cn = new SqlConnection(Utilidades.CadenaConexion);
          cn.Open();
          //SqlHelper.SetContextInfo(cn, null);

          //create a command and prepare it for execution
          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, cn, (SqlTransaction)null, CommandType.StoredProcedure, spName, commandParameters);

          //execute the command & return the results
          object retval = cmd.ExecuteScalar();

          // detach the SqlParameters from the command object, so they can be used again.
          cmd.Parameters.Clear();
          cn.Close();
          cn.Dispose();
          return retval;
      }
      public static object ExecuteScalar(string spName, int nTimeout, params SqlParameter[] commandParameters)
      {
          SqlConnection cn = new SqlConnection(Utilidades.CadenaConexion);
          cn.Open();
          //SqlHelper.SetContextInfo(cn, null);

          //create a command and prepare it for execution
          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, cn, (SqlTransaction)null, CommandType.StoredProcedure, spName, commandParameters);
          cmd.CommandTimeout = nTimeout;
          //execute the command & return the results
          object retval = cmd.ExecuteScalar();

          // detach the SqlParameters from the command object, so they can be used again.
          cmd.Parameters.Clear();
          cn.Close();
          cn.Dispose();
          return retval;
      }

      public static object ExecuteScalarTransaccion(SqlTransaction transaccion, string spName, params SqlParameter[] commandParameters)
      {
          if (transaccion == null) throw (new NullReferenceException("Transacción no existente"));

          //create a command and prepare it for execution
          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, transaccion.Connection, transaccion, CommandType.StoredProcedure, spName, commandParameters);

          //execute the command & return the results
          object retval = cmd.ExecuteScalar();

          // detach the SqlParameters from the command object, so they can be used again.
          cmd.Parameters.Clear();
          return retval;
      }
      public static object ExecuteScalarTransaccion(SqlTransaction transaccion, string spName, int nTimeout, params SqlParameter[] commandParameters)
      {
          if (transaccion == null) throw (new NullReferenceException("Transacción no existente"));

          //create a command and prepare it for execution
          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, transaccion.Connection, transaccion, CommandType.StoredProcedure, spName, commandParameters);
          cmd.CommandTimeout = nTimeout;

          //execute the command & return the results
          object retval = cmd.ExecuteScalar();

          // detach the SqlParameters from the command object, so they can be used again.
          cmd.Parameters.Clear();
          return retval;
      }

      public static SqlDataReader ExecuteSqlDataReader(string spName, int nTimeout, params SqlParameter[] commandParameters)
      {
          SqlConnection cn = new SqlConnection(Utilidades.CadenaConexion);
          cn.Open();

          //La línea inferior es necesaria únicamente si se desea controlar la actividad de la aplicación
          //SqlHelper.SetContextInfo(cn, null);

          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, cn, null, CommandType.StoredProcedure, spName, commandParameters);
          cmd.CommandTimeout = nTimeout;

          return cmd.ExecuteReader(CommandBehavior.CloseConnection);
      }

      public static SqlDataReader ExecuteSqlDataReader(string spName, params SqlParameter[] commandParameters)
      {
          SqlConnection cn = new SqlConnection(Utilidades.CadenaConexion);
          cn.Open();

          //La línea inferior es necesaria únicamente si se desea controlar la actividad de la aplicación
          //SqlHelper.SetContextInfo(cn, null);
          return ExecuteSqlDataReader(cn, CommandType.StoredProcedure, spName, commandParameters);
      }

      public static SqlDataReader ExecuteSqlDataReaderTransaccion(SqlTransaction transaccion, string spName, params SqlParameter[] commandParameters)
      {
          if (transaccion == null) throw (new NullReferenceException("Transacción no existente"));

          //create a command and prepare it for execution
          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, transaccion.Connection, transaccion, CommandType.StoredProcedure, spName, commandParameters);

          //finally, execute the command.
          SqlDataReader dr = cmd.ExecuteReader();

          // detach the SqlParameters from the command object, so they can be used again.
          cmd.Parameters.Clear();
          return dr;
      }

      public static SqlDataReader ExecuteSqlDataReader(string spName, params object[] parameterValues)
      {
          string sCadenaConexion = Utilidades.CadenaConexion;
          //if we receive parameter values, we need to figure out where they go
          if ((parameterValues != null) && (parameterValues.Length > 0))
          {
              //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
              SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(sCadenaConexion, spName);

              //assign the provided values to these parameters based on parameter order
              AssignParameterValues(commandParameters, parameterValues);

              //call the overload that takes an array of SqlParameters
              return ExecuteSqlDataReader(sCadenaConexion, CommandType.StoredProcedure, spName, commandParameters);
          }
          //otherwise we can just call the SP without params
          else
          {
              return ExecuteSqlDataReader(sCadenaConexion, CommandType.StoredProcedure, spName);
          }
      }

      public static SqlDataReader ExecuteSqlDataReader(string select, int nTimeout)
      {
          SqlConnection cn = new SqlConnection(Utilidades.CadenaConexion);
          cn.Open();
          SqlCommand cmd = new SqlCommand();
          cmd.CommandText = select;
          cmd.CommandType = CommandType.Text;
          cmd.Connection = cn;
          cmd.CommandTimeout = nTimeout;
          //La línea inferior es necesaria únicamente si se desea controlar la actividad de la aplicación
          //SqlHelper.SetContextInfo(cn, null);
          return cmd.ExecuteReader(CommandBehavior.CloseConnection);
      }

      public static SqlDataReader ExecuteSqlDataReaderTransaccion(SqlTransaction transaccion, string select, int nTimeout)
      {
          if (transaccion == null) throw (new NullReferenceException("Transacción no existente"));

          SqlCommand cmd = new SqlCommand();
          cmd.CommandText = select;
          cmd.CommandType = CommandType.Text;
          cmd.Connection = transaccion.Connection;
          cmd.CommandTimeout = nTimeout;
          //finally, execute the command.
          return cmd.ExecuteReader();
      }


        #endregion

        public static DataTable GetDataTableFromArrayListTPVInteger(int[] aLista)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID", typeof(int)));

            if (aLista != null) foreach (object x in aLista){ dt.Rows.Add(x);}
            return dt;
        }
        //*********************************************************************
        //
        // This method is used to attach array of SqlParameters to a SqlCommand.
        // 
        // This method will assign a value of DbNull to any parameter with a direction of
        // InputOutput and a value of null.  
        // 
        // This behavior will prevent default values from being used, but
        // this will be the less common case than an intended pure output parameter (derived as InputOutput)
        // where the user provided no input value.
        // 
        // param name="command" The command to which the parameters will be added
        // param name="commandParameters" an array of SqlParameters tho be added to command
        //
        //*********************************************************************
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
      {
          foreach (SqlParameter p in commandParameters)
          {
              //check for derived output value with no value assigned
              if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
              {
                  p.Value = DBNull.Value;
              }

              command.Parameters.Add(p);
          }
      }

      //*********************************************************************
      //
      // This method assigns an array of values to an array of SqlParameters.
      // 
      // param name="commandParameters" array of SqlParameters to be assigned values
      // param name="parameterValues" array of objects holding the values to be assigned
      //
      //*********************************************************************
      private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
      {
          if ((commandParameters == null) || (parameterValues == null))
          {
              //do nothing if we get no data
              return;
          }

          // we must have the same number of values as we pave parameters to put them in
          /*			if (commandParameters.Length != parameterValues.Length)
                      {
                          throw new ArgumentException("Parameter count does not match Parameter Value count.");
                      }
          */
          //iterate through the SqlParameters, assigning the values from the corresponding position in the 
          //value array
          //			for (int i = 0, j = commandParameters.Length; i < j; i++)
          for (int i = 0, j = parameterValues.Length; i < j; i++)
          {
              commandParameters[i].Value = parameterValues[i];
          }
      }

      //*********************************************************************
      //
      // This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
      // to the provided command.
      // 
      // param name="command" the SqlCommand to be prepared
      // param name="connection" a valid SqlConnection, on which to execute this command
      // param name="transaction" a valid SqlTransaction, or 'null'
      // param name="commandType" the CommandType (stored procedure, text, etc.)
      // param name="commandText" the stored procedure name or T-SQL command
      // param name="commandParameters" an array of SqlParameters to be associated with the command or 'null' if no parameters are required
      //
      //*********************************************************************
      private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
      {
          //if the provided connection is not open, we will open it
          if (connection.State != ConnectionState.Open)
          {
              connection.Open();
          }

          //associate the connection with the command
          command.Connection = connection;
          //command.CommandTimeout = 60;
          //26/03/2015 Por indicación de Víctor subimos el timeout a media hora
          command.CommandTimeout = 1800;
          //set the command text (stored procedure name or SQL statement)
          command.CommandText = commandText;

          //if we were provided a transaction, assign it.
          if (transaction != null)
          {
              command.Transaction = transaction;
          }

          //set the command type
          command.CommandType = commandType;

          //attach the command parameters if they are provided
          if (commandParameters != null)
          {
              AttachParameters(command, commandParameters);
          }

          return;
      }

      //*********************************************************************
      //
      // Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
      // using the provided parameters.
      //
      // e.g.:  
      //  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
      //
      // param name="connectionString" a valid connection string for a SqlConnection
      // param name="commandType" the CommandType (stored procedure, text, etc.)
      // param name="commandText" the stored procedure name or T-SQL command
      // param name="commandParameters" an array of SqlParamters used to execute the command
      // returns an int representing the number of rows affected by the command
      //
      //*********************************************************************
      public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
      {
          //create & open a SqlConnection, and dispose of it after we are done.
          using (SqlConnection cn = new SqlConnection(connectionString))
          {
              cn.Open();
              //SqlHelper.SetContextInfo(cn, null);

              //call the overload that takes a connection in place of the connection string
              int nResul = ExecuteNonQuery(cn, commandType, commandText, commandParameters);
              cn.Close();
              cn.Dispose();
              return nResul;
          }
      }

      //*********************************************************************
      //
      // Execute a SqlCommand (that returns no resultset) against the specified SqlConnection 
      // using the provided parameters.
      // 
      // e.g.:  
      //  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
      // 
      // param name="connection" a valid SqlConnection 
      // param name="commandType" the CommandType (stored procedure, text, etc.) 
      // param name="commandText" the stored procedure name or T-SQL command 
      // param name="commandParameters" an array of SqlParamters used to execute the command 
      // returns an int representing the number of rows affected by the command
      //
      //*********************************************************************
      public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
      {
          //create a command and prepare it for execution
          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters);

          //finally, execute the command.
          int retval = cmd.ExecuteNonQuery();

          // detach the SqlParameters from the command object, so they can be used again.
          cmd.Parameters.Clear();
          return retval;
      }

      //*********************************************************************
      //
      // Execute a stored procedure via a SqlCommand (that returns no resultset) against the database specified in 
      // the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
      // stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
      // 
      // This method provides no access to output parameters or the stored procedure's return value parameter.
      // 
      // e.g.:  
      //  int result = ExecuteNonQuery(connString, "PublishOrders", 24, 36);
      //
      // param name="connectionString" a valid connection string for a SqlConnection
      // param name="spName" the name of the stored prcedure
      // param name="parameterValues" an array of objects to be assigned as the input values of the stored procedure
      // returns an int representing the number of rows affected by the command
      //
      //*********************************************************************
      public static int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
      {
          //if we receive parameter values, we need to figure out where they go
          if ((parameterValues != null) && (parameterValues.Length > 0))
          {
              //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
              SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

              //assign the provided values to these parameters based on parameter order
              AssignParameterValues(commandParameters, parameterValues);

              //call the overload that takes an array of SqlParameters
              return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
          }
          //otherwise we can just call the SP without params
          else
          {
              return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
          }
      }
      /*
              public static int ExecuteNonQuery(SqlConnection connection, string spName, params object[] parameterValues)
              {
                  //if we receive parameter values, we need to figure out where they go
                  if ((parameterValues != null) && (parameterValues.Length > 0))
                  {
                      //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                      SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

                      //assign the provided values to these parameters based on parameter order
                      AssignParameterValues(commandParameters, parameterValues);

                      //call the overload that takes an array of SqlParameters
                      return ExecuteNonQuery(connection.ConnectionString, CommandType.StoredProcedure, spName, commandParameters);
                  }
                  //otherwise we can just call the SP without params
                  else
                  {
                      return ExecuteNonQuery(connection.ConnectionString, CommandType.StoredProcedure, spName);
                  }
              }
      */
      public static int ExecuteNonQueryTransaccion(SqlTransaction transaccion, string spName, params object[] parameterValues)
      {
          if (transaccion == null) throw (new NullReferenceException("Transacción no existente"));

          SqlCommand cmd = new SqlCommand();

          if ((parameterValues != null) && (parameterValues.Length > 0))
          {
              //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
              SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSetTransaction(transaccion, spName, false);

              //assign the provided values to these parameters based on parameter order
              AssignParameterValues(commandParameters, parameterValues);

              //create a command and prepare it for execution


              PrepareCommand(cmd, transaccion.Connection, transaccion, CommandType.StoredProcedure, spName, commandParameters);
          }

          //finally, execute the command.

          int nRes = cmd.ExecuteNonQuery();

          //			// detach the SqlParameters from the command object, so they can be used again.
          cmd.Parameters.Clear();
          return nRes;
      }

      public static SqlDataReader SqlDataReader(string connectionString, string sentencia)
      {
          SqlConnection cn = new SqlConnection(connectionString);
          SqlCommand cmd = new SqlCommand(sentencia);

          cn.Open();
          cmd.Connection = cn;
          //26/03/2015 Por indicación de Víctor subimos el timeout a media hora
          //cmd.CommandTimeout = 60;
          cmd.CommandTimeout = 1800;

          return cmd.ExecuteReader(CommandBehavior.CloseConnection);
      }

      public static SqlDataReader ExecuteSqlDataReader(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
      {
          SqlConnection cn = new SqlConnection(connectionString);
          cn.Open();
          return ExecuteSqlDataReader(cn, commandType, commandText, commandParameters);
      }
      //*********************************************************************
      //(1)
      // Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
      // the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
      // stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
      // 
      // This method provides no access to output parameters or the stored procedure's return value parameter.
      // 
      // e.g.:  
      //  SqlDataReader dr = ExecuteSqlDataReader(connString, "GetOrders", 24, 36);
      // 
      // param name="connectionString" a valid connection string for a SqlConnection
      // param name="spName" the name of the stored procedure
      // param name="parameterValues" an array of objects to be assigned as the input values of the stored procedure
      // returns a dataset containing the resultset generated by the command
      //
      //*********************************************************************

      public static SqlDataReader ExecuteSqlDataReader(string connectionString, string spName, params object[] parameterValues)
      {
          //if we receive parameter values, we need to figure out where they go
          if ((parameterValues != null) && (parameterValues.Length > 0))
          {
              //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
              SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

              //assign the provided values to these parameters based on parameter order
              AssignParameterValues(commandParameters, parameterValues);

              //call the overload that takes an array of SqlParameters
              return ExecuteSqlDataReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
          }
          //otherwise we can just call the SP without params
          else
          {
              return ExecuteSqlDataReader(connectionString, CommandType.StoredProcedure, spName);
          }
      }

      public static SqlDataReader ExecuteSqlDataReader(SqlConnection connection, string spName, params object[] parameterValues)
      {
          //if we receive parameter values, we need to figure out where they go
          if ((parameterValues != null) && (parameterValues.Length > 0))
          {
              //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
              SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

              //assign the provided values to these parameters based on parameter order
              AssignParameterValues(commandParameters, parameterValues);

              //call the overload that takes an array of SqlParameters
              return ExecuteSqlDataReader(connection.ConnectionString, CommandType.StoredProcedure, spName, commandParameters);
          }
          //otherwise we can just call the SP without params
          else
          {
              return ExecuteSqlDataReader(connection.ConnectionString, CommandType.StoredProcedure, spName);
          }
      }

      //*********************************************************************
      //
      // Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
      // using the provided parameters.
      // 
      // e.g.:  
      //  SqlDataReader dr = ExecuteSqlDataReader(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
      //
      // param name="connection" a valid SqlConnection
      // param name="commandType" the CommandType (stored procedure, text, etc.)
      // param name="commandText" the stored procedure name or T-SQL command
      // param name="commandParameters" an array of SqlParamters used to execute the command
      // returns a dataset containing the resultset generated by the command
      //
      //*********************************************************************
      public static SqlDataReader ExecuteSqlDataReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
      {
          //create a command and prepare it for execution
          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters);

          return cmd.ExecuteReader(CommandBehavior.CloseConnection);
      }

      public static SqlDataReader ExecuteSqlDataReaderTransaccion(SqlTransaction transaccion, string spName, params object[] parameterValues)
      {
          if (transaccion == null) throw (new NullReferenceException("Transacción no existente"));

          SqlCommand cmd = new SqlCommand();
          if ((parameterValues != null) && (parameterValues.Length > 0))
          {
              //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
              SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSetTransaction(transaccion, spName, false);

              //assign the provided values to these parameters based on parameter order
              AssignParameterValues(commandParameters, parameterValues);

              //create a command and prepare it for execution
              PrepareCommand(cmd, transaccion.Connection, transaccion, CommandType.StoredProcedure, spName, commandParameters);
          }

          //finally, execute the command.
          SqlDataReader dr = cmd.ExecuteReader();

          return dr;
      }

      //*********************************************************************
      //
      // Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
      // using the provided parameters.
      // 
      // e.g.:  
      //  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
      // 
      // param name="connectionString" a valid connection string for a SqlConnection 
      // param name="commandType" the CommandType (stored procedure, text, etc.) 
      // param name="commandText" the stored procedure name or T-SQL command 
      // param name="commandParameters" an array of SqlParamters used to execute the command 
      // returns a dataset containing the resultset generated by the command
      //
      //*********************************************************************
      public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
      {
          //create & open a SqlConnection, and dispose of it after we are done.
          using (SqlConnection cn = new SqlConnection(connectionString))
          {
              cn.Open();
              //call the overload that takes a connection in place of the connection string
              DataSet ds = ExecuteDataset(cn, commandType, commandText, commandParameters);
              cn.Close();
              cn.Dispose();
              return ds;
          }
      }

      public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, int nTimeout, params SqlParameter[] commandParameters)
      {
          //create & open a SqlConnection, and dispose of it after we are done.
          using (SqlConnection cn = new SqlConnection(connectionString))
          {
              cn.Open();
              //call the overload that takes a connection in place of the connection string
              DataSet ds = ExecuteDataset(cn, commandType, commandText, nTimeout, commandParameters);
              cn.Close();
              cn.Dispose();
              return ds;
          }
      }

      //*********************************************************************
      //
      // Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
      // the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
      // stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
      // 
      // This method provides no access to output parameters or the stored procedure's return value parameter.
      // 
      // e.g.:  
      //  DataSet ds = ExecuteDataset(connString, "GetOrders", 24, 36);
      // 
      // param name="connectionString" a valid connection string for a SqlConnection
      // param name="spName" the name of the stored procedure
      // param name="parameterValues" an array of objects to be assigned as the input values of the stored procedure
      // returns a dataset containing the resultset generated by the command
      //
      //*********************************************************************

      public static DataSet ExecuteDataset(string connectionString, string spName, params object[] parameterValues)
      {
          //if we receive parameter values, we need to figure out where they go
          if ((parameterValues != null) && (parameterValues.Length > 0))
          {
              //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
              SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

              //assign the provided values to these parameters based on parameter order
              AssignParameterValues(commandParameters, parameterValues);

              //call the overload that takes an array of SqlParameters
              return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters);
          }
          //otherwise we can just call the SP without params
          else
          {
              return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
          }
      }

      public static DataSet ExecuteDataset(string connectionString, string spName, int nTimeout, params object[] parameterValues)
      {
          //if we receive parameter values, we need to figure out where they go
          if ((parameterValues != null) && (parameterValues.Length > 0))
          {
              //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
              SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

              //assign the provided values to these parameters based on parameter order
              AssignParameterValues(commandParameters, parameterValues);

              //call the overload that takes an array of SqlParameters
              return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, nTimeout, commandParameters);
              //return ExecuteDataset(connectionString, spName, nTimeout, commandParameters);
          }
          //otherwise we can just call the SP without params
          else
          {
              return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
          }
      }

      /* Para que las consultas personalizadas no cacheen los parámetros */
      public static DataSet ExecuteDatasetCP(string connectionString, string spName, int nTimeout, params object[] parameterValues)
      {
          //if we receive parameter values, we need to figure out where they go
          if ((parameterValues != null) && (parameterValues.Length > 0))
          {
              //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
              SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSetCP(connectionString, spName, false);

              //assign the provided values to these parameters based on parameter order
              AssignParameterValues(commandParameters, parameterValues);

              //call the overload that takes an array of SqlParameters
              return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, nTimeout, commandParameters);
              //return ExecuteDataset(connectionString, spName, nTimeout, commandParameters);
          }
          //otherwise we can just call the SP without params
          else
          {
              return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
          }
      }

      public static DataSet ExecuteDataset(SqlConnection connection, string spName, params object[] parameterValues)
      {
          //if we receive parameter values, we need to figure out where they go
          if ((parameterValues != null) && (parameterValues.Length > 0))
          {
              //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
              SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

              //assign the provided values to these parameters based on parameter order
              AssignParameterValues(commandParameters, parameterValues);

              //call the overload that takes an array of SqlParameters
              return ExecuteDataset(connection.ConnectionString, CommandType.StoredProcedure, spName, commandParameters);
          }
          //otherwise we can just call the SP without params
          else
          {
              return ExecuteDataset(connection.ConnectionString, CommandType.StoredProcedure, spName);
          }
      }

      //*********************************************************************
      //
      // Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
      // using the provided parameters.
      // 
      // e.g.:  
      //  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
      //
      // param name="connection" a valid SqlConnection
      // param name="commandType" the CommandType (stored procedure, text, etc.)
      // param name="commandText" the stored procedure name or T-SQL command
      // param name="commandParameters" an array of SqlParamters used to execute the command
      // returns a dataset containing the resultset generated by the command
      //
      //*********************************************************************
      public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
      {
          //create a command and prepare it for execution
          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters);

          //create the DataAdapter & DataSet
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          DataSet ds = new DataSet();

          //fill the DataSet using default values for DataTable names, etc.
          da.Fill(ds);

          // detach the SqlParameters from the command object, so they can be used again.			
          cmd.Parameters.Clear();

          //return the dataset
          return ds;
      }

      public static DataSet ExecuteDataset(SqlTransaction transaccion, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
      {
          //create a command and prepare it for execution
          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, transaccion.Connection, transaccion, commandType, commandText, commandParameters);

          //create the DataAdapter & DataSet
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          DataSet ds = new DataSet();

          //fill the DataSet using default values for DataTable names, etc.
          da.Fill(ds);

          // detach the SqlParameters from the command object, so they can be used again.			
          cmd.Parameters.Clear();

          //return the dataset
          return ds;
      }

      public static DataSet ExecuteDataset(SqlTransaction transaccion, CommandType commandType, string commandText, int nTimeout, params SqlParameter[] commandParameters)
      {
          //create a command and prepare it for execution
          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, transaccion.Connection, transaccion, commandType, commandText, commandParameters);
          //después del PrepareCommand, que le pone un timeout de 60
          cmd.CommandTimeout = nTimeout;

          //create the DataAdapter & DataSet
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          DataSet ds = new DataSet();

          //fill the DataSet using default values for DataTable names, etc.
          da.Fill(ds);

          // detach the SqlParameters from the command object, so they can be used again.			
          cmd.Parameters.Clear();

          //return the dataset
          return ds;
      }

      public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, int nTimeout, params SqlParameter[] commandParameters)
      {
          //create a command and prepare it for execution
          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters);
          //después del PrepareCommand, que le pone un timeout de 60
          cmd.CommandTimeout = nTimeout;

          //create the DataAdapter & DataSet
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          DataSet ds = new DataSet();

          //fill the DataSet using default values for DataTable names, etc.
          da.Fill(ds);

          // detach the SqlParameters from the command object, so they can be used again.			
          cmd.Parameters.Clear();

          //return the dataset
          return ds;
      }

      //*********************************************************************
      //
      // Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
      // using the provided parameters.
      // 
      // e.g.:  
      //  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
      // 
      // param name="connectionString" a valid connection string for a SqlConnection 
      // param name="commandType" the CommandType (stored procedure, text, etc.) 
      // param name="commandText" the stored procedure name or T-SQL command 
      // param name="commandParameters" an array of SqlParamters used to execute the command 
      // returns an object containing the value in the 1x1 resultset generated by the command
      //
      //*********************************************************************
      public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
      {
          //create & open a SqlConnection, and dispose of it after we are done.
          using (SqlConnection cn = new SqlConnection(connectionString))
          {
              cn.Open();
              //call the overload that takes a connection in place of the connection string
              object o = ExecuteScalar(cn, commandType, commandText, commandParameters);
              cn.Close();
              cn.Dispose();
              return o;
          }
      }

      //*********************************************************************
      //
      // Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the database specified in 
      // the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
      // stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
      // 
      // This method provides no access to output parameters or the stored procedure's return value parameter.
      // 
      // e.g.:  
      //  int orderCount = (int)ExecuteScalar(connString, "GetOrderCount", 24, 36);
      // 
      // param name="connectionString" a valid connection string for a SqlConnection 
      // param name="spName" the name of the stored procedure 
      // param name="parameterValues" an array of objects to be assigned as the input values of the stored procedure 
      // returns an object containing the value in the 1x1 resultset generated by the command
      //
      //*********************************************************************
      public static object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
      {
          //if we receive parameter values, we need to figure out where they go
          if ((parameterValues != null) && (parameterValues.Length > 0))
          {
              //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
              SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

              //assign the provided values to these parameters based on parameter order
              AssignParameterValues(commandParameters, parameterValues);

              //call the overload that takes an array of SqlParameters
              return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
          }
          //otherwise we can just call the SP without params
          else
          {
              return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
          }
      }

      public static object ExecuteScalar(SqlConnection connection, string spName, params object[] parameterValues)
      {
          //if we receive parameter values, we need to figure out where they go
          if ((parameterValues != null) && (parameterValues.Length > 0))
          {
              //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
              SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

              //assign the provided values to these parameters based on parameter order
              AssignParameterValues(commandParameters, parameterValues);

              //call the overload that takes an array of SqlParameters
              return ExecuteScalar(connection.ConnectionString, CommandType.StoredProcedure, spName, commandParameters);
          }
          //otherwise we can just call the SP without params
          else
          {
              return ExecuteScalar(connection.ConnectionString, CommandType.StoredProcedure, spName);
          }
      }

      public static object ExecuteScalarTransaccion(SqlTransaction transaccion, string spName, params object[] parameterValues)
      {
          if (transaccion == null) throw (new NullReferenceException("Transacción no existente"));

          SqlCommand cmd = new SqlCommand();

          if ((parameterValues != null) && (parameterValues.Length > 0))
          {
              //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
              SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSetTransaction(transaccion, spName, false);

              //assign the provided values to these parameters based on parameter order
              AssignParameterValues(commandParameters, parameterValues);

              //create a command and prepare it for execution


              PrepareCommand(cmd, transaccion.Connection, transaccion, CommandType.StoredProcedure, spName, commandParameters);
          }

          //finally, execute the command.

          object nRes = cmd.ExecuteScalar();

          // detach the SqlParameters from the command object, so they can be used again.
          cmd.Parameters.Clear();
          return nRes;
      }

      //*********************************************************************
      //
      // Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection 
      // using the provided parameters.
      // 
      // e.g.:  
      //  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
      // 
      // param name="connection" a valid SqlConnection 
      // param name="commandType" the CommandType (stored procedure, text, etc.) 
      // param name="commandText" the stored procedure name or T-SQL command 
      // param name="commandParameters" an array of SqlParamters used to execute the command 
      // returns an object containing the value in the 1x1 resultset generated by the command
      //
      //*********************************************************************
      public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
      {
          //create a command and prepare it for execution
          SqlCommand cmd = new SqlCommand();
          PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters);

          //execute the command & return the results
          object retval = cmd.ExecuteScalar();

          // detach the SqlParameters from the command object, so they can be used again.
          cmd.Parameters.Clear();
          return retval;

      }
      public static void SetCommandType(SqlCommand sqlCmd, CommandType cmdType, string cmdText)
      {
          sqlCmd.CommandType = cmdType;
          sqlCmd.CommandText = cmdText;
      }
      public static Object ExecuteScalarCmd(SqlCommand sqlCmd)
      {
          if (sqlCmd == null)
              throw (new ArgumentNullException("sqlCmd"));

          Object result = null;
          SqlConnection cn = new SqlConnection(Utilidades.CadenaConexion);
          sqlCmd.Connection = cn;
          //26/03/2015 Por indicación de Víctor subimos el timeout a media hora
          //sqlCmd.CommandTimeout = 60;
          sqlCmd.CommandTimeout = 1800;
          cn.Open();
          //sqlCmd.ExecuteScalar();

          try
          {
              result = sqlCmd.ExecuteScalar();
          }
          catch (System.Exception objError)
          {
              switch (objError.GetType().ToString())
              {
                  case "System.Data.SqlClient.SqlException":
                      System.Data.SqlClient.SqlException nuevoError = (System.Data.SqlClient.SqlException)objError;
                      switch (nuevoError.Number)
                      {
                          case 547:
                              result = "Denegado. El sistema ha detectado elementos relacionados con el registro seleccionado. ";
                              break;
                          default:
                              result = "Error de Sql Server: " + nuevoError.Message;
                              break;
                      }
                      break;
                  default:
                      result += "Error: " + objError.Message;
                      break;
              }
          }
          cn.Close();
          return result;
      }
      public static void AddParamToSQLCmd(SqlCommand sqlCmd, string paramId, SqlDbType sqlType, int paramSize, ParameterDirection paramDirection, object paramvalue)
      {
          // Validate Parameter Properties
          if (sqlCmd == null)
              throw (new ArgumentNullException("sqlCmd"));
          if (paramId == string.Empty)
              throw (new ArgumentOutOfRangeException("paramId"));

          // Add Parameter
          SqlParameter newSqlParam = new SqlParameter();
          newSqlParam.ParameterName = paramId;
          newSqlParam.SqlDbType = sqlType;
          newSqlParam.Direction = paramDirection;

          if (paramSize > 0)
              newSqlParam.Size = paramSize;

          if (paramvalue != null)
              newSqlParam.Value = paramvalue;

          sqlCmd.Parameters.Add(newSqlParam);
      }
  }

  //*********************************************************************
  //
  // SqlHelperParameterCache provides functions to leverage a static cache of procedure parameters, and the
  // ability to discover parameters for stored procedures at run-time.
  //
  //*********************************************************************

  public sealed class SqlHelperParameterCache
  {
      //*********************************************************************
      //
      // Since this class provides only static methods, make the default constructor private to prevent 
      // instances from being created with "new SqlHelperParameterCache()".
      //
      //*********************************************************************

      private SqlHelperParameterCache() { }

      private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

      //*********************************************************************
      //
      // resolve at run time the appropriate set of SqlParameters for a stored procedure
      // 
      // param name="connectionString" a valid connection string for a SqlConnection 
      // param name="spName" the name of the stored procedure 
      // param name="includeReturnValueParameter" whether or not to include their return value parameter 
      //
      //*********************************************************************

      private static SqlParameter[] DiscoverSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
      {
          using (SqlConnection cn = new SqlConnection(connectionString))
          using (SqlCommand cmd = new SqlCommand(spName, cn))
          {
              cn.Open();
              cmd.CommandType = CommandType.StoredProcedure;

              SqlCommandBuilder.DeriveParameters(cmd);

              if (!includeReturnValueParameter)
              {
                  cmd.Parameters.RemoveAt(0);
              }

              SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count]; ;

              cmd.Parameters.CopyTo(discoveredParameters, 0);

              return discoveredParameters;
          }
      }
      private static SqlParameter[] DiscoverSpParameterSetTransaction(SqlTransaction transaction, string spName, bool includeReturnValueParameter)
      {
          using (SqlCommand cmd = new SqlCommand(spName, transaction.Connection, transaction))
          {
              cmd.CommandType = CommandType.StoredProcedure;
              SqlCommandBuilder.DeriveParameters(cmd);

              if (!includeReturnValueParameter)
              {
                  cmd.Parameters.RemoveAt(0);
              }

              SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count]; ;

              cmd.Parameters.CopyTo(discoveredParameters, 0);

              return discoveredParameters;
          }
      }
      private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
      {
          //deep copy of cached SqlParameter array
          SqlParameter[] clonedParameters = new SqlParameter[originalParameters.Length];

          for (int i = 0, j = originalParameters.Length; i < j; i++)
          {
              clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
          }

          return clonedParameters;
      }

      //*********************************************************************
      //
      // add parameter array to the cache
      //
      // param name="connectionString" a valid connection string for a SqlConnection 
      // param name="commandText" the stored procedure name or T-SQL command 
      // param name="commandParameters" an array of SqlParamters to be cached 
      //
      //*********************************************************************

      public static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)
      {
          string hashKey = connectionString + ":" + commandText;

          paramCache[hashKey] = commandParameters;
      }

      //*********************************************************************
      //
      // Retrieve a parameter array from the cache
      // 
      // param name="connectionString" a valid connection string for a SqlConnection 
      // param name="commandText" the stored procedure name or T-SQL command 
      // returns an array of SqlParamters
      //
      //*********************************************************************

      public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
      {
          string hashKey = connectionString + ":" + commandText;

          SqlParameter[] cachedParameters = (SqlParameter[])paramCache[hashKey];

          if (cachedParameters == null)
          {
              return null;
          }
          else
          {
              return CloneParameters(cachedParameters);
          }
      }

      //*********************************************************************
      //
      // Retrieves the set of SqlParameters appropriate for the stored procedure
      // 
      // This method will query the database for this information, and then store it in a cache for future requests.
      // 
      // param name="connectionString" a valid connection string for a SqlConnection 
      // param name="spName" the name of the stored procedure 
      // returns an array of SqlParameters
      //
      //*********************************************************************

      public static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
      {
          return GetSpParameterSet(connectionString, spName, false);
      }

      //*********************************************************************
      //
      // Retrieves the set of SqlParameters appropriate for the stored procedure
      // 
      // This method will query the database for this information, and then store it in a cache for future requests.
      // 
      // param name="connectionString" a valid connection string for a SqlConnection 
      // param name="spName" the name of the stored procedure 
      // param name="includeReturnValueParameter" a bool value indicating whether the return value parameter should be included in the results 
      // returns an array of SqlParameters
      //
      //*********************************************************************

      public static SqlParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
      {
          string hashKey = connectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");

          SqlParameter[] cachedParameters;

          cachedParameters = (SqlParameter[])paramCache[hashKey];

          if (cachedParameters == null)
          {
              cachedParameters = (SqlParameter[])(paramCache[hashKey] = DiscoverSpParameterSet(connectionString, spName, includeReturnValueParameter));
          }

          return CloneParameters(cachedParameters);
      }
      /* Para que las consultas personalizadas no cacheen los parámetros. */
      public static SqlParameter[] GetSpParameterSetCP(string connectionString, string spName, bool includeReturnValueParameter)
      {
          SqlParameter[] cachedParameters = DiscoverSpParameterSet(connectionString, spName, includeReturnValueParameter);
          return CloneParameters(cachedParameters);
      }

      public static SqlParameter[] GetSpParameterSetTransaction(SqlTransaction transaccion, string spName, bool includeReturnValueParameter)
      {
          string hashKey = transaccion.Connection.ConnectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");

          SqlParameter[] cachedParameters;

          cachedParameters = (SqlParameter[])paramCache[hashKey];

          if (cachedParameters == null)
          {
              cachedParameters = (SqlParameter[])(paramCache[hashKey] = DiscoverSpParameterSetTransaction(transaccion, spName, includeReturnValueParameter));
          }

          return CloneParameters(cachedParameters);
      }
  }
  public class Utilidades
    {
        public Utilidades()
        {
            //
            // TODO: agregar aquí la lógica del constructor
            //
        }

        public static string CadenaConexion
        {
            get { return "server=HCDISBEFE;database=HCDIS_OPT;uid=sa;pwd=pass@word1;Trusted_Connection=no;app=IndigoHIS;"; }
        }
    }
  public class ParametroSql
    {
        public static SqlParameter add(string sNombre, SqlDbType oTipo, int nLongitud, object oValor)
        {
            SqlParameter oParam = new SqlParameter(sNombre, oTipo, (oTipo == SqlDbType.Text) ? 2147483647 : nLongitud);
            oParam.Value = oValor;
            return oParam;
        }
        public static SqlParameter add(string sNombre, SqlDbType oTipo, ParameterDirection oDirection)
        {
            SqlParameter oParam = new SqlParameter(sNombre, oTipo);
            oParam.Direction = oDirection;
            return oParam;
        }
        //Para usar tipos estructurados (tipo table) de SQL-Server
        public static SqlParameter add(string sNombre, SqlDbType oTipo, object oValor)
        {
            SqlParameter oParam = new SqlParameter(sNombre, oTipo);
            oParam.Value = oValor;
            return oParam;
        }
    }
}
