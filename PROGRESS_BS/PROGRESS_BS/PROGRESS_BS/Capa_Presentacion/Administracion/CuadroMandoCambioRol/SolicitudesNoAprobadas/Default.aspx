<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_CuadroMandoCambioRol_SolicitudesNoAprobadas_Default" %>

<%@ Register  Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title>Solicitudes rechazadas</title>
    <link href="../../../../js/plugins/tablesorter/css/style.css?v=1" rel="stylesheet" type="text/css" />
</head>
<body>
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
    <br class="hidden-xs" />
    <br class="hidden-xs" />
    <br />

    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="col-xs-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title" style="display: inline-block; margin-right: 15px">Solicitudes rechazadas, a la espera del visto bueno de RRHH</h3>
                        (Nº de solicitudes:
                    <label id="numSolicitudes" style="font-weight: normal"></label>
                        )
                    </div>
                    <div class="panel-body">
                        <table id="tablaSolicitudesNoAprobadas" class="table header-fixed tablesorter">
                            <thead>
                                <tr>
                                    <th><span class="margenCabecerasOrdenables">Profesional</span></th>
                                    <th><span class="margenCabecerasOrdenables">Evaluador</span></th>
                                    <th><span class="margenCabecerasOrdenables">Rol actual</span></th>
                                    <th><span class="margenCabecerasOrdenables">Rol propuesto</span></th>
                                    <th><span>M. Solicitud</span></th>
                                    <th><span class="margenCabecerasOrdenables">Solicitud</span></th>
                                    <th><span class="margenCabecerasOrdenables">Aprobador</span></th>
                                    <th><span>M. Rechazo</span></th>
                                    <th><span class="margenCabecerasOrdenables">Rechazo</span></th>

                                </tr>
                            </thead>
                            <tbody runat="server" id="tblSolicitudesNoAprobadas">
                            </tbody>
                        </table>

                    </div>
                </div>

                <div class="pull-right">
                    <button id="btnAceptarNoAprobacion" type="button" class="btn btn-primary">Aceptar el rechazo</button>
                    <button id="btnStandby" type="button" class="btn btn-primary" style="margin-left: 7px;">Poner en Stand by</button>
                </div>
            </div>
        </div>
    </form>

    
    <div class="modal fade" id="modal-aceptarNoAprobacion">
	<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header bg-primary">
				<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
				<h4 class="modal-title">Aceptar el rechazo</h4>
			</div>
			<div class="modal-body">	
                
                <div class="well" id="divProfesionalesAceptar"></div>
                <span id="spanTexto">Pulsa el botón 'Aceptar' para hacer efectivos los rechazos para los profesionales indicados</span><hr />
			</div>
			<div class="modal-footer">
				<button id="btnAceptarFinal" type="button" class="btn btn-primary">Aceptar</button>
                <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>				
			</div>
		</div>
	</div>
</div>

     
    <div class="modal fade" id="modal-standby">
	<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header bg-primary">
				<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
				<h4 class="modal-title">Poner en Stand by</h4>
			</div>
			<div class="modal-body">	
                
                <div class="well" id="divProfesionalesStandby"></div>
                <span>Pulsa el botón 'Aceptar' para poner en Stand by las solicitudes de cambio de rol para los profesionales indicados</span><hr />
			</div>
			<div class="modal-footer">
				<button id="btnStandbyFinal" type="button" class="btn btn-primary">Aceptar</button>
                <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>				
			</div>
		</div>
	</div>
</div>



</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
<script type="text/javascript" src="../../../../js/plugins/tablesorter/jquery.tablesorter.min.js"></script> 
<script type="text/javascript" src="../../../../js/moment.locale.min.js"></script>
