using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using IB.SUPER.APP.Models;

namespace IB.SUPER.APP.DAL
{
    public class ProyectoEconomico
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;

        private enum enumDBFields : byte
        {
            nom_proyecto = 1,
            cod_cliente = 2,
            cod_contrato = 3,
            modalidad = 4,
            inicio_previsto = 5,
            fin_previsto = 6,
            categoria = 7,
            modelo_costes = 8,
            modelo_tarifas = 9,
            automatico = 10,
            fecha_sap = 11,
            den_proyecto=12,
            cod_proyecto=13,
            cod_subnodo=14,
            cualidad=15,
            cod_usuario_responsable = 16,
            seudonimo=17,
            cod_naturaleza=18
        }
        internal ProyectoEconomico(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
        #endregion
        internal int GenerarProyecto(Models.ProyectoEconomico oPE)
        {
            //try
            //{
            SqlParameter[] dbparams = new SqlParameter[13] {
                    Param(enumDBFields.nom_proyecto, oPE.nom_proyecto),
                    Param(enumDBFields.den_proyecto, oPE.nom_proyecto),
                    Param(enumDBFields.cod_cliente, oPE.cod_cliente),
                    Param(enumDBFields.cod_contrato, oPE.cod_contrato),
                    Param(enumDBFields.cod_naturaleza, oPE.cod_naturaleza),
                    Param(enumDBFields.modalidad, oPE.modalidad),
                    Param(enumDBFields.inicio_previsto, oPE.fini_prevista),
                    Param(enumDBFields.fin_previsto, oPE.ffin_prevista),
                    Param(enumDBFields.categoria, oPE.categoria),
                    Param(enumDBFields.modelo_costes, oPE.modelo_coste),
                    Param(enumDBFields.modelo_tarifas, oPE.modelo_tarifa),
                    Param(enumDBFields.automatico, oPE.automatico),
                    Param(enumDBFields.fecha_sap, oPE.fecha_sap)
                };

            //return (int)cDblib.ExecuteScalar("ITZ_CREARPROYECTO", dbparams);
           int idPE = (int)cDblib.ExecuteScalar("SUP_CREARPROYECTO", dbparams);

            return idPE;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        internal int GenerarProyectoSubnodo(Models.ProyectoEconomico oPE)
        {
            SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(enumDBFields.cod_proyecto, oPE.cod_proyecto),
                    Param(enumDBFields.cod_subnodo, oPE.cod_subnodo),
                    Param(enumDBFields.cualidad, oPE.cualidad),
                    Param(enumDBFields.cod_usuario_responsable, oPE.cod_usuario_responsable),
                    Param(enumDBFields.seudonimo, oPE.seudonimo)
                };

            //return (int)cDblib.ExecuteScalar("ITZ_CREARPROYECTOSUBNODO", dbparams);
           int idPSN = (int)cDblib.ExecuteScalar("SUP_CREARPROYECTOSUBNODO", dbparams);

            return idPSN;
        }

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
                case enumDBFields.nom_proyecto:
                    paramName = "@t301_denominacion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 70;
                    break;
                case enumDBFields.den_proyecto:
                    paramName = "@t301_descripcion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 70;
                    break;
                case enumDBFields.cod_cliente:
                    paramName = "@t302_idcliente_proyecto";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.cod_contrato:
                    paramName = "@t306_idcontrato";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.cod_naturaleza:
                    paramName = "@t323_idnaturaleza";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.modalidad:
                    paramName = "@t316_idmodalidad";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.inicio_previsto:
                    paramName = "@t301_fiprev";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
                    break;
                case enumDBFields.fin_previsto:
                    paramName = "@t301_ffprev";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
                    break;
                case enumDBFields.categoria:
                    paramName = "@t301_categoria";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.modelo_costes:
                    paramName = "@t301_modelocoste";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.modelo_tarifas:
                    paramName = "@t301_modelotarif";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.automatico:
                    paramName = "@t301_automatico";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.fecha_sap:
                    paramName = "@t301_fechasap";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
                    break;

                case enumDBFields.cod_proyecto:
                    paramName = "@t301_idproyecto";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.cod_subnodo:
                    paramName = "@t304_idsubnodo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break; 
                case enumDBFields.cualidad:
                    paramName = "@t305_cualidad";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break; 
                case enumDBFields.cod_usuario_responsable:
                    paramName = "@t314_idusuario_responsable";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break; 
                case enumDBFields.seudonimo:
                    paramName = "@t305_seudonimo";
                    paramType = SqlDbType.VarChar;
                    paramSize = 70;
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