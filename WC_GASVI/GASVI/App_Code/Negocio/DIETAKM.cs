using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GASVI.DAL;
using System.Text;
using System.Text.RegularExpressions;

namespace GASVI.BLL
{
	public partial class DIETAKM
	{
		#region Propiedades y Atributos

		private byte _t069_iddietakm;
		public byte t069_iddietakm
		{
			get {return _t069_iddietakm;}
			set { _t069_iddietakm = value ;}
		}

		private string _t069_descripcion;
		public string t069_descripcion
		{
			get {return _t069_descripcion;}
			set { _t069_descripcion = value ;}
		}

		private decimal _t069_icdc;
		public decimal t069_icdc
		{
			get {return _t069_icdc;}
			set { _t069_icdc = value ;}
		}

		private decimal _t069_icmd;
		public decimal t069_icmd
		{
			get {return _t069_icmd;}
			set { _t069_icmd = value ;}
		}

		private decimal _t069_icda;
		public decimal t069_icda
		{
			get {return _t069_icda;}
			set { _t069_icda = value ;}
		}

		private decimal _t069_icde;
		public decimal t069_icde
		{
			get {return _t069_icde;}
			set { _t069_icde = value ;}
		}

		private decimal _t069_ick;
		public decimal t069_ick
		{
			get {return _t069_ick;}
			set { _t069_ick = value ;}
		}

		#endregion

		#region Constructor

        public DIETAKM()
        {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}
        public DIETAKM(byte iddietakm, 
                        string descripcion, 
                        decimal icdc,
                        decimal icmd,
                        decimal icda,
                        decimal icde,
                        decimal ick
        )
        {
			this.t069_iddietakm = iddietakm;
            this.t069_descripcion = descripcion; 
            this.t069_icdc = icdc;
            this.t069_icmd = icmd;
            this.t069_icda = icda;
            this.t069_icde = icde;
            this.t069_ick = ick;
		}
		#endregion

