if (!window.JSON) $.ajax({ type: "GET", url: "../../../Javascript/json-2.4.js", dataType: "script" });
//if (!window.JSON) $.ajax({ type: "GET", url: "../../../Javascript/json2.js", dataType: "script" });
var contador = 0;
var form;
var texto = "";
//***Directiva ajax
$.ajaxSetup({ cache: false });

var arrayUpdate = [];

var oNodo = new Object();

function init() {
    //actualizarLupas("tblCatIni", "tree");
}
function unload() {
    if (bOnline) bCambios = false;
}

$(function() {

    $("#tree").fancytree({
        checkbox: false,
        selectMode: 3,
        tabbable: true,
        titlesTabbable: false,     // Add all node titles to TAB chain
        source: arbol,
        extensions: ["table", "gridnav"],
        table: {
            indentation: 20,
            nodeColumnIdx: 1,
            checkboxColumnIdx: 0
        },
        gridnav: {
            autofocusInput: false,
            handleCursorKeys: true
        },

        renderColumns: function(event, data) {
            var node = data.node,
            $tdList = $(node.tr).find(">td");
            $tdList.eq(1).addClass("alignVertical");
            var reg = /\|/g;
            $tdList.eq(1)[0].children[0].children[2].innerText = $tdList.eq(1)[0].innerText.replace(reg, "'");
            //if (node.key != "0")
            var sChecked = (node.data.activo == 1) ? "checked=true" : "";
            $tdList.eq(2).html("<input type='checkbox' class='check' style='cursor:pointer;' tipo='activo' id='" + node.key + "' value='" + node.data.activo + "' " + sChecked + ">");
            $tdList.eq(2).addClass("alignCenter");
        }
    }   )
});

$(function() {
    /* Evento al hacer click sobre algun dato de la columna activo */

    $("#tree").delegate("input[tipo='activo']", "click", function(e) {
        var node = $.ui.fancytree.getNode(e);
	    $input = $(e.target);
        if($input.is(":checked")) 
            node.data.activo = "1";     
        else
            node.data.activo = "0";

        node.data.bd = "U";
        aG();
        if (bOnline) grabar();
        
        e.stopPropagation();  // prevent fancytree activate for this row
    });
});
function aG() {
    activarGrabar();
}

function RegUpdates() {
    var NodoRaiz = $("#tree").fancytree("getRootNode")
    var proceso = true;
    if (NodoRaiz != null) {
        nodes = NodoRaiz.getChildren();

        for (var i = 0; i < nodes.length; i++) {
            var node = nodes[i];
            if (node == null) break;
            if (node.key == "") continue;
            if (node.data.bd == "U") {
                var o = new Object();
                o.key = node.key;
                o.nivel = node.data.nivel;
                o.activo = node.data.activo;
                arrayUpdate.push(o);
                node.data.bd = "";
            }
            node.span.children[2].className = "fancytree-title";
            if (node.children == null) continue;
            if (node.children.length > 0) RegNodosHijos(node.children);
        }
    }
}
function RegNodosHijos(aNodosHijos) {
    if (aNodosHijos == null) return false;

    for (var j = 0; j < aNodosHijos.length; j++) {
        node = aNodosHijos[j];
        if (node.key == "") continue;

        if (node.data.bd == "U") {
            var o = new Object();
            o.key = node.key;
            o.nivel = node.data.nivel;
            o.activo = node.data.activo;
            arrayUpdate.push(o);
            node.data.bd = "";
        }

        try { $(node.span.children[2]).removeClass("fancytree-color"); } catch (e) { };

        if (node.children == null) {
            RegNodosHijos(null);
            continue;
        }
        if (node.children.length > 0) RegNodosHijos(node.children);
    }
    return true;
}
function grabar() {
    RegUpdates();
    var sb = new StringBuilder;

    if (arrayUpdate.length > 0) {
        for (var i = 0; i < arrayUpdate.length; i++) {
            if (arrayUpdate[i].key == "") continue;
            var obj = arrayUpdate[i]
            //sb.Append("U##"); //0
            sb.Append(obj.key + "##"); //1
            sb.Append(obj.nivel + "##"); //2
            sb.Append(obj.activo + "##"); //3      
            sb.Append("///");
            //alert("key: " + obj.key + " title: " + obj.title + " parent:" + obj.parent + " parentIni:" + obj.parentIni);
        }
    }
    $I("hdnUpdate").value = sb.ToString();
    sb = null;

    //if ($I("hdnDelete").value == "" && $I("hdnInsert").value == "" && $I("hdnUpdate").value == "") {
    if ($I("hdnUpdate").value == "") {
        desActivarGrabar();
        alert("No hay datos modificados")
        return;
    }

    mostrarProcesando();

    $.ajax({
        url: "Default.aspx/grabarAjax",
        data: JSON.stringify({sUpdate: $I("hdnUpdate").value }),
        async: true,
        type: "POST", // data has to be POSTed
        contentType: "application/json; charset=utf-8", // posting JSON content    
        dataType: "json",  // type of data is JSON (must be upper case!)
        //timeout: 10000,    // AJAX timeout. Se comentariza para debugar
        success: function(result) {
            var aResul = result.d.split("@#@");
            if (aResul[0] != "OK")        
            {
                ocultarProcesando();
                desActivarGrabar();
                //arrayDelete = [];
                bCambios = false;

                if (aResul[2] == "547") //error de integridad referencial, no se ha podido eliminar//error de integridad referencial, no se ha podido eliminar
                    mmoff("inf", "No se ha podido realizar el borrado porque existen datos relacionados con el elemento.", 540);
                else
                    mmoff("Err", aResul[1], 540);                
                return;
            }
            
            arrayUpdate = [];

            ocultarProcesando();
            //mmoff("Suc", "Grabación correcta", 160);
            desActivarGrabar();

        },
        error: function(ex, status) {
            error$ajax("Ocurrió un error en la aplicación", ex, status)
        }
    });
}
function error$ajax(msg, ex, status) {
    ocultarProcesando();
    if (status == "timeout") {
        alert("Se ha sobrepasado el tiempo límite de espera para procesar la petición en servidor.\n\nVuelve a intentarlo y, si persiste el problema, notifica la incidencia al CAU.\n\nDisculpa las molestias.");
        return;
    }
    bCambios = false;
    var desc = "";
    var reg = /\\n/g;
    if (ex.responseText != "undefined") {    
        desc = Utilidades.unescape($.parseJSON(ex.responseText).Message);
        desc = desc.replace(reg, "\n");
        var iPos = desc.indexOf("integridad referencial");
        if (iPos > 0) {
            //arrayDelete = [];
            desc = "No se puede eliminar el elemento al estar ya asignado.";
            mmoff("Inf", desc, 360);
            return;
        }
    }
    arrayUpdate = [];
    mostrarError(msg + "\n\n" + desc);
}

