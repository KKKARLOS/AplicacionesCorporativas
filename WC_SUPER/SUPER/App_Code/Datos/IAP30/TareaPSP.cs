using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TareaPSP
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class TareaPSP 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t332_idtarea = 1,
			t332_destarea = 2,
			t332_destarealong = 3,
			t331_idpt = 4,
			t334_idfase = 5,
			t335_idactividad = 6,
			t314_idusuario_promotor = 7,
			t314_idusuario_ultmodif = 8,
			t332_falta = 9,
			t332_fultmodif = 10,
			t332_fiv = 11,
			t332_ffv = 12,
			t332_estado = 13,
			t332_fipl = 14,
			t332_ffpl = 15,
			t332_etpl = 16,
			t332_ffpr = 17,
			t332_etpr = 18,
			t346_idpst = 19,
			t332_cle = 20,
			t332_tipocle = 21,
			t332_orden = 22,
			t332_facturable = 23,
			t332_presupuesto = 24,
			t353_idorigen = 25,
			t332_otl = 26,
			t332_incidencia = 27,
			t332_observaciones = 28,
			t332_notificable = 29,
			t332_notas1 = 30,
			t332_notas2 = 31,
			t332_notas3 = 32,
			t332_notas4 = 33,
			t332_avance = 34,
			t332_avanceauto = 35,
			t314_idusuario_fin = 36,
			t332_ffin = 37,
			t314_idusuario_cierre = 38,
			t332_fcierre = 39,
			t332_impiap = 40,
			t332_notasiap = 41,
			t332_heredanodo = 42,
			t332_heredaproyeco = 43,
			t332_mensaje = 44,
			t332_acceso_iap = 45,
			t324_idmodofact = 46,
			t324_denominacion = 47
        }

        internal TareaPSP(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion
	
        //#region funciones publicas
        ///// <summary>
        ///// Inserta un TareaPSP
        ///// </summary>
        //internal int Insert(Models.TareaPSP oTareaPSP)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[47] {
        //            Param(enumDBFields.t332_idtarea, oTareaPSP.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oTareaPSP.t332_destarea),
        //            Param(enumDBFields.t332_destarealong, oTareaPSP.t332_destarealong),
        //            Param(enumDBFields.t331_idpt, oTareaPSP.t331_idpt),
        //            Param(enumDBFields.t334_idfase, oTareaPSP.t334_idfase),
        //            Param(enumDBFields.t335_idactividad, oTareaPSP.t335_idactividad),
        //            Param(enumDBFields.t314_idusuario_promotor, oTareaPSP.t314_idusuario_promotor),
        //            Param(enumDBFields.t314_idusuario_ultmodif, oTareaPSP.t314_idusuario_ultmodif),
        //            Param(enumDBFields.t332_falta, oTareaPSP.t332_falta),
        //            Param(enumDBFields.t332_fultmodif, oTareaPSP.t332_fultmodif),
        //            Param(enumDBFields.t332_fiv, oTareaPSP.t332_fiv),
        //            Param(enumDBFields.t332_ffv, oTareaPSP.t332_ffv),
        //            Param(enumDBFields.t332_estado, oTareaPSP.t332_estado),
        //            Param(enumDBFields.t332_fipl, oTareaPSP.t332_fipl),
        //            Param(enumDBFields.t332_ffpl, oTareaPSP.t332_ffpl),
        //            Param(enumDBFields.t332_etpl, oTareaPSP.t332_etpl),
        //            Param(enumDBFields.t332_ffpr, oTareaPSP.t332_ffpr),
        //            Param(enumDBFields.t332_etpr, oTareaPSP.t332_etpr),
        //            Param(enumDBFields.t346_idpst, oTareaPSP.t346_idpst),
        //            Param(enumDBFields.t332_cle, oTareaPSP.t332_cle),
        //            Param(enumDBFields.t332_tipocle, oTareaPSP.t332_tipocle),
        //            Param(enumDBFields.t332_orden, oTareaPSP.t332_orden),
        //            Param(enumDBFields.t332_facturable, oTareaPSP.t332_facturable),
        //            Param(enumDBFields.t332_presupuesto, oTareaPSP.t332_presupuesto),
        //            Param(enumDBFields.t353_idorigen, oTareaPSP.t353_idorigen),
        //            Param(enumDBFields.t332_otl, oTareaPSP.t332_otl),
        //            Param(enumDBFields.t332_incidencia, oTareaPSP.t332_incidencia),
        //            Param(enumDBFields.t332_observaciones, oTareaPSP.t332_observaciones),
        //            Param(enumDBFields.t332_notificable, oTareaPSP.t332_notificable),
        //            Param(enumDBFields.t332_notas1, oTareaPSP.t332_notas1),
        //            Param(enumDBFields.t332_notas2, oTareaPSP.t332_notas2),
        //            Param(enumDBFields.t332_notas3, oTareaPSP.t332_notas3),
        //            Param(enumDBFields.t332_notas4, oTareaPSP.t332_notas4),
        //            Param(enumDBFields.t332_avance, oTareaPSP.t332_avance),
        //            Param(enumDBFields.t332_avanceauto, oTareaPSP.t332_avanceauto),
        //            Param(enumDBFields.t314_idusuario_fin, oTareaPSP.t314_idusuario_fin),
        //            Param(enumDBFields.t332_ffin, oTareaPSP.t332_ffin),
        //            Param(enumDBFields.t314_idusuario_cierre, oTareaPSP.t314_idusuario_cierre),
        //            Param(enumDBFields.t332_fcierre, oTareaPSP.t332_fcierre),
        //            Param(enumDBFields.t332_impiap, oTareaPSP.t332_impiap),
        //            Param(enumDBFields.t332_notasiap, oTareaPSP.t332_notasiap),
        //            Param(enumDBFields.t332_heredanodo, oTareaPSP.t332_heredanodo),
        //            Param(enumDBFields.t332_heredaproyeco, oTareaPSP.t332_heredaproyeco),
        //            Param(enumDBFields.t332_mensaje, oTareaPSP.t332_mensaje),
        //            Param(enumDBFields.t332_acceso_iap, oTareaPSP.t332_acceso_iap),
        //            Param(enumDBFields.t324_idmodofact, oTareaPSP.t324_idmodofact),
        //            Param(enumDBFields.t324_denominacion, oTareaPSP.t324_denominacion)
        //        };

        //        return (int)cDblib.Execute("_TareaPSP_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Obtiene un TareaPSP a partir del id
        /// </summary>
        internal Models.TareaPSP Select(int t332_idtarea)
        {
            Models.TareaPSP oTareaPSP = null;
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t332_idtarea, t332_idtarea)
                };

                dr = cDblib.DataReader("SUP_TareaPSP_S", dbparams);
                if (dr.Read())
                {
                    oTareaPSP = new Models.TareaPSP();
                    oTareaPSP.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    oTareaPSP.t332_destarea = Convert.ToString(dr["t332_destarea"]);
                    oTareaPSP.t332_destarealong = Convert.ToString(dr["t332_destarealong"]);
                    oTareaPSP.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    if (!Convert.IsDBNull(dr["t334_idfase"]))
                        oTareaPSP.t334_idfase = Convert.ToInt32(dr["t334_idfase"]);
                    if (!Convert.IsDBNull(dr["t335_idactividad"]))
                        oTareaPSP.t335_idactividad = Convert.ToInt32(dr["t335_idactividad"]);
                    if (!Convert.IsDBNull(dr["t314_idusuario_promotor"]))
                        oTareaPSP.t314_idusuario_promotor = Convert.ToInt32(dr["t314_idusuario_promotor"]);
                    if (!Convert.IsDBNull(dr["t314_idusuario_ultmodif"]))
                        oTareaPSP.t314_idusuario_ultmodif = Convert.ToInt32(dr["t314_idusuario_ultmodif"]);
                    oTareaPSP.t332_falta = Convert.ToDateTime(dr["t332_falta"]);
                    oTareaPSP.t332_fultmodif = Convert.ToDateTime(dr["t332_fultmodif"]);
                    oTareaPSP.t332_fiv = Convert.ToDateTime(dr["t332_fiv"]);
                    if (!Convert.IsDBNull(dr["t332_ffv"]))
                        oTareaPSP.t332_ffv = Convert.ToDateTime(dr["t332_ffv"]);
                    oTareaPSP.t332_estado = Convert.ToByte(dr["t332_estado"]);
                    if (!Convert.IsDBNull(dr["t332_fipl"]))
                        oTareaPSP.t332_fipl = Convert.ToDateTime(dr["t332_fipl"]);
                    if (!Convert.IsDBNull(dr["t332_ffpl"]))
                        oTareaPSP.t332_ffpl = Convert.ToDateTime(dr["t332_ffpl"]);
                    if (!Convert.IsDBNull(dr["t332_etpl"]))
                        oTareaPSP.t332_etpl = Convert.ToDouble(dr["t332_etpl"]);
                    if (!Convert.IsDBNull(dr["t332_ffpr"]))
                        oTareaPSP.t332_ffpr = Convert.ToDateTime(dr["t332_ffpr"]);
                    if (!Convert.IsDBNull(dr["t332_etpr"]))
                        oTareaPSP.t332_etpr = Convert.ToDouble(dr["t332_etpr"]);
                    if (!Convert.IsDBNull(dr["t346_idpst"]))
                        oTareaPSP.t346_idpst = Convert.ToInt32(dr["t346_idpst"]);
                    if (!Convert.IsDBNull(dr["t332_cle"]))
                        oTareaPSP.t332_cle = Convert.ToSingle(dr["t332_cle"]);
                    oTareaPSP.t332_tipocle = Convert.ToString(dr["t332_tipocle"]);
                    oTareaPSP.t332_orden = Convert.ToInt32(dr["t332_orden"]);
                    oTareaPSP.t332_facturable = Convert.ToBoolean(dr["t332_facturable"]);
                    oTareaPSP.t332_presupuesto = Convert.ToDecimal(dr["t332_presupuesto"]);
                    if (!Convert.IsDBNull(dr["t353_idorigen"]))
                        oTareaPSP.t353_idorigen = Convert.ToInt32(dr["t353_idorigen"]);
                    oTareaPSP.t332_otl = Convert.ToString(dr["t332_otl"]);
                    oTareaPSP.t332_incidencia = Convert.ToString(dr["t332_incidencia"]);
                    oTareaPSP.t332_observaciones = Convert.ToString(dr["t332_observaciones"]);
                    oTareaPSP.t332_notificable = Convert.ToBoolean(dr["t332_notificable"]);
                    oTareaPSP.t332_notas1 = Convert.ToString(dr["t332_notas1"]);
                    oTareaPSP.t332_notas2 = Convert.ToString(dr["t332_notas2"]);
                    oTareaPSP.t332_notas3 = Convert.ToString(dr["t332_notas3"]);
                    oTareaPSP.t332_notas4 = Convert.ToString(dr["t332_notas4"]);
                    if (!Convert.IsDBNull(dr["t332_avance"]))
                        oTareaPSP.t332_avance = Convert.ToDouble(dr["t332_avance"]);
                    oTareaPSP.t332_avanceauto = Convert.ToBoolean(dr["t332_avanceauto"]);
                    if (!Convert.IsDBNull(dr["t314_idusuario_fin"]))
                        oTareaPSP.t314_idusuario_fin = Convert.ToInt32(dr["t314_idusuario_fin"]);
                    if (!Convert.IsDBNull(dr["t332_ffin"]))
                        oTareaPSP.t332_ffin = Convert.ToDateTime(dr["t332_ffin"]);
                    if (!Convert.IsDBNull(dr["t314_idusuario_cierre"]))
                        oTareaPSP.t314_idusuario_cierre = Convert.ToInt32(dr["t314_idusuario_cierre"]);
                    if (!Convert.IsDBNull(dr["t332_fcierre"]))
                        oTareaPSP.t332_fcierre = Convert.ToDateTime(dr["t332_fcierre"]);
                    oTareaPSP.t332_impiap = Convert.ToBoolean(dr["t332_impiap"]);
                    oTareaPSP.t332_notasiap = Convert.ToBoolean(dr["t332_notasiap"]);
                    oTareaPSP.t332_heredanodo = Convert.ToBoolean(dr["t332_heredanodo"]);
                    oTareaPSP.t332_heredaproyeco = Convert.ToBoolean(dr["t332_heredaproyeco"]);
                    oTareaPSP.t332_mensaje = Convert.ToString(dr["t332_mensaje"]);
                    oTareaPSP.t332_acceso_iap = Convert.ToString(dr["t332_acceso_iap"]);
                    if (!Convert.IsDBNull(dr["t324_idmodofact"]))
                        oTareaPSP.t324_idmodofact = Convert.ToInt32(dr["t324_idmodofact"]);
                    if (!Convert.IsDBNull(dr["t324_denominacion"]))
                        oTareaPSP.t324_denominacion = Convert.ToString(dr["t324_denominacion"]);

                }
                return oTareaPSP;

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
        ///// Actualiza un TareaPSP a partir del id
        ///// </summary>
        //internal int Update(Models.TareaPSP oTareaPSP)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[47] {
        //            Param(enumDBFields.t332_idtarea, oTareaPSP.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oTareaPSP.t332_destarea),
        //            Param(enumDBFields.t332_destarealong, oTareaPSP.t332_destarealong),
        //            Param(enumDBFields.t331_idpt, oTareaPSP.t331_idpt),
        //            Param(enumDBFields.t334_idfase, oTareaPSP.t334_idfase),
        //            Param(enumDBFields.t335_idactividad, oTareaPSP.t335_idactividad),
        //            Param(enumDBFields.t314_idusuario_promotor, oTareaPSP.t314_idusuario_promotor),
        //            Param(enumDBFields.t314_idusuario_ultmodif, oTareaPSP.t314_idusuario_ultmodif),
        //            Param(enumDBFields.t332_falta, oTareaPSP.t332_falta),
        //            Param(enumDBFields.t332_fultmodif, oTareaPSP.t332_fultmodif),
        //            Param(enumDBFields.t332_fiv, oTareaPSP.t332_fiv),
        //            Param(enumDBFields.t332_ffv, oTareaPSP.t332_ffv),
        //            Param(enumDBFields.t332_estado, oTareaPSP.t332_estado),
        //            Param(enumDBFields.t332_fipl, oTareaPSP.t332_fipl),
        //            Param(enumDBFields.t332_ffpl, oTareaPSP.t332_ffpl),
        //            Param(enumDBFields.t332_etpl, oTareaPSP.t332_etpl),
        //            Param(enumDBFields.t332_ffpr, oTareaPSP.t332_ffpr),
        //            Param(enumDBFields.t332_etpr, oTareaPSP.t332_etpr),
        //            Param(enumDBFields.t346_idpst, oTareaPSP.t346_idpst),
        //            Param(enumDBFields.t332_cle, oTareaPSP.t332_cle),
        //            Param(enumDBFields.t332_tipocle, oTareaPSP.t332_tipocle),
        //            Param(enumDBFields.t332_orden, oTareaPSP.t332_orden),
        //            Param(enumDBFields.t332_facturable, oTareaPSP.t332_facturable),
        //            Param(enumDBFields.t332_presupuesto, oTareaPSP.t332_presupuesto),
        //            Param(enumDBFields.t353_idorigen, oTareaPSP.t353_idorigen),
        //            Param(enumDBFields.t332_otl, oTareaPSP.t332_otl),
        //            Param(enumDBFields.t332_incidencia, oTareaPSP.t332_incidencia),
        //            Param(enumDBFields.t332_observaciones, oTareaPSP.t332_observaciones),
        //            Param(enumDBFields.t332_notificable, oTareaPSP.t332_notificable),
        //            Param(enumDBFields.t332_notas1, oTareaPSP.t332_notas1),
        //            Param(enumDBFields.t332_notas2, oTareaPSP.t332_notas2),
        //            Param(enumDBFields.t332_notas3, oTareaPSP.t332_notas3),
        //            Param(enumDBFields.t332_notas4, oTareaPSP.t332_notas4),
        //            Param(enumDBFields.t332_avance, oTareaPSP.t332_avance),
        //            Param(enumDBFields.t332_avanceauto, oTareaPSP.t332_avanceauto),
        //            Param(enumDBFields.t314_idusuario_fin, oTareaPSP.t314_idusuario_fin),
        //            Param(enumDBFields.t332_ffin, oTareaPSP.t332_ffin),
        //            Param(enumDBFields.t314_idusuario_cierre, oTareaPSP.t314_idusuario_cierre),
        //            Param(enumDBFields.t332_fcierre, oTareaPSP.t332_fcierre),
        //            Param(enumDBFields.t332_impiap, oTareaPSP.t332_impiap),
        //            Param(enumDBFields.t332_notasiap, oTareaPSP.t332_notasiap),
        //            Param(enumDBFields.t332_heredanodo, oTareaPSP.t332_heredanodo),
        //            Param(enumDBFields.t332_heredaproyeco, oTareaPSP.t332_heredaproyeco),
        //            Param(enumDBFields.t332_mensaje, oTareaPSP.t332_mensaje),
        //            Param(enumDBFields.t332_acceso_iap, oTareaPSP.t332_acceso_iap),
        //            Param(enumDBFields.t324_idmodofact, oTareaPSP.t324_idmodofact),
        //            Param(enumDBFields.t324_denominacion, oTareaPSP.t324_denominacion)
        //        };
                           
        //        return (int)cDblib.Execute("_TareaPSP_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un TareaPSP a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_TareaPSP_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los TareaPSP
        ///// </summary>
        //internal List<Models.TareaPSP> Catalogo(Models.TareaPSP oTareaPSPFilter)
        //{
        //    Models.TareaPSP oTareaPSP = null;
        //    List<Models.TareaPSP> lst = new List<Models.TareaPSP>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[47] {
        //            Param(enumDBFields.t332_idtarea, oTEMP_TareaPSPFilter.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oTEMP_TareaPSPFilter.t332_destarea),
        //            Param(enumDBFields.t332_destarealong, oTEMP_TareaPSPFilter.t332_destarealong),
        //            Param(enumDBFields.t331_idpt, oTEMP_TareaPSPFilter.t331_idpt),
        //            Param(enumDBFields.t334_idfase, oTEMP_TareaPSPFilter.t334_idfase),
        //            Param(enumDBFields.t335_idactividad, oTEMP_TareaPSPFilter.t335_idactividad),
        //            Param(enumDBFields.t314_idusuario_promotor, oTEMP_TareaPSPFilter.t314_idusuario_promotor),
        //            Param(enumDBFields.t314_idusuario_ultmodif, oTEMP_TareaPSPFilter.t314_idusuario_ultmodif),
        //            Param(enumDBFields.t332_falta, oTEMP_TareaPSPFilter.t332_falta),
        //            Param(enumDBFields.t332_fultmodif, oTEMP_TareaPSPFilter.t332_fultmodif),
        //            Param(enumDBFields.t332_fiv, oTEMP_TareaPSPFilter.t332_fiv),
        //            Param(enumDBFields.t332_ffv, oTEMP_TareaPSPFilter.t332_ffv),
        //            Param(enumDBFields.t332_estado, oTEMP_TareaPSPFilter.t332_estado),
        //            Param(enumDBFields.t332_fipl, oTEMP_TareaPSPFilter.t332_fipl),
        //            Param(enumDBFields.t332_ffpl, oTEMP_TareaPSPFilter.t332_ffpl),
        //            Param(enumDBFields.t332_etpl, oTEMP_TareaPSPFilter.t332_etpl),
        //            Param(enumDBFields.t332_ffpr, oTEMP_TareaPSPFilter.t332_ffpr),
        //            Param(enumDBFields.t332_etpr, oTEMP_TareaPSPFilter.t332_etpr),
        //            Param(enumDBFields.t346_idpst, oTEMP_TareaPSPFilter.t346_idpst),
        //            Param(enumDBFields.t332_cle, oTEMP_TareaPSPFilter.t332_cle),
        //            Param(enumDBFields.t332_tipocle, oTEMP_TareaPSPFilter.t332_tipocle),
        //            Param(enumDBFields.t332_orden, oTEMP_TareaPSPFilter.t332_orden),
        //            Param(enumDBFields.t332_facturable, oTEMP_TareaPSPFilter.t332_facturable),
        //            Param(enumDBFields.t332_presupuesto, oTEMP_TareaPSPFilter.t332_presupuesto),
        //            Param(enumDBFields.t353_idorigen, oTEMP_TareaPSPFilter.t353_idorigen),
        //            Param(enumDBFields.t332_otl, oTEMP_TareaPSPFilter.t332_otl),
        //            Param(enumDBFields.t332_incidencia, oTEMP_TareaPSPFilter.t332_incidencia),
        //            Param(enumDBFields.t332_observaciones, oTEMP_TareaPSPFilter.t332_observaciones),
        //            Param(enumDBFields.t332_notificable, oTEMP_TareaPSPFilter.t332_notificable),
        //            Param(enumDBFields.t332_notas1, oTEMP_TareaPSPFilter.t332_notas1),
        //            Param(enumDBFields.t332_notas2, oTEMP_TareaPSPFilter.t332_notas2),
        //            Param(enumDBFields.t332_notas3, oTEMP_TareaPSPFilter.t332_notas3),
        //            Param(enumDBFields.t332_notas4, oTEMP_TareaPSPFilter.t332_notas4),
        //            Param(enumDBFields.t332_avance, oTEMP_TareaPSPFilter.t332_avance),
        //            Param(enumDBFields.t332_avanceauto, oTEMP_TareaPSPFilter.t332_avanceauto),
        //            Param(enumDBFields.t314_idusuario_fin, oTEMP_TareaPSPFilter.t314_idusuario_fin),
        //            Param(enumDBFields.t332_ffin, oTEMP_TareaPSPFilter.t332_ffin),
        //            Param(enumDBFields.t314_idusuario_cierre, oTEMP_TareaPSPFilter.t314_idusuario_cierre),
        //            Param(enumDBFields.t332_fcierre, oTEMP_TareaPSPFilter.t332_fcierre),
        //            Param(enumDBFields.t332_impiap, oTEMP_TareaPSPFilter.t332_impiap),
        //            Param(enumDBFields.t332_notasiap, oTEMP_TareaPSPFilter.t332_notasiap),
        //            Param(enumDBFields.t332_heredanodo, oTEMP_TareaPSPFilter.t332_heredanodo),
        //            Param(enumDBFields.t332_heredaproyeco, oTEMP_TareaPSPFilter.t332_heredaproyeco),
        //            Param(enumDBFields.t332_mensaje, oTEMP_TareaPSPFilter.t332_mensaje),
        //            Param(enumDBFields.t332_acceso_iap, oTEMP_TareaPSPFilter.t332_acceso_iap),
        //            Param(enumDBFields.t324_idmodofact, oTEMP_TareaPSPFilter.t324_idmodofact),
        //            Param(enumDBFields.t324_denominacion, oTEMP_TareaPSPFilter.t324_denominacion)
        //        };

        //        dr = cDblib.DataReader("_TareaPSP_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oTareaPSP = new Models.TareaPSP();
        //            oTareaPSP.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oTareaPSP.t332_destarea=Convert.ToString(dr["t332_destarea"]);
        //            oTareaPSP.t332_destarealong=Convert.ToString(dr["t332_destarealong"]);
        //            oTareaPSP.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
        //            if(!Convert.IsDBNull(dr["t334_idfase"]))
        //                oTareaPSP.t334_idfase=Convert.ToInt32(dr["t334_idfase"]);
        //            if(!Convert.IsDBNull(dr["t335_idactividad"]))
        //                oTareaPSP.t335_idactividad=Convert.ToInt32(dr["t335_idactividad"]);
        //            if(!Convert.IsDBNull(dr["t314_idusuario_promotor"]))
        //                oTareaPSP.t314_idusuario_promotor=Convert.ToInt32(dr["t314_idusuario_promotor"]);
        //            if(!Convert.IsDBNull(dr["t314_idusuario_ultmodif"]))
        //                oTareaPSP.t314_idusuario_ultmodif=Convert.ToInt32(dr["t314_idusuario_ultmodif"]);
        //            oTareaPSP.t332_falta=Convert.ToDateTime(dr["t332_falta"]);
        //            oTareaPSP.t332_fultmodif=Convert.ToDateTime(dr["t332_fultmodif"]);
        //            oTareaPSP.t332_fiv=Convert.ToDateTime(dr["t332_fiv"]);
        //            if(!Convert.IsDBNull(dr["t332_ffv"]))
        //                oTareaPSP.t332_ffv=Convert.ToDateTime(dr["t332_ffv"]);
        //            oTareaPSP.t332_estado=Convert.ToByte(dr["t332_estado"]);
        //            if(!Convert.IsDBNull(dr["t332_fipl"]))
        //                oTareaPSP.t332_fipl=Convert.ToDateTime(dr["t332_fipl"]);
        //            if(!Convert.IsDBNull(dr["t332_ffpl"]))
        //                oTareaPSP.t332_ffpl=Convert.ToDateTime(dr["t332_ffpl"]);
        //            if(!Convert.IsDBNull(dr["t332_etpl"]))
        //                oTareaPSP.t332_etpl=Convert.ToDouble(dr["t332_etpl"]);
        //            if(!Convert.IsDBNull(dr["t332_ffpr"]))
        //                oTareaPSP.t332_ffpr=Convert.ToDateTime(dr["t332_ffpr"]);
        //            if(!Convert.IsDBNull(dr["t332_etpr"]))
        //                oTareaPSP.t332_etpr=Convert.ToDouble(dr["t332_etpr"]);
        //            if(!Convert.IsDBNull(dr["t346_idpst"]))
        //                oTareaPSP.t346_idpst=Convert.ToInt32(dr["t346_idpst"]);
        //            if(!Convert.IsDBNull(dr["t332_cle"]))
        //                oTareaPSP.t332_cle=Convert.ToSingle(dr["t332_cle"]);
        //            oTareaPSP.t332_tipocle=Convert.ToString(dr["t332_tipocle"]);
        //            oTareaPSP.t332_orden=Convert.ToInt32(dr["t332_orden"]);
        //            oTareaPSP.t332_facturable=Convert.ToBoolean(dr["t332_facturable"]);
        //            oTareaPSP.t332_presupuesto=Convert.ToDecimal(dr["t332_presupuesto"]);
        //            if(!Convert.IsDBNull(dr["t353_idorigen"]))
        //                oTareaPSP.t353_idorigen=Convert.ToInt32(dr["t353_idorigen"]);
        //            oTareaPSP.t332_otl=Convert.ToString(dr["t332_otl"]);
        //            oTareaPSP.t332_incidencia=Convert.ToString(dr["t332_incidencia"]);
        //            oTareaPSP.t332_observaciones=Convert.ToString(dr["t332_observaciones"]);
        //            oTareaPSP.t332_notificable=Convert.ToBoolean(dr["t332_notificable"]);
        //            oTareaPSP.t332_notas1=Convert.ToString(dr["t332_notas1"]);
        //            oTareaPSP.t332_notas2=Convert.ToString(dr["t332_notas2"]);
        //            oTareaPSP.t332_notas3=Convert.ToString(dr["t332_notas3"]);
        //            oTareaPSP.t332_notas4=Convert.ToString(dr["t332_notas4"]);
        //            if(!Convert.IsDBNull(dr["t332_avance"]))
        //                oTareaPSP.t332_avance=Convert.ToDouble(dr["t332_avance"]);
        //            oTareaPSP.t332_avanceauto=Convert.ToBoolean(dr["t332_avanceauto"]);
        //            if(!Convert.IsDBNull(dr["t314_idusuario_fin"]))
        //                oTareaPSP.t314_idusuario_fin=Convert.ToInt32(dr["t314_idusuario_fin"]);
        //            if(!Convert.IsDBNull(dr["t332_ffin"]))
        //                oTareaPSP.t332_ffin=Convert.ToDateTime(dr["t332_ffin"]);
        //            if(!Convert.IsDBNull(dr["t314_idusuario_cierre"]))
        //                oTareaPSP.t314_idusuario_cierre=Convert.ToInt32(dr["t314_idusuario_cierre"]);
        //            if(!Convert.IsDBNull(dr["t332_fcierre"]))
        //                oTareaPSP.t332_fcierre=Convert.ToDateTime(dr["t332_fcierre"]);
        //            oTareaPSP.t332_impiap=Convert.ToBoolean(dr["t332_impiap"]);
        //            oTareaPSP.t332_notasiap=Convert.ToBoolean(dr["t332_notasiap"]);
        //            oTareaPSP.t332_heredanodo=Convert.ToBoolean(dr["t332_heredanodo"]);
        //            oTareaPSP.t332_heredaproyeco=Convert.ToBoolean(dr["t332_heredaproyeco"]);
        //            oTareaPSP.t332_mensaje=Convert.ToString(dr["t332_mensaje"]);
        //            oTareaPSP.t332_acceso_iap=Convert.ToString(dr["t332_acceso_iap"]);
        //            if(!Convert.IsDBNull(dr["t324_idmodofact"]))
        //                oTareaPSP.t324_idmodofact=Convert.ToInt32(dr["t324_idmodofact"]);
        //            if(!Convert.IsDBNull(dr["t324_denominacion"]))
        //                oTareaPSP.t324_denominacion=Convert.ToString(dr["t324_denominacion"]);

        //            lst.Add(oTareaPSP);

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
				case enumDBFields.t332_idtarea:
					paramName = "@t332_idtarea";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_destarea:
					paramName = "@t332_destarea";
					paramType = SqlDbType.VarChar;
					paramSize = 100;
					break;
				case enumDBFields.t332_destarealong:
					paramName = "@t332_destarealong";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t331_idpt:
					paramName = "@t331_idpt";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t334_idfase:
					paramName = "@t334_idfase";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t335_idactividad:
					paramName = "@t335_idactividad";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t314_idusuario_promotor:
					paramName = "@t314_idusuario_promotor";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t314_idusuario_ultmodif:
					paramName = "@t314_idusuario_ultmodif";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_falta:
					paramName = "@t332_falta";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t332_fultmodif:
					paramName = "@t332_fultmodif";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t332_fiv:
					paramName = "@t332_fiv";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t332_ffv:
					paramName = "@t332_ffv";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t332_estado:
					paramName = "@t332_estado";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.t332_fipl:
					paramName = "@t332_fipl";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t332_ffpl:
					paramName = "@t332_ffpl";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t332_etpl:
					paramName = "@t332_etpl";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.t332_ffpr:
					paramName = "@t332_ffpr";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t332_etpr:
					paramName = "@t332_etpr";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.t346_idpst:
					paramName = "@t346_idpst";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_cle:
					paramName = "@t332_cle";
					paramType = SqlDbType.Real;
					paramSize = 8;
					break;
				case enumDBFields.t332_tipocle:
					paramName = "@t332_tipocle";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
				case enumDBFields.t332_orden:
					paramName = "@t332_orden";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_facturable:
					paramName = "@t332_facturable";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t332_presupuesto:
					paramName = "@t332_presupuesto";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.t353_idorigen:
					paramName = "@t353_idorigen";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_otl:
					paramName = "@t332_otl";
					paramType = SqlDbType.VarChar;
					paramSize = 25;
					break;
				case enumDBFields.t332_incidencia:
					paramName = "@t332_incidencia";
					paramType = SqlDbType.VarChar;
					paramSize = 25;
					break;
				case enumDBFields.t332_observaciones:
					paramName = "@t332_observaciones";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t332_notificable:
					paramName = "@t332_notificable";
					paramType = SqlDbType.Bit;
					paramSize = 1;
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
				case enumDBFields.t332_avance:
					paramName = "@t332_avance";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.t332_avanceauto:
					paramName = "@t332_avanceauto";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_idusuario_fin:
					paramName = "@t314_idusuario_fin";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_ffin:
					paramName = "@t332_ffin";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t314_idusuario_cierre:
					paramName = "@t314_idusuario_cierre";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_fcierre:
					paramName = "@t332_fcierre";
					paramType = SqlDbType.DateTime;
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
				case enumDBFields.t332_heredanodo:
					paramName = "@t332_heredanodo";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t332_heredaproyeco:
					paramName = "@t332_heredaproyeco";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t332_mensaje:
					paramName = "@t332_mensaje";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t332_acceso_iap:
					paramName = "@t332_acceso_iap";
					paramType = SqlDbType.Char;
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
			}


            dbParam = cDblib.dbParameter(paramName, paramType, paramSize);
            dbParam.Direction = paramDirection;
            if (paramDirection == ParameterDirection.Input | paramDirection == ParameterDirection.InputOutput) dbParam.Value = value;

            return dbParam;

        }
		
		#endregion
    
    }

}
