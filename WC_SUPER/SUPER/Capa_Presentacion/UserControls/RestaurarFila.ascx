<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RestaurarFila.ascx.cs" Inherits="Capa_Presentacion_UserControls_RestaurarFila" %>
<div id="ie5menu" class="skin0" onmouseover="highlightie5(event)" onmouseout="lowlightie5(event)" style="display:none; width:100px;">
<div class="menuitems" style="width:100px;" onclick="restaurarFila();">No eliminar</div>
</div>
<script type="text/javascript">
	var menuobj=document.getElementById("ie5menu")
    var oFilaARestaurar;
    var bMostrarNE=false;

    function click(e) 
    {
        if (!e) e = event; 
        var oElement = (typeof e.srcElement!='undefined') ? e.srcElement : e.target;
        
        if (nName == 'ie') 
        {
            if (e.button == 2) 
            {
                bMostrarNE=false;
	            if (oElement.outerHTML.indexOf("imgFD.gif") == -1 || oElement.outerHTML.substring(0, 4) != "<IMG"){
                    var oControl = oElement;
                    while (oControl != document.body){
                        if (oControl.tagName.toUpperCase() == "TR"){
                            if (oControl.cells[0].children[0] != null && oControl.cells[0].children[0].tagName != null 
                                && oControl.cells[0].children[0].tagName.toUpperCase() == "IMG" 
                                && oControl.cells[0].children[0].outerHTML.indexOf("imgFD.gif") != -1){
                                oFilaARestaurar=oControl;
                                bMostrarNE=true;
                                if (oFilaARestaurar.className != "FS") oFilaARestaurar.className = "FS";
                                return;
                            }
                        }
                        oControl = oControl.parentNode;
                    }
	                
	                //try{ document.fireEvent("onmouseup"); }catch(e){}
	                
	                try {
	                    if (document.all) {
	                        document.fireEvent("onmouseup");
	                    } else {
	                        var changeEvent = document.createEvent("MouseEvent");
	                        changeEvent.initEvent("mouseup", false, true);
	                        currentCal.form[currentCal.field].dispatchEvent(changeEvent);
	                    }

	                } catch (e) { }
	                
    	            //alert(message + " de restaurarFila");
	                return;
	            }
	            else{
	                bMostrarNE=true;
	                oFilaARestaurar=oElement.parentNode.parentNode;
	            }
            }            
        }    
        else if (nName == 'firefox' || nName == 'safari' || nName == 'mozilla' || nName == 'chrome')
        {
            if (e.which == 3 || e.which == 2)
            {
                bMostrarNE=false;
	            if (oElement.outerHTML.indexOf("imgFD.gif") == -1 || oElement.outerHTML.substring(0, 4) != "<IMG"){
                    var oControl = oElement;
                    while (oControl != document.body){
                        if (oControl.tagName.toUpperCase() == "TR"){
                            if (oControl.cells[0].children[0] != null && oControl.cells[0].children[0].tagName != null 
                                && oControl.cells[0].children[0].tagName.toUpperCase() == "IMG" 
                                && oControl.cells[0].children[0].outerHTML.indexOf("imgFD.gif") != -1){
                                oFilaARestaurar=oControl;
                                bMostrarNE=true;
                                if (oFilaARestaurar.className != "FS") oFilaARestaurar.className = "FS";
                                return;
                            }
                        }
                        oControl = oControl.parentNode;
                    }
	                try{
                        var clickEvent = window.document.createEvent("MouseEvent"); 
                        clickEvent.initEvent("mouseup", false, true); 
                        document.dispatchEvent(clickEvent); 	                 
	                    }
	                catch(e){}
	                
    	            //alert(message + " de restaurarFila");
	                return;
	            }
	            else{
	                bMostrarNE=true;
	                oFilaARestaurar=oElement.parentNode.parentNode;
	            }
            }             
        }
    }
    
    /********** Funciones para restaurar filas marcadas para borrar ***********/
    function showmenuie5(e){
        if (!e) e = event; 
        var oElement = (typeof e.srcElement!='undefined') ? e.srcElement : e.target;

	    var rightedge = document.body.clientWidth-e.clientX;
	    var bottomedge= document.body.clientHeight-e.clientY;

	    if (rightedge<menuobj.offsetWidth)
		    menuobj.style.left= (document.body.scrollLeft+e.clientX-menuobj.offsetWidth) + "px";
	    else
		    menuobj.style.left= (document.body.scrollLeft+e.clientX) + "px";

	    if (bottomedge<menuobj.offsetHeight)
		    menuobj.style.top= (document.body.scrollTop+e.clientY-menuobj.offsetHeight) + "px";
	    else
		    menuobj.style.top= (document.body.scrollTop+e.clientY) + "px";

        if (bMostrarNE)
	        menuobj.style.visibility="visible";
	    return false
    }

    function hidemenuie5(e){
	    menuobj.style.visibility="hidden";
    }

    function highlightie5(e){
        if (!e) e = event; 
        var oElement = (typeof e.srcElement!='undefined') ? e.srcElement : e.target;

	    var firingobj= oElement;
	    if (firingobj.className=="menuitems"){
	        firingobj.style.backgroundColor="highlight";
	        firingobj.style.color="white";
	    }
    }

    function lowlightie5(e){
        if (!e) e = event; 
        var oElement = (typeof e.srcElement!='undefined') ? e.srcElement : e.target;
    
	    var firingobj= oElement;
	    if (firingobj.className=="menuitems"){
	        firingobj.style.backgroundColor="";
	        firingobj.style.color="black";
	    }
    }
    function restaurarFila(){
        try{
            var oTable;
            oTable = oFilaARestaurar.parentNode;
            for (var x=0; x<oTable.rows.length; x++){
                if (oTable.rows[x].getAttribute("bd") == "D" && oTable.rows[x].className == "FS"){
                    if (oTable.rows[x].cells[0].children[0] != null){
                        oTable.rows[x].cells[0].children[0].src = strServer+"images/imgFU.gif";
                    }
                    //oTable.rows[x].bd = "U";
                    oTable.rows[x].setAttribute("bd", "U");
                }
            }         
	    }catch(e){
		    mostrarErrorAplicacion("Error al restaurar la fila", e.message);
        }
    }
        
    if (typeof document.attachEvent != 'undefined') {
        window.document.attachEvent("onmousedown", click);
        window.document.attachEvent("oncontextmenu", showmenuie5);
        window.document.attachEvent("onclick", hidemenuie5);
    } else {
        window.document.addEventListener("mousedown", click, false);
        window.document.addEventListener("contextmenu", showmenuie5, false);
        window.document.addEventListener("click", hidemenuie5, false);
    }

	menuobj.style.display='';
</script>
