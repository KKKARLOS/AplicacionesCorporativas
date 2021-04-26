using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
//using SUPER.Capa_Datos;
namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de EXPFICEPIPERFIL
    /// </summary>
    public partial class EXPFICEPIPERFIL
    {

        #region Propiedades y Atributos
        private int _t813_idexpficepiperfil;
        public int t813_idexpficepiperfil
        {
            get { return _t813_idexpficepiperfil; }
            set { _t813_idexpficepiperfil = value; }
        }

        private DateTime? _t813_finicio;
        public DateTime? t813_finicio
        {
            get { return _t813_finicio; }
            set { _t813_finicio = value; }
        }

        private DateTime? _t813_ffin;
        public DateTime? t813_ffin
        {
            get { return _t813_ffin; }
            set { _t813_ffin = value; }
        }

        private string _t813_funcion;
        public string t813_funcion
        {
            get { return _t813_funcion; }
            set { _t813_funcion = value; }
        }

        private string _t813_observa;
        public string t813_observa
        {
            get { return _t813_observa; }
            set { _t813_observa = value; }
        }

        private string _t839_idestado;
        public string t839_idestado
        {
            get { return _t839_idestado; }
            set { _t839_idestado = value; }
        }

        private string _t838_motivort;
        public string t838_motivort
        {
            get { return _t838_motivort; }
            set { _t838_motivort = value; }
        }

        private DateTime _t813_fechau;
        public DateTime t813_fechau
        {
            get { return _t813_fechau; }
            set { _t813_fechau = value; }
        }

        private int _t035_idcodperfil;
        public int t035_idcodperfil
        {
            get { return _t035_idcodperfil; }
            set { _t035_idcodperfil = value; }
        }

        private int _t812_idexpprofficepi;
        public int t812_idexpprofficepi
        {
            get { return _t812_idexpprofficepi; }
            set { _t812_idexpprofficepi = value; }
        }

        private int _t001_idficepiu;
        public int t001_idficepiu
        {
            get { return _t001_idficepiu; }
            set { _t001_idficepiu = value; }
        }
        private short _t020_idcodidioma;
        public short t020_idcodidioma
        {
            get { return _t020_idcodidioma; }
            set { _t020_idcodidioma = value; }
        }
        #endregion

        #region Constructor
        public EXPFICEPIPERFIL()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public EXPFICEPIPERFIL(SqlTransaction tr, int t813_idexpficepiperfil)
        {
            this.t813_idexpficepiperfil = t813_idexpficepiperfil;
            if (t813_idexpficepiperfil != -1)
            {
                SUPER.DAL.EXPFICEPIPERFIL oExp = SUPER.DAL.EXPFICEPIPERFIL.Select(tr, t813_idexpficepiperfil);
                this.t001_idficepiu = oExp.t001_idficepiu;
                this.t035_idcodperfil = oExp.t035_idcodperfil;
                this.t812_idexpprofficepi = oExp.t812_idexpprofficepi;
                this.t839_idestado = oExp.t839_idestado;
                this.t813_fechau = oExp.t813_fechau;
                this.t813_ffin = oExp.t813_ffin;
                this.t813_finicio = oExp.t813_finicio;
                this.t813_funcion = oExp.t813_funcion;
                this.t838_motivort = oExp.t838_motivort;
                this.t813_observa = oExp.t813_observa;
				this.t020_idcodidioma = oExp.t020_idcodidioma;
            }
        }
        #endregion
        #region Métodos
        public static string MiCVCatalogo(bool bEnIbermatica, int t001_idficepi, int t808_idexpprof)
        {
            //string motivoT = "";
            string sTooltip = "";
            SqlDataReader dr = SUPER.DAL.EXPFICEPIPERFIL.Catalogo(t001_idficepi, t808_idexpprof);
            StringBuilder sb = new StringBuilder();
            string Tipo = "";
            sb.Append("<table id='tblDatosPerfiles' class='texto MA' style='width:580px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:20px;'/>");
            sb.Append("<col style='width:20px;'/>");
            sb.Append("<col style='width:400px;'/>");
            sb.Append("<col style='width:70px;'/>");
            sb.Append("<col style='width:70px;'/>");
            sb.Append("</colgroup>");

            while (dr.Read())
            {

                Tipo = dr["Tipo"].ToString();
                sb.Append("<tr style='height:20px;' bd=''");
                sb.Append(" id='" + dr["idReg"].ToString() + "'");
                sb.Append(" tipo='" + dr["Tipo"].ToString() + "'");//P->plantilla, M->manual
                sb.Append(" idEPF='" + dr["t812_idexpprofficepi"].ToString() + "'");
                sb.Append(" est='" + dr["ESTADO"].ToString() + "' onclick='ms(this);'");//onclick='mm(event);'
                sb.Append(" ondblclick='mostrarPerfil(this);'>");
                sb.Append("<td><img src='../../../../../images/imgFN.gif' /></td>");
                sb.Append("<td>");
                switch (dr["ESTADO"].ToString())
                {
                    //06/08/2015 PPOO nos pide que no figuren las leyendas Pdte Validar ni Info privada
                    //case ("O"):
                    //case ("P"):
                    //    sb.Append("<img src='../../../../../images/imgPenValidar.png' title='Datos pendientes de validar por la organización' />");
                    //    break;
                    //case ("R"):
                    //    sb.Append("<img src='../../../../../images/imgRechazar.png' title='Este dato es únicamente visible por ti' />");
                    //    break;
                    case ("X"):
                    case ("Y"):
                        sb.Append("<img src='../../../../../images/imgPseudovalidado.png' title='Pendiente de adjuntar la documentación acreditativa' />");
                        break;
                    case ("S"):
                    case ("T"):
                        sb.Append("<img src='../../../../../images/imgPenCumplimentar.png' title='Datos que tienes pendiente de completar, actualizar o modificar' />");
                        break;
                    case ("B"):
                        sb.Append("<img src='../../../../../images/imgBorrador.png' title='Datos en borrador' />");
                        break;
                    default:
                        sb.Append("<img src='../../../../../images/imgFN.gif' title='' />");
                        break;
                }
                sb.Append("</td>");
                //sb.Append("<td title='" + dr["FUNCION"].ToString() + "' style='padding-left:3px;'>");
                sTooltip = dr["FUNCION"].ToString().Trim();
                if (sTooltip != "")
                {
                    sTooltip = SUPER.Capa_Negocio.Utilidades.escape(sTooltip.Replace("'", "&#39;").Replace("\"", "&#34;"));
                    sb.Append("<td style='padding-left:3px;' onmouseover=\'showTTE(\"" + sTooltip + "\",\"Funciones\",null,350)\' onMouseout=\"hideTTE()\" >");
                }
                else
                {
                    sb.Append("<td style='padding-left:3px;' >");
                }
                sb.Append("<nobr class='NBR W400'>" + dr["T035_DESCRIPCION"].ToString() + "</nobr></td>");
                if (dr["finicio"].ToString() == "")
                    sb.Append("<td style='padding-left:3px;'></td>");
                else
                    sb.Append("<td style='padding-left:3px;'>" + DateTime.Parse(dr["finicio"].ToString()).ToShortDateString() + "</td>");
                if (dr["ffin"].ToString() == "")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td>" + DateTime.Parse(dr["ffin"].ToString()).ToShortDateString() + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            sb.Append("@#@" + Tipo);//P->plantilla, M->manual;
            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }
        //public static void PedirBorrado(int t812_idexpprofficepi, int t001_idficepi_petbor, string sMotivo, string sDatosCorreo)
        //{
        //    //string sRes = "OK@#@";
        //    //try
        //    //{
        //    #region Inicio Transacción
        //    SqlConnection oConn;
        //    SqlTransaction tr;
        //    try
        //    {
        //        oConn = SUPER.Capa_Negocio.Conexion.Abrir();
        //        tr = SUPER.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (new Exception("Error al abrir la conexion", ex));
        //    }

        //    #endregion
        //    try
        //    {
        //        SUPER.DAL.EXPPROFFICEPI.PedirBorrado(null, t812_idexpprofficepi, t001_idficepi_petbor, sMotivo);
        //        SUPER.Capa_Negocio.Correo.EnviarPetBorrado(sDatosCorreo, sMotivo);
        //        SUPER.Capa_Negocio.Conexion.CommitTransaccion(tr);
        //    }
        //    catch (Exception ex)
        //    {
        //        SUPER.Capa_Negocio.Conexion.CerrarTransaccion(tr);
        //        throw ex;
        //    }
        //    finally
        //    {
        //        SUPER.Capa_Negocio.Conexion.Cerrar(oConn);
        //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    sRes = "ERROR@#@" + ex.Message;
        //    //}
        //    //return sRes;
        //}

        public static string Borrar(string sTipos, string sIds)
        {
            string sRes = "OK@#@";
            try
            {
                #region Inicio Transacción
                SqlConnection oConn;
                SqlTransaction tr;
                try
                {
                    oConn = SUPER.Capa_Negocio.Conexion.Abrir();
                    tr = SUPER.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
                }
                catch (Exception ex)
                {
                    throw (new Exception("Error al abrir la conexion", ex));
                }

                #endregion
                try
                {
                    string[] aTipos = Regex.Split(sTipos, "##");
                    string[] aReg = Regex.Split(sIds, "##");

                    //foreach (string oReg in aReg)
                    //{
                    //    if (oReg == "") continue;
                    //    SUPER.DAL.EXPFICEPIPERFIL.Delete(tr, int.Parse(oReg));
                    //}

                    for (int i = 0; i < aReg.Length; i++)
                    {
                        if (aReg[i].ToString() == "") continue;

                        if (aTipos[i].ToString() == "P")
                            SUPER.DAL.EXPFICEPIPERFIL.DesasignarPlantilla(tr, int.Parse(aReg[i]));        
                        else
                            SUPER.DAL.EXPFICEPIPERFIL.Delete(tr, int.Parse(aReg[i]));                    
                    }


                    SUPER.Capa_Negocio.Conexion.CommitTransaccion(tr);
                }
                catch (Exception ex)
                {
                    SUPER.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                    throw ex;
                }
                finally
                {
                    SUPER.Capa_Negocio.Conexion.Cerrar(oConn);
                }
            }
            catch (Exception ex)
            {
                sRes = "ERROR@#@" + ex.Message;
            }
            return sRes;
        }


        public static string Borrar(string sPerfiles)
        {
            string sRes = "OK@#@";
            try
            {
                #region Inicio Transacción
                SqlConnection oConn;
                SqlTransaction tr;
                try
                {
                    oConn = SUPER.Capa_Negocio.Conexion.Abrir();
                    tr = SUPER.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
                }
                catch (Exception ex)
                {
                    throw (new Exception("Error al abrir la conexion", ex));
                }

                #endregion
                try
                {
                    string[] aReg = Regex.Split(sPerfiles, "##");
                    foreach (string oReg in aReg)
                    {
                        if (oReg == "") continue;
                        SUPER.DAL.EXPFICEPIPERFIL.Delete(tr, int.Parse(oReg));
                    }
                    SUPER.Capa_Negocio.Conexion.CommitTransaccion(tr);
                }
                catch (Exception ex)
                {
                    SUPER.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                    throw ex;
                }
                finally
                {
                    SUPER.Capa_Negocio.Conexion.Cerrar(oConn);
                }
            }
            catch (Exception ex)
            {
                sRes = "ERROR@#@" + ex.Message;
            }
            return sRes;
        }
        #endregion
    }
}