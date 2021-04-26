<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="GridEditableFancy" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<style type="text/css"> 
/*custom alignment (set by 'renderColumns'' event)*/ 
td.alignRight { 
    text-align: right; 
} 
td input[type=input] { 
    width: 240px; 
} 
.fancytree-edit-input /* Caja de texto en modificación */
{
    border-top: solid 1px #848284;
    border-right: solid 1px #EEEEEE;
    border-bottom: solid 1px #EEEEEE;
    border-left: solid 1px #848284;
    padding: 0px 0px 2px 2px;
    margin: 0px;
    font-size: 11px;
    font-family: Arial, Helvetica, sans-serif;
    height: 12px;
}
.fancytree-edit-dirty  /* Caja de texto transparente */
{
    border: 0px;
    padding: 1px 0px 2px 2px;
    margin: 0px;
    font-size: 11px;
    background-color: Transparent;
    font-family: Arial, Helvetica, sans-serif;
    height: 14px;
    cursor:pointer;
}
.fancytree-color  /* Caja de texto transparente */
{
    border: 0px;
    padding: 1px 0px 2px 2px;
    margin: 0px;
    font-size: 11px;
    background-color: Transparent;
    font-family: Arial, Helvetica, sans-serif;
    height: 14px;
    cursor:pointer;
    color:Fuchsia !important;
}
</style> 

<script type="text/javascript">
    var arbol = eval("<%=sTreeView%>");
</script>
<center>
<br />

	<div id="treeData" style="width: 556px; height:490px;overflow-y:auto;overflow-x:hidden">
	    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif');width: 540px">
            <table id="tree" style="text-align:left;border-collapse:collapse;table-layout:fixed;"> 
            <colgroup> 
            <col width="0px"></col> 
            <col width="460px"></col> 
            <col width="80px"></col> 
            </colgroup> 
            <thead> 
            <tr class="TBLINI" style="height:20px;width:540px;">
                <th></th>
                <th style="text-align:left">Denominación</th>
                <th>Cod.Externo</th> 
            </tr> 
            </thead> 
            <tbody> 
            </tbody> 
            </table> 
        </div>            
	</div>
    <table style="width: 540px; height: 17px; position:relative;left:-10px">
        <tr class="TBLFIN">
            <td></td>
        </tr>
    </table>  
<br />
<table id="tblBotonera" style="width:780px; height:30px; margin-top:10px" border="0">
<colgroup>
    <col style="width: 130px;" />
    <col style="width: 130px;" />
    <col style="width: 130px;" />
    <col style="width: 130px;" />
    <col style="width: 130px;" />
    <col style="width: 130px;" />    
</colgroup>
    <tr style="vertical-align: top;">
    <td>      
    </td>   

	<td>
		<button id="btnToogle" type="button" class="btnH25W110" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgProcesar.gif" /><span>Expan/Contra</span>
		</button>	 	
	</td>
	<td>
		<button id="btnExpandir" type="button" class="btnH25W110" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgProcesar.gif" /><span>Expandir</span>
		</button>	 	
	</td>
	<td>    
		<button id="btnContraer" type="button" class="btnH25W110" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgProcesar.gif" /><span>Contraer</span>
		</button>		    
    </td> 
    <td>
		<button id="btnGrabar" type="button" class="btnH25W110" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgGrabar.gif" /><span>Grabar</span>
		</button>	    
    
    </td> 	
    <td>    
		<button id="btnCambios" type="button" class="btnH25W110" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgProcesar.gif" /><span>Cambios</span>
		</button>    
    </td> 

    </tr>     
</table>	
</center>	
<asp:TextBox ID="hdnDelete" runat="server" readonly="true" style="visibility:hidden"></asp:TextBox>
<asp:TextBox ID="hdnInsert" runat="server" readonly="true" style="visibility:hidden"></asp:TextBox>
<asp:TextBox ID="hdnUpdate" runat="server" readonly="true" style="visibility:hidden"></asp:TextBox>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

            switch (strBoton) {
                case "grabar":
                    {
                        bEnviar = false;
                        setTimeout("grabar()", 100);
                        break;
                    }
            }
        }

        var theform = document.forms[0];
        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) theform.submit();
    }

    function WebForm_CallbackComplete() {
        for (var i = 0; i < __pendingCallbacks.length; i++) {
            callbackObject = __pendingCallbacks[i];
            if (callbackObject && callbackObject.xmlRequest && (callbackObject.xmlRequest.readyState == 4)) {
                WebForm_ExecuteCallback(callbackObject);
                if (!__pendingCallbacks[i].async) {
                    __synchronousCallBackIndex = -1;
                }
                __pendingCallbacks[i] = null;
                var callbackFrameID = "__CALLBACKFRAME" + i;
                var xmlRequestFrame = document.getElementById(callbackFrameID);
                if (xmlRequestFrame) {
                    xmlRequestFrame.parentNode.removeChild(xmlRequestFrame);
                }
            }
        }
    }
-->
</script>
</asp:Content>


