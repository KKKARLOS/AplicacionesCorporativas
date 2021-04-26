<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Grafico.aspx.cs" Inherits="Capa_Presentacion_ECO_Consultas_LineaBase_Grafico" %>

<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <title></title>
    <script src="../../../../Javascript/funciones.js" type="text/Javascript"></script></head>
  	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>    
<body>
    <form id="form1" runat="server">
     <script type="text/javascript">
         var strServer = "<%=Session["strServer"]%>";
         function mostrar(idPE) {
             //alert("Proyecto " + idPSN);
             try {
                 //mostrarProcesando();

                 //location.href = "../../ValorGanado/Default.aspx?sp=" + codpar(idPSN);
                 //var aResul = idPSN.split("(");
                 //aResul = aResul[1].split(")");
                 //var sUrl = "../../ValorGanado/Default.aspx?sp=" + codpar(aResul[0]);
                 //location.href = sUrl
                 //setTimeout("cerrarVentana", 50);

                 //var sUrl = "../../ValorGanado/Default.aspx?pe=" + codpar(idPE);// + "&lp=" + codpar($I("hdnListaPE").value);
                 //window.open(sUrl, "", "resizable=no,status=no,scrollbars=yes,menubar=no,top=" + eval(screen.availHeight / 2 - 384) + ",left=" + eval(screen.availWidth / 2 - 512) + ",width=1014px,height=709px");
                 //modalDialog.Close(window, sUrl);
                 var sPantalla = strServer + "Capa_Presentacion/ECO/ValorGanadoModal/Default.aspx?pe=" + codpar(idPE);// + "&lp=" + codpar($I("hdnListaPE").value);
                 modalDialog.Show(sPantalla, self, sSize(1020, 780))
                     .then(function (ret) {
                         if (ret != null) {
                             //location.href = ret;
                             ocultarProcesando();
                         }
                     });
             } catch (e) {
                 mostrarErrorAplicacion("Error al ir a mostrar la pantalla de Valor Ganado.", e.message);
             }
         }
         //function cerrarVentana() {
         //    modalDialog.Close(window, returnValue);
         //}
    </script>
       <div>
            <div id="divGrafico" title="Grafico CPI/PSI" style="width:800px; height:600px; font-size:larger;">
                <asp:CHART id="ChartSPI2" runat="server" Palette="None" Width="790px" Height="590px" BorderDashStyle="Solid" 
                    BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1" BorderlineColor="#1A3B69" BorderlineDashStyle="Solid" BorderlineWidth="2" ImageLocation="~/TempImagesGraficos/ChartPic_#SEQ(300,3)" BackColor="#D6D6D6" SuppressExceptions="True" BorderSkin-BackColor="Gray" BorderSkin-PageColor="#DDE6E9" BorderSkin-SkinStyle="None" BackImageTransparentColor="#DDE6E9" ImageType="Png" IsSoftShadows="False" ImageStorageMode="UseImageLocation">
                    <titles>
                        <asp:Title ShadowColor="32, 0, 0, 0" Font="Arial, 14pt, style=Bold" ShadowOffset="3" Text="Situación de proyectos a mes actual" Name="Title1" ForeColor="26, 59, 105"></asp:Title>
                    </titles>
                    <legends>
		                <asp:legend LegendStyle="Table" IsTextAutoFit="False" Docking="Bottom" Name="Default" BackColor="Transparent" Font="Arial, 8.25pt, style=Bold" Alignment="Center"></asp:legend>
                    </legends>
                    <BorderSkin SkinStyle="Emboss" BackImageTransparentColor="#DDE6E9" BorderWidth="0" BorderColor="#DDE6E9" PageColor="#DDE6E9" />
			        <series>
				        <asp:Series IsValueShownAsLabel="False" YValuesPerPoint="1" Name="Proyecto" ChartType="Bubble" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240" Font="Trebuchet MS, 9pt">
					        <points>
						        <asp:DataPoint YValues="3,4" />
					        </points>
				        </asp:Series>
			        </series>                            
                    <chartareas>
                        <asp:ChartArea Name="ChartArea1" BorderColor="Black" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom" BorderDashStyle="Solid">
                            <area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
                            <axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8" Title="Retraso <-- SPI --> Adelanto" Minimum="0" Maximum="2" Interval="1">
	                            <LabelStyle Font="Arial, 8.25pt, style=Bold" />
	                            <MajorGrid LineColor="64, 64, 64, 64" />
                            </axisy>
                            <axisx LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8" Title="Sobrecoste <-- CPI --> Infracoste"  Minimum="0" Maximum="2" Interval="1">
	                            <LabelStyle Font="Arial, 8.25pt, style=Bold" />
	                            <MajorGrid LineColor="64, 64, 64, 64" />
                            </axisx>
                        </asp:ChartArea>
                    </chartareas>
                </asp:CHART>
            </div>
        </div>
    </form>
</body>
</html>
