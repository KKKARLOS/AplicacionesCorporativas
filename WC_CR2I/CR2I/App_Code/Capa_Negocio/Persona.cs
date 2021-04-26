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
using CR2I.Capa_Datos;

namespace CR2I.Capa_Negocio
{
    /// <summary>
    /// Summary description for Calendario
    /// </summary>
    public class Persona
    {
        #region Atributos privados

        private string _sNombre;
        private string _sApellido;
        private DateTime _dFecAlta;
        private DateTime _dFecUltImp;
        private int _nNumEmp;
        private int _nIDFicepi;

        //Atributo auxiliares
        //private string _sDesUne;

        #endregion

        #region Propiedades públicas

        public string sNombre
        {
            get { return _sNombre; }
            set { _sNombre = value; }
        }
        public string sApellido
        {
            get { return _sApellido; }
            set { _sApellido = value; }
        }
        public DateTime dFecAlta
        {
            get { return _dFecAlta; }
            set { _dFecAlta = value; }
        }
        public DateTime dFecUltImp
        {
            get { return _dFecUltImp; }
            set { _dFecUltImp = value; }
        }
        public int nNumEmp
        {
            get { return _nNumEmp; }
            set { _nNumEmp = value; }
        }
        public int nIDFicepi
        {
            get { return _nIDFicepi; }
            set { _nIDFicepi = value; }
        }

        #endregion

        #region Constructores

        public Persona()
        {
            //En el constructor vacío, se inicializan los atributo
            //con los valores predeterminados según el tipo de dato.
        }

        public Persona(string sNombre, string sApellido, DateTime dFecAlta, DateTime dFecUltImp, int nNumEmp, int nIDFicepi)
        {
            //En el constructor vacío, se inicializan los atributo
            //con los valores predeterminados según el tipo de dato.
            this.sNombre = sNombre;
            this.sApellido = sApellido;
            this.dFecAlta = dFecAlta;
            this.dFecUltImp = dFecUltImp;
            this.nNumEmp = nNumEmp;
            this.nIDFicepi = nIDFicepi;
        }

        #endregion

        #region	Métodos públicos

        /// <summary>
        /// 
        /// Obtiene una lista de los diferentes calendarios,
        /// correspondientes a la tabla T039_CALENDARIO.
        /// </summary>
        public List<Persona> ObtenerCatalogo()
        {
            List<Persona> MiLista = new List<Persona>();
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion,
                "PSP_TEST");
            Persona oPer;
            while (dr.Read())
            {
                oPer = new Persona(dr["NOMBRE"].ToString(), dr["APELLIDO1"].ToString(), DateTime.Parse(dr["FEC_ALTA"].ToString()), DateTime.Parse(dr["F_ULT_IMPUTAC"].ToString()), int.Parse(dr["NUM_EMPLEADO"].ToString()), int.Parse(dr["IDFICEPI"].ToString()));
                MiLista.Add(oPer);
            }
            dr.Close();
            dr.Dispose();

            return MiLista;
        }

        public SqlDataReader ObtenerCatalogoDR()
        {
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion,
                "PSP_TEST");

            return dr;
        }


        #endregion
    }

}
