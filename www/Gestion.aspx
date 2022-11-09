<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gestion.aspx.cs" Inherits="www.Gestion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Gestión</title>
    <style type="text/css">
        .auto-style1 {
            text-align: right;
        }
        .auto-style2 {
            width: 180px;
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
        .auto-style3 {
            width: 180px;
            height: 55px;
        }
        .auto-style4 {
            height: 55px;
        }
        .auto-style5 {
            width: 180px;
            height: 26px;
        }
        .auto-style6 {
            height: 26px;
            vertical-align: middle;
        }
    </style>
</head>
<body>
    <header class="barra">
        &nbsp;<asp:Label ID="tltGestion" runat="server" Text="Zona Gestión"></asp:Label>
    </header>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%;">
                <tr>
                    <td class="auto-style2">Hola,
                        <asp:Label ID="lblUser" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td class="auto-style1">
                        <asp:Button ID="btnIrEntradas" runat="server" OnClick="btnIrEntradas_Click" Text="Ir a Entradas" />
                        <asp:Button ID="btnCerrarSesion" runat="server" OnClick="btnCerrarSesion_Click" Text="Cerrar Sesión" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5"></td>
                    <td class="auto-style6">
                        Sección:
                        <asp:DropDownList ID="ddlSelFuncionGestor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelFuncionGestor_SelectedIndex" style="height: 22px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"></td>
                    <td class="auto-style4">
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td>
                        <asp:GridView ID="gvGestionUsuarios" runat="server" CellPadding="8" AutoGenerateColumns="false" HorizontalAlign="Center" Visible="False">
                            <Columns>
                                 <asp:BoundField DataField="Id." HeaderText="Id. Usuario"/>
                                 <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                 <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" />
                                 <asp:BoundField DataField="Email" HeaderText="Email" />
                                 <asp:BoundField DataField="Gestor" HeaderText="Gestor" />
                             </Columns>
                        </asp:GridView>
                        <asp:GridView ID="gvLog" runat="server" CellPadding="8" AutoGenerateColumns="false" HorizontalAlign="Center" Visible="False">
                            <Columns>
                                 <asp:BoundField DataField="Id." HeaderText="Id. Log" />
                                 <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                 <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                                 <asp:BoundField DataField="Tipo de acceso" HeaderText="Tipo de acceso" />
                             </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
