using System.Data.SqlClient;
using System.Text;
using System;
using System.Text.RegularExpressions;
//Para el ArrayList de correos
using System.Collections;
//Para listas genéricas
using System.Collections.Generic;
//pARA ACCEDER AL WEB.CONFIG
using System.Configuration;
using SUPER.DAL;

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de FORANEO
    /// </summary>
    public class FORANEO
    {
        #region Propiedades y Atributos

        private int _t001_idficepi;
        public int t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        private int _t314_idusuario;
        public int t314_idusuario
        {
            get { return _t314_idusuario; }
            set { _t314_idusuario = value; }
        }

        private string _t001_tiporecurso;
        public string t001_tiporecurso
        {
            get { return _t001_tiporecurso; }
            set { _t001_tiporecurso = value; }
        }

        private string _t001_apellido1;
        public string t001_apellido1
        {
            get { return _t001_apellido1; }
            set { _t001_apellido1 = value; }
        }
        private string _t001_apellido2;
        public string t001_apellido2
        {
            get { return _t001_apellido2; }
            set { _t001_apellido2 = value; }
        }
        private string _t001_nombre;
        public string t001_nombre
        {
            get { return _t001_nombre; }
            set { _t001_nombre = value; }
        }

        private string _NombreCompleto;
        public string NombreCompleto
        {
            get { return _NombreCompleto; }
            set { _NombreCompleto = value; }
        }

        private string _t001_cip;
        public string t001_cip
        {
            get { return _t001_cip; }
            set { _t001_cip = value; }
        }

        private string _t001_alias;
        public string t001_alias
        {
            get { return _t001_alias; }
            set { _t001_alias = value; }
        }

        private DateTime _t001_fecalta;
        public DateTime t001_fecalta
        {
            get { return _t001_fecalta; }
            set { _t001_fecalta = value; }
        }
        private DateTime? _t001_fecbaja;
        public DateTime? t001_fecbaja
        {
            get { return _t001_fecbaja; }
            set { _t001_fecbaja = value; }
        }

        private string _t001_email;
        public string t001_email
        {
            get { return _t001_email; }
            set { _t001_email = value; }
        }
        private string _t001_exttel;
        public string t001_exttel
        {
            get { return _t001_exttel; }
            set { _t001_exttel = value; }
        }

        private int _t066_idcal;
        public int t066_idcal
        {
            get { return _t066_idcal; }
            set { _t066_idcal = value; }
        }

        private string _t001_sexo;
        public string t001_sexo
        {
            get { return _t001_sexo; }
            set { _t001_sexo = value; }
        }

        private string _t080_passw;
        public string t080_passw
        {
            get { return _t080_passw; }
            set { _t080_passw = value; }
        }

        private byte _t080_nintentos;
        public byte t080_nintentos
        {
            get { return _t080_nintentos; }
            set { _t080_nintentos = value; }
        }

        private string _t080_pregunta;
        public string t080_pregunta
        {
            get { return _t080_pregunta; }
            set { _t080_pregunta = value; }
        }

        private string _t080_respuesta;
        public string t080_respuesta
        {
            get { return _t080_respuesta; }
            set { _t080_respuesta = value; }
        }

        private string _NombreCompletoProm;
        public string NombreCompletoProm
        {
            get { return _NombreCompletoProm; }
            set { _NombreCompletoProm = value; }
        }

        private DateTime? _t080_falta;
        public DateTime? t080_falta
        {
            get { return _t080_falta; }
            set { _t080_falta = value; }
        }

        private DateTime? _t080_fultacc;
        public DateTime? t080_fultacc
        {
            get { return _t080_fultacc; }
            set { _t080_fultacc = value; }
        }

        private DateTime? _t080_facep;
        public DateTime? t080_facep
        {
            get { return _t080_facep; }
            set { _t080_facep = value; }
        }

        private string _t066_descal;
        public string t066_descal
        {
            get { return _t066_descal; }
            set { _t066_descal = value; }
        }

        private int _Njorlabcal;
        public int Njorlabcal
        {
            get { return _Njorlabcal; }
            set { _Njorlabcal = value; }
        }

        private decimal _t314_costejornada;
        public decimal t314_costejornada
        {
            get { return _t314_costejornada; }
            set { _t314_costejornada = value; }
        }

        private decimal _t314_costehora;
        public decimal t314_costehora
        {
            get { return _t314_costehora; }
            set { _t314_costehora = value; }
        }

        private string _t422_idmoneda;
        public string t422_idmoneda
        {
            get { return _t422_idmoneda; }
            set { _t422_idmoneda = value; }
        }
        private string _MonedaCoste;
        public string MonedaCoste
        {
            get { return _MonedaCoste; }
            set { _MonedaCoste = value; }
        }

        private bool _t314_accesohabilitado;
        public bool t314_accesohabilitado
        {
            get { return _t314_accesohabilitado; }
            set { _t314_accesohabilitado = value; }
        }
        private string _t314_alias;
        public string t314_alias
        {
            get { return _t314_alias; }
            set { _t314_alias = value; }
        }
        private DateTime _t314_falta;
        public DateTime t314_falta
        {
            get { return _t314_falta; }
            set { _t314_falta = value; }
        }
        private DateTime? _t314_fbaja;
        public DateTime? t314_fbaja
        {
            get { return _t314_fbaja; }
            set { _t314_fbaja = value; }
        }
        private bool _t314_calculoJA;
        public bool t314_calculoJA
        {
            get { return _t314_calculoJA; }
            set { _t314_calculoJA = value; }
        }
        private bool _t314_controlhuecos;
        public bool t314_controlhuecos
        {
            get { return _t314_controlhuecos; }
            set { _t314_controlhuecos = value; }
        }

        private bool _t314_mailiap;
        public bool t314_mailiap
        {
            get { return _t314_mailiap; }
            set { _t314_mailiap = value; }
        }

        private DateTime? _fultImpIAP;
        public DateTime? fultImpIAP
        {
            get { return _fultImpIAP; }
            set { _fultImpIAP = value; }
        }
        #endregion

        //    public FORANEO()
        //    {
        //        //
        //        // TODO: Agregar aquí la lógica del constructor
        //        //
        //    }

        /// <summary>
        /// Crea un usuario SUPER para el foráneo
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t001_idficepi"></param>
        /// <returns></returns>
        public static int InsertarEnSuper(SqlTransaction tr, int t001_idficepi, string t314_alias, DateTime t314_falta)
        {
            //decimal dCoste = (decimal)0.0001;
            //El 28/04/2014 Víctor dice que el coste debe ser cero
            decimal dCoste = 0;
            return SUPER.Capa_Negocio.USUARIO.InsertarForaneo(tr, t001_idficepi, t314_alias, t314_falta, false, false, dCoste, dCoste, false, "EUR", true);
        }
        /// <summary>
        /// Guarda datos del foráneo en la tabla T080_FICEPIFORANEO
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t001_idficepi"></param>
        /// <returns></returns>
        public static void InsertarEnFicepiForaneo(SqlTransaction tr, int t001_idficepi, string sPassw)
        {
            int idFicepiPromotor = int.Parse(System.Web.HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString());
            SUPER.DAL.FICEPIFORANEO.InsertarFicepiForaneo(tr, t001_idficepi, idFicepiPromotor, SUPER.BLL.Seguridad.Encriptar(sPassw.ToUpper()));
        }

        /// <summary>
        /// Busca si existe en FICEPI un profesional con ese CIP
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t001_cip"></param>
        /// <returns></returns>
        public static FORANEO GetByNif(SqlTransaction tr, string t001_cip)
        {
            FORANEO o = new FORANEO();
            SqlDataReader dr = SUPER.DAL.FORANEO.GetByNif(tr, t001_cip);
            if (dr.Read())
            {
                o.t001_cip = t001_cip;
                if (dr["t001_idficepi"] != DBNull.Value)
                    o.t001_idficepi = int.Parse(dr["t001_idficepi"].ToString());
                //if (dr["t314_idusuario"] != DBNull.Value)
                //    o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
                if (dr["t001_sexo"] != DBNull.Value)
                    o.t001_sexo = dr["t001_sexo"].ToString();
                if (dr["t001_tiporecurso"] != DBNull.Value)
                    o.t001_tiporecurso = dr["t001_tiporecurso"].ToString();
                if (dr["t001_apellido1"] != DBNull.Value)
                    o.t001_apellido1 = dr["t001_apellido1"].ToString().ToUpper();
                if (dr["t001_apellido2"] != DBNull.Value)
                    o.t001_apellido2 = dr["t001_apellido2"].ToString().ToUpper();
                if (dr["t001_nombre"] != DBNull.Value)
                    o.t001_nombre = dr["t001_nombre"].ToString().ToUpper();
                if (dr["Profesional"] != DBNull.Value)
                    o.NombreCompleto = dr["Profesional"].ToString().ToUpper();
                if (dr["t001_alias"] != DBNull.Value)
                    o.t001_alias = dr["t001_alias"].ToString().ToUpper();
                if (dr["t001_email"] != DBNull.Value)
                    o.t001_email = dr["t001_email"].ToString();
                if (dr["t001_exttel"] != DBNull.Value)
                    o.t001_exttel = dr["t001_exttel"].ToString();
                if (dr["t066_idcal"] != DBNull.Value)
                    o.t066_idcal = (int)dr["t066_idcal"];
                if (dr["t001_fecalta"] != DBNull.Value)
                    o.t001_fecalta = DateTime.Parse(dr["t001_fecalta"].ToString());
                if (dr["t001_fecbaja"] != DBNull.Value)
                    o.t001_fecbaja = DateTime.Parse(dr["t001_fecbaja"].ToString());
                else
                    o.t001_fecbaja = null;
                if (dr["t080_facep"] != DBNull.Value)
                    o.t080_facep = DateTime.Parse(dr["t080_facep"].ToString());
                else
                    o.t080_facep = null;

                if (dr["t080_passw"] != DBNull.Value)
                    o.t080_passw = SUPER.BLL.Seguridad.DesEncriptar(dr["t080_passw"].ToString());
                if (dr["t314_accesohabilitado"] != DBNull.Value)
                    o.t314_accesohabilitado = (bool)dr["t314_accesohabilitado"];
                if (dr["t080_nintentos"] != DBNull.Value)
                    o.t080_nintentos = byte.Parse(dr["t080_nintentos"].ToString());
                if (dr["t080_pregunta"] != DBNull.Value)
                    o.t080_pregunta = SUPER.BLL.Seguridad.DesEncriptar(dr["t080_pregunta"].ToString());
                if (dr["t080_respuesta"] != DBNull.Value)
                    o.t080_respuesta = SUPER.BLL.Seguridad.DesEncriptar(dr["t080_respuesta"].ToString());
            }
            else
            {
                dr.Close();
                dr.Dispose();
                //throw (new NullReferenceException("No se ha obtenido ningun dato de Experiencia profesional"));
                o.t001_idficepi = -1;
            }

            dr.Close();
            dr.Dispose();
            return o;
        }
        /// <summary>
        /// Busca si existe en FICEPI un profesional con ese nombre y apellidos
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t001_cip"></param>
        /// <returns></returns>
        private static FORANEO GetByNombre(SqlTransaction tr, string t001_ape1, string t001_ape2, string t001_nom)
        {
            string t001_apellido1 = t001_ape1;
            string t001_apellido2 = t001_ape2;
            string t001_nombre = t001_nom;
            FORANEO o = new FORANEO();
            SqlDataReader dr = SUPER.DAL.FORANEO.GetByNombre(tr, t001_apellido1, t001_apellido2, t001_nombre);
            if (dr.Read())
            {
                if (dr["t001_cip"] != DBNull.Value)
                    o.t001_cip = dr["t001_cip"].ToString().ToUpper();
                if (dr["t001_idficepi"] != DBNull.Value)
                    o.t001_idficepi = int.Parse(dr["t001_idficepi"].ToString());
                //if (dr["t314_idusuario"] != DBNull.Value)
                //    o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
                if (dr["t001_sexo"] != DBNull.Value)
                    o.t001_sexo = dr["t001_sexo"].ToString();
                if (dr["t001_tiporecurso"] != DBNull.Value)
                    o.t001_tiporecurso = dr["t001_tiporecurso"].ToString();
                o.t001_apellido1 = t001_apellido1.ToUpper();
                o.t001_apellido2 = t001_apellido2.ToUpper();
                o.t001_nombre = t001_nombre.ToUpper();
                if (dr["Profesional"] != DBNull.Value)
                    o.NombreCompleto = dr["Profesional"].ToString().ToUpper();
                if (dr["t001_alias"] != DBNull.Value)
                    o.t001_alias = dr["t001_alias"].ToString().ToUpper();
                if (dr["t001_email"] != DBNull.Value)
                    o.t001_email = dr["t001_email"].ToString();
                if (dr["t001_exttel"] != DBNull.Value)
                    o.t001_exttel = dr["t001_exttel"].ToString();
                if (dr["t066_idcal"] != DBNull.Value)
                    o.t066_idcal = (int)dr["t066_idcal"];
                if (dr["t001_fecalta"] != DBNull.Value)
                    o.t001_fecalta = DateTime.Parse(dr["t001_fecalta"].ToString());
                if (dr["t001_fecbaja"] != DBNull.Value)
                    o.t001_fecbaja = DateTime.Parse(dr["t001_fecbaja"].ToString());
                else
                    o.t001_fecbaja = null;

                if (dr["t080_passw"] != DBNull.Value)
                    o.t080_passw = SUPER.BLL.Seguridad.DesEncriptar(dr["t080_passw"].ToString());
                if (dr["t314_accesohabilitado"] != DBNull.Value)
                    o.t314_accesohabilitado = (bool)dr["t314_accesohabilitado"];
                if (dr["t080_nintentos"] != DBNull.Value)
                    o.t080_nintentos = byte.Parse(dr["t080_nintentos"].ToString());
            }
            else
            {
                dr.Close();
                dr.Dispose();
                //throw (new NullReferenceException("No se ha obtenido ningun dato de Experiencia profesional"));
                o.t001_idficepi = -1;
            }

            dr.Close();
            dr.Dispose();
            return o;
        }

        public static string Grabar(string strDatos)
        {
            int idFicepi = -1, idUser = -1;
            string sAccion = "", sNif = "", sNombre = "", sAlias = "", sTipoError = "", sPassw = "", sNifIni = "", sEmail = "";
            SqlConnection oConn = null;
            SqlTransaction tr;
            //string sElementosInsertados = "";
            //string sDenominacionDelete = "";
            string sResul = "";
            bool bHayError = false;
            ArrayList aListCorreo = new ArrayList();
            //SUPER.Capa_Negocio.cLog miLog = new SUPER.Capa_Negocio.cLog();

            string[] aForaneo = Regex.Split(strDatos, "///");
            foreach (string oForaneo in aForaneo)
            {
                sAlias = "";
                sTipoError = "";
                sPassw = "";
                sNifIni = "";
                sEmail = "";
                string[] aValores = Regex.Split(oForaneo, "##");
                sAccion = aValores[0];
                //0. Opcion BD. "I", "U", "D"
                switch (aValores[0])
                {
                    case "I":
                        #region Comprobaciones
                        sNifIni = SUPER.Capa_Negocio.Utilidades.unescape(aValores[1]).ToUpper();
                        sNif = "FOR_" + sNifIni;
                        #region Compruebo que no haya un NIF de un no foráneo igual de alta (sin prefijo)
                        FORANEO oFor3 = FORANEO.GetByNif(null, sNifIni);
                        if (oFor3.t001_idficepi != -1)
                        {
                            if (oFor3.t001_fecbaja == null)
                                sTipoError = "E6@#@" + oFor3.NombreCompleto;
                            else
                            {
                                if (oFor3.t001_fecbaja > DateTime.Now)
                                    sTipoError = "E6@#@" + oFor3.NombreCompleto;
                                //else
                                //    "El profesional está de baja -> sigo el resto del circuito";
                            }
                        }
                        #endregion
                        if (sTipoError == "")
                        {
                            #region Compruebo que no exista el NIF y que no haya otro nombre igual
                            FORANEO oFor = FORANEO.GetByNif(null, sNif);
                            //Compruebo que no exista otro profesional con los mismos apellidos y nombre
                            FORANEO oFor2 = FORANEO.GetByNombre(null, SUPER.Capa_Negocio.Utilidades.unescape(aValores[2]),
                                                                SUPER.Capa_Negocio.Utilidades.unescape(aValores[3]),
                                                                SUPER.Capa_Negocio.Utilidades.unescape(aValores[4]));
                            if (oFor.t001_idficepi != -1)
                            {
                                #region El NIF del foráneo (incluido su prefijo) ya existe
                                idFicepi = oFor.t001_idficepi;
                                if (oFor2.t001_idficepi != -1)//Y hay un profesional con el mismo nombre y apellido -> no hay que hacer el alta
                                {//Compruebo si hay usuario SUPER de alta
                                    SqlDataReader dr = SUPER.DAL.FORANEO.GetUsuariosSuper(null, idFicepi);
                                    if (!dr.Read())
                                        sTipoError = "E1";//"El profesional existe en FICEPI pero no en SUPER"
                                    else
                                    {
                                        if (dr["t314_fbaja"].ToString() == "")
                                            sTipoError = "E2";//"El profesional existe en FICEPI y está de alta en SUPER"
                                        else
                                        {
                                            if (DateTime.Parse(dr["t314_fbaja"].ToString()) > DateTime.Now)
                                                sTipoError = "E3";//"El profesional existe en FICEPI y va a ser dado de baja en SUPER"
                                            else
                                                sTipoError = "E4";//"El profesional existe en FICEPI pero no hay usuario de alta en SUPER"
                                        }
                                    }
                                }
                                else
                                {//Coincide el NIF pero no el nombre
                                    sTipoError = "E5@#@" + oFor.NombreCompleto;
                                }
                                #endregion
                            }
                            else//El NIF del nuevo no existe
                            {
                                if (oFor2.t001_idficepi != -1)//Pero hay un usuario con el mismo nombre y apellido -> ponemos alias
                                    sAlias = sNifIni;
                            }
                            #endregion
                        }
                        #endregion
                        #region Grabacion
                        if (sTipoError == "")
                        {

                            #region Abrir conexión y transacción
                            try
                            {
                                oConn = SUPER.Capa_Negocio.Conexion.Abrir();
                                tr = SUPER.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
                            }
                            catch (Exception ex)
                            {
                                throw (new Exception("Error al abrir la conexion", ex));
                            }
                            #endregion
                            try
                            {
                                sEmail = SUPER.Capa_Negocio.Utilidades.unescape(aValores[6]);
                                idFicepi = DAL.FORANEO.Insertar(tr, sNif,
                                                            SUPER.Capa_Negocio.Utilidades.unescape(aValores[2]),//Ap1
                                                            SUPER.Capa_Negocio.Utilidades.unescape(aValores[3]),//Ap2
                                                            SUPER.Capa_Negocio.Utilidades.unescape(aValores[4]),//Nombre
                                                            "", //Alias
                                                            aValores[5], //Sexo
                                                            sEmail, int.Parse(aValores[7]),//Calendario
                                                            DateTime.Parse(aValores[8]));//fecha de alta);

                                sPassw = SUPER.Capa_Negocio.Utilidades.GenerarPassw(8).ToUpper();
                                sNombre = SUPER.Capa_Negocio.Utilidades.unescape(aValores[2]).ToUpper() + " " +
                                          SUPER.Capa_Negocio.Utilidades.unescape(aValores[3]).ToUpper() + ", " +
                                          SUPER.Capa_Negocio.Utilidades.unescape(aValores[4]).ToUpper();
                                idUser = InsertarEnSuper(tr, idFicepi, sAlias, DateTime.Parse(aValores[8]));
                                InsertarEnFicepiForaneo(tr, idFicepi, sPassw);
                                EncolarCorreo(aListCorreo, sNif, sNombre, sEmail, sNifIni, sPassw);
                                //para usar la gestión de miebros de ASP.NET
                                //miLog.put("Antes de CrearMiembro. sPassw= " + sPassw);
                                //string sAux= SUPER.Capa_Negocio.Miembros.CrearMiembro(sNifIni, sPassw, sEmail);
                                //miLog.put("CrearMiembro = " + sAux);
                                //////////////////////////////////////////////
                                SUPER.Capa_Negocio.Conexion.CommitTransaccion(tr);
                            }
                            catch (Exception e1)
                            {
                                bHayError = true;
                                sTipoError = e1.Message;
                                SUPER.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                            }
                            finally
                            {
                                SUPER.Capa_Negocio.Conexion.Cerrar(oConn);
                            }
                        }
                        else
                        {
                            bHayError = true;
                            break;
                        }
                        #endregion
                        break;
                    case "U":
                        #region Update
                        idFicepi = int.Parse(aValores[0]);
                        //DAL.FORANEO.Updatear(null, idFicepi, SUPER.Capa_Negocio.Utilidades.unescape(aValores[2]),
                        //                            SUPER.Capa_Negocio.Utilidades.unescape(aValores[3]),
                        //                            SUPER.Capa_Negocio.Utilidades.unescape(aValores[4]),
                        //                            SUPER.Capa_Negocio.Utilidades.unescape(aValores[5]),
                        //                            SUPER.Capa_Negocio.Utilidades.unescape(aValores[6]),
                        //                            DateTime.Parse(aValores[7]), null, aValores[8],
                        //                            SUPER.Capa_Negocio.Utilidades.unescape(aValores[9]),
                        //                            SUPER.Capa_Negocio.Utilidades.unescape(aValores[10]),
                        //                            int.Parse(aValores[12]));
                        #endregion
                        break;
                    case "D":
                        //sDenominacionDelete = aValores[3];
                        //DAL.FORANEO.ModificarBaja(null, int.Parse(aValores[1]), DateTime.Parse(aValores[2]));
                        break;
                }
            }
            //sResul = sElementosInsertados;
            if (bHayError)
                sResul = "Error@#@" + sTipoError;
            else
            {
                sResul = "OK@#@" + idUser.ToString() + "@#@" + sNombre;
                EnviarCorreos(aListCorreo);
            }

            return sResul;
        }
        /// <summary>
        /// Comprueba si existe un usuario foráneo y devuelve su IdFicepi
        /// </summary>
        /// <param name="sUser">NIF  de la persona con el prefijo FOR_</param>
        /// <param name="sPassw"></param>
        /// <returns>-1 si no existe, IdFicepi en caso contrario</returns>
        public static int Validar(string sUser, string sPassw)
        {
            int idFicepi = -1;
            SqlDataReader dr = SUPER.DAL.FORANEO.Validar(null, sUser, SUPER.BLL.Seguridad.Encriptar(sPassw.ToUpper()));
            if (dr.Read())
            {
                idFicepi = int.Parse(dr["t001_idficepi"].ToString());
            }
            dr.Close();
            dr.Dispose();

            return idFicepi;
        }
        /// <summary>
        /// Comprueba si existe un usuario foráneo por Nif y Email y devuelve su IdFicepi
        /// </summary>
        /// <param name="sUser">NIF  de la persona con el prefijo FOR_</param>
        /// <param name="sMail"></param>
        /// <returns>-1 si no existe, IdFicepi en caso contrario</returns>
        public static int ValidarMail(string sUser, string sMail)
        {
            int idFicepi = -1;
            SqlDataReader dr = SUPER.DAL.FORANEO.GetByNif(null, sUser);
            if (dr.Read())
            {
                if (dr["t001_email"].ToString().ToUpper() == sMail.ToUpper())
                    idFicepi = int.Parse(dr["t001_idficepi"].ToString());
            }
            dr.Close();
            dr.Dispose();

            return idFicepi;
        }

        public static void ActualizarPassword(int t001_idficepi, string sPassw)
        {
            SUPER.DAL.FICEPIFORANEO.Updatear(null, t001_idficepi, null, null, null, SUPER.BLL.Seguridad.Encriptar(sPassw), "", "");
        }

        /// <summary>
        /// Genera un correo para el profesional que está dando de alta el foráneo y para los administradores
        /// </summary>
        /// <param name="sNif"></param>
        /// <param name="sNombre"></param>
        /// <param name="sUser"></param>
        /// <param name="sPassw"></param>
        private static void EncolarCorreo(ArrayList aListCorreo, string sNif, string sNombre, string sEmail, string sUser, string sPassw)
        {
            string sAsunto = "Alta de foráneo", sTexto = "", sProy="";
            string sTO = System.Web.HttpContext.Current.Session["IDRED"].ToString();
            StringBuilder sbuilder = new StringBuilder();

            sbuilder.Append(@"<br />SUPER le informa de los datos con los que se ha procedido a dar de alta al profesional foráneo:");

            sbuilder.Append("<br /><br /><label style='width:120px'>Promotor: </label>" + System.Web.HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + "<br>");
            if (System.Web.HttpContext.Current.Session["ID_PROYECTOSUBNODO"].ToString() != "")
            {
                sProy = SUPER.DAL.PROYECTOSUBNODO.GetNombre(int.Parse(System.Web.HttpContext.Current.Session["ID_PROYECTOSUBNODO"].ToString()));
                sbuilder.Append("<label style='width:120px'>Proyecto: </label>" + sProy + "<br>");
            }

            sbuilder.Append("<br /><br /><label style='width:120px'>Nombre: </label>" + sNombre + "<br>");
            //sbuilder.Append("<label style='width:120px'>NIF: </label>" + sNif + "<br>");
            sbuilder.Append("<label style='width:120px'>E-mail: </label>" + sEmail + "<br>");
            sbuilder.Append("<label style='width:120px'>Usuario: </label>" + sUser + "<br>");
            sbuilder.Append("<label style='width:120px'>Contraseña: </label>" + sPassw + "<br>");
            sbuilder.Append("<label style='width:120px'>URL acceso: </label>" + System.Configuration.ConfigurationManager.AppSettings["UrlForaneo"] + "<br>");
            sbuilder.Append("<br /><br />Recuerda que para el acceso a SUPER el navegador debe tener desactivado el bloqueador de ventanas emergentes.");
            sbuilder.Append("<br />Además también es conveniente añadir la URL a los sitios de confianza.");
            sTexto = sbuilder.ToString();
            string[] aMail = { sAsunto, sTexto, sTO };
            aListCorreo.Add(aMail);

            #region Genero correos para los administradores de SUPER. Solicitado en GESTAR 6987 que solo llegeu a CAU-DEF
            //SqlDataReader dr = SUPER.DAL.Administradores.CatalogoSUPER("PC");
            //while (dr.Read())
            //{
            //    if (dr["t001_codred"].ToString() != "")
            //    {
            //        string[] aMail2 = { sAsunto, sTexto, dr["t001_codred"].ToString() };
            //        aListCorreo.Add(aMail2);
            //    }
            //}
            //dr.Close();
            //dr.Dispose();
            sTO = ConfigurationManager.AppSettings["CorreoDEF"].ToString();
            string[] aMail2 = { sAsunto, sTexto, sTO };
            #endregion

        }
        private static void EnviarCorreos(ArrayList aListCorreo)
        {
            SUPER.Capa_Negocio.Correo.EnviarCorreos(aListCorreo);
        }

        public static List<FORANEO> GetLista(string sNif, string t001_ape1, string t001_ape2, string t001_nom)
        {
            List<FORANEO> listaForaneos = null;
            SqlDataReader dr;
            if (sNif != "")
                dr = SUPER.DAL.FORANEO.GetByNif(null, sNif);
            else
                dr = SUPER.DAL.FORANEO.GetByNombre(null, t001_ape1, t001_ape2, t001_nom);
            while (dr.Read())
            {
                FORANEO o = new FORANEO();
                if (dr["t001_idficepi"] != DBNull.Value)
                    o.t001_idficepi = int.Parse(dr["t001_idficepi"].ToString());
                if (dr["t001_cip"] != DBNull.Value)
                    o.t001_cip = dr["t001_cip"].ToString();
                //if (dr["t314_idusuario"] != DBNull.Value)
                //    o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
                if (dr["t001_tiporecurso"] != DBNull.Value)
                    o.t001_tiporecurso = dr["t001_tiporecurso"].ToString();
                if (dr["t001_sexo"] != DBNull.Value)
                    o.t001_sexo = dr["t001_sexo"].ToString();
                if (dr["t001_apellido1"] != DBNull.Value)
                    o.t001_apellido1 = dr["t001_apellido1"].ToString().ToUpper();
                if (dr["t001_apellido2"] != DBNull.Value)
                    o.t001_apellido2 = dr["t001_apellido2"].ToString().ToUpper();
                if (dr["t001_nombre"] != DBNull.Value)
                    o.t001_nombre = dr["t001_nombre"].ToString().ToUpper();
                if (dr["Profesional"] != DBNull.Value)
                    o.NombreCompleto = dr["Profesional"].ToString().ToUpper();
                if (dr["t001_alias"] != DBNull.Value)
                    o.t001_alias = dr["t001_alias"].ToString().ToUpper();
                if (dr["t001_email"] != DBNull.Value)
                    o.t001_email = dr["t001_email"].ToString();
                if (dr["t066_idcal"] != DBNull.Value)
                    o.t066_idcal = (int)dr["t066_idcal"];
                if (dr["t001_fecalta"] != DBNull.Value)
                    o.t001_fecalta = DateTime.Parse(dr["t001_fecalta"].ToString());
                if (dr["t001_fecbaja"] != DBNull.Value)
                    o.t001_fecbaja = DateTime.Parse(dr["t001_fecbaja"].ToString());
                else
                    o.t001_fecbaja = null;

                listaForaneos.Add(o);
            }
            dr.Close();
            dr.Dispose();

            return listaForaneos;
        }

        #region T080_FICEPIFORANEO
        public static void GrabarRecordatorio(string sPregunta, string sRespuesta)
        {
            //int idFicepi = int.Parse(System.Web.HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString());
            int idFicepi = int.Parse(System.Web.HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString());
            //string[] aValores = Regex.Split(strDatos, "##");
            //0. Pregunta
            //1 Respuesta
            DAL.FICEPIFORANEO.Updatear(null, idFicepi, null, DateTime.Now, DateTime.Now, "",
                                       SUPER.Capa_Negocio.Utilidades.unescape(sPregunta),
                                       SUPER.Capa_Negocio.Utilidades.unescape(sRespuesta));
        }
        public static string GetDatosRecordatorio(int t001_idficepi)
        {
            string resul = "";

            SqlDataReader dr = DAL.FICEPIFORANEO.GetDatos(null, t001_idficepi);
            if (dr.Read())
            {
                if (dr["t080_facep"].ToString() != "")
                    resul = "T@#@";
                else
                    resul = "F@#@";
                resul += dr["t080_pregunta"].ToString() + "@#@" + dr["t080_respuesta"].ToString() + "@#@" +
                         SUPER.BLL.Seguridad.DesEncriptar(dr["t080_passw"].ToString());
            }
            dr.Close();
            dr.Dispose();

            return resul;
        }
        public static void RegistrarAcceso(int t001_idficepi)
        {
            SUPER.DAL.FICEPIFORANEO.RegistrarAccesoCorrecto(null, t001_idficepi);
        }
        public static void RegistrarAccesoFallido(int t001_idficepi)
        {
            SUPER.DAL.FICEPIFORANEO.RegistrarAccesoFallido(null, t001_idficepi);
        }
        #endregion

        #region CONSULTAS FORANEOS

        public static string CatalogoConsulta(string apellido1, string apellido2, string nombre, Nullable<int> promotor, int bloqueados)
        {
            SqlDataReader dr = DAL.FORANEO.CatalogoConsulta(null, apellido1, apellido2, nombre, promotor, bloqueados);
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblCatalogo' style='width:960px' mantenimiento ='0' border='0' class='MA' >");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:20px'/>");
            sb.Append("<col style='width:310px'/>");
            sb.Append("<col style='width:290px'/>");
            sb.Append("<col style='width:70px'/>");
            sb.Append("<col style='width:130px;'/>");
            sb.Append("<col style='width:70px;'/>");
            sb.Append("<col style='width:70px;'/>");
            sb.Append("</colgroup>");			

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T001_IDFICEPI"].ToString() + "' sw='0' sexo='" + dr["T001_SEXO"].ToString() + "'>");
                sb.Append("<td></td>");
                sb.Append("<td title='" + dr["Profesional"].ToString() + "'><nobr class='NBR W300'>" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<td title='" + dr["Promotor"].ToString() + "'><nobr class='NBR W280'>" + dr["Promotor"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["t080_falta"].ToString() + "</td>");
                //sb.Append("<td>" + dr["t001_fecalta"].ToString() + "</td>");
                sb.Append("<td>" + dr["t080_fultacc"].ToString() + "</td>");
                sb.Append("<td style='text-align:right'>" + int.Parse(dr["NumProys"].ToString()).ToString("#,##0") + "</td>");
                if ((bool)dr["t314_accesohabilitado"])
                    sb.Append("<td align='center' bloqueado='0'></td>");
                else
                    sb.Append("<td align='center' bloqueado='1'></td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

        public static FORANEO ConsultaSelect(int t001_idficepi)
        {
            FORANEO o = new FORANEO();
            SqlDataReader dr = DAL.FORANEO.ConsultaSelect(null, t001_idficepi);
            if (dr.Read())
            {
                o.t001_idficepi = t001_idficepi;
                //DATOS PERSONALES
                if (dr["t314_idusuario"] != DBNull.Value)
                    o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
                if (dr["Profesional"] != DBNull.Value)
                    o.NombreCompleto = dr["Profesional"].ToString().ToUpper();
                if (dr["t001_apellido1"] != DBNull.Value)
                    o.t001_apellido1 = dr["t001_apellido1"].ToString().ToUpper();
                if (dr["t001_apellido2"] != DBNull.Value)
                    o.t001_apellido2 = dr["t001_apellido2"].ToString().ToUpper();
                if (dr["t001_nombre"] != DBNull.Value)
                    o.t001_nombre = dr["t001_nombre"].ToString().ToUpper();
                if (dr["t001_cip"] != DBNull.Value)
                    o.t001_cip = dr["t001_cip"].ToString().ToUpper();
                if (dr["t001_exttel"] != DBNull.Value)
                    o.t001_exttel = dr["t001_exttel"].ToString();
                if (dr["t001_sexo"] != DBNull.Value)
                    o.t001_sexo = dr["t001_sexo"].ToString();
                if (dr["t001_email"] != DBNull.Value)
                    o.t001_email = dr["t001_email"].ToString();
                //DADTOS EMPRESARIALES
                if (dr["Promotor"] != DBNull.Value)
                    o.NombreCompletoProm = dr["Promotor"].ToString().ToUpper();
                if (dr["t066_descal"] != DBNull.Value)
                    o.t066_descal = dr["t066_descal"].ToString();
                if (dr["t066_idcal"] != DBNull.Value)
                    o.t066_idcal = int.Parse(dr["t066_idcal"].ToString());
                if (dr["Njorlabcal"] != DBNull.Value)
                    o.Njorlabcal = int.Parse(dr["Njorlabcal"].ToString());
                if (dr["t001_fecalta"] != DBNull.Value)
                    o.t001_fecalta = DateTime.Parse(dr["t001_fecalta"].ToString());
                if (dr["t080_fultacc"] != DBNull.Value)
                    o.t080_fultacc = DateTime.Parse(dr["t080_fultacc"].ToString());
                //CREDENCIALES
                if (dr["t080_passw"] != DBNull.Value)
                    o.t080_passw = dr["t080_passw"].ToString();
                if (dr["t080_pregunta"] != DBNull.Value)
                    o.t080_pregunta = dr["t080_pregunta"].ToString();
                if (dr["t080_respuesta"] != DBNull.Value)
                    o.t080_respuesta = dr["t080_respuesta"].ToString();
                if (dr["t080_facep"] != DBNull.Value)
                    o.t080_facep = DateTime.Parse(dr["t080_facep"].ToString());
                if (dr["t080_falta"] != DBNull.Value)
                    o.t080_falta = DateTime.Parse(dr["t080_falta"].ToString());
                //Datos del usuario SUPER
                if (dr["t314_accesohabilitado"] != DBNull.Value)
                    o.t314_accesohabilitado = (bool)dr["t314_accesohabilitado"];
                if (dr["costejornada"] != DBNull.Value)
                    o.t314_costejornada = decimal.Parse(dr["costejornada"].ToString());
                if (dr["costehora"] != DBNull.Value)
                    o.t314_costehora = decimal.Parse(dr["costehora"].ToString());

                if (dr["t422_idmoneda"] != DBNull.Value)
                    o.t422_idmoneda = dr["t422_idmoneda"].ToString();
                if (dr["t422_denominacion"] != DBNull.Value)
                    o.MonedaCoste = dr["t422_denominacion"].ToString();

                if (dr["t314_alias"] != DBNull.Value)
                    o.t314_alias = dr["t314_alias"].ToString();
                if (dr["t314_falta"] != DBNull.Value)
                    o.t314_falta = DateTime.Parse(dr["t314_falta"].ToString());
                if (dr["t314_fbaja"] != DBNull.Value)
                    o.t314_fbaja = DateTime.Parse(dr["t314_fbaja"].ToString());
                if (dr["t314_calculoJA"] != DBNull.Value)
                    o.t314_calculoJA = (bool)dr["t314_calculoJA"];
                if (dr["t314_controlhuecos"] != DBNull.Value)
                    o.t314_controlhuecos = (bool)dr["t314_controlhuecos"];
                if (dr["t314_mailiap"] != DBNull.Value)
                    o.t314_mailiap = (bool)dr["t314_mailiap"];
                if (dr["fultImpIAP"] != DBNull.Value)
                    o.fultImpIAP = DateTime.Parse(dr["fultImpIAP"].ToString());
            }
            else
            {
                dr.Close();
                dr.Dispose();
                //throw (new NullReferenceException("No se ha obtenido ningun dato de Experiencia profesional"));
                o.t001_idficepi = -1;
            }

            dr.Close();
            dr.Dispose();
            return o;

        }

        public static void Update(int idCal, bool bloqueado, int t001_idficepi, string sApe1, string sApe2, string sNombre,
                                  string sSexo, string sTfno, string sMail, string sDatosUser)
        {
            SqlConnection oConn = SUPER.Capa_Negocio.Conexion.Abrir();
            SqlTransaction tr = SUPER.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
            try
            {
                //SUPER.DAL.FORANEO.Update(tr, idCal, bloqueado, t001_idficepi);
                SUPER.Capa_Datos.Ficepi.UpdateForaneo(tr, t001_idficepi, sApe1.ToUpper(), sApe2.ToUpper(), sNombre.ToUpper(),
                                                      sSexo, sTfno, idCal, sMail);
                string[] aDatos = Regex.Split(sDatosUser, "##");
                SUPER.DAL.FORANEO.ModificarUsuario(tr, int.Parse(aDatos[0]), SUPER.Capa_Negocio.Utilidades.unescape(aDatos[2]),
                                                    DateTime.Parse(aDatos[3]),//fecha de alta
                                                    (aDatos[4] == "") ? null : (DateTime?)DateTime.Parse(aDatos[4]),//fecha baja
                                                    (aDatos[5] == "1") ? true : false,//Control de huecos
                                                    (aDatos[6] == "1") ? true : false,//Mail IAP
                                                    (aDatos[7] == "1") ? false : true,//Acceso a SUPER bloqueado
                                                    decimal.Parse(aDatos[8]),//coste hora
                                                    decimal.Parse(aDatos[9]),//coste jornada
                                                    (aDatos[10] == "1") ? true : false,//Cálculo jornadas adaptadas
                                                    aDatos[11]);//Moneda

                SUPER.Capa_Negocio.Conexion.CommitTransaccion(tr);
            }
            catch (Exception e)
            {
                SUPER.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                throw (new Exception("Error al actualizar datos del profesional foráneo.\r\n" + e.Message)); 
            }
            finally
            {
                SUPER.Capa_Negocio.Conexion.Cerrar(oConn);
            }
        }
        #endregion
    }
}
