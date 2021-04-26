if (!window.JSON) $.ajax({ type: "GET", url: "../../../Javascript/json-2.4.js", dataType: "script" });
//if (!window.JSON) $.ajax({ type: "GET", url: "../../../Javascript/json2.js", dataType: "script" });
var contador = 0;
var form;
var texto = "";
//***Directiva ajax
$.ajaxSetup({ cache: false });

var CLIPBOARD = null;
var arrayDelete = [];
var arrayInsert = [];
var arrayUpdate = [];

var oNodo = new Object();
var sAccion = "";

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
        extensions: ["edit", "dnd", "table", "gridnav"],

        dnd: {
            preventVoidMoves: true,
            preventRecursiveMoves: true,
            autoExpandMS: 400,
            dragStart: function(node, data) {
                return true;
            },
            dragEnter: function(node, data) {
                // return ["before", "after"]; 
                return true;
            },
            dragDrop: function(node, data) {
                // controles

                var nivelNode = data.node.data.nivel;
                var nivelOtherNode = data.otherNode.data.nivel;
                if (data.hitMode == "over")
                    nivelNode++;
                if (nivelNode == nivelOtherNode) {
                    data.otherNode.moveTo(node, data.hitMode);
                    if (data.otherNode.data.bd == "") data.otherNode.data.bd = "U";
                    $(data.otherNode.span.children[2]).addClass("fancytree-color");
                    aG();

                    if (bOnline) grabar();
                }
                else return;
                //alert("Los nodos no están al mismo nivel");
            }
        },
        edit: {
            beforeClose: function(event, data) {

                // Return false to prevent cancel/save (data.input is available)
            },
            beforeEdit: function(event, data) {
                var reg = /\|/g;
                data.node.title = data.node.title.replace(reg, "'");
                texto = data.node.title;
                // Return false to prevent edit mode
            },
            close: function(event, data) {

                // Editor was removed
                if (texto != data.node.title) {
                    texto = data.node.title;
                    aG();
                    $(data.node.span.children[2]).addClass("fancytree-color");

                    if (data.node.data.bd == "I") {
                        for (var i = 0; i < arrayInsert.length; i++) {
                            var obj = arrayInsert[i];
                            if (arrayInsert[i].key == data.node.key) {
                                arrayInsert[i].title = data.node.title;
                                break;
                            }
                        }
                    }
                    if (data.node.data.bd == "U") {
                        for (var i = 0; i < arrayUpdate.length; i++) {
                            var obj = arrayUpdate[i];
                            if (arrayUpdate[i].key == data.node.key) {
                                arrayUpdate[i].title = data.node.title;
                                break;
                            }
                        }
                    }

                    if (bOnline) grabar();
                }
            },
            edit: function(event, data) {
                data.input[0].style.width = "360px";
                // Editor was opened (available as data.input)
            },
            save: function(event, data) {
                if (data.input.val() == "") {
                    mmoff("War", "No se permiten valores en blanco", 230);
                    data.input.val(texto);
                    //data.node.editStart();
                    return false;
                }            
                // Save data.input.val() or return false to keep editor open
                //alert("save " + data.input.val());
                //                if (texto != data.input.val()) {
                //                    texto = data.input.val();           
            }
        },
        table: {
            indentation: 20,
            nodeColumnIdx: 1,
            checkboxColumnIdx: 0
        },
        gridnav: {
            autofocusInput: false,
            handleCursorKeys: true
        },

        //lazyLoad: function(event, data) { 
        //  data.result = {url: "../demo/ajax-sub2.json"};
        //}, 

        renderColumns: function(event, data) {
            var node = data.node,
            $tdList = $(node.tr).find(">td");
            // (index #0 is rendered by fancytree by adding the checkbox) 
            //$tdList.eq(0).text(node.getIndexHier()).addClass("alignRight");
            // (index #2 is rendered by fancytree)
            // $tdList.eq(3).text(node.key);
            //$tdList.eq(2).text(node.data.codext);
            //if (node.key != "0")
            //    $tdList.eq(2).html("<input type='input' class='txtL' style='width:70px' tipo='codext' id='" + node.key + "' value='" + node.data.codext + "'>");
            $tdList.eq(1).addClass("alignVertical");

            var reg = /\|/g;
            $tdList.eq(1)[0].children[0].children[2].innerText = $tdList.eq(1)[0].innerText.replace(reg, "'");
        }
    }).on("nodeCommand", function(event, data) {
        // Custom event handler that is triggered by keydown-handler and
        // context menu: 

        var refNode, moveMode,
        tree = $(this).fancytree("getTree"),
        node = tree.getActiveNode();

        switch (data.cmd) {
            case "moveUp":
                node.moveTo(node.getPrevSibling(), "before");
                node.setActive();
                break;
            case "moveDown":
                node.moveTo(node.getNextSibling(), "after");
                node.setActive();
                break;
            case "indent":
                refNode = node.getPrevSibling();
                node.moveTo(refNode, "child");
                refNode.setExpanded();
                node.setActive();
                break;
            case "outdent":
                node.moveTo(node.getParent(), "after");
                node.setActive();
                break;
            case "rename":
                if (node.data.bd == "") node.data.bd = "U";
                node.editStart();
                //node.title = "<span style='background-color:red'>" + node.title + "</span>";
                break;
            case "remove":
                var sElemento = (node.data.nivel == "1") ? "sector" : "segmento";
                jqConfirm("", "Se procederá al borrado del " + sElemento + " seleccionado.<br><br>¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
                    if (answer) {
                        var o = new Object();
                        o.key = node.key;
                        o.nivel = node.data.nivel;
                        o.title = node.title;
                        o.bd = "D";

                        var bExiste = false;

                        for (var j = 0; j < arrayInsert.length; j++) {
                            var obj = arrayInsert[j];
                            if (arrayInsert[j].key == node.key) {
                                arrayInsert[j].key = "";
                                node.key = "";
                                bExiste = true;
                                break;
                            }
                        }

                        if (bExiste == false) {
                            for (var j = 0; j < arrayUpdate.length; j++) {
                                var obj = arrayUpdate[j];
                                if (arrayUpdate[j].key == node.key) {
                                    arrayUpdate[j].key = "";
                                    node.key = "";
                                    bExiste = true;
                                    break;
                                }
                            }
                        }

                        if (bExiste == false) arrayDelete.push(o);
                        aG(0);
                        if (node.children != null) {
                            if (node.children.length > 0) UpdBDNodosHijos(node.children);
                        }
                        sAccion = "D";
                        oNodo = node;
                        //node.remove();

                        if (bOnline) grabar();
                    }
                });
                break;

            case "addChild":
                if (parseInt(node.data.nivel, 10) == 2) return;


                refNode = node.addChildren({
                    title: "Nuevo nodo",
                    isNew: true
                });
                if (parseInt(node.data.nivel, 10) == 0) refNode.folder = true;

                node.setExpanded();
                contador--;
                refNode.key = contador;
                refNode.data.bd = "I";
                refNode.data.nivel = parseInt(node.data.nivel, 10) + 1;
                refNode.data.parentIni = "";
                refNode.data.codext = "";
                refNode.editStart();
                break;
            case "addSibling":
                if (parseInt(node.data.nivel, 10) == 2) return;
                refNode = node.getParent().addChildren({
                    title: "Nuevo nodo",
                    isNew: true
                }, node.getNextSibling());

                if (parseInt(node.data.nivel, 10) == 0) refNode.folder = true;
                contador--;
                refNode.key = contador;
                refNode.data.bd = "I";
                refNode.data.nivel = parseInt(node.data.nivel, 10) + 1;
                refNode.data.parentIni = "";
                refNode.data.codext = "";
                refNode.editStart();
                break;
            case "cut":
                CLIPBOARD = { mode: data.cmd, data: node };
                break;
            case "copy":
                CLIPBOARD = {
                    mode: data.cmd,
                    data: node.toDict(function(n) {
                        delete n.key;
                    })
                };
                break;
            case "clear":
                CLIPBOARD = null;
                break;
            case "paste":
                if (CLIPBOARD.mode === "cut") {
                    var nivelNode = node.data.nivel;
                    var nivelOtherNode = CLIPBOARD.data.data.nivel;
                    //if (data.hitMode == "over")
                    nivelNode++;
                    if (nivelNode == nivelOtherNode) {
                        CLIPBOARD.data.data.bd = "U";
                        CLIPBOARD.data.data.parentIni = (node.key == null) ? "" : node.key;
                        CLIPBOARD.data.codext = "";
                        CLIPBOARD.data.moveTo(node, "child");
                        CLIPBOARD.data.setActive();
                        $(CLIPBOARD.data.span.children[2]).addClass("fancytree-color");
                        CLIPBOARD = null;
                        aG();
                        if (bOnline) grabar();

                        //                    if (CLIPBOARD.data.children != null) {
                        //                        if (CLIPBOARD.data.children.length > 0) UpdBDNodosHijos(CLIPBOARD.data.children);
                        //                    }
                    }
                    else return;

                } else if (CLIPBOARD.mode === "copy") {
                    var nivelNode = node.data.nivel;
                    var nivelOtherNode = CLIPBOARD.data.data.nivel;
                    //if (data.hitMode == "over")
                    nivelNode++;
                    if (nivelNode == nivelOtherNode) {
                        CLIPBOARD.data.data.bd = "I";
                        contador--;
                        CLIPBOARD.data.key = contador;
                        CLIPBOARD.data.title = "Copia de " + CLIPBOARD.data.title;
                        CLIPBOARD.data.codext = "";

                        if (CLIPBOARD.data.children != null) {
                            if (CLIPBOARD.data.children.length > 0) AsigIdVirtualNodosHijos(CLIPBOARD.data.children, CLIPBOARD.data.key);
                        }
                        node.addChildren(CLIPBOARD.data).setActive();
                        var node = $("#tree").fancytree("getActiveNode");
                        $(node.span.children[2]).addClass("fancytree-color");
                        aG();
                        CLIPBOARD = null;
                        if (bOnline) grabar();
                    }
                    else return;
                }

                break;
            default:
                alert("Unhandled command: " + data.cmd);
                return;
        }

    }).on("keydown", function(e) {
        var c = String.fromCharCode(e.which),
      cmd = null;

        if (c === "N" && e.ctrlKey && e.shiftKey) {
            cmd = "addChild";
        } else if (c === "C" && e.ctrlKey) {
            cmd = "copy";
        } else if (c === "V" && e.ctrlKey) {
            cmd = "paste";
        } else if (c === "X" && e.ctrlKey) {
            cmd = "cut";
        } else if (c === "N" && e.ctrlKey) {
            cmd = "addSibling";
        } /*else if (e.which === $.ui.keyCode.DELETE) {
        cmd = "remove";
    }*/
        else if (e.which === $.ui.keyCode.F2) {
            cmd = "rename";
        } else if (e.which === $.ui.keyCode.UP && e.ctrlKey) {
            cmd = "moveUp";
        } else if (e.which === $.ui.keyCode.DOWN && e.ctrlKey) {
            cmd = "moveDown";
        } else if (e.which === $.ui.keyCode.RIGHT && e.ctrlKey) {
            cmd = "indent";
        } else if (e.which === $.ui.keyCode.LEFT && e.ctrlKey) {
            cmd = "outdent";
        }
        if (cmd) {
            $(this).trigger("nodeCommand", { cmd: cmd });
            return false;
        }
        //});
    }).on("dblclick", function(e) {
        var c = String.fromCharCode(e.which),
    cmd = "rename";
        $(this).trigger("nodeCommand", { cmd: cmd });
        return false;
    });

    /* 
    Context menu (https://github.com/mar10/jquery-ui-contextmenu) 
    */
    $("#tree").contextmenu({
        delegate: null, //"span.fancytree-title",
        menu: [
      { title: "Editar", cmd: "rename", uiIcon: "ui-icon-pencil" },
      { title: "Borrar", cmd: "remove", uiIcon: "ui-icon-trash" },
      { title: "Nuevo nodo", cmd: "addChild", uiIcon: "ui-icon-plus" },
      { title: "Cortar", cmd: "cut", uiIcon: "ui-icon-scissors" },
      { title: "Copiar", cmd: "copy", uiIcon: "ui-icon-copy" },
      { title: "Pegar", cmd: "paste", uiIcon: "ui-icon-clipboard", disabled: true }
      ],
        beforeOpen: function(event, ui) {
            targetId = (ui.target).is(document) ? $("body") : (ui.target).uniqueId().attr("id");
            var node = $.ui.fancytree.getNode(ui.target);
            $(node.span).find("span.fancytree-title").focus();

            if (node.data.nivel == "0") {
                $("#tree").contextmenu("showEntry", "rename", false);
                $("#tree").contextmenu("showEntry", "remove", false);
                $("#tree").contextmenu("showEntry", "addChild", true);
                $("#tree").contextmenu("showEntry", "cut", false);
                $("#tree").contextmenu("showEntry", "copy", false);
                $("#tree").contextmenu("showEntry", "paste", true);
            }
            else if (node.data.nivel == "1") {
                $("#tree").contextmenu("showEntry", "rename", true);
                $("#tree").contextmenu("showEntry", "remove", true);
                $("#tree").contextmenu("showEntry", "addChild", true);
                $("#tree").contextmenu("showEntry", "cut", true);
                $("#tree").contextmenu("showEntry", "copy", true);
                $("#tree").contextmenu("showEntry", "paste", true);
            }
            else {
                $("#tree").contextmenu("showEntry", "rename", true);
                $("#tree").contextmenu("showEntry", "remove", true);
                $("#tree").contextmenu("showEntry", "addChild", false);
                $("#tree").contextmenu("showEntry", "cut", true);
                $("#tree").contextmenu("showEntry", "copy", true);
                $("#tree").contextmenu("showEntry", "paste", false);
            }  
                      
            $("#tree").contextmenu("enableEntry", "paste", !!CLIPBOARD);
            node.setActive();
        },
        select: function(event, ui) {
            var that = this;
            // delay the event, so the menu can close and the click event does
            // not interfere with the edit control

            setTimeout(function() {
                $(that).trigger("nodeCommand", { cmd: ui.cmd });
            }, 100);
        }
    });
});

