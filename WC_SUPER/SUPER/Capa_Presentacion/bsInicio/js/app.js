$(document).ready(function () { SUPER.HOME.app.init(); });

var SUPER = SUPER || {};
SUPER.HOME = SUPER.HOME || {};

SUPER.HOME.app = function (view, FUview, avisosView, accionesview, getUsuariosSuperview, dal) {

    var init = function () {
        
        //Mensajes de error
        if (IB.vars.error) {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", IB.vars.error);
            return;
        }

        //Si esto se cumple abrir modal de selección de usuario
        if (bMultiusuario && sUsuarioSeleccionado == "0") {
            IB.SUPER.GetUsuariosSuper.init();
        }

        else {
            //Mensaje de bienvenida      
            if (!IB.vars["msgBienvenida"].bienvenidaMostrada && IB.vars["msgBienvenida"].mostrarMensajeBienvenida) {
                IB.DAL.post(null, "BienvenidaMostrada", null, null,
                function (data) {
                    FUview.render(IB.vars["msgBienvenida"]);
                });
            }
        }
       
        //25/05/2015 Petición de Javi Asenjo. Para verificar si la petición de entrada a SUPER viene del CURVIT viejo
        if (IB.vars.vieneDeCurvitViejo) {
            if (IB.vars.vieneDeCurvitViejo == "S") {
                location.href = IB.vars.strserver + "Capa_Presentacion/CVT/MiCV/Default.aspx";
                return;
            }
        }

        //Habilitamos los botones de acceso basándonos en el menu de navegación (roles website.map)        
        //Esto se hace después de ejecutar las acciones pendientes.
        IB.SUPER.HomeView.render();

        //Comprobamos si se ha cargado el fichero novedades.js. Si no está cargado, lo incluímos. 
        if (typeof IB.SUPER.Novedades === "object") {
            IB.SUPER.Novedades.init();
        }


        //******* AVISOS ****************************
        var indiceAvisos = 0;
        var lstAvisos;

        //Muestra el aviso actual
        var mostrarAviso = function () {

            if (indiceAvisos >= lstAvisos.length) {
                avisosView.ocultar();
                return;
            }

            avisosView.render(lstAvisos[indiceAvisos], indiceAvisos + 1);
        }

        //Destruye el aviso y muestra el siguiente de la lista.
        var destruirAviso = function () {

            try {
                var payload = { t448_idaviso: lstAvisos[indiceAvisos].t448_idaviso }
                dal.post(null, "eliminarAviso", payload, null, null);
            }
            catch (e) {
                IB.bserror.mostrarErrorAplicacion("Error de aplicación.", e.message);
            }

            indiceAvisos++;
            mostrarAviso();

        }

        //Conserva el aviso y muestra el siguiente de la lista.
        var conservarAviso = function () {

            indiceAvisos++;
            mostrarAviso();
        }

        //Si hay avisos, los mostramos y permitimos conservarlos o eliminarlos
        if (typeof IB.SUPER.AvisosView === "object") {

            //Atacheo de eventos
            avisosView.attachEvents("click", avisosView.dom.btnConservar, conservarAviso);
            avisosView.attachEvents("click", avisosView.dom.btnDestruir, destruirAviso);

            //Obtener los avisos de bbdd
            try {
                dal.post(null, "obtenerAvisos", null, null,
                    function (data) {
                        if (data.length > 0) {
                            avisosView.init(data.length); //Inicializar la modal
                            lstAvisos = data;
                            mostrarAviso();
                        }
                    }
                );
            } catch (e) {
                IB.bserror.mostrarErrorAplicacion("Error de aplicación.", e.message);
            }
        }
        //******* FIN AVISOS ****************************


        //Si tuviera, mostramos el número de acciones pendientes del usuario.
        //if (typeof IB.SUPER.AccionesPendientes === "object") {
        //if (sUsuarioSeleccionado != "") {
            try {
                dal.post(null, "ObtenerAccionesPendientes", null, null,
                    function (data) {
                        if (JSON.parse(data).length > 0) {

                            accionesview.init(JSON.parse(data).length);

                            var datos = JSON.parse(data);

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
                            })
                        }
                        else {
                            accionesview.ocultarAcciones();
                        }

                        //Habilitamos los botones de acceso basándonos en el menu de navegación (roles website.map) y las acciones pendientes
                        //IB.SUPER.HomeView.render();
                    }
                ,null,1000000);
            } catch (e) {
                IB.bserror.mostrarErrorAplicacion("Error de aplicación.", e.message)
            }
            //}

        //}
    }

    return {
        init: init
    }

}(IB.SUPER.HomeView,
  IB.SUPER.FichaUsuarioView,
  IB.SUPER.AvisosView,
  IB.SUPER.AccionesPendientesView,
  IB.SUPER.GetUsuariosSuper,
  IB.DAL);

