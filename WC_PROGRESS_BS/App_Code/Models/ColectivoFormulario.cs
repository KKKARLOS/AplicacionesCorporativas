using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.Progress.Models
{
    public class ColectivoFormulario
    {
        public ColectivoFormulario()
        {
            _select2 = new List<Colectivo>();
            _select1 = new List<ModeloFormulario>();
            _selectForzadas1 = new List<Colectivo>();
            _selectForzadas2 = new List<Colectivo>();
            _selectForzadas3 = new List<Colectivo>();
        }

        private List<Colectivo> _select2;
        private List<Colectivo> _selectForzadas1;

        
        private List<Colectivo> _selectForzadas2;
        private List<Colectivo> _selectForzadas3;
        private List<ModeloFormulario> _select1;

        public List<Colectivo> Select2
        {
            get { return _select2; }
            set { _select2 = value; }
        }

        public List<ModeloFormulario> Select1
        {
            get { return _select1; }
            set { _select1 = value; }
        }

        public List<Colectivo> SelectForzadas1
        {
            get { return _selectForzadas1; }
            set { _selectForzadas1 = value; }
        }

        public List<Colectivo> SelectForzadas2
        {
            get { return _selectForzadas2; }
            set { _selectForzadas2 = value; }
        }

        public List<Colectivo> SelectForzadas3
        {
            get { return _selectForzadas3; }
            set { _selectForzadas3 = value; }
        }

    }



    public class ModeloFormulario
    {
        private int _t934_idmodeloformulario;

        public int T934_idmodeloformulario
        {
            get { return _t934_idmodeloformulario; }
            set { _t934_idmodeloformulario = value; }
        }
        private string _t934_denominacion;

        public string T934_denominacion
        {
            get { return _t934_denominacion; }
            set { _t934_denominacion = value; }
        }

    }



}