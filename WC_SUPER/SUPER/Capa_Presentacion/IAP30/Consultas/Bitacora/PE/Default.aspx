<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_IAP30_Consultas_Bitacora_PE_Default" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="cb1" TagName="Menu" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="cb1" TagName="Head" %>

<!DOCTYPE html>

<html lang="es" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:Head runat="server" id="Head" />
    <title>::: SUPER ::: - Consulta de bitácoras de Proyecto Económico</title>    
    <link rel="stylesheet" type="text/css" href="<% =Session["strServer"].ToString() %>plugins/daterangepicker/daterangepicker.css?20170503_01" />
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.css"/>
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>plugins/datatables/Buttons/buttons.dataTables.min.css"/>
    <link type="text/css" href="<% =Session["strServer"].ToString() %>plugins/datatables/Checkboxes/dataTables.checkboxes.css" rel="stylesheet" />   
</head>

<body>
    <script>
        var opciones = { delay: 1 }
        IB.procesando.opciones(opciones);
        IB.procesando.mostrar();
    </script>
    <cb1:Menu runat="server" id="Menu" />    
        
    <%--<form id="form1" runat="server"></form>--%>
    <form id="frmDatos" runat="server">
     <input type="hidden" id="FORMATO" name="ctl00$CPHC$FORMATO" value="PDF" />
     <input type="hidden" id="hdnConsumos" name="ctl00$CPHC$hdnConsumos"  value="" />
    </form>
    <div class="container-fluid ocultable">
        <div class="ibox-content blockquote blockquote-info">
        <div class="panel-group">
            <div class="ibox-title">
        <h1 class="sr-only">::: SUPER ::: - Consulta de bitácoras de Proyecto Económico</h1>

        <div class="row">
            <h2 class="sr-only">Criterios de selección</h2>
            <div class="col-xs-12 col-sm-4">
                <div class="col-xs-12 col-md-4 col-lg-3">
                    <label id="lblProy" title="Selección de proyecto" role="link" class="control-label fk-label btn-link txtLinkU">Proyecto</label><span class="text-danger"> *</span>
                    </div>   
                <div class="col-xs-12 col-md-8 col-lg-9">
                    <input id="txtPro" name="" type="text" class="form-control input-md fk_filtro" value="" aria-required="true" required="required" runat="server" />
                </div>
            </div>
            <div class="col-xs-12 col-sm-4">
                <label for="txtDenominacion" class="col-xs-12 col-sm-1 col-md-1 col-lg-2 control-label fk-label">Denominación</label>
                <div class="col-xs-12 col-sm-11 col-md-11 col-lg-10">
                    <input id="txtDenominacion" name="" type="text" class="form-control input-md fk_Proyecto fk_filtro" value="" runat="server" />
                </div>
            </div>

            <div class="col-xs-12 col-sm-4">
                <fieldset class="fieldset">
                    <legend class="fieldset"><span>Tipo búsqueda</span></legend>
                    <div class="col-xs-6 col-sm-12 col-md-6 checkbox no-margin">
                        <label>
                            <input id="chkAutomatica" type="checkbox" class="fk_chk fk_filtro"  runat="server" />Automática
                        </label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-6 checkbox no-margin text-nowrap">
                        <label>
                            <input id="chkConAcciones" type="checkbox" class="fk_chk fk_filtro" runat="server" />Incluir acciones
                        </label>
                    </div>
                </fieldset>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12 col-sm-3">
                <label class="col-xs-12 col-sm-1 col-md-1 col-lg-2 control-label fk-label" for="cboTipo">Tipo</label>
                <div class="col-xs-12 col-sm-11 col-md-11 col-lg-10">
                    <select id="cboTipo" name="cboTipo" class="form-control fk_filtro" runat="server">
                    </select>
                </div>
            </div>
            <div class="col-xs-12 col-sm-3">
                <label class="col-xs-12 col-sm-1 col-md-1 col-lg-2 control-label fk-label" for="cboEstado">Estado</label>
                <div class="col-xs-12 col-sm-11 col-md-11 col-lg-10">
                    <select id="cboEstado" name="cboEstado" class="form-control fk_filtro" runat="server">
                        <option value="0">Todos</option>
                        <option value="1">Registrado</option>
                        <option value="2">Asignado</option>
                        <option value="3">Resuelto</option>
                        <option value="4">Verificado</option>
                        <option value="5">Aprobado</option>
                        <option value="6">Reabierto</option>
                    </select>
                </div>
            </div>
            <div class="col-xs-12 col-sm-3">
                <label class="col-md-12 col-lg-2 control-label fk-label" for="cboSeveridad">Severidad</label>
                <div class="col-md-12 col-lg-10">
                    <select id="cboSeveridad" name="cboSeveridad" class="form-control fk_filtro" runat="server">
                        <option value="0">Todas</option>
                        <option value="1">Crítica</option>
                        <option value="2">Grave</option>
                        <option value="3">Normal</option>
                        <option value="4">Leve</option>
                    </select>
                </div>
            </div>
            <div class="col-xs-12 col-sm-3">
                <label class="col-xs-12 col-sm-1 col-md-1 col-lg-2 control-label fk-label" for="cboPrioridad">Prioridad</label>
                <div class="col-xs-12 col-sm-11 col-md-11 col-lg-10">
                    <select id="cboPrioridad" name="cboPrioridad" class="form-control fk_filtro" runat="server">
                        <option value="0">Todas</option>
                        <option value="1">Máxima</option>
                        <option value="2">Alta</option>
                        <option value="3">Media</option>
                        <option value="4">Baja</option>
                    </select>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-xs-10 col-sm-6 col-lg-4">
                <fieldset class="fieldset">
                    <legend class="fieldset"><span>Notificación</span></legend>
                        <div class="form-group">
                            <div class="col-xs-12 col-lg-10">
                                <input title="Formato fecha: dd/mm/aaaa - dd/mm/aaaa" placeholder="dd/mm/aaaa - dd/mm/aaaa" id="txtNotif" type="text" class="form-control input-md fk_fecha" value="" />
                            </div>
                        </div>
                </fieldset>
            </div>
            <div class="col-xs-10 col-sm-6 col-lg-4">
                <fieldset class="fieldset">
                    <legend class="fieldset"><span>Límite</span></legend>
                        <div class="form-group">
                            <div class="col-xs-12 col-lg-10">
                                <input title="Formato fecha: dd/mm/aaaa - dd/mm/aaaa" placeholder="dd/mm/aaaa - dd/mm/aaaa" id="txtLimite" type="text" class="form-control input-md fk_fecha" value="" />
                            </div>
                        </div>
                </fieldset>
            </div>
            <div class="col-xs-10 col-sm-6 col-lg-4">
                <fieldset class="fieldset">
                    <legend class="fieldset"><span>Fin</span></legend>
                        <div class="form-group">
                            <div class="col-xs-12 col-lg-10">
                                <input title="Formato fecha: dd/mm/aaaa - dd/mm/aaaa" placeholder="dd/mm/aaaa - dd/mm/aaaa" id="txtFin" type="text" class="form-control input-md fk_fecha" value="" />
                            </div>
                        </div>
                </fieldset>
            </div>
        </div>

        <div class="row margin-bottom">
            <div id="divBotones">
                <button id="btngoAsunto" type="button" class="btn btn-default">
                    <span>Detalle</span>
                </button>                
                <button id="btnObtener" type="button" class="btn btn-primary">
                    <span>Obtener</span>
                </button>
            </div>
        </div>
        <div class="row">
            <div id="contenedor-tabla">
                <h2 class="sr-only">Tabla de elementos de bitácora de proyecto económico</h2>
                <table summary="Tabla de elementos de bitácora de proyecto económico" id="tablaResultados" class="table table-bordered table-no-border display" width="100%" >
                    <thead>
                        <tr>                            
                            <th id="N" class="cabecera bg-primary">Notificación</th>
                            <th id="T" class="cabecera bg-primary">Tipo</th>
                            <th id="D" class="cabecera bg-primary">Denominación</th>
                            <th id="S" class="cabecera bg-primary"><abbr title="Severidad">Sev.</abbr></th>
                            <th id="P" class="cabecera bg-primary"><abbr title="Prioridad">Prio.</abbr></th>
                            <th id="L" class="cabecera bg-primary">Límite</th>
                            <th id="F" class="cabecera bg-primary">Fin</th>
                            <th id="A" class="cabecera bg-primary">Avance</th>
                            <th id="E" class="cabecera bg-primary">Estado</th>
                            <th id="X" class="cabecera bg-primary">Descripción</th>
                        </tr>
                    </thead>
                    <tbody id="bodyTabla">                        
                    </tbody>
                </table>
            </div>    
        </div>
        </div>
        </div>
        </div>
    </div>
    <br />

    <!-- Modal de selección de proyecto -->
    <div class="fk_ayudaProyecto"></div>
    <div class="fk_ayudaCatalogoBasico"></div>
    <input type="hidden" name="hdnIdProyectoSubNodo" id="hdnIdProyectoSubNodo" value="" runat="server" /> 
    </body>

</html>
<script src="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/scripts/JavaScript.js" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/moment.locale.min.js"></script>
<script type="text/javascript" src="<% =Session["strServer"].ToString() %>plugins/daterangepicker/daterangepicker.js?20170503_01"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/string.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/accounting.min.js" type="text/javascript" ></script>
<%--<script src="js/JavaScript.js" type="text/javascript"></script>--%>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/Buttons/dataTables.buttons.min.js"></script>
<script type="text/javascript" src="<% =Session["strServer"].ToString() %>plugins/datatables/Checkboxes/dataTables.checkboxes.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/IB/buscacatalogobasico.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/IB/buscaproyecto.js?20171214_07" type="text/javascript"></script>

<script src="js/view.js?20180110_04" type="text/javascript"></script>
<script src="js/app.js?20171214_17" type="text/javascript"></script>