$(function() {
    $("#btnExpandir").click(function(event) {
        $("#tree").fancytree("getRootNode").visit(function(node) {
            node.setExpanded(true);
        });
    });

    $("#btnContraer").click(function(event) {
        $("#tree").fancytree("getRootNode").visit(function(node) {
            node.setExpanded(false);
        });
    });

    var sCodExterno = "";
    /* Evento al hacer click sobre algun dato de la columna codexterno */

    $("#tree").delegate("input[tipo='codext']", "click", function(e) {
        var node = $.ui.fancytree.getNode(e),
	    $input = $(e.target);
        $input.context.className = "txtM";
        $input.focus();
        $input.select();
        sCodExterno = node.data.codext;
        e.stopPropagation();  // prevent fancytree activate for this row
    });

    $("#tree").delegate("input[tipo='codext']", "blur", function(e) {
        var node = $.ui.fancytree.getNode(e),
	    $input = $(e.target);
        if (sCodExterno != $input.val()) {
            $(node.span.children[2]).addClass("fancytree-color");
            aG();
            if (node.data.bd == "") node.data.bd = "U";
            node.data.codext = $input.val();
            sCodExterno = node.data.codext;
        }
        $input.context.className = "txtL";
        e.stopPropagation();  // prevent fancytree activate for this row
    });
    $("#tree").delegate("input[tipo='codext']", "focus", function(e) {
        var node = $.ui.fancytree.getNode(e)
        $input = e.target;
        $input.className = "txtM";
        $input.focus();
        $input.select();
        sCodExterno = $input.value;
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

        for (var i = 0; i < nodes[0].children.length; i++) {
            var node = nodes[0].children[i];
            if (node == null) break;
            if (node.key == "") continue;
            if (node.data.bd == "I") {
                var o = new Object();
                o.key = node.key;
                o.title = node.title;
                o.nivel = node.data.nivel;
                o.parentIni = node.data.parentIni;
                o.parent = node.parent.key;
                o.codext = node.data.codext;

                var bExiste = false;

                for (var j = 0; j < arrayInsert.length; j++) {
                    var obj = arrayInsert[j];
                    if (arrayInsert[j].key == node.key) {
                        arrayInsert[j].title = node.title;
                        arrayInsert[j].nivel = node.data.nivel;
                        arrayInsert[j].parentIni = node.data.parentIni;
                        arrayInsert[j].parent = node.parent.key;
                        arrayInsert[j].codext = node.data.codext;
                        bExiste = true;
                        break;
                    }
                }

                if (bExiste == false) arrayInsert.push(o);
                //node.data.bd = "";   
            }
            if (node.data.bd == "U") {
                var o = new Object();
                o.key = node.key;
                o.title = node.title;
                o.nivel = node.data.nivel;
                o.parentIni = node.data.parentIni;
                o.parent = node.parent.key;
                o.codext = node.data.codext;
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
        if (node.data.bd == "I") {
            var o = new Object();
            o.key = node.key;
            o.title = node.title;
            o.nivel = node.data.nivel;
            o.parentIni = node.data.parentIni;
            o.parent = node.parent.key;
            o.codext = node.data.codext;

            var bExiste = false;

            for (var z = 0; z < arrayInsert.length; z++) {
                var obj = arrayInsert[z];
                if (arrayInsert[z].key == node.key) {
                    arrayInsert[z].title = node.title;
                    arrayInsert[z].nivel = node.data.nivel;
                    arrayInsert[z].parentIni = node.data.parentIni;
                    arrayInsert[z].parent = node.parent.key;
                    arrayInsert[z].codext = node.data.codext;
                    bExiste = true;
                    break;
                }
            }

            if (bExiste == false) arrayInsert.push(o);
        }

        if (node.data.bd == "U") {
            var o = new Object();
            o.key = node.key;
            o.title = node.title;
            o.nivel = node.data.nivel;
            o.parentIni = node.data.parentIni;
            o.parent = node.parent.key;
            o.codext = node.data.codext;
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

    if (arrayDelete.length > 0) {
        for (var i = 0; i < arrayDelete.length; i++) {
            if (arrayDelete[i].key == "") continue;
            var obj = arrayDelete[i];

            //sb.Append("D##"); //0
            sb.Append(obj.key + "##"); //1
            sb.Append(obj.title + "##"); //2
            sb.Append(obj.nivel + "##"); //3                            }
            sb.Append("///");
            //alert("key: " + obj.key + "    title: " + obj.title);
        }
    }
    $I("hdnDelete").value = sb.ToString();
    sb = null;
    var sb = new StringBuilder;

    if (arrayInsert.length > 0) {
        for (var i = 0; i < arrayInsert.length; i++) {
            if (arrayInsert[i].key == "") continue;
            var obj = arrayInsert[i];
            //sb.Append("I##"); //0
            sb.Append(obj.key + "##"); //1
            sb.Append(obj.title + "##"); //2
            sb.Append(obj.nivel + "##"); //3
            sb.Append(obj.parent + "##"); //4
            //sb.Append(obj.codext + "##"); //5                
            sb.Append("///");
            //alert("key: " + obj.key + " title: " + obj.title + " parent:" + obj.parent + " parentIni:" + obj.parentIni);
        }
    }
    $I("hdnInsert").value = sb.ToString();
    sb = null;
    var sb = new StringBuilder;

    if (arrayUpdate.length > 0) {
        for (var i = 0; i < arrayUpdate.length; i++) {
            if (arrayUpdate[i].key == "") continue;
            var obj = arrayUpdate[i]
            //sb.Append("U##"); //0
            sb.Append(obj.key + "##"); //1
            sb.Append(obj.title + "##"); //2
            sb.Append(obj.nivel + "##"); //3
            sb.Append(obj.parent + "##"); //4
            //sb.Append(obj.codext + "##"); //5      
            sb.Append("///");
            //alert("key: " + obj.key + " title: " + obj.title + " parent:" + obj.parent + " parentIni:" + obj.parentIni);
        }
    }
    $I("hdnUpdate").value = sb.ToString();
    sb = null;

    if ($I("hdnDelete").value == "" && $I("hdnInsert").value == "" && $I("hdnUpdate").value == "") {
        desActivarGrabar();
        alert("No hay datos modificados")
        return;
    }

    mostrarProcesando();

    $.ajax({
        url: "Default.aspx/grabarAjax",
        data: JSON.stringify({ sDelete: $I("hdnDelete").value, sInsert: $I("hdnInsert").value, sUpdate: $I("hdnUpdate").value }),
        async: true,
        type: "POST", // data has to be POSTed
        contentType: "application/json; charset=utf-8", // posting JSON content    
        dataType: "json",  // type of data is JSON (must be upper case!)
        timeout: 10000,    // AJAX timeout. Se comentariza para debugar
        success: function(result) {
            var aResul = result.d.split("@#@");
            if (aResul[0] != "OK")        
            {
                ocultarProcesando();
                desActivarGrabar();
                arrayDelete = [];
                bCambios = false;

                if (aResul[2] == "547") //error de integridad referencial, no se ha podido eliminar//error de integridad referencial, no se ha podido eliminar
                    mmoff("inf", "No se ha podido realizar el borrado porque existen datos relacionados con el elemento.", 540);
                else
                    mmoff("Err", aResul[1], 540);                
                return;                
            }
            var sInsertados = aResul[1];
            var tree = $("#tree").fancytree("getTree");

            if (sInsertados != "") {
                var aSI = sInsertados.split("//");
                for (var i = 0; i < aSI.length; i++) {
                    aNodo = aSI[i].split("::");
                    var node = tree.getNodeByKey(parseInt(aNodo[0], 10));
                    if (node == null) continue;
                    //node.key = parseInt(aNodo[1], 10);
                    node.key = aNodo[1];
                    node.data.bd = "";
                }
            }
            if (sAccion == "D") {
                oNodo.remove();
                sAccion = "";
            }

            $("#tree").fancytree("getRootNode").sortChildren(null, true);

            //$("#tree").fancytree("getRootNode").visit(function(node) { node.setExpanded(false); });
            arrayDelete = [];
            arrayInsert = [];
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
            arrayDelete = [];
            desc = "No se puede eliminar el elemento al estar ya asignado.";
            mmoff("Inf", desc, 360);
            return;
        }          
    }
    mostrarError(msg + "\n\n" + desc);
}

function AsigIdVirtualNodosHijos(aNodosHijos, iParent) {
    if (aNodosHijos == null) return false;

    for (var j = 0; j < aNodosHijos.length; j++) {
        node = aNodosHijos[j];

        node.data.bd = "I";
        contador--;
        node.key = contador;
        node.title = "Copia de " + node.title;
        node.parent = iParent;
        node.data.codext = "";

        aNodosHijos[j] = node;
        if (node.children == null) {
            AsigIdVirtualNodosHijos(null, iParent);
            continue;
        }
        if (node.children.length > 0) AsigIdVirtualNodosHijos(node.children, iParent);
    }
    return true;
}
function UpdBDNodosHijos(aNodosHijos, iParent) {
    if (aNodosHijos == null) return false;

    for (var j = 0; j < aNodosHijos.length; j++) {
        node = aNodosHijos[j];

        for (var i = 0; i < arrayInsert.length-1; i++) {
            var obj = arrayInsert[i];
            if (arrayInsert[i].key == node.key) {
                arrayInsert[i].key = "";
                node.key = "";
                break;
            }
        }

        if (node.children == null) {
            UpdBDNodosHijos(null, iParent);
            continue;
        }
        if (node.children.length > 0) UpdBDNodosHijos(node.children, iParent);
    }
    return true;
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
