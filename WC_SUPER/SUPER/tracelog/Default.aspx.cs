using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Script.Services;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class tracelog_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod()]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ejecuta()
    {
        //abrir conexion
        IB.sqldblib.SqlServerSP cDblib = null;

        string json = "";
        double tms1 = -1;
        double tms2 = -1;
        string errorText = "";


        try
        {
            TimeSpan ts1, ts2;

            ts1 = new TimeSpan(DateTime.Now.Ticks);
            cDblib = new IB.sqldblib.SqlServerSP(GetConStr());
            ts2 = new TimeSpan(DateTime.Now.Ticks);
            tms1 = Math.Round((ts2 - ts1).TotalMilliseconds, 0);

            //ejecutar sp
            //*** AVISO: Cambiar esto en funcion de la aplicación que se este logeando ***
            ts1 = new TimeSpan(DateTime.Now.Ticks);
            //SqlParameter[] dbparams = new SqlParameter[1] {
            //        Param(cDblib, enumDBFields.t010_idoficina, 3)
            //    };
            //DataTable dt = cDblib.DataTable("CR2I_GETLISTARECURSOS_SALAREUNION_POR_OFICINA", dbparams);
            DataTable dt = cDblib.DataTable("ZZJAVIPROC_SUPER_LOG", null);

            ts2 = new TimeSpan(DateTime.Now.Ticks);
            tms2 = Math.Round((ts2 - ts1).TotalMilliseconds, 0);

            //cerrar conexion
            cDblib.Dispose();

            json = "{" + "\"openConnection\":" + tms1.ToString() + ", \"executeSP\":" + tms2.ToString() + ", \"errorText\":\"" + errorText + "\"}";

            return json;

        }
        catch (Exception ex)
        {
            errorText = ex.Message;
            if (ex.InnerException != null) errorText += " " + ex.InnerException.Message;
            json = "{" + "\"openConnection\":" + tms1.ToString() + ", \"executeSP\":" + tms2.ToString() + ", \"errorText\":\"" + errorText + "\"}";
            return json;
        }
        finally
        {
            if (cDblib != null) cDblib.Dispose();
        }


    }

    [WebMethod()]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void graba(string app, string proxy, string hostName, List<logrow> lst)
    {
        //abrir conexion
        IB.sqldblib.SqlServerSP cDblib = null;

        try
        {
            cDblib = new IB.sqldblib.SqlServerSP(GetConStr());

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("netTime", typeof(int)));
            dt.Columns.Add(new DataColumn("openConnection", typeof(int)));
            dt.Columns.Add(new DataColumn("executeSP", typeof(int)));
            dt.Columns.Add(new DataColumn("totalTime", typeof(int)));
            dt.Columns.Add(new DataColumn("errorText", typeof(string)));


            foreach (logrow o in lst)
            {
                DataRow row = dt.NewRow();
                row["netTime"] = o.netTime;
                row["openConnection"] = o.openConnection;
                row["executeSP"] = o.executeSP;
                row["totalTime"] = o.totalTime;
                row["errorText"] = o.errorText;
                dt.Rows.Add(row);
            }

            //ejecutar sp
            SqlParameter[] dbparams = new SqlParameter[4] {
                    Param(cDblib, enumDBFields.app, app),
                    Param(cDblib, enumDBFields.proxy, proxy),
                    Param(cDblib, enumDBFields.hostName, hostName),
                    Param(cDblib, enumDBFields.tabRows, dt)
                };
            cDblib.Execute("ZZJAVIPROC_GRABALOG_APP", dbparams);

            //cerrar conexion
            cDblib.Dispose();


        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (cDblib != null) cDblib.Dispose();
        }
    }

    private enum enumDBFields : byte
    {
        t010_idoficina,
        app,
        proxy,
        hostName,
        tabRows
    }

    private static SqlParameter Param(IB.sqldblib.SqlServerSP cDblib, enumDBFields dbField, object value)
    {
        SqlParameter dbParam = null;
        string paramName = null;
        SqlDbType paramType = default(SqlDbType);
        int paramSize = 0;
        ParameterDirection paramDirection = ParameterDirection.Input;

        switch (dbField)
        {
            case enumDBFields.t010_idoficina:
                paramName = "@t010_idoficina";
                paramType = SqlDbType.SmallInt;
                paramSize = 2;
                break;
            case enumDBFields.app:
                paramName = "@app";
                paramType = SqlDbType.VarChar;
                paramSize = 20;
                break;
            case enumDBFields.hostName:
                paramName = "@hostName";
                paramType = SqlDbType.VarChar;
                paramSize = 20;
                break;
            case enumDBFields.proxy:
                paramName = "@proxy";
                paramType = SqlDbType.Bit;
                paramSize = 1;
                break;
            case enumDBFields.tabRows:
                paramName = "@tabRows";
                paramType = SqlDbType.Structured;
                paramSize = 0;
                break;

        }

        dbParam = cDblib.dbParameter(paramName, paramType, paramSize);
        dbParam.Direction = paramDirection;
        if (paramDirection == ParameterDirection.Input | paramDirection == ParameterDirection.InputOutput) dbParam.Value = value;

        return dbParam;

    }

    private static string GetConStr()
    {
        string _constr = "";

        try
        {

            if (ConfigurationManager.AppSettings["ENTORNO"].ToUpper() == "D")
            {
                _constr = ConfigurationManager.ConnectionStrings["ConexionDesarrollo"].ConnectionString;
                return _constr;
            }
            if (ConfigurationManager.AppSettings["ENTORNO"].ToUpper() == "E")
            {
                _constr = ConfigurationManager.ConnectionStrings["ConexionExplotacion"].ConnectionString;
                return _constr;
            }

            //ninguna de ellas --> lanzar excepción de error de configuración en el web.config
            throw new Exception("Error en el fichero de configuración. <add key='ENTORNO' value='" + ConfigurationManager.AppSettings["ENTORNO"] + "' />");
        }
        catch (Exception ex)
        {
            throw new Exception("Error en el fichero de configuración.", ex);
        }

    }
}

public class logrow
{
    public int netTime { get; set; }
    public int openConnection { get; set; }
    public int executeSP { get; set; }
    public int totalTime { get; set; }
    public string errorText { get; set; }
}