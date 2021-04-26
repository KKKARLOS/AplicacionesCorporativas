<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Inicio2_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Head runat="server" ID="Head" />
     <title>Home Super</title>
    <link href="carrousel/flaticon.css" rel="stylesheet" />
     <style>
        .flaticon-carrusel:before {
            content: "\f100";
            font-size: 4.97em;
            margin-top: -15px;
            display: inline-block;
        }
        
    </style>
    <script>
        var sMensajeMMOFF = "<%=sMensajeMMOFF %>"; 
        var sUsuarioSeleccionado = "<%=sUS %>"; 
        var bMultiusuario = <% =((bool)Session["MULTIUSUARIO"])? "true":"false" %>;
        IB.vars["msgBienvenida"] = {
            mostrarMensajeBienvenida: <% =((bool)Session["MostrarMensajeBienvenida"])? "true": "false" %>,
            msgBienvenida: "<% =Session["MSGBIENVENIDA"] %>" ,
            imgFotoBase64: "<% =imgFotoBase64 %>",
            tiempoMensajeBienvenida: "<% =Session["TiempoMensajeBienvenida"].ToString() %>",
            bienvenidaMostrada : <% =((bool)Session["BIENVENIDAMOSTRADA"])? "true": "false" %>
            };
        
    </script>
  
