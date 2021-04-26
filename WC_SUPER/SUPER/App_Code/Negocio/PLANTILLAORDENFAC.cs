using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Collections;
using System.Collections.Generic;

namespace SUPER.Capa_Negocio
{
    public partial class PLANTILLAORDENFAC
    {
        #region Propiedades y Atributos complementarios

        private string _t302_denominacion_respago;
        public string t302_denominacion_respago
        {
            get { return _t302_denominacion_respago; }
            set { _t302_denominacion_respago = value; }
        }
        private string _t302_denominacion_destfact;
        public string t302_denominacion_destfact
        {
            get { return _t302_denominacion_destfact; }
            set { _t302_denominacion_destfact = value; }
        }

        private string _NifRespPago;
        public string NifRespPago
        {
            get { return _NifRespPago; }
            set { _NifRespPago = value; }
        }
        private string _NifDestFra;
        public string NifDestFra
        {
            get { return _NifDestFra; }
            set { _NifDestFra = value; }
        }

        private string _t622_denominacion;
        public string t622_denominacion
        {
            get { return _t622_denominacion; }
            set { _t622_denominacion = value; }
        }

        private string _direccion;
        public string direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        private int? _t301_idproyecto;
        public int? t301_idproyecto
        {
            get { return _t301_idproyecto; }
            set { _t301_idproyecto = value; }
        }

