using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TareaIAP
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class TareaIAP 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t332_idtarea = 1,
			t332_destarea = 2,
			t331_idpt = 3,
			t335_idactividad = 4,
			t301_idproyecto = 5,
			t305_seudonimo = 6,
			t301_denominacion = 7,
			t331_despt = 8,
			t332_destarealong = 9,
			t332_notas1 = 10,
			t332_notas2 = 11,
			t332_notas3 = 12,
			t332_notas4 = 13,
			t332_mensaje = 14,
			t336_etp = 15,
			t336_ffp = 16,
			t336_ete = 17,
			t336_ffe = 18,
			t336_completado = 19,
			t334_desfase = 20,
			t335_desactividad = 21,
			t336_indicaciones = 22,
			t336_comentario = 23,
			dPrimerConsumo = 24,
			dUltimoConsumo = 25,
			esfuerzo = 26,
			esfuerzoenjor = 27,
			nPendienteEstimado = 28,
			nAvanceTeorico = 29,
			t332_impiap = 30,
			t332_notasiap = 31,
			t324_idmodofact = 32,
			t324_denominacion = 33,
			t336_estado = 34,
            nUsuario = 35,
            nTarea=36,
            t314_idusuario =37,
            nIdTarea=38
        }

        internal TareaIAP(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion
	
        //#region funciones publicas

        /// <summary>
        /// Obtiene un TareaIAP a partir del id
        /// </summary>
        internal Models.TareaIAP Select(Int32 idTarea, Int32 nUsuario)
        {
            Models.TareaIAP oTareaIAP = null;
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.nIdTarea, idTarea),
                    Param(enumDBFields.nUsuario, nUsuario)
                };

                dr = cDblib.DataReader("SUP_TAREAIAP_S", dbparams);
                if (dr.Read())
                {
                    oTareaIAP = new Models.TareaIAP();
                    oTareaIAP.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    oTareaIAP.t332_destarea = Convert.ToString(dr["t332_destarea"]);
                    oTareaIAP.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    if (!Convert.IsDBNull(dr["t335_idactividad"]))
                        oTareaIAP.t335_idactividad = Convert.ToInt32(dr["t335_idactividad"]);
                    oTareaIAP.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oTareaIAP.t305_seudonimo = Convert.ToString(dr["t305_seudonimo"]);
                    oTareaIAP.t301_denominacion = Convert.ToString(dr["t301_denominacion"]);
                    oTareaIAP.t331_despt = Convert.ToString(dr["t331_despt"]);
                    oTareaIAP.t332_destarealong = Convert.ToString(dr["t332_destarealong"]);
                    oTareaIAP.t332_notas1 = Convert.ToString(dr["t332_notas1"]);
                    oTareaIAP.t332_notas2 = Convert.ToString(dr["t332_notas2"]);
                    oTareaIAP.t332_notas3 = Convert.ToString(dr["t332_notas3"]);
                    oTareaIAP.t332_notas4 = Convert.ToString(dr["t332_notas4"]);
                    oTareaIAP.t332_mensaje = Convert.ToString(dr["t332_mensaje"]);
                    oTareaIAP.t334_desfase = Convert.ToString(dr["t334_desfase"]);
                    oTareaIAP.t335_desactividad = Convert.ToString(dr["t335_desactividad"]);

                    oTareaIAP.t336_etp = Convert.ToDouble(dr["t336_etp"]);
                    if (!Convert.IsDBNull(dr["t336_ffp"]))
                        oTareaIAP.t336_ffp = Convert.ToDateTime(dr["t336_ffp"]);
                    oTareaIAP.t336_ete = Convert.ToDouble(dr["t336_ete"]);
                    if (!Convert.IsDBNull(dr["t336_ffe"]))
                        oTareaIAP.t336_ffe = Convert.ToDateTime(dr["t336_ffe"]);
                    if (!Convert.IsDBNull(dr["t336_completado"]))
                        oTareaIAP.t336_completado = Convert.ToByte(dr["t336_completado"]);
                    oTareaIAP.t336_indicaciones = Convert.ToString(dr["t336_indicaciones"]);
                    oTareaIAP.t336_comentario = Convert.ToString(dr["t336_comentario"]);

                    if (!Convert.IsDBNull(dr["dPrimerConsumo"]))
                        oTareaIAP.dPrimerConsumo = Convert.ToDateTime(dr["dPrimerConsumo"]);
                    if (!Convert.IsDBNull(dr["dUltimoConsumo"]))
                        oTareaIAP.dUltimoConsumo = Convert.ToDateTime(dr["dUltimoConsumo"]);
                    if (!Convert.IsDBNull(dr["esfuerzo"]))
                        oTareaIAP.esfuerzo = Convert.ToDouble(dr["esfuerzo"]);
                    if (!Convert.IsDBNull(dr["esfuerzoenjor"]))
                        oTareaIAP.esfuerzoenjor = Convert.ToDouble(dr["esfuerzoenjor"]);
                    oTareaIAP.nPendienteEstimado = Convert.ToDouble(dr["nPendienteEstimado"]);
                    if (!Convert.IsDBNull(dr["nAvanceTeorico"]))
                        oTareaIAP.nAvanceTeorico = Convert.ToDouble(dr["nAvanceTeorico"]);
                    oTareaIAP.t332_impiap = Convert.ToBoolean(dr["t332_impiap"]);
                    oTareaIAP.t332_notasiap = Convert.ToBoolean(dr["t332_notasiap"]);
                    if (!Convert.IsDBNull(dr["t324_idmodofact"]))
                        oTareaIAP.t324_idmodofact = Convert.ToInt32(dr["t324_idmodofact"]);
                    oTareaIAP.t324_denominacion = Convert.ToString(dr["t324_denominacion"]);
                    oTareaIAP.t336_estado = Convert.ToBoolean(dr["t336_estado"]);

                }
                return oTareaIAP;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }

        internal Models.TareaIAP SelectBitacora(Int32 idTarea)
        {
            Models.TareaIAP oTareaIAP = null;
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t332_idtarea, idTarea)
                };

                dr = cDblib.DataReader("SUP_TAREAPSPS", dbparams);
                if (dr.Read())
                {
                    oTareaIAP = new Models.TareaIAP();
                    oTareaIAP.t332_idtarea = idTarea;
                    oTareaIAP.t332_destarea = Convert.ToString(dr["t332_destarea"]);
                    oTareaIAP.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    if (!Convert.IsDBNull(dr["t334_idfase"]))
                        oTareaIAP.t334_idfase = Convert.ToInt32(dr["t334_idfase"]);
                    if (!Convert.IsDBNull(dr["t335_idactividad"]))
                        oTareaIAP.t335_idactividad = Convert.ToInt32(dr["t335_idactividad"]);
                    oTareaIAP.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oTareaIAP.t301_denominacion = Convert.ToString(dr["t301_denominacion"]);
                    oTareaIAP.t331_despt = Convert.ToString(dr["t331_despt"]);
                    oTareaIAP.t334_desfase = Convert.ToString(dr["t334_desfase"]);
                    oTareaIAP.t335_desactividad = Convert.ToString(dr["t335_desactividad"]);
                    oTareaIAP.t305_seudonimo = Convert.ToString(dr["t305_seudonimo"]);

                }
                return oTareaIAP;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }
        /// <summary>
        /// Comprueba si una tarea es modificable en función del perfil del recurso que la está accediendo
        /// Devuelve el modo de acceso a una Tarea: N-> sin acceso, R->lectura, W->escritura
        /// </summary>
        /// <param name="idTarea"></param>
        /// <param name="nUsuario"></param>
        /// <returns></returns>
        internal bool TareaAsignada(Int32 idTarea, Int32 nUsuario)
        {
            bool bRes = false;
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t332_idtarea, idTarea),
                    Param(enumDBFields.t314_idusuario, nUsuario)
                };

                dr = cDblib.DataReader("SUP_TAREARECURSO_S", dbparams);
                if (dr.Read())
                {
                    bRes=true;
                }
                return bRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
            
        }
        internal string getAcceso(Int32 idTarea, Int32 nUsuario)
        {
            string sRes = "N";
            IDataReader dr = null;
            try
            {
                if (IB.SUPER.Shared.Utils.EsAdminProduccion())
                    sRes = "W";
                else
                {
                    if (TareaAsignada(idTarea, nUsuario))
                        sRes = "R";
                    else
                    {
                        SqlParameter[] dbparams = new SqlParameter[2] {
                        Param(enumDBFields.nIdTarea, idTarea),
                        Param(enumDBFields.t314_idusuario, nUsuario)
                    };

                        dr = cDblib.DataReader("SUP_PERMISO_TAREA", dbparams);
                        if (dr.Read())
                        {
                            if ((bool)dr[0]) sRes = "R";
                            else sRes = "W";
                        }
                    }
                }
                return sRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }

        }

        ///// <summary>
        ///// Inserta un TareaIAP
        ///// </summary>
        //internal int Insert(Models.TareaIAP oTareaIAP)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[34] {
        //            Param(enumDBFields.t332_idtarea, oTareaIAP.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oTareaIAP.t332_destarea),
        //            Param(enumDBFields.t331_idpt, oTareaIAP.t331_idpt),
        //            Param(enumDBFields.t335_idactividad, oTareaIAP.t335_idactividad),
        //            Param(enumDBFields.t301_idproyecto, oTareaIAP.t301_idproyecto),
        //            Param(enumDBFields.t305_seudonimo, oTareaIAP.t305_seudonimo),
        //            Param(enumDBFields.t301_denominacion, oTareaIAP.t301_denominacion),
        //            Param(enumDBFields.t331_despt, oTareaIAP.t331_despt),
        //            Param(enumDBFields.t332_destarealong, oTareaIAP.t332_destarealong),
        //            Param(enumDBFields.t332_notas1, oTareaIAP.t332_notas1),
        //            Param(enumDBFields.t332_notas2, oTareaIAP.t332_notas2),
        //            Param(enumDBFields.t332_notas3, oTareaIAP.t332_notas3),
        //            Param(enumDBFields.t332_notas4, oTareaIAP.t332_notas4),
        //            Param(enumDBFields.t332_mensaje, oTareaIAP.t332_mensaje),
        //            Param(enumDBFields.t336_etp, oTareaIAP.t336_etp),
        //            Param(enumDBFields.t336_ffp, oTareaIAP.t336_ffp),
        //            Param(enumDBFields.t336_ete, oTareaIAP.t336_ete),
        //            Param(enumDBFields.t336_ffe, oTareaIAP.t336_ffe),
        //            Param(enumDBFields.t336_completado, oTareaIAP.t336_completado),
        //            Param(enumDBFields.t334_desfase, oTareaIAP.t334_desfase),
        //            Param(enumDBFields.t335_desactividad, oTareaIAP.t335_desactividad),
        //            Param(enumDBFields.t336_indicaciones, oTareaIAP.t336_indicaciones),
        //            Param(enumDBFields.t336_comentario, oTareaIAP.t336_comentario),
        //            Param(enumDBFields.dPrimerConsumo, oTareaIAP.dPrimerConsumo),
        //            Param(enumDBFields.dUltimoConsumo, oTareaIAP.dUltimoConsumo),
        //            Param(enumDBFields.esfuerzo, oTareaIAP.esfuerzo),
        //            Param(enumDBFields.esfuerzoenjor, oTareaIAP.esfuerzoenjor),
        //            Param(enumDBFields.nPendienteEstimado, oTareaIAP.nPendienteEstimado),
        //            Param(enumDBFields.nAvanceTeorico, oTareaIAP.nAvanceTeorico),
        //            Param(enumDBFields.t332_impiap, oTareaIAP.t332_impiap),
        //            Param(enumDBFields.t332_notasiap, oTareaIAP.t332_notasiap),
        //            Param(enumDBFields.t324_idmodofact, oTareaIAP.t324_idmodofact),
        //            Param(enumDBFields.t324_denominacion, oTareaIAP.t324_denominacion),
        //            Param(enumDBFields.t336_estado, oTareaIAP.t336_estado)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_TareaIAP_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        ///// <summary>
        ///// Actualiza un TareaIAP a partir del id
        ///// </summary>
        //internal int Update(Models.TareaIAP oTareaIAP)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[34] {
        //            Param(enumDBFields.t332_idtarea, oTareaIAP.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oTareaIAP.t332_destarea),
        //            Param(enumDBFields.t331_idpt, oTareaIAP.t331_idpt),
        //            Param(enumDBFields.t335_idactividad, oTareaIAP.t335_idactividad),
        //            Param(enumDBFields.t301_idproyecto, oTareaIAP.t301_idproyecto),
        //            Param(enumDBFields.t305_seudonimo, oTareaIAP.t305_seudonimo),
        //            Param(enumDBFields.t301_denominacion, oTareaIAP.t301_denominacion),
        //            Param(enumDBFields.t331_despt, oTareaIAP.t331_despt),
        //            Param(enumDBFields.t332_destarealong, oTareaIAP.t332_destarealong),
        //            Param(enumDBFields.t332_notas1, oTareaIAP.t332_notas1),
        //            Param(enumDBFields.t332_notas2, oTareaIAP.t332_notas2),
        //            Param(enumDBFields.t332_notas3, oTareaIAP.t332_notas3),
        //            Param(enumDBFields.t332_notas4, oTareaIAP.t332_notas4),
        //            Param(enumDBFields.t332_mensaje, oTareaIAP.t332_mensaje),
        //            Param(enumDBFields.t336_etp, oTareaIAP.t336_etp),
        //            Param(enumDBFields.t336_ffp, oTareaIAP.t336_ffp),
        //            Param(enumDBFields.t336_ete, oTareaIAP.t336_ete),
        //            Param(enumDBFields.t336_ffe, oTareaIAP.t336_ffe),
        //            Param(enumDBFields.t336_completado, oTareaIAP.t336_completado),
        //            Param(enumDBFields.t334_desfase, oTareaIAP.t334_desfase),
        //            Param(enumDBFields.t335_desactividad, oTareaIAP.t335_desactividad),
        //            Param(enumDBFields.t336_indicaciones, oTareaIAP.t336_indicaciones),
        //            Param(enumDBFields.t336_comentario, oTareaIAP.t336_comentario),
        //            Param(enumDBFields.dPrimerConsumo, oTareaIAP.dPrimerConsumo),
        //            Param(enumDBFields.dUltimoConsumo, oTareaIAP.dUltimoConsumo),
        //            Param(enumDBFields.esfuerzo, oTareaIAP.esfuerzo),
        //            Param(enumDBFields.esfuerzoenjor, oTareaIAP.esfuerzoenjor),
        //            Param(enumDBFields.nPendienteEstimado, oTareaIAP.nPendienteEstimado),
        //            Param(enumDBFields.nAvanceTeorico, oTareaIAP.nAvanceTeorico),
        //            Param(enumDBFields.t332_impiap, oTareaIAP.t332_impiap),
        //            Param(enumDBFields.t332_notasiap, oTareaIAP.t332_notasiap),
        //            Param(enumDBFields.t324_idmodofact, oTareaIAP.t324_idmodofact),
        //            Param(enumDBFields.t324_denominacion, oTareaIAP.t324_denominacion),
        //            Param(enumDBFields.t336_estado, oTareaIAP.t336_estado)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_TareaIAP_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Elimina un TareaIAP a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {


        //        return (int)cDblib.Execute("SUPER.IAP30_TareaIAP_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los TareaIAP
        ///// </summary>
        //internal List<Models.TareaIAP> Catalogo(Models.TareaIAP oTareaIAPFilter)
        //{
        //    Models.TareaIAP oTareaIAP = null;
        //    List<Models.TareaIAP> lst = new List<Models.TareaIAP>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[34] {
        //            Param(enumDBFields.t332_idtarea, oTEMP_TareaIAPFilter.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oTEMP_TareaIAPFilter.t332_destarea),
        //            Param(enumDBFields.t331_idpt, oTEMP_TareaIAPFilter.t331_idpt),
        //            Param(enumDBFields.t335_idactividad, oTEMP_TareaIAPFilter.t335_idactividad),
        //            Param(enumDBFields.t301_idproyecto, oTEMP_TareaIAPFilter.t301_idproyecto),
        //            Param(enumDBFields.t305_seudonimo, oTEMP_TareaIAPFilter.t305_seudonimo),
        //            Param(enumDBFields.t301_denominacion, oTEMP_TareaIAPFilter.t301_denominacion),
        //            Param(enumDBFields.t331_despt, oTEMP_TareaIAPFilter.t331_despt),
        //            Param(enumDBFields.t332_destarealong, oTEMP_TareaIAPFilter.t332_destarealong),
        //            Param(enumDBFields.t332_notas1, oTEMP_TareaIAPFilter.t332_notas1),
        //            Param(enumDBFields.t332_notas2, oTEMP_TareaIAPFilter.t332_notas2),
        //            Param(enumDBFields.t332_notas3, oTEMP_TareaIAPFilter.t332_notas3),
        //            Param(enumDBFields.t332_notas4, oTEMP_TareaIAPFilter.t332_notas4),
        //            Param(enumDBFields.t332_mensaje, oTEMP_TareaIAPFilter.t332_mensaje),
        //            Param(enumDBFields.t336_etp, oTEMP_TareaIAPFilter.t336_etp),
        //            Param(enumDBFields.t336_ffp, oTEMP_TareaIAPFilter.t336_ffp),
        //            Param(enumDBFields.t336_ete, oTEMP_TareaIAPFilter.t336_ete),
        //            Param(enumDBFields.t336_ffe, oTEMP_TareaIAPFilter.t336_ffe),
        //            Param(enumDBFields.t336_completado, oTEMP_TareaIAPFilter.t336_completado),
        //            Param(enumDBFields.t334_desfase, oTEMP_TareaIAPFilter.t334_desfase),
        //            Param(enumDBFields.t335_desactividad, oTEMP_TareaIAPFilter.t335_desactividad),
        //            Param(enumDBFields.t336_indicaciones, oTEMP_TareaIAPFilter.t336_indicaciones),
        //            Param(enumDBFields.t336_comentario, oTEMP_TareaIAPFilter.t336_comentario),
        //            Param(enumDBFields.dPrimerConsumo, oTEMP_TareaIAPFilter.dPrimerConsumo),
        //            Param(enumDBFields.dUltimoConsumo, oTEMP_TareaIAPFilter.dUltimoConsumo),
        //            Param(enumDBFields.esfuerzo, oTEMP_TareaIAPFilter.esfuerzo),
        //            Param(enumDBFields.esfuerzoenjor, oTEMP_TareaIAPFilter.esfuerzoenjor),
        //            Param(enumDBFields.nPendienteEstimado, oTEMP_TareaIAPFilter.nPendienteEstimado),
        //            Param(enumDBFields.nAvanceTeorico, oTEMP_TareaIAPFilter.nAvanceTeorico),
        //            Param(enumDBFields.t332_impiap, oTEMP_TareaIAPFilter.t332_impiap),
        //            Param(enumDBFields.t332_notasiap, oTEMP_TareaIAPFilter.t332_notasiap),
        //            Param(enumDBFields.t324_idmodofact, oTEMP_TareaIAPFilter.t324_idmodofact),
        //            Param(enumDBFields.t324_denominacion, oTEMP_TareaIAPFilter.t324_denominacion),
        //            Param(enumDBFields.t336_estado, oTEMP_TareaIAPFilter.t336_estado)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_TareaIAP_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oTareaIAP = new Models.TareaIAP();
        //            oTareaIAP.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oTareaIAP.t332_destarea=Convert.ToString(dr["t332_destarea"]);
        //            oTareaIAP.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
        //            if(!Convert.IsDBNull(dr["t335_idactividad"]))
        //                oTareaIAP.t335_idactividad=Convert.ToInt32(dr["t335_idactividad"]);
        //            oTareaIAP.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);
        //            oTareaIAP.t305_seudonimo=Convert.ToString(dr["t305_seudonimo"]);
        //            oTareaIAP.t301_denominacion=Convert.ToString(dr["t301_denominacion"]);
        //            oTareaIAP.t331_despt=Convert.ToString(dr["t331_despt"]);
        //            oTareaIAP.t332_destarealong=Convert.ToString(dr["t332_destarealong"]);
        //            oTareaIAP.t332_notas1=Convert.ToString(dr["t332_notas1"]);
        //            oTareaIAP.t332_notas2=Convert.ToString(dr["t332_notas2"]);
        //            oTareaIAP.t332_notas3=Convert.ToString(dr["t332_notas3"]);
        //            oTareaIAP.t332_notas4=Convert.ToString(dr["t332_notas4"]);
        //            oTareaIAP.t332_mensaje=Convert.ToString(dr["t332_mensaje"]);
        //            oTareaIAP.t336_etp=Convert.ToDouble(dr["t336_etp"]);
        //            if(!Convert.IsDBNull(dr["t336_ffp"]))
        //                oTareaIAP.t336_ffp=Convert.ToDateTime(dr["t336_ffp"]);
        //            oTareaIAP.t336_ete=Convert.ToDouble(dr["t336_ete"]);
        //            if(!Convert.IsDBNull(dr["t336_ffe"]))
        //                oTareaIAP.t336_ffe=Convert.ToDateTime(dr["t336_ffe"]);
        //            if(!Convert.IsDBNull(dr["t336_completado"]))
        //                oTareaIAP.t336_completado=Convert.ToByte(dr["t336_completado"]);
        //            oTareaIAP.t334_desfase=Convert.ToString(dr["t334_desfase"]);
        //            oTareaIAP.t335_desactividad=Convert.ToString(dr["t335_desactividad"]);
        //            oTareaIAP.t336_indicaciones=Convert.ToString(dr["t336_indicaciones"]);
        //            oTareaIAP.t336_comentario=Convert.ToString(dr["t336_comentario"]);
        //            if(!Convert.IsDBNull(dr["dPrimerConsumo"]))
        //                oTareaIAP.dPrimerConsumo=Convert.ToDateTime(dr["dPrimerConsumo"]);
        //            if(!Convert.IsDBNull(dr["dUltimoConsumo"]))
        //                oTareaIAP.dUltimoConsumo=Convert.ToDateTime(dr["dUltimoConsumo"]);
        //            if(!Convert.IsDBNull(dr["esfuerzo"]))
        //                oTareaIAP.esfuerzo=Convert.ToDouble(dr["esfuerzo"]);
        //            if(!Convert.IsDBNull(dr["esfuerzoenjor"]))
        //                oTareaIAP.esfuerzoenjor=Convert.ToDouble(dr["esfuerzoenjor"]);
        //            oTareaIAP.nPendienteEstimado=Convert.ToDouble(dr["nPendienteEstimado"]);
        //            if(!Convert.IsDBNull(dr["nAvanceTeorico"]))
        //                oTareaIAP.nAvanceTeorico=Convert.ToDouble(dr["nAvanceTeorico"]);
        //            oTareaIAP.t332_impiap=Convert.ToBoolean(dr["t332_impiap"]);
        //            oTareaIAP.t332_notasiap=Convert.ToBoolean(dr["t332_notasiap"]);
        //            if(!Convert.IsDBNull(dr["t324_idmodofact"]))
        //                oTareaIAP.t324_idmodofact=Convert.ToInt32(dr["t324_idmodofact"]);
        //            oTareaIAP.t324_denominacion=Convert.ToString(dr["t324_denominacion"]);
        //            oTareaIAP.t336_estado=Convert.ToBoolean(dr["t336_estado"]);

        //            lst.Add(oTareaIAP);

        //        }
        //        return lst;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dr != null)
        //        {
        //            if (!dr.IsClosed) dr.Close();
        //            dr.Dispose();
        //        }
        //    }
        //}

        //#endregion

        #region funciones privadas
        private SqlParameter Param(enumDBFields dbField, object value)
        {
            SqlParameter dbParam = null;
            string paramName = null;
            SqlDbType paramType = default(SqlDbType);
            int paramSize = 0;
			ParameterDirection paramDirection = ParameterDirection.Input;
			
			switch (dbField)
			{
                case enumDBFields.nIdTarea:
                    paramName = "@nIdTarea";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t332_idtarea:
                    paramName = "@t332_idtarea";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.nTarea:
                    paramName = "@nTarea";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t332_destarea:
					paramName = "@t332_destarea";
					paramType = SqlDbType.VarChar;
					paramSize = 100;
					break;
				case enumDBFields.t331_idpt:
					paramName = "@t331_idpt";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t335_idactividad:
					paramName = "@t335_idactividad";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t301_idproyecto:
					paramName = "@t301_idproyecto";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t305_seudonimo:
					paramName = "@t305_seudonimo";
					paramType = SqlDbType.VarChar;
					paramSize = 70;
					break;
				case enumDBFields.t301_denominacion:
					paramName = "@t301_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 70;
					break;
				case enumDBFields.t331_despt:
					paramName = "@t331_despt";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t332_destarealong:
					paramName = "@t332_destarealong";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t332_notas1:
					paramName = "@t332_notas1";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t332_notas2:
					paramName = "@t332_notas2";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t332_notas3:
					paramName = "@t332_notas3";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t332_notas4:
					paramName = "@t332_notas4";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t332_mensaje:
					paramName = "@t332_mensaje";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t336_etp:
					paramName = "@t336_etp";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.t336_ffp:
					paramName = "@t336_ffp";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t336_ete:
					paramName = "@t336_ete";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.t336_ffe:
					paramName = "@t336_ffe";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t336_completado:
					paramName = "@t336_completado";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.t334_desfase:
					paramName = "@t334_desfase";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t335_desactividad:
					paramName = "@t335_desactividad";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t336_indicaciones:
					paramName = "@t336_indicaciones";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t336_comentario:
					paramName = "@t336_comentario";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.dPrimerConsumo:
					paramName = "@dPrimerConsumo";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.dUltimoConsumo:
					paramName = "@dUltimoConsumo";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.esfuerzo:
					paramName = "@esfuerzo";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.esfuerzoenjor:
					paramName = "@esfuerzoenjor";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.nPendienteEstimado:
					paramName = "@nPendienteEstimado";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.nAvanceTeorico:
					paramName = "@nAvanceTeorico";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.t332_impiap:
					paramName = "@t332_impiap";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t332_notasiap:
					paramName = "@t332_notasiap";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t324_idmodofact:
					paramName = "@t324_idmodofact";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t324_denominacion:
					paramName = "@t324_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 20;
					break;
				case enumDBFields.t336_estado:
					paramName = "@t336_estado";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
                case enumDBFields.nUsuario:
                    paramName = "@nUsuario";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t314_idusuario:
                    paramName = "@t314_idusuario";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
            }


            dbParam = cDblib.dbParameter(paramName, paramType, paramSize);
            dbParam.Direction = paramDirection;
            if (paramDirection == ParameterDirection.Input | paramDirection == ParameterDirection.InputOutput) dbParam.Value = value;

            return dbParam;

        }
		
		#endregion
    
    }

}
