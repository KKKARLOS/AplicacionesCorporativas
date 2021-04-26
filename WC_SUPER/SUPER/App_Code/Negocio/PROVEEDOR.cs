using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Web;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : PROVEEDOR
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T315_PROVEEDOR
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	22/04/2008 15:51:59	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class PROVEEDOR
	{

		#region Metodos

        public static SqlDataReader SelectByNombre(Nullable<int> t315_idproveedor, string t315_denominacion, byte nOrden, byte nAscDesc, string sTipoBusqueda, bool bProfesionales)
		{
			SqlParameter[] aParam = new SqlParameter[7];
			aParam[0] = new SqlParameter("@t315_idproveedor", SqlDbType.Int, 4);
			aParam[0].Value = t315_idproveedor;
			aParam[1] = new SqlParameter("@t315_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t315_denominacion;

            aParam[2] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[2].Value = nOrden;
            aParam[3] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[3].Value = nAscDesc;
            aParam[4] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[4].Value = sTipoBusqueda;
            aParam[5] = new SqlParameter("@t315_provrec", SqlDbType.Bit, 1);
            aParam[5].Value = bProfesionales;
            aParam[6] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[6].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
            

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_PROVEEDOR_ByNombre", aParam);
		}
        public static SqlDataReader SelectByNombreHuecos(string t315_denominacion, string sCodigoExterno, string sTipoBusqueda, bool bEstado)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t315_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t315_denominacion;
            aParam[1] = new SqlParameter("@t315_codigoexterno", SqlDbType.VarChar, 15);
            aParam[1].Value = sCodigoExterno;
            aParam[2] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[2].Value = sTipoBusqueda;
            aParam[3] = new SqlParameter("@t315_estado", SqlDbType.Bit, 1);
            aParam[3].Value = bEstado;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_PROVEEDOR_HUECOS", aParam);
        }
        public static int Upd_Control_Huecos(SqlTransaction tr, int t315_idproveedor, bool t315_controlhuecos)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t315_idproveedor", SqlDbType.SmallInt, 2);
            aParam[0].Value = t315_idproveedor;
            aParam[1] = new SqlParameter("@t315_controlhuecos", SqlDbType.Bit, 1);
            aParam[1].Value = t315_controlhuecos;

            // Ejecuta la query y devuelve el numero de registros modificados.
            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("SUP_PROVEEDOR_U_CH", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROVEEDOR_U_CH", aParam);

            return returnValue;
        }
        public static SqlDataReader Catalogo(string t315_denominacion, string sTipoBusqueda, Nullable<int> t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t315_denominacion", SqlDbType.Text, 50);
			aParam[0].Value = t315_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_PROVEEDOR_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_PROVEEDOR_CAT_USU", aParam);
        }
        public static SqlDataReader Excel(string strCodigos, string sEstado, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@CODIGO", SqlDbType.VarChar, 4200);
            aParam[0].Value = strCodigos;
            aParam[1] = new SqlParameter("@ESTADO", SqlDbType.VarChar, 1);
            aParam[1].Value = sEstado;
            aParam[2] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[2].Value = t422_idmoneda;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_RECURSOSEXTERNOS_C", aParam);
        }
        public static SqlDataReader ObtenerConsumos(
                   int t314_idusuario,
                   DateTime nDesde,
                   DateTime nHasta,
                   Nullable<int> nNivelEstructura,
                   string t301_categoria,
                   string t305_cualidad,
                   string sProyectos,
                   string sClientes,
                   string sResponsables,
                   string sNaturalezas,
                   string sHorizontal,
                   string sModeloContrato,
                   string sContrato,
                   string sIDEstructura,
                   string sSectores,
                   string sSegmentos,
                   string sProveedores,
                   bool bComparacionLogica,
                   string sCNP,
                   string sCSN1P,
                   string sCSN2P,
                   string sCSN3P,
                   string sCSN4P,
                   string t422_idmoneda
               )
        {
            SqlParameter[] aParam = new SqlParameter[24];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@nNivelEstructura", SqlDbType.TinyInt, 2);
            aParam[3].Value = nNivelEstructura;
            aParam[4] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[4].Value = t301_categoria;
            aParam[5] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[5].Value = t305_cualidad;
            aParam[6] = new SqlParameter("@sProyectos", SqlDbType.VarChar, 8000);
            aParam[6].Value = sProyectos;
            aParam[7] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[7].Value = sClientes;
            aParam[8] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[8].Value = sResponsables;
            aParam[9] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[9].Value = sNaturalezas;
            aParam[10] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[10].Value = sHorizontal;
            aParam[11] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[11].Value = sModeloContrato;
            aParam[12] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[12].Value = sContrato;
            aParam[13] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[13].Value = sIDEstructura;
            aParam[14] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[14].Value = sSectores;
            aParam[15] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[15].Value = sSegmentos;
            aParam[16] = new SqlParameter("@sProveedores", SqlDbType.VarChar, 8000);
            aParam[16].Value = sProveedores;
            aParam[17] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[17].Value = bComparacionLogica;
            aParam[18] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[18].Value = sCNP;
            aParam[19] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[19].Value = sCSN1P;
            aParam[20] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[20].Value = sCSN2P;
            aParam[21] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[21].Value = sCSN3P;
            aParam[22] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[22].Value = sCSN4P;
            aParam[23] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[23].Value = t422_idmoneda;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOS_PROVEEDOR_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOS_PROVEEDOR_USU", aParam);
        }

        public static SqlDataReader ObtenerProveedoresSegunVisionUsuario(SqlTransaction tr, int nUsuario, bool bSoloActivos)
        {
            SqlParameter[] aParam = null;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {
                aParam = new SqlParameter[1];
                aParam[0] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
                aParam[0].Value = bSoloActivos;
            }
            else
            {
                aParam = new SqlParameter[2];
                aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                aParam[0].Value = nUsuario;
                aParam[1] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
                aParam[1].Value = bSoloActivos;
            }

            if (tr == null)
                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_PROV_ADM", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_PROV_USU", aParam);
            else
                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETVISION_PROV_ADM", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETVISION_PROV_USU", aParam);
        }
		#endregion
	}
}
