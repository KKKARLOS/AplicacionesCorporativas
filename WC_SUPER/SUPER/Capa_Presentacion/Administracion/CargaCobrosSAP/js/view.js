var SUPER = SUPER || {};
SUPER.APP = SUPER.APP || {};
SUPER.APP.CobrosSAP = SUPER.APP.CobrosSAP || {}

SUPER.APP.CobrosSAP.View = (function (e) {
    var dom = {
        btnProcesar: $('#btnProcesar'),
        txtFecha: $("#txtFecha")
    };
    var selectores = {
        //btnModif: ".btnModif"
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }
    //para elementos que no existen de inicio
    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    var init = function () {

        //Datepicker fecha
        dom.txtFecha.datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd/mm/yy',
            //yearRange: new Date().getFullYear() + ":2050",
            yearRange: new Date().getFullYear()-1 + ":" + new Date().getFullYear(),
            //minDate: 0,

            beforeShow: function (input, inst) {
                //$(inst.dpDiv).removeClass('calendar-off');
            },
            onClose: function (dateText, inst) {
                var FFEdate;
                try {
                    FFEdate = $.datepicker.parseDate('dd/mm/yy', dateText);
                } catch (e) {
                    IB.bsalert.toastdanger("La fecha introducida no es correcta.");
                    //dom.txtffindesde.val("");
                    dom.txtFecha.focus();
                    return;
                }
            }

        });
    }

    var visualizarContenedor = function () {

        $(selectores.container).css("visibility", "visible");
    }

    /*var resalta = function(n) {

        for (var i = 1; i < n; i++) {
            $("#lbl" + i).css("font-weight", "normal"); 
            $("#img" + i).attr("src", IB.vars["strserver"] + "Images/imgAceptar.gif");
        }

        if (n <= 3 && $("#img" + n).attr("src").indexOf("ajax-loader.gif") == -1) {
            $("#lbl" + n).css("font-weight", "bold");
            $("#img" + n).attr("src", IB.vars["strserver"] + "Images/ajax-loader.gif");
        }
    }

    var resaltaError = function (n) {

        for (var i = 1; i < n; i++) {
            $("#lbl" + i).css("font-weight", "normal");
            $("#img" + i).attr("src", IB.vars["strserver"] + "Images/imgAceptar.gif");
        }
        $("#lbl" + n).css("font-weight", "normal");
        $("#img" + n).attr("src", IB.vars["strserver"] + "Images/imgCancelar.gif");
    }*/

    return {
        init: init,
        dom: dom,
        selectores: selectores,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        visualizarContenedor: visualizarContenedor
        /*resalta: resalta,
        resaltaError: resaltaError*/
}

}());

