/************************************************************************************************************
(C) www.dhtmlgoodies.com, November 2005
************************************************************************************************************/
/* VARIABLES YOU COULD MODIFY */
var boxSizeArrayV = [1, 1, 1, 1, 1]; // Array indicating how many items there is rooom for in the right column ULs

var arrow_offsetXV = -5; // Offset X - position of small arrow
var arrow_offsetYV = 0; // Offset Y - position of small arrow

var arrow_offsetXV_firefox = -6; // Firefox - offset X small arrow
var arrow_offsetYV_firefox = -13; // Firefox - offset Y small arrow

var verticalSpaceBetweenListItemsV = -1; // Pixels space between one <li> and next	
// Same value or higher as margin bottom in CSS for #dragDropContainer ul li,#dragContent li

var indicateDestionationByUseOfArrowV = true; // Display arrow to indicate where object will be dropped(false = use rectangle)

var cloneSourceItemsV = true; // Items picked from main container will be cloned(i.e. "copy" instead of "cut").	
var cloneAllowDuplicatesV = false; // Allow multiple instances of an item inside a small box(example: drag Student 1 to team A twice

var newItemV;

/* END VARIABLES YOU COULD MODIFY */

var dragDropTopContainerV = false;
var dragTimerV = -1;
var dragContentObjV = false;
var contentToBeDraggedV = false; // Reference to dragged <li>
var contentToBeDraggedV_src = false; // Reference to parent of <li> before drag started
var contentToBeDraggedV_next = false; 	// Reference to next sibling of <li> to be dragged
var destinationObjV = false; // Reference to <UL> or <LI> where element is dropped.
var dragDropIndicatorV = false; // Reference to small arrow indicating where items will be dropped
var ulPositionArrayV = new Array();
var mouseoverObjV = false; // Reference to highlighted DIV

var MSIEV = navigator.userAgent.indexOf('MSIEV') >= 0 ? true : false;
var navigatorVersionV = navigator.appVersion.replace(/.*?MSIEV (\d\.\d).*/g, '$1') / 1;

var indicateDestinationBoxV = false;
var sOrigen = "V";
var objFilaActivaV = null;
var strOrigenIconoV = "";

function getTopPosV(inputObj) {
    var returnValue = inputObj.offsetTop;
    while ((inputObj = inputObj.offsetParent) != null) {
        if (inputObj.tagName != 'HTML') returnValue += inputObj.offsetTop;
    }
    return returnValue;
}

function getLeftPos(inputObj) {
    var returnValue = inputObj.offsetLeft;
    while ((inputObj = inputObj.offsetParent) != null) {
        if (inputObj.tagName != 'HTML') returnValue += inputObj.offsetLeft;
    }
    return returnValue;
}

function cancelEventV() {
    return false;
}

function initDragV(e)	// Mouse button is pressed down on a LI
{
    if (!e) e = event;
    var oObject = e.srcElement ? e.srcElement : e.target;

    switch (oObject.tagName) {
        case "LI": if (getOp(oObject) != 100) return; break;
        case "IMG": if (getOp(oObject.parentNode) != 100) return; break;
    }
    sOrigen = "V";

    var st = Math.max(document.body.scrollTop, document.documentElement.scrollTop);
    var sl = Math.max(document.body.scrollLeft, document.documentElement.scrollLeft);

    dragTimerV = 0;

    if (ie) {
        dragContentObjV.style.left = e.clientX + sl + 'px';
        dragContentObjV.style.top = e.clientY + st + 'px';
    }
    else {
        dragContentObjV.style.left = e.pageX + sl + 'px';
        dragContentObjV.style.top = e.pageY + st + 'px';
    }
    if (oObject.tagName == "IMG") oObject = oObject.parentNode;

    contentToBeDraggedV = oObject;
    contentToBeDraggedV_src = oObject.parentNode;
    contentToBeDraggedV_next = false;

    if (cloneSourceItemsV && contentToBeDraggedV.parentNode.id == 'allItemsV') {
        newItemV = contentToBeDraggedV.cloneNode(true);
        if (newItemV.id == "DV" || newItemV.id == "CV" || newItemV.id == "JV" || newItemV.id == "MV" || newItemV.id == "BV" ||
            newItemV.id == "SV" || newItemV.id == "IV") {
            newItemV.style.width = "auto";
        }
        newItemV.onmousedown = contentToBeDraggedV.onmousedown;
        contentToBeDraggedV = newItemV;
        contentToBeDraggedV_src = newItemV.parentNode;
        contentToBeDraggedV_next = false;
    }
    if (this.nextSibling) {
        contentToBeDraggedV_next = this.nextSibling;
        if (!this.tagName && contentToBeDraggedV_next.nextSibling) contentToBeDraggedV_next = contentToBeDraggedV_next.nextSibling;
    }

    timerDragV();
    //Código para evitar que al arrastrar una figura, se arrastre la fila.
    if (!e) var e = window.event
    e.cancelBubble = true;
    if (e.stopPropagation) e.stopPropagation();
    /////////////////	
    aG(2);
    try {
        if (contentToBeDraggedV.parentNode.id != "allItemsV") {
            strOrigenIconoV = "";
            objFilaActivaV = oObject.parentNode.parentNode.parentNode.parentNode;
        } else strOrigenIconoV = "selector";
    } catch (e) { }
    return false;
}

