﻿/************************************************************************************************************
(C) www.dhtmlgoodies.com, November 2005
************************************************************************************************************/
/* VARIABLES YOU COULD MODIFY */
var boxSizeArray = [1,1,1,1,1];	// Array indicating how many items there is rooom for in the right column ULs

var arrow_offsetX = -5;	// Offset X - position of small arrow
var arrow_offsetY = 0;	// Offset Y - position of small arrow

var arrow_offsetX_firefox = -6;	// Firefox - offset X small arrow
var arrow_offsetY_firefox = -13; // Firefox - offset Y small arrow

var verticalSpaceBetweenListItems = -1;	// Pixels space between one <li> and next	
										// Same value or higher as margin bottom in CSS for #dragDropContainer ul li,#dragContent li
										
//var indicateDestionationByUseOfArrow = true;	// Display arrow to indicate where object will be dropped(false = use rectangle)

var cloneSourceItems = true;	// Items picked from main container will be cloned(i.e. "copy" instead of "cut").	
var cloneAllowDuplicates = false;	// Allow multiple instances of an item inside a small box(example: drag Student 1 to team A twice


/* END VARIABLES YOU COULD MODIFY */

var dragDropTopContainer = false;
var dragTimer = -1;
var dragContentObj = false;
var contentToBeDragged = false;	// Reference to dragged <li>
var contentToBeDragged_src = false;	// Reference to parent of <li> before drag started
var contentToBeDragged_next = false; 	// Reference to next sibling of <li> to be dragged
var destinationObj = false;	// Reference to <UL> or <LI> where element is dropped.
var dragDropIndicator = false;	// Reference to small arrow indicating where items will be dropped
var ulPositionArray = new Array();
var mouseoverObj = false;	// Reference to highlighted DIV

var MSIE = navigator.userAgent.indexOf('MSIE')>=0?true:false;
var navigatorVersion = navigator.appVersion.replace(/.*?MSIE (\d\.\d).*/g,'$1')/1;

var indicateDestinationBox = false;
function getTopPos(inputObj)
{		
  var returnValue = inputObj.offsetTop;
  while((inputObj = inputObj.offsetParent) != null){
  	if(inputObj.tagName!='HTML')returnValue += inputObj.offsetTop;
  }
  return returnValue;
}

function getLeftPos(inputObj)
{
  var returnValue = inputObj.offsetLeft;
  while((inputObj = inputObj.offsetParent) != null){
  	if(inputObj.tagName!='HTML')returnValue += inputObj.offsetLeft;
  }
  return returnValue;
}
	
function cancelEvent()
{
	return false;
}
var objFilaActiva = null;
var strOrigenIcono = "";
function initDrag(e)	// Mouse button is pressed down on a LI
{
	if(document.all)e = event;
	if (sFiguraActiva != "R" && sFiguraActiva != "D") return;
	switch (event.srcElement.tagName){
	    case "LI": if (getOp(event.srcElement) != 100) return; break;
	    case "IMG": if (getOp(event.srcElement) != 100) return; break;
	}
	
	var st = Math.max(document.body.scrollTop,document.documentElement.scrollTop);
	var sl = Math.max(document.body.scrollLeft,document.documentElement.scrollLeft);
	
	dragTimer = 0;
	dragContentObj.style.left = e.clientX + sl + 'px';
	dragContentObj.style.top = e.clientY + st + 'px';
	contentToBeDragged = this;
	contentToBeDragged_src = this.parentNode;
	contentToBeDragged_next = false;
	if(this.nextSibling){
		contentToBeDragged_next = this.nextSibling;
		if(!this.tagName && contentToBeDragged_next.nextSibling)contentToBeDragged_next = contentToBeDragged_next.nextSibling;
	}
	timerDrag();
	//Código para evitar que al arrastrar una figura, se arrastre la fila.
    if (!e) var e = window.event
	e.cancelBubble = true;
	if (e.stopPropagation) e.stopPropagation();
    /////////////////	
    aGProf(2);
    try{
        if (contentToBeDragged.parentNode.id != "allItems"){
            strOrigenIcono = "";
            objFilaActiva = event.srcElement.parentNode.parentNode.parentNode.parentNode.parentNode;
        }else strOrigenIcono = "selector";
    }catch(e){}
	return false;
}

