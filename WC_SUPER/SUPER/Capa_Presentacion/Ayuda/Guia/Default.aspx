<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Ayuda_Guia_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<div style="overflow: auto; overflow-x: hidden; width: 338px; position:absolute; top:130px; left:30px;">
    <div align="left" style="width: 300px">
        <div align="center" style="background-image: url('../../../Images/imgFondoCal3.gif');background-repeat:no-repeat;
            width: 90px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;">
            &nbsp;General</div>
        <table border="0" cellpadding="0" cellspacing="0" class="texto" width="300px">
            <tr>
                <td background="../../../Images/Tabla/7.gif" height="6" width="6">
                </td>
                <td background="../../../Images/Tabla/8.gif" height="6">
                </td>
                <td background="../../../Images/Tabla/9.gif" height="6" width="6">
                </td>
            </tr>
            <tr>
                <td background="../../../Images/Tabla/4.gif" width="6">
                    &nbsp;</td>
                <td background="../../../Images/Tabla/5.gif" style="padding: 5px">
                    <!-- Inicio del contenido propio de la página -->
                    <br />
                    <DIV style="overflow: auto; overflow-x: hidden; width: 276px; height:140px">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:260px;">
                        <table class='texto MA' style='width: 260px; BORDER-COLLAPSE: collapse;' cellspacing=0 cellpadding=0 border=0>
                        <colgroup>
                            <col style="width:25px;" />
                            <col />
                        </colgroup>
                        <tr style="height:20px;" ondblclick="mostrarGuia('Genesis.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Génesis</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('FilosofiaSuper.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Filosofía SUPER</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('BuenasPracticas.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Buenas prácticas</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('SimbologiayConceptos.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Simbología y conceptos</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('EstructurayFiguras.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Estructura y figuras</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('NombramientoFiguras.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Nombramiento de Figuras</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('Preferencias.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Preferencias</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('Preferencias.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Preferencias</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('Bitacora.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Bitácora</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('Calendarios.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Calendarios</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('CodigosExternos.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Códigos externos</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('PlantillasGen.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>¿Qué son plantillas?</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('ReordenacionFilas.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Reordenación de filas</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('Criterios.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Criterios de selección</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('Multiresolucion.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Multiresolución</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('Multiresolucion.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Multiresolución</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('MisConsultas.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Mis consultas</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('Replicas.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Réplicas</td>
                        </tr>
                        </table>
                    </div>
                    </div>
                    <!-- Fin del contenido propio de la página -->
                </td>
                <td background="../../../Images/Tabla/6.gif" width="6">
                    &nbsp;</td>
            </tr>
            <tr>
                <td background="../../../Images/Tabla/1.gif" height="6" width="6">
                </td>
                <td background="../../../Images/Tabla/2.gif" height="6">
                </td>
                <td background="../../../Images/Tabla/3.gif" height="6" width="6">
                </td>
            </tr>
        </table>
    </div>
</div>
<div style="overflow: auto; overflow-x: hidden; width: 338px; position:absolute; top:130px; left:360px;">
    <div align="left" style="width: 300px">
        <div align="center" style="background-image: url('../../../Images/imgFondoCal3.gif');background-repeat:no-repeat;
            width: 90px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;">
            &nbsp;ADM</div>
        <table border="0" cellpadding="0" cellspacing="0" class="texto" width="300px">
            <tr>
                <td background="../../../Images/Tabla/7.gif" height="6" width="6">
                </td>
                <td background="../../../Images/Tabla/8.gif" height="6">
                </td>
                <td background="../../../Images/Tabla/9.gif" height="6" width="6">
                </td>
            </tr>
            <tr>
                <td background="../../../Images/Tabla/4.gif" width="6">
                    &nbsp;</td>
                <td background="../../../Images/Tabla/5.gif" style="padding: 5px">
                    <!-- Inicio del contenido propio de la página -->
                    <br />
                    <DIV style="overflow: auto; overflow-x: hidden; width: 276px; height:140px">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:260px;">
                        <table class='texto MA' style='width: 260px; BORDER-COLLAPSE: collapse;' cellspacing=0 cellpadding=0 border=0>
                        <colgroup>
                            <col style="width:25px;" />
                            <col />
                        </colgroup>
                        <tr style="height:20px;" ondblclick="mostrarGuia('CambioEstructuraContrato.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Cambio de estructura de contrato</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('CambioEstructuraProyecto.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Cambio de estructura de proyecto</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('CambioEstructuraUsuario.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Cambio de estructura de usuario</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('AbrirCerrarMes.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Apertura y cierre de mes de proyecto</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('BorradoProyecto.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Borrado de proyecto</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('CalculoGastosFinancieros.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Cálculo de gastos financieros</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('CambioEstadoProyecto.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Cambio de estado de proyecto</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('CargaFacturacionSAP.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Carga de facturación SAP</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('CierreIAPoEconomico.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Cierre mensual económico e IAP</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('CreacionPIG.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Creacion de proyectos PIG</td>
                        </tr>
                        </table>
                    </div>
                    </div>
                    <!-- Fin del contenido propio de la página -->
                </td>
                <td background="../../../Images/Tabla/6.gif" width="6">
                    &nbsp;</td>
            </tr>
            <tr>
                <td background="../../../Images/Tabla/1.gif" height="6" width="6">
                </td>
                <td background="../../../Images/Tabla/2.gif" height="6">
                </td>
                <td background="../../../Images/Tabla/3.gif" height="6" width="6">
                </td>
            </tr>
        </table>
    </div>
