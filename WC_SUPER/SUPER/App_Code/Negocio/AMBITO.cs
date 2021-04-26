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
	public partial class AMBITO
	{
        #region Propiedades y Atributos

        protected int _t481_idambito;
        public int t481_idambito
        {
            get { return _t481_idambito; }
            set { _t481_idambito = value; }
        }
        protected string _t481_denominacion;
        public string t481_denominacion
        {
            get { return _t481_denominacion; }
            set { _t481_denominacion = value; }
        }
        #endregion

        #region Constructores

        public AMBITO()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados segÃºn el tipo de dato.
        }

        public AMBITO(int t481_idambito, string t481_denominacion)
        {
            this.t481_idambito = t481_idambito;
            this.t481_denominacion = t481_denominacion;
        }
        #endregion

		#region Metodos
        public static string cargarZonasAmbito(string sID)
        {
            //try
            //{
                StringBuilder sb = new System.Text.StringBuilder();
                SqlDataReader dr = SUPER.DAL.AMBITO.Zonas(int.Parse(sID)); //Mostrar todos todos las zonas relacionados a un determinado ámbito

                while (dr.Read())
                {
                    sb.Append(dr["identificador"].ToString() + "##" + dr["denominacion"].ToString() + "///");
                }
                dr.Close();
                dr.Dispose();

                return sb.ToString();
            //}
            //catch (Exception ex)
            //{
            //    string sError = Errores.mostrarError("Error al obtener las zonas de un determinado ámbito", ex);
            //    string[] aError = Regex.Split(sError, "@#@");
            //    throw new Exception(Utilidades.escape(aError[0]), ex);
            //}
        }
        public static void CatalogoAmbitosCache(DropDownList cboAmbito, string defValue)
        {
            cboAmbito.DataValueField = "identificador";
            cboAmbito.DataTextField = "denominacion";
            DataSet ds = null;

            if (HttpContext.Current.Cache["Lista_Ambitos"] == null)
            {
                SqlParameter[] aParam = new SqlParameter[0];
                ds = SqlHelper.ExecuteDataset("SUP_AMBITO_C", aParam);
                HttpContext.Current.Cache.Insert("Lista_Ambitos", ds, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
            }
            else
                ds = (DataSet)HttpContext.Current.Cache["Lista_Ambitos"];

            cboAmbito.DataSource = ds;
            cboAmbito.DataBind();
            cboAmbito.Items.Insert(0, new ListItem("", defValue));
        }
        public static List<AMBITO> ListaAmbitos()
        {
            if (HttpContext.Current.Cache["Lista_Ambitos"] == null)
            {
                List<AMBITO> oLista = new List<AMBITO>();
                SqlDataReader dr = SUPER.DAL.AMBITO.Catalogo();
                AMBITO oElem;
                while (dr.Read())
                {
                    oElem = new AMBITO(int.Parse(dr["identificador"].ToString()), dr["denominacion"].ToString());
                    oLista.Add(oElem);
                }
                dr.Close();
                dr.Dispose();

                HttpContext.Current.Cache.Insert("Lista_Ambitos", oLista, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
                return oLista;
            }
            else
            {
                return (List<AMBITO>)HttpContext.Current.Cache["Lista_Ambitos"];
            }
        }
        public static string Arbol()
        {
            SqlDataReader dr = SUPER.DAL.AMBITO.Arbol();
            StringBuilder sb = new StringBuilder();
            string nIdAmbito = "";
            string nIdZona = "";
            int indice1 = 0;
            int indice2 = 0;

            sb.Append("[");
            sb.Append("{ title: 'Ámbitos', key:0, folder: true, expanded: false, data:{nivel:'0',codext:''}, children: [");

            while (dr.Read())
            {
                if (nIdAmbito != dr["identificador1"].ToString())
                {
                    indice2 = 0;
                    if (indice1 == 1)
                    {
                        sb.Append("]},");
                        indice1 = 0;
                    }
                    //sb.Append("{title: '" + dr["identificador1"].ToString() + "-" + dr["denominacion1"].ToString() + "', key: '" + dr["identificador1"].ToString() + "', folder: true, expanded: false, data:{bd: '', parentIni: '',nivel:'1'}, children: [");
                    sb.Append("{title: '" + dr["denominacion1"].ToString().Replace("'", "|") + "', key: 'N1_" + dr["identificador1"].ToString() + "', folder: true, expanded: false, data:{bd: '', parentIni: '',nivel:'1', codext:'" + dr["CODEXT1"].ToString() + "'}, children: [");
                    nIdAmbito = dr["identificador1"].ToString();
                    if (indice1 == 0) indice1 = 1;
                }
                if (dr["identificador2"].ToString() == "0") continue;
                if (nIdZona != dr["identificador2"].ToString())
                {
                    if (indice2 == 1) sb.Append(",");
                    //sb.Append("{title: '" + dr["identificador2"].ToString() + "-" + dr["denominacion2"].ToString() + "', key: '" + dr["identificador2"].ToString() + "', data:{bd: '', parentIni: '" + dr["identificador1"].ToString() + "',nivel:'2'}}");
                    sb.Append("{title: '" + dr["denominacion2"].ToString().Replace("'", "|") + "', key: 'N2_" + dr["identificador2"].ToString() + "', data:{bd: '', parentIni: '" + dr["identificador1"].ToString() + "',nivel:'2', codext:'" + dr["CODEXT2"].ToString() + "'}}");
                    nIdZona = dr["identificador2"].ToString();
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
            int nIdAmbito = -1;
            int nIdZona = -1;
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
                throw new Exception(Errores.mostrarError("Error al abrir la conexión", ex));
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
                        ///aDel[2] = parent
                        ///
                        if (Utilidades.isNumeric(aDel[0])) sKey = aDel[0];
                        else sKey = aDel[0].Substring(3, aDel[0].Length - 3);

                        if (aDel[2] == "1") SUPER.DAL.AMBITO.Delete(tr, int.Parse(sKey));
                        else if (aDel[2] == "2") SUPER.DAL.ZONA.Delete(tr, int.Parse(sKey));
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
                        ///aInsert[3] = parent(ambito para el caso de las zonas)
                        ///aInsert[4] = codext                 
                        // Estoy metiendo el codigo externo con el valor de la denominacion

                        if (aInsert[2] == "1")
                        {
                            nIdAmbito = SUPER.DAL.AMBITO.Insert(tr, aInsert[1], aInsert[4]);
                            if (sInsertados == "") sInsertados = aInsert[0] + "::N1_" + nIdAmbito.ToString();
                            else sInsertados += "//" + aInsert[0] + "::N1_" + nIdAmbito.ToString();
                        }
                        else if (aInsert[2] == "2")
                        {
                            if (Utilidades.isNumeric(aInsert[3])) sKeyParent = aInsert[3];
                            else sKeyParent = aInsert[3].Substring(3, aInsert[3].Length - 3);

                            string sID = (int.Parse(sKeyParent) < 0) ? nIdAmbito.ToString() : sKeyParent;
                            nIdZona = SUPER.DAL.ZONA.Insert(tr, aInsert[1], aInsert[4], int.Parse(sID));
                            if (sInsertados == "") sInsertados = aInsert[0] + "::N2_" + nIdZona.ToString();
                            else sInsertados += "//" + aInsert[0] + "::N2_" + nIdZona.ToString();
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
                        ///aUpd[3] = parent(ambito para el caso de las zonas)
                        ///aUpd[4] = codext
                        ///


                        if (Utilidades.isNumeric(aUpd[0])) sKey = aUpd[0];
                        else sKey = aUpd[0].Substring(3, aUpd[0].Length - 3);

                        if (aUpd[2] == "1") SUPER.DAL.AMBITO.Update(tr, int.Parse(sKey), aUpd[1], aUpd[4]);
                        else if (aUpd[2] == "2")
                        {
                            if (Utilidades.isNumeric(aUpd[3])) sKeyParent = aUpd[3];
                            else sKeyParent = aUpd[3].Substring(3, aUpd[3].Length - 3);
                            string sID = (int.Parse(sKeyParent) < 0) ? nIdAmbito.ToString() : sKeyParent;
                            SUPER.DAL.ZONA.Update(tr, int.Parse(sKey), aUpd[1], aUpd[4], int.Parse(sID));
                        }
                    }
                }
                #endregion

                Conexion.CommitTransaccion(tr);

                if (HttpContext.Current.Cache["Lista_Ambitos"] != null)
                    HttpContext.Current.Cache.Remove("Lista_Ambitos");

                sResul = sInsertados;
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                //string sError = Errores.mostrarError("Error al grabar los datos del árbol. ", ex);
                //string[] aError = Regex.Split(sError, "@#@");
                //throw new Exception(Utilidades.escape(aError[0] + "\n\n" + "Código Error: " + aError[1]), ex);
                //if (SUPER.BLL.Log.logger.IsDebugEnabled) SUPER.BLL.Log.logger.Debug("Grabación Erronea.");
                throw ex;
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
            //if (SUPER.BLL.Log.logger.IsDebugEnabled) SUPER.BLL.Log.logger.Debug("Grabación Correcta.");
            return sResul;
        }
        
        #endregion
	}
}
