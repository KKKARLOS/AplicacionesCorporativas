﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Mantenimientos_ComunidadProgess_Default" %>
<%@ Register Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>
<html>
<head>
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title>Comunidad Progress</title>
</head>

<body data-codigopantalla="408">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
        
    <br class="hidden-xs" />
    <br class="hidden-xs" />
    <div class="container">

     
        
         <div id="divEmpresas">
            <div class="row">
                <div id="lisEmpresas" class="text-left dual-list list-left col-xs-5">
                    <h4>Empresas</h4>
                    <div class="well">
                        <div class="row">
                            <div class="btn-group col-xs-2">
                                <a class="btn btn-default selector" data-toggle="popover" title="" data-content="Marcar/desmarcar todo" data-placement="top">
                                    <i class="glyphicon glyphicon-unchecked"></i>
                                </a>
                            </div>
                            <div class="col-xs-10 input-group">
                                <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                            </div>
                        </div>
                        <ul class="list-group" runat="server" id="UlEmpresas1">
                        </ul>
                    </div>
                </div>
                <div class="list-arrows col-xs-2 text-center">
                    <div class="row">
                        <button class="btn btn-default btn-sm move-right">
                            <span class="glyphicon glyphicon-chevron-right"></span>
                        </button>
                    </div>
                    <div class="row">
                        <button class="btn btn-default btn-sm move-left">
                            <span class="glyphicon glyphicon-chevron-left"></span>
                        </button>
                    </div>
                </div>
                <div id="lisEmpresas2" class="dual-list list-right col-xs-5">
                    <h4>Empresas dentro de la comunidad</h4>
                    <div class="well">
                        <div class="row">
                            <div class="btn-group col-xs-2">
                                <a class="btn btn-default selector" data-toggle="popover" title="" data-content="Marcar/desmarcar todo" data-placement="top">
                                    <i class="glyphicon glyphicon-unchecked"></i>
                                </a>
                            </div>
                            <div class="col-xs-10 input-group">
                                <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                            </div>
                        </div>
                        <ul class="list-group" runat="server" id="UlEmpresas2">
                        </ul>
                    </div>
                </div>
            </div>
        </div>


        <!-- CR-->
        <div id="divCR">
            <div class="row">
                <div id="lisCR" class="text-left dual-list list-left col-xs-5">
                    <h4>CR's</h4>
                    <div class="well">
                        <div class="row">
                            <div class="btn-group col-xs-2">
                                <a class="btn btn-default selector" data-toggle="popover" title="" data-content="Marcar/desmarcar todo" data-placement="top">
                                    <i class="glyphicon glyphicon-unchecked"></i>
                                </a>
                            </div>
                            <div class="col-xs-10 input-group">
                                <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                            </div>
                        </div>
                        <ul class="list-group" runat="server" id="ulCR1">
                        </ul>
                    </div>
                </div>
                <div class="list-arrows col-xs-2 text-center">
                    <div class="row">
                        <button class="btn btn-default btn-sm move-right">
                            <span class="glyphicon glyphicon-chevron-right"></span>
                        </button>
                    </div>
                    <div class="row">
                        <button class="btn btn-default btn-sm move-left">
                            <span class="glyphicon glyphicon-chevron-left"></span>
                        </button>
                    </div>
                </div>
                <div id="lisCR2" class="dual-list list-right col-xs-5">
                    <h4>CR's fuera de la comunidad</h4>
                    <div class="well">
                        <div class="row">
                            <div class="btn-group col-xs-2">
                                <a class="btn btn-default selector" data-toggle="popover" title="" data-content="Marcar/desmarcar todo" data-placement="top">
                                    <i class="glyphicon glyphicon-unchecked"></i>
                                </a>
                            </div>
                            <div class="col-xs-10 input-group">
                                <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                            </div>
                        </div>
                        <ul class="list-group" runat="server" id="ulCR2">
                        </ul>
                    </div>
                </div>
            </div>
        </div>

      

           <!-- FICEPI -->
        <div id="divProfesionales">
            <div class="row">
                <div id="lisProfesionales" class="text-left dual-list list-left col-xs-5">
                    <h4>Profesionales</h4>
                    <div class="well">
                        <div class="row">
                            <div class="btn-group col-xs-2">
                                <a class="btn btn-default selector" data-toggle="popover" title="" data-content="Marcar/desmarcar todo" data-placement="top">
                                    <i class="glyphicon glyphicon-unchecked"></i>
                                </a>
                            </div>
                            <div class="col-xs-10 input-group">
                                <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                            </div>
                        </div>
                        <ul class="list-group" runat="server" id="ulProfesionales1">
                        </ul>
                    </div>
                </div>
                <div class="list-arrows col-xs-2 text-center">
                    <div class="row">
                        <button class="btn btn-default btn-sm move-right">
                            <span class="glyphicon glyphicon-chevron-right"></span>
                        </button>
                    </div>
                    <div class="row">
                        <button class="btn btn-default btn-sm move-left">
                            <span class="glyphicon glyphicon-chevron-left"></span>
                        </button>
                    </div>
                </div>
                <div id="lisProfesionales2" class="dual-list list-right col-xs-5">
                    <h4>Profesionales fuera de la comunidad</h4>
                    <div class="well">
                        <div class="row">
                            <div class="btn-group col-xs-2">
                                <a class="btn btn-default selector" data-toggle="popover" title="" data-content="Marcar/desmarcar todo" data-placement="top">
                                    <i class="glyphicon glyphicon-unchecked"></i>
                                </a>
                            </div>
                            <div class="col-xs-10 input-group">
                                <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                            </div>
                        </div>
                        <ul class="list-group" runat="server" id="ulProfesionales2">
                        </ul>
                    </div>
                </div>
            </div>
        </div>


           <hr class="hrColor" />


          <!-- Profesionales forzados -->
           <div id="divProfesionalesForzados">
            <div class="row">
                <div id="lisProfesionalesForzados" class="text-left dual-list list-left col-xs-5">
                    <h4>Profesionales</h4>
                    <div class="well">
                        <div class="row">
                            <div class="btn-group col-xs-2">
                                <a class="btn btn-default selector" data-toggle="popover" title="" data-content="Marcar/desmarcar todo" data-placement="top">
                                    <i class="glyphicon glyphicon-unchecked"></i>
                                </a>
                            </div>
                            <div class="col-xs-10 input-group">
                                <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                            </div>
                        </div>
                        <ul class="list-group" runat="server" id="ulForzados1">
                        </ul>
                    </div>
                </div>
                <div class="list-arrows col-xs-2 text-center">
                    <div class="row">
                        <button class="btn btn-default btn-sm move-right">
                            <span class="glyphicon glyphicon-chevron-right"></span>
                        </button>
                    </div>
                    <div class="row">
                        <button class="btn btn-default btn-sm move-left">
                            <span class="glyphicon glyphicon-chevron-left"></span>
                        </button>
                    </div>
                </div>
                <div id="lisProfesionalesForzados2" class="dual-list list-right col-xs-5">
                    <h4>Profesionales pertenecientes a la comunidad, forzadamente</h4>
                    <div class="well">
                        <div class="row">
                            <div class="btn-group col-xs-2">
                                <a class="btn btn-default selector" data-toggle="popover" title="" data-content="Marcar/desmarcar todo" data-placement="top">
                                    <i class="glyphicon glyphicon-unchecked"></i>
                                </a>
                            </div>
                            <div class="col-xs-10 input-group">
                                <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                            </div>
                        </div>
                        <ul class="list-group" runat="server" id="ulForzados2">
                        </ul>
                    </div>
                </div>
            </div>
        </div>


    </div>
    
</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
