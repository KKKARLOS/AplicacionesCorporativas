<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_MantMotivoOCyFA_Default" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="cb1" TagName="Menu" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="cb1" TagName="Head" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:Head runat="server" id="Head" />    
    <!--<link href="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/Reporte/Bitacora/css/StyleSheet.css" rel="stylesheet"/>-->
    <title>::: SUPER ::: Mantenimiento motivos OC y FA</title>  
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>css/jquery-ui.min.css"/>
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.css"/>
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>plugins/datatables/Buttons/buttons.dataTables.min.css"/>
</head>
<body>
    <script>
        var opciones = { delay: 1 }
        IB.procesando.opciones(opciones);
        IB.procesando.mostrar();
    </script>
    <cb1:Menu runat="server" id="Menu" />
    <div class="container-fluid">
        <h1 class="hiddenStructure">::: SUPER ::: Mantenimiento motivos OC y FA</h1>  
        <br class="visible-xs" />
        <div class="ibox-content blockquote blockquote-info" >
            <div class="row">
                <div class="ibox-title">
		            <div class="panel-group">
                        <form id="frmDatos" class="form-horizontal" runat="server">                    
                            <div class="form-group row">		
                                <div class="col-xs-12 col-md-12">
					                <fieldset class="fieldset">
                                        <h2 class="hiddenStructure">información sobre motivos de OC y FA</h2>           
						                <legend class="fieldset"><span class="text-nowrap">Motivos de OC y FA</span></legend>
                                            <div class="row">
                                                <div class="col-xs-12">	
                                                    <div class="table-responsive">
                                                    <table id="tabla" class="table table-bordered table-condensed" title="Motivos de OC y FA">
                                                        <thead>    
                                                            <tr>
                                                                <th data-type="String" class="bg-primary ">Tipo</th>
                                                                <th data-type="String" class="bg-primary ">Denominación</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="bodyDatos"></tbody>
                                                    </table>
                                                    </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <button type="button" id="btnAdd" class="btn btn-primary bottom">Añadir</button>
                                                <button type="button" id="btnMod" class="btn btn-primary bottom">Modificar</button>
                                                <button type="button" id="btnDel" class="btn btn-primary bottom ">Eliminar</button>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
			                </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>       

    <!-- Modal de detalle -->
    <div class="modal fade" id="modalDetalle" role="dialog" tabindex="-1" title=":::SUPER:::">
        <div class="modal-dialog modal-md" role="dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                    <h1 class="modal-title" id="tituloModalComentario">:::SUPER:::</h1>
                </div>
                <div class="modal-body">
                    <div class="row row-modal">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <label class="col-sm-2 control-label" for="txtDen">Denominación</label>
                                <div class="col-sm-8">
                                    <input id="txtDen" name="" type="text" class="form-control input-md" value="" maxlength="70" required />
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12">
                            <div class="form-group">
                                <label class="col-sm-2 control-label" for="cboTipo">Tipo</label>
                                <div class="col-sm-8">
                                    <select id="cboTipo" name="tipo" class="form-control">
                                        <option selected="selected" value="O">Obra en curso</option>
                                        <option value="F">Facturación anticipada</option>
                                    </select>  	
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b><button id="btnAceptar" class="btn btn-primary" data-dismiss="modal">Grabar</button></b>                    
                    <b><button id="btnCancelar" class="btn btn-default" data-dismiss="modal">Cancelar</button></b>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div> 

</body>
</html>
<script src="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/scripts/JavaScript.js" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/jquery-ui.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/Buttons/dataTables.buttons.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/moment.locale.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/accounting.min.js" type="text/javascript" ></script>
<script src="<% =Session["strServer"].ToString() %>scripts/stringbuilder.js" type="text/javascript" ></script>
<script src="<% =Session["strServer"].ToString() %>scripts/exportaciones.js" type="text/javascript" ></script>
<script src="js/view.js?20170518_03" type="text/javascript"></script>
<script src="js/app.js?20170518_03" type="text/javascript"></script>