        private string _t301_denominacion;
        public string t301_denominacion
        {
            get { return _t301_denominacion; }
            set { _t301_denominacion = value; }
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T629_PLANTILLAORDENFAC.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	18/11/2010 10:31:34
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t629_denominacion, string t629_estado, int t314_idusuario, Nullable<int> t305_idproyectosubnodo, Nullable<int> t302_idcliente_solici, Nullable<int> t302_idcliente_respago, Nullable<int> t302_idcliente_destfact, string t629_condicionpago, string t629_viapago, string t629_refcliente, Nullable<DateTime> t629_fprevemifact, string t629_moneda, Nullable<int> t622_idagrupacion, string t629_observacionespool, string t629_comentario, string t621_idovsap, float t629_dto_porcen, decimal t629_dto_importe, bool t629_ivaincluido, string t629_observacionesplan, string t631_usuticks, string t629_textocabecera)
        {
            SqlParameter[] aParam = new SqlParameter[22];
            aParam[0] = new SqlParameter("@t629_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t629_denominacion;
            aParam[1] = new SqlParameter("@t629_estado", SqlDbType.Text, 1);
            aParam[1].Value = t629_estado;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;
            aParam[3] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[3].Value = t305_idproyectosubnodo;
            aParam[4] = new SqlParameter("@t302_idcliente_solici", SqlDbType.Int, 4);
            aParam[4].Value = t302_idcliente_solici;
            aParam[5] = new SqlParameter("@t302_idcliente_respago", SqlDbType.Int, 4);
            aParam[5].Value = t302_idcliente_respago;
            aParam[6] = new SqlParameter("@t302_idcliente_destfact", SqlDbType.Int, 4);
            aParam[6].Value = t302_idcliente_destfact;
            aParam[7] = new SqlParameter("@t629_condicionpago", SqlDbType.Text, 4);
            aParam[7].Value = t629_condicionpago;
            aParam[8] = new SqlParameter("@t629_viapago", SqlDbType.Text, 1);
            aParam[8].Value = t629_viapago;
            aParam[9] = new SqlParameter("@t629_refcliente", SqlDbType.Text, 35);
            aParam[9].Value = t629_refcliente;
            aParam[10] = new SqlParameter("@t629_fprevemifact", SqlDbType.SmallDateTime, 4);
            aParam[10].Value = t629_fprevemifact;
            aParam[11] = new SqlParameter("@t629_moneda", SqlDbType.Text, 5);
            aParam[11].Value = t629_moneda;
            aParam[12] = new SqlParameter("@t622_idagrupacion", SqlDbType.Int, 4);
            aParam[12].Value = t622_idagrupacion;
            aParam[13] = new SqlParameter("@t629_observacionespool", SqlDbType.Text, 2147483647);
            aParam[13].Value = t629_observacionespool;
            aParam[14] = new SqlParameter("@t629_comentario", SqlDbType.Text, 2147483647);
            aParam[14].Value = t629_comentario;
            aParam[15] = new SqlParameter("@t621_idovsap", SqlDbType.Text, 4);
            aParam[15].Value = t621_idovsap;
            aParam[16] = new SqlParameter("@t629_dto_porcen", SqlDbType.Real, 4);
            aParam[16].Value = t629_dto_porcen;
            aParam[17] = new SqlParameter("@t629_dto_importe", SqlDbType.Money, 8);
            aParam[17].Value = t629_dto_importe;
            aParam[18] = new SqlParameter("@t629_ivaincluido", SqlDbType.Bit, 1);
            aParam[18].Value = t629_ivaincluido;
            aParam[19] = new SqlParameter("@t629_observacionesplan", SqlDbType.Text, 2147483647);
            aParam[19].Value = t629_observacionesplan;
            aParam[20] = new SqlParameter("@t631_usuticks", SqlDbType.VarChar, 50);
            aParam[20].Value = t631_usuticks;
            aParam[21] = new SqlParameter("@t629_textocabecera", SqlDbType.Text, 2147483647);
            aParam[21].Value = t629_textocabecera;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PLANTILLAORDENFAC_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PLANTILLAORDENFAC_INS", aParam));
        }


        public static PLANTILLAORDENFAC Select(SqlTransaction tr, int t629_idplantillaof)
        {
            PLANTILLAORDENFAC o = new PLANTILLAORDENFAC();

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
            aParam[1] = new SqlParameter("@t629_idplantillaof", SqlDbType.Int, 4);
            aParam[1].Value = t629_idplantillaof;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLAORDENFAC_O", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PLANTILLAORDENFAC_O", aParam);

            if (dr.Read())
            {
                if (dr["t629_idplantillaof"] != DBNull.Value)
                    o.t629_idplantillaof = int.Parse(dr["t629_idplantillaof"].ToString());
                if (dr["t629_denominacion"] != DBNull.Value)
                    o.t629_denominacion = (string)dr["t629_denominacion"];
                if (dr["t629_estado"] != DBNull.Value)
                    o.t629_estado = (string)dr["t629_estado"];
                if (dr["t314_idusuario"] != DBNull.Value)
                    o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = int.Parse(dr["t305_idproyectosubnodo"].ToString());
                if (dr["t302_idcliente_solici"] != DBNull.Value)
                    o.t302_idcliente_solici = int.Parse(dr["t302_idcliente_solici"].ToString());
                if (dr["t302_idcliente_respago"] != DBNull.Value)
                    o.t302_idcliente_respago = int.Parse(dr["t302_idcliente_respago"].ToString());
                if (dr["t302_idcliente_destfact"] != DBNull.Value)
                    o.t302_idcliente_destfact = int.Parse(dr["t302_idcliente_destfact"].ToString());
                if (dr["t629_condicionpago"] != DBNull.Value)
                    o.t629_condicionpago = (string)dr["t629_condicionpago"];
                if (dr["t629_viapago"] != DBNull.Value)
                    o.t629_viapago = (string)dr["t629_viapago"];
                if (dr["t629_refcliente"] != DBNull.Value)
                    o.t629_refcliente = (string)dr["t629_refcliente"];
                if (dr["t629_fprevemifact"] != DBNull.Value)
                    o.t629_fprevemifact = (DateTime)dr["t629_fprevemifact"];
                if (dr["t629_moneda"] != DBNull.Value)
                    o.t629_moneda = (string)dr["t629_moneda"];
                if (dr["t622_idagrupacion"] != DBNull.Value)
                    o.t622_idagrupacion = int.Parse(dr["t622_idagrupacion"].ToString());
                if (dr["t629_observacionespool"] != DBNull.Value)
                    o.t629_observacionespool = (string)dr["t629_observacionespool"];
                if (dr["t629_comentario"] != DBNull.Value)
                    o.t629_comentario = (string)dr["t629_comentario"];
                if (dr["t621_idovsap"] != DBNull.Value)
                    o.t621_idovsap = (string)dr["t621_idovsap"];
                if (dr["t629_dto_porcen"] != DBNull.Value)
                    o.t629_dto_porcen = float.Parse(dr["t629_dto_porcen"].ToString());
                if (dr["t629_dto_importe"] != DBNull.Value)
                    o.t629_dto_importe = decimal.Parse(dr["t629_dto_importe"].ToString());
                if (dr["t629_ivaincluido"] != DBNull.Value)
                    o.t629_ivaincluido = (bool)dr["t629_ivaincluido"];
                if (dr["t629_observacionesplan"] != DBNull.Value)
                    o.t629_observacionesplan = (string)dr["t629_observacionesplan"];

                if (dr["t302_denominacion_respago"] != DBNull.Value)
                    o.t302_denominacion_respago = (string)dr["t302_denominacion_respago"];
                if (dr["t302_denominacion_destfact"] != DBNull.Value)
                    o.t302_denominacion_destfact = (string)dr["t302_denominacion_destfact"];
                if (dr["NifRespPago"] != DBNull.Value)
                    o.NifRespPago = (string)dr["NifRespPago"];
                if (dr["NifDestFra"] != DBNull.Value)
                    o.NifDestFra = (string)dr["NifDestFra"];
                if (dr["t622_denominacion"] != DBNull.Value)
                    o.t622_denominacion = (string)dr["t622_denominacion"];
                if (dr["direccion"] != DBNull.Value)
                    o.direccion = (string)dr["direccion"];
                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.t301_idproyecto = int.Parse(dr["t301_idproyecto"].ToString());
                if (dr["t301_denominacion"] != DBNull.Value)
                    o.t301_denominacion = (string)dr["t301_denominacion"];
                if (dr["t629_textocabecera"] != DBNull.Value)
                    o.t629_textocabecera = (string)dr["t629_textocabecera"];
                if (dr["t302_efactur"] != DBNull.Value)
                    o.t302_efactur = (bool)dr["t302_efactur"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de PLANTILLAORDENFAC"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static SqlDataReader CatalogoADM(SqlTransaction tr, Nullable<int> t302_idcliente, Nullable<int> t305_idproyectosubnodo, Nullable<int> t629_idplantillaof)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@entorno", SqlDbType.Char, 1, System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper());
            aParam[i++] = ParametroSql.add("@t302_idcliente", SqlDbType.Int, 4, t302_idcliente);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t629_idplantillaof", SqlDbType.Int, 4, t629_idplantillaof);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLASOF_ADM", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PLANTILLASOF_ADM", aParam);
        }

        public static SqlDataReader CatalogoPrivadas(SqlTransaction tr, int t314_idusuario, Nullable<int> t302_idcliente, Nullable<int> t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[2].Value = t302_idcliente;
            aParam[3] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[3].Value = t305_idproyectosubnodo;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLASOF_PRIVADAS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PLANTILLASOF_PRIVADAS", aParam);
        }
        public static SqlDataReader CatalogoProyectos(SqlTransaction tr, int t314_idusuario, Nullable<int> t302_idcliente, Nullable<int> t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[2].Value = t302_idcliente;
            aParam[3] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[3].Value = t305_idproyectosubnodo;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLASOF_PROYECTOS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PLANTILLASOF_PROYECTOS", aParam);
        }
        public static SqlDataReader CatalogoFavoritas(SqlTransaction tr, int t314_idusuario, Nullable<int> t302_idcliente, Nullable<int> t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[2].Value = t302_idcliente;
            aParam[3] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[3].Value = t305_idproyectosubnodo;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLASOF_FAVORITAS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PLANTILLASOF_FAVORITAS", aParam);
        }

        public static SqlDataReader Previsualizar(int t629_idplantillaof)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
            aParam[1] = new SqlParameter("@t629_idplantillaof", SqlDbType.Int, 4);
            aParam[1].Value = t629_idplantillaof;

            return SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLAORDENFAC_PREV_O", aParam);
        }

