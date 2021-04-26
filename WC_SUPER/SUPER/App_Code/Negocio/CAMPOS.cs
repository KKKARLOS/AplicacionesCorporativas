using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Datos;
//Para el ArrayList
using System.Collections;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : CAMPOS
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T290_CAMPOS
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	23/07/2012 12:36:46	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class CAMPOS
    {

        #region Constructor

        public CAMPOS()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos


        public static string Grabar(string strDatos)
        {
            string sElementosInsertados = "";
            int nAux = 0;
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
                if (strDatos != "") //No se ha modificado nada 
                {
					string[] aCampo = Regex.Split(strDatos, "///");
					//Primero se hacen las deletes para evitar errores por denominaciones duplicadas.
					foreach (string oCampo in aCampo)
					{
						if (oCampo == "") continue;
						string[] aValores = Regex.Split(oCampo, "##");
						//0. Opcion BD. "I", "D"
						//1. ID Campo
						//2. Descripcion
                        //3. t291_idtipodato
                        //4. Ambito
                        //5  t001_ficepi_owner
                        //6  t305_idproyectosubnodo
                        //7  t295_uidequipo

						if (aValores[0] != "D") continue;
						SUPER.Capa_Datos.CAMPOS.Delete(tr, int.Parse(aValores[1]));
					}

					foreach (string oCampo in aCampo)
					{
						if (oCampo == "") continue;
						string[] aValores = Regex.Split(oCampo, "##");
						//0. Opcion BD. "I", "D"
						//1. t290_idcampo
						//2. t290_denominación
						//3. t291_idtipodato
                        //4. Ambito
                        //5  t001_ficepi_owner
                        //6  t305_idproyectosubnodo
                        //7  t295_uidequipo

						switch (aValores[0])
						{
							case "I":

								nAux = SUPER.Capa_Datos.CAMPOS.Insert(tr, 
																Utilidades.unescape(aValores[2]),
                                                                int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()),
																aValores[3],
                                                                int.Parse(aValores[4]),
                                                                (aValores[5] == "0") ? null : (int?)int.Parse(aValores[5]),
                                                                (aValores[6] == "0") ? null : (int?)int.Parse(aValores[6]),
                                                                (aValores[7] == "") ?  null : Utilidades.unescape(aValores[7])
																);
								if (sElementosInsertados == "") sElementosInsertados = nAux.ToString();
								else sElementosInsertados += "//" + nAux.ToString();
								break;
						}
					}				
                }
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al actualizar los campos ligados a tarea.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            sResul = sElementosInsertados;
            return "OK@#@" + sResul;
        }

        public static string Catalogo(int iFicepiEntrada, int codAmbito, string codTipo, int t305_idproyectosubnodo, ArrayList lstCamposPT)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                #region Cabecera tabla HTML
                sb.Append(@"<table id='tblDatos' class='texto' style='width:900px;' mantenimiento='1'>
                        <colgroup>
			                <col style='width:480px;' />
                            <col style='width:80px;' />
			                <col style='width:70px;' />
							<col style='width:270px;' />
                        </colgroup>");

                #endregion
                string strAmbito="";
                string strTitulo = "";
                SqlDataReader dr = SUPER.Capa_Datos.CAMPOS.Catalogo(iFicepiEntrada, codAmbito, codTipo, t305_idproyectosubnodo, lstCamposPT);
                while (dr.Read())
                {
                    sb.Append("<tr bd='' id='" + dr["IDENTIFICADOR"].ToString() + "'  onclick='mm(event);' ");
                    sb.Append("creador='" + dr["t001_idficepi_creador"].ToString() + "' ");
                    sb.Append("owner='" + dr["t001_ficepi_owner"].ToString() + "' ");
                    sb.Append(" style='height:20px;'>");
                    //sb.Append("<td><img src='../../../../../images/imgFN.gif'></td>");
                    sb.Append("<td><nobr class='NBR W480' onmouseover='TTip(event)'>" + dr["DENOMINACION"].ToString() + "</nobr></td>");
                    
                    strTitulo = "";
                    switch (dr["codAmbito"].ToString())
                    {
                        case ("0"):
                            strAmbito="Empresarial";
                            break;
                        case ("1"):
                            strAmbito="Privado";
                            strTitulo = dr["profesional_owner"].ToString();                            
                            break;
                        case ("2"):
                            strAmbito="Proyecto";
                            strTitulo = dr["denominacion_proyecto"].ToString();
                            break;
                        case ("3"):
                            strAmbito="Cliente";
                            strTitulo = dr["denominacion_cliente"].ToString();
                            break;
                        case ("4"):
                            strAmbito = "C.R.";
                            strTitulo = dr["denominacion_nodo"].ToString();
                            break;
                        case ("5"):
                            strAmbito = "Equipo";
                            strTitulo = dr["denominacion_equipo"].ToString();
                            break;
                    }
                    sb.Append("<td><label title='" + strTitulo + "'>" + strAmbito + "</label></td>");
                    sb.Append("<td>" + dr["t291_denominacion"].ToString() + "</td>");
                    sb.Append("<td><nobr class='NBR W270' onmouseover='TTip(event)'>" + dr["ProfesionalCreador"].ToString() + "</nobr></td>");
                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();

                sb.Append("</table>");
                return "OK@#@" + sb.ToString();
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al obtener los campos a asignar ligados a la tarea", ex);
            }
        }

         #endregion
    }
}
