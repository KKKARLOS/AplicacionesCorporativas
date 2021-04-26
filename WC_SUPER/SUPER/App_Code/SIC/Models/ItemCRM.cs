using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.SIC.Models
{
    public class ItemCRM
    {
        public ItemCRM()
        {
        }

        public string itemorigen { get; set; }
        public int iditemorigen { get; set; }

        public string denominacion { get; set; }
        public string cuenta { get; set; }
        public string exito { get; set; }
        public string gestorProduccion { get; set; }
        public string gestorProduccion_nombre { get; set; }
        public string cod_comercial { get; set; }
        public string comercial { get; set; }
        public string importe { get; set; }
        public string organizacionComercial { get; set; }
        public string desc_objetivo { get; set; }
        public string centroResponsabilidad { get; set; }
        public string rentabilidad { get; set; }
        public string areaConTecnologico { get; set; }
        public string areaConSectorial { get; set; }
        public string duracionProyecto { get; set; }
        public string fechaCierre { get; set; }
        public string fechaLimitePresentacion { get; set; }
        public string etapaVentas { get; set; }
        public string estado { get; set; }

        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public string oferta { get; set; }
        public string contratacionPrevista { get; set; }
        public string costePrevisto { get; set; }
        public string resultado { get; set; }

        public string moneda { get; set; }

        public int num_oportunidad { get; set; }
        public string den_oportunidad { get; set; }
        public bool botonactivo { get; set; }
        public string tipo_negocio { get; set; }
        public string oferta_objetivo { get; set; }

    }
}