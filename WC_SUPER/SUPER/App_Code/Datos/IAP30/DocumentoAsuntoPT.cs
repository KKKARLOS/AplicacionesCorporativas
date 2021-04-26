using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for DocumentoAsuntoPT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class DocumentoAsuntoPT 
    {
        //#region variables privadas y constructor
        //private sqldblib.SqlServerSP cDblib;
		
		
        //private enum enumDBFields : byte
        //{
        //    T409_idasunto = 1,
        //    t314_idusuario_autor = 2,
        //    autor = 3,
        //    T411_autormodif = 4,
        //    T411_descripcion = 5,
        //    T411_fecha = 6,
        //    T411_fechamodif = 7,
        //    T411_iddocasu = 8,
        //    T411_modolectura = 9,
        //    T411_nombrearchivo = 10,
        //    T411_privado = 11,
        //    T411_tipogestion = 12,
        //    T411_weblink = 13
        //}

        //internal DocumentoAsuntoPT(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un DocumentoAsuntoPT
        ///// </summary>
        //internal int Insert(Models.DocumentoAsuntoPT oDocumentoAsuntoPT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.T409_idasunto, oDocumentoAsuntoPT.T409_idasunto),
        //            Param(enumDBFields.t314_idusuario_autor, oDocumentoAsuntoPT.t314_idusuario_autor),
        //            Param(enumDBFields.autor, oDocumentoAsuntoPT.autor),
        //            Param(enumDBFields.T411_autormodif, oDocumentoAsuntoPT.T411_autormodif),
        //            Param(enumDBFields.T411_descripcion, oDocumentoAsuntoPT.T411_descripcion),
        //            Param(enumDBFields.T411_fecha, oDocumentoAsuntoPT.T411_fecha),
        //            Param(enumDBFields.T411_fechamodif, oDocumentoAsuntoPT.T411_fechamodif),
        //            Param(enumDBFields.T411_iddocasu, oDocumentoAsuntoPT.T411_iddocasu),
        //            Param(enumDBFields.T411_modolectura, oDocumentoAsuntoPT.T411_modolectura),
        //            Param(enumDBFields.T411_nombrearchivo, oDocumentoAsuntoPT.T411_nombrearchivo),
        //            Param(enumDBFields.T411_privado, oDocumentoAsuntoPT.T411_privado),
        //            Param(enumDBFields.T411_tipogestion, oDocumentoAsuntoPT.T411_tipogestion),
        //            Param(enumDBFields.T411_weblink, oDocumentoAsuntoPT.T411_weblink)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_DocumentoAsuntoPT_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un DocumentoAsuntoPT a partir del id
        ///// </summary>
        //internal Models.DocumentoAsuntoPT Select()
        //{
        //    Models.DocumentoAsuntoPT oDocumentoAsuntoPT = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_DocumentoAsuntoPT_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oDocumentoAsuntoPT = new Models.DocumentoAsuntoPT();
        //            oDocumentoAsuntoPT.T409_idasunto=Convert.ToInt32(dr["T409_idasunto"]);
        //            oDocumentoAsuntoPT.t314_idusuario_autor=Convert.ToInt32(dr["t314_idusuario_autor"]);
        //            oDocumentoAsuntoPT.autor=Convert.ToString(dr["autor"]);
        //            oDocumentoAsuntoPT.T411_autormodif=Convert.ToInt32(dr["T411_autormodif"]);
        //            oDocumentoAsuntoPT.T411_descripcion=Convert.ToString(dr["T411_descripcion"]);
        //            oDocumentoAsuntoPT.T411_fecha=Convert.ToDateTime(dr["T411_fecha"]);
        //            oDocumentoAsuntoPT.T411_fechamodif=Convert.ToDateTime(dr["T411_fechamodif"]);
        //            oDocumentoAsuntoPT.T411_iddocasu=Convert.ToInt32(dr["T411_iddocasu"]);
        //            oDocumentoAsuntoPT.T411_modolectura=Convert.ToBoolean(dr["T411_modolectura"]);
        //            oDocumentoAsuntoPT.T411_nombrearchivo=Convert.ToString(dr["T411_nombrearchivo"]);
        //            oDocumentoAsuntoPT.T411_privado=Convert.ToBoolean(dr["T411_privado"]);
        //            oDocumentoAsuntoPT.T411_tipogestion=Convert.ToBoolean(dr["T411_tipogestion"]);
        //            oDocumentoAsuntoPT.T411_weblink=Convert.ToString(dr["T411_weblink"]);

        //        }
        //        return oDocumentoAsuntoPT;
				
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
        ///// Actualiza un DocumentoAsuntoPT a partir del id
        ///// </summary>
        //internal int Update(Models.DocumentoAsuntoPT oDocumentoAsuntoPT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.T409_idasunto, oDocumentoAsuntoPT.T409_idasunto),
        //            Param(enumDBFields.t314_idusuario_autor, oDocumentoAsuntoPT.t314_idusuario_autor),
        //            Param(enumDBFields.autor, oDocumentoAsuntoPT.autor),
        //            Param(enumDBFields.T411_autormodif, oDocumentoAsuntoPT.T411_autormodif),
        //            Param(enumDBFields.T411_descripcion, oDocumentoAsuntoPT.T411_descripcion),
        //            Param(enumDBFields.T411_fecha, oDocumentoAsuntoPT.T411_fecha),
        //            Param(enumDBFields.T411_fechamodif, oDocumentoAsuntoPT.T411_fechamodif),
        //            Param(enumDBFields.T411_iddocasu, oDocumentoAsuntoPT.T411_iddocasu),
        //            Param(enumDBFields.T411_modolectura, oDocumentoAsuntoPT.T411_modolectura),
        //            Param(enumDBFields.T411_nombrearchivo, oDocumentoAsuntoPT.T411_nombrearchivo),
        //            Param(enumDBFields.T411_privado, oDocumentoAsuntoPT.T411_privado),
        //            Param(enumDBFields.T411_tipogestion, oDocumentoAsuntoPT.T411_tipogestion),
        //            Param(enumDBFields.T411_weblink, oDocumentoAsuntoPT.T411_weblink)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_DocumentoAsuntoPT_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un DocumentoAsuntoPT a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_DocumentoAsuntoPT_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los DocumentoAsuntoPT
        ///// </summary>
        //internal List<Models.DocumentoAsuntoPT> Catalogo(Models.DocumentoAsuntoPT oDocumentoAsuntoPTFilter)
        //{
        //    Models.DocumentoAsuntoPT oDocumentoAsuntoPT = null;
        //    List<Models.DocumentoAsuntoPT> lst = new List<Models.DocumentoAsuntoPT>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.T409_idasunto, oTEMP_DocumentoAsuntoPTFilter.T409_idasunto),
        //            Param(enumDBFields.t314_idusuario_autor, oTEMP_DocumentoAsuntoPTFilter.t314_idusuario_autor),
        //            Param(enumDBFields.autor, oTEMP_DocumentoAsuntoPTFilter.autor),
        //            Param(enumDBFields.T411_autormodif, oTEMP_DocumentoAsuntoPTFilter.T411_autormodif),
        //            Param(enumDBFields.T411_descripcion, oTEMP_DocumentoAsuntoPTFilter.T411_descripcion),
        //            Param(enumDBFields.T411_fecha, oTEMP_DocumentoAsuntoPTFilter.T411_fecha),
        //            Param(enumDBFields.T411_fechamodif, oTEMP_DocumentoAsuntoPTFilter.T411_fechamodif),
        //            Param(enumDBFields.T411_iddocasu, oTEMP_DocumentoAsuntoPTFilter.T411_iddocasu),
        //            Param(enumDBFields.T411_modolectura, oTEMP_DocumentoAsuntoPTFilter.T411_modolectura),
        //            Param(enumDBFields.T411_nombrearchivo, oTEMP_DocumentoAsuntoPTFilter.T411_nombrearchivo),
        //            Param(enumDBFields.T411_privado, oTEMP_DocumentoAsuntoPTFilter.T411_privado),
        //            Param(enumDBFields.T411_tipogestion, oTEMP_DocumentoAsuntoPTFilter.T411_tipogestion),
        //            Param(enumDBFields.T411_weblink, oTEMP_DocumentoAsuntoPTFilter.T411_weblink)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_DocumentoAsuntoPT_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oDocumentoAsuntoPT = new Models.DocumentoAsuntoPT();
        //            oDocumentoAsuntoPT.T409_idasunto=Convert.ToInt32(dr["T409_idasunto"]);
        //            oDocumentoAsuntoPT.t314_idusuario_autor=Convert.ToInt32(dr["t314_idusuario_autor"]);
        //            oDocumentoAsuntoPT.autor=Convert.ToString(dr["autor"]);
        //            oDocumentoAsuntoPT.T411_autormodif=Convert.ToInt32(dr["T411_autormodif"]);
        //            oDocumentoAsuntoPT.T411_descripcion=Convert.ToString(dr["T411_descripcion"]);
        //            oDocumentoAsuntoPT.T411_fecha=Convert.ToDateTime(dr["T411_fecha"]);
        //            oDocumentoAsuntoPT.T411_fechamodif=Convert.ToDateTime(dr["T411_fechamodif"]);
        //            oDocumentoAsuntoPT.T411_iddocasu=Convert.ToInt32(dr["T411_iddocasu"]);
        //            oDocumentoAsuntoPT.T411_modolectura=Convert.ToBoolean(dr["T411_modolectura"]);
        //            oDocumentoAsuntoPT.T411_nombrearchivo=Convert.ToString(dr["T411_nombrearchivo"]);
        //            oDocumentoAsuntoPT.T411_privado=Convert.ToBoolean(dr["T411_privado"]);
        //            oDocumentoAsuntoPT.T411_tipogestion=Convert.ToBoolean(dr["T411_tipogestion"]);
        //            oDocumentoAsuntoPT.T411_weblink=Convert.ToString(dr["T411_weblink"]);

        //            lst.Add(oDocumentoAsuntoPT);

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
        //        case enumDBFields.T409_idasunto:
        //            paramName = "@T409_idasunto";
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
        //        case enumDBFields.T411_autormodif:
        //            paramName = "@T411_autormodif";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.T411_descripcion:
        //            paramName = "@T411_descripcion";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 50;
        //            break;
        //        case enumDBFields.T411_fecha:
        //            paramName = "@T411_fecha";
        //            paramType = SqlDbType.DateTime;
        //            paramSize = 8;
        //            break;
        //        case enumDBFields.T411_fechamodif:
        //            paramName = "@T411_fechamodif";
        //            paramType = SqlDbType.DateTime;
        //            paramSize = 8;
        //            break;
        //        case enumDBFields.T411_iddocasu:
        //            paramName = "@T411_iddocasu";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.T411_modolectura:
        //            paramName = "@T411_modolectura";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.T411_nombrearchivo:
        //            paramName = "@T411_nombrearchivo";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 250;
        //            break;
        //        case enumDBFields.T411_privado:
        //            paramName = "@T411_privado";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.T411_tipogestion:
        //            paramName = "@T411_tipogestion";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.T411_weblink:
        //            paramName = "@T411_weblink";
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
