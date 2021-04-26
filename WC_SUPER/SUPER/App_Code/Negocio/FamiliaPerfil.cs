using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.Data.SqlClient;

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de FamiliaPerfil
    /// </summary>
    public class FamiliaPerfil
    {
        #region Propiedades y Atributos

        private int _t859_idfamperfil;
        public int t859_idfamperfil
        {
            get { return _t859_idfamperfil; }
            set { _t859_idfamperfil = value; }
        }

        private string _t859_denominacion;
        public string t859_denominacion
        {
            get { return _t859_denominacion; }
            set { _t859_denominacion = value; }
        }

        private bool _t859_publica;
        public bool t859_publica
        {
            get { return _t859_publica; }
            set { _t859_publica = value; }
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

        private DateTime _t859_fcreacion;
        public DateTime t859_fcreacion
        {
            get { return _t859_fcreacion; }
            set { _t859_fcreacion = value; }
        }

        private DateTime _t859_fmodificacion;
        public DateTime t859_fmodificacion
        {
            get { return _t859_fmodificacion; }
            set { _t859_fmodificacion = value; }
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
        public FamiliaPerfil()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        #region Familia
        public static List<FamiliaPerfil> Catalogo()
        {
            List<FamiliaPerfil> MiLista = new List<FamiliaPerfil>();
            SqlDataReader dr = SUPER.DAL.FamiliaPerfil.Catalogo();
            FamiliaPerfil oElem;
            while (dr.Read())
            {
                oElem = new FamiliaPerfil();
                oElem.t859_idfamperfil = int.Parse(dr["t859_idfamperfil"].ToString());
                oElem.t859_denominacion = dr["t859_denominacion"].ToString();
                oElem.t001_idficepi_autor = int.Parse(dr["t001_idficepi_autor"].ToString());
                oElem.t859_publica = (bool)dr["t859_publica"];
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
        public static List<FamiliaPerfil> CatalogoProfesional(int t001_idficepi, string sSoloPrivadas)
        {
            List<FamiliaPerfil> MiLista = new List<FamiliaPerfil>();
            SqlDataReader dr = SUPER.DAL.FamiliaPerfil.CatalogoProfesional(t001_idficepi, sSoloPrivadas);
            FamiliaPerfil oElem;
            while (dr.Read())
            {
                oElem = new FamiliaPerfil();
                oElem.t859_idfamperfil = int.Parse(dr["t859_idfamperfil"].ToString());
                oElem.t859_denominacion = dr["t859_denominacion"].ToString();
                oElem.t001_idficepi_autor = int.Parse(dr["t001_idficepi_autor"].ToString());
                oElem.t859_publica = (bool)dr["t859_publica"];
                oElem.Autor = dr["Autor"].ToString();
                oElem.Modificador = dr["Modificador"].ToString();
                MiLista.Add(oElem);
            }
            dr.Close();
            dr.Dispose();

            return MiLista;
        }
        public static void Modificar(SqlTransaction tr, int t859_idfamperfil, string t859_denominacion, int t001_idficepi, bool t859_publica)
        {
            SUPER.DAL.FamiliaPerfil.Modificar(tr, t859_idfamperfil, t859_denominacion, t001_idficepi, t859_publica);
        }
        public static void CambiarDenominacion(SqlTransaction tr, int t859_idfamperfil, string t859_denominacion, int t001_idficepi)
        {
            SUPER.DAL.FamiliaPerfil.CambiarDenominacion(tr, t859_idfamperfil, t859_denominacion, t001_idficepi);
        }
        public static int Insertar(SqlTransaction tr, string t859_denominacion, int t001_idficepi, bool t859_publica)
        {
            if (t859_denominacion.Length > 50)
                t859_denominacion = t859_denominacion.Substring(0, 50);
            int idNewFamilia = SUPER.DAL.FamiliaPerfil.Insertar(tr, t859_denominacion, t001_idficepi, t859_publica);
            return idNewFamilia;
        }
        public static void Borrar(SqlTransaction tr, int t859_idfamperfil)
        {
            SUPER.DAL.FamiliaPerfil.Borrar(tr, t859_idfamperfil);
        }
        public static void Publicar(SqlTransaction tr, int t859_idfamperfil, int t001_idficepi)
        {
            SUPER.DAL.FamiliaPerfil.Publicar(tr, t859_idfamperfil, t001_idficepi);
        }
        public static void CopiarPerfiles(SqlTransaction tr, int idFamOrigen, int idFamDestino)
        {
            SUPER.DAL.FamiliaPerfil.CopiarPerfiles(tr, idFamOrigen, idFamDestino);
        }
        /// <summary>
        /// Comprueba si para un autor y denominación existe ya registro en la tabla de Familias de perfiles
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t859_denominacion"></param>
        /// <param name="t001_idficepi"></param>
        /// <returns></returns>
        public static bool ExisteDenominacion(SqlTransaction tr, string t859_denominacion, int t001_idficepi)
        {
            bool bExiste = false;
            SqlDataReader dr = SUPER.DAL.FamiliaPerfil.SelectDenyAutor(tr, t859_denominacion, t001_idficepi);
            if (dr.Read())
                bExiste = true;
            dr.Close();
            dr.Dispose();
            
            return bExiste;
        }
        #endregion
        #region Perfil en la familia
        public static List<ElementoLista> CatalogoPerfil(int t859_idfamperfil)
        {
            List<ElementoLista> MiLista = new List<ElementoLista>();
            SqlDataReader dr = SUPER.DAL.FamiliaPerfil.CatalogoPerfil(t859_idfamperfil);
            while (dr.Read())
            {
                ElementoLista oElem = new ElementoLista(dr["T035_IDCODPERFIL"].ToString(), dr["T035_DESCRIPCION"].ToString());
                MiLista.Add(oElem);
            }
            dr.Close();
            dr.Dispose();

            return MiLista;
        }
        public static void InsertarPerfil(SqlTransaction tr, int t859_idfamperfil, int t035_idcodperfil)
        {
            SUPER.DAL.FamiliaPerfil.InsertarPerfil(tr, t859_idfamperfil, t035_idcodperfil);
        }
        public static void BorrarPerfil(SqlTransaction tr, int t859_idfamperfil, int t035_idcodperfil)
        {
            SUPER.DAL.FamiliaPerfil.BorrarPerfil(tr, t859_idfamperfil, t035_idcodperfil);
        }
        #endregion
    }
}