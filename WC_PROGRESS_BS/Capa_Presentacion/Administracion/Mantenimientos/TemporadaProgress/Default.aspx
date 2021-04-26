<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Mantenimientos_TemporadaProgress_Default" %>

<%@ Register Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>
<html>
<head>
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title>Temporada Progress</title>
    <link rel="Stylesheet" href="../../../../js/plugins/jQueryUI/jquery-ui.min.css" />
    <style>
        /*input[type=text]::-ms-clear, input[type=number]::-ms-clear  {
            display: none;
        }*/
    </style>

</head>

<body data-codigopantalla="418">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>

    <br />
    <br class="hidden-xs" />
    <br class="hidden-xs" />

    <div class="container">

        <div class="row">

            <div class="col-xs-12 well">

                <fieldset class="fieldset">

                    
                    <legend class="fieldset">Temporada y período Progress</legend>

                    <br />
                    
                    <div class="form-group">
                        <label class="col-xs-2 control-label" for="inputTemporada">Temporada</label>
                        <div class="col-xs-2">
                            <input  id="inputTemporada" name="inputTemporada" type="number" style="padding-top:0; padding-bottom:0" min="0" max="9999" onkeypress="return isNumberKey(event)" class="form-control text-center input-xs"  oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);"  maxlength = "4">                                   
                        </div>
                    </div>

                    <br /><br />
                    
                    <div class="form-group">
                        <label class="col-xs-2 control-label" for="inputPeriodo">Período</label>
                        <div class="col-xs-4">
                            <input id="inputPeriodo" runat="server" name="textinput" style="padding-top:0; padding-bottom:0" type="text" class="form-control input-xs">                            
                        </div>
                    </div>

                    
                </fieldset>

                <button id="btnGrabar" class="btn btn-primary pull-right">Grabar</button>

            </div>
        </div>

    </div>

</body>
</html>

<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
