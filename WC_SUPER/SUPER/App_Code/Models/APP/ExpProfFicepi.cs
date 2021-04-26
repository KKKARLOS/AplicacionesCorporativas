using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace IB.SUPER.APP.Models
{

    /// <summary>
    /// Descripción breve de ExpProfFicepi
    /// </summary>
    public class ExpProfFicepi
{
        public int t812_idexpprofficepi { get; set; }
        public int t001_idficepi { get; set; }
        public int? t001_idficepi_validador { get; set; }
        public string tipo { get; set; }
        public string sexo { get; set; }
        public bool baja { get; set; }
        public string profesional { get; set; }
        public string denValidador { get; set; }
        public DateTime? finicio { get; set; }
        public DateTime? ffin { get; set; }
        public string visibleCV { get; set; }
        public int? idPlantilla { get; set; }
        public string perfil { get; set; }
        public string oficina { get; set; }
        public int? anomesPrimerConsumo { get; set; }
        public int? anomesUltimoConsumo { get; set; }
        public double? esfuerzoJornadas { get; set; }

    }
}