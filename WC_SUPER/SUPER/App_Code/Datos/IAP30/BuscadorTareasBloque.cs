using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for BuscadorTareasBloque
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class BuscadorTareasBloque 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			
            nUsuario = 1,
            dUMC = 2,
            dInicioImput = 3,
            dFinImput = 4,
            idFicepi = 5,
            dUMCIAP = 6,
            dDesde = 7,
            dHasta = 8,
            idTarea = 9,
            bMostrarPSN = 10,
            bMostrarPT = 11,
            bMostrarF = 12,
            bMostrarA = 13,
            bMostrarT = 14
        }

        internal BuscadorTareasBloque(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los BuscadorTareasBloque
        /// </summary>
        internal List<Models.BuscadorTareasBloque> Catalogo(Int32 nUsuario, DateTime ultimoMesCerrado, Nullable<DateTime> fechaInicio, Nullable<DateTime> fechaFin)
		{
			Models.BuscadorTareasBloque oBuscadorTareasBloque = null;
            List<Models.BuscadorTareasBloque> lst = new List<Models.BuscadorTareasBloque>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[10] {
                    Param(enumDBFields.nUsuario, nUsuario),
                    Param(enumDBFields.dDesde, fechaInicio),
                    Param(enumDBFields.dHasta, fechaFin),
                    Param(enumDBFields.dUMCIAP, ultimoMesCerrado),
                    Param(enumDBFields.idTarea, null),
                    Param(enumDBFields.bMostrarPSN, 1),
                    Param(enumDBFields.bMostrarPT, 1),
                    Param(enumDBFields.bMostrarF, 1),
                    Param(enumDBFields.bMostrarA, 1),
                    Param(enumDBFields.bMostrarT, 1)

                };

				dr = cDblib.DataReader("SUP_ESTRUCTURAIMPUTABLEENTREFECHAS_IAP30", dbparams);
				while (dr.Read())
				{
					oBuscadorTareasBloque = new Models.BuscadorTareasBloque();
					if(!Convert.IsDBNull(dr["nivel"]))
						oBuscadorTareasBloque.nivel=Convert.ToInt32(dr["nivel"]);
					oBuscadorTareasBloque.Tipo=Convert.ToString(dr["Tipo"]);
					oBuscadorTareasBloque.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);
					oBuscadorTareasBloque.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
					oBuscadorTareasBloque.t305_seudonimo=Convert.ToString(dr["denominacion"]);
					oBuscadorTareasBloque.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
					oBuscadorTareasBloque.t302_denominacion=Convert.ToString(dr["t302_denominacion"]);
					if(!Convert.IsDBNull(dr["responsable"]))
						oBuscadorTareasBloque.responsable=Convert.ToString(dr["responsable"]);
                    //oBuscadorTareasBloque.t301_estado=Convert.ToString(dr["t301_estado"]);
                    if (!Convert.IsDBNull(dr["t331_idpt"]))
                    oBuscadorTareasBloque.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
                    if (!Convert.IsDBNull(dr["t334_idfase"]))
                        oBuscadorTareasBloque.t334_idfase=Convert.ToInt32(dr["t334_idfase"]);
                    if (!Convert.IsDBNull(dr["t335_idactividad"]))
                        oBuscadorTareasBloque.t335_idactividad=Convert.ToInt32(dr["t335_idactividad"]);
                    if (!Convert.IsDBNull(dr["t332_idtarea"]))
                        oBuscadorTareasBloque.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
                    if (!Convert.IsDBNull(dr["t332_estado"]))
                        oBuscadorTareasBloque.t332_estado=Convert.ToInt32(dr["t332_estado"]);
					oBuscadorTareasBloque.denominacion=Convert.ToString(dr["denominacion"]);
                    //oBuscadorTareasBloque.t332_impiap=Convert.ToInt32(dr["t332_impiap"]);
                    if (!Convert.IsDBNull(dr["t323_regjornocompleta"]))
                        oBuscadorTareasBloque.t323_regjornocompleta=Convert.ToInt32(dr["t323_regjornocompleta"]);
                    if (!Convert.IsDBNull(dr["t323_regfes"]))
                        oBuscadorTareasBloque.t323_regfes=Convert.ToInt32(dr["t323_regfes"]);
                    if (!Convert.IsDBNull(dr["t331_obligaest"]))
                        oBuscadorTareasBloque.t331_obligaest=Convert.ToInt32(dr["t331_obligaest"]);
					if(!Convert.IsDBNull(dr["orden"]))
						oBuscadorTareasBloque.orden=Convert.ToString(dr["orden"]);
                    if (!Convert.IsDBNull(dr["fechainicioimputacionpermitida"]))
                        oBuscadorTareasBloque.fechaInicioImpPermitida = Convert.ToDateTime(dr["fechainicioimputacionpermitida"]);
                    if (!Convert.IsDBNull(dr["fechafinimputacionpermitida"]))
                        oBuscadorTareasBloque.fechaFinImpPermitida = Convert.ToDateTime(dr["fechafinimputacionpermitida"]);
                    lst.Add(oBuscadorTareasBloque);

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

        
        /// <summary>
        /// Obtiene todos los BuscadorTareasBloque
        /// </summary>
        internal List<Models.BuscadorTareasBloque> CatalogoAgenda(Int32 idFicepi)
        {
            Models.BuscadorTareasBloque oBuscadorTareasBloque = null;
            List<Models.BuscadorTareasBloque> lst = new List<Models.BuscadorTareasBloque>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.idFicepi, idFicepi)
                    
                };

                dr = cDblib.DataReader("[SUP_TAREASAGENDA_BLOQUE]", dbparams);
                while (dr.Read())
                {
                    oBuscadorTareasBloque = new Models.BuscadorTareasBloque();
                    if (!Convert.IsDBNull(dr["nivel"]))
                        oBuscadorTareasBloque.nivel = Convert.ToInt32(dr["nivel"]);
                    oBuscadorTareasBloque.Tipo = Convert.ToString(dr["Tipo"]);
                    oBuscadorTareasBloque.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oBuscadorTareasBloque.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oBuscadorTareasBloque.t305_seudonimo = Convert.ToString(dr["t305_seudonimo"]);
                    oBuscadorTareasBloque.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                    oBuscadorTareasBloque.t302_denominacion = Convert.ToString(dr["t302_denominacion"]);
                    if (!Convert.IsDBNull(dr["responsable"]))
                        oBuscadorTareasBloque.responsable = Convert.ToString(dr["responsable"]);
                    //oBuscadorTareasBloque.t301_estado = Convert.ToString(dr["t301_estado"]);
                    oBuscadorTareasBloque.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    oBuscadorTareasBloque.t334_idfase = Convert.ToInt32(dr["t334_idfase"]);
                    oBuscadorTareasBloque.t335_idactividad = Convert.ToInt32(dr["t335_idactividad"]);
                    oBuscadorTareasBloque.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    oBuscadorTareasBloque.t332_estado = Convert.ToInt32(dr["estado"]);
                    oBuscadorTareasBloque.denominacion = Convert.ToString(dr["denominacion"]);                    
                    if (!Convert.IsDBNull(dr["orden"]))
                        oBuscadorTareasBloque.orden = Convert.ToString(dr["orden"]);

                    lst.Add(oBuscadorTareasBloque);

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


        internal List<Models.BuscadorTareasBloque> CatalogoBitacoraIAP(Int32 nUsuario)
        {
            Models.BuscadorTareasBloque oBuscadorTareasBloque = null;
            List<Models.BuscadorTareasBloque> lst = new List<Models.BuscadorTareasBloque>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[7] {
                    Param(enumDBFields.nUsuario, nUsuario),
                    Param(enumDBFields.idTarea, null),
                    Param(enumDBFields.bMostrarPSN, 1),
                    Param(enumDBFields.bMostrarPT, 1),
                    Param(enumDBFields.bMostrarF, 1),
                    Param(enumDBFields.bMostrarA, 1),
                    Param(enumDBFields.bMostrarT, 1)

                };

                dr = cDblib.DataReader("SUP_ESTRUCTURA_TAREA_BITACORA_IAP30", dbparams);
                while (dr.Read())
                {
                    oBuscadorTareasBloque = new Models.BuscadorTareasBloque();
                    if (!Convert.IsDBNull(dr["nivel"]))
                        oBuscadorTareasBloque.nivel = Convert.ToInt32(dr["nivel"]);
                    oBuscadorTareasBloque.Tipo = Convert.ToString(dr["Tipo"]);
                    oBuscadorTareasBloque.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oBuscadorTareasBloque.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oBuscadorTareasBloque.t305_seudonimo = Convert.ToString(dr["denominacion"]);
                    oBuscadorTareasBloque.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                    oBuscadorTareasBloque.t302_denominacion = Convert.ToString(dr["t302_denominacion"]);
                    if (!Convert.IsDBNull(dr["responsable"]))
                        oBuscadorTareasBloque.responsable = Convert.ToString(dr["responsable"]);
                    if (!Convert.IsDBNull(dr["t331_idpt"]))
                        oBuscadorTareasBloque.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    if (!Convert.IsDBNull(dr["t334_idfase"]))
                        oBuscadorTareasBloque.t334_idfase = Convert.ToInt32(dr["t334_idfase"]);
                    if (!Convert.IsDBNull(dr["t335_idactividad"]))
                        oBuscadorTareasBloque.t335_idactividad = Convert.ToInt32(dr["t335_idactividad"]);
                    if (!Convert.IsDBNull(dr["t332_idtarea"]))
                        oBuscadorTareasBloque.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    if (!Convert.IsDBNull(dr["t332_estado"]))
                        oBuscadorTareasBloque.t332_estado = Convert.ToInt32(dr["t332_estado"]);
                    oBuscadorTareasBloque.denominacion = Convert.ToString(dr["denominacion"]);
                    if (!Convert.IsDBNull(dr["orden"]))
                        oBuscadorTareasBloque.orden = Convert.ToString(dr["orden"]);
                    lst.Add(oBuscadorTareasBloque);

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
				
                case enumDBFields.nUsuario:
                    paramName = "@nUsuario";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.dUMC:
                    paramName = "@dUMC";
                    paramType = SqlDbType.SmallDateTime;
                    paramSize = 8;
                    break;
                case enumDBFields.dInicioImput:
                    paramName = "@dInicioImput";
                    paramType = SqlDbType.SmallDateTime;
                    paramSize = 8;
                    break;
                case enumDBFields.dFinImput:
                    paramName = "@dFinImput";
                    paramType = SqlDbType.SmallDateTime;
                    paramSize = 8;
                    break;
                case enumDBFields.idFicepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.dUMCIAP:
                    paramName = "@dUMCIAP";
                    paramType = SqlDbType.Date;
                    paramSize = 8;
                    break;
                case enumDBFields.dDesde:
                    paramName = "@dDesde";
                    paramType = SqlDbType.Date;
                    paramSize = 8;
                    break;
                case enumDBFields.dHasta:
                    paramName = "@dHasta";
                    paramType = SqlDbType.Date;
                    paramSize = 8;
                    break;
                case enumDBFields.idTarea:
                    paramName = "@idTarea";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.bMostrarPSN:
                    paramName = "@bMostrarPSN";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.bMostrarPT:
                    paramName = "@bMostrarPT";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.bMostrarF:
                    paramName = "@bMostrarF";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.bMostrarA:
                    paramName = "@bMostrarA";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.bMostrarT:
                    paramName = "@bMostrarT";
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
