using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Datos;
using SUPER.BLL;
using SUPER.Capa_Negocio;

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de CuentasCVT
    /// </summary>
    public partial class CuentasCVT
    {
        #region Propiedades y Atributos

       

        #endregion

        #region Constructor

        public CuentasCVT()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        #endregion

        #region Metodos
        /// <summary>
        /// Crea una tabla HTML con la relación de cuentas
        /// </summary>
        /// <returns></returns>
        public static string Catalogo(string t811_denominacion, Nullable<byte> t811_estado, string sTipoBusqueda)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblCatalogo' style='width:700px;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:20px;' />");
            sb.Append(" <col style='width:300px;' />");
            sb.Append(" <col style='width:330px;' />");
            sb.Append(" <col style='width:50px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");


            SqlDataReader dr = DAL.CuentasCVT.Catalogo(null, t811_denominacion, t811_estado, sTipoBusqueda);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T811_IDCUENTA"].ToString() + "' chk='" + dr["T811_ESTADO"] + "' segmento='" + dr["T484_IDSEGMENTO"].ToString() + "' bd='N'>");
                sb.Append("<td></td>");
                sb.Append("<td>" + dr["T811_DENOMINACION"].ToString() + "</td>");
                sb.Append("<td>" + dr["T484_DENOMINACION"].ToString() + "</td>");
                sb.Append("<td style='text-align:center;'></td>");
                sb.Append("</tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtiene la union de cuentasCVT validadas y clientes (códigos negativos)
        /// </summary>
        /// <param name="t811_denominacion"></param>
        /// <param name="t811_estado"></param>
        /// <param name="sTipoBusqueda"></param>
        /// <returns></returns>
        public static string CatalogoSimple(string t811_denominacion, Nullable<byte> t811_estado, string sTipoBusqueda)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='MANO' style='width:450px;' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<tbody>");


            SqlDataReader dr = DAL.CuentasCVT.CuentasMasClientes(null, t811_denominacion, t811_estado, sTipoBusqueda);
            while (dr.Read())
            {
                sb.Append("<tr id=" + dr["codigo"].ToString() + " idSeg=" + dr["T484_IDSEGMENTO"].ToString());
                sb.Append(" onclick='ms(this)'>");
                sb.Append("<td><nobr class='NBR W430'>" + dr["denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();
        }

        /// <summary>
        /// Devuelve un DataReader con las cuentas cuya denominación empieza
        /// por el contenido del parametro sDen
        /// </summary>
        /// <param name="sDen"></param>
        /// <param name="idSegmento"></param>
        /// <returns></returns>
        public static SqlDataReader Catalogo(string sDen, Nullable<int> idSegmento)
        {
            return SUPER.DAL.CuentasCVT.Catalogo(null, sDen, true, idSegmento, 2, 0);
        }

        public static SqlDataReader CatalogoCuentaCVT(string sDen)//, Nullable<int> origen
        {
            return SUPER.DAL.CuentasCVT.CatalogoCuentaCVT(null, sDen);//, origen
        }

        public static string Grabar(string strDatos)
        {
            string sDen = "";
            string sAccion = "";
            #region Inicio Transacción

            SqlConnection oConn;
            SqlTransaction tr;
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
                #region Grabar

                string sIdCuenta = "";
                string[] aCuenta = Regex.Split(strDatos, "@cuenta@");
                foreach (string sCuenta in aCuenta)
                {
                    if (sCuenta != "")
                    {
                        string[] aDatosCuenta = Regex.Split(sCuenta, "@dato@");
                        sAccion = aDatosCuenta[0];
                        sDen = aDatosCuenta[2];
                        switch (sAccion)
                        {
                            case "I":
                                //aDatosCuenta[1]-->IDCuenta
                                //aDatosCuenta[2]-->Denominacion
                                //aDatosCuenta[3]-->IdSegemento
                                //aDatosCuenta[4]-->Estado
                                sIdCuenta += DAL.CuentasCVT.Insert(tr, aDatosCuenta[2].ToString(), int.Parse(aDatosCuenta[3].ToString()), (aDatosCuenta[4].ToString() == "true") ? byte.Parse("1") : byte.Parse("0"), null) + "//";
                                break;
                            case "U":
                                DAL.CuentasCVT.Update(tr, int.Parse(aDatosCuenta[1].ToString()), aDatosCuenta[2].ToString(), int.Parse(aDatosCuenta[3].ToString()), (aDatosCuenta[4].ToString() == "true") ? byte.Parse("1") : byte.Parse("0"));
                                break;
                            case "D":
                                //sDenominacionDelete = aDatosCuenta[2];
                                DAL.CuentasCVT.Delete(tr, int.Parse(aDatosCuenta[1].ToString()));
                                break;
                        }
                    }
                }
                
                #endregion  
                Conexion.CommitTransaccion(tr);
                return "OK@#@" + sIdCuenta;

            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                if (Errores.EsErrorIntegridad(ex))
                {
                    if (sAccion == "D")
                        throw new Exception("ErrorControlado##EC##No se puede eliminar la cuenta \"" + sDen + "\" por tener elementos relacionados.");
                    else
                        throw new Exception("ErrorControlado##EC##No se puede grabar la cuenta \"" + sDen + "\" porque ya existe esa denominación.");
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
            
            
        }
                
        public static string SectorSegmentoHTML()
        {
            SqlDataReader dr = DAL.CuentasCVT.SectorSegmento(null);
            StringBuilder sb = new StringBuilder();
            string aux = "";
            int nRegistros = 0;

            sb.Append("<select name='cboSector' id='cboSector' class='combo' style='width:300px;' onChange=\"mfa(this.parentNode.parentNode,'U')\">");
            sb.Append("<option id='' value=''></option>");
            while (dr.Read())
            {
                if (aux == "")//Primer registro
                {
                    aux = dr["T483_IDSECTOR"].ToString();
                    sb.Append("<optgroup label=\"" + dr["T483_DENOMINACION"].ToString() + "\">");
                    sb.Append("<option value=\"" + dr["T484_IDSEGMENTO"].ToString() + "\">" + dr["T484_DENOMINACION"].ToString() + "</option>");
                    nRegistros++;
                }
                else if (dr["T483_IDSECTOR"].ToString() == aux) //Si es el mismo id se añade un registro (option) al combo
                {
                    //segunda fila o posterior
                    aux = dr["T483_IDSECTOR"].ToString();
                    sb.Append("<option value=\"" + dr["T484_IDSEGMENTO"].ToString() + "\">" + dr["T484_DENOMINACION"].ToString() + "</option>");
                }
                else //Distinto id distinto <optgroup>
                {
                    //Fin fila
                    sb.Append("</optgroup>");
                    //Vuelvo a montar la siguiente opcion del combo
                    sb.Append("<optgroup label=\"" + dr["T483_DENOMINACION"].ToString() + "\">");
                    //Primera fila
                    sb.Append("<option value=\"" + dr["T484_IDSEGMENTO"].ToString() + "\">" + dr["T484_DENOMINACION"].ToString() + "</option>");
                    aux = dr["T483_IDSECTOR"].ToString();
                }
            }
            if (nRegistros>0)
                sb.Append("</optgroup>");
            sb.Append("</select>");
            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

        public static int Insert(SqlTransaction tr, string denCuenta, int idSegmento)
        {
            return SUPER.DAL.CuentasCVT.Insert(tr, denCuenta, idSegmento, 0, null);
        }
        public static int Insert(SqlTransaction tr, string denCuenta, Nullable<int> idSegmento, byte bEstado, Nullable<int> t302_idcliente)
        {
            return SUPER.DAL.CuentasCVT.Insert(tr, denCuenta, idSegmento, bEstado, t302_idcliente);
        }
        /// <summary>
        /// dado un segmento y denominación de cuenta devuelve el código de cuenta que le corresponde
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="sDenCuenta"></param>
        /// <param name="idSegmento"></param>
        /// <returns></returns>

        /// <summary>
        /// Obtiene los Profesionales Asociados asociados a un cliente no ibermatica
        /// </summary>
        /// <returns></returns>
        public static string ProfesionalesAsociados(int t811_idcuenta)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            sb.Append("<table id='tblProf' style='width:450px;' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<tbody>");

            SqlDataReader dr = DAL.CuentasCVT.ProfAsociados(null, t811_idcuenta);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "'>");
                sb.Append("<td><nobr class='NBR W430'>" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
                i++;
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();

        }
        public static string ElementosAsociadoAReasignar(int t019_idcodtitulo)
        {
            return Curriculum.ElementosAsociadoAReasignar(DAL.CuentasCVT.ElementosAsociadoAReasignar(null, t019_idcodtitulo));
        }

        /// <summary>
        /// Busca en cuentasCVT por denominación exacta
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t811_denominacion"></param>
        /// <returns>0 si no existe, en caso contrario el código de la cuenta</returns>
        public static int ObtenerPorNombre(SqlTransaction tr, string t811_denominacion)
        {
            int iRes = 0;

            SqlDataReader dr = SUPER.DAL.CuentasCVT.ObtenerPorNombre(tr, t811_denominacion);
            if (dr.Read())
                iRes = int.Parse(dr["t811_idcuenta"].ToString());
            dr.Close();
            dr.Dispose();

            return iRes;
        }
        
        public static int GetSegmento(SqlTransaction tr, int t811_idcuenta)
        {
            int iRes = -1;
            SqlDataReader dr = SUPER.DAL.CuentasCVT.Datos(tr, t811_idcuenta);
            if (dr.Read())
            {
                if (dr["t484_idsegmento"].ToString() != "")
                    iRes = int.Parse(dr["t484_idsegmento"].ToString());
            }
            dr.Close();
            dr.Dispose();

            return iRes;
        }
        #endregion
    }
}