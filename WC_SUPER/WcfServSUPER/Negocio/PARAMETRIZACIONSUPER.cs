using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace IB.Services.Super.Negocio
{
    public class PARAMETRIZACIONSUPER
    {
        #region Propiedades y Atributos

        private int _t725_ultcierreempresa_IAP;
        public int t725_ultcierreempresa_IAP
        {
            get { return _t725_ultcierreempresa_IAP; }
            set { _t725_ultcierreempresa_IAP = value; }
        }

        #endregion
        public PARAMETRIZACIONSUPER()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static PARAMETRIZACIONSUPER GetDatos(SqlTransaction tr)
        {
            PARAMETRIZACIONSUPER o = new PARAMETRIZACIONSUPER();
            SqlDataReader dr = IB.Services.Super.DAL.PARAMETRIZACIONSUPER.Select(tr);
            if (dr.Read())
            {
                if (dr["t725_ultcierreempresa_IAP"] != DBNull.Value)
                    o.t725_ultcierreempresa_IAP = int.Parse(dr["t725_ultcierreempresa_IAP"].ToString());
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de PARAMETRIZACIONSUPER"));
            }
            dr.Close();
            dr.Dispose();
            return o;
        }
    }
}
