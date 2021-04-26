using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.Progress.Models
{

    public class Perfiles
    {
        public Perfiles()
        {
            _selectPersonas = new List<PersonasPerfiles>();
            _selectPersonas2 = new List<PersonasPerfiles>();
            _selectPersonas3 = new List<PersonasPerfiles>();
            _selectPersonas4 = new List<PersonasPerfiles>();
            
            
        }


        #region Private Variables

        private List<PersonasPerfiles> _selectPersonas;
        private List<PersonasPerfiles> _selectPersonas2;
        private List<PersonasPerfiles> _selectPersonas3;
        private List<PersonasPerfiles> _selectPersonas4;
        
        

        #endregion

        #region Public Properties

        public List<PersonasPerfiles> SelectPersonas
        {
            get { return _selectPersonas; }
            set { _selectPersonas = value; }
        }

        public List<PersonasPerfiles> SelectPersonas2
        {
            get { return _selectPersonas2; }
            set { _selectPersonas2 = value; }
        }

        public List<PersonasPerfiles> SelectPersonas3
        {
            get { return _selectPersonas3; }
            set { _selectPersonas3 = value; }
        }

        public List<PersonasPerfiles> SelectPersonas4
        {
            get { return _selectPersonas4; }
            set { _selectPersonas4 = value; }
        }

     
        #endregion


    }



    public class PersonasPerfiles
    {
        private short _t001_idficepi;
        private string _profesional;
        private string _t939_figura;

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

        public string T939_figura
        {
            get { return _t939_figura; }
            set { _t939_figura = value; }
        }
    }


   

}
