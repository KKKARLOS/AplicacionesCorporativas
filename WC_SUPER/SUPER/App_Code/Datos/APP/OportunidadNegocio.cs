using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using IB.SUPER.APP.Models;

/// <summary>
/// Summary description for OportunidadNegocio
/// </summary>

namespace IB.SUPER.APP.DAL
{
    /// <summary>
    /// Descripción breve de OportunidadNegocio
    /// </summary>
    internal class OportunidadNegocio
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t303_idnodo = 1,
            desde=2,
            hasta=3,
            idOportunidad=4,
            denOportunidad=5,
            t302_idcliente_contrato=6,
            t314_idusuario_responsable=7,
            t314_idusuario_gestorprod=8,
            t314_idusuario_comercialhermes=9,
            t422_idmoneda=10,
            ta212_idorganizacioncomercial=11,
            t377_idextension=12,
            t377_fechacontratacion=13,
            t377_importeser=14,
            t377_marpreser=15,
            t377_importepro=16,
            t377_marprepro=18,
            nom_proyecto = 19,
            cod_cliente = 20,
            cod_contrato = 21,
            modalidad = 22,
            inicio_previsto = 23,
            fin_previsto = 24,
            categoria = 25,
            modelo_costes = 26,
            modelo_tarifas = 27,
            automatico = 28,
            fecha_sap = 29,
            den_proyecto = 30,
            cod_proyecto = 31,
            cod_subnodo = 32,
            cualidad = 33,
            cod_usuario_responsable = 34,
            seudonimo = 35,
            cod_naturaleza = 36,
            t195_idlineaoferta=37

        }
        internal OportunidadNegocio(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
        #endregion

        #region funciones publicas        

        /// <summary>
        /// Obtiene las oportunidades de negocio sin contrato en SUPER
        /// </summary>
        internal List<Models.OportunidadNegocio> CatalogoSinContrato(int idNodo, DateTime dtDesde, DateTime dtHasta)
        {
            Models.OportunidadNegocio oON = null;
            List<Models.OportunidadNegocio> lst = new List<Models.OportunidadNegocio>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t303_idnodo, idNodo),
                    Param(enumDBFields.desde, dtDesde),
                    Param(enumDBFields.hasta, dtHasta)
                };

                dr = cDblib.DataReader("SUP_BUSCAOPORT_HERMESNOSUPER", dbparams);
                while (dr.Read())
                {
                    oON = new Models.OportunidadNegocio();
                    oON.t306_icontrato = Convert.ToInt32(dr["t306_idcontrato"]);
                    oON.t377_idextension = Convert.ToInt32(dr["t377_idextension"]);
                    oON.t377_denominacion = Convert.ToString(dr["t377_denominacion"]);

                    oON.ta212_idorganizacioncomercial = Convert.ToInt32(dr["ta212_idorganizacioncomercial"]);
                    oON.ta212_denominacion = Convert.ToString(dr["ta212_denominacion"]);

                    oON.t302_idcliente_contrato = Convert.ToInt32(dr["t302_idcliente_contrato"]);
                    oON.cliente = Convert.ToString(dr["cliente"]);

                    oON.t314_idusuario_comercialhermes = Convert.ToInt32(dr["t314_idusuario_comercialhermes"]);
                    oON.comercial = Convert.ToString(dr["Comercial"]);

                    oON.t314_idusuario_gestorprod = Convert.ToInt32(dr["t314_idusuario_gestorprod"]);
                    oON.gestor = Convert.ToString(dr["Gest_Prod"]);

                    //oON.codune = Convert.ToInt32(dr["cod_une"]);
                    oON.t377_fechacontratacion = Convert.ToDateTime(dr["t377_fechacontratacion"].ToString());
                    //oON.nomune = Convert.ToString(dr["nom_une"]);

                    oON.t422_idmoneda = Convert.ToString(dr["t422_idmoneda"]);

                    oON.t377_importepro = Convert.ToDecimal(dr["t377_importepro"].ToString());
                    oON.t377_importeser = Convert.ToDecimal(dr["t377_importeser"].ToString());
                    oON.t377_marprepro = Convert.ToDecimal(dr["t377_marprepro"].ToString());
                    oON.t377_marpreser = Convert.ToDecimal(dr["t377_marpreser"].ToString());

                    //Necesario para la generación de proyectos
                    oON.duracion = Convert.ToDecimal(dr["DURMESES"].ToString());
                    oON.tipocontrato = Convert.ToString(dr["TIPOCONTRATO"]);
                    oON.codred_gestor_produccion = Convert.ToString(dr["CodRedGestorProduccion"]);
                    //Nueva línea de oferta
                    oON.t195_idlineaoferta = Convert.ToInt32(dr["t195_idlineaoferta"]);
                    oON.t195_denominacion = Convert.ToString(dr["t195_denominacion"]);

                    lst.Add(oON);

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

        internal int GenerarContrato(int t306_icontrato, int t314_idusuario_responsable)
        {
            //try
            //{
            SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.idOportunidad, t306_icontrato),
                    Param(enumDBFields.t314_idusuario_responsable, t314_idusuario_responsable)
                };

            return (int)cDblib.ExecuteScalar("SUP_ALTAOPORTHERMESNOSUPER", dbparams);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        internal int GenerarContrato(Models.OportunidadNegocio oON)
        {
            //try
            //{
                SqlParameter[] dbparams = new SqlParameter[9] {
                    Param(enumDBFields.idOportunidad, oON.t306_icontrato),
                    Param(enumDBFields.t303_idnodo, oON.t303_idnodo),
                    Param(enumDBFields.t302_idcliente_contrato, oON.t302_idcliente_contrato),
                    Param(enumDBFields.t314_idusuario_responsable, oON.t314_idusuario_responsable),
                    Param(enumDBFields.t314_idusuario_gestorprod, oON.t314_idusuario_gestorprod),
                    Param(enumDBFields.t314_idusuario_comercialhermes, oON.t314_idusuario_comercialhermes),
                    Param(enumDBFields.t422_idmoneda, oON.t422_idmoneda),
                    Param(enumDBFields.ta212_idorganizacioncomercial, oON.ta212_idorganizacioncomercial),
                    Param(enumDBFields.t195_idlineaoferta, oON.t195_idlineaoferta)
                };
            int idContrato = (int)cDblib.ExecuteScalar("SUP_CONTRATOHERMES_I", dbparams);

            return idContrato;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        internal int GenerarExtension(Models.OportunidadNegocio oON)
        {
            //try
            //{
                SqlParameter[] dbparams = new SqlParameter[8] {
                    Param(enumDBFields.idOportunidad, oON.t306_icontrato),
                    Param(enumDBFields.t377_idextension, oON.t377_idextension),
                    Param(enumDBFields.denOportunidad, oON.t377_denominacion),
                    Param(enumDBFields.t377_fechacontratacion, oON.t377_fechacontratacion),
                    Param(enumDBFields.t377_importeser, oON.t377_importeser),
                    Param(enumDBFields.t377_marpreser, oON.t377_marpreser),
                    Param(enumDBFields.t377_importepro, oON.t377_importepro),
                    Param(enumDBFields.t377_marprepro, oON.t377_marprepro)
                };

                return (int)cDblib.ExecuteScalar("SUP_EXTENSIONCONTRATOHERMES_I", dbparams);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        internal int GetNumProyectos(int t306_idcontrato)
        {
            int iNumProys = 0;
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.idOportunidad, t306_idcontrato)
                };

                dr = cDblib.DataReader("SUP_CONTRATO_COMP_BORRADO", dbparams);
                if (dr.Read())
                {
                    iNumProys = Convert.ToInt32(dr["numero"]);
                }
                return iNumProys;
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
                case enumDBFields.t303_idnodo:
                    paramName = "@t303_idnodo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.desde:
                    paramName = "@FECHAINI";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
                    break;
                case enumDBFields.hasta:
                    paramName = "@FECHAFIN";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
                    break; 
                case enumDBFields.idOportunidad:
                    paramName = "@t306_idcontrato";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t377_idextension:
                    paramName = "@t377_idextension";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.denOportunidad:
                    paramName = "@t377_denominacion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 70;
                    break; 
                case enumDBFields.t302_idcliente_contrato:
                    paramName = "@t302_idcliente_contrato";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break; 
                case enumDBFields.t314_idusuario_responsable:
                    paramName = "@t314_idusuario_responsable";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break; 
                case enumDBFields.t314_idusuario_gestorprod:
                    paramName = "@t314_idusuario_gestorprod";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break; 
                case enumDBFields.t314_idusuario_comercialhermes:
                    paramName = "@t314_idusuario_comercialhermes";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break; 
                case enumDBFields.t422_idmoneda:
                    paramName = "@t422_idmoneda";
                    paramType = SqlDbType.VarChar;
                    paramSize = 5;
                    break; 
                case enumDBFields.ta212_idorganizacioncomercial:
                    paramName = "@ta212_idorganizacioncomercial";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break; 
                case enumDBFields.t377_fechacontratacion:
                    paramName = "@t377_fechacontratacion";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
                    break;
                case enumDBFields.t377_importeser:
                    paramName = "@t377_importeser";
                    paramType = SqlDbType.Float;
                    paramSize = 8;
                    break;
                case enumDBFields.t377_marpreser:
                    paramName = "@t377_marpreser";
                    paramType = SqlDbType.Float;
                    paramSize = 8;
                    break;
                case enumDBFields.t377_importepro:
                    paramName = "@t377_importepro";
                    paramType = SqlDbType.Float;
                    paramSize = 8;
                    break;
                case enumDBFields.t377_marprepro:
                    paramName = "@t377_marprepro";
                    paramType = SqlDbType.Float;
                    paramSize = 8;
                    break;

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
                case enumDBFields.t195_idlineaoferta:
                    paramName = "@t195_idlineaoferta";
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