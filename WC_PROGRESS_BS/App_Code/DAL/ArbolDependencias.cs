using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Árbol de dependencias
/// </summary>

namespace IB.Progress.DAL
{

    internal class ArbolDependencias
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t001_idficepi = 1,            
        }

        internal ArbolDependencias(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        public ArbolDependencias()
        {

            //lo dejo pero de momento no se usa
        }

        #endregion

        #region funciones publicas



        internal List<Models.ArbolDependencias> catalogoArbolDependencias(int t001_idficepi)
        {
            Models.ArbolDependencias oArbolDependencias = null;
            List<Models.ArbolDependencias> returnList = new List<Models.ArbolDependencias>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t001_idficepi, t001_idficepi.ToString())                    
                };
                dr = cDblib.DataReader("PRO_ARBOLDEPENDENCIAS", dbparams);
                while (dr.Read())
                {
                    oArbolDependencias = new Models.ArbolDependencias();
                    if (!Convert.IsDBNull(dr["idficepievaluador"]))
                        oArbolDependencias.idficepievaluador = int.Parse(dr["idficepievaluador"].ToString());
                    if (!Convert.IsDBNull(dr["evaluador"]))
                    oArbolDependencias.Evaluador = dr["evaluador"].ToString();
                    

                    if (!Convert.IsDBNull(dr["evaluadordelevaluador"]))
                        oArbolDependencias.Evaluadordelevaluador = Convert.ToInt32(dr["evaluadordelevaluador"]);

                    if (!Convert.IsDBNull(dr["roldelevaluador"]))
                        oArbolDependencias.Roldelevaluador = dr["roldelevaluador"].ToString();

                    if (!Convert.IsDBNull(dr["evaluadorpotencial"]))
                        oArbolDependencias.Evaluadorpotencial = bool.Parse(dr["evaluadorpotencial"].ToString());

                    if (!Convert.IsDBNull(dr["evaluadorrealizapruebas"]))
                        oArbolDependencias.Evaluadorrealizapruebas = bool.Parse(dr["evaluadorrealizapruebas"].ToString());

                    if (!Convert.IsDBNull(dr["evaluadorconvocadoapruebas"]))
                        oArbolDependencias.Evaluadorconvocadoapruebas = int.Parse(dr["evaluadorconvocadoapruebas"].ToString());

                    if (!Convert.IsDBNull(dr["evaluadoryoenibermatica"]))
                        oArbolDependencias.Evaluadoryoenibermatica = bool.Parse(dr["evaluadoryoenibermatica"].ToString());

                    if (!Convert.IsDBNull(dr["sexoevaluador"]))
                        oArbolDependencias.Sexo = dr["sexoevaluador"].ToString();

                    if (!Convert.IsDBNull(dr["colectivoevaluador"]))
                        oArbolDependencias.colectivoevaluador = int.Parse(dr["colectivoevaluador"].ToString());

                    returnList.Add(oArbolDependencias);
                }


                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        oArbolDependencias = new Models.ArbolDependencias();
                        if (!Convert.IsDBNull(dr["idficepievaluado"]))
                            oArbolDependencias.idficepievaluado = int.Parse(dr["idficepievaluado"].ToString());

                        if (!Convert.IsDBNull(dr["evaluado"]))
                            oArbolDependencias.Evaluado = dr["evaluado"].ToString();

                        if (!Convert.IsDBNull(dr["roldelevaluado"]))
                            oArbolDependencias.Roldelevaluado = dr["roldelevaluado"].ToString();

                        if (!Convert.IsDBNull(dr["elevaluadotienehijos"]))
                            oArbolDependencias.Elevaluadotienehijos = bool.Parse(dr["elevaluadotienehijos"].ToString());

                        if (!Convert.IsDBNull(dr["evaluadopotencial"]))
                            oArbolDependencias.Evaluadopotencial = bool.Parse(dr["evaluadopotencial"].ToString());

                        if (!Convert.IsDBNull(dr["evaluadoyoenibermatica"]))
                            oArbolDependencias.Evaluadoyoenibermatica = bool.Parse(dr["evaluadoyoenibermatica"].ToString());

                        if (!Convert.IsDBNull(dr["evaluadorealizapruebas"]))
                            oArbolDependencias.Evaluadorealizapruebas = bool.Parse(dr["evaluadorealizapruebas"].ToString());

                        if (!Convert.IsDBNull(dr["evaluadoconvocadoapruebas"]))
                            oArbolDependencias.Evaluadoconvocadoapruebas = int.Parse(dr["evaluadoconvocadoapruebas"].ToString());

                        if (!Convert.IsDBNull(dr["colectivoevaluado"]))
                            oArbolDependencias.colectivoevaluado = int.Parse(dr["colectivoevaluado"].ToString());

                        returnList.Add(oArbolDependencias);
                    }

                    
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



        internal List<Models.ArbolDependencias> catalogoArbolDependenciasAll(int t001_idficepi)
        {
            Models.ArbolDependencias oArbolDependencias = null;
            List<Models.ArbolDependencias> returnList = new List<Models.ArbolDependencias>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t001_idficepi, t001_idficepi.ToString())                    
                };
                dr = cDblib.DataReader("PRO_ARBOLDEPENDENCIAS_EXCELTOTAL", dbparams);
                while (dr.Read())
                {
                    oArbolDependencias = new Models.ArbolDependencias();

                    if (!Convert.IsDBNull(dr["nombre_evaluador"]))
                        oArbolDependencias.Evaluador = dr["nombre_evaluador"].ToString();

                    if (!Convert.IsDBNull(dr["rol_evaluador"]))
                        oArbolDependencias.Roldelevaluador = dr["rol_evaluador"].ToString();

                    if (!Convert.IsDBNull(dr["nombre_evaluado"]))
                        oArbolDependencias.Evaluado = dr["nombre_evaluado"].ToString();

                    if (!Convert.IsDBNull(dr["rol_evaluado"]))
                        oArbolDependencias.Roldelevaluado = dr["rol_evaluado"].ToString();
                  

                    returnList.Add(oArbolDependencias);
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




        internal List<Models.ArbolDependencias> SelectPotencialidad(int t001_idficepi)
        {
            Models.ArbolDependencias oArbolDependencias = null;
            List<Models.ArbolDependencias> returnList = new List<Models.ArbolDependencias>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t001_idficepi, t001_idficepi.ToString())                    
                };
                dr = cDblib.DataReader("PRO_POTENCIALIDAD", dbparams);
                while (dr.Read())
                {
                    oArbolDependencias = new Models.ArbolDependencias();

                    if (!Convert.IsDBNull(dr["realizapruebas"]))
                        oArbolDependencias.Realizapruebas = bool.Parse(dr["realizapruebas"].ToString());

                    if (!Convert.IsDBNull(dr["convocadoapruebas"]))
                        oArbolDependencias.Convocadoapruebas = int.Parse(dr["convocadoapruebas"].ToString());

                    if (!Convert.IsDBNull(dr["potfecha"]))
                        oArbolDependencias.Potfecha = DateTime.Parse(dr["potfecha"].ToString());

                    if (!Convert.IsDBNull(dr["potorigen"]))
                        oArbolDependencias.Potorigen = dr["potorigen"].ToString();

                    if (!Convert.IsDBNull(dr["espotencial"]))
                        oArbolDependencias.esPotencial = bool.Parse(dr["espotencial"].ToString());

                    returnList.Add(oArbolDependencias);
                }


                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        oArbolDependencias = new Models.ArbolDependencias();

                        if (!Convert.IsDBNull(dr["t2_iddocumento"]))
                            oArbolDependencias.T2_iddocumento = int.Parse(dr["t2_iddocumento"].ToString());

                        if (!Convert.IsDBNull(dr["t153_nombre"]))
                            oArbolDependencias.T153_nombre = dr["t153_nombre"].ToString();
                       
                        returnList.Add(oArbolDependencias);
                    }


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



        internal List<Models.ArbolDependencias> SelectYO(int t001_idficepi)
        {
            Models.ArbolDependencias oArbolDependencias = null;
            List<Models.ArbolDependencias> returnList = new List<Models.ArbolDependencias>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t001_idficepi, t001_idficepi.ToString())                    
                };
                dr = cDblib.DataReader("PRO_YOENIBERMATICA", dbparams);
                while (dr.Read())
                {
                    oArbolDependencias = new Models.ArbolDependencias();
                    if (!Convert.IsDBNull(dr["t2_iddocumento"]))
                        oArbolDependencias.T2_iddocumento = int.Parse(dr["t2_iddocumento"].ToString());

                    if (!Convert.IsDBNull(dr["t153_nombre"]))
                        oArbolDependencias.T153_nombre = dr["t153_nombre"].ToString();

                    returnList.Add(oArbolDependencias);
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
                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 2;
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
