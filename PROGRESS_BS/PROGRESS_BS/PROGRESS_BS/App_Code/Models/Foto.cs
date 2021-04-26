using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.Progress.Models
{
    public class Foto
    {
        #region Private Variables
        private Int16 _t932_idfoto;
        private string _t932_denominacion;
        private DateTime _t932_fechafoto;
        private Int32 _t001_idficepi_creador;
        private string _nombre;

        #endregion

        #region Public Properties
        public Int16 t932_idfoto
        {
            get { return _t932_idfoto; }
            set { _t932_idfoto = value; }
        }

        public string t932_denominacion
        {
            get { return _t932_denominacion; }
            set { _t932_denominacion = value; }
        }

        public DateTime t932_fechafoto
        {
            get { return _t932_fechafoto; }
            set { _t932_fechafoto = value; }
        }

        public Int32 t001_idficepi_creador
        {
            get { return _t001_idficepi_creador; }
            set { _t001_idficepi_creador = value; }
        }

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        
        #endregion
    }
}
