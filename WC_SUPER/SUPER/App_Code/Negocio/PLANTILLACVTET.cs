using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;

//using SUPER.Capa_Datos;
//using SUPER.Capa_Negocio;
//Para usar el StringBuilder
using System.Text;
//Para usar el RegEx
using System.Text.RegularExpressions;

/// <summary>
/// Entornos teconologicos de PLANTILLAS DE CURVIT
/// </summary>

namespace SUPER.BLL
{
    public partial class PLANTILLACVTET
    {
        public PLANTILLACVTET()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        //public static void Insertar(SqlTransaction tr, int idPlant, int idEntorno)
        //{
        //    SUPER.DAL.PLANTILLACVTET.Insert(tr, idPlant, idEntorno);
        //}
        public static string Catalogo(bool bModoLectura, int idPlant)
        {
            //SUPER.DAL.PLANTILLACVTET.GetEntornos(idPlant);
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblEnt'");
            if (bModoLectura)
                sb.Append(" class='texto' style='width:550px; table-layout:fixed;' cellspacing='0' border='0'>");
            else
                sb.Append(" class='texto MANO' style='width:550px; table-layout:fixed;' cellspacing='0' border='0'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:20px;' />");
            sb.Append(" <col style='width:530px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = SUPER.DAL.PLANTILLACVTET.GetEntornos(null, idPlant);
            while (dr.Read())
            {
                //sb.Append("<tr idE='" + dr["t808_idexpprof"].ToString() + "' idT='" + dr["t810_idacontecno"].ToString() + "' bd='' ");
                sb.Append("<tr id='" + dr["t036_idcodentorno"].ToString() + "' bd='' ");
                if (bModoLectura)
                    sb.Append("style='height:20px; display:table-row;' >");
                else
                    sb.Append("style='height:20px; display:table-row;' onclick='mm(event);'>");

                sb.Append("<td><img src='../../../../../../images/imgFN.gif'></td>");
                sb.Append("<td><nobr class='NBR W520'>" + dr["t036_descripcion"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static void Grabar(SqlTransaction tr, int idPlant, string strDatos)
        {
            string[] aElem = Regex.Split(strDatos, "///");
            for (int i = 0; i < aElem.Length; i++)
            {
                if (aElem[i] != "")
                {
                    string[] aEnt = Regex.Split(aElem[i], "##");
                    switch (aEnt[0])
                    {
                        case "I":
                            SUPER.DAL.PLANTILLACVTET.Insert(tr, idPlant, int.Parse(aEnt[1]));
                            break;
                        case "D":
                            SUPER.DAL.PLANTILLACVTET.Delete(tr, idPlant, int.Parse(aEnt[1]));
                            break;
                    }
                }
            }
        }
    }
}