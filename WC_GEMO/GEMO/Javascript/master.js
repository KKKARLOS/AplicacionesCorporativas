<!--
var bLectura = false;

function setNewSession(){
    try{
        window.frames["iFrmSession"].clearTimeout(window.frames["iFrmSession"].nIDTimeMin);
        window.frames["iFrmSession"].clearTimeout(window.frames["iFrmSession"].nIDTimeSeg);
        window.frames["iFrmSession"].nSession = intSession+1;
        window.frames["iFrmSession"].restaSession();
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar la caducidad de sesi�n", e.message);
	}
}

function setMenu(){
    Menu_HoverDynamic = function(item) {
                        try{
                            var node = (item.tagName.toLowerCase() == "td") ?
                                item:
                                item.cells[0];
                            //node.innerText = node.style.zIndex +": "+ node.innerText;
                            var data = Menu_GetData(item);
                            if (!data) return;
                            var nodeTable = WebForm_GetElementByTagName(node, "table");
                            if (data.hoverClass) {
                                nodeTable.hoverClass = data.hoverClass ;
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
                            }else{
                                node.style.cursor = "pointer";
                                //Modificaci�n 10/12/2012 para que el click funcione en toda la fila, no solo en el texto.
                                if (node.tagName && node.tagName == "A" && node.href != "#"){
                                    node.onclick = function(e){ /* 04/03/2014: para que al simular click en toda la fila, no se dispare el window.onbeforeunload 2 veces */
                                            try{
                                                if (!e) e = window.event;
                                                e.cancelBubble = true;
                                                if (e.stopPropagation) e.stopPropagation();
                                            }catch(e){};
                                    }
                                    node.parentNode.onclick = function(e){
                                            try{
                                                if (!e) e = window.event;
                                                e.cancelBubble = true;
                                                if (e.stopPropagation) e.stopPropagation();
                                                location.href = node.href.replace("#","");  //Se le da acci�n a toda la celda
                                                node.href = "#";                            //Y se le quita al enlace, para que no haga dos.
                                            }catch(e){};
                                    }
                                }
                            }
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
                        if (
                            (node.innerText == "PGE" && bBloquearPGEByAcciones)
                            || (node.innerText == "PST" && bBloquearPSTByAcciones)
                            || (node.innerText == "IAP" && bBloquearIAPByAcciones)
                            ){
                                node.onclick = function(){
                                                if (typeof(mmoff) == "function") mmoff("War","M�dulo bloqueado debido a la existencia de acciones pendientes de importancia cr�tica.", 350, 5000);
                                                else alert("M�dulo bloqueado debido a la existencia de acciones de importancia cr�tica.");
                                                }
                                return;
                            }
                        
                        //__disappearAfter = data.disappearAfter;
                        //Menu_Expand(node, data.horizontalOffset, data.verticalOffset); 
                        if (node.onclick==null){
                            node.onclick = function(){Menu_Expand(node, data.horizontalOffset, data.verticalOffset)};
                        }
                    }catch(e){}
                }
      //Esta funci�n se ha sobreescrito porque si se modifican din�micamente los valores zIndex de los
      //objetos de una pantalla, el men� queda por debajo de los mismos. Ej: Cuadro de mando.
      PopOut_Show = function (panelId, hideScrollers, data) {
            var panel = WebForm_GetElementById(panelId);
            if (panel && panel.tagName.toLowerCase() == "div") {
                panel.style.visibility = "visible";
                panel.style.display = "inline";
                if (!panel.offset || hideScrollers) {
                    panel.scrollTop = 0;
                    panel.offset = 0;
                    var table = WebForm_GetElementByTagName(panel, "TABLE");
                    if (table) {
                        WebForm_SetElementY(table, 0);
                    }
                }
                PopOut_Position(panel, hideScrollers);
                var z = 1;
                var isIE = window.navigator && window.navigator.appName == "Microsoft Internet Explorer" && !window.opera;
                if (isIE && data) {
                    var childFrameId = panel.id + "_MenuIFrame";
                    var childFrame = WebForm_GetElementById(childFrameId);
                    var parent = panel.offsetParent;
                    if (!childFrame) {
                        childFrame = document.createElement("iframe");
                        childFrame.id = childFrameId;
                        childFrame.src = (data.iframeUrl ? data.iframeUrl : "about:blank");
                        childFrame.style.position = "absolute";
                        childFrame.style.display = "none";
                        childFrame.scrolling = "no";
                        childFrame.frameBorder = "0";
                        if (parent.tagName.toLowerCase() == "html") {
                            document.body.appendChild(childFrame);
                        }
                        else {
                            parent.appendChild(childFrame);
                        }
                    }
                    var pos = WebForm_GetElementPosition(panel);
                    var parentPos = WebForm_GetElementPosition(parent);
                    WebForm_SetElementX(childFrame, pos.x - parentPos.x);
                    WebForm_SetElementY(childFrame, pos.y - parentPos.y);
                    WebForm_SetElementWidth(childFrame, pos.width);
                    WebForm_SetElementHeight(childFrame, pos.height);
                    childFrame.style.display = "block";
                    if (panel.currentStyle && panel.currentStyle.zIndex && !isNaN(parseInt(panel.currentStyle.zIndex))) {
                        z = panel.currentStyle.zIndex;
                    }
                    else if (panel.style.zIndex) {
                        z = panel.style.zIndex;
                    }
                }
                //panel.style.zIndex = z;
                panel.className += " MenuIE8";
            }
        }              

    $I("nbrMenu").style.display = "";
}
   var ModalDialog = function() {

        this.iframe = null;
        this.returnValue = null;

        this.Show = function(url, arg, opt) {
            //alert("Entra en funci�n show");
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
                            'z-Index': 1000000});

            var aOpt = opt.split(";");
            var nWidthDialogo = 0;
            var nHeightDialogo = 0;
            
            for (var i=0; i<aOpt.length; i++)
            {
                var aOpciones = aOpt[i].split(":");
                switch (aOpciones[0].toLowerCase().replace(/\s/g, '')){
                    case "dialogwidth": nWidthDialogo = parseInt(aOpciones[1].replace("px","").replace(" ",""), 10) ; break;
                    case "dialogheight": nHeightDialogo = parseInt(aOpciones[1].replace("px","").replace(" ",""), 10) ; break;
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
                open: function(){
                    parent.modalDialog.returnValue = null;
                    $(this).css("width", "100%");
                },
                close: function() {
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
-->