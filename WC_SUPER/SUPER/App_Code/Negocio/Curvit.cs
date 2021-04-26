using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SUPER.DAL;
using System.Text.RegularExpressions;

namespace SUPER.BLL
{/// <summary>
    /// Descripción breve de Curvit
    /// </summary>
    public class Curvit
    {

        #region Constructor
        public Curvit()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        #endregion

        #region Metodos

        public static string ToolTipEstados(string estado)
        {
            string tooltip = "";
            switch (estado)
            {
                case "B":
                    tooltip = "Datos en borrador"; 
                    break;
                case "O":
                case "P":
                    tooltip = "Datos pendientes de validar por la organización";
                    break;
                case "R":
                    tooltip = "Este datos es únicamente visible por ti";
                    break;
                case "S":
                case "T":
                    tooltip = "Datos que tienes pendiente de completar, actualizar o modificar";
                    break;
                case "X":
                case "Y":
                    tooltip = "Pendiente de adjuntar la documentación acreditativa";
                    break;

            }

            return tooltip;
        }

        #endregion
    }

    public class CVT
    {
        public enum Accion:int
        {
            Aparcar=1,
            Enviar,
            Cumplimentar,
            Validar,
            Pseudovalidar,
            Rechazar,
            Lectura
        }


    }
}

