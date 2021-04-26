using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for DocumentoAsuntoT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class DocumentoAsuntoT 
    {
        //#region variables privadas y constructor
        //private sqldblib.SqlServerSP cDblib;
		
		
        //private enum enumDBFields : byte
        //{
        //    T600_idasunto = 1,
        //    t314_idusuario_autor = 2,
        //    autor = 3,
        //    T602_autormodif = 4,
        //    T602_descripcion = 5,
        //    T602_fecha = 6,
        //    T602_fechamodif = 7,
        //    T602_iddocasu = 8,
        //    T602_modolectura = 9,
        //    T602_nombrearchivo = 10,
        //    T602_privado = 11,
        //    T602_tipogestion = 12,
        //    T602_weblink = 13
        //}

        //internal DocumentoAsuntoT(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un DocumentoAsuntoT
        ///// </summary>
        //internal int Insert(Models.DocumentoAsuntoT oDocumentoAsuntoT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.T600_idasunto, oDocumentoAsuntoT.T600_idasunto),
        //            Param(enumDBFields.t314_idusuario_autor, oDocumentoAsuntoT.t314_idusuario_autor),
        //            Param(enumDBFields.autor, oDocumentoAsuntoT.autor),
        //            Param(enumDBFields.T602_autormodif, oDocumentoAsuntoT.T602_autormodif),
        //            Param(enumDBFields.T602_descripcion, oDocumentoAsuntoT.T602_descripcion),
        //            Param(enumDBFields.T602_fecha, oDocumentoAsuntoT.T602_fecha),
        //            Param(enumDBFields.T602_fechamodif, oDocumentoAsuntoT.T602_fechamodif),
        //            Param(enumDBFields.T602_iddocasu, oDocumentoAsuntoT.T602_iddocasu),
        //            Param(enumDBFields.T602_modolectura, oDocumentoAsuntoT.T602_modolectura),
        //            Param(enumDBFields.T602_nombrearchivo, oDocumentoAsuntoT.T602_nombrearchivo),
        //            Param(enumDBFields.T602_privado, oDocumentoAsuntoT.T602_privado),
        //            Param(enumDBFields.T602_tipogestion, oDocumentoAsuntoT.T602_tipogestion),
        //            Param(enumDBFields.T602_weblink, oDocumentoAsuntoT.T602_weblink)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_DocumentoAsuntoT_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un DocumentoAsuntoT a partir del id
        ///// </summary>
        //internal Models.DocumentoAsuntoT Select()
        //{
        //    Models.DocumentoAsuntoT oDocumentoAsuntoT = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_DocumentoAsuntoT_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oDocumentoAsuntoT = new Models.DocumentoAsuntoT();
        //            oDocumentoAsuntoT.T600_idasunto=Convert.ToInt32(dr["T600_idasunto"]);
        //            oDocumentoAsuntoT.t314_idusuario_autor=Convert.ToInt32(dr["t314_idusuario_autor"]);
        //            oDocumentoAsuntoT.autor=Convert.ToString(dr["autor"]);
        //            oDocumentoAsuntoT.T602_autormodif=Convert.ToInt32(dr["T602_autormodif"]);
        //            oDocumentoAsuntoT.T602_descripcion=Convert.ToString(dr["T602_descripcion"]);
        //            oDocumentoAsuntoT.T602_fecha=Convert.ToDateTime(dr["T602_fecha"]);
        //            oDocumentoAsuntoT.T602_fechamodif=Convert.ToDateTime(dr["T602_fechamodif"]);
        //            oDocumentoAsuntoT.T602_iddocasu=Convert.ToInt32(dr["T602_iddocasu"]);
        //            oDocumentoAsuntoT.T602_modolectura=Convert.ToBoolean(dr["T602_modolectura"]);
        //            oDocumentoAsuntoT.T602_nombrearchivo=Convert.ToString(dr["T602_nombrearchivo"]);
        //            oDocumentoAsuntoT.T602_privado=Convert.ToBoolean(dr["T602_privado"]);
        //            oDocumentoAsuntoT.T602_tipogestion=Convert.ToBoolean(dr["T602_tipogestion"]);
        //            oDocumentoAsuntoT.T602_weblink=Convert.ToString(dr["T602_weblink"]);

        //        }
        //        return oDocumentoAsuntoT;
				
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
        ///// Actualiza un DocumentoAsuntoT a partir del id
        ///// </summary>
        //internal int Update(Models.DocumentoAsuntoT oDocumentoAsuntoT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.T600_idasunto, oDocumentoAsuntoT.T600_idasunto),
        //            Param(enumDBFields.t314_idusuario_autor, oDocumentoAsuntoT.t314_idusuario_autor),
        //            Param(enumDBFields.autor, oDocumentoAsuntoT.autor),
        //            Param(enumDBFields.T602_autormodif, oDocumentoAsuntoT.T602_autormodif),
        //            Param(enumDBFields.T602_descripcion, oDocumentoAsuntoT.T602_descripcion),
        //            Param(enumDBFields.T602_fecha, oDocumentoAsuntoT.T602_fecha),
        //            Param(enumDBFields.T602_fechamodif, oDocumentoAsuntoT.T602_fechamodif),
        //            Param(enumDBFields.T602_iddocasu, oDocumentoAsuntoT.T602_iddocasu),
        //            Param(enumDBFields.T602_modolectura, oDocumentoAsuntoT.T602_modolectura),
        //            Param(enumDBFields.T602_nombrearchivo, oDocumentoAsuntoT.T602_nombrearchivo),
        //            Param(enumDBFields.T602_privado, oDocumentoAsuntoT.T602_privado),
        //            Param(enumDBFields.T602_tipogestion, oDocumentoAsuntoT.T602_tipogestion),
        //            Param(enumDBFields.T602_weblink, oDocumentoAsuntoT.T602_weblink)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_DocumentoAsuntoT_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un DocumentoAsuntoT a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_DocumentoAsuntoT_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los DocumentoAsuntoT
        ///// </summary>
        //internal List<Models.DocumentoAsuntoT> Catalogo(Models.DocumentoAsuntoT oDocumentoAsuntoTFilter)
        //{
        //    Models.DocumentoAsuntoT oDocumentoAsuntoT = null;
        //    List<Models.DocumentoAsuntoT> lst = new List<Models.DocumentoAsuntoT>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.T600_idasunto, oTEMP_DocumentoAsuntoTFilter.T600_idasunto),
        //            Param(enumDBFields.t314_idusuario_autor, oTEMP_DocumentoAsuntoTFilter.t314_idusuario_autor),
        //            Param(enumDBFields.autor, oTEMP_DocumentoAsuntoTFilter.autor),
        //            Param(enumDBFields.T602_autormodif, oTEMP_DocumentoAsuntoTFilter.T602_autormodif),
        //            Param(enumDBFields.T602_descripcion, oTEMP_DocumentoAsuntoTFilter.T602_descripcion),
        //            Param(enumDBFields.T602_fecha, oTEMP_DocumentoAsuntoTFilter.T602_fecha),
        //            Param(enumDBFields.T602_fechamodif, oTEMP_DocumentoAsuntoTFilter.T602_fechamodif),
        //            Param(enumDBFields.T602_iddocasu, oTEMP_DocumentoAsuntoTFilter.T602_iddocasu),
        //            Param(enumDBFields.T602_modolectura, oTEMP_DocumentoAsuntoTFilter.T602_modolectura),
        //            Param(enumDBFields.T602_nombrearchivo, oTEMP_DocumentoAsuntoTFilter.T602_nombrearchivo),
        //            Param(enumDBFields.T602_privado, oTEMP_DocumentoAsuntoTFilter.T602_privado),
        //            Param(enumDBFields.T602_tipogestion, oTEMP_DocumentoAsuntoTFilter.T602_tipogestion),
        //            Param(enumDBFields.T602_weblink, oTEMP_DocumentoAsuntoTFilter.T602_weblink)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_DocumentoAsuntoT_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oDocumentoAsuntoT = new Models.DocumentoAsuntoT();
        //            oDocumentoAsuntoT.T600_idasunto=Convert.ToInt32(dr["T600_idasunto"]);
        //            oDocumentoAsuntoT.t314_idusuario_autor=Convert.ToInt32(dr["t314_idusuario_autor"]);
        //            oDocumentoAsuntoT.autor=Convert.ToString(dr["autor"]);
        //            oDocumentoAsuntoT.T602_autormodif=Convert.ToInt32(dr["T602_autormodif"]);
        //            oDocumentoAsuntoT.T602_descripcion=Convert.ToString(dr["T602_descripcion"]);
        //            oDocumentoAsuntoT.T602_fecha=Convert.ToDateTime(dr["T602_fecha"]);
        //            oDocumentoAsuntoT.T602_fechamodif=Convert.ToDateTime(dr["T602_fechamodif"]);
        //            oDocumentoAsuntoT.T602_iddocasu=Convert.ToInt32(dr["T602_iddocasu"]);
        //            oDocumentoAsuntoT.T602_modolectura=Convert.ToBoolean(dr["T602_modolectura"]);
        //            oDocumentoAsuntoT.T602_nombrearchivo=Convert.ToString(dr["T602_nombrearchivo"]);
        //            oDocumentoAsuntoT.T602_privado=Convert.ToBoolean(dr["T602_privado"]);
        //            oDocumentoAsuntoT.T602_tipogestion=Convert.ToBoolean(dr["T602_tipogestion"]);
        //            oDocumentoAsuntoT.T602_weblink=Convert.ToString(dr["T602_weblink"]);

        //            lst.Add(oDocumentoAsuntoT);

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
        //        case enumDBFields.T600_idasunto:
        //            paramName = "@T600_idasunto";
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
        //        case enumDBFields.T602_autormodif:
        //            paramName = "@T602_autormodif";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.T602_descripcion:
        //            paramName = "@T602_descripcion";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 50;
        //            break;
        //        case enumDBFields.T602_fecha:
        //            paramName = "@T602_fecha";
        //            paramType = SqlDbType.DateTime;
        //            paramSize = 8;
        //            break;
        //        case enumDBFields.T602_fechamodif:
        //            paramName = "@T602_fechamodif";
        //            paramType = SqlDbType.DateTime;
        //            paramSize = 8;
        //            break;
        //        case enumDBFields.T602_iddocasu:
        //            paramName = "@T602_iddocasu";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.T602_modolectura:
        //            paramName = "@T602_modolectura";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.T602_nombrearchivo:
        //            paramName = "@T602_nombrearchivo";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 250;
        //            break;
        //        case enumDBFields.T602_privado:
        //            paramName = "@T602_privado";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.T602_tipogestion:
        //            paramName = "@T602_tipogestion";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.T602_weblink:
        //            paramName = "@T602_weblink";
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
