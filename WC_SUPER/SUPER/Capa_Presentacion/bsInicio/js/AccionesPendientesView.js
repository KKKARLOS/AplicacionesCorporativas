var IB = IB || {};
IB.SUPER = IB.SUPER || {};


IB.SUPER.AccionesPendientesView = (function () {
    var dom = {
        divAcciones: $("#divAccionesPendientes"),
        numAcciones: $("#spanNumAcciones"),
        html: "<div id='divBox' class='offer offer-danger pull-right in'>" +
                      "<div class='shape'>" +
                          "<div class='shape-text'>" +
                              "<i style='cursor:pointer;font-size:1.2em' id='btnAcciones' data-toggle='toggle' data-target='#divBox' class='fa fa-arrows-alt' aria-hidden='true'></i>" +
                          "</div>" +
                      "</div>" +
                      "<div  class='offer-content text-center'>" +
                         "<h4>Acciones pendientes</h4>" +
                            "<img id='imgAccionesPendientes' style='cursor:pointer' src='../bsImages/imgAccionesPendientes.png' />" +
                            "<a href='../APP/bsCatalogoAccionesPendientes/default.aspx'><span id='spanNumAcciones'></span></a>" +
                      "</div>" +
                  "</div>"
    }

    var init = function (datos) {
        if (datos > 0) {
            dom.divAcciones.html(dom.html);
            $("#spanNumAcciones").html(datos);
        }
    }

    $(document).on("click", "#btnAcciones", function () {
        var selector = $(this).data("target");
        $(selector).toggleClass('in');
    });

    $(document).on("click", "#imgAccionesPendientes", function () {
        location.href = "../APP/bsCatalogoAccionesPendientes/default.aspx";
    })

    //$("#imgAccionesPendientes, #divAccionesPendientes").on("click", function () {
    //    location.href = "../APP/bsCatalogoAccionesPendientes/default.aspx";
    //})

    var numAcciones = function (datos) {
        dom.numAcciones.html()
    }

    var ocultarAcciones = function () {
        dom.divAcciones.addClass("hide");
    }

    var BloquearPGEByAcciones = function () {
        $("#menuPGE").on("click", function () {
            IB.bsalert.toastdanger("Módulo bloqueado debido a la existencia de acciones pendientes de importancia crítica.");
            return false;
        })

        $("#menuPGE").on("mouseover", function () { return false; })

        $("div[data-parent='PGE']").css("opacity", "0.4").css("pointer-events", "none");
        $("div[data-parent='PGE'] a").removeAttr("href");
        $("div[data-parent='PGE'] span").css("cursor", "default");
    }

    var BloquearPSTByAcciones = function () {
        $("#menuPST").on("click", function () {
            IB.bsalert.toastdanger("Módulo bloqueado debido a la existencia de acciones pendientes de importancia crítica.");
            return false;
        })

        $("#menuPST").on("mouseover", function () { return false; })

        $("div[data-parent='PST']").css("opacity", "0.4").css("pointer-events", "none");
        $("div[data-parent='PST'] a").removeAttr("href");
        $("div[data-parent='PST'] span").css("cursor", "default");
    }

    var BloquearIAPByAcciones = function () {
        $("#menuIAP").on("mouseover", function () {
            IB.bsalert.toastdanger("Módulo bloqueado debido a la existencia de acciones pendientes de importancia crítica.");
            return false;
        })
        $("#menuIAP").on("mouseover", function () { return false; })
        $("div[data-parent='IAP']").css("opacity", "0.4").css("pointer-events", "none");
        $("div[data-parent='IAP'] a").removeAttr("href");
        $("div[data-parent='IAP'] span").css("cursor", "default");
    }


    return {
        init: init,
        ocultarAcciones: ocultarAcciones,
        BloquearPGEByAcciones: BloquearPGEByAcciones,
        BloquearPSTByAcciones: BloquearPSTByAcciones,
        BloquearIAPByAcciones: BloquearIAPByAcciones
    }

})()