var procesando = (function () {


    var scrollProcesando = function () {
        var d = $("#divlock_procesando");
        if ($(d).css("display") == "block") {
            var span = $(d).find("span:first");
            var h = $(document).scrollTop() + ($(window).height() / 2);
            h = h - (72 / 2);

            $(span).css("top", h);
        }
    }


    return {

        mostrar: function () {

            var h = $(document).scrollTop() + ($(window).height() / 2);
            var w = $(document).width() / 2;

            h = h - (72 / 2);
            w = w - (72 / 2);


            var d = $("#divlock_procesando");
            if (d.length == 0) {
                $("body").append("<div id='divlock_procesando' style='position:absolute; top:0px;'>" +
                                 "<div id='divlock2_procesando' class='ui-widget-overlay' style='position:relative; z-index: 10000; display:block; '></div>" +
                                    "<span style='position: absolute;top:" + h + "px;left:" + w + "px;z-index:10001;border:5px solid #FFF;padding:15px;'>" +
                                        "<img src='" + strServer + "imagenes/preloader.png' style='vertical-align:top'></img>" +                                        
                                "</span></div>");
                d = $("#divlock_procesando");
            }


            $(d).css("height", $(document).height());
            $(d).css("width", $(document).width());
            $(d).css("display", "block");

            $(window).off("scroll", scrollProcesando).on("scroll", scrollProcesando);


        },

        ocultar: function () {
            $("#divlock_procesando").css("display", "none");
            $(window).off("scroll", scrollProcesando);
        }


    };

})();
