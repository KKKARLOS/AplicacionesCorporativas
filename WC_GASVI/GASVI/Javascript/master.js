var bLectura = false;
    
function setNewSession(){
    try{
        window.frames.iFrmSession.clearTimeout(window.frames.iFrmSession.nIDTimeMin);
        window.frames.iFrmSession.clearTimeout(window.frames.iFrmSession.nIDTimeSeg);
        window.frames.iFrmSession.nSession = intSession+1;
        window.frames.iFrmSession.restaSession();
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar la caducidad de sesión", e.message);
	}
}

function sesionCaducada()
{
    try
    {
        bCambios=false;
        window.frames["iFrmSession"].nSeg = 0;
        window.frames["iFrmSession"].restaSegundos();
	}
	catch(e)
	{
		mostrarErrorAplicacion("Error al actualizar la caducidad de sesión", e.message);
	}
}

function setMenu(){
    Menu_HoverDynamic = function(item) {
                        try{
                            var node = (item.tagName.toLowerCase() == "td") ?
                                item:
                                item.cells[0];
                            var data = Menu_GetData(item);
                            if (!data) return;
                            var nodeTable = WebForm_GetElementByTagName(node, "table");
                            if (data.hoverClass) {
                                nodeTable.hoverClass = data.hoverClass;
                                WebForm_AppendToClassName(nodeTable, data.hoverClass);
                            }
                            node = nodeTable.rows[0].cells[0].childNodes[0];
                            if (data.hoverHyperLinkClass) {
                                node.hoverHyperLinkClass = data.hoverHyperLinkClass;
                                WebForm_AppendToClassName(node, data.hoverHyperLinkClass);
                            }
                            if (data.disappearAfter >= 200) {
                                __disappearAfter = data.disappearAfter;
                            }
                            Menu_Expand(node, data.horizontalOffset, data.verticalOffset); 
                            //alert(node.outerHTML);
                            if(node.href.indexOf("#") != -1){
                                node.style.cursor = "default";
                                node.onclick = null;   
                            }else
                                node.style.cursor = "pointer";
                        }catch(e){}
                    }
    Menu_Expand = function (item, horizontalOffset, verticalOffset, hideScrollers) {
                    try{
                        Menu_ClearInterval();
                        var tr = item.parentNode.parentNode.parentNode.parentNode.parentNode;
                        var horizontal = true;
                        if (!tr.id) {
                            horizontal = false;
                            tr = tr.parentNode;
                        }
                        var child = Menu_FindSubMenu(item);
                        if (child) {
                            var data = Menu_GetData(item);
                            if (!data) {
                                return null;
                            }
                            child.rel = tr.id;
                            child.x = horizontalOffset;
                            child.y = verticalOffset;
                            if (horizontal) child.pos = "bottom";
                            PopOut_Show(child.id, hideScrollers, data);
                        }
                        Menu_SetRoot(item);
                        if (child) {
                            if (!document.body.__oldOnClick && document.body.onclick) {
                                document.body.__oldOnClick = document.body.onclick;
                            }
                            if (__rootMenuItem) {
                                //document.body.onclick = Menu_HideItems;
                            }
                        }
                        Menu_ResetSiblings(tr);
                        return child;
                    }catch(e){}
                }
    Menu_HoverStatic = function(item) {
                    try{
                        var node = Menu_HoverRoot(item);
                        var data = Menu_GetData(item);
                        if (!data) return;
                        node.style.cursor = "pointer";
                        //__disappearAfter = data.disappearAfter;
                        //Menu_Expand(node, data.horizontalOffset, data.verticalOffset); 
                        if (node.onclick==null)
                            node.onclick = function(){Menu_Expand(node, data.horizontalOffset, data.verticalOffset)};
                    }catch(e){}
                }
    var aTables = $I("nbrMenu").getElementsByTagName("TABLE");
    for (var i=0;i<aTables.length;i++){
        aTables[i].style.tableLayout = "auto";
    }
    aTables = null;
    $I("nbrMenu").style.display = "";
}
/*
window.document.onkeydown = function (){ 
	                            if (event.keyCode==13){
		                            event.keyCode=9;
	                            }
                            }  
*/

var ModalDialog = function() {

    this.iframe = null;
    this.returnValue = null;

    this.Show = function(url, arg, opt) {
        //alert("Entra en función show");
        url = url || ''; //URL of a dialog
        arg = arg || null; //arguments to a dialog
        opt = opt || 'dialogWidth:300px;dialogHeight:200px;dialogTop:90px;dialogLeft:150px;'; //options: dialogTop;dialogLeft;dialogWidth;dialogHeight or CSS styles
        var timestamp = new Date().getTime();
        var id_dialogo = "dialogo_" + timestamp;
        var id_dialog_close = "dialog_close_" + timestamp;
        var id_dialog_body = "dialog_body_" + timestamp;

        this.iframe = $("<iframe id='" + id_dialog_body + "' name='" + id_dialog_body + "' />");
        this.iframe.attr({ 'src': url,
            'returnValue': null,
            'dialogArguments': arg
        });
        this.iframe.css({ 'padding': 0,
            'margin': 0,
            'padding-bottom': 0,
            'z-Index': 1000000
        });

        var aOpt = opt.split(";");
        var nWidthDialogo = 0;
        var nHeightDialogo = 0;

        for (var i = 0; i < aOpt.length; i++) {
            var aOpciones = aOpt[i].split(":");
            switch (aOpciones[0].toLowerCase().replace(/\s/g, '')) {
                case "dialogwidth": nWidthDialogo = parseInt(aOpciones[1].replace("px", "").replace(" ", ""), 10); break;
                case "dialogheight": nHeightDialogo = parseInt(aOpciones[1].replace("px", "").replace(" ", ""), 10); break;
            }
        }

        var defer = $.Deferred();
        this.iframe.dialog({
            modal: true,
            width: nWidthDialogo,
            height: nHeightDialogo + 25,
            resizable: false,
            zIndex: 1000000,
            create: function(event, ui) {
                $(this).parents(".ui-dialog:first").attr({ 'id': id_dialogo });
                this.contentWindow.id_dialogo = id_dialogo;
                this.contentWindow.id_dialog_body = id_dialog_body;
            },
            open: function() {
                parent.modalDialog.returnValue = null;
                $(this).css("width", "100%");
            },
            close: function() {
                $(this).attr("src", "#"); //Está documentado que hay situaciones en las que la página se recarga al cerrar el diálogo. Así lo evitamos.
                $(this).dialog('destroy').remove();
                defer.resolve(parent.modalDialog.returnValue);
            }
        });

        return defer.promise()
    }

    this.Close = function(oWindow, retval) {
        this.returnValue = retval;
        this.iframe = $("#" + oWindow.name);
        this.iframe.dialog("close");
    }
};

var modalDialog = new ModalDialog();                            