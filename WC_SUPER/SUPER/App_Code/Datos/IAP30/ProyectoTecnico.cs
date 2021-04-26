using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ProyectoTecnico
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ProyectoTecnico 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t331_idpt = 1,
			t331_despt = 2,
			cod_une = 3,
			t303_denominacion = 4,
			t305_idproyectosubnodo = 5,
			t305_cualidad = 6,
			num_proyecto = 7,
			t301_estado = 8,
			nom_proyecto = 9,
			t331_estado = 10,
			t331_obligaest = 11,
			t331_orden = 12,
			t346_idpst = 13,
			t346_codpst = 14,
			t346_despst = 15,
			cod_cliente = 16,
			nom_cliente = 17,
			t331_desptlong = 18,
			t331_heredanodo = 19,
			t331_heredaproyeco = 20,
			t331_acceso_iap = 21,
			t305_admiterecursospst = 22,
			t305_avisorecursopst = 23,
			t305_accesobitacora_pst = 24,
			t301_esreplicable = 25
        }

        internal ProyectoTecnico(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un ProyectoTecnico
        ///// </summary>
        //internal int Insert(Models.ProyectoTecnico oProyectoTecnico)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[25] {
        //            Param(enumDBFields.t331_idpt, oProyectoTecnico.t331_idpt),
        //            Param(enumDBFields.t331_despt, oProyectoTecnico.t331_despt),
        //            Param(enumDBFields.cod_une, oProyectoTecnico.cod_une),
        //            Param(enumDBFields.t303_denominacion, oProyectoTecnico.t303_denominacion),
        //            Param(enumDBFields.t305_idproyectosubnodo, oProyectoTecnico.t305_idproyectosubnodo),
        //            Param(enumDBFields.t305_cualidad, oProyectoTecnico.t305_cualidad),
        //            Param(enumDBFields.num_proyecto, oProyectoTecnico.num_proyecto),
        //            Param(enumDBFields.t301_estado, oProyectoTecnico.t301_estado),
        //            Param(enumDBFields.nom_proyecto, oProyectoTecnico.nom_proyecto),
        //            Param(enumDBFields.t331_estado, oProyectoTecnico.t331_estado),
        //            Param(enumDBFields.t331_obligaest, oProyectoTecnico.t331_obligaest),
        //            Param(enumDBFields.t331_orden, oProyectoTecnico.t331_orden),
        //            Param(enumDBFields.t346_idpst, oProyectoTecnico.t346_idpst),
        //            Param(enumDBFields.t346_codpst, oProyectoTecnico.t346_codpst),
        //            Param(enumDBFields.t346_despst, oProyectoTecnico.t346_despst),
        //            Param(enumDBFields.cod_cliente, oProyectoTecnico.cod_cliente),
        //            Param(enumDBFields.nom_cliente, oProyectoTecnico.nom_cliente),
        //            Param(enumDBFields.t331_desptlong, oProyectoTecnico.t331_desptlong),
        //            Param(enumDBFields.t331_heredanodo, oProyectoTecnico.t331_heredanodo),
        //            Param(enumDBFields.t331_heredaproyeco, oProyectoTecnico.t331_heredaproyeco),
        //            Param(enumDBFields.t331_acceso_iap, oProyectoTecnico.t331_acceso_iap),
        //            Param(enumDBFields.t305_admiterecursospst, oProyectoTecnico.t305_admiterecursospst),
        //            Param(enumDBFields.t305_avisorecursopst, oProyectoTecnico.t305_avisorecursopst),
        //            Param(enumDBFields.t305_accesobitacora_pst, oProyectoTecnico.t305_accesobitacora_pst),
        //            Param(enumDBFields.t301_esreplicable, oProyectoTecnico.t301_esreplicable)
        //        };

        //        return (int)cDblib.Execute("_ProyectoTecnico_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene un ProyectoTecnico a partir del id
        ///// </summary>
        //internal Models.ProyectoTecnico Select()
        //{
        //    Models.ProyectoTecnico oProyectoTecnico = null;
        //    IDataReader dr = null;

        //    try
        //    {


        //        dr = cDblib.DataReader("_ProyectoTecnico_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oProyectoTecnico = new Models.ProyectoTecnico();
        //            oProyectoTecnico.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
        //            oProyectoTecnico.t331_despt=Convert.ToString(dr["t331_despt"]);
        //            oProyectoTecnico.cod_une=Convert.ToInt32(dr["cod_une"]);
        //            oProyectoTecnico.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
        //            oProyectoTecnico.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oProyectoTecnico.t305_cualidad=Convert.ToString(dr["t305_cualidad"]);
        //            oProyectoTecnico.num_proyecto=Convert.ToInt32(dr["num_proyecto"]);
        //            oProyectoTecnico.t301_estado=Convert.ToString(dr["t301_estado"]);
        //            oProyectoTecnico.nom_proyecto=Convert.ToString(dr["nom_proyecto"]);
        //            oProyectoTecnico.t331_estado=Convert.ToByte(dr["t331_estado"]);
        //            oProyectoTecnico.t331_obligaest=Convert.ToBoolean(dr["t331_obligaest"]);
        //            oProyectoTecnico.t331_orden=Convert.ToInt32(dr["t331_orden"]);
        //            if(!Convert.IsDBNull(dr["t346_idpst"]))
        //                oProyectoTecnico.t346_idpst=Convert.ToInt32(dr["t346_idpst"]);
        //            if(!Convert.IsDBNull(dr["t346_codpst"]))
        //                oProyectoTecnico.t346_codpst=Convert.ToString(dr["t346_codpst"]);
        //            if(!Convert.IsDBNull(dr["t346_despst"]))
        //                oProyectoTecnico.t346_despst=Convert.ToString(dr["t346_despst"]);
        //            oProyectoTecnico.cod_cliente=Convert.ToInt32(dr["cod_cliente"]);
        //            if(!Convert.IsDBNull(dr["nom_cliente"]))
        //                oProyectoTecnico.nom_cliente=Convert.ToString(dr["nom_cliente"]);
        //            oProyectoTecnico.t331_desptlong=Convert.ToString(dr["t331_desptlong"]);
        //            oProyectoTecnico.t331_heredanodo=Convert.ToBoolean(dr["t331_heredanodo"]);
        //            oProyectoTecnico.t331_heredaproyeco=Convert.ToBoolean(dr["t331_heredaproyeco"]);
        //            oProyectoTecnico.t331_acceso_iap=Convert.ToString(dr["t331_acceso_iap"]);
        //            oProyectoTecnico.t305_admiterecursospst=Convert.ToBoolean(dr["t305_admiterecursospst"]);
        //            oProyectoTecnico.t305_avisorecursopst=Convert.ToBoolean(dr["t305_avisorecursopst"]);
        //            oProyectoTecnico.t305_accesobitacora_pst=Convert.ToString(dr["t305_accesobitacora_pst"]);
        //            oProyectoTecnico.t301_esreplicable=Convert.ToBoolean(dr["t301_esreplicable"]);

        //        }
        //        return oProyectoTecnico;

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
        ///// Actualiza un ProyectoTecnico a partir del id
        ///// </summary>
        //internal int Update(Models.ProyectoTecnico oProyectoTecnico)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[25] {
        //            Param(enumDBFields.t331_idpt, oProyectoTecnico.t331_idpt),
        //            Param(enumDBFields.t331_despt, oProyectoTecnico.t331_despt),
        //            Param(enumDBFields.cod_une, oProyectoTecnico.cod_une),
        //            Param(enumDBFields.t303_denominacion, oProyectoTecnico.t303_denominacion),
        //            Param(enumDBFields.t305_idproyectosubnodo, oProyectoTecnico.t305_idproyectosubnodo),
        //            Param(enumDBFields.t305_cualidad, oProyectoTecnico.t305_cualidad),
        //            Param(enumDBFields.num_proyecto, oProyectoTecnico.num_proyecto),
        //            Param(enumDBFields.t301_estado, oProyectoTecnico.t301_estado),
        //            Param(enumDBFields.nom_proyecto, oProyectoTecnico.nom_proyecto),
        //            Param(enumDBFields.t331_estado, oProyectoTecnico.t331_estado),
        //            Param(enumDBFields.t331_obligaest, oProyectoTecnico.t331_obligaest),
        //            Param(enumDBFields.t331_orden, oProyectoTecnico.t331_orden),
        //            Param(enumDBFields.t346_idpst, oProyectoTecnico.t346_idpst),
        //            Param(enumDBFields.t346_codpst, oProyectoTecnico.t346_codpst),
        //            Param(enumDBFields.t346_despst, oProyectoTecnico.t346_despst),
        //            Param(enumDBFields.cod_cliente, oProyectoTecnico.cod_cliente),
        //            Param(enumDBFields.nom_cliente, oProyectoTecnico.nom_cliente),
        //            Param(enumDBFields.t331_desptlong, oProyectoTecnico.t331_desptlong),
        //            Param(enumDBFields.t331_heredanodo, oProyectoTecnico.t331_heredanodo),
        //            Param(enumDBFields.t331_heredaproyeco, oProyectoTecnico.t331_heredaproyeco),
        //            Param(enumDBFields.t331_acceso_iap, oProyectoTecnico.t331_acceso_iap),
        //            Param(enumDBFields.t305_admiterecursospst, oProyectoTecnico.t305_admiterecursospst),
        //            Param(enumDBFields.t305_avisorecursopst, oProyectoTecnico.t305_avisorecursopst),
        //            Param(enumDBFields.t305_accesobitacora_pst, oProyectoTecnico.t305_accesobitacora_pst),
        //            Param(enumDBFields.t301_esreplicable, oProyectoTecnico.t301_esreplicable)
        //        };

        //        return (int)cDblib.Execute("_ProyectoTecnico_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Elimina un ProyectoTecnico a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {


        //        return (int)cDblib.Execute("_ProyectoTecnico_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los ProyectoTecnico
        ///// </summary>
        //internal List<Models.ProyectoTecnico> Catalogo(Models.ProyectoTecnico oProyectoTecnicoFilter)
        //{
        //    Models.ProyectoTecnico oProyectoTecnico = null;
        //    List<Models.ProyectoTecnico> lst = new List<Models.ProyectoTecnico>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[25] {
        //            Param(enumDBFields.t331_idpt, oTEMP_ProyectoTecnicoFilter.t331_idpt),
        //            Param(enumDBFields.t331_despt, oTEMP_ProyectoTecnicoFilter.t331_despt),
        //            Param(enumDBFields.cod_une, oTEMP_ProyectoTecnicoFilter.cod_une),
        //            Param(enumDBFields.t303_denominacion, oTEMP_ProyectoTecnicoFilter.t303_denominacion),
        //            Param(enumDBFields.t305_idproyectosubnodo, oTEMP_ProyectoTecnicoFilter.t305_idproyectosubnodo),
        //            Param(enumDBFields.t305_cualidad, oTEMP_ProyectoTecnicoFilter.t305_cualidad),
        //            Param(enumDBFields.num_proyecto, oTEMP_ProyectoTecnicoFilter.num_proyecto),
        //            Param(enumDBFields.t301_estado, oTEMP_ProyectoTecnicoFilter.t301_estado),
        //            Param(enumDBFields.nom_proyecto, oTEMP_ProyectoTecnicoFilter.nom_proyecto),
        //            Param(enumDBFields.t331_estado, oTEMP_ProyectoTecnicoFilter.t331_estado),
        //            Param(enumDBFields.t331_obligaest, oTEMP_ProyectoTecnicoFilter.t331_obligaest),
        //            Param(enumDBFields.t331_orden, oTEMP_ProyectoTecnicoFilter.t331_orden),
        //            Param(enumDBFields.t346_idpst, oTEMP_ProyectoTecnicoFilter.t346_idpst),
        //            Param(enumDBFields.t346_codpst, oTEMP_ProyectoTecnicoFilter.t346_codpst),
        //            Param(enumDBFields.t346_despst, oTEMP_ProyectoTecnicoFilter.t346_despst),
        //            Param(enumDBFields.cod_cliente, oTEMP_ProyectoTecnicoFilter.cod_cliente),
        //            Param(enumDBFields.nom_cliente, oTEMP_ProyectoTecnicoFilter.nom_cliente),
        //            Param(enumDBFields.t331_desptlong, oTEMP_ProyectoTecnicoFilter.t331_desptlong),
        //            Param(enumDBFields.t331_heredanodo, oTEMP_ProyectoTecnicoFilter.t331_heredanodo),
        //            Param(enumDBFields.t331_heredaproyeco, oTEMP_ProyectoTecnicoFilter.t331_heredaproyeco),
        //            Param(enumDBFields.t331_acceso_iap, oTEMP_ProyectoTecnicoFilter.t331_acceso_iap),
        //            Param(enumDBFields.t305_admiterecursospst, oTEMP_ProyectoTecnicoFilter.t305_admiterecursospst),
        //            Param(enumDBFields.t305_avisorecursopst, oTEMP_ProyectoTecnicoFilter.t305_avisorecursopst),
        //            Param(enumDBFields.t305_accesobitacora_pst, oTEMP_ProyectoTecnicoFilter.t305_accesobitacora_pst),
        //            Param(enumDBFields.t301_esreplicable, oTEMP_ProyectoTecnicoFilter.t301_esreplicable)
        //        };

        //        dr = cDblib.DataReader("_ProyectoTecnico_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oProyectoTecnico = new Models.ProyectoTecnico();
        //            oProyectoTecnico.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
        //            oProyectoTecnico.t331_despt=Convert.ToString(dr["t331_despt"]);
        //            oProyectoTecnico.cod_une=Convert.ToInt32(dr["cod_une"]);
        //            oProyectoTecnico.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
        //            oProyectoTecnico.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oProyectoTecnico.t305_cualidad=Convert.ToString(dr["t305_cualidad"]);
        //            oProyectoTecnico.num_proyecto=Convert.ToInt32(dr["num_proyecto"]);
        //            oProyectoTecnico.t301_estado=Convert.ToString(dr["t301_estado"]);
        //            oProyectoTecnico.nom_proyecto=Convert.ToString(dr["nom_proyecto"]);
        //            oProyectoTecnico.t331_estado=Convert.ToByte(dr["t331_estado"]);
        //            oProyectoTecnico.t331_obligaest=Convert.ToBoolean(dr["t331_obligaest"]);
        //            oProyectoTecnico.t331_orden=Convert.ToInt32(dr["t331_orden"]);
        //            if(!Convert.IsDBNull(dr["t346_idpst"]))
        //                oProyectoTecnico.t346_idpst=Convert.ToInt32(dr["t346_idpst"]);
        //            if(!Convert.IsDBNull(dr["t346_codpst"]))
        //                oProyectoTecnico.t346_codpst=Convert.ToString(dr["t346_codpst"]);
        //            if(!Convert.IsDBNull(dr["t346_despst"]))
        //                oProyectoTecnico.t346_despst=Convert.ToString(dr["t346_despst"]);
        //            oProyectoTecnico.cod_cliente=Convert.ToInt32(dr["cod_cliente"]);
        //            if(!Convert.IsDBNull(dr["nom_cliente"]))
        //                oProyectoTecnico.nom_cliente=Convert.ToString(dr["nom_cliente"]);
        //            oProyectoTecnico.t331_desptlong=Convert.ToString(dr["t331_desptlong"]);
        //            oProyectoTecnico.t331_heredanodo=Convert.ToBoolean(dr["t331_heredanodo"]);
        //            oProyectoTecnico.t331_heredaproyeco=Convert.ToBoolean(dr["t331_heredaproyeco"]);
        //            oProyectoTecnico.t331_acceso_iap=Convert.ToString(dr["t331_acceso_iap"]);
        //            oProyectoTecnico.t305_admiterecursospst=Convert.ToBoolean(dr["t305_admiterecursospst"]);
        //            oProyectoTecnico.t305_avisorecursopst=Convert.ToBoolean(dr["t305_avisorecursopst"]);
        //            oProyectoTecnico.t305_accesobitacora_pst=Convert.ToString(dr["t305_accesobitacora_pst"]);
        //            oProyectoTecnico.t301_esreplicable=Convert.ToBoolean(dr["t301_esreplicable"]);

        //            lst.Add(oProyectoTecnico);

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
        internal Models.ProyectoTecnico Select(int t331_idpt)
        {
            Models.ProyectoTecnico oPT = null;
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t331_idpt, t331_idpt)
                };

                dr = cDblib.DataReader("SUP_PT_S", dbparams);
                if (dr.Read())
                {
                    oPT = new Models.ProyectoTecnico();
                    oPT.num_proyecto = Convert.ToInt32(dr["num_proyecto"]);
                    oPT.nom_proyecto = Convert.ToString(dr["nom_proyecto"]);
                    oPT.t301_estado = Convert.ToString(dr["t301_estado"]);
                    oPT.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                }
                return oPT;
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
				case enumDBFields.t331_idpt:
					paramName = "@t331_idpt";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t331_despt:
					paramName = "@t331_despt";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.cod_une:
					paramName = "@cod_une";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t303_denominacion:
					paramName = "@t303_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t305_idproyectosubnodo:
					paramName = "@t305_idproyectosubnodo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t305_cualidad:
					paramName = "@t305_cualidad";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
				case enumDBFields.num_proyecto:
					paramName = "@num_proyecto";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t301_estado:
					paramName = "@t301_estado";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
				case enumDBFields.nom_proyecto:
					paramName = "@nom_proyecto";
					paramType = SqlDbType.VarChar;
					paramSize = 70;
					break;
				case enumDBFields.t331_estado:
					paramName = "@t331_estado";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.t331_obligaest:
					paramName = "@t331_obligaest";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t331_orden:
					paramName = "@t331_orden";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t346_idpst:
					paramName = "@t346_idpst";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t346_codpst:
					paramName = "@t346_codpst";
					paramType = SqlDbType.VarChar;
					paramSize = 25;
					break;
				case enumDBFields.t346_despst:
					paramName = "@t346_despst";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.cod_cliente:
					paramName = "@cod_cliente";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.nom_cliente:
					paramName = "@nom_cliente";
					paramType = SqlDbType.VarChar;
					paramSize = 100;
					break;
				case enumDBFields.t331_desptlong:
					paramName = "@t331_desptlong";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t331_heredanodo:
					paramName = "@t331_heredanodo";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t331_heredaproyeco:
					paramName = "@t331_heredaproyeco";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t331_acceso_iap:
					paramName = "@t331_acceso_iap";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
				case enumDBFields.t305_admiterecursospst:
					paramName = "@t305_admiterecursospst";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t305_avisorecursopst:
					paramName = "@t305_avisorecursopst";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t305_accesobitacora_pst:
					paramName = "@t305_accesobitacora_pst";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
				case enumDBFields.t301_esreplicable:
					paramName = "@t301_esreplicable";
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
