<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="MantAmbitosZonas" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<style type="text/css"> 
/* custom alignment (set by 'renderColumns' event) */ 
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
    padding: 0px 0px 1px 2px;
    margin: 0px;
    font-size: 11px;
    font-family: Arial, Helvetica, sans-serif;
    height: 12px;
}
.fancytree-edit-dirty  /* Caja de texto transparente */
{
    border: 0px;
    padding: 1px 0px 1px 2px;
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
    padding: 1px 0px 1px 2px;
    margin: 0px;
    font-size: 11px;
    background-color: Transparent;
    font-family: Arial, Helvetica, sans-serif;
    height: 14px;
    cursor:pointer;
    color:Fuchsia !important;
}
.alignVertical  /* Caja de texto transparente */
{
    vertical-align: bottom;   
}
.alignCenter  /* Caja de texto transparente */
{
    text-align: center
}
#tree tr {height:20px;}
</style> 

<script type="text/javascript">
    var arbol = eval("<%=sTreeView%>");
    var bOnline = <%=bOnline%>; 
    var strServer = "<%=Session["strServer"]%>";       
</script>
<center>
<br />
    <table id="tblCatIni" style="width: 540px; height: 17px;text-align:left;position:relative;left:-8px;">
        <colgroup> 
            <col width="0px"></col> 
            <col width="460px"></col> 
            <col width="80px"></col> 
        </colgroup> 
        <tr class="TBLINI">     
                <td></td>
                <td style="text-align:left;padding-left:5px">Denominación
                    <img style="display: none; CURSOR: pointer;" id="imgLupa1" onclick="buscarSiguiente()" height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                      
                    <img style="CURSOR: pointer;" onclick="buscarDescripcion()" height="11" src="../../../Images/imgLupa.gif" width="20" />
			    </td>                                 
                <td align="center">Activo</td> 
        </tr>
    </table>  
    <div id="divCatalogo" style="width: 556px; height:520px;overflow-y:auto;overflow-x:hidden;text-align:left;">
        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif');width: 540px">
                <table id="tree" style="text-align:left;border-collapse:collapse;table-layout:fixed;" cellpadding="0" cellspacing="0"> 
                <colgroup> 
                <col width="0px"></col> 
                <col width="460px"></col> 
                <col width="80px"></col> 
                </colgroup> 
                <thead> 
                <tr style="height:0px;">
                    <th></th>
                    <th></th>
                    <th></th> 
                </tr> 
                </thead> 
                <tbody> 
                </tbody> 
                </table> 
            </div>            
	</div>
    <table style="width: 540px; height: 17px;text-align:left;position:relative;left:-8px;">
        <tr class="TBLFIN">
            <td></td>
        </tr>
    </table>  
<br />
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


