using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.SIC.Models
{
    public class UsuarioCRM
    {
        public int IDFICEPI_PC_ACTUAL { get; set; }
        public int IDFICEPI_ENTRADA { get; set; }
        public string APELLIDO1 { get; set; }
        public string APELLIDO2 { get; set; }
        public string NOMBRE { get; set; }
        public string IDRED { get; set; }
        public string DES_EMPLEADO_ENTRADA { get; set; }

        public UsuarioCRM()
        {
            IDFICEPI_PC_ACTUAL = 0;
            IDFICEPI_ENTRADA = 0;
            APELLIDO1 = "";
            APELLIDO2 = "";
            NOMBRE = "";
            IDRED = "";
            DES_EMPLEADO_ENTRADA = "";
        }
    }
}