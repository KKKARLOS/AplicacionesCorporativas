using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PerfilesTarea
/// </summary>
/// 
namespace IB.SUPER.SIC.Models
{
    [Serializable]
    public class PerfilesEdicion
    {
        public int idficepi { get; set; }
        public bool soyAdministrador { get; set; }
        public bool soyFiguraArea { get; set; }
        public bool soyFiguraSubarea { get; set; }
        public bool soyFiguraAreaActual { get; set; }
        public bool soyFiguraSubareaActual { get; set; }
        public bool soyLider { get; set; }
        public bool soyPosibleLider { get; set; }
        public bool soySuperEditor { get; set; }
        public bool soyComercial { get; set; }

        //TODO borrar estas de abajo
        public string estadoParticipacion { get; set; }
        public string modoPantalla { get; set; }
        public string origenPantalla { get; set; }


        public PerfilesEdicion()
        {
            soyAdministrador = false;
            soyFiguraArea = false;
            soyFiguraSubarea = false;
            soyFiguraAreaActual = false;
            soyFiguraSubareaActual = false;
            soyLider = false;
            soyPosibleLider = false;
            soySuperEditor = false;
            soyComercial = false;
        }
    }
}