function timerDrag()
{
	if(dragTimer>=0 && dragTimer<10){
		dragTimer++;
		setTimeout('timerDrag()',10);
		return;
	}
	if(dragTimer==10){
		
		if(cloneSourceItems && contentToBeDragged.parentNode.id=='allItems'){
			newItem = contentToBeDragged.cloneNode(true);
			newItem.onmousedown = contentToBeDragged.onmousedown;
			newItem.style.width = "";
			contentToBeDragged = newItem;
		}
		dragContentObj.style.display='block';
		dragContentObj.appendChild(contentToBeDragged);
	}
}

function moveDragContent(e)
{
	if(dragTimer<10){
		if(contentToBeDragged){
			if(contentToBeDragged_next){
				contentToBeDragged_src.insertBefore(contentToBeDragged,contentToBeDragged_next);
			}else{
				contentToBeDragged_src.appendChild(contentToBeDragged);
			}	
		}
		return;
	}
	if(document.all)e = event;
	var st = 0;
	var sl = 5;
	
	dragContentObj.style.left = e.clientX + sl + 'px';
	dragContentObj.style.top = e.clientY + st + 'px';

	if(mouseoverObj)mouseoverObj.className='';
	destinationObj = false;
	dragDropIndicator.style.display='none';
	if(indicateDestinationBox)indicateDestinationBox.style.display='none';
	var x = e.clientX + sl;
	var y = e.clientY + st;
	var width = dragContentObj.offsetWidth;
	var height = dragContentObj.offsetHeight;
	
	var tmpOffsetX = arrow_offsetX;
	var tmpOffsetY = arrow_offsetY;
	if(!document.all){
		tmpOffsetX = arrow_offsetX_firefox;
		tmpOffsetY = arrow_offsetY_firefox;
	}

	for(var no=0;no<ulPositionArray.length;no++){
		var ul_leftPos = ulPositionArray[no]['left'];	
		var ul_topPos = ulPositionArray[no]['top'];	
		var ul_height = ulPositionArray[no]['height'];
		var ul_width = ulPositionArray[no]['width'];

		if((x+width) > ul_leftPos && x<(ul_leftPos + ul_width) && (y+height)> ul_topPos && y<(ul_topPos + ul_height)){
			var noExisting = ulPositionArray[no]['obj'].getElementsByTagName('LI').length;
			if(indicateDestinationBox && indicateDestinationBox.parentNode==ulPositionArray[no]['obj'])noExisting--;
			if(noExisting<boxSizeArray[no-1] || no==0){
				dragDropIndicator.style.left = ul_leftPos + tmpOffsetX + 'px';
				var subLi = ulPositionArray[no]['obj'].getElementsByTagName('LI');
				
				var clonedItemAllreadyAdded = false;
				if(cloneSourceItems && !cloneAllowDuplicates){
					for(var liIndex=0;liIndex<subLi.length;liIndex++){
						if(contentToBeDragged.id == subLi[liIndex].id)clonedItemAllreadyAdded = true;
					}
					if(clonedItemAllreadyAdded)continue;
				}
				
				for(var liIndex=0;liIndex<subLi.length;liIndex++){
					var tmpTop = getTopPos(subLi[liIndex]);
					if(y<tmpTop){
						destinationObj = subLi[liIndex];
						dragDropIndicator.style.top = tmpTop + tmpOffsetY - Math.round(dragDropIndicator.clientHeight/2) + 'px';
						dragDropIndicator.style.display='block';
						break;
					}	
				}
				
				if(subLi.length>0 && dragDropIndicator.style.display=='none'){
					dragDropIndicator.style.top = getTopPos(subLi[subLi.length-1]) + subLi[subLi.length-1].offsetHeight + tmpOffsetY + 'px';
					dragDropIndicator.style.display='block';
				}
				if(subLi.length==0){
					dragDropIndicator.style.top = ul_topPos + arrow_offsetY + 'px'
					dragDropIndicator.style.display='block';
				}
				
				if(!destinationObj)destinationObj = ulPositionArray[no]['obj'];
				mouseoverObj = ulPositionArray[no]['obj'].parentNode;
				mouseoverObj.className='mouseover';
				return;
			}
		}
	}
}

