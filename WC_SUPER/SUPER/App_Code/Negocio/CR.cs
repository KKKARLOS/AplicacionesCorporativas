using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using SUPER.Capa_Datos;
//using System.Transactions;

namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Summary description for Calendario
    /// </summary>
    public class CR
    {
        #region Atributos privados

        private int _nIdCR;
        private string _sDesCR;
        private bool _bEstado;

        #endregion

        #region Propiedades públicas

        public int nIdCR
        {
            get { return _nIdCR; }
            set { _nIdCR = value; }
        }
        public string sDesCR
        {
            get { return _sDesCR; }
            set { _sDesCR = value; }
        }

        public bool bEstado
        {
            get { return _bEstado; }
            set { _bEstado = value; }
        }

        #endregion

        #region Constructores

        public CR()
        {
            //En el constructor vacío, se inicializan los atributo
            //con los valores predeterminados según el tipo de dato.
        }

        public CR(int nIdCR, string sDesCR)
        {
            //En el constructor vacío, se inicializan los atributo
            //con los valores predeterminados según el tipo de dato.
            this.nIdCR = nIdCR;
            this.sDesCR = sDesCR;
        }

        public CR(int nIdCR, string sDesCR, bool bEstado)
        {
            //En el constructor vacío, se inicializan los atributo
            //con los valores predeterminados según el tipo de dato.
            this.nIdCR = nIdCR;
            this.sDesCR = sDesCR;
            this.bEstado = bEstado;
        }

        #endregion

        #region	Métodos públicos

        /// <summary>
        /// 
        /// Obtiene los datos generales de un calendario determinado,
        /// correspondientes a la tabla T039_CALENDARIO.
        /// </summary>
        public void Obtener(int nIdCR)
        {
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, "SUP_CRS", nIdCR);

            if (dr.Read())
            {
                this.nIdCR = int.Parse(dr["t303_idnodo"].ToString());
                this.sDesCR = dr["t303_denominacion"].ToString();
            }
            dr.Close();
            dr.Dispose();
        }

        /// <summary>
        /// 
        /// Obtiene una lista de los diferentes calendarios,
        /// correspondientes a la tabla T039_CALENDARIO.
        /// </summary>
        public static SqlDataReader ObtenerCatalogoDR()
        {
            return SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, "SUP_CRCATA");
        }

        /// <summary>
        /// 
        /// Obtiene una lista de los diferentes calendarios,
        /// correspondientes a la tabla T039_CALENDARIO.
        /// </summary>
        public List<CR> ObtenerCatalogo()
        {
            List<CR> MiLista = new List<CR>();
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, "SUP_CRCATA");
            CR oCR;
            while (dr.Read())
            {
                oCR = new CR(int.Parse(dr["t303_idnodo"].ToString()), dr["t303_denominacion"].ToString(), bool.Parse(dr["t303_estado"].ToString()));
                MiLista.Add(oCR);
            }
            dr.Close();
            dr.Dispose();

            return MiLista;
        }

        /// <summary>
        /// 
        /// Obtiene una lista de los diferentes CR´s a los que puede acceder el usuario en calidad de Director de CR
        /// o miembro de Oficina técnica o figura desde el nivel de nodo hacia arriba. 
        /// Si es administrador tendrá acceso a todos los CR´s
        /// </summary>
        public List<CR> ObtenerCatalogoPlant(int iNumEmpleado)
        {
            List<CR> MiLista = new List<CR>();
            string sDesNodo;
            SqlDataReader dr = NODO.CatalogoAdministrables(iNumEmpleado, true);

            CR oCR;
            while (dr.Read())
            {
                sDesNodo=dr["t303_idnodo"].ToString()+ " - " + dr["t303_denominacion"].ToString();
                oCR = new CR(int.Parse(dr["t303_idnodo"].ToString()), sDesNodo);
                MiLista.Add(oCR);
            }
            dr.Close();
            dr.Dispose();

            return MiLista;
        }

        #endregion
    }
}
