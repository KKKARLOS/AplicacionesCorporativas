using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.SIC.Models
{

    public class DocumentacionPreventa
    {

        /// <summary>
        /// Summary description for DocumentacionPreventa
        /// </summary>
        #region Private Variables
        private Int32 _ta210_iddocupreventa;
        private Int64? _t2_iddocumento;
        private String _ta210_destino;
        private String _ta210_descripcion;
        private String _ta210_nombrefichero;
        private Int32 _ta210_kbytes;
        private DateTime? _ta210_fechamod;
        private Int32? _ta204_idaccionpreventa;
        private Int32? _ta207_idtareapreventa;
        private Int32 _t001_idficepi_autor;
        private Byte _ta211_idtipodocumento;
        private Guid? _ta210_guidprovisional;
        private string _ta211_denominacion;
        private string _autor;
        private string _ta207_denominacion;
        private bool _fileupdated;
        private string _origenEdicion;
        private string _estado;

        #endregion

        #region Public Properties
        public Int32 ta210_iddocupreventa
        {
            get { return _ta210_iddocupreventa; }
            set { _ta210_iddocupreventa = value; }
        }

        public Int64? t2_iddocumento
        {
            get { return _t2_iddocumento; }
            set { _t2_iddocumento = value; }
        }

        public String ta210_destino
        {
            get { return _ta210_destino; }
            set { _ta210_destino = value; }
        }

        public String ta210_descripcion
        {
            get { return _ta210_descripcion; }
            set { _ta210_descripcion = value; }
        }

        public String ta210_nombrefichero
        {
            get { return _ta210_nombrefichero; }
            set { _ta210_nombrefichero = value; }
        }

        public Int32 ta210_kbytes
        {
            get { return _ta210_kbytes; }
            set { _ta210_kbytes = value; }
        }

        public DateTime? ta210_fechamod
        {
            get { return _ta210_fechamod; }
            set { _ta210_fechamod = value; }
        }

        public Int32? ta204_idaccionpreventa
        {
            get { return _ta204_idaccionpreventa; }
            set { _ta204_idaccionpreventa = value; }
        }

        public Int32? ta207_idtareapreventa
        {
            get { return _ta207_idtareapreventa; }
            set { _ta207_idtareapreventa = value; }
        }

        public Int32 t001_idficepi_autor
        {
            get { return _t001_idficepi_autor; }
            set { _t001_idficepi_autor = value; }
        }

        public Byte ta211_idtipodocumento
        {
            get { return _ta211_idtipodocumento; }
            set { _ta211_idtipodocumento = value; }
        }

        public Guid? ta210_guidprovisional
        {
            get { return _ta210_guidprovisional; }
            set { _ta210_guidprovisional = value; }
        }

        public String autor
        {
            get { return _autor; }
            set { _autor = value; }
        }

        public String ta211_denominacion
        {
            get { return _ta211_denominacion; }
            set { _ta211_denominacion = value; }
        }

        public String ta207_denominacion
        {
            get { return _ta207_denominacion; }
            set { _ta207_denominacion = value; }
        }

        public bool fileupdated
        {
            get { return _fileupdated; }
            set { _fileupdated = value; }
        }

        public String origenEdicion
        {
            get { return _origenEdicion; }
            set { _origenEdicion = value; }
        }

        public String estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        

        #endregion

    }
}
