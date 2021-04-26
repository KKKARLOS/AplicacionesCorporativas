<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_ColumnDrag_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script>
//// init the script
//window.onload = function() {
//// create a variable for the object
//t1 = new dragTable('tableOne')
//}
function init(){
t1 = new dragTable('tableOne')
}
</script>
<table id="tableOne">
<%--    <thead>
        <tr>
            <th>HEADER 1</th>
            <th>HEADER 2</th>
            <th>HEADER 3</th>
            <th>HEADER 4</th>
            <th>HEADER 5</th>
            <th>HEADER 6</th>
            <th>HEADER 7</th>
            <th>HEADER 8</th>
            <th>HEADER 9</th>
            <th>HEADER 10</th>
        </tr>
    </thead>--%>
    <tbody>
        <tr>
            <td>HEADER 1</td>
            <td>HEADER 2</td>
            <td>HEADER 3</td>
            <td>HEADER 4</td>
            <td>HEADER 5</td>
            <td>HEADER 6</td>
            <td>HEADER 7</td>
            <td>HEADER 8</td>
            <td>HEADER 9</td>
            <td>HEADER 10</td>
        </tr>
        <tr>
            <td>Dummy 1</td>
            <td>Dummy 2</td>
            <td>Dummy 3</td>
            <td>Dummy 4</td>
            <td>Dummy 5</td>
            <td>Dummy 6</td>
            <td>Dummy 7</td>
            <td>Dummy 8</td>
            <td>Dummy 9</td>
            <td>Dummy 10</td>
        </tr>
        <tr>
            <td>Dummy 1</td>
            <td>Dummy 2</td>
            <td>Dummy 3</td>
            <td>Dummy 4</td>
            <td>Dummy 5</td>
            <td>Dummy 6</td>
            <td>Dummy 7</td>
            <td>Dummy 8</td>
            <td>Dummy 9</td>
            <td>Dummy 10</td>
        </tr>
        <tr>
            <td>Dummy 1</td>
            <td>Dummy 2</td>
            <td>Dummy 3</td>
            <td>Dummy 4</td>
            <td>Dummy 5</td>
            <td>Dummy 6</td>
            <td>Dummy 7</td>
            <td>Dummy 8</td>
            <td>Dummy 9</td>
            <td>Dummy 10</td>
        </tr>
        <tr>
            <td>Dummy 1</td>
            <td>Dummy 2</td>
            <td>Dummy 3</td>
            <td>Dummy 4</td>
            <td>Dummy 5</td>
            <td>Dummy 6</td>
            <td>Dummy 7</td>
            <td>Dummy 8</td>
            <td>Dummy 9</td>
            <td>Dummy 10</td>
        </tr>
        <tr>
            <td>Dummy 1</td>
            <td>Dummy 2</td>
            <td>Dummy 3</td>
            <td>Dummy 4</td>
            <td>Dummy 5</td>
            <td>Dummy 6</td>
            <td>Dummy 7</td>
            <td>Dummy 8</td>
            <td>Dummy 9</td>
            <td>Dummy 10</td>
        </tr>
        <tr>
            <td>Dummy 1</td>
            <td>Dummy 2</td>
            <td>Dummy 3</td>
            <td>Dummy 4</td>
            <td>Dummy 5</td>
            <td>Dummy 6</td>
            <td>Dummy 7</td>
            <td>Dummy 8</td>
            <td>Dummy 9</td>
            <td>Dummy 10</td>
        </tr>
        <tr>
            <td>Dummy 1</td>
            <td>Dummy 2</td>
            <td>Dummy 3</td>
            <td>Dummy 4</td>
            <td>Dummy 5</td>
            <td>Dummy 6</td>
            <td>Dummy 7</td>
            <td>Dummy 8</td>
            <td>Dummy 9</td>
            <td>Dummy 10</td>
        </tr>
        <tr>
            <td>Dummy 1</td>
            <td>Dummy 2</td>
            <td>Dummy 3</td>
            <td>Dummy 4</td>
            <td>Dummy 5</td>
            <td>Dummy 6</td>
            <td>Dummy 7</td>
            <td>Dummy 8</td>
            <td>Dummy 9</td>
            <td>Dummy 10</td>
        </tr>
        <tr>
            <td>Dummy 1</td>
            <td>Dummy 2</td>
            <td>Dummy 3</td>
            <td>Dummy 4</td>
            <td>Dummy 5</td>
            <td>Dummy 6</td>
            <td>Dummy 7</td>
            <td>Dummy 8</td>
            <td>Dummy 9</td>
            <td>Dummy 10</td>
        </tr>
    </tbody>
</table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

