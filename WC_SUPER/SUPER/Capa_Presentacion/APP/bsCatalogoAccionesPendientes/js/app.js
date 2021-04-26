$(document).ready(function () { SUPER.HOME.app.init(); });

var SUPER = SUPER || {};
SUPER.HOME = SUPER.HOME || {};

SUPER.HOME.app = function (dal, accionesview, view) {

    var init = function () {

        //Mensajes de error
        if (IB.vars.error) {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", IB.vars.error);
            return;
        }

        //Si tuviera, mostramos el número de acciones pendientes del usuario.
        //if (typeof IB.SUPER.AccionesPendientes === "object") {
        //if (sUsuarioSeleccionado != "") {
            try {
                dal.post(null, "ObtenerAccionesPendientes", null, null,
                    function (data) {
                        if (JSON.parse(data).length > 0) {

                            var datos = JSON.parse(data);
                            var html = "";

                            $(datos).each(function () {
                                if (this.BloquearPGEByAcciones == "1") {
                                    accionesview.BloquearPGEByAcciones();//Bloquear menu PGE                                    
                                }

                                if (this.BloquearPSTByAcciones == "1") {
                                    accionesview.BloquearPSTByAcciones();//Bloquear menu PST                                    
                                }

                                if (this.BloquearIAPByAcciones == "1") {
                                    accionesview.BloquearIAPByAcciones();//Bloquear menu IAP                                    
                                }

                                html += "<tr data-codigo='"+ this.codigo +"'><td><span class='underline'>"+ this.Denominacion +"</span></td></tr>";
                              
                               

                            })
                            
                            view.dom.tbdAcciones.html(html);

                            view.goAccion_onClick();
                           
                        }
                        else {
                            accionesview.ocultarAcciones();
                        }
                    }
                );
            } catch (e) {
                IB.bserror.mostrarErrorAplicacion("Error de aplicación.", e.message)
            }
            //}

        //}
    }

   

    return {
        init: init
    }

}(IB.DAL, IB.SUPER.AccionesPendientesView, SUPER.HOME.view);