function ContextMenu( element, menu, contextHandler ) {
	var THIS = this
	  , contextEnabled = true;
	
	THIS.clickTarget = null;
	THIS.target = document.getElementById( element );
	THIS.menu = document.getElementById( menu );
	
	if( THIS.target ) {
	    EventUtil.addHandler(THIS.target, 'contextmenu', function(e) {
	        if (contextEnabled) {
	            e = EventUtil.getEventObj(e);
	            e.target = EventUtil.getEventTarget(e);

	            var oTD = null;
	            var oControl = e.target;
	            while (oControl != document.body) {
	                if (oControl.tagName.toUpperCase() == "TD") {
	                    oTD = oControl;
	                    break;
	                }
	                oControl = oControl.parentNode;
	            }

                if (oTD != null) {
                if (!(typeof (oTD.getAttribute("comentario")) == "string")) return;
                sTablaContextMenu = oTD.parentNode.parentNode.parentNode.id;
                /*$I("cm_cut").style.display = "none";
                $I("cm_copy").style.display = "none";
                $I("cm_paste").style.display = "none";
                $I("cm_linea").style.display = "none";*/
                $I("cm_addcom").style.display = "none";
                $I("cm_updcom").style.display = "none";
                $I("cm_delcom").style.display = "none";

                switch (sTablaContextMenu) {
                    case "tblTituloMovil":
                        if (oTD.getAttribute("comentario") == "") {
                            $I("cm_addcom").style.display = "block";
                        } else {
                            $I("cm_updcom").style.display = "block";
                            $I("cm_delcom").style.display = "block";
                        }
                        break;
                    case "tblBodyMovil":
                        /*$I("cm_cut").style.display = "block";
                        $I("cm_copy").style.display = "block";
                        $I("cm_paste").style.display = "block";
                        $I("cm_linea").style.display = "block";*/
                        if (typeof (oTD.getAttribute("comentario")) == "string") {
                            if (oTD.getAttribute("comentario") == "") {
                                $I("cm_addcom").style.display = "block";
                            } else {
                                $I("cm_updcom").style.display = "block";
                                $I("cm_delcom").style.display = "block";
                            }
                        }
                        break;
	                }
	            }

	            //THIS.menu.style.top = e.clientY + "px";
	            //THIS.menu.style.left = e.clientX + "px";
	            THIS.menu.style.display = "block";

	            THIS.menu.parentNode.style.top = e.clientY + "px";
	            THIS.menu.parentNode.style.left = e.clientX + "px";
	            THIS.menu.parentNode.style.display = "block";

	            //THIS.clickTarget = e.target;
	            THIS.clickTarget = (e.target.tagName.toUpperCase() == "INPUT") ? e.target.parentNode : e.target;

	            if (typeof contextHandler == 'function') contextHandler(e);

	            EventUtil.preventDefault(e);
	        }
	    });
		
		EventUtil.addHandler( document.documentElement, 'click', function( e ) {
			if( contextEnabled ) {
			    THIS.menu.parentNode.style.display = 'none';
			    THIS.menu.style.display = 'none';
			}
		});
	}
	
//	THIS.EnableContextMenu = function( status ) {
//		contextEnabled = status == true ? true : false;
//	};
}