/* End dragging 
Put <LI> into a destination or back to where it came from.
*/	
function dragDropEnd(e)
{
	if(dragTimer==-1)return;
	if(dragTimer<10){
		dragTimer = -1;
		return;
	}
	dragTimer = -1;
	if(document.all)e = event;	
	var bEliminar = false;
	
	destinationObj = event.srcElement;
	//alert(event.srcElement.tagName);
	if (event.srcElement.tagName == "IMG" && event.srcElement.src.toLowerCase().indexOf("eliminar") != -1){
	    bEliminar = true;
	}else if (event.srcElement.tagName == "UL" || event.srcElement.tagName == "IMG" || event.srcElement.tagName == "LI"){
	    var subLi;
	    var oUL;
	    
	    switch (event.srcElement.tagName){
	        case "UL": oUL = event.srcElement; break;
	        case "LI": oUL = event.srcElement.parentNode; break;
	        case "IMG": 
	            oUL = event.srcElement.parentNode.parentNode; 
	            if (event.srcElement.src.indexOf("imgDelegado.gif") == -1
	                && event.srcElement.src.indexOf("imgColaborador.gif") == -1
	                && event.srcElement.src.indexOf("imgJefeProyecto.gif") == -1
	                && event.srcElement.src.indexOf("imgSubjefeProyecto.gif") == -1
	                && event.srcElement.src.indexOf("imgBitacorico.gif") == -1
	                && event.srcElement.src.indexOf("imgSecretaria.gif") == -1
	                && event.srcElement.src.indexOf("imgInvitado.gif") == -1){
                        bEliminar = true;
	                }
	            break;
	    }
        //var subLi = oUL.getElementsByTagName('LI'); 
        var subLi = new Array();
        if (oUL.tagName == "UL") subLi = oUL.getElementsByTagName('LI'); 
	    var clonedItemAllreadyAdded = false;
	    if(cloneSourceItems && !cloneAllowDuplicates){
		    for(var liIndex=0;liIndex<subLi.length;liIndex++){
			    if(contentToBeDragged.id == subLi[liIndex].id)clonedItemAllreadyAdded = true;
		    }
		    //if(clonedItemAllreadyAdded)continue;
	        if(clonedItemAllreadyAdded){
	            bEliminar = true;
	        }
            if (!clonedItemAllreadyAdded && !comprobarIncompatibilidades(contentToBeDragged, subLi)) bEliminar = true;
	    }
	}else{
        bEliminar = true;
	}
	
	if (bEliminar){
	    if (contentToBeDragged.parentNode.id != "allItems")
            contentToBeDragged.parentNode.removeChild(contentToBeDragged);
        contentToBeDragged = false;
        dragDropIndicator.style.display='none';
        destinationObj = false;
        mouseoverObj = false;
        if (strOrigenIcono != "selector"){
            fm(objFilaActiva);
            objFilaActiva = null;
        }
        return;
	}
	
	if(cloneSourceItems && (!destinationObj || (destinationObj && (destinationObj.id=='allItems' || destinationObj.parentNode.id=='allItems')))){
	    if (contentToBeDragged.parentNode.id != "allItems")
    		contentToBeDragged.parentNode.removeChild(contentToBeDragged);
	}else{	
		
		if(destinationObj){
		    //para quitar el literal al soltar la imagen.
		    contentToBeDragged.innerHTML = contentToBeDragged.innerHTML.substring(0, contentToBeDragged.innerHTML.indexOf(">")+1);
			oUL.appendChild(contentToBeDragged);
            fm(event.srcElement);
            aGProf(2);

			mouseoverObj.className='';
			destinationObj = false;
			dragDropIndicator.style.display='none';
			if(indicateDestinationBox){
				indicateDestinationBox.style.display='none';
				document.body.appendChild(indicateDestinationBox);
			}
			contentToBeDragged = false;
			
            aLI = oUL.getElementsByTagName('LI'); 
            //2º Reordena las figuras
            for (var i=0; i<aLI.length; i++){
                for (var x=0; x<aLI.length; x++){
                    if (aLI[i].id == aLI[x].id) continue;
                    if (aLI[i].value > aLI[x].value && i<x){
                        if (document.all) 
                            {oUL.children(i).swapNode(oUL.children(x));}
                        else{
      	                    var t = oUL.children(i).outerHTML;
                            oUL.children(i).outerHTML = oUL.children(x).outerHTML;
                            oUL.children(x).outerHTML = t;
                        }
                    
                        
                    }
                }
            }
			
			return;
		}		

		if(contentToBeDragged_next){
			contentToBeDragged_src.insertBefore(contentToBeDragged,contentToBeDragged_next);
		}else{
			contentToBeDragged_src.appendChild(contentToBeDragged);
		}
	}
	contentToBeDragged = false;
	dragDropIndicator.style.display='none';
	if(indicateDestinationBox){
		indicateDestinationBox.style.display='none';
		document.body.appendChild(indicateDestinationBox);
		
	}
	mouseoverObj = false;
}

