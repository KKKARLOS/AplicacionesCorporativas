using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for UsuarioLiquidacion
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class UsuarioLiquidacion 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t314_idusuario = 1,
			T001_IDFICEPI = 2,
			Profesional = 3,
			t422_idmoneda = 4,
			t313_idempresa = 5,
			t313_denominacion = 6,
			t007_idterrfis = 7,
			t007_nomterrfis = 8,
			t010_idoficina_liquidadora = 9,
			t010_idoficina_base = 10,
			t010_desoficina = 11,
			t069_iddietakm = 12,
			T069_descripcion = 13,
			T069_icdc = 14,
			T069_icmd = 15,
			T069_icda = 16,
			T069_icde = 17,
			T069_ick = 18,
			t007_iterdc = 19,
			t007_itermd = 20,
			t007_iterda = 21,
			t007_iterde = 22,
			t007_iterk = 23,
			autorresponsable = 24,
			t303_denominacion = 25,
			t303_idnodo = 26,
			CentrosCoste = 27,
			t313_idempresa_defecto = 28
        }

        internal UsuarioLiquidacion(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un UsuarioLiquidacion
        ///// </summary>
        //internal int Insert(Models.UsuarioLiquidacion oUsuarioLiquidacion)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[28] {
        //            Param(enumDBFields.t314_idusuario, oUsuarioLiquidacion.t314_idusuario),
        //            Param(enumDBFields.T001_IDFICEPI, oUsuarioLiquidacion.T001_IDFICEPI),
        //            Param(enumDBFields.Profesional, oUsuarioLiquidacion.Profesional),
        //            Param(enumDBFields.t422_idmoneda, oUsuarioLiquidacion.t422_idmoneda),
        //            Param(enumDBFields.t313_idempresa, oUsuarioLiquidacion.t313_idempresa),
        //            Param(enumDBFields.t313_denominacion, oUsuarioLiquidacion.t313_denominacion),
        //            Param(enumDBFields.t007_idterrfis, oUsuarioLiquidacion.t007_idterrfis),
        //            Param(enumDBFields.t007_nomterrfis, oUsuarioLiquidacion.t007_nomterrfis),
        //            Param(enumDBFields.t010_idoficina_liquidadora, oUsuarioLiquidacion.t010_idoficina_liquidadora),
        //            Param(enumDBFields.t010_idoficina_base, oUsuarioLiquidacion.t010_idoficina_base),
        //            Param(enumDBFields.t010_desoficina, oUsuarioLiquidacion.t010_desoficina),
        //            Param(enumDBFields.t069_iddietakm, oUsuarioLiquidacion.t069_iddietakm),
        //            Param(enumDBFields.T069_descripcion, oUsuarioLiquidacion.T069_descripcion),
        //            Param(enumDBFields.T069_icdc, oUsuarioLiquidacion.T069_icdc),
        //            Param(enumDBFields.T069_icmd, oUsuarioLiquidacion.T069_icmd),
        //            Param(enumDBFields.T069_icda, oUsuarioLiquidacion.T069_icda),
        //            Param(enumDBFields.T069_icde, oUsuarioLiquidacion.T069_icde),
        //            Param(enumDBFields.T069_ick, oUsuarioLiquidacion.T069_ick),
        //            Param(enumDBFields.t007_iterdc, oUsuarioLiquidacion.t007_iterdc),
        //            Param(enumDBFields.t007_itermd, oUsuarioLiquidacion.t007_itermd),
        //            Param(enumDBFields.t007_iterda, oUsuarioLiquidacion.t007_iterda),
        //            Param(enumDBFields.t007_iterde, oUsuarioLiquidacion.t007_iterde),
        //            Param(enumDBFields.t007_iterk, oUsuarioLiquidacion.t007_iterk),
        //            Param(enumDBFields.autorresponsable, oUsuarioLiquidacion.autorresponsable),
        //            Param(enumDBFields.t303_denominacion, oUsuarioLiquidacion.t303_denominacion),
        //            Param(enumDBFields.t303_idnodo, oUsuarioLiquidacion.t303_idnodo),
        //            Param(enumDBFields.CentrosCoste, oUsuarioLiquidacion.CentrosCoste),
        //            Param(enumDBFields.t313_idempresa_defecto, oUsuarioLiquidacion.t313_idempresa_defecto)
        //        };

        //        return (int)cDblib.Execute("_UsuarioLiquidacion_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un UsuarioLiquidacion a partir del id
        ///// </summary>
        //internal Models.UsuarioLiquidacion Select()
        //{
        //    Models.UsuarioLiquidacion oUsuarioLiquidacion = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_UsuarioLiquidacion_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oUsuarioLiquidacion = new Models.UsuarioLiquidacion();
        //            oUsuarioLiquidacion.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
        //            oUsuarioLiquidacion.T001_IDFICEPI=Convert.ToInt32(dr["T001_IDFICEPI"]);
        //            if(!Convert.IsDBNull(dr["Profesional"]))
        //                oUsuarioLiquidacion.Profesional=Convert.ToString(dr["Profesional"]);
        //            oUsuarioLiquidacion.t422_idmoneda=Convert.ToString(dr["t422_idmoneda"]);
        //            if(!Convert.IsDBNull(dr["t313_idempresa"]))
        //                oUsuarioLiquidacion.t313_idempresa=Convert.ToInt32(dr["t313_idempresa"]);
        //            if(!Convert.IsDBNull(dr["t313_denominacion"]))
        //                oUsuarioLiquidacion.t313_denominacion=Convert.ToString(dr["t313_denominacion"]);
        //            if(!Convert.IsDBNull(dr["t007_idterrfis"]))
        //                oUsuarioLiquidacion.t007_idterrfis=Convert.ToInt16(dr["t007_idterrfis"]);
        //            if(!Convert.IsDBNull(dr["t007_nomterrfis"]))
        //                oUsuarioLiquidacion.t007_nomterrfis=Convert.ToString(dr["t007_nomterrfis"]);
        //            oUsuarioLiquidacion.t010_idoficina_liquidadora=Convert.ToInt16(dr["t010_idoficina_liquidadora"]);
        //            if(!Convert.IsDBNull(dr["t010_idoficina_base"]))
        //                oUsuarioLiquidacion.t010_idoficina_base=Convert.ToInt16(dr["t010_idoficina_base"]);
        //            oUsuarioLiquidacion.t010_desoficina=Convert.ToString(dr["t010_desoficina"]);
        //            if(!Convert.IsDBNull(dr["t069_iddietakm"]))
        //                oUsuarioLiquidacion.t069_iddietakm=Convert.ToByte(dr["t069_iddietakm"]);
        //            if(!Convert.IsDBNull(dr["T069_descripcion"]))
        //                oUsuarioLiquidacion.T069_descripcion=Convert.ToString(dr["T069_descripcion"]);
        //            oUsuarioLiquidacion.T069_icdc=Convert.ToDecimal(dr["T069_icdc"]);
        //            oUsuarioLiquidacion.T069_icmd=Convert.ToDecimal(dr["T069_icmd"]);
        //            oUsuarioLiquidacion.T069_icda=Convert.ToDecimal(dr["T069_icda"]);
        //            oUsuarioLiquidacion.T069_icde=Convert.ToDecimal(dr["T069_icde"]);
        //            oUsuarioLiquidacion.T069_ick=Convert.ToDecimal(dr["T069_ick"]);
        //            if(!Convert.IsDBNull(dr["t007_iterdc"]))
        //                oUsuarioLiquidacion.t007_iterdc=Convert.ToDecimal(dr["t007_iterdc"]);
        //            if(!Convert.IsDBNull(dr["t007_itermd"]))
        //                oUsuarioLiquidacion.t007_itermd=Convert.ToDecimal(dr["t007_itermd"]);
        //            if(!Convert.IsDBNull(dr["t007_iterda"]))
        //                oUsuarioLiquidacion.t007_iterda=Convert.ToDecimal(dr["t007_iterda"]);
        //            if(!Convert.IsDBNull(dr["t007_iterde"]))
        //                oUsuarioLiquidacion.t007_iterde=Convert.ToDecimal(dr["t007_iterde"]);
        //            if(!Convert.IsDBNull(dr["t007_iterk"]))
        //                oUsuarioLiquidacion.t007_iterk=Convert.ToDecimal(dr["t007_iterk"]);
        //            oUsuarioLiquidacion.autorresponsable=Convert.ToInt32(dr["autorresponsable"]);
        //            oUsuarioLiquidacion.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
        //            if(!Convert.IsDBNull(dr["t303_idnodo"]))
        //                oUsuarioLiquidacion.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
        //            oUsuarioLiquidacion.CentrosCoste=Convert.ToInt32(dr["CentrosCoste"]);
        //            oUsuarioLiquidacion.t313_idempresa_defecto=Convert.ToInt32(dr["t313_idempresa_defecto"]);

        //        }
        //        return oUsuarioLiquidacion;
				
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
        ///// Actualiza un UsuarioLiquidacion a partir del id
        ///// </summary>
        //internal int Update(Models.UsuarioLiquidacion oUsuarioLiquidacion)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[28] {
        //            Param(enumDBFields.t314_idusuario, oUsuarioLiquidacion.t314_idusuario),
        //            Param(enumDBFields.T001_IDFICEPI, oUsuarioLiquidacion.T001_IDFICEPI),
        //            Param(enumDBFields.Profesional, oUsuarioLiquidacion.Profesional),
        //            Param(enumDBFields.t422_idmoneda, oUsuarioLiquidacion.t422_idmoneda),
        //            Param(enumDBFields.t313_idempresa, oUsuarioLiquidacion.t313_idempresa),
        //            Param(enumDBFields.t313_denominacion, oUsuarioLiquidacion.t313_denominacion),
        //            Param(enumDBFields.t007_idterrfis, oUsuarioLiquidacion.t007_idterrfis),
        //            Param(enumDBFields.t007_nomterrfis, oUsuarioLiquidacion.t007_nomterrfis),
        //            Param(enumDBFields.t010_idoficina_liquidadora, oUsuarioLiquidacion.t010_idoficina_liquidadora),
        //            Param(enumDBFields.t010_idoficina_base, oUsuarioLiquidacion.t010_idoficina_base),
        //            Param(enumDBFields.t010_desoficina, oUsuarioLiquidacion.t010_desoficina),
        //            Param(enumDBFields.t069_iddietakm, oUsuarioLiquidacion.t069_iddietakm),
        //            Param(enumDBFields.T069_descripcion, oUsuarioLiquidacion.T069_descripcion),
        //            Param(enumDBFields.T069_icdc, oUsuarioLiquidacion.T069_icdc),
        //            Param(enumDBFields.T069_icmd, oUsuarioLiquidacion.T069_icmd),
        //            Param(enumDBFields.T069_icda, oUsuarioLiquidacion.T069_icda),
        //            Param(enumDBFields.T069_icde, oUsuarioLiquidacion.T069_icde),
        //            Param(enumDBFields.T069_ick, oUsuarioLiquidacion.T069_ick),
        //            Param(enumDBFields.t007_iterdc, oUsuarioLiquidacion.t007_iterdc),
        //            Param(enumDBFields.t007_itermd, oUsuarioLiquidacion.t007_itermd),
        //            Param(enumDBFields.t007_iterda, oUsuarioLiquidacion.t007_iterda),
        //            Param(enumDBFields.t007_iterde, oUsuarioLiquidacion.t007_iterde),
        //            Param(enumDBFields.t007_iterk, oUsuarioLiquidacion.t007_iterk),
        //            Param(enumDBFields.autorresponsable, oUsuarioLiquidacion.autorresponsable),
        //            Param(enumDBFields.t303_denominacion, oUsuarioLiquidacion.t303_denominacion),
        //            Param(enumDBFields.t303_idnodo, oUsuarioLiquidacion.t303_idnodo),
        //            Param(enumDBFields.CentrosCoste, oUsuarioLiquidacion.CentrosCoste),
        //            Param(enumDBFields.t313_idempresa_defecto, oUsuarioLiquidacion.t313_idempresa_defecto)
        //        };
                           
        //        return (int)cDblib.Execute("_UsuarioLiquidacion_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un UsuarioLiquidacion a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_UsuarioLiquidacion_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los UsuarioLiquidacion
        ///// </summary>
        //internal List<Models.UsuarioLiquidacion> Catalogo(Models.UsuarioLiquidacion oUsuarioLiquidacionFilter)
        //{
        //    Models.UsuarioLiquidacion oUsuarioLiquidacion = null;
        //    List<Models.UsuarioLiquidacion> lst = new List<Models.UsuarioLiquidacion>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[28] {
        //            Param(enumDBFields.t314_idusuario, oTEMP_UsuarioLiquidacionFilter.t314_idusuario),
        //            Param(enumDBFields.T001_IDFICEPI, oTEMP_UsuarioLiquidacionFilter.T001_IDFICEPI),
        //            Param(enumDBFields.Profesional, oTEMP_UsuarioLiquidacionFilter.Profesional),
        //            Param(enumDBFields.t422_idmoneda, oTEMP_UsuarioLiquidacionFilter.t422_idmoneda),
        //            Param(enumDBFields.t313_idempresa, oTEMP_UsuarioLiquidacionFilter.t313_idempresa),
        //            Param(enumDBFields.t313_denominacion, oTEMP_UsuarioLiquidacionFilter.t313_denominacion),
        //            Param(enumDBFields.t007_idterrfis, oTEMP_UsuarioLiquidacionFilter.t007_idterrfis),
        //            Param(enumDBFields.t007_nomterrfis, oTEMP_UsuarioLiquidacionFilter.t007_nomterrfis),
        //            Param(enumDBFields.t010_idoficina_liquidadora, oTEMP_UsuarioLiquidacionFilter.t010_idoficina_liquidadora),
        //            Param(enumDBFields.t010_idoficina_base, oTEMP_UsuarioLiquidacionFilter.t010_idoficina_base),
        //            Param(enumDBFields.t010_desoficina, oTEMP_UsuarioLiquidacionFilter.t010_desoficina),
        //            Param(enumDBFields.t069_iddietakm, oTEMP_UsuarioLiquidacionFilter.t069_iddietakm),
        //            Param(enumDBFields.T069_descripcion, oTEMP_UsuarioLiquidacionFilter.T069_descripcion),
        //            Param(enumDBFields.T069_icdc, oTEMP_UsuarioLiquidacionFilter.T069_icdc),
        //            Param(enumDBFields.T069_icmd, oTEMP_UsuarioLiquidacionFilter.T069_icmd),
        //            Param(enumDBFields.T069_icda, oTEMP_UsuarioLiquidacionFilter.T069_icda),
        //            Param(enumDBFields.T069_icde, oTEMP_UsuarioLiquidacionFilter.T069_icde),
        //            Param(enumDBFields.T069_ick, oTEMP_UsuarioLiquidacionFilter.T069_ick),
        //            Param(enumDBFields.t007_iterdc, oTEMP_UsuarioLiquidacionFilter.t007_iterdc),
        //            Param(enumDBFields.t007_itermd, oTEMP_UsuarioLiquidacionFilter.t007_itermd),
        //            Param(enumDBFields.t007_iterda, oTEMP_UsuarioLiquidacionFilter.t007_iterda),
        //            Param(enumDBFields.t007_iterde, oTEMP_UsuarioLiquidacionFilter.t007_iterde),
        //            Param(enumDBFields.t007_iterk, oTEMP_UsuarioLiquidacionFilter.t007_iterk),
        //            Param(enumDBFields.autorresponsable, oTEMP_UsuarioLiquidacionFilter.autorresponsable),
        //            Param(enumDBFields.t303_denominacion, oTEMP_UsuarioLiquidacionFilter.t303_denominacion),
        //            Param(enumDBFields.t303_idnodo, oTEMP_UsuarioLiquidacionFilter.t303_idnodo),
        //            Param(enumDBFields.CentrosCoste, oTEMP_UsuarioLiquidacionFilter.CentrosCoste),
        //            Param(enumDBFields.t313_idempresa_defecto, oTEMP_UsuarioLiquidacionFilter.t313_idempresa_defecto)
        //        };

        //        dr = cDblib.DataReader("_UsuarioLiquidacion_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oUsuarioLiquidacion = new Models.UsuarioLiquidacion();
        //            oUsuarioLiquidacion.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
        //            oUsuarioLiquidacion.T001_IDFICEPI=Convert.ToInt32(dr["T001_IDFICEPI"]);
        //            if(!Convert.IsDBNull(dr["Profesional"]))
        //                oUsuarioLiquidacion.Profesional=Convert.ToString(dr["Profesional"]);
        //            oUsuarioLiquidacion.t422_idmoneda=Convert.ToString(dr["t422_idmoneda"]);
        //            if(!Convert.IsDBNull(dr["t313_idempresa"]))
        //                oUsuarioLiquidacion.t313_idempresa=Convert.ToInt32(dr["t313_idempresa"]);
        //            if(!Convert.IsDBNull(dr["t313_denominacion"]))
        //                oUsuarioLiquidacion.t313_denominacion=Convert.ToString(dr["t313_denominacion"]);
        //            if(!Convert.IsDBNull(dr["t007_idterrfis"]))
        //                oUsuarioLiquidacion.t007_idterrfis=Convert.ToInt16(dr["t007_idterrfis"]);
        //            if(!Convert.IsDBNull(dr["t007_nomterrfis"]))
        //                oUsuarioLiquidacion.t007_nomterrfis=Convert.ToString(dr["t007_nomterrfis"]);
        //            oUsuarioLiquidacion.t010_idoficina_liquidadora=Convert.ToInt16(dr["t010_idoficina_liquidadora"]);
        //            if(!Convert.IsDBNull(dr["t010_idoficina_base"]))
        //                oUsuarioLiquidacion.t010_idoficina_base=Convert.ToInt16(dr["t010_idoficina_base"]);
        //            oUsuarioLiquidacion.t010_desoficina=Convert.ToString(dr["t010_desoficina"]);
        //            if(!Convert.IsDBNull(dr["t069_iddietakm"]))
        //                oUsuarioLiquidacion.t069_iddietakm=Convert.ToByte(dr["t069_iddietakm"]);
        //            if(!Convert.IsDBNull(dr["T069_descripcion"]))
        //                oUsuarioLiquidacion.T069_descripcion=Convert.ToString(dr["T069_descripcion"]);
        //            oUsuarioLiquidacion.T069_icdc=Convert.ToDecimal(dr["T069_icdc"]);
        //            oUsuarioLiquidacion.T069_icmd=Convert.ToDecimal(dr["T069_icmd"]);
        //            oUsuarioLiquidacion.T069_icda=Convert.ToDecimal(dr["T069_icda"]);
        //            oUsuarioLiquidacion.T069_icde=Convert.ToDecimal(dr["T069_icde"]);
        //            oUsuarioLiquidacion.T069_ick=Convert.ToDecimal(dr["T069_ick"]);
        //            if(!Convert.IsDBNull(dr["t007_iterdc"]))
        //                oUsuarioLiquidacion.t007_iterdc=Convert.ToDecimal(dr["t007_iterdc"]);
        //            if(!Convert.IsDBNull(dr["t007_itermd"]))
        //                oUsuarioLiquidacion.t007_itermd=Convert.ToDecimal(dr["t007_itermd"]);
        //            if(!Convert.IsDBNull(dr["t007_iterda"]))
        //                oUsuarioLiquidacion.t007_iterda=Convert.ToDecimal(dr["t007_iterda"]);
        //            if(!Convert.IsDBNull(dr["t007_iterde"]))
        //                oUsuarioLiquidacion.t007_iterde=Convert.ToDecimal(dr["t007_iterde"]);
        //            if(!Convert.IsDBNull(dr["t007_iterk"]))
        //                oUsuarioLiquidacion.t007_iterk=Convert.ToDecimal(dr["t007_iterk"]);
        //            oUsuarioLiquidacion.autorresponsable=Convert.ToInt32(dr["autorresponsable"]);
        //            oUsuarioLiquidacion.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
        //            if(!Convert.IsDBNull(dr["t303_idnodo"]))
        //                oUsuarioLiquidacion.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
        //            oUsuarioLiquidacion.CentrosCoste=Convert.ToInt32(dr["CentrosCoste"]);
        //            oUsuarioLiquidacion.t313_idempresa_defecto=Convert.ToInt32(dr["t313_idempresa_defecto"]);

        //            lst.Add(oUsuarioLiquidacion);

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
				case enumDBFields.t314_idusuario:
					paramName = "@t314_idusuario";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.T001_IDFICEPI:
					paramName = "@T001_IDFICEPI";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.Profesional:
					paramName = "@Profesional";
					paramType = SqlDbType.VarChar;
					paramSize = 150;
					break;
				case enumDBFields.t422_idmoneda:
					paramName = "@t422_idmoneda";
					paramType = SqlDbType.VarChar;
					paramSize = 5;
					break;
				case enumDBFields.t313_idempresa:
					paramName = "@t313_idempresa";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t313_denominacion:
					paramName = "@t313_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t007_idterrfis:
					paramName = "@t007_idterrfis";
					paramType = SqlDbType.SmallInt;
					paramSize = 2;
					break;
				case enumDBFields.t007_nomterrfis:
					paramName = "@t007_nomterrfis";
					paramType = SqlDbType.VarChar;
					paramSize = 25;
					break;
				case enumDBFields.t010_idoficina_liquidadora:
					paramName = "@t010_idoficina_liquidadora";
					paramType = SqlDbType.SmallInt;
					paramSize = 2;
					break;
				case enumDBFields.t010_idoficina_base:
					paramName = "@t010_idoficina_base";
					paramType = SqlDbType.SmallInt;
					paramSize = 2;
					break;
				case enumDBFields.t010_desoficina:
					paramName = "@t010_desoficina";
					paramType = SqlDbType.VarChar;
					paramSize = 40;
					break;
				case enumDBFields.t069_iddietakm:
					paramName = "@t069_iddietakm";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.T069_descripcion:
					paramName = "@T069_descripcion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.T069_icdc:
					paramName = "@T069_icdc";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.T069_icmd:
					paramName = "@T069_icmd";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.T069_icda:
					paramName = "@T069_icda";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.T069_icde:
					paramName = "@T069_icde";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.T069_ick:
					paramName = "@T069_ick";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.t007_iterdc:
					paramName = "@t007_iterdc";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.t007_itermd:
					paramName = "@t007_itermd";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.t007_iterda:
					paramName = "@t007_iterda";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.t007_iterde:
					paramName = "@t007_iterde";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.t007_iterk:
					paramName = "@t007_iterk";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.autorresponsable:
					paramName = "@autorresponsable";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t303_denominacion:
					paramName = "@t303_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t303_idnodo:
					paramName = "@t303_idnodo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.CentrosCoste:
					paramName = "@CentrosCoste";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t313_idempresa_defecto:
					paramName = "@t313_idempresa_defecto";
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
