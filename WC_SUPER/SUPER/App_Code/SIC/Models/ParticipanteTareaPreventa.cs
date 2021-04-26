using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.SIC.Models
{

    public class ParticipanteTareaPreventa
    {

        /// <summary>
        /// Summary description for ParticipanteTareaPreventa
        /// </summary>
        #region Private Variables
        private Int32 _ta207_idtareapreventa;
        private Int32 _t001_idficepi_participante;
        private String _ta214_estado;
        private string _participante;

        #endregion

        #region Public Properties
        public Int32 ta207_idtareapreventa
        {
            get { return _ta207_idtareapreventa; }
            set { _ta207_idtareapreventa = value; }
        }

        public Int32 t001_idficepi_participante
        {
            get { return _t001_idficepi_participante; }
            set { _t001_idficepi_participante = value; }
        }

        public String ta214_estado
        {
            get { return _ta214_estado; }
            set { _ta214_estado = value; }
        }
        public String participante
        {
            get { return _participante; }
            set { _participante = value; }
        }


        #endregion

    }
}
