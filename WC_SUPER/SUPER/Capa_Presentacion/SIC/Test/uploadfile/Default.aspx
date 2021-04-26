<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_SIC_Test_uploadfile_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="dropzone.css" rel="stylesheet" />
</head>
<body>
    <script src="../../../../scripts/IB.js"></script>
    <form id="form1" runat="server"></form>
        
    <div id="dropzone" class="dropzonee needsclick dz-clickable">
                <div class="dz-message needsclick">
                    <h2>Arrastra aquí los ficheros o haz click.</h2>
                </div>
        </div>

        <script src="../../../../scripts/jquery-2.2.1.min.js"></script>
        <script src="dropzone.js"></script>
        <script src="app.js"></script>
</body>
</html>
