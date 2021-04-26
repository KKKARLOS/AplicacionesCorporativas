var IB = IB || {};
IB.SUPER = IB.SUPER || {};
var template;

IB.SUPER.NovedadesView = (function () {
 
    var dom = {        
        novedades: $('#divNovedades'),
        btnCerrar: $("#btncerrarNovedades"),        
        html: "<div class='offer offer-primary'>"+
                        "<div class='shape'>"+
                            "<div class='shape-text'>"+
                                "NEW"+
                            "</div>"+
                        "</div>"+
                        "<div class='offer-content'>"+
                            "<button id='btncerrarNovedades' type='button' class='close'>&times;</button><br />"+
                            "<p>"+
                             "   Se han producido novedades en la aplicación SUPER."+
						"<br />"+
                                "<br />"+
                                "Para verlas, pulsa <a href='" + IB.vars.strserver + "Capa_Presentacion/Ayuda/Novedades/Default.aspx'>aquí</a>." +
                            "</p>"+
                        "</div>"+
                    "</div>"               
    }

   

    var render = function () {
        //Para inyectarselo directamente... sin usar plantilla tpl
        dom.novedades.html(dom.html);
        dom.novedades.slideDown("slow");

        //esto es para cargar la plantilla con handlebars
        //$.get('templates/novedades.html',mostrarNovedades)
    }

    var mostrarNovedades = function (filecontent) {
        var source = $(filecontent).filter('#novedadesTpl').html();
        var template = Handlebars.compile(source);        
        var html = template(template);
        dom.novedades.html(html);
        dom.novedades.slideDown("slow");
    }

    var ocultarNovedades = function () {
        dom.novedades.slideUp("slow");
    }

    dom.novedades.on("click", dom.btnCerrar, ocultarNovedades);
    

    return {
        render: render
    }
})();

IB.SUPER.Novedades = (function (view) {

    var init = function () {
        view.render();
    }

    return {
        init: init        
    }

})(IB.SUPER.NovedadesView);


