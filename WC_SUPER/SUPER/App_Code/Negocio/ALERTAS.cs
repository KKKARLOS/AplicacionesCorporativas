using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : ALERTAS
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T830_PSNALERTAS
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	23/07/2012 12:36:46	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class ALERTAS
    {
        #region Propiedades y Atributos


        #endregion

        #region Constructor

        public ALERTAS()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        public static string Grabar(string strDatos)
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
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión. " + ex.Message));
            }
            #endregion

            try
            {
                #region Alertas globales
                if (strDatos != "") //No se ha modificado nada 
                {
                    string[] aDatos = Regex.Split(strDatos, "///");
                    foreach (string oDatos in aDatos)
                    {
                        if (oDatos == "") continue;
                        string[] aValores = Regex.Split(oDatos, "##");

                        ///aValores[0] = ID
                        ///aValores[1] = ALERTAS ACTUALES
                        ///aValores[2] = ALERTAS FUTURAS

                        if (aValores[1] != "") SUPER.Capa_Datos.ALERTAS.UpdatePSNAlertas(tr, byte.Parse(aValores[0]), aValores[1]);
                        if (aValores[2] != "") SUPER.Capa_Datos.ALERTAS.UpdateNodoAlertas(tr, byte.Parse(aValores[0]), aValores[2]);
                    }
                }
                #endregion
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al actualizar las alertas de nodo o proyecto.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            return "OK@#@";
        }
        public static string Catalogo()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                #region Cabecera tabla HTML
                sb.Append(@"<table id='tblDatos' style='width:660px;' cellpadding='0' cellspacing='0' border='0' mantenimiento='1'>
                        <colgroup>
                            <col style='width:30px;' />
			                <col style='width:430px;' />
			                <col style='width:100px;' />
							<col style='width:100px;' />
                        </colgroup>");

                #endregion

                SqlDataReader dr = SUPER.Capa_Datos.ALERTAS.Catalogo(null);
                string sCombo = "<select class='combo' style='width:100px;' onchange=\"aG(this);\">";
                sCombo += "<option value='' selected></option>";
                sCombo += "<option value='A'>Activar</option>";
                sCombo += "<option value='D'>Desactivar</option>";
                sCombo += "</select>";
                while (dr.Read())
                {
                    sb.Append("<tr bd='' id='" + dr["t820_idalerta"].ToString() + "' ");
                    sb.Append(" style='height:20px;'>");
                    sb.Append("<td style='text-align:right;padding-right:5px'>" + dr["t820_idalerta"].ToString() + "</td>");
                    sb.Append("<td style='padding-left:3px;'><nobr class='NBR W430' onmouseover='TTip(event)'>" + dr["t820_denominacion"].ToString() + "</nobr></td>");

                    sb.Append("<td style='text-align:center;'>" + sCombo + "</td>");
                    sb.Append("<td style='text-align:center;'>" + sCombo + "</td>");

                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();

                sb.Append("</table>");
                return "OK@#@" + sb.ToString();
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al obtener las alertas", ex);
            }
        }

        #endregion
    }
}
