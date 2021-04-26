using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for DocumentoAsunto
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class DocumentoAsunto 
    {
        //#region variables privadas y constructor
        //private sqldblib.SqlServerSP cDblib;
		
		
        //private enum enumDBFields : byte
        //{
        //    T382_idasunto = 1,
        //    t314_idusuario_autor = 2,
        //    autor = 3,
        //    T386_autormodif = 4,
        //    T386_descripcion = 5,
        //    T386_fecha = 6,
        //    T386_fechamodif = 7,
        //    T386_iddocasu = 8,
        //    T386_modolectura = 9,
        //    T386_nombrearchivo = 10,
        //    T386_privado = 11,
        //    T386_tipogestion = 12,
        //    T386_weblink = 13
        //}

        //internal DocumentoAsunto(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un DocumentoAsunto
        ///// </summary>
        //internal int Insert(Models.DocumentoAsunto oDocumentoAsunto)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.T382_idasunto, oDocumentoAsunto.T382_idasunto),
        //            Param(enumDBFields.t314_idusuario_autor, oDocumentoAsunto.t314_idusuario_autor),
        //            Param(enumDBFields.autor, oDocumentoAsunto.autor),
        //            Param(enumDBFields.T386_autormodif, oDocumentoAsunto.T386_autormodif),
        //            Param(enumDBFields.T386_descripcion, oDocumentoAsunto.T386_descripcion),
        //            Param(enumDBFields.T386_fecha, oDocumentoAsunto.T386_fecha),
        //            Param(enumDBFields.T386_fechamodif, oDocumentoAsunto.T386_fechamodif),
        //            Param(enumDBFields.T386_iddocasu, oDocumentoAsunto.T386_iddocasu),
        //            Param(enumDBFields.T386_modolectura, oDocumentoAsunto.T386_modolectura),
        //            Param(enumDBFields.T386_nombrearchivo, oDocumentoAsunto.T386_nombrearchivo),
        //            Param(enumDBFields.T386_privado, oDocumentoAsunto.T386_privado),
        //            Param(enumDBFields.T386_tipogestion, oDocumentoAsunto.T386_tipogestion),
        //            Param(enumDBFields.T386_weblink, oDocumentoAsunto.T386_weblink)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_DocumentoAsunto_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un DocumentoAsunto a partir del id
        ///// </summary>
        //internal Models.DocumentoAsunto Select()
        //{
        //    Models.DocumentoAsunto oDocumentoAsunto = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_DocumentoAsunto_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oDocumentoAsunto = new Models.DocumentoAsunto();
        //            oDocumentoAsunto.T382_idasunto=Convert.ToInt32(dr["T382_idasunto"]);
        //            oDocumentoAsunto.t314_idusuario_autor=Convert.ToInt32(dr["t314_idusuario_autor"]);
        //            oDocumentoAsunto.autor=Convert.ToString(dr["autor"]);
        //            oDocumentoAsunto.T386_autormodif=Convert.ToInt32(dr["T386_autormodif"]);
        //            oDocumentoAsunto.T386_descripcion=Convert.ToString(dr["T386_descripcion"]);
        //            oDocumentoAsunto.T386_fecha=Convert.ToDateTime(dr["T386_fecha"]);
        //            oDocumentoAsunto.T386_fechamodif=Convert.ToDateTime(dr["T386_fechamodif"]);
        //            oDocumentoAsunto.T386_iddocasu=Convert.ToInt32(dr["T386_iddocasu"]);
        //            oDocumentoAsunto.T386_modolectura=Convert.ToBoolean(dr["T386_modolectura"]);
        //            oDocumentoAsunto.T386_nombrearchivo=Convert.ToString(dr["T386_nombrearchivo"]);
        //            oDocumentoAsunto.T386_privado=Convert.ToBoolean(dr["T386_privado"]);
        //            oDocumentoAsunto.T386_tipogestion=Convert.ToBoolean(dr["T386_tipogestion"]);
        //            oDocumentoAsunto.T386_weblink=Convert.ToString(dr["T386_weblink"]);

        //        }
        //        return oDocumentoAsunto;
				
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
        ///// Actualiza un DocumentoAsunto a partir del id
        ///// </summary>
        //internal int Update(Models.DocumentoAsunto oDocumentoAsunto)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.T382_idasunto, oDocumentoAsunto.T382_idasunto),
        //            Param(enumDBFields.t314_idusuario_autor, oDocumentoAsunto.t314_idusuario_autor),
        //            Param(enumDBFields.autor, oDocumentoAsunto.autor),
        //            Param(enumDBFields.T386_autormodif, oDocumentoAsunto.T386_autormodif),
        //            Param(enumDBFields.T386_descripcion, oDocumentoAsunto.T386_descripcion),
        //            Param(enumDBFields.T386_fecha, oDocumentoAsunto.T386_fecha),
        //            Param(enumDBFields.T386_fechamodif, oDocumentoAsunto.T386_fechamodif),
        //            Param(enumDBFields.T386_iddocasu, oDocumentoAsunto.T386_iddocasu),
        //            Param(enumDBFields.T386_modolectura, oDocumentoAsunto.T386_modolectura),
        //            Param(enumDBFields.T386_nombrearchivo, oDocumentoAsunto.T386_nombrearchivo),
        //            Param(enumDBFields.T386_privado, oDocumentoAsunto.T386_privado),
        //            Param(enumDBFields.T386_tipogestion, oDocumentoAsunto.T386_tipogestion),
        //            Param(enumDBFields.T386_weblink, oDocumentoAsunto.T386_weblink)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_DocumentoAsunto_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un DocumentoAsunto a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_DocumentoAsunto_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los DocumentoAsunto
        ///// </summary>
        //internal List<Models.DocumentoAsunto> Catalogo(Models.DocumentoAsunto oDocumentoAsuntoFilter)
        //{
        //    Models.DocumentoAsunto oDocumentoAsunto = null;
        //    List<Models.DocumentoAsunto> lst = new List<Models.DocumentoAsunto>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.T382_idasunto, oTEMP_DocumentoAsuntoFilter.T382_idasunto),
        //            Param(enumDBFields.t314_idusuario_autor, oTEMP_DocumentoAsuntoFilter.t314_idusuario_autor),
        //            Param(enumDBFields.autor, oTEMP_DocumentoAsuntoFilter.autor),
        //            Param(enumDBFields.T386_autormodif, oTEMP_DocumentoAsuntoFilter.T386_autormodif),
        //            Param(enumDBFields.T386_descripcion, oTEMP_DocumentoAsuntoFilter.T386_descripcion),
        //            Param(enumDBFields.T386_fecha, oTEMP_DocumentoAsuntoFilter.T386_fecha),
        //            Param(enumDBFields.T386_fechamodif, oTEMP_DocumentoAsuntoFilter.T386_fechamodif),
        //            Param(enumDBFields.T386_iddocasu, oTEMP_DocumentoAsuntoFilter.T386_iddocasu),
        //            Param(enumDBFields.T386_modolectura, oTEMP_DocumentoAsuntoFilter.T386_modolectura),
        //            Param(enumDBFields.T386_nombrearchivo, oTEMP_DocumentoAsuntoFilter.T386_nombrearchivo),
        //            Param(enumDBFields.T386_privado, oTEMP_DocumentoAsuntoFilter.T386_privado),
        //            Param(enumDBFields.T386_tipogestion, oTEMP_DocumentoAsuntoFilter.T386_tipogestion),
        //            Param(enumDBFields.T386_weblink, oTEMP_DocumentoAsuntoFilter.T386_weblink)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_DocumentoAsunto_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oDocumentoAsunto = new Models.DocumentoAsunto();
        //            oDocumentoAsunto.T382_idasunto=Convert.ToInt32(dr["T382_idasunto"]);
        //            oDocumentoAsunto.t314_idusuario_autor=Convert.ToInt32(dr["t314_idusuario_autor"]);
        //            oDocumentoAsunto.autor=Convert.ToString(dr["autor"]);
        //            oDocumentoAsunto.T386_autormodif=Convert.ToInt32(dr["T386_autormodif"]);
        //            oDocumentoAsunto.T386_descripcion=Convert.ToString(dr["T386_descripcion"]);
        //            oDocumentoAsunto.T386_fecha=Convert.ToDateTime(dr["T386_fecha"]);
        //            oDocumentoAsunto.T386_fechamodif=Convert.ToDateTime(dr["T386_fechamodif"]);
        //            oDocumentoAsunto.T386_iddocasu=Convert.ToInt32(dr["T386_iddocasu"]);
        //            oDocumentoAsunto.T386_modolectura=Convert.ToBoolean(dr["T386_modolectura"]);
        //            oDocumentoAsunto.T386_nombrearchivo=Convert.ToString(dr["T386_nombrearchivo"]);
        //            oDocumentoAsunto.T386_privado=Convert.ToBoolean(dr["T386_privado"]);
        //            oDocumentoAsunto.T386_tipogestion=Convert.ToBoolean(dr["T386_tipogestion"]);
        //            oDocumentoAsunto.T386_weblink=Convert.ToString(dr["T386_weblink"]);

        //            lst.Add(oDocumentoAsunto);

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
        //        case enumDBFields.T382_idasunto:
        //            paramName = "@T382_idasunto";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.t314_idusuario_autor:
        //            paramName = "@t314_idusuario_autor";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.autor:
        //            paramName = "@autor";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 73;
        //            break;
        //        case enumDBFields.T386_autormodif:
        //            paramName = "@T386_autormodif";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.T386_descripcion:
        //            paramName = "@T386_descripcion";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 50;
        //            break;
        //        case enumDBFields.T386_fecha:
        //            paramName = "@T386_fecha";
        //            paramType = SqlDbType.DateTime;
        //            paramSize = 8;
        //            break;
        //        case enumDBFields.T386_fechamodif:
        //            paramName = "@T386_fechamodif";
        //            paramType = SqlDbType.DateTime;
        //            paramSize = 8;
        //            break;
        //        case enumDBFields.T386_iddocasu:
        //            paramName = "@T386_iddocasu";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.T386_modolectura:
        //            paramName = "@T386_modolectura";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.T386_nombrearchivo:
        //            paramName = "@T386_nombrearchivo";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 250;
        //            break;
        //        case enumDBFields.T386_privado:
        //            paramName = "@T386_privado";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.T386_tipogestion:
        //            paramName = "@T386_tipogestion";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.T386_weblink:
        //            paramName = "@T386_weblink";
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
