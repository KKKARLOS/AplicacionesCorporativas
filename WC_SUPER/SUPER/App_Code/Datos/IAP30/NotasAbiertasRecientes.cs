using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for NotasAbiertasRecientes
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class NotasAbiertasRecientes 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t420_idreferencia = 1,
			nota_aparcada = 2,
			TipoNota = 3,
			t431_idestado = 4,
			t431_denominacion = 5,
			t420_fTramitada = 6,
			t314_idusuario_interesado = 7,
			t001_sexo_interesado = 8,
			Interesado = 9,
			Solicitante = 10,
			AprobadaPor = 11,
			NoAprobadaPor = 12,
			AceptadaPor = 13,
			NoAceptadaPor = 14,
			t420_fAprobada = 15,
			t420_fNoAprobada = 16,
			t420_fAceptada = 17,
			t420_fNoaceptada = 18,
			t420_fAnulada = 19,
			AnuladaPor = 20,
			t420_fContabilizada = 21,
			t420_fPagada = 22,
			t420_concepto = 23,
			t423_denominacion = 24,
			t301_idproyecto = 25,
			t301_denominacion = 26,
			TOTALVIAJE = 27,
			TOTALEUROS = 28,
			ACOBRAR_SINRETENCION = 29,
			ACOBRAR_SINRETENCION_EUROS = 30,
			ACOBRAR_NOMINA = 31,
			t422_idmoneda = 32,
			t422_denominacion = 33
        }

        internal NotasAbiertasRecientes(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un NotasAbiertasRecientes
        ///// </summary>
        //internal int Insert(Models.NotasAbiertasRecientes oNotasAbiertasRecientes)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[33] {
        //            Param(enumDBFields.t420_idreferencia, oNotasAbiertasRecientes.t420_idreferencia),
        //            Param(enumDBFields.nota_aparcada, oNotasAbiertasRecientes.nota_aparcada),
        //            Param(enumDBFields.TipoNota, oNotasAbiertasRecientes.TipoNota),
        //            Param(enumDBFields.t431_idestado, oNotasAbiertasRecientes.t431_idestado),
        //            Param(enumDBFields.t431_denominacion, oNotasAbiertasRecientes.t431_denominacion),
        //            Param(enumDBFields.t420_fTramitada, oNotasAbiertasRecientes.t420_fTramitada),
        //            Param(enumDBFields.t314_idusuario_interesado, oNotasAbiertasRecientes.t314_idusuario_interesado),
        //            Param(enumDBFields.t001_sexo_interesado, oNotasAbiertasRecientes.t001_sexo_interesado),
        //            Param(enumDBFields.Interesado, oNotasAbiertasRecientes.Interesado),
        //            Param(enumDBFields.Solicitante, oNotasAbiertasRecientes.Solicitante),
        //            Param(enumDBFields.AprobadaPor, oNotasAbiertasRecientes.AprobadaPor),
        //            Param(enumDBFields.NoAprobadaPor, oNotasAbiertasRecientes.NoAprobadaPor),
        //            Param(enumDBFields.AceptadaPor, oNotasAbiertasRecientes.AceptadaPor),
        //            Param(enumDBFields.NoAceptadaPor, oNotasAbiertasRecientes.NoAceptadaPor),
        //            Param(enumDBFields.t420_fAprobada, oNotasAbiertasRecientes.t420_fAprobada),
        //            Param(enumDBFields.t420_fNoAprobada, oNotasAbiertasRecientes.t420_fNoAprobada),
        //            Param(enumDBFields.t420_fAceptada, oNotasAbiertasRecientes.t420_fAceptada),
        //            Param(enumDBFields.t420_fNoaceptada, oNotasAbiertasRecientes.t420_fNoaceptada),
        //            Param(enumDBFields.t420_fAnulada, oNotasAbiertasRecientes.t420_fAnulada),
        //            Param(enumDBFields.AnuladaPor, oNotasAbiertasRecientes.AnuladaPor),
        //            Param(enumDBFields.t420_fContabilizada, oNotasAbiertasRecientes.t420_fContabilizada),
        //            Param(enumDBFields.t420_fPagada, oNotasAbiertasRecientes.t420_fPagada),
        //            Param(enumDBFields.t420_concepto, oNotasAbiertasRecientes.t420_concepto),
        //            Param(enumDBFields.t423_denominacion, oNotasAbiertasRecientes.t423_denominacion),
        //            Param(enumDBFields.t301_idproyecto, oNotasAbiertasRecientes.t301_idproyecto),
        //            Param(enumDBFields.t301_denominacion, oNotasAbiertasRecientes.t301_denominacion),
        //            Param(enumDBFields.TOTALVIAJE, oNotasAbiertasRecientes.TOTALVIAJE),
        //            Param(enumDBFields.TOTALEUROS, oNotasAbiertasRecientes.TOTALEUROS),
        //            Param(enumDBFields.ACOBRAR_SINRETENCION, oNotasAbiertasRecientes.ACOBRAR_SINRETENCION),
        //            Param(enumDBFields.ACOBRAR_SINRETENCION_EUROS, oNotasAbiertasRecientes.ACOBRAR_SINRETENCION_EUROS),
        //            Param(enumDBFields.ACOBRAR_NOMINA, oNotasAbiertasRecientes.ACOBRAR_NOMINA),
        //            Param(enumDBFields.t422_idmoneda, oNotasAbiertasRecientes.t422_idmoneda),
        //            Param(enumDBFields.t422_denominacion, oNotasAbiertasRecientes.t422_denominacion)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_NotasAbiertasRecientes_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un NotasAbiertasRecientes a partir del id
        ///// </summary>
        //internal Models.NotasAbiertasRecientes Select()
        //{
        //    Models.NotasAbiertasRecientes oNotasAbiertasRecientes = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_NotasAbiertasRecientes_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oNotasAbiertasRecientes = new Models.NotasAbiertasRecientes();
        //            oNotasAbiertasRecientes.t420_idreferencia=Convert.ToInt32(dr["t420_idreferencia"]);
        //            oNotasAbiertasRecientes.nota_aparcada=Convert.ToInt32(dr["nota_aparcada"]);
        //            oNotasAbiertasRecientes.TipoNota=Convert.ToString(dr["TipoNota"]);
        //            oNotasAbiertasRecientes.t431_idestado=Convert.ToString(dr["t431_idestado"]);
        //            oNotasAbiertasRecientes.t431_denominacion=Convert.ToString(dr["t431_denominacion"]);
        //            if(!Convert.IsDBNull(dr["t420_fTramitada"]))
        //                oNotasAbiertasRecientes.t420_fTramitada=Convert.ToDateTime(dr["t420_fTramitada"]);
        //            oNotasAbiertasRecientes.t314_idusuario_interesado=Convert.ToInt32(dr["t314_idusuario_interesado"]);
        //            oNotasAbiertasRecientes.t001_sexo_interesado=Convert.ToString(dr["t001_sexo_interesado"]);
        //            if(!Convert.IsDBNull(dr["Interesado"]))
        //                oNotasAbiertasRecientes.Interesado=Convert.ToString(dr["Interesado"]);
        //            if(!Convert.IsDBNull(dr["Solicitante"]))
        //                oNotasAbiertasRecientes.Solicitante=Convert.ToString(dr["Solicitante"]);
        //            oNotasAbiertasRecientes.AprobadaPor=Convert.ToString(dr["AprobadaPor"]);
        //            oNotasAbiertasRecientes.NoAprobadaPor=Convert.ToString(dr["NoAprobadaPor"]);
        //            oNotasAbiertasRecientes.AceptadaPor=Convert.ToString(dr["AceptadaPor"]);
        //            oNotasAbiertasRecientes.NoAceptadaPor=Convert.ToString(dr["NoAceptadaPor"]);
        //            if(!Convert.IsDBNull(dr["t420_fAprobada"]))
        //                oNotasAbiertasRecientes.t420_fAprobada=Convert.ToDateTime(dr["t420_fAprobada"]);
        //            if(!Convert.IsDBNull(dr["t420_fNoAprobada"]))
        //                oNotasAbiertasRecientes.t420_fNoAprobada=Convert.ToDateTime(dr["t420_fNoAprobada"]);
        //            if(!Convert.IsDBNull(dr["t420_fAceptada"]))
        //                oNotasAbiertasRecientes.t420_fAceptada=Convert.ToDateTime(dr["t420_fAceptada"]);
        //            if(!Convert.IsDBNull(dr["t420_fNoaceptada"]))
        //                oNotasAbiertasRecientes.t420_fNoaceptada=Convert.ToDateTime(dr["t420_fNoaceptada"]);
        //            if(!Convert.IsDBNull(dr["t420_fAnulada"]))
        //                oNotasAbiertasRecientes.t420_fAnulada=Convert.ToDateTime(dr["t420_fAnulada"]);
        //            if(!Convert.IsDBNull(dr["AnuladaPor"]))
        //                oNotasAbiertasRecientes.AnuladaPor=Convert.ToString(dr["AnuladaPor"]);
        //            if(!Convert.IsDBNull(dr["t420_fContabilizada"]))
        //                oNotasAbiertasRecientes.t420_fContabilizada=Convert.ToDateTime(dr["t420_fContabilizada"]);
        //            if(!Convert.IsDBNull(dr["t420_fPagada"]))
        //                oNotasAbiertasRecientes.t420_fPagada=Convert.ToDateTime(dr["t420_fPagada"]);
        //            oNotasAbiertasRecientes.t420_concepto=Convert.ToString(dr["t420_concepto"]);
        //            oNotasAbiertasRecientes.t423_denominacion=Convert.ToString(dr["t423_denominacion"]);
        //            if(!Convert.IsDBNull(dr["t301_idproyecto"]))
        //                oNotasAbiertasRecientes.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);
        //            if(!Convert.IsDBNull(dr["t301_denominacion"]))
        //                oNotasAbiertasRecientes.t301_denominacion=Convert.ToString(dr["t301_denominacion"]);
        //            if(!Convert.IsDBNull(dr["TOTALVIAJE"]))
        //                oNotasAbiertasRecientes.TOTALVIAJE=Convert.ToDecimal(dr["TOTALVIAJE"]);
        //            if(!Convert.IsDBNull(dr["TOTALEUROS"]))
        //                oNotasAbiertasRecientes.TOTALEUROS=Convert.ToDecimal(dr["TOTALEUROS"]);
        //            if(!Convert.IsDBNull(dr["ACOBRAR_SINRETENCION"]))
        //                oNotasAbiertasRecientes.ACOBRAR_SINRETENCION=Convert.ToDecimal(dr["ACOBRAR_SINRETENCION"]);
        //            if(!Convert.IsDBNull(dr["ACOBRAR_SINRETENCION_EUROS"]))
        //                oNotasAbiertasRecientes.ACOBRAR_SINRETENCION_EUROS=Convert.ToDecimal(dr["ACOBRAR_SINRETENCION_EUROS"]);
        //            if(!Convert.IsDBNull(dr["ACOBRAR_NOMINA"]))
        //                oNotasAbiertasRecientes.ACOBRAR_NOMINA=Convert.ToDecimal(dr["ACOBRAR_NOMINA"]);
        //            oNotasAbiertasRecientes.t422_idmoneda=Convert.ToString(dr["t422_idmoneda"]);
        //            oNotasAbiertasRecientes.t422_denominacion=Convert.ToString(dr["t422_denominacion"]);

        //        }
        //        return oNotasAbiertasRecientes;
				
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
        ///// Actualiza un NotasAbiertasRecientes a partir del id
        ///// </summary>
        //internal int Update(Models.NotasAbiertasRecientes oNotasAbiertasRecientes)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[33] {
        //            Param(enumDBFields.t420_idreferencia, oNotasAbiertasRecientes.t420_idreferencia),
        //            Param(enumDBFields.nota_aparcada, oNotasAbiertasRecientes.nota_aparcada),
        //            Param(enumDBFields.TipoNota, oNotasAbiertasRecientes.TipoNota),
        //            Param(enumDBFields.t431_idestado, oNotasAbiertasRecientes.t431_idestado),
        //            Param(enumDBFields.t431_denominacion, oNotasAbiertasRecientes.t431_denominacion),
        //            Param(enumDBFields.t420_fTramitada, oNotasAbiertasRecientes.t420_fTramitada),
        //            Param(enumDBFields.t314_idusuario_interesado, oNotasAbiertasRecientes.t314_idusuario_interesado),
        //            Param(enumDBFields.t001_sexo_interesado, oNotasAbiertasRecientes.t001_sexo_interesado),
        //            Param(enumDBFields.Interesado, oNotasAbiertasRecientes.Interesado),
        //            Param(enumDBFields.Solicitante, oNotasAbiertasRecientes.Solicitante),
        //            Param(enumDBFields.AprobadaPor, oNotasAbiertasRecientes.AprobadaPor),
        //            Param(enumDBFields.NoAprobadaPor, oNotasAbiertasRecientes.NoAprobadaPor),
        //            Param(enumDBFields.AceptadaPor, oNotasAbiertasRecientes.AceptadaPor),
        //            Param(enumDBFields.NoAceptadaPor, oNotasAbiertasRecientes.NoAceptadaPor),
        //            Param(enumDBFields.t420_fAprobada, oNotasAbiertasRecientes.t420_fAprobada),
        //            Param(enumDBFields.t420_fNoAprobada, oNotasAbiertasRecientes.t420_fNoAprobada),
        //            Param(enumDBFields.t420_fAceptada, oNotasAbiertasRecientes.t420_fAceptada),
        //            Param(enumDBFields.t420_fNoaceptada, oNotasAbiertasRecientes.t420_fNoaceptada),
        //            Param(enumDBFields.t420_fAnulada, oNotasAbiertasRecientes.t420_fAnulada),
        //            Param(enumDBFields.AnuladaPor, oNotasAbiertasRecientes.AnuladaPor),
        //            Param(enumDBFields.t420_fContabilizada, oNotasAbiertasRecientes.t420_fContabilizada),
        //            Param(enumDBFields.t420_fPagada, oNotasAbiertasRecientes.t420_fPagada),
        //            Param(enumDBFields.t420_concepto, oNotasAbiertasRecientes.t420_concepto),
        //            Param(enumDBFields.t423_denominacion, oNotasAbiertasRecientes.t423_denominacion),
        //            Param(enumDBFields.t301_idproyecto, oNotasAbiertasRecientes.t301_idproyecto),
        //            Param(enumDBFields.t301_denominacion, oNotasAbiertasRecientes.t301_denominacion),
        //            Param(enumDBFields.TOTALVIAJE, oNotasAbiertasRecientes.TOTALVIAJE),
        //            Param(enumDBFields.TOTALEUROS, oNotasAbiertasRecientes.TOTALEUROS),
        //            Param(enumDBFields.ACOBRAR_SINRETENCION, oNotasAbiertasRecientes.ACOBRAR_SINRETENCION),
        //            Param(enumDBFields.ACOBRAR_SINRETENCION_EUROS, oNotasAbiertasRecientes.ACOBRAR_SINRETENCION_EUROS),
        //            Param(enumDBFields.ACOBRAR_NOMINA, oNotasAbiertasRecientes.ACOBRAR_NOMINA),
        //            Param(enumDBFields.t422_idmoneda, oNotasAbiertasRecientes.t422_idmoneda),
        //            Param(enumDBFields.t422_denominacion, oNotasAbiertasRecientes.t422_denominacion)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_NotasAbiertasRecientes_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un NotasAbiertasRecientes a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_NotasAbiertasRecientes_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los NotasAbiertasRecientes
        ///// </summary>
        //internal List<Models.NotasAbiertasRecientes> Catalogo(Models.NotasAbiertasRecientes oNotasAbiertasRecientesFilter)
        //{
        //    Models.NotasAbiertasRecientes oNotasAbiertasRecientes = null;
        //    List<Models.NotasAbiertasRecientes> lst = new List<Models.NotasAbiertasRecientes>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[33] {
        //            Param(enumDBFields.t420_idreferencia, oTEMP_NotasAbiertasRecientesFilter.t420_idreferencia),
        //            Param(enumDBFields.nota_aparcada, oTEMP_NotasAbiertasRecientesFilter.nota_aparcada),
        //            Param(enumDBFields.TipoNota, oTEMP_NotasAbiertasRecientesFilter.TipoNota),
        //            Param(enumDBFields.t431_idestado, oTEMP_NotasAbiertasRecientesFilter.t431_idestado),
        //            Param(enumDBFields.t431_denominacion, oTEMP_NotasAbiertasRecientesFilter.t431_denominacion),
        //            Param(enumDBFields.t420_fTramitada, oTEMP_NotasAbiertasRecientesFilter.t420_fTramitada),
        //            Param(enumDBFields.t314_idusuario_interesado, oTEMP_NotasAbiertasRecientesFilter.t314_idusuario_interesado),
        //            Param(enumDBFields.t001_sexo_interesado, oTEMP_NotasAbiertasRecientesFilter.t001_sexo_interesado),
        //            Param(enumDBFields.Interesado, oTEMP_NotasAbiertasRecientesFilter.Interesado),
        //            Param(enumDBFields.Solicitante, oTEMP_NotasAbiertasRecientesFilter.Solicitante),
        //            Param(enumDBFields.AprobadaPor, oTEMP_NotasAbiertasRecientesFilter.AprobadaPor),
        //            Param(enumDBFields.NoAprobadaPor, oTEMP_NotasAbiertasRecientesFilter.NoAprobadaPor),
        //            Param(enumDBFields.AceptadaPor, oTEMP_NotasAbiertasRecientesFilter.AceptadaPor),
        //            Param(enumDBFields.NoAceptadaPor, oTEMP_NotasAbiertasRecientesFilter.NoAceptadaPor),
        //            Param(enumDBFields.t420_fAprobada, oTEMP_NotasAbiertasRecientesFilter.t420_fAprobada),
        //            Param(enumDBFields.t420_fNoAprobada, oTEMP_NotasAbiertasRecientesFilter.t420_fNoAprobada),
        //            Param(enumDBFields.t420_fAceptada, oTEMP_NotasAbiertasRecientesFilter.t420_fAceptada),
        //            Param(enumDBFields.t420_fNoaceptada, oTEMP_NotasAbiertasRecientesFilter.t420_fNoaceptada),
        //            Param(enumDBFields.t420_fAnulada, oTEMP_NotasAbiertasRecientesFilter.t420_fAnulada),
        //            Param(enumDBFields.AnuladaPor, oTEMP_NotasAbiertasRecientesFilter.AnuladaPor),
        //            Param(enumDBFields.t420_fContabilizada, oTEMP_NotasAbiertasRecientesFilter.t420_fContabilizada),
        //            Param(enumDBFields.t420_fPagada, oTEMP_NotasAbiertasRecientesFilter.t420_fPagada),
        //            Param(enumDBFields.t420_concepto, oTEMP_NotasAbiertasRecientesFilter.t420_concepto),
        //            Param(enumDBFields.t423_denominacion, oTEMP_NotasAbiertasRecientesFilter.t423_denominacion),
        //            Param(enumDBFields.t301_idproyecto, oTEMP_NotasAbiertasRecientesFilter.t301_idproyecto),
        //            Param(enumDBFields.t301_denominacion, oTEMP_NotasAbiertasRecientesFilter.t301_denominacion),
        //            Param(enumDBFields.TOTALVIAJE, oTEMP_NotasAbiertasRecientesFilter.TOTALVIAJE),
        //            Param(enumDBFields.TOTALEUROS, oTEMP_NotasAbiertasRecientesFilter.TOTALEUROS),
        //            Param(enumDBFields.ACOBRAR_SINRETENCION, oTEMP_NotasAbiertasRecientesFilter.ACOBRAR_SINRETENCION),
        //            Param(enumDBFields.ACOBRAR_SINRETENCION_EUROS, oTEMP_NotasAbiertasRecientesFilter.ACOBRAR_SINRETENCION_EUROS),
        //            Param(enumDBFields.ACOBRAR_NOMINA, oTEMP_NotasAbiertasRecientesFilter.ACOBRAR_NOMINA),
        //            Param(enumDBFields.t422_idmoneda, oTEMP_NotasAbiertasRecientesFilter.t422_idmoneda),
        //            Param(enumDBFields.t422_denominacion, oTEMP_NotasAbiertasRecientesFilter.t422_denominacion)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_NotasAbiertasRecientes_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oNotasAbiertasRecientes = new Models.NotasAbiertasRecientes();
        //            oNotasAbiertasRecientes.t420_idreferencia=Convert.ToInt32(dr["t420_idreferencia"]);
        //            oNotasAbiertasRecientes.nota_aparcada=Convert.ToInt32(dr["nota_aparcada"]);
        //            oNotasAbiertasRecientes.TipoNota=Convert.ToString(dr["TipoNota"]);
        //            oNotasAbiertasRecientes.t431_idestado=Convert.ToString(dr["t431_idestado"]);
        //            oNotasAbiertasRecientes.t431_denominacion=Convert.ToString(dr["t431_denominacion"]);
        //            if(!Convert.IsDBNull(dr["t420_fTramitada"]))
        //                oNotasAbiertasRecientes.t420_fTramitada=Convert.ToDateTime(dr["t420_fTramitada"]);
        //            oNotasAbiertasRecientes.t314_idusuario_interesado=Convert.ToInt32(dr["t314_idusuario_interesado"]);
        //            oNotasAbiertasRecientes.t001_sexo_interesado=Convert.ToString(dr["t001_sexo_interesado"]);
        //            if(!Convert.IsDBNull(dr["Interesado"]))
        //                oNotasAbiertasRecientes.Interesado=Convert.ToString(dr["Interesado"]);
        //            if(!Convert.IsDBNull(dr["Solicitante"]))
        //                oNotasAbiertasRecientes.Solicitante=Convert.ToString(dr["Solicitante"]);
        //            oNotasAbiertasRecientes.AprobadaPor=Convert.ToString(dr["AprobadaPor"]);
        //            oNotasAbiertasRecientes.NoAprobadaPor=Convert.ToString(dr["NoAprobadaPor"]);
        //            oNotasAbiertasRecientes.AceptadaPor=Convert.ToString(dr["AceptadaPor"]);
        //            oNotasAbiertasRecientes.NoAceptadaPor=Convert.ToString(dr["NoAceptadaPor"]);
        //            if(!Convert.IsDBNull(dr["t420_fAprobada"]))
        //                oNotasAbiertasRecientes.t420_fAprobada=Convert.ToDateTime(dr["t420_fAprobada"]);
        //            if(!Convert.IsDBNull(dr["t420_fNoAprobada"]))
        //                oNotasAbiertasRecientes.t420_fNoAprobada=Convert.ToDateTime(dr["t420_fNoAprobada"]);
        //            if(!Convert.IsDBNull(dr["t420_fAceptada"]))
        //                oNotasAbiertasRecientes.t420_fAceptada=Convert.ToDateTime(dr["t420_fAceptada"]);
        //            if(!Convert.IsDBNull(dr["t420_fNoaceptada"]))
        //                oNotasAbiertasRecientes.t420_fNoaceptada=Convert.ToDateTime(dr["t420_fNoaceptada"]);
        //            if(!Convert.IsDBNull(dr["t420_fAnulada"]))
        //                oNotasAbiertasRecientes.t420_fAnulada=Convert.ToDateTime(dr["t420_fAnulada"]);
        //            if(!Convert.IsDBNull(dr["AnuladaPor"]))
        //                oNotasAbiertasRecientes.AnuladaPor=Convert.ToString(dr["AnuladaPor"]);
        //            if(!Convert.IsDBNull(dr["t420_fContabilizada"]))
        //                oNotasAbiertasRecientes.t420_fContabilizada=Convert.ToDateTime(dr["t420_fContabilizada"]);
        //            if(!Convert.IsDBNull(dr["t420_fPagada"]))
        //                oNotasAbiertasRecientes.t420_fPagada=Convert.ToDateTime(dr["t420_fPagada"]);
        //            oNotasAbiertasRecientes.t420_concepto=Convert.ToString(dr["t420_concepto"]);
        //            oNotasAbiertasRecientes.t423_denominacion=Convert.ToString(dr["t423_denominacion"]);
        //            if(!Convert.IsDBNull(dr["t301_idproyecto"]))
        //                oNotasAbiertasRecientes.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);
        //            if(!Convert.IsDBNull(dr["t301_denominacion"]))
        //                oNotasAbiertasRecientes.t301_denominacion=Convert.ToString(dr["t301_denominacion"]);
        //            if(!Convert.IsDBNull(dr["TOTALVIAJE"]))
        //                oNotasAbiertasRecientes.TOTALVIAJE=Convert.ToDecimal(dr["TOTALVIAJE"]);
        //            if(!Convert.IsDBNull(dr["TOTALEUROS"]))
        //                oNotasAbiertasRecientes.TOTALEUROS=Convert.ToDecimal(dr["TOTALEUROS"]);
        //            if(!Convert.IsDBNull(dr["ACOBRAR_SINRETENCION"]))
        //                oNotasAbiertasRecientes.ACOBRAR_SINRETENCION=Convert.ToDecimal(dr["ACOBRAR_SINRETENCION"]);
        //            if(!Convert.IsDBNull(dr["ACOBRAR_SINRETENCION_EUROS"]))
        //                oNotasAbiertasRecientes.ACOBRAR_SINRETENCION_EUROS=Convert.ToDecimal(dr["ACOBRAR_SINRETENCION_EUROS"]);
        //            if(!Convert.IsDBNull(dr["ACOBRAR_NOMINA"]))
        //                oNotasAbiertasRecientes.ACOBRAR_NOMINA=Convert.ToDecimal(dr["ACOBRAR_NOMINA"]);
        //            oNotasAbiertasRecientes.t422_idmoneda=Convert.ToString(dr["t422_idmoneda"]);
        //            oNotasAbiertasRecientes.t422_denominacion=Convert.ToString(dr["t422_denominacion"]);

        //            lst.Add(oNotasAbiertasRecientes);

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
				case enumDBFields.t420_idreferencia:
					paramName = "@t420_idreferencia";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.nota_aparcada:
					paramName = "@nota_aparcada";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.TipoNota:
					paramName = "@TipoNota";
					paramType = SqlDbType.VarChar;
					paramSize = 3;
					break;
				case enumDBFields.t431_idestado:
					paramName = "@t431_idestado";
					paramType = SqlDbType.VarChar;
					paramSize = 1;
					break;
				case enumDBFields.t431_denominacion:
					paramName = "@t431_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 20;
					break;
				case enumDBFields.t420_fTramitada:
					paramName = "@t420_fTramitada";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t314_idusuario_interesado:
					paramName = "@t314_idusuario_interesado";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t001_sexo_interesado:
					paramName = "@t001_sexo_interesado";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
				case enumDBFields.Interesado:
					paramName = "@Interesado";
					paramType = SqlDbType.VarChar;
					paramSize = 150;
					break;
				case enumDBFields.Solicitante:
					paramName = "@Solicitante";
					paramType = SqlDbType.VarChar;
					paramSize = 150;
					break;
				case enumDBFields.AprobadaPor:
					paramName = "@AprobadaPor";
					paramType = SqlDbType.VarChar;
					paramSize = 150;
					break;
				case enumDBFields.NoAprobadaPor:
					paramName = "@NoAprobadaPor";
					paramType = SqlDbType.VarChar;
					paramSize = 150;
					break;
				case enumDBFields.AceptadaPor:
					paramName = "@AceptadaPor";
					paramType = SqlDbType.VarChar;
					paramSize = 150;
					break;
				case enumDBFields.NoAceptadaPor:
					paramName = "@NoAceptadaPor";
					paramType = SqlDbType.VarChar;
					paramSize = 150;
					break;
				case enumDBFields.t420_fAprobada:
					paramName = "@t420_fAprobada";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t420_fNoAprobada:
					paramName = "@t420_fNoAprobada";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t420_fAceptada:
					paramName = "@t420_fAceptada";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t420_fNoaceptada:
					paramName = "@t420_fNoaceptada";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t420_fAnulada:
					paramName = "@t420_fAnulada";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.AnuladaPor:
					paramName = "@AnuladaPor";
					paramType = SqlDbType.VarChar;
					paramSize = 150;
					break;
				case enumDBFields.t420_fContabilizada:
					paramName = "@t420_fContabilizada";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t420_fPagada:
					paramName = "@t420_fPagada";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t420_concepto:
					paramName = "@t420_concepto";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t423_denominacion:
					paramName = "@t423_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t301_idproyecto:
					paramName = "@t301_idproyecto";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t301_denominacion:
					paramName = "@t301_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 70;
					break;
				case enumDBFields.TOTALVIAJE:
					paramName = "@TOTALVIAJE";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.TOTALEUROS:
					paramName = "@TOTALEUROS";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.ACOBRAR_SINRETENCION:
					paramName = "@ACOBRAR_SINRETENCION";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.ACOBRAR_SINRETENCION_EUROS:
					paramName = "@ACOBRAR_SINRETENCION_EUROS";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.ACOBRAR_NOMINA:
					paramName = "@ACOBRAR_NOMINA";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.t422_idmoneda:
					paramName = "@t422_idmoneda";
					paramType = SqlDbType.VarChar;
					paramSize = 5;
					break;
				case enumDBFields.t422_denominacion:
					paramName = "@t422_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
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
