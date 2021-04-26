using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GASVI.DAL;
using System.Text;
using System.Text.RegularExpressions;

namespace GASVI.BLL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GASVI
	/// Class	 : FICEPIAVISOS
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T775_FICEPIAVISOSGASVI
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/04/2009 11:29:20	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FICEPIAVISOS
	{
		#region Propiedades y Atributos

		private int _t774_idaviso;
		public int t774_idaviso
		{
			get {return _t774_idaviso;}
			set { _t774_idaviso = value ;}
		}

		private int _t001_idficepi;
		public int t001_idficepi
		{
			get {return _t001_idficepi;}
			set { _t001_idficepi = value ;}
		}
		#endregion

		#region Constructor

        public FICEPIAVISOS()
        {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

        public static bool VerSiHay(int t001_idficepi)
        {
            bool bHay = false;
            int iHay = DAL.FICEPIAVISOS.CountByFicepi(null, t001_idficepi);
            if (iHay > 0) bHay = true;
            return bHay;
        }

        public static void EliminarAviso(string sIdAviso, int t001_idficepi)
        {
            string sResul = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
                DAL.FICEPIAVISOS.Insert(tr, int.Parse(sIdAviso), t001_idficepi);

                int iNumAvisos = DAL.FICEPIAVISOS.CountByFicepi(tr, t001_idficepi);
                Conexion.CommitTransaccion(tr);
                if (iNumAvisos == 0) HttpContext.Current.Session["GVT_HAYAVISOS"] = "0";
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al eliminar el aviso " + sIdAviso, ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
        }

        public static string ObtenerAvisos(int t001_idficepi)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos3'>");
            SqlDataReader dr = DAL.FICEPIAVISOS.ObtenerAvisos(t001_idficepi);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t774_idaviso"].ToString() + "' titulo='" + dr["t774_titulo"].ToString() + "'");
                string sBorrable = ((bool)dr["t774_borrable"]) ? "1" : "0";
                sb.Append(" texto='" + dr["t774_texto"].ToString() + "' borrable='" + sBorrable + "'><BR>");
                sb.Append("<td></td><BR></tr>");
            }
            sb.Append("</table>");
            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

		#endregion
	}
}
