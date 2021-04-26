
var currentNode;
var currentOtherNode;
/*Funciones de grabación*/
function grabarCambioCheck(chk, key){
    mostrarProcesando();
    $.ajax({
            url: "Default.aspx/GrabarCCCheck",   // Current Page, Method
            cache: false,
            data: JSON.stringify({id: key, ischecked : (($(chk).is(':checked'))? 1 :0) }),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content    
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 30000,    // AJAX timeout
            success: function(result) {
            //si todo ha ido bien cambiamos la key con el nuevo valor del check
                $("#tblEstructura").fancytree("getTree").getNodeByKey(key).key = key.substring(0,key.length-1) + (($(chk).is(':checked'))? 1 :0);
            },
            error: function(ex, dragtus) {
                try { 
                    var reg = /\\n/g; 
                    alert(Utilidades.unescape($.parseJSON(ex.responseText).Message).replace(reg,"\n")); 
                }catch (e) { alert("Ocurrió un error al cargar la estructura.", e.name + ": " + e.message); }
            }
    });
    ocultarProcesando();
}
function grabarVinEstCC(nodeKey, idCC){
    mostrarProcesando();
    $.ajax({
            url: "Default.aspx/GrabarVinEstCC",   // Current Page, Method
            cache: false,
            data: JSON.stringify({nodeKey: nodeKey, idCC: idCC}),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content    
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 30000,    // AJAX timeout
            success: function(result) {
                var newNode = {title: currentOtherNode.title, key: currentOtherNode.key};
                if (getTabla(currentNode) == "tblEstructura")
                    currentNode.addChildren(newNode);
                else
                    currentNode.getParent().addChildren(newNode);
                currentNode.setExpanded();
                currentOtherNode.remove();
            },
            error: function(ex, status) {
                try { 
                    var reg = /\\n/g; 
                    alert(Utilidades.unescape($.parseJSON(ex.responseText).Message).replace(reg,"\n")); 
                }catch (e) { alert("Ocurrió un error al cargar la estructura.", e.name + ": " + e.message); }
            }
    });
    ocultarProcesando();
}

/*Fin funciones de grabación*/

/*Funciones drag and dropg */

function dragEnter(node, data){
    var sTipo = node.key.split("@#sep#@")[0];
    if (sTipo == "ND" || sTipo == "SN") //si el nodo destino es no representativo o si es un centro de coste no dejar insertar
        return ["over"]; //sino si se inserta dentro de un elemento y el nivel es nodo o subnodo entonces que deje draggear.
    if (sTipo == "CC")
        return ["before", "after"]
    else return false;
}


function fndragDrop(node,data){
    currentNode = node;
    currentOtherNode = data.otherNode;
    if (data.hitMode != "over") //cogemos el nodo en función de si es drag over o dragbefore o dragafter
        currentNode = node.getParent();
    grabarVinEstCC(currentNode.key ,data.otherNode.key);
}

function fndragDropCC(node,data){
    currentNode = node;
    currentOtherNode = data.otherNode;
    grabarVinEstCC(node.key ,data.otherNode.key);
}

/*Fin funciones drag and dropg */


/* Funciones para las lupas*/
/* Valores necesario para las funciones de buscarDescripcion + buscarSiguiente */
var strCadena = "";
var bPrimeraBusqueda = 0;
/****************************/

