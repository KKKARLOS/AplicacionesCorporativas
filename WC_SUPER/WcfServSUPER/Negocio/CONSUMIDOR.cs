using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
//using IB.Services.Super.DAL;

namespace IB.Services.Super.Negocio
{
    public class CONSUMIDOR
    {
        #region Propiedades y Atributos

        private string _t145_idusuario;
        public string t145_idusuario
        {
            get { return _t145_idusuario; }
            set { _t145_idusuario = value; }
        }

        private string _t145_clave;
        public string t145_clave
        {
            get { return _t145_clave; }
            set { _t145_clave = value; }
        }

        private DateTime _t145_fiv;
        public DateTime t145_fiv
        {
            get { return _t145_fiv; }
            set { _t145_fiv = value; }
        }

        private DateTime _t145_ffv;
        public DateTime t145_ffv
        {
            get { return _t145_ffv; }
            set { _t145_ffv = value; }
        }

        private short _t145_intentos;
        public short t145_intentos
        {
            get { return _t145_intentos; }
            set { _t145_intentos = value; }
        }

        #endregion
        public CONSUMIDOR()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static CONSUMIDOR GetDatos(SqlTransaction tr, string t145_idusuario)
        {
            //string sError = "(a)";
            CONSUMIDOR o = new CONSUMIDOR();
            //sError = "(b)";
            try
            {
                SqlDataReader dr = IB.Services.Super.DAL.CONSUMIDOR.Select(tr, t145_idusuario);
                //sError = "(c)";
                if (dr.Read())
                {
                    o.t145_idusuario = t145_idusuario;
                    if (dr["t145_clave"] != DBNull.Value)
                        o.t145_clave = dr["t145_clave"].ToString();
                    if (dr["t145_intentos"] != DBNull.Value)
                        o.t145_intentos = short.Parse(dr["t145_intentos"].ToString());
                    else
                        o.t145_intentos = 0;
                    if (dr["t145_fiv"] != DBNull.Value)
                        o.t145_fiv = (DateTime)dr["t145_fiv"];
                    if (dr["t145_ffv"] != DBNull.Value)
                        o.t145_ffv = (DateTime)dr["t145_ffv"];
                }
                else
                {
                    o.t145_idusuario = "";
                    //throw (new NullReferenceException("No se ha obtenido ningun dato de CONSUMIDOR"));
                }
                dr.Close();
                dr.Dispose();
            }
            catch(Exception e)
            {
                o.t145_idusuario = "-1";
                o.t145_clave = "Error " + e.Message;
            }
            return o;
        }
        /// <summary>
        /// Establece el nº de accesos erróneos de un consumidor de servicios web
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t145_idusuario"></param>
        /// <param name="nIntentos"></param>
        public static void SetIntentos(SqlTransaction tr, string t145_idusuario, short nIntentos)
        {
            IB.Services.Super.DAL.CONSUMIDOR.SetIntentos(tr, t145_idusuario, nIntentos);
        }
    }
}
