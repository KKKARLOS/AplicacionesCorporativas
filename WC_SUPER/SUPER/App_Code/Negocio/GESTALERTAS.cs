using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SUPER.Capa_Datos;
using System.Text.RegularExpressions;

namespace SUPER.Capa_Negocio
{
    public partial class GESTALERTAS
    {
        #region Propiedades y Atributos

        private int _t392_idsupernodo2;
        public int t392_idsupernodo2
        {
            get { return _t392_idsupernodo2; }
            set { _t392_idsupernodo2 = value; }
        }

        private int _t314_idusuario;
        public int t314_idusuario
        {
            get { return _t314_idusuario; }
            set { _t314_idusuario = value; }
        }

        #endregion

        #region Constructores

        public GESTALERTAS()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        public static string ObtenerGestoresAlertas()
        {
            StringBuilder sb = new StringBuilder();
            string sDenominacionG1 = "";

            try
            {
                sb.Append(@"<table id='tblNegocios' style='width:560px;' cellpadding='0' cellspacing='0' border='0'>
                        <colgroup> 
                            <col style='width:35px;' />
                            <col style='width:245px;' />
                            <col style='width:280px;' />
                        </colgroup>
                        <tbody>");

                int i = 0;
                DataSet ds = SUPER.DAL.GESTALERTAS.ObtenerGestoresAlertas(null);
                //Tabla[0] -> Negocios
                //Tabla[1] -> Gestores
                foreach (DataRow oFila in ds.Tables[0].Rows)
                {
                    sb.Append("<tr id='" + oFila["t392_idsupernodo2"].ToString() + "' class='"+ ((i%2==0)?"FA":"FB") +"' valign='top'>");
                    sb.Append("<td style='text-align:center; padding-top:3px;'><input type='checkbox' class='check' style='cursor:pointer;' /></td>");
                    sb.Append("<td onmouseover='TTip(event)' style='padding-top:5px;'><nobr class='NBR W270'>" + oFila["t392_denominacion"].ToString() + "</nobr></td>");
                    sb.Append("<td>");//Gestores grupo 1
                    #region Gestores grupo 1
                    sb.Append(@"<table id='tblGestoresG1_" + oFila["t392_idsupernodo2"].ToString() + @"' style='width:280px;' cellpadding='0' cellspacing='0' border='0'>
                            <colgroup> 
                                <col style='width:20px;' />
                                <col style='width:260px;' />
                            </colgroup>
                            <tbody>");
                    foreach (DataRow oFilaGes in ds.Tables[1].Rows)
                    {
                        //if ((byte)oFilaGes["t821_idgrupoalerta"] != 1) continue;
                        if ((int)oFilaGes["t392_idsupernodo2"] == (int)oFila["t392_idsupernodo2"])
                        {
                            sb.Append("<tr id='" + oFilaGes["t314_idusuario"].ToString() + "' onclick='mef(this)' ");
                            sb.Append("idSN2='" + oFila["t392_idsupernodo2"].ToString() + "' >");
                            sb.Append("<td><img src='../../../Images/imgUsu" + oFilaGes["tipo"].ToString() + oFilaGes["t001_sexo"].ToString() + ".gif' /></td>");
                            sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W250 MANO'>" + oFilaGes["Gestor"].ToString() + "</nobr></td>");
                            sb.Append("</tr>");
                        }
                        else if ((int)oFilaGes["t392_idsupernodo2"] > (int)oFila["t392_idsupernodo2"])
                        {
                            break;
                        }
                    }
                    sb.Append(@"</tbody>
                            </table>");
                    #endregion

                    sb.Append("</td>");
                    sb.Append("</tr>");

                    i++;
                }

                sDenominacionG1 = ds.Tables[2].Rows[1]["t821_denominacion"].ToString();

                ds.Dispose();
                sb.Append("</tbody>");
                sb.Append("</table>");

                return "OK@#@" + sb.ToString() + "@#@" + sDenominacionG1;
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al obtener los gestores.", ex);
            }
        }

        public static string InsertarGestores(string strDatos)
        {
            string sResul = "";
            #region Inicio Transacción

            SqlConnection oConn;
            SqlTransaction tr;
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                throw (new Exception("Error al abrir la conexion", ex));
            }

            #endregion

            try
            {
                string[] aProfesional = Regex.Split(strDatos, "{fila}");
                foreach (string oProfesional in aProfesional)
                {
                    string[] aDatos = Regex.Split(oProfesional, "{dato}");
                    switch (aDatos[0])
                    {
                        case "I":
                            DAL.GESTALERTAS.Insertar(tr, int.Parse(aDatos[1]), int.Parse(aDatos[2]));
                            break;
                        case "D":
                            DAL.GESTALERTAS.Eliminar(tr, int.Parse(aDatos[1]), int.Parse(aDatos[2]));
                            break;
                    }
                }

                Conexion.CommitTransaccion(tr);

                sResul = "OK@#@";
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = "Error@#@" + Errores.mostrarError("Error al obtener los gestores.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
            return sResul;
        }

        #endregion
    }
}