</div>
<div style="overflow: auto; overflow-x: hidden; width: 338px; position:absolute; top:130px; left:690px;">
    <div align="left" style="width: 300px">
        <div align="center" style="background-image: url('../../../Images/imgFondoCal3.gif');background-repeat:no-repeat;
            width: 90px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;">
            &nbsp;CVT</div>
        <table border="0" cellpadding="0" cellspacing="0" class="texto" width="300px">
            <tr>
                <td background="../../../Images/Tabla/7.gif" height="6" width="6">
                </td>
                <td background="../../../Images/Tabla/8.gif" height="6">
                </td>
                <td background="../../../Images/Tabla/9.gif" height="6" width="6">
                </td>
            </tr>
            <tr>
                <td background="../../../Images/Tabla/4.gif" width="6">
                    &nbsp;</td>
                <td background="../../../Images/Tabla/5.gif" style="padding: 5px">
                    <!-- Inicio del contenido propio de la página -->
                    <br />
                    <div style="overflow: auto; overflow-x: hidden; width: 276px; height:140px">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:260px;">
                        <table class='texto MA' style='width: 260px; BORDER-COLLAPSE: collapse;' cellspacing=0 cellpadding=0 border=0>
                        <colgroup>
                            <col style="width:25px;" />
                            <col />
                        </colgroup>
                        <tr style="height:20px;" ondblclick="mostrarGuia('GuiaMiCV.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Mi currículum</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('GuiaFormacionAcademica.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Formación académica</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('GuiaAccionesFormativas.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Acciones formativas</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('GuiaExperienciasIB.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Experiencia profesional en IBERMÁTICA</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('GuiaExperienciasNoIB.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Experiencia profesional fuera de IBERMÁTICA</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('GuiaCertificaciones.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Certificaciones</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('GuiaIdiomas.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>idiomas</td>
                        </tr>
                        </table>
                    </div>
                    </div>
                    <!-- Fin del contenido propio de la página -->
                </td>
                <td background="../../../Images/Tabla/6.gif" width="6">
                    &nbsp;</td>
            </tr>
            <tr>
                <td background="../../../Images/Tabla/1.gif" height="6" width="6">
                </td>
                <td background="../../../Images/Tabla/2.gif" height="6">
                </td>
                <td background="../../../Images/Tabla/3.gif" height="6" width="6">
                </td>
            </tr>
        </table>
    </div>
</div>

