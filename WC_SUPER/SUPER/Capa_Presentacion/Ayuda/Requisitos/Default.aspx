<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Ayuda_Requisitos_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<br />
<center>
	<DIV id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 738px; height:570px">
        <div align="left" style="width: 700px">
        <table border="0" class="texto" width="700px" cellspacing="0" cellpadding="0">
          <tr>
            <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
          </tr>
          <tr>
            <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../Images/Tabla/5.gif" style="padding:15px">
            <!-- Inicio del contenido propio de la p�gina -->
            <br />
            <lu>
                <li><b>Navegadores soportados: </b>SUPER est� homologado para Internet Explorer 11, Chrome 56, Mozilla Firefox 52 y Safari 5.1.7. Cualquier otro navegador o versi�n diferente a la especificada, puede dar lugar a situaciones inesperadas.<br /><br /></li>
                <li><b>Exportaci�n a Excel:</b> Para poder generar y abrir el archivo Excel, adem�s de tener instalado Microsoft Excel en la m�quina local, en las pol�ticas de seguridad de su navegador debe configurar el par�metro "Inicializar y activar la secuencia de comandos de los controles de Active X no marcados como seguros", tanto en el apartado "Seguridad --> Intranet local" si accede desde la Intranet como en el "Seguridad --> Internet" si el acceso se produce desde la Extranet.<br /><br /></li>
                <li><b>Exportaci�n a PDF:</b> Para poder generar y abrir el fichero PDF, se requiere de tener instalado Adobe Reader en la m�quina local.<br /><br /></li>
                <li><b>Visualizaci�n de textos de ayuda:</b> Se requiere de tener instalado Adobe Reader en la m�quina local.<br /><br /></li>
                <li><b>Visualizaci�n de v�deos de ayuda:</b> Para la visualizaci�n de v�deos de ayuda, deber� tener instalada una herramienta reproductora de v�deos en formato WMF. Si dispone de Windows Media Player (est�ndar en Iberm�tica), en funci�n de la tarjeta de v�deo que tenga instalada en su ordenador, puede que los colores de los v�deos se vean distorsionados (en rojo generalmente). Para solucionar esto, deber� configurar la "aceleraci�n de v�deo" al m�nimo posible. Esto se hace desde el men� "Herramientas" (Tools si dispone de la versi�n inglesa) del Windows Media Player, opci�n "Rendimiento" (Performance si dispone de la versi�n inglesa).<br /><br /></li>
            </lu> 
            <!-- Fin del contenido propio de la p�gina -->
            </td>
            <td width="6" background="../../../Images/Tabla/6.gif">&nbsp;</td>
          </tr>
          <tr>
            <td width="6" height="6" background="../../../Images/Tabla/1.gif"></td>
            <td height="6" background="../../../Images/Tabla/2.gif"></td>
            <td width="6" height="6" background="../../../Images/Tabla/3.gif"></td>
          </tr>
        </table>
        </div>
        <br />
    </DIV>
</center>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

