using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for DocumentoAccionT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class DocumentoAccionT 
    {
        //#region variables privadas y constructor
        //private sqldblib.SqlServerSP cDblib;
		
		
        //private enum enumDBFields : byte
        //{
        //    T601_idaccion = 1,
        //    T314_idusuario_autor = 2,
        //    autor = 3,
        //    T603_autormodif = 4,
        //    T603_descripcion = 5,
        //    T603_fecha = 6,
        //    T603_fechamodif = 7,
        //    T603_iddocacc = 8,
        //    T603_modolectura = 9,
        //    T603_nombrearchivo = 10,
        //    T603_privado = 11,
        //    T603_tipogestion = 12,
        //    T603_weblink = 13
        //}

        //internal DocumentoAccionT(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un DocumentoAccionT
        ///// </summary>
        //internal int Insert(Models.DocumentoAccionT oDocumentoAccionT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.T601_idaccion, oDocumentoAccionT.T601_idaccion),
        //            Param(enumDBFields.T314_idusuario_autor, oDocumentoAccionT.T314_idusuario_autor),
        //            Param(enumDBFields.autor, oDocumentoAccionT.autor),
        //            Param(enumDBFields.T603_autormodif, oDocumentoAccionT.T603_autormodif),
        //            Param(enumDBFields.T603_descripcion, oDocumentoAccionT.T603_descripcion),
        //            Param(enumDBFields.T603_fecha, oDocumentoAccionT.T603_fecha),
        //            Param(enumDBFields.T603_fechamodif, oDocumentoAccionT.T603_fechamodif),
        //            Param(enumDBFields.T603_iddocacc, oDocumentoAccionT.T603_iddocacc),
        //            Param(enumDBFields.T603_modolectura, oDocumentoAccionT.T603_modolectura),
        //            Param(enumDBFields.T603_nombrearchivo, oDocumentoAccionT.T603_nombrearchivo),
        //            Param(enumDBFields.T603_privado, oDocumentoAccionT.T603_privado),
        //            Param(enumDBFields.T603_tipogestion, oDocumentoAccionT.T603_tipogestion),
        //            Param(enumDBFields.T603_weblink, oDocumentoAccionT.T603_weblink)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_DocumentoAccionT_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un DocumentoAccionT a partir del id
        ///// </summary>
        //internal Models.DocumentoAccionT Select()
        //{
        //    Models.DocumentoAccionT oDocumentoAccionT = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_DocumentoAccionT_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oDocumentoAccionT = new Models.DocumentoAccionT();
        //            oDocumentoAccionT.T601_idaccion=Convert.ToInt32(dr["T601_idaccion"]);
        //            oDocumentoAccionT.T314_idusuario_autor=Convert.ToInt32(dr["T314_idusuario_autor"]);
        //            oDocumentoAccionT.autor=Convert.ToString(dr["autor"]);
        //            oDocumentoAccionT.T603_autormodif=Convert.ToInt32(dr["T603_autormodif"]);
        //            oDocumentoAccionT.T603_descripcion=Convert.ToString(dr["T603_descripcion"]);
        //            oDocumentoAccionT.T603_fecha=Convert.ToDateTime(dr["T603_fecha"]);
        //            oDocumentoAccionT.T603_fechamodif=Convert.ToDateTime(dr["T603_fechamodif"]);
        //            oDocumentoAccionT.T603_iddocacc=Convert.ToInt32(dr["T603_iddocacc"]);
        //            oDocumentoAccionT.T603_modolectura=Convert.ToBoolean(dr["T603_modolectura"]);
        //            oDocumentoAccionT.T603_nombrearchivo=Convert.ToString(dr["T603_nombrearchivo"]);
        //            oDocumentoAccionT.T603_privado=Convert.ToBoolean(dr["T603_privado"]);
        //            oDocumentoAccionT.T603_tipogestion=Convert.ToBoolean(dr["T603_tipogestion"]);
        //            oDocumentoAccionT.T603_weblink=Convert.ToString(dr["T603_weblink"]);

        //        }
        //        return oDocumentoAccionT;
				
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
        ///// Actualiza un DocumentoAccionT a partir del id
        ///// </summary>
        //internal int Update(Models.DocumentoAccionT oDocumentoAccionT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.T601_idaccion, oDocumentoAccionT.T601_idaccion),
        //            Param(enumDBFields.T314_idusuario_autor, oDocumentoAccionT.T314_idusuario_autor),
        //            Param(enumDBFields.autor, oDocumentoAccionT.autor),
        //            Param(enumDBFields.T603_autormodif, oDocumentoAccionT.T603_autormodif),
        //            Param(enumDBFields.T603_descripcion, oDocumentoAccionT.T603_descripcion),
        //            Param(enumDBFields.T603_fecha, oDocumentoAccionT.T603_fecha),
        //            Param(enumDBFields.T603_fechamodif, oDocumentoAccionT.T603_fechamodif),
        //            Param(enumDBFields.T603_iddocacc, oDocumentoAccionT.T603_iddocacc),
        //            Param(enumDBFields.T603_modolectura, oDocumentoAccionT.T603_modolectura),
        //            Param(enumDBFields.T603_nombrearchivo, oDocumentoAccionT.T603_nombrearchivo),
        //            Param(enumDBFields.T603_privado, oDocumentoAccionT.T603_privado),
        //            Param(enumDBFields.T603_tipogestion, oDocumentoAccionT.T603_tipogestion),
        //            Param(enumDBFields.T603_weblink, oDocumentoAccionT.T603_weblink)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_DocumentoAccionT_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un DocumentoAccionT a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_DocumentoAccionT_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los DocumentoAccionT
        ///// </summary>
        //internal List<Models.DocumentoAccionT> Catalogo(Models.DocumentoAccionT oDocumentoAccionTFilter)
        //{
        //    Models.DocumentoAccionT oDocumentoAccionT = null;
        //    List<Models.DocumentoAccionT> lst = new List<Models.DocumentoAccionT>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.T601_idaccion, oTEMP_DocumentoAccionTFilter.T601_idaccion),
        //            Param(enumDBFields.T314_idusuario_autor, oTEMP_DocumentoAccionTFilter.T314_idusuario_autor),
        //            Param(enumDBFields.autor, oTEMP_DocumentoAccionTFilter.autor),
        //            Param(enumDBFields.T603_autormodif, oTEMP_DocumentoAccionTFilter.T603_autormodif),
        //            Param(enumDBFields.T603_descripcion, oTEMP_DocumentoAccionTFilter.T603_descripcion),
        //            Param(enumDBFields.T603_fecha, oTEMP_DocumentoAccionTFilter.T603_fecha),
        //            Param(enumDBFields.T603_fechamodif, oTEMP_DocumentoAccionTFilter.T603_fechamodif),
        //            Param(enumDBFields.T603_iddocacc, oTEMP_DocumentoAccionTFilter.T603_iddocacc),
        //            Param(enumDBFields.T603_modolectura, oTEMP_DocumentoAccionTFilter.T603_modolectura),
        //            Param(enumDBFields.T603_nombrearchivo, oTEMP_DocumentoAccionTFilter.T603_nombrearchivo),
        //            Param(enumDBFields.T603_privado, oTEMP_DocumentoAccionTFilter.T603_privado),
        //            Param(enumDBFields.T603_tipogestion, oTEMP_DocumentoAccionTFilter.T603_tipogestion),
        //            Param(enumDBFields.T603_weblink, oTEMP_DocumentoAccionTFilter.T603_weblink)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_DocumentoAccionT_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oDocumentoAccionT = new Models.DocumentoAccionT();
        //            oDocumentoAccionT.T601_idaccion=Convert.ToInt32(dr["T601_idaccion"]);
        //            oDocumentoAccionT.T314_idusuario_autor=Convert.ToInt32(dr["T314_idusuario_autor"]);
        //            oDocumentoAccionT.autor=Convert.ToString(dr["autor"]);
        //            oDocumentoAccionT.T603_autormodif=Convert.ToInt32(dr["T603_autormodif"]);
        //            oDocumentoAccionT.T603_descripcion=Convert.ToString(dr["T603_descripcion"]);
        //            oDocumentoAccionT.T603_fecha=Convert.ToDateTime(dr["T603_fecha"]);
        //            oDocumentoAccionT.T603_fechamodif=Convert.ToDateTime(dr["T603_fechamodif"]);
        //            oDocumentoAccionT.T603_iddocacc=Convert.ToInt32(dr["T603_iddocacc"]);
        //            oDocumentoAccionT.T603_modolectura=Convert.ToBoolean(dr["T603_modolectura"]);
        //            oDocumentoAccionT.T603_nombrearchivo=Convert.ToString(dr["T603_nombrearchivo"]);
        //            oDocumentoAccionT.T603_privado=Convert.ToBoolean(dr["T603_privado"]);
        //            oDocumentoAccionT.T603_tipogestion=Convert.ToBoolean(dr["T603_tipogestion"]);
        //            oDocumentoAccionT.T603_weblink=Convert.ToString(dr["T603_weblink"]);

        //            lst.Add(oDocumentoAccionT);

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
        //        case enumDBFields.T601_idaccion:
        //            paramName = "@T601_idaccion";
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
        //        case enumDBFields.T603_autormodif:
        //            paramName = "@T603_autormodif";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.T603_descripcion:
        //            paramName = "@T603_descripcion";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 50;
        //            break;
        //        case enumDBFields.T603_fecha:
        //            paramName = "@T603_fecha";
        //            paramType = SqlDbType.DateTime;
        //            paramSize = 8;
        //            break;
        //        case enumDBFields.T603_fechamodif:
        //            paramName = "@T603_fechamodif";
        //            paramType = SqlDbType.DateTime;
        //            paramSize = 8;
        //            break;
        //        case enumDBFields.T603_iddocacc:
        //            paramName = "@T603_iddocacc";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.T603_modolectura:
        //            paramName = "@T603_modolectura";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.T603_nombrearchivo:
        //            paramName = "@T603_nombrearchivo";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 250;
        //            break;
        //        case enumDBFields.T603_privado:
        //            paramName = "@T603_privado";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.T603_tipogestion:
        //            paramName = "@T603_tipogestion";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.T603_weblink:
        //            paramName = "@T603_weblink";
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
