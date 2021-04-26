using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for InforImpuMen
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class InforImpuMen 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
	    //return InforImpuMen.Catalogo(sProfesionales, nDesde, nHasta, sTipo);	

        private enum enumDBFields : byte
        {
			Profesionales = 1,
			nDesde = 2,
			nHasta = 3,
			tipo = 4
        }

        internal InforImpuMen(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion
	
        #region funciones publicas
        ///// <summary>
        ///// Inserta un InforImpuMen
        ///// </summary>
        //internal int Insert(Models.InforImpuMen oInforImpuMen)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[7] {
        //            Param(enumDBFields.t314_idusuario, oInforImpuMen.t314_idusuario),
        //            Param(enumDBFields.Profesional, oInforImpuMen.Profesional),
        //            Param(enumDBFields.AnnoMes, oInforImpuMen.AnnoMes),
        //            Param(enumDBFields.AnnoMesText, oInforImpuMen.AnnoMesText),
        //            Param(enumDBFields.Fecha, oInforImpuMen.Fecha),
        //            Param(enumDBFields.DiaSemana, oInforImpuMen.DiaSemana),
        //            Param(enumDBFields.Horas, oInforImpuMen.Horas)
        //        };

        //        return (int)cDblib.Execute("_InforImpuMen_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un InforImpuMen a partir del id
        ///// </summary>
        //internal Models.InforImpuMen Select()
        //{
        //    Models.InforImpuMen oInforImpuMen = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_InforImpuMen_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oInforImpuMen = new Models.InforImpuMen();
        //            if(!Convert.IsDBNull(dr["t314_idusuario"]))
        //                oInforImpuMen.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
        //            if(!Convert.IsDBNull(dr["Profesional"]))
        //                oInforImpuMen.Profesional=Convert.ToString(dr["Profesional"]);
        //            if(!Convert.IsDBNull(dr["AnnoMes"]))
        //                oInforImpuMen.AnnoMes=Convert.ToInt32(dr["AnnoMes"]);
        //            if(!Convert.IsDBNull(dr["AnnoMesText"]))
        //                oInforImpuMen.AnnoMesText=Convert.ToString(dr["AnnoMesText"]);
        //            if(!Convert.IsDBNull(dr["Fecha"]))
        //                oInforImpuMen.Fecha=Convert.ToDateTime(dr["Fecha"]);
        //            if(!Convert.IsDBNull(dr["DiaSemana"]))
        //                oInforImpuMen.DiaSemana=Convert.ToString(dr["DiaSemana"]);
        //            if(!Convert.IsDBNull(dr["Horas"]))
        //                oInforImpuMen.Horas=Convert.ToSingle(dr["Horas"]);

        //        }
        //        return oInforImpuMen;
				
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
        ///// Actualiza un InforImpuMen a partir del id
        ///// </summary>
        //internal int Update(Models.InforImpuMen oInforImpuMen)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[7] {
        //            Param(enumDBFields.t314_idusuario, oInforImpuMen.t314_idusuario),
        //            Param(enumDBFields.Profesional, oInforImpuMen.Profesional),
        //            Param(enumDBFields.AnnoMes, oInforImpuMen.AnnoMes),
        //            Param(enumDBFields.AnnoMesText, oInforImpuMen.AnnoMesText),
        //            Param(enumDBFields.Fecha, oInforImpuMen.Fecha),
        //            Param(enumDBFields.DiaSemana, oInforImpuMen.DiaSemana),
        //            Param(enumDBFields.Horas, oInforImpuMen.Horas)
        //        };
                           
        //        return (int)cDblib.Execute("_InforImpuMen_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un InforImpuMen a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_InforImpuMen_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los InforImpuMen
        ///// </summary>
        internal List<Models.InforImpuMen> Catalogo(string Profesionales, int nDesde, int nHasta, string tipo)
        {
            Models.InforImpuMen oInforImpuMen = null;
            List<Models.InforImpuMen> lst = new List<Models.InforImpuMen>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
                    Param(enumDBFields.Profesionales, Profesionales),
                    Param(enumDBFields.nDesde, nDesde),
                    Param(enumDBFields.nHasta, nHasta),
                    Param(enumDBFields.tipo, tipo)
                };

                dr = cDblib.DataReader("SUP_INFORIMPUMEN_DA", dbparams);
                while (dr.Read())
                {
                    oInforImpuMen = new Models.InforImpuMen();
                    if (!Convert.IsDBNull(dr["t314_idusuario"]))
                        oInforImpuMen.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
                    if (!Convert.IsDBNull(dr["Profesional"]))
                        oInforImpuMen.Profesional = Convert.ToString(dr["Profesional"]);
                    if (!Convert.IsDBNull(dr["AnnoMes"]))
                        oInforImpuMen.AnnoMes = Convert.ToInt32(dr["AnnoMes"]);
                    if (!Convert.IsDBNull(dr["AnnoMesText"]))
                        oInforImpuMen.AnnoMesText = Convert.ToString(dr["AnnoMesText"]);
                    if (tipo=="D")
                    {
                        if (!Convert.IsDBNull(dr["Fecha"]))
                            oInforImpuMen.Fecha = Convert.ToDateTime(dr["Fecha"]);
                        if (!Convert.IsDBNull(dr["DiaSemana"]))
                            oInforImpuMen.DiaSemana = Convert.ToString(dr["DiaSemana"]);
                    }

                    if (!Convert.IsDBNull(dr["Horas"]))
                        oInforImpuMen.Horas = Convert.ToSingle(dr["Horas"]);

                    lst.Add(oInforImpuMen);

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
                case enumDBFields.Profesionales:
                    paramName = "@Profesionales";
					paramType = SqlDbType.VarChar;
					paramSize = 8000;
					break;
				case enumDBFields.nDesde:
                    paramName = "@nDesde";
                    paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.nHasta:
                    paramName = "@nHasta";
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
