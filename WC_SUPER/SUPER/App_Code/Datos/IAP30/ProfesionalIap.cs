using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;
using System.Collections;

/// <summary>
/// Summary description for ProfesionalIap
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ProfesionalIap 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			num_empleado = 1,
			sPerfil = 2,
			sApellido1 = 3,
			sApellido2 = 4,
			sNombre = 5,
			IdFicepi = 6,
			bForaneos = 7,
            bMostrarBajas = 8,
            sAp1 = 9,
			sAp2 = 10,
            cr=11
        }


        internal ProfesionalIap(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los ProfesionalIap
        /// </summary>
        public List<Models.ProfesionalIap> Catalogo(String tipoBuscqueda, Hashtable mapaParametros)
        {
            Models.ProfesionalIap oProfesionalIap = null;
            List<Models.ProfesionalIap> lst = new List<Models.ProfesionalIap>();

            string nomproc = "";           
            

            IDataReader dr = null;
            SqlParameter[] dbparams = null;

            try
            {

                switch (tipoBuscqueda)
                {
                    case "PROFESIONALES":
                        #region PROFESIONALES
                        nomproc = "SUP_PROFESIONALES_IAP";
                        dbparams = new SqlParameter[7] {
                            Param(enumDBFields.num_empleado, mapaParametros["num_empleado"]),
                            Param(enumDBFields.sPerfil, mapaParametros["sPerfil"]),
                            Param(enumDBFields.sApellido1, mapaParametros["sApellido1"]),
                            Param(enumDBFields.sApellido2, mapaParametros["sApellido2"]),
                            Param(enumDBFields.sNombre, mapaParametros["sNombre"]),
                            Param(enumDBFields.IdFicepi, mapaParametros["IdFicepi"]),
                            Param(enumDBFields.bForaneos, mapaParametros["bForaneos"])

                        };

                        dr = cDblib.DataReader(nomproc, dbparams);

                        while (dr.Read())
                        {
                    
                            oProfesionalIap = new Models.ProfesionalIap();
                            oProfesionalIap.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
                            oProfesionalIap.PROFESIONAL = Convert.ToString(dr["PROFESIONAL"]);
                            if (!Convert.IsDBNull(dr["EMPRESA"]))
                                oProfesionalIap.EMPRESA = Convert.ToString(dr["EMPRESA"]);
                            oProfesionalIap.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                            oProfesionalIap.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                            oProfesionalIap.t001_codred = Convert.ToString(dr["t001_codred"]);
                            oProfesionalIap.t001_sexo = Convert.ToString(dr["t001_sexo"]);
                            if (!Convert.IsDBNull(dr["tipo"]))
                                oProfesionalIap.tipo = Convert.ToString(dr["tipo"]);

                            lst.Add(oProfesionalIap);

                        }
                        break;
                    #endregion
                    case "PROFESIONALES_AGENDA":
                        #region PROFESIONALES_AGENDA
                        nomproc = "SUP_PROFESIONALES_IAP";
                        dbparams = new SqlParameter[6] {
                            Param(enumDBFields.num_empleado, mapaParametros["num_empleado"]),
                            Param(enumDBFields.sPerfil, mapaParametros["sPerfil"]),
                            Param(enumDBFields.sApellido1, mapaParametros["sApellido1"]),
                            Param(enumDBFields.sApellido2, mapaParametros["sApellido2"]),
                            Param(enumDBFields.sNombre, mapaParametros["sNombre"]),
                            Param(enumDBFields.IdFicepi, mapaParametros["IdFicepi"])
                        };

                        dr = cDblib.DataReader(nomproc, dbparams);

                        while (dr.Read())
                        {

                            oProfesionalIap = new Models.ProfesionalIap();
                            oProfesionalIap.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
                            oProfesionalIap.PROFESIONAL = Convert.ToString(dr["PROFESIONAL"]);
                            if (!Convert.IsDBNull(dr["EMPRESA"]))
                                oProfesionalIap.EMPRESA = Convert.ToString(dr["EMPRESA"]);
                            oProfesionalIap.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                            oProfesionalIap.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                            oProfesionalIap.t001_codred = Convert.ToString(dr["t001_codred"]);
                            oProfesionalIap.t001_sexo = Convert.ToString(dr["t001_sexo"]);
                            if (!Convert.IsDBNull(dr["tipo"]))
                                oProfesionalIap.tipo = Convert.ToString(dr["tipo"]);

                            lst.Add(oProfesionalIap);

                        }
                        break;
                    #endregion
                    case "RESPONSABLES_PROYECTO":
                        #region RESPONSABLES_PROYECTO
                        nomproc = "SUP_PROFRESPONSABLE_PROYECTO";
                        dbparams = new SqlParameter[4] {
                            Param(enumDBFields.sAp1, mapaParametros["sAp1"]),
                            Param(enumDBFields.sAp2, mapaParametros["sAp2"]),
                            Param(enumDBFields.sNombre, mapaParametros["sNombre"]),
                            Param(enumDBFields.bMostrarBajas, mapaParametros["bMostrarBajas"])
                        };

                        dr = cDblib.DataReader(nomproc, dbparams);

                        while (dr.Read())
                        {
                            oProfesionalIap = new Models.ProfesionalIap();
                            oProfesionalIap.t314_idusuario = Convert.ToInt32(dr["idusuario"]);
                            oProfesionalIap.PROFESIONAL = Convert.ToString(dr["PROFESIONAL"]);
                            if (!Convert.IsDBNull(dr["EMPRESA"]))
                                oProfesionalIap.EMPRESA = Convert.ToString(dr["EMPRESA"]);
                            oProfesionalIap.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                            oProfesionalIap.es_responsable = Convert.ToInt32(dr["es_responsable"]);
                            lst.Add(oProfesionalIap);

                        }

                        break;
                    #endregion
                    case "USUARIOS":
                        #region USUARIOS
                        nomproc = "SUP_PROF_VISIBLES_ADM";
                        dbparams = new SqlParameter[6] {
                            Param(enumDBFields.sAp1, mapaParametros["sAp1"]),
                            Param(enumDBFields.sAp2, mapaParametros["sAp2"]),
                            Param(enumDBFields.sNombre, mapaParametros["sNombre"]),
                            Param(enumDBFields.bMostrarBajas, mapaParametros["bMostrarBajas"]),
                            Param(enumDBFields.cr, mapaParametros["t303_idnodo"]),
                            Param(enumDBFields.bForaneos, mapaParametros["bForaneos"])

                        };

                        dr = cDblib.DataReader(nomproc, dbparams);

                        while (dr.Read())
                        {

                            oProfesionalIap = new Models.ProfesionalIap();
                            oProfesionalIap.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
                            oProfesionalIap.PROFESIONAL = Convert.ToString(dr["PROFESIONAL"]);
                            if (!Convert.IsDBNull(dr["EMPRESA"]))
                                oProfesionalIap.EMPRESA = Convert.ToString(dr["EMPRESA"]);
                            oProfesionalIap.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                            oProfesionalIap.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                            oProfesionalIap.t001_codred = Convert.ToString(dr["t001_codred"]);
                            oProfesionalIap.t001_sexo = Convert.ToString(dr["t001_sexo"]);
                            if (!Convert.IsDBNull(dr["tipo"]))
                                oProfesionalIap.tipo = Convert.ToString(dr["tipo"]);

                            lst.Add(oProfesionalIap);

                        }
                        break;
                        #endregion
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



        ///// <summary>
        ///// Inserta un ProfesionalIap
        ///// </summary>
        //internal int Insert(Models.ProfesionalIap oProfesionalIap)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[8] {
        //            Param(enumDBFields.t314_idusuario, oProfesionalIap.t314_idusuario),
        //            Param(enumDBFields.PROFESIONAL, oProfesionalIap.PROFESIONAL),
        //            Param(enumDBFields.EMPRESA, oProfesionalIap.EMPRESA),
        //            Param(enumDBFields.t303_denominacion, oProfesionalIap.t303_denominacion),
        //            Param(enumDBFields.t001_idficepi, oProfesionalIap.t001_idficepi),
        //            Param(enumDBFields.t001_codred, oProfesionalIap.t001_codred),
        //            Param(enumDBFields.t001_sexo, oProfesionalIap.t001_sexo),
        //            Param(enumDBFields.tipo, oProfesionalIap.tipo)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_ProfesionalIap_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un ProfesionalIap a partir del id
        ///// </summary>
        //internal Models.ProfesionalIap Select()
        //{
        //    Models.ProfesionalIap oProfesionalIap = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_ProfesionalIap_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oProfesionalIap = new Models.ProfesionalIap();
        //            oProfesionalIap.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
        //            oProfesionalIap.PROFESIONAL=Convert.ToString(dr["PROFESIONAL"]);
        //            if(!Convert.IsDBNull(dr["EMPRESA"]))
        //                oProfesionalIap.EMPRESA=Convert.ToString(dr["EMPRESA"]);
        //            oProfesionalIap.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
        //            oProfesionalIap.t001_idficepi=Convert.ToInt32(dr["t001_idficepi"]);
        //            oProfesionalIap.t001_codred=Convert.ToString(dr["t001_codred"]);
        //            oProfesionalIap.t001_sexo=Convert.ToString(dr["t001_sexo"]);
        //            if(!Convert.IsDBNull(dr["tipo"]))
        //                oProfesionalIap.tipo=Convert.ToString(dr["tipo"]);

        //        }
        //        return oProfesionalIap;
				
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
        ///// Actualiza un ProfesionalIap a partir del id
        ///// </summary>
        //internal int Update(Models.ProfesionalIap oProfesionalIap)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[8] {
        //            Param(enumDBFields.t314_idusuario, oProfesionalIap.t314_idusuario),
        //            Param(enumDBFields.PROFESIONAL, oProfesionalIap.PROFESIONAL),
        //            Param(enumDBFields.EMPRESA, oProfesionalIap.EMPRESA),
        //            Param(enumDBFields.t303_denominacion, oProfesionalIap.t303_denominacion),
        //            Param(enumDBFields.t001_idficepi, oProfesionalIap.t001_idficepi),
        //            Param(enumDBFields.t001_codred, oProfesionalIap.t001_codred),
        //            Param(enumDBFields.t001_sexo, oProfesionalIap.t001_sexo),
        //            Param(enumDBFields.tipo, oProfesionalIap.tipo)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_ProfesionalIap_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un ProfesionalIap a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_ProfesionalIap_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        		
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
				case enumDBFields.num_empleado:
					paramName = "@num_empleado";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.sPerfil :
					paramName = "@sPerfil ";
					paramType = SqlDbType.VarChar;
					paramSize = 2;
					break;
				case enumDBFields.sApellido1:
					paramName = "@sApellido1";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.sApellido2:
					paramName = "@sApellido2";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
                case enumDBFields.sAp1:
                    paramName = "@sAp1";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.sAp2:
                    paramName = "@sAp2";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
				case enumDBFields.sNombre:
					paramName = "@sNombre";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.IdFicepi:
					paramName = "@IdFicepi";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.bForaneos:
					paramName = "@bForaneos";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
                case enumDBFields.bMostrarBajas:
                    paramName = "@bMostrarBajas";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.cr:
                    paramName = "@t303_idnodo";
                    paramType = SqlDbType.Int;
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
