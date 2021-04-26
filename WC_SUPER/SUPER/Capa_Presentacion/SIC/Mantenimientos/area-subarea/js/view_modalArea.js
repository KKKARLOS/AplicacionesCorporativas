var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.view_ModalArea = (function (dal) {
    
    _ta200_idareapreventa = null;
    var dom = {
        inputResponsable: $("#inputResponsable"),
        divAyudaProfesionales: $("#divAyudaProfesionales"),
        filasFigurasArea: $("#tblFiguras tr"),
        camposEditablesArea: $("#modal-area"),
        btnCerrar: $("#btnCerrar")
    }

    var selectores = {
        //Selectores                
        linkResponsable: "#linkResponsable",
        filasFiguras: "#tblProfesionales tr",
        checkbox: "#tblFiguras input[type='checkbox']",
        btnGrabar: "#btnGrabar",
        fk_grabar: ".fk_grabar",
        chkRequeridosArea: "#modal-area #tblFiguras input",
        camposEditablesArea: "#modal-area",
        btnCerrar:"#btnCerrar"
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    //live events
    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    //Eventos
    attachLiveEvents("click", selectores.checkbox, selCheckbox);
   

    //Checkbox excluyentes
    function selCheckbox() {

        if ($(this).is(':checked')) {            
            $(this).parent().parent().find('input[type="checkbox"]').prop("checked", false);
            $(this).prop("checked", true);
        }
    }

    //Ayuda de profesionales
    $("#linkProfesionales").on("click", function () {
        var lstSeleccionados = []
        $("#tblFiguras tr").each(function () {
            lstSeleccionados.push({ t001_idficepi: $(this).attr("data-idficepi"), profesional: $(this).find("td:nth-child(1)").html(), estado: $(this).attr("data-estado") })
        })
        dom.divAyudaProfesionales.buscaprofmulti("option", "lstSeleccionados", lstSeleccionados);
        dom.divAyudaProfesionales.buscaprofmulti("show")
    });

    dom.divAyudaProfesionales.buscaprofmulti({
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
            var tablaOriginal = $("#tblFiguras tr");
            
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
                        html += "<tr data-estado='"+ item.estado +"' data-idficepi='" + item.t001_idficepi + "'><td>" + item.profesional + "</td>";
                        html += "<td class='text-center'><input data-figura='D' required='required' type='checkbox'/></td>";//Delegado
                        html += "<td class='text-center'><input data-figura='C' required='required' type='checkbox'/></td>";//Colaborador
                        html += "<td class='text-center'><input data-figura='I' required='required' type='checkbox'/></td>";//Invitado
                        html += "</tr>";
                    }
                }
                
            });

            $('#tblFiguras').append(html);
            //Modificamos estilos 
            $("#tblFiguras tr[data-estado='X'] td").css("text-decoration", "line-through").css("color", "rgb(169, 68, 66)");
            $("#tblFiguras tr[data-estado='X'] td input[type='checkbox']").prop("disabled", true);
            $("#tblFiguras tr[data-estado='E'] td").css("text-decoration", "none").css("color", "inherit");
            $("#tblFiguras tr[data-estado='E'] td input[type='checkbox']").prop("disabled", false);
        }
    });

    function init() {
        initAyudaResponsables();
    }

    //Quitamos la clase "requerido" en cuanto se mete valor en el campo   
    attachLiveEvents("click", selectores.chkRequeridosArea, function () {
        var obj = $(this);
        if (obj.val() != null && obj.val().length > 0) obj.parent().parent().find(">td").css("border", "1px solid #ddd");
    })

    //Validación requerida del formulario
    function requiredValidation() {

        var valid = true;
       
        $("#tblFiguras tr[data-estado!='X']").each(function () {

            if (!$(this).find(">td > input").is(':checked')) {           
                $(this).find(">td").css("border", "1px solid rgba(255, 0, 0, 0.6)");
                valid = false;
            }

        });

        if (!valid)
            IB.bsalert.toastdanger("Debes cumplimentar los campos obligatorios. Están identificados en rojo.");

        return valid;
    }

    function renderModal(datos) {
        var objDatos = JSON.parse(datos);
        _ta200_idareapreventa = objDatos.ta200_idareapreventa;
        $("#txtUnidad").text(objDatos.ta199_denominacion);
        $("#inputDenominacion").val(objDatos.ta200_denominacion);
        $("#inputResponsable").val(objDatos.responsable);
        $("#inputResponsable").attr("data-idficepi", objDatos.t001_idficepi_responsable);
    }

    function initAyudaResponsables() {
        $("#linkResponsable").on("click", function () { $("#divAyudaResponsables").buscaprof("show") });
        $("#divAyudaResponsables").buscaprof({
            titulo: "Selección de responsables",
            modulo: "SIC",
            autoSearch: false,
            autoShow: false,
            tipoAyuda: "GeneralFicepi",
            notFound: "No se han encontrado resultados.",
            onSeleccionar: function (data) {
                dom.inputResponsable.attr('data-idficepi', data.idficepi);
                dom.inputResponsable.val(data.profesional);
            }
        });

    }

    function mostrarModal() {
        $("#modal-area").modal("show");
        $("#modal-area").modal({ keyboard: false, backdrop: "static", dismiss: "modal" });
    }

    function ocultarModal() {
        dom.camposEditablesArea.modal("hide");
    }

    function renderListaFiguras(datos) {
        var tblFiguras = "";
        var dat = JSON.parse(datos);
        var checkedDelegado = "";
        var checkedColaborador = "";
        var checkedInvitado = "";

        for (var i = 0; i < dat.length; i++) {
            tblFiguras += "<tr data-estado='E' data-idficepi='" + dat[i].t001_idficepi + "'>";
                tblFiguras += "<td>" + dat[i].profesional + "</td>";
                if (dat[i].ta202_figura == "D") checkedDelegado = "checked='checked'"; else checkedDelegado = "";
                if (dat[i].ta202_figura == "C") checkedColaborador = "checked='checked'"; else checkedColaborador = "";
                if (dat[i].ta202_figura == "I") checkedInvitado = "checked='checked'"; else checkedInvitado = "";

                tblFiguras += "<td class='text-center'><input   " + checkedDelegado + " required='required' type='checkbox' data-figura='D'/></td>";
                tblFiguras += "<td class='text-center'><input  " + checkedColaborador + " required='required' type='checkbox' data-figura='C'/></td>";
                tblFiguras += "<td class='text-center'><input  " + checkedInvitado + "required='required' type='checkbox' data-figura='I'/></td>";
            tblFiguras += "</tr>";
        };

        //Inyectar html en la tabla de profesionales/figuras
        $("#tblFiguras").html(tblFiguras);
    }

    //Obtenemos los valores del formulario para ser accedidos desde app.js
    function getViewValues() {
        oArea = new Object();
        //propiedades de campos.
        oArea.ta200_idareapreventa = _ta200_idareapreventa;
        oArea.ta200_denominacion = $("#inputDenominacion").val();
        oArea.t001_idficepi_responsable= $("#inputResponsable").attr("data-idficepi");
        return oArea;
    }

    function obtenerTablaFiguras() {
        var quitarEliminados = $("#tblFiguras tr[data-estado='X']")
        var selector = $("#tblFiguras tr td").not(".dataTables_empty").parent().not(quitarEliminados);
        var lista = [];

        selector.each(function () {
            var oFigurasArea = new Object();
            oFigurasArea.t001_idficepi = $(this).attr("data-idficepi");
            oFigurasArea.ta202_figura = $(this).find(">td>input:checked").attr("data-figura");
            lista.push(oFigurasArea);
        });
        return lista;
    }

    return {
        init: init,
        dom:dom,
        getViewValues: getViewValues,
        obtenerTablaFiguras:obtenerTablaFiguras,
        attachEvents:attachEvents,
        mostrarModal: mostrarModal,
        ocultarModal:ocultarModal,
        renderModal: renderModal,
        renderListaFiguras: renderListaFiguras,
        requiredValidation: requiredValidation
    }

})(IB.DAL);


