<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Evaluacion_MisEvaluaciones2_Default" %>

<%@ Register Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagName="HeaderMeta" TagPrefix="cb1" %>
<%@ Register Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:HeaderMeta runat="server" ID="HeaderMeta"></cb1:HeaderMeta>    
    <title></title>      
    
    <style>
        body > .container {
            /*padding: 30px 15px 0;*/
        }
       .container {
    width: 970px !important;
    
}
   
/*LISTA DUAL*/
.dual-list .list-group {
    margin-top: 50px;
    height: 40vh;
    overflow: auto;
}

.list-left li, .list-right li {
    cursor: pointer;
}

li.list-group-item {
    display: table;
    width:100%;
}

        .list-group-item.active, .list-group-item.active:hover, .list-group-item.active:focus {
            background-color: #D8D8D8 !important;
            color: #000;
            border-color: #FFF;
        }
    </style>
    
   
</head>

<body>
    <%--<uc3:mensajeSession ID="MensajeSession" runat="server" />--%>
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
    
       <br />
    <br />
   

   <div class="container" id="capaPaso1">
        <div id="divMiEquipo">
            <div class="row">
                <div class="text-left dual-list list-left col-xs-5">
                    <h4>Mi equipo</h4>
                    <div class="well padding10">
                        <div class="row">
                            <div class="btn-group col-xs-2">
                                <a class="btn btn-default selector" data-placement="top" data-toggle="popover" title="" data-content="Marcar/desmarcar todo">
                                    <i class="glyphicon glyphicon-unchecked"></i>
                                </a>
                            </div>
                            <div class="col-xs-10 input-group">
                                <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                            </div>
                        </div>
                        <ul class="list-group" runat="server" id="lisMiEquipo">
                              <li class="list-group-item " value="6893"><span>AGIRRE ETXEBERRIA, Gurutze</span></li>
                            <li class="list-group-item " value="6893"><span>AGIRRE ETXEBERRIA, Gurutze</span></li>
                            <li class="list-group-item " value="6893"><span>AGIRRE ETXEBERRIA, Gurutze</span></li>
                            <li class="list-group-item " value="6893"><span>AGIRRE ETXEBERRIA, Gurutze</span></li>
                            <li class="list-group-item " value="6893"><span>AGIRRE ETXEBERRIA, Gurutze</span></li>
                            
                        </ul>
                    </div>



                    <div class="row">
                        <div class="text-left col-xs-12" id="divLeyendas2">
                            <i class="fa fa-file-text-o verde"></i>
                            <span>Evaluación abierta</span>
                            <i style="margin-left: 5px" class="fa fa-file-text-o azul"></i>
                            <span>Evaluación en curso</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="text-left col-xs-12" id="divLeyendas">
                            <span class="glyphicon glyphicon-new-window"></span>
                            <span>Salida tramitada</span>
                            <i style="margin-left: 15px" class="fa fa-compress"></i>
                            <span>Salida rechazada</span>
                        </div>
                    </div>



                </div>



                <div class="list-arrows col-xs-2 text-center">
                    <div class="row">
                        <button class="btn btn-default btn-sm move-right">
                            <span class="glyphicon glyphicon-chevron-right"></span>
                        </button>
                    </div>
                    <div class="row">
                        <button class="btn btn-default btn-sm move-left">
                            <span class="glyphicon glyphicon-chevron-left"></span>
                        </button>
                    </div>
                </div>



                <div class="dual-list list-right col-xs-5">
                    <h4>Profesionales a evaluar</h4>
                    <div class="well padding10">
                        <div class="row">
                            <div class="btn-group col-xs-2">
                                <a class="btn btn-default selector" data-toggle="popover" data-placement="top" title="" data-content="Marcar/desmarcar todo">
                                    <i class="glyphicon glyphicon-unchecked"></i>
                                </a>
                            </div>
                            <div class="col-xs-10 input-group">
                                <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                            </div>
                        </div>
                        <ul class="list-group" runat="server" id="lisAEvaluar">
                            <li>David</li>
                            <li>David</li>
                            <li>David</li>
                            <li>David</li>
                            <li>David</li>
                            <li>David</li>
                        </ul>
                    </div>

                </div>

                

            </div>









        </div>
    </div>

        
    

