using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.SIC.Models
{

    public class EjecutorTareaPreventa
    {

        /// <summary>
        /// Summary description for EjecutorTareaPreventa
        /// </summary>
        #region Private Variables
        private Int32 _ta207_idtareapreventa;
        private Int32 _t001_idficepi_ejecutor;
        private String _ta214_estado;

        #endregion

        #region Public Properties
        public Int32 ta207_idtareapreventa
        {
            get { return _ta207_idtareapreventa; }
            set { _ta207_idtareapreventa = value; }
        }

        public Int32 t001_idficepi_ejecutor
        {
            get { return _t001_idficepi_ejecutor; }
            set { _t001_idficepi_ejecutor = value; }
        }

        public String ta214_estado
        {
            get { return _ta214_estado; }
            set { _ta214_estado = value; }
        }


        #endregion

    }
}
