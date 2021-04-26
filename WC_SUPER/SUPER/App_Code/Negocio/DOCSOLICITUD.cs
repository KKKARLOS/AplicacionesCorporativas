using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de DOCSOLICITUD
    /// </summary>
    public class DOCSOLICITUD
    {
        #region Propiedades y Atributos

        private int _t697_iddoc;
        public int t697_iddoc
        {
            get { return _t697_iddoc; }
            set { _t697_iddoc = value; }
        }

        private int _t696_id;
        public int t696_id
        {
            get { return _t696_id; }
            set { _t696_id = value; }
        }

        private string _t697_descripcion;
        public string t697_descripcion
        {
            get { return _t697_descripcion; }
            set { _t697_descripcion = value; }
        }

        private string _t697_nombrearchivo;
        public string t697_nombrearchivo
        {
            get { return _t697_nombrearchivo; }
            set { _t697_nombrearchivo = value; }
        }

        private byte[] _t697_archivo;
        public byte[] t697_archivo
        {
            get { return _t697_archivo; }
            set { _t697_archivo = value; }
        }

        private int _t001_idficepi_autor;
        public int t001_idficepi_autor
        {
            get { return _t001_idficepi_autor; }
            set { _t001_idficepi_autor = value; }
        }

        private DateTime _t697_fecha;
        public DateTime t697_fecha
        {
            get { return _t697_fecha; }
            set { _t697_fecha = value; }
        }

        private int _t001_idficepi_modif;
        public int t001_idficepi_modif
        {
            get { return _t001_idficepi_modif; }
            set { _t001_idficepi_modif = value; }
        }

        private DateTime _t697_fechamodif;
        public DateTime t697_fechamodif
        {
            get { return _t697_fechamodif; }
            set { _t697_fechamodif = value; }
        }

        private string _DesAutor;
        public string DesAutor
        {
            get { return _DesAutor; }
            set { _DesAutor = value; }
        }

        private string _DesAutorModif;
        public string DesAutorModif
        {
            get { return _DesAutorModif; }
            set { _DesAutorModif = value; }
        }
        private long? _t2_iddocumento;
        public long? t2_iddocumento
        {
            get { return _t2_iddocumento; }
            set { _t2_iddocumento = value; }
        }

        #endregion

        public DOCSOLICITUD()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T697_DOC_SOLICITUD, y devuelve una instancia u objeto del tipo DOCSOLICITUD
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	18/02/2014 10:14:49
        /// </history>
        /// -----------------------------------------------------------------------------
        public static DOCSOLICITUD Select(SqlTransaction tr, int t697_iddoc)//, bool bTraerArchivo)
        {
            DOCSOLICITUD o = new DOCSOLICITUD();
            SqlDataReader dr = SUPER.DAL.DOCSOLICITUD.Select(tr, t697_iddoc);//, bTraerArchivo);
            if (dr.Read())
            {
                if (dr["t697_iddoc"] != DBNull.Value)
                    o.t697_iddoc = int.Parse(dr["t697_iddoc"].ToString());
                if (dr["t696_id"] != DBNull.Value)
                    o.t696_id = int.Parse(dr["t696_id"].ToString());
                if (dr["t697_descripcion"] != DBNull.Value)
                    o.t697_descripcion = (string)dr["t697_descripcion"];
                if (dr["t697_nombrearchivo"] != DBNull.Value)
                    o.t697_nombrearchivo = (string)dr["t697_nombrearchivo"];
                //if (bTraerArchivo)
                //{
                //    if (dr["t2_iddocumento"].ToString() == "")
                //    {
                //        SUPER.BLL.ContentServer oCS = new SUPER.BLL.ContentServer(long.Parse(dr["t2_iddocumento"].ToString()));
                //        o.sError = oCS.Error;
                //        o.t697_archivo = oCS.Archivo;
                //    }
                //    else
                //        o.sError = "La clave para identificar el documento en el Content-Server es vacía";
                //}
                if (dr["t001_idficepi_autor"] != DBNull.Value)
                    o.t001_idficepi_autor = int.Parse(dr["t001_idficepi_autor"].ToString());
                if (dr["t697_fecha"] != DBNull.Value)
                    o.t697_fecha = (DateTime)dr["t697_fecha"];
                if (dr["t001_idficepi_modif"] != DBNull.Value)
                    o.t001_idficepi_modif = int.Parse(dr["t001_idficepi_modif"].ToString());
                if (dr["t697_fechamodif"] != DBNull.Value)
                    o.t697_fechamodif = (DateTime)dr["t697_fechamodif"];
                if (dr["autor"] != DBNull.Value)
                    o.DesAutor = (string)dr["autor"];
                if (dr["autormodif"] != DBNull.Value)
                    o.DesAutorModif = dr["autormodif"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato del documento de solicitud de certificado"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
        public static int Insert(SqlTransaction tr, Nullable<int> t696_id, string t697_descripcion, string t697_nombrearchivo,
                                 Nullable<long> idContentServer, int t001_idficepi_autor, string t697_usuticks)
        {
            return SUPER.DAL.DOCSOLICITUD.Insert(tr, t696_id, t697_descripcion, t697_nombrearchivo, idContentServer, t001_idficepi_autor, t697_usuticks);
        }

        public static int Update(SqlTransaction tr, int t696_id, int t697_iddoc, string t697_descripcion, string t697_nombrearchivo,
                                 Nullable<long> idContentServer,int t001_idficepi_modif)
        {
            return SUPER.DAL.DOCSOLICITUD.Update(tr, t696_id, t697_iddoc, t697_descripcion, t697_nombrearchivo, idContentServer,t001_idficepi_modif);
        }
        public static int Delete(SqlTransaction tr, int t697_iddoc)
        {
            return SUPER.DAL.DOCSOLICITUD.Delete(tr, t697_iddoc);
        }
        public static int DeleteByUsuTicks(SqlTransaction tr, string t697_usuticks)
        {
            return SUPER.DAL.DOCSOLICITUD.DeleteByUsuTicks(tr, t697_usuticks);
        }

        /// <summary>
        /// Obtiene el catálogo de documentos asociados a una solicitud
        /// </summary>
        /// <param name="t696_id"></param>
        /// <returns></returns>
        public static SqlDataReader Catalogo(int t696_id)
        {
            return SUPER.DAL.DOCSOLICITUD.Catalogo(t696_id);
        }
        /// <summary>
        /// Obtiene el catalogo de documentos asociados a una solicitud que todavía no se ha grabado
        /// </summary>
        /// <param name="t624_usuticks"></param>
        /// <returns></returns>
        public static SqlDataReader CatalogoByUsuTicks(string t697_usuticks)
        {
            return SUPER.DAL.DOCSOLICITUD.CatalogoByUsuTicks(t697_usuticks);
        }

    }
}