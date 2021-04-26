
    var bLectura = false;

    function setNewSession(){
        try{
            window.frames["iFrmSession"].clearTimeout(window.frames["iFrmSession"].nIDTimeMin);
            window.frames["iFrmSession"].clearTimeout(window.frames["iFrmSession"].nIDTimeSeg);
            window.frames["iFrmSession"].nSession = intSession+1;
            window.frames["iFrmSession"].restaSession();
	    }catch(e){
		    mostrarErrorAplicacion("Error al actualizar la caducidad de sesión", e.message);
	    }
    }
    function sesionCaducada() {
        try {
            bCambios = false;
            window.frames["iFrmSession"].nSeg = 0;
            window.frames["iFrmSession"].restaSegundos();
        }
        catch (e) {
            mostrarErrorAplicacion("Error al actualizar la caducidad de sesión", e.message);
        }
    }

    var ModalDialog = function() {

        this.iframe = null;
        this.returnValue = null;
        //Modificación para navegar entre los diálogos
        this.array_dialogos = [];

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
                    //Modificación para navegar entre los diálogos
                    modalDialog.array_dialogos.push(id_dialog_body);
                },
                close: function() {
                    $(this).attr("src", "#"); //Está documentado que hay situaciones en las que la página se recarga al cerrar el diálogo. Así lo evitamos.
                    $(this).dialog('destroy').remove();
                    defer.resolve(parent.modalDialog.returnValue);
                    parent.modalDialog.returnValue = null;
                    //Modificación para navegar entre los diálogos
                    modalDialog.array_dialogos.pop();
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
    function navegador() {
        /*  Nombre a utilizar en la aplicación del navegador correspondiente
    
            opera   -- Opera 
            ie      -- Internet Explorer 
            safari  -- Safari 
            firefox -- FireFox 
            mozilla -- Mozilla 
            chrome  -- Chome
    
        */
        var ua = navigator.userAgent.toLowerCase();
        if (ua.indexOf("opera") != -1) {
            nName = "opera";
        }
        else if (ua.indexOf("msie") != -1) {
            nName = "ie";
        }
        else if (ua.indexOf("chrome") != -1) {
            nName = "chrome";
        }
        else if (ua.indexOf("safari") != -1) {
            nName = "safari";
        }
        else if (ua.indexOf("mozilla") != -1) {
            if (ua.indexOf("firefox") != -1) {
                nName = "firefox";
            } else {
                nName = "mozilla";
            }
        }

        nVer = navigator.appVersion;
        ie = (document.all) ? true : false;
    }

    navegador();