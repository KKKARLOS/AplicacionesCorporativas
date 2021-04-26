using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.Data.SqlClient;

namespace SUPER.BLL
{

    /// <summary>
    /// Descripción breve de FamiliaEntorno
    /// </summary>
    public class FamiliaEntorno
    {
        #region Propiedades y Atributos

        private int _t861_idfament;
        public int t861_idfament
        {
            get { return _t861_idfament; }
            set { _t861_idfament = value; }
        }

        private string _t861_denominacion;
        public string t861_denominacion
        {
            get { return _t861_denominacion; }
            set { _t861_denominacion = value; }
        }

        private bool _t861_publica;
        public bool t861_publica
        {
            get { return _t861_publica; }
            set { _t861_publica = value; }
        }

        private int _t001_idficepi_autor;
        public int t001_idficepi_autor
        {
            get { return _t001_idficepi_autor; }
            set { _t001_idficepi_autor = value; }
        }

        private int _t001_idficepi_modif;
        public int t001_idficepi_modif
        {
            get { return _t001_idficepi_modif; }
            set { _t001_idficepi_modif = value; }
        }

        private DateTime _t861_fcreacion;
        public DateTime t861_fcreacion
        {
            get { return _t861_fcreacion; }
            set { _t861_fcreacion = value; }
        }

        private DateTime _t861_fmodif;
        public DateTime t861_fmodif
        {
            get { return _t861_fmodif; }
            set { _t861_fmodif = value; }
        }
        private string _Autor;
        public string Autor
        {
            get { return _Autor; }
            set { _Autor = value; }
        }
        private string _Modificador;
        public string Modificador
        {
            get { return _Modificador; }
            set { _Modificador = value; }
        }

        #endregion
        public FamiliaEntorno()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        #region Familia
        public static List<FamiliaEntorno> Catalogo()
        {
            List<FamiliaEntorno> MiLista = new List<FamiliaEntorno>();
            SqlDataReader dr = SUPER.DAL.FamiliaEntorno.Catalogo();
            FamiliaEntorno oElem;
            while (dr.Read())
            {
                oElem = new FamiliaEntorno();
                oElem.t861_idfament = int.Parse(dr["t861_idfament"].ToString());
                oElem.t861_denominacion = dr["t861_denominacion"].ToString();
                oElem.t001_idficepi_autor = int.Parse(dr["t001_idficepi_autor"].ToString());
                oElem.t861_publica = (bool)dr["t861_publica"];
                oElem.Autor = dr["Autor"].ToString();
                oElem.Modificador = dr["Modificador"].ToString();
                MiLista.Add(oElem);
            }
            dr.Close();
            dr.Dispose();

            return MiLista;
        }
        /// <summary>
        /// Obtiene las familias privadas + las públicas
        /// </summary>
        /// <returns></returns>
        public static List<FamiliaEntorno> CatalogoProfesional(int t001_idficepi, string sSoloPrivadas)
        {
            List<FamiliaEntorno> MiLista = new List<FamiliaEntorno>();
            SqlDataReader dr = SUPER.DAL.FamiliaEntorno.CatalogoProfesional(t001_idficepi, sSoloPrivadas);
            FamiliaEntorno oElem;
            while (dr.Read())
            {
                oElem = new FamiliaEntorno();
                oElem.t861_idfament = int.Parse(dr["t861_idfament"].ToString());
                oElem.t861_denominacion = dr["t861_denominacion"].ToString();
                oElem.t001_idficepi_autor = int.Parse(dr["t001_idficepi_autor"].ToString());
                oElem.t861_publica = (bool)dr["t861_publica"];
                oElem.Autor = dr["Autor"].ToString();
                oElem.Modificador = dr["Modificador"].ToString();
                MiLista.Add(oElem);
            }
            dr.Close();
            dr.Dispose();

            return MiLista;
        }
        public static void Modificar(SqlTransaction tr, int t861_idfament, string t861_denominacion, int t001_idficepi, bool t861_publica)
        {
            SUPER.DAL.FamiliaEntorno.Modificar(tr, t861_idfament, t861_denominacion, t001_idficepi, t861_publica);
        }
        public static void CambiarDenominacion(SqlTransaction tr, int t861_idfament, string t861_denominacion, int t001_idficepi)
        {
            SUPER.DAL.FamiliaEntorno.CambiarDenominacion(tr, t861_idfament, t861_denominacion, t001_idficepi);
        }
        public static int Insertar(SqlTransaction tr, string t861_denominacion, int t001_idficepi, bool t861_publica)
        {
            int idNewFamilia = SUPER.DAL.FamiliaEntorno.Insertar(tr, t861_denominacion, t001_idficepi, t861_publica);
            return idNewFamilia;
        }
        public static void Borrar(SqlTransaction tr, int t861_idfament)
        {
            SUPER.DAL.FamiliaEntorno.Borrar(tr, t861_idfament);
        }
        public static void Publicar(SqlTransaction tr, int t861_idfament, int t001_idficepi)
        {
            SUPER.DAL.FamiliaEntorno.Publicar(tr, t861_idfament, t001_idficepi);
        }
        public static void CopiarEntornos(SqlTransaction tr, int idFamOrigen, int idFamDestino)
        {
            SUPER.DAL.FamiliaEntorno.CopiarEntornos(tr, idFamOrigen, idFamDestino);
        }
        /// <summary>
        /// Comprueba si para un autor y denominación existe ya registro en la tabla de Familias de entornos
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t861_denominacion"></param>
        /// <param name="t001_idficepi"></param>
        /// <returns></returns>
        public static bool ExisteDenominacion(SqlTransaction tr, string t861_denominacion, int t001_idficepi)
        {
            bool bExiste = false;
            SqlDataReader dr = SUPER.DAL.FamiliaEntorno.SelectDenyAutor(tr, t861_denominacion, t001_idficepi);
            if (dr.Read())
                bExiste = true;
            dr.Close();
            dr.Dispose();

            return bExiste;
        }
        #endregion
        #region Entornos en la familia
        public static List<ElementoLista> CatalogoEntorno(int t861_idfament)
        {
            List<ElementoLista> MiLista = new List<ElementoLista>();
            SqlDataReader dr = SUPER.DAL.FamiliaEntorno.CatalogoEntorno(t861_idfament);
            while (dr.Read())
            {
                ElementoLista oElem = new ElementoLista(dr["T036_IDCODENTORNO"].ToString(), dr["T036_DESCRIPCION"].ToString());
                MiLista.Add(oElem);
            }
            dr.Close();
            dr.Dispose();

            return MiLista;
        }
        public static void InsertarEntorno(SqlTransaction tr, int t861_idfament, int t036_idcodentorno)
        {
            SUPER.DAL.FamiliaEntorno.InsertarEntorno(tr, t861_idfament, t036_idcodentorno);
        }
        public static void BorrarEntorno(SqlTransaction tr, int t861_idfament, int t036_idcodentorno)
        {
            SUPER.DAL.FamiliaEntorno.BorrarEntorno(tr, t861_idfament, t036_idcodentorno);
        }
        #endregion
    }
}
