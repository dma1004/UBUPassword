<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="www.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inicio de sesión</title>
    <style type="text/css">
        .auto-style8 {
            width: 163px;
            height: 23px;
            text-align: center;
        }
        .auto-style9 {
            height: 23px;
            width: 185px;
        }
        .auto-style11 {
            width: 24%;
            height: 118px;
        }
        .auto-style12 {
            width: 185px;
            text-align: right;
        }

        .barra {
            list-style-type: none;
            height: 25px;
            margin: 0;
            padding: 4px 0 0 0;
            overflow: hidden;
            vertical-align: middle;
            background-color: #a0a0a0;
            text-align: center;
        }

        h1 {
            margin: 0;
        }

        .auto-style13 {
            height: 23px;
            text-align: center;
        }

        .auto-style14 {
            width: 163px;
            text-align: left;
        }
        .auto-style15 {
            text-align: center;
        }
        .auto-style16 {
            width: 185px;
            text-align: right;
            height: 26px;
        }
        .auto-style17 {
            width: 163px;
            text-align: left;
            height: 26px;
        }

    </style>
</head>
<body>
        <header class="barra">
        &nbsp; UBUPassword
         </header>
        <form id="form1" runat="server">
        <div>
            <table align="center" class="auto-style11">
                <tr>
                    <td class="auto-style13" colspan="2"></td>
                </tr>
                <tr>
                    <td class="auto-style13" colspan="2">
                        <h1><asp:Label ID="tltInicioSesion" runat="server" Text="Inicio de sesión" Font-Bold="True"></asp:Label></h1>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style9"></td>
                    <td class="auto-style8">
                    </td>
                </tr>
                <tr>
                    <td class="auto-style12">
                        <asp:Label ID="Nombre" runat="server" Text="Nombre de Usuario"></asp:Label>
                    </td>
                    <td class="auto-style14">
                        <asp:TextBox ID="TBXUserName" runat="server" Width="140px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16">
                        <asp:Label ID="Email" runat="server" Text="Password"></asp:Label>
                    </td>
                    <td class="auto-style17">
                        <asp:TextBox ID="TBXPassword" runat="server" TextMode="Password" Width="140px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style15" colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style15" colspan="2">
                        <asp:Button ID="Entrar" runat="server" OnClick="Entrar_Click" Text="Entrar" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style15" colspan="2">
                        <asp:Label ID="lblerror" runat="server" Text="Label" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        
        </form>
        
</body>
</html>