        public static string CatalogoDietaKm()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblImportesConvenio' cellpadding='0' cellspacing='0' border='0'  class='MANO W700' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:15px; cursor:pointer;' />");
            sb.Append("    <col style='width:250px; cursor:pointer;' />");
            sb.Append("    <col style='width:86px; cursor:pointer;' />");
            sb.Append("    <col style='width:86px; cursor:pointer;' />");
            sb.Append("    <col style='width:87px; cursor:pointer;' />");
            sb.Append("    <col style='width:86px; cursor:pointer;' />");
            sb.Append("    <col style='width:50px; cursor:pointer;' />");
            sb.Append("    <col style='width:40px; cursor:pointer;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.DIETAKM.CatalogoDietaKm();
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T069_iddietakm"].ToString() + "' ");
                sb.Append("bd='' ");
                sb.Append("onKeyUp='fm(event)' onClick=\"ms(this,'FG');\"");
                sb.Append("style='height:20px'> ");
                sb.Append("<td style=\"border-right:0px;\"><img src='../../../images/imgFN.gif'></td>");

                sb.Append("<td><input id='txtDenomi" + dr["T069_iddietakm"].ToString() + "' type='text' style='width:245px;' class='txtL' onfocus='gf;' onchange='activarGrabar();' value=\"");
                sb.Append(dr["T069_descripcion"].ToString() + "\" maxlength='50'></td>");

                sb.Append("<td><input id='txtCompleta" + dr["T069_iddietakm"].ToString() + "' type='text' class='txtNumL' style='width:77px; padding-right:5px;'  onfocus='fn(this);gf;' onchange='activarGrabar();' value=\"");
                if(float.Parse(dr["T069_icdc"].ToString()) > 0) sb.Append(((decimal)dr["T069_icdc"]).ToString("N"));
                sb.Append("\" maxlength='25'></td>");

                sb.Append("<td><input id='txtMedia" + dr["T069_iddietakm"].ToString() + "' class='txtNumL' type='text' style='width:77px; padding-right:5px;'  onfocus='fn(this);gf;' onchange='activarGrabar();' value=\"");
                if (float.Parse(dr["T069_icmd"].ToString()) > 0) sb.Append(((decimal)dr["T069_icmd"]).ToString("N"));
                sb.Append("\" maxlength='25'></td>");

                sb.Append("<td><input id='txtAlojamiento" + dr["T069_iddietakm"].ToString() + "' type='text' style='width:77px; padding-right:5px;' class='txtNumL' onfocus='fn(this);gf;' onchange='activarGrabar();' value=\"");
                if (float.Parse(dr["T069_icda"].ToString()) > 0) sb.Append(((decimal)dr["T069_icda"]).ToString("N"));
                sb.Append("\" maxlength='25'></td>");

                sb.Append("<td><input id='txtEspecial" + dr["T069_iddietakm"].ToString() + "' type='text' style='width:77px; padding-right:5px;' class='txtNumL' onfocus='fn(this);gf;' onchange='activarGrabar();' value=\"");
                if (float.Parse(dr["T069_icde"].ToString()) > 0) sb.Append(((decimal)dr["T069_icde"]).ToString("N"));
                sb.Append("\" maxlength='25'></td>");

                sb.Append("<td><input id='txtKm" + dr["T069_iddietakm"].ToString() + "' type='text' style='width:45px; padding-right:5px;' class='txtNumL' onfocus='fn(this);gf;' onchange='activarGrabar();' value=\"");
                if (float.Parse(dr["T069_ick"].ToString()) > 0) sb.Append(((decimal)dr["T069_ick"]).ToString("N"));
                sb.Append("\" maxlength='4'></td>");

                sb.Append("<td><input type='checkbox' style='width:15px; cursor:pointer; margin-left:12px'");
                sb.Append(" name='chkActivo" + dr["T069_iddietakm"].ToString() + "' id='chkActivo" + dr["T069_iddietakm"].ToString() + "' class='checkTabla'");
                if (dr["t069_estado"].ToString() == "True")
                    sb.Append(" checked");
                sb.Append(" onclick='activarGrabar(); fm(event);'></td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string Grabar(string sDatos) {
            string sResul = "", sElementosInsertados = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;
            int nAux = 0;//, nDel = 0, nIdBono = -1, nFechas = 0;
            bool bErrorControlado = false;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch (Exception)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
                #region Importes convenio
                if (sDatos != "")
                {
                    //Con la cadena generamos una lista y la recorremos para grabar cada elemento
                    string[] aGrabar = Regex.Split(sDatos, "#sFin#");
                    for (int i = 0, nCount=aGrabar.Length; i <nCount ; i++)
                    {
                        string[] aElem = Regex.Split(aGrabar[i], "#sCad#");
                        switch (aElem[0])
                        {
                            case "I":
                                nAux = DAL.DIETAKM.InsertImporteConvenio(tr, 
                                                                        aElem[2], 
                                                                        float.Parse(aElem[3]), 
                                                                        float.Parse(aElem[4]), 
                                                                        float.Parse(aElem[5]), 
                                                                        float.Parse(aElem[6]), 
                                                                        float.Parse(aElem[7]), 
                                                                        short.Parse(aElem[8]));
                                sElementosInsertados += aElem[1] + "#sCad#" + nAux.ToString() + "#sFin#";
                                break;
                            case "D":
                                DAL.DIETAKM.DeleteImporteConvenio(tr, short.Parse(aElem[1]));
                                break;
                            case "U":
                                DAL.DIETAKM.UpdateImporteConvenio(tr,
                                                                short.Parse(aElem[1]),
                                                                aElem[2],
                                                                float.Parse(aElem[3]),
                                                                float.Parse(aElem[4]),
                                                                float.Parse(aElem[5]),
                                                                float.Parse(aElem[6]),
                                                                float.Parse(aElem[7]),
                                                                short.Parse(aElem[8]));
                                break;
                        }
                    }
                }
                if (sElementosInsertados != "") sElementosInsertados = sElementosInsertados.Substring(0, sElementosInsertados.Length - 6);
                #endregion

                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                if (bErrorControlado) sResul = ex.Message;
                else sResul = Errores.mostrarError("Error al grabar el importe de convenio.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            return sElementosInsertados;
        }

	}
}