<div style="overflow: auto; overflow-x: hidden; width: 338px; position:absolute; top:400px; left:30px;">
    <div align="left" style="width: 300px">
        <div align="center" style="background-image: url('../../../Images/imgFondoCal3.gif');background-repeat:no-repeat;
            width: 90px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;">
            &nbsp;PGE</div>
        <table border="0" cellpadding="0" cellspacing="0" class="texto" width="300px">
            <tr>
                <td background="../../../Images/Tabla/7.gif" height="6" width="6">
                </td>
                <td background="../../../Images/Tabla/8.gif" height="6">
                </td>
                <td background="../../../Images/Tabla/9.gif" height="6" width="6">
                </td>
            </tr>
            <tr>
                <td background="../../../Images/Tabla/4.gif" width="6">
                    &nbsp;</td>
                <td background="../../../Images/Tabla/5.gif" style="padding: 5px">
                    <!-- Inicio del contenido propio de la página -->
                    <br />
                    <DIV style="overflow: auto; overflow-x: hidden; width: 276px; height:140px">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:260px;">
                        <table class='texto MA' style='width: 260px; BORDER-COLLAPSE: collapse;' cellspacing=0 cellpadding=0 border=0>
                        <colgroup>
                            <col style="width:25px;" />
                            <col />
                        </colgroup>
                        <tr style="height:20px;" ondblclick="mostrarGuia('DetalleProyecto.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Detalle de proyecto</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('NuevoProyecto.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Creación de nuevo proyecto</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('DetalleEconomico.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Detalle económico</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('DetalleEconomico.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Detalle económico</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('ResumenEconomico.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Resumen económico</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('Cierre.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Cierre</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('CierreMensualProyectos.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Cierre global</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('Replica.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Réplica</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('ReplicaSimple.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Réplica simple</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('ReplicaGlobal.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Réplica global</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('CostesNodo.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Costes por nivel</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('AvanceGlobal.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Avance global</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('MantenimientoSubnodos.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Mantenimiento de <%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %></td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('TarifasCliente.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Tarifas por perfil (Cliente)</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('TarifasNodo.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Tarifas por  perfil (<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>)</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('TraspasoIAP.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Traspaso IAP</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('ValorGanado.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Valor Ganado</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('GuiaCM.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Cuadro de mando</td>
                        </tr>
                        </table>
                    </div>
                    </div>
                    <!-- Fin del contenido propio de la página -->
                </td>
                <td background="../../../Images/Tabla/6.gif" width="6">
                    &nbsp;</td>
            </tr>
            <tr>
                <td background="../../../Images/Tabla/1.gif" height="6" width="6">
                </td>
                <td background="../../../Images/Tabla/2.gif" height="6">
                </td>
                <td background="../../../Images/Tabla/3.gif" height="6" width="6">
                </td>
            </tr>
        </table>
    </div>
</div>
<div style="overflow: auto; overflow-x: hidden; width: 338px; position:absolute; top:400px; left:360px;">
    <div align="left" style="width: 300px">
        <div align="center" style="background-image: url('../../../Images/imgFondoCal3.gif');background-repeat:no-repeat;
            width: 90px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;">
            &nbsp;PST</div>
        <table border="0" cellpadding="0" cellspacing="0" class="texto" width="300px">
            <tr>
                <td background="../../../Images/Tabla/7.gif" height="6" width="6">
                </td>
                <td background="../../../Images/Tabla/8.gif" height="6">
                </td>
                <td background="../../../Images/Tabla/9.gif" height="6" width="6">
                </td>
            </tr>
            <tr>
                <td background="../../../Images/Tabla/4.gif" width="6">
                    &nbsp;</td>
                <td background="../../../Images/Tabla/5.gif" style="padding: 5px">
                    <!-- Inicio del contenido propio de la página -->
                    <br />
                    <DIV style="overflow: auto; overflow-x: hidden; width: 276px; height:140px">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:260px;">
                        <TABLE class='texto MA' style='width: 260px; BORDER-COLLAPSE: collapse;' cellspacing=0 cellpadding=0 border=0>
                        <colgroup>
                            <col style="width:25px;" />
                            <col />
                        </colgroup>
                            <tr style="height:20px;" ondblclick="mostrarGuia('ImportardeOpenProj.pdf')">
                                <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                                <td>Importar cronograma desde OpenProj</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('ExportaraOpenproj.pdf')">
                                <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                                <td>Exportar estructura técnica a OpenProj</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('EstructuraTecnica.pdf')">
                                <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                                <td>Estructura técnica de proyecto</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('EstructuraTecnica.wmv')">
                                <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                                <td>Estructura técnica de proyecto</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('DetallePT.pdf')">
                                <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                                <td>Detalle de proyecto técnico</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('DetalleProyectoTecnico.wmv')">
                                <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                                <td>Detalle de proyecto técnico</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('DetalleFase.pdf')">
                                <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                                <td>Detalle de fase</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('DetalleActividad.pdf')">
                                <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                                <td>Detalle de actividad</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('DetalleTarea.pdf')">
                                <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                                <td>Detalle de tarea</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('DetalleTarea.wmv')">
                                <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                                <td>Detalle de tarea</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('DetalleHito.pdf')">
                                <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                                <td>Detalle de hito</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('AvanceTecnico.pdf')">
                                <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                                <td>Avance técnico</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('CET.pdf')">
                                <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                                <td>Criterios estadísticos técnicos</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('OTC.pdf')">
                                <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                                <td>Orden de trabajo codificada</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('OTL.pdf')">
                                <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                                <td>Orden de trabajo libre</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('PlantillasPST.pdf')">
                                <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                                <td>Plantillas</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('OficinaTecnica.pdf')">
                                <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                                <td>Oficina técnica</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('GrupoFuncional.pdf')">
                                <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                                <td>Grupo funcional</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('OrigenesdeTareas.pdf')">
                                <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                                <td>Origen de tarea</td>
                            </tr>
                            <tr style="height:20px;" ondblclick="mostrarGuia('ValorGanado.pdf')">
                                <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                                <td>Valor Ganado</td>
                            </tr>
                        </table>
                    </div>
                    </div>
                    <!-- Fin del contenido propio de la página -->
                </td>
                <td background="../../../Images/Tabla/6.gif" width="6">
                    &nbsp;</td>
            </tr>
            <tr>
                <td background="../../../Images/Tabla/1.gif" height="6" width="6">
                </td>
                <td background="../../../Images/Tabla/2.gif" height="6">
                </td>
                <td background="../../../Images/Tabla/3.gif" height="6" width="6">
                </td>
            </tr>
        </table>
    </div>
