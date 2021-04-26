using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Aplicacion
/// </summary>
namespace IB.Progress.Models
{
    public class Aplicacion
    {
        public Aplicacion()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        private bool _estado;
        private string _motivo;
        public bool Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public string Motivo
        {
            get { return _motivo; }
            set { _motivo = value; }
        }
    }
}