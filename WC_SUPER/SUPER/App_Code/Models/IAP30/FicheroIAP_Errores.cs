using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
    public class FicheroIAP_Errores_Linea
    {
        #region Public Properties
        public int Fila { get; set; }
        public String Usuario { get; set; }
        //public DateTime? Fecha { get; set; }
        //public DateTime? FechaH { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? FechaH { get; set; }
        public String Tarea { get; set; }
        //public double? Esfuerzo { get; set; }
        public Double Esfuerzo { get; set; }
        public String Festivos { get; set; }
        public String Error { get; set; }
        #endregion
    }
    public class FicheroIAP_Errores
    {
        public int nFilas { get; set; }
        public int nFilasC { get; set; }
        public int nFilasE { get; set; }
        public List<FicheroIAP_Errores_Linea> Errores { get; set; }
    }
}