/* Funciones para las lupas*/
/* Valores necesario para las funciones de buscarDescripcion + buscarSiguiente */
var strCadena = "";
var bPrimeraBusqueda = 0;
/****************************/

function buscarDescripcion() {
    mostrarProcesando();
    bPrimeraBusqueda = 0;
    $I("imgLupa1").style.display = "none";
    //window.focus();
    modalDialog.Show(strServer + "Capa_Presentacion/buscarString.aspx", self, sSize(280, 110))
        .then(function(ret) {
            if (ret != null) {
                buscarTitle(ret);
                strCadena = ret;
            }
        });

    ocultarProcesando();
}
function buscarSiguiente() {
    bPrimeraBusqueda++;
    buscarTitle(strCadena);
}

function getObjects(obj, key, val) {//para buscar cadenas de texto
    var objects = [];
    for (var i in obj) {
        if (!obj.hasOwnProperty(i)) continue;
        if (typeof obj[i] == 'object') {
            objects = objects.concat(getObjects(obj[i], key, val));
        } else if (i == key && obj[key].toUpperCase().indexOf(val.toUpperCase()) != -1) {
            objects.push(obj);
        }
    }
    return objects;
}

function buscarTitle(sBus) {
    var tree = $("#tree").fancytree("getTree");
    arbol = tree.toDict(true);
    //    alert(JSON.stringify(arbol));
        
    var aKey = getObjects(arbol, 'title', sBus); // Returns an array of matching objects
    if (aKey.length > 0) { // si hay coincidencias
        var tree = $("#tree").fancytree("getTree");
        if (bPrimeraBusqueda == aKey.length) bPrimeraBusqueda = 0;
        var aParents = obtenerHijos(tree.getNodeByKey(aKey[bPrimeraBusqueda].key));
        while (aParents.length != 0) //expandimos todos los nodos desde la raiz hasta el nodo que deseamos.
            tree.getNodeByKey(aParents.pop()).setExpanded();
        tree.activateKey(aKey[bPrimeraBusqueda].key);
        //tree.getNodeByKey(aKey[bPrimeraBusqueda].key).scrollIntoView(false);
        $I("imgLupa1").style.display = "";
        $I("divCatalogo").scrollTop = obtenerScroll(tree.getNodeByKey(aKey[bPrimeraBusqueda].key).tr) * 20;
    }
}

function obtenerHijos(node) {
    var aParents = [];
    if (node == null) return aParents;
    if (node.parent == null) return aParents;
    else {
        aParents.push(node.key);
        aParents = aParents.concat(obtenerHijos(node.parent));
    }
    return aParents;
}

function obtenerScroll(oFila) {
    var tabla = oFila.parentNode;
    var scroll = oFila.rowIndex
    for (var i = 0; i < oFila.rowIndex; i++)
        if (tabla.children[i].style.display == "none") scroll--;
    return scroll - 1;
}

/*Fin Funciones para las lupas*/
