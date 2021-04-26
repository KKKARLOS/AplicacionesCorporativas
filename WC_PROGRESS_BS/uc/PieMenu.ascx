<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PieMenu.ascx.cs" Inherits="uc_PieMenu" %>
   <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <%--<script src="<% =Session["strServer"].ToString() %>js/jquery.min.js"></script>    --%>
<script src="<% =Session["strServer"].ToString() %>js/jquery-2.2.1.min.js"></script>    
    <script src="<% =Session["strServer"].ToString() %>js/jquery-ui.js"></script>    
    <script src="<% =Session["strServer"].ToString() %>js/bootstrap.min.js"></script>
    <script src="<% =Session["strServer"].ToString() %>js/jq.functions.js"></script>
    <script src="<% =Session["strServer"].ToString() %>js/ios-orientationchange-fix.js"></script>
    <script src="<% =Session["strServer"].ToString() %>js/JavaScript.js"></script>
    <!-- Agregar function genericas que tiran de jquery -->
    <!-- script propio de la página-->
    <script src="js/JavaScript.js"></script>
    <!-- Para el control de errores-->
    <script>
        var msgerr = "";
        <%=sErrores%>
       
    </script>
    

    
