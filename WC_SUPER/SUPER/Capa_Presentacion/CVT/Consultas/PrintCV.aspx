<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCV.aspx.cs" Inherits="Capa_Presentacion_CVT_Consultas_PrintCV" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Visualización de Currículum Vitae</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<style type="text/css">
	.tituloPrincipal{
	    margin-left:40px;
	    margin-top:50px;
	    width:90%;
	}
	.titulo1{
         font-weight: bold; 
         color: #336699;
         font-size: 16px; 
         /*text-decoration:underline;*/
         width: 100%;
         border-bottom: solid 2px #336699;
     }
     .titulo2{
         color: #336699;
         font-size: 13px; 
         font-weight: bold;
     }
     .titulo3{
         color: #336699;
         font-size: 12px;
     }
     .W95{
         width:95px;
     }
     .W510{
         width:510px;
     }
     
    @media print {    
        .ScrollingContent { display:block; }    
        .PrintingContent { display:block; }    
        #tblBotonera { display:none; }
    } 

     
	</style>
	<script language="javascript" type="text/javascript">
	    function imprimirCV() {
	        window.focus();
	        window.print();
	    }
	</script>
</head>
<body>
    <form id="form1" runat="server">

    </form>
</body>
</html>
