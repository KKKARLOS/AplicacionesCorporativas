<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_CargaCobrosSAP_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="cb1" TagName="Menu" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="cb1" TagName="Head" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:Head runat="server" ID="Head" />
    <title>::: SUPER ::: Cargar cobros desde SAP</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>css/jquery-ui.min.css" />
</head>
<body>
    <script>
        var opciones = { delay: 1 }
        IB.procesando.opciones(opciones);
        IB.procesando.mostrar();
    </script>
    <cb1:Menu runat="server" ID="Menu" />
    <div class="container-fluid">
        <h1 class="hiddenStructure">::: SUPER ::: Cargar cobros desde SAP</h1>
        <br class="visible-xs" />
        <div class="ibox-content blockquote blockquote-info">
            <div class="row">
                <div class="ibox-title">
                    <div class="panel-group">
                        <form id="frmDatos" class="form-horizontal" runat="server">
                            <div class="form-group justify-content-center">
                                <h2 class="hiddenStructure">Cargar cobros desde SAP</h2>
                                <br />
                                <div class="input-group col-sm-6 col-sm-offset-3 col-md-4 col-md-offset-4">
                                    <fieldset class="fieldset">
                                    <legend class="fieldset"><span>Cargar cobros desde SAP</span></legend>
                                    <br />
                                    <div class="col-xs-12">
                                        <label class="control-label col-xs-12 col-lg-5" style="margin-top: 0px;" for="txtFecha">Fecha de cobro</label>
                                        <div class="col-xs-12 col-lg-7">
                                            <input type="text" id="txtFecha" class="form-control text-center fk_filter" />                                        
                                        </div>
                                        <div class="col-xs-12">
                                            <button type="button" id="btnProcesar" style="margin-top:10px"" class="btn btn-primary bottom pull-right">Cargar</button>
                                        </div>
                                    </div>
                                    <br />
                                    </fieldset>
                                </div>
                            </div>
                            <!--<div class="row pull-right">
                                             
                                    <label id="lbl1" class="col-xs-6 control-label" for="img1">Borrar tabla interfaz</label>
                                <img id="img1" src="<% =Session["strServer"] %>Images/void.gif" />
                            </div>
                            <div class="row">
                                <label id="lbl2" class="col-xs-6 control-label" for="img2">Recoger saldos de SAP</label>
                                <img id="img2" src="<% =Session["strServer"] %>Images/void.gif" />
                            </div>
                            <div class="row">
                                <label id="lbl3" class="col-xs-6 control-label" for="img3">Cargar cobros en SUPER</label>
                                <img id="img3" src="<% =Session["strServer"] %>Images/void.gif" />
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-xs-12 col-md-6">
                                    <button type="button" id="btnProcesar" class="btn btn-primary bottom">Cargar</button>
                                </div>
                            </div>-->
                            <!--<div class="form-group">
                                <div id="pieTarea" class="pull-right" style="padding-right: 20px;">
                                    <div id="divGrabar">
                                        <button type="button" id="btnProcesar" class="btn btn-primary bottom">Cargar</button>
                                    </div>
                                </div>
                            </div>   -->
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
<script src="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/scripts/JavaScript.js" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/jquery-ui.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/moment.locale.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/accounting.min.js" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/stringbuilder.js" type="text/javascript"></script>
<script src="js/view.js?20180118" type="text/javascript"></script>
<script src="js/app.js?20170719_01" type="text/javascript"></script>
