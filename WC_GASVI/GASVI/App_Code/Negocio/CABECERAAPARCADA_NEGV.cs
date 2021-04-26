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
    public class CABECERAAPARCADA_NEGV
    {
        #region Propiedades y Atributos

        private int _t660_idreferencia;
        public int t660_idreferencia
        {
            get { return _t660_idreferencia; }
            set { _t660_idreferencia = value; }
        }

        private string _t660_concepto;
        public string t660_concepto
        {
            get { return _t660_concepto; }
            set { _t660_concepto = value; }
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

        private int _t001_idficepi_interesado;
        public int t001_idficepi_interesado
        {
            get { return _t001_idficepi_interesado; }
            set { _t001_idficepi_interesado = value; }
        }

        private string _t001_codred;
        public string t001_codred
        {
            get { return _t001_codred; }
            set { _t001_codred = value; }
        }

        private byte _t423_idmotivo;
        public byte t423_idmotivo
        {
            get { return _t423_idmotivo; }
            set { _t423_idmotivo = value; }
        }

        private bool? _t660_justificantes;
        public bool? t660_justificantes
        {
            get { return _t660_justificantes; }
            set { _t660_justificantes = value; }
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

        private string _t660_comentarionota;
        public string t660_comentarionota
        {
            get { return _t660_comentarionota; }
            set { _t660_comentarionota = value; }
        }

        private string _t660_anotaciones;
        public string t660_anotaciones
        {
            get { return _t660_anotaciones; }
            set { _t660_anotaciones = value; }
        }

        private decimal _t660_importeanticipo;
        public decimal t660_importeanticipo
        {
            get { return _t660_importeanticipo; }
            set { _t660_importeanticipo = value; }
        }

        private DateTime? _t660_fanticipo;
        public DateTime? t660_fanticipo
        {
            get { return _t660_fanticipo; }
            set { _t660_fanticipo = value; }
        }

        private string _t660_lugaranticipo;
        public string t660_lugaranticipo
        {
            get { return _t660_lugaranticipo; }
            set { _t660_lugaranticipo = value; }
        }

        private decimal _t660_importedevolucion;
        public decimal t660_importedevolucion
        {
            get { return _t660_importedevolucion; }
            set { _t660_importedevolucion = value; }
        }

        private DateTime? _t660_fdevolucion;
        public DateTime? t660_fdevolucion
        {
            get { return _t660_fdevolucion; }
            set { _t660_fdevolucion = value; }
        }

        private string _t660_lugardevolucion;
        public string t660_lugardevolucion
        {
            get { return _t660_lugardevolucion; }
            set { _t660_lugardevolucion = value; }
        }

        private string _t660_aclaracionesanticipo;
        public string t660_aclaracionesanticipo
        {
            get { return _t660_aclaracionesanticipo; }
            set { _t660_aclaracionesanticipo = value; }
        }

        private decimal _t660_pagadotransporte;
        public decimal t660_pagadotransporte
        {
            get { return _t660_pagadotransporte; }
            set { _t660_pagadotransporte = value; }
        }

        private decimal _t660_pagadohotel;
        public decimal t660_pagadohotel
        {
            get { return _t660_pagadohotel; }
            set { _t660_pagadohotel = value; }
        }

        private decimal _t660_pagadootros;
        public decimal t660_pagadootros
        {
            get { return _t660_pagadootros; }
            set { _t660_pagadootros = value; }
        }

        private string _t660_aclaracionepagado;
        public string t660_aclaracionepagado
        {
            get { return _t660_aclaracionepagado; }
            set { _t660_aclaracionepagado = value; }
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

        private string _t423_denominacion;
        public string t423_denominacion
        {
            get { return _t423_denominacion; }
            set { _t423_denominacion = value; }
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
        //60
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

        private int _nCCIberper;
        public int nCCIberper
        {
            get { return _nCCIberper; }
            set { _nCCIberper = value; }
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

        public CABECERAAPARCADA_NEGV()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        public static CABECERAAPARCADA_NEGV ObtenerDatosCabecera(int t660_idreferencia)
        {
            CABECERAAPARCADA_NEGV o = new CABECERAAPARCADA_NEGV();

            if (t660_idreferencia == 0) return o;
            //Si el interesdo sigue perteneciendo a la misma empresa con la que se aparcó la nota, la empresa se toma de la nota
            //Sino, si solo pertenece a una empresa se toma esa
            //  Sino, se deja en blanco para obligarle a elegir
            SqlDataReader dr;
            dr = DAL.CABECERAAPARCADA_NEGV.ObtenerDatosCabecera(null, t660_idreferencia);

            if (dr.Read())
            {
                if (dr["t660_idreferencia"] != DBNull.Value)
                    o.t660_idreferencia = int.Parse(dr["t660_idreferencia"].ToString());
                if (dr["t660_concepto"] != DBNull.Value)
                    o.t660_concepto = (string)dr["t660_concepto"];
                if (dr["t001_idficepi_solicitante"] != DBNull.Value)
                    o.t001_idficepi_solicitante = int.Parse(dr["t001_idficepi_solicitante"].ToString());
                if (dr["Solicitante"] != DBNull.Value)
                    o.Solicitante = (string)dr["Solicitante"];
                if (dr["t001_idficepi_interesado"] != DBNull.Value)
                    o.t001_idficepi_interesado = int.Parse(dr["t001_idficepi_interesado"].ToString());
                if (dr["t001_codred"] != DBNull.Value)
                    o.t001_codred = (string)dr["t001_codred"];
                if (dr["t314_idusuario_interesado"] != DBNull.Value)
                    o.t314_idusuario_interesado = int.Parse(dr["t314_idusuario_interesado"].ToString());
                if (dr["Interesado"] != DBNull.Value)
                    o.Interesado = (string)dr["Interesado"];
                if (dr["t423_idmotivo"] != DBNull.Value)
                    o.t423_idmotivo = byte.Parse(dr["t423_idmotivo"].ToString());
                if (dr["t423_denominacion"] != DBNull.Value)
                    o.t423_denominacion = (string)dr["t423_denominacion"];
                if (dr["t660_justificantes"] != DBNull.Value)
                    o.t660_justificantes = (bool)dr["t660_justificantes"];
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
                if (dr["t660_comentarionota"] != DBNull.Value)
                    o.t660_comentarionota = (string)dr["t660_comentarionota"];
                if (dr["t660_anotaciones"] != DBNull.Value)
                    o.t660_anotaciones = (string)dr["t660_anotaciones"];
                if (dr["t660_importeanticipo"] != DBNull.Value)
                    o.t660_importeanticipo = decimal.Parse(dr["t660_importeanticipo"].ToString());
                if (dr["t660_fanticipo"] != DBNull.Value)
                    o.t660_fanticipo = (DateTime)dr["t660_fanticipo"];
                if (dr["t660_lugaranticipo"] != DBNull.Value)
                    o.t660_lugaranticipo = (string)dr["t660_lugaranticipo"];
                if (dr["t660_importedevolucion"] != DBNull.Value)
                    o.t660_importedevolucion = decimal.Parse(dr["t660_importedevolucion"].ToString());
                if (dr["t660_fdevolucion"] != DBNull.Value)
                    o.t660_fdevolucion = (DateTime)dr["t660_fdevolucion"];
                if (dr["t660_lugardevolucion"] != DBNull.Value)
                    o.t660_lugardevolucion = (string)dr["t660_lugardevolucion"];
                if (dr["t660_aclaracionesanticipo"] != DBNull.Value)
                    o.t660_aclaracionesanticipo = (string)dr["t660_aclaracionesanticipo"];
                if (dr["t660_pagadotransporte"] != DBNull.Value)
                    o.t660_pagadotransporte = decimal.Parse(dr["t660_pagadotransporte"].ToString());
                if (dr["t660_pagadohotel"] != DBNull.Value)
                    o.t660_pagadohotel = decimal.Parse(dr["t660_pagadohotel"].ToString());
                if (dr["t660_pagadootros"] != DBNull.Value)
                    o.t660_pagadootros = decimal.Parse(dr["t660_pagadootros"].ToString());
                if (dr["t660_aclaracionepagado"] != DBNull.Value)
                    o.t660_aclaracionepagado = (string)dr["t660_aclaracionepagado"];
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
                if (dr["CentrosCoste"] != DBNull.Value)
                    o.nCCIberper = int.Parse(dr["CentrosCoste"].ToString());
                if (dr["t303_idnodo_beneficiario"] != DBNull.Value)
                    o.t303_idnodo_beneficiario = int.Parse(dr["t303_idnodo_beneficiario"].ToString());
                if (dr["t303_denominacion_beneficiario"] != DBNull.Value)
                    o.t303_denominacion_beneficiario = (string)dr["t303_denominacion_beneficiario"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de CABECERAAPARCADA_NEGV"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static string AparcarNotaEstandar(string strDatosCabecera, string strDatosPosiciones)
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
            ///aDatosCabecera[2] = cboMotivo //2
            ///aDatosCabecera[3] = rdbJustificantes //3
            ///aDatosCabecera[4] = hdnIdProyectoSubNodo //4
            ///aDatosCabecera[5] = cboMoneda //5
            ///aDatosCabecera[6] = txtObservacionesNota //6
            ///aDatosCabecera[7] = hdnAnotacionesPersonales //7
            ///aDatosCabecera[8] = txtImpAnticipo //8
            ///aDatosCabecera[9] = txtFecAnticipo //9
            ///aDatosCabecera[10] = txtOficinaAnticipo //10
            ///aDatosCabecera[11] = txtImpDevolucion //11
            ///aDatosCabecera[12] = txtFecDevolucion //12
            ///aDatosCabecera[13] = txtOficinaDevolucion //13
            ///aDatosCabecera[14] = txtAclaracionesAnticipos //14
            ///aDatosCabecera[15] = txtPagadoTransporte //15
            ///aDatosCabecera[16] = txtPagadoHotel //16
            ///aDatosCabecera[17] = txtPagadoOtros //17
            ///aDatosCabecera[18] = txtAclaracionesPagado //18
            ///aDatosCabecera[19] = hdnReferencia //19
            ///aDatosCabecera[20] = hdnIdEmpresa //20
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
                    if (aDatosCabecera[19] == "")
                    {
                        nReferencia = DAL.CABECERAAPARCADA_NEGV.AparcarCabecera(tr,
                                                        Utilidades.unescape(aDatosCabecera[0]),
                                                        (int)HttpContext.Current.Session["GVT_IDFICEPI"],
                                                        int.Parse(aDatosCabecera[1]),
                                                        byte.Parse(aDatosCabecera[2]),
                                                        (aDatosCabecera[3] == "1") ? true : false,
                                                        (aDatosCabecera[4] == "") ? null : (int?)int.Parse(aDatosCabecera[4]),
                                                        aDatosCabecera[5],
                                                        Utilidades.unescape(aDatosCabecera[6]),
                                                        Utilidades.unescape(aDatosCabecera[7]),
                                                        decimal.Parse(aDatosCabecera[8]),
                                                        (aDatosCabecera[9] == "") ? null : (DateTime?)DateTime.Parse(aDatosCabecera[9]),
                                                        Utilidades.unescape(aDatosCabecera[10]),
                                                        decimal.Parse(aDatosCabecera[11]),
                                                        (aDatosCabecera[12] == "") ? null : (DateTime?)DateTime.Parse(aDatosCabecera[12]),
                                                        Utilidades.unescape(aDatosCabecera[13]),
                                                        Utilidades.unescape(aDatosCabecera[14]),
                                                        decimal.Parse(aDatosCabecera[15]),
                                                        decimal.Parse(aDatosCabecera[16]),
                                                        decimal.Parse(aDatosCabecera[17]),
                                                        Utilidades.unescape(aDatosCabecera[18]),
                                                        (aDatosCabecera[20] == "") ? null : (int?)int.Parse(aDatosCabecera[20])
                                                        );
                    }
                    else
                    {
                        bExisteNota = true;
                        nReferencia = int.Parse(aDatosCabecera[19]);
                        nFilasModificadas = DAL.CABECERAAPARCADA_NEGV.ReAparcarCabecera(tr,
                                                        nReferencia,
                                                        Utilidades.unescape(aDatosCabecera[0]),
                                                        (int)HttpContext.Current.Session["GVT_IDFICEPI"],
                                                        int.Parse(aDatosCabecera[1]),
                                                        byte.Parse(aDatosCabecera[2]),
                                                        (aDatosCabecera[3] == "1") ? true : false,
                                                        (aDatosCabecera[4] == "") ? null : (int?)int.Parse(aDatosCabecera[4]),
                                                        aDatosCabecera[5],
                                                        Utilidades.unescape(aDatosCabecera[6]),
                                                        Utilidades.unescape(aDatosCabecera[7]),
                                                        decimal.Parse(aDatosCabecera[8]),
                                                        (aDatosCabecera[9] == "") ? null : (DateTime?)DateTime.Parse(aDatosCabecera[9]),
                                                        Utilidades.unescape(aDatosCabecera[10]),
                                                        decimal.Parse(aDatosCabecera[11]),
                                                        (aDatosCabecera[12] == "") ? null : (DateTime?)DateTime.Parse(aDatosCabecera[12]),
                                                        Utilidades.unescape(aDatosCabecera[13]),
                                                        Utilidades.unescape(aDatosCabecera[14]),
                                                        decimal.Parse(aDatosCabecera[15]),
                                                        decimal.Parse(aDatosCabecera[16]),
                                                        decimal.Parse(aDatosCabecera[17]),
                                                        Utilidades.unescape(aDatosCabecera[18]),
                                                        DateTime.Now,
                                                        (aDatosCabecera[20] == "") ? null : (int?)int.Parse(aDatosCabecera[20])
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
                        DAL.POSICIONAPARCADA_NEGV.DeleteByT660_idreferencia(tr, nReferencia);
                    }

                    #region Aparcar Posiciones
                    foreach (string oPosicion in aPosiciones)
                    {
                        if (oPosicion == "") continue;
                        string[] aDatosPosicion = Regex.Split(oPosicion, "#sep#");

                        DAL.POSICIONAPARCADA_NEGV.AparcarPosicion(tr, nReferencia,
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
                                                decimal.Parse(aDatosPosicion[13])
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
                else sResul = Errores.mostrarError("Error al aparcar la nota estándar.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }

            return nReferencia.ToString();
        }

        public static void Eliminar(int t660_idreferencia)
        {
            DAL.CABECERAAPARCADA_NEGV.EliminarNotaAparcada(null, t660_idreferencia);
        }

        #endregion
    }
}
