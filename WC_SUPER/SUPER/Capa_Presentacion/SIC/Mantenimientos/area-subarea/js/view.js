var SUPER = SUPER || {};
SUPER.SIC = SUPER.SIC || {};

SUPER.SIC.view = (function () {
    var _dtFilter = null;

    var dom = {
        inputResponsable : $("#inputResponsable")
    }

    var selectores = {
        //Selectores
        fk_ta200_idareapreventa: ".fk_ta200_idareapreventa",
        fk_ta201_idsubareapreventa: ".fk_ta201_idsubareapreventa",
        filaArea: "#tblAreas tr",
        linkResponsable: "#linkResponsable"
        
    }

    function attachEvents(event, selector, callback) {
        $(selector).on(event, callback);
    }

    //live events
    function attachLiveEvents(event, selector, callback) {
        $(document).on(event, selector, callback);
    }

    function detachEvents() {
        $(selector).off(event, callback);
    }

    //detach live events
    function detachLiveEvents(event, selector, callback) {
        $(document).off(event, selector, callback);
    }
  
    function init() {
        //initAyudaResposnables();        
        initDatatableArea();
        _dtFilter = -1;
        initDatatableSubArea(_dtFilter);
    }

    function addSelected(fila) {
        $("#tblAreas tr").removeClass("selected");
        fila.addClass("selected");
        //$("#tblSubAreas").removeClass("hide").fadeIn("slow");
        return $("#tblAreas tr.selected").attr("data-ta200_idareapreventa");
        
    }

    function getIdArea(el) {
        return $(el).attr("data-id");
    }

    function getIdSubArea(el) {
        return $(el).attr("data-id");
    }

   
    function initDatatableArea() {

        $('#tblAreas').DataTable({
            "language": {
                "url": "../../../../plugins/datatables/Spanish.txt",
            },
            "language": {
                "emptyTable": "No hay resultados"
            },
            "procesing": true,
            "paginate": false,
            "responsive": false,
            "autoWidth": false,
            "order": [],
            "scrollY": "20vh",
            "scrollCollapse": false,
            "info": false,
            "searching": false,
            "destroy": true,
            "ajax": {
                "url": "Default.aspx/getAreasByFicepi",
                "type": "POST",
                "contentType": "application/json; charset=utf-8",
                "data": function () { return JSON.stringify({ origenMenu: IB.vars.origenMenu }) },
                
                "dataSrc": function (data) {                    
                    return JSON.parse(data.d);
                },
                "error": function (ex, status) {
                    if (status != "abort") IB.bserror.error$ajax(ex, status);
                }
            },

            "columns": [
                { "data": "ta200_idareapreventa" },
                { "data": "ta200_denominacion" },
                { "data": "responsable" },
                { "data": "ta199_denominacion" }

            ],

            "columnDefs": [
                {
                    "targets": 0,
                    "className": "text-center",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (row.accesoadetalle)
                                return "<span class='fk_ta200_idareapreventa underline' data-id='" + row.ta200_idareapreventa + "'>" + row.ta200_idareapreventa + "</span>";
                            else return row.ta200_idareapreventa;
                        }
                    }                    
                }

            ],

            "createdRow": function (row, data, index) {
                //fila creada
                $(row).attr("data-ta200_idareapreventa", data.ta200_idareapreventa);

            }

        })

        
    }

    function initDatatableSubArea(ta200_idareapreventa) {
        $('#tblSubAreas').DataTable({
            "language": {
                "url": "../../../../plugins/datatables/Spanish.txt",
            },
            "language": {
                "emptyTable": "No hay resultados"
            },
            "procesing": true,
            "paginate": false,
            "responsive": false,
            "autoWidth": false,
            "order": [],
            "scrollY": "25vh",
            "scrollCollapse": false,
            "info": false,
            "searching": false,
            "destroy": true,
            "ajax": {
                "url": "Default.aspx/getSubAreasByFicepi",
                "type": "POST",
                "contentType": "application/json; charset=utf-8",
                "data": function () { return JSON.stringify({ ta200_idareapreventa: _dtFilter, origenMenu: IB.vars.origenMenu }) },
                "dataSrc": function (data) {
                    return JSON.parse(data.d);
                },
                "error": function (ex, status) {
                    if (status != "abort") IB.bserror.error$ajax(ex, status);
                }
            },

            "columns": [
                { "data": "ta201_idsubareapreventa" },
                { "data": "ta201_denominacion" },
                { "data": "responsable" },
                { "data": "ta200_denominacion" }

            ],

            "columnDefs": [
                {
                    "targets": 0,
                    "className": "text-center",
                    "render": {
                        "display": function (data, type, row, meta) {
                            if (row.accesoAdetalle)
                                return "<span data-mtoFiguras='" + row.mantenimientoDeFiguras + "' class='fk_ta201_idsubareapreventa underline' data-id='" + row.ta201_idsubareapreventa + "'>" + row.ta201_idsubareapreventa + "</span>";
                            else return row.ta201_idsubareapreventa;
                        }
                    }
                }

            ],

            "createdRow": function (row, data, index) {
                //fila creada
                $(row).attr("data-ta201_idsubareapreventa", data.ta201_idsubareapreventa);

            }

        })
    }

    function estaFilaSeleccionada(ta200_idareapreventa) {
        var seleccionarFila = false;

        if ($("#tblAreas tbody tr[data-ta200_idareapreventa='" + ta200_idareapreventa + "']").hasClass("selected")) {
            seleccionarFila = true;
        }

        return seleccionarFila;
    }
    

    function refreshDatatableArea(filter, seleccionaFila) {

        _dtFilter = filter;

        var oDataTable = $('#tblAreas').DataTable();

        oDataTable.clear().draw();
        
        if (seleccionaFila)
            oDataTable.ajax.reload(function () {                
                $("#tblAreas tbody tr[data-ta200_idareapreventa='"+ filter +"']").addClass("selected");                
            });

        else oDataTable.ajax.reload(null, true);      
    }

    function refreshDatatable(filter) {

        _dtFilter = filter;

        var oDataTable = $('#tblSubAreas').DataTable();

        oDataTable.clear().draw();
        oDataTable.ajax.reload(null, true);
    }

   
    return {
        init: init,
        initDatatableSubArea:initDatatableSubArea,
        dom: dom,
        addSelected:addSelected,
        selectores:selectores,
        attachEvents: attachEvents,
        attachLiveEvents: attachLiveEvents,
        refreshDatatable: refreshDatatable,
        refreshDatatableArea:refreshDatatableArea,
        getIdArea: getIdArea,
        getIdSubArea:getIdSubArea,
        estaFilaSeleccionada: estaFilaSeleccionada
        
    }

})();


