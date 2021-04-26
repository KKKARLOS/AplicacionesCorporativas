using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.ADM.SIC.Models
{

    public class FiguraAreaPreventa
    {

        /// <summary>
        /// Summary description for FiguraAreaPreventa
        /// </summary>
        #region Private Variables
        private Int32 _ta200_idareapreventa;
        private Int32 _t001_idficepi;
        private String _ta202_figura;

        private String _sexo;
        private String _tipoProf;
        private String _profesional;
        private Int32 _orden;
        #endregion

        #region Public Properties
        public Int32 ta200_idareapreventa
        {
            get { return _ta200_idareapreventa; }
            set { _ta200_idareapreventa = value; }
        }

        public Int32 t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        public String ta202_figura
        {
            get { return _ta202_figura; }
            set { _ta202_figura = value; }
        }


        public String sexo
        {
            get { return _sexo; }
            set { _sexo = value; }
        }
        public String tipoProf
        {
            get { return _tipoProf; }
            set { _tipoProf = value; }
        }
        public String profesional
        {
            get { return _profesional; }
            set { _profesional = value; }
        }
        public Int32 orden
        {
            get { return _orden; }
            set { _orden = value; }
        }
        #endregion

    }
}
