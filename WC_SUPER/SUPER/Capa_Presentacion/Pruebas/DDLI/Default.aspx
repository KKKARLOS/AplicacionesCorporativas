<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_DDImages_Default" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
	<style type="text/css">
	body{
		font-family: Trebuchet MS, Lucida Sans Unicode, Arial, sans-serif;	/* Font to use */
		background-color:#E2EBED;
	}
	#footer{
		height:30px;
		vertical-align:middle;
		text-align:right;
		clear:both;
		padding-right:3px;
		background-color:#317082;
		margin-top:2px;
		width:780px;
	}
	#footer form{
		margin:0px;
		margin-top:2px;
	}
	#dragDropContainer{	/* Main container for this script */
		width:780px;
		height:380px;
		border:1px solid #317082;
		background-color:#FFF;
		-moz-user-select:none;
	}
	#dragDropContainer ul{	/* General rules for all <ul> */
		margin-top:0px;
		margin-left:0px;
		margin-bottom:0px;
		padding:2px;
	}
	
	#dragDropContainer li,#dragContent li,li#indicateDestination{	/* Movable items, i.e. <LI> */
		list-style-type:none;
		height:18px;
		/*width: 20px;*/
		background-color:#EEE;
		border:1px solid #000;
		padding:2px;
		margin-bottom:2px;
		cursor:pointer;
		font-size:0.8em;
	}
	
	li#indicateDestination{	/* Box indicating where content will be dropped - i.e. the one you use if you don't use arrow */
		border:1px solid #317082;	
		background-color:#FFF;
	}

	
	/* LEFT COLUMN CSS */
	div#listOfItems{	/* Left column "Available students" */
		
		float:left;
		padding-left:10px;
		padding-right:10px;
		width: 180px;	/* IE 5.x */
	}
	#listOfItems ul{	/* Left(Sources) column <ul> */
		height:340px;	

	}
		
	div#listOfItems div{
		border:1px solid #999;		
	}
	div#listOfItems div ul{	/* Left column <ul> */
		/*margin-left:10px;	*//* Space at the left of list - the arrow will be positioned there */
	}
	#listOfItems div p{	/* Heading above left column */
		margin:0px;	
		font-weight:bold;
		padding-left:12px;
		background-color:#317082;	
		color:#FFF;
		margin-bottom:5px;
	}
	/* END LEFT COLUMN CSS */
	
	#dragDropContainer .mouseover{	/* Mouse over effect DIV box in right column */
		background-color:#E2EBED;
		border:1px solid #317082;
	}
	
	/* Start main container CSS */
	
	div#mainContainer{	/* Right column DIV */
		width:590px;
		float:left;	
	}
	#mainContainer div{	/* Parent <div> of small boxes */
		float:left;
		margin-right:10px;
		margin-bottom:10px;
		margin-top:0px;
		border:1px solid #999;

		/* CSS HACK */
		width: 172px;	/* IE 5.x */
		width/* */:/**/170px;	/* Other browsers */
		width: /**/170px;
				
	}
	#mainContainer div ul{
		margin-left:10px;
	}
	
	#mainContainer div p{	/* Heading above small boxes */
		margin:0px;
		padding:0px;
		padding-left:12px;
		font-weight:bold;
		background-color:#317082;	
		color:#FFF;	
		margin-bottom:5px;
	}
	
	#mainContainer ul{	/* Small box in right column ,i.e <ul> */
		width:152px;
		height:80px;	
		border:0px;	
		margin-bottom:0px;
		/*overflow:hidden;*/
	}
	
	#dragContent{	/* Drag container */
		position:absolute;
		width:150px;
		height:20px;
		display:none;
		margin:0px;
		padding:0px;
		z-index:2000;
	}

	#dragDropIndicator{	/* DIV for the small arrow */
		position:absolute;
		width:7px;
		height:10px;
		display:none;
		z-index:1000;
		margin:0px;
		padding:0px;
	}
	</style>
	
	<script type="text/javascript">
	/************************************************************************************************************
	(C) www.dhtmlgoodies.com, November 2005
	************************************************************************************************************/
	/* VARIABLES YOU COULD MODIFY */
	var boxSizeArray = [4,4,4,3,7];	// Array indicating how many items there is rooom for in the right column ULs
	
	var arrow_offsetX = -5;	// Offset X - position of small arrow
	var arrow_offsetY = 0;	// Offset Y - position of small arrow
	
	var arrow_offsetX_firefox = -6;	// Firefox - offset X small arrow
	var arrow_offsetY_firefox = -13; // Firefox - offset Y small arrow
	
	var verticalSpaceBetweenListItems = 3;	// Pixels space between one <li> and next	
											// Same value or higher as margin bottom in CSS for #dragDropContainer ul li,#dragContent li
	
											
	var indicateDestionationByUseOfArrow = true;	// Display arrow to indicate where object will be dropped(false = use rectangle)

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
	function initDrag(e)	// Mouse button is pressed down on a LI
	{
		if(document.all)e = event;
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
	    //C?digo para evitar que al arrastrar una figura, se arrastre la fila.
        if (!e) var e = window.event
	    e.cancelBubble = true;
	    if (e.stopPropagation) e.stopPropagation();
        /////////////////	
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
		var st = Math.max(document.body.scrollTop,document.documentElement.scrollTop);
		var sl = Math.max(document.body.scrollLeft,document.documentElement.scrollLeft);
		
		dragContentObj.style.left = e.clientX-10 + sl + 'px';
		dragContentObj.style.top = e.clientY-10 + st + 'px';
		if(mouseoverObj)mouseoverObj.className='';

		if (window.getSelection) window.getSelection().removeAllRanges();
		else if (document.selection && document.selection.empty) document.selection.empty();

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
						if(!indicateDestionationByUseOfArrow){
							if(y<tmpTop){
								destinationObj = subLi[liIndex];
								indicateDestinationBox.style.display='block';
								subLi[liIndex].parentNode.insertBefore(indicateDestinationBox,subLi[liIndex]);
								break;
							}
						}else{							
							if(y<tmpTop){
								destinationObj = subLi[liIndex];
								dragDropIndicator.style.top = tmpTop + tmpOffsetY - Math.round(dragDropIndicator.clientHeight/2) + 'px';
								dragDropIndicator.style.display='block';
								break;
							}	
						}					
					}
					
					if(!indicateDestionationByUseOfArrow){
						if(indicateDestinationBox.style.display=='none'){
							indicateDestinationBox.style.display='block';
							ulPositionArray[no]['obj'].appendChild(indicateDestinationBox);
						}
						
					}else{
						if(subLi.length>0 && dragDropIndicator.style.display=='none'){
							dragDropIndicator.style.top = getTopPos(subLi[subLi.length-1]) + subLi[subLi.length-1].offsetHeight + tmpOffsetY + 'px';
							dragDropIndicator.style.display='block';
						}
						if(subLi.length==0){
							dragDropIndicator.style.top = ul_topPos + arrow_offsetY + 'px'
							dragDropIndicator.style.display='block';
						}
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
		
		
		if(cloneSourceItems && (!destinationObj || (destinationObj && (destinationObj.id=='allItems' || destinationObj.parentNode.id=='allItems')))){
    	    if (contentToBeDragged.parentNode.id != "allItems")
	    		contentToBeDragged.parentNode.removeChild(contentToBeDragged);
		}else{	
			
			if(destinationObj){
				if(destinationObj.tagName=='UL'){
					destinationObj.appendChild(contentToBeDragged);
				}else{
					destinationObj.parentNode.insertBefore(contentToBeDragged,destinationObj);
				}
				mouseoverObj.className='';
				destinationObj = false;
				dragDropIndicator.style.display='none';
				if(indicateDestinationBox){
					indicateDestinationBox.style.display='none';
					document.body.appendChild(indicateDestinationBox);
				}
				contentToBeDragged = false;
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
	
	/* 
	Preparing data to be saved 
	*/
	function saveDragDropNodes()
	{
		var saveString = "";
		var uls = dragDropTopContainer.getElementsByTagName('UL');
		for(var no=1;no<uls.length;no++){	// LOoping through all <ul>
			var lis = uls[no].getElementsByTagName('LI');
			for(var no2=0;no2<lis.length;no2++){
				if(saveString.length>0)saveString = saveString + ";";
				saveString = saveString + uls[no].id + '|' + lis[no2].id;
			}	
		}		
		document.getElementById('saveContent').innerHTML = '<h1>Ready to save these nodes:</h1> ' + saveString.replace(/;/g,';<br>') + '<p>Format: ID of ul |(pipe) ID of li;(semicolon)</p><p>You can put these values into a hidden form fields, post it to the server and explode the submitted value there</p>';
	}
	
	function initDragDropScript()
	{
		dragContentObj = document.getElementById('dragContent');
		dragDropIndicator = document.getElementById('dragDropIndicator');
		dragDropTopContainer = document.getElementById('dragDropContainer');
		//document.documentElement.onselectstart = cancelEvent;;
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
			uls[no].style.height = itemHeight * boxSizeArray[no]  + 'px';
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
		}
		
		if(!indicateDestionationByUseOfArrow){
			indicateDestinationBox = document.createElement('LI');
			indicateDestinationBox.id = 'indicateDestination';
			indicateDestinationBox.style.display='none';
			document.body.appendChild(indicateDestinationBox);
		}
	}
	
	window.onload = initDragDropScript;
	ocultarProcesando();

	</script>
<div id="dragDropContainer">
	<div id="listOfItems">
		<div>
			<p>Figuras disponibles</p>		
		<ul id="allItems">
			<li id="node1"><img src="../../../Images/imgColaborador.gif" /></li>
			<li id="node2"><img src="../../../Images/imgJefeProyecto.gif" /></li>
			<li id="node3"><img src="../../../Images/imgSubjefeProyecto.gif" /></li>
			<li id="node4"><img src="../../../Images/imgBitacorico.gif" /></li>
			<li id="node5"><img src="../../../Images/imgSecretaria.gif" /></li>
			<li id="node6"><img src="../../../Images/imgInvitado.gif" /></li>
		</ul>
		</div>
	</div>	
	<div id="mainContainer">
		<!-- ONE <UL> for each "room" -->
		<div>
			<p>Team A</p>
			<ul id="box1">
				
			</ul>
		</div>
		<div>		
			<p>Team B</p>
			<ul id="box2"></ul>
		</div>
		<div>
			<p>Team C</p>
			<ul id="box3">
				
				
			</ul>
		</div>
		<div>
			<p>Team D</p>
			<ul id="box4"></ul>
		</div>
		<div>	
			<p>Team E</p>
			<ul id="box5">

			</ul>
		</div>
	</div>
</div>
<div id="footer">
	<form action="aPage.html" method="post"><input type="button" onclick="saveDragDropNodes()" value="Save"></form>
</div>
<ul id="dragContent"></ul>
<div id="dragDropIndicator"><img src="../../../images/plus.gif"></div>
<div id="saveContent" style="position:absolute; top: 100px; left: 800px;"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

