$(document).ready(function () { m.init(); });


var m = (function () {

     function init() {

        var options = {
            titulo: "Selección de profesionales",
            modulo: "SIC",
            autoSearch: true,
            autoShow: false,
            searchParams: {
                tipoAyuda: "lideres",
                ta201_idsubareapreventa: 23
            },
            onSeleccionar: function (data) {
                alert("seleccionado");
                console.log("seleccionar");
                console.log(data.idficepi);
                console.log(data.profesional);
            },
            onCancelar: function () {
                console.log("cancelar");
            }
        };
         //$("#divayudapersonas").buscaprof(options);
        $(".fk_ayuda").buscaprof(options);


        $("#btn1").on("click", function () {
            var o = {
                tipoAyuda: "lideres",
                ta201_idsubareapreventa: 14
            }

            $('#divayudapersonas').buscaprof("option", "searchParams", o);
            $('#divayudapersonas').buscaprof("option", "autoSearch", true);
            $('#divayudapersonas').buscaprof("option", "titulo", "Selección de líder");
            $('#divayudapersonas').buscaprof("show");
        })
        $("#btn2").on("click", function () {
            var o = { tipoAyuda: "GeneralFicepi" }
            $('#divayudapersonas2').buscaprof("option", "searchParams", o);
            $('#divayudapersonas2').buscaprof("option", "autoSearch", false);
            $('#divayudapersonas2').buscaprof("option", "titulo", "Selección de participantees");
            $('#divayudapersonas2').buscaprof("show");
        })


        /* MULTISELECCION */

        var options2 = {
            titulo: "Selección de participantes",
            modulo: "SIC",
            autoSearch: true,
            autoShow: false,
            searchParams: {
                tipoAyuda: "GeneralFicepi",
            },
            onAceptar: function (data) {
                alert("seleccionado");
                console.log("seleccionar multi");
                console.log(data.idficepi);
                console.log(data.profesional);
            },
            onCancelar: function () {
                alert("cancelado");
                console.log("cancelar multi");
            }
        };

        $("#divayudapersonasmulti1").buscaprofmulti(options2);

        $("#btn3").on("click", function () {
           
            var options2 = {
                tipoAyuda: "GeneralFicepi"

            };

            var $participantes = $("#tblParticipantes tbody tr");
            var lstParticipantes = [];

            $participantes.each(function (index) {
                oParticipante = new Object();
                oParticipante.t001_idficepi = $(this).attr("data-t001_idficepi_participante");
                oParticipante.profesional = $(this).find(".fk_nombre").text();
                oParticipante.estado = $(this).attr("data-estado");                
                lstParticipantes.push(oParticipante);
            });
 
            $('#divayudapersonasmulti1').buscaprofmulti("option", "modulo", "SIC");
            $('#divayudapersonasmulti1').buscaprofmulti("option", "lstParticipantes", lstParticipantes);            
            $('#divayudapersonasmulti1').buscaprofmulti("option", "autoSearch", false);
            $('#divayudapersonasmulti1').buscaprofmulti("option", "autoShow", false);            
            $('#divayudapersonasmulti1').buscaprofmulti("show");

        })

    }

    return {
        init: init,
    }
})();