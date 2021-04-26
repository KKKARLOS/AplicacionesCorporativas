using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.Progress.Models
{

    public class DesgloseRol
    {

        /// <summary>
        /// Summary description for DesgloseRol
        /// </summary>
        #region Private Variables

        private Int32 _t001_idficepi;        
        private string _profesional;
        private string _nombrecorto;
        private string _tipo;
        private Int32 _parentesco;               
        private Int32 _t004_idrol_actual;
        private string _desRol;
        
        #endregion

        #region Public Properties
       

        public String Profesional
        {
            get { return _profesional; }
            set { _profesional = value; }
        }

        public String Nombrecorto
        {
            get { return _nombrecorto; }
            set { _nombrecorto = value; }
        }

        public String Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public Int32 t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

       
        public Int32 t004_idrol_actual
        {
            get { return _t004_idrol_actual; }
            set { _t004_idrol_actual = value; }
        }

        public Int32 Parentesco
        {
            get { return _parentesco; }
            set { _parentesco = value; }
        }

        public String DesRol
        {
            get { return _desRol; }
            set { _desRol = value; }
        }

        #endregion

    }

    
}
