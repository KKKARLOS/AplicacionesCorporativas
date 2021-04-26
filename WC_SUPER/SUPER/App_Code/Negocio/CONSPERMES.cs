using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : CONSPERMES
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T378_CONSPERMES
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	07/05/2008 16:32:10	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class CONSPERMES
    {
        #region Metodos

        public static void Insert(SqlTransaction tr, int t325_idsegmesproy, int t314_idusuario, double t378_unidades, decimal t378_costeunitariocon, decimal t378_costeunitariorep, Nullable<int> t303_idnodo_usuariomes, Nullable<int> t313_idempresa_nodomes)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t378_unidades", SqlDbType.Float, 8);
            aParam[2].Value = t378_unidades;
            aParam[3] = new SqlParameter("@t378_costeunitariocon", SqlDbType.Money, 8);
            aParam[3].Value = t378_costeunitariocon;
            aParam[4] = new SqlParameter("@t378_costeunitariorep", SqlDbType.Money, 8);
            aParam[4].Value = t378_costeunitariorep;
            aParam[5] = new SqlParameter("@t303_idnodo_usuariomes", SqlDbType.Int, 4);
            aParam[5].Value = t303_idnodo_usuariomes;
            aParam[6] = new SqlParameter("@t313_idempresa_nodomes", SqlDbType.Int, 4);
            aParam[6].Value = t313_idempresa_nodomes;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONSPERMES_I", aParam);
        }

        public static void DeleteByT325_idsegmesproy(SqlTransaction tr, int t325_idsegmesproy)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONSPERMES_DByT325_idsegmesproy", aParam);
        }

        public static int Delete(SqlTransaction tr, int t325_idsegmesproy, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONSPERMES_D", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Comprueba si existe consumo de un usuario en un mes
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static Nullable<double> GetUnidades_old(SqlTransaction tr, int nIdSegMesProy, int nIdRecurso)
        {
            double? dUnidades = null;
            SqlDataReader dr;

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdSegMesProy", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdRecurso", SqlDbType.Int, 4);

            aParam[0].Value = nIdSegMesProy;
            aParam[1].Value = nIdRecurso;

            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_CONSPERMES_UNIDADES_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONSPERMES_UNIDADES_S", aParam);

            if (dr.Read())
            {
                dUnidades = (double)dr[0];
            }
            dr.Close();
            dr.Dispose();
            return dUnidades;
/*
            if (tr == null)
                return (double)SqlHelper.ExecuteScalar("SUP_CONSPERMES_UNIDADES_S", aParam);
            else
                return (double)SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CONSPERMES_UNIDADES_S", aParam);
*/
        }
        public static Nullable<double> GetUnidades(SqlTransaction tr, int nIdSegMesProy, int nIdRecurso)
        {
            object oResul = null;
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nIdSegMesProy", SqlDbType.Int, 4, nIdSegMesProy);
            aParam[i++] = ParametroSql.add("@nIdRecurso", SqlDbType.Int, 4, nIdRecurso);

            if (tr == null)
                oResul = SqlHelper.ExecuteScalar("SUP_CONSPERMES_UNIDADES_S", aParam);
            else
                oResul = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CONSPERMES_UNIDADES_S", aParam);

            return (oResul == DBNull.Value) ? null : (double?)oResul;
        }

        public static void UpdateUnidades_old(SqlTransaction tr, int t325_idsegmesproy, int t314_idusuario, double t378_unidades)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t378_unidades", SqlDbType.Float, 8);
            aParam[2].Value = t378_unidades;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONSPERMES_UNIDADES_U", aParam);
        }
        public static void UpdateUnidades(SqlTransaction tr, int t325_idsegmesproy, int t314_idusuario, double t378_unidades)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t325_idsegmesproy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t378_unidades", SqlDbType.Float, 8, t378_unidades);

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONSPERMES_UNIDADES_U", aParam);
        }


        public static SqlDataReader CatalogoMesCerrado(int t325_idsegmesproy, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nSegMesProy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOPROF_MESC", aParam);
        }
        public static SqlDataReader CatalogoMesCerradoReplicado(int t325_idsegmesproy, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nSegMesProy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOPROF_MESC_REP", aParam);
        }

        public static SqlDataReader CatalogoMesCerradoSA(int t325_idsegmesproy, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nSegMesProy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOPROF_MESC_SA", aParam);
        }
        public static SqlDataReader CatalogoMesCerradoSAReplicado(int t325_idsegmesproy, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nSegMesProy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOPROF_MESC_SA_REP", aParam);
        }

        public static SqlDataReader CatalogoMesAbierto(int t325_idsegmesproy, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nSegMesProy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOPROF_MESA", aParam);
        }
        public static SqlDataReader CatalogoMesAbiertoReplicado(int t325_idsegmesproy, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nSegMesProy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOPROF_MESA_REP", aParam);
        }

        public static SqlDataReader CatalogoProdIngProfesionales(int t325_idsegmesproy, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nSegMesProy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            return SqlHelper.ExecuteSqlDataReader("SUP_PRODING_PROF_REP", aParam);
        }

        public static SqlDataReader ObtenerPSNaTraspasar(int nUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYTRASPASOJOR_C", aParam);
        }
        public static SqlDataReader ObtenerPSNaTraspasarByPSN(int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYTRASPASOJOR_C_PSN", aParam);
        }
        public static DataSet ObtenerPSNaTraspasarDS(int nUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;

            return SqlHelper.ExecuteDataset("SUP_GETPROYTRASPASOJOR_C", aParam);
        }
        public static DataSet ObtenerPSNaTraspasarByNodoDS(SqlTransaction tr, string sNodos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sNodos", SqlDbType.VarChar, 4000);
            aParam[0].Value = sNodos;

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_GETPROYTRASPASOJOR_ByNodo_C", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_GETPROYTRASPASOJOR_ByNodo_C", aParam);
        }
        public static SqlDataReader ObtenerDatosPSNaTraspasar(SqlTransaction tr, int nPSN, int nAnnomes, string sModelocoste, bool bConConsumos, bool bSinDatosEco)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;
            aParam[1] = new SqlParameter("@nAnnomes", SqlDbType.Int, 4);
            aParam[1].Value = nAnnomes;
            aParam[2] = new SqlParameter("@bConConsumos", SqlDbType.Bit, 1);
            aParam[2].Value = bConConsumos;
            aParam[3] = new SqlParameter("@bSinDatosEco", SqlDbType.Bit, 1);
            aParam[3].Value = bSinDatosEco;

            //if (sModelocoste == "J")
            //{
            //    //return SqlHelper.ExecuteSqlDataReader("SUP_TRASPASOJORNADAS_C", aParam);
            //    if (tr == null)
            //        return SqlHelper.ExecuteSqlDataReader("SUP_TRASPASOJORNADAS_C", aParam);
            //    else
            //        return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TRASPASOJORNADAS_C", aParam);
            //}
            //else
            //{
            //    //return SqlHelper.ExecuteSqlDataReader("SUP_TRASPASOHORAS_C", aParam);
            //    if (tr == null)
            //        return SqlHelper.ExecuteSqlDataReader("SUP_TRASPASOHORAS_C", aParam);
            //    else
            //        return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TRASPASOHORAS_C", aParam);
            //}
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_TRASPASOESFUERZOS_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TRASPASOESFUERZOS_C", aParam);
        }
        public static DataSet ObtenerDatosPSNaTraspasarDS(SqlTransaction tr, int nPSN, int nAnnomes, string sModelocoste, bool bConConsumos, bool bSinDatosEco)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;
            aParam[1] = new SqlParameter("@nAnnomes", SqlDbType.Int, 4);
            aParam[1].Value = nAnnomes;
            aParam[2] = new SqlParameter("@bConConsumos", SqlDbType.Bit, 1);
            aParam[2].Value = bConConsumos;
            aParam[3] = new SqlParameter("@bSinDatosEco", SqlDbType.Bit, 1);
            aParam[3].Value = bSinDatosEco;

            //if (sModelocoste == "J")
            //{
            //    if (tr == null)
            //        return SqlHelper.ExecuteDataset("SUP_TRASPASOJORNADAS_C", aParam);
            //    else
            //        return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_TRASPASOJORNADAS_C", aParam);
            //}
            //else
            //{
            //    if (tr == null)
            //        return SqlHelper.ExecuteDataset("SUP_TRASPASOHORAS_C", aParam);
            //    else
            //        return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_TRASPASOHORAS_C", aParam);
            //}
            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_TRASPASOESFUERZOS_C", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_TRASPASOESFUERZOS_C", aParam);
        }

        public static SqlDataReader ObtenerProfYProy_Profesionales(int nUsuario, Nullable<int> idNodo, string sEstado, string sCategoria, Nullable<int> idCliente, Nullable<int> nResponsable, Nullable<int> nHorizontal, Nullable<int> nContrato, int nDesde, int nHasta, Nullable<int> nIdProfesional, string sCualidad, Nullable<int> idSubnodo, string sTipo, Nullable<int> nCDPA, Nullable<int> nCDPB)
        {
            SqlParameter[] aParam = new SqlParameter[16];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
            aParam[1].Value = idNodo;
            aParam[2] = new SqlParameter("@sEstado", SqlDbType.Char, 1);
            aParam[2].Value = sEstado;
            aParam[3] = new SqlParameter("@sCategoria", SqlDbType.Char, 1);
            aParam[3].Value = sCategoria;
            aParam[4] = new SqlParameter("@idCliente", SqlDbType.Int, 4);
            aParam[4].Value = idCliente;
            aParam[5] = new SqlParameter("@nResponsable", SqlDbType.Int, 4);
            aParam[5].Value = nResponsable;
            aParam[6] = new SqlParameter("@nHorizontal", SqlDbType.Int, 4);
            aParam[6].Value = nHorizontal;
            aParam[7] = new SqlParameter("@nContrato", SqlDbType.Int, 4);
            aParam[7].Value = nContrato;
            aParam[8] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[8].Value = nDesde;
            aParam[9] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[9].Value = nHasta;
            aParam[10] = new SqlParameter("@nProfesional", SqlDbType.Int, 4);
            aParam[10].Value = nIdProfesional;
            aParam[11] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
            aParam[11].Value = sCualidad;
            aParam[12] = new SqlParameter("@idSubnodo", SqlDbType.Int, 4);
            aParam[12].Value = idSubnodo;
            aParam[13] = new SqlParameter("@sTipo", SqlDbType.Char, 1);
            aParam[13].Value = sTipo;
            aParam[14] = new SqlParameter("@nCDPA", SqlDbType.Int, 4);
            aParam[14].Value = nCDPA;
            aParam[15] = new SqlParameter("@nCDPB", SqlDbType.Int, 4);
            aParam[15].Value = nCDPB;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_DEDICACIONESPROFPROY_PROF_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_DEDICACIONESPROFPROY_PROF", aParam);
        }
        public static SqlDataReader ObtenerProfYProy_Proyectos(int nUsuario, Nullable<int> idNodo, string sEstado, string sCategoria, Nullable<int> idCliente, Nullable<int> nResponsable, Nullable<int> nHorizontal, Nullable<int> nContrato, int nDesde, int nHasta, Nullable<int> nProfesional, string sCualidad, Nullable<int> idSubnodo, Nullable<int> nCDPA, Nullable<int> nCDPB)
        {
            SqlParameter[] aParam = new SqlParameter[15];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
            aParam[1].Value = idNodo;
            aParam[2] = new SqlParameter("@sEstado", SqlDbType.Char, 1);
            aParam[2].Value = sEstado;
            aParam[3] = new SqlParameter("@sCategoria", SqlDbType.Char, 1);
            aParam[3].Value = sCategoria;
            aParam[4] = new SqlParameter("@idCliente", SqlDbType.Int, 4);
            aParam[4].Value = idCliente;
            aParam[5] = new SqlParameter("@nResponsable", SqlDbType.Int, 4);
            aParam[5].Value = nResponsable;
            aParam[6] = new SqlParameter("@nHorizontal", SqlDbType.Int, 4);
            aParam[6].Value = nHorizontal;
            aParam[7] = new SqlParameter("@nContrato", SqlDbType.Int, 4);
            aParam[7].Value = nContrato;
            aParam[8] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[8].Value = nDesde;
            aParam[9] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[9].Value = nHasta;
            aParam[10] = new SqlParameter("@nProfesional", SqlDbType.Int, 4);
            aParam[10].Value = nProfesional;
            aParam[11] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
            aParam[11].Value = sCualidad;
            aParam[12] = new SqlParameter("@idSubnodo", SqlDbType.Int, 4);
            aParam[12].Value = idSubnodo;
            aParam[13] = new SqlParameter("@nCDPA", SqlDbType.Int, 4);
            aParam[13].Value = nCDPA;
            aParam[14] = new SqlParameter("@nCDPB", SqlDbType.Int, 4);
            aParam[14].Value = nCDPB;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_DEDICACIONESPROFPROY_PROY_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_DEDICACIONESPROFPROY_PROY", aParam);
        }
        public static SqlDataReader ObtenerProfYProy_Total(int nUsuario, Nullable<int> idNodo, string sEstado, string sCategoria, Nullable<int> idCliente, Nullable<int> nResponsable, Nullable<int> nHorizontal, Nullable<int> nContrato, int nDesde, int nHasta, Nullable<int> nProfesional, string sCualidad, Nullable<int> idSubnodo, string sTipo, Nullable<int> nCDPA, Nullable<int> nCDPB)
        {
            SqlParameter[] aParam = new SqlParameter[16];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
            aParam[1].Value = idNodo;
            aParam[2] = new SqlParameter("@sEstado", SqlDbType.Char, 1);
            aParam[2].Value = sEstado;
            aParam[3] = new SqlParameter("@sCategoria", SqlDbType.Char, 1);
            aParam[3].Value = sCategoria;
            aParam[4] = new SqlParameter("@idCliente", SqlDbType.Int, 4);
            aParam[4].Value = idCliente;
            aParam[5] = new SqlParameter("@nResponsable", SqlDbType.Int, 4);
            aParam[5].Value = nResponsable;
            aParam[6] = new SqlParameter("@nHorizontal", SqlDbType.Int, 4);
            aParam[6].Value = nHorizontal;
            aParam[7] = new SqlParameter("@nContrato", SqlDbType.Int, 4);
            aParam[7].Value = nContrato;
            aParam[8] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[8].Value = nDesde;
            aParam[9] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[9].Value = nHasta;
            aParam[10] = new SqlParameter("@nProfesional", SqlDbType.Int, 4);
            aParam[10].Value = nProfesional;
            aParam[11] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
            aParam[11].Value = sCualidad;
            aParam[12] = new SqlParameter("@idSubnodo", SqlDbType.Int, 4);
            aParam[12].Value = idSubnodo;
            aParam[13] = new SqlParameter("@sTipo", SqlDbType.Char, 1);
            aParam[13].Value = sTipo;
            aParam[14] = new SqlParameter("@nCDPA", SqlDbType.Int, 4);
            aParam[14].Value = nCDPA;
            aParam[15] = new SqlParameter("@nCDPB", SqlDbType.Int, 4);
            aParam[15].Value = nCDPB;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_DEDICACIONESPROFPROY_TOTAL_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_DEDICACIONESPROFPROY_TOTAL", aParam);
        }

        public static SqlDataReader ObtenerProyYProf_Proyectos(int nUsuario, Nullable<int> idNodo, string sEstado, string sCategoria, Nullable<int> idCliente, Nullable<int> nResponsable, Nullable<int> nHorizontal, Nullable<int> nContrato, int nDesde, int nHasta, Nullable<int> nIdProyecto, string sCualidad, Nullable<int> idSubnodo, string sTipo, Nullable<int> nCDPA, Nullable<int> nCDPB)
        {
            SqlParameter[] aParam = new SqlParameter[16];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
            aParam[1].Value = idNodo;
            aParam[2] = new SqlParameter("@sEstado", SqlDbType.Char, 1);
            aParam[2].Value = sEstado;
            aParam[3] = new SqlParameter("@sCategoria", SqlDbType.Char, 1);
            aParam[3].Value = sCategoria;
            aParam[4] = new SqlParameter("@idCliente", SqlDbType.Int, 4);
            aParam[4].Value = idCliente;
            aParam[5] = new SqlParameter("@nResponsable", SqlDbType.Int, 4);
            aParam[5].Value = nResponsable;
            aParam[6] = new SqlParameter("@nHorizontal", SqlDbType.Int, 4);
            aParam[6].Value = nHorizontal;
            aParam[7] = new SqlParameter("@nContrato", SqlDbType.Int, 4);
            aParam[7].Value = nContrato;
            aParam[8] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[8].Value = nDesde;
            aParam[9] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[9].Value = nHasta;
            aParam[10] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[10].Value = nIdProyecto;
            aParam[11] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
            aParam[11].Value = sCualidad;
            aParam[12] = new SqlParameter("@idSubnodo", SqlDbType.Int, 4);
            aParam[12].Value = idSubnodo;
            aParam[13] = new SqlParameter("@sTipo", SqlDbType.Char, 1);
            aParam[13].Value = sTipo;
            aParam[14] = new SqlParameter("@nCDPA", SqlDbType.Int, 4);
            aParam[14].Value = nCDPA;
            aParam[15] = new SqlParameter("@nCDPB", SqlDbType.Int, 4);
            aParam[15].Value = nCDPB;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_DEDICACIONESPROYPROF_PROY_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_DEDICACIONESPROYPROF_PROY", aParam);
        }
        public static SqlDataReader ObtenerProyYProf_Profesionales(int nPSN, int nDesde, int nHasta, string sTipo)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@sTipo", SqlDbType.Char, 1);
            aParam[3].Value = sTipo;

            //if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            //    return SqlHelper.ExecuteSqlDataReader("SUP_DEDICACIONESPROFPROY_PROF_ADMIN", aParam);
            //else
            return SqlHelper.ExecuteSqlDataReader("SUP_DEDICACIONESPROYPROF_PROF", aParam);
        }
        public static SqlDataReader ObtenerProyYProf_Total(int nUsuario, Nullable<int> idNodo, string sEstado, string sCategoria, Nullable<int> idCliente, Nullable<int> nResponsable, Nullable<int> nHorizontal, Nullable<int> nContrato, int nDesde, int nHasta, Nullable<int> nIdProyecto, string sCualidad, Nullable<int> idSubnodo, string sTipo, Nullable<int> nCDPA, Nullable<int> nCDPB)
        {
            SqlParameter[] aParam = new SqlParameter[16];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
            aParam[1].Value = idNodo;
            aParam[2] = new SqlParameter("@sEstado", SqlDbType.Char, 1);
            aParam[2].Value = sEstado;
            aParam[3] = new SqlParameter("@sCategoria", SqlDbType.Char, 1);
            aParam[3].Value = sCategoria;
            aParam[4] = new SqlParameter("@idCliente", SqlDbType.Int, 4);
            aParam[4].Value = idCliente;
            aParam[5] = new SqlParameter("@nResponsable", SqlDbType.Int, 4);
            aParam[5].Value = nResponsable;
            aParam[6] = new SqlParameter("@nHorizontal", SqlDbType.Int, 4);
            aParam[6].Value = nHorizontal;
            aParam[7] = new SqlParameter("@nContrato", SqlDbType.Int, 4);
            aParam[7].Value = nContrato;
            aParam[8] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[8].Value = nDesde;
            aParam[9] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[9].Value = nHasta;
            aParam[10] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[10].Value = nIdProyecto;
            aParam[11] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
            aParam[11].Value = sCualidad;
            aParam[12] = new SqlParameter("@idSubnodo", SqlDbType.Int, 4);
            aParam[12].Value = idSubnodo;
            aParam[13] = new SqlParameter("@sTipo", SqlDbType.Char, 1);
            aParam[13].Value = sTipo;
            aParam[14] = new SqlParameter("@nCDPA", SqlDbType.Int, 4);
            aParam[14].Value = nCDPA;
            aParam[15] = new SqlParameter("@nCDPB", SqlDbType.Int, 4);
            aParam[15].Value = nCDPB;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_DEDICACIONESPROYPROF_TOTAL_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_DEDICACIONESPROYPROF_TOTAL", aParam);
        }

        #endregion
    }
}
