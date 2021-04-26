using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SUPER.Capa_Datos;
using SUPER.DAL;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace SUPER.Capa_Negocio
{
	public partial class PAIS
	{
        #region Propiedades y Atributos

        protected int _t172_idpais;
        public int t172_idpais
        {
            get { return _t172_idpais; }
            set { _t172_idpais = value; }
        }
        protected string _t172_denominacion;
        public string t172_denominacion
        {
            get { return _t172_denominacion; }
            set { _t172_denominacion = value; }
        }
        #endregion

        #region Constructores

        public PAIS()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados segÃºn el tipo de dato.
        }

        public PAIS(int t172_idpais, string t172_denominacion)
        {
            this.t172_idpais = t172_idpais;
            this.t172_denominacion = t172_denominacion;
        }
        #endregion

		#region Metodos

        public static void CatalogoPaisesGesCache(DropDownList cboPaisGes, string defValue)
        {
            cboPaisGes.DataValueField = "identificador";
            cboPaisGes.DataTextField = "denominacion";
            DataSet ds = null;

            if (HttpContext.Current.Cache["Lista_PaisesGes"] == null)
            {
				SqlParameter[] aParam = new SqlParameter[0];
                ds = SqlHelper.ExecuteDataset("SUP_PAIS_C", aParam);
                HttpContext.Current.Cache.Insert("Lista_PaisesGes", ds, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
            }
            else
                ds = (DataSet)HttpContext.Current.Cache["Lista_PaisesGes"];

            cboPaisGes.DataSource = ds;
            cboPaisGes.DataBind();
            cboPaisGes.Items.Insert(0, new ListItem("", defValue));
        }
        public static List<PAIS> ListaPaisesGes()
        {
            if (HttpContext.Current.Cache["Lista_PaisesGes"] == null)
            {
                List<PAIS> oLista = new List<PAIS>();
                SqlDataReader dr = SUPER.DAL.PAIS.Catalogo();
                PAIS oElem;
                while (dr.Read())
                {
                    oElem = new PAIS(int.Parse(dr["identificador"].ToString()), dr["denominacion"].ToString());
                    oLista.Add(oElem);
                }
                dr.Close();
                dr.Dispose();

                HttpContext.Current.Cache.Insert("Lista_PaisesGes", oLista, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
                return oLista;
            }
            else
            {
                return (List<PAIS>)HttpContext.Current.Cache["Lista_PaisesGes"];
            }
        }

        public static List<PAIS> ListaPaisesFis()
        {
            if (HttpContext.Current.Cache["Lista_PaisesFis"] == null)
            {
                List<PAIS> oLista = new List<PAIS>();
                SqlDataReader dr = SUPER.DAL.PAIS.Catalogo();
                PAIS oElem;
                while (dr.Read())
                {
                    oElem = new PAIS(int.Parse(dr["identificador"].ToString()), dr["denominacion"].ToString());
                    oLista.Add(oElem);
                }
                dr.Close();
                dr.Dispose();

                HttpContext.Current.Cache.Insert("Lista_PaisesFis", oLista, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
                return oLista;
            }
            else
            {
                return (List<PAIS>)HttpContext.Current.Cache["Lista_PaisesFis"];
            }
        }


        public static void CatalogoPaisesFisCache(DropDownList cboPaisFis, string defValue)
        {
            cboPaisFis.DataValueField = "identificador";
            cboPaisFis.DataTextField = "denominacion";
            DataSet ds = null;

            if (HttpContext.Current.Cache["Lista_PaisesFis"] == null)
            {
                SqlParameter[] aParam = new SqlParameter[0];
                ds = SqlHelper.ExecuteDataset("SUP_PAIS_C", aParam);
                HttpContext.Current.Cache.Insert("Lista_PaisesFis", ds, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
            }
            else
                ds = (DataSet)HttpContext.Current.Cache["Lista_PaisesFis"];

            cboPaisFis.DataSource = ds;
            cboPaisFis.DataBind();
            cboPaisFis.Items.Insert(0, new ListItem("", defValue));
        }
        public static string cargarProvinciasGtonPais(string sID)
        {
            string sResul = "";
            StringBuilder sb = new StringBuilder();
            //try
            //{
                SqlDataReader dr = SUPER.DAL.PAIS.Provincias(int.Parse(sID)); //Mostrar todos todos las provincias relacionadas a un país determinado

                sb.Append("<table id='tblDatos' class='texto' style='width:840px;'>");
                sb.Append("<colgroup>");
                sb.Append("    <col style='width:20px;' />");
                sb.Append("    <col style='width:410px;' />");
                sb.Append("    <col style='width:410px;' />");
                sb.Append("</colgroup>");
                sb.Append("<tbody>");

                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["identificador"].ToString() + "' bd=''");
                    sb.Append("zona='" + dr["cod_zona"].ToString() + "' ");
                    sb.Append("onclick='mm(event);' ");
                    sb.Append("style='height:18px' >");
                    sb.Append("<td><img src='" + HttpContext.Current.Session["strServer"].ToString() + "images/imgFN.gif'></td>");
                    sb.Append("<td style='padding-left:3px;'><nobr class='NBR'>" + dr["denominacion"].ToString() + "</nobr></td>");
                    sb.Append("<td style='padding-left:3px;'><nobr class='NBR'>" + dr["zona"].ToString() + "</nobr></td>");
                    sb.Append("</tr>");
                }

                dr.Close();
                dr.Dispose();
                sb.Append("</tbody>");
                sb.Append("</table>");

                //sResul = "OK@#@" + sb.ToString(); ;
                sResul = sb.ToString(); ;
            //}
            //catch (Exception ex)
            //{
            //    string sError = Errores.mostrarError("Error al obtener las provincias de gestión de un determinado país", ex);
            //    string[] aError = Regex.Split(sError, "@#@");
            //    throw new Exception(Utilidades.escape(aError[0]), ex);
            //}
            return sResul;
        }
        public static string Arbol()
        {
            SqlDataReader dr = SUPER.DAL.PAIS.Arbol();
            StringBuilder sb = new StringBuilder();
            string nIdPais = "";
            string nIdProvincia = "";
            int indice1 = 0;
            int indice2 = 0;

            sb.Append("[");
 //           sb.Append("{ title: 'Países', key:0, folder: true, expanded: false, data:{nivel:'0',activo:''}, children: [");
            string sActivo="";

            while (dr.Read())
            {
                if (nIdPais != dr["identificador1"].ToString())
                {
                    indice2 = 0;
                    if (indice1 == 1)
                    {
                        sb.Append("]},");
                        indice1 = 0;
                    }
                    sActivo = ((bool)dr["ACTIVO1"]) ? "1" : "0";
                    sb.Append("{title: '" + dr["denominacion1"].ToString().Replace("'", "|") + "', key: 'N1_" + dr["identificador1"].ToString() + "', folder: true, expanded: false, data:{bd:'', parentIni: '',nivel:'1', activo:'" + sActivo + "'}, children: [");
                    nIdPais = dr["identificador1"].ToString();
                    if (indice1 == 0) indice1 = 1;
                }
                if (dr["identificador2"].ToString() == "0") continue;
                if (nIdProvincia != dr["identificador2"].ToString())
                {
                    if (indice2 == 1) sb.Append(",");
                    sActivo = ((bool)dr["ACTIVO2"]) ? "1" : "0";
                    sb.Append("{title: '" + dr["denominacion2"].ToString().Replace("'", "|") + "', key: 'N2_" + dr["identificador2"].ToString() + "', data:{bd: '', parentIni: '" + dr["identificador1"].ToString() + "',nivel:'2', activo:'" + sActivo + "'}}");
                    nIdProvincia = dr["identificador2"].ToString();
                    indice2 = 1;
                }
            }
            if (indice1 == 1) sb.Append("]}");

            //sb.Append("]}");

            sb.Append("]");

            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }
        public static string Grabar(string sUpdate)
        {
            SqlConnection oConn = null;
            SqlTransaction tr;

            string sResul = "";
            string sKey = "";

            //string[] aDatosBasicos = null;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw new Exception(Errores.mostrarError("Error al abrir la conexión", ex));
            }
            #endregion
            try
            {
                #region Modificaciones
                if (sUpdate != "") //No se han realizado modificaciones
                {
                    string[] aUpdates = Regex.Split(sUpdate, "///");
                    foreach (string oUpdate in aUpdates)
                    {
                        if (oUpdate == "") continue;
                        string[] aUpd = Regex.Split(oUpdate, "##");

                        ///aUpd[0] = id
                        ///aUpd[1] = nivel
                        ///aUpd[2] = activo
                        ///
                        if (Utilidades.isNumeric(aUpd[0])) sKey = aUpd[0];
                        else sKey = aUpd[0].Substring(3, aUpd[0].Length - 3);

                        if (aUpd[1] == "1") SUPER.DAL.PAIS.ActivarDesactivar(tr, int.Parse(sKey), (aUpd[2] == "1") ? true : false);
                        else if (aUpd[1] == "2")
                        {
                            SUPER.DAL.PROVINCIA.ActivarDesactivar(tr, int.Parse(sKey), (aUpd[2] == "1") ? true : false);
                        }
                    }
                }
                #endregion

                Conexion.CommitTransaccion(tr);

                //if (HttpContext.Current.Cache["Lista_Ambitos"] != null)
                //    HttpContext.Current.Cache.Remove("Lista_Ambitos");

                //sResul = sInsertados;
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
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
