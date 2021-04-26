using System.Data;
using System.Web;
using System.Data.SqlClient;
//para gestion de roles
using System.Text;
using System.Text.RegularExpressions;
using GASVI.DAL;
using System;

namespace GASVI.BLL
{
    public partial class Administracion
    {
        #region Propiedades

        #endregion

        public Administracion()
        {

        }
        //public static int GetNumConsultas() {
        //    int nCount = 0;
        //    nCount = DAL.Administracion.GetNumConsultas();
        //    return (nCount < 0 ? 0 : nCount);
        //}

        public static string CatalogoConsultas(string sEstado)
        {
            StringBuilder sb = new StringBuilder();
            string sColor = "black";
            int nConsultas = 0;
            sb.Append("<table id='tblConsultas' style='width:500px;'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:20px;' />");
            sb.Append("     <col style='width:50px; ' />");
            sb.Append("     <col style='width:10px;' />");
            sb.Append("     <col style='width:420px; padding-left:3px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody id='tbodyDatos'>");

            SqlDataReader dr = DAL.Administracion.CatalogoConsultas(short.Parse(sEstado));
            while (dr.Read())
            {
                nConsultas = int.Parse(dr["num_consultas"].ToString());
                sb.Append("<tr id='" + dr["t674_idconsulta"].ToString() + "' ");
                if ((bool)dr["t674_estado"])
                {
                    sb.Append("activa='1' ");
                    sColor = "black";
                }
                else
                {
                    sb.Append("activa='0' ");
                    sColor = "#CCCCCC";
                }
                sb.Append("procalm='" + dr["t674_procalm"].ToString() + "' ");
                sb.Append("num_parametros='" + dr["num_parametros"].ToString() + "' ");
                sb.Append("titulo='" + Utilidades.escape(dr["t674_descripcion"].ToString()) + "' ");
                sb.Append("onclick='ms(this);' ");
                sb.Append("style='height:20px;color:" + sColor + ";");
                if (dr["t674_descripcion"].ToString() != "")
                    sb.Append("noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/imgComGastoOn.gif' style='vertical-align:middle'>  Comentario] body=[" + Utilidades.CadenaParaTooltipExtendido(dr["t674_descripcion"].ToString()) + "] hideselects=[off]\"");
                else
                    sb.Append("' ");

                sb.Append("><td>");
                //Reordenación comentada, ya que las consultas no son por usuario.
                //sb.Append("<img src='../../images/imgMoveRow.gif' style='cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' >");
                sb.Append("</td>");
                sb.Append("<td class='MA' ondblclick='detalle(this.parentNode);' style='text-align:right;'>");
                sb.Append("<nobr class='NBR W35'>"+ dr["t674_idconsulta"].ToString() + "</nobr></td>");
                sb.Append("<td>-</td>");
                sb.Append("<td class='MA' ondblclick='ejecutar(this.parentNode);'><nobr class='NBR W420' ");
                sb.Append(">" + dr["t674_denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("@#@" + nConsultas);
            return sb.ToString();
        }

        public static string ObtenerParametros(int nIdConsulta)
        {
            StringBuilder sb = new StringBuilder();
            string sChecked = "", sValor = "";
            sb.Append("<table id='tblDatos' style='width:700px; text-align:left'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:150px; ' />");
            sb.Append("     <col style='width:150px;' />");
            sb.Append("     <col style='width:400px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = DAL.Administracion.SelectByIdconsulta(null, nIdConsulta);

            while (dr.Read())
            {
                sChecked = "";
                sValor = "";
                sb.Append("<tr style=\"height: 20px; ");
                if (dr["t675_visible"].ToString() == "N") sb.Append("display:'none'; ");
                else sb.Append("display:'block'; ");
                sb.Append("\" ");
                if ((bool)dr["t675_opcional"]) sb.Append("opcional=1 ");
                else sb.Append("opcional=0 ");
                sb.Append("tipoparam='" + dr["t675_tipoparametro"].ToString() + "' >");
                //sb.Append("<td>");
                switch (dr["t675_tipoparametro"].ToString())
                {
                    case "I":
                        sb.Append("<td title='Entero' style='padding-left:3px;'>" + dr["t675_nombreparametro"].ToString() + "</td>");
                        sb.Append("<td>");
                        if (dr["t675_valordefecto"].ToString() != "") sValor = int.Parse(dr["t675_valordefecto"].ToString()).ToString("#,##0");
                        if (dr["t675_visible"].ToString() == "M") sb.Append("<input type='text' class='txtNumM' style='width:60px;' value='" + sValor + "' onfocus='fn(this,9,0)' />");
                        else sb.Append("<input type='text' class='txtNumL' style='width:60px;' value='" + dr["t675_valordefecto"].ToString() + "' readonly />");
                        break;
                    case "V":
                        sb.Append("<td title='Carácter' style='padding-left:3px;'>" + dr["t675_nombreparametro"].ToString() + "</td>");
                        sb.Append("<td title='" + dr["t675_valordefecto"].ToString() + "'>");
                        if (dr["t675_visible"].ToString() == "M") sb.Append("<input type='text' class='txtM' style='width:100px;' maxlength='7500' value='" + dr["t675_valordefecto"].ToString() + "' />");
                        else sb.Append("<input type='text' class='txtL' style='width:140px;' value='" + dr["t675_valordefecto"].ToString() + "' readonly />");
                        break;
                    case "M":
                        sb.Append("<td title='Importe' style='padding-left:3px;'>" + dr["t675_nombreparametro"].ToString() + "</td>");
                        sb.Append("<td>");
                        if (dr["t675_valordefecto"].ToString() != "") sValor = double.Parse(dr["t675_valordefecto"].ToString()).ToString("N");
                        if (dr["t675_visible"].ToString() == "M") sb.Append("<input type='text' class='txtNumM' style='width:60px;' value='" + sValor + "' onfocus='fn(this,9,2)' />");
                        else sb.Append("<input type='text' class='txtNumL' style='width:60px;' value='" + sValor + "' readonly />");
                        break;
                    case "D":
                        sb.Append("<td title='Fecha' style='padding-left:3px;'>" + dr["t675_nombreparametro"].ToString() + "</td>");
                        sb.Append("<td>");
                        if (dr["t675_visible"].ToString() == "M")
                        {
                            if (HttpContext.Current.Session["GVT_BTN_FECHA"].ToString() == "I")
                                sb.Append("<input type='text' id='" + dr["t674_idconsulta"].ToString() + dr["t675_textoparametro"].ToString().Trim() + "' class='txtM MANO' style='width:60px;' value='" + dr["t675_valordefecto"].ToString() + "' Calendar='oCal' onclick='mc(this);' ReadOnly='true' />");
                            else
                                sb.Append("<input type='text' id='" + dr["t674_idconsulta"].ToString() + dr["t675_textoparametro"].ToString().Trim() + "' class='txtM MANO' style='width:60px;' value='" + dr["t675_valordefecto"].ToString() + "' Calendar='oCal' onfocus='focoFecha(this);' onmousedown='mc1(this)'/>");
                        }
                        else sb.Append("<input type='text' id='" + dr["t674_idconsulta"].ToString() + dr["t675_textoparametro"].ToString().Trim() + "' class='txtM MANO' style='width:60px; ' value='" + dr["t675_valordefecto"].ToString() + "' readonly />");
                        break;
                    case "B":
                        sb.Append("<td title='Booleano' style='padding-left:3px;'>" + dr["t675_nombreparametro"].ToString() + "</td>");
                        sb.Append("<td>");
                        if (dr["t675_valordefecto"].ToString() == "1") sChecked = "checked";
                        else sChecked = "";
                        if (dr["t675_visible"].ToString() == "M") sb.Append("<input type='checkbox' class='check' " + sChecked + " />");
                        else sb.Append("<input type='checkbox' class='check' " + sChecked + " disabled />");
                        break;
                    case "A":
                        sb.Append("<td title='Mes y año' style='padding-left:3px;'>" + dr["t675_nombreparametro"].ToString() + "</td>");
                        sb.Append("<td>");
                        if (dr["t675_visible"].ToString() == "M") sb.Append("<input type='text' class='txtM MANO' style='width:90px; text-align:center;' value='" + dr["t675_valordefecto"].ToString() + " ' ReadOnly='true' onclick='getMesValor(this)' />");
                        else sb.Append("<input type='text' class='txtM MANO' style='width:90px; text-align:center;' value='" + dr["t675_valordefecto"].ToString() + "' readonly />");
                        break;
                }
                sb.Append("</td>");

                sb.Append("<td onmouseover='TTip(event)' style='padding-left:3px;'><nobr class='NBR W390'>" + dr["t675_comentarioparametro"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();
        }

        public static string ejecutarConsulta(string sProdAlm, string sParametros)
        {
            StringBuilder sb = new StringBuilder();
            string sAux = "", sPrimer = "";
            string[] aParametros = Regex.Split(sParametros, "///");
            object[] aObjetos = new object[(sParametros == "") ? 0 : aParametros.Length];
            //object[] aObjetos = new object[(sParametros == "") ? 1 : aParametros.Length + 1];
            //aObjetos[0] = (int)HttpContext.Current.Session["GVT_USUARIOSUPER"];
            int v = 0;
            foreach (string oParametro in aParametros)
            {
                if (oParametro == "") continue;
                string[] aDatos = Regex.Split(oParametro, "##");
                switch (aDatos[0])
                {
                    case "A": aObjetos[v] = int.Parse(aDatos[1]); break;
                    case "M": aObjetos[v] = double.Parse(aDatos[1].Replace(".", ",")); break;
                    default: aObjetos[v] = aDatos[1]; break;
                }

                v++;
            }
            DataSet ds = DAL.Administracion.EjecutarConsulta(sProdAlm, aObjetos);

            for (int i = 0; i < ds.Tables.Count; i++)
            {
                sb.Append("<table id='tblDatos' style='font-family:Arial; font-size:8pt;' border='1'>");
                sb.Append("<tbody>");
                bool bTitulos = false;

                for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                {
                    if (!bTitulos)
                    {
                        sb.Append("<tr  style='text-align:center'>");
                        for (int x = 0; x < ds.Tables[i].Columns.Count; x++)
                        {
                            sb.Append("<td style='background-color:#BCD4DF; font-weight:bold;'>" + ds.Tables[i].Columns[x].ColumnName + "</td>");
                        }
                        sb.Append("</tr>");
                        bTitulos = true;
                    }
                    sb.Append("<tr>");
                    for (int x = 0; x < ds.Tables[i].Columns.Count; x++)
                    {
                        sAux = ds.Tables[i].Rows[j][x].ToString();

                        if (ds.Tables[i].Columns[x].DataType.Name == "text" && sAux.Trim() != "")
                        {//Para el contenido de campos de tipo Text hacemos transformaciones para que no falle la exportación a Excel
                            sAux = sAux.Replace("<", " < ");
                            sAux = sAux.Replace(">", " > ");
                            sAux = sAux.Trim();
                            sPrimer = sAux.Substring(0, 1);
                            switch (sPrimer)
                            {
                                case "-":
                                case "+":
                                case "=":
                                    sAux = "(" + sPrimer + ")" + sAux.Substring(1);
                                    break;
                            }
                        }
                        sb.Append("<td>" + sAux + "</td>");
                    }
                    sb.Append("</tr>");
                }
                sb.Append("</tbody>");
                sb.Append("</table>{{septabla}}");
            }
            return sb.ToString();
        }

        public static DataSet ejecutarConsultaDS(string sProdAlm, string sParametros)
        {
            StringBuilder sb = new StringBuilder();
            string[] aParametros = Regex.Split(sParametros, "///");
            object[] aObjetos = new object[(sParametros == "") ? 0 : aParametros.Length];
            //object[] aObjetos = new object[(sParametros == "") ? 1 : aParametros.Length + 1];
            //aObjetos[0] = (int)HttpContext.Current.Session["GVT_USUARIOSUPER"];
            int v = 0;
            foreach (string oParametro in aParametros)
            {
                if (oParametro == "") continue;
                string[] aDatos = Regex.Split(oParametro, "##");
                switch (aDatos[0])
                {
                    case "A": aObjetos[v] = int.Parse(aDatos[1]); break;
                    case "M": aObjetos[v] = double.Parse(aDatos[1].Replace(".", ",")); break;
                    default: aObjetos[v] = aDatos[1]; break;
                }

                v++;
            }

            return DAL.Administracion.EjecutarConsulta(sProdAlm, aObjetos);
        }

        public static void Grabar(string sIdConsulta, string sDes, string sEstado, string sDenom)
        {
            DAL.Administracion.UpdateConsulta(null, int.Parse(sIdConsulta), Utilidades.unescape(sDes), short.Parse(sEstado), Utilidades.unescape(sDenom));
        }

    }
}