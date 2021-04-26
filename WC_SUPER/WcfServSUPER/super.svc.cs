using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using IB.Services.Super.Negocio;
using IB.Services.Super.Globales;
//using System.Data;

namespace IB.Services.Super
{
    public class Super : ISuper
    {
        #region Acceso a consultas personalizadas SUPER
        //[OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public string getDatosSuper(string sXml)
        {
            string sRes = "", sPaso="Inicio\r\n";
            string sNomPar = "", sValPar = "";
            string t145_idusuario = "", t145_clave = "", t314_password = "", t742_clavews = "";
            int t314_idusuario = -1;
            bool buser = false, bclave = false, bidUser = false, bpassw = false, bcons = false, bConsultaRealizada=false;
            StringBuilder sb = new StringBuilder();
            List<PARAMETRO> aListParamXML = new List<PARAMETRO>();
            List<PARAMETRO> aListParamProc = new List<PARAMETRO>();

            try
            {
                #region Leo el XML de entrada para obtener los parámetros de la consulta
                //ArrayList aListParam = new ArrayList();
                System.Xml.XmlDocument doc = new XmlDocument();
                doc.LoadXml(sXml);
                XmlNode oXML2 = doc.FirstChild;
                XmlNodeList lNodos = oXML2.ChildNodes;
                //sPaso += "Despues de inicializar parámetros. ";
                foreach (XmlNode oNodo in lNodos)
                {
                    //sPaso += "oNodo.Name=" + oNodo.Name + " ";
                    switch (oNodo.Name)
                    {
                        case "user"://Código de usuario del servicio web
                            buser = true;
                            t145_idusuario = oNodo.InnerText.Trim();
                            //sPaso += "t145_idusuario=" + t145_idusuario;
                            break;
                        case "clave"://clave de usuario del servicio web
                            bclave = true;
                            t145_clave = oNodo.InnerText.Trim();
                            break;
                        case "idUser"://Usuario que pide la consulta
                            bidUser = true;
                            t314_idusuario = Utilidades.getEntero("idUser", oNodo.InnerText);
                            break;
                        case "passw"://clave de usuario SUPER para acceso al servicio web
                            bpassw = true;
                            t314_password = oNodo.InnerText.Trim();
                            break;
                        case "cons"://clave que identifica la consulta personalizada
                            bcons = true;
                            t742_clavews = oNodo.InnerText.Trim();
                            break;
                        case "params"://Recorro la lista de parámetros
                            int iNumParam = oNodo.ChildNodes.Count;
                            foreach (XmlNode oParams in oNodo.ChildNodes)
                            {
                                sNomPar = "";
                                sValPar = "";
                                //sPaso += "oParams.Name=" + oParams.Name + " ";
                                foreach (XmlNode oPar in oParams.ChildNodes)
                                {
                                    //sPaso += "oPar.Name=" + oPar.Name + " ";
                                    switch (oPar.Name)
                                    {
                                        case "nombre":
                                            //sPaso += "oParXML.t474_textoparametro=" + oPar.InnerText.Trim() + " ";
                                            sNomPar = oPar.InnerText.Trim();
                                            break;
                                        case "valor":
                                            //sPaso += "oParXML.valor=" + oPar.InnerText.Trim() + " ";
                                            sValPar = oPar.InnerText.Trim();
                                            break;
                                    }
                                }
                                if (sNomPar != "" && sValPar != "")
                                {
                                    //sPaso += "Creo una instancia del objeto PARAMETRO";
                                    PARAMETRO oParXML = new PARAMETRO(sNomPar, sValPar);
                                    //sPaso += "Añado el parametro a la lista de parametros aListParamXML";
                                    aListParamXML.Add(oParXML);
                                    //sPaso += "Despues de añadir el parametro a la lista de parametros aListParamXML";
                                }
                            }
                            break;
                    }
                }
                #endregion
                //sPaso += "Despues de leer el XML de entrada.\r\n";
                #region Valido los parámetros de entrada
                #region Comprobar la existencia de parámetros de entrada
                if (!buser)
                {
                    sRes = @"<Datos><Error>-1</Error><Message>Falta el parámetro código de usuario del servicio web</Message></Datos>";
                }
                if (sRes == "")
                {
                    if (!bclave)
                    {
                        sRes = @"<Datos><Error>-1</Error><Message>Falta el parámetro código de clave del servicio web</Message></Datos>";
                    }
                }
                if (sRes == "")
                {
                    if (!bidUser)
                    {
                        sRes = @"<Datos><Error>-1</Error><Message>Falta el parámetro código de usuario de SUPER</Message></Datos>";
                    }
                }
                if (sRes == "")
                {
                    if (!bpassw)
                    {
                        sRes = @"<Datos><Error>-1</Error><Message>Falta el parámetro contraseña de usuario para acceso a servicios web de SUPER</Message></Datos>";
                    }
                }
                if (sRes == "")
                {
                    if (!bcons)
                    {
                        sRes = @"<Datos><Error>-1</Error><Message>Falta el parámetro código de consulta SUPER</Message></Datos>";
                    }
                }
                #endregion
                //sPaso += "Despues de comprobar la existencia de parametros de entrada.\r\n";
                #region Comprobar que los valores de los parámetros del XML son válidos
                CONSUMIDOR oCons= new CONSUMIDOR();
                //sPaso += "Despues de crear instancia de CONSUMIDOR.\r\n";
                if (sRes == "")
                {
                    if (t145_idusuario == "")
                    {
                        sRes = @"<Datos><Error>-2</Error><Message>Debe indicar código de usuario del servicio web</Message></Datos>";
                    }
                    else
                    {
                        //sPaso += "Antes de asignar valores a la instancia de CONSUMIDOR. t145_idusuario=" + t145_idusuario + "\r\n";
                        oCons = CONSUMIDOR.GetDatos(null, t145_idusuario);
                        if (oCons.t145_idusuario == "-1")
                        {
                            sRes = @"<Datos><Error>-1</Error><Message>Error al obtener los datos del Consumidor" + t145_idusuario + "\r\n" + oCons.t145_clave + "</Message></Datos>";
                        }
                        //sPaso += "Despues de asignar valores a la instancia de CONSUMIDOR.\r\n";
                        if (sRes == "")
                        {
                            if (oCons.t145_intentos>10)
                                sRes = @"<Datos><Error>-3</Error><Message>El usuario de servicio web ha sobrepasado el nº de intentos de acceso erróneos</Message></Datos>";
                        }
                        if (sRes == "")
                        {
                            if (oCons.t145_idusuario != "")
                            {
                                if (DateTime.Now < oCons.t145_fiv || DateTime.Now > oCons.t145_ffv.AddDays(1))
                                    sRes = @"<Datos><Error>-5</Error><Message>Usuario de servicio web " + oCons.t145_idusuario +
                                            @" FIV: " + oCons.t145_fiv.ToString() + @"FFV: " + oCons.t145_ffv.AddDays(1).ToString() +
                                            @" fuera de vigencia</Message></Datos>";
                            }
                            else
                                sRes = @"<Datos><Error>-4</Error><Message>Usuario de servicio web no válido</Message></Datos>";
                        }
                    }
                }
                if (sRes == "")
                {
                    if (t145_clave == "")
                        sRes = @"<Datos><Error>-2</Error><Message>Debe indicar clave de usuario del servicio web</Message></Datos>";
                    else
                    {
                        if (t145_clave != oCons.t145_clave)
                            sRes = @"<Datos><Error>-6</Error><Message>Clave de usuario del servicio web no válida</Message></Datos>";
                    }
                }
                USUARIO oUser= new USUARIO();
                //sPaso += "Despues de crear instancia de USUARIO.\r\n";
                if (sRes == "")
                {
                    if (t314_idusuario == -1)
                    {
                        sRes = @"<Datos><Error>-2</Error><Message>Debe indicar código de usuario de SUPER</Message></Datos>";
                    }
                    else
                    {
                        oUser = USUARIO.GetDatos(null, t314_idusuario);
                        if (oUser.t314_idusuario != -1)
                        {
                            if (DateTime.Now < oUser.t314_falta)
                                sRes = @"<Datos><Error>-9</Error><Message>Usuario SUPER no activo. Fecha de alta: " + oUser.t314_falta.ToString() + "</Message></Datos>";
                            if (sRes == "" && oUser.t314_fbaja != null)
                            {
                                DateTime dtAux = (DateTime)oUser.t314_fbaja;
                                dtAux=dtAux.AddDays(1);
                                if (DateTime.Now > dtAux)
                                    sRes = @"<Datos><Error>-9</Error><Message>Usuario SUPER no activo. Fecha de baja: " + dtAux.ToString() + "</Message></Datos>";
                            }
                        }
                        else
                            sRes = @"<Datos><Error>-8</Error><Message>Usuario SUPER no válido</Message></Datos>";
                    }
                }
                if (sRes == "")
                {
                    if (DateTime.Now < oUser.t001_fecalta)
                        sRes = @"<Datos><Error>-10</Error><Message>Profesional FICEPI no activo</Message></Datos>";
                    if (sRes == "" && oUser.t001_fecbaja != null)
                    {
                        DateTime dtAux = (DateTime)oUser.t001_fecbaja;
                        dtAux = dtAux.AddDays(1);
                        if (DateTime.Now > dtAux)
                            sRes = @"<Datos><Error>-10</Error><Message>Profesional FICEPI no activo. Fecha de baja: " + dtAux.ToString() + "</Message></Datos>";
                    }
                }
                if (sRes == "")
                {
                    if (t314_password == "")
                        sRes = @"<Datos><Error>-2</Error><Message>Debe indicar contraseña del usuario SUPER</Message></Datos>";
                    else
                    {
                        if (t314_password != oUser.t314_password)
                            sRes = @"<Datos><Error>-11</Error><Message>Contraseña del usuario SUPER no válida</Message></Datos>";
                    }
                }
                CONSULTA oConsulta= new CONSULTA();
                //sPaso += "Despues de crear instancia de CONSULTA.\r\n";
                if (sRes == "")
                {
                    if (t742_clavews == "")
                        sRes = @"<Datos><Error>-2</Error><Message>Debe indicar la consulta a realizar</Message></Datos>";
                    else
                    {
                        oConsulta = CONSULTA.GetDatos(null, t742_clavews, t314_idusuario);
                        if (oConsulta.t472_idconsulta == -1)
                        {
                            sRes = @"<Datos><Error>-12</Error><Message>Consulta no válida. t742_clavews=" + t742_clavews +
                                    " t314_idusuario=" + t314_idusuario + " Traza: " + oConsulta.t472_procalm + "</Message></Datos>";
                        }
                        else
                        {
                            if (!oConsulta.t472_estado)
                                sRes = @"<Datos><Error>-13</Error><Message>Consulta no activa</Message></Datos>";
                        }
                    }
                }
                if (sRes == "")
                {
                    if (oConsulta.t314_idusuario == -1)
                        sRes = @"<Datos><Error>-14</Error><Message>Consulta no accesible</Message></Datos>";
                    else
                    {
                        if (!oConsulta.t473_estado)
                            sRes = @"<Datos><Error>-15</Error><Message>Consulta no vigente</Message></Datos>";
                    }
                }
                #endregion
                //sPaso += "Despues de comprobar que los valores de los parametros del XML son validos.\r\n";
                #region Comprobar parametros del procedimiento almacenado y asignales valores
                if (sRes == "")
                {
                    aListParamProc = PARAMETRO.GetParametros(null, oConsulta.t472_idconsulta);
                    foreach (PARAMETRO oParam in aListParamProc)
                    {
                        if (sRes == "")
                        {
                            //Compruebo que para cada uno de los parámetros obligatorios de la consulta se ha pasado el parámetro en el XML
                            PARAMETRO oParXML = PARAMETRO.BuscarParametro(aListParamXML, oParam.t474_textoparametro);
                            if (oParXML == null)
                            {
                                if (!oParam.t474_opcional)
                                    sRes = @"<Datos><Error>-16</Error><Message>Faltan parámetros para la consulta</Message></Datos>";
                            }
                            else
                            {
                                //Solo recogeré el valor del parametro que viene en el XML si el valor del parametro en el proc.alm.
                                //es modificable por el usuario
                                if (oParam.t474_visible == "M")
                                {
                                    #region Compruebo que el valor pasado es del tipo adecuado
                                    switch (oParam.t474_tipoparametro)
                                    {//I->Entero; V->Varchar; M->Money; D->Date; B-> Booleano; A->Añomes
                                        case "I":
                                            if (!Utilidades.isInteger(oParXML.valor))
                                                sRes = @"<Datos><Error>-17</Error><Message>El valor del parámetro " + oParam.t474_textoparametro + " no es un entero correcto</Message></Datos>";
                                            break;
                                        case "M":
                                            if (!Utilidades.isNumeric(oParXML.valor))
                                                sRes = @"<Datos><Error>-17</Error><Message>El valor del parámetro " + oParam.t474_textoparametro + " no es un double correcto</Message></Datos>";
                                            break;
                                        case "A":
                                            if (!Utilidades.isAnoMes(oParXML.valor))
                                                sRes = @"<Datos><Error>-17</Error><Message>El valor del parámetro " + oParam.t474_textoparametro + " no es un AñoMes correcto</Message></Datos>";
                                            break;
                                        case "D":
                                            if (!Utilidades.isDate(oParXML.valor))
                                                sRes = @"<Datos><Error>-17</Error><Message>El valor del parámetro " + oParam.t474_textoparametro + " no es una fecha correcta</Message></Datos>";
                                            break;
                                        case "B":
                                            if (oParXML.valor != "0" && oParXML.valor != "1")
                                                sRes = @"<Datos><Error>-17</Error><Message>El valor del parámetro " + oParam.t474_textoparametro + " no es un booleano correcto</Message></Datos>";
                                            break;
                                    }
                                    #endregion
                                    if (sRes == "")
                                    {//Asigno el valor del parametro del proc.alm. con el valor que viene en el XML
                                        oParam.valor = oParXML.valor;
                                    }
                                }
                                else
                                {//El usuario está pasando un parámetro para el que no puede indicar valor
                                    sRes = @"<Datos><Error>-18</Error><Message>El valor del parámetro " + oParam.t474_textoparametro + " no es modificable</Message></Datos>";
                                }
                            }
                        }
                        else
                            break;
                    }
                        
                }
                #endregion
                //sPaso += "Despues de comprobar los parametros del procedimiento almacenado.\r\n";
                #endregion
                #region Obtengo los datos
                if (sRes == "")
                {
                    //sPaso += "Antes de ejecutarConsultaDS\r\n\tt314_idusuario=" + t314_idusuario.ToString() +
                    //            "\toConsulta.t472_procalm=" + oConsulta.t472_procalm ;
                    sb.Append(@"<Datos><Error>0</Error>");
                    try
                    {
                        sb.Append(CONSULTA.ejecutarConsultaDS(t314_idusuario, oConsulta.t472_procalm, aListParamProc));
                        sb.Append(@"</Datos>");
                        sRes = sb.ToString();
                        bConsultaRealizada = true;
                    }
                    catch (Exception e)
                    {
                        //sRes = @"<Datos><Paso>" + sPaso + "</Paso><Error>-20</Error><Message>" + e.Message.ToString() + "</Message></Datos>";
                        sRes = @"<Datos><Error>-20</Error><Message>" + e.Message.ToString() + "</Message></Datos>";
                    }
                }
                #endregion
                #region Actualizar contador de intentos
                if (bConsultaRealizada)//Si el acceso a sido correcto pongo a cero el contador de accesos erróneos
                    CONSUMIDOR.SetIntentos(null, oCons.t145_idusuario, (short)0);
                else
                {
                    oCons.t145_intentos++;
                    CONSUMIDOR.SetIntentos(null, oCons.t145_idusuario, oCons.t145_intentos);
                }
                #endregion
            }
            catch (Exception e1)
            {
                //sRes = @"<Datos><Paso>" + sPaso + "</Paso><Error>-1</Error><Message>" + e1.Message.ToString() + "</Message></Datos>";
                sRes = @"<Datos><Error>-1</Error><Message>" + e1.Message.ToString() + "</Message></Datos>";
            }
            return Utilidades.cabXml() + sRes;
        }
        #endregion

