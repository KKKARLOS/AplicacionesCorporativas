using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CR2I.Capa_Datos;
using System.Collections;

namespace CR2I.Capa_Negocio
{
    /// <summary>
    /// Summary description for TELERREUNION_FICEPI
    /// </summary>
    public class TELERREUNION_FICEPI
    {
        #region Propiedades y Atributos

        private int _t149_idTL;
        public int t149_idTL
        {
            get { return _t149_idTL; }
            set { _t149_idTL = value; }
        }

        private int _t001_idficepi;
        public int t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        private string _t150_figura;
        public string t150_figura
        {
            get { return _t150_figura; }
            set { _t150_figura = value; }
        }
        #endregion

        #region Constructor

        public TELERREUNION_FICEPI()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        public static void Insertar(SqlTransaction tr, int t149_idTL, int t001_idficepi, string @t150_figura)
        {
            if (tr == null)
                SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion, "CR2_TELERREUNION_FICEPI_I", t149_idTL, t001_idficepi, @t150_figura);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "CR2_TELERREUNION_FICEPI_I", t149_idTL, t001_idficepi, @t150_figura);
        }

        public static SqlDataReader SelectBy_IDRESERVA(SqlTransaction tr, int t149_idTL, string sAccion)
        {
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, "CR2_TELERREUNION_FICEPI_SBy_IDRESERVA", sAccion, t149_idTL);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "CR2_TELERREUNION_FICEPI_SBy_IDRESERVA", sAccion, t149_idTL);
        }

        public static void DeleteByT149_IDTL(SqlTransaction tr, int t149_idTL)
        {
            if (tr == null)
                SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion, "CR2_TELERREUNION_FICEPI_DByT149_IDTL", t149_idTL);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "CR2_TELERREUNION_FICEPI_DByT149_IDTL", t149_idTL);
        }

        public static bool AsisteATelerreunion(int t149_idTL, int t001_idficepi)
        {
            object oResul = SqlHelper.ExecuteScalar(Utilidades.CadenaConexion,
                "CR2_ASISTETELERREUNION", t149_idTL, t001_idficepi);

            return (Convert.ToInt32(oResul) > 0) ? true : false;
        }

        #endregion

    }
}
