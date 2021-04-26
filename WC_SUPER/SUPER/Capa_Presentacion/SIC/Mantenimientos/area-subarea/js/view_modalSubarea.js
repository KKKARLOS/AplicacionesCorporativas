var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.view_ModalSubArea = (function (dal) {

    _ta201_idsubareapreventa = null;
    var dom = {
        inputResponsableSubArea: $("#inputResponsableSubArea"),
        divAyudaProfesionalesSubArea: $("#divAyudaProfesionalesSubArea"),
        tblFigurasSubArea: $("#tblFigurasSubArea"),
        txtDenominacionSubArea: $("#txtDenominacionSubArea"),
        chkAutoAsignacion: $("#chkAutoAsignacion"),
        modalSubArea: $("#modal-subarea")
    }

    var selectores = {
        //Selectores                
        chkRequeridosSubArea: "#modal-subarea #tblFigurasSubArea input",
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    //live events
    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    attachLiveEvents("click", selectores.chkRequeridosSubArea, function (e) {        
        if ($(this).is(':checked') && $(this).attr("data-figura") == "D") {            
            $(this).parent().parent().find("[data-figura='C']").prop("checked", false);
            $(this).prop("checked", true);
        }

        if ($(this).is(':checked') && $(this).attr("data-figura") == "C") {            
            $(this).parent().parent().find("[data-figura='D']").prop("checked", false);
            $(this).prop("checked", true);
        }

    })
   
    //Ayuda de profesionales
    $("#linkProfesionalesSubArea").on("click", function () {
        var lstSeleccionados = []
        $("#tblFigurasSubArea tr").each(function () {
            lstSeleccionados.push({ t001_idficepi: $(this).attr("data-idficepi"), profesional: $(this).find("td:nth-child(1)").html(), estado: $(this).attr("data-estado") })
        })
        dom.divAyudaProfesionalesSubArea.buscaprofmulti("option", "lstSeleccionados", lstSeleccionados);
        dom.divAyudaProfesionalesSubArea.buscaprofmulti("show")
    });

    dom.divAyudaProfesionalesSubArea.buscaprofmulti({
        titulo: "Selección de profesionales",
        tituloContIzda: "Profesionales",
        tituloContDcha: "Profesionales seleccionados",
        notFound: "No se han encontrado resultados.",
        modulo: "SIC",
        tipoAyuda: "GeneralFicepi",
        autoSearch: false,
        autoShow: false,
        eliminarExistentes: true,
        onAceptar: function (data) {
            var html = "";
            var tablaOriginal = $("#tblFigurasSubArea tr");

            //Recorremos la lista de seleccionados del plugin
            data.forEach(function (item) {

                //todo hay que añadir sólo los nuevos y los que hayan cambiado
                var _profEncontrado = false;
                for (var i = 0; i < tablaOriginal.length; i++) {

                    if (tablaOriginal[i].getAttribute("data-idficepi") == item.t001_idficepi) {
                        if (tablaOriginal[i].getAttribute("data-estado") != item.estado) {
                            if (item.estado == "X") {
                                tablaOriginal[i].setAttribute("data-estado", "X");
                            }
                            else {
                                tablaOriginal[i].setAttribute("data-estado", "E");
                                _profEncontrado = true;
                            }
                        }

                        else {
                            _profEncontrado = true;
                        }
                    }
                }

                if (!_profEncontrado) {
                    if (item.estado != "X") {
                        //html += "<tr data-estado='N' data-idficepi='" + item.t001_idficepi + "'><td>" + item.profesional + "</td>";
                        html += "<tr data-estado='" + item.estado + "' data-idficepi='" + item.t001_idficepi + "'><td>" + item.profesional + "</td>";
                        html += "<td class='text-center'><input data-figura='D' required='required' type='checkbox'/></td>";//Delegado
                        html += "<td class='text-center'><input data-figura='C' required='required' type='checkbox'/></td>";//Colaborador
                        html += "<td class='text-center'><input data-figura='L' required='required' type='checkbox'/></td>";//Líder
                        html += "</tr>";
                    }
                }

            });

            $('#tblFigurasSubArea').append(html);
            //Modificamos estilos 
            $("#tblFigurasSubArea tr[data-estado='X'] td").css("text-decoration", "line-through").css("color", "rgb(169, 68, 66)");
            $("#tblFigurasSubArea tr[data-estado='X'] td input[type='checkbox']").prop("disabled", true);

            

            $("#tblFigurasSubArea tr[data-estado='E'] td").css("text-decoration", "none").css("color", "inherit");
            
        }
    });

    function init() {
       initAyudaResponsablesSubArea();        
    }

    //Quitamos la clase "requerido" en cuanto se mete valor en el campo
    attachLiveEvents("click", selectores.chkRequeridosSubArea, function () {
        var obj = $(this);
        if (obj.val() != null && obj.val().length > 0) obj.parent().parent().find(">td").css("border", "1px solid #ddd");
    })

    //Validación requerida del formulario
    function requiredValidation() {

        var valid = true;
      
        $("#tblFigurasSubArea tr[data-estado!='X']").each(function () {

            if (!$(this).find(">td > input").is(':checked')) {
                $(this).find(">td").css("border", "1px solid rgba(255, 0, 0, 0.6)");
                valid = false;
            }

        });

        if (!valid)
            IB.bsalert.toastdanger("Debes cumplimentar los campos obligatorios. Están identificados en rojo.");

        return valid;
    }

    //$("#btnCerrarSubArea").on("click", function () {
    //    dom.tblFigurasSubArea.html("");
    //})

    $("#modal-subarea").on("hidden.bs.modal", function () {
        dom.tblFigurasSubArea.html("");
    });

    function renderModal(datos) {
        var objDatos = JSON.parse(datos);
        _ta201_idsubareapreventa = objDatos.ta201_idsubareapreventa;
        $("#txtDenominacionSubArea").val(objDatos.ta201_denominacion);
        $("#inputResponsableSubArea").val(objDatos.responsable);
        $("#inputResponsableSubArea").attr("data-idficepi", objDatos.t001_idficepi_responsable);
        if (objDatos.ta201_permitirautoasignacionlider)
            $("#chkAutoAsignacion").prop("checked", true);
        else $("#chkAutoAsignacion").prop("checked", false);

        var mtoFiguras = $("#tblSubAreas tr[data-ta201_idsubareapreventa='" + _ta201_idsubareapreventa + "']").find("span").attr("data-mtofiguras");
        //Se oculta en caso de no tener acceso
        if (!mtoFiguras)
            $("#divFiguras").css("display", "none");
    }

    function initAyudaResponsablesSubArea() {        
        $("#linkResponsableSubArea").on("click", function () { $("#divAyudaResponsablesSubArea").buscaprof("show") });
        $("#divAyudaResponsablesSubArea").buscaprof({
            titulo: "Selección de responsables",
            modulo: "SIC",
            autoSearch: false,
            autoShow: false,
            tipoAyuda: "GeneralFicepi",
            notFound: "No se han encontrado resultados.",
            onSeleccionar: function (data) {                
                dom.inputResponsableSubArea.attr('data-idficepi', data.idficepi);
                dom.inputResponsableSubArea.val(data.profesional);
            }
        });

    }

    function mostrarModal() {
        $("#modal-subarea").modal("show");
        $("#modal-subarea").modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    }

    function ocultarModal() {
        dom.modalSubArea.modal("hide");
    }

    function renderListaFigurasSubArea(datos) {
        
        var dat = JSON.parse(datos);        
        var _idficepi = 0;
        var tblTR = "";
        
        for (var i = 0; i < dat.length; i++) {
                        
            if (dat[i].t001_idficepi != _idficepi) {
                tblTR = "<tr data-estado='E' data-idficepi='" + dat[i].t001_idficepi + "'>";
                tblTR += "<td>" + dat[i].profesional + "</td>";
                tblTR += "<td class='text-center'><input required='required' type='checkbox' data-figura='D'/></td>";
                tblTR += "<td class='text-center'><input required='required' type='checkbox' data-figura='C'/></td>";
                tblTR += "<td class='text-center'><input required='required' type='checkbox' data-figura='L'/></td>";
                tblTR += "</tr>";

                $("#tblFigurasSubArea").append(tblTR);

            }

            $("#tblFigurasSubArea tr[data-idficepi='" + dat[i].t001_idficepi + "']").find("input[data-figura='" + dat[i].ta203_figura + "']").prop("checked", true);

            _idficepi = dat[i].t001_idficepi;
        };

        
    }

    function obtenerTablaFiguras() {
        var quitarEliminados = $("#tblFigurasSubArea tr[data-estado='X']")
        var selector = $("#tblFigurasSubArea tr td").not(".dataTables_empty").parent().not(quitarEliminados);
        var lista = [];

        selector.each(function () {
            var oFigurasSubArea = new Object();
            oFigurasSubArea.t001_idficepi = $(this).attr("data-idficepi");
            oFigurasSubArea.ta203_figura = $(this).find(">td>input:checked").attr("data-figura");
                            
            lista.push(oFigurasSubArea);

            if ($(this).find(">td>input:checked").length > 1) {
                oFigurasSubArea = new Object();
                oFigurasSubArea.t001_idficepi = $(this).attr("data-idficepi");
                oFigurasSubArea.ta203_figura = $(this).find(">td>input:checked")[1].getAttribute("data-figura");
                lista.push(oFigurasSubArea);
            }

        });
        return lista;
    }

    //Obtenemos los valores del formulario para ser accedidos desde app.js
    function getViewValues() {
        oArea = new Object();
        //propiedades de campos.
        oArea.ta201_idsubareapreventa = _ta201_idsubareapreventa;
        oArea.ta201_denominacion = dom.txtDenominacionSubArea.val();
        oArea.t001_idficepi_responsable = dom.inputResponsableSubArea.attr("data-idficepi");
        oArea.ta201_estadoactiva = null;
        if ($("#chkAutoAsignacion").is(':checked'))
            oArea.ta201_permitirautoasignacionlider = true;
        else oArea.ta201_permitirautoasignacionlider = false;
        return oArea;
    }

    return {
        init: init,
        dom: dom,
        getViewValues: getViewValues,
        obtenerTablaFiguras: obtenerTablaFiguras,
        attachEvents: attachEvents,
        mostrarModal: mostrarModal,
        ocultarModal: ocultarModal,
        renderModal: renderModal,
        renderListaFigurasSubArea: renderListaFigurasSubArea,
        requiredValidation: requiredValidation
    }

})(IB.DAL);