        //[OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public string getFechaCierreIAP(string sXml)
        {
            string sRes = "";
            string sNomPar = "", sValPar = "";
            int t314_idusuario = -1;
            bool bidUser = false;
            StringBuilder sb = new StringBuilder();

            try
            {
                #region Leo el XML de entrada para obtener los parámetros de la consulta
                System.Xml.XmlDocument doc = new XmlDocument();
                doc.LoadXml(sXml);
                XmlNode oXML2 = doc.FirstChild;
                XmlNodeList lNodos = oXML2.ChildNodes;
                foreach (XmlNode oNodo in lNodos)
                {
                    switch (oNodo.Name)
                    {
                        case "idUser"://Usuario que pide la consulta
                            bidUser = true;
                            t314_idusuario = Utilidades.getEntero("idUser", oNodo.InnerText);
                            break;
                    }
                }
                #endregion
                #region Valido los parámetros de entrada
                #region Comprobar la existencia de parámetros de entrada
                if (sRes == "")
                {
                    if (!bidUser)
                    {
                        sRes = @"<Datos><Error>-1</Error><Message>Falta el parámetro código de usuario de SUPER</Message></Datos>";
                    }
                }
                #endregion
                #region Comprobar que los valores de los parámetros del XML son válidos
                USUARIO oUser = new USUARIO();
                //sPaso += "Despues de crear instancia de USUARIO.\r\n";
                if (sRes == "")
                {
                    if (t314_idusuario == -1)
                    {
                        sRes = @"<Datos><Error>-2</Error><Message>Debe indicar código de usuario de SUPER</Message></Datos>";
                    }
                    else
                    {
                        oUser = USUARIO.GetDatos(null, t314_idusuario);
                        if (oUser.t314_idusuario != -1)
                        {
                            if (DateTime.Now < oUser.t314_falta)
                                sRes = @"<Datos><Error>-4</Error><Message>Usuario SUPER no activo. Fecha de alta: " + oUser.t314_falta.ToString() + "</Message></Datos>";
                            if (sRes == "" && oUser.t314_fbaja != null)
                            {
                                DateTime dtAux = (DateTime)oUser.t314_fbaja;
                                dtAux = dtAux.AddDays(1);
                                if (DateTime.Now > dtAux)
                                    sRes = @"<Datos><Error>-4</Error><Message>Usuario SUPER no activo. Fecha de baja: " + dtAux.ToString() + "</Message></Datos>";
                            }
                        }
                        else
                            sRes = @"<Datos><Error>-3</Error><Message>Usuario SUPER no válido</Message></Datos>";
                    }
                }
                if (sRes == "")
                {
                    if (DateTime.Now < oUser.t001_fecalta)
                        sRes = @"<Datos><Error>-5</Error><Message>Profesional FICEPI no activo</Message></Datos>";
                    if (sRes == "" && oUser.t001_fecbaja != null)
                    {
                        DateTime dtAux = (DateTime)oUser.t001_fecbaja;
                        dtAux = dtAux.AddDays(1);
                        if (DateTime.Now > dtAux)
                            sRes = @"<Datos><Error>-5</Error><Message>Profesional FICEPI no activo. Fecha de baja: " + dtAux.ToString() + "</Message></Datos>";
                    }
                }
                #endregion
                #endregion
                #region Obtengo los datos
                if (sRes == "")
                {
                    sb.Append(@"<Datos><Error>0</Error><Res>");
                    PARAMETRIZACIONSUPER oParSuper = PARAMETRIZACIONSUPER.GetDatos(null);
                    sb.Append(oParSuper.t725_ultcierreempresa_IAP.ToString());
                    sb.Append(@"</Res></Datos>");
                    sRes = sb.ToString();
                }
                #endregion
            }
            catch (Exception e1)
            {
                sRes = @"<Datos><Error>-1</Error><Message>" + e1.Message.ToString() + "</Message></Datos>";
            }
            return Utilidades.cabXml() + sRes;
        }

    }
}