</div>
<div style="overflow: auto; overflow-x: hidden; width: 338px; position:absolute; top:400px; left:690px;">
    <div align="left" style="width: 300px">
        <div align="center" style="background-image: url('../../../Images/imgFondoCal3.gif');background-repeat:no-repeat;
            width: 90px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;">
            &nbsp;IAP</div>
        <table border="0" cellpadding="0" cellspacing="0" class="texto" width="300px">
            <tr>
                <td background="../../../Images/Tabla/7.gif" height="6" width="6">
                </td>
                <td background="../../../Images/Tabla/8.gif" height="6">
                </td>
                <td background="../../../Images/Tabla/9.gif" height="6" width="6">
                </td>
            </tr>
            <tr>
                <td background="../../../Images/Tabla/4.gif" width="6">
                    &nbsp;</td>
                <td background="../../../Images/Tabla/5.gif" style="padding: 5px">
                    <!-- Inicio del contenido propio de la página -->
                    <br />
                    <DIV style="overflow: auto; overflow-x: hidden; width: 276px; height:140px">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:260px;">
                        <TABLE class='texto MA' style='width: 260px; BORDER-COLLAPSE: collapse;' cellspacing=0 cellpadding=0 border=0>
                        <colgroup>
                            <col style="width:25px;" />
                            <col />
                        </colgroup>
                        <tr style="height:20px;" ondblclick="mostrarGuia('ImputacionCalendario.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Imputación día a día</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('ImputacionCalendario.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Imputación día a día</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('ImputacionMasiva.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Imputación masiva</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('ImputacionMasiva.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Imputación masiva</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('Agenda.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Agenda</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('Agenda.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Agenda</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('ConsultaImputaciones.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Consulta de imputaciones</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('ConsultaImputaciones.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Consulta de imputaciones</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('ConsultaFacturabilidad.pdf')">
                            <td><img src="../../../Images/Botones/imgPDF.gif" class="ICO" /></td>
                            <td>Consulta de facturabilidad</td>
                        </tr>
                        <tr style="height:20px;" ondblclick="mostrarGuia('ConsultaFacturabilidad.wmv')">
                            <td><img src="../../../Images/Botones/imgVideo.gif" class="ICO" /></td>
                            <td>Consulta de facturabilidad</td>
                        </tr>
                        </table>
                    </div>
                    </div>
                    <!-- Fin del contenido propio de la página -->
                </td>
                <td background="../../../Images/Tabla/6.gif" width="6">
                    &nbsp;</td>
            </tr>
            <tr>
                <td background="../../../Images/Tabla/1.gif" height="6" width="6">
                </td>
                <td background="../../../Images/Tabla/2.gif" height="6">
                </td>
                <td background="../../../Images/Tabla/3.gif" height="6" width="6">
                </td>
            </tr>
        </table>
    </div>
</div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

