using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.Progress.Models
{

    public class ComunidadProgress
    {
        public ComunidadProgress()
        {
            _selectPersonas = new List<Personas>();
            _selectPersonas2 = new List<Personas>();
            _selectCR = new List<CR>();
            _selectCR2 = new List<CR>();
            _selectEmpresas = new List<Empresas>();
            _selectEmpresas2 = new List<Empresas>();
            _selectPersonasForzadas = new List<Personas>();
            _selectPersonasForzadas2 = new List<Personas>();
        }


        #region Private Variables

        private List<Personas> _selectPersonas;
        private List<Personas> _selectPersonas2;
        private List<CR> _selectCR;
        private List<CR> _selectCR2;
        private List<Empresas> _selectEmpresas;
        private List<Empresas> _selectEmpresas2;
        private List<Personas> _selectPersonasForzadas;
        private List<Personas> _selectPersonasForzadas2;

        #endregion

        #region Public Properties

        public List<Personas> SelectPersonas
        {
            get { return _selectPersonas; }
            set { _selectPersonas = value; }
        }

        public List<Personas> SelectPersonas2
        {
            get { return _selectPersonas2; }
            set { _selectPersonas2 = value; }
        }

        public List<CR> SelectCR
        {
            get { return _selectCR; }
            set { _selectCR = value; }
        }

        public List<CR> SelectCR2
        {
            get { return _selectCR2; }
            set { _selectCR2 = value; }
        }

        public List<Empresas> SelectEmpresas
        {
            get { return _selectEmpresas; }
            set { _selectEmpresas = value; }
        }

        public List<Empresas> SelectEmpresas2
        {
            get { return _selectEmpresas2; }
            set { _selectEmpresas2 = value; }
        }

        public List<Personas> SelectPersonasForzadas
        {
            get { return _selectPersonasForzadas; }
            set { _selectPersonasForzadas = value; }
        }

        public List<Personas> SelectPersonasForzadas2
        {
            get { return _selectPersonasForzadas2; }
            set { _selectPersonasForzadas2 = value; }
        }

        #endregion


    }



    public class Personas
    {
        private short _t001_idficepi;
        private string _profesional;
        private int _t946_modo;
        private bool _t946_modos;

        public short T001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        public string Profesional
        {
            get { return _profesional; }
            set { _profesional = value; }
        }

        public int T946_modo
        {
            get { return _t946_modo; }
            set { _t946_modo = value; }
        }

        public bool T946_modos
        {
            get { return _t946_modos; }
            set { _t946_modos = value; }
        }


    }


    public class CR
    {

        private short _t303_idnodo;
        private string _t303_denominacion;

        public short T303_idnodo
        {
            get { return _t303_idnodo; }
            set { _t303_idnodo = value; }
        }

        public string T303_denominacion
        {
            get { return _t303_denominacion; }
            set { _t303_denominacion = value; }
        }
        
    }

     public class Empresas
    {

        private short _t313_idempresa;
        private string _t313_denominacion;

        public short T313_idempresa
        {
            get { return _t313_idempresa; }
            set { _t313_idempresa = value; }
        }

        public string T313_denominacion
        {
            get { return _t313_denominacion; }
            set { _t313_denominacion = value; }
        }
        
    }


    //public class ComunidadProgress
    //{
        
    //    public class Personas
    //    {

    //        private short _t001_idficepi;
    //        private string _profesional;

    //        public short T001_idficepi
    //        {
    //            get { return _t001_idficepi; }
    //            set { _t001_idficepi = value; }
    //        }

    //        public string Profesional
    //        {
    //            get { return _profesional; }
    //            set { _profesional = value; }
    //        }

    //    }



    //    public class CR
    //    {
           

    //    }

    //    #region empresas
    //    public class Empresas
    //    {
           
    //    }
    //    #endregion


    //}




}
