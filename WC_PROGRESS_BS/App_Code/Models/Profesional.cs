
using System.Collections.Generic;
namespace IB.Progress.Models
{
    [System.Serializable]
    public class Profesional
    {

        /// <summary>
        /// Summary description for profesional
        /// </summary>
        #region Private Variables
        private int _t001_idficepi;
        private string _sexo;

        public string Sexo
        {
            get { return _sexo; }
            set { _sexo = value; }
        }

        private string _t001_nombre;

        public string T001_nombre
        {
            get { return _t001_nombre; }
            set { _t001_nombre = value; }
        }

        private string _nombreapellidosprofesional;
        private bool _validoEvalProgress;
        private int _t001_evalprogress;
        private string _nombre;
        private string _nombrecorto;
        private string _nombrelargo;
        private bool _bIdentificado;
        private int _t303_idnodo;
        private string _correo;
        private string _t001_correointext;
        private string _nombreprofesional;
        private string _correo_profesional;
        private string _codred;
        private List<string> _roles;
        private string _t004_desrol;
        private string _t001_apellido1;
        private string _t001_apellido2;

        public string Codred
        {
            get { return _codred; }
            set { _codred = value; }
        }
        

        public string T001_correointext
        {
            get { return _t001_correointext; }
            set { _t001_correointext = value; }
        }

        public string Correo
        {
            get { return _correo; }
            set { _correo = value; }
        }

        public int T303_idnodo
        {
            get { return _t303_idnodo; }
            set { _t303_idnodo = value; }
        }
        private string _t303_denominacion;

        public string T303_denominacion
        {
            get { return _t303_denominacion; }
            set { _t303_denominacion = value; }
        }

        public List<string> roles
        {
            get { return _roles; }
            set { _roles = value; }
        }
        

        #endregion

        #region Public Properties
        public int t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        public int t001_evalprogress
        {
            get { return _t001_evalprogress; }
            set { _t001_evalprogress = value; }
        }

        public bool validoEvalProgress
        {
            get { return _validoEvalProgress; }
            set { _validoEvalProgress = value; }
        }

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string nombreprofesional
        {
            get { return _nombreprofesional; }
            set { _nombreprofesional = value; }
        }

        public string correo_profesional
        {
            get { return _correo_profesional; }
            set { _correo_profesional = value; }
        }

        public string nombreapellidosprofesional
        {
            get { return _nombreapellidosprofesional; }
            set { _nombreapellidosprofesional = value; }
        }

        
        public string nombrecorto
        {
            get { return _nombrecorto; }
            set { _nombrecorto = value; }
        }

        public string nombrelargo
        {
            get { return _nombrelargo; }
            set { _nombrelargo = value; }
        }

        public bool bIdentificado
        {
            get { return _bIdentificado; }
            set { _bIdentificado = value; }
        }

        public string T004_desrol
        {
            get { return _t004_desrol; }
            set { _t004_desrol = value; }
        }

        public string t001_apellido1
        {
            get { return _t001_apellido1; }
            set { _t001_apellido1 = value; }
        }

        public string t001_apellido2
        {
            get { return _t001_apellido2; }
            set { _t001_apellido2 = value; }
        }
        
        #endregion

    }
}
