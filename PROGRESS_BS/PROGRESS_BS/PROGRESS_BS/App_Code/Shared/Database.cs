using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
//using IB.dblib;

namespace IB.Progress.Shared
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
                try
                {

                    if (ConfigurationManager.AppSettings["ENTORNO"].ToUpper() == "D")
                    {
                        _constr = ConfigurationManager.ConnectionStrings["ConexionDesarrollo"].ConnectionString;
                        return _constr;
                    }

                    //Entorno de pruebas
                    if (ConfigurationManager.AppSettings["ENTORNO"].ToUpper() == "P")
                    {
                        _constr = ConfigurationManager.ConnectionStrings["ConexionPruebas"].ConnectionString;
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

            return _constr;
        }

    }

}
