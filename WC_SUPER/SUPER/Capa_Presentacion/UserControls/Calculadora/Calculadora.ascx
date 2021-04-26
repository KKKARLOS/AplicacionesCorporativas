<%@ Control Language="C#" ClassName="Calculadora" %>
<script type="text/javascript" language="javascript">
	var oDestinoCalculadora = "";
	function getCalculadora(nLeft, nTop){
	    try{
	        if (typeof(window.frames.iFrameCalculadora) != "undefined"){
	            if (nTop != null && nLeft != null){
                    if (typeof(window.dialogArguments)=="undefined"){
                        $I("iFrameCalculadora").style.top = nTop;
                        $I("iFrameCalculadora").style.left = nLeft;
                    }else{
                        $I("iFrameCalculadora").style.top = nTop;
                        $I("iFrameCalculadora").style.left = nLeft;
                    }
	            } 
	        
	            window.frames.iFrameCalculadora.document.getElementById("imgTapaLlevar").style.display = (oDestinoCalculadora == "")? "block":"none";
	            window.frames.iFrameCalculadora.document.calculator.expr.value = "0";
    	        window.frames.iFrameCalculadora.show_calc();
    	        window.frames.iFrameCalculadora.focus();
    	        
                if (oDestinoCalculadora != ""){
    	            $I(oDestinoCalculadora).style.backgroundImage = "url(../../../Images/imgFondoCalc.gif)";
    	            $I(oDestinoCalculadora).style.backgroundRepeat= "no-repeat";
    	        }
    	    }
	    }catch(e){
		    mostrarErrorAplicacion("Error al mostrar la calculadora", e.message);
	    }
	}
	function ic(sID){
	    try{
	        //alert(oDestinoCalculadora);
	        oDestinoCalculadora = sID;
	    }catch(e){
		    mostrarErrorAplicacion("Error al establecer el destino de la calculadora", e.message);
	    }
	}

</script>
<iframe name="iFrameCalculadora" id="iFrameCalculadora" 
            scrolling="no" 
            src="<% =Session["strServer"].ToString() %>Capa_Presentacion/UserControls/Calculadora/Default.aspx" 
            marginwidth="0" frameborder="0" framespacing="0" border="10" onblur="window.frames.iFrameCalculadora.hide_calc()"
            style="visibility:hidden; 
                position:absolute; left:800px; top:60px; width:165px; height:250px;"></iframe>		        						           
<script type="text/javascript" language="javascript">
<!--
    try {
        setOp($I("iFrameCalculadora"), 0);
        if (typeof(window.dialogArguments)=="undefined"){
            $I("iFrameCalculadora").style.top = (document.body.clientHeight/2) - 125;
            $I("iFrameCalculadora").style.left = (document.body.clientWidth/2) - 95;
        }else{
            $I("iFrameCalculadora").style.top = (parseInt(window.dialogHeight, 10)/2) - 125;
            $I("iFrameCalculadora").style.left = (parseInt(window.dialogWidth, 10)/2) - 95;
        }
    }catch(e){}
-->
</script>

<script runat="server">

</script>
