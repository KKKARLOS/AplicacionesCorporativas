using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoTecnicoIAP
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ConsumoTecnicoIAP 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
        private enum enumDBFields : byte
        {
			t314_idusuario = 1,
			t305_idproyectosubnodo = 2,
			dDesde = 3,
			dHasta = 4
        }

        internal ConsumoTecnicoIAP(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un ConsumoTecnicoIAP
        ///// </summary>
        //internal int Insert(Models.ConsumoTecnicoIAP oConsumoTecnicoIAP)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[22] {
        //            Param(enumDBFields.t301_idproyecto, oConsumoTecnicoIAP.t301_idproyecto),
        //            Param(enumDBFields.t305_idproyectosubnodo, oConsumoTecnicoIAP.t305_idproyectosubnodo),
        //            Param(enumDBFields.t331_idpt, oConsumoTecnicoIAP.t331_idpt),
        //            Param(enumDBFields.t334_idfase, oConsumoTecnicoIAP.t334_idfase),
        //            Param(enumDBFields.t335_idactividad, oConsumoTecnicoIAP.t335_idactividad),
        //            Param(enumDBFields.t332_idtarea, oConsumoTecnicoIAP.t332_idtarea),
        //            Param(enumDBFields.t337_fecha, oConsumoTecnicoIAP.t337_fecha),
        //            Param(enumDBFields.t301_denominacion, oConsumoTecnicoIAP.t301_denominacion),
        //            Param(enumDBFields.Cualidad, oConsumoTecnicoIAP.Cualidad),
        //            Param(enumDBFields.t302_denominacion, oConsumoTecnicoIAP.t302_denominacion),
        //            Param(enumDBFields.t303_idnodo, oConsumoTecnicoIAP.t303_idnodo),
        //            Param(enumDBFields.t303_denominacion, oConsumoTecnicoIAP.t303_denominacion),
        //            Param(enumDBFields.T331_despt, oConsumoTecnicoIAP.T331_despt),
        //            Param(enumDBFields.t334_desfase, oConsumoTecnicoIAP.t334_desfase),
        //            Param(enumDBFields.t335_desactividad, oConsumoTecnicoIAP.t335_desactividad),
        //            Param(enumDBFields.t332_destarea, oConsumoTecnicoIAP.t332_destarea),
        //            Param(enumDBFields.TotalHorasReportadas, oConsumoTecnicoIAP.TotalHorasReportadas),
        //            Param(enumDBFields.TotalJornadasReportadas, oConsumoTecnicoIAP.TotalJornadasReportadas),
        //            Param(enumDBFields.Comentarios, oConsumoTecnicoIAP.Comentarios),
        //            Param(enumDBFields.t301_estado, oConsumoTecnicoIAP.t301_estado),
        //            Param(enumDBFields.Responsable, oConsumoTecnicoIAP.Responsable),
        //            Param(enumDBFields.orden, oConsumoTecnicoIAP.orden)
        //        };

        //        return (int)cDblib.Execute("_ConsumoTecnicoIAP_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un ConsumoTecnicoIAP a partir del id
        ///// </summary>
        //internal Models.ConsumoTecnicoIAP Select()
        //{
        //    Models.ConsumoTecnicoIAP oConsumoTecnicoIAP = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_ConsumoTecnicoIAP_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oConsumoTecnicoIAP = new Models.ConsumoTecnicoIAP();
        //            oConsumoTecnicoIAP.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);
        //            oConsumoTecnicoIAP.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oConsumoTecnicoIAP.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
        //            oConsumoTecnicoIAP.t334_idfase=Convert.ToInt32(dr["t334_idfase"]);
        //            oConsumoTecnicoIAP.t335_idactividad=Convert.ToInt32(dr["t335_idactividad"]);
        //            oConsumoTecnicoIAP.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oConsumoTecnicoIAP.t337_fecha=Convert.ToDateTime(dr["t337_fecha"]);
        //            oConsumoTecnicoIAP.t301_denominacion=Convert.ToString(dr["t301_denominacion"]);
        //            if(!Convert.IsDBNull(dr["Cualidad"]))
        //                oConsumoTecnicoIAP.Cualidad=Convert.ToString(dr["Cualidad"]);
        //            oConsumoTecnicoIAP.t302_denominacion=Convert.ToString(dr["t302_denominacion"]);
        //            oConsumoTecnicoIAP.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
        //            oConsumoTecnicoIAP.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
        //            oConsumoTecnicoIAP.T331_despt=Convert.ToString(dr["T331_despt"]);
        //            if(!Convert.IsDBNull(dr["t334_desfase"]))
        //                oConsumoTecnicoIAP.t334_desfase=Convert.ToString(dr["t334_desfase"]);
        //            if(!Convert.IsDBNull(dr["t335_desactividad"]))
        //                oConsumoTecnicoIAP.t335_desactividad=Convert.ToString(dr["t335_desactividad"]);
        //            oConsumoTecnicoIAP.t332_destarea=Convert.ToString(dr["t332_destarea"]);
        //            oConsumoTecnicoIAP.TotalHorasReportadas=Convert.ToSingle(dr["TotalHorasReportadas"]);
        //            oConsumoTecnicoIAP.TotalJornadasReportadas=Convert.ToDouble(dr["TotalJornadasReportadas"]);
        //            if(!Convert.IsDBNull(dr["Comentarios"]))
        //                oConsumoTecnicoIAP.Comentarios=Convert.ToString(dr["Comentarios"]);
        //            oConsumoTecnicoIAP.t301_estado=Convert.ToString(dr["t301_estado"]);
        //            if(!Convert.IsDBNull(dr["Responsable"]))
        //                oConsumoTecnicoIAP.Responsable=Convert.ToString(dr["Responsable"]);
        //            if(!Convert.IsDBNull(dr["orden"]))
        //                oConsumoTecnicoIAP.orden=Convert.ToString(dr["orden"]);

        //        }
        //        return oConsumoTecnicoIAP;
				
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
        ///// Actualiza un ConsumoTecnicoIAP a partir del id
        ///// </summary>
        //internal int Update(Models.ConsumoTecnicoIAP oConsumoTecnicoIAP)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[22] {
        //            Param(enumDBFields.t301_idproyecto, oConsumoTecnicoIAP.t301_idproyecto),
        //            Param(enumDBFields.t305_idproyectosubnodo, oConsumoTecnicoIAP.t305_idproyectosubnodo),
        //            Param(enumDBFields.t331_idpt, oConsumoTecnicoIAP.t331_idpt),
        //            Param(enumDBFields.t334_idfase, oConsumoTecnicoIAP.t334_idfase),
        //            Param(enumDBFields.t335_idactividad, oConsumoTecnicoIAP.t335_idactividad),
        //            Param(enumDBFields.t332_idtarea, oConsumoTecnicoIAP.t332_idtarea),
        //            Param(enumDBFields.t337_fecha, oConsumoTecnicoIAP.t337_fecha),
        //            Param(enumDBFields.t301_denominacion, oConsumoTecnicoIAP.t301_denominacion),
        //            Param(enumDBFields.Cualidad, oConsumoTecnicoIAP.Cualidad),
        //            Param(enumDBFields.t302_denominacion, oConsumoTecnicoIAP.t302_denominacion),
        //            Param(enumDBFields.t303_idnodo, oConsumoTecnicoIAP.t303_idnodo),
        //            Param(enumDBFields.t303_denominacion, oConsumoTecnicoIAP.t303_denominacion),
        //            Param(enumDBFields.T331_despt, oConsumoTecnicoIAP.T331_despt),
        //            Param(enumDBFields.t334_desfase, oConsumoTecnicoIAP.t334_desfase),
        //            Param(enumDBFields.t335_desactividad, oConsumoTecnicoIAP.t335_desactividad),
        //            Param(enumDBFields.t332_destarea, oConsumoTecnicoIAP.t332_destarea),
        //            Param(enumDBFields.TotalHorasReportadas, oConsumoTecnicoIAP.TotalHorasReportadas),
        //            Param(enumDBFields.TotalJornadasReportadas, oConsumoTecnicoIAP.TotalJornadasReportadas),
        //            Param(enumDBFields.Comentarios, oConsumoTecnicoIAP.Comentarios),
        //            Param(enumDBFields.t301_estado, oConsumoTecnicoIAP.t301_estado),
        //            Param(enumDBFields.Responsable, oConsumoTecnicoIAP.Responsable),
        //            Param(enumDBFields.orden, oConsumoTecnicoIAP.orden)
        //        };
                           
        //        return (int)cDblib.Execute("_ConsumoTecnicoIAP_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un ConsumoTecnicoIAP a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_ConsumoTecnicoIAP_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los ConsumoTecnicoIAP
        ///// </summary>
        internal List<Models.ConsumoTecnicoIAP> Catalogo(int t314_idusuario, Nullable<int> t305_idproyectosubnodo, DateTime dDesde, DateTime dHasta)
        {
            Models.ConsumoTecnicoIAP oConsumoTecnicoIAP = null;
            List<Models.ConsumoTecnicoIAP> lst = new List<Models.ConsumoTecnicoIAP>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
                    Param(enumDBFields.t314_idusuario, t314_idusuario),
                    Param(enumDBFields.t305_idproyectosubnodo, t305_idproyectosubnodo),
                    Param(enumDBFields.dDesde, dDesde),
                    Param(enumDBFields.dHasta, dHasta)
                };

                dr = cDblib.DataReader("SUP_RESUMENDECONSUMOS_IAP30", dbparams);
                while (dr.Read())
                {
                    oConsumoTecnicoIAP = new Models.ConsumoTecnicoIAP();
                    oConsumoTecnicoIAP.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oConsumoTecnicoIAP.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oConsumoTecnicoIAP.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    if (!Convert.IsDBNull(dr["t334_idfase"])) oConsumoTecnicoIAP.t334_idfase = Convert.ToInt32(dr["t334_idfase"]);
                    if (!Convert.IsDBNull(dr["t335_idactividad"])) oConsumoTecnicoIAP.t335_idactividad = Convert.ToInt32(dr["t335_idactividad"]);
                    oConsumoTecnicoIAP.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    oConsumoTecnicoIAP.t337_fecha = Convert.ToDateTime(dr["t337_fecha"]);
                    oConsumoTecnicoIAP.t301_denominacion = Convert.ToString(dr["t301_denominacion"]);
                    if (!Convert.IsDBNull(dr["Cualidad"]))
                        oConsumoTecnicoIAP.Cualidad = Convert.ToString(dr["Cualidad"]);
                    oConsumoTecnicoIAP.t302_denominacion = Convert.ToString(dr["t302_denominacion"]);
                    oConsumoTecnicoIAP.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                    oConsumoTecnicoIAP.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                    oConsumoTecnicoIAP.T331_despt = Convert.ToString(dr["T331_despt"]);
                    if (!Convert.IsDBNull(dr["t334_desfase"]))
                        oConsumoTecnicoIAP.t334_desfase = Convert.ToString(dr["t334_desfase"]);
                    if (!Convert.IsDBNull(dr["t335_desactividad"]))
                        oConsumoTecnicoIAP.t335_desactividad = Convert.ToString(dr["t335_desactividad"]);
                    oConsumoTecnicoIAP.t332_destarea = Convert.ToString(dr["t332_destarea"]);
                    oConsumoTecnicoIAP.TotalHorasReportadas = Convert.ToSingle(dr["TotalHorasReportadas"]);
                    oConsumoTecnicoIAP.TotalJornadasReportadas = Convert.ToDouble(dr["TotalJornadasReportadas"]);
                    if (!Convert.IsDBNull(dr["t337_comentario"]))
                        oConsumoTecnicoIAP.Comentarios = Convert.ToString(dr["t337_comentario"]);
                    oConsumoTecnicoIAP.t301_estado = Convert.ToString(dr["t301_estado"]);
                    if (!Convert.IsDBNull(dr["Responsable"]))
                        oConsumoTecnicoIAP.Responsable = Convert.ToString(dr["Responsable"]);
                    if (!Convert.IsDBNull(dr["orden"]))
                        oConsumoTecnicoIAP.orden = Convert.ToString(dr["orden"]);

                    lst.Add(oConsumoTecnicoIAP);

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
                case enumDBFields.t314_idusuario:
                    paramName = "@t314_idusuario";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t305_idproyectosubnodo:
					paramName = "@t305_idproyectosubnodo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.dDesde:
                    paramName = "@dDesde";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
                case enumDBFields.dHasta:
                    paramName = "@dHasta";
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
