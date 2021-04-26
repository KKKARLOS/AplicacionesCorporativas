using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Descripción breve de USUARIOSIAUTO
    /// </summary>
    public class USUARIOSIAUTO
    {
        #region Atributos Y Propiedades
        private int _t314_idusuario;
        public int t314_idusuario
        {
            get { return _t314_idusuario; }
            set { _t314_idusuario = value; }
        }

        private int _t332_idtarea;
        public int t332_idtarea
        {
            get { return _t332_idtarea; }
            set { _t332_idtarea = value; }
        }
        private float _t143_horas_L;
        public float t143_horas_L
        {
            get { return _t143_horas_L; }
            set { _t143_horas_L = value; }
        }
        private float _t143_horas_M;
        public float t143_horas_M
        {
            get { return _t143_horas_M; }
            set { _t143_horas_M = value; }
        }
        private float _t143_horas_X;
        public float t143_horas_X
        {
            get { return _t143_horas_X; }
            set { _t143_horas_X = value; }
        }
        private float _t143_horas_J;
        public float t143_horas_J
        {
            get { return _t143_horas_J; }
            set { _t143_horas_J = value; }
        }
        private float _t143_horas_V;
        public float t143_horas_V
        {
            get { return _t143_horas_V; }
            set { _t143_horas_V = value; }
        }
        private float _t143_horas_S;
        public float t143_horas_S
        {
            get { return _t143_horas_S; }
            set { _t143_horas_S = value; }
        }
        private float _t143_horas_D;
        public float t143_horas_D
        {
            get { return _t143_horas_D; }
            set { _t143_horas_D = value; }
        }
        private bool _t143_avisomail;
        public bool t143_avisomail
        {
            get { return _t143_avisomail; }
            set { _t143_avisomail = value; }
        }
        #endregion

        public USUARIOSIAUTO()
        {

        }

        #region Metodos

        public static string obtenerProfesionales()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SUPER.Capa_Datos.USUARIOSIAUTO.ObtenerProfesionales(null);
            string sTooltip = "";

            sb.Append(@"<table id='tblDatos' style='width:985px;text-align:left' mantenimiento='1' cellpadding='0' cellspacing='0'>
                        <colgroup>
                            <col style='width: 10px' />
                            <col style='width: 15px;'/>
					        <col style='width: 310px' />
					        <col style='width: 370px' />
					        <col style='width: 35px' />
					        <col style='width: 35px' />
					        <col style='width: 35px' />
					        <col style='width: 35px' />
					        <col style='width: 35px' />
					        <col style='width: 35px' />
					        <col style='width: 35px' />
					        <col style='width: 35px' />
                        </colgroup>
                        <tbody>
                        ");

            while (dr.Read())
            {

                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' bd='' "); 
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("idTarea='" + dr["t332_idtarea"].ToString() + "' ");

                sTooltip = "<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39);
                sTooltip += "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###");
                sTooltip += "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39);
                sTooltip += "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39);

                sb.Append("tooltipProf=\"" + sTooltip + "\" ");

                sTooltip = "<label style='width:70px;'>P.Económico:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>P.Técnico:</label>" + dr["t331_despt"].ToString().Replace((char)34, (char)39);
                if (dr["t334_desfase"].ToString() != "")
                    sTooltip += "<br><label style='width:70px;'>Fase:</label>" + dr["t334_desfase"].ToString().Replace((char)34, (char)39);
                if (dr["t335_desactividad"].ToString() != "")
                    sTooltip += "<br><label style='width:70px;'>Actividad:</label>" + dr["t335_desactividad"].ToString().Replace((char)34, (char)39);

                sTooltip += "<br><label style='width:70px;'>Tarea:</label>" + int.Parse(dr["t332_idtarea"].ToString()).ToString("###,###") + " - " + dr["t332_destarea"].ToString().Replace((char)34, (char)39);

                sb.Append("tooltipTarea=\"" + sTooltip + "\" ");

                sb.Append(">");
                //sb.Append("style='height:20px;'>");

                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center'></td>");

                //sb.Append("<td style='text-align:right; padding-right: 5px;'>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "</td>");

                //sb.Append("<td title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");                
                //sb.Append(dr["Profesional"].ToString() + "</td>");

                sb.Append("<td>" + dr["Profesional"].ToString() + "</td>");

                //sContenido = "<label style='width:70px;'>P.Económico:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>P.Técnico:</label>" + dr["t331_despt"].ToString().Replace((char)34, (char)39);
                //if (dr["t334_desfase"].ToString() != "")
                //    sContenido += "<br><label style='width:70px;'>Fase:</label>" + dr["t334_desfase"].ToString().Replace((char)34, (char)39);
                //if (dr["t335_desactividad"].ToString() != "")
                //    sContenido += "<br><label style='width:70px;'>Actividad  :</label>" + dr["t335_desactividad"].ToString().Replace((char)34, (char)39);

                //sContenido += "<br><label style='width:70px;'>Tarea      :</label>" + int.Parse(dr["t332_idtarea"].ToString()).ToString("###,###") + " - " + dr["t332_destarea"].ToString().Replace((char)34, (char)39);
                //sb.Append("<td class='MAM' ondblclick='getTarea(this)' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=["+ sContenido + "] hideselects=[off]\">");

                sb.Append("<td class='MAM' ondblclick='getTarea(this)'>" + dr["t332_destarea"].ToString() + "</td>");

                if (dr["t143_horas_L"].ToString()=="0")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td>" + float.Parse(dr["t143_horas_L"].ToString()).ToString("N") + "</td>");

                if (dr["t143_horas_M"].ToString() == "0")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td>" + float.Parse(dr["t143_horas_M"].ToString()).ToString("N") + "</td>");

                if (dr["t143_horas_X"].ToString() == "0")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td>" + float.Parse(dr["t143_horas_X"].ToString()).ToString("N") + "</td>");

                if (dr["t143_horas_J"].ToString() == "0")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td>" + float.Parse(dr["t143_horas_J"].ToString()).ToString("N") + "</td>");

                if (dr["t143_horas_V"].ToString() == "0")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td>" + float.Parse(dr["t143_horas_V"].ToString()).ToString("N") + "</td>");

                if (dr["t143_horas_S"].ToString() == "0")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td>" + float.Parse(dr["t143_horas_S"].ToString()).ToString("N") + "</td>");

                if (dr["t143_horas_D"].ToString() == "0")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td>" + float.Parse(dr["t143_horas_D"].ToString()).ToString("N") + "</td>");
                
                
                string aviso = (bool.Parse(dr["t143_avisomail"].ToString())) ? "1":"0";
                sb.Append("<td>" + aviso + "</td>");
                sb.Append("</tr>" + (char)10);
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString(); ;

        }

        public static string Grabar(string strDatos)
        {
            string sElementosInsertados = "";
            string sResul = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión. " + ex.Message));
            }
            #endregion

            try
            {
                if (strDatos != "") //No se ha modificado nada 
                {
                    string[] aDatos = Regex.Split(strDatos, "///");
                    foreach (string oDatos in aDatos)
                    {
                        if (oDatos == "") continue;
                        string[] aValores = Regex.Split(oDatos, "##");

                        ///aValores[0] = bd
                        ///aValores[1] = t314_idusuario
                        ///aValores[2] = t332_idtarea
                        ///aValores[3] = t143_horas_L
                        ///aValores[4] = t143_horas_M
                        ///aValores[5] = t143_horas_X
                        ///aValores[6] = t143_horas_J
                        ///aValores[7] = t143_horas_V
                        ///aValores[8] = t143_horas_S
                        ///aValores[9] = t143_horas_D
                        ///aValores[10] = t143_avisomail
                        ////*

                        switch (aValores[0])
                        {
                            case "I":
                                SUPER.Capa_Datos.USUARIOSIAUTO.Insert   (
                                                                        tr, 
                                                                        int.Parse(aValores[1]), 
                                                                        int.Parse(aValores[2]),
                                                                        (aValores[3] == "") ? 0 : float.Parse(aValores[3]),
                                                                        (aValores[4] == "") ? 0 : float.Parse(aValores[4]),
                                                                        (aValores[5] == "") ? 0 : float.Parse(aValores[5]),
                                                                        (aValores[6] == "") ? 0 : float.Parse(aValores[6]),
                                                                        (aValores[7] == "") ? 0 : float.Parse(aValores[7]),
                                                                        (aValores[8] == "") ? 0 : float.Parse(aValores[8]),
                                                                        (aValores[9] == "") ? 0 : float.Parse(aValores[9]),
                                                                        (aValores[10]=="1") ? true : false
                                                                        );
                                if (sElementosInsertados == "") sElementosInsertados = aValores[1];
                                else sElementosInsertados += "//" + aValores[1];
                                break;
                            case "U":
                                SUPER.Capa_Datos.USUARIOSIAUTO.Update(
                                                                        tr,
                                                                        int.Parse(aValores[1]),
                                                                        int.Parse(aValores[2]),
                                                                        (aValores[3] == "") ? 0 : float.Parse(aValores[3]),
                                                                        (aValores[4] == "") ? 0 : float.Parse(aValores[4]),
                                                                        (aValores[5] == "") ? 0 : float.Parse(aValores[5]),
                                                                        (aValores[6] == "") ? 0 : float.Parse(aValores[6]),
                                                                        (aValores[7] == "") ? 0 : float.Parse(aValores[7]),
                                                                        (aValores[8] == "") ? 0 : float.Parse(aValores[8]),
                                                                        (aValores[9] == "") ? 0 : float.Parse(aValores[9]),
                                                                        (aValores[10] == "1") ? true : false
                                                                        );
                                break;
                            case "D":
                                SUPER.Capa_Datos.USUARIOSIAUTO.Delete(tr, int.Parse(aValores[1]));
                                break;
                        }                    
                    }
                }
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar los parámetros de las imputaciones automáticas.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            sResul = sElementosInsertados;
            return "OK@#@" + sResul;
        }

        #endregion
    }
}

