using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : ESCENARIOMES
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T795_ESCENARIOMES
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	09/05/2012 17:32:36	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ESCENARIOMES
	{
		#region Propiedades y Atributos

		private int _t795_idescenariomes;
		public int t795_idescenariomes
		{
			get {return _t795_idescenariomes;}
			set { _t795_idescenariomes = value ;}
		}

		private int _t789_idescenario;
		public int t789_idescenario
		{
			get {return _t789_idescenario;}
			set { _t789_idescenario = value ;}
		}

		private int _t795_anomes;
		public int t795_anomes
		{
			get {return _t795_anomes;}
			set { _t795_anomes = value ;}
		}

		private string _t795_comentario;
		public string t795_comentario
		{
			get {return _t795_comentario;}
			set { _t795_comentario = value ;}
		}
		#endregion

		#region Constructor

		public ESCENARIOMES() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

        public static void InsertarMeses(SqlTransaction tr, int t789_idescenario, int anomes_min, int anomes_max)
        {
            Capa_Datos.ESCENARIOMES.InsertarMeses(tr, t789_idescenario, anomes_min, anomes_max);
        }

        public static Hashtable ObtenerCatalogoEscenario(SqlTransaction tr, int t789_idescenario)
        {
            Hashtable oHT = new Hashtable();
            SqlDataReader dr = Capa_Datos.ESCENARIOMES.ObtenerCatalogoEscenario(tr, t789_idescenario);

            while (dr.Read())
            {
                oHT.Add((int)dr["t795_anomes"], new int[] { (int)dr["t795_idescenariomes"], (int)dr["t795_anomes"] });
            }
            dr.Close();
            dr.Dispose();

            return oHT;
        }

        public static string ObtenerMesesBorrables(int t789_idescenario)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("<table id='tblDatos' style='width:210px;'>");
                sb.Append("<colgroup>");
                sb.Append("   <col style='width:40px;' />");
                sb.Append("   <col style='width:160px;' />");
                sb.Append("</colgroup>");

                SqlDataReader dr = SUPER.Capa_Datos.ESCENARIOMES.ObtenerMesesBorrables(null, t789_idescenario);
                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["t795_idescenariomes"].ToString() + "' bd='' style='height:20px;'>");
                    sb.Append("<td><input type='checkbox' class='checkTabla' style='margin-left:10px; cursor:pointer;'></td>");
                    sb.Append("<td>" + dr["Mes"].ToString() + "</td>");
                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();
                sb.Append("</tbody>");
                sb.Append("</table>");

                return "OK@#@" + sb.ToString();
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al obtener los meses.", ex);
            }
        }

		#endregion
	}
}
