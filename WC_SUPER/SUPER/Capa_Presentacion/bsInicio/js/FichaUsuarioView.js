var IB = IB || {};
IB.SUPER = IB.SUPER || {};


IB.SUPER.FichaUsuarioView = (function () {
    var dom = {
        foto: $('#divFoto'),
        tarjeta: function (userData) {
            var cadena = "";
            if (userData.imgFotoBase64.length != null && typeof userData.imgFotoBase64 !== "undefined" && userData.imgFotoBase64.length > 0)
                cadena = "<img alt='Foto' id='imgFoto' src='data:image/jpeg;base64," + userData.imgFotoBase64 + "' class='img-responsive' />";

            var html = "<div class='col-xs-7'>" + 
                        "<span>" + userData.msgBienvenida + "</span><br />" +
                        "</div>" +
                        "<div class='col-xs-5'>" +
                        cadena
                        "</div>";

            return html;
        }
    }

    var render = function (userData) {
        dom.foto.html(dom.tarjeta(userData)),
        dom.foto.slideDown("slow");

        if (userData.tiempoMensajeBienvenida > 0) dom.foto.delay(userData.tiempoMensajeBienvenida * 1000).slideUp("slow");
        
    }

    return {
        render: render        
    }

})();



