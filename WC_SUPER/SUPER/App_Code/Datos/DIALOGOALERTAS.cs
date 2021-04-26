using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SUPER.Capa_Datos
{
    public class DIALOGOALERTAS
    {
        public static SqlDataReader ObtenerDatosDialogoAlerta(SqlTransaction tr, int t831_iddialogoalerta)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t831_iddialogoalerta", SqlDbType.Int, 4, @t831_iddialogoalerta);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_DIALOGOALERTAS_SEL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DIALOGOALERTAS_SEL", aParam);
        }
        public static DataSet ObtenerDialogoAlerta(SqlTransaction tr, int t831_iddialogoalerta, int t314_idusuario, bool bEsGestor)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t831_iddialogoalerta", SqlDbType.Int, 4, @t831_iddialogoalerta);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@bEsGestor", SqlDbType.Bit, 1, bEsGestor);

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_DIALOGOALERTAS_O", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_DIALOGOALERTAS_O", aParam);
        }

        public static bool EsGestorDeDialogoAlerta(SqlTransaction tr, int t831_iddialogoalerta, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t831_iddialogoalerta", SqlDbType.Int, 4, t831_iddialogoalerta);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_GESTALERTAS_ESGESTOR", aParam)) == 0) ? false : true;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_GESTALERTAS_ESGESTOR", aParam)) == 0) ? false : true;
        }
        /// <summary>
        /// Si la alerta es de tipo OCFA y el usuario es el interlocutor DEF de estas alertas
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t831_iddialogoalerta"></param>
        /// <param name="t314_idusuario"></param>
        /// <returns></returns>
        public static bool EsGestorDEF_AlertaOC_FA(SqlTransaction tr, int t831_iddialogoalerta, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t831_iddialogoalerta", SqlDbType.Int, 4, t831_iddialogoalerta);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_GESTALEROCFA_ESGESTORDEF", aParam)) == 0) ? false : true;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_GESTALEROCFA_ESGESTORDEF", aParam)) == 0) ? false : true;
        }


        public static SqlDataReader ObtenerOtrosDialogosAbiertos(SqlTransaction tr, int t831_iddialogoalerta, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t831_iddialogoalerta", SqlDbType.Int, 4, t831_iddialogoalerta);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            //aParam[i++] = ParametroSql.add("@bAdmin", SqlDbType.Bit, 1, (System.Web.HttpContext.Current.Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "") ? true : false);
            aParam[i++] = ParametroSql.add("@bAdmin", SqlDbType.Bit, 1, SUPER.Capa_Negocio.Utilidades.EsAdminProduccion());

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_DIALOGOALERTAS_OTROSByDialogo", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DIALOGOALERTAS_OTROSByDialogo", aParam);
        }
        public static SqlDataReader ObtenerDialogosByPSN(SqlTransaction tr, int t305_idproyectosubnodo, bool bSoloActivos, bool bRestringirOCyFACerrados)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@bSoloActivos", SqlDbType.Bit, 1, bSoloActivos);
            aParam[i++] = ParametroSql.add("@bRestringirOCyFACerrados", SqlDbType.Bit, 1, bRestringirOCyFACerrados);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_DIALOGOALERTAS_ByPSN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DIALOGOALERTAS_ByPSN", aParam);
        }

        public static void CerrarDialogo(SqlTransaction tr, int t001_idficepi_cierre, int t831_iddialogoalerta, byte t827_idestadodialogoalerta, bool? t831_justificada, 
                                        string t831_causamotivoOC, string t831_accionesacordadas, Nullable<int> t840_idmotivo)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi_cierre", SqlDbType.Int, 4, t001_idficepi_cierre);
            aParam[i++] = ParametroSql.add("@t831_iddialogoalerta", SqlDbType.Int, 4, t831_iddialogoalerta);
            aParam[i++] = ParametroSql.add("@t827_idestadodialogoalerta", SqlDbType.TinyInt, 1, t827_idestadodialogoalerta);
            aParam[i++] = ParametroSql.add("@t831_justificada", SqlDbType.Bit, 1, t831_justificada);
            aParam[i++] = ParametroSql.add("@t831_causamotivoOC", SqlDbType.Text, 16, t831_causamotivoOC);
            aParam[i++] = ParametroSql.add("@t831_accionesacordadas", SqlDbType.Text, 16, t831_accionesacordadas);
            aParam[i++] = ParametroSql.add("@t840_idmotivo", SqlDbType.Int, 4, t840_idmotivo);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_DIALOGOALERTAS_CERRAR", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DIALOGOALERTAS_CERRAR", aParam);

        }

        public static int NumDocs(SqlTransaction tr, int t831_iddialogoalerta)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t831_iddialogoalerta", SqlDbType.Int, 4, t831_iddialogoalerta);

            if (tr == null)
                return System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCDIALOGO_COUNT", aParam));
            else
                return System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCDIALOGO_COUNT", aParam));
        }

        public static DataSet ObtenerDialogosPendientes(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_DIALOGOALERTAS_PENDIENTES", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_DIALOGOALERTAS_PENDIENTES", aParam);
        }

        public static SqlDataReader ObtenerMisDialogosAbiertos(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_DIALOGOALERTAS_MISABIERTOS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DIALOGOALERTAS_MISABIERTOS", aParam);
        }
        public static SqlDataReader ObtenerDialogosMiGestion(SqlTransaction tr, 
           int t314_idusuario,
	       bool bSoloAbiertos,
           Nullable<byte> t821_idgrupoalerta,
           Nullable<byte> t820_idalerta,
           Nullable<int> t305_idproyectosubnodo,
	       Nullable<int> t001_idficepi_interlocutor,
	       Nullable<byte> t827_idestadodialogoalerta,
	       Nullable<int>  t303_idnodo,
	       Nullable<int> t302_idcliente,
	       Nullable<int> t831_iddialogoalerta,
	       Nullable<DateTime> t831_flr,
	       Nullable<int> t314_idusuario_gestor,
           Nullable<int> t314_idusuario_responsable,
           Nullable<int> nMesDesde,
           Nullable<int> nMesHasta,
           byte nOrden,
           byte nAscDesc
            )
        {
            SqlParameter[] aParam = new SqlParameter[17];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@bSoloAbiertos", SqlDbType.Bit, 1, bSoloAbiertos);
            aParam[i++] = ParametroSql.add("@t821_idgrupoalerta", SqlDbType.TinyInt, 1, t821_idgrupoalerta);
            aParam[i++] = ParametroSql.add("@t820_idalerta", SqlDbType.TinyInt, 1, t820_idalerta);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t001_idficepi_interlocutor", SqlDbType.Int, 4, t001_idficepi_interlocutor);
            aParam[i++] = ParametroSql.add("@t827_idestadodialogoalerta", SqlDbType.TinyInt, 1, t827_idestadodialogoalerta);
            aParam[i++] = ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo);
            aParam[i++] = ParametroSql.add("@t302_idcliente", SqlDbType.Int, 4, t302_idcliente);
            aParam[i++] = ParametroSql.add("@t831_iddialogoalerta", SqlDbType.Int, 4, t831_iddialogoalerta);
            aParam[i++] = ParametroSql.add("@t831_flr", SqlDbType.SmallDateTime, 4, t831_flr);
            aParam[i++] = ParametroSql.add("@t314_idusuario_gestor", SqlDbType.Int, 4, t314_idusuario_gestor);
            aParam[i++] = ParametroSql.add("@t314_idusuario_responsable", SqlDbType.Int, 4, t314_idusuario_responsable);
            aParam[i++] = ParametroSql.add("@nMesDesde", SqlDbType.Int, 4, nMesDesde);
            aParam[i++] = ParametroSql.add("@nMesHasta", SqlDbType.Int, 4, nMesHasta);
            aParam[i++] = ParametroSql.add("@nOrden", SqlDbType.TinyInt, 1, nOrden);
            aParam[i++] = ParametroSql.add("@nAscDesc", SqlDbType.TinyInt, 1, nAscDesc);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_DIALOGOALERTAS_CATGESTION", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DIALOGOALERTAS_CATGESTION", aParam);
        }

        public static SqlDataReader ObtenerDialogosOCyFA(SqlTransaction tr,
           int t314_idusuario,
           Nullable<byte> t820_idalerta,
           Nullable<byte> t827_idestadodialogoalerta,
           Nullable<int> t314_idusuario_gestor,
           Nullable<int> t305_idproyectosubnodo,
           Nullable<int> t303_idnodo,
           Nullable<int> t302_idcliente,
           Nullable<int> t001_idficepi_interlocutor,
           Nullable<bool> t831_justificada,
           Nullable<int> t314_idusuario_responsable,
           Nullable<int> nMesDesde,
           Nullable<int> nMesHasta
            )
        {
            SqlParameter[] aParam = new SqlParameter[12];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t820_idalerta", SqlDbType.TinyInt, 1, t820_idalerta);
            aParam[i++] = ParametroSql.add("@t827_idestadodialogoalerta", SqlDbType.TinyInt, 1, t827_idestadodialogoalerta);
            aParam[i++] = ParametroSql.add("@t314_idusuario_gestor", SqlDbType.Int, 4, t314_idusuario_gestor);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo);
            aParam[i++] = ParametroSql.add("@t302_idcliente", SqlDbType.Int, 4, t302_idcliente);
            aParam[i++] = ParametroSql.add("@t001_idficepi_interlocutor", SqlDbType.Int, 4, t001_idficepi_interlocutor);
            aParam[i++] = ParametroSql.add("@t831_justificada", SqlDbType.Bit, 1, t831_justificada);
            aParam[i++] = ParametroSql.add("@t314_idusuario_responsable", SqlDbType.Int, 4, t314_idusuario_responsable);
            aParam[i++] = ParametroSql.add("@nMesDesde", SqlDbType.Int, 4, nMesDesde);
            aParam[i++] = ParametroSql.add("@nMesHasta", SqlDbType.Int, 4, nMesHasta);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_DIALOGOALERTAS_OCyFA", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DIALOGOALERTAS_OCyFA", aParam);
        }

        public static void ActualizarFechaTipo(SqlTransaction tr, int t831_iddialogoalerta, byte t820_idalerta, Nullable<DateTime> t831_flr)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t831_iddialogoalerta", SqlDbType.Int, 4, t831_iddialogoalerta);
            aParam[i++] = ParametroSql.add("@t820_idalerta", SqlDbType.TinyInt, 1, t820_idalerta);
            aParam[i++] = ParametroSql.add("@t831_flr", SqlDbType.SmallDateTime, 4, t831_flr);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_DIALOGOALERTAS_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DIALOGOALERTAS_UPD", aParam);

        }

        public static SqlDataReader ObtenerCatalogoGrupos(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_GRUPOALERTAS_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GRUPOALERTAS_CAT", aParam);
        }
        public static SqlDataReader ObtenerCatalogoAlertas(SqlTransaction tr, Nullable<byte> t821_idgrupoalerta)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t821_idgrupoalerta", SqlDbType.TinyInt, 1, t821_idgrupoalerta);
            
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ALERTAS_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ALERTAS_CAT", aParam);
        }

        public static bool TienePermisoCreacion(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DIALOGOALERTAS_PERMISOCREAR", aParam)) == 0) ? false : true;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DIALOGOALERTAS_PERMISOCREAR", aParam)) == 0) ? false : true;
        }
        public static SqlDataReader ObtenerAsuntosCreacion(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_DIALOGOALERTAS_TIPOCREAR", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DIALOGOALERTAS_TIPOCREAR", aParam);
        }

        public static int CrearDialogo(SqlTransaction tr, int t305_idproyectosubnodo,
                                                    byte t820_idalerta,
                                                    string t831_entepromotor,
                                                    Nullable<int> t831_anomesdecierre,
                                                    int t314_idusuario_promotor,
                                                    Nullable<DateTime> t831_flr)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t820_idalerta", SqlDbType.TinyInt, 1, t820_idalerta);
            aParam[i++] = ParametroSql.add("@t831_entepromotor", SqlDbType.Char, 1, t831_entepromotor);
            aParam[i++] = ParametroSql.add("@t831_anomesdecierre", SqlDbType.Int, 4, t831_anomesdecierre);
            aParam[i++] = ParametroSql.add("@t314_idusuario_promotor", SqlDbType.Int, 4, t314_idusuario_promotor);
            aParam[i++] = ParametroSql.add("@t831_flr", SqlDbType.SmallDateTime, 4, t831_flr);

            if (tr == null)
                return System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DIALOGOALERTAS_INS", aParam));
            else
                return System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DIALOGOALERTAS_INS", aParam));
        }

        public static SqlDataReader CountDialogosAbiertos(SqlTransaction tr, int t305_idproyectosubnodo, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_DIALOGOALERTAS_ABIERTOS_ByPSN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DIALOGOALERTAS_ABIERTOS_ByPSN", aParam);
        }

        //Método para obtener los datos en el caso de diálogo cerrado (se justifica, causas y acciones acometidas)
        public static SqlDataReader ObtenerDatosDialogoCerrado(SqlTransaction tr, int t831_iddialogoalerta)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t831_iddialogoalerta", SqlDbType.Int, 4, @t831_iddialogoalerta);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_DIALOGOALERTAS_S", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DIALOGOALERTAS_S", aParam);
        }


    }
}