function timerDragV() {
    if (dragTimerV >= 0 && dragTimerV < 10) {
        dragTimerV++;
        setTimeout('timerDragV()', 10);
        return;
    }
    if (dragTimerV == 10) {

//        if (cloneSourceItemsV && contentToBeDraggedV.parentNode.id == 'allItemsV') {
//            newItemV = contentToBeDraggedV.cloneNode(true);
//            newItemV.onmousedown = contentToBeDraggedV.onmousedown;
//            contentToBeDraggedV = newItemV;
//        }
        dragContentObjV.style.display = 'block';
        dragContentObjV.appendChild(contentToBeDraggedV);
    }
}
//function moveDragContentG(e) {
//    if (sOrigen == "R") moveDragContent(e);
//    else moveDragContentV(e);
//}

function moveDragContentV(e) {
    if (dragTimerV < 10) {
        if (contentToBeDraggedV) {
            if (contentToBeDraggedV_next) {
                contentToBeDraggedV_src.insertBefore(contentToBeDraggedV, contentToBeDraggedV_next);
            } else {
                contentToBeDraggedV_src.appendChild(contentToBeDraggedV);
            }
        }
        return;
    }
    if (ie) e = event;

    var st = 0;
    var sl = 5;

    dragContentObjV.style.left = e.clientX + sl + 'px';
    dragContentObjV.style.top = e.clientY + st + 'px';
    if (mouseoverObjV) mouseoverObjV.className = '';

    if (window.getSelection) window.getSelection().removeAllRanges();
    else if (document.selection && document.selection.empty) document.selection.empty();

    destinationObjV = false;
    dragDropIndicatorV.style.display = 'none';
    if (indicateDestinationBoxV) indicateDestinationBoxV.style.display = 'none';
    var x;
    var y;
    if (ie) {
        x = e.clientX + sl;
        y = e.clientY + st;
    }
    else {
        x = e.pageX + sl;
        y = e.pageY + st;
    }
    var width = dragContentObjV.offsetWidth;
    var height;
    if (dragContentObjV.offsetHeight != 'undefined') height = dragContentObjV.offsetHeight;
    else height = dragContentObjV.innerHeight;

    var tmpOffsetX = arrow_offsetXV;
    var tmpOffsetY = arrow_offsetYV;

    if (!document.all) {
        tmpOffsetX = arrow_offsetXV_firefox;
        tmpOffsetY = arrow_offsetYV_firefox;
    }

    for (var no = 0; no < ulPositionArrayV.length; no++) {
        var ul_leftPos = ulPositionArrayV[no]['left'];
        var ul_topPos = ulPositionArrayV[no]['top'];
        var ul_height = ulPositionArrayV[no]['height'];
        var ul_width = ulPositionArrayV[no]['width'];

        if ((x + width) > ul_leftPos && x < (ul_leftPos + ul_width) && (y + height) > ul_topPos && y < (ul_topPos + ul_height)) {
            var noExisting = ulPositionArrayV[no]['obj'].getElementsByTagName('LI').length;
            if (indicateDestinationBoxV && indicateDestinationBoxV.parentNode == ulPositionArrayV[no]['obj']) noExisting--;
            if (noExisting < boxSizeArrayV[no - 1] || no == 0) {
                dragDropIndicatorV.style.left = ul_leftPos + tmpOffsetX + 'px';
                var subLi = ulPositionArrayV[no]['obj'].getElementsByTagName('LI');

                var clonedItemAllreadyAdded = false;
                if (cloneSourceItemsV && !cloneAllowDuplicatesV) {
                    for (var liIndex = 0; liIndex < subLi.length; liIndex++) {
                        if (contentToBeDraggedV.id == subLi[liIndex].id) clonedItemAllreadyAdded = true;
                    }
                    if (clonedItemAllreadyAdded) continue;
                }

                for (var liIndex = 0; liIndex < subLi.length; liIndex++) {
                    var tmpTop = getTopPosV(subLi[liIndex]);
                    if (!indicateDestionationByUseOfArrowV) {
                        if (y < tmpTop) {
                            destinationObjV = subLi[liIndex];
                            indicateDestinationBoxV.style.display = 'block';
                            subLi[liIndex].parentNode.insertBefore(indicateDestinationBoxV, subLi[liIndex]);
                            break;
                        }
                    } else {
                        if (y < tmpTop) {
                            destinationObjV = subLi[liIndex];
                            dragDropIndicatorV.style.top = tmpTop + tmpOffsetY - Math.round(dragDropIndicatorV.clientHeight / 2) + 'px';
                            dragDropIndicatorV.style.display = 'block';
                            break;
                        }
                    }
                }

                if (!indicateDestionationByUseOfArrowV) {
                    if (indicateDestinationBox.style.display == 'none') {
                        indicateDestinationBox.style.display = 'block';
                        ulPositionArray[no]['obj'].appendChild(indicateDestinationBox);
                    }

                } else {
                    if (subLi.length > 0 && dragDropIndicatorV.style.display == 'none') {
                        if (subLi[subLi.length - 1].offsetHeight != 'undefined') dragDropIndicatorV.style.top = getTopPos(subLi[subLi.length - 1]) + subLi[subLi.length - 1].offsetHeight + tmpOffsetYV + 'px';
                        else dragDropIndicatorV.style.top = getTopPos(subLi[subLi.length - 1]) + subLi[subLi.length - 1].innerHeight + tmpOffsetYV + 'px';
                        dragDropIndicator.style.display = 'block';
                    }
                    if (subLi.length == 0) {
                        dragDropIndicatorV.style.top = ul_topPos + arrow_offsetYV + 'px'
                        dragDropIndicatorV.style.display = 'block';
                    }
                }

                if (!destinationObjV) destinationObjV = ulPositionArrayV[no]['obj'];
                mouseoverObjV = ulPositionArrayV[no]['obj'].parentNode;
                mouseoverObjV.className = 'mouseover';
                return;
            }
        }
    }
}

