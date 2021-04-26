using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using SUPER.Capa_Datos;

/// <summary>
/// Manejo de descripciones dinámicas de CR, area de negocio, linea, etc....
/// </summary>
/// 
namespace SUPER.Capa_Negocio
{
public class Estructura
{
        public enum sTipoElem
        {
            SUBNODO,
            NODO,
            SUPERNODO1,
            SUPERNODO2,
            SUPERNODO3,
            SUPERNODO4
        }

        #region propiedades
        protected byte _Cod;
        protected string _DesC;
        protected string _DesL;
        protected bool _bUtilizado;

        public byte nCodigo
	    {
            get { return _Cod; }
            set { _Cod = value; }
	    }
        public string sDesCorta
        {
            get { return _DesC; }
            set { _DesC = value; }
        }
        public string sDesLarga
        {
            get { return _DesL; }
            set { _DesL = value; }
        }
        public bool bUtilizado
        {
            get { return _bUtilizado; }
            set { _bUtilizado = value; }
        }

        #endregion
        public Estructura()
	    {
		    //
		    // TODO: Add constructor logic here
		    //
	    }
        public static List<Estructura> ListaGlobal()
        {
            if (HttpContext.Current.Cache["Lista_Estructura"] == null)
            {
                List<Estructura> oLista = new List<Estructura>();
                SqlParameter[] aParam = new SqlParameter[0];
                SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_DEN_ESTRUCTURA_C", aParam);
                Estructura oElem;
                while (dr.Read())
                {
                    oElem = new Estructura();
                    oElem.nCodigo = byte.Parse(dr["t400_idestructura"].ToString());
                    oElem.sDesCorta = dr["t400_dencorta"].ToString();
                    oElem.sDesLarga = dr["t400_denlarga"].ToString();
                    oElem.bUtilizado = (bool)dr["t400_utilizado"];
                    oLista.Add(oElem);
                }
                dr.Close();
                dr.Dispose();

                HttpContext.Current.Cache.Insert("Lista_Estructura", oLista, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
                return oLista;
            }
            else
            {
                return (List<Estructura>)HttpContext.Current.Cache["Lista_Estructura"];
            }
        }
        public static SqlDataReader ListaActiva()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_ESTRUCTURA_AC", aParam);
        }
        public static List<Estructura> ListaGlobalActiva()
        {
            if (HttpContext.Current.Cache["Lista_Estructura"] == null)
            {
                List<Estructura> oLista = new List<Estructura>();
                SqlParameter[] aParam = new SqlParameter[0];
                SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_DEN_ESTRUCTURA_C", aParam);
                Estructura oElem;
                while (dr.Read())
                {
                    oElem = new Estructura();
                    oElem.nCodigo = byte.Parse(dr["t400_idestructura"].ToString());
                    oElem.sDesCorta = dr["t400_dencorta"].ToString();
                    oElem.sDesLarga = dr["t400_denlarga"].ToString();
                    oElem.bUtilizado = (bool)dr["t400_utilizado"];
                    oLista.Add(oElem);
                }
                dr.Close();
                dr.Dispose();

                HttpContext.Current.Cache.Insert("Lista_Estructura", oLista, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
                return oLista;
            }
            else
            {
                return (List<Estructura>)HttpContext.Current.Cache["Lista_Estructura"];
            }
        }
        /// <summary>
        /// Obtiene la descripcion corta del tipo de elemento SUBNODO->Area negocio; NODO->C.R.; SNN1->SuperNodo Nivel 1
        /// </summary>
        /// 
        public static string getDefCorta(sTipoElem sTE)
        {
            string sRes = "";
            int iInd = 0;
            List<Estructura> listaEstructura = Estructura.ListaGlobal();
            switch (sTE)
            {
                case sTipoElem.SUBNODO:
                    iInd = 1;
                    break;
                case sTipoElem.NODO:
                    iInd = 2;
                    break;
                case sTipoElem.SUPERNODO1:
                    iInd = 3;
                    break;
                case sTipoElem.SUPERNODO2:
                    iInd = 4;
                    break;
                case sTipoElem.SUPERNODO3:
                    iInd = 5;
                    break;
                case sTipoElem.SUPERNODO4:
                    iInd = 6;
                    break;
            }
            foreach (Estructura oEL in listaEstructura)
            {
                if (oEL.nCodigo == iInd)
                {
                    sRes = oEL.sDesCorta;
                    break;
                }
            }
            return sRes.Replace((char)34, (char)39);
        }
        public static string getDefLarga(sTipoElem sTE)
        {
            string sRes = "";
            int iInd = 0;
            List<Estructura> listaEstructura = Estructura.ListaGlobal();
            switch (sTE)
            {
                case sTipoElem.SUBNODO:
                    iInd = 1;
                    break;
                case sTipoElem.NODO:
                    iInd = 2;
                    break;
                case sTipoElem.SUPERNODO1:
                    iInd = 3;
                    break;
                case sTipoElem.SUPERNODO2:
                    iInd = 4;
                    break;
                case sTipoElem.SUPERNODO3:
                    iInd = 5;
                    break;
                case sTipoElem.SUPERNODO4:
                    iInd = 6;
                    break;
            }
            foreach (Estructura oEL in listaEstructura)
            {
                if (oEL.nCodigo == iInd)
                {
                    sRes = oEL.sDesLarga;
                    break;
                }
            }
            return sRes.Replace((char)34, (char)39);
        }

        public static SqlDataReader GetEstructuraOrganizativa(bool bMostrarInactivos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@bMostrarInactivos", SqlDbType.Bit, 1);
            aParam[0].Value = bMostrarInactivos;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETESTRUCTURA_ORGANIZATIVA", aParam);
        }
        public static SqlDataReader GetEstructuraOrganizativa(int t314_idusuario, bool bMostrarInactivos, string sOrigen)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@bMostrarInactivos", SqlDbType.Bit, 1);
            aParam[1].Value = bMostrarInactivos;

            if (sOrigen=="PST")
                return SqlHelper.ExecuteSqlDataReader("SUP_GETESTRUCTURA_ORGANIZATIVA_USUARIO_PST", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_GETESTRUCTURA_ORGANIZATIVA_USUARIO_ECO", aParam);
        }
        public static DataSet GetEstructuraOrganizativa_DS(bool bMostrarInactivos, bool bMostrarInstrumentales)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@bMostrarInactivos", SqlDbType.Bit, 1);
            aParam[0].Value = bMostrarInactivos;
            aParam[1] = new SqlParameter("@bMostrarInstrumentales", SqlDbType.Bit, 1);
            aParam[1].Value = bMostrarInstrumentales;

            return SqlHelper.ExecuteDataset("SUP_GETESTRUCTURA_ORGANIZATIVA", aParam);
        }
        public static SqlDataReader GetEstructuraMantNodo(int t314_idusuario, bool bMostrarInactivos)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@bMostrarInactivos", SqlDbType.Bit, 1);
            aParam[1].Value = bMostrarInactivos;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETESTRUCTURA_MANTNODO", aParam);
        }
        public static SqlDataReader GetEstructuraOrganizativaSubnodos(string sSubnodos, bool bMostrarInactivos)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@sSubnodos", SqlDbType.VarChar, 8000, sSubnodos),
                ParametroSql.add("@bMostrarInactivos", SqlDbType.Bit, 1, bMostrarInactivos)
            };

            return SqlHelper.ExecuteSqlDataReader("SUP_CONS_ESTRUCTURA_SUBNODOS", aParam);
        }
        public static SqlDataReader GetEstructuraOrganizativaNodos(string sNodos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sNodos", SqlDbType.VarChar, 8000);
            aParam[0].Value = sNodos;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONS_ESTRUCTURA_NODOS", aParam);
        }
        public static SqlDataReader GetNodosEvaluados(int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETNODOS_EVALUADOS", aParam);
        }
    }
}