using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ProyectoSubNodoSD
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ProyectoSubNodoSD 
    {
        //#region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            t305_idproyectosubnodo = 1,
        //    t301_idproyecto = 2,
        //    t304_idsubnodo = 3,
        //    t305_finalizado = 4,
        //    t305_cualidad = 5,
        //    t305_heredanodo = 6,
        //    t303_idnodo = 7,
        //    t303_denominacion = 8,
        //    t304_denominacion = 9,
        //    t303_ultcierreeco = 10,
        //    t314_idusuario_responsable = 11,
        //    Responsable = 12,
        //    t305_seudonimo = 13,
        //    t305_accesobitacora_iap = 14,
        //    t305_accesobitacora_pst = 15,
        //    t305_imputablegasvi = 16,
        //    t305_admiterecursospst = 17,
        //    t305_avisoresponsablepst = 18,
        //    t305_avisorecursopst = 19,
        //    t305_avisofigura = 20,
        //    t305_modificaciones = 21,
        //    t305_observaciones = 22,
        //    t320_facturable = 23,
        //    mesesCerrados = 24,
        //    t001_ficepi_visador = 25,
        //    Visador = 26,
        //    t305_supervisor_visador = 27,
        //    t476_idcnp = 28,
        //    t485_idcsn1p = 29,
        //    t487_idcsn2p = 30,
        //    t489_idcsn3p = 31,
        //    t491_idcsn4p = 32,
        //    t305_observacionesadm = 33,
        //    t305_importaciongasvi = 34,
        //    t391_denominacion = 35,
        //    t392_denominacion = 36,
        //    t393_denominacion = 37,
        //    t394_denominacion = 38,
        //    t301_categoria = 39,
        //    t422_idmoneda = 40,
        //    t422_denominacion = 41,
        //    t422_denominacionimportes = 42,
        //    t305_opd = 43,
        //    t001_idficepi_visadorcv = 44,
        //    VisadorCV = 45,
        //    t001_idficepi_interlocutor = 46,
        //    Interlocutor = 47,
        //    PROFESIONAL_DICENO_CVT = 48,
        //    t301_fechano_cvt = 49,
        //    t301_motivono_cvt = 50
        }

        internal ProyectoSubNodoSD(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
        //#endregion
	
        //#region funciones publicas
        ///// <summary>
        ///// Inserta un ProyectoSubNodoSD
        ///// </summary>
        //internal int Insert(Models.ProyectoSubNodoSD oProyectoSubNodoSD)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[50] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oProyectoSubNodoSD.t305_idproyectosubnodo),
        //            Param(enumDBFields.t301_idproyecto, oProyectoSubNodoSD.t301_idproyecto),
        //            Param(enumDBFields.t304_idsubnodo, oProyectoSubNodoSD.t304_idsubnodo),
        //            Param(enumDBFields.t305_finalizado, oProyectoSubNodoSD.t305_finalizado),
        //            Param(enumDBFields.t305_cualidad, oProyectoSubNodoSD.t305_cualidad),
        //            Param(enumDBFields.t305_heredanodo, oProyectoSubNodoSD.t305_heredanodo),
        //            Param(enumDBFields.t303_idnodo, oProyectoSubNodoSD.t303_idnodo),
        //            Param(enumDBFields.t303_denominacion, oProyectoSubNodoSD.t303_denominacion),
        //            Param(enumDBFields.t304_denominacion, oProyectoSubNodoSD.t304_denominacion),
        //            Param(enumDBFields.t303_ultcierreeco, oProyectoSubNodoSD.t303_ultcierreeco),
        //            Param(enumDBFields.t314_idusuario_responsable, oProyectoSubNodoSD.t314_idusuario_responsable),
        //            Param(enumDBFields.Responsable, oProyectoSubNodoSD.Responsable),
        //            Param(enumDBFields.t305_seudonimo, oProyectoSubNodoSD.t305_seudonimo),
        //            Param(enumDBFields.t305_accesobitacora_iap, oProyectoSubNodoSD.t305_accesobitacora_iap),
        //            Param(enumDBFields.t305_accesobitacora_pst, oProyectoSubNodoSD.t305_accesobitacora_pst),
        //            Param(enumDBFields.t305_imputablegasvi, oProyectoSubNodoSD.t305_imputablegasvi),
        //            Param(enumDBFields.t305_admiterecursospst, oProyectoSubNodoSD.t305_admiterecursospst),
        //            Param(enumDBFields.t305_avisoresponsablepst, oProyectoSubNodoSD.t305_avisoresponsablepst),
        //            Param(enumDBFields.t305_avisorecursopst, oProyectoSubNodoSD.t305_avisorecursopst),
        //            Param(enumDBFields.t305_avisofigura, oProyectoSubNodoSD.t305_avisofigura),
        //            Param(enumDBFields.t305_modificaciones, oProyectoSubNodoSD.t305_modificaciones),
        //            Param(enumDBFields.t305_observaciones, oProyectoSubNodoSD.t305_observaciones),
        //            Param(enumDBFields.t320_facturable, oProyectoSubNodoSD.t320_facturable),
        //            Param(enumDBFields.mesesCerrados, oProyectoSubNodoSD.mesesCerrados),
        //            Param(enumDBFields.t001_ficepi_visador, oProyectoSubNodoSD.t001_ficepi_visador),
        //            Param(enumDBFields.Visador, oProyectoSubNodoSD.Visador),
        //            Param(enumDBFields.t305_supervisor_visador, oProyectoSubNodoSD.t305_supervisor_visador),
        //            Param(enumDBFields.t476_idcnp, oProyectoSubNodoSD.t476_idcnp),
        //            Param(enumDBFields.t485_idcsn1p, oProyectoSubNodoSD.t485_idcsn1p),
        //            Param(enumDBFields.t487_idcsn2p, oProyectoSubNodoSD.t487_idcsn2p),
        //            Param(enumDBFields.t489_idcsn3p, oProyectoSubNodoSD.t489_idcsn3p),
        //            Param(enumDBFields.t491_idcsn4p, oProyectoSubNodoSD.t491_idcsn4p),
        //            Param(enumDBFields.t305_observacionesadm, oProyectoSubNodoSD.t305_observacionesadm),
        //            Param(enumDBFields.t305_importaciongasvi, oProyectoSubNodoSD.t305_importaciongasvi),
        //            Param(enumDBFields.t391_denominacion, oProyectoSubNodoSD.t391_denominacion),
        //            Param(enumDBFields.t392_denominacion, oProyectoSubNodoSD.t392_denominacion),
        //            Param(enumDBFields.t393_denominacion, oProyectoSubNodoSD.t393_denominacion),
        //            Param(enumDBFields.t394_denominacion, oProyectoSubNodoSD.t394_denominacion),
        //            Param(enumDBFields.t301_categoria, oProyectoSubNodoSD.t301_categoria),
        //            Param(enumDBFields.t422_idmoneda, oProyectoSubNodoSD.t422_idmoneda),
        //            Param(enumDBFields.t422_denominacion, oProyectoSubNodoSD.t422_denominacion),
        //            Param(enumDBFields.t422_denominacionimportes, oProyectoSubNodoSD.t422_denominacionimportes),
        //            Param(enumDBFields.t305_opd, oProyectoSubNodoSD.t305_opd),
        //            Param(enumDBFields.t001_idficepi_visadorcv, oProyectoSubNodoSD.t001_idficepi_visadorcv),
        //            Param(enumDBFields.VisadorCV, oProyectoSubNodoSD.VisadorCV),
        //            Param(enumDBFields.t001_idficepi_interlocutor, oProyectoSubNodoSD.t001_idficepi_interlocutor),
        //            Param(enumDBFields.Interlocutor, oProyectoSubNodoSD.Interlocutor),
        //            Param(enumDBFields.PROFESIONAL_DICENO_CVT, oProyectoSubNodoSD.PROFESIONAL_DICENO_CVT),
        //            Param(enumDBFields.t301_fechano_cvt, oProyectoSubNodoSD.t301_fechano_cvt),
        //            Param(enumDBFields.t301_motivono_cvt, oProyectoSubNodoSD.t301_motivono_cvt)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_ProyectoSubNodoSD_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un ProyectoSubNodoSD a partir del id
        ///// </summary>
        internal Models.ProyectoSubNodoSD Select(int t305_idproyectosubnodo)
        {
            Models.ProyectoSubNodoSD oProyectoSubNodoSD = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t305_idproyectosubnodo, t305_idproyectosubnodo)
                };

                dr = cDblib.DataReader("SUP_PROYECTOSUBNODO_SD", dbparams);
                if (dr.Read())
                {
                    oProyectoSubNodoSD = new Models.ProyectoSubNodoSD();
                    oProyectoSubNodoSD.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oProyectoSubNodoSD.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oProyectoSubNodoSD.t304_idsubnodo = Convert.ToInt32(dr["t304_idsubnodo"]);
                    oProyectoSubNodoSD.t305_finalizado = Convert.ToBoolean(dr["t305_finalizado"]);
                    oProyectoSubNodoSD.t305_cualidad = Convert.ToString(dr["t305_cualidad"]);
                    oProyectoSubNodoSD.t305_heredanodo = Convert.ToBoolean(dr["t305_heredanodo"]);
                    oProyectoSubNodoSD.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                    oProyectoSubNodoSD.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                    oProyectoSubNodoSD.t304_denominacion = Convert.ToString(dr["t304_denominacion"]);
                    oProyectoSubNodoSD.t303_ultcierreeco = Convert.ToInt32(dr["t303_ultcierreeco"]);
                    oProyectoSubNodoSD.t314_idusuario_responsable = Convert.ToInt32(dr["t314_idusuario_responsable"]);
                    if (!Convert.IsDBNull(dr["Responsable"]))
                        oProyectoSubNodoSD.Responsable = Convert.ToString(dr["Responsable"]);
                    oProyectoSubNodoSD.t305_seudonimo = Convert.ToString(dr["t305_seudonimo"]);
                    oProyectoSubNodoSD.t305_accesobitacora_iap = Convert.ToString(dr["t305_accesobitacora_iap"]);
                    oProyectoSubNodoSD.t305_accesobitacora_pst = Convert.ToString(dr["t305_accesobitacora_pst"]);
                    oProyectoSubNodoSD.t305_imputablegasvi = Convert.ToBoolean(dr["t305_imputablegasvi"]);
                    oProyectoSubNodoSD.t305_admiterecursospst = Convert.ToBoolean(dr["t305_admiterecursospst"]);
                    oProyectoSubNodoSD.t305_avisoresponsablepst = Convert.ToBoolean(dr["t305_avisoresponsablepst"]);
                    oProyectoSubNodoSD.t305_avisorecursopst = Convert.ToBoolean(dr["t305_avisorecursopst"]);
                    oProyectoSubNodoSD.t305_avisofigura = Convert.ToBoolean(dr["t305_avisofigura"]);
                    oProyectoSubNodoSD.t305_modificaciones = Convert.ToString(dr["t305_modificaciones"]);
                    oProyectoSubNodoSD.t305_observaciones = Convert.ToString(dr["t305_observaciones"]);
                    oProyectoSubNodoSD.t320_facturable = Convert.ToBoolean(dr["t320_facturable"]);
                    if (!Convert.IsDBNull(dr["mesesCerrados"]))
                        oProyectoSubNodoSD.mesesCerrados = Convert.ToInt32(dr["mesesCerrados"]);
                    if (!Convert.IsDBNull(dr["t001_ficepi_visador"]))
                        oProyectoSubNodoSD.t001_ficepi_visador = Convert.ToInt32(dr["t001_ficepi_visador"]);
                    if (!Convert.IsDBNull(dr["Visador"]))
                        oProyectoSubNodoSD.Visador = Convert.ToString(dr["Visador"]);
                    oProyectoSubNodoSD.t305_supervisor_visador = Convert.ToBoolean(dr["t305_supervisor_visador"]);
                    if (!Convert.IsDBNull(dr["t476_idcnp"]))
                        oProyectoSubNodoSD.t476_idcnp = Convert.ToInt32(dr["t476_idcnp"]);
                    if (!Convert.IsDBNull(dr["t485_idcsn1p"]))
                        oProyectoSubNodoSD.t485_idcsn1p = Convert.ToInt32(dr["t485_idcsn1p"]);
                    if (!Convert.IsDBNull(dr["t487_idcsn2p"]))
                        oProyectoSubNodoSD.t487_idcsn2p = Convert.ToInt32(dr["t487_idcsn2p"]);
                    if (!Convert.IsDBNull(dr["t489_idcsn3p"]))
                        oProyectoSubNodoSD.t489_idcsn3p = Convert.ToInt32(dr["t489_idcsn3p"]);
                    if (!Convert.IsDBNull(dr["t491_idcsn4p"]))
                        oProyectoSubNodoSD.t491_idcsn4p = Convert.ToInt32(dr["t491_idcsn4p"]);
                    oProyectoSubNodoSD.t305_observacionesadm = Convert.ToString(dr["t305_observacionesadm"]);
                    oProyectoSubNodoSD.t305_importaciongasvi = Convert.ToByte(dr["t305_importaciongasvi"]);
                    oProyectoSubNodoSD.t391_denominacion = Convert.ToString(dr["t391_denominacion"]);
                    oProyectoSubNodoSD.t392_denominacion = Convert.ToString(dr["t392_denominacion"]);
                    oProyectoSubNodoSD.t393_denominacion = Convert.ToString(dr["t393_denominacion"]);
                    oProyectoSubNodoSD.t394_denominacion = Convert.ToString(dr["t394_denominacion"]);
                    oProyectoSubNodoSD.t301_categoria = Convert.ToString(dr["t301_categoria"]);
                    oProyectoSubNodoSD.t422_idmoneda = Convert.ToString(dr["t422_idmoneda"]);
                    oProyectoSubNodoSD.t422_denominacion = Convert.ToString(dr["t422_denominacion"]);
                    oProyectoSubNodoSD.t422_denominacionimportes = Convert.ToString(dr["t422_denominacionimportes"]);
                    oProyectoSubNodoSD.t305_opd = Convert.ToBoolean(dr["t305_opd"]);
                    if (!Convert.IsDBNull(dr["t001_idficepi_visadorcv"]))
                        oProyectoSubNodoSD.t001_idficepi_visadorcv = Convert.ToInt32(dr["t001_idficepi_visadorcv"]);
                    if (!Convert.IsDBNull(dr["VisadorCV"]))
                        oProyectoSubNodoSD.VisadorCV = Convert.ToString(dr["VisadorCV"]);
                    if (!Convert.IsDBNull(dr["t001_idficepi_interlocutor"]))
                        oProyectoSubNodoSD.t001_idficepi_interlocutor = Convert.ToInt32(dr["t001_idficepi_interlocutor"]);
                    if (!Convert.IsDBNull(dr["Interlocutor"]))
                        oProyectoSubNodoSD.Interlocutor = Convert.ToString(dr["Interlocutor"]);
                    if (!Convert.IsDBNull(dr["PROFESIONAL_DICENO_CVT"]))
                        oProyectoSubNodoSD.PROFESIONAL_DICENO_CVT = Convert.ToString(dr["PROFESIONAL_DICENO_CVT"]);
                    if (!Convert.IsDBNull(dr["t301_fechano_cvt"]))
                        oProyectoSubNodoSD.t301_fechano_cvt = Convert.ToDateTime(dr["t301_fechano_cvt"]);
                    oProyectoSubNodoSD.t301_motivono_cvt = Convert.ToString(dr["t301_motivono_cvt"]);

                }
                return oProyectoSubNodoSD;

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
        ///// Actualiza un ProyectoSubNodoSD a partir del id
        ///// </summary>
        //internal int Update(Models.ProyectoSubNodoSD oProyectoSubNodoSD)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[50] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oProyectoSubNodoSD.t305_idproyectosubnodo),
        //            Param(enumDBFields.t301_idproyecto, oProyectoSubNodoSD.t301_idproyecto),
        //            Param(enumDBFields.t304_idsubnodo, oProyectoSubNodoSD.t304_idsubnodo),
        //            Param(enumDBFields.t305_finalizado, oProyectoSubNodoSD.t305_finalizado),
        //            Param(enumDBFields.t305_cualidad, oProyectoSubNodoSD.t305_cualidad),
        //            Param(enumDBFields.t305_heredanodo, oProyectoSubNodoSD.t305_heredanodo),
        //            Param(enumDBFields.t303_idnodo, oProyectoSubNodoSD.t303_idnodo),
        //            Param(enumDBFields.t303_denominacion, oProyectoSubNodoSD.t303_denominacion),
        //            Param(enumDBFields.t304_denominacion, oProyectoSubNodoSD.t304_denominacion),
        //            Param(enumDBFields.t303_ultcierreeco, oProyectoSubNodoSD.t303_ultcierreeco),
        //            Param(enumDBFields.t314_idusuario_responsable, oProyectoSubNodoSD.t314_idusuario_responsable),
        //            Param(enumDBFields.Responsable, oProyectoSubNodoSD.Responsable),
        //            Param(enumDBFields.t305_seudonimo, oProyectoSubNodoSD.t305_seudonimo),
        //            Param(enumDBFields.t305_accesobitacora_iap, oProyectoSubNodoSD.t305_accesobitacora_iap),
        //            Param(enumDBFields.t305_accesobitacora_pst, oProyectoSubNodoSD.t305_accesobitacora_pst),
        //            Param(enumDBFields.t305_imputablegasvi, oProyectoSubNodoSD.t305_imputablegasvi),
        //            Param(enumDBFields.t305_admiterecursospst, oProyectoSubNodoSD.t305_admiterecursospst),
        //            Param(enumDBFields.t305_avisoresponsablepst, oProyectoSubNodoSD.t305_avisoresponsablepst),
        //            Param(enumDBFields.t305_avisorecursopst, oProyectoSubNodoSD.t305_avisorecursopst),
        //            Param(enumDBFields.t305_avisofigura, oProyectoSubNodoSD.t305_avisofigura),
        //            Param(enumDBFields.t305_modificaciones, oProyectoSubNodoSD.t305_modificaciones),
        //            Param(enumDBFields.t305_observaciones, oProyectoSubNodoSD.t305_observaciones),
        //            Param(enumDBFields.t320_facturable, oProyectoSubNodoSD.t320_facturable),
        //            Param(enumDBFields.mesesCerrados, oProyectoSubNodoSD.mesesCerrados),
        //            Param(enumDBFields.t001_ficepi_visador, oProyectoSubNodoSD.t001_ficepi_visador),
        //            Param(enumDBFields.Visador, oProyectoSubNodoSD.Visador),
        //            Param(enumDBFields.t305_supervisor_visador, oProyectoSubNodoSD.t305_supervisor_visador),
        //            Param(enumDBFields.t476_idcnp, oProyectoSubNodoSD.t476_idcnp),
        //            Param(enumDBFields.t485_idcsn1p, oProyectoSubNodoSD.t485_idcsn1p),
        //            Param(enumDBFields.t487_idcsn2p, oProyectoSubNodoSD.t487_idcsn2p),
        //            Param(enumDBFields.t489_idcsn3p, oProyectoSubNodoSD.t489_idcsn3p),
        //            Param(enumDBFields.t491_idcsn4p, oProyectoSubNodoSD.t491_idcsn4p),
        //            Param(enumDBFields.t305_observacionesadm, oProyectoSubNodoSD.t305_observacionesadm),
        //            Param(enumDBFields.t305_importaciongasvi, oProyectoSubNodoSD.t305_importaciongasvi),
        //            Param(enumDBFields.t391_denominacion, oProyectoSubNodoSD.t391_denominacion),
        //            Param(enumDBFields.t392_denominacion, oProyectoSubNodoSD.t392_denominacion),
        //            Param(enumDBFields.t393_denominacion, oProyectoSubNodoSD.t393_denominacion),
        //            Param(enumDBFields.t394_denominacion, oProyectoSubNodoSD.t394_denominacion),
        //            Param(enumDBFields.t301_categoria, oProyectoSubNodoSD.t301_categoria),
        //            Param(enumDBFields.t422_idmoneda, oProyectoSubNodoSD.t422_idmoneda),
        //            Param(enumDBFields.t422_denominacion, oProyectoSubNodoSD.t422_denominacion),
        //            Param(enumDBFields.t422_denominacionimportes, oProyectoSubNodoSD.t422_denominacionimportes),
        //            Param(enumDBFields.t305_opd, oProyectoSubNodoSD.t305_opd),
        //            Param(enumDBFields.t001_idficepi_visadorcv, oProyectoSubNodoSD.t001_idficepi_visadorcv),
        //            Param(enumDBFields.VisadorCV, oProyectoSubNodoSD.VisadorCV),
        //            Param(enumDBFields.t001_idficepi_interlocutor, oProyectoSubNodoSD.t001_idficepi_interlocutor),
        //            Param(enumDBFields.Interlocutor, oProyectoSubNodoSD.Interlocutor),
        //            Param(enumDBFields.PROFESIONAL_DICENO_CVT, oProyectoSubNodoSD.PROFESIONAL_DICENO_CVT),
        //            Param(enumDBFields.t301_fechano_cvt, oProyectoSubNodoSD.t301_fechano_cvt),
        //            Param(enumDBFields.t301_motivono_cvt, oProyectoSubNodoSD.t301_motivono_cvt)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_ProyectoSubNodoSD_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un ProyectoSubNodoSD a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_ProyectoSubNodoSD_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los ProyectoSubNodoSD
        ///// </summary>
        //internal List<Models.ProyectoSubNodoSD> Catalogo(Models.ProyectoSubNodoSD oProyectoSubNodoSDFilter)
        //{
        //    Models.ProyectoSubNodoSD oProyectoSubNodoSD = null;
        //    List<Models.ProyectoSubNodoSD> lst = new List<Models.ProyectoSubNodoSD>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[50] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oTEMP_ProyectoSubNodoSDFilter.t305_idproyectosubnodo),
        //            Param(enumDBFields.t301_idproyecto, oTEMP_ProyectoSubNodoSDFilter.t301_idproyecto),
        //            Param(enumDBFields.t304_idsubnodo, oTEMP_ProyectoSubNodoSDFilter.t304_idsubnodo),
        //            Param(enumDBFields.t305_finalizado, oTEMP_ProyectoSubNodoSDFilter.t305_finalizado),
        //            Param(enumDBFields.t305_cualidad, oTEMP_ProyectoSubNodoSDFilter.t305_cualidad),
        //            Param(enumDBFields.t305_heredanodo, oTEMP_ProyectoSubNodoSDFilter.t305_heredanodo),
        //            Param(enumDBFields.t303_idnodo, oTEMP_ProyectoSubNodoSDFilter.t303_idnodo),
        //            Param(enumDBFields.t303_denominacion, oTEMP_ProyectoSubNodoSDFilter.t303_denominacion),
        //            Param(enumDBFields.t304_denominacion, oTEMP_ProyectoSubNodoSDFilter.t304_denominacion),
        //            Param(enumDBFields.t303_ultcierreeco, oTEMP_ProyectoSubNodoSDFilter.t303_ultcierreeco),
        //            Param(enumDBFields.t314_idusuario_responsable, oTEMP_ProyectoSubNodoSDFilter.t314_idusuario_responsable),
        //            Param(enumDBFields.Responsable, oTEMP_ProyectoSubNodoSDFilter.Responsable),
        //            Param(enumDBFields.t305_seudonimo, oTEMP_ProyectoSubNodoSDFilter.t305_seudonimo),
        //            Param(enumDBFields.t305_accesobitacora_iap, oTEMP_ProyectoSubNodoSDFilter.t305_accesobitacora_iap),
        //            Param(enumDBFields.t305_accesobitacora_pst, oTEMP_ProyectoSubNodoSDFilter.t305_accesobitacora_pst),
        //            Param(enumDBFields.t305_imputablegasvi, oTEMP_ProyectoSubNodoSDFilter.t305_imputablegasvi),
        //            Param(enumDBFields.t305_admiterecursospst, oTEMP_ProyectoSubNodoSDFilter.t305_admiterecursospst),
        //            Param(enumDBFields.t305_avisoresponsablepst, oTEMP_ProyectoSubNodoSDFilter.t305_avisoresponsablepst),
        //            Param(enumDBFields.t305_avisorecursopst, oTEMP_ProyectoSubNodoSDFilter.t305_avisorecursopst),
        //            Param(enumDBFields.t305_avisofigura, oTEMP_ProyectoSubNodoSDFilter.t305_avisofigura),
        //            Param(enumDBFields.t305_modificaciones, oTEMP_ProyectoSubNodoSDFilter.t305_modificaciones),
        //            Param(enumDBFields.t305_observaciones, oTEMP_ProyectoSubNodoSDFilter.t305_observaciones),
        //            Param(enumDBFields.t320_facturable, oTEMP_ProyectoSubNodoSDFilter.t320_facturable),
        //            Param(enumDBFields.mesesCerrados, oTEMP_ProyectoSubNodoSDFilter.mesesCerrados),
        //            Param(enumDBFields.t001_ficepi_visador, oTEMP_ProyectoSubNodoSDFilter.t001_ficepi_visador),
        //            Param(enumDBFields.Visador, oTEMP_ProyectoSubNodoSDFilter.Visador),
        //            Param(enumDBFields.t305_supervisor_visador, oTEMP_ProyectoSubNodoSDFilter.t305_supervisor_visador),
        //            Param(enumDBFields.t476_idcnp, oTEMP_ProyectoSubNodoSDFilter.t476_idcnp),
        //            Param(enumDBFields.t485_idcsn1p, oTEMP_ProyectoSubNodoSDFilter.t485_idcsn1p),
        //            Param(enumDBFields.t487_idcsn2p, oTEMP_ProyectoSubNodoSDFilter.t487_idcsn2p),
        //            Param(enumDBFields.t489_idcsn3p, oTEMP_ProyectoSubNodoSDFilter.t489_idcsn3p),
        //            Param(enumDBFields.t491_idcsn4p, oTEMP_ProyectoSubNodoSDFilter.t491_idcsn4p),
        //            Param(enumDBFields.t305_observacionesadm, oTEMP_ProyectoSubNodoSDFilter.t305_observacionesadm),
        //            Param(enumDBFields.t305_importaciongasvi, oTEMP_ProyectoSubNodoSDFilter.t305_importaciongasvi),
        //            Param(enumDBFields.t391_denominacion, oTEMP_ProyectoSubNodoSDFilter.t391_denominacion),
        //            Param(enumDBFields.t392_denominacion, oTEMP_ProyectoSubNodoSDFilter.t392_denominacion),
        //            Param(enumDBFields.t393_denominacion, oTEMP_ProyectoSubNodoSDFilter.t393_denominacion),
        //            Param(enumDBFields.t394_denominacion, oTEMP_ProyectoSubNodoSDFilter.t394_denominacion),
        //            Param(enumDBFields.t301_categoria, oTEMP_ProyectoSubNodoSDFilter.t301_categoria),
        //            Param(enumDBFields.t422_idmoneda, oTEMP_ProyectoSubNodoSDFilter.t422_idmoneda),
        //            Param(enumDBFields.t422_denominacion, oTEMP_ProyectoSubNodoSDFilter.t422_denominacion),
        //            Param(enumDBFields.t422_denominacionimportes, oTEMP_ProyectoSubNodoSDFilter.t422_denominacionimportes),
        //            Param(enumDBFields.t305_opd, oTEMP_ProyectoSubNodoSDFilter.t305_opd),
        //            Param(enumDBFields.t001_idficepi_visadorcv, oTEMP_ProyectoSubNodoSDFilter.t001_idficepi_visadorcv),
        //            Param(enumDBFields.VisadorCV, oTEMP_ProyectoSubNodoSDFilter.VisadorCV),
        //            Param(enumDBFields.t001_idficepi_interlocutor, oTEMP_ProyectoSubNodoSDFilter.t001_idficepi_interlocutor),
        //            Param(enumDBFields.Interlocutor, oTEMP_ProyectoSubNodoSDFilter.Interlocutor),
        //            Param(enumDBFields.PROFESIONAL_DICENO_CVT, oTEMP_ProyectoSubNodoSDFilter.PROFESIONAL_DICENO_CVT),
        //            Param(enumDBFields.t301_fechano_cvt, oTEMP_ProyectoSubNodoSDFilter.t301_fechano_cvt),
        //            Param(enumDBFields.t301_motivono_cvt, oTEMP_ProyectoSubNodoSDFilter.t301_motivono_cvt)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_ProyectoSubNodoSD_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oProyectoSubNodoSD = new Models.ProyectoSubNodoSD();
        //            oProyectoSubNodoSD.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oProyectoSubNodoSD.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);
        //            oProyectoSubNodoSD.t304_idsubnodo=Convert.ToInt32(dr["t304_idsubnodo"]);
        //            oProyectoSubNodoSD.t305_finalizado=Convert.ToBoolean(dr["t305_finalizado"]);
        //            oProyectoSubNodoSD.t305_cualidad=Convert.ToString(dr["t305_cualidad"]);
        //            oProyectoSubNodoSD.t305_heredanodo=Convert.ToBoolean(dr["t305_heredanodo"]);
        //            oProyectoSubNodoSD.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
        //            oProyectoSubNodoSD.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
        //            oProyectoSubNodoSD.t304_denominacion=Convert.ToString(dr["t304_denominacion"]);
        //            oProyectoSubNodoSD.t303_ultcierreeco=Convert.ToInt32(dr["t303_ultcierreeco"]);
        //            oProyectoSubNodoSD.t314_idusuario_responsable=Convert.ToInt32(dr["t314_idusuario_responsable"]);
        //            if(!Convert.IsDBNull(dr["Responsable"]))
        //                oProyectoSubNodoSD.Responsable=Convert.ToString(dr["Responsable"]);
        //            oProyectoSubNodoSD.t305_seudonimo=Convert.ToString(dr["t305_seudonimo"]);
        //            oProyectoSubNodoSD.t305_accesobitacora_iap=Convert.ToString(dr["t305_accesobitacora_iap"]);
        //            oProyectoSubNodoSD.t305_accesobitacora_pst=Convert.ToString(dr["t305_accesobitacora_pst"]);
        //            oProyectoSubNodoSD.t305_imputablegasvi=Convert.ToBoolean(dr["t305_imputablegasvi"]);
        //            oProyectoSubNodoSD.t305_admiterecursospst=Convert.ToBoolean(dr["t305_admiterecursospst"]);
        //            oProyectoSubNodoSD.t305_avisoresponsablepst=Convert.ToBoolean(dr["t305_avisoresponsablepst"]);
        //            oProyectoSubNodoSD.t305_avisorecursopst=Convert.ToBoolean(dr["t305_avisorecursopst"]);
        //            oProyectoSubNodoSD.t305_avisofigura=Convert.ToBoolean(dr["t305_avisofigura"]);
        //            oProyectoSubNodoSD.t305_modificaciones=Convert.ToString(dr["t305_modificaciones"]);
        //            oProyectoSubNodoSD.t305_observaciones=Convert.ToString(dr["t305_observaciones"]);
        //            oProyectoSubNodoSD.t320_facturable=Convert.ToBoolean(dr["t320_facturable"]);
        //            if(!Convert.IsDBNull(dr["mesesCerrados"]))
        //                oProyectoSubNodoSD.mesesCerrados=Convert.ToInt32(dr["mesesCerrados"]);
        //            if(!Convert.IsDBNull(dr["t001_ficepi_visador"]))
        //                oProyectoSubNodoSD.t001_ficepi_visador=Convert.ToInt32(dr["t001_ficepi_visador"]);
        //            if(!Convert.IsDBNull(dr["Visador"]))
        //                oProyectoSubNodoSD.Visador=Convert.ToString(dr["Visador"]);
        //            oProyectoSubNodoSD.t305_supervisor_visador=Convert.ToBoolean(dr["t305_supervisor_visador"]);
        //            if(!Convert.IsDBNull(dr["t476_idcnp"]))
        //                oProyectoSubNodoSD.t476_idcnp=Convert.ToInt32(dr["t476_idcnp"]);
        //            if(!Convert.IsDBNull(dr["t485_idcsn1p"]))
        //                oProyectoSubNodoSD.t485_idcsn1p=Convert.ToInt32(dr["t485_idcsn1p"]);
        //            if(!Convert.IsDBNull(dr["t487_idcsn2p"]))
        //                oProyectoSubNodoSD.t487_idcsn2p=Convert.ToInt32(dr["t487_idcsn2p"]);
        //            if(!Convert.IsDBNull(dr["t489_idcsn3p"]))
        //                oProyectoSubNodoSD.t489_idcsn3p=Convert.ToInt32(dr["t489_idcsn3p"]);
        //            if(!Convert.IsDBNull(dr["t491_idcsn4p"]))
        //                oProyectoSubNodoSD.t491_idcsn4p=Convert.ToInt32(dr["t491_idcsn4p"]);
        //            oProyectoSubNodoSD.t305_observacionesadm=Convert.ToString(dr["t305_observacionesadm"]);
        //            oProyectoSubNodoSD.t305_importaciongasvi=Convert.ToByte(dr["t305_importaciongasvi"]);
        //            oProyectoSubNodoSD.t391_denominacion=Convert.ToString(dr["t391_denominacion"]);
        //            oProyectoSubNodoSD.t392_denominacion=Convert.ToString(dr["t392_denominacion"]);
        //            oProyectoSubNodoSD.t393_denominacion=Convert.ToString(dr["t393_denominacion"]);
        //            oProyectoSubNodoSD.t394_denominacion=Convert.ToString(dr["t394_denominacion"]);
        //            oProyectoSubNodoSD.t301_categoria=Convert.ToString(dr["t301_categoria"]);
        //            oProyectoSubNodoSD.t422_idmoneda=Convert.ToString(dr["t422_idmoneda"]);
        //            oProyectoSubNodoSD.t422_denominacion=Convert.ToString(dr["t422_denominacion"]);
        //            oProyectoSubNodoSD.t422_denominacionimportes=Convert.ToString(dr["t422_denominacionimportes"]);
        //            oProyectoSubNodoSD.t305_opd=Convert.ToBoolean(dr["t305_opd"]);
        //            if(!Convert.IsDBNull(dr["t001_idficepi_visadorcv"]))
        //                oProyectoSubNodoSD.t001_idficepi_visadorcv=Convert.ToInt32(dr["t001_idficepi_visadorcv"]);
        //            if(!Convert.IsDBNull(dr["VisadorCV"]))
        //                oProyectoSubNodoSD.VisadorCV=Convert.ToString(dr["VisadorCV"]);
        //            if(!Convert.IsDBNull(dr["t001_idficepi_interlocutor"]))
        //                oProyectoSubNodoSD.t001_idficepi_interlocutor=Convert.ToInt32(dr["t001_idficepi_interlocutor"]);
        //            if(!Convert.IsDBNull(dr["Interlocutor"]))
        //                oProyectoSubNodoSD.Interlocutor=Convert.ToString(dr["Interlocutor"]);
        //            if(!Convert.IsDBNull(dr["PROFESIONAL_DICENO_CVT"]))
        //                oProyectoSubNodoSD.PROFESIONAL_DICENO_CVT=Convert.ToString(dr["PROFESIONAL_DICENO_CVT"]);
        //            if(!Convert.IsDBNull(dr["t301_fechano_cvt"]))
        //                oProyectoSubNodoSD.t301_fechano_cvt=Convert.ToDateTime(dr["t301_fechano_cvt"]);
        //            oProyectoSubNodoSD.t301_motivono_cvt=Convert.ToString(dr["t301_motivono_cvt"]);

        //            lst.Add(oProyectoSubNodoSD);

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
		
        //#region funciones privadas
        private SqlParameter Param(enumDBFields dbField, object value)
        {
            SqlParameter dbParam = null;
            string paramName = null;
            SqlDbType paramType = default(SqlDbType);
            int paramSize = 0;
            ParameterDirection paramDirection = ParameterDirection.Input;
			
            switch (dbField)
            {
                case enumDBFields.t305_idproyectosubnodo:
                    paramName = "@t305_idproyectosubnodo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
        //        case enumDBFields.t301_idproyecto:
        //            paramName = "@t301_idproyecto";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.t304_idsubnodo:
        //            paramName = "@t304_idsubnodo";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.t305_finalizado:
        //            paramName = "@t305_finalizado";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.t305_cualidad:
        //            paramName = "@t305_cualidad";
        //            paramType = SqlDbType.Char;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.t305_heredanodo:
        //            paramName = "@t305_heredanodo";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.t303_idnodo:
        //            paramName = "@t303_idnodo";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.t303_denominacion:
        //            paramName = "@t303_denominacion";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 50;
        //            break;
        //        case enumDBFields.t304_denominacion:
        //            paramName = "@t304_denominacion";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 50;
        //            break;
        //        case enumDBFields.t303_ultcierreeco:
        //            paramName = "@t303_ultcierreeco";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.t314_idusuario_responsable:
        //            paramName = "@t314_idusuario_responsable";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.Responsable:
        //            paramName = "@Responsable";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 150;
        //            break;
        //        case enumDBFields.t305_seudonimo:
        //            paramName = "@t305_seudonimo";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 70;
        //            break;
        //        case enumDBFields.t305_accesobitacora_iap:
        //            paramName = "@t305_accesobitacora_iap";
        //            paramType = SqlDbType.Char;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.t305_accesobitacora_pst:
        //            paramName = "@t305_accesobitacora_pst";
        //            paramType = SqlDbType.Char;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.t305_imputablegasvi:
        //            paramName = "@t305_imputablegasvi";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.t305_admiterecursospst:
        //            paramName = "@t305_admiterecursospst";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.t305_avisoresponsablepst:
        //            paramName = "@t305_avisoresponsablepst";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.t305_avisorecursopst:
        //            paramName = "@t305_avisorecursopst";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.t305_avisofigura:
        //            paramName = "@t305_avisofigura";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.t305_modificaciones:
        //            paramName = "@t305_modificaciones";
        //            paramType = SqlDbType.Text;
        //            paramSize = 2147483647;
        //            break;
        //        case enumDBFields.t305_observaciones:
        //            paramName = "@t305_observaciones";
        //            paramType = SqlDbType.Text;
        //            paramSize = 2147483647;
        //            break;
        //        case enumDBFields.t320_facturable:
        //            paramName = "@t320_facturable";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.mesesCerrados:
        //            paramName = "@mesesCerrados";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.t001_ficepi_visador:
        //            paramName = "@t001_ficepi_visador";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.Visador:
        //            paramName = "@Visador";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 150;
        //            break;
        //        case enumDBFields.t305_supervisor_visador:
        //            paramName = "@t305_supervisor_visador";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.t476_idcnp:
        //            paramName = "@t476_idcnp";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.t485_idcsn1p:
        //            paramName = "@t485_idcsn1p";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.t487_idcsn2p:
        //            paramName = "@t487_idcsn2p";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.t489_idcsn3p:
        //            paramName = "@t489_idcsn3p";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.t491_idcsn4p:
        //            paramName = "@t491_idcsn4p";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.t305_observacionesadm:
        //            paramName = "@t305_observacionesadm";
        //            paramType = SqlDbType.Text;
        //            paramSize = 2147483647;
        //            break;
        //        case enumDBFields.t305_importaciongasvi:
        //            paramName = "@t305_importaciongasvi";
        //            paramType = SqlDbType.TinyInt;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.t391_denominacion:
        //            paramName = "@t391_denominacion";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 50;
        //            break;
        //        case enumDBFields.t392_denominacion:
        //            paramName = "@t392_denominacion";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 50;
        //            break;
        //        case enumDBFields.t393_denominacion:
        //            paramName = "@t393_denominacion";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 50;
        //            break;
        //        case enumDBFields.t394_denominacion:
        //            paramName = "@t394_denominacion";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 50;
        //            break;
        //        case enumDBFields.t301_categoria:
        //            paramName = "@t301_categoria";
        //            paramType = SqlDbType.Char;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.t422_idmoneda:
        //            paramName = "@t422_idmoneda";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 5;
        //            break;
        //        case enumDBFields.t422_denominacion:
        //            paramName = "@t422_denominacion";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 50;
        //            break;
        //        case enumDBFields.t422_denominacionimportes:
        //            paramName = "@t422_denominacionimportes";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 50;
        //            break;
        //        case enumDBFields.t305_opd:
        //            paramName = "@t305_opd";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.t001_idficepi_visadorcv:
        //            paramName = "@t001_idficepi_visadorcv";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.VisadorCV:
        //            paramName = "@VisadorCV";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 150;
        //            break;
        //        case enumDBFields.t001_idficepi_interlocutor:
        //            paramName = "@t001_idficepi_interlocutor";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.Interlocutor:
        //            paramName = "@Interlocutor";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 150;
        //            break;
        //        case enumDBFields.PROFESIONAL_DICENO_CVT:
        //            paramName = "@PROFESIONAL_DICENO_CVT";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 150;
        //            break;
        //        case enumDBFields.t301_fechano_cvt:
        //            paramName = "@t301_fechano_cvt";
        //            paramType = SqlDbType.DateTime;
        //            paramSize = 8;
        //            break;
        //        case enumDBFields.t301_motivono_cvt:
        //            paramName = "@t301_motivono_cvt";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 250;
        //            break;
            }


            dbParam = cDblib.dbParameter(paramName, paramType, paramSize);
            dbParam.Direction = paramDirection;
            if (paramDirection == ParameterDirection.Input | paramDirection == ParameterDirection.InputOutput) dbParam.Value = value;

            return dbParam;

        }
		
        //#endregion
    
    }

}
