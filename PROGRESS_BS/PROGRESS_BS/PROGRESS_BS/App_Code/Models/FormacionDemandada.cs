using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.Progress.Models
{
    /// <summary>
    /// Descripción breve de TramitarSalidas
    /// </summary>
    public class FormacionDemandada
    {
        public FormacionDemandada()
        {
            _formacionDemandadaS1 = new List<FormacionDemandadaSelect1>();
        }


        #region Private Variables

        private List<FormacionDemandadaSelect1> _formacionDemandadaS1;
        private int _nevaluaciones;

        #endregion

        #region Public Properties


        public int Nevaluciones
        {
            get { return _nevaluaciones; }
            set { _nevaluaciones = value; }
        }

        public List<FormacionDemandadaSelect1> FormacionDemandadaS1
        {
            get { return _formacionDemandadaS1; }
            set { _formacionDemandadaS1 = value; }
        }


        #endregion

       
    }



    public class FormacionDemandadaSelect1
    {

       
        #region Private Variables

        private int _t930_idvaloracion;
        private string _evaluador;
        private string _evaluado;
        private string _formacion;
        private int _idformulario;
        

        #endregion

        #region Public Properties



        public int T930_idvaloracion
        {
            get { return _t930_idvaloracion; }
            set { _t930_idvaloracion = value; }
        }

        public string Evaluador
        {
            get { return _evaluador; }
            set { _evaluador = value; }
        }

        public string Evaluado
        {
            get { return _evaluado; }
            set { _evaluado = value; }
        }

        public string Formacion
        {
            get { return _formacion; }
            set { _formacion = value; }
        }

        public int idformulario
        {
            get { return _idformulario; }
            set { _idformulario = value; }
        }
       
        #endregion

    }


}