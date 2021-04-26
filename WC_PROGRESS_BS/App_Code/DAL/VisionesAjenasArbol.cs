using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;



namespace IB.Progress.DAL
{

    internal class VisionesAjenasArbol
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            idficepiVisualizador = 1, 
            idficepiVisualizado = 2,
            tabla = 3,
            accion = 4,
            datatable = 5,
            visualizador = 6
        }

        internal VisionesAjenasArbol(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        internal int Delete(int idficepiVisualizador, Nullable<int> idficepiVisualizado, string tabla)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.idficepiVisualizador, idficepiVisualizador),
                    Param(enumDBFields.idficepiVisualizado, idficepiVisualizado),
                    Param(enumDBFields.tabla, tabla)
				};

                return (int)cDblib.ExecuteScalar("PRO_DELVISUALIZADORVISUALIZADO", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VisionesAjenasArbol()
        {

            //lo dejo pero de momento no se usa
        }

        #endregion

        #region funciones publicas



        internal List<Models.VisionesAjenasArbol> catalogoVisionesAjenasArbol(string tabla, Nullable<int> t001_idficepi_visualizador)
        {
            Models.VisionesAjenasArbol oVisionesAjenasArbol = null;
            List<Models.VisionesAjenasArbol> returnList = new List<Models.VisionesAjenasArbol>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {					
					Param(enumDBFields.tabla, tabla),
                    Param(enumDBFields.idficepiVisualizador, t001_idficepi_visualizador)
					
				};
                dr = cDblib.DataReader("PRO_MANTENIMIENTOVISUALIZADOS", dbparams);
                while (dr.Read())
                {
                    oVisionesAjenasArbol = new Models.VisionesAjenasArbol();
                    oVisionesAjenasArbol.Idficepi_visualizador = int.Parse(dr["idficepi_visualizador"].ToString());
                    oVisionesAjenasArbol.Idficepi_visualizado = int.Parse(dr["idficepi_visualizado"].ToString());
                    oVisionesAjenasArbol.Nombre_visualizador = dr["nombre_visualizador"].ToString();
                    oVisionesAjenasArbol.Nombre_visualizado = dr["nombre_visualizado"].ToString();
                    oVisionesAjenasArbol.T949_accion = dr["t949_accion"].ToString();
                    oVisionesAjenasArbol.Sexo_Visualizador = dr["sexo_visualizador"].ToString();

                    returnList.Add(oVisionesAjenasArbol);
                }
                return returnList;
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

        internal List<Models.VisionesAjenasArbol> catalogoVisionesAjenasArbol2(string tabla, Nullable<int> t001_idficepi_visualizador)
        {
            Models.VisionesAjenasArbol oVisionesAjenasArbol = null;
            List<Models.VisionesAjenasArbol> returnList = new List<Models.VisionesAjenasArbol>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {					
					Param(enumDBFields.tabla, tabla),
                    Param(enumDBFields.idficepiVisualizador, t001_idficepi_visualizador)
					
				};
                dr = cDblib.DataReader("PRO_MANTENIMIENTOVISUALIZADOS", dbparams);
                while (dr.Read())
                {
                    oVisionesAjenasArbol = new Models.VisionesAjenasArbol();
                    oVisionesAjenasArbol.Idficepi_visualizador = int.Parse(dr["idficepi_supervisualizador"].ToString());
                    oVisionesAjenasArbol.Idficepi_visualizado = int.Parse(dr["idficepi_visualizador"].ToString());
                    oVisionesAjenasArbol.Nombre_visualizador = dr["nombre_supervisualizador"].ToString();
                    oVisionesAjenasArbol.Nombre_visualizado = dr["nombre_visualizador"].ToString();
                    
                    oVisionesAjenasArbol.Sexo_Visualizador = dr["sexo_supervisualizador"].ToString();
                    
                    returnList.Add(oVisionesAjenasArbol);
                }
                return returnList;
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



        internal string Insert(string tabla, int idficepi_visualizador, DataTable visualizados)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.visualizador, idficepi_visualizador),
                    Param(enumDBFields.datatable, visualizados),
                    Param(enumDBFields.tabla, tabla),
					
					
				};

                return (string)cDblib.Desc("PRO_PUTVISUALIZADORVISUALIZADO", dbparams);                
            }
            catch (Exception ex)
            {
                throw ex;
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
                case enumDBFields.idficepiVisualizador:
                    paramName = "@t001_idficepi_visualizador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.idficepiVisualizado:
                    paramName = "@t001_idficepi_visualizado";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.tabla:
                    paramName = "@tabla";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;

                case enumDBFields.accion:
                    paramName = "@t949_accion";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;

                case enumDBFields.datatable:
                    paramName = "@TMPVISUALIZADOS";
                    paramType = SqlDbType.Structured;
                    paramSize = 4;
                    break;

                case enumDBFields.visualizador:
                    paramName = "@visualizador";
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
