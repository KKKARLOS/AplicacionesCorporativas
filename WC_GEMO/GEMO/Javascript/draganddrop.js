<!--
var nFilaDesde = -1;
var nFilaHasta = -1;

// evento pare evitar que se seleccionen los objetos
//document.onselectstart = function() { return false; };

/*--------- Inicio de funciones para reordenar filas en una tabla ------------*/
//tbody = document.getElementById('tbodyDatos'); 
//tbody.onmousedown = startDrag; 
function startDrag(e) 
{ 
    if (bLectura) return;   
    if (!e) e = event; 
    var row = e.srcElement ? e.srcElement : e.target; 
    while (row && row.nodeName != 'TR') row = row.parentNode; 
    if (!row) return false;             
    
    var tbody = this; 
    tbody.activeRow = row; 
    nFilaDesde = row.rowIndex;

    (typeof document.detachEvent != 'undefined') ? tbody.attachEvent('onmousemove', doDrag) : tbody.addEventListener('mousemove', doDrag, false);
    (typeof document.detachEvent != 'undefined') ? document.attachEvent('onmouseup', startDragMouseUp) : document.addEventListener('mouseup', startDragMouseUp, false);     
} 
function startDragMouseUp(e)
{
    var tbody = this; 
    tbody.activeRow.style.backgroundColor="";
    nFilaHasta = tbody.activeRow.rowIndex;

    if (typeof document.detachEvent != 'undefined') {
        tbody.detachEvent("onmousemove", doDrag);
        window.document.detachEvent("onmouseup", startDragMouseUp);
    } else {
        tbody.removeEventListener("mousemove", doDrag, false);
        window.document.removeEventListener("mouseup", startDragMouseUp, false);
    }
    
    //tbody.onmousemove = null; 
    //document.onmouseup = null; 
    
    try{
        //(ie)? tbody.activeRow.className = "FS" : tbody.activeRow.setAttribute("class","FS");
        tbody.activeRow.className = "FS";
    }catch(e){}
    
    tbody.activeRow = null; 
    try{
        if ($I("ctl00_CPHB_Botonera") != null || $I("tblGrabar") != null){
            if (nFilaDesde != nFilaHasta)
                activarGrabar();    
        }else{ //pantalla modal
            try{ activarGrabar(); }catch(e){}
        }    
    }catch(e){}
}
function doDrag(e) 
{ 
    if (!e) e = event; 
    var row = e.srcElement ? e.srcElement : e.target; 
    this.activeRow.style.backgroundColor="#4D6D90"; //"red";
    while (row && row.nodeName != 'TR') row = row.parentNode; 
    if (!row || row == this.activeRow) return false; 
    if (row.parentNode != this.activeRow.parentNode) return false; 
    
    var oTable = this.activeRow.parentNode;
    while (oTable.nodeName != 'TABLE') oTable = oTable.parentNode; 
    //alert("row:"+row.rowIndex+" active:"+this.activeRow.rowIndex);
    //oTable.moveRow(row.rowIndex, this.activeRow.rowIndex);
    oTable.moveRow(this.activeRow.rowIndex, row.rowIndex);
    //else moveRow(this.activeRow.parentNode,row.rowIndex,this.activeRow.rowIndex);
    
} 

function startDragIMG(e) 
{ 
    if (bLectura) return;   
    if (!e) e = event; 
    var row = e.srcElement ? e.srcElement : e.target; 
//    if (e.srcElement.nodeName != 'IMG') return;
//    if (e.srcElement.src.indexOf("imgMoveRow")==-1) return;    
    if (row.nodeName != 'IMG') return;
    if (row.src.indexOf("imgMoveRow")==-1) return;
    while (row && row.nodeName != 'TR') row = row.parentNode; 
    if (!row) return false;             
    
    var tbody = this; 
    tbody.activeRow = row; 
    nFilaDesde = row.rowIndex;
    tbody.onmousemove = doDrag; 
    document.onmouseup = function () 
    { 
        //document.body.style.cursor = "default";
        tbody.activeRow.style.backgroundColor="";
        tbody.onmousemove = null; 
        nFilaHasta = tbody.activeRow.rowIndex;
        document.onmouseup = null; 
        //fm(row);
        try{
            tbody.activeRow.className = "FS";
        }catch(e){}
        tbody.activeRow = null; 
        try{
            if ($I("ctl00_CPHB_Botonera") != null || $I("tblGrabar") != null){
                if (nFilaDesde != nFilaHasta)
                    activarGrabar();    
            }else{ //pantalla modal
                try{ activarGrabar(); }catch(e){}
            }    
        }catch(e){}
    } 
} 