        //public static int Privatizar(int t629_idplantillaof, int t314_idusuario)
        //{
        //    SqlParameter[] aParam = new SqlParameter[2];
        //    aParam[0] = new SqlParameter("@t629_idplantillaof", SqlDbType.Int, 4);
        //    aParam[0].Value = t629_idplantillaof;
        //    aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
        //    aParam[1].Value = t314_idusuario;

        //    return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PLANTILLAORDENFAC_ByPlantilla", aParam));
        //}

        public static int CrearDesdeOrden(SqlTransaction tr, int t610_idordenfac, int t314_idusuario, string sAccDoc)
        {
            int idPlant = -1;
            idPlant = SUPER.DAL.PLANTILLAORDENFAC.CrearDesdeOrden(tr, t610_idordenfac, t314_idusuario);
            switch (sAccDoc)
            {
                case "M":
                    SUPER.DAL.PLANTILLAORDENFAC.MantenerDocsOF(tr, t610_idordenfac, t314_idusuario, idPlant);
                    break;
                case "G":
                    CopiarDocsOFaPlantilla(tr, t610_idordenfac, t314_idusuario, idPlant);
                    break;
            }

            return idPlant;
        }
        public static int CrearDesdePlantilla(SqlTransaction tr, int t629_idplantillaof, int t314_idusuario, string sAccDoc)
        {
            int idNewPlant = -1;
            idNewPlant = SUPER.DAL.PLANTILLAORDENFAC.CrearDesdePlantilla(tr, t629_idplantillaof, t314_idusuario);
            switch (sAccDoc)
            {
                case "M":
                    SUPER.DAL.PLANTILLAORDENFAC.MantenerDocs(tr, t629_idplantillaof, t314_idusuario, idNewPlant);
                    break;
                case "G":
                    DuplicarDocsPlantilla(tr, t629_idplantillaof, t314_idusuario, idNewPlant);
                    break;
            }

            return idNewPlant;
        }
        /// <summary>
        /// Recorre la lista de documentos asociados a la orden de facturación
        /// Genera un copia en el Content-Server y crea un nuevo registro de documento con el nuevo identificador
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t610_idordenfac"></param>
        /// <param name="t314_idusuario"></param>
        /// <param name="iNewOrdenFac"></param>
        public static void CopiarDocsOFaPlantilla(SqlTransaction tr, int t610_idordenfac, int t314_idusuario, int t629_idplantillaof)
        {
            long idContentServer = -1;
            //Obtengo la lista de documentos asociados a la orden de facturación origen
            List<DOCUOF> Lista = SUPER.Capa_Negocio.DOCUOF.Lista(tr, t610_idordenfac);
            foreach (DOCUOF oDoc in Lista)
            {
                //Inserto el archivo en Atenea
                idContentServer = IB.Conserva.ConservaHelper.SubirDocumento(oDoc.t624_nombrearchivo, oDoc.t624_archivo);
                //Inserto un registro en la tabal de documentos de orden de facturación con referencia al nuevo documento en Atenea
                SUPER.DAL.PLANTILLADOCUOF.Insert(tr, t629_idplantillaof, oDoc.t624_descripcion, oDoc.t624_nombrearchivo, 
                                                 idContentServer, t314_idusuario);
            }
        }
        /// <summary>
        /// Recorre la lista de documentos asociados a la plantilla de orden de facturación
        /// Genera un copia en el Content-Server y crea un nuevo registro de documento con el nuevo identificador
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t629_idplantillaof"></param>
        /// <param name="t314_idusuario"></param>
        /// <param name="iNewPlant"></param>
        public static void DuplicarDocsPlantilla(SqlTransaction tr, int t629_idplantillaof, int t314_idusuario, int idNewPlant)
        {
            long idContentServer = -1;
            //Obtengo la lista de documentos asociados a la orden de facturación origen
            List<PLANTILLADOCUOF> Lista = SUPER.Capa_Negocio.PLANTILLADOCUOF.Lista(tr, t629_idplantillaof);
            foreach (PLANTILLADOCUOF oDoc in Lista)
            {
                //Inserto el archivo en Atenea
                idContentServer = IB.Conserva.ConservaHelper.SubirDocumento(oDoc.t631_nombrearchivo, oDoc.t631_archivo);
                //Inserto un registro en la tabal de documentos de orden de facturación con referencia al nuevo documento en Atenea
                SUPER.DAL.PLANTILLADOCUOF.Insert(tr, idNewPlant, oDoc.t631_descripcion, oDoc.t631_nombrearchivo, 
                                                 idContentServer, t314_idusuario);
            }
        }

        public static bool HayDocs(SqlTransaction tr, string slPlantillas)
        {
            return SUPER.DAL.PLANTILLAORDENFAC.HayDocs(tr, slPlantillas);
        }
        #endregion
    }
}
