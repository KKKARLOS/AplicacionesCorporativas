using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GASVI.DAL;

namespace GASVI.BLL
{
    public class CABECERAAPARCADA_NMPGV
    {
        #region Propiedades y Atributos

        private int _t663_idreferencia;
        public int t663_idreferencia
        {
            get { return _t663_idreferencia; }
            set { _t663_idreferencia = value; }
        }

        private string _t663_concepto;
        public string t663_concepto
        {
            get { return _t663_concepto; }
            set { _t663_concepto = value; }
        }

        private int _t001_idficepi_solicitante;
        public int t001_idficepi_solicitante
        {
            get { return _t001_idficepi_solicitante; }
            set { _t001_idficepi_solicitante = value; }
        }

        private int _t314_idusuario_interesado;
        public int t314_idusuario_interesado
        {
            get { return _t314_idusuario_interesado; }
            set { _t314_idusuario_interesado = value; }
        }

        private string _t001_codred;
        public string t001_codred
        {
            get { return _t001_codred; }
            set { _t001_codred = value; }
        }

        private int _t001_idficepi_interesado;
        public int t001_idficepi_interesado
        {
            get { return _t001_idficepi_interesado; }
            set { _t001_idficepi_interesado = value; }
        }

        private bool? _t663_justificantes;
        public bool? t663_justificantes
        {
            get { return _t663_justificantes; }
            set { _t663_justificantes = value; }
        }

        private int? _t305_idproyectosubnodo;
        public int? t305_idproyectosubnodo
        {
            get { return _t305_idproyectosubnodo; }
            set { _t305_idproyectosubnodo = value; }
        }

        private string _t422_idmoneda;
        public string t422_idmoneda
        {
            get { return _t422_idmoneda; }
            set { _t422_idmoneda = value; }
        }

        private string _t663_comentarionota;
        public string t663_comentarionota
        {
            get { return _t663_comentarionota; }
            set { _t663_comentarionota = value; }
        }

        private string _t663_anotaciones;
        public string t663_anotaciones
        {
            get { return _t663_anotaciones; }
            set { _t663_anotaciones = value; }
        }

        
        #endregion

        #region Propiedades y Atributos complementarios

        private string _Solicitante;
        public string Solicitante
        {
            get { return _Solicitante; }
            set { _Solicitante = value; }
        }

