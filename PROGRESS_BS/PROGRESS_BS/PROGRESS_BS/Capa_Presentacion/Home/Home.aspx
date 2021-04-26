<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Capa_Presentacion_Home_Home" %>


<%@ Register Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title>HOME PROGRESS</title>
    <style>
        .container {
            width: 970px !important;            
        }

        #tabla1 {
            margin-bottom:0;
        }
        #tabla1 tr, #tabla2 tr, #tabla3 tr {
            background-color:#FFF;
        }
        #tabla1 tr td, #tabla2 tr td, #tabla3 tr td {
            text-align:center;
        }

        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            border-top: none !important;
        }

        .table tbody > tr > td {
            vertical-align: middle !important;
        }

       
        #tabla3 img {
            width:20%;
        }

        .inline-block {
            display: inline-block;
        }

        .textoTemporada {
            font-size: 30px;
            color: #6699cc;            
        }


        #tablaProgress tr td {
            padding-left:0;
        }
        #divPostit img {
            width:70%;
            margin-left:15px;
        }

        
        #imgEvaluaciones {
            width: 50%;
        }

        #imgProcedimiento {
            width: 28%;
        }

       

        #imgDecalogo {
            width: 35%;
        }

        #imgGuia {
            width: 40%;
        }

        #imgValores {
            width: 47%;
        }

        #imgHabilidades {
            width: 50%;
        }


        #imgCompetencias {
            width: 50%;
        }

        #imgEjemplos {
            width: 46%;
        }

        #imgOficina {
            width: 42%;
        }


        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            padding-top: 2px !important;
            padding-bottom:2px !important;
        }

        .anyoProgress {
            margin-left:5px;
        }


      

        a[data-toggle=modal] {
            color: #FFF;
        }

        a[data-toggle=modal]:hover, a[data-toggle=modal]:active, a[data-toggle=modal]:visited, a[data-toggle=modal]:link {
            color: #FFF;
            text-decoration: none;
        }

        a[data-toggle=modal]:focus {
            outline: none;
        }

        .textoPeriodo {
            font-size:13px;
            color:#6699cc;
            display:block;
            margin-top:-10px;
        }

        #btnNoCancelacion, #btnNoEnvio {
            margin-left:7px;
        }
       

        /*Para resoluciones mayores*/
        @media (min-width: 1200px) {
            .container {
                width: 1170px !important;
            }
        }
    </style>

     <script type="text/javascript">
         var strServer = "<%=Session["strServer"]%>";    
         
    </script>
