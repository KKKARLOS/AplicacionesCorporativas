using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.Progress.Models
{

    public class ArbolDependencias
    {

        /// <summary>
        /// Summary description for Árbol de dependencias
        /// </summary>
        #region Private Variables

        private Int32 _idficepievaluador;
        private Int32 _idficepievaluado;
        private string _evaluador;
        private string _evaluado;
        private Nullable <Int32> _evaluadordelevaluador;
        private string _roldelevaluador;
        private string _roldelevaluado;
        private bool _elevaluadotienehijos;                
        private bool _evaluadorpotencial;
        private bool _evaluadoryoenibermatica;
        private bool _evaluadorrealizapruebas;

        private int _evaluadorconvocadoapruebas;
        private int _evaluadoconvocadoapruebas;
        
        private bool _evaluadopotencial;
        private bool _evaluadoyoenibermatica;
        private bool _evaluadorealizapruebas;

        private bool _pruebasrealizadas;
        private String _t153_nombre;
        private Nullable<DateTime> _potfecha;
        private String _potorigen;
        private string _sexo;
        private int _colectivoevaluador;
        private int _colectivoevaluado;
        private bool _realizapruebas;
        private bool _espotencial;
        private int _convocadoapruebas;


        public string Sexo
        {
            get { return _sexo; }
            set { _sexo = value; }
        }
        
        
        

        #endregion

        #region Public Properties


        public String Evaluador
        {
            get { return _evaluador; }
            set { _evaluador = value; }
        }

        public String Evaluado
        {
            get { return _evaluado; }
            set { _evaluado = value; }
        }

        public Nullable<Int32> Evaluadordelevaluador
        {
            get { return _evaluadordelevaluador; }
            set { _evaluadordelevaluador = value; }
        }


        public Int32 idficepievaluador
        {
            get { return _idficepievaluador; }
            set { _idficepievaluador = value; }
        }

        public Int32 idficepievaluado
        {
            get { return _idficepievaluado; }
            set { _idficepievaluado = value; }
        }

        public String Roldelevaluador
        {
            get { return _roldelevaluador; }
            set { _roldelevaluador = value; }
        }

        public String Roldelevaluado
        {
            get { return _roldelevaluado; }
            set { _roldelevaluado = value; }
        }

        public bool Elevaluadotienehijos
        {
            get { return _elevaluadotienehijos; }
            set { _elevaluadotienehijos = value; }
        }

        public bool Evaluadorpotencial
        {
            get { return _evaluadorpotencial; }
            set { _evaluadorpotencial = value; }
        }

        public bool Evaluadoryoenibermatica
        {
            get { return _evaluadoryoenibermatica; }
            set { _evaluadoryoenibermatica = value; }
        }

        public bool Evaluadorrealizapruebas
        {
            get { return _evaluadorrealizapruebas; }
            set { _evaluadorrealizapruebas = value; }
        }

        public bool Evaluadopotencial
        {
            get { return _evaluadopotencial; }
            set { _evaluadopotencial = value; }
        }

        public bool Evaluadoyoenibermatica
        {
            get { return _evaluadoyoenibermatica; }
            set { _evaluadoyoenibermatica = value; }
        }

        public bool Evaluadorealizapruebas
        {
            get { return _evaluadorealizapruebas; }
            set { _evaluadorealizapruebas = value; }
        }

        public bool Pruebasrealizadas
        {
            get { return _pruebasrealizadas; }
            set { _pruebasrealizadas = value; }
        }

        public Nullable<DateTime> Potfecha
        {
            get { return _potfecha; }
            set { _potfecha = value; }
        }

        public String Potorigen
        {
            get { return _potorigen; }
            set { _potorigen = value; }
        }


        private int _t2_iddocumento;

        public int T2_iddocumento
        {
            get { return _t2_iddocumento; }
            set { _t2_iddocumento = value; }
        }

        public String T153_nombre
        {
            get { return _t153_nombre; }
            set { _t153_nombre = value; }
        }

        public int Evaluadorconvocadoapruebas
        {
            get { return _evaluadorconvocadoapruebas; }
            set { _evaluadorconvocadoapruebas = value; }
        }

        public int Evaluadoconvocadoapruebas
         {
             get { return _evaluadoconvocadoapruebas; }
             set { _evaluadoconvocadoapruebas = value; }
         }

         public Int32 colectivoevaluador
         {
             get { return _colectivoevaluador; }
             set { _colectivoevaluador = value; }
         }

         public Int32 colectivoevaluado
         {
             get { return _colectivoevaluado; }
             set { _colectivoevaluado = value; }
         }

         public bool Realizapruebas
         {
             get { return _realizapruebas; }
             set { _realizapruebas = value; }
         }

         public bool esPotencial
         {
             get { return _espotencial; }
             set { _espotencial = value; }
         }

         public Int32 Convocadoapruebas
         {
             get { return _convocadoapruebas; }
             set { _convocadoapruebas = value; }
         }
        
        
        #endregion

    }


}
