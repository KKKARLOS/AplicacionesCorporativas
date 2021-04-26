using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Documento
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class Documento 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            // Documentación detalle Tarea

			t363_iddocut = 1,
			t332_idtarea = 2,
			t363_descripcion = 3,
			t363_weblink = 4,
			t363_nombrearchivo = 5,
			t2_iddocumento = 6,
			t363_privado = 7,
			t363_modolectura = 8,
			t363_tipogestion = 9,
			t314_idusuario_autor = 10,
			t363_fecha = 11,
			autor = 12,
			t314_idusuario_modif = 13,
			t363_fechamodif = 14,
			autormodif = 15,

            // Documentación detalle Asunto PE

            t382_idasunto = 16,
            t386_autor = 17,
            t386_iddocasu = 18,
            t386_descripcion = 19,
            t386_weblink = 20,
            t386_nombrearchivo = 21,
            t386_privado = 22,
            t386_modolectura = 23,
            t386_tipogestion = 24,
            t386_autormodif = 25,
            t386_fecha = 26,

            // Documentación detalle Accion PE

            t383_idaccion = 27,
            t387_autor = 28,
            t387_iddocacc = 29,
            t387_descripcion = 30,
            t387_weblink = 31,
            t387_nombrearchivo = 32,
            t387_privado = 33,
            t387_modolectura = 34,
            t387_tipogestion = 35,
            t387_autormodif = 36,
            t387_fecha = 37,

             // Documentación detalle Asunto PT

            t409_idasunto = 38,
            t411_autor = 39,
            t411_iddocasu = 40,
            t411_descripcion = 41,
            t411_weblink = 42,
            t411_nombrearchivo = 43,
            t411_privado = 44,
            t411_modolectura = 45,
            t411_tipogestion = 46,
            t411_autormodif = 47,
            t411_fecha = 48,

            // Documentación detalle Accion PT

            t410_idaccion = 49,
            t412_autor = 50,
            t412_iddocacc = 51,
            t412_descripcion = 52,
            t412_weblink = 53,
            t412_nombrearchivo = 54,
            t412_privado = 55,
            t412_modolectura = 56,
            t412_tipogestion = 57,
            t412_autormodif = 58,
            t412_fecha = 59,

            // Documentación detalle Asunto TA

            t600_idasunto = 60,
            t602_autor = 61,
            t602_iddocasu = 62,
            t602_descripcion = 63,
            t602_weblink = 64,
            t602_nombrearchivo = 65,
            t602_privado = 66,
            t602_modolectura = 67,
            t602_tipogestion = 68,
            t602_autormodif = 69,
            t602_fecha = 70,

            // Documentación detalle Accion TA

            t601_idaccion = 71,
            t603_autor = 72,
            t603_iddocacc = 73,
            t603_descripcion = 74,
            t603_weblink = 75,
            t603_nombrearchivo = 76,
            t603_privado = 77,
            t603_modolectura = 78,
            t603_tipogestion = 79,
            t603_autormodif = 80,
            t603_fecha = 81       
        }

        internal Documento(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un Documento
        /// </summary>
        internal int Insert(BLL.Documento.enumOrigenEdicion enumProp, Models.Documento oDocumento)
        {
            try
            {
                string nomProc = "";
                SqlParameter[] dbparams = null;

                switch (enumProp)
                {
                    case BLL.Documento.enumOrigenEdicion.detalleTarea:
                        nomProc = "SUP_DOCUT_I";
                        dbparams = new SqlParameter[9] {
                            Param(enumDBFields.t332_idtarea, oDocumento.idElemento),
                            Param(enumDBFields.t363_descripcion, oDocumento.descripcion),
                            Param(enumDBFields.t363_weblink, oDocumento.weblink),
                            Param(enumDBFields.t363_nombrearchivo, oDocumento.nombrearchivo),
                            Param(enumDBFields.t2_iddocumento, oDocumento.t2_iddocumento),
                            Param(enumDBFields.t363_privado, oDocumento.privado),
                            Param(enumDBFields.t363_modolectura, oDocumento.modolectura),
                            Param(enumDBFields.t363_tipogestion, oDocumento.tipogestion),
                            Param(enumDBFields.t314_idusuario_autor, oDocumento.idusuario_autor)
                        };
                        
                        break;

                    case BLL.Documento.enumOrigenEdicion.detalleAsuntoPE:
                        nomProc = "SUP_DOCASU_I";
                        dbparams = new SqlParameter[10] {
                            Param(enumDBFields.t382_idasunto, oDocumento.idElemento),
                            Param(enumDBFields.t386_descripcion, oDocumento.descripcion),
                            Param(enumDBFields.t386_weblink, oDocumento.weblink),
                            Param(enumDBFields.t386_nombrearchivo, oDocumento.nombrearchivo),
                            Param(enumDBFields.t2_iddocumento, oDocumento.t2_iddocumento),
                            Param(enumDBFields.t386_privado, oDocumento.privado),
                            Param(enumDBFields.t386_modolectura, oDocumento.modolectura),
                            Param(enumDBFields.t386_tipogestion, oDocumento.tipogestion),
                            Param(enumDBFields.t386_autor, oDocumento.idusuario_autor),
                            Param(enumDBFields.t386_autormodif, oDocumento.idusuario_modif)
                        };

                        break;

                    case BLL.Documento.enumOrigenEdicion.detalleAccionPE:
                        nomProc = "SUP_DOCACC_I";
                        dbparams = new SqlParameter[10] {
                            Param(enumDBFields.t383_idaccion, oDocumento.idElemento),
                            Param(enumDBFields.t387_descripcion, oDocumento.descripcion),
                            Param(enumDBFields.t387_weblink, oDocumento.weblink),
                            Param(enumDBFields.t387_nombrearchivo, oDocumento.nombrearchivo),
                            Param(enumDBFields.t2_iddocumento, oDocumento.t2_iddocumento),
                            Param(enumDBFields.t387_privado, oDocumento.privado),
                            Param(enumDBFields.t387_modolectura, oDocumento.modolectura),
                            Param(enumDBFields.t387_tipogestion, oDocumento.tipogestion),
                            Param(enumDBFields.t387_autor, oDocumento.idusuario_autor),
                            Param(enumDBFields.t387_autormodif, oDocumento.idusuario_modif)
                        };

                        break;
                    case BLL.Documento.enumOrigenEdicion.detalleAsuntoPT:
                        nomProc = "SUP_DOCASU_PT_I";
                        dbparams = new SqlParameter[10] {
                            Param(enumDBFields.t409_idasunto, oDocumento.idElemento),
                            Param(enumDBFields.t411_descripcion, oDocumento.descripcion),
                            Param(enumDBFields.t411_weblink, oDocumento.weblink),
                            Param(enumDBFields.t411_nombrearchivo, oDocumento.nombrearchivo),
                            Param(enumDBFields.t2_iddocumento, oDocumento.t2_iddocumento),
                            Param(enumDBFields.t411_privado, oDocumento.privado),
                            Param(enumDBFields.t411_modolectura, oDocumento.modolectura),
                            Param(enumDBFields.t411_tipogestion, oDocumento.tipogestion),
                            Param(enumDBFields.t411_autor, oDocumento.idusuario_autor),
                            Param(enumDBFields.t411_autormodif, oDocumento.idusuario_modif)
                        };

                        break;

                    case BLL.Documento.enumOrigenEdicion.detalleAccionPT:
                        nomProc = "SUP_DOCACC_PT_I";
                        dbparams = new SqlParameter[10] {
                            Param(enumDBFields.t410_idaccion, oDocumento.idElemento),
                            Param(enumDBFields.t412_descripcion, oDocumento.descripcion),
                            Param(enumDBFields.t412_weblink, oDocumento.weblink),
                            Param(enumDBFields.t412_nombrearchivo, oDocumento.nombrearchivo),
                            Param(enumDBFields.t2_iddocumento, oDocumento.t2_iddocumento),
                            Param(enumDBFields.t412_privado, oDocumento.privado),
                            Param(enumDBFields.t412_modolectura, oDocumento.modolectura),
                            Param(enumDBFields.t412_tipogestion, oDocumento.tipogestion),
                            Param(enumDBFields.t412_autor, oDocumento.idusuario_autor),
                            Param(enumDBFields.t412_autormodif, oDocumento.idusuario_modif)
                        };
                        break;
                    case BLL.Documento.enumOrigenEdicion.detalleAsuntoTA:
                        nomProc = "SUP_DOCASU_T_I";
                        dbparams = new SqlParameter[10] {
                            Param(enumDBFields.t600_idasunto, oDocumento.idElemento),
                            Param(enumDBFields.t602_descripcion, oDocumento.descripcion),
                            Param(enumDBFields.t602_weblink, oDocumento.weblink),
                            Param(enumDBFields.t602_nombrearchivo, oDocumento.nombrearchivo),
                            Param(enumDBFields.t2_iddocumento, oDocumento.t2_iddocumento),
                            Param(enumDBFields.t602_privado, oDocumento.privado),
                            Param(enumDBFields.t602_modolectura, oDocumento.modolectura),
                            Param(enumDBFields.t602_tipogestion, oDocumento.tipogestion),
                            Param(enumDBFields.t602_autor, oDocumento.idusuario_autor),
                            Param(enumDBFields.t602_autormodif, oDocumento.idusuario_modif)
                        };

                        break;

                    case BLL.Documento.enumOrigenEdicion.detalleAccionTA:
                        nomProc = "SUP_DOCACC_T_I";
                        dbparams = new SqlParameter[10] {
                            Param(enumDBFields.t601_idaccion, oDocumento.idElemento),
                            Param(enumDBFields.t603_descripcion, oDocumento.descripcion),
                            Param(enumDBFields.t603_weblink, oDocumento.weblink),
                            Param(enumDBFields.t603_nombrearchivo, oDocumento.nombrearchivo),
                            Param(enumDBFields.t2_iddocumento, oDocumento.t2_iddocumento),
                            Param(enumDBFields.t603_privado, oDocumento.privado),
                            Param(enumDBFields.t603_modolectura, oDocumento.modolectura),
                            Param(enumDBFields.t603_tipogestion, oDocumento.tipogestion),
                            Param(enumDBFields.t603_autor, oDocumento.idusuario_autor),
                            Param(enumDBFields.t603_autormodif, oDocumento.idusuario_modif)
                        };
                        break;
                }                

                return (int)cDblib.ExecuteScalar(nomProc, dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un Documento a partir del id
        /// </summary>
        internal Models.Documento Select(BLL.Documento.enumOrigenEdicion enumProp, Int32 idDocumento)
        {
            Models.Documento oDocumento = null;
            IDataReader dr = null;

            try
            {

                string nomProc = "";
                SqlParameter[] dbparams = null;

                switch (enumProp)
                {
                    case BLL.Documento.enumOrigenEdicion.detalleTarea:
                        nomProc = "SUP_DOCUT_S";
                        dbparams = new SqlParameter[1] {
                            Param(enumDBFields.t363_iddocut, idDocumento)
                        };
                        break;
                    case BLL.Documento.enumOrigenEdicion.detalleAsuntoPE:
                        nomProc = "SUP_DOCASU_S";
                        dbparams = new SqlParameter[1] {
                            Param(enumDBFields.t386_iddocasu, idDocumento)
                        };
                        break;
                    case BLL.Documento.enumOrigenEdicion.detalleAccionPE:
                        nomProc = "SUP_DOCACC_S";
                        dbparams = new SqlParameter[1] {
                            Param(enumDBFields.t387_iddocacc, idDocumento)
                        };
                        break;
                    case BLL.Documento.enumOrigenEdicion.detalleAsuntoPT:
                        nomProc = "SUP_DOCASU_PT_S";
                        dbparams = new SqlParameter[1] {
                            Param(enumDBFields.t411_iddocasu, idDocumento)
                        };
                        break;
                    case BLL.Documento.enumOrigenEdicion.detalleAccionPT:
                        nomProc = "SUP_DOCACC_PT_S";
                        dbparams = new SqlParameter[1] {
                            Param(enumDBFields.t412_iddocacc, idDocumento)
                        };
                        break;
                    case BLL.Documento.enumOrigenEdicion.detalleAsuntoTA:
                        nomProc = "SUP_DOCASU_T_S";
                        dbparams = new SqlParameter[1] {
                            Param(enumDBFields.t602_iddocasu, idDocumento)
                        };
                        break;
                    case BLL.Documento.enumOrigenEdicion.detalleAccionTA:
                        nomProc = "SUP_DOCACC_T_S";
                        dbparams = new SqlParameter[1] {
                            Param(enumDBFields.t603_iddocacc, idDocumento)
                        };
                        break;
                }                
                dr = cDblib.DataReader(nomProc, dbparams);
                if (dr.Read())
                {
                    switch (enumProp)
                    {
                        case BLL.Documento.enumOrigenEdicion.detalleTarea:
                            oDocumento = new Models.Documento();
                            oDocumento.idDocumento = Convert.ToInt32(dr["t363_iddocut"]);
                            oDocumento.idElemento = Convert.ToInt32(dr["t332_idtarea"]);
                            oDocumento.descripcion = Convert.ToString(dr["t363_descripcion"]);
                            oDocumento.weblink = Convert.ToString(dr["t363_weblink"]);
                            oDocumento.nombrearchivo = Convert.ToString(dr["t363_nombrearchivo"]);
                            if (!Convert.IsDBNull(dr["t2_iddocumento"]))
                                oDocumento.t2_iddocumento = Convert.ToInt64(dr["t2_iddocumento"]);
                            oDocumento.privado = Convert.ToBoolean(dr["t363_privado"]);
                            oDocumento.modolectura = Convert.ToBoolean(dr["t363_modolectura"]);
                            oDocumento.tipogestion = Convert.ToBoolean(dr["t363_tipogestion"]);
                            oDocumento.idusuario_autor = Convert.ToInt32(dr["t314_idusuario_autor"]);
                            oDocumento.fecha = Convert.ToDateTime(dr["t363_fecha"]);
                            oDocumento.autor = Convert.ToString(dr["autor"]);
                            if (!Convert.IsDBNull(dr["t314_idusuario_modif"]))
                                oDocumento.idusuario_modif = Convert.ToInt32(dr["t314_idusuario_modif"]);
                            oDocumento.fechamodif = Convert.ToDateTime(dr["t363_fechamodif"]);
                            oDocumento.autormodif = Convert.ToString(dr["autormodif"]);
                            break;
                        case BLL.Documento.enumOrigenEdicion.detalleAsuntoPE:
                            oDocumento = new Models.Documento();
                            oDocumento.idDocumento = Convert.ToInt32(dr["T386_iddocasu"]);
                            oDocumento.idElemento = Convert.ToInt32(dr["T382_idasunto"]);
                            oDocumento.descripcion = Convert.ToString(dr["T386_descripcion"]);
                            oDocumento.weblink = Convert.ToString(dr["T386_weblink"]);
                            oDocumento.nombrearchivo = Convert.ToString(dr["T386_nombrearchivo"]);
                            if (!Convert.IsDBNull(dr["t2_iddocumento"]))
                                oDocumento.t2_iddocumento = Convert.ToInt64(dr["t2_iddocumento"]);
                            oDocumento.privado = Convert.ToBoolean(dr["T386_privado"]);
                            oDocumento.modolectura = Convert.ToBoolean(dr["T386_modolectura"]);
                            oDocumento.tipogestion = Convert.ToBoolean(dr["T386_tipogestion"]);
                            oDocumento.idusuario_autor = Convert.ToInt32(dr["T386_autor"]);
                            oDocumento.fecha = Convert.ToDateTime(dr["T386_fecha"]);
                            oDocumento.autor = Convert.ToString(dr["autor"]);
                            oDocumento.idusuario_modif = Convert.ToInt32(dr["T386_autormodif"]);
                            oDocumento.fechamodif = Convert.ToDateTime(dr["t386_fechamodif"]);
                            oDocumento.autormodif = Convert.ToString(dr["autorMODIF"]);
                            break;
                        case BLL.Documento.enumOrigenEdicion.detalleAccionPE:
                            oDocumento = new Models.Documento();
                            oDocumento.idDocumento = Convert.ToInt32(dr["T387_iddocacc"]);
                            oDocumento.idElemento = Convert.ToInt32(dr["t383_idaccion"]);
                            oDocumento.descripcion = Convert.ToString(dr["T387_descripcion"]);
                            oDocumento.weblink = Convert.ToString(dr["T387_weblink"]);
                            oDocumento.nombrearchivo = Convert.ToString(dr["T387_nombrearchivo"]);
                            if (!Convert.IsDBNull(dr["t2_iddocumento"]))
                                oDocumento.t2_iddocumento = Convert.ToInt64(dr["t2_iddocumento"]);
                            oDocumento.privado = Convert.ToBoolean(dr["T387_privado"]);
                            oDocumento.modolectura = Convert.ToBoolean(dr["T387_modolectura"]);
                            oDocumento.tipogestion = Convert.ToBoolean(dr["T387_tipogestion"]);
                            oDocumento.idusuario_autor = Convert.ToInt32(dr["T387_autor"]);
                            oDocumento.fecha = Convert.ToDateTime(dr["T387_fecha"]);
                            oDocumento.autor = Convert.ToString(dr["autor"]);
                            oDocumento.idusuario_modif = Convert.ToInt32(dr["T387_autormodif"]);
                            oDocumento.fechamodif = Convert.ToDateTime(dr["t387_fechamodif"]);
                            oDocumento.autormodif = Convert.ToString(dr["autorMODIF"]);
                            break;
                        case BLL.Documento.enumOrigenEdicion.detalleAsuntoPT:
                            oDocumento = new Models.Documento();
                            oDocumento.idDocumento = Convert.ToInt32(dr["T411_iddocasu"]);
                            oDocumento.idElemento = Convert.ToInt32(dr["T409_idasunto"]);
                            oDocumento.descripcion = Convert.ToString(dr["T411_descripcion"]);
                            oDocumento.weblink = Convert.ToString(dr["T411_weblink"]);
                            oDocumento.nombrearchivo = Convert.ToString(dr["T411_nombrearchivo"]);
                            if (!Convert.IsDBNull(dr["t2_iddocumento"]))
                                oDocumento.t2_iddocumento = Convert.ToInt64(dr["t2_iddocumento"]);
                            oDocumento.privado = Convert.ToBoolean(dr["T411_privado"]);
                            oDocumento.modolectura = Convert.ToBoolean(dr["T411_modolectura"]);
                            oDocumento.tipogestion = Convert.ToBoolean(dr["T411_tipogestion"]);
                            oDocumento.idusuario_autor = Convert.ToInt32(dr["T411_autor"]);
                            oDocumento.fecha = Convert.ToDateTime(dr["T411_fecha"]);
                            oDocumento.autor = Convert.ToString(dr["autor"]);
                            oDocumento.idusuario_modif = Convert.ToInt32(dr["T411_autormodif"]);
                            oDocumento.fechamodif = Convert.ToDateTime(dr["T411_fechamodif"]);
                            oDocumento.autormodif = Convert.ToString(dr["autorMODIF"]);
                            break;
                        case BLL.Documento.enumOrigenEdicion.detalleAccionPT:
                            oDocumento = new Models.Documento();
                            oDocumento.idDocumento = Convert.ToInt32(dr["T412_iddocacc"]);
                            oDocumento.idElemento = Convert.ToInt32(dr["T410_idaccion"]);
                            oDocumento.descripcion = Convert.ToString(dr["T412_descripcion"]);
                            oDocumento.weblink = Convert.ToString(dr["T412_weblink"]);
                            oDocumento.nombrearchivo = Convert.ToString(dr["T412_nombrearchivo"]);
                            if (!Convert.IsDBNull(dr["t2_iddocumento"]))
                                oDocumento.t2_iddocumento = Convert.ToInt64(dr["t2_iddocumento"]);
                            oDocumento.privado = Convert.ToBoolean(dr["T412_privado"]);
                            oDocumento.modolectura = Convert.ToBoolean(dr["T412_modolectura"]);
                            oDocumento.tipogestion = Convert.ToBoolean(dr["T412_tipogestion"]);
                            oDocumento.idusuario_autor = Convert.ToInt32(dr["T412_autor"]);
                            oDocumento.fecha = Convert.ToDateTime(dr["T412_fecha"]);
                            oDocumento.autor = Convert.ToString(dr["autor"]);
                            oDocumento.idusuario_modif = Convert.ToInt32(dr["T412_autormodif"]);
                            oDocumento.fechamodif = Convert.ToDateTime(dr["T412_fechamodif"]);
                            oDocumento.autormodif = Convert.ToString(dr["autorMODIF"]);
                            break;
                        case BLL.Documento.enumOrigenEdicion.detalleAsuntoTA:
                            oDocumento = new Models.Documento();
                            oDocumento.idDocumento = Convert.ToInt32(dr["T602_iddocasu"]);
                            oDocumento.idElemento = Convert.ToInt32(dr["T600_idasunto"]);
                            oDocumento.descripcion = Convert.ToString(dr["T602_descripcion"]);
                            oDocumento.weblink = Convert.ToString(dr["T602_weblink"]);
                            oDocumento.nombrearchivo = Convert.ToString(dr["T602_nombrearchivo"]);
                            if (!Convert.IsDBNull(dr["t2_iddocumento"]))
                                oDocumento.t2_iddocumento = Convert.ToInt64(dr["t2_iddocumento"]);
                            oDocumento.privado = Convert.ToBoolean(dr["T602_privado"]);
                            oDocumento.modolectura = Convert.ToBoolean(dr["T602_modolectura"]);
                            oDocumento.tipogestion = Convert.ToBoolean(dr["T602_tipogestion"]);
                            oDocumento.idusuario_autor = Convert.ToInt32(dr["T602_autor"]);
                            oDocumento.fecha = Convert.ToDateTime(dr["T602_fecha"]);
                            oDocumento.autor = Convert.ToString(dr["autor"]);
                            oDocumento.idusuario_modif = Convert.ToInt32(dr["T602_autormodif"]);
                            oDocumento.fechamodif = Convert.ToDateTime(dr["T602_fechamodif"]);
                            oDocumento.autormodif = Convert.ToString(dr["autorMODIF"]);
                            break;
                        case BLL.Documento.enumOrigenEdicion.detalleAccionTA:
                            oDocumento = new Models.Documento();
                            oDocumento.idDocumento = Convert.ToInt32(dr["T603_iddocacc"]);
                            oDocumento.idElemento = Convert.ToInt32(dr["T601_idaccion"]);
                            oDocumento.descripcion = Convert.ToString(dr["T603_descripcion"]);
                            oDocumento.weblink = Convert.ToString(dr["T603_weblink"]);
                            oDocumento.nombrearchivo = Convert.ToString(dr["T603_nombrearchivo"]);
                            if (!Convert.IsDBNull(dr["t2_iddocumento"]))
                                oDocumento.t2_iddocumento = Convert.ToInt64(dr["t2_iddocumento"]);
                            oDocumento.privado = Convert.ToBoolean(dr["T603_privado"]);
                            oDocumento.modolectura = Convert.ToBoolean(dr["T603_modolectura"]);
                            oDocumento.tipogestion = Convert.ToBoolean(dr["T603_tipogestion"]);
                            oDocumento.idusuario_autor = Convert.ToInt32(dr["T603_autor"]);
                            oDocumento.fecha = Convert.ToDateTime(dr["T603_fecha"]);
                            oDocumento.autor = Convert.ToString(dr["autor"]);
                            oDocumento.idusuario_modif = Convert.ToInt32(dr["T603_autormodif"]);
                            oDocumento.fechamodif = Convert.ToDateTime(dr["T603_fechamodif"]);
                            oDocumento.autormodif = Convert.ToString(dr["autorMODIF"]);
                            break;
                    }
                }
                return oDocumento;

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
        /// Actualiza un Documento a partir del id
        /// </summary>
        internal int Update(BLL.Documento.enumOrigenEdicion enumProp, Models.Documento oDocumento)
        {
            try

            {
                string nomProc = "";
                SqlParameter[] dbparams = null;

                switch (enumProp)
                {
                case BLL.Documento.enumOrigenEdicion.detalleTarea:
                    nomProc = "SUP_DOCUT_U";
                    dbparams = new SqlParameter[10] {
                        Param(enumDBFields.t363_iddocut, oDocumento.idDocumento),
                        Param(enumDBFields.t332_idtarea, oDocumento.idElemento),
                        Param(enumDBFields.t363_descripcion, oDocumento.descripcion),
                        Param(enumDBFields.t363_weblink, oDocumento.weblink),
                        Param(enumDBFields.t363_nombrearchivo, oDocumento.nombrearchivo),
                        Param(enumDBFields.t2_iddocumento, oDocumento.t2_iddocumento),
                        Param(enumDBFields.t363_privado, oDocumento.privado),
                        Param(enumDBFields.t363_modolectura, oDocumento.modolectura),
                        Param(enumDBFields.t363_tipogestion, oDocumento.tipogestion),
                        Param(enumDBFields.t314_idusuario_modif, oDocumento.idusuario_autor),
                      };
                    break;
                case BLL.Documento.enumOrigenEdicion.detalleAsuntoPE:
                    nomProc = "SUP_DOCASU_U";
                    dbparams = new SqlParameter[10] {
                            Param(enumDBFields.t382_idasunto, oDocumento.idElemento),
                            Param(enumDBFields.t2_iddocumento, oDocumento.t2_iddocumento),
                            Param(enumDBFields.t386_autormodif, oDocumento.idusuario_modif),
                            Param(enumDBFields.t386_descripcion, oDocumento.descripcion),
                            Param(enumDBFields.t386_iddocasu, oDocumento.idDocumento),
                            Param(enumDBFields.t386_modolectura, oDocumento.modolectura),
                            Param(enumDBFields.t386_nombrearchivo, oDocumento.nombrearchivo),
                            Param(enumDBFields.t386_privado, oDocumento.privado),
                            Param(enumDBFields.t386_tipogestion, oDocumento.tipogestion),                            
                            Param(enumDBFields.t386_weblink, oDocumento.weblink)
                        };
                    break;
                case BLL.Documento.enumOrigenEdicion.detalleAccionPE:
                    nomProc = "SUP_DOCACC_U";
                    dbparams = new SqlParameter[10] {
                            Param(enumDBFields.t383_idaccion, oDocumento.idElemento),
                            Param(enumDBFields.t2_iddocumento, oDocumento.t2_iddocumento),
                            Param(enumDBFields.t387_autormodif, oDocumento.idusuario_modif),
                            Param(enumDBFields.t387_descripcion, oDocumento.descripcion),
                            Param(enumDBFields.t387_iddocacc, oDocumento.idDocumento),
                            Param(enumDBFields.t387_modolectura, oDocumento.modolectura),
                            Param(enumDBFields.t387_nombrearchivo, oDocumento.nombrearchivo),
                            Param(enumDBFields.t387_privado, oDocumento.privado),
                            Param(enumDBFields.t387_tipogestion, oDocumento.tipogestion),                            
                            Param(enumDBFields.t387_weblink, oDocumento.weblink)
                        };
                    break;
                case BLL.Documento.enumOrigenEdicion.detalleAsuntoPT:
                    nomProc = "SUP_DOCASU_PT_U";
                    dbparams = new SqlParameter[10] {
                            Param(enumDBFields.t409_idasunto, oDocumento.idElemento),
                            Param(enumDBFields.t2_iddocumento, oDocumento.t2_iddocumento),
                            Param(enumDBFields.t411_autormodif, oDocumento.idusuario_modif),
                            Param(enumDBFields.t411_descripcion, oDocumento.descripcion),
                            Param(enumDBFields.t411_iddocasu, oDocumento.idDocumento),
                            Param(enumDBFields.t411_modolectura, oDocumento.modolectura),
                            Param(enumDBFields.t411_nombrearchivo, oDocumento.nombrearchivo),
                            Param(enumDBFields.t411_privado, oDocumento.privado),
                            Param(enumDBFields.t411_tipogestion, oDocumento.tipogestion),                            
                            Param(enumDBFields.t411_weblink, oDocumento.weblink)
                        };
                    break;
                case BLL.Documento.enumOrigenEdicion.detalleAccionPT:
                    nomProc = "SUP_DOCACC_PT_U";
                    dbparams = new SqlParameter[10] {
                            Param(enumDBFields.t410_idaccion, oDocumento.idElemento),
                            Param(enumDBFields.t2_iddocumento, oDocumento.t2_iddocumento),
                            Param(enumDBFields.t412_autormodif, oDocumento.idusuario_modif),
                            Param(enumDBFields.t412_descripcion, oDocumento.descripcion),
                            Param(enumDBFields.t412_iddocacc, oDocumento.idDocumento),
                            Param(enumDBFields.t412_modolectura, oDocumento.modolectura),
                            Param(enumDBFields.t412_nombrearchivo, oDocumento.nombrearchivo),
                            Param(enumDBFields.t412_privado, oDocumento.privado),
                            Param(enumDBFields.t412_tipogestion, oDocumento.tipogestion),                            
                            Param(enumDBFields.t412_weblink, oDocumento.weblink)
                        };
                    break;
                case BLL.Documento.enumOrigenEdicion.detalleAsuntoTA:
                    nomProc = "SUP_DOCASU_T_U";
                    dbparams = new SqlParameter[10] {
                            Param(enumDBFields.t600_idasunto, oDocumento.idElemento),
                            Param(enumDBFields.t2_iddocumento, oDocumento.t2_iddocumento),
                            Param(enumDBFields.t602_autormodif, oDocumento.idusuario_modif),
                            Param(enumDBFields.t602_descripcion, oDocumento.descripcion),
                            Param(enumDBFields.t602_iddocasu, oDocumento.idDocumento),
                            Param(enumDBFields.t602_modolectura, oDocumento.modolectura),
                            Param(enumDBFields.t602_nombrearchivo, oDocumento.nombrearchivo),
                            Param(enumDBFields.t602_privado, oDocumento.privado),
                            Param(enumDBFields.t602_tipogestion, oDocumento.tipogestion),                            
                            Param(enumDBFields.t602_weblink, oDocumento.weblink)
                        };
                    break;
                case BLL.Documento.enumOrigenEdicion.detalleAccionTA:
                    nomProc = "SUP_DOCACC_T_U";
                    dbparams = new SqlParameter[10] {
                            Param(enumDBFields.t601_idaccion, oDocumento.idElemento),
                            Param(enumDBFields.t2_iddocumento, oDocumento.t2_iddocumento),
                            Param(enumDBFields.t603_autormodif, oDocumento.idusuario_modif),
                            Param(enumDBFields.t603_descripcion, oDocumento.descripcion),
                            Param(enumDBFields.t603_iddocacc, oDocumento.idDocumento),
                            Param(enumDBFields.t603_modolectura, oDocumento.modolectura),
                            Param(enumDBFields.t603_nombrearchivo, oDocumento.nombrearchivo),
                            Param(enumDBFields.t603_privado, oDocumento.privado),
                            Param(enumDBFields.t603_tipogestion, oDocumento.tipogestion),                            
                            Param(enumDBFields.t603_weblink, oDocumento.weblink)
                        };
                    break;
                }


                return (int)cDblib.Execute(nomProc, dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un Documento a partir del id
        /// </summary>
        internal int Delete(BLL.Documento.enumOrigenEdicion enumProp, Int32 idDocumento)
        {
           
            try
            {

                string nomProc = "";
                SqlParameter[] dbparams = null;

                switch (enumProp)
                {
                    case BLL.Documento.enumOrigenEdicion.detalleTarea:
                        nomProc = "SUP_DOCUT_D";
                        dbparams = new SqlParameter[1] {
                            Param(enumDBFields.t363_iddocut, idDocumento)
                        };
                        break;
                    case BLL.Documento.enumOrigenEdicion.detalleAsuntoPE:
                        nomProc = "SUP_DOCASU_D";
                        dbparams = new SqlParameter[1] {
                            Param(enumDBFields.t386_iddocasu, idDocumento)
                        };
                        break;
                    case BLL.Documento.enumOrigenEdicion.detalleAccionPE:
                        nomProc = "SUP_DOCACC_D";
                        dbparams = new SqlParameter[1] {
                            Param(enumDBFields.t387_iddocacc, idDocumento)
                        };
                        break;
                    case BLL.Documento.enumOrigenEdicion.detalleAsuntoPT:
                        nomProc = "SUP_DOCASU_PT_D";
                        dbparams = new SqlParameter[1] {
                            Param(enumDBFields.t411_iddocasu, idDocumento)
                        };
                        break;
                    case BLL.Documento.enumOrigenEdicion.detalleAccionPT:
                        nomProc = "SUP_DOCACC_PT_D";
                        dbparams = new SqlParameter[1] {
                            Param(enumDBFields.t412_iddocacc, idDocumento)
                        };
                        break;
                    case BLL.Documento.enumOrigenEdicion.detalleAsuntoTA:
                        nomProc = "SUP_DOCASU_T_D";
                        dbparams = new SqlParameter[1] {
                            Param(enumDBFields.t602_iddocasu, idDocumento)
                        };
                        break;
                    case BLL.Documento.enumOrigenEdicion.detalleAccionTA:
                        nomProc = "SUP_DOCACC_T_D";
                        dbparams = new SqlParameter[1] {
                            Param(enumDBFields.t603_iddocacc, idDocumento)
                        };
                        break;
                }                      

                return (int)cDblib.Execute(nomProc, dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los DocumentacionPreventa
        /// </summary>
        internal List<Models.Documento> Catalogo(BLL.Documento.enumOrigenEdicion enumProp, int idUsuarioAutorizado, int? idElemento)
        {
            Models.Documento oDocumento = null;
            List<Models.Documento> lst = new List<Models.Documento>();
            IDataReader dr = null;

            try
            {
                string nomProc = "";
                SqlParameter[] dbparams = null;
                
                switch (enumProp)
                {
                    case BLL.Documento.enumOrigenEdicion.detalleTarea:
                        nomProc = "SUP_DOCUT_C3";
                        dbparams = new SqlParameter[2] {
                            Param(enumDBFields.t332_idtarea, idElemento),
                            Param(enumDBFields.t314_idusuario_autor, idUsuarioAutorizado)
                        };
                        break;
                    case BLL.Documento.enumOrigenEdicion.detalleAsuntoPE:
                        nomProc = "SUP_DOCASU_C2";
                        dbparams = new SqlParameter[2] {
                            Param(enumDBFields.t382_idasunto, idElemento),
                            Param(enumDBFields.t386_autor, idUsuarioAutorizado)
                        };
                        break;

                    case BLL.Documento.enumOrigenEdicion.detalleAccionPE:
                        nomProc = "SUP_DOCACC_C2";
                        dbparams = new SqlParameter[2] {
                            Param(enumDBFields.t383_idaccion, idElemento),
                            Param(enumDBFields.t387_autor, idUsuarioAutorizado)
                        };
                        break;
                    case BLL.Documento.enumOrigenEdicion.detalleAsuntoPT:
                        nomProc = "SUP_DOCASU_PT_C2";
                        dbparams = new SqlParameter[2] {
                            Param(enumDBFields.t409_idasunto, idElemento),
                            Param(enumDBFields.t411_autor, idUsuarioAutorizado)
                        };
                        break;

                    case BLL.Documento.enumOrigenEdicion.detalleAccionPT:
                        nomProc = "SUP_DOCACC_PT_C2";
                        dbparams = new SqlParameter[2] {
                            Param(enumDBFields.t410_idaccion, idElemento),
                            Param(enumDBFields.t412_autor, idUsuarioAutorizado)
                        };
                        break;
                    case BLL.Documento.enumOrigenEdicion.detalleAsuntoTA:
                        nomProc = "SUP_DOCASU_T_C2";
                        dbparams = new SqlParameter[2] {
                            Param(enumDBFields.t600_idasunto, idElemento),
                            Param(enumDBFields.t602_autor, idUsuarioAutorizado)
                        };
                        break;

                    case BLL.Documento.enumOrigenEdicion.detalleAccionTA:
                        nomProc = "SUP_DOCACC_T_C2";
                        dbparams = new SqlParameter[2] {
                            Param(enumDBFields.t601_idaccion, idElemento),
                            Param(enumDBFields.t603_autor, idUsuarioAutorizado)
                        };
                        break;
                }

                dr = cDblib.DataReader(nomProc, dbparams);
                while (dr.Read())
                {
                   oDocumento = new Models.Documento();
                   switch (enumProp)
                   {
                       case BLL.Documento.enumOrigenEdicion.detalleTarea:
                           oDocumento.idDocumento = Convert.ToInt32(dr["t363_iddocut"]);
                           oDocumento.idElemento = Convert.ToInt32(dr["t332_idtarea"]);
                           oDocumento.descripcion = Convert.ToString(dr["t363_descripcion"]);
                           oDocumento.weblink = Convert.ToString(dr["t363_weblink"]);
                           oDocumento.nombrearchivo = Convert.ToString(dr["t363_nombrearchivo"]);
                           if (!Convert.IsDBNull(dr["t2_iddocumento"]))
                               oDocumento.t2_iddocumento = Convert.ToInt64(dr["t2_iddocumento"]);
                           //oDocumento.t363_privado = Convert.ToBoolean(dr["t363_privado"]);
                           oDocumento.modolectura = Convert.ToBoolean(dr["t363_modolectura"]);
                           oDocumento.tipogestion = Convert.ToBoolean(dr["t363_tipogestion"]);
                           oDocumento.idusuario_autor = Convert.ToInt32(dr["t314_idusuario_autor"]);
                           //oDocumento.t363_fecha = Convert.ToDateTime(dr["t363_fecha"]);
                           oDocumento.autor = Convert.ToString(dr["autor"]);
                           /*oDocumento.t314_idusuario_modif = Convert.ToInt32(dr["t314_idusuario_modif"]);
                           oDocumento.t363_fechamodif = Convert.ToDateTime(dr["t363_fechamodif"]);
                           oDocumento.autormodif = Convert.ToString(dr["autormodif"]);*/
                           break;
                       case BLL.Documento.enumOrigenEdicion.detalleAsuntoPE:
                           oDocumento = new Models.Documento();
                           oDocumento.idDocumento = Convert.ToInt32(dr["T386_iddocasu"]);
                           oDocumento.idElemento = Convert.ToInt32(dr["T382_idasunto"]);
                           oDocumento.descripcion = Convert.ToString(dr["T386_descripcion"]);
                           oDocumento.weblink = Convert.ToString(dr["T386_weblink"]);
                           oDocumento.nombrearchivo = Convert.ToString(dr["T386_nombrearchivo"]);
                           if (!Convert.IsDBNull(dr["t2_iddocumento"]))
                               oDocumento.t2_iddocumento = Convert.ToInt64(dr["t2_iddocumento"]);
                           oDocumento.privado = Convert.ToBoolean(dr["T386_privado"]);
                           oDocumento.modolectura = Convert.ToBoolean(dr["T386_modolectura"]);
                           oDocumento.tipogestion = Convert.ToBoolean(dr["T386_tipogestion"]);
                           oDocumento.idusuario_autor = Convert.ToInt32(dr["T314_idusuario_autor"]);
                           oDocumento.fecha = Convert.ToDateTime(dr["T386_fecha"]);
                           oDocumento.autor = Convert.ToString(dr["autor"]);
                           oDocumento.idusuario_modif = Convert.ToInt32(dr["T386_autormodif"]);
                           oDocumento.fechamodif = Convert.ToDateTime(dr["T386_fechamodif"]);
                           oDocumento.autormodif = Convert.ToString(dr["T386_autormodif"]);
                           break;
                       case BLL.Documento.enumOrigenEdicion.detalleAccionPE:
                           oDocumento = new Models.Documento();
                           oDocumento.idDocumento = Convert.ToInt32(dr["T387_iddocacc"]);
                           oDocumento.idElemento = Convert.ToInt32(dr["T383_idaccion"]);
                           oDocumento.descripcion = Convert.ToString(dr["T387_descripcion"]);
                           oDocumento.weblink = Convert.ToString(dr["T387_weblink"]);
                           oDocumento.nombrearchivo = Convert.ToString(dr["T387_nombrearchivo"]);
                           if (!Convert.IsDBNull(dr["t2_iddocumento"]))
                               oDocumento.t2_iddocumento = Convert.ToInt64(dr["t2_iddocumento"]);
                           oDocumento.privado = Convert.ToBoolean(dr["T387_privado"]);
                           oDocumento.modolectura = Convert.ToBoolean(dr["T387_modolectura"]);
                           oDocumento.tipogestion = Convert.ToBoolean(dr["T387_tipogestion"]);
                           oDocumento.idusuario_autor = Convert.ToInt32(dr["T314_idusuario_autor"]);
                           oDocumento.fecha = Convert.ToDateTime(dr["T387_fecha"]);
                           oDocumento.autor = Convert.ToString(dr["autor"]);
                           oDocumento.idusuario_modif = Convert.ToInt32(dr["T387_autormodif"]);
                           oDocumento.fechamodif = Convert.ToDateTime(dr["T387_fechamodif"]);
                           oDocumento.autormodif = Convert.ToString(dr["T387_autormodif"]);
                           break;
                       case BLL.Documento.enumOrigenEdicion.detalleAsuntoPT:
                           oDocumento = new Models.Documento();
                           oDocumento.idDocumento = Convert.ToInt32(dr["T411_iddocasu"]);
                           oDocumento.idElemento = Convert.ToInt32(dr["T409_idasunto"]);
                           oDocumento.descripcion = Convert.ToString(dr["T411_descripcion"]);
                           oDocumento.weblink = Convert.ToString(dr["T411_weblink"]);
                           oDocumento.nombrearchivo = Convert.ToString(dr["T411_nombrearchivo"]);
                           if (!Convert.IsDBNull(dr["t2_iddocumento"]))
                               oDocumento.t2_iddocumento = Convert.ToInt64(dr["t2_iddocumento"]);
                           oDocumento.privado = Convert.ToBoolean(dr["T411_privado"]);
                           oDocumento.modolectura = Convert.ToBoolean(dr["T411_modolectura"]);
                           oDocumento.tipogestion = Convert.ToBoolean(dr["T411_tipogestion"]);
                           oDocumento.idusuario_autor = Convert.ToInt32(dr["T314_idusuario_autor"]);
                           oDocumento.fecha = Convert.ToDateTime(dr["T411_fecha"]);
                           oDocumento.autor = Convert.ToString(dr["autor"]);
                           oDocumento.idusuario_modif = Convert.ToInt32(dr["T411_autormodif"]);
                           oDocumento.fechamodif = Convert.ToDateTime(dr["T411_fechamodif"]);
                           oDocumento.autormodif = Convert.ToString(dr["T411_autormodif"]);
                           break;
                       case BLL.Documento.enumOrigenEdicion.detalleAccionPT:
                           oDocumento = new Models.Documento();
                           oDocumento.idDocumento = Convert.ToInt32(dr["T412_iddocacc"]);
                           oDocumento.idElemento = Convert.ToInt32(dr["T410_idaccion"]);
                           oDocumento.descripcion = Convert.ToString(dr["T412_descripcion"]);
                           oDocumento.weblink = Convert.ToString(dr["T412_weblink"]);
                           oDocumento.nombrearchivo = Convert.ToString(dr["T412_nombrearchivo"]);
                           if (!Convert.IsDBNull(dr["t2_iddocumento"]))
                               oDocumento.t2_iddocumento = Convert.ToInt64(dr["t2_iddocumento"]);
                           oDocumento.privado = Convert.ToBoolean(dr["T412_privado"]);
                           oDocumento.modolectura = Convert.ToBoolean(dr["T412_modolectura"]);
                           oDocumento.tipogestion = Convert.ToBoolean(dr["T412_tipogestion"]);
                           oDocumento.idusuario_autor = Convert.ToInt32(dr["T314_idusuario_autor"]);
                           oDocumento.fecha = Convert.ToDateTime(dr["T412_fecha"]);
                           oDocumento.autor = Convert.ToString(dr["autor"]);
                           oDocumento.idusuario_modif = Convert.ToInt32(dr["T412_autormodif"]);
                           oDocumento.fechamodif = Convert.ToDateTime(dr["T412_fechamodif"]);
                           oDocumento.autormodif = Convert.ToString(dr["T412_autormodif"]);
                           break;
                       case BLL.Documento.enumOrigenEdicion.detalleAsuntoTA:
                           oDocumento = new Models.Documento();
                           oDocumento.idDocumento = Convert.ToInt32(dr["T602_iddocasu"]);
                           oDocumento.idElemento = Convert.ToInt32(dr["T600_idasunto"]);
                           oDocumento.descripcion = Convert.ToString(dr["T602_descripcion"]);
                           oDocumento.weblink = Convert.ToString(dr["T602_weblink"]);
                           oDocumento.nombrearchivo = Convert.ToString(dr["T602_nombrearchivo"]);
                           if (!Convert.IsDBNull(dr["t2_iddocumento"]))
                               oDocumento.t2_iddocumento = Convert.ToInt64(dr["t2_iddocumento"]);
                           oDocumento.privado = Convert.ToBoolean(dr["T602_privado"]);
                           oDocumento.modolectura = Convert.ToBoolean(dr["T602_modolectura"]);
                           oDocumento.tipogestion = Convert.ToBoolean(dr["T602_tipogestion"]);
                           oDocumento.idusuario_autor = Convert.ToInt32(dr["T314_idusuario_autor"]);
                           oDocumento.fecha = Convert.ToDateTime(dr["T602_fecha"]);
                           oDocumento.autor = Convert.ToString(dr["autor"]);
                           oDocumento.idusuario_modif = Convert.ToInt32(dr["T602_autormodif"]);
                           oDocumento.fechamodif = Convert.ToDateTime(dr["T602_fechamodif"]);
                           oDocumento.autormodif = Convert.ToString(dr["T602_autormodif"]);
                           break;
                       case BLL.Documento.enumOrigenEdicion.detalleAccionTA:
                           oDocumento = new Models.Documento();
                           oDocumento.idDocumento = Convert.ToInt32(dr["T603_iddocacc"]);
                           oDocumento.idElemento = Convert.ToInt32(dr["T601_idaccion"]);
                           oDocumento.descripcion = Convert.ToString(dr["T603_descripcion"]);
                           oDocumento.weblink = Convert.ToString(dr["T603_weblink"]);
                           oDocumento.nombrearchivo = Convert.ToString(dr["T603_nombrearchivo"]);
                           if (!Convert.IsDBNull(dr["t2_iddocumento"]))
                               oDocumento.t2_iddocumento = Convert.ToInt64(dr["t2_iddocumento"]);
                           oDocumento.privado = Convert.ToBoolean(dr["T603_privado"]);
                           oDocumento.modolectura = Convert.ToBoolean(dr["T603_modolectura"]);
                           oDocumento.tipogestion = Convert.ToBoolean(dr["T603_tipogestion"]);
                           oDocumento.idusuario_autor = Convert.ToInt32(dr["T314_idusuario_autor"]);
                           oDocumento.fecha = Convert.ToDateTime(dr["T603_fecha"]);
                           oDocumento.autor = Convert.ToString(dr["autor"]);
                           oDocumento.idusuario_modif = Convert.ToInt32(dr["T603_autormodif"]);
                           oDocumento.fechamodif = Convert.ToDateTime(dr["T603_fechamodif"]);
                           oDocumento.autormodif = Convert.ToString(dr["T603_autormodif"]);
                           break;
                   }
                   lst.Add(oDocumento);
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
				case enumDBFields.t363_iddocut:
					paramName = "@t363_iddocut";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_idtarea:
					paramName = "@t332_idtarea";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t363_descripcion:
					paramName = "@t363_descripcion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t363_weblink:
					paramName = "@t363_weblink";
					paramType = SqlDbType.VarChar;
					paramSize = 250;
					break;
				case enumDBFields.t363_nombrearchivo:
					paramName = "@t363_nombrearchivo";
					paramType = SqlDbType.VarChar;
					paramSize = 250;
					break;
				case enumDBFields.t2_iddocumento:
					paramName = "@t2_iddocumento";
					paramType = SqlDbType.BigInt;
					paramSize = 8;
					break;
				case enumDBFields.t363_privado:
					paramName = "@t363_privado";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t363_modolectura:
					paramName = "@t363_modolectura";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t363_tipogestion:
					paramName = "@t363_tipogestion";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_idusuario_autor:
					paramName = "@t314_idusuario_autor";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t363_fecha:
					paramName = "@t363_fecha";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.autor:
					paramName = "@autor";
					paramType = SqlDbType.VarChar;
					paramSize = 73;
					break;
				case enumDBFields.t314_idusuario_modif:
					paramName = "@t314_idusuario_modif";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t363_fechamodif:
					paramName = "@t363_fechamodif";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.autormodif:
					paramName = "@autormodif";
					paramType = SqlDbType.VarChar;
					paramSize = 73;
					break;
                case enumDBFields.t382_idasunto:
                    paramName = "@T382_idasunto";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.t386_iddocasu:
                    paramName = "@T386_iddocasu";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;  
 				case enumDBFields.t386_descripcion:
					paramName = "@t386_descripcion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
 				case enumDBFields.t386_weblink:
					paramName = "@t386_weblink";
					paramType = SqlDbType.VarChar;
					paramSize = 250;
					break;
 				case enumDBFields.t386_nombrearchivo:
					paramName = "@t386_nombrearchivo";
					paramType = SqlDbType.VarChar;
					paramSize = 250;
					break;
				case enumDBFields.t386_privado:
					paramName = "@t386_privado";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t386_modolectura:
					paramName = "@t386_modolectura";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t386_tipogestion:
					paramName = "@t386_tipogestion";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t386_fecha:
					paramName = "@t386_fecha";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t386_autormodif:
					paramName = "@T386_autormodif";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.t386_autor:
                    paramName = "@T386_autor";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;

                case enumDBFields.t383_idaccion:
                    paramName = "@T383_idaccion";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.t387_autor:
                    paramName = "@T387_autor";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.t387_autormodif:
                    paramName = "@T387_autormodif";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;     
                case enumDBFields.t387_iddocacc:
                    paramName = "@T387_iddocacc";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;  

 				case enumDBFields.t387_descripcion:
					paramName = "@T387_descripcion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
 				case enumDBFields.t387_weblink:
					paramName = "@t387_weblink";
					paramType = SqlDbType.VarChar;
					paramSize = 250;
					break;
 				case enumDBFields.t387_nombrearchivo:
					paramName = "@t387_nombrearchivo";
					paramType = SqlDbType.VarChar;
					paramSize = 250;
					break;
				case enumDBFields.t387_privado:
					paramName = "@t387_privado";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t387_modolectura:
					paramName = "@t387_modolectura";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t387_tipogestion:
					paramName = "@t387_tipogestion";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t387_fecha:
					paramName = "@t387_fecha";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
                case enumDBFields.t409_idasunto:
                    paramName = "@T409_idasunto";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.t411_iddocasu:
                    paramName = "@T411_iddocasu";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;  
 				case enumDBFields.t411_descripcion:
					paramName = "@T411_descripcion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
 				case enumDBFields.t411_weblink:
					paramName = "@T411_weblink";
					paramType = SqlDbType.VarChar;
					paramSize = 250;
					break;
 				case enumDBFields.t411_nombrearchivo:
					paramName = "@T411_nombrearchivo";
					paramType = SqlDbType.VarChar;
					paramSize = 250;
					break;
				case enumDBFields.t411_privado:
					paramName = "@T411_privado";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t411_modolectura:
					paramName = "@T411_modolectura";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t411_tipogestion:
					paramName = "@T411_tipogestion";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t411_fecha:
					paramName = "@T411_fecha";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t411_autormodif:
					paramName = "@T411_autormodif";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.t411_autor:
                    paramName = "@T411_autor";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;

                case enumDBFields.t410_idaccion:
                    paramName = "@T410_idaccion";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.t412_autor:
                    paramName = "@T412_autor";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.t412_autormodif:
                    paramName = "@T412_autormodif";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;     
                case enumDBFields.t412_iddocacc:
                    paramName = "@T412_iddocacc";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;  

 				case enumDBFields.t412_descripcion:
					paramName = "@T412_descripcion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
 				case enumDBFields.t412_weblink:
					paramName = "@T412_weblink";
					paramType = SqlDbType.VarChar;
					paramSize = 250;
					break;
 				case enumDBFields.t412_nombrearchivo:
					paramName = "@T412_nombrearchivo";
					paramType = SqlDbType.VarChar;
					paramSize = 250;
					break;
				case enumDBFields.t412_privado:
					paramName = "@T412_privado";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t412_modolectura:
					paramName = "@T412_modolectura";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t412_tipogestion:
					paramName = "@T412_tipogestion";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t412_fecha:
					paramName = "@T412_fecha";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;

                case enumDBFields.t600_idasunto:
                    paramName = "@T600_idasunto";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.t602_iddocasu:
                    paramName = "@T602_iddocasu";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;  
 				case enumDBFields.t602_descripcion:
					paramName = "@T602_descripcion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
 				case enumDBFields.t602_weblink:
					paramName = "@T602_weblink";
					paramType = SqlDbType.VarChar;
					paramSize = 250;
					break;
 				case enumDBFields.t602_nombrearchivo:
					paramName = "@T602_nombrearchivo";
					paramType = SqlDbType.VarChar;
					paramSize = 250;
					break;
				case enumDBFields.t602_privado:
					paramName = "@T602_privado";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t602_modolectura:
					paramName = "@T602_modolectura";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t602_tipogestion:
					paramName = "@T602_tipogestion";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t602_fecha:
					paramName = "@T602_fecha";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t602_autormodif:
					paramName = "@T602_autormodif";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.t602_autor:
                    paramName = "@T602_autor";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;

                case enumDBFields.t601_idaccion:
                    paramName = "@T601_idaccion";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.t603_autor:
                    paramName = "@T603_autor";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.t603_autormodif:
                    paramName = "@T603_autormodif";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;     
                case enumDBFields.t603_iddocacc:
                    paramName = "@T603_iddocacc";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;  

 				case enumDBFields.t603_descripcion:
					paramName = "@T603_descripcion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
 				case enumDBFields.t603_weblink:
					paramName = "@T603_weblink";
					paramType = SqlDbType.VarChar;
					paramSize = 250;
					break;
 				case enumDBFields.t603_nombrearchivo:
					paramName = "@T603_nombrearchivo";
					paramType = SqlDbType.VarChar;
					paramSize = 250;
					break;
				case enumDBFields.t603_privado:
					paramName = "@T603_privado";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t603_modolectura:
					paramName = "@T603_modolectura";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t603_tipogestion:
					paramName = "@T603_tipogestion";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t603_fecha:
					paramName = "@T603_fecha";
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
