using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SUPER.Capa_Datos;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace SUPER.Capa_Negocio
{
	public partial class SECTOR
	{
        #region Propiedades y Atributos

        protected int _t483_idsector;
        public int t483_idsector
        {
            get { return _t483_idsector; }
            set { _t483_idsector = value; }
        }
        protected string _t483_denominacion;
        public string t483_denominacion
        {
            get { return _t483_denominacion; }
            set { _t483_denominacion = value; }
        }
        #endregion

        #region Constructores

        public SECTOR()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados segÃºn el tipo de dato.
        }

        public SECTOR(int t483_idsector, string t483_denominacion)
        {
            this.t483_idsector = t483_idsector;
            this.t483_denominacion = t483_denominacion;
        }
        #endregion
		#region Metodos
        public static SqlDataReader CatalogoDenominacion(string t483_denominacion, string sTipoBusqueda, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t483_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t483_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_SECTOR_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_SECTOR_CAT_USU", aParam);
        }

        public static SqlDataReader CatalogoDenominacion()
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t483_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = "";
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = "I";
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = null;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado. 
            return SqlHelper.ExecuteSqlDataReader("SUP_SECTOR_CAT", aParam);
            
        }

        public static void CatalogoSectoresCache(DropDownList cboSector)
        {
            cboSector.DataValueField = "identificador";
            cboSector.DataTextField = "denominacion";
            DataSet ds = null;

            if (HttpContext.Current.Cache["Lista_Sectores"] == null)
            {
                SqlParameter[] aParam = new SqlParameter[0];
                ds = SqlHelper.ExecuteDataset("SUP_SECTORES_C", aParam);
                HttpContext.Current.Cache.Insert("Lista_Sectores", ds, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
            }
            else
                ds = (DataSet)HttpContext.Current.Cache["Lista_Sectores"];

            cboSector.DataSource = ds;
            cboSector.DataBind();
        }
        public static List<SECTOR> ListaSectores()
        {
            if (HttpContext.Current.Cache["Lista_Sectores"] == null)
            {
                List<SECTOR> oLista = new List<SECTOR>();
                SqlDataReader dr = SUPER.DAL.SECTOR.Catalogo();
                SECTOR oElem;
                while (dr.Read())
                {
                    oElem = new SECTOR(int.Parse(dr["identificador"].ToString()), dr["denominacion"].ToString());
                    oLista.Add(oElem);
                }
                dr.Close();
                dr.Dispose();

                HttpContext.Current.Cache.Insert("Lista_Sectores", oLista, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
                return oLista;
            }
            else
            {
                return (List<SECTOR>)HttpContext.Current.Cache["Lista_Sectores"];
            }
        }
        
        public static string Arbol()
        {
            SqlDataReader dr = SUPER.DAL.SECTOR.Arbol();
            StringBuilder sb = new StringBuilder();
            string sIdSector = "";
            string sIdSegmento = "";
            int indice1 = 0;
            int indice2 = 0;

            sb.Append("[");
            sb.Append("{ title: 'Sectores', key:0, folder: true, expanded: false, data:{nivel:'0',codext:''}, children: [");

            while (dr.Read())
            {
                if (sIdSector != dr["identificador1"].ToString())
                {
                    indice2 = 0;
                    if (indice1 == 1)
                    {
                        sb.Append("]},");
                        indice1 = 0;
                    }
                    //sb.Append("{title: '" + dr["identificador1"].ToString() + "-" + dr["denominacion1"].ToString() + "', key: '" + dr["identificador1"].ToString() + "', folder: true, expanded: false, data:{bd: '', parentIni: '',nivel:'1'}, children: [");
                    sb.Append("{title: '" + dr["denominacion1"].ToString().Replace("'", "|") + "', key: 'N1_" + dr["identificador1"].ToString() + "', folder: true, expanded: false, data:{bd: '', parentIni: '',nivel:'1', codext:'" + dr["CODEXT1"].ToString() + "'}, children: [");
                    sIdSector = dr["identificador1"].ToString();
                    if (indice1 == 0) indice1 = 1;
                }
                if (dr["identificador2"].ToString() == "0") continue;
                if (sIdSegmento != dr["identificador2"].ToString())
                {
                    if (indice2 == 1) sb.Append(",");
                    //sb.Append("{title: '" + dr["identificador2"].ToString() + "-" + dr["denominacion2"].ToString() + "', key: '" + dr["identificador2"].ToString() + "', data:{bd: '', parentIni: '" + dr["identificador1"].ToString() + "',nivel:'2'}}");
                    sb.Append("{title: '" + dr["denominacion2"].ToString().Replace("'", "|") + "', key: 'N2_" + dr["identificador2"].ToString() + "', data:{bd: '', parentIni: '" + dr["identificador1"].ToString() + "',nivel:'2', codext:'" + dr["CODEXT2"].ToString() + "'}}");
                    sIdSegmento = dr["identificador2"].ToString();
                    indice2 = 1;
                }
            }
            if (indice1 == 1) sb.Append("]}");

            sb.Append("]}");

            sb.Append("]");

            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

        public static string Grabar(string sDelete, string sInsert, string sUpdate)
        {
            SqlConnection oConn = null;
            SqlTransaction tr;

            string sResul = "", sInsertados = "";
            int nIDSector = -1;
            int nIDSegmen = -1;
            string sKey = "";
            string sKeyParent = "";
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

                string sError = Errores.mostrarError("Error al abrir la conexión", ex);
                string[] aError = Regex.Split(sError, "@#@");
                throw new Exception(Utilidades.escape(aError[0]), ex);
            }
            #endregion
            try
            {
                #region Bajas
                if (sDelete != "") //No se han realizado bajas
                {
                    string[] aDeletes = Regex.Split(sDelete, "///");
                    foreach (string oDelete in aDeletes)
                    {
                        if (oDelete == "") continue;
                        string[] aDel = Regex.Split(oDelete, "##");

                        ///aDel[0] = id
                        ///aDel[1] = title
                        ///aDel[2] = nivel
                        ///aDel[3] = parent
                        ///
                        if (Utilidades.isNumeric(aDel[0])) sKey = aDel[0];
                        else sKey = aDel[0].Substring(3, aDel[0].Length - 3);

                        if (aDel[2] == "1") SUPER.DAL.SECTOR.Delete(tr, int.Parse(sKey));
                        else if (aDel[2] == "2") SUPER.DAL.SEGMENTO.Delete(tr, int.Parse(sKey));
                    }
                }

                #endregion
                #region Inserciones
                if (sInsert != "") // No se han realizado Inserciones
                {
                    string[] aInserts = Regex.Split(sInsert, "///");
                    foreach (string oInsert in aInserts)
                    {
                        if (oInsert == "") continue;
                        string[] aInsert = Regex.Split(oInsert, "##");

                        ///aInsert[0] = id virtual
                        ///aInsert[1] = title
                        ///aInsert[2] = nivel
                        ///aInsert[3] = parent(sector para el caso de los segmentos)
                        ///aInsert[4] = codext                 
                        // Estoy metiendo el codigo externo con el valor de la denominacion

                        if (aInsert[2] == "1")
                        {
                            nIDSector = SUPER.DAL.SECTOR.Insert(tr, aInsert[1], "");
                            if (sInsertados == "") sInsertados = aInsert[0] + "::N1_" + nIDSector.ToString();
                            else sInsertados += "//" + aInsert[0] + "::N1_" + nIDSector.ToString();
                        }
                        else if (aInsert[2] == "2")
                        {
                            if (Utilidades.isNumeric(aInsert[3])) sKeyParent = aInsert[3];
                            else sKeyParent = aInsert[3].Substring(3, aInsert[3].Length - 3);
                            
                            string sID = (int.Parse(sKeyParent) < 0) ? nIDSector.ToString() : sKeyParent;
                            nIDSegmen = SUPER.DAL.SEGMENTO.Insert(tr, aInsert[1], "", int.Parse(sID));
                            if (sInsertados == "") sInsertados = aInsert[0] + "::N2_" + nIDSegmen.ToString();
                            else sInsertados += "//" + aInsert[0] + "::N2_" + nIDSegmen.ToString();
                        }
                    }
                }
                #endregion
                #region Modificaciones
                if (sUpdate != "") //No se han realizado modificaciones
                {
                    string[] aUpdates = Regex.Split(sUpdate, "///");
                    foreach (string oUpdate in aUpdates)
                    {
                        if (oUpdate == "") continue;
                        string[] aUpd = Regex.Split(oUpdate, "##");

                        ///aUpd[0] = id
                        ///aUpd[1] = title
                        ///aUpd[2] = nivel
                        ///aUpd[3] = parent(sector para el caso de los segmentos)
                        ///aUpd[4] = codext
                        ///

                        if (Utilidades.isNumeric(aUpd[0])) sKey = aUpd[0];
                        else sKey = aUpd[0].Substring(3, aUpd[0].Length - 3);

                        if (aUpd[2] == "1") SUPER.DAL.SECTOR.Update(tr, int.Parse(sKey), aUpd[1], null);
                        else if (aUpd[2] == "2")
                        {
                            if (Utilidades.isNumeric(aUpd[3])) sKeyParent = aUpd[3];
                            else sKeyParent = aUpd[3].Substring(3, aUpd[3].Length - 3);

                            string sID = (int.Parse(sKeyParent) < 0) ? nIDSector.ToString() : sKeyParent;
                            SUPER.DAL.SEGMENTO.Update(tr, int.Parse(sKey), aUpd[1], null, int.Parse(sID));
                        }
                    }
                }
                #endregion

                Conexion.CommitTransaccion(tr);

                if (HttpContext.Current.Cache["Lista_Sectores"] != null)
                    HttpContext.Current.Cache.Remove("Lista_Sectores");

                sResul = sInsertados;
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                //string sError = Errores.mostrarError("Error al grabar los datos del árbol. ", ex);
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
        //public class parametros
        //{
        //    public string sDelete;
        //    public string sInsert;
        //    public string sUpdate;
        //}
	}
}
