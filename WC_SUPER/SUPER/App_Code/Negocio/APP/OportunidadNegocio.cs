using System;
using System.Collections.Generic;
//Para el StringBuilder
using System.Text;
//Para el ArrayList
using System.Collections;

using IB.SUPER.APP.DAL;
using IB.SUPER.APP.Models;
using SUPER.Capa_Negocio;
using SUPDAL = SUPER.DAL;

namespace IB.SUPER.APP.BLL
{
    /// <summary>
    /// Descripción breve de OportunidadNegocio
    /// </summary>
    public class OportunidadNegocio:IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("4BA4161C-45C2-4554-B392-D929D5102C07");
        private bool disposed = false;

        #endregion

        #region Constructor

        public OportunidadNegocio()
            : base()
        {
            //OpenDbConn();
        }

        public OportunidadNegocio(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.OportunidadNegocio> CatalogoSinContrato(int idNodo, DateTime dtDesde, DateTime dtHasta)
        {
            OpenDbConn();

            DAL.OportunidadNegocio cOportunidadNegocio = new DAL.OportunidadNegocio(cDblib);
            return cOportunidadNegocio.CatalogoSinContrato(idNodo, dtDesde, dtHasta);

        }


        public List<Models.ProyectoEconomico> generarContratos (List<IB.SUPER.APP.Models.OportunidadNegocio> Oportunidades)
        {
            int iNumProys = 0, idPE=-1, idPSN=-1;
            bool bConTransaccion = false;
            Guid methodOwnerID = new Guid("2558CB99-C43F-4930-A56C-D6F5CDE51980");
            //IB.SUPER.APP.BLL.ProyectoEconomico ProyBLL = new IB.SUPER.APP.BLL.ProyectoEconomico();
            List<Models.ProyectoEconomico> lstGenerados = new List<Models.ProyectoEconomico>();

            OpenDbConn();
            if (cDblib.Transaction.ownerID.Equals(new Guid()))
                bConTransaccion = true;
            if (bConTransaccion)
                cDblib.beginTransaction(methodOwnerID);

            BLL.Contrato oContrato = new Contrato(cDblib);
            BLL.FIGURAPROYECTOSUBNODO oFigura = new FIGURAPROYECTOSUBNODO(cDblib);
            IB.SUPER.IAP30.BLL.Nodo oNodo = new IAP30.BLL.Nodo();

            try
            {
                foreach (IB.SUPER.APP.Models.OportunidadNegocio oport in Oportunidades)
                {
                    
                    DAL.OportunidadNegocio oON = new DAL.OportunidadNegocio(cDblib);
                    #region Contrato y Extensión
                    //Si la extension==0 -> grabarcontrato
                    if (oport.t377_idextension==0)
                    {
                        oON.GenerarContrato(oport);
                    }
                    else
                    {
                        //Compruebo que existe contrato. Si no existe, hay que darlo de alta
                        //oContrato = new Contrato(cDblib);
                        if (!oContrato.Existe(oport.t306_icontrato))
                        {
                            oON.GenerarContrato(oport.t306_icontrato, oport.t314_idusuario_responsable);
                        }
                        
                    }
                    //La extensión se graba siempre
                    oON.GenerarExtension(oport);
                    //Si no existe proyecto asociado al contrato-> Generarlo (según parametrización puede crear 1 o 2 proyectos)
                    iNumProys = oON.GetNumProyectos(oport.t306_icontrato);


                    //PARA PRUEBAS
                    //if (bConTransaccion) cDblib.commitTransaction(methodOwnerID);

                    #endregion

                    #region Proyecto Económico
                    if (iNumProys == 0)
                    {
                        //PARA PRUEBAS
                        //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

                        //Obtengo el subnodo al que asociar el proyecto
                        int t304_idsubnodo = SUBNODO.GetSubNodoDefecto(oport.t303_idnodo);

                        //Obtengo datos del nodo para la creación del proyecto
                        //oNodo = new IAP30.BLL.Nodo();
                        IB.SUPER.IAP30.Models.Nodo mNodo = oNodo.Select(oport.t303_idnodo);

                        #region Cargo los datos para la generación del proyecto
                        Models.ProyectoEconomico mPE = new IB.SUPER.APP.Models.ProyectoEconomico();
                        mPE.automatico = true;
                        if (oport.t377_importeser >= oport.t377_importepro)
                            mPE.categoria = "S";
                        else
                            mPE.categoria = "P";
                        mPE.cod_cliente = oport.t302_idcliente_contrato;
                        mPE.cod_contrato = oport.t306_icontrato;
                        mPE.cod_naturaleza = 1;
                        mPE.fecha_sap = DateTime.Now;
                        
                        mPE.fini_prevista = Fechas.getSigDiaUltMesCerrado(mNodo.t303_ultcierreECO);
                        mPE.ffin_prevista = GetFechaFin(mPE.fini_prevista, oport.duracion);
                        mPE.modalidad = GetModalidad(oport.tipocontrato.ToUpper());

                        if (mNodo.t303_modelocostes == "X")
                            mPE.modelo_coste = "J";
                        else
                            mPE.modelo_coste = mNodo.t303_modelocostes;
                        if (mNodo.t303_modelotarifas == "X")
                            mPE.modelo_tarifa = "J";
                        else
                            mPE.modelo_tarifa = mNodo.t303_modelotarifas;
                        mPE.nom_proyecto = oport.t377_denominacion;
                        #endregion
                        #region genero proyecto
                        if (mNodo.t303_desglose)
                        {
                            #region  Crea 2 proyectos
                            if (oport.t377_importepro != 0)
                            {
                                if (oport.t377_importeser != 0)
                                {
                                    mPE.nom_proyecto = "P/" + oport.t377_denominacion;
                                }
                                #region Crea un proyecto de categoria PRODUCTO
                                //idPE = ProyBLL.Insert(mPE);
                                //DAL.ProyectoEconomico cPE = new DAL.ProyectoEconomico(cDblib);
                                //idPE = cPE.GenerarProyecto(mPE);
                                idPE = oON.GenerarProyecto(mPE);

                                //Creo el proyecto subnodo
                                mPE.cod_proyecto = idPE;
                                mPE.cod_subnodo = t304_idsubnodo;
                                mPE.cualidad = "P";
                                mPE.cod_usuario_responsable = mNodo.t314_idusuario_responsable;
                                mPE.seudonimo = mPE.nom_proyecto;

                                //idPSN = cPE.GenerarProyectoSubnodo(mPE);
                                idPSN = oON.GenerarProyectoSubnodo(mPE);

                                //Asigno la figura de RTPE
                                Models.FIGURAPROYECTOSUBNODO mFigura = new Models.FIGURAPROYECTOSUBNODO();
                                mFigura.t305_idproyectosubnodo = idPSN;
                                mFigura.t310_figura = "M";
                                mFigura.t314_idusuario = oport.t314_idusuario_gestorprod;

                                //oFigura = new FIGURAPROYECTOSUBNODO(cDblib);
                                oFigura.Insert(mFigura);
                                #region Cargo la lista de vuelta para mostrar al usuario los proyectos generados
                                Models.ProyectoEconomico oProyectoEconomico = new Models.ProyectoEconomico();
                                oProyectoEconomico.cod_proyecto = idPE;
                                oProyectoEconomico.nom_proyecto = mPE.nom_proyecto;
                                oProyectoEconomico.cod_contrato = oport.t306_icontrato;
                                oProyectoEconomico.cod_extension = oport.t377_idextension;
                                oProyectoEconomico.t305_idproyectosubnodo = idPSN;
                                oProyectoEconomico.t301_estado = "A";
                                oProyectoEconomico.t302_denominacion = oport.cliente;
                                oProyectoEconomico.t301_categoria = mPE.categoria;
                                oProyectoEconomico.t305_cualidad = "C";
                                oProyectoEconomico.proy_responsable = oport.responsable;

                                lstGenerados.Add(oProyectoEconomico);
                                #endregion
                                #endregion
                            }
                            if (oport.t377_importeser != 0)
                            {
                                if (oport.t377_importepro != 0)
                                {
                                    mPE.nom_proyecto = "S/" + oport.t377_denominacion;
                                }
                                #region Crea un proyecto de categoria SERVICIO
                                //DAL.ProyectoEconomico cPE = new DAL.ProyectoEconomico(cDblib);
                                //idPE = cPE.GenerarProyecto(mPE);
                                idPE = oON.GenerarProyecto(mPE);

                                //Creo el proyecto subnodo
                                mPE.cod_proyecto = idPE;
                                mPE.cod_subnodo = t304_idsubnodo;
                                mPE.cualidad = "C";
                                mPE.cod_usuario_responsable = mNodo.t314_idusuario_responsable;
                                mPE.seudonimo = mPE.nom_proyecto;

                                //idPSN = cPE.GenerarProyectoSubnodo(mPE);
                                idPSN = oON.GenerarProyectoSubnodo(mPE);

                                //Asigno la figura de RTPE
                                Models.FIGURAPROYECTOSUBNODO mFigura = new Models.FIGURAPROYECTOSUBNODO();
                                mFigura.t305_idproyectosubnodo = idPSN;
                                mFigura.t310_figura = "M";
                                mFigura.t314_idusuario = oport.t314_idusuario_gestorprod;

                                oFigura = new FIGURAPROYECTOSUBNODO(cDblib);
                                oFigura.Insert(mFigura);

                                #region Cargo la lista de vuelta para mostrar al usuario los proyectos generados
                                Models.ProyectoEconomico oProyectoEconomico = new Models.ProyectoEconomico();
                                oProyectoEconomico.cod_proyecto = idPE;
                                oProyectoEconomico.nom_proyecto = mPE.nom_proyecto;
                                oProyectoEconomico.cod_contrato = oport.t306_icontrato;
                                oProyectoEconomico.cod_extension = oport.t377_idextension;
                                oProyectoEconomico.t305_idproyectosubnodo = idPSN;
                                oProyectoEconomico.t301_estado = "A";
                                oProyectoEconomico.t302_denominacion = oport.cliente;
                                oProyectoEconomico.t301_categoria = mPE.categoria;
                                oProyectoEconomico.t305_cualidad = "C";
                                oProyectoEconomico.proy_responsable = oport.responsable;
                                #endregion
                                #endregion
                            }

                            #endregion
                        }
                        else
                        {
                            #region Crea un proyecto
                            //DAL.ProyectoEconomico cPE = new DAL.ProyectoEconomico(cDblib);
                            //idPE = cPE.GenerarProyecto(mPE);
                            idPE = oON.GenerarProyecto(mPE);
                            //Creo el proyecto subnodo
                            mPE.cod_proyecto = idPE;
                            mPE.cod_subnodo = t304_idsubnodo;
                            mPE.cualidad = "C";
                            mPE.cod_usuario_responsable = mNodo.t314_idusuario_responsable;
                            mPE.seudonimo = mPE.nom_proyecto;

                            //idPSN = cPE.GenerarProyectoSubnodo(mPE);
                            idPSN = oON.GenerarProyectoSubnodo(mPE);

                            //Asigno la figura de RTPE
                            Models.FIGURAPROYECTOSUBNODO mFigura = new Models.FIGURAPROYECTOSUBNODO();
                            mFigura.t305_idproyectosubnodo = idPSN;
                            mFigura.t310_figura = "M";
                            mFigura.t314_idusuario = oport.t314_idusuario_gestorprod;

                            oFigura = new FIGURAPROYECTOSUBNODO(cDblib);
                            oFigura.Insert(mFigura);

                            #region Cargo la lista de vuelta para mostrar al usuario los proyectos generados
                            Models.ProyectoEconomico oProyectoEconomico = new Models.ProyectoEconomico();
                            oProyectoEconomico.cod_proyecto = idPE;
                            oProyectoEconomico.nom_proyecto = mPE.nom_proyecto;
                            oProyectoEconomico.cod_contrato = oport.t306_icontrato;
                            oProyectoEconomico.cod_extension = oport.t377_idextension;
                            oProyectoEconomico.t305_idproyectosubnodo = idPSN;
                            oProyectoEconomico.t301_estado = "A";
                            oProyectoEconomico.t302_denominacion = oport.cliente;
                            oProyectoEconomico.t301_categoria = mPE.categoria;
                            oProyectoEconomico.t305_cualidad = "C";
                            oProyectoEconomico.proy_responsable = oport.responsable;
                            oProyectoEconomico.codred_gestor_produccion = oport.codred_gestor_produccion;

                            lstGenerados.Add(oProyectoEconomico);
                            #endregion
                            #endregion
                        }
                        #endregion
                        //PARA PRUEBAS
                        //cDblib.commitTransaction(methodOwnerID);
                    }
                    #endregion
                }
                //PARA PRUEBAS
                if (bConTransaccion)
                    cDblib.commitTransaction(methodOwnerID);

                #region Correo
                //Con la lista de proyectos generados agrupo para cada gestor de producción concernido la lista de sus nuevos proyectos
                StringBuilder sb = new StringBuilder();
                List<Models.Mail> lstGestores = new List<Models.Mail>();
                try
                {
                    foreach (Models.ProyectoEconomico oPE in lstGenerados)
                    {
                        sb.Length = 0;
                        Models.Mail oProf = BuscarCodRed(lstGestores, oPE.codred_gestor_produccion);
                        if (oProf == null)
                        {
                            oProf = new Models.Mail();
                            oProf.codred = oPE.codred_gestor_produccion;
                            sb.Append(ponerCabecera());
                            sb.Append(@"<tr style='height:16px'><td style='width:80px;padding-left:3px;'>" + oPE.cod_proyecto.ToString("#,###") + "</td>");
                            sb.Append(@"<td style='width:460px;text-overflow:ellipsis;overflow:hidden;'>" + oPE.nom_proyecto + "</td>");
                            sb.Append(@"<td style='width:80px;'>" + oPE.cod_contrato.ToString("#,###") + "</td>");
                            sb.Append(@"<td style='width:80px;'>" + oPE.cod_extension.ToString("#,###") + "</td></tr>");

                            oProf.mensaje = sb.ToString();

                            lstGestores.Add(oProf);
                        }
                        else
                        {
                            sb.Append(@"<tr style='height:16px'><td style='width:80px;padding-left:3px;'>" + oPE.cod_proyecto.ToString("#,###") + "</td>");
                            sb.Append(@"<td style='width:460px;text-overflow:ellipsis;overflow:hidden;'>" + oPE.nom_proyecto + "</td>");
                            sb.Append(@"<td style='width:80px;'>" + oPE.cod_contrato.ToString("#,###") + "</td>");
                            sb.Append(@"<td style='width:80px;'>" + oPE.cod_extension.ToString("#,###") + "</td></tr>");

                            oProf.mensaje += sb.ToString();
                        }

                    }
                    //Con la lista de proyectos agrupada envío a cada gestor de producción concernido la lista de sus nuevos proyectos
                    string sAsunto = "SUPER: Proyectos generados";
                    ArrayList aListCorreo = new ArrayList();
                    string sMensaje = "";
                    foreach (Models.Mail oGestor in lstGestores)
                    {
                        sMensaje= oGestor.mensaje + ponerPie();
                        string[] aMail = { sAsunto, sMensaje, oGestor.codred };
                        aListCorreo.Add(aMail);
                    }
                    Correo.EnviarCorreos(aListCorreo);
                }
                catch (Exception e1) {
                    SUPDAL.Log.Insertar("App_Code/Negocio/APP/OportunidadNegocio.generarContratos. Error al enviar correos. " + e1.Message);
                }
                #endregion
                return lstGenerados;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid()))
                    cDblib.rollbackTransaction(methodOwnerID);
                throw new Exception(ex.Message);
            }
            finally
            {
                //nota.Dispose();
                //ProyBLL.Dispose();
                oContrato.Dispose();
                oNodo.Dispose();
                oFigura.Dispose();
            }
        }
        #endregion

        #region funciones privadas
        private string ponerCabecera()
        {
            string webServer = "http://imagenes.intranet.ibermatica/SUPERNET/";
            string sRes = @"<tr><td colspan='3' style='padding-left:10px;'><br />SUPER te informa que se han generado los siguientes proyectos a partir de oportunidades en HERMES<br /><br /></td></tr>";
            sRes += @"<tr><td colspan='3' style='padding-left:10px;'>";
            sRes += @"<table style='WIDTH: 700px; BORDER-COLLAPSE: collapse; HEIGHT: 17px' cellSpacing='0' cellpadding='0' border='0'>";
            sRes += @"<tr style='FONT-WEIGHT: bold; FONT-SIZE: 12px; background-color:#5894ae; BACKGROUND-IMAGE: url(" + webServer + "fondoEncabezamientoListas.gif);";
            sRes += @" COLOR: #ffffff; FONT-FAMILY: Arial, Helvetica, sans-serif; background-color:#5894ae;'>";
            sRes += @"<td style='width:80px;padding-left:3px;'><b>Nº</b></td><td style='width:460px'><b>Proyecto</b></td><td style='width:80px'><b>Contrato</b></td><td style='width:80px'><b>Extensión</b></td></tr></table>";
            sRes += @"<div style='background-image:url(" + webServer + "imgFT16.gif); width:0%; height:0%'>";
            sRes += @"<table style='FONT-FAMILY: Arial;FONT-SIZE: 12px; table-layout:fixed; border-collapse:collapse;' width='700px' border='0' cellspacing='0' cellpadding='0'>";
            return sRes;

        }
        private string ponerPie()
        {
            string webServer = "http://imagenes.intranet.ibermatica/SUPERNET/";
            string sRes = "";
            sRes = @"</table></td></tr><tr><td colspan='3' style='padding-left:10px;'>";
            sRes += @"<table style='WIDTH: 700px; HEIGHT: 17px' cellSpacing='0' cellpadding='0' border='0'>";
            sRes += @"<tr style='FONT-WEIGHT: bold; FONT-SIZE: 12px; BACKGROUND-IMAGE: url(" + webServer + "fondoTotalResListas.gif);";
            sRes += @" COLOR: #ffffff; FONT-FAMILY: Arial, Helvetica, sans-serif; height:17px; background-color:#bcd4df;'>";
            sRes += @"<td></td></tr></table></td></tr>";

            return sRes;
        }
        private static byte GetModalidad(string sTipoContrato)
        {
            byte idModalidad = 4;
            switch (sTipoContrato)
            {
                case "ASISTENCIA TÉCNICA":
                    idModalidad = 1;
                    break;
                case "SERVICIOS ANSS":
                    idModalidad = 2;
                    break;
                case "PRODUCTO":
                    idModalidad = 3;
                    break;
                case "PROYECTO":
                    idModalidad = 4;
                    break;
                case "MTO PRODUCTOS":
                    idModalidad = 5;
                    break;
                case "SOLUCIONES":
                    idModalidad = 6;
                    break;
                case "PROYECTO SIN RIESGO ECONÓMICO":
                    idModalidad = 7;
                    break;
                case "SERVICIOS SIN ANS":
                    idModalidad = 8;
                    break;
            }

            return idModalidad;
        }
        private static DateTime GetFechaFin(DateTime dtIni, decimal dDuracion)
        {
            DateTime dtFin = dtIni;
            if (dDuracion>0)
            {
                int numMeses = Convert.ToInt32(Decimal.Round(dDuracion,0));
                dtFin = dtIni.AddMonths(numMeses);
            }
            return dtFin;
        }
        private Models.Mail BuscarCodRed(List<Models.Mail> oListaGestores, string sCodRed)
        {
            return oListaGestores.Find(delegate (Models.Mail item) { return (item.codred == sCodRed); });
        }

        #endregion
        #region Conexion base de datos y dispose
        private void OpenDbConn()
        {
            if (cDblib == null)
                cDblib = new IB.sqldblib.SqlServerSP(Shared.Database.GetConStr(), classOwnerID);
        }
        private void AttachDbConn(sqldblib.SqlServerSP extcDblib)
        {
            cDblib = extcDblib;
        }
        private void Dispose(bool disposing)
        {
            if (!this.disposed && disposing) if (cDblib != null && cDblib.OwnerID.Equals(classOwnerID)) cDblib.Dispose();
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~OportunidadNegocio()
        {
            Dispose(false);
        }

        #endregion
    }
}