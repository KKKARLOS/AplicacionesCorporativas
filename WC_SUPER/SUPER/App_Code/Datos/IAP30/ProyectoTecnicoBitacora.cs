using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ProyectoTecnicoBitacora
/// </summary>

namespace IB.SUPER.IAP30.DAL
{
    internal class ProyectoTecnicoBitacora
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            idPT = 1,
            t305_idproyectosubnodo = 2,
            sNomPT = 3,
            t314_idusuario = 4,
            tipoBusq = 5
        }

        internal ProyectoTecnicoBitacora(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        #region funciones publicas
        ///// <summary>
        ///// Inserta un ProyectoTecnicoBitacora
        ///// </summary>
        //internal int Insert(Models.ProyectoTecnicoBitacora oProyectoTecnicoBitacora)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[9] {
        //            Param(enumDBFields.cod_pt, oProyectoTecnicoBitacora.cod_pt),
        //            Param(enumDBFields.nom_pt, oProyectoTecnicoBitacora.nom_pt),
        //            Param(enumDBFields.cod_pe, oProyectoTecnicoBitacora.cod_pe),
        //            Param(enumDBFields.nom_pe, oProyectoTecnicoBitacora.nom_pe),
        //            Param(enumDBFields.t301_estado, oProyectoTecnicoBitacora.t301_estado),
        //            Param(enumDBFields.t305_idproyectosubnodo, oProyectoTecnicoBitacora.t305_idproyectosubnodo),
        //            Param(enumDBFields.cod_une, oProyectoTecnicoBitacora.cod_une),
        //            Param(enumDBFields.t331_orden, oProyectoTecnicoBitacora.t331_orden),
        //            Param(enumDBFields.t331_acceso_iap, oProyectoTecnicoBitacora.t331_acceso_iap)
        //        };

        //        return (int)cDblib.Execute("_ProyectoTecnicoBitacora_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Obtiene un ProyectoTecnicoBitacora a partir del id
        /// </summary>
        internal Models.ProyectoTecnicoBitacora Select(int idPT)
        {
            Models.ProyectoTecnicoBitacora oProyectoTecnicoBitacora = null;
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                            Param(enumDBFields.idPT, idPT),
                        };
                dr = cDblib.DataReader("SUP_PT_S", dbparams);
                if (dr.Read())
                {
                    oProyectoTecnicoBitacora = new Models.ProyectoTecnicoBitacora();
                    oProyectoTecnicoBitacora.cod_pt = Convert.ToInt32(dr["t331_idpt"]);
                    oProyectoTecnicoBitacora.nom_pt = Convert.ToString(dr["t331_despt"]);
                    oProyectoTecnicoBitacora.cod_pe = Convert.ToInt32(dr["num_proyecto"]);
                    oProyectoTecnicoBitacora.nom_pe = Convert.ToString(dr["nom_proyecto"]);
                    oProyectoTecnicoBitacora.t301_estado = Convert.ToString(dr["t301_estado"]);
                    oProyectoTecnicoBitacora.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oProyectoTecnicoBitacora.cod_une = Convert.ToInt32(dr["cod_une"]);
                    oProyectoTecnicoBitacora.t331_orden = Convert.ToInt32(dr["t331_orden"]);
                    oProyectoTecnicoBitacora.t331_acceso_iap = Convert.ToString(dr["t331_acceso_iap"]);
                    oProyectoTecnicoBitacora.t305_accesobitacora_pst = Convert.ToString(dr["t305_accesobitacora_pst"]);
                }
                return oProyectoTecnicoBitacora;

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
        ///// Actualiza un ProyectoTecnicoBitacora a partir del id
        ///// </summary>
        //internal int Update(Models.ProyectoTecnicoBitacora oProyectoTecnicoBitacora)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[9] {
        //            Param(enumDBFields.cod_pt, oProyectoTecnicoBitacora.cod_pt),
        //            Param(enumDBFields.nom_pt, oProyectoTecnicoBitacora.nom_pt),
        //            Param(enumDBFields.cod_pe, oProyectoTecnicoBitacora.cod_pe),
        //            Param(enumDBFields.nom_pe, oProyectoTecnicoBitacora.nom_pe),
        //            Param(enumDBFields.t301_estado, oProyectoTecnicoBitacora.t301_estado),
        //            Param(enumDBFields.t305_idproyectosubnodo, oProyectoTecnicoBitacora.t305_idproyectosubnodo),
        //            Param(enumDBFields.cod_une, oProyectoTecnicoBitacora.cod_une),
        //            Param(enumDBFields.t331_orden, oProyectoTecnicoBitacora.t331_orden),
        //            Param(enumDBFields.t331_acceso_iap, oProyectoTecnicoBitacora.t331_acceso_iap)
        //        };

        //        return (int)cDblib.Execute("_ProyectoTecnicoBitacora_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Elimina un ProyectoTecnicoBitacora a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {


        //        return (int)cDblib.Execute("_ProyectoTecnicoBitacora_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los ProyectoTecnicoBitacora
        ///// </summary>
        //internal List<Models.ProyectoTecnicoBitacora> Catalogo(Models.ProyectoTecnicoBitacora oProyectoTecnicoBitacoraFilter)
        //{
        //    Models.ProyectoTecnicoBitacora oProyectoTecnicoBitacora = null;
        //    List<Models.ProyectoTecnicoBitacora> lst = new List<Models.ProyectoTecnicoBitacora>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[9] {
        //            Param(enumDBFields.cod_pt, oTEMP_ProyectoTecnicoBitacoraFilter.cod_pt),
        //            Param(enumDBFields.nom_pt, oTEMP_ProyectoTecnicoBitacoraFilter.nom_pt),
        //            Param(enumDBFields.cod_pe, oTEMP_ProyectoTecnicoBitacoraFilter.cod_pe),
        //            Param(enumDBFields.nom_pe, oTEMP_ProyectoTecnicoBitacoraFilter.nom_pe),
        //            Param(enumDBFields.t301_estado, oTEMP_ProyectoTecnicoBitacoraFilter.t301_estado),
        //            Param(enumDBFields.t305_idproyectosubnodo, oTEMP_ProyectoTecnicoBitacoraFilter.t305_idproyectosubnodo),
        //            Param(enumDBFields.cod_une, oTEMP_ProyectoTecnicoBitacoraFilter.cod_une),
        //            Param(enumDBFields.t331_orden, oTEMP_ProyectoTecnicoBitacoraFilter.t331_orden),
        //            Param(enumDBFields.t331_acceso_iap, oTEMP_ProyectoTecnicoBitacoraFilter.t331_acceso_iap)
        //        };

        //        dr = cDblib.DataReader("_ProyectoTecnicoBitacora_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oProyectoTecnicoBitacora = new Models.ProyectoTecnicoBitacora();
        //            oProyectoTecnicoBitacora.cod_pt=Convert.ToInt32(dr["cod_pt"]);
        //            oProyectoTecnicoBitacora.nom_pt=Convert.ToString(dr["nom_pt"]);
        //            oProyectoTecnicoBitacora.cod_pe=Convert.ToInt32(dr["cod_pe"]);
        //            oProyectoTecnicoBitacora.nom_pe=Convert.ToString(dr["nom_pe"]);
        //            oProyectoTecnicoBitacora.t301_estado=Convert.ToString(dr["t301_estado"]);
        //            oProyectoTecnicoBitacora.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oProyectoTecnicoBitacora.cod_une=Convert.ToInt32(dr["cod_une"]);
        //            oProyectoTecnicoBitacora.t331_orden=Convert.ToInt32(dr["t331_orden"]);
        //            oProyectoTecnicoBitacora.t331_acceso_iap=Convert.ToString(dr["t331_acceso_iap"]);

        //            lst.Add(oProyectoTecnicoBitacora);

        //        }
        //        return lst;

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
        /// Obtiene todos los ProyectoTecnico de un PSN que tienen bitácora
        /// </summary>
        internal List<Models.ProyectoTecnico> Catalogo(int idPSN, int idUser, bool bAdmin)
        {
            Models.ProyectoTecnico oProyectoTecnico = null;
            List<Models.ProyectoTecnico> lst = new List<Models.ProyectoTecnico>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
                    Param(enumDBFields.t305_idproyectosubnodo, idPSN),
                    Param(enumDBFields.sNomPT, ""),
                    Param(enumDBFields.t314_idusuario, idUser),
                    Param(enumDBFields.tipoBusq, "I")
                };
                //A este método se le llama desde la Bitácora de PE, por lo que si tiene permiso para estar aquí
                //hay que mostrarle todos los PT que tengan bitácora
                //if (bAdmin)
                dr = cDblib.DataReader("SUP_PT_BIT_C_ADMIN", dbparams);
                //else
                //    dr = cDblib.DataReader("SUP_PT_BIT_C", dbparams);
                while (dr.Read())
                {
                    oProyectoTecnico = new Models.ProyectoTecnico();
                    oProyectoTecnico.t331_idpt = Convert.ToInt32(dr["cod_pt"]);
                    oProyectoTecnico.t331_despt = Convert.ToString(dr["nom_pt"]);
                    oProyectoTecnico.num_proyecto = Convert.ToInt32(dr["cod_pe"]);
                    oProyectoTecnico.nom_proyecto = Convert.ToString(dr["nom_pe"]);
                    oProyectoTecnico.t331_acceso_iap = Convert.ToString(dr["t331_acceso_iap"]);

                    lst.Add(oProyectoTecnico);
                }
                return lst;
            }
            catch (Exception ex) { throw ex; }
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
                case enumDBFields.idPT:
                    paramName = "@t331_idpt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t305_idproyectosubnodo:
                    paramName = "@t305_idproyectosubnodo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.sNomPT:
                    paramName = "@sNomPT";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.t314_idusuario:
                    paramName = "@t314_idusuario";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.tipoBusq:
                    paramName = "@sTipoBusq";
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
