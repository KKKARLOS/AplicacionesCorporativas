<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Mantenimientos_HistoricoRoles_Default" %>

<%@ Register  Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>

<html>
<head>
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title>Histórico de roles</title>
    <link href="../../../js/plugins/tablesorter/css/style.css" rel="stylesheet" type="text/css" />
    
    <script>
        var strServer = "<% =Session["strServer"].ToString() %>";
      
    </script>
  
</head>
<body data-codigopantalla="404">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
    
    <br class="hidden-xs" />
    <br class="hidden-xs" />

    <div class="container">

        <div class="row">
              <div class="col-xs-12">
                <fieldset class="fieldset">
                    <legend  style="height:28px" class="fieldset"><span style="margin-top:4px;display:inline-block">Profesional</span><button class="btn-xs btn-primary pull-right" id="btnRestablecer">Restablecer</button></legend>

                   <div id="txtBusqueda" class="row">
                        <div class="pull-left">
                             <label for="lblApellido1">Apellido 1º</label><br />
                                <input id="txtApellido1" type="text" />
                        </div>
                        <div class="pull-left" style="margin-left:10px">
                             <label for="lblApellido2">Apellido 2º</label><br />
                                <input id="txtApellido2" type="text" />
                        </div>
                        <div class="pull-left" style="margin-left:10px">
                            <label for="lblNombre">Nombre</label><br />
                            <input id="txtNombre" type="text" />
                        </div>
                        
                    </div>

                    <div class="clearfix"></div>

                </fieldset>
            </div>
        </div>

        <div class="row">
            
                <div class="col-xs-8">
                    <fieldset class="fieldset">
                        <legend class="fieldset"><span>Período</span></legend>
                        <div class="col-xs-6">
                            <select id="selMesIni" runat="server" onchange="ValHistorico()"></select>
                            <select id="selAnoIni" runat="server" onchange="ValHistorico()"></select>
                        </div>
                        <div class="col-xs-6">
                            <select id="selMesFin" runat="server" onchange="ValHistorico()"></select>
                            <select id="selAnoFin" runat="server" onchange="ValHistorico()"></select>
                        </div>
                    </fieldset>
                </div>




                <div class="clearfix"></div>

        </div>




        <div class="row">
            <div class="col-xs-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Histórico de roles</h3>                        
                    </div>
                    <div class="panel-body">
                        <table id="tblHistorico" class="table header-fixed tablesorter">
                            <thead>
                                <tr>                                    
                                    <th><span class="margenCabecerasOrdenables">Profesional</span></th>
                                    <th><span class="margenCabecerasOrdenables">Rol</span></th>                                                                        
                                    <th><span class="margenCabecerasOrdenables">Fecha</span></th>
                                </tr>
                            </thead>
                            <tbody runat="server" id="tbdHistorico">
                                
                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
        </div>

       <%-- <div class="row">
            <div class="col-xs-12">
                <span style="">Número de profesionales: <span id="spanTotal"></span></span>
            </div>
        </div>--%>
       
    </div>




  
</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
<script type="text/javascript" src="../../../js/plugins/tablesorter/jquery.tablesorter.min.js"></script> 
<script type="text/javascript" src="../../../js/moment.locale.min.js"></script>


 

 