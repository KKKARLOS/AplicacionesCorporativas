using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.APP.Models
{

    public class FIGURAPROYECTOSUBNODO
    {

        /// <summary>
        /// Summary description for FIGURAPROYECTOSUBNODO
        /// </summary>
        #region Private Variables
        private Int32 _t305_idproyectosubnodo;
        private Int32 _t314_idusuario;
        private String _t310_figura;

        #endregion

        #region Public Properties
        public Int32 t305_idproyectosubnodo
        {
            get { return _t305_idproyectosubnodo; }
            set { _t305_idproyectosubnodo = value; }
        }

        public Int32 t314_idusuario
        {
            get { return _t314_idusuario; }
            set { _t314_idusuario = value; }
        }

        public String t310_figura
        {
            get { return _t310_figura; }
            set { _t310_figura = value; }
        }


        #endregion

    }
}