/* End dragging 
Put <LI> into a destination or back to where it came from.
*/
function dragDropEndV(e) {
    if (dragTimerV == -1) return;
    if (dragTimerV < 10) {
        dragTimerV = -1;
        return;
    }
    dragTimerV = -1;
    if (ie) e = event;
    var bEliminar = false;

    destinationObjV = e.srcElement ? e.srcElement : e.target;
    //alert(event.srcElement.tagName);
    if (destinationObjV.tagName == "UL" || destinationObjV.tagName == "IMG" || destinationObjV.tagName == "LI") {
        var subLi;
        var oUL;
        switch (destinationObjV.tagName) {
            case "UL": oUL = destinationObjV; break;
            case "LI": oUL = destinationObjV.parentNode; break;
            case "IMG":
                oUL = destinationObjV.parentNode.parentNode;
                if (destinationObjV.src.indexOf("imgDelegado.gif") == -1
	                && destinationObjV.src.indexOf("imgColaborador.gif") == -1
	                && destinationObjV.src.indexOf("imgJefeProyecto.gif") == -1
	                && destinationObjV.src.indexOf("imgSubjefeProyecto.gif") == -1
	                && destinationObjV.src.indexOf("imgBitacorico.gif") == -1
	                && destinationObjV.src.indexOf("imgSecretaria.gif") == -1
	                && destinationObjV.src.indexOf("imgInvitado.gif") == -1) {
                    bEliminar = true;
                }

                break;
        }
        //var subLi = oUL.getElementsByTagName('LI'); 
        var subLi = new Array();
        if (oUL.tagName == "UL") subLi = oUL.getElementsByTagName('LI');
        var clonedItemAllreadyAdded = false;
        if (cloneSourceItemsV && !cloneAllowDuplicatesV) {
            for (var liIndex = 0; liIndex < subLi.length; liIndex++) {
                if (contentToBeDraggedV.id == subLi[liIndex].id) clonedItemAllreadyAdded = true;
            }
            //if(clonedItemAllreadyAdded)continue;
            if (clonedItemAllreadyAdded) {
                bEliminar = true;
            }
            if (!clonedItemAllreadyAdded && !comprobarIncompatibilidadesV(contentToBeDraggedV, subLi)) bEliminar = true;
        }
    } else {
        bEliminar = true;
    }


    if (bEliminar) {
        if (contentToBeDraggedV.parentNode.id != "allItemsV")
            contentToBeDraggedV.parentNode.removeChild(contentToBeDraggedV);
        contentToBeDraggedV = false;
        dragDropIndicatorV.style.display = 'none';
        destinationObjV = false;
        mouseoverObjV = false;
        if (strOrigenIconoV != "selector") {
            fm_mn(objFilaActivaV);
            objFilaActivaV = null;
        }
        return;
    }

    if (cloneSourceItemsV && (!destinationObjV || (destinationObjV && (destinationObjV.id == 'allItemsV' || destinationObjV.parentNode.id == 'allItemsV')))) {
        if (contentToBeDraggedV.parentNode.id != "allItemsV")
            contentToBeDraggedV.parentNode.removeChild(contentToBeDraggedV);
    } else {

        if (destinationObjV) {
            //para quitar el literal al soltar la imagen.
            contentToBeDraggedV.innerHTML = contentToBeDraggedV.innerHTML.substring(0, contentToBeDraggedV.innerHTML.indexOf(">") + 1);
            oUL.appendChild(contentToBeDraggedV);
            aG(2);
            fm_mn(destinationObjV);

            mouseoverObjV.className = '';
            destinationObjV = false;
            dragDropIndicatorV.style.display = 'none';
            if (indicateDestinationBoxV) {
                indicateDestinationBoxV.style.display = 'none';
                document.body.appendChild(indicateDestinationBoxV);
            }
            contentToBeDraggedV = false;

            aLI = oUL.getElementsByTagName('LI');
            //2º Reordena las figuras
            for (var i = 0; i < aLI.length; i++) {
                for (var x = 0; x < aLI.length; x++) {
                    if (aLI[i].id == aLI[x].id) continue;
                    if (aLI[i].value > aLI[x].value && i < x) {
                        oUL.children(i).swapNode(oUL.children(x));
                    }
                }
            }
            initDragDropScriptV();
            return;
        }

        if (contentToBeDraggedV_next) {
            contentToBeDraggedV_src.insertBefore(contentToBeDraggedV, contentToBeDraggedV_next);
        } else {
            contentToBeDraggedV_src.appendChild(contentToBeDraggedV);
        }
    }
    contentToBeDraggedV = false;
    dragDropIndicatorV.style.display = 'none';
    if (indicateDestinationBoxV) {
        indicateDestinationBoxV.style.display = 'none';
        document.body.appendChild(indicateDestinationBoxV);

    }
    mouseoverObjV = false;
}

