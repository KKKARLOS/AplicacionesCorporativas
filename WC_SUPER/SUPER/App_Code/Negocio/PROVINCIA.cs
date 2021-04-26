using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SUPER.Capa_Datos;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.UI.WebControls;

namespace SUPER.Capa_Negocio
{
	public partial class PROVINCIA
	{
		#region Metodos

        public static string Grabar(string strProvincias)
        {
            string sResul = "";
            SqlConnection oConn = null;
            SqlTransaction tr;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);

                string sError = Errores.mostrarError("Error al abrir la conexión", ex);
                string[] aError = Regex.Split(sError, "@#@");
                throw new Exception(Utilidades.escape(aError[0]), ex);
            }
            #endregion
            try
            {
                #region Datos Provincias
                if (strProvincias != "")//No se ha modificado nada de la pestaña de Figuras
                {
                    string[] aProvincias = Regex.Split(strProvincias, "///");
                    foreach (string oProvincia in aProvincias)
                    {
                        if (oProvincia == "") continue;
                        string[] aValores = Regex.Split(oProvincia, "##");
                        ///aValores[0] = id provincia
                        ///aValores[1] = id zona

                        SUPER.DAL.PROVINCIA.UpdateZona(tr, int.Parse(aValores[0]), int.Parse(aValores[1]));
                    }
                }

                #endregion

                Conexion.CommitTransaccion(tr);
                sResul = "";
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                //string sError = Errores.mostrarError("Error al grabar los datos de la provincia", ex);
                //string[] aError = Regex.Split(sError, "@#@");
                //throw new Exception(Utilidades.escape(aError[0]), ex);
                throw ex;
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
            return sResul;
        }

		#endregion

	}
}
