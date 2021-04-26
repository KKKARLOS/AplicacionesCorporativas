using System.Data.SqlClient;
using System.Text;
using System;
using System.Text.RegularExpressions;
using SUPER.DAL;
using SUPER.Capa_Negocio;

namespace SUPER.BLL
{
    public partial class AreaConocimientoCTV
    {
        #region Metodos

        /// <summary>
        /// bActiva a true muestra únicamente las activas. Si es nulo, muestra todas
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="bActiva"></param>
        /// <returns></returns>
        public static string CatalogoAreaConocimientoSec(int tipo, Nullable<bool> bActiva)
        {            
            SqlDataReader dr = null;
            switch (tipo)
            {
                case 1:
                    dr = DAL.AreaConocimientoCTV.CatalogoConSec(null, bActiva);                   
                     break;

                case 2:
                     dr = DAL.AreaConocimientoCTV.CatalogoConTecno(null, bActiva);
                     break;
                default:
                    break;
            }
                         
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblCatalogo' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:15px;'/><col style='width:485px;'/></colgroup>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["IDCONOCIMIENTO"].ToString() + "' bd='N' ");
                if ((bool)dr["ACTIVA"])
                    sb.Append("act=1 ");
                else
                    sb.Append("act=0 ");
                sb.Append("><td></td>");
                sb.Append("<td>" + dr["DENOMINACION"] + "</td><td></td>");
                sb.Append("</tr>");


            }
            sb.Append("</table>");
            dr.Close();
            dr.Dispose();
            return "OK@#@" + sb.ToString() ;
        }

        public static string Grabar(string strDatos)
        {
            int nAux = 0;
            string sDenominacionDelete = "";
            string sAccion = "";
            SqlConnection oConn = null;
            SqlTransaction tr ;
            string sElementosInsertados = "";
            string sResul = "";
            bool bActiva=true;

            #region Conexión
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                throw (new Exception("Error al abrir la conexion", ex));
            }
            #endregion

            try
            {
                string[] aConocimiento = Regex.Split(strDatos, "///");
                foreach (string oConocimiento in aConocimiento)
                {
                    string[] aValores = Regex.Split(oConocimiento, "##");
                    sAccion = aValores[0];
                    sDenominacionDelete = "";
                    //0. Opcion BD. "I", "U", "D"
                    //1. ID Conocimiento                                      
                    //2. Tipo de conocimiento
                    //3. Denominación
                    //4. Activo
                    switch (aValores[0])
                    {
                        case "I":                            
                            if (aValores[4]=="1")bActiva=true;
                            else bActiva=false;
                            switch (aValores[2])
                            {
                                case "1": //1- Sectorial
                                    nAux = DAL.AreaConocimientoCTV.InsertConSec(tr, Utilidades.unescape(aValores[3]),bActiva);
                                    if (sElementosInsertados == "") sElementosInsertados = nAux.ToString();
                                    else sElementosInsertados += "//" + nAux.ToString();
                                    break;
                                case "2": //2- Tecnológico
                                    nAux = DAL.AreaConocimientoCTV.InsertConTec(tr, Utilidades.unescape(aValores[3]),bActiva);
                                    if (sElementosInsertados == "") sElementosInsertados = nAux.ToString();
                                    else sElementosInsertados += "//" + nAux.ToString();
                                    break;
                                default:
                                    break;
                            }                            
                            break;
                        case "U":
                            if (aValores[4]=="1")bActiva=true;
                            else bActiva=false;
                            switch (aValores[2])
                            {
                                case "1": //1- Sectorial                                    
                                    DAL.AreaConocimientoCTV.UpdateConSec(tr, int.Parse(aValores[1]), Utilidades.unescape(aValores[3]),bActiva);                                    
                                    break;
                                case "2": //2- Tecnológico
                                    DAL.AreaConocimientoCTV.UpdateConTec(tr, int.Parse(aValores[1]), Utilidades.unescape(aValores[3]),bActiva);
                                    break;                                    
                            }
                               
                            break;
                        case "D":
                            sDenominacionDelete = aValores[3];
                            switch (aValores[2])
                            {
                                case "1": //1- Sectorial                                    
                                    DAL.AreaConocimientoCTV.DeleteConSec(tr, short.Parse(aValores[1]));
                                    break;
                                case "2": //2- Tecnológico
                                    DAL.AreaConocimientoCTV.DeleteConTec(tr, short.Parse(aValores[1]));
                                    break;
                            }

                            break;
                            
                    }
                }

                Conexion.CommitTransaccion(tr);
                sResul = sElementosInsertados;

            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                if (Errores.EsErrorIntegridad(ex) && sAccion == "D")
                {
                    throw new Exception("ErrorControlado##EC##No se puede eliminar el area de conocimiento \"" + sDenominacionDelete + "\" por tener elementos relacionados.");
                }
                else
                {
                    throw ex;
                }
            }

            finally
            {
                Conexion.Cerrar(oConn);

            }

            return sResul;
        }

        public static string CatalogoAreas(string tipo, string cono)
        {
            SqlDataReader dr = null;
            switch (tipo)
            {
                case "ACS":
                    dr = DAL.AreaConocimientoCTV.CatalogoAreasSec(null,cono);
                    break;

                case "ACT":
                    dr = DAL.AreaConocimientoCTV.CatalogoAreasTecno(null,cono);
                    break;
                default:
                    break;
            }

            StringBuilder sbT = new StringBuilder();
            StringBuilder sb = new StringBuilder();

            sbT.Append("<table id='tblDatosTodos' class='texto MAM' style='width:300px'>");
            sbT.Append("<colgroup><col style='width:20px;'/><col style='width:280px;'/></colgroup>");

            sb.Append("<table id='tblDatos' class='texto MM' style='width:300px'>");
            sb.Append("<colgroup><col style='width:20px;'/><col style='width:280px;'/></colgroup>");

            while (dr.Read())
            {

                sbT.Append("<tr id='" + dr["IDCONOCIMIENTO"].ToString() + "' style='height:16px;' onclick='mm(event);' ondblclick='insertarItem(this)' onmousedown='DD(event);'>");//onclick='ms(this);cargarDatosConsultor(this);'
                sbT.Append("<td></td>");
                sbT.Append("<td>" + dr["DENOMINACION"] + "</td>");
                sbT.Append("</tr>");
                
                if (dr["Tipo"].ToString() == "0")
                {
                    sb.Append("<tr id='" + dr["IDCONOCIMIENTO"].ToString() + "' style='height:16px;' onclick='mm(event);' onmousedown='DD(event);'>");
                    sb.Append("<td></td>");
                    sb.Append("<td>" + dr["DENOMINACION"] + "</td>");
                    sb.Append("</tr>");
                }


            }
            sbT.Append("</table>");
            sb.Append("</table>");
            dr.Close();
            dr.Dispose();
            return sbT.ToString() + "@#@" + sb.ToString();
        }

        #endregion
    }
}
