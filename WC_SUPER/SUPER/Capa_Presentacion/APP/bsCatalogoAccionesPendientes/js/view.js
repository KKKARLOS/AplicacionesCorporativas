
var SUPER = SUPER || {};
SUPER.HOME = SUPER.HOME || {};

SUPER.HOME.view = function () {

    var dom = {
        tbdAcciones: $("#tbdAcciones"),
        spanAccion: $("#tbdAcciones tr td span")

    }

    function goAccion_onClick() {
        $("#tbdAcciones tr td span").on("click", function () {
            goAccion(parseInt($(this).parent().parent().attr("data-codigo")));
        })
    }

    function goAccion(nAccion) {
        try {

            var strEnlace = "";

            switch (nAccion) {
                case 1: strEnlace = IB.vars.strserver + "Capa_Presentacion/ECO/DialogoAlertas/CatalogoPendientes/Default.aspx"; break;
                case 2: strEnlace = IB.vars.strserver + "Capa_Presentacion/CVT/Validacion/Default.aspx"; break;
                case 3: strEnlace = IB.vars.strserver + "Capa_Presentacion/CVT/MantCV/Default.aspx"; break;
                case 4: strEnlace = IB.vars.strserver + "Capa_Presentacion/CVT/Cualificacion/Default.aspx"; break;
                case 5: strEnlace = IB.vars.strserver + "Capa_Presentacion/CVT/MiCV/Default.aspx?origen=1"; break;
                case 6: strEnlace = IB.vars.strserver + "Capa_Presentacion/CVT/Mantenimientos/CuentasCVT/Default.aspx"; break;
                case 7: strEnlace = IB.vars.strserver + "Capa_Presentacion/CVT/Mantenimientos/EntornoTecno/Default.aspx"; break;
                case 8: strEnlace = IB.vars.strserver + "Capa_Presentacion/CVT/Mantenimientos/Titulaciones/Default.aspx"; break;
                case 9: strEnlace = IB.vars.strserver + "Capa_Presentacion/CVT/Borrados/Experiencias/Default.aspx"; break;
                case 10: strEnlace = IB.vars.strserver + "Capa_Presentacion/CVT/CVEvaluados/Default.aspx"; break;
                case 11: strEnlace = IB.vars.strserver + "Capa_Presentacion/SIC/Accion/CatalogoComoLider/Default.aspx"; break;
                case 12: strEnlace = IB.vars.strserver + "Capa_Presentacion/SIC/Accion/CatalogoPdteLider/Default.aspx?b3JpZ2VubWVudT1TSUM="; break;
                case 13: strEnlace = IB.vars.strserver + "Capa_Presentacion/SIC/Accion/CatalogoPosibleLider/Default.aspx"; break;
                case 14: strEnlace = IB.vars.strserver + "Capa_Presentacion/SIC/Tarea/CatalogoParticipante/Default.aspx"; break;

                case 32: strEnlace = IB.vars.strserver + "Capa_Presentacion/IAP/Calendario/Default.aspx"; break;
            }

            if (strEnlace == "") {
                IB.bsalert.toastdanger("No se ha podido determinar el destino de la acción.");
                return;
            }

            location.href = strEnlace;

        } catch (e) {
            IB.bserror.mostrarErrorAplicacion("Error al acceder a la url.", e.message)
        }
    }
   
    return {   
        dom: dom,
        goAccion_onClick: goAccion_onClick
    }

}();