</head>
<body>
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>

    <br class="hidden-xs" />
    <br class="hidden-xs" />

    <form id="form1" runat="server">
        <div class="container">

            
            <div class="row">
                <div class="col-xs-12">
                    <table id="tablaProgress" class="table">
                        <tr>
                            <td>
                                <div class="pull-left">
                                    <h3 class="inline-block textoTemporada">Progress</h3>
                                    <h3 id="anyoactual" runat="server" class="anyoProgress inline-block textoTemporada"></h3>
                                    <span id="txtPeriodo" runat="server" class="textoPeriodo"></span>
                                </div>

                                <div id="divPostit" class="pull-left">
                                    <a href="#" target="_blank"> <img id="imgPostit" src="../../imagenes/imgPostit.png" /></a>
                                </div>
                                
                                 
                               
                                 
                            </td>
                           
                           
                        </tr>
                       
                    </table>
                </div>
            </div>
            
            
            <div class="row">
            <div class="col-xs-12">
            <table id="tabla1" class="table">
                <tr>
                    <td ><center><a href="../../Docs/EVALUACIONES/Evaluacion_del_desempeno.pdf" target="_blank">
                        <img id="imgEvaluaciones" src="../../imagenes/imgEvaluacionesTile.png" class="img-responsive" /></a></center>
                    </td>
                    <td>
                        <center><a data-toggle="modal" href="#modal-procedimientos">
                            <img id="imgProcedimiento" src="../../imagenes/imgProcedimientoTile.png" class="img-responsive" /></a></center>

                    </td>
                    <td>
                        <center><a data-toggle="modal" href="#modal-decalogo">
                            <img id="imgDecalogo" src="../../imagenes/imgDecalogoTile.png" class="img-responsive" /></a></center>
                    </td>
                    <td>
                        <center><a href="<% =Session["strServer"].ToString() %>Docs/ENTREVISTA/edGuiaEntrevista 16.pdf" target="_blank">
                            <img id="imgGuia" src="../../imagenes/imgGuiaTile.png" class="img-responsive" /></a></center>
                    </td>
                </tr>

                <tr>
                    <td><span>Evaluaciones</span></td>
                    <td><span>Procedimiento</span></td>
                    <td> <span>Decálogo +</span> <br />                    
                    <span>modelo en blanco</span></td>
                    <td><span>Guía entrevista</span></td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>

                 <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>


            </table>
            </div>
            </div>


            <div class="row">
            <div class="col-xs-12">
            <table id="tabla2" class="table">
                <tr>
                    <td> <center><a href="<% =Session["strServer"].ToString() %>Docs/VALORES_CORPORATIVOS/Valores.pdf" target="_blank"><img id="imgValores" src="../../imagenes/imgValoresTile.png" class="img-responsive" /></a></center>

                    </td>
                    <td>
                        <center><a href="<% =Session["strServer"].ToString() %>Docs/HABILIDADES/Habilidades_Progress_2016.pdf" target="_blank"> <img id="imgHabilidades" src="../../imagenes/imgHabilidadesTile.png" class="img-responsive" /></a></center>
                    </td>
                    <td>
                        <center><a id="linkCompetencias" href="#" target="_blank"><img id="imgCompetencias" src="../../imagenes/imgCompetenciasTile.png" class="img-responsive"/></a></center>
                    </td>
                    <td>
                        <center><a data-toggle="modal" href="#modal-ejemplos"> <img id="imgEjemplos" src="../../imagenes/imgEjemplosTile.png" class="img-responsive" /></a></center>
                    </td>
                    <td>
                        <center><a data-toggle="modal" href="#modal-correo"><img id="imgOficina"  src="../../imagenes/imgOficinaTile.png" class="img-responsive" /></a></center>
                    </td>
                </tr>

                <tr>
                    <td><span>Valores</span><br /><span>corporativos</span></td>
                    <td><span>Habilidades</span></td>
                    <td><span>Competencias</span><br /><span>y roles</span></td>
                    <td><span>Ejemplos</span></td>
                    <td><span>Oficina</span><br /><span>técnica</span></td>
                </tr>
            </table>

            </div>
            </div>



            <div id="fila3" class="row hide">
                <div class="col-xs-12">
                    <table id="tabla3" class="table">
                        <tr>
                            <td>
                                <center><a href="../../Docs/EQUIPO/Equipo.pdf" target="_blank">
                                <img id="imgEquipo" src="../../imagenes/imgEquipoTile.png" class="img-responsive" /></a></center>
                            </td>
                            <td>
                                <center><a href="../../Docs/ROLES/Roles.pdf" target="_blank">
                                <img id="imgRoles" src="../../imagenes/imgRolesTile.png" class="img-responsive" /></a></center>
                            </td>
                        </tr>
                        <tr>
                            <td>Equipo</td>
                            <td>Roles</td>
                        </tr>
                    </table>
                </div>
            </div>



   <!--Modal Correo a oficina técnica -->
  <div class="modal fade" id="modal-correo">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Correo a Oficina Técnica</h4>
                </div>
                <div class="modal-body">                    
                    <div class="row">
                        <div class="col-xs-8">
                            <span>Texto</span><br />
                            <textarea autofocus="autofocus" id="txtComentario" maxlength="750" style="width: 570px; height: 200px"></textarea>
                            <br />                            
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <b><button id="btnEnviar" class="btn btn-primary" data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#modal-finalizar">Enviar</button></b>
                    <button id="btnCancelar" style="margin-left:7px" type="button" class="btn btn-default">Cancelar</button>                                            
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
<!-- FIN Modal Correo a oficina técnica-->


        <div class="modal fade" id="modal-confirmacion-cancelar">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Confirmación de cancelación</h4>
                </div>
                <div class="modal-body">                    
                    <div class="row">
                        <div class="col-xs-8">
                            <span>Has pulsado el botón 'Cancelar'. Pulsa 'Sí' si efectivamente quieres cancelar. Pulsa 'No' para volver a la pantalla de anterior.</span>
                            
                            <br />                            
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <b><button id="btnOkCancelacion" class="btn btn-primary" data-toggle="modal" data-backdrop="static" data-keyboard="false">Sí</button></b>                    
                    <b><button id="btnNoCancelacion" class="btn btn-default" data-toggle="modal" data-backdrop="static" data-keyboard="false">No</button></b>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>



         <div class="modal fade" id="modal-confirmacion-envio">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Confirmación de envío</h4>
                </div>
                <div class="modal-body">                    
                    <div class="row">
                        <div class="col-xs-8">
                            <span>Has pulsado el botón 'Enviar'. Pulsa 'Sí' si efectivamente quieres hacer el envío. Pulsa 'No' para volver a la pantalla de anterior.</span>                            
                            <br />                            
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <b><button id="btnOkEnvio" class="btn btn-primary" data-toggle="modal" data-backdrop="static" data-keyboard="false">Sí</button></b>                    
                    <b><button id="btnNoEnvio" class="btn btn-default" data-toggle="modal" data-backdrop="static" data-keyboard="false">No</button></b>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


        <!-- Capa Decálogo-->
        <div class="modal fade" id="modal-decalogo">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header btn-default">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Decálogo + modelo en blanco</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-xs-8">
                                <ul>
                                    <li><a href="<% =Session["strServer"].ToString() %>Docs/DECALOGOYMODELOENBLANCO/Decalogo_Formulario_2016.pdf" target="_blank">PROGRESS</a></li>
                                    <li><a href="<% =Session["strServer"].ToString() %>Docs/DECALOGOYMODELOENBLANCO/Decalogo_Formulario_2016_Atencion_Clientes.pdf" target="_blank">ATENCIÓN A CLIENTES</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Capa Procedimientos-->
        <div class="modal fade" id="modal-procedimientos">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header btn-default">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Procedimientos</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-xs-8">
                                <ul>
                                    <li><a href="<% =Session["strServer"].ToString() %>Docs/PROCEDIMIENTO/Evaluacion_del_desempeno.pdf" target="_blank">Evaluación del desempeño</a></li>
                                    <li><a href="<% =Session["strServer"].ToString() %>Docs/PROCEDIMIENTO/Esquema_evaluacion_del_desempeno.pdf" target="_blank">Resumen</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        


        <!-- Capa Ejemplos-->
        <div class="modal fade" id="modal-ejemplos">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header btn-default">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Ejemplos</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-xs-8">
                                <ul>                                    
                                    <li><a href="<% =Session["strServer"].ToString() %>Docs/EJEMPLOS/Tecnico_soporte_negocio.pdf" target="_blank">Técnico de soporte al negocio</a></li>
                                    <li><a href="<% =Session["strServer"].ToString() %>Docs/EJEMPLOS/Tecnico_desarrollo.pdf" target="_blank">Técnico de desarrollo</a></li>
                                    <li><a href="<% =Session["strServer"].ToString() %>Docs/EJEMPLOS/Consultor_procesos.pdf" target="_blank">Consultor de procesos</a></li>
                                    <li><a href="<% =Session["strServer"].ToString() %>Docs/EJEMPLOS/Comercial.pdf" target="_blank">Comercial</a></li>                                    
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>



    </form>

