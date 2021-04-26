<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Reporte_Fichero_Default" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="cb1" TagName="Menu" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="cb1" TagName="Head" %>
<!DOCTYPE html>

<html lang="es" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:Head runat="server" id="Head" />
    <link href="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/Reporte/Fichero/css/StyleSheet.css" rel="stylesheet"/>
    <link href="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/Reporte/Fichero/css/uploadfile.css" rel="stylesheet"/>
    <title>::: SUPER ::: - Carga de datos en IAP desde fichero</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="Stylesheet" href="../../../../css/jquery-ui.min.css"/>
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.css"/>
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>plugins/datatables/Buttons/buttons.dataTables.min.css"/>
    <!--<link href="http://hayageek.github.io/jQuery-Upload-File/4.0.10/uploadfile.css" rel="stylesheet">-->
</head>

<body>    
    <script>
        var opciones = { delay: 1 }
        IB.procesando.opciones(opciones);
        IB.procesando.mostrar();
    </script>

    <cb1:Menu runat="server" id="Menu" />
    <form id="frmDatos" class="form-horizontal" runat="server" enctype="multipart/form-data"></form>
    <div class="container-fluid ocultable">
            <h1 class="hiddenStructure">::: SUPER ::: - Carga de datos en IAP desde fichero</h1>
            <br class="hidden-xs" />
            <br />
        <!---->
        <div class="ibox-content blockquote blockquote-info" style="visibility:hidden;">
            <div class="panel-group">
                <div class="ibox-title">
                    <div class="row">
                        <div class="col-xs-12 col-sm-9 col-md-7 col-lg-6">
                            <h2 class="hiddenStructure">Entrada de datos</h2>
                            <!--<div class="filegroup margin-bottom">-->
                            <!--<label for="inputFichero">Fichero</label><input type="file" id="inputFichero"/>-->
                            <div id="fileuploader">Upload</div>
                            <!--</div>-->
                        </div>
                        <div class="col-xs-12 col-sm-3 col-md-5 col-lg-6">
                            <p id="btnFormato" role="link" tabindex="0" class="txtLinkU">Ver formato fichero entrada</p>
                        </div>
                    </div>
                    <div class="row text-center">
                        <fieldset class="fieldset">
                            <div class="col-xs-12 margin-top no-padding">
                                <div class="col-xs-8 col-sm-6 col-md-4 col-lg-3">
                                    <div class="form-group">
                                        <fieldset class="fieldset">
                                            <legend class="fieldset"><span>Tipo de imputación</span></legend>
                                            <div>
                                                <label class="radio-inline">
                                                    <input type="radio" id="radioD" checked="checked" name="optradio" value="D" />Diaria</label>
                                                <label class="radio-inline">
                                                    <input type="radio" id="radioM" name="optradio" value="M" />Masiva</label>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="col-xs-8 col-sm-6">
                                    <button id="btnAnalizar" class="btn btn-primary">Analizar</button>
                                    <button id="btnVisualizar" class="btn btn-primary" style="display: none">Visualizar</button>
                                    <button id="btnGrabar" class="btn btn-primary">Grabar</button>
                                </div>
                            </div>
                            <!--
                        <div class="col-xs-12 margin-top no-padding">                                                          
                            <div class="col-xs-5 col-sm-3 no-padding margin-bottom-xs ">
                                <div class="form-group">
                                    <fieldset class="fieldset">
                                        <legend class="fieldset"><span>Tipo de imputación</span></legend>
                                        <div>
                                            <label class="radio-inline">
                                                <input type="radio" id="radioD" checked="checked" name="optradio" value="D"/>Diaria</label>
                                            <label class="radio-inline">
                                                <input type="radio" id="radioM" name="optradio" value="M" />Masiva</label>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="col-xs-8 col-sm-5 no-padding">
                                <label for="uFichero" class="col-xs-8 col-md-6 control-label fk-label text-left">Último fichero cargado:</label>    
                                <div class="col-lg-12">
                                    <input id="uFichero" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true" runat="server"/>                                       
                                </div>
                            </div>
                        </div>
                        -->
                            <div class="col-xs-12 margin-top no-padding">
                                <div class="col-xs-12 col-sm-4 no-padding margin-bottom-xs">
                                    <div class="form-group">
                                        <label for="nFilas" class="col-xs-8 col-md-6 control-label fk-label text-left">Nº de filas:</label>
                                        <div class="col-xs-6 col-md-5">
                                            <input id="nFilas" name="" type="text" class="form-control input-md text-right" value="0" disabled="disabled" aria-readonly="true" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-4 no-padding margin-bottom-xs">
                                    <div class="form-group">
                                        <label for="nFilasC" class="col-xs-8 col-md-6 control-label fk-label text-left">Nº de filas correctas:</label>
                                        <div class="col-xs-6 col-md-5">
                                            <input id="nFilasC" name="" type="text" class="form-control input-md text-right" value="0" disabled="disabled" aria-readonly="true" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-4 no-padding">
                                    <div class="form-group">
                                        <label for="nFilasE" class="col-xs-8 col-md-6 control-label fk-label text-left">Nº de filas erróneas:</label>
                                        <div class="col-xs-6 col-md-5">
                                            <input id="nFilasE" name="" type="text" class="form-control input-md text-right" value="0" disabled="disabled" aria-readonly="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <br />
                    <div class="row">
                        <fieldset class="fieldset">
                            <h2 class="hiddenStructure">Relación de filas erróneas en el proceso de validación</h2>
                            <legend class="fieldset"><span>Relación de filas erróneas en el proceso de validación</span></legend>
                            <div class="table-responsive">
                                <div id="divTablaV">
                                    <table id="tablaV" class="table" width="100%">
                                        <thead>
                                            <tr>
                                                <th data-type="String" id="UsuV" class="bg-primary">Usuario</th>
                                                <th data-type="DateTime" id="FV" class="bg-primary">Fecha</th>
                                                <th data-type="String" id="TV" class="bg-primary">Tarea</th>
                                                <th data-type="Double" id="EsV" class="bg-primary">Esfuerzo</th>
                                                <th data-type="String" id="ErrV" class="bg-primary">Error</th>
                                            </tr>
                                        </thead>
                                        <tbody id="bodyTabla">
                                        </tbody>
                                    </table>
                                </div>
                                <div id="divTablaVM" style="display: none;">
                                    <table id="tablaVM" class="table" width="100%">
                                        <thead>
                                            <tr>
                                                <th data-type="String" id="UsuM" class="bg-primary">Usuario</th>
                                                <th data-type="DateTime" id="FDM" class="bg-primary">Desde</th>
                                                <th data-type="DateTime" id="FhM" class="bg-primary">Hasta</th>
                                                <th data-type="String" id="TM" class="bg-primary">Tarea</th>
                                                <th data-type="Double" id="EsM" class="bg-primary">Esfuerzo</th>
                                                <th data-type="String" id="FestM" class="bg-primary">Festivo</th>
                                                <th data-type="String" id="ErrM" class="bg-primary">Error</th>
                                            </tr>
                                        </thead>
                                        <tbody id="bodyTablaM">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <fieldset class="fieldset">
                            <h2 class="hiddenStructure">Relación de filas erróneas en el proceso de grabación</h2>
                            <legend class="fieldset"><span>Relación de filas erróneas en el proceso de grabación</span></legend>
                            <div class="table-responsive">
                                <table id="tablaG" class="table" width="100%">
                                    <thead>
                                        <tr>
                                            <th data-type="String" id="NLineaG" class="cabecera bg-primary">Nº Línea</th>
                                            <th data-type="String" id="ErrG" class="cabecera bg-primary">Error</th>
                                        </tr>
                                    </thead>
                                    <tbody id="bodyTabla2">
                                    </tbody>
                                </table>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal de visualización del formato del fichero de entrada -->
    <div class="modal fade" id="modal-Formato" role="dialog" tabindex="-1" title=":::SUPER::: - Carga de datos en IAP desde fichero - Formato del fichero de entrada">
        <div class="modal-dialog modal-lg" role="dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                    <h1 class="modal-title">:::SUPER::: - Carga de datos en IAP desde fichero - Formato del fichero de entrada</h1>
                </div>
                <div class="modal-body">
                    <div class="row row-modal">
                        <div id="estructura_dia" class="col-xs-12">
                            <div class="text-center"><b>Estructura fichero de entrada</b></div>
                            <br/>
                            - Los campos del fichero deben estar separados por tabuladores:<br/>
                            <br/>
                            1.- Identificador de tarea.<br/>
                            2.- Identificador de usuario.<br/>
                            3.- Fecha (DD/MM/AAAA).<br/>
                            4.- Esfuerzo en horas.<br/>
                            5.- Comentarios<br/>
                        </div>
                        <div id="estructura_masiva" class="col-xs-12">
                            <div class="text-center"><b>Estructura fichero de entrada</b></div>
                            <br/>
                            - Los campos del fichero deben estar separados por tabuladores:<br/>
                            <br/>
                            1.- Identificador de tarea.<br/>
                            2.- Identificador de usuario.<br/>
                            3.- Fecha desde (DD/MM/AAAA).<br/>
                            4.- Fecha hasta (DD/MM/AAAA).<br/>
                            5.- Esfuerzo en horas.<br/>
                            6.- Permitir imputar festivos o no laborables<br/>
                            7.- Comentarios<br/>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b><button id="btnVolver" class="btn btn-default" data-dismiss="modal">Volver</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <input type="hidden" runat="server" id="hdnNumfacts" value="0" />
    <input type="hidden" runat="server" id="hdnIniciado" value="F" />
    <input type="hidden" runat="server" id="sCab1" value="" />
    <input type="hidden" runat="server" id="sCab2" value="" />
    <input type="hidden" runat="server" id="sFLS" value="" />
    <input type="hidden" runat="server" id="sPie1" value="" />
    <input type="hidden" runat="server" id="sPie2" value="" />
</body>

</html>
<script src="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/scripts/JavaScript.js" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/jquery-ui.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/moment.locale.min.js"></script>
<script src="js/jquery.uploadfile.min.js" type="text/javascript" ></script>
<script src="js/view.js" type="text/javascript"></script>
<script src="js/app.js" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/Buttons/dataTables.buttons.min.js"></script>
<!--<script src="http://hayageek.github.io/jQuery-Upload-File/4.0.10/jquery.uploadfile.min.js"></script>-->
<script src="<% =Session["strServer"].ToString() %>scripts/stringbuilder.js" type="text/javascript" ></script>
<script src="<% =Session["strServer"].ToString() %>scripts/exportaciones.js" type="text/javascript" ></script>


