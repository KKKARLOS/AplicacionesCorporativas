<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ayudapersonas.aspx.cs" Inherits="Capa_Presentacion_SIC_Test_ayudapersonas" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>




<!DOCTYPE html>

<html>
<head>
    <uc1:Head runat="server" ID="Head" />
    <link href="../../../plugins/ayudapersonas/ayudapersonas.css" rel="stylesheet" />
</head>


<body>
    <uc1:Menu runat="server" ID="Menu" />

    <form id="form1" runat="server"></form>
    <input type="button" id="btn1" value="Ayuda 1" />
    <br />
    <input type="button" id="btn2" value="Ayuda 2" />

    <br /><br /><br />

    <input type="button" id="btn3" value="Ayuda multi 1" />
    <br />
    <input type="button" id="btn4" value="Ayuda multi 2" />
    
    <hr />

    <table id="tblParticipantes" class="compact cell-border dataTable no-footer" role="grid" style="width: 100%;">
        <thead class="bg-primary">
            <tr role="row" style="height: 0px;">
                <th class="sorting" aria-controls="tblParticipantes" rowspan="1" colspan="1" aria-label="Profesional: activate to sort column ascending" style="padding-top: 0px; padding-bottom: 0px; border-top-width: 0px; border-bottom-width: 0px; height: 0px; width: 211px;">
                    <div class="dataTables_sizing" style="height: 0; overflow: hidden;">Profesional</div>
                </th>
                <th class="sorting" aria-controls="tblParticipantes" rowspan="1" colspan="1" aria-label="Participación: activate to sort column ascending" style="padding-top: 0px; padding-bottom: 0px; border-top-width: 0px; border-bottom-width: 0px; height: 0px; width: 101px;">
                    <div class="dataTables_sizing" style="height: 0; overflow: hidden;">Participación</div>
                </th>
            </tr>
        </thead>


        <tbody>
            <tr id="0" data-t001_idficepi_participante="7583" data-estado="A" role="row" class="odd">
                <td class="fk_nombre">ASENJO ANDUEZA, Javier</td>
                <td><span class="fk_ParticipacionProfesional underline">En curso</span></td>
            </tr>
            <tr id="1" data-t001_idficepi_participante="12155" data-estado="A" role="row" class="even">
                <td class="fk_nombre">VELAZQUEZ MONTAÑA, David</td>
                <td><span class="fk_ParticipacionProfesional underline">En curso</span></td>
            </tr>
        </tbody>
    </table>

    <div id="divayudapersonas" class="fk_ayuda"></div>
    <div id="divayudapersonas2" class="fk_ayuda"></div>

    <div id="divayudapersonasmulti1"></div>
    <div id="divayudapersonasmulti2"></div>


    <script src="../../../plugins/IB/buscaprof.js"></script>
    <script src="../../../plugins/IB/buscaprofmulti.js"></script>
    <script src="app.js"></script>
    

</body>
</html>


