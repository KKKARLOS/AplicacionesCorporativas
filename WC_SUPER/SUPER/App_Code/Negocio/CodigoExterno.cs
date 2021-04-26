using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
//Para el stringbuilder
using System.Text;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : codigoexterno
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: codigoexterno
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	03/01/2007 11:22:22	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class codigoexterno
	{
		#region Propiedades y Atributos

		private int _t302_idcliente;
		public int t302_idcliente
		{
			get {return _t302_idcliente;}
			set { _t302_idcliente = value ;}
		}

		private int _t314_idusuario;
        public int t314_idusuario
		{
            get { return _t314_idusuario; }
            set { _t314_idusuario = value; }
		}

		private string _t360_codigoexterno;
        public string t360_codigoexterno
		{
            get { return _t360_codigoexterno; }
            set { _t360_codigoexterno = value; }
		}
		#endregion

		#region Constructores

		public codigoexterno() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla codigoexterno.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	03/01/2007 11:22:22
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t302_idcliente , int numerosuper , string desc)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
			aParam[0].Value = t302_idcliente;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = numerosuper;
			aParam[2] = new SqlParameter("@t360_codigoexterno", SqlDbType.Text, 15);
			aParam[2].Value = desc;

			if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_CODEXT_I", aParam);
			else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CODEXT_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla codigoexterno.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	03/01/2007 11:22:22
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t302_idcliente, int numerosuper, string desc)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
			aParam[0].Value = t302_idcliente;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = numerosuper;
            aParam[2] = new SqlParameter("@t360_codigoexterno", SqlDbType.Text, 15);
            aParam[2].Value = desc;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CODEXT_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CODEXT_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla codigoexterno a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	03/01/2007 11:22:22
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t302_idcliente, int numerosuper, string desc)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
			aParam[0].Value = t302_idcliente;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = numerosuper;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CODEXT_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CODEXT_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla codigoexterno,
		/// y devuelve una instancia u objeto del tipo codigoexterno
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	03/01/2007 11:22:22
		/// </history>
		/// -----------------------------------------------------------------------------
        public static codigoexterno Select(SqlTransaction tr, int t302_idcliente, int numerosuper, string desc) 
		{
			codigoexterno o = new codigoexterno();

			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
			aParam[0].Value = t302_idcliente;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = numerosuper;
            aParam[2] = new SqlParameter("@t360_codigoexterno", SqlDbType.Text, 15);
            aParam[2].Value = desc;

			SqlDataReader dr;
			if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_CODEXT_S", aParam);
			else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CODEXT_S", aParam);

			if (dr.Read())
			{
				if (dr["t302_idcliente"] != DBNull.Value)
					o.t302_idcliente = (int)dr["t302_idcliente"];
                if (dr["t314_idusuario"] != DBNull.Value)
                    o.t314_idusuario = (int)dr["t314_idusuario"];
                if (dr["t360_codigoexterno"] != DBNull.Value)
                    o.t360_codigoexterno = (string)dr["t360_codigoexterno"];
			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de codigoexterno"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla codigoexterno en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	03/01/2007 11:22:22
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader SelectByCliente(int t302_idcliente, bool bMostrarBajas) 
		{
			SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[0].Value = t302_idcliente;
            aParam[1] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[1].Value = bMostrarBajas;

            return SqlHelper.ExecuteSqlDataReader("SUP_CODEXT_SByCliente", aParam);
		}
        //Añadido manualmente
        public static string ObtenerIntegrantes(int iCodCliente, bool bMostrarBajas)
        {// Devuelve el código HTML del catalogo de personas que son códigos externos del cliente que se pasa como parametro
            StringBuilder sb = new StringBuilder();
            string sCodExt, sCodSuper, sNombre;
            try
            {
                SqlDataReader dr = SelectByCliente(iCodCliente, bMostrarBajas);

                sb.Append("<table id='tblOpciones2' class='texto MM' style='width: 495px;' mantenimiento='1'>");
                sb.Append("<colgroup><col style='width:10px;' /><col style='width:20px;' /><col style='width:310px;' /><col style='width:155px;' /></colgroup>");//padding-left:5px
                sb.Append("<tbody id='tbodyDestino'>");
                while (dr.Read())
                {
                    sCodSuper = dr["t314_idusuario"].ToString();
                    sNombre = dr["nombrecompleto"].ToString();
                    sCodExt = dr["t360_codigoexterno"].ToString();

                    sb.Append("<tr id='" + sCodSuper + "' onclick='mm(event)' onKeyUp=\"mfa(this,'U')\" bd='' style='height:20px;' onmousedown='DD(event)' ");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb.Append("baja='" + dr["baja"].ToString() + "' ");
                    if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                    else sb.Append("tipo='P' ");

                    //sb.Append("><td></td><td></td><td><label class=label id='txtN'");
                    //if (sNombre.Length > 30) 
                    //    sb.Append(" title='" + sNombre + "'");
                    //sb.Append("><NOBR>" + sNombre + "</NOBR></label></td>");
                    sb.Append("><td></td><td></td><td title='" + sNombre + "'>");
                    sb.Append("<span class='NBR W310'>" + sNombre + "</span></td>");

                    sb.Append("<td><input type='text' class='txtL' style='width:140px' MaxLength=15 value='" + sCodExt + "' ></td></tr>");
                }
                sb.Append("</tbody></table>");

                return sb.ToString();
            }
            catch (Exception)
            {
                //Master.sErrores = Errores.mostrarError("Error al obtener las personas", ex);
                return "error@#@";
            }
        }

		#endregion
	}
}
