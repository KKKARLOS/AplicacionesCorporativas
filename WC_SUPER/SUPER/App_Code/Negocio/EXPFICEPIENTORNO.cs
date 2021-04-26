using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
//using SUPER.Capa_Datos;
namespace SUPER.BLL
{
    public class EXPFICEPIENTORNO
    {
        public EXPFICEPIENTORNO()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public static void Reasignar(SqlTransaction tr, int idOrigen, string sDestino)
        {
            SUPER.DAL.EXPFICEPIENTORNO.Reasignar(tr, idOrigen, sDestino);
        }
    }
}