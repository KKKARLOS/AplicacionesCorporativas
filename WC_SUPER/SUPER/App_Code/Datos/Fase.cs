using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
//Para usar List<>
using System.Collections.Generic;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de Fase
    /// </summary>
    public class Fase
    {
        public Fase()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        /// <summary>
        /// Obtiene las tareas vivas de una Fase
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t334_idfase"></param>
        /// <param name="t314_idusuario">Restringe las tareas a las asignadas al profesional</param>
        /// <param name="bAsignadas">Indica que hay que restringir las tareas al profesional</param>
        /// <param name="bSoloActivas">Restringe las tareas a las que su asociación al profesional este activa</param>
        /// <returns></returns>
        public static List<SUPER.Capa_Negocio.TAREAPSP> GetTareasVivas(SqlTransaction tr, int t334_idfase, int t314_idusuario,
                                                   bool bAsignadas, bool bSoloActivas)
        {
            SqlDataReader dr;
            if (bAsignadas)
            {
                SqlParameter[] aParam = new SqlParameter[]{  
                    ParametroSql.add("@t334_idfase", SqlDbType.Int, 4, t334_idfase),
                    ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                    ParametroSql.add("@bSoloActivas", SqlDbType.Bit, 1, bSoloActivas)
                };
                if (tr == null)
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREAS_PROFESIONAL_ByFase", aParam);
                else
                    dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREAS_PROFESIONAL_ByFase", aParam);
            }
            else
            {
                SqlParameter[] aParam = new SqlParameter[]{  
                    ParametroSql.add("@t334_idfase", SqlDbType.Int, 4, t334_idfase)
                };
                if (tr == null)
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREA_S3", aParam);
                else
                    dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREA_S3", aParam);
            }
            List<SUPER.Capa_Negocio.TAREAPSP> aTareas = new List<SUPER.Capa_Negocio.TAREAPSP>();
            SUPER.Capa_Negocio.TAREAPSP oTarea;

            while (dr.Read())
            {
                oTarea = new SUPER.Capa_Negocio.TAREAPSP();
                oTarea.t332_idtarea = int.Parse(dr["t332_idtarea"].ToString());
                oTarea.t332_notif_prof = (bool)dr["t332_notif_prof"];
                oTarea.t332_destarea = dr["t332_destarea"].ToString();
                oTarea.num_proyecto = int.Parse(dr["num_proyecto"].ToString());
                oTarea.nom_proyecto = dr["nom_proyecto"].ToString();
                oTarea.t331_despt = dr["t331_despt"].ToString();
                oTarea.t334_desfase = dr["t334_desfase"].ToString();
                oTarea.t335_desactividad = dr["t335_desactividad"].ToString();
                oTarea.t346_codpst = dr["t346_codpst"].ToString();
                oTarea.t346_despst = dr["t346_despst"].ToString();
                oTarea.t332_otl = dr["t332_otl"].ToString();
                oTarea.t332_incidencia = dr["t332_incidencia"].ToString();
                oTarea.t332_mensaje = dr["t332_mensaje"].ToString();

                aTareas.Add(oTarea);
            }
            dr.Close();
            dr.Dispose();

            return aTareas;
        }
    }
}