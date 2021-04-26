using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//Para el ArrayList
using System.Collections;


namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
    /// Class	 : SUBNODO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T304_SUBNodo
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	23/11/2007 09:38:27	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class SUBNODO
	{
        #region Propiedades y Atributos

        private string _DesResponsable;
        public string DesResponsable
        {
            get { return _DesResponsable; }
            set { _DesResponsable = value; }
        }
        #endregion

		#region Metodos 

        public static SqlDataReader CatalogoFigura(SqlTransaction tr, int t303_idnodo, int nUsuario, int nGestorSubNodo)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[1].Value = nUsuario;
            aParam[2] = new SqlParameter("@nGestorSubNodo", SqlDbType.Int, 4);
            aParam[2].Value = nGestorSubNodo;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_SUBNODO_Figura", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUBNODO_Figura", aParam);
        }

        public static SUBNODO Obtener(SqlTransaction tr, int t304_idsubnodo)
        {
            SUBNODO o = new SUBNODO();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t304_idsubnodo;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_SUBNODO_O", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUBNODO_O", aParam);

            if (dr.Read())
            {
                if (dr["t304_idsubnodo"] != DBNull.Value)
                    o.t304_idsubnodo = int.Parse(dr["t304_idsubnodo"].ToString());
                if (dr["t304_denominacion"] != DBNull.Value)
                    o.t304_denominacion = (string)dr["t304_denominacion"];
                if (dr["t303_idnodo"] != DBNull.Value)
                    o.t303_idnodo = int.Parse(dr["t303_idnodo"].ToString());
                if (dr["t304_estado"] != DBNull.Value)
                    o.t304_estado = (bool)dr["t304_estado"];
                if (dr["t304_orden"] != DBNull.Value)
                    o.t304_orden = int.Parse(dr["t304_orden"].ToString());
                if (dr["t314_idusuario_responsable"] != DBNull.Value)
                    o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
                if (dr["responsable"] != DBNull.Value)
                    o.DesResponsable = (string)dr["responsable"];
                if (dr["t304_maniobra"] != DBNull.Value)
                    o.t304_maniobra = byte.Parse(dr["t304_maniobra"].ToString());
                if (dr["t313_idempresa"] != DBNull.Value)
                    o.t313_idempresa = int.Parse(dr["t313_idempresa"].ToString());
                if (dr["t313_denominacion"] != DBNull.Value)
                    o.t313_denominacion = (string)dr["t313_denominacion"]; 
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de SUBNODO"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static SqlDataReader ObtenerSegunVisionProyectos(SqlTransaction tr, int t303_idnodo, int nUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[1].Value = nUsuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_SUBNODO_VISIONPROY", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUBNODO_VISIONPROY", aParam);
        }
        public static SqlDataReader ObtenerSubNodosUsuarioSegunVisionProyectosTEC(SqlTransaction tr, int nUsuario, bool bMostrarBitacoricos, bool bSoloActivos)
        {
            SqlParameter[] aParam = null;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {
                aParam = new SqlParameter[1];
                aParam[0] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
                aParam[0].Value = bSoloActivos;
            }
            else
            {
                aParam = new SqlParameter[3];
                aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                aParam[0].Value = nUsuario;
                aParam[1] = new SqlParameter("@bMostrarBitacoricos", SqlDbType.Bit, 1);
                aParam[1].Value = bMostrarBitacoricos;
                aParam[2] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
                aParam[2].Value = bSoloActivos;
            }

            if (tr == null)
                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_SUBNODOS_PROY_USUARIO_TEC_ADM", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_SUBNODOS_PROY_USUARIO_TEC", aParam);
            else
                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETVISION_SUBNODOS_PROY_USUARIO_TEC_ADM", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETVISION_SUBNODOS_PROY_USUARIO_TEC", aParam);
        }

        public static SqlDataReader ObtenerConEmpresaAsignada(SqlTransaction tr, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_SUBNODO_EMPRESAS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUBNODO_EMPRESAS", aParam);
        }
        public static SqlDataReader CatalogoPorNodo(SqlTransaction tr, int t303_idnodo, byte nOpcionManiobra)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@nOpcionManiobra", SqlDbType.TinyInt, 2);
            aParam[1].Value = nOpcionManiobra;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_SUBNODO_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUBNODO_CAT", aParam);
        }
        public static SqlDataReader CatalogoPorUsuarioNodo(SqlTransaction tr, int nUsuario, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_SUBNODOGESTOR", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUBNODOGESTOR", aParam);
        }
        public static DataSet CatalogoActivos(SqlTransaction tr, int t303_idnodo, bool bMostrarManiobra)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@bMostrarManiobra", SqlDbType.Bit, 1);
            aParam[1].Value = bMostrarManiobra;

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_SUBNODO_ACTIVOS", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_SUBNODO_ACTIVOS", aParam);
        }
        public static int ObtenerSubnodoManiobra2(SqlTransaction tr, int t303_idnodo)
        {
            int nSubnodo = 0;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_SUBNODO_MANIOBRA2_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUBNODO_MANIOBRA2_S", aParam);

            if (dr.Read()){
                nSubnodo = (int)dr["t304_idsubnodo"];
            }
            dr.Close();
            dr.Dispose();
            return nSubnodo;
        }
        /// <summary>
        /// Dado un un elemento de estructura devuelve una cadena con los id's desde SuprNodo Nivel 4 hasta subnodo
        /// </summary>
        public static string fgGetCadenaID(string sTipo, string sId)
        {
            string sRes = "";
            if (sTipo == "1")
                sRes = sId + "-0-0-0-0-0";
            else
            {
                string sProcAlm = "";
                SqlParameter[] aParam = new SqlParameter[1];
                aParam[0] = new SqlParameter("@idElem", SqlDbType.Int, 4);
                aParam[0].Value = int.Parse(sId);
                switch (sTipo)
                {
                    case "2":
                        sProcAlm="SUP_SUPERNODO3_GETID";
                        break;
                    case "3":
                        sProcAlm="SUP_SUPERNODO2_GETID";
                        break;
                    case "4":
                        sProcAlm="SUP_SUPERNODO1_GETID";
                        break;
                    case "5":
                        sProcAlm="SUP_NODO_GETID";
                        break;
                    case "6":
                        sProcAlm="SUP_SUBNODO_GETID";
                        break;
                }
                SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(sProcAlm, aParam);
                if (dr.Read())
                {
                    sRes=dr["cadId"].ToString();
                }
                dr.Close();
                dr.Dispose();
            }
            return sRes;
        }
        public static SqlDataReader CatalogoParaReasignacion(int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@bAdmin", SqlDbType.Bit, 1);
            aParam[1].Value = SUPER.Capa_Negocio.Utilidades.EsAdminProduccion();

            return SqlHelper.ExecuteSqlDataReader("SUP_SUBNODOREASIGNACION_CAT", aParam);
        }
        /// <summary>
        /// Catalogo de subnodos por tipos de maniobra
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t303_idnodo"></param>
        /// <param name="bMostrarManiobra"></param>
        /// <returns></returns>
        public static SqlDataReader CatalogoPorTipoManiobra(SqlTransaction tr, int t303_idnodo, ArrayList TiposManiobra)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo),
                ParametroSql.add("@TMP_MANIOBRA", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListCod(TiposManiobra))
                };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_SUBNODO_SByManiobra", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUBNODO_SByManiobra", aParam);
        }
        /// <summary>
        /// Establece el tipo de maniobra para un subnodo
        /// 0-> No es de maniobra
        /// 1-> 
        /// 2-> Maniobra para proyectos PIG
        /// 3-> Maniobra por defecto para réplicas
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t304_idsubnodo"></param>
        /// <param name="t304_maniobra"></param>
        public static void SetManiobra(SqlTransaction tr, int t304_idsubnodo, byte t304_maniobra)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@t304_maniobra", SqlDbType.TinyInt, 1);

            aParam[0].Value = t304_idsubnodo;
            aParam[1].Value = t304_maniobra;
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_SUBNODO_MANIOBRA_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SUBNODO_MANIOBRA_U", aParam);
        }
        /// <summary>
        /// Obtiene el subnodo por defecto de un nodo para crear un PE.
        /// Si no existe, crea uno de maniobra
        /// </summary>
        /// <param name="t303_idnodo"></param>
        /// <returns></returns>
        public static int GetSubNodoDefecto(int t303_idnodo)
        {
            #region Obtención de subnodo para crear el proyectosubnodo
            int nCount = 0;
            int nCountManiobraTipo2 = 0;
            int idSubNodoAuxDestino = 0;
            int idSubNodoAuxManiobra = 0;
            int idSubNodoGrabar = 0;

            int nCountSubnodosNoManiobra = 0;
            int nResponsableSubNodo = 0;
            string sDenominacionNodo = "";
            DataSet dsSubNodos = null;

            try
            {
                dsSubNodos = SUBNODO.CatalogoActivos(null, t303_idnodo, true);
                foreach (DataRow oSN in dsSubNodos.Tables[0].Rows)
                {
                    if ((byte)oSN["t304_maniobra"] == 1)
                    {
                        nCount++;
                        idSubNodoAuxManiobra = (int)oSN["t304_idsubnodo"];
                        nResponsableSubNodo = (int)oSN["t314_idusuario_responsable"];
                        sDenominacionNodo = oSN["t303_denominacion"].ToString();
                    }
                    else if ((byte)oSN["t304_maniobra"] == 0)
                    {
                        idSubNodoAuxDestino = (int)oSN["t304_idsubnodo"];
                        nCountSubnodosNoManiobra++;
                        nResponsableSubNodo = (int)oSN["t314_idusuario_responsable"];
                        sDenominacionNodo = oSN["t303_denominacion"].ToString();
                    }
                    else nCountManiobraTipo2++;
                }
                if (nCountSubnodosNoManiobra == 1) //si solo hay un subnodo en el nodo, que la réplica se haga a ese subnodo.
                {
                    idSubNodoGrabar = idSubNodoAuxDestino;
                }
                else
                {
                    if (nCount == 0)
                    {
                        NODO oNodo = NODO.Select(null, t303_idnodo);
                        //crear subnodo maniobra
                        idSubNodoGrabar = SUBNODO.Insert(null, "Proyectos a reasignar", t303_idnodo, 0, true, 1, oNodo.t314_idusuario_responsable, null);
                        nResponsableSubNodo = oNodo.t314_idusuario_responsable;
                    }
                    else
                    {
                        //if (nCount > 1)
                        //{
                        //    dsSubNodos.Dispose();
                        //    throw (new Exception("El número de subnodos de maniobra es " + nCount.ToString() + " en el nodo " + sDenominacionNodo + ". Por favor avise al administrador."));
                        //}

                        if (dsSubNodos.Tables[0].Rows.Count - 1 - nCountManiobraTipo2 > 1 || dsSubNodos.Tables[0].Rows.Count - 1 - nCountManiobraTipo2 == 0)
                        {
                            idSubNodoGrabar = idSubNodoAuxManiobra;
                        }
                        else
                        {
                            idSubNodoGrabar = idSubNodoAuxDestino;
                        }
                    }
                }
                dsSubNodos.Dispose();
                #endregion
                return idSubNodoGrabar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dsSubNodos != null)
                {
                    dsSubNodos.Dispose();
                }
            }
        }

        #endregion
    }
}
