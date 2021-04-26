using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CR2I.Capa_Datos;
using System.Collections;

namespace CR2I.Capa_Negocio
{
    /// <summary>
    /// Summary description for LicenciaWebex
    /// </summary>
    public class LicenciaWebex
    {
        #region Atributos privados

        private int _t148_idlicenciawebex;
        private string _t148_denominacion;

        #endregion

        #region Propiedades públicas

        public int t148_idlicenciawebex
        {
            get { return _t148_idlicenciawebex; }
            set { _t148_idlicenciawebex = value; }
        }
        public string t148_denominacion
        {
            get { return _t148_denominacion; }
            set { _t148_denominacion = value; }
        }

        #endregion

        #region Constructores
        public LicenciaWebex()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public LicenciaWebex(int t148_idlicenciawebex, 
            string t148_denominacion)
		{
            this.t148_idlicenciawebex = t148_idlicenciawebex;
            this.t148_denominacion = t148_denominacion;
        }

        #endregion

        #region	Métodos públicos

        public SqlDataReader ObtenerLicenciasWebex(bool bSoloActivos)
        {
            return SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion,
                "CR2_LICENCIAWEBEX_CAT", bSoloActivos);
        }

        public static ArrayList ListaLicenciasWebex()
        {
            if (HttpContext.Current.Cache["cr2_Telerreuniones"] == null)
            {
                ArrayList aTablaLW = new ArrayList();

                LicenciaWebex objLW = new LicenciaWebex();
                SqlDataReader dr = objLW.ObtenerLicenciasWebex(true);
                LicenciaWebex objLWAux;
                while (dr.Read())
                {
                    objLWAux = new LicenciaWebex((int)dr["CODIGO"], dr["DESCRIPCION"].ToString());
                    aTablaLW.Add(objLWAux);
                }
                dr.Close();
                dr.Dispose();

                HttpContext.Current.Cache.Insert("cr2_Telerreuniones", aTablaLW, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
                return aTablaLW;
            }
            else
            {
                return (ArrayList)HttpContext.Current.Cache["cr2_Telerreuniones"];
            }
        }

        public static LicenciaWebex Obtener(SqlTransaction tr, int t148_idlicenciawebex)
        {
            LicenciaWebex o = new LicenciaWebex();

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion,
                    "CR2_LICENCIAWEBEX_S", t148_idlicenciawebex);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "CR2_LICENCIAWEBEX_S", t148_idlicenciawebex);

            if (dr.Read())
            {
                if (dr["t148_idlicenciawebex"] != DBNull.Value)
                    o.t148_idlicenciawebex = int.Parse(dr["t148_idlicenciawebex"].ToString());
                if (dr["t148_denominacion"] != DBNull.Value)
                    o.t148_denominacion = (string)dr["t148_denominacion"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de LicenciaWebex"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static DataSet ObtenerReservas(int t148_idlicenciawebex, DateTime strFechaIni, DateTime strFechaFin)
        {
            return SqlHelper.ExecuteDataset(Utilidades.CadenaConexion,
                "CR2_TELERREUNION_SRES", t148_idlicenciawebex, strFechaIni, strFechaFin);
        }

        public static SqlDataReader ObtenerDisponibilidad(SqlTransaction tr, int t148_idlicenciawebex, DateTime strFechaIni, DateTime strFechaFin, int t149_idTL)
        {
            return SqlHelper.ExecuteSqlDataReaderTransaccion(tr,
                "CR2_DISPONIBILIDAD_LW", t148_idlicenciawebex, strFechaIni, strFechaFin, t149_idTL);
        }

        #endregion
    }
}