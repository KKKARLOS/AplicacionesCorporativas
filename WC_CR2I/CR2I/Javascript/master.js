<!--
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