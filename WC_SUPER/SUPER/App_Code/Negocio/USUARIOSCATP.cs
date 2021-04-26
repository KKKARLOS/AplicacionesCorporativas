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
    /// Descripción breve de USUARIOSCATP
    /// </summary>
    public class USUARIOSCATP
    {
        #region Atributos Y Propiedades
        private int _t314_idusuario;
        public int t314_idusuario
        {
            get { return _t314_idusuario; }
            set { _t314_idusuario = value; }
        }

        private float _t144_horasmax_L;
        public float t144_horasmax_L
        {
            get { return _t144_horasmax_L; }
            set { _t144_horasmax_L = value; }
        }
        private float _t144_horasmax_M;
        public float t144_horasmax_M
        {
            get { return _t144_horasmax_M; }
            set { _t144_horasmax_M = value; }
        }
        private float _t144_horasmax_X;
        public float t144_horasmax_X
        {
            get { return _t144_horasmax_X; }
            set { _t144_horasmax_X = value; }
        }
        private float _t144_horasmax_J;
        public float t144_horasmax_J
        {
            get { return _t144_horasmax_J; }
            set { _t144_horasmax_J = value; }
        }
        private float _t144_horasmax_V;
        public float t144_horasmax_V
        {
            get { return _t144_horasmax_V; }
            set { _t144_horasmax_V = value; }
        }
        private float _t144_horasmax_S;
        public float t144_horasmax_S
        {
            get { return _t144_horasmax_S; }
            set { _t144_horasmax_S = value; }
        }
        private float _t144_horasmax_D;
        public float t144_horasmax_D
        {
            get { return _t144_horasmax_D; }
            set { _t144_horasmax_D = value; }
        }
        private float _t144_horasmax_SEM;
        public float t144_horasmax_SEM
        {
            get { return _t144_horasmax_SEM; }
            set { _t144_horasmax_SEM = value; }
        }
        private float _t144_horasmax_MES;
        public float t144_horasmax_MES
        {
            get { return _t144_horasmax_MES; }
            set { _t144_horasmax_MES = value; }
        }
        private bool _t144_avisomail;
        public bool t144_avisomail
        {
            get { return _t144_avisomail; }
            set { _t144_avisomail = value; }
        }
        #endregion

        public USUARIOSCATP()
        {
        }

        #region Metodos

        public static string LimiteExcedido(string sFecha)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SUPER.Capa_Datos.USUARIOSCATP.LimiteExcedido(null, sFecha);
            string sActivarImgCaution = "N";
            string sTooltip = "";

            sb.Append(@"<table id='tblDatos' style='width:830px;text-align:left' cellpadding='0' cellspacing='0'>
                        <colgroup>
                            <col style='width: 25px;'/>
					        <col style='width: 470px' />
					        <col style='width: 35px' />
					        <col style='width: 35px' />
					        <col style='width: 35px' />
					        <col style='width: 35px' />
					        <col style='width: 35px' />
					        <col style='width: 35px' />
					        <col style='width: 35px' />
					        <col style='width: 45px' />
					        <col style='width: 45px' />
                        </colgroup>
                        <tbody>
                        ");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' bd='' "); 
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");

                sTooltip = "<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39);
                sTooltip += "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###");
                sTooltip += "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39);
                sTooltip += "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39);

                sb.Append("tooltipProf=\"" + sTooltip + "\" ");

                //sb.Append("style='height:20px;'>");
                sb.Append(">");

                sb.Append("<td style='text-align:center'></td>");
                //sb.Append("<td style='text-align:right; padding-right: 5px;'>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "</td>");
                //sb.Append("<td title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");
                sb.Append("<td>" + dr["Profesional"].ToString() + "</td>");

                if (dr["L_C"].ToString() == "S")
                {
                    sb.Append("<td style='text-align:right;background-color: #F58D8D;font-weight: bold;'>");
                    sActivarImgCaution = "S";
                }
                else sb.Append("<td style='text-align:right;'>");

                if (dr["t144_horasmax_L"].ToString() == "0")
                    sb.Append("</td>");
                else
                    sb.Append(float.Parse(dr["t144_horasmax_L"].ToString()).ToString("N") + "</td>");

                if (dr["M_C"].ToString() == "S") 
                {
                    sb.Append("<td style='text-align:right;background-color: #F58D8D;font-weight: bold;'>");
                    sActivarImgCaution = "S";
                }
                else sb.Append("<td style='text-align:right;'>");

                if (dr["t144_horasmax_M"].ToString() == "0")
                    sb.Append("</td>");
                else
                    sb.Append(float.Parse(dr["t144_horasmax_M"].ToString()).ToString("N") + "</td>");

                if (dr["X_C"].ToString() == "S")
                {
                    sb.Append("<td style='text-align:right;background-color: #F58D8D;font-weight: bold;'>");
                    sActivarImgCaution = "S";
                }
                else sb.Append("<td style='text-align:right;'>");

                if (dr["t144_horasmax_X"].ToString() == "0")
                    sb.Append("</td>");
                else
                    sb.Append(float.Parse(dr["t144_horasmax_X"].ToString()).ToString("N") + "</td>");

                if (dr["J_C"].ToString() == "S") 
                {
                    sb.Append("<td style='text-align:right;background-color: #F58D8D;font-weight: bold;'>");
                    sActivarImgCaution = "S";
                }
                else sb.Append("<td style='text-align:right;'>");

                if (dr["t144_horasmax_J"].ToString() == "0")
                    sb.Append("</td>");
                else
                    sb.Append(float.Parse(dr["t144_horasmax_J"].ToString()).ToString("N") + "</td>");

                if (dr["V_C"].ToString() == "S") 
                {
                    sb.Append("<td style='text-align:right;background-color: #F58D8D;font-weight: bold;'>");
                    sActivarImgCaution = "S";
                }
                else sb.Append("<td style='text-align:right;'>");

                if (dr["t144_horasmax_V"].ToString() == "0")
                    sb.Append("</td>");
                else
                    sb.Append(float.Parse(dr["t144_horasmax_V"].ToString()).ToString("N") + "</td>");

                if (dr["S_C"].ToString() == "S") 
                {
                    sb.Append("<td style='text-align:right;background-color: #F58D8D;font-weight: bold;'>");
                    sActivarImgCaution = "S";
                }
                else sb.Append("<td style='text-align:right;'>");

                if (dr["t144_horasmax_S"].ToString() == "0")
                    sb.Append("</td>");
                else
                    sb.Append(float.Parse(dr["t144_horasmax_S"].ToString()).ToString("N") + "</td>");

                if (dr["D_C"].ToString() == "S") 
                {
                    sb.Append("<td style='text-align:right;background-color: #F58D8D;font-weight: bold;'>");
                    sActivarImgCaution = "S";
                }
                else sb.Append("<td style='text-align:right;'>");

                if (dr["t144_horasmax_D"].ToString() == "0")
                    sb.Append("</td>");
                else
                    sb.Append(float.Parse(dr["t144_horasmax_D"].ToString()).ToString("N") + "</td>");

                if (dr["SEM_C"].ToString() == "S") 
                {
                    sb.Append("<td align='left' style='text-align:right;background-color: #F58D8D;font-weight: bold;'>");
                    sActivarImgCaution = "S";
                }
                else sb.Append("<td  style='text-align:right;'>");

                if (dr["t144_horasmax_SEM"].ToString() == "0")
                    sb.Append("</td>");
                else
                    sb.Append(float.Parse(dr["t144_horasmax_SEM"].ToString()).ToString("##0.00") + "</td>");

                if (dr["MES_C"].ToString() == "S")
                {
                    sb.Append("<td style='text-align:right;background-color: #F58D8D;font-weight: bold;'>");
                    sActivarImgCaution = "S";
                }
                else sb.Append("<td style='text-align:right;'>");

                if (dr["t144_horasmax_MES"].ToString() == "0")
                    sb.Append("</td>");
                else
                    sb.Append(float.Parse(dr["t144_horasmax_MES"].ToString()).ToString("##0.00") + "</td>");
                sb.Append("</tr>" + (char)10);
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString() + "@#@" + sActivarImgCaution;

        }

        public static string obtenerProfesionales()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SUPER.Capa_Datos.USUARIOSCATP.ObtenerProfesionales(null);
            string sTooltip = "";

            sb.Append(@"<table id='tblDatos' style='width:830px;text-align:left' mantenimiento='1' cellpadding='0' cellspacing='0'>
                        <colgroup>
                            <col style='width: 10px' />
                            <col style='width: 15px;'/>
					        <col style='width: 435px'/>
					        <col style='width: 35px' />
					        <col style='width: 35px' />
					        <col style='width: 35px' />
					        <col style='width: 35px' />
					        <col style='width: 35px' />
					        <col style='width: 35px' />
					        <col style='width: 35px' />
					        <col style='width: 45px' />
					        <col style='width: 45px' />
                            <col style='width: 35px' />
                        </colgroup>
                        <tbody>
                        ");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' bd='' "); 
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");

                sTooltip = "<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39);
                sTooltip += "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###");
                sTooltip += "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39);
                sTooltip += "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39);

                sb.Append("tooltipProf=\"" + sTooltip + "\" ");

                sb.Append(">");

                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center'></td>");

                //sb.Append("<td style='text-align:right; padding-right: 5px;'>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "</td>");

                //sb.Append("<td title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");
                //sb.Append(dr["Profesional"].ToString() + "</td>");
                sb.Append("<td>" + dr["Profesional"].ToString() + "</td>");

                if (dr["t144_horasmax_L"].ToString() == "0")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td>" + float.Parse(dr["t144_horasmax_L"].ToString()).ToString("N") + "</td>");

                if (dr["t144_horasmax_M"].ToString() == "0")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td>" + float.Parse(dr["t144_horasmax_M"].ToString()).ToString("N") + "</td>");

                if (dr["t144_horasmax_X"].ToString() == "0")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td>" + float.Parse(dr["t144_horasmax_X"].ToString()).ToString("N") + "</td>");

                if (dr["t144_horasmax_J"].ToString() == "0")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td>" + float.Parse(dr["t144_horasmax_J"].ToString()).ToString("N") + "</td>");

                if (dr["t144_horasmax_V"].ToString() == "0")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td>" + float.Parse(dr["t144_horasmax_V"].ToString()).ToString("N") + "</td>");

                if (dr["t144_horasmax_S"].ToString() == "0")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td>" + float.Parse(dr["t144_horasmax_S"].ToString()).ToString("N") + "</td>");

                if (dr["t144_horasmax_D"].ToString() == "0")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td>" + float.Parse(dr["t144_horasmax_D"].ToString()).ToString("N") + "</td>");

                if (dr["t144_horasmax_SEM"].ToString() == "0")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td align='left'>" + float.Parse(dr["t144_horasmax_SEM"].ToString()).ToString("##0.00") + "</td>");

                if (dr["t144_horasmax_MES"].ToString() == "0")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td>" + float.Parse(dr["t144_horasmax_MES"].ToString()).ToString("##0.00") + "</td>");

                string aviso = (bool.Parse(dr["t144_avisomail"].ToString())) ? "1" : "0";
                sb.Append("<td>" + aviso + "</td>");

                sb.Append("</tr>" + (char)10);
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString(); ;

        }
        public static string RelacionProfesionales()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SUPER.Capa_Datos.USUARIOSCATP.ObtenerProfesionales(null);
            string sTooltip = "";

            sb.Append(@"<table id='tblDatos' style='width:490px;text-align:left' cellpadding='0' cellspacing='0'>
                        <colgroup>
                            <col style='width: 20px' />
                            <col style='width: 60px;' />
					        <col style='width: 410px' />
                        </colgroup>
                        <tbody>
                        ");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");

                sTooltip = "<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39);
                sTooltip += "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###");
                sTooltip += "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39);
                sTooltip += "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39);

                sb.Append("tooltipProf=\"" + sTooltip + "\" ");

                sb.Append(">");

                sb.Append("<td style='text-align:center'></td>");

                sb.Append("<td style='text-align:right; padding-right: 5px;'>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td>" + dr["Profesional"].ToString() + "</td>");

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
                        ///aValores[2] = t144_horasmax_L
                        ///aValores[3] = t144_horasmax_M
                        ///aValores[4] = t144_horasmax_X
                        ///aValores[5] = t144_horasmax_J
                        ///aValores[6] = t144_horasmax_V
                        ///aValores[7] = t144_horasmax_S
                        ///aValores[8] = t144_horasmax_D
                        ///aValores[9] = t144_horasmax_SEM
                        ///aValores[10] = t144_horasmax_MES
                        ///aValores[11] = t144_avisomail
                        ////*

                        switch (aValores[0])
                        {
                            case "I":
                                SUPER.Capa_Datos.USUARIOSCATP.Insert(
                                                                        tr, 
                                                                        int.Parse(aValores[1]),
                                                                        (aValores[2] == "") ? 0 : float.Parse(aValores[2]),
                                                                        (aValores[3] == "") ? 0 : float.Parse(aValores[3]),
                                                                        (aValores[4] == "") ? 0 : float.Parse(aValores[4]),
                                                                        (aValores[5] == "") ? 0 : float.Parse(aValores[5]),
                                                                        (aValores[6] == "") ? 0 : float.Parse(aValores[6]),
                                                                        (aValores[7] == "") ? 0 : float.Parse(aValores[7]),
                                                                        (aValores[8] == "") ? 0 : float.Parse(aValores[8]),
                                                                        (aValores[9] == "") ? 0 : float.Parse(aValores[9]),
                                                                        (aValores[10] == "") ? 0 : float.Parse(aValores[10]),
                                                                        (aValores[11] == "1") ? true : false
                                                                        );
                                if (sElementosInsertados == "") sElementosInsertados = aValores[1];
                                else sElementosInsertados += "//" + aValores[1];
                                break;
                            case "U":
                                SUPER.Capa_Datos.USUARIOSCATP.Update(
                                                                        tr,
                                                                        int.Parse(aValores[1]),
                                                                        (aValores[2] == "") ? 0 : float.Parse(aValores[2]),
                                                                        (aValores[3] == "") ? 0 : float.Parse(aValores[3]),
                                                                        (aValores[4] == "") ? 0 : float.Parse(aValores[4]),
                                                                        (aValores[5] == "") ? 0 : float.Parse(aValores[5]),
                                                                        (aValores[6] == "") ? 0 : float.Parse(aValores[6]),
                                                                        (aValores[7] == "") ? 0 : float.Parse(aValores[7]),
                                                                        (aValores[8] == "") ? 0 : float.Parse(aValores[8]),
                                                                        (aValores[9] == "") ? 0 : float.Parse(aValores[9]),
                                                                        (aValores[10] == "") ? 0 : float.Parse(aValores[10]),
                                                                        (aValores[11] == "1") ? true : false    
                                                                        );
                                break;
                            case "D":
                                SUPER.Capa_Datos.USUARIOSCATP.Delete(tr, int.Parse(aValores[1]));
                                break;
                        }                    
                    }
                }
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar los parámetros de los contratos a tiempo parcial.", ex);
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

