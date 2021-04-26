
var IB = IB || {};
IB.SUPER = IB.SUPER || {};

//Vista Avisos Home
IB.SUPER.GetUsuariosSuper = (function (dal) {
    
    var init = function () {
        initModal();
        showModal();
    }
    
    var pluginName = "getUsuariosSuper";
    var dom = {        
        el: $("#divayudapersonas")
    }
    
    function initModal() {

        dom.el.append("<div class='modal fade' id='" + pluginName + "-modal'>" +
                    "<div class='modal-dialog modal-lg' role='dialog'>" +
                        "<div class='modal-content'>" +

                            "<div class='modal-header btn-primary'>" +
                                "<h4 class='modal-title'>Selección de usuario</h4>" +
                            "</div>" +

                            "<div class='modal-body'>" +                               
                                "<div class='row'>" +
                                    "<div class='col-xs-12'>" +
                                    "<table class='table table-striped'>" +
                                    "<thead>" +
                                    "<th>Usuario</th>" +
                                    "<th>Profesional</th>" +
                                    "<th>Empresa/Proveedor</th>" +
                                    "<th>C. Responsabilidad</th>" +
                                    "</thead>"+ 
                                    "<tbody class='list-group table-hover' id='" + pluginName + "-listcontent'>"+
                                    
                                       
                                    "</tbody>"+                            
                                    "</table>"+
                                    "</div>" +
                                "</div>" +
                            "</div>" +

                            "<div class='modal-footer'>" +
                                "<b>" +
                                    "<button type='button' id='" + pluginName + "-btnSeleccionar' class='btn btn-primary'>Seleccionar</button></b>" +                                
                            "</div>" +
                        "</div>" +
                   "</div>" +
              "</div>");


        //Elemento activo de la lista
        dom.el.on("click", ".fk_filas", function (e) {
            dom.el.find(".fk_filas").removeClass('active');            
            $(this).addClass('active');
        });

        //Botón seleccionar de la ventana
        dom.el.find("#" + pluginName + "-btnSeleccionar").on('click', function (e) {

            var $selected = dom.el.find("#" + pluginName + "-listcontent").find("tr.active");

            if ($selected.length == 0) {
                IB.bsalert.toastwarning("No has seleccionado ningún profesional");
                return false;
            }

            var data = {
                t314_idusuario: $selected.attr("data-t314_idusuario"),
                profesional: $selected.html()
            }
            
            location.href = IB.vars.strserver + "Default.aspx?iu=" + IB.uri.encode(data.t314_idusuario);

            hideModal();
        });
    }

    function showModal() {

        dom.el.find('.ocultable').attr('aria-hidden', 'true');
        dom.el.find(".modal").modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
        dom.el.find(".modal").modal('show');

        var n = window.setTimeout(function () { dom.el.find("#" + pluginName + "-listcontent").html("Buscando ...") }, 500)
        var html = "";

        dal.post(null, "UsuariosSuper", null, null,
            function (data) {
                data.forEach(function (item) {
                    html += "<tr data-t314_idusuario='" + item.t314_idusuario + "' class='fk_filas'>";
                    html += "<td>" + item.t314_idusuario + "</td>";
                    html += "<td>" + item.profesional + "</td>";
                    if (item.empresa == null) item.empresa = "";
                        html += "<td>" + item.empresa + "</td>";
                        if (item.t303_denominacion == null) item.t303_denominacion = "";
                        html += "<td>" + item.t303_denominacion + "</td>";
                    html +="</tr>";
                        
                });
                if (html.length == 0) html += options.notFound;

                dom.el.find("#" + pluginName + "-listcontent").html(html);
                window.clearTimeout(n);
            }
        );

    }

    function hideModal() {

        dom.el.find(".modal").modal('hide');        
        dom.el.find("#" + pluginName + "-listcontent").empty();
    }


    return {        
        init: init,        
    }

})(IB.DAL)





