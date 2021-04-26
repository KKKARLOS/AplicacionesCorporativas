<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_ContratosHermes_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="cb1" TagName="Menu" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="cb1" TagName="Head" %>

<!DOCTYPE html>

<html lang="es" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:Head runat="server" id="Head" />
    <title>::: SUPER ::: - Generación de contratos desde HERMES</title>    
    <link rel="stylesheet" type="text/css" href="<% =Session["strServer"].ToString() %>plugins/daterangepicker/daterangepicker.css" />
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
        
    <form id="frmDatos" runat="server">
     <input type="hidden" id="hdnConsumos" name="ctl00$CPHC$hdnConsumos"  value="" />
    </form>
    <div class="container-fluid ocultable">
        <div class="ibox-content blockquote blockquote-info">
            <div class="panel-group">
                <div class="ibox-title">
                    <h1 class="sr-only">::: SUPER ::: - Generación de contratos desde HERMES</h1>

                    <div class="row">
                        <h2 class="sr-only">Criterios de selección</h2>
                        <div class="col-xs-12 col-sm-4">
                            <fieldset class="fieldset">
                                <legend class="fieldset"><span>Periodo</span></legend>
                                    <div class="form-group">
                                        <div class="col-xs-12 col-md-10 col-md-offset-1 col-lg-8 col-lg-offset-2">
                                            <input title="Formato fecha: dd/mm/aaaa - dd/mm/aaaa" placeholder="dd/mm/aaaa - dd/mm/aaaa" id="txtRango" type="text" class="form-control input-md fk_filtro" value="" />
                                        </div>
                                    </div>
                            </fieldset>
                        </div>
                        <div class="col-xs-12 col-sm-4"><br />
                            <label for="txtCR" role="link" class="underline btn-link col-xs-12 col-sm-1 control-label fk-label no-padding-xs-sm">
                                <span id="lblCR">C.R.</span>
                            </label>
                            <div class="col-xs-12 col-sm-10 no-padding-xs-sm">                                    
                                <div class="input-group no-padding">
                                    <input id="txtCR" name="" type="text" class="form-control input-md fk_filtro fk_filtro_proy fk_filtro_proy_input" value="" disabled="disabled"/>                                    
                                    <span class="input-group-btn">
                                        <button class="btn btn-default btn-inline-input fk_filtro" type="button" id="btnEliminarCR" aria-label="Eliminar la selección de CR actual">
                                            <i class="fa fa-trash-o" aria-hidden="true"></i>
                                        </button>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-4">
                        <div id="divBotones">
                            <button id="btnObtener" type="button" class="btn btn-primary">
                                <span>Obtener</span>
                            </button>
                        </div>
                        </div>
                    </div>
                    <div class="row margin-bottom">
                    </div>
                    <div class="row">
                        <div id="contenedor-tabla">
                            <h2 class="sr-only">Oportunidades de negocio en HERMES</h2>
                            <table summary="Tabla de oportunidades de negocio en HERMES" id="tablaON" class="table table-bordered table-no-border display">
                                <thead>
                                    <tr>                            
                                        <th id="hCh" class="cabecera bg-primary"></th>
                                        <th id="N" class="cabecera bg-primary"><abbr title="Número de contrato">Nº</abbr></th>
                                        <th id="E" class="cabecera bg-primary"><abbr title="Extensión">Ext.</abbr></th>
                                        <th id="D" class="cabecera bg-primary">Descripción</th>
                                        <th id="O" class="cabecera bg-primary"><abbr title="Organización Comercial">Org. Comercial</abbr></th>
                                        <th id="C" class="cabecera bg-primary">Cliente</th>
                                        <th id="M" class="cabecera bg-primary">Comercial</th>
                                        <th id="G" class="cabecera bg-primary">Gestor Producción</th>
                                        <th id="F" class="cabecera bg-primary"><abbr title="Fecha de Contratación">F.Contr.</abbr></th>
                                        <th id="IS" class="cabecera bg-primary">Imp.Servicio</th>
                                        <th id="MS" class="cabecera bg-primary">Mrg.Servicio</th>
                                        <th id="IP" class="cabecera bg-primary">Imp.Producto</th>
                                        <th id="MP" class="cabecera bg-primary">Mrg.Producto</th>
                                        <th id="LO" class="cabecera bg-primary">Línea Oferta</th>
                                    </tr>
                                </thead>
                                <tbody id="bodyTabla">                        
                                </tbody>
                            </table>
                        </div>                
                    </div>
                    <div class="row">
					    <div id="divProf" title="Responsable de contrato" class="link text-center col-xs-12 col-sm-offset-6 col-sm-6" role="link" aria-label="Responsable de contrato" tabindex="0" >						
						    <span id="icoProf" role="link" class="fa fa-user fa-2x"></span>
                            <span class="txtProfesional">Responsable para contratos a generar:</span>
						    <span class="txtProfesional" id="spProfesional"></span>
					    </div>
                    </div>
                    <div class="row">
                        <div id="pieAsunto" class="pull-right" >
                            <div class="col-pie">
                                <button id="btnGrabar" type="button" class="btn btn-primary btn-default-lite">
                                    <span class="fa fa-share-square-o-rev fa-2x fa-blanco" style="vertical-align: middle;"></span>
                                    <span>Generar</span>
                                </button>
                                <button id="btnSalir" type="button" class="btn btn-default btn-default-lite">
                                    <span class="fa fa-share-square-o-rev fa-2x fa-blanco" style="vertical-align: middle;"></span>
                                    <span>Volver</span>
                                </button>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="buscadorUsuario" id="buscProf"></div>
    <div class="fk_ayudaCatalogoBasico"></div>

        <!-- Modal de selección de proyecto -->
    <div class="modal fade ocultable2" id="modal-Proyecto" role="dialog" tabindex="-1" title="::SUPER::: - Selección de proyectos">
        <div class="modal-dialog modal-lg" role="dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                    <h2 class="modal-title">:::SUPER::: - Proyectos generados</h2>
                </div>
                <div class="modal-body">
                    <table id="tablaProyectos" class="table table-bordered table-no-border display">
                        <thead>
                            <tr>
                                <th id="NumProy" class="cabecera bg-primary text-left"><abbr title="Número de proyecto">Nº</abbr></th>
                                <th id="DesProy" class="cabecera bg-primary text-left">Proyecto</th>
                                <th id="NumContr" class="cabecera bg-primary text-left">Contrato</th>
                                <th id="NumExte" class="cabecera bg-primary text-left">Extensión</th>
                            </tr>
                        </thead>
                        <tbody id="bodyTablaProyectos">
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <b><button id="btnCancelarProyecto" class="btn btn-default" data-dismiss="modal">Cerrar</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    </body>

</html>
<script src="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/scripts/JavaScript.js" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/moment.locale.min.js"></script>
<script type="text/javascript" src="<% =Session["strServer"].ToString() %>plugins/daterangepicker/daterangepicker.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/string.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/accounting.min.js" type="text/javascript" ></script>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/Buttons/dataTables.buttons.min.js"></script>
<script type="text/javascript" src="<% =Session["strServer"].ToString() %>plugins/datatables/Checkboxes/dataTables.checkboxes.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/IB/buscacatalogobasico.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/BuscadorPersonas/BuscadorPersonas.js"></script>

<script src="js/view.js?v=20180207_03" type="text/javascript"></script>
<script src="js/app.js?v=20180207" type="text/javascript"></script>