function initDragDropScriptV() {
    dragContentObjV = document.getElementById('dragContentV');
    dragDropIndicatorV = document.getElementById('dragDropIndicatorV');
    dragDropTopContainerV = document.getElementById('dragDropContainerV');
    //document.documentElement.onselectstart = cancelEventV;
    var listItems = dragDropTopContainerV.getElementsByTagName('LI'); // Get array containing all <LI>
    var itemHeight = false;
    for (var no = 0; no < listItems.length; no++) {
        listItems[no].attachEvent("onmousedown", initDragV);
        listItems[no].attachEvent("onselectstart", cancelEventV);
        if (!itemHeight) itemHeight = listItems[no].offsetHeight;
        if (MSIEV && navigatorVersionV / 1 < 6) {
            listItems[no].style.cursor = 'pointer';
        }
    }

    var mainContainer = document.getElementById('mainContainerV');
    var uls = mainContainer.getElementsByTagName('UL');
    itemHeight = itemHeight + verticalSpaceBetweenListItemsV;
    for (var no = 0; no < uls.length; no++) {
        //uls[no].style.height = itemHeight * boxSizeArrayV[no]  + 'px';
        if (itemHeight < 0) continue;
        uls[no].style.height = itemHeight + 'px';
    }

    var leftContainer = document.getElementById('listOfItemsV');
    var itemBox = leftContainer.getElementsByTagName('UL')[0];

    if (typeof document.attachEvent != 'undefined') {
        document.documentElement.attachEvent("onmousemove", moveDragContentG);
        document.documentElement.attachEvent("onmouseup", dragDropEndG);
    } else {
        document.addEventListener("mousemove", moveDragContentG, false);
        document.addEventListener("mouseup", dragDropEndG, false);
    }

    //var ulArray = dragDropTopContainerV.getElementsByTagName('UL');
    var ulArray = mainContainer.getElementsByTagName('UL');
    for (var no = 0; no < ulArray.length; no++) {
        ulPositionArrayV[no] = new Array();
        //ulPositionArrayV[no]['left'] = getLeftPos(ulArray[no]);
        //ulPositionArrayV[no]['top'] = getTopPosV(ulArray[no]);
        //ulPositionArrayV[no]['width'] = ulArray[no].offsetWidth;
        //ulPositionArrayV[no]['height'] = ulArray[no].clientHeight;
        ulPositionArrayV[no]['obj'] = ulArray[no];
        if (no >= 0) {
            ulArray[no].onmouseover = function() { this.className = 'mouseover' }
            ulArray[no].onmouseout = function() { this.className = '' }
        }
    }
    if (!indicateDestionationByUseOfArrowV) {
        indicateDestinationBoxV = document.createElement('LI');
        indicateDestinationBoxV.id = 'indicateDestination';
        indicateDestinationBoxV.style.display = 'none';
        document.body.appendChild(indicateDestinationBoxV);
    }
}


