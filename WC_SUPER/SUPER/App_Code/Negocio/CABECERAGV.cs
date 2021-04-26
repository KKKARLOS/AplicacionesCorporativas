using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;
using SUPER.Capa_Datos;
using SUPER.Capa_Negocio;

namespace SUPER.BLL
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
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

        private string _t317_idcencos;
        public string t317_idcencos
        {
            get { return _t317_idcencos; }
            set { _t317_idcencos = value; }
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

        private string _t317_denominacion;
        public string t317_denominacion
        {
            get { return _t317_denominacion; }
            set { _t317_denominacion = value; }
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

        private short? _t010_idoficina_base;
        public short? t010_idoficina_base
        {
            get { return _t010_idoficina_base; }
            set { _t010_idoficina_base = value; }
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

            SqlDataReader dr = SUPER.Capa_Datos.CABECERAGV.ObtenerDatosCabecera(null, t420_idreferencia);

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
                if (dr["Interesado"] != DBNull.Value)
                    o.Interesado = (string)dr["Interesado"];
                if (dr["t001_idficepi_aprobadapor"] != DBNull.Value)
                    o.t001_idficepi_aprobadapor = int.Parse(dr["t001_idficepi_aprobadapor"].ToString());
                if (dr["Aprobador"] != DBNull.Value)
                    o.Aprobador = (string)dr["Aprobador"];
                if (dr["t001_idficepi_aceptadapor"] != DBNull.Value)
                    o.t001_idficepi_aceptadapor = int.Parse(dr["t001_idficepi_aceptadapor"].ToString());
                if (dr["Aceptador"] != DBNull.Value)
                    o.Aceptador = (string)dr["Aceptador"];
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
                if (dr["t317_idcencos"] != DBNull.Value)
                    o.t317_idcencos = (string)dr["t317_idcencos"];
                if (dr["t317_denominacion"] != DBNull.Value)
                    o.t317_denominacion = (string)dr["t317_denominacion"];
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

            SqlDataReader dr = SUPER.Capa_Datos.CABECERAGV.ObtenerDatosCabeceraBono(null, t420_idreferencia);

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
                if (dr["Interesado"] != DBNull.Value)
                    o.Interesado = (string)dr["Interesado"];
                if (dr["t001_idficepi_aprobadapor"] != DBNull.Value)
                    o.t001_idficepi_aprobadapor = int.Parse(dr["t001_idficepi_aprobadapor"].ToString());
                if (dr["Aprobador"] != DBNull.Value)
                    o.Aprobador = (string)dr["Aprobador"];
                if (dr["t001_idficepi_aceptadapor"] != DBNull.Value)
                    o.t001_idficepi_aceptadapor = int.Parse(dr["t001_idficepi_aceptadapor"].ToString());
                if (dr["Aceptador"] != DBNull.Value)
                    o.Aceptador = (string)dr["Aceptador"];
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

            SqlDataReader dr = SUPER.Capa_Datos.CABECERAGV.ObtenerDatosCabeceraPago(null, t420_idreferencia);

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
                if (dr["Interesado"] != DBNull.Value)
                    o.Interesado = (string)dr["Interesado"];
                if (dr["t001_idficepi_aprobadapor"] != DBNull.Value)
                    o.t001_idficepi_aprobadapor = int.Parse(dr["t001_idficepi_aprobadapor"].ToString());
                if (dr["Aprobador"] != DBNull.Value)
                    o.Aprobador = (string)dr["Aprobador"];
                if (dr["t001_idficepi_aceptadapor"] != DBNull.Value)
                    o.t001_idficepi_aceptadapor = int.Parse(dr["t001_idficepi_aceptadapor"].ToString());
                if (dr["Aceptador"] != DBNull.Value)
                    o.Aceptador = (string)dr["Aceptador"];
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
            #endregion Datos Posiciones

            #region Abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch (Exception)
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
                    if (aDatosCabecera[11] == "")
                    {
                        nReferencia = SUPER.Capa_Datos.CABECERAGV.InsertarCabeceraPago(tr,
                                                    aDatosCabecera[0],//estado
                                                    Utilidades.unescape(aDatosCabecera[1]),//concepto
                                                    (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"],//idficepi_solicitante
                                                    int.Parse(aDatosCabecera[4]),//idusuario_interesado
                                                    (aDatosCabecera[3] == "") ? null : (int?)int.Parse(aDatosCabecera[3]),//idproyectosubnodo
                                                    aDatosCabecera[9],//moneda
                                                    Utilidades.unescape(aDatosCabecera[5]),//comentarionota
                                                    Utilidades.unescape(aDatosCabecera[6]),//anotaciones
                                                    decimal.Parse(aDatosCabecera[7]),//importe
                                                    byte.Parse(aDatosCabecera[8]),//motivo
                                                    (aDatosCabecera[2] == "") ? null : (int?)int.Parse(aDatosCabecera[2]),//idacuerdogv
                                                    int.Parse(aDatosCabecera[10]), //idempresa
                                                    byte.Parse(aDatosCabecera[12]) // Territorio
                                                    );
                    }
                    else
                    {
                        nReferencia = int.Parse(aDatosCabecera[13]);
                        nFilasModificadas = SUPER.Capa_Datos.CABECERAGV.ModificarCabeceraPago(tr,
                                                    nReferencia,
                                                    aDatosCabecera[0],//estado
                                                    Utilidades.unescape(aDatosCabecera[1]),//concepto
                                                    (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"],//idficepi_solicitante
                                                    int.Parse(aDatosCabecera[4]),//idusuario_interesado
                                                    (aDatosCabecera[3] == "") ? null : (int?)int.Parse(aDatosCabecera[3]),//idproyectosubnodo
                                                    aDatosCabecera[9],//moneda
                                                    Utilidades.unescape(aDatosCabecera[5]),//comentarionota
                                                    Utilidades.unescape(aDatosCabecera[6]),//anotaciones
                                                    decimal.Parse(aDatosCabecera[7]),//importe
                                                    byte.Parse(aDatosCabecera[8]),//motivo
                                                    (aDatosCabecera[2] == "") ? null : (int?)int.Parse(aDatosCabecera[2]),//idacuerdogv
                                                    int.Parse(aDatosCabecera[10]), //idempresa
                                                    byte.Parse(aDatosCabecera[12]) // Territorio                                                                                                                                                            
                                                    );
                        if (nFilasModificadas == 0)
                        {
                            sResul = "Tramitacion anulada";
                            bErrorControlado = true;
                            throw (new Exception(sResul));
                        }
                        string sCentroCoste = SUPER.Capa_Datos.CABECERAGV.ObtenerCentroCoste(tr, nReferencia);
                        if (sCentroCoste == "")
                        {
                            sResul = "Operación denegada.\n\nNo se ha podido determinar el centro de coste a asociar a la solicitud.";
                            bErrorControlado = true;
                            throw (new Exception(sResul));
                        }
                        else
                        {
                            SUPER.Capa_Datos.CABECERAGV.UpdateCentroCoste(tr, nReferencia, sCentroCoste);
                        }
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
            catch (Exception)
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

                    if (aDatosCabecera[34] == "" || aDatosCabecera[34] == "P")
                    {
                        nReferencia = SUPER.Capa_Datos.CABECERAGV.InsertarCabecera(tr,
                                                    aDatosCabecera[0],
                                                    Utilidades.unescape(aDatosCabecera[1]),
                                                    (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"],
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
                                                    null
                                                    );
                    }
                    else
                    {
                        bExisteNota = true;
                        nReferencia = int.Parse(aDatosCabecera[33]);
                        nFilasModificadas = SUPER.Capa_Datos.CABECERAGV.ModificarCabecera(tr, nReferencia,
                                                    aDatosCabecera[0],
                                                    Utilidades.unescape(aDatosCabecera[1]),
                                                    (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"],
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
                                                    short.Parse(aDatosCabecera[32])
                                                    );
                        if (nFilasModificadas == 0)
                        {
                            sResul = "Tramitacion anulada";
                            bErrorControlado = true;
                            throw (new Exception(sResul));
                        }
                        string sCentroCoste = SUPER.Capa_Datos.CABECERAGV.ObtenerCentroCoste(tr, nReferencia);
                        if (sCentroCoste == "")
                        {
                            sResul = "Operación denegada.\n\nNo se ha podido determinar el centro de coste a asociar a la solicitud.";
                            bErrorControlado = true;
                            throw (new Exception(sResul));
                        }
                        else
                        {
                            SUPER.Capa_Datos.CABECERAGV.UpdateCentroCoste(tr, nReferencia, sCentroCoste);
                        }
                    }

                    //Si el beneficiario es autorresponsable, comprobaciones de motivos, excepciones, etc, para
                    //pasar la nota a aprobada.
                    if (aDatosCabecera[35] == "1")
                    {
                        SUPER.Capa_Datos.CABECERAGV.GestionarAutorresponsabilidad(tr, nReferencia);
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
                        SUPER.Capa_Datos.POSICIONGV.DeleteByT420_idreferencia(tr, nReferencia);
                    }

                    #region Insertar Posiciones

                    Hashtable htAnnoGasto = AnnoGasto.ObtenerHTAnnoGasto(tr);
                    Hashtable htUsuarioPSN = USUARIOPROYECTOSUBNODOGV.ObtenerFechasAsociacionPSN(tr, int.Parse(aDatosCabecera[2]));

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


                        SUPER.Capa_Datos.POSICIONGV.InsertarPosicion(tr, nReferencia,
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
            int nReferencia = 0;//, nFilasModificadas = 0;
            int? nIDLote = null;
            SqlConnection oConn = null;
            SqlTransaction tr = null;
            bool bErrorControlado = false;

            string[] aPSN = Regex.Split(sProyectosSubnodos, ",");

            string[] aDatosCabecera = Regex.Split(strDatosCabecera, "#sep#");
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
            catch (Exception)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
                Hashtable htAnnoGasto = AnnoGasto.ObtenerHTAnnoGasto(tr);
                Hashtable htUsuarioPSN = USUARIOPROYECTOSUBNODOGV.ObtenerFechasAsociacionPSN(tr, int.Parse(aDatosCabecera[2]));

                foreach (string sPSN in aPSN)
                {
                    try
                    {
                        #region Insertar Cabecera
                        nReferencia = SUPER.Capa_Datos.CABECERAGV.InsertarCabecera(tr,
                                            aDatosCabecera[0],
                                            Utilidades.unescape(aDatosCabecera[1]),
                                            (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"],
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
                                            nIDLote
                                            );


                        if (nIDLote == null && aPSN.Length > 1)
                        {
                            nIDLote = nReferencia;
                            SUPER.Capa_Datos.CABECERAGV.UpdateLote(tr, nReferencia, (int)nIDLote);
                        }

                        string sCentroCoste = SUPER.Capa_Datos.CABECERAGV.ObtenerCentroCoste(tr, nReferencia);
                        if (sCentroCoste == "")
                        {
                            sResul = "Operación denegada.\n\nNo se ha podido determinar el centro de coste a asociar a la solicitud.";
                            bErrorControlado = true;
                            throw (new Exception(sResul));
                        }
                        else
                        {
                            SUPER.Capa_Datos.CABECERAGV.UpdateCentroCoste(tr, nReferencia, sCentroCoste);
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

                            SUPER.Capa_Datos.POSICIONGV.InsertarPosicion(tr, nReferencia,
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
            sb.Append(" <col style='width:20px;' />");
            sb.Append(" <col style='width:60px;' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append(" <col style='width:200px;' />");
            sb.Append(" <col style='width:180px;' />");
            sb.Append(" <col style='width:140px;' />");
            sb.Append(" <col style='width:140px;' />");
            sb.Append(" <col style='width:70px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = SUPER.Capa_Datos.CABECERAGV.ObtenerNotasAbiertasYRecientes(null, t001_idficepi);
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
                if (dr["t422_idmoneda"].ToString() != "EUR")
                {
                    sTooltip += "<br><label style='width:100px'>Moneda:</label>" + dr["t422_denominacion"].ToString();
                    if (dr["t431_idestado"].ToString() != "L" && dr["t431_idestado"].ToString() != "C" || dr["t431_idestado"].ToString() != "S")
                        sTooltip += "<br><label style='width:100px'>Importe:</label>" + decimal.Parse(dr["TOTALEUROS"].ToString()).ToString("N");
                }
                switch (dr["t431_idestado"].ToString())
                {
                    case "A":
                        sFecha = (dr["fAprobada"] != DBNull.Value) ? ((DateTime)dr["fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "B":
                        sFecha = (dr["fNoAprobada"] != DBNull.Value) ? ((DateTime)dr["fNoAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>No aprobada por:</label>" + dr["NoAprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "L":
                        sFecha = (dr["fAprobada"] != DBNull.Value) ? ((DateTime)dr["fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["fAceptada"] != DBNull.Value) ? ((DateTime)dr["fAceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aceptada por:</label>" + dr["AceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "O":
                        sFecha = (dr["fAprobada"] != DBNull.Value) ? ((DateTime)dr["fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["fNoAceptada"] != DBNull.Value) ? ((DateTime)dr["fNoAceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>No aceptada por:</label>" + dr["NoAceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "X":
                        sFecha = (dr["fAnulada"] != DBNull.Value) ? ((DateTime)dr["fAnulada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Anulada por:</label>" + dr["AnuladaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        break;
                    case "C":
                        sFecha = (dr["fAprobada"] != DBNull.Value) ? ((DateTime)dr["fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["fAceptada"] != DBNull.Value) ? ((DateTime)dr["fAceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aceptada por:</label>" + dr["AceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["fContabilizada"] != DBNull.Value) ? ((DateTime)dr["fContabilizada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:115px'>Fecha contabilización:</label>" + sFecha.Substring(0, sFecha.Length - 3);
                        break;
                    case "S":
                        sFecha = (dr["fAprobada"] != DBNull.Value) ? ((DateTime)dr["fAprobada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["fAceptada"] != DBNull.Value) ? ((DateTime)dr["fAceptada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:100px'>Aceptada por:</label>" + dr["AceptadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                        sFecha = (dr["fContabilizada"] != DBNull.Value) ? ((DateTime)dr["fContabilizada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:115px'>Fecha contabilización:</label>" + sFecha.Substring(0, sFecha.Length - 3);
                        sFecha = (dr["fPagada"] != DBNull.Value) ? ((DateTime)dr["fPagada"]).ToString() : "   ";
                        sTooltip += "<br><label style='width:115px'>Fecha pagada:</label>" + sFecha.Substring(0, sFecha.Length - 3);
                        break;
                }
                if (dr["t431_idestado"].ToString() != "P")
                {//si no están aparcadas
                    if (dr["t422_idmoneda"].ToString() == "EUR" || dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C"  || dr["t431_idestado"].ToString() == "S" )
                    {
                        sTooltip += "<br><label style='width:115px;'>A cobrar sin retención:</label><label style='width:50px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_SINRETENCION"].ToString()).ToString("N") + "</label> &euro;";
                        sTooltip += "<br><label style='width:115px;'>A cobrar en nómina:</label><label style='width:50px;text-align:right;font-weight:bold;'>" + double.Parse(dr["ACOBRAR_NOMINA"].ToString()).ToString("N") + "</label> &euro;";
                    }
                }

                sb.Append("body=[" + Utilidades.CadenaParaTooltipExtendido(sTooltip) + "]\" ");
                sb.Append("ondblclick='md(this);'");
                sb.Append(">");
                sb.Append("<td style='text-align:center;'><img src='../../images/imgTipo" + dr["TipoNota"].ToString() + ".gif'></td>");
                if (dr["t431_idestado"].ToString() != "P") //Si la nota no está aparcada
                    sb.Append("<td style='text-align:right; padding-right:10px;'>" + dr["t420_idreferencia"].ToString() + "</td>");
                else
                    sb.Append("<td></td>");
                sb.Append("<td>" + dr["t431_denominacion"].ToString() + "</td>");
                if (dr["FTramitada"] != DBNull.Value)
                    sb.Append("<td>" + ((DateTime)dr["FTramitada"]).ToShortDateString() + "</td>");
                else
                    sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W190'>" + dr["Interesado"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W170'>" + dr["t420_concepto"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["t423_denominacion"].ToString() + "</td>");
                sb.Append("<td><nobr class='NBR W130'>");
                if (dr["t301_idproyecto"].ToString() != "")
                    sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - ");

                sb.Append(dr["t301_denominacion"].ToString() + "</nobr></td>");
                if (dr["t422_idmoneda"].ToString() == "EUR" || dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C" || dr["t431_idestado"].ToString() == "S")
                {
                    sb.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["TOTALEUROS"].ToString()).ToString("N") + "</td>");
                }
                else
                {
                    sb.Append("<td style='text-align:right; padding-right:2px;'>?</td>");
                }
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }
        public static bool ExistenNotasBloqueantes(int t001_idficepi)
        {
            bool bBloqueantes = false;

            SqlDataReader dr = SUPER.Capa_Datos.CABECERAGV.ObtenerNotasAbiertasYRecientes(null, t001_idficepi);

            while (dr.Read())
            {
                if (dr["t431_idestado"].ToString() == "B" || dr["t431_idestado"].ToString() == "O")
                {
                    bBloqueantes = true;
                    break;
                }
            }
            dr.Close();
            dr.Dispose();

            return bBloqueantes;
        }

        public static int RecuperarNotaEstandar(int t420_idreferencia)
        {
            return SUPER.Capa_Datos.CABECERAGV.RecuperarNotaEstandar(null, t420_idreferencia);
        }

        public static int RecuperarBono(int t420_idreferencia)
        {
            return SUPER.Capa_Datos.CABECERAGV.RecuperarBono(null, t420_idreferencia);
        }

        public static int RecuperarPago(int t420_idreferencia)
        {
            return SUPER.Capa_Datos.CABECERAGV.RecuperarPago(null, t420_idreferencia);
        }

        public static string ObtenerHistorial(int t420_idreferencia)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatosHistorial' style='width:610px;'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:105px;' />");
            sb.Append(" <col style='width:105px;' />");
            sb.Append(" <col style='width:400px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = SUPER.Capa_Datos.CABECERAGV.ObtenerHistorial(null, t420_idreferencia);
            int i = 0;
            string sFecha = "";
            while (dr.Read())
            {
                sb.Append("<tr style='vertical-align:top;' ");
                if (i % 2 == 0) sb.Append("class='FA' ");
                else sb.Append("class='FB' ");
                //color pijama
                sb.Append(">");
                sb.Append("<td style='padding-left:3px;'>" + dr["t431_denominacion"].ToString() + "</td>");
                sFecha = ((DateTime)dr["t659_fecha"]).ToString();
                sb.Append("<td>" + sFecha.Substring(0, sFecha.Length - 3) + "</td>");

                sb.Append("<td style='padding-right:3px;'>" + dr["Profesional"].ToString());
                if (dr["t659_motivo"].ToString() != "")
                {
                    sb.Append("<br><span style='width:350px; margin-left:30px;'>" + dr["t659_motivo"].ToString() + "</span>");
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
            sbNotas.Append("<col style='width:30px;' />");
            sbNotas.Append("<col style='width:20px;' />");
            sbNotas.Append("<col style='width:26px;' />");
            sbNotas.Append("<col style='width:70px;' />");
            sbNotas.Append("<col style='width:165px;' />");
            sbNotas.Append("<col style='width:154px;' />");
            sbNotas.Append("<col style='width:120px;' />");
            sbNotas.Append("<col style='width:20px;' />");
            sbNotas.Append("<col style='width:110px;' />");
            sbNotas.Append("<col style='width:70px;' />");
            sbNotas.Append("</colgroup>");

            sbBonos.Append("<table id='tblDatosBonoTrans' class='MA' style='width:370px;'>");
            sbBonos.Append("<colgroup>");
            sbBonos.Append("<col style='width:40px;' />");
            sbBonos.Append("<col style='width:60px;' />");
            sbBonos.Append("<col style='width:100px;' />");
            sbBonos.Append("<col style='width:95px;' />");
            sbBonos.Append("<col style='width:65px;' />");
            sbBonos.Append("</colgroup>");

            sbPagos.Append("<table id='tblDatosPagosConcertados' class='MA' style='width:370px;'>");
            sbPagos.Append("<colgroup>");
            sbPagos.Append("<col style='width:40px;' />");
            sbPagos.Append("<col style='width:60px;' />");
            sbPagos.Append("<col style='width:195px;' />");
            sbPagos.Append("<col style='width:65px;' />");
            sbPagos.Append("</colgroup>");
            #endregion

            if (sOpcion == "APROBAR")
                dr = SUPER.Capa_Datos.CABECERAGV.ObtenerNotasPendientesAprobar(null, t001_idficepi);
            else //"ACEPTAR"
                dr = SUPER.Capa_Datos.CABECERAGV.ObtenerNotasPendientesAceptar(null, t001_idficepi);
            while (dr.Read())
            {
                sTooltip = "<label style='width:100px'>" + ((dr["t001_sexo_interesado"].ToString() == "V") ? "Beneficiario" : "Beneficiaria") + ":</label>" + dr["Interesado"].ToString();
                sTooltip += "<br><label style='width:100px'>Concepto:</label>" + dr["t420_concepto"].ToString();
                if (sOpcion == "ACEPTAR")
                {
                    sFecha = (dr["fAprobada"] != DBNull.Value) ? ((DateTime)dr["fAprobada"]).ToString() : "   ";
                    sTooltip += "<br><label style='width:100px'>Aprobada por:</label>" + dr["AprobadaPor"].ToString() + " (" + sFecha.Substring(0, sFecha.Length - 3) + ")";
                }
                if (dr["t422_idmoneda"].ToString() != "EUR")
                {
                    sTooltip += "<br><label style='width:100px'>Moneda:</label>" + dr["t422_denominacion"].ToString();
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
                        sbNotas.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle;' />&nbsp;&nbsp;Informaci&oacute;n] ");
                        
                        if (sOpcion == "ACEPTAR")
                        {
                            sbNotas.Append("avisomescerrado='" + dr["avisomescerrado"].ToString() + "' ");
                            sbNotas.Append("feccontab='" + ((DateTime)dr["fec_contabilizacion"]).ToShortDateString() + "' ");

                            if (dr["t420_idreferencia_lote"].ToString() != "")
                            {
                                sTooltip += "<br><label style='width:100px'><b><u>Lote:</u></b></label><span style='backgrounColor:red'>" + int.Parse(dr["t420_idreferencia_lote"].ToString()).ToString("#,###") + "</span>";
                                sTooltip += "<br>" + dr["paquete"].ToString();
                            }
                        }
                        sbNotas.Append("body=[" + Utilidades.CadenaParaTooltipExtendido(sTooltip) + "]\" ");
                        sbNotas.Append("ondblclick='md(this);' ");
                        sbNotas.Append(">");

                        sbNotas.Append("<td style='padding-left:13px;'>");
                        if (sOpcion == "APROBAR" || dr["t422_idmoneda"].ToString() == "EUR")
                            sbNotas.Append("<input type='checkbox' class='check'>");
                        sbNotas.Append("</td>");

                        if (sOpcion == "ACEPTAR")
                        {
                            switch (dr["nIconoJustificante"].ToString())
                            {
                                case "2":
                                    sbNotas.Append("<td style='text-align:center;'><img src='../../images/imgJustCatKO.gif' style='width:16px; height:16px;' title='Solicitud sin justificantes, pero con gastos que los requieren' /></td>"); 
                                    break;
                                case "1":
                                    sbNotas.Append("<td style='text-align:center;'><img src='../../images/imgJustCatOK.gif' style='width:16px; height:16px;' title='Solicitud con justificantes' /></td>"); 
                                    break;
                                default: sbNotas.Append("<td></td>"); break;
                            }

                            switch (dr["nIconoECO"].ToString())
                            {
                                case "2": sbNotas.Append("<td style='text-align:center;'><img src='../../images/imgECOCatReq.gif' style='width:26px; height:16px;' title='Solicitud con kilometraje, pero sin referencia ECO' /></td>"); break;
                                case "1": sbNotas.Append("<td style='text-align:center;'><img src='../../images/imgECOOK.gif' style='width:26px; height:16px;' title='Solicitud con referencia ECO'/></td>"); break;
                                default: sbNotas.Append("<td></td>"); break;
                            }
                        }
                        else
                        {
                            sbNotas.Append("<td></td>");
                            sbNotas.Append("<td></td>");
                        }

                        sbNotas.Append("<td style='text-align:right; padding-right:10px;'>" + int.Parse(dr["t420_idreferencia"].ToString()).ToString("#,###") + "</td>");
                        sbNotas.Append("<td><nobr class='NBR W155'>" + dr["Interesado"].ToString() + "</nobr></td>");
                        sbNotas.Append("<td><nobr class='NBR W145'>" + dr["t420_concepto"].ToString() + "</nobr></td>");
                        sbNotas.Append("<td>" + dr["t423_denominacion"].ToString() + "</td>");

                        if (sOpcion == "ACEPTAR" && dr["t420_idreferencia_lote"].ToString() != "")
                            sbNotas.Append("<td><img src='../../images/imgSolMP.gif' style='width:16px; height:16px;' title='Solicitud multiproyecto' /></td>");
                        else
                            sbNotas.Append("<td></td>");

                        sbNotas.Append("<td><nobr class='NBR W100'>");
                        if (dr["t301_idproyecto"].ToString() != "")
                            sbNotas.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - ");

                        sbNotas.Append(dr["t301_denominacion"].ToString() + "</nobr></td>");

                        if (dr["t422_idmoneda"].ToString() == "EUR" || dr["t431_idestado"].ToString() == "L" || dr["t431_idestado"].ToString() == "C" || dr["t431_idestado"].ToString() == "S")
                        {
                            sbNotas.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["TOTALEUROS"].ToString()).ToString("N") + "</td>");
                        }
                        else
                        {
                            sbNotas.Append("<td style='text-align:right; padding-right:2px;'>?</td>");
                        }
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
                        }
                        sbBonos.Append("style='height:20px;' ");
                        sbBonos.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle;' />&nbsp;&nbsp;Informaci&oacute;n] ");
                        sbBonos.Append("body=[" + Utilidades.CadenaParaTooltipExtendido(sTooltip) + "]\" ");
                        sbBonos.Append("ondblclick='md(this);' >");
                        sbBonos.Append("<td style='text-align:center;'><input type='checkbox' class='check'></td>");
                        sbBonos.Append("<td style='text-align:right; padding-right:10px;'>" + int.Parse(dr["t420_idreferencia"].ToString()).ToString("#,###") + "</td>");
                        sbBonos.Append("<td><nobr class='NBR W90'>" + dr["Interesado"].ToString() + "</nobr></td>");
                        sbBonos.Append("<td><nobr class='NBR W85'>" + dr["MesBono"].ToString() + "</nobr></td>");
                        sbBonos.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["TOTALEUROS"].ToString()).ToString("N") + "</td>");
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
                        }
                        sbPagos.Append("style='height:20px;' ");
                        sbPagos.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle;' />&nbsp;&nbsp;Informaci&oacute;n] ");
                        sbPagos.Append("body=[" + Utilidades.CadenaParaTooltipExtendido(sTooltip) + "]\" ");
                        sbPagos.Append("ondblclick='md(this);'>");
                        sbPagos.Append("<td style='text-align:center;'><input type='checkbox' class='check'></td>");
                        sbPagos.Append("<td style='text-align:right; padding-right:10px;'>" + int.Parse(dr["t420_idreferencia"].ToString()).ToString("#,###") + "</td>");
                        sbPagos.Append("<td><nobr class='NBR W185'>" + dr["Interesado"].ToString() + "</nobr></td>");
                        sbPagos.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["TOTALEUROS"].ToString()).ToString("N") + "</td>");
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
            SUPER.Capa_Datos.CABECERAGV.UpdateAcuerdo(null, int.Parse(sReferencias), int.Parse(sidAcuerdo));
        }

        public static void Aprobar(string sReferencias)
        {
            SUPER.Capa_Datos.CABECERAGV.Aprobar(null, sReferencias);
        }
        public static int NoAprobar(int t420_idreferencia, string t659_motivo)
        {
            return SUPER.Capa_Datos.CABECERAGV.NoAprobar(null, t420_idreferencia, Utilidades.unescape(t659_motivo));
        }

        public static void Aceptar(string sReferenciasYFechas)
        {
            SUPER.Capa_Datos.CABECERAGV.Aceptar(null, sReferenciasYFechas);
        }
        public static int NoAceptar(int t420_idreferencia, string t659_motivo)
        {
            return SUPER.Capa_Datos.CABECERAGV.NoAceptar(null, t420_idreferencia, Utilidades.unescape(t659_motivo));
        }

        public static int Anular(int t420_idreferencia, string t659_motivo)
        {
            return SUPER.Capa_Datos.CABECERAGV.Anular(null, t420_idreferencia, Utilidades.unescape(t659_motivo));
        }

        public static string ObtenerNotasDeUnLote(int t420_idreferencia_lote)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' style='width:750px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:70px;' />");
            sb.Append("    <col style='width:70px;' />");
            sb.Append("    <col style='width:70px;' />");
            sb.Append("    <col style='width:70px;' />");
            sb.Append("    <col style='width:70px;' />");
            sb.Append("    <col style='width:100px;' />");
            sb.Append("    <col style='width:330px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = SUPER.Capa_Datos.CABECERAGV.ObtenerNotasDeUnLote(null, t420_idreferencia_lote);
            string sFecha = "";
            while (dr.Read())
            {
                sb.Append("<tr style='height:20px;'>");
                sb.Append("<td style='padding-right:2px; text-align:right;'>" + int.Parse(dr["t420_idreferencia"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td style='padding-right:2px; text-align:right;'>" + double.Parse(dr["t421_peajepark"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='padding-right:2px; text-align:right;'>" + double.Parse(dr["t421_comida"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='padding-right:2px; text-align:right;'>" + double.Parse(dr["t421_transporte"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='padding-right:2px; text-align:right;'>" + double.Parse(dr["t421_hotel"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='padding-left:5px;'>" + dr["t431_denominacion"].ToString() + "</td>");
                if (dr["fAceptada"] != DBNull.Value)
                {
                    sFecha = ((DateTime)dr["fAceptada"]).ToString();
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

        #endregion

    }
}
