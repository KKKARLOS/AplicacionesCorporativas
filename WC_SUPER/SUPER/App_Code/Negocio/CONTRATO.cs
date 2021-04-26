using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	public partial class CONTRATO
	{
		#region Metodos

        public static SqlDataReader ObtenerContratos(Nullable<int> t303_idnodo, bool bMostrarTodos, Nullable<int> t314_idusuario, Nullable<int> t306_idcontrato, string t377_denominacion, string sTipoBusq, Nullable<int> t302_idcliente)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo;
            aParam[2] = new SqlParameter("@bMostrarTodos", SqlDbType.Bit, 1);
            aParam[2].Value = bMostrarTodos;
            aParam[3] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
            aParam[3].Value = t306_idcontrato;
            aParam[4] = new SqlParameter("@t377_denominacion", SqlDbType.VarChar, 70);
            aParam[4].Value = t377_denominacion;
            aParam[5] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[5].Value = sTipoBusq;
            aParam[6] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[6].Value = t302_idcliente;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_GETCONTRATO", aParam);
        }
        public static SqlDataReader ObtenerContratosVisionProyectos(Nullable<int> t303_idnodo, bool bMostrarTodos, int t314_idusuario, Nullable<int> t306_idcontrato, string t377_denominacion, string sTipoBusq, Nullable<int> t302_idcliente)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo;
            aParam[2] = new SqlParameter("@bMostrarTodos", SqlDbType.Bit, 1);
            aParam[2].Value = bMostrarTodos;
            aParam[3] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
            aParam[3].Value = t306_idcontrato;
            aParam[4] = new SqlParameter("@t377_denominacion", SqlDbType.VarChar, 70);
            aParam[4].Value = t377_denominacion;
            aParam[5] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[5].Value = sTipoBusq;
            aParam[6] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[6].Value = t302_idcliente;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_GETCONTRATO_VISION_PROY_ADM", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_GETCONTRATO_VISION_PROY_USU", aParam);
        }

        //public static SqlDataReader ObtenerContratosCambioEstructura(string sOpcion, string sValor)
        public static SqlDataReader ObtenerContratosCambioEstructura(Nullable<int> t303_idnodo,
                                                                     Nullable<int> t314_idusuario_gestorprod,
                                                                     Nullable<int> t302_idcliente_contrato,
                                                                     Nullable<int> t314_idusuario_comercialhermes,
                                                                     string slContratos)
        {
            //SqlParameter[] aParam = new SqlParameter[2];
            //aParam[0] = new SqlParameter("@sOpcion", SqlDbType.Char, 1);
            //aParam[0].Value = sOpcion;
            //aParam[1] = new SqlParameter("@sValor", SqlDbType.VarChar, 8000);
            //aParam[1].Value = sValor;

            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo),
                ParametroSql.add("@t314_idusuario_gestorprod", SqlDbType.Int, 4, t314_idusuario_gestorprod),
                ParametroSql.add("@t302_idcliente_contrato", SqlDbType.Int, 4, t302_idcliente_contrato),
                ParametroSql.add("@t314_idusuario_comercialhermes", SqlDbType.Int, 4, t314_idusuario_comercialhermes),
                ParametroSql.add("@sValor", SqlDbType.VarChar, 8000, slContratos),
            };
            return SqlHelper.ExecuteSqlDataReader("SUP_CAMBIOESTRUCTURA_CONTRATO_CAT", aParam);
        }

        public static SqlDataReader ObtenerContratosCambioEstructuraParesDatos(string sParesDatos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sParesDatos", SqlDbType.VarChar, 8000);
            aParam[0].Value = sParesDatos;

            return SqlHelper.ExecuteSqlDataReader("SUP_CAMBIOESTRUCTURACONTRATO_LISTA_CAT", aParam);
        }
        public static SqlDataReader ObtenerContratosCambioMasivo(string sListaContratos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sListaContratos", SqlDbType.VarChar, 8000);
            aParam[0].Value = sListaContratos;

            return SqlHelper.ExecuteSqlDataReader("SUP_CAMBIOCONTRATOMASIVO_LISTA_CAT", aParam);
        }

        public static int ModificarNodo(SqlTransaction tr, int t306_idcontrato, int t303_idnodo_destino)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
            aParam[0].Value = t306_idcontrato;
            aParam[1] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_destino;

            if (tr==null)
                return SqlHelper.ExecuteNonQuery("SUP_CONTRATO_U_NODO", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONTRATO_U_NODO", aParam);
        }

        public static int Modificar(SqlTransaction tr, int t306_idcontrato, Nullable<int> t303_idnodo,
                                    Nullable<int> t314_idusuario_gestorprod, Nullable<int> t302_idcliente_contrato,
                                    Nullable<int> t314_idusuario_responsable, Nullable<int> t314_idusuario_comercialhermes)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t306_idcontrato", SqlDbType.Int, 4, t306_idcontrato),
                ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo),
                ParametroSql.add("@t314_idusuario_gestorprod", SqlDbType.Int, 4, t314_idusuario_gestorprod),
                ParametroSql.add("@t302_idcliente_contrato", SqlDbType.Int, 4, t302_idcliente_contrato),
                ParametroSql.add("@t314_idusuario_responsable", SqlDbType.Int, 4, t314_idusuario_responsable),
                ParametroSql.add("@t314_idusuario_comercialhermes", SqlDbType.Int, 4, t314_idusuario_comercialhermes),
            };

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CONTRATO_U2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONTRATO_U2", aParam);
        }

        public static SqlDataReader ObtenerDatosEconomicos(int t305_idproyectosubnodo)
		{
			SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONTRATODATOSECO", aParam);
		}
        public static SqlDataReader CatalogoDenominacion(string t377_denominacion, string sTipoBusqueda, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t377_denominacion", SqlDbType.Text, 70);
            aParam[0].Value = t377_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CONTRATO_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CONTRATO_CAT_USU", aParam);
        }
        public static SqlDataReader Cuadre(
                   int nOpcion,
                   int t314_idusuario,
                   Nullable<int> nNivelEstructura,
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
                   bool bComparacionLogica,
                   string sCNP,
                   string sCSN1P,
                   string sCSN2P,
                   string sCSN3P,
                   string sCSN4P,
                   string sSoporteAdm,
                   bool bCuadre,
                   double dTolerancia,
                   string t422_idmoneda
               )
        {
            SqlParameter[] aParam = new SqlParameter[23];
            aParam[0] = new SqlParameter("@nOpcion", SqlDbType.Int, 4);
            aParam[0].Value = nOpcion;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@nNivelEstructura", SqlDbType.TinyInt, 2);
            aParam[2].Value = nNivelEstructura;
            aParam[3] = new SqlParameter("@sProyectos", SqlDbType.VarChar, 8000);
            aParam[3].Value = sProyectos;
            aParam[4] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[4].Value = sClientes;
            aParam[5] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[5].Value = sResponsables;
            aParam[6] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[6].Value = sNaturalezas;
            aParam[7] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[7].Value = sHorizontal;
            aParam[8] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[8].Value = sModeloContrato;
            aParam[9] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[9].Value = sContrato;
            aParam[10] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[10].Value = sIDEstructura;
            aParam[11] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[11].Value = sSectores;
            aParam[12] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[12].Value = sSegmentos;
            aParam[13] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[13].Value = bComparacionLogica;
            aParam[14] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[14].Value = sCNP;
            aParam[15] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[15].Value = sCSN1P;
            aParam[16] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[16].Value = sCSN2P;
            aParam[17] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[17].Value = sCSN3P;
            aParam[18] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[18].Value = sCSN4P;
            aParam[19] = new SqlParameter("@bCuadre", SqlDbType.Bit, 1);
            aParam[19].Value = bCuadre;
            aParam[20] = new SqlParameter("@Tolerancia", SqlDbType.Money, 8);
            aParam[20].Value = dTolerancia;
            aParam[21] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[21].Value = t422_idmoneda;
            aParam[22] = new SqlParameter("@sSoporteAdm", SqlDbType.VarChar, 8000);
            aParam[22].Value = sSoporteAdm;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CUADRE_CONTRATOS_ADM", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CUADRE_CONTRATOS_USU", aParam);
        }
        public static SqlDataReader DeUnResponsable(Nullable<int> idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = idusuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CONTRATO_RESPON", aParam);
        }
        public static SqlDataReader ObtenerExtensiones(int t306_idcontrato)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
            aParam[0].Value = t306_idcontrato;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            // return SqlHelper.ExecuteSqlDataReader("SUP_CONTRATO_EXTEN", aParam);
            return SqlHelper.ExecuteSqlDataReader("SUP_CONTRATO_EXTEN_MODIF", aParam);            
        }
        
        public static SqlDataReader ObtenerExtensionPadre(int t306_idcontrato)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
            aParam[0].Value = t306_idcontrato;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CONTRATO_EXTEN_PADRE", aParam);
        }
        public static SqlDataReader ObtenerExtensionPadre(int t306_idcontrato, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
            aParam[0].Value = t306_idcontrato;
            aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo;


            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CONTRATO_EXTEN_PADRE_NODO", aParam);
        }
        
        public static SqlDataReader ObtenerExtensionPadreByNombre(string t377_denominacion, string sTipoBusqueda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t377_denominacion", SqlDbType.Text, 70);
            aParam[0].Value = t377_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CONTRATO_EXTEN_PADRE_BY_NOMBRE", aParam);
        }

        public static SqlDataReader ProyectosCliente
            (
            Nullable<int> t314_idusuario,
            string t301_estado,
            string t301_categoria,
            Nullable<int> t301_idproyecto,
            Nullable<int> t306_idcontrato,
            Nullable<int> t303_idnodo,
            Nullable<int> t302_idcliente,
            Nullable<int> t314_idusuario_Resp_Proy,
            Nullable<int> t314_idusuario_Resp_Contr,
            string t480_pedido_cliente,
            string t480_pedido_ibermatica,
            string t422_idmoneda
            )
        {
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()) t314_idusuario = 0;

            SqlParameter[] aParam = new SqlParameter[12];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t301_estado", SqlDbType.Char, 1);
            aParam[1].Value = t301_estado;
            aParam[2] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[2].Value = t301_categoria;
            aParam[3] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[3].Value = t301_idproyecto;
            aParam[4] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
            aParam[4].Value = t306_idcontrato;
            aParam[5] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[5].Value = t303_idnodo;
            aParam[6] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[6].Value = t302_idcliente;
            aParam[7] = new SqlParameter("@t314_idusuario_Resp_Proy", SqlDbType.Int, 4);
            aParam[7].Value = t314_idusuario_Resp_Proy;
            aParam[8] = new SqlParameter("@t314_idusuario_Resp_Contr", SqlDbType.Int, 4);
            aParam[8].Value = t314_idusuario_Resp_Contr;
            aParam[9] = new SqlParameter("@t480_pedido_cliente", SqlDbType.Text, 15);
            aParam[9].Value = t480_pedido_cliente;
            aParam[10] = new SqlParameter("@t480_pedido_ibermatica", SqlDbType.Text, 15);
            aParam[10].Value = t480_pedido_ibermatica;
            aParam[11] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[11].Value = t422_idmoneda;
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            
            return SqlHelper.ExecuteSqlDataReader("SUP_CONTRATOS_PROYECTOS", 300, aParam);

        }
        public static SqlDataReader ComprobarContratoProyecto(int t306_idcontrato)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
            aParam[0].Value = t306_idcontrato;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CONTRATO_COMP_BORRADO", aParam);
        }

        //Actualiza el responsable todos los proyectosubnodos contratantes, no históricos relacionados con el contrato
        public static int SetResponsableProyectos(SqlTransaction tr, int t306_idcontrato, int t314_idusuario_responsable)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t306_idcontrato", SqlDbType.Int, 4, t306_idcontrato),
                ParametroSql.add("@t314_idusuario_responsable", SqlDbType.Int, 4, t314_idusuario_responsable)
            };

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PSN_CONTRATO_RESP_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PSN_CONTRATO_RESP_U", aParam);
        }
        //Actualiza el cliente todos los proyectosubnodos contratantes, no históricos relacionados con el contrato
        public static int SetClienteProyectos(SqlTransaction tr, int t306_idcontrato, int t302_idcliente_proyecto)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t306_idcontrato", SqlDbType.Int, 4, t306_idcontrato),
                ParametroSql.add("@t302_idcliente_proyecto", SqlDbType.Int, 4, t302_idcliente_proyecto)
            };

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PROY_CONTRATO_CLI_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROY_CONTRATO_CLI_U", aParam);
        }

        #endregion
	}
}
