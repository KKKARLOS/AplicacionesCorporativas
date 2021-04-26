using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for DocumentoAccion
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class DocumentoAccion 
    {
        //#region variables privadas y constructor
        //private sqldblib.SqlServerSP cDblib;
		
		
        //private enum enumDBFields : byte
        //{
        //    T383_idaccion = 1,
        //    T314_idusuario_autor = 2,
        //    autor = 3,
        //    T387_autormodif = 4,
        //    T387_descripcion = 5,
        //    T387_fecha = 6,
        //    T387_fechamodif = 7,
        //    T387_iddocacc = 8,
        //    T387_modolectura = 9,
        //    T387_nombrearchivo = 10,
        //    T387_privado = 11,
        //    T387_tipogestion = 12,
        //    T387_weblink = 13
        //}

        //internal DocumentoAccion(sqldblib.SqlServerSP extcDblib)
        //{
        //    if(extcDblib == null)
        //        throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

        //    if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
        //        throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

        //    cDblib = extcDblib;
        //}
			
        //#endregion
	
        //#region funciones publicas
        ///// <summary>
        ///// Inserta un DocumentoAccion
        ///// </summary>
        //internal int Insert(Models.DocumentoAccion oDocumentoAccion)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.T383_idaccion, oDocumentoAccion.T383_idaccion),
        //            Param(enumDBFields.T314_idusuario_autor, oDocumentoAccion.T314_idusuario_autor),
        //            Param(enumDBFields.autor, oDocumentoAccion.autor),
        //            Param(enumDBFields.T387_autormodif, oDocumentoAccion.T387_autormodif),
        //            Param(enumDBFields.T387_descripcion, oDocumentoAccion.T387_descripcion),
        //            Param(enumDBFields.T387_fecha, oDocumentoAccion.T387_fecha),
        //            Param(enumDBFields.T387_fechamodif, oDocumentoAccion.T387_fechamodif),
        //            Param(enumDBFields.T387_iddocacc, oDocumentoAccion.T387_iddocacc),
        //            Param(enumDBFields.T387_modolectura, oDocumentoAccion.T387_modolectura),
        //            Param(enumDBFields.T387_nombrearchivo, oDocumentoAccion.T387_nombrearchivo),
        //            Param(enumDBFields.T387_privado, oDocumentoAccion.T387_privado),
        //            Param(enumDBFields.T387_tipogestion, oDocumentoAccion.T387_tipogestion),
        //            Param(enumDBFields.T387_weblink, oDocumentoAccion.T387_weblink)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_DocumentoAccion_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un DocumentoAccion a partir del id
        ///// </summary>
        //internal Models.DocumentoAccion Select()
        //{
        //    Models.DocumentoAccion oDocumentoAccion = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_DocumentoAccion_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oDocumentoAccion = new Models.DocumentoAccion();
        //            oDocumentoAccion.T383_idaccion=Convert.ToInt32(dr["T383_idaccion"]);
        //            oDocumentoAccion.T314_idusuario_autor=Convert.ToInt32(dr["T314_idusuario_autor"]);
        //            oDocumentoAccion.autor=Convert.ToString(dr["autor"]);
        //            oDocumentoAccion.T387_autormodif=Convert.ToInt32(dr["T387_autormodif"]);
        //            oDocumentoAccion.T387_descripcion=Convert.ToString(dr["T387_descripcion"]);
        //            oDocumentoAccion.T387_fecha=Convert.ToDateTime(dr["T387_fecha"]);
        //            oDocumentoAccion.T387_fechamodif=Convert.ToDateTime(dr["T387_fechamodif"]);
        //            oDocumentoAccion.T387_iddocacc=Convert.ToInt32(dr["T387_iddocacc"]);
        //            oDocumentoAccion.T387_modolectura=Convert.ToBoolean(dr["T387_modolectura"]);
        //            oDocumentoAccion.T387_nombrearchivo=Convert.ToString(dr["T387_nombrearchivo"]);
        //            oDocumentoAccion.T387_privado=Convert.ToBoolean(dr["T387_privado"]);
        //            oDocumentoAccion.T387_tipogestion=Convert.ToBoolean(dr["T387_tipogestion"]);
        //            oDocumentoAccion.T387_weblink=Convert.ToString(dr["T387_weblink"]);

        //        }
        //        return oDocumentoAccion;
				
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
        ///// Actualiza un DocumentoAccion a partir del id
        ///// </summary>
        //internal int Update(Models.DocumentoAccion oDocumentoAccion)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.T383_idaccion, oDocumentoAccion.T383_idaccion),
        //            Param(enumDBFields.T314_idusuario_autor, oDocumentoAccion.T314_idusuario_autor),
        //            Param(enumDBFields.autor, oDocumentoAccion.autor),
        //            Param(enumDBFields.T387_autormodif, oDocumentoAccion.T387_autormodif),
        //            Param(enumDBFields.T387_descripcion, oDocumentoAccion.T387_descripcion),
        //            Param(enumDBFields.T387_fecha, oDocumentoAccion.T387_fecha),
        //            Param(enumDBFields.T387_fechamodif, oDocumentoAccion.T387_fechamodif),
        //            Param(enumDBFields.T387_iddocacc, oDocumentoAccion.T387_iddocacc),
        //            Param(enumDBFields.T387_modolectura, oDocumentoAccion.T387_modolectura),
        //            Param(enumDBFields.T387_nombrearchivo, oDocumentoAccion.T387_nombrearchivo),
        //            Param(enumDBFields.T387_privado, oDocumentoAccion.T387_privado),
        //            Param(enumDBFields.T387_tipogestion, oDocumentoAccion.T387_tipogestion),
        //            Param(enumDBFields.T387_weblink, oDocumentoAccion.T387_weblink)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_DocumentoAccion_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un DocumentoAccion a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_DocumentoAccion_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los DocumentoAccion
        ///// </summary>
        //internal List<Models.DocumentoAccion> Catalogo(Models.DocumentoAccion oDocumentoAccionFilter)
        //{
        //    Models.DocumentoAccion oDocumentoAccion = null;
        //    List<Models.DocumentoAccion> lst = new List<Models.DocumentoAccion>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.T383_idaccion, oTEMP_DocumentoAccionFilter.T383_idaccion),
        //            Param(enumDBFields.T314_idusuario_autor, oTEMP_DocumentoAccionFilter.T314_idusuario_autor),
        //            Param(enumDBFields.autor, oTEMP_DocumentoAccionFilter.autor),
        //            Param(enumDBFields.T387_autormodif, oTEMP_DocumentoAccionFilter.T387_autormodif),
        //            Param(enumDBFields.T387_descripcion, oTEMP_DocumentoAccionFilter.T387_descripcion),
        //            Param(enumDBFields.T387_fecha, oTEMP_DocumentoAccionFilter.T387_fecha),
        //            Param(enumDBFields.T387_fechamodif, oTEMP_DocumentoAccionFilter.T387_fechamodif),
        //            Param(enumDBFields.T387_iddocacc, oTEMP_DocumentoAccionFilter.T387_iddocacc),
        //            Param(enumDBFields.T387_modolectura, oTEMP_DocumentoAccionFilter.T387_modolectura),
        //            Param(enumDBFields.T387_nombrearchivo, oTEMP_DocumentoAccionFilter.T387_nombrearchivo),
        //            Param(enumDBFields.T387_privado, oTEMP_DocumentoAccionFilter.T387_privado),
        //            Param(enumDBFields.T387_tipogestion, oTEMP_DocumentoAccionFilter.T387_tipogestion),
        //            Param(enumDBFields.T387_weblink, oTEMP_DocumentoAccionFilter.T387_weblink)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_DocumentoAccion_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oDocumentoAccion = new Models.DocumentoAccion();
        //            oDocumentoAccion.T383_idaccion=Convert.ToInt32(dr["T383_idaccion"]);
        //            oDocumentoAccion.T314_idusuario_autor=Convert.ToInt32(dr["T314_idusuario_autor"]);
        //            oDocumentoAccion.autor=Convert.ToString(dr["autor"]);
        //            oDocumentoAccion.T387_autormodif=Convert.ToInt32(dr["T387_autormodif"]);
        //            oDocumentoAccion.T387_descripcion=Convert.ToString(dr["T387_descripcion"]);
        //            oDocumentoAccion.T387_fecha=Convert.ToDateTime(dr["T387_fecha"]);
        //            oDocumentoAccion.T387_fechamodif=Convert.ToDateTime(dr["T387_fechamodif"]);
        //            oDocumentoAccion.T387_iddocacc=Convert.ToInt32(dr["T387_iddocacc"]);
        //            oDocumentoAccion.T387_modolectura=Convert.ToBoolean(dr["T387_modolectura"]);
        //            oDocumentoAccion.T387_nombrearchivo=Convert.ToString(dr["T387_nombrearchivo"]);
        //            oDocumentoAccion.T387_privado=Convert.ToBoolean(dr["T387_privado"]);
        //            oDocumentoAccion.T387_tipogestion=Convert.ToBoolean(dr["T387_tipogestion"]);
        //            oDocumentoAccion.T387_weblink=Convert.ToString(dr["T387_weblink"]);

        //            lst.Add(oDocumentoAccion);

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
        //private SqlParameter Param(enumDBFields dbField, object value)
        //{
        //    SqlParameter dbParam = null;
        //    string paramName = null;
        //    SqlDbType paramType = default(SqlDbType);
        //    int paramSize = 0;
        //    ParameterDirection paramDirection = ParameterDirection.Input;
			
        //    switch (dbField)
        //    {
        //        case enumDBFields.T383_idaccion:
        //            paramName = "@T383_idaccion";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.T314_idusuario_autor:
        //            paramName = "@T314_idusuario_autor";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.autor:
        //            paramName = "@autor";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 73;
        //            break;
        //        case enumDBFields.T387_autormodif:
        //            paramName = "@T387_autormodif";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.T387_descripcion:
        //            paramName = "@T387_descripcion";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 50;
        //            break;
        //        case enumDBFields.T387_fecha:
        //            paramName = "@T387_fecha";
        //            paramType = SqlDbType.DateTime;
        //            paramSize = 8;
        //            break;
        //        case enumDBFields.T387_fechamodif:
        //            paramName = "@T387_fechamodif";
        //            paramType = SqlDbType.DateTime;
        //            paramSize = 8;
        //            break;
        //        case enumDBFields.T387_iddocacc:
        //            paramName = "@T387_iddocacc";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.T387_modolectura:
        //            paramName = "@T387_modolectura";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.T387_nombrearchivo:
        //            paramName = "@T387_nombrearchivo";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 250;
        //            break;
        //        case enumDBFields.T387_privado:
        //            paramName = "@T387_privado";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.T387_tipogestion:
        //            paramName = "@T387_tipogestion";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.T387_weblink:
        //            paramName = "@T387_weblink";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 250;
        //            break;
        //    }


        //    dbParam = cDblib.dbParameter(paramName, paramType, paramSize);
        //    dbParam.Direction = paramDirection;
        //    if (paramDirection == ParameterDirection.Input | paramDirection == ParameterDirection.InputOutput) dbParam.Value = value;

        //    return dbParam;

        //}
		
        //#endregion
    
    }

}
