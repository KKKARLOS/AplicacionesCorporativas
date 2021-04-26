using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;
using System.Collections;

/// <summary>
/// Summary description for TareaCTIAP
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class TareaCTIAP 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t001_codred = 1,
			t332_idtarea = 2,
			t332_destarea = 3,
			t331_despt = 4,
			t335_desactividad = 5,
			t334_desfase = 6,
			t301_idproyecto = 7,
			t301_denominacion = 8,
			MAIL = 9,
            t337_fecha = 10
        }

        internal TareaCTIAP(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene la relación de destinatarios a los que hay que enviar mail si se realiza alguna imputación IAP 
        /// con el traspaso de IAP realizado. Responsable del proyecto + Figuras delegado y colaborador
        /// </summary>
        internal List<Models.TareaCTIAP> Catalogo(int t332_idtarea, DateTime t337_fecha)
        {
            Models.TareaCTIAP oTareaCTIAP = null;
            List<Models.TareaCTIAP> lst = new List<Models.TareaCTIAP>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                            Param(enumDBFields.t332_idtarea, t332_idtarea),
                            Param(enumDBFields.t337_fecha, t337_fecha)
                        };
                dr = cDblib.DataReader("SUP_TAREA_CTIAP", dbparams);
                while (dr.Read())
                {
                    oTareaCTIAP = new Models.TareaCTIAP();
                    oTareaCTIAP.t001_codred = Convert.ToString(dr["t001_codred"]);
                    oTareaCTIAP.t332_idtarea = t332_idtarea;
                    oTareaCTIAP.t332_destarea = Convert.ToString(dr["t332_destarea"]);
                    oTareaCTIAP.t331_despt = Convert.ToString(dr["t331_despt"]);
                    oTareaCTIAP.t335_desactividad = Convert.ToString(dr["t335_desactividad"]);
                    oTareaCTIAP.t334_desfase = Convert.ToString(dr["t334_desfase"]);
                    oTareaCTIAP.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oTareaCTIAP.t301_denominacion = Convert.ToString(dr["t301_denominacion"]);
                    oTareaCTIAP.MAIL = Convert.ToString(dr["MAIL"]);

                    lst.Add(oTareaCTIAP);

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

        internal ArrayList getRTPT(int t332_idtarea)
        {
            Models.TareaCTIAP oTareaCTIAP = null;
            ArrayList lst = new ArrayList();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                            Param(enumDBFields.t332_idtarea, t332_idtarea)
                };

                dr = cDblib.DataReader("SUP_TAREA_CLE_CORREO", dbparams);
                while (dr.Read())
                {
                    oTareaCTIAP = new Models.TareaCTIAP();
                    oTareaCTIAP.t001_codred = Convert.ToString(dr["t001_codred"]);
                    oTareaCTIAP.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]); ;
                    oTareaCTIAP.t332_destarea = Convert.ToString(dr["t332_destarea"]);
                    oTareaCTIAP.t331_despt = Convert.ToString(dr["t331_despt"]);
                    oTareaCTIAP.t335_desactividad = Convert.ToString(dr["t335_desactividad"]);
                    oTareaCTIAP.t334_desfase = Convert.ToString(dr["t334_desfase"]);
                    oTareaCTIAP.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oTareaCTIAP.t301_denominacion = Convert.ToString(dr["t301_denominacion"]);
                    oTareaCTIAP.MAIL = Convert.ToString(dr["MAIL"]);

                    lst.Add(oTareaCTIAP);

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
				case enumDBFields.t001_codred:
					paramName = "@t001_codred";
					paramType = SqlDbType.VarChar;
					paramSize = 15;
					break;
				case enumDBFields.t332_idtarea:
					paramName = "@t332_idtarea";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_destarea:
					paramName = "@t332_destarea";
					paramType = SqlDbType.VarChar;
					paramSize = 100;
					break;
				case enumDBFields.t331_despt:
					paramName = "@t331_despt";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t335_desactividad:
					paramName = "@t335_desactividad";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t334_desfase:
					paramName = "@t334_desfase";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t301_idproyecto:
					paramName = "@t301_idproyecto";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t301_denominacion:
					paramName = "@t301_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 70;
					break;
				case enumDBFields.MAIL:
					paramName = "@MAIL";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
                case enumDBFields.t337_fecha:
                    paramName = "@t337_fecha";
                    paramType = SqlDbType.DateTime;
                    paramSize = 8;
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
