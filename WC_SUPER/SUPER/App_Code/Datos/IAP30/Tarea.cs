using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Tarea
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class Tarea 
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
			t332_notificable = 7,
			t332_fiv = 8,
			t332_ffv = 9,
			t332_estado = 10,
			t332_fipl = 11,
			t332_ffpl = 12,
			t332_etpl = 13,
			t332_ffpr = 14,
			t332_etpr = 15,
			t332_observaciones = 16,
			t332_cle = 17,
			t332_tipocle = 18,
			t332_orden = 19,
			t332_facturable = 20,
			t305_idproyectosubnodo = 21,
			t305_cualidad = 22,
			t303_idnodo = 23,
			t303_denominacion = 24,
			num_proyecto = 25,
			nom_proyecto = 26,
			t331_despt = 27,
			t334_desfase = 28,
			t335_desactividad = 29,
			cod_cliente = 30,
			nom_cliente = 31,
			t332_presupuesto = 32,
			t353_idorigen = 33,
			t332_incidencia = 34,
			t332_avance = 35,
			t332_avanceauto = 36,
			t332_impiap = 37,
			t305_admiterecursospst = 38,
			t331_heredanodo = 39,
			t331_heredaproyeco = 40,
			t334_heredanodo = 41,
			t334_heredaproyeco = 42,
			t335_heredanodo = 43,
			t335_heredaproyeco = 44,
			t332_heredanodo = 45,
			t332_heredaproyeco = 46,
			t332_mensaje = 47,
			t332_notif_prof = 48,
			t305_avisorecursopst = 49,
			t301_estado = 50,
			t332_acceso_iap = 51,
			t324_idmodofact = 52,
			t324_denominacion = 53,
			t301_esreplicable = 54,
			t305_opd = 55,
            nProy = 56
        }

        internal Tarea(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un Tarea
        ///// </summary>
        //internal int Insert(Models.Tarea oTarea)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[55] {
        //            Param(enumDBFields.t332_idtarea, oTarea.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oTarea.t332_destarea),
        //            Param(enumDBFields.t332_destarealong, oTarea.t332_destarealong),
        //            Param(enumDBFields.t331_idpt, oTarea.t331_idpt),
        //            Param(enumDBFields.t334_idfase, oTarea.t334_idfase),
        //            Param(enumDBFields.t335_idactividad, oTarea.t335_idactividad),
        //            Param(enumDBFields.t332_notificable, oTarea.t332_notificable),
        //            Param(enumDBFields.t332_fiv, oTarea.t332_fiv),
        //            Param(enumDBFields.t332_ffv, oTarea.t332_ffv),
        //            Param(enumDBFields.t332_estado, oTarea.t332_estado),
        //            Param(enumDBFields.t332_fipl, oTarea.t332_fipl),
        //            Param(enumDBFields.t332_ffpl, oTarea.t332_ffpl),
        //            Param(enumDBFields.t332_etpl, oTarea.t332_etpl),
        //            Param(enumDBFields.t332_ffpr, oTarea.t332_ffpr),
        //            Param(enumDBFields.t332_etpr, oTarea.t332_etpr),
        //            Param(enumDBFields.t332_observaciones, oTarea.t332_observaciones),
        //            Param(enumDBFields.t332_cle, oTarea.t332_cle),
        //            Param(enumDBFields.t332_tipocle, oTarea.t332_tipocle),
        //            Param(enumDBFields.t332_orden, oTarea.t332_orden),
        //            Param(enumDBFields.t332_facturable, oTarea.t332_facturable),
        //            Param(enumDBFields.t305_idproyectosubnodo, oTarea.t305_idproyectosubnodo),
        //            Param(enumDBFields.t305_cualidad, oTarea.t305_cualidad),
        //            Param(enumDBFields.t303_idnodo, oTarea.t303_idnodo),
        //            Param(enumDBFields.t303_denominacion, oTarea.t303_denominacion),
        //            Param(enumDBFields.num_proyecto, oTarea.num_proyecto),
        //            Param(enumDBFields.nom_proyecto, oTarea.nom_proyecto),
        //            Param(enumDBFields.t331_despt, oTarea.t331_despt),
        //            Param(enumDBFields.t334_desfase, oTarea.t334_desfase),
        //            Param(enumDBFields.t335_desactividad, oTarea.t335_desactividad),
        //            Param(enumDBFields.cod_cliente, oTarea.cod_cliente),
        //            Param(enumDBFields.nom_cliente, oTarea.nom_cliente),
        //            Param(enumDBFields.t332_presupuesto, oTarea.t332_presupuesto),
        //            Param(enumDBFields.t353_idorigen, oTarea.t353_idorigen),
        //            Param(enumDBFields.t332_incidencia, oTarea.t332_incidencia),
        //            Param(enumDBFields.t332_avance, oTarea.t332_avance),
        //            Param(enumDBFields.t332_avanceauto, oTarea.t332_avanceauto),
        //            Param(enumDBFields.t332_impiap, oTarea.t332_impiap),
        //            Param(enumDBFields.t305_admiterecursospst, oTarea.t305_admiterecursospst),
        //            Param(enumDBFields.t331_heredanodo, oTarea.t331_heredanodo),
        //            Param(enumDBFields.t331_heredaproyeco, oTarea.t331_heredaproyeco),
        //            Param(enumDBFields.t334_heredanodo, oTarea.t334_heredanodo),
        //            Param(enumDBFields.t334_heredaproyeco, oTarea.t334_heredaproyeco),
        //            Param(enumDBFields.t335_heredanodo, oTarea.t335_heredanodo),
        //            Param(enumDBFields.t335_heredaproyeco, oTarea.t335_heredaproyeco),
        //            Param(enumDBFields.t332_heredanodo, oTarea.t332_heredanodo),
        //            Param(enumDBFields.t332_heredaproyeco, oTarea.t332_heredaproyeco),
        //            Param(enumDBFields.t332_mensaje, oTarea.t332_mensaje),
        //            Param(enumDBFields.t332_notif_prof, oTarea.t332_notif_prof),
        //            Param(enumDBFields.t305_avisorecursopst, oTarea.t305_avisorecursopst),
        //            Param(enumDBFields.t301_estado, oTarea.t301_estado),
        //            Param(enumDBFields.t332_acceso_iap, oTarea.t332_acceso_iap),
        //            Param(enumDBFields.t324_idmodofact, oTarea.t324_idmodofact),
        //            Param(enumDBFields.t324_denominacion, oTarea.t324_denominacion),
        //            Param(enumDBFields.t301_esreplicable, oTarea.t301_esreplicable),
        //            Param(enumDBFields.t305_opd, oTarea.t305_opd)
        //        };

        //        return (int)cDblib.Execute("_Tarea_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un Tarea a partir del id
        ///// </summary>
        //internal Models.Tarea Select()
        //{
        //    Models.Tarea oTarea = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_Tarea_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oTarea = new Models.Tarea();
        //            oTarea.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oTarea.t332_destarea=Convert.ToString(dr["t332_destarea"]);
        //            oTarea.t332_destarealong=Convert.ToString(dr["t332_destarealong"]);
        //            oTarea.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
        //            if(!Convert.IsDBNull(dr["t334_idfase"]))
        //                oTarea.t334_idfase=Convert.ToInt32(dr["t334_idfase"]);
        //            if(!Convert.IsDBNull(dr["t335_idactividad"]))
        //                oTarea.t335_idactividad=Convert.ToInt32(dr["t335_idactividad"]);
        //            oTarea.t332_notificable=Convert.ToBoolean(dr["t332_notificable"]);
        //            oTarea.t332_fiv=Convert.ToDateTime(dr["t332_fiv"]);
        //            if(!Convert.IsDBNull(dr["t332_ffv"]))
        //                oTarea.t332_ffv=Convert.ToDateTime(dr["t332_ffv"]);
        //            oTarea.t332_estado=Convert.ToByte(dr["t332_estado"]);
        //            if(!Convert.IsDBNull(dr["t332_fipl"]))
        //                oTarea.t332_fipl=Convert.ToDateTime(dr["t332_fipl"]);
        //            if(!Convert.IsDBNull(dr["t332_ffpl"]))
        //                oTarea.t332_ffpl=Convert.ToDateTime(dr["t332_ffpl"]);
        //            if(!Convert.IsDBNull(dr["t332_etpl"]))
        //                oTarea.t332_etpl=Convert.ToDouble(dr["t332_etpl"]);
        //            if(!Convert.IsDBNull(dr["t332_ffpr"]))
        //                oTarea.t332_ffpr=Convert.ToDateTime(dr["t332_ffpr"]);
        //            if(!Convert.IsDBNull(dr["t332_etpr"]))
        //                oTarea.t332_etpr=Convert.ToDouble(dr["t332_etpr"]);
        //            oTarea.t332_observaciones=Convert.ToString(dr["t332_observaciones"]);
        //            if(!Convert.IsDBNull(dr["t332_cle"]))
        //                oTarea.t332_cle=Convert.ToSingle(dr["t332_cle"]);
        //            oTarea.t332_tipocle=Convert.ToString(dr["t332_tipocle"]);
        //            oTarea.t332_orden=Convert.ToInt32(dr["t332_orden"]);
        //            oTarea.t332_facturable=Convert.ToBoolean(dr["t332_facturable"]);
        //            oTarea.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oTarea.t305_cualidad=Convert.ToString(dr["t305_cualidad"]);
        //            oTarea.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
        //            oTarea.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
        //            oTarea.num_proyecto=Convert.ToInt32(dr["num_proyecto"]);
        //            oTarea.nom_proyecto=Convert.ToString(dr["nom_proyecto"]);
        //            oTarea.t331_despt=Convert.ToString(dr["t331_despt"]);
        //            oTarea.t334_desfase=Convert.ToString(dr["t334_desfase"]);
        //            oTarea.t335_desactividad=Convert.ToString(dr["t335_desactividad"]);
        //            oTarea.cod_cliente=Convert.ToInt32(dr["cod_cliente"]);
        //            oTarea.nom_cliente=Convert.ToString(dr["nom_cliente"]);
        //            oTarea.t332_presupuesto=Convert.ToDecimal(dr["t332_presupuesto"]);
        //            if(!Convert.IsDBNull(dr["t353_idorigen"]))
        //                oTarea.t353_idorigen=Convert.ToInt32(dr["t353_idorigen"]);
        //            oTarea.t332_incidencia=Convert.ToString(dr["t332_incidencia"]);
        //            if(!Convert.IsDBNull(dr["t332_avance"]))
        //                oTarea.t332_avance=Convert.ToDouble(dr["t332_avance"]);
        //            oTarea.t332_avanceauto=Convert.ToBoolean(dr["t332_avanceauto"]);
        //            oTarea.t332_impiap=Convert.ToBoolean(dr["t332_impiap"]);
        //            oTarea.t305_admiterecursospst=Convert.ToBoolean(dr["t305_admiterecursospst"]);
        //            oTarea.t331_heredanodo=Convert.ToBoolean(dr["t331_heredanodo"]);
        //            oTarea.t331_heredaproyeco=Convert.ToBoolean(dr["t331_heredaproyeco"]);
        //            if(!Convert.IsDBNull(dr["t334_heredanodo"]))
        //                oTarea.t334_heredanodo=Convert.ToBoolean(dr["t334_heredanodo"]);
        //            if(!Convert.IsDBNull(dr["t334_heredaproyeco"]))
        //                oTarea.t334_heredaproyeco=Convert.ToBoolean(dr["t334_heredaproyeco"]);
        //            if(!Convert.IsDBNull(dr["t335_heredanodo"]))
        //                oTarea.t335_heredanodo=Convert.ToBoolean(dr["t335_heredanodo"]);
        //            if(!Convert.IsDBNull(dr["t335_heredaproyeco"]))
        //                oTarea.t335_heredaproyeco=Convert.ToBoolean(dr["t335_heredaproyeco"]);
        //            oTarea.t332_heredanodo=Convert.ToBoolean(dr["t332_heredanodo"]);
        //            oTarea.t332_heredaproyeco=Convert.ToBoolean(dr["t332_heredaproyeco"]);
        //            oTarea.t332_mensaje=Convert.ToString(dr["t332_mensaje"]);
        //            oTarea.t332_notif_prof=Convert.ToBoolean(dr["t332_notif_prof"]);
        //            oTarea.t305_avisorecursopst=Convert.ToBoolean(dr["t305_avisorecursopst"]);
        //            oTarea.t301_estado=Convert.ToString(dr["t301_estado"]);
        //            oTarea.t332_acceso_iap=Convert.ToString(dr["t332_acceso_iap"]);
        //            if(!Convert.IsDBNull(dr["t324_idmodofact"]))
        //                oTarea.t324_idmodofact=Convert.ToInt32(dr["t324_idmodofact"]);
        //            oTarea.t324_denominacion=Convert.ToString(dr["t324_denominacion"]);
        //            oTarea.t301_esreplicable=Convert.ToBoolean(dr["t301_esreplicable"]);
        //            oTarea.t305_opd=Convert.ToBoolean(dr["t305_opd"]);

        //        }
        //        return oTarea;
				
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
		
        ///// <summary>
        ///// Actualiza un Tarea a partir del id
        ///// </summary>
        //internal int Update(Models.Tarea oTarea)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[55] {
        //            Param(enumDBFields.t332_idtarea, oTarea.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oTarea.t332_destarea),
        //            Param(enumDBFields.t332_destarealong, oTarea.t332_destarealong),
        //            Param(enumDBFields.t331_idpt, oTarea.t331_idpt),
        //            Param(enumDBFields.t334_idfase, oTarea.t334_idfase),
        //            Param(enumDBFields.t335_idactividad, oTarea.t335_idactividad),
        //            Param(enumDBFields.t332_notificable, oTarea.t332_notificable),
        //            Param(enumDBFields.t332_fiv, oTarea.t332_fiv),
        //            Param(enumDBFields.t332_ffv, oTarea.t332_ffv),
        //            Param(enumDBFields.t332_estado, oTarea.t332_estado),
        //            Param(enumDBFields.t332_fipl, oTarea.t332_fipl),
        //            Param(enumDBFields.t332_ffpl, oTarea.t332_ffpl),
        //            Param(enumDBFields.t332_etpl, oTarea.t332_etpl),
        //            Param(enumDBFields.t332_ffpr, oTarea.t332_ffpr),
        //            Param(enumDBFields.t332_etpr, oTarea.t332_etpr),
        //            Param(enumDBFields.t332_observaciones, oTarea.t332_observaciones),
        //            Param(enumDBFields.t332_cle, oTarea.t332_cle),
        //            Param(enumDBFields.t332_tipocle, oTarea.t332_tipocle),
        //            Param(enumDBFields.t332_orden, oTarea.t332_orden),
        //            Param(enumDBFields.t332_facturable, oTarea.t332_facturable),
        //            Param(enumDBFields.t305_idproyectosubnodo, oTarea.t305_idproyectosubnodo),
        //            Param(enumDBFields.t305_cualidad, oTarea.t305_cualidad),
        //            Param(enumDBFields.t303_idnodo, oTarea.t303_idnodo),
        //            Param(enumDBFields.t303_denominacion, oTarea.t303_denominacion),
        //            Param(enumDBFields.num_proyecto, oTarea.num_proyecto),
        //            Param(enumDBFields.nom_proyecto, oTarea.nom_proyecto),
        //            Param(enumDBFields.t331_despt, oTarea.t331_despt),
        //            Param(enumDBFields.t334_desfase, oTarea.t334_desfase),
        //            Param(enumDBFields.t335_desactividad, oTarea.t335_desactividad),
        //            Param(enumDBFields.cod_cliente, oTarea.cod_cliente),
        //            Param(enumDBFields.nom_cliente, oTarea.nom_cliente),
        //            Param(enumDBFields.t332_presupuesto, oTarea.t332_presupuesto),
        //            Param(enumDBFields.t353_idorigen, oTarea.t353_idorigen),
        //            Param(enumDBFields.t332_incidencia, oTarea.t332_incidencia),
        //            Param(enumDBFields.t332_avance, oTarea.t332_avance),
        //            Param(enumDBFields.t332_avanceauto, oTarea.t332_avanceauto),
        //            Param(enumDBFields.t332_impiap, oTarea.t332_impiap),
        //            Param(enumDBFields.t305_admiterecursospst, oTarea.t305_admiterecursospst),
        //            Param(enumDBFields.t331_heredanodo, oTarea.t331_heredanodo),
        //            Param(enumDBFields.t331_heredaproyeco, oTarea.t331_heredaproyeco),
        //            Param(enumDBFields.t334_heredanodo, oTarea.t334_heredanodo),
        //            Param(enumDBFields.t334_heredaproyeco, oTarea.t334_heredaproyeco),
        //            Param(enumDBFields.t335_heredanodo, oTarea.t335_heredanodo),
        //            Param(enumDBFields.t335_heredaproyeco, oTarea.t335_heredaproyeco),
        //            Param(enumDBFields.t332_heredanodo, oTarea.t332_heredanodo),
        //            Param(enumDBFields.t332_heredaproyeco, oTarea.t332_heredaproyeco),
        //            Param(enumDBFields.t332_mensaje, oTarea.t332_mensaje),
        //            Param(enumDBFields.t332_notif_prof, oTarea.t332_notif_prof),
        //            Param(enumDBFields.t305_avisorecursopst, oTarea.t305_avisorecursopst),
        //            Param(enumDBFields.t301_estado, oTarea.t301_estado),
        //            Param(enumDBFields.t332_acceso_iap, oTarea.t332_acceso_iap),
        //            Param(enumDBFields.t324_idmodofact, oTarea.t324_idmodofact),
        //            Param(enumDBFields.t324_denominacion, oTarea.t324_denominacion),
        //            Param(enumDBFields.t301_esreplicable, oTarea.t301_esreplicable),
        //            Param(enumDBFields.t305_opd, oTarea.t305_opd)
        //        };
                           
        //        return (int)cDblib.Execute("_Tarea_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un Tarea a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_Tarea_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Obtiene todos los Tarea
        /// </summary>
        internal List<Models.Tarea> Catalogo(Int32 t305_idproyectosubnodo)
        {
            Models.Tarea oTarea = null;
            List<Models.Tarea> lst = new List<Models.Tarea>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.nProy, t305_idproyectosubnodo)
                };

                dr = cDblib.DataReader("SUP_TAREACATA2", dbparams);
                while (dr.Read())
                {

                    oTarea = new Models.Tarea();
                    oTarea.t332_idtarea = Convert.ToInt32(dr["codTarea"]);
                    oTarea.t332_destarea = Convert.ToString(dr["desTarea"]);
                    if (!Convert.IsDBNull(dr["ETPL"]))
                        oTarea.t332_etpl = Convert.ToDouble(dr["ETPL"]);
                    if (!Convert.IsDBNull(dr["FIPL"]))
                        oTarea.t332_fipl = Convert.ToDateTime(dr["FIPL"]);
                    if (!Convert.IsDBNull(dr["FFPL"]))
                        oTarea.t332_ffpl = Convert.ToDateTime(dr["FFPL"]);
                    if (!Convert.IsDBNull(dr["FFPR"]))
                        oTarea.t332_ffpr = Convert.ToDateTime(dr["FFPR"]);
                    if (!Convert.IsDBNull(dr["ETPR"]))
                        oTarea.t332_etpr = Convert.ToDouble(dr["ETPR"]);
                    
                    oTarea.consumo = Convert.ToDouble(dr["Consumo"]);

                    if (!Convert.IsDBNull(dr["t332_avance"]))
                        oTarea.t332_avance = Convert.ToDouble(dr["t332_avance"]);
                    oTarea.t332_avanceauto = Convert.ToBoolean(dr["t332_avanceauto"]);
                    oTarea.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oTarea.num_proyecto = Convert.ToInt32(dr["num_proyecto"]);
                    oTarea.nom_proyecto = Convert.ToString(dr["nom_proyecto"]);
                    oTarea.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    oTarea.t331_despt = Convert.ToString(dr["t331_despt"]);
                    oTarea.t334_idfase = Convert.ToInt32(dr["t334_idfase"]);
                    oTarea.t334_desfase = Convert.ToString(dr["t334_desfase"]);
                    oTarea.t335_idactividad = Convert.ToInt32(dr["t335_idactividad"]);
                    oTarea.t335_desactividad = Convert.ToString(dr["t335_desactividad"]);

                    lst.Add(oTarea);
                }
                return lst;

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
				case enumDBFields.t332_notificable:
					paramName = "@t332_notificable";
					paramType = SqlDbType.Bit;
					paramSize = 1;
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
				case enumDBFields.t332_observaciones:
					paramName = "@t332_observaciones";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
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
				case enumDBFields.t305_idproyectosubnodo:
					paramName = "@t305_idproyectosubnodo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t305_cualidad:
					paramName = "@t305_cualidad";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
				case enumDBFields.t303_idnodo:
					paramName = "@t303_idnodo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t303_denominacion:
					paramName = "@t303_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.num_proyecto:
					paramName = "@num_proyecto";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.nom_proyecto:
					paramName = "@nom_proyecto";
					paramType = SqlDbType.VarChar;
					paramSize = 70;
					break;
				case enumDBFields.t331_despt:
					paramName = "@t331_despt";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
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
				case enumDBFields.cod_cliente:
					paramName = "@cod_cliente";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.nom_cliente:
					paramName = "@nom_cliente";
					paramType = SqlDbType.VarChar;
					paramSize = 100;
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
				case enumDBFields.t332_incidencia:
					paramName = "@t332_incidencia";
					paramType = SqlDbType.VarChar;
					paramSize = 25;
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
				case enumDBFields.t332_impiap:
					paramName = "@t332_impiap";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t305_admiterecursospst:
					paramName = "@t305_admiterecursospst";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t331_heredanodo:
					paramName = "@t331_heredanodo";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t331_heredaproyeco:
					paramName = "@t331_heredaproyeco";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t334_heredanodo:
					paramName = "@t334_heredanodo";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t334_heredaproyeco:
					paramName = "@t334_heredaproyeco";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t335_heredanodo:
					paramName = "@t335_heredanodo";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t335_heredaproyeco:
					paramName = "@t335_heredaproyeco";
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
				case enumDBFields.t332_notif_prof:
					paramName = "@t332_notif_prof";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t305_avisorecursopst:
					paramName = "@t305_avisorecursopst";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t301_estado:
					paramName = "@t301_estado";
					paramType = SqlDbType.Char;
					paramSize = 1;
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
				case enumDBFields.t301_esreplicable:
					paramName = "@t301_esreplicable";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t305_opd:
					paramName = "@t305_opd";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.nProy:
                    paramName = "@nProy";
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
