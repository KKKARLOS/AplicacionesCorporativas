using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for DocumentoAccionPT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class DocumentoAccionPT 
    {
        //#region variables privadas y constructor
        //private sqldblib.SqlServerSP cDblib;
		
		
        //private enum enumDBFields : byte
        //{
        //    T410_idaccion = 1,
        //    T314_idusuario_autor = 2,
        //    autor = 3,
        //    T412_autormodif = 4,
        //    T412_descripcion = 5,
        //    T412_fecha = 6,
        //    T412_fechamodif = 7,
        //    T412_iddocacc = 8,
        //    T412_modolectura = 9,
        //    T412_nombrearchivo = 10,
        //    T412_privado = 11,
        //    T412_tipogestion = 12,
        //    T412_weblink = 13
        //}

        //internal DocumentoAccionPT(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un DocumentoAccionPT
        ///// </summary>
        //internal int Insert(Models.DocumentoAccionPT oDocumentoAccionPT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.T410_idaccion, oDocumentoAccionPT.T410_idaccion),
        //            Param(enumDBFields.T314_idusuario_autor, oDocumentoAccionPT.T314_idusuario_autor),
        //            Param(enumDBFields.autor, oDocumentoAccionPT.autor),
        //            Param(enumDBFields.T412_autormodif, oDocumentoAccionPT.T412_autormodif),
        //            Param(enumDBFields.T412_descripcion, oDocumentoAccionPT.T412_descripcion),
        //            Param(enumDBFields.T412_fecha, oDocumentoAccionPT.T412_fecha),
        //            Param(enumDBFields.T412_fechamodif, oDocumentoAccionPT.T412_fechamodif),
        //            Param(enumDBFields.T412_iddocacc, oDocumentoAccionPT.T412_iddocacc),
        //            Param(enumDBFields.T412_modolectura, oDocumentoAccionPT.T412_modolectura),
        //            Param(enumDBFields.T412_nombrearchivo, oDocumentoAccionPT.T412_nombrearchivo),
        //            Param(enumDBFields.T412_privado, oDocumentoAccionPT.T412_privado),
        //            Param(enumDBFields.T412_tipogestion, oDocumentoAccionPT.T412_tipogestion),
        //            Param(enumDBFields.T412_weblink, oDocumentoAccionPT.T412_weblink)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_DocumentoAccionPT_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un DocumentoAccionPT a partir del id
        ///// </summary>
        //internal Models.DocumentoAccionPT Select()
        //{
        //    Models.DocumentoAccionPT oDocumentoAccionPT = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_DocumentoAccionPT_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oDocumentoAccionPT = new Models.DocumentoAccionPT();
        //            oDocumentoAccionPT.T410_idaccion=Convert.ToInt32(dr["T410_idaccion"]);
        //            oDocumentoAccionPT.T314_idusuario_autor=Convert.ToInt32(dr["T314_idusuario_autor"]);
        //            oDocumentoAccionPT.autor=Convert.ToString(dr["autor"]);
        //            oDocumentoAccionPT.T412_autormodif=Convert.ToInt32(dr["T412_autormodif"]);
        //            oDocumentoAccionPT.T412_descripcion=Convert.ToString(dr["T412_descripcion"]);
        //            oDocumentoAccionPT.T412_fecha=Convert.ToDateTime(dr["T412_fecha"]);
        //            oDocumentoAccionPT.T412_fechamodif=Convert.ToDateTime(dr["T412_fechamodif"]);
        //            oDocumentoAccionPT.T412_iddocacc=Convert.ToInt32(dr["T412_iddocacc"]);
        //            oDocumentoAccionPT.T412_modolectura=Convert.ToBoolean(dr["T412_modolectura"]);
        //            oDocumentoAccionPT.T412_nombrearchivo=Convert.ToString(dr["T412_nombrearchivo"]);
        //            oDocumentoAccionPT.T412_privado=Convert.ToBoolean(dr["T412_privado"]);
        //            oDocumentoAccionPT.T412_tipogestion=Convert.ToBoolean(dr["T412_tipogestion"]);
        //            oDocumentoAccionPT.T412_weblink=Convert.ToString(dr["T412_weblink"]);

        //        }
        //        return oDocumentoAccionPT;
				
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
        ///// Actualiza un DocumentoAccionPT a partir del id
        ///// </summary>
        //internal int Update(Models.DocumentoAccionPT oDocumentoAccionPT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.T410_idaccion, oDocumentoAccionPT.T410_idaccion),
        //            Param(enumDBFields.T314_idusuario_autor, oDocumentoAccionPT.T314_idusuario_autor),
        //            Param(enumDBFields.autor, oDocumentoAccionPT.autor),
        //            Param(enumDBFields.T412_autormodif, oDocumentoAccionPT.T412_autormodif),
        //            Param(enumDBFields.T412_descripcion, oDocumentoAccionPT.T412_descripcion),
        //            Param(enumDBFields.T412_fecha, oDocumentoAccionPT.T412_fecha),
        //            Param(enumDBFields.T412_fechamodif, oDocumentoAccionPT.T412_fechamodif),
        //            Param(enumDBFields.T412_iddocacc, oDocumentoAccionPT.T412_iddocacc),
        //            Param(enumDBFields.T412_modolectura, oDocumentoAccionPT.T412_modolectura),
        //            Param(enumDBFields.T412_nombrearchivo, oDocumentoAccionPT.T412_nombrearchivo),
        //            Param(enumDBFields.T412_privado, oDocumentoAccionPT.T412_privado),
        //            Param(enumDBFields.T412_tipogestion, oDocumentoAccionPT.T412_tipogestion),
        //            Param(enumDBFields.T412_weblink, oDocumentoAccionPT.T412_weblink)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_DocumentoAccionPT_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un DocumentoAccionPT a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_DocumentoAccionPT_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los DocumentoAccionPT
        ///// </summary>
        //internal List<Models.DocumentoAccionPT> Catalogo(Models.DocumentoAccionPT oDocumentoAccionPTFilter)
        //{
        //    Models.DocumentoAccionPT oDocumentoAccionPT = null;
        //    List<Models.DocumentoAccionPT> lst = new List<Models.DocumentoAccionPT>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.T410_idaccion, oTEMP_DocumentoAccionPTFilter.T410_idaccion),
        //            Param(enumDBFields.T314_idusuario_autor, oTEMP_DocumentoAccionPTFilter.T314_idusuario_autor),
        //            Param(enumDBFields.autor, oTEMP_DocumentoAccionPTFilter.autor),
        //            Param(enumDBFields.T412_autormodif, oTEMP_DocumentoAccionPTFilter.T412_autormodif),
        //            Param(enumDBFields.T412_descripcion, oTEMP_DocumentoAccionPTFilter.T412_descripcion),
        //            Param(enumDBFields.T412_fecha, oTEMP_DocumentoAccionPTFilter.T412_fecha),
        //            Param(enumDBFields.T412_fechamodif, oTEMP_DocumentoAccionPTFilter.T412_fechamodif),
        //            Param(enumDBFields.T412_iddocacc, oTEMP_DocumentoAccionPTFilter.T412_iddocacc),
        //            Param(enumDBFields.T412_modolectura, oTEMP_DocumentoAccionPTFilter.T412_modolectura),
        //            Param(enumDBFields.T412_nombrearchivo, oTEMP_DocumentoAccionPTFilter.T412_nombrearchivo),
        //            Param(enumDBFields.T412_privado, oTEMP_DocumentoAccionPTFilter.T412_privado),
        //            Param(enumDBFields.T412_tipogestion, oTEMP_DocumentoAccionPTFilter.T412_tipogestion),
        //            Param(enumDBFields.T412_weblink, oTEMP_DocumentoAccionPTFilter.T412_weblink)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_DocumentoAccionPT_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oDocumentoAccionPT = new Models.DocumentoAccionPT();
        //            oDocumentoAccionPT.T410_idaccion=Convert.ToInt32(dr["T410_idaccion"]);
        //            oDocumentoAccionPT.T314_idusuario_autor=Convert.ToInt32(dr["T314_idusuario_autor"]);
        //            oDocumentoAccionPT.autor=Convert.ToString(dr["autor"]);
        //            oDocumentoAccionPT.T412_autormodif=Convert.ToInt32(dr["T412_autormodif"]);
        //            oDocumentoAccionPT.T412_descripcion=Convert.ToString(dr["T412_descripcion"]);
        //            oDocumentoAccionPT.T412_fecha=Convert.ToDateTime(dr["T412_fecha"]);
        //            oDocumentoAccionPT.T412_fechamodif=Convert.ToDateTime(dr["T412_fechamodif"]);
        //            oDocumentoAccionPT.T412_iddocacc=Convert.ToInt32(dr["T412_iddocacc"]);
        //            oDocumentoAccionPT.T412_modolectura=Convert.ToBoolean(dr["T412_modolectura"]);
        //            oDocumentoAccionPT.T412_nombrearchivo=Convert.ToString(dr["T412_nombrearchivo"]);
        //            oDocumentoAccionPT.T412_privado=Convert.ToBoolean(dr["T412_privado"]);
        //            oDocumentoAccionPT.T412_tipogestion=Convert.ToBoolean(dr["T412_tipogestion"]);
        //            oDocumentoAccionPT.T412_weblink=Convert.ToString(dr["T412_weblink"]);

        //            lst.Add(oDocumentoAccionPT);

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
        //        case enumDBFields.T410_idaccion:
        //            paramName = "@T410_idaccion";
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
        //        case enumDBFields.T412_autormodif:
        //            paramName = "@T412_autormodif";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.T412_descripcion:
        //            paramName = "@T412_descripcion";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 50;
        //            break;
        //        case enumDBFields.T412_fecha:
        //            paramName = "@T412_fecha";
        //            paramType = SqlDbType.DateTime;
        //            paramSize = 8;
        //            break;
        //        case enumDBFields.T412_fechamodif:
        //            paramName = "@T412_fechamodif";
        //            paramType = SqlDbType.DateTime;
        //            paramSize = 8;
        //            break;
        //        case enumDBFields.T412_iddocacc:
        //            paramName = "@T412_iddocacc";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.T412_modolectura:
        //            paramName = "@T412_modolectura";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.T412_nombrearchivo:
        //            paramName = "@T412_nombrearchivo";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 250;
        //            break;
        //        case enumDBFields.T412_privado:
        //            paramName = "@T412_privado";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.T412_tipogestion:
        //            paramName = "@T412_tipogestion";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.T412_weblink:
        //            paramName = "@T412_weblink";
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
