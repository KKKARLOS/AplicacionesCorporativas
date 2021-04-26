using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Tecnicos
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class Tecnicos 
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;

        private enum enumDBFields : byte
        {
            Apellido1 = 1,
            Apellido2 = 2,
            Nombre = 3,
            idNodo = 4,
            nPSN = 5,
            Cualidad = 6,
            idTarea = 7,
            Foraneos = 8,
            SoloActivos = 9
        }

        internal Tecnicos(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
    //    #endregion
	
    //    #region funciones publicas
    //    /// <summary>
    //    /// Inserta un Tecnicos
    //    /// </summary>
    //    internal int Insert(Models.Tecnicos oTecnicos)
    //    {
    //        try
    //        {
    //            SqlParameter[] dbparams = new SqlParameter[12] {
    //                Param(enumDBFields.t314_idusuario, oTecnicos.t314_idusuario),
    //                Param(enumDBFields.Profesional, oTecnicos.Profesional),
    //                Param(enumDBFields.IdTarifa, oTecnicos.IdTarifa),
    //                Param(enumDBFields.t303_idnodo, oTecnicos.t303_idnodo),
    //                Param(enumDBFields.t001_sexo, oTecnicos.t001_sexo),
    //                Param(enumDBFields.t001_codred, oTecnicos.t001_codred),
    //                Param(enumDBFields.baja, oTecnicos.baja),
    //                Param(enumDBFields.EMPRESA, oTecnicos.EMPRESA),
    //                Param(enumDBFields.t303_denominacion, oTecnicos.t303_denominacion),
    //                Param(enumDBFields.tipo, oTecnicos.tipo),
    //                Param(enumDBFields.t001_email, oTecnicos.t001_email),
    //                Param(enumDBFields.MAIL, oTecnicos.MAIL)
    //            };

    //            return (int)cDblib.Execute("SUPER.IAP30_Tecnicos_INS", dbparams);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
		
    //    /// <summary>
    //    /// Obtiene un Tecnicos a partir del id
    //    /// </summary>
    //    internal Models.Tecnicos Select()
    //    {
    //        Models.Tecnicos oTecnicos = null;
    //        IDataReader dr = null;

    //        try
    //        {
				

    //            dr = cDblib.DataReader("SUPER.IAP30_Tecnicos_SEL", dbparams);
    //            if (dr.Read())
    //            {
    //                oTecnicos = new Models.Tecnicos();
    //                oTecnicos.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
    //                if(!Convert.IsDBNull(dr["Profesional"]))
    //                    oTecnicos.Profesional=Convert.ToString(dr["Profesional"]);
    //                if(!Convert.IsDBNull(dr["IdTarifa"]))
    //                    oTecnicos.IdTarifa=Convert.ToInt32(dr["IdTarifa"]);
    //                if(!Convert.IsDBNull(dr["t303_idnodo"]))
    //                    oTecnicos.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
    //                oTecnicos.t001_sexo=Convert.ToString(dr["t001_sexo"]);
    //                oTecnicos.t001_codred=Convert.ToString(dr["t001_codred"]);
    //                oTecnicos.baja=Convert.ToInt32(dr["baja"]);
    //                if(!Convert.IsDBNull(dr["EMPRESA"]))
    //                    oTecnicos.EMPRESA=Convert.ToString(dr["EMPRESA"]);
    //                oTecnicos.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
    //                if(!Convert.IsDBNull(dr["tipo"]))
    //                    oTecnicos.tipo=Convert.ToString(dr["tipo"]);
    //                oTecnicos.t001_email=Convert.ToString(dr["t001_email"]);
    //                oTecnicos.MAIL=Convert.ToString(dr["MAIL"]);

    //            }
    //            return oTecnicos;
				
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //        finally
    //        {
    //            if (dr != null)
    //            {
    //                if (!dr.IsClosed) dr.Close();
    //                dr.Dispose();
    //            }
    //        }
    //    }
		
    //    /// <summary>
    //    /// Actualiza un Tecnicos a partir del id
    //    /// </summary>
    //    internal int Update(Models.Tecnicos oTecnicos)
    //    {
    //        try
    //        {
    //            SqlParameter[] dbparams = new SqlParameter[12] {
    //                Param(enumDBFields.t314_idusuario, oTecnicos.t314_idusuario),
    //                Param(enumDBFields.Profesional, oTecnicos.Profesional),
    //                Param(enumDBFields.IdTarifa, oTecnicos.IdTarifa),
    //                Param(enumDBFields.t303_idnodo, oTecnicos.t303_idnodo),
    //                Param(enumDBFields.t001_sexo, oTecnicos.t001_sexo),
    //                Param(enumDBFields.t001_codred, oTecnicos.t001_codred),
    //                Param(enumDBFields.baja, oTecnicos.baja),
    //                Param(enumDBFields.EMPRESA, oTecnicos.EMPRESA),
    //                Param(enumDBFields.t303_denominacion, oTecnicos.t303_denominacion),
    //                Param(enumDBFields.tipo, oTecnicos.tipo),
    //                Param(enumDBFields.t001_email, oTecnicos.t001_email),
    //                Param(enumDBFields.MAIL, oTecnicos.MAIL)
    //            };
                           
    //            return (int)cDblib.Execute("SUPER.IAP30_Tecnicos_UPD", dbparams);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
		
    //    /// <summary>
    //    /// Elimina un Tecnicos a partir del id
    //    /// </summary>
    //    internal int Delete()
    //    {
    //        try
    //        {
				
            
    //            return (int)cDblib.Execute("SUPER.IAP30_Tecnicos_DEL", dbparams);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    /// <summary>
    //    /// Obtiene todos los Tecnicos
    //    /// </summary>
        internal List<Models.Tecnicos> Catalogo(Models.Tecnicos oTecnicosFilter)
        {
            Models.Tecnicos oTecnicos = null;
            List<Models.Tecnicos> lst = new List<Models.Tecnicos>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[9] {
                    Param(enumDBFields.Apellido1, oTecnicosFilter.Apellido1),
                    Param(enumDBFields.Apellido2, oTecnicosFilter.Apellido2),
                    Param(enumDBFields.Nombre, oTecnicosFilter.Nombre),
                    Param(enumDBFields.idNodo, oTecnicosFilter.t303_idnodo),
                    Param(enumDBFields.nPSN, oTecnicosFilter.nPSN),
                    Param(enumDBFields.Cualidad, oTecnicosFilter.Cualidad),
                    Param(enumDBFields.idTarea, oTecnicosFilter.idTarea),
                    Param(enumDBFields.Foraneos, oTecnicosFilter.Foraneos),
                    Param(enumDBFields.SoloActivos, oTecnicosFilter.SoloActivos)
                };

                dr = cDblib.DataReader("SUP_TECNICOS_TARIFA_NOMBRE", dbparams);
                while (dr.Read())
                {
                    oTecnicos = new Models.Tecnicos();
                    oTecnicos.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
                    if (!Convert.IsDBNull(dr["Profesional"]))
                        oTecnicos.Profesional = Convert.ToString(dr["Profesional"]);
                    if (!Convert.IsDBNull(dr["IdTarifa"]))
                        oTecnicos.IdTarifa = Convert.ToInt32(dr["IdTarifa"]);
                    if (!Convert.IsDBNull(dr["t303_idnodo"]))
                        oTecnicos.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                    oTecnicos.t001_sexo = Convert.ToString(dr["t001_sexo"]);
                    oTecnicos.t001_codred = Convert.ToString(dr["t001_codred"]);
                    oTecnicos.baja = Convert.ToInt32(dr["baja"]);
                    if (!Convert.IsDBNull(dr["EMPRESA"]))
                        oTecnicos.EMPRESA = Convert.ToString(dr["EMPRESA"]);
                    oTecnicos.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                    if (!Convert.IsDBNull(dr["tipo"]))
                        oTecnicos.tipo = Convert.ToString(dr["tipo"]);
                    oTecnicos.t001_email = Convert.ToString(dr["t001_email"]);
                    oTecnicos.MAIL = Convert.ToString(dr["MAIL"]);

                    lst.Add(oTecnicos);

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
                case enumDBFields.Apellido1:
                    paramName = "@sApellido1";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.Apellido2:
                    paramName = "@sApellido2";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.Nombre:
                    paramName = "@sNombre";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.idNodo:
                    paramName = "@idNodo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.nPSN:
                    paramName = "@nPSN";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.Cualidad:
                    paramName = "@sCualidad";
                    paramType = SqlDbType.VarChar;
                    paramSize = 1;
                    break;
                case enumDBFields.idTarea:
                    paramName = "@idTarea";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.Foraneos:
                    paramName = "@bForaneos";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.SoloActivos:
                    paramName = "@bSoloActivos";
                    paramType = SqlDbType.Bit;
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