</body>


</html>

<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>


<script>
    $(document).ready(function () {
        comprobarerrores();
        try {
            //Inicializamos tooltip
            $("[data-toggle=tooltip]").tooltip();


            //Click en el botón siguiente
            $('#pieFormulario button i.glyphicon-chevron-right').parent().on('click', function () { siguiente(); });
            //Click en el botón anterior
            $('#pieFormulario button i.glyphicon-chevron-left').parent().on('click', function () { anterior(); });

            //Selección
            $('body').on('click', '#divMiEquipo .list-group .list-group-item', function (e) {
                lista = $(this).parent().children();
                if (e.shiftKey && lista.filter('.active').length > 0) {
                    first = lista.filter('.active:first').index();//Primer seleccionado
                    last = lista.filter('.active:last').index();//Último seleccionado
                    $('#divMiEquipo .list-group .list-group-item').removeClass('active');//Borrar de las dos listas
                    if ($(this).index() > first)
                        lista.slice(first, $(this).index() + 1).addClass('active');
                    else
                        lista.slice($(this).index(), last + 1).addClass('active');
                }
                else if (e.ctrlKey) {
                    $(this).toggleClass('active');
                } else {
                    //$('#divMiEquipo .list-group .list-group-item').removeClass('active');
                    //$(this).addClass('active');
                    $(this).toggleClass('active');
                }
            });

            //Click en los botones
            $('.list-arrows button').on('click', function () {
                var $button = $(this), actives = '';
                if ($button.hasClass('move-left')) {
                    actives = $('.list-right ul li.active');
                    if (actives.length > 0) {
                        actives.clone().appendTo('.list-left ul');
                        actives.each(function () { aEvaluar($(this).attr('value'), false); });
                        ordenar($('.list-left ul li'));
                    } else {
                        alertNew("warning", "Tienes que seleccionar algún profesional");
                    }
                } else if ($button.hasClass('move-right')) {
                    actives = $('.list-left ul li.active:not(.pend)');
                    lisActNoPass = $('.list-left ul li.active.pend');

                    if ((actives.length + lisActNoPass.length) > 0) {
                        actives.clone().appendTo('.list-right ul');
                        actives.each(function () { aEvaluar($(this).attr('value'), true); });
                        ordenar($('.list-right ul li'));
                        if (lisActNoPass.length > 0) {
                            alertNew("warning", "No puedes abrir una nueva evaluación a los profesionales que ya tienen otra evaluación abierta, en curso o están en trámite de salida del equipo.", null, 6000, null);
                        }
                    } else {
                        alertNew("warning", "Tienes que seleccionar algún profesional");
                    }
                }
                actives.remove();
                $('.list-group-item').removeClass('active');
                $('.dual-list .selector').children('i').addClass('glyphicon-unchecked');

                $('[data-toggle="popover"]').popover({
                    trigger: 'hover',
                    html: true,

                });
                //grabar();
            });

            //Seleccionar todos o ninguno
            $('.dual-list .selector').on('click', function () {
                var $checkBox = $(this);
                if (!$checkBox.hasClass('selected')) {
                    $checkBox.addClass('selected').closest('.well').find("li:not(.pend)").addClass("active");

                    //$checkBox.addClass('selected');
                    //var $items = $("#lisMiEquipo li:not(.pend)");

                    //$($items).not(".active").addClass("active");
                    $checkBox.children('i').removeClass('glyphicon-unchecked').addClass('glyphicon-check');
                    //$checkBox.tooltip('toggle').attr('data-original-title', "Desmarcar todos los profesionales").tooltip('fixTitle').tooltip('toggle');
                } else {


                    $checkBox.removeClass('selected');
                    $("#lisMiEquipo li.active").removeClass("active");

                    //$checkBox.removeClass('selected').closest('.well').find('ul li.active').removeClass('active');
                    $checkBox.children('i').removeClass('glyphicon-check').addClass('glyphicon-unchecked');
                    //$checkBox.tooltip('toggle').attr('data-original-title', "Marcar todos los profesionales").tooltip('fixTitle').tooltip('toggle');
                }
            });

            //Buscar
            $('#capaPaso1 [name="SearchDualList"]').on('keyup', function (e) {
                var code = e.keyCode || e.which;
                if (code == '9') return;
                if (code == '27') $(this).val(null);
                var $rows = $(this).closest('.dual-list').find('.list-group li');
                var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
                $rows.show().filter(function () {
                    var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                    return !~text.indexOf(val);
                }).hide();
            });

            //Seleccionar todos o ninguno
            $('th i').on('click', function () {
                var $checkBox = $(this);
                if (!$checkBox.hasClass('selected')) {
                    $('#tbdProfesionales i.glyphicon').removeClass('glyphicon-unchecked').addClass('glyphicon-check selected');
                    $('#tbdProfesionales textarea').prop("disabled", false);
                    $checkBox.removeClass('glyphicon-unchecked').addClass('glyphicon-check selected');
                    //$checkBox.tooltip('toggle').attr('data-original-title', "Marcar todos los profesionales, para enviarles un correo con el comunicado de apertura de evaluación").tooltip('fixTitle').tooltip('toggle');
                } else {
                    $('#tbdProfesionales i.glyphicon').removeClass('glyphicon-check selected').addClass('glyphicon-unchecked');
                    $('#tbdProfesionales textarea').prop("disabled", true);
                    $checkBox.removeClass('glyphicon-check selected').addClass('glyphicon-unchecked');
                    //$checkBox.tooltip('toggle').attr('data-original-title', "Desmarcar todos los profesionales, para no enviarles un correo con el comunicado de apertura de evaluación").tooltip('fixTitle').tooltip('toggle');
                }
            });

            //Añado el evento al body pq son elementos q se generan dinámicamente
            $('body').on('click', '#tbdProfesionales i.glyphicon', function () {
                var $checkBox = $(this);
                if (!$checkBox.hasClass('selected')) {
                    $checkBox.removeClass('glyphicon-unchecked').addClass('glyphicon-check selected');
                    $checkBox.closest('tr').find('textarea').prop("disabled", false);
                } else {
                    $checkBox.removeClass('glyphicon-check selected').addClass('glyphicon-unchecked');
                    $checkBox.closest('tr').find('textarea').prop("disabled", true);
                }
            });

            //Seleccionar todos o ninguno
            $('#modal-imprimir .selector i').on('click', function () {
                var $checkBox = $(this);
                if (!$checkBox.hasClass('selected')) {
                    $checkBox.removeClass('glyphicon-unchecked').addClass('glyphicon-check selected');
                    $('#lisImpEvaluados i').removeClass('glyphicon-unchecked').addClass('glyphicon-check selected');
                    //$checkBox.parent().tooltip('toggle').attr('data-original-title', "Desmarcar todos").tooltip('fixTitle').tooltip('toggle');
                } else {
                    $checkBox.removeClass('glyphicon-check selected').addClass('glyphicon-unchecked');
                    $('#lisImpEvaluados i').removeClass('glyphicon-check selected').addClass('glyphicon-unchecked');
                    //$checkBox.parent().tooltip('toggle').attr('data-original-title', "Marcar todos").tooltip('fixTitle').tooltip('toggle');
                }
            });

            //Selección individual
            $('body').on('click', '#lisImpEvaluados i', function () {
                var $checkBox = $(this);
                if (!$checkBox.hasClass('selected')) {
                    $checkBox.removeClass('glyphicon-unchecked').addClass('glyphicon-check selected');
                } else {
                    $checkBox.removeClass('glyphicon-check selected').addClass('glyphicon-unchecked');
                }
            });



            $('[data-toggle="popover"]').popover({
                trigger: 'hover',
                html: true,

            });


        } catch (e) {
            alertNew("danger", "Ocurrió un error al iniciar la página.");
        }
    });
</script>