        private string _Interesado;
        public string Interesado
        {
            get { return _Interesado; }
            set { _Interesado = value; }
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

        private string _t422_denominacion;
        public string t422_denominacion
        {
            get { return _t422_denominacion; }
            set { _t422_denominacion = value; }
        }

        private int? _t313_idempresa;
        public int? t313_idempresa
        {
            get { return _t313_idempresa; }
            set { _t313_idempresa = value; }
        }

        private string _t313_denominacion;
        public string t313_denominacion
        {
            get { return _t313_denominacion; }
            set { _t313_denominacion = value; }
        }

        private OFICINA _oOficinaLiquidadora;
        public OFICINA oOficinaLiquidadora
        {
            get { return _oOficinaLiquidadora; }
            set { _oOficinaLiquidadora = value; }
        }

        private TERRITORIO _oTerritorio;
        public TERRITORIO oTerritorio
        {
            get { return _oTerritorio; }
            set { _oTerritorio = value; }
        }

        private DIETAKM _oDietaKm;
        public DIETAKM oDietaKm
        {
            get { return _oDietaKm; }
            set { _oDietaKm = value; }
        }

        private int? _t010_idoficina_base;
        public int? t010_idoficina_base
        {
            get { return _t010_idoficina_base; }
            set { _t010_idoficina_base = value; }
        }

        private int _t303_idnodo_beneficiario;
        public int t303_idnodo_beneficiario
        {
            get { return _t303_idnodo_beneficiario; }
            set { _t303_idnodo_beneficiario = value; }
        }
        private string _t303_denominacion_beneficiario;
        public string t303_denominacion_beneficiario
        {
            get { return _t303_denominacion_beneficiario; }
            set { _t303_denominacion_beneficiario = value; }
        }
        #endregion

        #region Constructor

        public CABECERAAPARCADA_NMPGV()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos
        public static CABECERAAPARCADA_NMPGV ObtenerDatosCabecera(int t663_idreferencia)
        {
            CABECERAAPARCADA_NMPGV o = new CABECERAAPARCADA_NMPGV();

            if (t663_idreferencia == 0) return o;

            SqlDataReader dr = DAL.CABECERAAPARCADA_NMPGV.ObtenerDatosCabecera(null, t663_idreferencia);

            if (dr.Read())
            {
                if (dr["t663_idreferencia"] != DBNull.Value)
                    o.t663_idreferencia = int.Parse(dr["t663_idreferencia"].ToString());
                if (dr["t663_concepto"] != DBNull.Value)
                    o.t663_concepto = (string)dr["t663_concepto"];
                if (dr["t001_idficepi_solicitante"] != DBNull.Value)
                    o.t001_idficepi_solicitante = int.Parse(dr["t001_idficepi_solicitante"].ToString());
                if (dr["Solicitante"] != DBNull.Value)
                    o.Solicitante = (string)dr["Solicitante"];
                if (dr["t314_idusuario_interesado"] != DBNull.Value)
                    o.t314_idusuario_interesado = int.Parse(dr["t314_idusuario_interesado"].ToString());
                if (dr["t001_idficepi_interesado"] != DBNull.Value)
                    o.t001_idficepi_interesado = int.Parse(dr["t001_idficepi_interesado"].ToString());
                if (dr["t001_codred"] != DBNull.Value)
                    o.t001_codred = (string)dr["t001_codred"];
                if (dr["Interesado"] != DBNull.Value)
                    o.Interesado = (string)dr["Interesado"];
                if (dr["t663_justificantes"] != DBNull.Value)
                    o.t663_justificantes = (bool)dr["t663_justificantes"];
                if (dr["t422_idmoneda"] != DBNull.Value)
                    o.t422_idmoneda = (string)dr["t422_idmoneda"];
                if (dr["t422_denominacion"] != DBNull.Value)
                    o.t422_denominacion = (string)dr["t422_denominacion"];
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = int.Parse(dr["t305_idproyectosubnodo"].ToString());
                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.t301_idproyecto = int.Parse(dr["t301_idproyecto"].ToString());
                if (dr["t301_denominacion"] != DBNull.Value)
                    o.t301_denominacion = (string)dr["t301_denominacion"];
                if (dr["t663_comentarionota"] != DBNull.Value)
                    o.t663_comentarionota = (string)dr["t663_comentarionota"];
                if (dr["t663_anotaciones"] != DBNull.Value)
                    o.t663_anotaciones = (string)dr["t663_anotaciones"];
                if (dr["t313_idempresa"] != DBNull.Value)
                    o.t313_idempresa = int.Parse(dr["t313_idempresa"].ToString());
                if (dr["t313_denominacion"] != DBNull.Value)
                    o.t313_denominacion = (string)dr["t313_denominacion"];

                o.oTerritorio = new TERRITORIO(byte.Parse(dr["t007_idterrfis"].ToString()),
                                                    dr["t007_nomterrfis"].ToString(),
                                                    decimal.Parse(dr["t007_iterdc"].ToString()),
                                                    decimal.Parse(dr["t007_itermd"].ToString()),
                                                    decimal.Parse(dr["t007_iterda"].ToString()),
                                                    decimal.Parse(dr["t007_iterde"].ToString()),
                                                    decimal.Parse(dr["t007_iterk"].ToString())
                                                );
                o.oOficinaLiquidadora = new OFICINA((short)dr["t010_idoficina_liquidadora"],
                                                    dr["t010_desoficina"].ToString());
                if (dr["t069_iddietakm"] != DBNull.Value)
                    o.oDietaKm = new DIETAKM((byte)dr["t069_iddietakm"],
                                                    dr["T069_descripcion"].ToString(),
                                                    decimal.Parse(dr["T069_icdc"].ToString()),
                                                    decimal.Parse(dr["T069_icmd"].ToString()),
                                                    decimal.Parse(dr["T069_icda"].ToString()),
                                                    decimal.Parse(dr["T069_icde"].ToString()),
                                                    decimal.Parse(dr["T069_ick"].ToString())
                                                );
                if (dr["t010_idoficina_base"] != DBNull.Value)
                    o.t010_idoficina_base = (int?)int.Parse(dr["t010_idoficina_base"].ToString());
                if (dr["t303_idnodo_beneficiario"] != DBNull.Value)
                    o.t303_idnodo_beneficiario = int.Parse(dr["t303_idnodo_beneficiario"].ToString());
                if (dr["t303_denominacion_beneficiario"] != DBNull.Value)
                    o.t303_denominacion_beneficiario = (string)dr["t303_denominacion_beneficiario"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de CABECERAAPARCADA_NMPGV"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
        public static string AparcarNotaMultiProyecto(string strDatosCabecera, string strDatosPosiciones)
        {
            string sResul = "";
            int nReferencia = 0, nFilasModificadas = 0;
            SqlConnection oConn = null;
            SqlTransaction tr = null;
            bool bErrorControlado = false, bExisteNota = false;

            string[] aDatosCabecera = Regex.Split(strDatosCabecera, "#sep#");
            #region Datos Cabecera
            ///aDatosCabecera[0] = txtConcepto //0
            ///aDatosCabecera[1] = hdnInteresado //1
            ///aDatosCabecera[2] = rdbJustificantes //2
            ///aDatosCabecera[3] = hdnIdProyectoSubNodo //3
            ///aDatosCabecera[4] = cboMoneda //4
            ///aDatosCabecera[5] = txtObservacionesNota //5
            ///aDatosCabecera[6] = hdnAnotacionesPersonales //6
            ///aDatosCabecera[7] = hdnReferencia //7
            ///aDatosCabecera[8] = hdnIdEmpresa //8
            #endregion Datos Posiciones
            string[] aPosiciones = Regex.Split(strDatosPosiciones, "#reg#");
            #region Posiciones
            ///aPosiciones[0] = sDesde  //0
            ///aPosiciones[1] = sHasta  //
            ///aPosiciones[2] = destino
            ///aPosiciones[3] = comentario
            ///aPosiciones[4] = DC
            ///aPosiciones[5] = MD
            ///aPosiciones[6] = DE
            ///aPosiciones[7] = DA
            ///aPosiciones[8] = KMS
            ///aPosiciones[9] = eco
            ///aPosiciones[10] = Peaje
            ///aPosiciones[11] = Comida
            ///aPosiciones[12] = Trans
            ///aPosiciones[13] = Hoteles
            ///aPosiciones[14] = idPSN

            #endregion

            #region Abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
                try
                {
                    #region Aparcar Cabecera
                    if (aDatosCabecera[7] == "")
                    {
                        nReferencia = DAL.CABECERAAPARCADA_NMPGV.AparcarCabecera(tr,
                                                        Utilidades.unescape(aDatosCabecera[0]),
                                                        (int)HttpContext.Current.Session["GVT_IDFICEPI"],
                                                        int.Parse(aDatosCabecera[1]),
                                                        (aDatosCabecera[2] == "1") ? true : false,
                                                        (aDatosCabecera[3] == "") ? null : (int?)int.Parse(aDatosCabecera[3]),
                                                        aDatosCabecera[4],
                                                        Utilidades.unescape(aDatosCabecera[5]),
                                                        Utilidades.unescape(aDatosCabecera[6]),
                                                        (aDatosCabecera[8] == "") ? null : (int?)int.Parse(aDatosCabecera[8])
                                                        );
                    }
                    else
                    {
                        bExisteNota = true;
                        nReferencia = int.Parse(aDatosCabecera[7]);
                        nFilasModificadas = DAL.CABECERAAPARCADA_NMPGV.ReAparcarCabecera(tr,
                                                        nReferencia,
                                                        Utilidades.unescape(aDatosCabecera[0]),
                                                        (int)HttpContext.Current.Session["GVT_IDFICEPI"],
                                                        int.Parse(aDatosCabecera[1]),
                                                        (aDatosCabecera[2] == "1") ? true : false,
                                                        (aDatosCabecera[3] == "") ? null : (int?)int.Parse(aDatosCabecera[3]),
                                                        aDatosCabecera[4],
                                                        Utilidades.unescape(aDatosCabecera[5]),
                                                        Utilidades.unescape(aDatosCabecera[6]),
                                                        DateTime.Now,
                                                        (aDatosCabecera[8] == "") ? null : (int?)int.Parse(aDatosCabecera[8])
                                                        );

                        if (nFilasModificadas == 0)
                        {
                            sResul = "Solicitud aparcada no existente";
                            bErrorControlado = true;
                            throw (new Exception(sResul));
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    if (bErrorControlado) throw (new Exception(ex.Message));
                    else throw (new Exception("Error al aparcar la cabecera. " + ex.Message));
                }

                try
                {
                    if (bExisteNota)
                    {
                        DAL.POSICIONAPARCADA_NMPGV.DeleteByT663_idreferencia(tr, nReferencia);
                    }

                    #region Aparcar Posiciones
                    foreach (string oPosicion in aPosiciones)
                    {
                        if (oPosicion == "") continue;
                        string[] aDatosPosicion = Regex.Split(oPosicion, "#sep#");

                        DAL.POSICIONAPARCADA_NMPGV.AparcarPosicion(tr, nReferencia,
                                                (aDatosPosicion[0] == "") ? null : (DateTime?)DateTime.Parse(aDatosPosicion[0]),
                                                (aDatosPosicion[1] == "") ? null : (DateTime?)DateTime.Parse(aDatosPosicion[1]),
                                                Utilidades.unescape(aDatosPosicion[2]),
                                                Utilidades.unescape(aDatosPosicion[3]),
                                                (byte)decimal.Parse(aDatosPosicion[4]),
                                                (byte)decimal.Parse(aDatosPosicion[5]),
                                                (byte)decimal.Parse(aDatosPosicion[6]),
                                                (byte)decimal.Parse(aDatosPosicion[7]),
                                                (short)decimal.Parse(aDatosPosicion[8]),
                                                (aDatosPosicion[9] == "") ? null : (int?)int.Parse(aDatosPosicion[9]),
                                                decimal.Parse(aDatosPosicion[10]),
                                                decimal.Parse(aDatosPosicion[11]),
                                                decimal.Parse(aDatosPosicion[12]),
                                                decimal.Parse(aDatosPosicion[13]),
                                                (aDatosPosicion[14] == "") ? null : (int?)int.Parse(aDatosPosicion[14])
                                                );
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    throw (new Exception("Error al aparcar la posición. " + ex.Message));
                }

                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                if (bErrorControlado) sResul = ex.Message;
                else sResul = Errores.mostrarError("Error al aparcar la nota multiproyecto.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }

            return nReferencia.ToString();
        }

        public static void Eliminar(int t663_idreferencia)
        {
            DAL.CABECERAAPARCADA_NMPGV.EliminarNotaAparcada(null, t663_idreferencia);
        }

        #endregion
    }
}