function initDragDropScript()
{
	dragContentObj = document.getElementById('dragContent');
	dragDropIndicator = document.getElementById('dragDropIndicator');
	dragDropTopContainer = document.getElementById('dragDropContainer');
	//document.documentElement.onselectstart = cancelEvent;
	var listItems = dragDropTopContainer.getElementsByTagName('LI');	// Get array containing all <LI>
	var itemHeight = false;
	for(var no=0;no<listItems.length;no++){
		listItems[no].onmousedown = initDrag;
		listItems[no].onselectstart = cancelEvent;
		if(!itemHeight)itemHeight = listItems[no].offsetHeight;
		if(MSIE && navigatorVersion/1<6){
			listItems[no].style.cursor='pointer';
		}			
	}
	
	var mainContainer = document.getElementById('mainContainer');
	var uls = mainContainer.getElementsByTagName('UL');
	itemHeight = itemHeight + verticalSpaceBetweenListItems;
	for(var no=0;no<uls.length;no++){
		//uls[no].style.height = itemHeight * boxSizeArray[no]  + 'px';
		uls[no].style.height = itemHeight + 'px';
	}
	
	var leftContainer = document.getElementById('listOfItems');
	var itemBox = leftContainer.getElementsByTagName('UL')[0];
	
	document.documentElement.onmousemove = moveDragContent;	// Mouse move event - moving draggable div
	document.documentElement.onmouseup = dragDropEnd;	// Mouse move event - moving draggable div
	
	var ulArray = dragDropTopContainer.getElementsByTagName('UL');
	for(var no=0;no<ulArray.length;no++){
		ulPositionArray[no] = new Array();
		ulPositionArray[no]['left'] = getLeftPos(ulArray[no]);	
		ulPositionArray[no]['top'] = getTopPos(ulArray[no]);	
		ulPositionArray[no]['width'] = ulArray[no].offsetWidth;
		ulPositionArray[no]['height'] = ulArray[no].clientHeight;
		ulPositionArray[no]['obj'] = ulArray[no];
		if (no>0){
		    ulArray[no].onmouseover = function (){this.className='mouseover'}
		    ulArray[no].onmouseout = function (){this.className=''}
		}
	}
	
}


