using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.SIC.Models
{

    public class TareaPreventa
    {

        /// <summary>
        /// Summary description for TareaPreventa
        /// </summary>
        #region Private Variables
        private Int32 _ta207_idtareapreventa;
        private Int32 _ta204_idaccionpreventa;
        private Int32 _t001_idficepi_promotor;
        private Int32 _t001_idficepi_ultmodificador;
        private String _ta207_descripcion;
        private DateTime _ta207_fechaprevista;
        private String _ta207_estado;
        private String _ta207_motivoanulacion;
        private String _ta207_comentario;
        private Guid _uidDocumento { get; set; }
        private string _ta207_denominacion { get; set; }
        private string _ta207_observaciones { get; set; }

        #endregion

        #region Public Properties
        public Int32 ta207_idtareapreventa
        {
            get { return _ta207_idtareapreventa; }
            set { _ta207_idtareapreventa = value; }
        }

        public Int32 ta204_idaccionpreventa
        {
            get { return _ta204_idaccionpreventa; }
            set { _ta204_idaccionpreventa = value; }
        }

        public Int32 t001_idficepi_promotor
        {
            get { return _t001_idficepi_promotor; }
            set { _t001_idficepi_promotor = value; }
        }

        public Int32 t001_idficepi_ultmodificador
        {
            get { return _t001_idficepi_ultmodificador; }
            set { _t001_idficepi_ultmodificador = value; }
        }

        public String ta207_descripcion
        {
            get { return _ta207_descripcion; }
            set { _ta207_descripcion = value; }
        }

        public DateTime ta207_fechaprevista
        {
            get { return _ta207_fechaprevista; }
            set { _ta207_fechaprevista = value; }
        }

        public String ta207_estado
        {
            get { return _ta207_estado; }
            set { _ta207_estado = value; }
        }

        public String ta207_motivoanulacion
        {
            get { return _ta207_motivoanulacion; }
            set { _ta207_motivoanulacion = value; }
        }

        public String ta207_comentario
        {
            get { return _ta207_comentario; }
            set { _ta207_comentario = value; }
        }

        public Guid uidDocumento
        {
            get { return _uidDocumento; }
            set { _uidDocumento = value; }
        }

        public String ta207_denominacion
        {
            get { return _ta207_denominacion; }
            set { _ta207_denominacion = value; }
        }

        public String ta207_observaciones
        {
            get { return _ta207_observaciones; }
            set { _ta207_observaciones = value; }
        }

        public int? ta219_idtipotareapreventa { get; set; }
        public string ta219_denominacion { get; set; }
        
        #endregion

    }
}
