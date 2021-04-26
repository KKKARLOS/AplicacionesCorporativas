<%@ Control Language="C#" ClassName="Procesando" %>
<div id="procesandoSuperior" 
    style="z-index:2000;
        position:absolute;
        visibility: hidden;
        top:0px;
        left:0px;
        width:100%;
        height:100%;
        background-repeat:repeat;">
    <div id="procesando" 
        style="z-index:11; 
               position:absolute;
               visibility: hidden; 
               top:260px;
               left:420px;
               width:152px;
               height:33px"><asp:Image ID="imgProcesando" runat="server" Height="33px" Width="152px" ImageUrl="~/images/imgProcesando.gif" />
               <div id="reloj" style="z-index:12; 
                                       position:absolute;
                                       top:1px;
                                       left:118px; 
                                       width:32px; 
                                       height:32px"><asp:Image ID="Image1" runat="server" Height="32px" Width="32px" ImageUrl="~/images/imgRelojAnim.gif" />
                </div>
    </div>
</div>
<script type="text/javascript" language="javascript">
<!--
    function centrarProcesando() {
        $I("procesandoSuperior").style.width = "100%";
        $I("procesandoSuperior").style.height = "100%";
        if (typeof (window.dialogArguments) == "undefined") {
            if (document.all) {
                //alert("height:" + document.body.clientHeight + " width:" + document.body.clientWidth);
                //alert("height:" + document.documentElement.clientHeight + " width:" + document.documentElement.clientWidth);

                $I("procesando").style.top = ((document.documentElement.clientHeight / 2) - 50) + "px";
                $I("procesando").style.left = ((document.documentElement.clientWidth / 2) - 76) + "px";
            }
            else {
                $I("procesando").style.top = ((window.innerHeight / 2) - 50) + "px";
                $I("procesando").style.left = ((window.innerWidth / 2) - 76) + "px";
            }
        } else {
            if (document.all) {
                $I("procesando").style.top = ((parseInt(window.dialogHeight, 10) / 2) - 50) + "px";
                $I("procesando").style.left = ((parseInt(window.dialogWidth, 10) / 2) - 76) + "px";
            }
            else {
                $I("procesando").style.top = ((window.innerHeight / 2) - 50) + "px";
                $I("procesando").style.left = ((window.innerWidth / 2) - 76) + "px";
            }
        }
    }
    centrarProcesando();
    mostrarProcesando();
-->
</script>
