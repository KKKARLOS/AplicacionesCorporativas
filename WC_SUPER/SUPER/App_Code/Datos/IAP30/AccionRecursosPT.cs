using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AccionRecursosPT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class AccionRecursosPT 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t314_idusuario = 1,
			MAIL = 2,
			nomRecurso = 3,
			t410_idaccion = 4,
			t414_notificar = 5,
			t001_sexo = 6,
			t303_idnodo = 7,
			baja = 8,
			tipo = 9
        }

        internal AccionRecursosPT(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion
	
        #region funciones publicas
        /// <summary>
        /// Inserta un AccionRecursosPT
        /// </summary>
        internal int Insert(Models.AccionRecursosPT oAccionRecursosPT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t314_idusuario, oAccionRecursosPT.t314_idusuario),
                    Param(enumDBFields.t410_idaccion, oAccionRecursosPT.t410_idaccion),
                    Param(enumDBFields.t414_notificar, oAccionRecursosPT.t414_notificar)
                };

                return (int)cDblib.Execute("SUP_ACCIONRECURSOS_PT_I_SNE", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
        ///// <summary>
        ///// Obtiene un AccionRecursosPT a partir del id
        ///// </summary>
        //internal Models.AccionRecursosPT Select()
        //{
        //    Models.AccionRecursosPT oAccionRecursosPT = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_AccionRecursosPT_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oAccionRecursosPT = new Models.AccionRecursosPT();
        //            oAccionRecursosPT.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
        //            oAccionRecursosPT.MAIL=Convert.ToString(dr["MAIL"]);
        //            if(!Convert.IsDBNull(dr["nomRecurso"]))
        //                oAccionRecursosPT.nomRecurso=Convert.ToString(dr["nomRecurso"]);
        //            oAccionRecursosPT.t410_idaccion=Convert.ToInt32(dr["t410_idaccion"]);
        //            oAccionRecursosPT.t414_notificar=Convert.ToBoolean(dr["t414_notificar"]);
        //            oAccionRecursosPT.t001_sexo=Convert.ToString(dr["t001_sexo"]);
        //            if(!Convert.IsDBNull(dr["t303_idnodo"]))
        //                oAccionRecursosPT.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
        //            oAccionRecursosPT.baja=Convert.ToInt32(dr["baja"]);
        //            if(!Convert.IsDBNull(dr["tipo"]))
        //                oAccionRecursosPT.tipo=Convert.ToString(dr["tipo"]);

        //        }
        //        return oAccionRecursosPT;
				
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

        /// <summary>
        /// Actualiza un AccionRecursosPT a partir del id
        /// </summary>
        internal int Update(Models.AccionRecursosPT oAccionRecursosPT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t314_idusuario, oAccionRecursosPT.t314_idusuario),
                    Param(enumDBFields.t410_idaccion, oAccionRecursosPT.t410_idaccion),
                    Param(enumDBFields.t414_notificar, oAccionRecursosPT.t414_notificar)
                };

                return (int)cDblib.Execute("SUP_ACCIONRECURSOS_PT_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un AccionRecursosPT a partir del id
        /// </summary>
        internal int Delete(Models.AccionRecursosPT oAccionRecursosPT)
        {
            try
            {

                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t410_idaccion, oAccionRecursosPT.t410_idaccion),
                    Param(enumDBFields.t314_idusuario, oAccionRecursosPT.t314_idusuario)
                };

                return (int)cDblib.Execute("SUP_ACCIONRECURSOS_PT_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los AccionRecursos
        /// </summary>
        internal List<Models.AccionRecursosPT> Catalogo(Models.AccionRecursosPT oAccionRecursosFilter)
        {
            Models.AccionRecursosPT oAccionRecursos = null;
            List<Models.AccionRecursosPT> lst = new List<Models.AccionRecursosPT>();
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t410_idaccion, oAccionRecursosFilter.t410_idaccion)
                };

                dr = cDblib.DataReader("SUP_ACCIONRECURSOS_PT_SByT410_idaccion", dbparams);
                while (dr.Read())
                {
                    oAccionRecursos = new Models.AccionRecursosPT();
                    oAccionRecursos.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
                    if (!Convert.IsDBNull(dr["nomRecurso"]))
                        oAccionRecursos.nomRecurso = Convert.ToString(dr["nomRecurso"]);
                    oAccionRecursos.t410_idaccion = Convert.ToInt32(dr["t410_idaccion"]);
                    oAccionRecursos.t414_notificar = Convert.ToBoolean(dr["t414_notificar"]);
                    oAccionRecursos.MAIL = Convert.ToString(dr["MAIL"]);
                    oAccionRecursos.t001_sexo = Convert.ToString(dr["t001_sexo"]);
                    if (!Convert.IsDBNull(dr["t303_idnodo"]))
                        oAccionRecursos.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                    oAccionRecursos.baja = Convert.ToInt32(dr["baja"]);
                    if (!Convert.IsDBNull(dr["tipo"]))
                        oAccionRecursos.tipo = Convert.ToString(dr["tipo"]);

                    lst.Add(oAccionRecursos);

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
		
		
        #endregion

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
                case enumDBFields.nomRecurso:
                    paramName = "@nomRecurso";
                    paramType = SqlDbType.VarChar;
                    paramSize = 150;
                    break;
                case enumDBFields.t410_idaccion:
                    paramName = "@T410_idaccion";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t414_notificar:
                    paramName = "@T414_notificar";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.MAIL:
                    paramName = "@MAIL";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.t001_sexo:
                    paramName = "@t001_sexo";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.t303_idnodo:
                    paramName = "@t303_idnodo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.baja:
                    paramName = "@baja";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.tipo:
                    paramName = "@tipo";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
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
