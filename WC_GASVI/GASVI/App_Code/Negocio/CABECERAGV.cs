using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;
using GASVI.DAL;

namespace GASVI.BLL
{
    /// -----------------------------------------------------------------------------
    /// Project	 : GASVI
    /// Class	 : CABECERAGV
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T420_CABECERAGV
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	16/03/2011 11:54:08	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class CABECERAGV
    {
        #region Propiedades y Atributos

        private int _t420_idreferencia;
        public int t420_idreferencia
        {
            get { return _t420_idreferencia; }
            set { _t420_idreferencia = value; }
        }

        private string _t431_idestado;
        public string t431_idestado
        {
            get { return _t431_idestado; }
            set { _t431_idestado = value; }
        }

        private string _t420_concepto;
        public string t420_concepto
        {
            get { return _t420_concepto; }
            set { _t420_concepto = value; }
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

        private int? _t001_idficepi_aprobadapor;
        public int? t001_idficepi_aprobadapor
        {
            get { return _t001_idficepi_aprobadapor; }
            set { _t001_idficepi_aprobadapor = value; }
        }

        private int? _t001_idficepi_aceptadapor;
        public int? t001_idficepi_aceptadapor
        {
            get { return _t001_idficepi_aceptadapor; }
            set { _t001_idficepi_aceptadapor = value; }
        }

        private byte _t423_idmotivo;
        public byte t423_idmotivo
        {
            get { return _t423_idmotivo; }
            set { _t423_idmotivo = value; }
        }

        private bool? _t420_justificantes;
        public bool? t420_justificantes
        {
            get { return _t420_justificantes; }
            set { _t420_justificantes = value; }
        }

        private int? _t305_idproyectosubnodo;
        public int? t305_idproyectosubnodo
        {
            get { return _t305_idproyectosubnodo; }
            set { _t305_idproyectosubnodo = value; }
        }

        private string _t175_idcc;
        public string t175_idcc
        {
            get { return _t175_idcc; }
            set { _t175_idcc = value; }
        }
        //20
        private string _t422_idmoneda;
        public string t422_idmoneda
        {
            get { return _t422_idmoneda; }
            set { _t422_idmoneda = value; }
        }

        private decimal _t420_importeanticipo;
        public decimal t420_importeanticipo
        {
            get { return _t420_importeanticipo; }
            set { _t420_importeanticipo = value; }
        }

        private DateTime? _t420_fanticipo;
        public DateTime? t420_fanticipo
        {
            get { return _t420_fanticipo; }
            set { _t420_fanticipo = value; }
        }

        private string _t420_lugaranticipo;
        public string t420_lugaranticipo
        {
            get { return _t420_lugaranticipo; }
            set { _t420_lugaranticipo = value; }
        }

        private decimal _t420_importedevolucion;
        public decimal t420_importedevolucion
        {
            get { return _t420_importedevolucion; }
            set { _t420_importedevolucion = value; }
        }

        private DateTime? _t420_fdevolucion;
        public DateTime? t420_fdevolucion
        {
            get { return _t420_fdevolucion; }
            set { _t420_fdevolucion = value; }
        }

        private string _t420_lugardevolucion;
        public string t420_lugardevolucion
        {
            get { return _t420_lugardevolucion; }
            set { _t420_lugardevolucion = value; }
        }

        private string _t420_aclaracionesanticipo;
        public string t420_aclaracionesanticipo
        {
            get { return _t420_aclaracionesanticipo; }
            set { _t420_aclaracionesanticipo = value; }
        }

        private decimal _t420_pagadotransporte;
        public decimal t420_pagadotransporte
        {
            get { return _t420_pagadotransporte; }
            set { _t420_pagadotransporte = value; }
        }

        private decimal _t420_pagadohotel;
        public decimal t420_pagadohotel
        {
            get { return _t420_pagadohotel; }
            set { _t420_pagadohotel = value; }
        }

        private decimal _t420_pagadootros;
        public decimal t420_pagadootros
        {
            get { return _t420_pagadootros; }
            set { _t420_pagadootros = value; }
        }

        private string _t420_aclaracionepagado;
        public string t420_aclaracionepagado
        {
            get { return _t420_aclaracionepagado; }
            set { _t420_aclaracionepagado = value; }
        }

        private string _t420_comentarionota;
        public string t420_comentarionota
        {
            get { return _t420_comentarionota; }
            set { _t420_comentarionota = value; }
        }

        private string _t420_anotaciones;
        public string t420_anotaciones
        {
            get { return _t420_anotaciones; }
            set { _t420_anotaciones = value; }
        }
        //30
        private int _t313_idempresa;
        public int t313_idempresa
        {
            get { return _t313_idempresa; }
            set { _t313_idempresa = value; }
        }

        private byte _t007_idterrfis;
        public byte t007_idterrfis
        {
            get { return _t007_idterrfis; }
            set { _t007_idterrfis = value; }
        }

        private decimal _t420_impdico;
        public decimal t420_impdico
        {
            get { return _t420_impdico; }
            set { _t420_impdico = value; }
        }

        private decimal _t420_impmdco;
        public decimal t420_impmdco
        {
            get { return _t420_impmdco; }
            set { _t420_impmdco = value; }
        }

        private decimal _t420_impalco;
        public decimal t420_impalco
        {
            get { return _t420_impalco; }
            set { _t420_impalco = value; }
        }

        private decimal _t420_impkmco;
        public decimal t420_impkmco
        {
            get { return _t420_impkmco; }
            set { _t420_impkmco = value; }
        }

        private decimal _t420_impdeco;
        public decimal t420_impdeco
        {
            get { return _t420_impdeco; }
            set { _t420_impdeco = value; }
        }

        private decimal _t420_impdiex;
        public decimal t420_impdiex
        {
            get { return _t420_impdiex; }
            set { _t420_impdiex = value; }
        }

        private decimal _t420_impmdex;
        public decimal t420_impmdex
        {
            get { return _t420_impmdex; }
            set { _t420_impmdex = value; }
        }

        private decimal _t420_impalex;
        public decimal t420_impalex
        {
            get { return _t420_impalex; }
            set { _t420_impalex = value; }
        }
        //40
        private decimal _t420_impkmex;
        public decimal t420_impkmex
        {
            get { return _t420_impkmex; }
            set { _t420_impkmex = value; }
        }

        private decimal _t420_impdeex;
        public decimal t420_impdeex
        {
            get { return _t420_impdeex; }
            set { _t420_impdeex = value; }
        }

        private decimal _t420_iteatdc;
        public decimal t420_iteatdc
        {
            get { return _t420_iteatdc; }
            set { _t420_iteatdc = value; }
        }

        private decimal _t420_iteatmd;
        public decimal t420_iteatmd
        {
            get { return _t420_iteatmd; }
            set { _t420_iteatmd = value; }
        }

        private decimal _t420_iteatda;
        public decimal t420_iteatda
        {
            get { return _t420_iteatda; }
            set { _t420_iteatda = value; }
        }

        private decimal _t420_iteatkm;
        public decimal t420_iteatkm
        {
            get { return _t420_iteatkm; }
            set { _t420_iteatkm = value; }
        }

        private decimal _t420_iteatde;
        public decimal t420_iteatde
        {
            get { return _t420_iteatde; }
            set { _t420_iteatde = value; }
        }

        private float _t420_tipocambio;
        public float t420_tipocambio
        {
            get { return _t420_tipocambio; }
            set { _t420_tipocambio = value; }
        }

        private short? _t010_idoficina;
        public short? t010_idoficina
        {
            get { return _t010_idoficina; }
            set { _t010_idoficina = value; }
        }
        
        private int? _t420_idreferencia_lote;
        public int? t420_idreferencia_lote
        {
            get { return _t420_idreferencia_lote; }
            set { _t420_idreferencia_lote = value; }
        }
        //50
        private DateTime? _t420_fcontabilizacion;
        public DateTime? t420_fcontabilizacion
        {
            get { return _t420_fcontabilizacion; }
            set { _t420_fcontabilizacion = value; }
        }

        #endregion

        #region Propiedades y Atributos complementarios

        private string _t431_denominacion;
        public string t431_denominacion
        {
            get { return _t431_denominacion; }
            set { _t431_denominacion = value; }
        }

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

        private string _Aprobador;
        public string Aprobador
        {
            get { return _Aprobador; }
            set { _Aprobador = value; }
        }

        private string _Aceptador;
        public string Aceptador
        {
            get { return _Aceptador; }
            set { _Aceptador = value; }
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

        private string _t175_denominacion;
        public string t175_denominacion
        {
            get { return _t175_denominacion; }
            set { _t175_denominacion = value; }
        }

        private string _t422_denominacion;
        public string t422_denominacion
        {
            get { return _t422_denominacion; }
            set { _t422_denominacion = value; }
        }
        //60
        private string _t313_denominacion;
        public string t313_denominacion
        {
            get { return _t313_denominacion; }
            set { _t313_denominacion = value; }
        }

        private string _t007_nomterrfis;
        public string t007_nomterrfis
        {
            get { return _t007_nomterrfis; }
            set { _t007_nomterrfis = value; }
        }

        private string _t010_desoficina;
        public string t010_desoficina
        {
            get { return _t010_desoficina; }
            set { _t010_desoficina = value; }
        }

        private bool _bAutorresponsable;
        public bool bAutorresponsable
        {
            get { return _bAutorresponsable; }
            set { _bAutorresponsable = value; }
        }

        private int _t655_idBono;
        public int t655_idBono
        {
            get { return _t655_idBono; }
            set { _t655_idBono = value; }
        }

        private decimal _t420_importe;
        public decimal t420_importe
        {
            get { return _t420_importe; }
            set { _t420_importe = value; }
        }

        private int _t420_anomesbono;
        public int t420_anomesbono
        {
            get { return _t420_anomesbono; }
            set { _t420_anomesbono = value; }
        }

        private string _t655_denominacion;
        public string t655_denominacion
        {
            get { return _t655_denominacion; }
            set { _t655_denominacion = value; }
        }

        private int? _t666_idacuerdogv;
        public int? t666_idacuerdogv
        {
            get { return _t666_idacuerdogv; }
            set { _t666_idacuerdogv = value; }
        }

        private string _t666_denominacion;
        public string t666_denominacion
        {
            get { return _t666_denominacion; }
            set { _t666_denominacion = value; }
        }
        //70
        private short? _t010_idoficina_base;
        public short? t010_idoficina_base
        {
            get { return _t010_idoficina_base; }
            set { _t010_idoficina_base = value; }
        }

        private string _TipoNota;
        public string TipoNota
        {
            get { return _TipoNota; }
            set { _TipoNota = value; }
        }

        private decimal _TOTALVIAJE;
        public decimal TOTALVIAJE
        {
            get { return _TOTALVIAJE; }
            set { _TOTALVIAJE = value; }
        }

        private string _t001_sexo_interesado;
        public string t001_sexo_interesado
        {
            get { return _t001_sexo_interesado; }
            set { _t001_sexo_interesado = value; }
        }

        private string _t175_idcc_solicitud;
        public string t175_idcc_solicitud
        {
            get { return _t175_idcc_solicitud; }
            set { _t175_idcc_solicitud = value; }
        }
        private string _t175_denominacion_solicitud;
        public string t175_denominacion_solicitud
        {
            get { return _t175_denominacion_solicitud; }
            set { _t175_denominacion_solicitud = value; }
        }
        private int? _t303_idnodo_solicitud;
        public int? t303_idnodo_solicitud
        {
            get { return _t303_idnodo_solicitud; }
            set { _t303_idnodo_solicitud = value; }
        }
        private string _t303_denominacion_solicitud;
        public string t303_denominacion_solicitud
        {
            get { return _t303_denominacion_solicitud; }
            set { _t303_denominacion_solicitud = value; }
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


        private int _nCCIberper;
        public int nCCIberper
        {
            get { return _nCCIberper; }
            set { _nCCIberper = value; }
        }


        private string _t422_idmoneda_acuerdo;
        public string t422_idmoneda_acuerdo
        {
            get { return _t422_idmoneda_acuerdo; }
            set { _t422_idmoneda_acuerdo = value; }
        }

        #endregion

        #region Constructor

        public CABECERAGV()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos
        
        public static CABECERAGV ObtenerDatosCabecera(int t420_idreferencia)
        {
            CABECERAGV o = new CABECERAGV();

            if (t420_idreferencia == 0) return o;

            SqlDataReader dr = DAL.CABECERAGV.ObtenerDatosCabecera(null, t420_idreferencia);

            if (dr.Read())
            {
                if (dr["t420_idreferencia"] != DBNull.Value)
                    o.t420_idreferencia = int.Parse(dr["t420_idreferencia"].ToString());
                if (dr["t431_idestado"] != DBNull.Value)
                    o.t431_idestado = (string)dr["t431_idestado"];
                if (dr["t431_denominacion"] != DBNull.Value)
                    o.t431_denominacion = (string)dr["t431_denominacion"];
                if (dr["t420_concepto"] != DBNull.Value)
                    o.t420_concepto = (string)dr["t420_concepto"];
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
                if (dr["t001_idficepi_aprobada"] != DBNull.Value)
                    o.t001_idficepi_aprobadapor = int.Parse(dr["t001_idficepi_aprobada"].ToString());
                if (dr["AprobadaPor"] != DBNull.Value)
                    o.Aprobador = (string)dr["AprobadaPor"];
                if (dr["t001_idficepi_aceptada"] != DBNull.Value)
                    o.t001_idficepi_aceptadapor = int.Parse(dr["t001_idficepi_aceptada"].ToString());
                if (dr["AceptadaPor"] != DBNull.Value)
                    o.Aceptador = (string)dr["AceptadaPor"];
                if (dr["t423_idmotivo"] != DBNull.Value)
                    o.t423_idmotivo = byte.Parse(dr["t423_idmotivo"].ToString());
                if (dr["t423_denominacion"] != DBNull.Value)
                    o.t423_denominacion = (string)dr["t423_denominacion"];
                if (dr["t420_pagadotransporte"] != DBNull.Value)
                    o.t420_pagadotransporte = decimal.Parse(dr["t420_pagadotransporte"].ToString());
                if (dr["t420_pagadohotel"] != DBNull.Value)
                    o.t420_pagadohotel = decimal.Parse(dr["t420_pagadohotel"].ToString());
                if (dr["t420_pagadootros"] != DBNull.Value)
                    o.t420_pagadootros = decimal.Parse(dr["t420_pagadootros"].ToString());
                if (dr["t420_aclaracionepagado"] != DBNull.Value)
                    o.t420_aclaracionepagado = (string)dr["t420_aclaracionepagado"];
                if (dr["t420_justificantes"] != DBNull.Value)
                    o.t420_justificantes = (bool)dr["t420_justificantes"];
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = int.Parse(dr["t305_idproyectosubnodo"].ToString());
                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.t301_idproyecto = int.Parse(dr["t301_idproyecto"].ToString());
                if (dr["t301_denominacion"] != DBNull.Value)
                    o.t301_denominacion = (string)dr["t301_denominacion"];
                if (dr["t175_idcc"] != DBNull.Value)
                    o.t175_idcc = (string)dr["t175_idcc"];
                if (dr["t422_idmoneda"] != DBNull.Value)
                    o.t422_idmoneda = (string)dr["t422_idmoneda"];
                if (dr["t422_denominacion"] != DBNull.Value)
                    o.t422_denominacion = (string)dr["t422_denominacion"];
                if (dr["t420_importeanticipo"] != DBNull.Value)
                    o.t420_importeanticipo = decimal.Parse(dr["t420_importeanticipo"].ToString());
                if (dr["t420_fanticipo"] != DBNull.Value)
                    o.t420_fanticipo = (DateTime)dr["t420_fanticipo"];
                if (dr["t420_lugaranticipo"] != DBNull.Value)
                    o.t420_lugaranticipo = (string)dr["t420_lugaranticipo"];
                if (dr["t420_importedevolucion"] != DBNull.Value)
                    o.t420_importedevolucion = decimal.Parse(dr["t420_importedevolucion"].ToString());
                if (dr["t420_fdevolucion"] != DBNull.Value)
                    o.t420_fdevolucion = (DateTime)dr["t420_fdevolucion"];
                if (dr["t420_lugardevolucion"] != DBNull.Value)
                    o.t420_lugardevolucion = (string)dr["t420_lugardevolucion"];
                if (dr["t420_aclaracionesanticipo"] != DBNull.Value)
                    o.t420_aclaracionesanticipo = (string)dr["t420_aclaracionesanticipo"];
                if (dr["t420_comentarionota"] != DBNull.Value)
                    o.t420_comentarionota = (string)dr["t420_comentarionota"];
                if (dr["t420_anotaciones"] != DBNull.Value)
                    o.t420_anotaciones = (string)dr["t420_anotaciones"];
                if (dr["t313_idempresa"] != DBNull.Value)
                    o.t313_idempresa = int.Parse(dr["t313_idempresa"].ToString());
                if (dr["t313_denominacion"] != DBNull.Value)
                    o.t313_denominacion = (string)dr["t313_denominacion"];
                if (dr["t007_idterrfis"] != DBNull.Value)
                    o.t007_idterrfis = byte.Parse(dr["t007_idterrfis"].ToString());
                if (dr["t007_nomterrfis"] != DBNull.Value)
                    o.t007_nomterrfis = (string)dr["t007_nomterrfis"];
                if (dr["t420_impdico"] != DBNull.Value)
                    o.t420_impdico = decimal.Parse(dr["t420_impdico"].ToString());
                if (dr["t420_impmdco"] != DBNull.Value)
                    o.t420_impmdco = decimal.Parse(dr["t420_impmdco"].ToString());
                if (dr["t420_impalco"] != DBNull.Value)
                    o.t420_impalco = decimal.Parse(dr["t420_impalco"].ToString());
                if (dr["t420_impkmco"] != DBNull.Value)
                    o.t420_impkmco = decimal.Parse(dr["t420_impkmco"].ToString());
                if (dr["t420_impdeco"] != DBNull.Value)
                    o.t420_impdeco = decimal.Parse(dr["t420_impdeco"].ToString());
                if (dr["t420_impdiex"] != DBNull.Value)
                    o.t420_impdiex = decimal.Parse(dr["t420_impdiex"].ToString());
                if (dr["t420_impmdex"] != DBNull.Value)
                    o.t420_impmdex = decimal.Parse(dr["t420_impmdex"].ToString());
                if (dr["t420_impalex"] != DBNull.Value)
                    o.t420_impalex = decimal.Parse(dr["t420_impalex"].ToString());
                if (dr["t420_impkmex"] != DBNull.Value)
                    o.t420_impkmex = decimal.Parse(dr["t420_impkmex"].ToString());
                if (dr["t420_impdeex"] != DBNull.Value)
                    o.t420_impdeex = decimal.Parse(dr["t420_impdeex"].ToString());
                if (dr["t420_iteatdc"] != DBNull.Value)
                    o.t420_iteatdc = decimal.Parse(dr["t420_iteatdc"].ToString());
                if (dr["t420_iteatmd"] != DBNull.Value)
                    o.t420_iteatmd = decimal.Parse(dr["t420_iteatmd"].ToString());
                if (dr["t420_iteatda"] != DBNull.Value)
                    o.t420_iteatda = decimal.Parse(dr["t420_iteatda"].ToString());
                if (dr["t420_iteatkm"] != DBNull.Value)
                    o.t420_iteatkm = decimal.Parse(dr["t420_iteatkm"].ToString());
                if (dr["t420_iteatde"] != DBNull.Value)
                    o.t420_iteatde = decimal.Parse(dr["t420_iteatde"].ToString());
                if (dr["t420_tipocambio"] != DBNull.Value)
                    o.t420_tipocambio = float.Parse(dr["t420_tipocambio"].ToString());
                if (dr["t010_idoficina"] != DBNull.Value)
                    o.t010_idoficina = (short)dr["t010_idoficina"];
                if (dr["t010_desoficina"] != DBNull.Value)
                    o.t010_desoficina = (string)dr["t010_desoficina"];
                if (dr["t420_idreferencia_lote"] != DBNull.Value)
                    o.t420_idreferencia_lote = int.Parse(dr["t420_idreferencia_lote"].ToString());
                if (dr["autorresponsable"] != DBNull.Value)
                    o.bAutorresponsable = ((int)dr["autorresponsable"] == 1) ? true : false;
                if (dr["t420_fcontabilizacion"] != DBNull.Value)
                    o.t420_fcontabilizacion = (DateTime)dr["t420_fcontabilizacion"];
                if (dr["t010_idoficina_base"] != DBNull.Value)
                    o.t010_idoficina_base = (short)dr["t010_idoficina_base"];
                if (dr["t001_sexo_interesado"] != DBNull.Value)
                    o.t001_sexo_interesado = (string)dr["t001_sexo_interesado"];

                if (dr["t175_idcc_solicitud"] != DBNull.Value)
                    o.t175_idcc_solicitud = (string)dr["t175_idcc_solicitud"];
                if (dr["t175_denominacion_solicitud"] != DBNull.Value)
                    o.t175_denominacion_solicitud = (string)dr["t175_denominacion_solicitud"];
                if (dr["t303_idnodo_solicitud"] != DBNull.Value)
                    o.t303_idnodo_solicitud = int.Parse(dr["t303_idnodo_solicitud"].ToString());
                if (dr["t303_denominacion_solicitud"] != DBNull.Value)
                    o.t303_denominacion_solicitud = (string)dr["t303_denominacion_solicitud"];
                
                if (dr["t303_idnodo_beneficiario"] != DBNull.Value)
                    o.t303_idnodo_beneficiario = int.Parse(dr["t303_idnodo_beneficiario"].ToString());
                if (dr["t303_denominacion_beneficiario"] != DBNull.Value)
                    o.t303_denominacion_beneficiario = (string)dr["t303_denominacion_beneficiario"];
                if (dr["CentrosCoste"] != DBNull.Value)
                    o.nCCIberper = int.Parse(dr["CentrosCoste"].ToString());
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de CABECERAGV"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static CABECERAGV ObtenerDatosCabeceraBono(int t420_idreferencia)
        {
            CABECERAGV o = new CABECERAGV();

            if (t420_idreferencia == 0) return o;

            SqlDataReader dr = DAL.CABECERAGV.ObtenerDatosCabeceraBono(null, t420_idreferencia);

            if (dr.Read())
            {
                if (dr["t420_idreferencia"] != DBNull.Value)
                    o.t420_idreferencia = int.Parse(dr["t420_idreferencia"].ToString());
                if (dr["t431_idestado"] != DBNull.Value)
                    o.t431_idestado = (string)dr["t431_idestado"];
                if (dr["t431_denominacion"] != DBNull.Value)
                    o.t431_denominacion = (string)dr["t431_denominacion"];
                if (dr["t420_concepto"] != DBNull.Value)
                    o.t420_concepto = (string)dr["t420_concepto"];
                if (dr["t001_idficepi_solicitante"] != DBNull.Value)
                    o.t001_idficepi_solicitante = int.Parse(dr["t001_idficepi_solicitante"].ToString());
                if (dr["Solicitante"] != DBNull.Value)
                    o.Solicitante = (string)dr["Solicitante"];
                if (dr["t314_idusuario_interesado"] != DBNull.Value)
                    o.t314_idusuario_interesado = int.Parse(dr["t314_idusuario_interesado"].ToString());
                if (dr["t001_idficepi_interesado"] != DBNull.Value)
                    o.t001_idficepi_interesado = int.Parse(dr["t001_idficepi_interesado"].ToString());
                if (dr["Interesado"] != DBNull.Value)
                    o.Interesado = (string)dr["Interesado"];
                if (dr["t423_idmotivo"] != DBNull.Value)
                    o.t423_idmotivo = byte.Parse(dr["t423_idmotivo"].ToString());
                if (dr["t422_idmoneda"] != DBNull.Value)
                    o.t422_idmoneda = (string)dr["t422_idmoneda"];
                if (dr["t422_denominacion"] != DBNull.Value)
                    o.t422_denominacion = (string)dr["t422_denominacion"];
                if (dr["t423_denominacion"] != DBNull.Value)
                    o.t423_denominacion = (string)dr["t423_denominacion"];
                
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = int.Parse(dr["t305_idproyectosubnodo"].ToString());
                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.t301_idproyecto = int.Parse(dr["t301_idproyecto"].ToString());
                if (dr["t301_denominacion"] != DBNull.Value)
                    o.t301_denominacion = (string)dr["t301_denominacion"];

                if (dr["t420_comentarionota"] != DBNull.Value)
                    o.t420_comentarionota = (string)dr["t420_comentarionota"];
                if (dr["t420_anotaciones"] != DBNull.Value)
                    o.t420_anotaciones = (string)dr["t420_anotaciones"];
                if (dr["t313_idempresa"] != DBNull.Value)
                    o.t313_idempresa = int.Parse(dr["t313_idempresa"].ToString());
                if (dr["t313_denominacion"] != DBNull.Value)
                    o.t313_denominacion = (string)dr["t313_denominacion"];
                if (dr["t007_idterrfis"] != DBNull.Value)
                    o.t007_idterrfis = byte.Parse(dr["t007_idterrfis"].ToString());
                if (dr["t655_idBono"] != DBNull.Value)
                    o.t655_idBono = int.Parse(dr["t655_idBono"].ToString());
                if (dr["t655_denominacion"] != DBNull.Value)
                    o.t655_denominacion = (string)dr["t655_denominacion"];
                if (dr["t420_importe"] != DBNull.Value)
                    o.t420_importe = decimal.Parse(dr["t420_importe"].ToString());
                if (dr["t420_anomesbono"] != DBNull.Value)
                    o.t420_anomesbono = int.Parse(dr["t420_anomesbono"].ToString());
                if (dr["t420_fcontabilizacion"] != DBNull.Value)
                    o.t420_fcontabilizacion = (DateTime)dr["t420_fcontabilizacion"];
                if (dr["t420_tipocambio"] != DBNull.Value)
                    o.t420_tipocambio = float.Parse(dr["t420_tipocambio"].ToString());
                if (dr["t001_sexo_interesado"] != DBNull.Value)
                    o.t001_sexo_interesado = (string)dr["t001_sexo_interesado"];

                if (dr["t175_idcc_solicitud"] != DBNull.Value)
                    o.t175_idcc_solicitud = (string)dr["t175_idcc_solicitud"];
                if (dr["t175_denominacion_solicitud"] != DBNull.Value)
                    o.t175_denominacion_solicitud = (string)dr["t175_denominacion_solicitud"];
                if (dr["t303_idnodo_solicitud"] != DBNull.Value)
                    o.t303_idnodo_solicitud = int.Parse(dr["t303_idnodo_solicitud"].ToString());
                if (dr["t303_denominacion_solicitud"] != DBNull.Value)
                    o.t303_denominacion_solicitud = (string)dr["t303_denominacion_solicitud"];
                if (dr["t303_idnodo_beneficiario"] != DBNull.Value)
                    o.t303_idnodo_beneficiario = int.Parse(dr["t303_idnodo_beneficiario"].ToString());
                if (dr["t303_denominacion_beneficiario"] != DBNull.Value)
                    o.t303_denominacion_beneficiario = (string)dr["t303_denominacion_beneficiario"];
                if (dr["t010_idoficina"] != DBNull.Value)
                    o.t010_idoficina = (short)dr["t010_idoficina"];
                if (dr["t010_desoficina"] != DBNull.Value)
                    o.t010_desoficina = (string)dr["t010_desoficina"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de CABECERAGV"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static CABECERAGV ObtenerDatosCabeceraPago(int t420_idreferencia)
        {
            CABECERAGV o = new CABECERAGV();

            if (t420_idreferencia == 0) return o;

            SqlDataReader dr = DAL.CABECERAGV.ObtenerDatosCabeceraPago(null, t420_idreferencia);

            if (dr.Read())
            {
                if (dr["t420_idreferencia"] != DBNull.Value)
                    o.t420_idreferencia = int.Parse(dr["t420_idreferencia"].ToString());
                if (dr["t431_idestado"] != DBNull.Value)
                    o.t431_idestado = (string)dr["t431_idestado"];
                if (dr["t431_denominacion"] != DBNull.Value)
                    o.t431_denominacion = (string)dr["t431_denominacion"];
                if (dr["t420_concepto"] != DBNull.Value)
                    o.t420_concepto = (string)dr["t420_concepto"];
                if (dr["t001_idficepi_solicitante"] != DBNull.Value)
                    o.t001_idficepi_solicitante = int.Parse(dr["t001_idficepi_solicitante"].ToString());
                if (dr["Solicitante"] != DBNull.Value)
                    o.Solicitante = (string)dr["Solicitante"];
                if (dr["t001_idficepi_interesado"] != DBNull.Value)
                    o.t001_idficepi_interesado = int.Parse(dr["t001_idficepi_interesado"].ToString());
                if (dr["t314_idusuario_interesado"] != DBNull.Value)
                    o.t314_idusuario_interesado = int.Parse(dr["t314_idusuario_interesado"].ToString());
                if (dr["Interesado"] != DBNull.Value)
                    o.Interesado = (string)dr["Interesado"];
                if (dr["t175_idcc"] != DBNull.Value)
                    o.t175_idcc = (string)dr["t175_idcc"];
                if (dr["t423_idmotivo"] != DBNull.Value)
                    o.t423_idmotivo = byte.Parse(dr["t423_idmotivo"].ToString());
                if (dr["t423_denominacion"] != DBNull.Value)
                    o.t423_denominacion = (string)dr["t423_denominacion"];

                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = int.Parse(dr["t305_idproyectosubnodo"].ToString());
                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.t301_idproyecto = int.Parse(dr["t301_idproyecto"].ToString());
                if (dr["t301_denominacion"] != DBNull.Value)
                    o.t301_denominacion = (string)dr["t301_denominacion"];

                if (dr["t420_comentarionota"] != DBNull.Value)
                    o.t420_comentarionota = (string)dr["t420_comentarionota"];
                if (dr["t420_anotaciones"] != DBNull.Value)
                    o.t420_anotaciones = (string)dr["t420_anotaciones"];
                if (dr["t313_idempresa"] != DBNull.Value)
                    o.t313_idempresa = int.Parse(dr["t313_idempresa"].ToString());
                if (dr["t313_denominacion"] != DBNull.Value)
                    o.t313_denominacion = (string)dr["t313_denominacion"];
                if (dr["t007_idterrfis"] != DBNull.Value)
                    o.t007_idterrfis = byte.Parse(dr["t007_idterrfis"].ToString());
                if (dr["t666_idacuerdogv"] != DBNull.Value)
                    o.t666_idacuerdogv = int.Parse(dr["t666_idacuerdogv"].ToString());
                if (dr["t666_denominacion"] != DBNull.Value)
                    o.t666_denominacion = (string)dr["t666_denominacion"];
                if (dr["t420_importe"] != DBNull.Value)
                    o.t420_importe = decimal.Parse(dr["t420_importe"].ToString());
                if (dr["t420_fcontabilizacion"] != DBNull.Value)
                    o.t420_fcontabilizacion = (DateTime)dr["t420_fcontabilizacion"];
                if (dr["t422_idmoneda"] != DBNull.Value)
                    o.t422_idmoneda = (string)dr["t422_idmoneda"];
                if (dr["t422_denominacion"] != DBNull.Value)
                    o.t422_denominacion = (string)dr["t422_denominacion"];
                if (dr["t420_tipocambio"] != DBNull.Value)
                    o.t420_tipocambio = float.Parse(dr["t420_tipocambio"].ToString());
                if (dr["t001_sexo_interesado"] != DBNull.Value)
                    o.t001_sexo_interesado = (string)dr["t001_sexo_interesado"];

                if (dr["t175_idcc_solicitud"] != DBNull.Value)
                    o.t175_idcc_solicitud = (string)dr["t175_idcc_solicitud"];
                if (dr["t175_denominacion_solicitud"] != DBNull.Value)
                    o.t175_denominacion_solicitud = (string)dr["t175_denominacion_solicitud"];
                if (dr["t303_idnodo_solicitud"] != DBNull.Value)
                    o.t303_idnodo_solicitud = int.Parse(dr["t303_idnodo_solicitud"].ToString());
                if (dr["t303_denominacion_solicitud"] != DBNull.Value)
                    o.t303_denominacion_solicitud = (string)dr["t303_denominacion_solicitud"];
                if (dr["t303_idnodo_beneficiario"] != DBNull.Value)
                    o.t303_idnodo_beneficiario = int.Parse(dr["t303_idnodo_beneficiario"].ToString());
                if (dr["t303_denominacion_beneficiario"] != DBNull.Value)
                    o.t303_denominacion_beneficiario = (string)dr["t303_denominacion_beneficiario"];
                if (dr["t422_idmoneda_acuerdo"] != DBNull.Value)
                    o.t422_idmoneda_acuerdo = (string)dr["t422_idmoneda_acuerdo"];
                if (dr["t010_idoficina"] != DBNull.Value)
                    o.t010_idoficina = (short)dr["t010_idoficina"];
                if (dr["t010_desoficina"] != DBNull.Value)
                    o.t010_desoficina = (string)dr["t010_desoficina"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de CABECERAGV"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static CABECERAGV ObtenerDatosCambioEstado(int t420_idreferencia)
        {
            CABECERAGV o = new CABECERAGV();

            if (t420_idreferencia == 0) return o;

            SqlDataReader dr = DAL.CABECERAGV.ObtenerDatosCambioEstado(null, t420_idreferencia);

            if (dr.Read())
            {
                if (dr["t420_idreferencia"] != DBNull.Value)
                    o.t420_idreferencia = int.Parse(dr["t420_idreferencia"].ToString());
                if (dr["t431_idestado"] != DBNull.Value)
                    o.t431_idestado = (string)dr["t431_idestado"];
                if (dr["t431_denominacion"] != DBNull.Value)
                    o.t431_denominacion = (string)dr["t431_denominacion"];
                if (dr["t420_concepto"] != DBNull.Value)
                    o.t420_concepto = (string)dr["t420_concepto"];
                if (dr["t001_idficepi_interesado"] != DBNull.Value)
                    o.t001_idficepi_interesado = int.Parse(dr["t001_idficepi_interesado"].ToString());
                if (dr["t314_idusuario_interesado"] != DBNull.Value)
                    o.t314_idusuario_interesado = int.Parse(dr["t314_idusuario_interesado"].ToString());
                if (dr["Interesado"] != DBNull.Value)
                    o.Interesado = (string)dr["Interesado"];
                if (dr["t423_idmotivo"] != DBNull.Value)
                    o.t423_idmotivo = byte.Parse(dr["t423_idmotivo"].ToString());
                if (dr["t423_denominacion"] != DBNull.Value)
                    o.t423_denominacion = (string)dr["t423_denominacion"];
                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.t301_idproyecto = int.Parse(dr["t301_idproyecto"].ToString());
                if (dr["t301_denominacion"] != DBNull.Value)
                    o.t301_denominacion = (string)dr["t301_denominacion"];
                if (dr["t422_idmoneda"] != DBNull.Value)
                    o.t422_idmoneda = (string)dr["t422_idmoneda"];
                if (dr["t422_denominacion"] != DBNull.Value)
                    o.t422_denominacion = (string)dr["t422_denominacion"];
                if (dr["t313_idempresa"] != DBNull.Value)
                    o.t313_idempresa = int.Parse(dr["t313_idempresa"].ToString());
                if (dr["t313_denominacion"] != DBNull.Value)
                    o.t313_denominacion = (string)dr["t313_denominacion"];
                if (dr["t010_idoficina"] != DBNull.Value)
                    o.t010_idoficina = (short)dr["t010_idoficina"];
                if (dr["t010_desoficina"] != DBNull.Value)
                    o.t010_desoficina = (string)dr["t010_desoficina"];
                if (dr["TipoNota"] != DBNull.Value)
                    o.TipoNota = (string)dr["TipoNota"];
                if (dr["TOTALVIAJE"] != DBNull.Value)
                    o.TOTALVIAJE = decimal.Parse(dr["TOTALVIAJE"].ToString());
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de CABECERAGV"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static string TramitarPagoConcertado(string strDatos)
        {
            string sResul = "";
            int nReferencia = 0, nFilasModificadas = 0;
            SqlConnection oConn = null;
            SqlTransaction tr = null;
            bool bErrorControlado = false;

            string[] aDatosCabecera = Regex.Split(strDatos, "#sCad#");
            #region Datos Cabecera

            ///aDatosCabecera[0] = hdnEstado //0
            ///aDatosCabecera[1] = Concepto //1
            ///aDatosCabecera[2] = hdnIdAcuerdoGV //2
            ///aDatosCabecera[3] = hdnIdProyectoSubNodo //3
            ///aDatosCabecera[4] = hdnInteresado //4
            ///aDatosCabecera[5] = txtObservaciones //5
            ///aDatosCabecera[6] = hdnAnotacionesPersonales //6
            ///aDatosCabecera[7] = hdnImporte //7
            ///aDatosCabecera[8] = cboMotivo //8
            ///aDatosCabecera[9] = cboMoneda //9
            ///aDatosCabecera[10] = hdnIDEmpresa //10
            ///aDatosCabecera[11] = hdnEstadoAnterior //11
            ///aDatosCabecera[12] = hdnIdTerritorio //12
            ///aDatosCabecera[13] = hdnReferencia //13
            ///aDatosCabecera[14] = hdnCentroCoste //14
            ///aDatosCabecera[15] = hdnOficinaLiquidadora //15
            
            #endregion Datos Posiciones

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
                    #region Tramitar Cabecera
                    if (aDatosCabecera[13] == "")
                    {
                        nReferencia = DAL.CABECERAGV.InsertarCabeceraPago(tr,
                                                    aDatosCabecera[0],//estado
                                                    aDatosCabecera[1],//concepto
                                                    (int)HttpContext.Current.Session["GVT_IDFICEPI"],//idficepi_solicitante
                                                    int.Parse(aDatosCabecera[4]),//idusuario_interesado
                                                    (aDatosCabecera[3] == "") ? null : (int?)int.Parse(aDatosCabecera[3]),//idproyectosubnodo
                                                    aDatosCabecera[9],//moneda
                                                    Utilidades.unescape(aDatosCabecera[5]),//comentarionota
                                                    Utilidades.unescape(aDatosCabecera[6]),//anotaciones
                                                    decimal.Parse(aDatosCabecera[7]),//importe
                                                    byte.Parse(aDatosCabecera[8]),//motivo
                                                    (aDatosCabecera[2] == "") ? null : (int?)int.Parse(aDatosCabecera[2]),//idacuerdogv
                                                    int.Parse(aDatosCabecera[10]), //idempresa
                                                    byte.Parse(aDatosCabecera[12]), // Territorio
                                                    (aDatosCabecera[14] == "") ? null : aDatosCabecera[14],
                                                    short.Parse(aDatosCabecera[15])
                                                    );
                    }
                    else
                    {
                        nReferencia = int.Parse(aDatosCabecera[13]);
                        nFilasModificadas = DAL.CABECERAGV.ModificarCabeceraPago(tr,
                                                    nReferencia,
                                                    aDatosCabecera[0],//estado
                                                    aDatosCabecera[1],//concepto
                                                    (int)HttpContext.Current.Session["GVT_IDFICEPI"],//idficepi_solicitante
                                                    int.Parse(aDatosCabecera[4]),//idusuario_interesado
                                                    (aDatosCabecera[3] == "") ? null : (int?)int.Parse(aDatosCabecera[3]),//idproyectosubnodo
                                                    aDatosCabecera[9],//moneda
                                                    Utilidades.unescape(aDatosCabecera[5]),//comentarionota
                                                    Utilidades.unescape(aDatosCabecera[6]),//anotaciones
                                                    decimal.Parse(aDatosCabecera[7]),//importe
                                                    byte.Parse(aDatosCabecera[8]),//motivo
                                                    (aDatosCabecera[2] == "") ? null : (int?)int.Parse(aDatosCabecera[2]),//idacuerdogv
                                                    int.Parse(aDatosCabecera[10]), //idempresa
                                                    byte.Parse(aDatosCabecera[12]), // Territorio
                                                    (aDatosCabecera[14] == "") ? null : aDatosCabecera[14],
                                                    short.Parse(aDatosCabecera[15])
                                                    );
                        if (nFilasModificadas == 0)
                        {
                            sResul = "Tramitacion anulada";
                            bErrorControlado = true;
                            throw (new Exception(sResul));
                        }
                        //string sCentroCoste = DAL.CABECERAGV.ObtenerCentroCoste(tr, nReferencia);
                        //if (sCentroCoste == "")
                        //{
                        //    sResul = "Operación denegada.\n\nNo se ha podido determinar el centro de coste a asociar a la solicitud.";
                        //    bErrorControlado = true;
                        //    throw (new Exception(sResul));
                        //}
                        //else
                        //{
                        //    DAL.CABECERAGV.UpdateCentroCoste(tr, nReferencia, sCentroCoste);
                        //}
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    if (bErrorControlado) throw (new Exception(ex.Message));
                    else throw (new Exception("Error al grabar la cabecera. " + ex.Message));
                }

                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                if (bErrorControlado) sResul = ex.Message;
                else sResul = Errores.mostrarError("Error al tramitar el pago concertado.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                {
                    if (bErrorControlado) sResul = "ErrorControlado##EC##" + sResul;
                    throw (new Exception(sResul));
                }
            }

            return nReferencia.ToString();
        }

        public static string TramitarNotaEstandar(string strDatosCabecera, string strDatosPosiciones)
        {
            string sResul = "";
            int nReferencia = 0, nFilasModificadas = 0;
            SqlConnection oConn = null;
            SqlTransaction tr = null;
            bool bErrorControlado = false, bExisteNota = false;

            string[] aDatosCabecera = Regex.Split(strDatosCabecera, "#sep#");
            if (aDatosCabecera.Length == 0)
            {
                sResul = "Cabecera sin datos";
                bErrorControlado = true;
                throw (new Exception(sResul));
            }
            #region Datos Cabecera
            ///aDatosCabecera[0] = hdnEstado //0
            ///aDatosCabecera[1] = txtConcepto //1
            ///aDatosCabecera[2] = hdnInteresado //2
            ///aDatosCabecera[3] = cboMotivo //3
            ///aDatosCabecera[4] = rdbJustificantes //4
            ///aDatosCabecera[5] = hdnIdProyectoSubNodo //5
            ///aDatosCabecera[6] = cboMoneda //6
            ///aDatosCabecera[7] = txtObservacionesNota //7
            ///aDatosCabecera[8] = hdnAnotacionesPersonales //8
            ///aDatosCabecera[9] = txtImpAnticipo //9
            ///aDatosCabecera[10] = txtFecAnticipo //10
            ///aDatosCabecera[11] = txtOficinaAnticipo //11
            ///aDatosCabecera[12] = txtImpDevolucion //12
            ///aDatosCabecera[13] = txtFecDevolucion //13
            ///aDatosCabecera[14] = txtOficinaDevolucion //14
            ///aDatosCabecera[15] = txtAclaracionesAnticipos //15
            ///aDatosCabecera[16] = txtPagadoTransporte //16
            ///aDatosCabecera[17] = txtPagadoHotel //17
            ///aDatosCabecera[18] = txtPagadoOtros //18
            ///aDatosCabecera[19] = txtAclaracionesPagado //19
            ///aDatosCabecera[20] = hdnIDEmpresa //20
            ///aDatosCabecera[21] = hdnIDTerritorio //21
            ///aDatosCabecera[22] = cldKMCO //22
            ///aDatosCabecera[23] = cldDCCO //23
            ///aDatosCabecera[24] = cldMDCO //24
            ///aDatosCabecera[25] = cldDECO //25
            ///aDatosCabecera[26] = cldDACO //26
            ///aDatosCabecera[27] = cldKMEX //27
            ///aDatosCabecera[28] = cldDCEX //28
            ///aDatosCabecera[29] = cldMDEX //29
            ///aDatosCabecera[30] = cldDEEX //30
            ///aDatosCabecera[31] = cldDAEX //31
            ///aDatosCabecera[32] = hdnOficinaLiquidadora //32
            ///aDatosCabecera[33] = hdnReferencia //33
            ///aDatosCabecera[34] = hdnEstadoAnterior //34
            ///aDatosCabecera[35] = hdnAutorresponsable //35
            ///aDatosCabecera[36] = txtProyecto //36
            ///aDatosCabecera[37] = hdnCentroCoste //37
            
            #endregion Datos Posiciones
            string[] aPosiciones = Regex.Split(strDatosPosiciones, "#reg#");
            if (aPosiciones.Length <= 1)
            {
                sResul = "Cabecera sin posiciones de gastos especificados";
                bErrorControlado = true;
                throw (new Exception(sResul));
            }
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
                    #region Tramitar Cabecera
                    if (aDatosCabecera[34] == "P") //Si el estado anterior es aparcada.
                    {
                        nFilasModificadas = DAL.CABECERAAPARCADA_NEGV.EliminarNotaAparcada(tr, int.Parse(aDatosCabecera[33]));
                        if (nFilasModificadas == 0)
                        {
                            sResul = "Solicitud aparcada no existente";
                            bErrorControlado = true;
                            throw (new Exception(sResul));
                        }
                    }

                    if (aDatosCabecera[34] == "" || aDatosCabecera[34] == "P")
                    {
                        nReferencia = DAL.CABECERAGV.InsertarCabecera(tr,
                                                    aDatosCabecera[0],
                                                    Utilidades.unescape(aDatosCabecera[1]),
                                                    (int)HttpContext.Current.Session["GVT_IDFICEPI"],
                                                    int.Parse(aDatosCabecera[2]),
                                                    byte.Parse(aDatosCabecera[3]),
                                                    (aDatosCabecera[4] == "1") ? true : false,
                                                    (aDatosCabecera[5] == "") ? null : (int?)int.Parse(aDatosCabecera[5]),
                                                    aDatosCabecera[6],
                                                    Utilidades.unescape(aDatosCabecera[7]),
                                                    Utilidades.unescape(aDatosCabecera[8]),
                                                    decimal.Parse(aDatosCabecera[9]),
                                                    (aDatosCabecera[10] == "") ? null : (DateTime?)DateTime.Parse(aDatosCabecera[10]),
                                                    Utilidades.unescape(aDatosCabecera[11]),
                                                    decimal.Parse(aDatosCabecera[12]),
                                                    (aDatosCabecera[13] == "") ? null : (DateTime?)DateTime.Parse(aDatosCabecera[13]),
                                                    Utilidades.unescape(aDatosCabecera[14]),
                                                    Utilidades.unescape(aDatosCabecera[15]),
                                                    decimal.Parse(aDatosCabecera[16]),
                                                    decimal.Parse(aDatosCabecera[17]),
                                                    decimal.Parse(aDatosCabecera[18]),
                                                    Utilidades.unescape(aDatosCabecera[19]),
                                                    int.Parse(aDatosCabecera[20]),
                                                    byte.Parse(aDatosCabecera[21]),
                                                    decimal.Parse(aDatosCabecera[23]),
                                                    decimal.Parse(aDatosCabecera[24]),
                                                    decimal.Parse(aDatosCabecera[26]),
                                                    decimal.Parse(aDatosCabecera[22]),
                                                    decimal.Parse(aDatosCabecera[25]),
                                                    decimal.Parse(aDatosCabecera[28]),
                                                    decimal.Parse(aDatosCabecera[29]),
                                                    decimal.Parse(aDatosCabecera[31]),
                                                    decimal.Parse(aDatosCabecera[27]),
                                                    decimal.Parse(aDatosCabecera[30]),
                                                    short.Parse(aDatosCabecera[32]),
                                                    null,
                                                    (aDatosCabecera[37] == "") ? null : aDatosCabecera[37]
                                                    );
                    }
                    else
                    {
                        bExisteNota = true;
                        nReferencia = int.Parse(aDatosCabecera[33]);
                        nFilasModificadas = DAL.CABECERAGV.ModificarCabecera(tr, nReferencia,
                                                    aDatosCabecera[0],
                                                    Utilidades.unescape(aDatosCabecera[1]),
                                                    (int)HttpContext.Current.Session["GVT_IDFICEPI"],
                                                    int.Parse(aDatosCabecera[2]),
                                                    byte.Parse(aDatosCabecera[3]),
                                                    (aDatosCabecera[4] == "1") ? true : false,
                                                    (aDatosCabecera[5] == "") ? null : (int?)int.Parse(aDatosCabecera[5]),
                                                    aDatosCabecera[6],
                                                    Utilidades.unescape(aDatosCabecera[7]),
                                                    Utilidades.unescape(aDatosCabecera[8]),
                                                    decimal.Parse(aDatosCabecera[9]),
                                                    (aDatosCabecera[10] == "") ? null : (DateTime?)DateTime.Parse(aDatosCabecera[10]),
                                                    Utilidades.unescape(aDatosCabecera[11]),
                                                    decimal.Parse(aDatosCabecera[12]),
                                                    (aDatosCabecera[13] == "") ? null : (DateTime?)DateTime.Parse(aDatosCabecera[13]),
                                                    Utilidades.unescape(aDatosCabecera[14]),
                                                    Utilidades.unescape(aDatosCabecera[15]),
                                                    decimal.Parse(aDatosCabecera[16]),
                                                    decimal.Parse(aDatosCabecera[17]),
                                                    decimal.Parse(aDatosCabecera[18]),
                                                    Utilidades.unescape(aDatosCabecera[19]),
                                                    int.Parse(aDatosCabecera[20]),
                                                    byte.Parse(aDatosCabecera[21]),
                                                    decimal.Parse(aDatosCabecera[23]),
                                                    decimal.Parse(aDatosCabecera[24]),
                                                    decimal.Parse(aDatosCabecera[26]),
                                                    decimal.Parse(aDatosCabecera[22]),
                                                    decimal.Parse(aDatosCabecera[25]),
                                                    decimal.Parse(aDatosCabecera[28]),
                                                    decimal.Parse(aDatosCabecera[29]),
                                                    decimal.Parse(aDatosCabecera[31]),
                                                    decimal.Parse(aDatosCabecera[27]),
                                                    decimal.Parse(aDatosCabecera[30]),
                                                    short.Parse(aDatosCabecera[32]),
                                                    (aDatosCabecera[37] == "") ? null : aDatosCabecera[37]
                                                    );
                        if (nFilasModificadas == 0)
                        {
                            sResul = "Tramitacion anulada";
                            bErrorControlado = true;
                            throw (new Exception(sResul));
                        }
                        //string sCentroCoste = DAL.CABECERAGV.ObtenerCentroCoste(tr, nReferencia);
                        //if (sCentroCoste == "")
                        //{
                        //    sResul = "Operación denegada.\n\nNo se ha podido determinar el centro de coste a asociar a la solicitud.";
                        //    bErrorControlado = true;
                        //    throw (new Exception(sResul));
                        //}
                        //else
                        //{
                        //    DAL.CABECERAGV.UpdateCentroCoste(tr, nReferencia, sCentroCoste);
                        //}
                    }

                    //Si el beneficiario es autorresponsable, comprobaciones de motivos, excepciones, etc, para
                    //pasar la nota a aprobada.
                    if (aDatosCabecera[35] == "1")
                    {
                        DAL.CABECERAGV.GestionarAutorresponsabilidad(tr, nReferencia);
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    if (bErrorControlado) throw (new Exception(ex.Message));
                    else throw (new Exception("Error al grabar la cabecera. " + ex.Message));
                }


                try
                {
                    if (bExisteNota)
                    {
                        DAL.POSICIONGV.DeleteByT420_idreferencia(tr, nReferencia);
                    }

                    #region Insertar Posiciones

                    Hashtable htAnnoGasto = AnnoGasto.ObtenerHTAnnoGasto(tr);
                    Hashtable htUsuarioPSN = USUARIOPROYECTOSUBNODO.ObtenerFechasAsociacionPSN(tr, int.Parse(aDatosCabecera[2]));

                    foreach (string oPosicion in aPosiciones)
                    {
                        if (oPosicion == "") continue;
                        string[] aDatosPosicion = Regex.Split(oPosicion, "#sep#");

                        DateTime oDesde = DateTime.Parse(aDatosPosicion[0]);
                        DateTime oHasta = DateTime.Parse(aDatosPosicion[1]);
                        DateTime oAuxiliar = oDesde;
                        DateTime oDesdePSN;
                        DateTime? oHastaPSN = null;
                        do
                        {
                            if ((DateTime[])htAnnoGasto[oAuxiliar.Year] == null)
                            {
                                bErrorControlado = true;
                                throw (new Exception("Tramitación denegada.\n\nNo se pueden tramitar gastos correspondientes al año " + oDesde.Year.ToString()));
                            }
                            else
                            {
                                if (oAuxiliar < ((DateTime[])htAnnoGasto[oAuxiliar.Year])[0])
                                {
                                    bErrorControlado = true;
                                    throw (new Exception("Tramitación denegada.\n\nHasta el día " + ((DateTime[])htAnnoGasto[oAuxiliar.Year])[0].ToShortDateString() + " no se permite tramitar gastos correspondientes al año " + oDesde.Year.ToString()));
                                }
                                else if (oAuxiliar > ((DateTime[])htAnnoGasto[oAuxiliar.Year])[1])
                                {
                                    bErrorControlado = true;
                                    throw (new Exception("Tramitación denegada.\n\nSe ha superado la fecha límite establecida para tramitar gastos correspondientes al año " + oDesde.Year.ToString() + " (" + ((DateTime[])htAnnoGasto[oAuxiliar.Year])[1].ToShortDateString() + ")"));
                                }
                            }

                            if (byte.Parse(aDatosCabecera[3]) == 1) //Si motivo proyecto
                            {
                                if ((DateTime?[])htUsuarioPSN[int.Parse(aDatosCabecera[5])] == null)
                                {
                                    bErrorControlado = true;
                                    throw (new Exception("Tramitación denegada.\n\nEl beneficiario no se encuentra asociado al proyecto."));
                                }
                                else
                                {
                                    oDesdePSN = (DateTime)((DateTime?[])htUsuarioPSN[int.Parse(aDatosCabecera[5])])[0];
                                    oHastaPSN = ((DateTime?[])htUsuarioPSN[int.Parse(aDatosCabecera[5])])[1];

                                    if (oAuxiliar < oDesdePSN || (oHastaPSN != null && oAuxiliar > oHastaPSN))
                                    {
                                        bErrorControlado = true;
                                        throw (new Exception("Tramitación denegada.\n\nEl rango de fechas de la imputación (" + oDesde.ToShortDateString() + " - " + oHasta.ToShortDateString() + ") se encuentra fuera del rango de fechas de asociación al proyecto \"" + aDatosCabecera[36] + "\" (" + oDesdePSN.ToShortDateString() + " - " + ((oHastaPSN!=null)?((DateTime)oHastaPSN).ToShortDateString():"") + ")"));
                                    }
                                }
                            }

                            oAuxiliar = oAuxiliar.AddDays(1);
                        } while (oAuxiliar <= oHasta);

                        //Comprobar que si se indica un desplazamiento ECO, se corresponda con un VUP
                        if (aDatosPosicion[9] != "")
                        {
                            if (!DAL.POSICIONGV.EsDesplazamientoECOenVUP(tr, int.Parse(aDatosPosicion[9])))
                            {
                                bErrorControlado = true;
                                throw (new Exception("Tramitación denegada.\n\nEl desplazamiento correspondiente a las fechas (" + oDesde.ToShortDateString() + " - " + oHasta.ToShortDateString() + ") con destino \"" + Utilidades.unescape(aDatosPosicion[2]) + "\" se ha realizado en un coche de flota, por lo que no procede asociarlo a una solicitud GASVI."));
                            }
                        }


                        DAL.POSICIONGV.InsertarPosicion(tr, nReferencia,
                                                DateTime.Parse(aDatosPosicion[0]),
                                                DateTime.Parse(aDatosPosicion[1]),
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
                    if (bErrorControlado) throw (new Exception(ex.Message));
                    else throw (new Exception("Error al grabar las posiciones. " + ex.Message));
                }

                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                if (bErrorControlado) sResul = ex.Message;
                else sResul = Errores.mostrarError("Error al tramitar la nota estándar.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                {
                    if (bErrorControlado) sResul = "ErrorControlado##EC##" + sResul;
                    throw (new Exception(sResul));
                }
            }

            return nReferencia.ToString();
        }

        public static string TramitarNotaMultiProyecto(string strDatosCabecera, string strDatosPosiciones, string sProyectosSubnodos)
        {
            string sResul = "";
            int nReferencia = 0, nFilasModificadas = 0;
            int? nIDLote = null;
            SqlConnection oConn = null;
            SqlTransaction tr = null;
            bool bErrorControlado = false;

            string[] aPSN = Regex.Split(sProyectosSubnodos, ",");

            string[] aDatosCabecera = Regex.Split(strDatosCabecera, "#sep#");
            if (aDatosCabecera.Length == 0)
            {
                sResul = "Cabecera sin datos";
                bErrorControlado = true;
                throw (new Exception(sResul));
            }
            #region Datos Cabecera
            ///aDatosCabecera[0] = hdnEstado //0
            ///aDatosCabecera[1] = txtConcepto //1
            ///aDatosCabecera[2] = hdnInteresado //2
            ///aDatosCabecera[3] = rdbJustificantes //
            ///aDatosCabecera[4] = cboMoneda //
            ///aDatosCabecera[5] = txtObservacionesNota //
            ///aDatosCabecera[6] = hdnAnotacionesPersonales //
            ///aDatosCabecera[7] = hdnIDEmpresa //
            ///aDatosCabecera[8] = hdnIDTerritorio //
            ///aDatosCabecera[9] = cldKMCO //
            ///aDatosCabecera[10] = cldDCCO //
            ///aDatosCabecera[11] = cldMDCO //
            ///aDatosCabecera[12] = cldDECO //
            ///aDatosCabecera[13] = cldDACO //
            ///aDatosCabecera[14] = cldKMEX //
            ///aDatosCabecera[15] = cldDCEX //
            ///aDatosCabecera[16] = cldMDEX //
            ///aDatosCabecera[17] = cldDEEX //
            ///aDatosCabecera[18] = cldDAEX //
            ///aDatosCabecera[19] = hdnOficinaLiquidadora //
            ///aDatosCabecera[20] = hdnReferencia //
            ///aDatosCabecera[21] = hdnEstadoAnterior //

            #endregion Datos Posiciones
            string[] aPosiciones = Regex.Split(strDatosPosiciones, "#reg#");
            if (aPosiciones.Length<=1)
            {
                sResul = "Cabecera sin posiciones de gastos especificados";
                bErrorControlado = true;
                throw (new Exception(sResul));
            }
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
            ///aPosiciones[14] = ID ProyectoSubnodo
            ///aPosiciones[15] = Nº Proyecto

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
                if (aDatosCabecera[21] == "P") //Si el estado anterior es aparcada.
                {
                    nFilasModificadas = DAL.CABECERAAPARCADA_NMPGV.EliminarNotaAparcada(tr, int.Parse(aDatosCabecera[20]));
                    if (nFilasModificadas == 0)
                    {
                        sResul = "Solicitud aparcada no existente";
                        bErrorControlado = true;
                        throw (new Exception(sResul));
                    }
                }

                Hashtable htAnnoGasto = AnnoGasto.ObtenerHTAnnoGasto(tr);
                Hashtable htUsuarioPSN = USUARIOPROYECTOSUBNODO.ObtenerFechasAsociacionPSN(tr, int.Parse(aDatosCabecera[2]));

                foreach (string sPSN in aPSN)
                {
                    try
                    {
                        #region Insertar Cabecera
                        nReferencia = DAL.CABECERAGV.InsertarCabecera(tr,
                                            aDatosCabecera[0],
                                            Utilidades.unescape(aDatosCabecera[1]),
                                            (int)HttpContext.Current.Session["GVT_IDFICEPI"],
                                            int.Parse(aDatosCabecera[2]),
                                            1, //Proyecto
                                            (aDatosCabecera[3] == "1") ? true : false,
                                            (int?)int.Parse(sPSN),
                                            aDatosCabecera[4],
                                            Utilidades.unescape(aDatosCabecera[5]),
                                            Utilidades.unescape(aDatosCabecera[6]),
                                            0, //anticipo
                                            null, //fecha anticipo,
                                            "", //lugar anticipo,
                                            0, //devolución
                                            null, //fecha devolución,
                                            "", //lugar devolución,
                                            "", //aclaraciones anticipo
                                            0, //pagado transporte
                                            0,//pagado hotel
                                            0,//pagado otros
                                            "", //aclaraciones pagado 
                                            int.Parse(aDatosCabecera[7]),
                                            byte.Parse(aDatosCabecera[8]),
                                            decimal.Parse(aDatosCabecera[10]),
                                            decimal.Parse(aDatosCabecera[11]),
                                            decimal.Parse(aDatosCabecera[13]),
                                            decimal.Parse(aDatosCabecera[9]),
                                            decimal.Parse(aDatosCabecera[12]),
                                            decimal.Parse(aDatosCabecera[15]),
                                            decimal.Parse(aDatosCabecera[16]),
                                            decimal.Parse(aDatosCabecera[18]),
                                            decimal.Parse(aDatosCabecera[14]),
                                            decimal.Parse(aDatosCabecera[17]),
                                            short.Parse(aDatosCabecera[19]),
                                            nIDLote,
                                            null
                                            );


                        if (nIDLote == null && aPSN.Length > 1)
                        {
                            nIDLote = nReferencia;
                            DAL.CABECERAGV.UpdateLote(tr, nReferencia, (int)nIDLote);
                        }

                        //string sCentroCoste = DAL.CABECERAGV.ObtenerCentroCoste(tr, nReferencia);
                        //if (sCentroCoste == "")
                        //{
                        //    sResul = "Operación denegada.\n\nNo se ha podido determinar el centro de coste a asociar a la solicitud.";
                        //    bErrorControlado = true;
                        //    throw (new Exception(sResul));
                        //}
                        //else
                        //{
                        //    DAL.CABECERAGV.UpdateCentroCoste(tr, nReferencia, sCentroCoste);
                        //}
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        if (bErrorControlado) throw (new Exception(ex.Message));
                        else throw (new Exception("Error al grabar la cabecera. " + ex.Message));
                    }


                    try
                    {
                        #region Insertar Posiciones
                        foreach (string oPosicion in aPosiciones)
                        {
                            if (oPosicion == "") continue;
                            string[] aDatosPosicion = Regex.Split(oPosicion, "#sep#");

                            if (aDatosPosicion[14] != sPSN) continue;

                            DateTime oDesde = DateTime.Parse(aDatosPosicion[0]);
                            DateTime oHasta = DateTime.Parse(aDatosPosicion[1]);
                            DateTime oAuxiliar = oDesde;
                            DateTime oDesdePSN;
                            DateTime? oHastaPSN = null;
                            do
                            {
                                if ((DateTime[])htAnnoGasto[oAuxiliar.Year] == null)
                                {
                                    bErrorControlado = true;
                                    throw (new Exception("Tramitación denegada.\n\nNo se pueden tramitar gastos correspondientes al año " + oDesde.Year.ToString()));
                                }
                                else
                                {
                                    if (oAuxiliar < ((DateTime[])htAnnoGasto[oAuxiliar.Year])[0])
                                    {
                                        bErrorControlado = true;
                                        throw (new Exception("Tramitación denegada.\n\nHasta el día " + ((DateTime[])htAnnoGasto[oAuxiliar.Year])[0].ToShortDateString() + " no se permite tramitar gastos correspondientes al año " + oDesde.Year.ToString()));
                                    }
                                    else if (oAuxiliar > ((DateTime[])htAnnoGasto[oAuxiliar.Year])[1])
                                    {
                                        bErrorControlado = true;
                                        throw (new Exception("Tramitación denegada.\n\nSe ha superado la fecha límite establecida para tramitar gastos correspondientes al año " + oDesde.Year.ToString() + " (" + ((DateTime[])htAnnoGasto[oAuxiliar.Year])[1].ToShortDateString() + ")"));
                                    }
                                }

                                if (byte.Parse(aDatosCabecera[3]) == 1) //Si motivo proyecto
                                {
                                    if ((DateTime?[])htUsuarioPSN[int.Parse(aDatosPosicion[14])] == null)
                                    {
                                        bErrorControlado = true;
                                        throw (new Exception("Tramitación denegada.\n\nEl beneficiario no se encuentra asociado al proyecto."));
                                    }
                                    else
                                    {
                                        oDesdePSN = (DateTime)((DateTime?[])htUsuarioPSN[int.Parse(aDatosPosicion[14])])[0];
                                        oHastaPSN = ((DateTime?[])htUsuarioPSN[int.Parse(aDatosPosicion[14])])[1];

                                        if (oAuxiliar < oDesdePSN || (oHastaPSN != null && oAuxiliar > oHastaPSN))
                                        {
                                            bErrorControlado = true;
                                            throw (new Exception("Tramitación denegada.\n\nEl rango de fechas de la imputación (" + oDesde.ToShortDateString() + " - " + oHasta.ToShortDateString() + ") se encuentra fuera del rango de fechas de asociación al proyecto \"" + aDatosPosicion[15] + "\" (" + oDesdePSN.ToShortDateString() + " - " + ((oHastaPSN != null) ? ((DateTime)oHastaPSN).ToShortDateString() : "") + ")"));
                                        }
                                    }
                                }

                                oAuxiliar = oAuxiliar.AddDays(1);
                            } while (oAuxiliar <= oHasta);

                            //Comprobar que si se indica un desplazamiento ECO, se corresponda con un VUP
                            if (aDatosPosicion[9] != "")
                            {
                                if (!DAL.POSICIONGV.EsDesplazamientoECOenVUP(tr, int.Parse(aDatosPosicion[9])))
                                {
                                    bErrorControlado = true;
                                    throw (new Exception("Tramitación denegada.\n\nEl desplazamiento correspondiente a las fechas (" + oDesde.ToShortDateString() + " - " + oHasta.ToShortDateString() + ") con destino \"" + Utilidades.unescape(aDatosPosicion[2]) + "\" se ha realizado en un coche de flota, por lo que no procede asociarlo a una solicitud GASVI."));
                                }
                            }

                            DAL.POSICIONGV.InsertarPosicion(tr, nReferencia,
                                                    DateTime.Parse(aDatosPosicion[0]),
                                                    DateTime.Parse(aDatosPosicion[1]),
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
                        if (bErrorControlado) throw (new Exception(ex.Message));
                        else throw (new Exception("Error al grabar las posiciones. " + ex.Message));
                    }
                }

                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                if (bErrorControlado) sResul = ex.Message;
                else sResul = Errores.mostrarError("Error al tramitar la nota estándar.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                {
                    if (bErrorControlado) sResul = "ErrorControlado##EC##" + sResul;
                    throw (new Exception(sResul));
                }
            }

            return nReferencia.ToString();
        }

        public static string ObtenerNotasAbiertasYRecientes(int t001_idficepi)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='MA' style='width:950px;'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:20px; text-align:center;' />");
            sb.Append(" <col style='width:60px; text-align:right; padding-right:10px;' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append(" <col style='width:150px;' />");
            sb.Append(" <col style='width:180px;' />");
            sb.Append(" <col style='width:140px;' />");
            sb.Append(" <col style='width:140px;' />");
            sb.Append(" <col style='width:50px;' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.CABECERAGV.ObtenerNotasAbiertasYRecientes(null, t001_idficepi);
            string sTooltip = "", sFecha = "";
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t420_idreferencia"].ToString() + "' ");
                sb.Append("tipo='" + dr["TipoNota"].ToString() + "' ");
                sb.Append("estado='" + dr["t431_idestado"].ToString() + "' ");
                sb.Append("style='height:20px;' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle;' />&nbsp;&nbsp;Informaci&oacute;n] ");
                sTooltip = "<label style='width:100px'>Solicitante:</label>" + dr["Solicitante"].ToString();
                sTooltip += "<br><label style='width:100px'>" + ((dr["t001_sexo_interesado"].ToString() == "V") ? "Beneficiario" : "Beneficiaria") + ":</label>" + dr["Interesado"].ToString();
                sTooltip += "<br><label style='width:100px'>Concepto:</label>" + dr["t420_concepto"].ToString();
                sTooltip += "<br><label style='width:100px'>Moneda:</label>" + dr["t422_denominacion"].ToString();
                //if (dr["t422_idmoneda"].ToString() != "EUR")
                //{
                //    if (dr["t431_idestado"].ToString() != "L" && dr["t431_idestado"].ToString() != "C" || dr["t431_idestado"].ToString() != "S")
                //        sTooltip += "<br><label style='width:100px'>Importe &euro;:</label>" + decimal.Parse(dr["TOTALEUROS"].ToString()).ToString("N");
                //}
                switch (dr["t431_idestado"].ToString())
                {
                    case "A":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "B":
                        sFecha = (dr["t420_fNoaprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fNoaprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>No aprobada por:</label>" + dr["NoAprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "L":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fAceptada"] != DBNull.Value) ? ((DateTime)dr["t420_fAceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aceptada por:</label>" + dr["AceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "O":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fNoaceptada"] != DBNull.Value) ? ((DateTime)dr["t420_fNoaceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>No aceptada por:</label>" + dr["NoAceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "X":
                        sFecha = (dr["t420_fAnulada"] != DBNull.Value) ? ((DateTime)dr["t420_fAnulada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Anulada por:</label>" + dr["AnuladaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "C":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fAceptada"] != DBNull.Value) ? ((DateTime)dr["t420_fAceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aceptada por:</label>" + dr["AceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fContabilizada"] != DBNull.Value) ? ((DateTime)dr["t420_fContabilizada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:115px'>Fecha contabilización:</label>" + sFecha.Substring(0, sFecha.Length - 3);
                        break;
                    case "S":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fAceptada"] != DBNull.Value) ? ((DateTime)dr["t420_fAceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aceptada por:</label>" + dr["AceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fContabilizada"] != DBNull.Value) ? ((DateTime)dr["t420_fContabilizada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:115px'>Fecha contabilización:</label>" + sFecha.Substring(0, sFecha.Length - 3);
                        sFecha = (dr["t420_fPagada"] != DBNull.Value) ? ((DateTime)dr["t420_fPagada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:115px'>Fecha pagada:</label>" + sFecha.Substring(0, sFecha.Length - 3);
                        break;
                }
                //if (dr["t431_idestado"].ToString() != "P")
                //{//si no están aparcadas
                //    if (dr["t422_idmoneda"].ToString() == "EUR" || dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C"  || dr["t431_idestado"].ToString() == "S" )
                //    {
                //        sTooltip += "<br><label style='width:115px;'>A cobrar sin retención:</label><label style='width:50px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_SINRETENCION"].ToString()).ToString("N") + "</label> " + dr["t422_idmoneda"].ToString();
                //        sTooltip += "<br><label style='width:115px;'>A cobrar en nómina:</label><label style='width:50px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_NOMINA"].ToString()).ToString("N") + "</label> EUR";
                //    }
                //}

                if (dr["t431_idestado"].ToString() != "P")
                {//si no están aparcadas
                    sTooltip += "<br><label style='width:115px;'>A cobrar sin retención:</label><label style='width:50px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_SINRETENCION_EUROS"].ToString()).ToString("N") + "</label> EUR";
                    if (dr["t422_idmoneda"].ToString() != "EUR" && (dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C"  || dr["t431_idestado"].ToString() == "S"))
                        sTooltip += "<label style='width:60px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_SINRETENCION"].ToString()).ToString("N") + "</label> " + dr["t422_idmoneda"].ToString();

                    if (dr["t422_idmoneda"].ToString() == "EUR" || dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C" || dr["t431_idestado"].ToString() == "S")
                    {
                        sTooltip += "<br><label style='width:115px;'>A cobrar en nómina:</label><label style='width:50px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_NOMINA"].ToString()).ToString("N") + "</label> EUR";
                    }
                }

                sb.Append("body=[" + Utilidades.CadenaParaTooltipExtendido(sTooltip) + "]\" ");
                sb.Append("onclick='ms(this);' ");
                sb.Append("ondblclick='md(this);' ");
                sb.Append(">");
                sb.Append("<td><img src='../../images/imgTipo" + dr["TipoNota"].ToString() + ".gif'></td>");
                if (dr["t431_idestado"].ToString() != "P") //Si la nota no está aparcada
                    sb.Append("<td>" + ((int)dr["t420_idreferencia"]).ToString("#,###") + "</td>");
                else
                    sb.Append("<td></td>");
                sb.Append("<td " + ((dr["t431_idestado"].ToString() == "B" || dr["t431_idestado"].ToString() == "O") ? "style='color:red;'" : "") + ">" + dr["t431_denominacion"].ToString() + "</td>");
                if (dr["t420_fTramitada"] != DBNull.Value)
                    sb.Append("<td>" + ((DateTime)dr["t420_fTramitada"]).ToShortDateString() + "</td>");
                else
                    sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W140'>" + dr["Interesado"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W170'>" + dr["t420_concepto"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["t423_denominacion"].ToString() + "</td>");
                sb.Append("<td><nobr class='NBR W130'>");
                if (dr["t301_idproyecto"].ToString() != "")
                    sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - ");

                sb.Append(dr["t301_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["t422_idmoneda"].ToString() + "</td>");

                //if (dr["t422_idmoneda"].ToString() == "EUR" || dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C" || dr["t431_idestado"].ToString() == "S")
                if (dr["t431_idestado"].ToString() != "P"){
                    sb.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["TOTALVIAJE"].ToString()).ToString("N") + "</td>");
                }
                else
                {
                    sb.Append("<td></td>");
                }
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static int RecuperarNotaEstandar(int t420_idreferencia)
        {
            return DAL.CABECERAGV.RecuperarNotaEstandar(null, t420_idreferencia);
        }

        public static int RecuperarBono(int t420_idreferencia)
        {
            return DAL.CABECERAGV.RecuperarBono(null, t420_idreferencia);
        }

        public static int RecuperarPago(int t420_idreferencia)
        {
            return DAL.CABECERAGV.RecuperarPago(null, t420_idreferencia);
        }

        public static string ObtenerHistorial(int t420_idreferencia)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatosHistorial' style='width:610px;'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:105px; padding-left:3px;' />");
            sb.Append(" <col style='width:105px;' />");
            sb.Append(" <col style='width:400px; padding-right:3px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.CABECERAGV.ObtenerHistorial(null, t420_idreferencia);
            int i = 0;
            string sFecha = "";
            while (dr.Read())
            {
                sb.Append("<tr style='vertical-align: top;' ");
                if (i % 2 == 0) sb.Append("class='FA' ");
                else sb.Append("class='FB' ");
                //color pijama
                sb.Append(">");
                if (dr["t431_denominacion"] != DBNull.Value)
                    sb.Append("<td>" + dr["t431_denominacion"].ToString() + "</td>");
                else
                    sb.Append("<td><img src='../../Images/imgEmail16.png'></td>");
                sFecha = ((DateTime)dr["t659_fecha"]).ToString();
                sb.Append("<td>" + sFecha.Substring(0, sFecha.Length - 3) + "</td>");

                sb.Append("<td>" + dr["Profesional"].ToString());
                if (dr["t659_motivo"].ToString() != "")
                {
                    sb.Append("<br><span style='width:350px; margin-left:30px;'>" + dr["t659_motivo"].ToString().Replace(((char)13).ToString() + ((char)10).ToString(), "<br>") + "</span>");
                }
                sb.Append("</td>");
                sb.Append("</tr>");
                i++;
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string ObtenerNotasPendientesAprobarAceptar(string sOpcion, int t001_idficepi)
        {
            SqlDataReader dr;
            StringBuilder sbNotas = new StringBuilder();
            StringBuilder sbBonos = new StringBuilder();
            StringBuilder sbPagos = new StringBuilder();
            string sTooltip = "", sFecha = "";

            #region Cabecera tablas HTML
            sbNotas.Append("<table id='tblDatosEstandar' class='MA' style='width:785px;'>");
            sbNotas.Append("<colgroup>");
            sbNotas.Append("<col style='width:30px; padding-left:13px;' />");
            sbNotas.Append("<col style='width:20px; text-align:center;' />");
            sbNotas.Append("<col style='width:26px; text-align:center;' />");
            sbNotas.Append("<col style='width:70px; text-align:right; padding-right:10px;' />");
            sbNotas.Append("<col style='width:115px;' />");
            sbNotas.Append("<col style='width:154px;' />");
            sbNotas.Append("<col style='width:120px;' />");
            sbNotas.Append("<col style='width:20px;' />");
            sbNotas.Append("<col style='width:110px;' />");
            sbNotas.Append("<col style='width:50px;' />");
            sbNotas.Append("<col style='width:70px; ' />");
            sbNotas.Append("</colgroup>");

            sbBonos.Append("<table id='tblDatosBonoTrans' class='MA' style='width:370px;'>");
            sbBonos.Append("<colgroup>");
            sbBonos.Append("<col style='width:30px;' />");
            sbBonos.Append("<col style='width:50px; text-align:right; padding-right:5px;' />");
            sbBonos.Append("<col style='width:100px;' />");
            sbBonos.Append("<col style='width:90px;' />");
            sbBonos.Append("<col style='width:45px;' />");
            sbBonos.Append("<col style='width:55px; text-align:right; padding-right:2px;' />");
            sbBonos.Append("</colgroup>");

            sbPagos.Append("<table id='tblDatosPagosConcertados' class='MA' style='width:370px;'>");
            sbPagos.Append("<colgroup>");
            sbPagos.Append("<col style='width:30px; text-align:center;' />");
            sbPagos.Append("<col style='width:50px; text-align:right; padding-right:5px;' />");
            sbPagos.Append("<col style='width:190px;' />");
            sbPagos.Append("<col style='width:45px;' />");
            sbPagos.Append("<col style='width:55px; text-align:right; padding-right:2px;' />");
            sbPagos.Append("</colgroup>");
            #endregion

            if (sOpcion == "APROBAR")
                dr = DAL.CABECERAGV.ObtenerNotasPendientesAprobar(null, t001_idficepi);
            else //"ACEPTAR"
                dr = DAL.CABECERAGV.ObtenerNotasPendientesAceptar(null, t001_idficepi);
            while (dr.Read())
            {
                sTooltip = "<label style='width:100px'>" + ((dr["t001_sexo_interesado"].ToString() == "V") ? "Beneficiario" : "Beneficiaria") + ":</label>" + dr["Interesado"].ToString();
                sTooltip += "<br><label style='width:100px'>Concepto:</label>" + dr["t420_concepto"].ToString();
                if (sOpcion == "ACEPTAR")
                {
                    sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                    sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                }
                sTooltip += "<br><label style='width:100px'>Moneda:</label>" + dr["t422_denominacion"].ToString();
                if (dr["t422_idmoneda"].ToString() != "EUR")
                {
                    if (dr["t431_idestado"].ToString() != "L" && dr["t431_idestado"].ToString() != "C" || dr["t431_idestado"].ToString() != "S")
                        sTooltip += "<br><label style='width:100px'>Importe:</label>" + decimal.Parse(dr["TOTALEUROS"].ToString()).ToString("N");
                }
                
                switch (dr["TipoNota"].ToString())
                {
                    case "E":
                        sbNotas.Append("<tr id='" + dr["t420_idreferencia"].ToString() + "' ");
                        sbNotas.Append("tipo='" + dr["TipoNota"].ToString() + "' ");
                        sbNotas.Append("estado='" + dr["t431_idestado"].ToString() + "' ");
                        sbNotas.Append("style='height:20px;' ");

                        if (sOpcion == "ACEPTAR")
                        {
                            sbNotas.Append("avisomescerrado='" + dr["avisomescerrado"].ToString() + "' ");
                            sbNotas.Append("feccontab='" + ((DateTime)dr["fec_contabilizacion"]).ToShortDateString() + "' ");
                            sbNotas.Append("tiene_anticipo='" + dr["tiene_anticipo"].ToString() + "' ");

                            if (dr["t420_idreferencia_lote"].ToString() != "")
                            {
                                sTooltip += "<br><label style='width:100px'><b><u>Lote:</u></b></label><span style='backgrounColor:red'>" + int.Parse(dr["t420_idreferencia_lote"].ToString()).ToString("#,###") + "</span>";
                                sTooltip += "<br>" + dr["paquete"].ToString();
                            }
                        }
                        sbNotas.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle;' />&nbsp;&nbsp;Informaci&oacute;n] ");
                        sbNotas.Append("body=[" + Utilidades.CadenaParaTooltipExtendido(sTooltip) + "]\" ");
                        sbNotas.Append("onclick='ms(this);' ondblclick='md(this);' ");
                        sbNotas.Append(">");

                        sbNotas.Append("<td style='text-align:center;'>");
                        if (sOpcion == "APROBAR" || dr["t422_idmoneda"].ToString() == "EUR")
                            sbNotas.Append("<input type='checkbox' class='check'>");
                        sbNotas.Append("</td>");

                        if (sOpcion == "ACEPTAR")
                        {
                            switch (dr["nIconoJustificante"].ToString())
                            {
                                case "2": sbNotas.Append("<td><img src='../../images/imgJustCatKO.gif' style='width:16px; heigth:16px;' title='Solicitud sin justificantes, pero con gastos que los requieren' /></td>"); break;
                                case "1": sbNotas.Append("<td><img src='../../images/imgJustCatOK.gif' style='width:16px; heigth:16px;' title='Solicitud con justificantes' /></td>"); break;
                                default: sbNotas.Append("<td></td>"); break;
                            }

                            switch (dr["nIconoECO"].ToString())
                            {
                                case "2": sbNotas.Append("<td><img src='../../images/imgECOCatReq.gif' style='width:26px; heigth:16px;' title='Solicitud con kilometraje, pero sin referencia ECO' /></td>"); break;
                                case "1": sbNotas.Append("<td><img src='../../images/imgECOOK.gif' style='width:26px; heigth:16px;' title='Solicitud con referencia ECO'/></td>"); break;
                                default: sbNotas.Append("<td></td>"); break;
                            }
                        }
                        else
                        {
                            sbNotas.Append("<td></td>");
                            sbNotas.Append("<td></td>");
                        }
                        
                        sbNotas.Append("<td>" + int.Parse(dr["t420_idreferencia"].ToString()).ToString("#,###") + "</td>");
                        sbNotas.Append("<td><nobr class='NBR W110'>" + dr["Interesado"].ToString() + "</nobr></td>");
                        sbNotas.Append("<td><nobr class='NBR W145'>" + dr["t420_concepto"].ToString() + "</nobr></td>");
                        sbNotas.Append("<td>" + dr["t423_denominacion"].ToString() + "</td>");

                        if (sOpcion == "ACEPTAR" && dr["t420_idreferencia_lote"].ToString() != "")
                            sbNotas.Append("<td><img src='../../images/imgSolMP.gif' style='width:16px; heigth:16px;' title='Solicitud multiproyecto' /></td>");
                        else
                            sbNotas.Append("<td></td>");

                        sbNotas.Append("<td><nobr class='NBR W100'>");
                        if (dr["t301_idproyecto"].ToString() != "")
                            sbNotas.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - ");

                        sbNotas.Append(dr["t301_denominacion"].ToString() + "</nobr></td>");

                        sbNotas.Append("<td>" + dr["t422_idmoneda"].ToString() + "</td>");

                        //if (dr["t422_idmoneda"].ToString() == "EUR" || dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C" || dr["t431_idestado"].ToString() == "S")
                        //{
                        sbNotas.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["TOTALVIAJE"].ToString()).ToString("N") + "</td>");
                        //}
                        //else
                        //{
                        //    sbNotas.Append("<td>?</td>");
                        //}
                        sbNotas.Append("</tr>");
                        break;
                    case "B":
                        sbBonos.Append("<tr id='" + dr["t420_idreferencia"].ToString() + "' ");
                        sbBonos.Append("tipo='" + dr["TipoNota"].ToString() + "' ");
                        sbBonos.Append("estado='" + dr["t431_idestado"].ToString() + "' ");
                        if (sOpcion == "ACEPTAR")
                        {
                            sbBonos.Append("avisomescerrado='" + dr["avisomescerrado"].ToString() + "' ");
                            sbBonos.Append("feccontab='" + ((DateTime)dr["fec_contabilizacion"]).ToShortDateString() + "' ");
                            sbBonos.Append("tiene_anticipo='" + dr["tiene_anticipo"].ToString() + "' ");
                        }
                        sbBonos.Append("style='height:20px;' ");
                        sbBonos.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle;' />&nbsp;&nbsp;Informaci&oacute;n] ");
                        sbBonos.Append("body=[" + Utilidades.CadenaParaTooltipExtendido(sTooltip) + "]\" ");
                        sbBonos.Append("onclick='ms(this);' ondblclick='md(this);' ");
                        sbBonos.Append(">");
                        sbBonos.Append("<td style='text-align:center;'>");
                        if (sOpcion == "APROBAR" || dr["t422_idmoneda"].ToString() == "EUR")
                            sbBonos.Append("<input type='checkbox' class='check'>");
                        sbBonos.Append("</td>");
                        sbBonos.Append("<td>" + int.Parse(dr["t420_idreferencia"].ToString()).ToString("#,###") + "</td>");
                        sbBonos.Append("<td><nobr class='NBR W90'>" + dr["Interesado"].ToString() + "</nobr></td>");
                        sbBonos.Append("<td><nobr class='NBR W85'>" + dr["MesBono"].ToString() + "</nobr></td>");
                        sbBonos.Append("<td>" + dr["t422_idmoneda"].ToString() + "</td>");
                        sbBonos.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["TOTALVIAJE"].ToString()).ToString("N") + "</td>");
                        sbBonos.Append("</tr>");
                        break;
                    case "P":
                        sbPagos.Append("<tr id='" + dr["t420_idreferencia"].ToString() + "' ");
                        sbPagos.Append("tipo='" + dr["TipoNota"].ToString() + "' ");
                        sbPagos.Append("estado='" + dr["t431_idestado"].ToString() + "' ");
                        if (sOpcion == "ACEPTAR")
                        {
                            sbPagos.Append("avisomescerrado='" + dr["avisomescerrado"].ToString() + "' ");
                            sbPagos.Append("feccontab='" + ((DateTime)dr["fec_contabilizacion"]).ToShortDateString() + "' ");
                            sbPagos.Append("tiene_anticipo='" + dr["tiene_anticipo"].ToString() + "' ");
                        }
                        sbPagos.Append("style='height:20px;' ");
                        sbPagos.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle;' />&nbsp;&nbsp;Informaci&oacute;n] ");
                        sbPagos.Append("body=[" + Utilidades.CadenaParaTooltipExtendido(sTooltip) + "]\" ");
                        sbPagos.Append("onclick='ms(this);' ondblclick='md(this);' ");
                        sbPagos.Append(">");
                        sbPagos.Append("<td style='text-align:center;'>");
                        if (sOpcion == "ACEPTAR" && dr["t422_idmoneda"].ToString() == "EUR")
                            sbPagos.Append("<input type='checkbox' class='check'>");
                        sbPagos.Append("</td>");
                        sbPagos.Append("<td>" + int.Parse(dr["t420_idreferencia"].ToString()).ToString("#,###") + "</td>");
                        sbPagos.Append("<td><nobr class='NBR W180'>" + dr["Interesado"].ToString() + "</nobr></td>");
                        sbPagos.Append("<td>" + dr["t422_idmoneda"].ToString() + "</td>");
                        sbPagos.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["TOTALVIAJE"].ToString()).ToString("N") + "</td>");
                        sbPagos.Append("</tr>");
                        break;

                }
            }
            dr.Close();
            dr.Dispose();

            sbNotas.Append("</table>");
            sbBonos.Append("</table>");
            sbPagos.Append("</table>");

            return sbNotas.ToString() + "#@septabla@#" + sbBonos.ToString() + "#@septabla@#" + sbPagos.ToString();
        }

        public static void UpdateAcuerdo(string sReferencias, string sidAcuerdo)
        {
            DAL.CABECERAGV.UpdateAcuerdo(null, int.Parse(sReferencias), int.Parse(sidAcuerdo));
        }

        public static void Aprobar(string sReferencias)
        {
            ArrayList aListCorreo = new ArrayList();
            StringBuilder sb = new StringBuilder();

            DataSet ds = DAL.CABECERAGV.Aprobar(null, sReferencias);
            //Devuelve la relación de beneficiarios que se han aprobado alguna solicitud
            //sin ser autorresonsable, por no haber deteminado otro aprobador.

            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                sb.Append("GASVI le comunica que la solicitud nº "+ ((int)oFila["t420_idreferencia"]).ToString("#,###") +" ");
                sb.Append("ha sido aprobada por "+ oFila["Beneficiario"].ToString() +" siendo la persona beneficiaria y sin ser autorresponsable.<br><br>");
                sb.Append("Motivo: el circuito de aprobación no ha podido determinar otro aprobador.");

                string[] aMail = { "Aprobador válido en GASVI", 
                                     sb.ToString(), 
                                     DAL.CABECERAGV.ObtenerAceptadoresCODRED(null, (int)oFila["t420_idreferencia"])};
                aListCorreo.Add(aMail);
            }
            ds.Dispose();

            if (aListCorreo.Count > 0)
                Correo.EnviarCorreos(aListCorreo);
        }
        public static int NoAprobar(int t420_idreferencia, string t659_motivo)
        {
            return DAL.CABECERAGV.NoAprobar(null, t420_idreferencia, Utilidades.unescape(t659_motivo));
        }

        public static void Aceptar(string sReferenciasYDatos)
        {
            string sDatos = "";
            string sReferenciasSinCentroCoste = "";
            string sPagadores = "";
            bool bPagadoresObtenidos = false;
            string sAsunto = "GASVI: Aceptada solicitud con anticipo";
            ArrayList aListCorreo = new ArrayList();
            StringBuilder sb = new StringBuilder();

            string[] aSolicitudes = Regex.Split(sReferenciasYDatos, ";");
            foreach (string oSolicitud in aSolicitudes)
            {
                if (oSolicitud == "") continue;
                string[] aDatosSolicitud = Regex.Split(oSolicitud, "#");

                string sCentroCoste = DAL.CABECERAGV.ObtenerCentroCoste(null, int.Parse(aDatosSolicitud[0]));
                if (sCentroCoste == "")
                {
                    sReferenciasSinCentroCoste += int.Parse(aDatosSolicitud[0]).ToString("#,###")+", ";
                }
                else
                {
                    sDatos += aDatosSolicitud[0] + "#" + aDatosSolicitud[1] + "@" + sCentroCoste + "$" + aDatosSolicitud[2] + ";";
                }

                //Si la solicitud tiene anticipo, y en caso afirmativo mail al pagador.
                //aDatosSolicitud[3] = anticipo (1/0)
                if (aDatosSolicitud[3] == "1")
                {
                    if (sPagadores == "" && !bPagadoresObtenidos)
                    {
                        sPagadores = DAL.Profesional.ObtenerPagadores(null);
                        bPagadoresObtenidos = true;
                        if (sPagadores == "") continue;

                        //envío de mail.
                        CABECERAGV oCabecera = CABECERAGV.ObtenerDatosCabecera(int.Parse(aDatosSolicitud[0]));

                        sb.Append("<label style='width:80px;'>Referencia:</label>" + oCabecera.t420_idreferencia.ToString("#,###") + "</br>");
                        sb.Append("<label style='width:80px;'>" + ((oCabecera.t001_sexo_interesado == "V") ? "Acreedor" : "Acreedora") + ":</label>" + oCabecera.t314_idusuario_interesado.ToString("#,###") + "</br>");
                        sb.Append("<label style='width:80px;'>" + ((oCabecera.t001_sexo_interesado == "V") ? "Beneficiario" : "Beneficiaria") + ":</label>" + oCabecera.Interesado + "</br></br>");

                        sb.Append("<b>Anticipado:</b></br>");
                        sb.Append("<label style='width:80px;'>Fecha:</label>" + ((oCabecera.t420_fanticipo.HasValue) ? ((DateTime)oCabecera.t420_fanticipo).ToShortDateString() : "") + "</br>");
                        sb.Append("<label style='width:80px;'>Importe:</label>" + oCabecera.t420_importeanticipo.ToString("N") + " "+ oCabecera.t422_denominacion +"</br>");
                        sb.Append("<label style='width:80px;'>Oficina:</label>" + oCabecera.t420_lugaranticipo + "</br></br>");
                        
                        sb.Append("<b>Devuelto:</b></br>");
                        sb.Append("<label style='width:80px;'>Fecha:</label>" + ((oCabecera.t420_fdevolucion.HasValue) ? ((DateTime)oCabecera.t420_fdevolucion).ToShortDateString() : "") + "</br>");
                        sb.Append("<label style='width:80px;'>Importe:</label>" + oCabecera.t420_importedevolucion.ToString("N") + " " + oCabecera.t422_denominacion + "</br>");
                        sb.Append("<label style='width:80px;'>Oficina:</label>" + oCabecera.t420_lugardevolucion + "</br></br>");
                        
                        sb.Append("<b>Aclaraciones:</b></br>");
                        sb.Append(oCabecera.t420_aclaracionesanticipo);

                        string[] aMail = { sAsunto, sb.ToString(), sPagadores };
                        aListCorreo.Add(aMail);
                    }
                }
            }

            if (sDatos.Length > 8000)
            {
                throw (new Exception("ErrorControlado##EC##El número de solicitudes a aceptar supera el máximo permitido.\n\nPor favor, redúzcalo."));
            }

            //DAL.CABECERAGV.Aceptar(null, sReferenciasYFechas);
            DAL.CABECERAGV.Aceptar(null, sDatos);

            if (aListCorreo.Count > 0)
                Correo.EnviarCorreos(aListCorreo);

            if (sReferenciasSinCentroCoste != "")
            {
                throw (new Exception("ErrorControlado##EC##¡Atención!\n\nExisten solicitudes para las cuales no se ha podido establecer el centro de coste y por tanto no han podido ser aceptadas.\n\nPor favor, tome nota de su(s) referencia(s) y contacte con el administador.\n\nReferencia(s): " + sReferenciasSinCentroCoste.Substring(0,sReferenciasSinCentroCoste.Length-2)));
            }
        }

        public static int NoAceptar(int t420_idreferencia, string t659_motivo)
        {
            return DAL.CABECERAGV.NoAceptar(null, t420_idreferencia, Utilidades.unescape(t659_motivo));
        }

        public static int Anular(int t420_idreferencia, string t659_motivo)
        {
            return DAL.CABECERAGV.Anular(null, t420_idreferencia, Utilidades.unescape(t659_motivo));
        }

        public static int AnularAdm(int t420_idreferencia, string t659_motivo)
        {
            return DAL.CABECERAGV.AnularAdm(null, t420_idreferencia, Utilidades.unescape(t659_motivo));
        }

        public static string ObtenerNotasDeUnLote(int t420_idreferencia_lote)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' style='width:750px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:70px; padding-right:2px; text-align:right;' />");
            sb.Append("    <col style='width:70px; padding-right:2px; text-align:right;' />");
            sb.Append("    <col style='width:70px; padding-right:2px; text-align:right;' />");
            sb.Append("    <col style='width:70px; padding-right:2px; text-align:right;' />");
            sb.Append("    <col style='width:70px; padding-right:2px; text-align:right;' />");
            sb.Append("    <col style='width:100px; padding-left:5px;' />");
            sb.Append("    <col style='width:330px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = DAL.CABECERAGV.ObtenerNotasDeUnLote(null, t420_idreferencia_lote);
            string sFecha = "";
            while (dr.Read())
            {
                sb.Append("<tr style='height:20px;'>");
                sb.Append("<td>" + int.Parse(dr["t420_idreferencia"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td>" + double.Parse(dr["t421_peajepark"].ToString()).ToString("N") + "</td>");
                sb.Append("<td>" + double.Parse(dr["t421_comida"].ToString()).ToString("N") + "</td>");
                sb.Append("<td>" + double.Parse(dr["t421_transporte"].ToString()).ToString("N") + "</td>");
                sb.Append("<td>" + double.Parse(dr["t421_hotel"].ToString()).ToString("N") + "</td>");
                sb.Append("<td>" + dr["t431_denominacion"].ToString() + "</td>");
                if (dr["t420_fAceptada"] != DBNull.Value)
                {
                    sFecha = ((DateTime)dr["t420_fAceptada"]).ToString();
                    sb.Append("<td><nobr class='NBR W320'>" + dr["AceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")</nobr></td>");
                }
                else
                    sb.Append("<td></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string ObtenerMisSolicitudes(string t001_idficepi,
            string sEstados,
            string sMotivos,
            string nDesde,
            string nHasta,
            string t420_concepto,
            string t305_idproyectosubnodo,
            string t420_idreferencia
            )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='MA' style='width:980px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:30px; padding-left:10px;' />");
            sb.Append(" <col style='width:20px; text-align:center;' />");
            sb.Append(" <col style='width:60px; text-align:right; padding-right:10px;' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append(" <col style='width:150px;' />");
            sb.Append(" <col style='width:180px;' />");
            sb.Append(" <col style='width:140px;' />");
            sb.Append(" <col style='width:140px;' />");
            sb.Append(" <col style='width:50px;' />");
            sb.Append(" <col style='width:70px; ' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.CABECERAGV.ObtenerMisSolicitudes(null, int.Parse(t001_idficepi),
                sEstados, sMotivos, int.Parse(nDesde), int.Parse(nHasta), t420_concepto,
                (t305_idproyectosubnodo == "") ? null : (int?)int.Parse(t305_idproyectosubnodo),
                (t420_idreferencia == "") ? null : (int?)int.Parse(t420_idreferencia));

            string sTooltip = "", sFecha = "";
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t420_idreferencia"].ToString() + "' ");
                sb.Append("tipo='" + dr["TipoNota"].ToString() + "' ");
                sb.Append("estado='" + dr["t431_idestado"].ToString() + "' ");
                sb.Append("style='height:20px;' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle;' />&nbsp;&nbsp;Informaci&oacute;n] ");
                sTooltip = "<label style='width:100px'>Solicitante:</label>" + dr["Solicitante"].ToString();
                sTooltip += "<br><label style='width:100px'>" + ((dr["t001_sexo_interesado"].ToString() == "V") ? "Beneficiario" : "Beneficiaria") + ":</label>" + dr["Interesado"].ToString();
                sTooltip += "<br><label style='width:100px'>Concepto:</label>" + dr["t420_concepto"].ToString();
                sTooltip += "<br><label style='width:100px'>Moneda:</label>" + dr["t422_denominacion"].ToString();
                //if (dr["t422_idmoneda"].ToString() != "EUR")
                //{
                //    if (dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C" || dr["t431_idestado"].ToString() == "S")
                //        sTooltip += "<br><label style='width:100px'>Importe &euro;:</label>" + decimal.Parse(dr["TOTALEUROS"].ToString()).ToString("N");
                //}

                switch (dr["t431_idestado"].ToString())
                {
                    case "A":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "B":
                        sFecha = (dr["t420_fNoaprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fNoaprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>No aprobada por:</label>" + dr["NoAprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "L":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fAceptada"] != DBNull.Value) ? ((DateTime)dr["t420_fAceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aceptada por:</label>" + dr["AceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "O":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fNoaceptada"] != DBNull.Value) ? ((DateTime)dr["t420_fNoaceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>No aceptada por:</label>" + dr["NoAceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "X":
                        sFecha = (dr["t420_fAnulada"] != DBNull.Value) ? ((DateTime)dr["t420_fAnulada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Anulada por:</label>" + dr["AnuladaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "C":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fAceptada"] != DBNull.Value) ? ((DateTime)dr["t420_fAceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aceptada por:</label>" + dr["AceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fContabilizada"] != DBNull.Value) ? ((DateTime)dr["t420_fContabilizada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:115px'>Fecha contabilización:</label>" + sFecha.Substring(0, sFecha.Length - 3);
                        break;
                    case "S":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fAceptada"] != DBNull.Value) ? ((DateTime)dr["t420_fAceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aceptada por:</label>" + dr["AceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fContabilizada"] != DBNull.Value) ? ((DateTime)dr["t420_fContabilizada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:115px'>Fecha contabilización:</label>" + sFecha.Substring(0, sFecha.Length - 3);
                        sFecha = (dr["t420_fPagada"] != DBNull.Value) ? ((DateTime)dr["t420_fPagada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:115px'>Fecha pagada:</label>" + sFecha.Substring(0, sFecha.Length - 3);
                        break;
                }
                //if (dr["t431_idestado"].ToString() != "P")
                //{//si no están aparcadas
                //    if (dr["t422_idmoneda"].ToString() == "EUR" || dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C" || dr["t431_idestado"].ToString() == "S")
                //    {
                //        sTooltip += "<br><label style='width:115px;'>A cobrar sin retención:</label><label style='width:50px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_SINRETENCION"].ToString()).ToString("N") + "</label> " + dr["t422_idmoneda"].ToString();
                //        sTooltip += "<br><label style='width:115px;'>A cobrar en nómina:</label><label style='width:50px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_NOMINA"].ToString()).ToString("N") + "</label> EUR";
                //    }
                //}
                if (dr["t431_idestado"].ToString() != "P")
                {//si no están aparcadas
                    sTooltip += "<br><label style='width:115px;'>A cobrar sin retención:</label><label style='width:50px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_SINRETENCION_EUROS"].ToString()).ToString("N") + "</label> EUR";
                    if (dr["t422_idmoneda"].ToString() != "EUR" && (dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C" || dr["t431_idestado"].ToString() == "S"))
                        sTooltip += "<label style='width:60px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_SINRETENCION"].ToString()).ToString("N") + "</label> " + dr["t422_idmoneda"].ToString();

                    if (dr["t422_idmoneda"].ToString() == "EUR" || dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C" || dr["t431_idestado"].ToString() == "S")
                    {
                        sTooltip += "<br><label style='width:115px;'>A cobrar en nómina:</label><label style='width:50px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_NOMINA"].ToString()).ToString("N") + "</label> EUR";
                    }
                }

                sb.Append("body=[" + Utilidades.CadenaParaTooltipExtendido(sTooltip) + "]\" ");
                //sb.Append("ondblclick='md(this);' "); //SE Pone con el scroll
                sb.Append(">");
                sb.Append("<td><input type='checkbox' class='check' checked></td>");
                //sb.Append("<td><img src='../../../images/imgTipo" + dr["TipoNota"].ToString() + ".gif'></td>");
                sb.Append("<td></td>");
                if (dr["t431_idestado"].ToString() != "P") //Si la nota no está aparcada
                    sb.Append("<td>" + ((int)dr["t420_idreferencia"]).ToString("#,###") + "</td>");
                else
                    sb.Append("<td></td>");
                sb.Append("<td " + ((dr["t431_idestado"].ToString() == "B" || dr["t431_idestado"].ToString() == "O") ? "style='color:#ff0000'" : "") + ">" + dr["t431_denominacion"].ToString() + "</td>");
                if (dr["t420_fTramitada"] != DBNull.Value)
                    sb.Append("<td>" + ((DateTime)dr["t420_fTramitada"]).ToShortDateString() + "</td>");
                else
                    sb.Append("<td></td>");
                //sb.Append("<td><nobr class='NBR W140'>" + dr["Interesado"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["Interesado"].ToString() + "</td>");
                //sb.Append("<td><nobr class='NBR W170'>" + dr["t420_concepto"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["t420_concepto"].ToString() + "</td>");
                sb.Append("<td>" + dr["t423_denominacion"].ToString() + "</td>");
                //sb.Append("<td><nobr class='NBR W130'>");
                sb.Append("<td>");
                if (dr["t301_idproyecto"].ToString() != "")
                    sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - ");

                sb.Append(dr["t301_denominacion"].ToString());
                //sb.Append("</nobr></td>");
                sb.Append("</td>");
                sb.Append("<td>" + dr["t422_idmoneda"].ToString() + "</td>");
                sb.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["TOTALVIAJE"].ToString()).ToString("N") + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string ObtenerMiAmbito(string t001_idficepi,
            string sMotivos,
            string nDesde,
            string nHasta)
        {
            StringBuilder sb = new StringBuilder();
            #region Pestaña Estructura
            sb.Append("<table id='tblDatosEstructura' style='width:1230px; text-align:right;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:310px;' />");
            sb.Append("    <col style='width:40px;' />");
            sb.Append("    <col style='width:40px;' />");
            sb.Append("    <col style='width:40px;' />");
            sb.Append("    <col style='width:40px;' />");
            sb.Append("    <col style='width:75px;' />");
            sb.Append("    <col style='width:75px;' />");
            sb.Append("    <col style='width:75px;' />");
            sb.Append("    <col style='width:75px;' />");
            sb.Append("    <col style='width:75px;' />");
            sb.Append("    <col style='width:75px;' />");
            sb.Append("    <col style='width:75px;' />");
            sb.Append("    <col style='width:75px;' />");
            sb.Append("    <col style='width:75px;' />");
            sb.Append("    <col style='width:85px;' />");
            sb.Append("</colgroup>");

            DataSet ds = DAL.CABECERAGV.ObtenerMiAmbito(null, int.Parse(t001_idficepi),
                                                        sMotivos, int.Parse(nDesde), int.Parse(nHasta));

            int nMargen = 0;
            int nWidth = 0;
            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                sb.Append("<tr ");
                sb.Append("nivel='" + oFila["nivel"].ToString() + "' ");
                sb.Append("style='height:20px;' ");
                sb.Append(">");
                sb.Append("<td style='text-align-left;'>");//
                switch ((int)oFila["indentacion"])
                {
                    case 1:
                        nMargen = 2;
                        nWidth = 280;
                        break;
                    case 2:
                        nMargen = 22;
                        nWidth = 260;
                        break;
                    case 3:
                        nMargen = 42;
                        nWidth = 240;
                        break;
                    case 4:
                        nMargen = 62;
                        nWidth = 220;
                        break;
                    case 5:
                        nMargen = 82;
                        nWidth = 200;
                        break;
                }

                switch (oFila["nivel"].ToString())
                {
                    case "SN4":
                    case "SN3":
                    case "SN2":
                    case "SN1":
                        sb.Append("<IMG src='../../../images/imgSN" + oFila["sufijoimg"].ToString() + ".gif' style='margin-left:" + nMargen.ToString() + "px;margin-right:2px;'>");
                        break;
                    case "N":
                        sb.Append("<IMG src='../../../images/imgNodo.gif' style='margin-left:" + nMargen.ToString() + "px;margin-right:2px;'>");
                        break;
                }

                sb.Append("<nobr style='text-align:left' class='NBR W" + nWidth.ToString() + "'>" + oFila["denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + ((!(bool)oFila["vision"] || (int)oFila["t421_ncdieta_count"] == 0) ? "" : ((int)oFila["t421_ncdieta_count"]).ToString("#,###")) + "</td>");
                sb.Append("<td>" + ((!(bool)oFila["vision"] || (int)oFila["t421_nmdieta_count"] == 0) ? "" : ((int)oFila["t421_nmdieta_count"]).ToString("#,###")) + "</td>");
                sb.Append("<td>" + ((!(bool)oFila["vision"] || (int)oFila["t421_nedieta_count"] == 0) ? "" : ((int)oFila["t421_nedieta_count"]).ToString("#,###")) + "</td>");
                sb.Append("<td>" + ((!(bool)oFila["vision"] || (int)oFila["t421_nadieta_count"] == 0) ? "" : ((int)oFila["t421_nadieta_count"]).ToString("#,###")) + "</td>");
                sb.Append("<td>" + ((!(bool)oFila["vision"] || double.Parse(oFila["impdietas_eur"].ToString()) == 0) ? "" : double.Parse(oFila["impdietas_eur"].ToString()).ToString("N")) + "</td>");

                sb.Append("<td>" + ((!(bool)oFila["vision"] || (int)oFila["t421_nkms_count"] == 0) ? "" : ((int)oFila["t421_nkms_count"]).ToString("#,###")) + "</td>");
                sb.Append("<td>" + ((!(bool)oFila["vision"] || double.Parse(oFila["impkms_eur"].ToString()) == 0) ? "" : double.Parse(oFila["impkms_eur"].ToString()).ToString("N")) + "</td>");

                sb.Append("<td>" + ((!(bool)oFila["vision"] || double.Parse(oFila["t421_peajepark_eur"].ToString()) == 0) ? "" : double.Parse(oFila["t421_peajepark_eur"].ToString()).ToString("N")) + "</td>");
                sb.Append("<td>" + ((!(bool)oFila["vision"] || double.Parse(oFila["t421_comida_eur"].ToString()) == 0) ? "" : double.Parse(oFila["t421_comida_eur"].ToString()).ToString("N")) + "</td>");
                sb.Append("<td>" + ((!(bool)oFila["vision"] || double.Parse(oFila["t421_transporte_eur"].ToString()) == 0) ? "" : double.Parse(oFila["t421_transporte_eur"].ToString()).ToString("N")) + "</td>");
                sb.Append("<td>" + ((!(bool)oFila["vision"] || double.Parse(oFila["t421_hotel_eur"].ToString()) == 0) ? "" : double.Parse(oFila["t421_hotel_eur"].ToString()).ToString("N")) + "</td>");
                sb.Append("<td>" + ((!(bool)oFila["vision"] || double.Parse(oFila["bono_transporte_eur"].ToString()) == 0) ? "" : double.Parse(oFila["bono_transporte_eur"].ToString()).ToString("N")) + "</td>");
                sb.Append("<td>" + ((!(bool)oFila["vision"] || double.Parse(oFila["pago_concertado_eur"].ToString()) == 0) ? "" : double.Parse(oFila["pago_concertado_eur"].ToString()).ToString("N")) + "</td>");

                sb.Append("<td style=\"border-right:''\">" + ((!(bool)oFila["vision"]) ? "" : double.Parse(oFila["Total"].ToString()).ToString("N")) + "</td>");

                sb.Append("</tr>");
            }
            sb.Append("</table>");
            #endregion
            sb.Append("#@septabla@#");
            #region Pestaña Profesionales
            sb.Append("<table id='tblDatosProfesional' style='width:970px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:95px; ' />");
            sb.Append("    <col style='width:100px; ' />");
            sb.Append("    <col style='width:30px;' />");
            sb.Append("    <col style='width:30px;' />");
            sb.Append("    <col style='width:30px;' />");
            sb.Append("    <col style='width:30px;' />");
            sb.Append("    <col style='width:65px;' />");
            sb.Append("    <col style='width:40px;' />");
            sb.Append("    <col style='width:65px;' />");
            sb.Append("    <col style='width:65px;' />");
            sb.Append("    <col style='width:65px;' />");
            sb.Append("    <col style='width:65px;' />");
            sb.Append("    <col style='width:65px;' />");
            sb.Append("    <col style='width:65px;' />");
            sb.Append("    <col style='width:65px;' />");
            sb.Append("    <col style='width:75px;' />");
            sb.Append("</colgroup>");

            foreach (DataRow oFila in ds.Tables[1].Rows)
            {
                sb.Append("<tr id='" + oFila["t001_idficepi_interesado"].ToString() + "' ");
                sb.Append("sexo='" + oFila["t001_sexo_interesado"].ToString() + "' ");
                sb.Append("linea='" + oFila["linea"].ToString() + "' ");
                sb.Append("unidad='" + oFila["unidad"].ToString() + "' ");
                sb.Append("cr='" + oFila["cr"].ToString() + "' ");
                sb.Append("motivo='" + oFila["t423_denominacion"].ToString() + "' ");
                sb.Append("idPE='" + oFila["t301_idproyecto"].ToString() + "' ");
                sb.Append("denPE='" + oFila["t301_denominacion"].ToString() + "' ");
                sb.Append("cli='" + oFila["t302_denominacion"].ToString() + "' ");
                sb.Append("style='height:20px;' ");
                sb.Append(">");
                sb.Append("<td style=\"text-align:center;border-right:''\"></td>");
                sb.Append("<td style='padding-left:2px;'><nobr class='NBR W100' onmouseover='TTip(event)'>" + oFila["Interesado"].ToString() + "</nobr></td>");
                sb.Append("<td style='padding-left:2px;'><nobr class='NBR W90' onmouseover='TTip(event)'>" + oFila["CR_Prof"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:right; padding-right:2px;'>" + ((int)oFila["t421_ncdieta_count"]).ToString("#,###") + "</td>");
                sb.Append("<td style='text-align:right; padding-right:2px;'>" + ((int)oFila["t421_nmdieta_count"]).ToString("#,###") + "</td>");
                sb.Append("<td style='text-align:right; padding-right:2px;'>" + ((int)oFila["t421_nedieta_count"]).ToString("#,###") + "</td>");
                sb.Append("<td style='text-align:right; padding-right:2px;'>" + ((int)oFila["t421_nadieta_count"]).ToString("#,###") + "</td>");
                sb.Append("<td style='text-align:right; padding-right:2px;'>" + double.Parse(oFila["impdietas_eur"].ToString()).ToString("N") + "</td>");

                sb.Append("<td style='text-align:right; padding-right:2px;'>" + ((int)oFila["t421_nkms_count"]).ToString("#,###") + "</td>");
                sb.Append("<td style='text-align:right; padding-right:2px;'>" + double.Parse(oFila["impkms_eur"].ToString()).ToString("N") + "</td>");

                sb.Append("<td style='text-align:right; padding-right:2px;'>" + double.Parse(oFila["t421_peajepark_eur"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='text-align:right; padding-right:2px;'>" + double.Parse(oFila["t421_comida_eur"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='text-align:right; padding-right:2px;'>" + double.Parse(oFila["t421_transporte_eur"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='text-align:right; padding-right:2px;'>" + double.Parse(oFila["t421_hotel_eur"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='text-align:right; padding-right:2px;'>" + double.Parse(oFila["bono_transporte_eur"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='text-align:right; padding-right:2px;'>" + double.Parse(oFila["pago_concertado_eur"].ToString()).ToString("N") + "</td>");

                sb.Append("<td style=\"text-align:right; padding-right:2px;border-right:''\">" + double.Parse(oFila["Total"].ToString()).ToString("N") + "</td>");

                sb.Append("</tr>");
            }
            sb.Append("</table>");
            #endregion
            ds.Dispose();
            return sb.ToString();
        }

        public static string ObtenerSolicitudesADM(string sEstados,
            string sMotivos,
            string nDesde,
            string nHasta,
            string t420_concepto,
            string t305_idproyectosubnodo,
            string t420_idreferencia,
            string t001_idficepi_aprobada,
            string t001_idficepi_interesado,
            string t303_idnodo_proyecto,
            string t302_idcliente_proyecto
            )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='MA' style='width:980px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:30px; padding-left:10px;' />");
            sb.Append(" <col style='width:20px; text-align:center;' />");
            sb.Append(" <col style='width:60px; text-align:right; padding-right:10px;' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append(" <col style='width:150px;' />");
            sb.Append(" <col style='width:180px;' />");
            sb.Append(" <col style='width:140px;' />");
            sb.Append(" <col style='width:140px;' />");
            sb.Append(" <col style='width:50px;' />");
            sb.Append(" <col style='width:70px; text-align:right; padding-right:2px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.CABECERAGV.ObtenerSolicitudesADM(null,
                sEstados, sMotivos, int.Parse(nDesde), int.Parse(nHasta), t420_concepto,
                (t305_idproyectosubnodo == "") ? null : (int?)int.Parse(t305_idproyectosubnodo),
                (t420_idreferencia == "") ? null : (int?)int.Parse(t420_idreferencia),
                (t001_idficepi_aprobada == "") ? null : (int?)int.Parse(t001_idficepi_aprobada),
                (t001_idficepi_interesado == "") ? null : (int?)int.Parse(t001_idficepi_interesado),
                (t303_idnodo_proyecto == "") ? null : (int?)int.Parse(t303_idnodo_proyecto),
                (t302_idcliente_proyecto == "") ? null : (int?)int.Parse(t302_idcliente_proyecto)
                );

            string sTooltip = "", sFecha = "";
            int i = 0;
            bool bExcede = false;

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t420_idreferencia"].ToString() + "' ");
                sb.Append("tipo='" + dr["TipoNota"].ToString() + "' ");
                sb.Append("estado='" + dr["t431_idestado"].ToString() + "' ");
                sb.Append("style='height:20px;' ");
                sb.Append("title=\"cssbody=[dvbdy] delay=[1000] offsety=[-165] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle;' />&nbsp;&nbsp;Informaci&oacute;n] ");
                sTooltip = "<label style='width:100px'>Solicitante:</label>" + dr["Solicitante"].ToString();
                sTooltip += "<br><label style='width:100px'>" + ((dr["t001_sexo_interesado"].ToString() == "V") ? "Beneficiario" : "Beneficiaria") + ":</label>" + dr["Interesado"].ToString();
                sTooltip += "<br><label style='width:100px'>Concepto:</label>" + dr["t420_concepto"].ToString();
                sTooltip += "<br><label style='width:100px'>Moneda:</label>" + dr["t422_denominacion"].ToString();
                //if (dr["t422_idmoneda"].ToString() != "EUR")
                //{
                //    if (dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C" || dr["t431_idestado"].ToString() == "S")
                //        sTooltip += "<br><label style='width:100px'>Importe &euro;:</label>" + decimal.Parse(dr["TOTALEUROS"].ToString()).ToString("N");
                //}

                switch (dr["t431_idestado"].ToString())
                {
                    case "A":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "B":
                        sFecha = (dr["t420_fNoaprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fNoaprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>No aprobada por:</label>" + dr["NoAprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "L":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fAceptada"] != DBNull.Value) ? ((DateTime)dr["t420_fAceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aceptada por:</label>" + dr["AceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "O":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fNoaceptada"] != DBNull.Value) ? ((DateTime)dr["t420_fNoaceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>No aceptada por:</label>" + dr["NoAceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "X":
                        sFecha = (dr["t420_fAnulada"] != DBNull.Value) ? ((DateTime)dr["t420_fAnulada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Anulada por:</label>" + dr["AnuladaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "C":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fAceptada"] != DBNull.Value) ? ((DateTime)dr["t420_fAceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aceptada por:</label>" + dr["AceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fContabilizada"] != DBNull.Value) ? ((DateTime)dr["t420_fContabilizada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:115px'>Fecha contabilización:</label>" + sFecha.Substring(0, sFecha.Length - 3);
                        break;
                    case "S":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fAceptada"] != DBNull.Value) ? ((DateTime)dr["t420_fAceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aceptada por:</label>" + dr["AceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fContabilizada"] != DBNull.Value) ? ((DateTime)dr["t420_fContabilizada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:115px'>Fecha contabilización:</label>" + sFecha.Substring(0, sFecha.Length - 3);
                        sFecha = (dr["t420_fPagada"] != DBNull.Value) ? ((DateTime)dr["t420_fPagada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:115px'>Fecha pagada:</label>" + sFecha.Substring(0, sFecha.Length - 3);
                        break;
                }
                //if (dr["t431_idestado"].ToString() != "P")
                //{//si no están aparcadas
                //    if (dr["t422_idmoneda"].ToString() == "EUR" || dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C" || dr["t431_idestado"].ToString() == "S")
                //    {
                //        sTooltip += "<br><label style='width:115px;'>A cobrar sin retención:</label><label style='width:50px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_SINRETENCION"].ToString()).ToString("N") + "</label> " + dr["t422_idmoneda"].ToString();
                //        sTooltip += "<br><label style='width:115px;'>A cobrar en nómina:</label><label style='width:50px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_NOMINA"].ToString()).ToString("N") + "</label> EUR";
                //    }
                //}
                if (dr["t431_idestado"].ToString() != "P")
                {//si no están aparcadas
                    sTooltip += "<br><label style='width:115px;'>A cobrar sin retención:</label><label style='width:50px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_SINRETENCION_EUROS"].ToString()).ToString("N") + "</label> EUR";
                    if (dr["t422_idmoneda"].ToString() != "EUR" && (dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C" || dr["t431_idestado"].ToString() == "S"))
                        sTooltip += "<label style='width:60px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_SINRETENCION"].ToString()).ToString("N") + "</label> " + dr["t422_idmoneda"].ToString();

                    if (dr["t422_idmoneda"].ToString() == "EUR" || dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C" || dr["t431_idestado"].ToString() == "S")
                    {
                        sTooltip += "<br><label style='width:115px;'>A cobrar en nómina:</label><label style='width:50px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_NOMINA"].ToString()).ToString("N") + "</label> EUR";
                    }
                }

                sb.Append("body=[" + Utilidades.CadenaParaTooltipExtendido(sTooltip) + "]\" ");
                //sb.Append("ondblclick='md(this);' "); //SE Pone con el scroll
                sb.Append(">");
                sb.Append("<td><input type='checkbox' class='check' checked></td>");
                //sb.Append("<td><img src='../../../images/imgTipo" + dr["TipoNota"].ToString() + ".gif'></td>");
                sb.Append("<td></td>");
                if (dr["t431_idestado"].ToString() != "P") //Si la nota no está aparcada
                    sb.Append("<td>" + ((int)dr["t420_idreferencia"]).ToString("#,###") + "</td>");
                else
                    sb.Append("<td></td>");
                sb.Append("<td " + ((dr["t431_idestado"].ToString() == "B" || dr["t431_idestado"].ToString() == "O") ? "style='color:#ff0000'" : "") + ">" + dr["t431_denominacion"].ToString() + "</td>");
                if (dr["t420_fTramitada"] != DBNull.Value)
                    sb.Append("<td>" + ((DateTime)dr["t420_fTramitada"]).ToShortDateString() + "</td>");
                else
                    sb.Append("<td></td>");
                //sb.Append("<td><nobr class='NBR W140'>" + dr["Interesado"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["Interesado"].ToString() + "</td>");
                //sb.Append("<td><nobr class='NBR W170'>" + dr["t420_concepto"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["t420_concepto"].ToString() + "</td>");
                sb.Append("<td>" + dr["t423_denominacion"].ToString() + "</td>");
                //sb.Append("<td><nobr class='NBR W130'>");
                sb.Append("<td>");
                if (dr["t301_idproyecto"].ToString() != "")
                    sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - ");

                sb.Append(dr["t301_denominacion"].ToString());
                //sb.Append("</nobr></td>");
                sb.Append("</td>");
                sb.Append("<td>" + dr["t422_idmoneda"].ToString() + "</td>");
                sb.Append("<td>" + decimal.Parse(dr["TOTALVIAJE"].ToString()).ToString("N") + "</td>");
                sb.Append("</tr>");

                i++;
                if (i > 5000)
                {
                    bExcede = true;
                    break;
                }
            }
            dr.Close();
            dr.Dispose();

            if (!bExcede)
            {
                sb.Append("</table>");
            }
            else
            {
                sb.Length = 0;
                sb.Append("EXCEDE");
            }
            return sb.ToString();
        }
        public static string ObtenerSolicitudesAmbito(
            string sOpcion,
            string sMotivos,
            string nDesde,
            string nHasta,
            string t420_concepto,
            string t305_idproyectosubnodo,
            string t420_idreferencia,
            string t001_idficepi_interesado,
            string t303_idnodo_proyecto,
            string t001_responsable_proyecto,
            string t302_idcliente_proyecto
            )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='MA' style='width:980px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:30px;;' />");
            sb.Append(" <col style='width:20px; ' />");
            sb.Append(" <col style='width:60px; ' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append(" <col style='width:150px;' />");
            sb.Append(" <col style='width:180px;' />");
            sb.Append(" <col style='width:140px;' />");
            sb.Append(" <col style='width:140px;' />");
            sb.Append(" <col style='width:50px;' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append("</colgroup>");


            SqlDataReader dr = DAL.CABECERAGV.ObtenerSolicitudesAmbito(null, sOpcion,
                                        (int)HttpContext.Current.Session["GVT_IDFICEPI"],
                                        sMotivos, int.Parse(nDesde), int.Parse(nHasta), t420_concepto,
                                        (t305_idproyectosubnodo == "") ? null : (int?)int.Parse(t305_idproyectosubnodo),
                                        (t420_idreferencia == "") ? null : (int?)int.Parse(t420_idreferencia),
                                        (t001_idficepi_interesado == "") ? null : (int?)int.Parse(t001_idficepi_interesado),
                                        (t303_idnodo_proyecto == "") ? null : (int?)int.Parse(t303_idnodo_proyecto),
                                        (t001_responsable_proyecto == "") ? null : (int?)int.Parse(t001_responsable_proyecto),
                                        (t302_idcliente_proyecto == "") ? null : (int?)int.Parse(t302_idcliente_proyecto));

            string sTooltip = "", sFecha = "";
            int i = 0;
            bool bExcede = false;

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t420_idreferencia"].ToString() + "' ");
                sb.Append("tipo='" + dr["TipoNota"].ToString() + "' ");
                sb.Append("estado='" + dr["t431_idestado"].ToString() + "' ");
                sb.Append("idcr='" + dr["t303_idnodo"].ToString() + "' ");
                sb.Append("cr='" + dr["t303_denominacion"].ToString() + "' ");
                sb.Append("style='height:20px;' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle;' />&nbsp;&nbsp;Informaci&oacute;n] ");
                sTooltip = "<label style='width:100px'>Solicitante:</label>" + dr["Solicitante"].ToString();
                sTooltip += "<br><label style='width:100px'>" + ((dr["t001_sexo_interesado"].ToString() == "V") ? "Beneficiario" : "Beneficiaria") + ":</label>" + dr["Interesado"].ToString();
                sTooltip += "<br><label style='width:100px'>Concepto:</label>" + dr["t420_concepto"].ToString();
                sTooltip += "<br><label style='width:100px'>Moneda:</label>" + dr["t422_denominacion"].ToString();
                //if (dr["t422_idmoneda"].ToString() != "EUR")
                //{
                //    if (dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C" || dr["t431_idestado"].ToString() == "S")
                //        sTooltip += "<br><label style='width:100px'>Importe &euro;:</label>" + decimal.Parse(dr["TOTALEUROS"].ToString()).ToString("N");
                //}

                switch (dr["t431_idestado"].ToString())
                {
                    case "A":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "B":
                        sFecha = (dr["t420_fNoaprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fNoaprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>No aprobada por:</label>" + dr["NoAprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "L":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fAceptada"] != DBNull.Value) ? ((DateTime)dr["t420_fAceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aceptada por:</label>" + dr["AceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "O":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fNoaceptada"] != DBNull.Value) ? ((DateTime)dr["t420_fNoaceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>No aceptada por:</label>" + dr["NoAceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "X":
                        sFecha = (dr["t420_fAnulada"] != DBNull.Value) ? ((DateTime)dr["t420_fAnulada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Anulada por:</label>" + dr["AnuladaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "C":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fAceptada"] != DBNull.Value) ? ((DateTime)dr["t420_fAceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aceptada por:</label>" + dr["AceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fContabilizada"] != DBNull.Value) ? ((DateTime)dr["t420_fContabilizada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:115px'>Fecha contabilización:</label>" + sFecha.Substring(0, sFecha.Length - 3);
                        break;
                    case "S":
                        sFecha = (dr["t420_fAprobada"] != DBNull.Value) ? ((DateTime)dr["t420_fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fAceptada"] != DBNull.Value) ? ((DateTime)dr["t420_fAceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aceptada por:</label>" + dr["AceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["t420_fContabilizada"] != DBNull.Value) ? ((DateTime)dr["t420_fContabilizada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:115px'>Fecha contabilización:</label>" + sFecha.Substring(0, sFecha.Length - 3);
                        sFecha = (dr["t420_fPagada"] != DBNull.Value) ? ((DateTime)dr["t420_fPagada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:115px'>Fecha pagada:</label>" + sFecha.Substring(0, sFecha.Length - 3);
                        break;
                }
                //if (dr["t431_idestado"].ToString() != "P")
                //{//si no están aparcadas
                //    if (dr["t422_idmoneda"].ToString() == "EUR" || dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C" || dr["t431_idestado"].ToString() == "S")
                //    {
                //        sTooltip += "<br><label style='width:115px;'>A cobrar sin retención:</label><label style='width:50px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_SINRETENCION"].ToString()).ToString("N") + "</label> " + dr["t422_idmoneda"].ToString();
                //        sTooltip += "<br><label style='width:115px;'>A cobrar en nómina:</label><label style='width:50px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_NOMINA"].ToString()).ToString("N") + "</label> EUR";
                //    }
                //}
                if (dr["t431_idestado"].ToString() != "P")
                {//si no están aparcadas
                    sTooltip += "<br><label style='width:115px;'>A cobrar sin retención:</label><label style='width:50px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_SINRETENCION_EUROS"].ToString()).ToString("N") + "</label> EUR";
                    if (dr["t422_idmoneda"].ToString() != "EUR" && (dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C" || dr["t431_idestado"].ToString() == "S"))
                        sTooltip += "<label style='width:60px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_SINRETENCION"].ToString()).ToString("N") + "</label> " + dr["t422_idmoneda"].ToString();

                    if (dr["t422_idmoneda"].ToString() == "EUR" || dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C" || dr["t431_idestado"].ToString() == "S")
                    {
                        sTooltip += "<br><label style='width:115px;'>A cobrar en nómina:</label><label style='width:50px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_NOMINA"].ToString()).ToString("N") + "</label> EUR";
                    }
                }
                if (dr["t301_idproyecto"].ToString() != "")
                {
                    sTooltip += "<br><label style='width:100px;'>C.R.:</label>" + dr["t303_denominacion"].ToString() + "";
                }
                sb.Append("body=[" + Utilidades.CadenaParaTooltipExtendido(sTooltip) + "]\" ");
                //sb.Append("ondblclick='md(this);' "); //SE Pone con el scroll
                sb.Append(">");
                sb.Append("<td style='padding-left:10px'><input type='checkbox' class='check' checked></td>");
                //sb.Append("<td><img src='../../../images/imgTipo" + dr["TipoNota"].ToString() + ".gif'></td>");
                sb.Append("<td style='text-align:center;'></td>");
                if (dr["t431_idestado"].ToString() != "P") //Si la nota no está aparcada
                    sb.Append("<td style='text-align:right; padding-right:10px;'>" + ((int)dr["t420_idreferencia"]).ToString("#,###") + "</td>");
                else
                    sb.Append("<td></td>");
                sb.Append("<td " + ((dr["t431_idestado"].ToString() == "B" || dr["t431_idestado"].ToString() == "O") ? "style='color:#ff0000'" : "") + ">" + dr["t431_denominacion"].ToString() + "</td>");
                if (dr["t420_fTramitada"] != DBNull.Value)
                    sb.Append("<td>" + ((DateTime)dr["t420_fTramitada"]).ToShortDateString() + "</td>");
                else
                    sb.Append("<td></td>");
                //sb.Append("<td><nobr class='NBR W140'>" + dr["Interesado"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["Interesado"].ToString() + "</td>");
                //sb.Append("<td><nobr class='NBR W170'>" + dr["t420_concepto"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["t420_concepto"].ToString() + "</td>");
                sb.Append("<td>" + dr["t423_denominacion"].ToString() + "</td>");
                //sb.Append("<td><nobr class='NBR W130'>");
                if (dr["t301_idproyecto"].ToString() != "")
                {
                    sb.Append("<td title="+ dr["t303_denominacion"].ToString() + ">");
                    sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - ");
                    sb.Append(dr["t301_denominacion"].ToString());
                    sb.Append("</td>");
                }
                else
                {
                    sb.Append("<td></td>");
                }
                //sb.Append("<td>");
                //if (dr["t301_idproyecto"].ToString() != "")
                //    sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - ");

                //sb.Append(dr["t301_denominacion"].ToString());
                //sb.Append("</td>");

                sb.Append("<td>" + dr["t422_idmoneda"].ToString() + "</td>");
                sb.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["TOTALVIAJE"].ToString()).ToString("N") + "</td>");
                sb.Append("</tr>");

                i++;
                if (i > 5000)
                {
                    bExcede = true;
                    break;
                }
            }
            dr.Close();
            dr.Dispose();

            if (!bExcede)
            {
                sb.Append("</table>");
            }
            else
            {
                sb.Length = 0;
                sb.Append("EXCEDE");
            }
            return sb.ToString();
        }

        public static object[] ObtenerDireccionesCorreo(int t420_idreferencia)
        {
            object[] aDatos = new object[6];

            SqlDataReader dr = DAL.CABECERAGV.ObtenerDireccionesCorreo(null, t420_idreferencia);

            while (dr.Read())
            {
                aDatos[0] = (int)dr["t001_idficepi_solicitante"];
                aDatos[1] = dr["t001_codred_solicitante"].ToString();
                aDatos[2] = dr["Solicitante"].ToString();
                aDatos[3] = (int)dr["t001_idficepi_beneficiario"];
                aDatos[4] = dr["t001_codred_beneficiario"].ToString();
                aDatos[5] = dr["Beneficiario"].ToString();
            }
            dr.Close();
            dr.Dispose();

            return aDatos;
        }

        public static void EnviarCorreoAceptador(string sReferencia, string sDestinatarios, string sTextoCorreo)
        {
            #region Envío de correo

            string[] aMail = { "Mensaje de "+ HttpContext.Current.Session["GVT_PROFESIONAL_ENTRADA"].ToString(), 
                                 "<BR>"+ Utilidades.unescape(sTextoCorreo).Replace(((char)13).ToString() + ((char)10).ToString(), "<br>") + "<br><br>", 
                                 sDestinatarios };
            ArrayList aListCorreo = new ArrayList();
            aListCorreo.Add(aMail);

            if (aListCorreo.Count > 0)
                Correo.EnviarCorreos(aListCorreo);
            #endregion

            #region Actualización del historial
            DAL.CRONOLOGIAGV.InsertarCorreoAceptador(null, int.Parse(sReferencia), (int)HttpContext.Current.Session["GVT_IDFICEPI_ENTRADA"], Utilidades.unescape(sTextoCorreo));
            #endregion
        }

        public static void CambiarEstado(string sReferencia, string sEstado, string sMotivo)
        {
            string sResul = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;
            int resAnular = 0;


            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
                if (sEstado != "")
                {
                    //Con la cadena generamos una lista y la recorremos para grabar cada elemento
                    //DAL.Tooltips.UpdateTooltips(tr, Utilidades.unescape(sDatos), short.Parse(sOrigen));
                    if (sEstado == "X")
                    {
                        resAnular = AnularAdm(int.Parse(sReferencia), Utilidades.unescape(sMotivo));
                        if (resAnular == 0)
                            throw (new Exception("¡¡¡ Atención !!!\n\nDurante su intervención, otro usuario ha modificado el estado de la solicitud, por lo que la acción no ha podido ser realizada."));
                    }
                    else {
                        DAL.CABECERAGV.UpdateEstado(tr, int.Parse(sReferencia), sEstado);
                    }
                }
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar el cambio de estado.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
        }

        public static string ObtenerVisadores(int t420_idreferencia, string sEstado)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblVisadores' style='width:500px;'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:300px; padding-left:2px;' />");
            sb.Append(" <col style='width:60px;' />");
            sb.Append(" <col style='width:100px;' />");
            sb.Append(" <col style='width:40px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr;
            if (sEstado == "T" || sEstado == "N")
                dr = DAL.CABECERAGV.ObtenerAprobadores(null, t420_idreferencia);
            else
                dr = DAL.CABECERAGV.ObtenerAceptadores(null, t420_idreferencia);

            int i = 0;
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                //sb.Append("cip='" + dr["t001_cip"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                //sb.Append("ondblclick='' ");
                sb.Append("style='height:20px;'>");
                sb.Append("<td>" + dr["Visador"].ToString() + "</td>");
                sb.Append("<td>" + dr["t001_exttel"].ToString() + "</td>");
                sb.Append("<td>" + dr["Jornada"].ToString() + "</td>");
                sb.Append("<td><label class='enlace' style='cursor:pointer' onclick=\"mostrarQEQ('" + dr["t001_cip"].ToString() + "')\">QEQ</label></td>");
                sb.Append("</tr>");
                i++;
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");

            if (i > 1) return "Las personas que pueden " + ((sEstado == "T" || sEstado == "N") ? "aprobarla" : "aceptarla") + " son:@#@" + sb.ToString();
            else return "La persona que ha de " + ((sEstado == "T") ? "aprobarla" : "aceptarla") + " es:@#@" + sb.ToString();
        }

        public static string ObtenerCentroCosteMotivo(string t314_idusuario_interesado, string t423_idmotivo, string t305_idproyectosubnodo)
        {
            string sResul = "";

            SqlDataReader dr = DAL.CABECERAGV.ObtenerCentroCosteMotivo(null, int.Parse(t314_idusuario_interesado),
                                                                        byte.Parse(t423_idmotivo),
                                                                        (t305_idproyectosubnodo=="") ? null : (int?)int.Parse(t305_idproyectosubnodo));
            if (dr.Read())
            {
                sResul += dr["t175_idcc"].ToString() + "{sep}";
                sResul += dr["t175_denominacion"].ToString() + "{sep}";
                sResul += dr["t303_idnodo"].ToString() + "{sep}";
                sResul += dr["t303_denominacion"].ToString();
            }
            dr.Close();
            dr.Dispose();

            return sResul;
        }
        #endregion

    }
}
