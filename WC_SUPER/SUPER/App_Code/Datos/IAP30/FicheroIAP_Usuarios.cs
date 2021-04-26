using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for FicheroIAP_Usuarios
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class FicheroIAP_Usuarios 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t001_IDFICEPI = 1,
			t314_idusuario = 2,
			Profesional = 3,
			t303_ultcierreIAP = 4,
			t303_idnodo = 5,
			t314_jornadareducida = 6,
			t314_horasjor_red = 7,
			t314_fdesde_red = 8,
			t314_fhasta_red = 9,
			t314_controlhuecos = 10,
			fUltImputacion = 11,
			t066_idcal = 12,
			t066_descal = 13,
			t066_semlabL = 14,
			t066_semlabM = 15,
			t066_semlabX = 16,
			t066_semlabJ = 17,
			t066_semlabV = 18,
			t066_semlabS = 19,
			t066_semlabD = 20,
			t001_codred = 21,
			t001_fecalta = 22,
			t001_fecbaja = 23
        }

        internal FicheroIAP_Usuarios(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un FicheroIAP_Usuarios
        ///// </summary>
        //internal int Insert(Models.FicheroIAP_Usuarios oFicheroIAP_Usuarios)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[23] {
        //            Param(enumDBFields.t001_IDFICEPI, oFicheroIAP_Usuarios.t001_IDFICEPI),
        //            Param(enumDBFields.t314_idusuario, oFicheroIAP_Usuarios.t314_idusuario),
        //            Param(enumDBFields.Profesional, oFicheroIAP_Usuarios.Profesional),
        //            Param(enumDBFields.t303_ultcierreIAP, oFicheroIAP_Usuarios.t303_ultcierreIAP),
        //            Param(enumDBFields.t303_idnodo, oFicheroIAP_Usuarios.t303_idnodo),
        //            Param(enumDBFields.t314_jornadareducida, oFicheroIAP_Usuarios.t314_jornadareducida),
        //            Param(enumDBFields.t314_horasjor_red, oFicheroIAP_Usuarios.t314_horasjor_red),
        //            Param(enumDBFields.t314_fdesde_red, oFicheroIAP_Usuarios.t314_fdesde_red),
        //            Param(enumDBFields.t314_fhasta_red, oFicheroIAP_Usuarios.t314_fhasta_red),
        //            Param(enumDBFields.t314_controlhuecos, oFicheroIAP_Usuarios.t314_controlhuecos),
        //            Param(enumDBFields.fUltImputacion, oFicheroIAP_Usuarios.fUltImputacion),
        //            Param(enumDBFields.t066_idcal, oFicheroIAP_Usuarios.t066_idcal),
        //            Param(enumDBFields.t066_descal, oFicheroIAP_Usuarios.t066_descal),
        //            Param(enumDBFields.t066_semlabL, oFicheroIAP_Usuarios.t066_semlabL),
        //            Param(enumDBFields.t066_semlabM, oFicheroIAP_Usuarios.t066_semlabM),
        //            Param(enumDBFields.t066_semlabX, oFicheroIAP_Usuarios.t066_semlabX),
        //            Param(enumDBFields.t066_semlabJ, oFicheroIAP_Usuarios.t066_semlabJ),
        //            Param(enumDBFields.t066_semlabV, oFicheroIAP_Usuarios.t066_semlabV),
        //            Param(enumDBFields.t066_semlabS, oFicheroIAP_Usuarios.t066_semlabS),
        //            Param(enumDBFields.t066_semlabD, oFicheroIAP_Usuarios.t066_semlabD),
        //            Param(enumDBFields.t001_codred, oFicheroIAP_Usuarios.t001_codred),
        //            Param(enumDBFields.t001_fecalta, oFicheroIAP_Usuarios.t001_fecalta),
        //            Param(enumDBFields.t001_fecbaja, oFicheroIAP_Usuarios.t001_fecbaja)
        //        };

        //        return (int)cDblib.Execute("_FicheroIAP_Usuarios_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un FicheroIAP_Usuarios a partir del id
        ///// </summary>
        //internal Models.FicheroIAP_Usuarios Select()
        //{
        //    Models.FicheroIAP_Usuarios oFicheroIAP_Usuarios = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_FicheroIAP_Usuarios_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oFicheroIAP_Usuarios = new Models.FicheroIAP_Usuarios();
        //            oFicheroIAP_Usuarios.t001_IDFICEPI=Convert.ToInt32(dr["t001_IDFICEPI"]);
        //            oFicheroIAP_Usuarios.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
        //            if(!Convert.IsDBNull(dr["Profesional"]))
        //                oFicheroIAP_Usuarios.Profesional=Convert.ToString(dr["Profesional"]);
        //            if(!Convert.IsDBNull(dr["t303_ultcierreIAP"]))
        //                oFicheroIAP_Usuarios.t303_ultcierreIAP=Convert.ToInt32(dr["t303_ultcierreIAP"]);
        //            if(!Convert.IsDBNull(dr["t303_idnodo"]))
        //                oFicheroIAP_Usuarios.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
        //            oFicheroIAP_Usuarios.t314_jornadareducida=Convert.ToBoolean(dr["t314_jornadareducida"]);
        //            oFicheroIAP_Usuarios.t314_horasjor_red=Convert.ToSingle(dr["t314_horasjor_red"]);
        //            if(!Convert.IsDBNull(dr["t314_fdesde_red"]))
        //                oFicheroIAP_Usuarios.t314_fdesde_red=Convert.ToDateTime(dr["t314_fdesde_red"]);
        //            if(!Convert.IsDBNull(dr["t314_fhasta_red"]))
        //                oFicheroIAP_Usuarios.t314_fhasta_red=Convert.ToDateTime(dr["t314_fhasta_red"]);
        //            oFicheroIAP_Usuarios.t314_controlhuecos=Convert.ToBoolean(dr["t314_controlhuecos"]);
        //            if(!Convert.IsDBNull(dr["fUltImputacion"]))
        //                oFicheroIAP_Usuarios.fUltImputacion=Convert.ToDateTime(dr["fUltImputacion"]);
        //            oFicheroIAP_Usuarios.t066_idcal=Convert.ToInt32(dr["t066_idcal"]);
        //            oFicheroIAP_Usuarios.t066_descal=Convert.ToString(dr["t066_descal"]);
        //            oFicheroIAP_Usuarios.t066_semlabL=Convert.ToInt32(dr["t066_semlabL"]);
        //            oFicheroIAP_Usuarios.t066_semlabM=Convert.ToInt32(dr["t066_semlabM"]);
        //            oFicheroIAP_Usuarios.t066_semlabX=Convert.ToInt32(dr["t066_semlabX"]);
        //            oFicheroIAP_Usuarios.t066_semlabJ=Convert.ToInt32(dr["t066_semlabJ"]);
        //            oFicheroIAP_Usuarios.t066_semlabV=Convert.ToInt32(dr["t066_semlabV"]);
        //            oFicheroIAP_Usuarios.t066_semlabS=Convert.ToInt32(dr["t066_semlabS"]);
        //            oFicheroIAP_Usuarios.t066_semlabD=Convert.ToInt32(dr["t066_semlabD"]);
        //            oFicheroIAP_Usuarios.t001_codred=Convert.ToString(dr["t001_codred"]);
        //            oFicheroIAP_Usuarios.t001_fecalta=Convert.ToDateTime(dr["t001_fecalta"]);
        //            oFicheroIAP_Usuarios.t001_fecbaja=Convert.ToDateTime(dr["t001_fecbaja"]);

        //        }
        //        return oFicheroIAP_Usuarios;
				
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
        ///// Actualiza un FicheroIAP_Usuarios a partir del id
        ///// </summary>
        //internal int Update(Models.FicheroIAP_Usuarios oFicheroIAP_Usuarios)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[23] {
        //            Param(enumDBFields.t001_IDFICEPI, oFicheroIAP_Usuarios.t001_IDFICEPI),
        //            Param(enumDBFields.t314_idusuario, oFicheroIAP_Usuarios.t314_idusuario),
        //            Param(enumDBFields.Profesional, oFicheroIAP_Usuarios.Profesional),
        //            Param(enumDBFields.t303_ultcierreIAP, oFicheroIAP_Usuarios.t303_ultcierreIAP),
        //            Param(enumDBFields.t303_idnodo, oFicheroIAP_Usuarios.t303_idnodo),
        //            Param(enumDBFields.t314_jornadareducida, oFicheroIAP_Usuarios.t314_jornadareducida),
        //            Param(enumDBFields.t314_horasjor_red, oFicheroIAP_Usuarios.t314_horasjor_red),
        //            Param(enumDBFields.t314_fdesde_red, oFicheroIAP_Usuarios.t314_fdesde_red),
        //            Param(enumDBFields.t314_fhasta_red, oFicheroIAP_Usuarios.t314_fhasta_red),
        //            Param(enumDBFields.t314_controlhuecos, oFicheroIAP_Usuarios.t314_controlhuecos),
        //            Param(enumDBFields.fUltImputacion, oFicheroIAP_Usuarios.fUltImputacion),
        //            Param(enumDBFields.t066_idcal, oFicheroIAP_Usuarios.t066_idcal),
        //            Param(enumDBFields.t066_descal, oFicheroIAP_Usuarios.t066_descal),
        //            Param(enumDBFields.t066_semlabL, oFicheroIAP_Usuarios.t066_semlabL),
        //            Param(enumDBFields.t066_semlabM, oFicheroIAP_Usuarios.t066_semlabM),
        //            Param(enumDBFields.t066_semlabX, oFicheroIAP_Usuarios.t066_semlabX),
        //            Param(enumDBFields.t066_semlabJ, oFicheroIAP_Usuarios.t066_semlabJ),
        //            Param(enumDBFields.t066_semlabV, oFicheroIAP_Usuarios.t066_semlabV),
        //            Param(enumDBFields.t066_semlabS, oFicheroIAP_Usuarios.t066_semlabS),
        //            Param(enumDBFields.t066_semlabD, oFicheroIAP_Usuarios.t066_semlabD),
        //            Param(enumDBFields.t001_codred, oFicheroIAP_Usuarios.t001_codred),
        //            Param(enumDBFields.t001_fecalta, oFicheroIAP_Usuarios.t001_fecalta),
        //            Param(enumDBFields.t001_fecbaja, oFicheroIAP_Usuarios.t001_fecbaja)
        //        };
                           
        //        return (int)cDblib.Execute("_FicheroIAP_Usuarios_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un FicheroIAP_Usuarios a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_FicheroIAP_Usuarios_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los FicheroIAP_Usuarios
        ///// </summary>
        //internal List<Models.FicheroIAP_Usuarios> Catalogo(Models.FicheroIAP_Usuarios oFicheroIAP_UsuariosFilter)
        //{
        //    Models.FicheroIAP_Usuarios oFicheroIAP_Usuarios = null;
        //    List<Models.FicheroIAP_Usuarios> lst = new List<Models.FicheroIAP_Usuarios>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[23] {
        //            Param(enumDBFields.t001_IDFICEPI, oTEMP_FicheroIAP_UsuariosFilter.t001_IDFICEPI),
        //            Param(enumDBFields.t314_idusuario, oTEMP_FicheroIAP_UsuariosFilter.t314_idusuario),
        //            Param(enumDBFields.Profesional, oTEMP_FicheroIAP_UsuariosFilter.Profesional),
        //            Param(enumDBFields.t303_ultcierreIAP, oTEMP_FicheroIAP_UsuariosFilter.t303_ultcierreIAP),
        //            Param(enumDBFields.t303_idnodo, oTEMP_FicheroIAP_UsuariosFilter.t303_idnodo),
        //            Param(enumDBFields.t314_jornadareducida, oTEMP_FicheroIAP_UsuariosFilter.t314_jornadareducida),
        //            Param(enumDBFields.t314_horasjor_red, oTEMP_FicheroIAP_UsuariosFilter.t314_horasjor_red),
        //            Param(enumDBFields.t314_fdesde_red, oTEMP_FicheroIAP_UsuariosFilter.t314_fdesde_red),
        //            Param(enumDBFields.t314_fhasta_red, oTEMP_FicheroIAP_UsuariosFilter.t314_fhasta_red),
        //            Param(enumDBFields.t314_controlhuecos, oTEMP_FicheroIAP_UsuariosFilter.t314_controlhuecos),
        //            Param(enumDBFields.fUltImputacion, oTEMP_FicheroIAP_UsuariosFilter.fUltImputacion),
        //            Param(enumDBFields.t066_idcal, oTEMP_FicheroIAP_UsuariosFilter.t066_idcal),
        //            Param(enumDBFields.t066_descal, oTEMP_FicheroIAP_UsuariosFilter.t066_descal),
        //            Param(enumDBFields.t066_semlabL, oTEMP_FicheroIAP_UsuariosFilter.t066_semlabL),
        //            Param(enumDBFields.t066_semlabM, oTEMP_FicheroIAP_UsuariosFilter.t066_semlabM),
        //            Param(enumDBFields.t066_semlabX, oTEMP_FicheroIAP_UsuariosFilter.t066_semlabX),
        //            Param(enumDBFields.t066_semlabJ, oTEMP_FicheroIAP_UsuariosFilter.t066_semlabJ),
        //            Param(enumDBFields.t066_semlabV, oTEMP_FicheroIAP_UsuariosFilter.t066_semlabV),
        //            Param(enumDBFields.t066_semlabS, oTEMP_FicheroIAP_UsuariosFilter.t066_semlabS),
        //            Param(enumDBFields.t066_semlabD, oTEMP_FicheroIAP_UsuariosFilter.t066_semlabD),
        //            Param(enumDBFields.t001_codred, oTEMP_FicheroIAP_UsuariosFilter.t001_codred),
        //            Param(enumDBFields.t001_fecalta, oTEMP_FicheroIAP_UsuariosFilter.t001_fecalta),
        //            Param(enumDBFields.t001_fecbaja, oTEMP_FicheroIAP_UsuariosFilter.t001_fecbaja)
        //        };

        //        dr = cDblib.DataReader("_FicheroIAP_Usuarios_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oFicheroIAP_Usuarios = new Models.FicheroIAP_Usuarios();
        //            oFicheroIAP_Usuarios.t001_IDFICEPI=Convert.ToInt32(dr["t001_IDFICEPI"]);
        //            oFicheroIAP_Usuarios.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
        //            if(!Convert.IsDBNull(dr["Profesional"]))
        //                oFicheroIAP_Usuarios.Profesional=Convert.ToString(dr["Profesional"]);
        //            if(!Convert.IsDBNull(dr["t303_ultcierreIAP"]))
        //                oFicheroIAP_Usuarios.t303_ultcierreIAP=Convert.ToInt32(dr["t303_ultcierreIAP"]);
        //            if(!Convert.IsDBNull(dr["t303_idnodo"]))
        //                oFicheroIAP_Usuarios.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
        //            oFicheroIAP_Usuarios.t314_jornadareducida=Convert.ToBoolean(dr["t314_jornadareducida"]);
        //            oFicheroIAP_Usuarios.t314_horasjor_red=Convert.ToSingle(dr["t314_horasjor_red"]);
        //            if(!Convert.IsDBNull(dr["t314_fdesde_red"]))
        //                oFicheroIAP_Usuarios.t314_fdesde_red=Convert.ToDateTime(dr["t314_fdesde_red"]);
        //            if(!Convert.IsDBNull(dr["t314_fhasta_red"]))
        //                oFicheroIAP_Usuarios.t314_fhasta_red=Convert.ToDateTime(dr["t314_fhasta_red"]);
        //            oFicheroIAP_Usuarios.t314_controlhuecos=Convert.ToBoolean(dr["t314_controlhuecos"]);
        //            if(!Convert.IsDBNull(dr["fUltImputacion"]))
        //                oFicheroIAP_Usuarios.fUltImputacion=Convert.ToDateTime(dr["fUltImputacion"]);
        //            oFicheroIAP_Usuarios.t066_idcal=Convert.ToInt32(dr["t066_idcal"]);
        //            oFicheroIAP_Usuarios.t066_descal=Convert.ToString(dr["t066_descal"]);
        //            oFicheroIAP_Usuarios.t066_semlabL=Convert.ToInt32(dr["t066_semlabL"]);
        //            oFicheroIAP_Usuarios.t066_semlabM=Convert.ToInt32(dr["t066_semlabM"]);
        //            oFicheroIAP_Usuarios.t066_semlabX=Convert.ToInt32(dr["t066_semlabX"]);
        //            oFicheroIAP_Usuarios.t066_semlabJ=Convert.ToInt32(dr["t066_semlabJ"]);
        //            oFicheroIAP_Usuarios.t066_semlabV=Convert.ToInt32(dr["t066_semlabV"]);
        //            oFicheroIAP_Usuarios.t066_semlabS=Convert.ToInt32(dr["t066_semlabS"]);
        //            oFicheroIAP_Usuarios.t066_semlabD=Convert.ToInt32(dr["t066_semlabD"]);
        //            oFicheroIAP_Usuarios.t001_codred=Convert.ToString(dr["t001_codred"]);
        //            oFicheroIAP_Usuarios.t001_fecalta=Convert.ToDateTime(dr["t001_fecalta"]);
        //            oFicheroIAP_Usuarios.t001_fecbaja=Convert.ToDateTime(dr["t001_fecbaja"]);

        //            lst.Add(oFicheroIAP_Usuarios);

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
				case enumDBFields.t001_IDFICEPI:
					paramName = "@t001_IDFICEPI";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t314_idusuario:
					paramName = "@t314_idusuario";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.Profesional:
					paramName = "@Profesional";
					paramType = SqlDbType.VarChar;
					paramSize = 150;
					break;
				case enumDBFields.t303_ultcierreIAP:
					paramName = "@t303_ultcierreIAP";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t303_idnodo:
					paramName = "@t303_idnodo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t314_jornadareducida:
					paramName = "@t314_jornadareducida";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_horasjor_red:
					paramName = "@t314_horasjor_red";
					paramType = SqlDbType.Real;
					paramSize = 8;
					break;
				case enumDBFields.t314_fdesde_red:
					paramName = "@t314_fdesde_red";
					paramType = SqlDbType.DateTime;
					paramSize = 4;
					break;
				case enumDBFields.t314_fhasta_red:
					paramName = "@t314_fhasta_red";
					paramType = SqlDbType.DateTime;
					paramSize = 4;
					break;
				case enumDBFields.t314_controlhuecos:
					paramName = "@t314_controlhuecos";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.fUltImputacion:
					paramName = "@fUltImputacion";
					paramType = SqlDbType.DateTime;
					paramSize = 4;
					break;
				case enumDBFields.t066_idcal:
					paramName = "@t066_idcal";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t066_descal:
					paramName = "@t066_descal";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t066_semlabL:
					paramName = "@t066_semlabL";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t066_semlabM:
					paramName = "@t066_semlabM";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t066_semlabX:
					paramName = "@t066_semlabX";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t066_semlabJ:
					paramName = "@t066_semlabJ";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t066_semlabV:
					paramName = "@t066_semlabV";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t066_semlabS:
					paramName = "@t066_semlabS";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t066_semlabD:
					paramName = "@t066_semlabD";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t001_codred:
					paramName = "@t001_codred";
					paramType = SqlDbType.VarChar;
					paramSize = 15;
					break;
				case enumDBFields.t001_fecalta:
					paramName = "@t001_fecalta";
					paramType = SqlDbType.DateTime;
					paramSize = 4;
					break;
				case enumDBFields.t001_fecbaja:
					paramName = "@t001_fecbaja";
					paramType = SqlDbType.DateTime;
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
