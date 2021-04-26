using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : ESCENARIOSPAR
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T790_ESCENARIOSPAR
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	09/05/2012 9:16:04	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ESCENARIOSPAR
	{
		#region Propiedades y Atributos

		private int _t790_idescenariopar;
		public int t790_idescenariopar
		{
			get {return _t790_idescenariopar;}
			set { _t790_idescenariopar = value ;}
		}

		private int _t789_idescenario;
		public int t789_idescenario
		{
			get {return _t789_idescenario;}
			set { _t789_idescenario = value ;}
		}

		private int _t437_idpartidaeco;
		public int t437_idpartidaeco
		{
			get {return _t437_idpartidaeco;}
			set { _t437_idpartidaeco = value ;}
		}

		private bool _t790_computable;
		public bool t790_computable
		{
			get {return _t790_computable;}
			set { _t790_computable = value ;}
		}
		#endregion

		#region Constructor

		public ESCENARIOSPAR() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

        public static string ObtenerPartidas(int t789_idescenario)
		{
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblPartidas' style='width:400px;'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:350px;' />");
            sb.Append(" <col style='width:50px;' />");
            sb.Append("</colgroup>");

            string sGrupo = "";

            SqlDataReader dr = Capa_Datos.ESCENARIOSPAR.ObtenerPartidas(null, t789_idescenario);
            while (dr.Read())
            {
                if (dr["t326_denominacion"].ToString() != sGrupo)
                {
                    sGrupo = dr["t326_denominacion"].ToString();
                    sb.Append("<tr>");
                    sb.Append("<td><nobr class='NBR W330' onmouseover='TTip(event)'>" + dr["t326_denominacion"].ToString() + "<nobr></td>");
                    sb.Append("<td></td>");
                    sb.Append("</tr>");
                }
                sb.Append("<tr bd='' ");
                sb.Append("idPartida='" + dr["t437_idpartidaeco"].ToString() + "'");
                sb.Append(">");
                sb.Append("<td><nobr class='NBR W300' style='margin-left:25px;' onmouseover='TTip(event)'>" + dr["t437_denominacion"].ToString() + "<nobr></td>");
                sb.Append("<td style='text-align:center;'><input type='checkbox' class='check' " + (((int)dr["Utilizado"]==1) ? "checked" : "") + "></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
		}

        public static Hashtable ObtenerCatalogoEscenario(SqlTransaction tr, int t789_idescenario)
        {
            Hashtable oHT = new Hashtable();
            SqlDataReader dr = Capa_Datos.ESCENARIOSPAR.ObtenerCatalogoEscenario(tr, t789_idescenario);

            while (dr.Read())
            {
                oHT.Add((int)dr["t437_idpartidaeco"], new int[] { (int)dr["t790_idescenariopar"], (int)dr["t437_idpartidaeco"] });
            }
            dr.Close();
            dr.Dispose();

            return oHT;
        }

		#endregion
	}
}