function buscarDescripcion(){
	mostrarProcesando();
	bPrimeraBusqueda = 0;
	$I("imgLupa1").style.display = "none";
//	var ret = window.showModalDialog(strServer +"Capa_Presentacion/buscarString.aspx", self, sSize(280, 110));
//  window.focus();

	modalDialog.Show(strServer + "Capa_Presentacion/buscarString.aspx", self, sSize(280, 110))
            .then(function(ret) {
	            if (ret != null) {
	                buscarTitle(ret);
	                strCadena = ret;
	            }
	            ocultarProcesando();
            });	
}
function buscarSiguiente(){
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

function buscarTitle(sBus){
    var aKey = getObjects(SOURCEEST, 'title', sBus); // Returns an array of matching objects
    if (aKey.length>0){ // si hay coincidencias
        var tree= $("#tblEstructura").fancytree("getTree");
        if (bPrimeraBusqueda == aKey.length)
            bPrimeraBusqueda = 0;
        var aParents = obtenerChildren(tree.getNodeByKey(aKey[bPrimeraBusqueda].key));
        while (aParents.length != 0) //expandimos todos los nodos desde la raiz hasta el nodo que deseamos.
            tree.getNodeByKey(aParents.pop()).setExpanded();

        tree.activateKey(aKey[bPrimeraBusqueda].key);
        //tree.getNodeByKey(aKey[bPrimeraBusqueda].key).scrollIntoView(false);
        $I("imgLupa1").style.display = "";
        $I("divCatalogoE").scrollTop = obtenerScroll(tree.getNodeByKey(aKey[bPrimeraBusqueda].key).tr) * 20;
    }
}

function obtenerChildren(node) {
    var aParents= [];
    if (node.parent == null) return aParents;
    else{ 
        aParents.push(node.key);
        aParents = aParents.concat(obtenerChildren(node.parent));
    }    
    return aParents;
}

/*Fin Funciones para las lupas*/

/*carga inicial*/
function CargarEstr(){
    mostrarProcesando();
    $.ajax({
            url: "Default.aspx/CargaInicialEstructura",   // Current Page, Method
            cache: false,
            data: JSON.stringify({ inactivos : (($("#chkInact").is(':checked'))? 1:0) }),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content    
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 30000,    // AJAX timeout
            success: function(result) {
               SOURCEEST = eval(result.d);
               initTree();
               var tree = $('#tblEstructura').fancytree('getTree');
               tree.reload(SOURCEEST);
               nNE = 1;
               colorearNE();
               ocultarProcesando();
            },
            error: function(ex, status) {
                try { 
                    var reg = /\\n/g; 
                    mostrarErrorAplicacion(Utilidades.unescape($.parseJSON(ex.responseText).Message).replace(reg,"\n"));
                    //alert(Utilidades.unescape($.parseJSON(ex.responseText).Message).replace(reg,"\n")); 
                }catch (e) { alert("Ocurrió un error al cargar la estructura.", e.name + ": " + e.message); }
            }
        })
}

function CargarCC(){
 mostrarProcesando();
 $.ajax({
            url: "Default.aspx/CargaInicialCentrosCoste",   // Current Page, Method
            cache: false,
            data: JSON.stringify({ }),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content    
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 30000,    // AJAX timeout
            success: function(result) {
               SOURCECC = eval(result.d);
               initCC();
               ocultarProcesando();
            },
            error: function(ex, status) {
                try { 
                    var reg = /\\n/g; 
                    mostrarErrorAplicacion(Utilidades.unescape($.parseJSON(ex.responseText).Message).replace(reg,"\n"));
                }catch (e) { alert("Ocurrió un error al cargar la estructura.", e.name + ": " + e.message); }
            }
        })
}
    function initTree(){
        $("#tblEstructura").fancytree({
            icons:false,
            source: SOURCEEST,
            autoActivate: true,
            extensions: ["dnd", "table"],
            dnd: {
              preventVoidMoves: true,
              preventRecursiveMoves: true,
              autoExpandMS: 400,
              dragStart: function(node, data) {
                    var sTipo = node.key.split("@#sep#@")[0];
                    if (sTipo=="CC")//sólo permite arrastrar los centros de coste
                        return true;
                    else
                        return false;
              },
              dragEnter: function(node, data) {
                  return dragEnter(node, data);
              },
              dragDrop: function(node, data) {
                fndragDrop(node,data);
              }
            },
            table: {
              indentation: 20,
              nodeColumnIdx: 0
            },
            renderColumns: function(event, data) {
              var node = data.node;
              $tdList = $(node.tr).find(">td");
              // (index #1 is rendered by fancytree title)
              var aKey = node.key.split("@#sep#@");
              insertarImagen($tdList.eq(0)[0].children[0], aKey[0]);
              if(aKey[0] == "CC")
                $tdList.eq(1).html("<input type='checkbox' onclick=\"grabarCambioCheck(this,'"+node.key+"')\" "  + ((aKey[3]=="1")? "checked='true'":"") + ">");
            }
        });
        $("#tblEstructura").contextmenu({
          delegate: "span.fancytree-title",
          menu: [
              {title: "Expandir", cmd: "exdr", uiIcon: "ui-expandir"},
              {title: "Contraer", cmd: "cont", uiIcon: "ui-contraer"}
          ],
          beforeOpen: function(event, ui) {
            var node = $.ui.fancytree.getNode(ui.target);
    //                node.setFocus();
            node.setActive();
            if(node.children == null)
                return false;
          },
          select: function(event, ui) {
            var node = $.ui.fancytree.getNode(ui.target);
            switch(ui.cmd){
                case "exdr":
                    expandir(node);
                break;
                case "cont":
                    contraer(node);
                break;
            }
          }
        });
    }

function initCC(){
        $("#tblCentroCoste").fancytree({
            icons:false,
            source: SOURCECC,
            extensions: ["dnd", "table"],
            dnd: {
              preventVoidMoves: true,
              preventRecursiveMoves: true,
              autoExpandMS: 400,
              dragStart: function(node, data) {
              if (node.key == "-1")
                return false;
              else
                return true;
              },
              dragEnter: function(node, data) {
                if (getTabla(data.otherNode) == "tblCentroCoste")
                    return false;
                else
                    return true;    
              },
              dragDrop: function(node, data) {
                    fndragDropCC(node,data);
              }
            },
            table: {
              indentation: 20,
              nodeColumnIdx: 0
            },
            renderColumns: function(event, data) {
              var node = data.node,
                $tdList =$(node.tr).find(">td");
                if (node.key == "-1")
                    $tdList.eq(0).addClass("cursiva");
            }
          });
        }
$(function () {
    nNE = 1;
    colorearNE();
    var SOURCEEST;
    var SOURCECC;
    CargarEstr();
    CargarCC();
});

function insertarImagen(oControl, tipoKey){ //para colocar la imagen del nodo, subnodo, supernodo....
    var img = "";
    switch (tipoKey){
        case "SN1":
            img = "imgSN1";
        break;
        case "SN2":
            img = "imgSN2"; 
        break;
        case "SN3":
            img = "imgSN3";
        break;
        case "SN4":
            img = "imgSN4";
        break;
        case "ND":
            img = "imgNodo";
        break;
        case "NDNR":
            img = "imgNodo";
        break;
        case "SN":
            img = "imgSubNodo";
        break;
    }
    if (img != ""){
        var oSpan = oControl.children[0].outerHTML + "<img border='0' src='../../../Images/" + img + ".gif' />" + oControl.children[1].outerHTML;
        $(oControl).html(oSpan);
    }
}
/*fin carga inicial*/

function getTabla(node){
    return $(node.tr).parents().find("table")[0].id;
}


function obtenerScroll(oFila){
    var tabla= oFila.parentNode;
    var scroll = oFila.rowIndex
    for (var i=0; i<oFila.rowIndex; i++)
        if (tabla.children[i].style.display == "none") scroll--;
    return scroll-1;
}

var nNE = 1;
function setNE(nValor) {
    try {
        nNE = nValor;
        colorearNE();
        mostrarProcesando();
        setTimeout("setNE2()", 100);
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}
function setNE2() {
    try {
        var NodoRaiz = $("#tblEstructura").fancytree("getRootNode")
        if (NodoRaiz != null) {
            if (nNE == 1)
            {
                if (NodoRaiz.children == null) return;
                if (NodoRaiz.children[0].isExpanded()) contraer2(NodoRaiz);
                ocultarProcesando();
                return;
            }
            if (nNE == 2)
            {
                if (NodoRaiz.children == null) return;
                contraer(NodoRaiz);
                expandir2(NodoRaiz);
                ocultarProcesando();
                return;
            }
            else
            {
                nodes = NodoRaiz.getChildren();
                obtenerHijos(nodes[0]);
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}
function obtenerHijos(nodos)
{
    for (var i = 0; i < nodos.children.length; i++) {
        if (i+1 == nodos.children.length) ocultarProcesando();

        var node = nodos.children[i];
        if (node == null) break;
        if (node.key == "") continue;
        var aKey = node.key.split("@#sep#@");

        //if (aKey[1] == "CONSULTORIA PREVENTA (132)") {
        //    alert("hola");
        //}
        if (parseInt(aKey[2], 10) > (nNE-1)) contraer(node);

        if ((parseInt(aKey[2], 10) <= (nNE - 1)) ||
            (parseInt(aKey[2], 10) <= 6 && nNE == 6))
        {
            try {
                node.parent.setExpanded(true);
                node.setExpanded(true)
            } catch (e) {
                node.parent.setExpanded(true);
                node.setExpanded(true);
                //alert("Clave: " + node.key + "\n\nDenominación: " + aKey[1]); continue;
            };
        }
        if (node.children == null) continue;
        if (node.children.length > 0) obtenerHijos(node);
    }
}
function expandir2(node) {
    node.setExpanded(true);
    if (node.children == null) return;
    else {
        for (var i = 0; i < node.children.length; i++)
            node.children[i].setExpanded(true);
    }
}

function contraer2(node) {
    if (node.children == null) return;
    else {
        for (var i = 0; i < node.children.length; i++)
            node.children[i].setExpanded(false);
    }
}

function expandir(node) {
    node.setExpanded(true);
    if (node.children == null) return;
    else {
        for (var i = 0; i < node.children.length; i++)
            expandir(node.children[i]);
    }
}

function contraer(node) {
    node.setExpanded(false);
    if (node.children == null) return;
    else {
        for (var i = 0; i < node.children.length; i++)
            contraer(node.children[i]);
    }
}
function colorearNE() {
    try {
        switch (nNE) {
            case 1:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2off.gif";
                $I("imgNE3").src = "../../../images/imgNE3off.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                $I("imgNE5").src = "../../../images/imgNE5off.gif";
                $I("imgNE6").src = "../../../images/imgNE6off.gif";
                break;
            case 2:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3off.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                $I("imgNE5").src = "../../../images/imgNE5off.gif";
                $I("imgNE6").src = "../../../images/imgNE6off.gif";
                break;
            case 3:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                $I("imgNE5").src = "../../../images/imgNE5off.gif";
                $I("imgNE6").src = "../../../images/imgNE6off.gif";
                break;
            case 4:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../images/imgNE4on.gif";
                $I("imgNE5").src = "../../../images/imgNE5off.gif";
                $I("imgNE6").src = "../../../images/imgNE6off.gif";
                break;
            case 5:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../images/imgNE4on.gif";
                $I("imgNE5").src = "../../../images/imgNE5on.gif";
                $I("imgNE6").src = "../../../images/imgNE6off.gif";
                break;
            case 6:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../images/imgNE4on.gif";
                $I("imgNE5").src = "../../../images/imgNE5on.gif";
                $I("imgNE6").src = "../../../images/imgNE6on.gif";
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer los colores del nivel de expansión", e.message);
    }
}
