using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Text;
using System.Text.RegularExpressions;

namespace SUPER.Capa_Negocio
{
    public partial class CEC
    {
        
		#region Propiedades y Atributos

        private int _t345_idcec;
		public int t345_idcec
		{
			get {return _t345_idcec;}
			set { _t345_idcec = value ;}
		}

		private string _t345_denominacion;
		public string t345_denominacion
		{
			get {return _t345_denominacion;}
			set { _t345_denominacion = value ;}
		}

		private bool _t345_estado;
		public bool t345_estado
		{
			get {return _t345_estado;}
			set { _t345_estado = value ;}
		}

		private int _t345_orden;
		public int t345_orden
		{
			get {return _t345_orden;}
			set { _t345_orden = value ;}
		}

		private bool _t381_obligatorio;
		public bool t381_obligatorio
		{
			get {return _t381_obligatorio;}
			set { _t381_obligatorio = value ;}
		}

		private int _t303_idnodo;
		public int t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}

        #endregion

		#region Constructores

		public CEC() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T345_CEC.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOARHUMI]	20/11/2007 8:38:39
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t345_denominacion, bool t345_estado, int t345_orden)//, bool t341_obligatorio, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t345_denominacion", SqlDbType.Text, 30);
            aParam[0].Value = t345_denominacion;
            aParam[1] = new SqlParameter("@t345_estado", SqlDbType.Bit, 1);
            aParam[1].Value = t345_estado;
            aParam[2] = new SqlParameter("@t345_orden", SqlDbType.Int, 4);
            aParam[2].Value = t345_orden;
            //aParam[3] = new SqlParameter("@t381_obligatorio", SqlDbType.Bit, 1);
            //aParam[3].Value = t381_obligatorio;
            //aParam[4] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            //aParam[4].Value = t303_idnodo;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CEC_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CEC_I", aParam));
        }
        public static int Update(SqlTransaction tr, int t345_idcec, string t345_denominacion, bool t345_estado, int t345_orden)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t345_idcec", SqlDbType.Int, 4);
            aParam[0].Value = t345_idcec;
            aParam[1] = new SqlParameter("@t345_denominacion", SqlDbType.Text, 30);
            aParam[1].Value = t345_denominacion;
            aParam[2] = new SqlParameter("@t345_estado", SqlDbType.Bit, 1);
            aParam[2].Value = t345_estado;
            aParam[3] = new SqlParameter("@t345_orden", SqlDbType.Int, 4);
            aParam[3].Value = t345_orden;
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CEC_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CEC_U", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T345_CEC.
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	20/08/2009 8:38:39
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> t345_idcec, string t345_denominacion, Nullable<bool> t345_estado, 
                                             Nullable<int> t345_orden, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@t345_idcec", SqlDbType.Int, 4);
            aParam[0].Value = t345_idcec;
            aParam[1] = new SqlParameter("@t345_denominacion", SqlDbType.Text, 30);
            aParam[1].Value = t345_denominacion;
            aParam[2] = new SqlParameter("@t345_estado", SqlDbType.Bit, 1);
            aParam[2].Value = t345_estado;
            aParam[3] = new SqlParameter("@t345_orden", SqlDbType.Int, 4);
            aParam[3].Value = t345_orden;

            aParam[4] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[4].Value = nOrden;
            aParam[5] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[5].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CEC_C", aParam);
        }
        public static SqlDataReader Asociados_A_Nodos(int t345_idcec, bool bValorAsignado)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t345_idcec", SqlDbType.Int, 4);
            aParam[0].Value = t345_idcec;
            aParam[1] = new SqlParameter("@bValorAsignado", SqlDbType.Bit, 1);
            aParam[1].Value = bValorAsignado;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CEC_NODOS", aParam);
        }
        public static SqlDataReader CatalogoCorporativosByNodo(int t303_idnodo, Nullable<bool> bActivos)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@idNodo", SqlDbType.SmallInt, 2);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@bActivos", SqlDbType.Bit, 1);
            aParam[1].Value = bActivos;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CEECorporativos_SByNodo", aParam);
        }
        public static SqlDataReader CatalogoCorporativosByListaNodos(string sListaNodos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@idsNodo", SqlDbType.VarChar, 1000);
            aParam[0].Value = sListaNodos;
            return SqlHelper.ExecuteSqlDataReader("SUP_CEECorporativos_SByListaNodo", aParam);
        }
        public static SqlDataReader CatalogoCorporativosByPSN(int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CEECorporativos_ByPSN", aParam);
        }
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CEEC", aParam);
        }
        public static int Delete(SqlTransaction tr, int t345_idcec)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t345_idcec", SqlDbType.Int, 4);
            aParam[0].Value = t345_idcec;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CEC_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CEC_D", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene el nº de registros que se están usando de un criterio estadístico
        /// pasando por parámetro el código de criterio estadístico.
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	04/08/2009 13:10:01
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int numCECusados(int t345_idcec)
        {
            int iRes = 0;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t345_idcec", SqlDbType.Int, 4);
            aParam[0].Value = t345_idcec;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            iRes = Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CEC_Count", aParam));
            return iRes;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla t345_CEC y devuelve una instancia u objeto del tipo CEC
        /// </summary>
        /// <returns></returns>
        /// <history></history>
        /// -----------------------------------------------------------------------------
        public static CEC SelectDescFK(Nullable<int> t303_idnodo, int t345_idcec)
        {
            CEC o = new CEC();

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@t345_idcec", SqlDbType.Int, 4);
            aParam[1].Value = t345_idcec;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_CEC_SDes", aParam);

            if (dr.Read())
            {
                if (dr["t345_idcec"] != DBNull.Value)
                    o.t345_idcec = (int)dr["t345_idcec"];
                if (dr["t345_denominacion"] != DBNull.Value)
                    o.t345_denominacion = (string)dr["t345_denominacion"];
                if (dr["t345_estado"] != DBNull.Value)
                    o.t345_estado = (bool)dr["t345_estado"];
                if (dr["t345_orden"] != DBNull.Value)
                    o.t345_orden = byte.Parse(dr["t345_orden"].ToString());
                if (dr["t381_obligatorio"] != DBNull.Value)
                    o.t381_obligatorio = (bool)dr["t381_obligatorio"];
                if (dr["t303_idnodo"] != DBNull.Value)
                    o.t303_idnodo = short.Parse(dr["t303_idnodo"].ToString());
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de criterio estadístico"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
        public static string Busqueda(
                                         string sDepartamentos,
                                         string sCEEC,
                                         string sValores
                                     )
        {
            StringBuilder sb = new StringBuilder();
            int numContLineas = 0;

            /////tabla de Proyectos/cr/criterios

            sb.Append("<table id='tblDatos' class='MANO' style='width:960px;'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:20px' />");   // Categoria
            sb.Append("     <col style='width:20px' />");   // Cualidad
            sb.Append("     <col style='width:20px' />");   // Estado
            sb.Append("     <col style='width:50px;' />");  // Nro proyecto
            sb.Append("     <col style='width:200px;' />"); // Descripcion Proyecto
            sb.Append("     <col style='width:250px;' />"); // Denominacion CR
            sb.Append("     <col style='width:250px;' />"); // Denominacion CEEC
            sb.Append("     <col style='width:150px;' />"); // Denominacion Valor
            sb.Append("</colgroup>");

            // sin hacer el procedimiento
            SqlDataReader dr = SUPER.Capa_Datos.CEC.Busqueda(sDepartamentos, sCEEC, sValores);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t305_idproyectosubnodo"].ToString() + "'");
                //sb.Append("<tr id='" + dr["t301_idproyecto"].ToString() + "'");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");

                sb.Append("style='height:20px'>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td");

                sb.Append(" style='text-align:right; padding-right:5px;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td><div class='NBR W200' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" >" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</div></td>");
// seguir
                sb.Append("<td style='padding-left:5px; padding-right:2px;'><nobr class='NBR W245'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td style='padding-left:5px; padding-right:2px;'><nobr class='NBR W245'>" + dr["DenomCEEC"].ToString() + "</nobr></td>");
                sb.Append("<td style='padding-left:5px; padding-right:2px;'><nobr class='NBR W150'>" + dr["DenomValor"].ToString() + "</nobr></td>");

                sb.Append("</tr>");
                numContLineas++;
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString() + "@#@" + numContLineas;
        }
        #endregion
    }
}