/*--------- Fin de funciones para reordenar filas en una tabla ------------*/

    /*--------- Inicio de funciones para pasar filas de una tabla a otra ------------*/
    var TimerID = 0;
    var oEl     = null;
    var aEl     = new Array();
    var oTarget = null;
    var beginDrag = false;
    var oRow = null;
    var nOpcionDD = 1;
    		
    function killTimer()
    {
	    if (TimerID != 0 )
	    {
		    clearTimeout(TimerID);
		    TimerID = 0;
	    }
    }
    		
    function fnShowDragWindow(e) 
    {
	    var obj = document.getElementById("DW");
	    killTimer();

	    if (oEl == null)  
	    {
		    return;
	    }
	    var nf= aEl.length;
	    if (nf<=0)nf=1;
	    if (nf>10)nf=10;
        obj.style.top		= oEl.offsetTop + "px";
        obj.style.left		= oEl.offsetLeft + "px";
        
        var h = 0;
        
        if (oEl.offsetHeight!=null) h = (nf * oEl.offsetHeight - 3);
	    else                        h = (nf * oEl.innerHeight - 3);

        if (h<0) h=0;
        obj.style.height = h + "px";
        
        h = (oEl.offsetWidth - 3);
        if (h<0) h=0;
	    obj.style.width = h + "px";
    	
	    //(ie)? obj.innerText = oEl.innerText :  obj.textContent = oEl.textContent;
        obj.innerText = oEl.innerText;
        	     
	    obj.style.zIndex	= 999;

        if (typeof document.attachEvent!='undefined') {
	        window.document.attachEvent( "onmousemove"  , fnMove );
	        window.document.attachEvent( "onscroll"  , fnMove );
	        window.document.attachEvent( "onmousemove" , fnCheckState );
	        window.document.attachEvent( "onmouseup"    , fnReleaseAux );
	        //window.document.attachEvent( "onselectstart", fnSelect );

        }else {

	        window.document.addEventListener( "mousemove"  , fnMove, false );
	        window.document.addEventListener( "scroll"  , fnMove, false );
	        window.document.addEventListener( "mousemove" , fnCheckState, false );
	        window.document.addEventListener( "mouseup"    , fnReleaseAux, false );
	        //window.document.addEventListener( "selectstart", fnSelect, false );
	        //obj.addEventListener("drag", fnSelect, false);
        }	
    	

	    beginDrag = true;
    }
    function fnReleaseAux(e)
    {
        if (!e) e = event; 
        var oElement = e.srcElement ? e.srcElement : e.target; 
        //alert(oElement.tagName);
        if (oElement.getAttribute("target") == "true" && oTarget != oElement){
            setTarget(oElement);
        }
        if (beginDrag) fnRelease(e);    
    }

    function setTarget(obj)
    { 
        //alert("entra setTarget");
        if (typeof document.attachEvent != 'undefined') {
            if (obj.onclick == null) obj.attachEvent('onclick', setTargetMN2);
            obj.click();
        } else { 
            var evObj = document.createEvent('MouseEvents');
            evObj.initEvent( 'click', true, true );
            obj.dispatchEvent(evObj);
            obj.addEventListener('click', setTargetMN2, false);
        }    
    }
    function setTargetMN2(e)
    {
        if (!e) e = event; 
        var oElement = e.srcElement ? e.srcElement : e.target; 

	    if (oElement == null) return;

        if (oElement.getAttribute("target") == "true")	
	    {
            if (typeof document.attachEvent != 'undefined') {
                if (oElement.onmouseout == null) oElement.attachEvent('onmouseout', delTarget);
            } else {
                if (oElement.onmouseout == null) oElement.addEventListener('mouseout', delTarget, false);
            }
          	
	        //src.onmouseout = delTarget;
    	    
		    oTarget = oElement;
		    //oTarget.style.backgroundColor = "red";
		    //nOpcionDD = nOpcion;
    			
            if (oElement.getAttribute("caso")!=null) nOpcionDD=oElement.getAttribute("caso");
    	            		
		    if (oElement.getElementsByTagName("TABLE")[0] == undefined) ToTable = null; //la papelera no tiene tables.
		    else {
		        ToTable = oElement.getElementsByTagName("TABLE")[0];
		        }	
        	
	    }
	    else
	    {
		    oTarget = null;	
	    }	
    }

    function delTarget(){
        oTarget = null;
    }
    function fnCheckState(e)
    {
        // En caso de que siga el evento activo y ya no estemos arrastrando la fila
        if (beginDrag==false){
            if (typeof document.detachEvent!='undefined') {       				    
	            window.document.detachEvent( "onmousemove" , fnMove );
	            window.document.detachEvent( "onscroll" , fnMove );
	            window.document.detachEvent( "onmousemove" , fnCheckState );
	            window.document.detachEvent( "onmouseup" , fnRelease );
	            //window.document.detachEvent( "onselectstart", fnSelect );
            }else {	
	            window.document.removeEventListener( "mousemove" , fnMove, false );
	            window.document.removeEventListener( "scroll" , fnMove , false );
	            window.document.removeEventListener( "mousemove" , fnCheckState , false );
	            window.document.removeEventListener( "mouseup" , fnRelease, false );
	            //window.document.removeEventListener( "selectstart", fnSelect, false ); 
            }
            return;
        }
        if (!e) e = event; 
        if (nName == 'ie') 
        {
            if (event.button != 1) fnRelease();
        }
        else if (nName == 'mozilla' || nName == 'firefox' || nName == 'safari' || nName == 'chrome')
        {
            if (e.which != 1) fnRelease();
        } 
    }

    function fnSelect()
    {
	    return false;
    }


    function selecCrome()
    {
        if (nName == 'chrome') document.onselectstart = function() { return false; };
    }

    selecCrome();


    function fnMove(e)
    {
        if (beginDrag==false) return;
        if (!e) e = event;     
        try
        {    
            if (nName == 'chrome') document.onselectstart = function() { return false; };
            else
            {
                if (window.getSelection) window.getSelection().removeAllRanges();
                else if (document.selection && document.selection.empty) document.selection.empty();
            }
        }
        catch(e){};

        
        if (nName == 'ie') 
        {
            if (e.button != 1)
	            {
		            fnRelease();
		            return;
	            }
        }
        else if (nName == 'mozilla' || nName == 'firefox' || nName == 'safari' || nName == 'chrome')
        {
            if (e.which != 1)
	            {
		            fnRelease();
		            return;
	            }        
        }
        	
	    var obj = document.getElementById("DW");
        if (ie)
        {
	        obj.style.top       = (e.clientY + 5 + window.document.body.scrollTop) + "px";    
	        obj.style.left      = (e.clientX - (obj.offsetWidth / 2 ) + window.document.body.scrollLeft) + "px";  
        }
        else
        {
	        obj.style.top       = (e.pageY + 5 + window.document.body.scrollTop) + "px";  
	        obj.style.left      = (e.pageX - (obj.offsetWidth / 2 ) + window.document.body.scrollLeft) + "px";      
        }

	    obj.style.display	= "block";
    }

    function fnRelease(e)
    {
        if (beginDrag == false) return;
        
        if (!e) e = event; 
        var oElement = e.srcElement ? e.srcElement : e.target; 
        
        if (typeof document.detachEvent!='undefined') {       				    
	        window.document.detachEvent( "onmousemove" , fnMove );
	        window.document.detachEvent( "onscroll" , fnMove );
	        window.document.detachEvent( "onmousemove" , fnCheckState );
	        window.document.detachEvent( "onmouseup" , fnRelease );
	        //window.document.detachEvent( "onselectstart", fnSelect );
        }else {	
	        window.document.removeEventListener( "mousemove" , fnMove, false );
	        window.document.removeEventListener( "scroll" , fnMove , false );
	        window.document.removeEventListener( "mousemove" , fnCheckState , false );
	        window.document.removeEventListener( "mouseup" , fnRelease, false );
	        //window.document.removeEventListener( "selectstart", fnSelect, false ); 
        }
        
	    var obj = document.getElementById("DW");
	    var nIndiceInsert = null;
	    var oTable;
	    if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
	    {	
	        switch (oElement.tagName){
	            case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
	            case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
	        }
	        oTable = oTarget.getElementsByTagName("TABLE")[0];
	        for (var x=0; x<=aEl.length-1;x++){
	            oRow = aEl[x];
	            switch(oTarget.id){
		            case "imgPapelera":
		            case "ctl00_CPHC_imgPapelera":
		                if (nOpcionDD == 3){
		                    if (oRow.getAttribute("bd") == "I"){
		                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
		                    }    
		                    else mfa(oRow, "D");
		                }else if (nOpcionDD == 4){
		                    oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
		                }
			            break;
		            case "divCatalogo2":
		            case "ctl00_CPHC_divCatalogo2":
		                if (FromTable == null || ToTable == null) continue;
		                if (nOpcionDD == 1){
	                        //var oTable = oTarget.getElementsByTagName("TABLE")[0];
	                        var sw = 0;
	                        //Controlar que el elemento a insertar no existe en la tabla
	                        for (var i=0;i<oTable.rows.length;i++){
		                        if (oTable.rows[i].id == oRow.id){
			                        //alert("Persona ya incluida");
			                        sw = 1;
			                        break;
		                        }
	                        }
                        
                            if (sw == 0){
	                            //Cambio por indicación de victor 10/03/2008. Cuando drag&drop, se insertan donde se se han soltado.
	                            //Si queremos reordenar, poner flechas
	                            var NewRow;
	                            if (nIndiceInsert == null){
                                    nIndiceInsert = oTable.rows.length;
                                    NewRow = oTable.insertRow(nIndiceInsert);
                                }
	                            else {
	                                if (nIndiceInsert > oTable.rows.length) 
	                                    nIndiceInsert = oTable.rows.length;
	                                NewRow = oTable.insertRow(nIndiceInsert);
	                            }
	                            nIndiceInsert++;
	                            var oCloneNode	= oRow.cloneNode(true);
	                            //(ie)? oCloneNode.className = "" : oCloneNode.setAttribute("class","");
	                            oCloneNode.className = "";
	                            NewRow.swapNode(oCloneNode);
	                            oCloneNode.setAttribute("class","");
                        	    
	                            //Se marca la fila como insertada
	                            oCloneNode.insertCell(0);
	                            oCloneNode.cells[0].appendChild(oImgFI.cloneNode(false));
	                            mfa(oCloneNode, "I");
                            }
                        }else if (nOpcionDD == 2){
	                        //var oTable = oTarget.getElementsByTagName("TABLE")[0];
	                        var sw = 0;
	                        //Controlar que el elemento a insertar no existe en la tabla
	                        for (var i=0;i<oTable.rows.length;i++){
		                        if (oTable.rows[i].id == oRow.id){
			                        //alert("Persona ya incluida");
			                        sw = 1;
			                        break;
		                        }
	                        }
                            if (sw == 0){
	                            //Cambio por indicación de victor 10/03/2008. Cuando drag&drop, se insertan donde se se han soltado.
	                            //Si queremos reordenar, poner flechas
	                            var NewRow;
	                            if (nIndiceInsert == null){
                                    nIndiceInsert = oTable.rows.length;
                                    NewRow = oTable.insertRow(nIndiceInsert);
                                }
	                            else {
	                                if (nIndiceInsert > oTable.rows.length) 
	                                    nIndiceInsert = oTable.rows.length;
	                                NewRow = oTable.insertRow(nIndiceInsert);
	                            }
	                            nIndiceInsert++;
	                            var oCloneNode	= oRow.cloneNode(true);
	                            oCloneNode.className = "";
	                            NewRow.swapNode(oCloneNode);
	                            NewRow.setAttribute("class","MM");
                                
                                activarGrabar();
                            }
                        }
			            break;
			    }
		    }
            switch(oTarget.id){
	            case "imgPapelera":
	            case "ctl00_CPHC_imgPapelera":
	                if (nOpcionDD == 3){
	                    if (oRow.getAttribute("bd") == "I"){
                            var oElem = getNextElementSibling(oElement.parentNode);	   
                            actualizarLupas(oElem.getElementsByTagName("TABLE")[0].id, oElem.getElementsByTagName("TABLE")[1].id);
	                    }    
	                }else if (nOpcionDD == 4){
                            var oElem = getNextElementSibling(oElement.parentNode);	   
                            actualizarLupas(oElem.getElementsByTagName("TABLE")[0].id, oElem.getElementsByTagName("TABLE")[1].id);
	                }
		            break;
	            case "divCatalogo2":
	            case "ctl00_CPHC_divCatalogo2":
	                //actualizarLupas(event.srcElement.previousSibling.id, event.srcElement.children[0].children[0].id);	            
	                var oElem = getPreviousElementSibling(oElement);	 
	                actualizarLupas(oElem.id, oElement.children[0].children[0].id);
	                break;
		    }
	    }
	    oTable = null;
	    killTimer();
	    CancelDrag();
    	
	    obj.style.display	= "none";
	    oEl					= null;
	    aEl.length = 0;
	    oTarget				= null;
	    beginDrag			= false;
	    TimerID				= 0;
	    oRow                = null;
        FromTable           = null;
        ToTable             = null;
    }

    function CancelDrag()
    {
	    if (beginDrag == false)
	    {
		    killTimer();
	    }
    }

    var FromTable = null;
    var ToTable = null;
    var nFilaFromTable;
    	
    function DD(e) /*lo que hacía el BeginDrag*/
    {   
        if (!e) e = event; 
        if (nName == 'ie') 
        {
            if (e.button == 2) return;
        }
        else if (nName == 'mozilla' || nName == 'firefox' || nName == 'safari' || nName == 'chrome')
        {
            if (e.which == 3 || e.which == 2) return;
        }
               
	    //alert("bLectura: "+ bLectura);
        if (bLectura) return;

        //var oElement = (typeof e.srcElement!='undefined') ? e.srcElement : e.target;
        var oElement = e.srcElement ? e.srcElement : e.target;

	    if (oElement == null)
	    {
		    return;
	    }

        // SI PINCHO UNA IMAGEN NO QUIERO QUE ME ARRASTRE LA FILA ( SOLO PARA EL RESTO DE NAVEGADORES)
        if (!ie)
        {
            if (startDragIMG!=null)
            {
                switch (oElement.tagName) {
                case "LI": if (getOp(oElement) == 100) return; break;
                case "IMG": if (getOp(oElement.parentNode) == 100) return; break;
                }
            }
        }
        
        var bFila = false;
        while (!bFila)
        {
            if (oElement.tagName.toUpperCase()=="TR") bFila = true;
            else oElement = oElement.parentNode;
        }
        
        var oFila = oElement;	

	    oTarget = null;
	    if (oFila.tagName == "TR"){
	        if (nName == 'firefox' && !oFila.getAttribute("draggable")){
	            oFila.setAttribute("draggable", true);
	        }
		    FromTable = oFila.parentNode.parentNode;
		    nFilaFromTable = oFila.rowIndex;
	        oEl = oFila;
	        var oTabla = oFila.parentNode.parentNode;
	        aEl.length = 0;
	        for (var i=0; i<oTabla.rows.length; i++){
                if (oTabla.rows[i].className=="FS" || oTabla.rows[i]==oFila){
                    aEl[aEl.length] = oTabla.rows[i];
                }
	        }
	    }
	    // Set the window timeout.
	    TimerID = setTimeout(fnShowDragWindow, 1);
	    return false;
    }				
    /*--------- Fin de funciones para pasar filas de una tabla a otra ------------*/
