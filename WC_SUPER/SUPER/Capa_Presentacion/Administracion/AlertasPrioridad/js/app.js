$(document).ready(function () {
    SUPER.APP.PrioAlertas.app.init();
})

var SUPER = SUPER || {};
SUPER.APP = SUPER.APP || {};
SUPER.APP.PrioAlertas = SUPER.APP.PrioAlertas || {}

SUPER.APP.PrioAlertas.app = (function (view) {

    var init = function () {
        if (typeof IB.vars.error !== "undefined") {
            IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido un error en la carga de la pantalla<br/><br/>" + IB.vars.error);
            return;
        }
        view.init();

        //SELECCIONAR FILA DEL CATALOGO
        view.attachLiveEvents("click", view.selectores.filas, marcarFila);
        //Añadir al catálogo 
        //view.attachEvents("click", view.dom.icoNuevo, addElemento);
        view.attachEvents("click", view.dom.btnAdd, addElemento);
        //Modificar fila seleccionada del catálogo
        view.attachEvents("click", view.dom.btnMod, goDetalle);
        //Eliminar fila seleccionada del catálogo
        //view.attachEvents("click", view.dom.btnDel, eliminar);
        //Acceso a detalle 
        //view.attachEvents("click", view.dom.btnGoDetalle, goDetalle);
        //editar al detalle 
        view.attachLiveEvents("dblclick", view.selectores.filaSel, goDetalle);
        //Botones exportación a excel
        view.attachLiveEvents("click", view.selectores.btnExcel, excelCatalogo);

        //Aceptar del modal de detalle
        view.attachEvents("keypress click", view.dom.btnAceptar, aceptarDetalle);


        //Al seleccionar un elemento en el primer combo
        view.attachEvents("change", view.dom.cboAlert1, cargarAlertas2);
        //Al seleccionar un elemento en el segundo combo
        view.attachEvents("change", view.dom.cboAlert2, cargarGanadora);

        consultar();

        cargarAlertas1();

    }

    var modo_modal;
    var idElemento_editing;

    var marcarFila = function (event) {
        view.marcarLinea(this);
    }

    var consultar = function () {
        var defer = $.Deferred();

        //var payload = { nTarea: nTarea, tipoAsunto: tipoAsunto, estado: estado };
        var payload = {};
        IB.DAL.post(null, "getCatalogo", payload, null,
            function (data) {
                //if (data.length != 0) {
                view.crear(data);
                //}
                $.when(IB.procesando.ocultar()).then(function () {
                    view.visualizarContenedor();
                    defer.resolve();
                });
            }
        );
        return defer.promise();
    }

    var grabacionCorrecta = function () {
        IB.bsalert.toast("Grabación correcta.", "info");
        //desactivarGrabar();
    }

    //Acceso a otras pantallas
    var addElemento = function (e) {
        modo_modal = "A";
        view.modalDetalle.init("A");
        //$.when(loadUnidades()).then(view.modalDetalle.mostrar);
        view.modalDetalle.mostrar();
    }
    var goDetalle = function () {
        IB.procesando.mostrar();
        idElemento_editing = $(view.selectores.filaSel).attr("id");
        //var el = event.target ? event.target : event.srcElement;
        //idElemento_editing = $(el).attr("id");
        if (!idElemento_editing) {
            IB.procesando.ocultar();
            IB.bsalert.toastwarning("Debes seleccionar una fila");
            return;
        }

        modo_modal = "E"
        view.modalDetalle.init("E");

        //Obtener la denominación de la fila del datatable y pintarla en la modal
        var table = view.dom.tabla.DataTable();
        var row = table.row('#' + idElemento_editing);
        //var o = {
        //        idAlert1: row.data().a1, idAlert2: row.data().a2, idAlertG: row.data().ag,
        //        denAlert1: row.data().denAlert1, denAlert2: row.data().denAlert2,
        //        grupo1: row.data().g1, grupo2: row.data().g2, grupoG: row.data().gg
        //}
        //view.modalDetalle.setValues(o);
        view.dom.cboAlert1.val(row.data().t820_idalerta_1);
        cargarAlertas2(row.data().grupo1);
        view.dom.cboAlert2.val(row.data().t820_idalerta_2);
        cargarGanadora();
        view.dom.cboAlertG.val(row.data().t820_idalerta_g);

        view.modalDetalle.mostrar();
        IB.procesando.ocultar();
    }
    //Pulsado del botón de aceptar del modal de detalle
    var aceptarDetalle = function (event) {
        //Hay que grabar y a la vuelta refrescar el catalogo

        IB.procesando.mostrar();
        if (modo_modal == "A") {//Alta
            //Compruebo que no exista ya el binomio alerta1 y alerta2
            if (!comprobarDatos())
                return;
            //grabar y en el callback refrescar datatable y seleccionar fila.
            var oPrioridad = view.modalDetalle.getValues();
            var payload = { mPrioridad: oPrioridad }
            IB.DAL.post(null, "Alta", payload, null,
                function (data) {
                    $.when(IB.procesando.ocultar()).then(function () {
                        view.modalDetalle.ocultar();
                        IB.bsalert.toast("Grabación correcta.");
                        consultar();
                    });
                }
            );
        }
        else { //Edición
            //grabar y en el callback refrescar datatable y seleccionar fila.
            var oPrioridad = view.modalDetalle.getValues();
            var payload = { mPrioridad: oPrioridad }
            IB.DAL.post(null, "Edicion", payload, null,
                function (data) {
                    $.when(IB.procesando.ocultar()).then(function () {
                        view.modalDetalle.ocultar();
                        IB.bsalert.toast("Grabación correcta.");
                        /*
                        //actualizar celda en el datatable
                        var table = $('#tabla').DataTable();
                        var row = table.row('#' + idElemento_editing);
                        var oData = row.data();
                        oData.t840_descripcion = payload.denominacion;
                        //falta asignar el valor del combo
                        row.data(oData).draw();
                        */
                        consultar();
                    });
                }
            );
        }
    }
    function comprobarDatos() {
        var bOK = true;
        var a1 = view.dom.cboAlert1.val();
        var a2 = view.dom.cboAlert2.val();

        view.dom.tbl.rows().every(function (rowIdx, tableLoop, rowLoop) {
            var data = this.data();
            if ((data.t820_idalerta_1 == a1 && data.t820_idalerta_2 == a2) || (data.t820_idalerta_1 == a2 && data.t820_idalerta_2 == a1))
                bOK = false;
        });
        if (!bOK)
            IB.bsalert.toastdanger("La prioridad ya está registrada.");

        return bOK;
    }
    function excelCatalogo() {
        if (!view.dom.tbl.rows().count()) {
            IB.bsalert.fixedAlert("warning", "Error de validación", "No hay datos para exportar");
            return;
        }
        IB.Exportaciones.exportarDataTableExcel(view.dom.tabla, "PrioridadesAlertas", "Prioridades de alertas", false);
    }

    var cargarAlertas1 = function () {
        var listitems="";
        var listitems2="";
        var idAlertaIni = "";
        var idGrupoIni="";//Grupo de la primera alerta del primer combo
        var tipoIni="";
        $.each(IB.vars.lstAlertas, function (key, value) {
            listitems += '<option value=' + value.t820_idalerta + '>' + value.t820_idalerta + ' ' + value.t820_denominacion + '</option>';
            //Relleno el segundo combo en funcion del grupo y tipo del primer elemento del primer combo
            if (idAlertaIni == "") {
                idAlertaIni = value.t820_idalerta;
                idGrupoIni = value.t821_idgrupoalerta;
                tipoIni = value.t820_tipo;
            }
            if (idAlertaIni != value.t820_idalerta && idGrupoIni == value.t821_idgrupoalerta && tipoIni == value.t820_tipo)
                listitems2 += '<option value=' + value.t820_idalerta + '>' + value.t820_idalerta + ' ' + value.t820_denominacion + '</option>';
        });
        view.dom.cboAlert1.append(listitems);
        view.dom.cboAlert2.append(listitems2);
        cargarGanadora();
    }
    var cargarAlertas2 = function (idGrupo) {
        view.dom.cboAlert2.empty();
        var listitems2="";
        var idAlertaIni = view.dom.cboAlert1.val();
        var idGrupoIni = getGrupo(idAlertaIni);//Grupo de la primera alertta del primer combo
        var tipoIni = getTipo(idAlertaIni);
        $.each(IB.vars.lstAlertas, function (key, value) {
            //Relleno el segundo combo en funcion del grupo y tipo del primer elemento del primer combo
            if (idAlertaIni != value.t820_idalerta && idGrupoIni == value.t821_idgrupoalerta && tipoIni == value.t820_tipo)
                listitems2 += '<option value=' + value.t820_idalerta + '>' + value.t820_idalerta + ' ' + value.t820_denominacion + '</option>';
        });
        view.dom.cboAlert2.append(listitems2);
        cargarGanadora();
    }
    var getGrupo = function (idAlerta) {
        var idGrupo = "";
        $.each(IB.vars.lstAlertas, function (key, value) {
            if (idAlerta == value.t820_idalerta)
                idGrupo = value.t821_idgrupoalerta;
        });
        return idGrupo;
    }
    var getTipo = function (idAlerta) {
        var tipo = "";
        $.each(IB.vars.lstAlertas, function (key, value) {
            if (idAlerta == value.t820_idalerta)
                tipo = value.t820_tipo;
        });
        return tipo;
    }
    var cargarGanadora = function () {
        view.dom.cboAlertG.empty();
        var listitems = '<option value=' + view.dom.cboAlert1.val() + '>' + view.dom.cboAlert1.find('option:selected').text() + '</option>';
        listitems += '<option value=' + view.dom.cboAlert2.val() + '>' + view.dom.cboAlert2.find('option:selected').text() + '</option>';
        view.dom.cboAlertG.append(listitems);
    }

    return {
        init: init
    }

})(SUPER.APP.PrioAlertas.View);