</head>
<body>
    
    <uc1:Menu runat="server" ID="Menu" />
    
    <form id="form1" runat="server">

        
         <!-- DIV con el número de acciones pendientes-->
        <div id="divAccionesPendientes" class="col-xs-4 col-lg-2 pull-right"></div>


         <!-- FIN DIV con el número de acciones pendientes-->
        <br class="visible-xs" /><br class="visible-xs" /><br class="visible-xs" /><br class="visible-xs" /><br class="visible-xs" />
        <br class="visible-xs" /><br class="visible-xs" />

        <div class="container">                
            <div class="panel-group">
                <div class="row">
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome01" data-parent="PGE" class="celda col-xs-12 text-center">
                            <a href="#">
                                <span class="fa-stack fa-2_5x ">
                                    <i class="fa fa-diamond fa-stack-2x"></i>
                                    <strong id="faNuevoProyecto" class="fa-stack-1x fa fa-plus"></strong>
                                </span>

                            </a>
                            <br />
                            <a href="#"><span id="nuevoProyecto" class="txtclass">Nuevo proyecto</span></a>
                        </div>
                    </div>
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome02" data-parent="PGE" class="celda col-xs-12 text-center">
                            <a><span class="fa fa-diamond fa-5x"></span></a>
                            <br />
                            <a><span class="txtclass">Detalle de proyecto</span></a>
                        </div>
                    </div>
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome03" data-parent="PGE" class="celda col-xs-12 text-center">                            
                            <a href="#"><span class="flaticon-carrusel"></span> </a>                                                        
                            <br />
                            <a id="linkDetalleEconomico"><span class="txtclass">Detalle económico</span></a>
                        </div>
                    </div>
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome04" data-parent="PGE" class="celda col-xs-12 text-center">                                                        
                             <a href="#"><span class="fa-stack fa-2_5x">
                                <i class="fa fa-square-o fa-stack-2x"></i>
                                <strong id="faResumenEconomico1" class="fa-stack-1x fa fa-diamond"></strong>
                                <strong id="faResumenEconomico2" class="fa-stack-1x fa fa-diamond"></strong>
                                <strong id="faResumenEconomico3" class="fa-stack-1x fa fa-diamond"></strong>
                                <strong id="faResumenEconomico4" class="fa-stack-1x fa fa-diamond"></strong>
                            </span></a>

                            <br />
                            <a><span class="txtclass">Resumen económico</span></a>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome05" data-parent="PGE" class="celda col-xs-12">
                            <a href="#"><span class="fa-stack fa-2_5x">
                                <i class="fa fa-diamond fa-stack-2x"></i>
                                <strong id="faProyectosNoCerrados" class="fa-stack-1x fa fa-unlock"></strong>
                            </span></a>
                            <br />
                            <a><span class="txtclass">Proyectos no cerrados</span></a>
                        </div>
                    </div>
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome06" data-parent="PGE" class="celda col-xs-12">
                            <a><span class="fa fa-line-chart fa-5x"></span></a>
                            <br />
                            <a><span class="txtclass">Avance económico</span></a>
                        </div>
                    </div>
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome07" data-parent="PGE" class="celda col-xs-12">
                            <a><span class="fa fa-trophy fa-5x"></span></a>
                            <br />
                            <a><span class="txtclass">Valor ganado</span></a>
                        </div>
                    </div>

                    
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome08" data-parent="PGE" class="celda col-xs-12">                            
                             <a href="#"><span class="fa-stack fa-2_5x">
                                <i class="fa fa-clone fa-stack-2x"></i>
                                <strong id="faReplicaGlobal" class="fa-stack-1x fa fa-diamond"></strong>
                            </span></a>
                            <br />
                            <a><span class="txtclass">Réplica global</span></a>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome09" data-parent="PGE" class="celda col-xs-12">
                            <a><span class="fa fa-share-square fa-5x"></span></a>
                            <br />
                            <a><span class="txtclass">Traspaso de IAP</span></a>
                        </div>
                    </div>
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome10" data-parent="PGE" class="celda col-xs-12">
                             <a href="#"><span class="fa-stack fa-2_5x">
                                <i class="fa fa-globe fa-stack-2x"></i>
                                <strong id="faCierreMensualGlobal" class="fa-stack-1x fa fa-link"></strong>
                            </span></a>
                            <br />
                            <a><span class="txtclass">Cierre mensual global</span></a>
                        </div>

                    </div>
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome11" data-parent="PGE" class="celda col-xs-12">
                            <a><span class="fa fa-list fa-5x"></span></a>
                            <br />
                            <a><span class="txtclass">Ordenes de facturación</span></a>
                        </div>
                    </div>
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome12" data-parent="PGE" class="celda col-xs-12">
                            <a><span class="fa fa-list-alt fa-5x"></span></a>
                            <br />
                            <a><span class="txtclass">Control de facturación</span></a>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome13" data-parent="PGE" class="celda col-xs-12">
                            <a><span class="fa fa-comments-o fa-5x"></span></a>
                            <br />
                            <a><span class="txtclass">Espacio de comunicación</span></a>
                        </div>
                    </div>
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome14" data-parent="PGE" class="celda col-xs-12">
                            
                             <a href="#"><span class="fa-stack fa-3x" style="font-size:2.5em">
                                <i class="fa fa-calendar-o fa-stack-2x"></i>
                                <strong><span id="faAgendaUsa">USA</span></strong>
                            </span></a>

                            <br />
                            <a><span class="txtclass">Agenda</span></a>
                        </div>
                    </div>
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome15" data-parent="PGE" class="celda col-xs-12">
                         <a href="#">
                        <span class="fa-stack fa-2_5x">
                            <i class="fa fa-diamond fa-stack-2x"></i>
                            <strong><span id="faExternalizacionUSA">USA</span></strong>
                            </span></a>

                            <br />
                            <a><span class="txtclass">Externalización</span></a>
                        </div>
                    </div>
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome16" data-parent="SIC"class="celda col-xs-12">
                            <a><span class="fa fa-object-group fa-5x"></span></a>
                            <br />
                            <a><span class="txtclass">Cuadro de mando</span></a>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome17" data-parent="PST" class="celda col-xs-12">
                            <a><span class="fa fa-sitemap fa-5x"></span></a>
                            <br />
                            <a><span class="txtclass">Estructura técnica</span></a>
                        </div>
                    </div>
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome18" data-parent="PST" class="celda col-xs-12">
                            <a><span class="fa fa-tasks fa-5x"></span></a><br />
                            <a><span class="txtclass">Avance técnico</span></a>
                        </div>
                    </div>
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome19" data-parent="IAP" class="celda col-xs-12">
                            <a><span class="fa fa-calendar fa-5x"></span></a>
                            <br />
                            <a><span class="txtclass">Reporte IAP diario</span></a>
                        </div>
                    </div>
                    <div class="col-xs-6 col-md-3">
                        <div id="btnAccesoHome20" data-parent="IAP" class="celda col-xs-12">
                            <a><span class="fa fa-calendar-check-o fa-5x"></span></a>
                            <br />
                            <a><span class="txtclass">Reporte IAP masivo</span></a>
                        </div>
                    </div>
                </div>

                <!-- Foto de usuario -->
                <div id="divFoto" style="display: none;"> </div>
               
                <!-- Novedades -->
                <div id="divNovedades" style="display: none" class="col-xs-12 col-sm-6 col-md-4 col-lg-3 pull-right"></div>

                <!-- Avisos -->
                <div id="divAvisos" class="col-xs-12 col-sm-6">
                    <div class="modal fade" id="modal-avisos" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header btn-primary">                                    
                                    <h4 class="modal-title"></h4>
                                </div>
                                <div class="modal-body">                                    
                                    <h5 class="pull-left">Comunicado de D.E.F.&nbsp;&nbsp;(<label id="lblNumAviso"></label>&nbsp;de&nbsp;<label id="lblTotalAvisos"></label>)</h5>
                                    <div class="pull-right">
                                        <span id="spanPGE" class="fa-stack fa-2x disabled">
                                            <i class="fa fa-sticky-note-o fa-stack-2x"></i>
                                            <strong id="strongPGE" class="fa-stack-1x disabled">PGE</strong>
                                        </span>
                                        <span id="spanPST" class="fa-stack fa-2x disabled">
                                            <i class="fa fa-sticky-note-o fa-stack-2x"></i>
                                            <strong id="strongPST" class="fa-stack-1x disabled">PST</strong>
                                        </span>
                                        <span id="spanIAP" class="fa-stack fa-2x disabled">
                                            <i class="fa fa-sticky-note-o fa-stack-2x"></i>
                                            <strong id="strongIAP" class="fa-stack-1x disabled">IAP</strong>
                                        </span>
                                    </div>

                                    <!--limpiamos float-->
                                    <div class="clearfix"></div>
                                    <hr />
                                    <div>
                                       <textarea disabled="disabled"  id="txtContenidoAviso"></textarea>
                                    </div>
                                    
                                </div>

                                <div class="modal-footer">                                    
                                    <button id="btnConservar" type="button" class="btn btn-primary">Conservar</button>
                                    <button id="btnDestruir" type="button" class="btn btn-default">Destruir</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </div>    

         
    </form>

    <div id="divayudapersonas"></div>
    

    <script src="js/HomeView.js"></script>
    <script src="js/FichaUsuarioView.js"></script>
    <script src="js/AccionesPendientesView.js"></script>
    <script src="js/getUsuariosView.js"></script>
    <script src="js/app.js?v=20180208"></script>
   
</body>
</html>



