using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.SIC.Models
{

    public class FiguraSubareaPreventa
    {

        /// <summary>
        /// Summary description for FiguraSubareaPreventa
        /// </summary>
        #region Private Variables
        private Int32 _ta201_idsubareapreventa;
        private Int32 _t001_idficepi;
        private String _ta203_figura;

        #endregion

        #region Public Properties
        public Int32 ta201_idsubareapreventa
        {
            get { return _ta201_idsubareapreventa; }
            set { _ta201_idsubareapreventa = value; }
        }

        public Int32 t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        public String ta203_figura
        {
            get { return _ta203_figura; }
            set { _ta203_figura = value; }
        }


        #endregion

    }
}