</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
<script type="text/javascript" src="../../js/ios-orientationchange-fix.js"></script> 


<script>


    $(document).ready(function () {

        if (strServer == "/PROGRESS20/") {
            $("#divPostit a").attr("href", "https://extranet.ibermatica.com/profesionales/EvaluacionDesempeno/Progress2016Comunicados.htm");
            $("#linkCompetencias").attr("href", "https://extranet.ibermatica.com/profesionales/DesarrolloProfesional/NivelesCompetenciaRoles.htm");
        }

        else {
            $("#divPostit a").attr("href", "http://www.intranet.ibermatica/profesionales/EvaluacionDesempeno/Progress2016Comunicados.htm");
            $("#linkCompetencias").attr("href", "http://www.intranet.ibermatica/profesionales/DesarrolloProfesional/NivelesCompetenciaRoles.htm");

        }

        //Sólo se muestra esta capa  a los evaluadores
        if ($("#CabeceraMenu_ulPrincipal li[data-name=Equipo]").length > 0) {
            $("#fila3").removeClass("hide").addClass("show");
            $("#fila1").css("margin-top", "0");
        }
        else {
            $("#fila1").css("margin-top", "5%");
        }
            
    })


    //Modal Oficina técnica
    $("#btnEnviar").on("click", function (e) {

        if ($("#txtComentario").val().length == 0) {
            alertNew("warning", "No has escrito ningún texto.", null, 2000, null);
            e.preventDefault();
            e.stopPropagation();            
        }
        else {
            $("#modal-confirmacion-envio").modal("show");
            e.preventDefault();
            e.stopPropagation();

        }

        //$("#btnEnviar").prop("disabled", true);

    })





    $("#btnCancelar").on("click", function (e) {
        e.preventDefault();
        e.stopPropagation();
        //$("#txtComentario").val("");        

        if ($("#txtComentario").val().length > 0)
            $("#modal-confirmacion-cancelar").modal("show");

        else {
            
            $("#modal-correo").modal("hide");            
        }
        
    })


    $("#btnOkEnvio").on("click", function (e) {

        $("#btnOkEnvio").prop("disabled", true);

        $.ajax({
            url: "Home.aspx/enviarCorreo",
            data: JSON.stringify({ texto: codpar($("#txtComentario").val()) }),  // parameter map as JSON
            type: "POST",
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            timeout: 20000,
            success: function (result) {
                $("#txtComentario").val("");
                $("#modal-confirmacion-envio").modal("hide");
                $("#modal-correo").modal("hide");

                alertNew("success", "Mensaje enviado.", null, 2000, null);

                
                //$("#btnEnviar").prop("disabled", false);
            },
            error: function (ex, status) {
                mostrarErrorAplicacion("Ocurrió un error al enviar el correo.", e.message)
            }
        });


    })

    $("#btnNoEnvio").on("click", function (e) {
        e.preventDefault();
        e.stopPropagation();
        $("#modal-confirmacion-envio").modal("hide");        
        
    })
    $("#btnOkCancelacion").on("click", function (e) {
        
        $("#modal-confirmacion-cancelar").modal("hide");
        $("#modal-correo").modal("hide");        
        $("#txtComentario").val("");
        
    })

    $("#btnNoCancelacion").on("click", function (e) {        
        e.preventDefault();
        e.stopPropagation();
        $("#modal-confirmacion-cancelar").modal("hide");
        
    })
  
    //Ponemos foco al campo comentario de la modal
    $(window).on('shown.bs.modal', function () {
        $("#txtComentario").focus();
    });
</script>


