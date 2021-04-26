using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for DocutC3
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class DocutC3 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t363_iddocut = 1,
			t332_idtarea = 2,
			t363_descripcion = 3,
			t363_weblink = 4,
			t363_nombrearchivo = 5,
			t363_privado = 6,
			t363_modolectura = 7,
			t363_tipogestion = 8,
			t314_idusuario_autor = 9,
			autor = 10
        }

        internal DocutC3(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un DocutC3
        ///// </summary>
        //internal int Insert(Models.DocutC3 oDocutC3)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[10] {
        //            Param(enumDBFields.t363_iddocut, oDocutC3.t363_iddocut),
        //            Param(enumDBFields.t332_idtarea, oDocutC3.t332_idtarea),
        //            Param(enumDBFields.t363_descripcion, oDocutC3.t363_descripcion),
        //            Param(enumDBFields.t363_weblink, oDocutC3.t363_weblink),
        //            Param(enumDBFields.t363_nombrearchivo, oDocutC3.t363_nombrearchivo),
        //            Param(enumDBFields.t363_privado, oDocutC3.t363_privado),
        //            Param(enumDBFields.t363_modolectura, oDocutC3.t363_modolectura),
        //            Param(enumDBFields.t363_tipogestion, oDocutC3.t363_tipogestion),
        //            Param(enumDBFields.t314_idusuario_autor, oDocutC3.t314_idusuario_autor),
        //            Param(enumDBFields.autor, oDocutC3.autor)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_DocutC3_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un DocutC3 a partir del id
        ///// </summary>
        //internal Models.DocutC3 Select()
        //{
        //    Models.DocutC3 oDocutC3 = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_DocutC3_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oDocutC3 = new Models.DocutC3();
        //            oDocutC3.t363_iddocut=Convert.ToInt32(dr["t363_iddocut"]);
        //            oDocutC3.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oDocutC3.t363_descripcion=Convert.ToString(dr["t363_descripcion"]);
        //            oDocutC3.t363_weblink=Convert.ToString(dr["t363_weblink"]);
        //            oDocutC3.t363_nombrearchivo=Convert.ToString(dr["t363_nombrearchivo"]);
        //            oDocutC3.t363_privado=Convert.ToBoolean(dr["t363_privado"]);
        //            oDocutC3.t363_modolectura=Convert.ToBoolean(dr["t363_modolectura"]);
        //            oDocutC3.t363_tipogestion=Convert.ToBoolean(dr["t363_tipogestion"]);
        //            oDocutC3.t314_idusuario_autor=Convert.ToInt32(dr["t314_idusuario_autor"]);
        //            oDocutC3.autor=Convert.ToString(dr["autor"]);

        //        }
        //        return oDocutC3;
				
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
        ///// Actualiza un DocutC3 a partir del id
        ///// </summary>
        //internal int Update(Models.DocutC3 oDocutC3)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[10] {
        //            Param(enumDBFields.t363_iddocut, oDocutC3.t363_iddocut),
        //            Param(enumDBFields.t332_idtarea, oDocutC3.t332_idtarea),
        //            Param(enumDBFields.t363_descripcion, oDocutC3.t363_descripcion),
        //            Param(enumDBFields.t363_weblink, oDocutC3.t363_weblink),
        //            Param(enumDBFields.t363_nombrearchivo, oDocutC3.t363_nombrearchivo),
        //            Param(enumDBFields.t363_privado, oDocutC3.t363_privado),
        //            Param(enumDBFields.t363_modolectura, oDocutC3.t363_modolectura),
        //            Param(enumDBFields.t363_tipogestion, oDocutC3.t363_tipogestion),
        //            Param(enumDBFields.t314_idusuario_autor, oDocutC3.t314_idusuario_autor),
        //            Param(enumDBFields.autor, oDocutC3.autor)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_DocutC3_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un DocutC3 a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_DocutC3_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Obtiene todos los DocutC3
        /// </summary>
        internal List<Models.DocutC3> Catalogo(Int32 idUsuarioAutorizado, Int32 idTarea)
        {
            Models.DocutC3 oDocutC3 = null;
            List<Models.DocutC3> lst = new List<Models.DocutC3>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t332_idtarea, idTarea),
                    Param(enumDBFields.t314_idusuario_autor, idUsuarioAutorizado)
                };

                dr = cDblib.DataReader("SUP_DOCUT_C3", dbparams);
                while (dr.Read())
                {
                    oDocutC3 = new Models.DocutC3();
                    oDocutC3.t363_iddocut = Convert.ToInt32(dr["t363_iddocut"]);
                    oDocutC3.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    oDocutC3.t363_descripcion = Convert.ToString(dr["t363_descripcion"]);
                    oDocutC3.t363_weblink = Convert.ToString(dr["t363_weblink"]);
                    oDocutC3.t363_nombrearchivo = Convert.ToString(dr["t363_nombrearchivo"]);
                    oDocutC3.t363_privado = Convert.ToBoolean(dr["t363_privado"]);
                    oDocutC3.t363_modolectura = Convert.ToBoolean(dr["t363_modolectura"]);
                    oDocutC3.t363_tipogestion = Convert.ToBoolean(dr["t363_tipogestion"]);
                    oDocutC3.t314_idusuario_autor = Convert.ToInt32(dr["t314_idusuario_autor"]);
                    oDocutC3.autor = Convert.ToString(dr["autor"]);

                    lst.Add(oDocutC3);

                }
                return lst;

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
				case enumDBFields.t363_iddocut:
					paramName = "@t363_iddocut";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_idtarea:
					paramName = "@t332_idtarea";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t363_descripcion:
					paramName = "@t363_descripcion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t363_weblink:
					paramName = "@t363_weblink";
					paramType = SqlDbType.VarChar;
					paramSize = 250;
					break;
				case enumDBFields.t363_nombrearchivo:
					paramName = "@t363_nombrearchivo";
					paramType = SqlDbType.VarChar;
					paramSize = 250;
					break;
				case enumDBFields.t363_privado:
					paramName = "@t363_privado";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t363_modolectura:
					paramName = "@t363_modolectura";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t363_tipogestion:
					paramName = "@t363_tipogestion";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_idusuario_autor:
					paramName = "@t314_idusuario_autor";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.autor:
					paramName = "@autor";
					paramType = SqlDbType.VarChar;
					paramSize = 73;
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
