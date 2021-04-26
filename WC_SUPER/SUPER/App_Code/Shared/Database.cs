using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;


namespace IB.SUPER.Shared
{

    public class Database
    {
        public Database()
        {
        }

        private static string _constr = "";

        public static string GetConStr()
        {
            if (_constr.Trim().Length == 0)
            {

                string entorno = ConfigurationManager.ConnectionStrings["ENTORNO"].ConnectionString; ;
                try
                {

                    if (entorno.ToUpper() == "D")
                    {
                        _constr = ConfigurationManager.ConnectionStrings["ConexionDesarrollo"].ConnectionString;
                        return _constr;
                    }
                    if (entorno.ToUpper() == "E")
                    {
                        _constr = ConfigurationManager.ConnectionStrings["ConexionExplotacion"].ConnectionString;
                        return _constr;
                    }

                    try 
                	{
                        _constr = ConfigurationManager.ConnectionStrings["Conexion" + entorno].ConnectionString;
                        return _constr;
	                }
	                catch (Exception)
	                {
                        throw new Exception("No se ha encontrado un ConnectionString con valor 'Conexion" + entorno  + "'");
	                }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error en web.config.", ex);
                }
            }

            return _constr;
        }


        #region array to datatable --> sqldbtype.structured
        public static DataTable ArrayToDataTable(string[] values, string nomcol) {

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn(nomcol, typeof(String)));

            if (values != null)
            {
                for (int j = 0; j <= values.Length - 1; j++)
                {
                    DataRow oRow = dt.NewRow();
                    oRow[0] = values[j];
                    dt.Rows.Add(oRow);
                }
            }

            return dt;
        }


        public static DataTable ArrayToDataTable(Int32[] values, string nomcol)
        {

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn(nomcol, typeof(String)));

            if (values != null)
            {
                for (int j = 0; j <= values.Length - 1; j++)
                {
                    DataRow oRow = dt.NewRow();
                    oRow[0] = values[j];
                    dt.Rows.Add(oRow);
                }
            }

            return dt;
        }
        #endregion

    }

}
