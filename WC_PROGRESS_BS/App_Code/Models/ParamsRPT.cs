using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.Progress.Models
{
    /// <summary>
    /// Descripción breve de Estadisticas
    /// </summary>
    /// 
    [Serializable]
    public class ParamsRPT
    {
        public ParamsRPT()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        //PROPIEDADES 

        private string _fecDesde;
        private string _fecHasta;
        private string _txtSituacion;
        private string _sexo;
        private string _txtEvaluador;
        private string _txtProfundizacion;
        private string _txtColectivo;       
        private string _txtCR_Evaluaciones;
        private string _txtCR_Evaluadores;
        private string _txtCR_Profesionales;
        private string _txtEstado;
        public string txtColectivoProgress { get; set; }


        public string fecDesde
        {
            get { return _fecDesde; }
            set { _fecDesde = value; }
        }

        public string fecHasta
        {
            get { return _fecHasta; }
            set { _fecHasta = value; }
        }

        public string txtSituacion
        {
            get { return _txtSituacion; }
            set { _txtSituacion = value; }
        }

        public string sexo
        {
            get { return _sexo; }
            set { _sexo = value; }
        }

        public string txtEvaluador
        {
            get { return _txtEvaluador; }
            set { _txtEvaluador = value; }
        }

        public string txtProfundizacion
        {
            get { return _txtProfundizacion; }
            set { _txtProfundizacion = value; }
        }

        public string txtColectivo
        {
            get { return _txtColectivo; }
            set { _txtColectivo = value; }
        }

        public string txtCR_Evaluaciones
        {
            get { return _txtCR_Evaluaciones; }
            set { _txtCR_Evaluaciones = value; }
        }

        public string txtCR_Evaluadores
        {
            get { return _txtCR_Evaluadores; }
            set { _txtCR_Evaluadores = value; }
        }

        public string txtCR_Profesionales
        {
            get { return _txtCR_Profesionales; }
            set { _txtCR_Profesionales = value; }
        }

        public string txtEstado
        {
            get { return _txtEstado; }
            set { _txtEstado = value; }
        }
    }
}