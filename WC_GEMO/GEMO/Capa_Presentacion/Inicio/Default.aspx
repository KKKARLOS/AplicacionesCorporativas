<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
	//var oBody = document.getElementsByTagName("body")[0];
	//oBody.style.backgroundImage = "url(../../Images/imgFondoSuper.gif)";
	//oBody.style.backgroundRepeat= "no-repeat";
	//oBody.style.backgroundPosition= "left center";
	//oBody.style.backgroundAttachment= "fixed";	
	//document.write(codpar("ADM"))
-->
</script>
<%--<div id="divPestRetr" style="position:absolute; left:600px; top:96px; width:400px; height:180px; clip:'rect(auto auto 0 auto)'">
    <table style="width:100%; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
    <tr valign=top>
        <td>
            <table class="texto" style="width:100%; height:180px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                <tr>
		            <td background="../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../Images/Tabla/5.gif" style="padding: 5px" valign="top">
                        <!-- Inicio del contenido propio de la página -->
                        <table style="margin-top:10px;" cellpadding=0 cellspacing=0 border=0>
                        <colgroup>
                            <col style="width: 80%;" />
                            <col style="width: 20%;" />
                        </colgroup>
                        <tr>
                            <td valign=top class="texto" style="font-size: 10pt;"><%=Session["MSGBIENVENIDA"].ToString().Split(',')[0] %>,<br /><%=Session["MSGBIENVENIDA"].ToString().Split(',')[1] %></td>
                            <td align=Right rowspan="2">
                                <% if (Session["FOTOUSUARIO"] != null)
                                   { %>
                                <img id="imgFoto" src=ObtenerFoto.aspx style="border-style:groove; border-width:2px; border-color: Gray;">
                                <% 
                                    if ((bool)Session["MULTIUSUARIO"] && sUS == "0") { }
                                    else{
                                        Session["BIENVENIDAMOSTRADA"] = true;
                                        //Session["FOTOUSUARIO"] = null;
                                    }
                                   }
                                   else
                                   { %>
                                <img id="imgFoto" src="../../images/imgseparador.gif" style="width:1px;">
                                <% } %>
                            </td>
                        </tr>
                        <tr>
                        <td valign=bottom><My:Weather ID="Weather" runat="server" /></td>
                        </tr>
                        </table>
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td background="../../Images/Tabla/6.gif" width="6">
                        &nbsp;</td>
                </tr>
                <tr>
				    <td background="../../Images/Tabla/1.gif" height="6" width="6">
				    </td>
                    <td background="../../Images/Tabla/2.gif" height="6">
                    </td>
                    <td background="../../Images/Tabla/3.gif" height="6" width="6">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
</div>
--%></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

