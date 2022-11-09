<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Entradas.aspx.cs" Inherits="www.Entradas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Entradas</title>
    <style type="text/css">
        .auto-style1 {
            text-align: right;
        }
        .auto-style2 {
            height: 59px;
        }
        .auto-style3 {
            width: 249px;
        }
        .auto-style4 {
            height: 59px;
            width: 249px;
        }
        .auto-style5 {
            width: 249px;
            height: 289px;
        }
        .auto-style6 {
            height: 289px;
            text-align: center;
            vertical-align: middle;
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
        .labelPass {
            margin-top: 10px;
            display: inline-block;
        }
        </style>
</head>
<body>
    <header class="barra">
        &nbsp;<asp:Label ID="tltEntradas" runat="server" Text="Entradas"></asp:Label>
    </header>
    <form id="form1" runat="server">
        <div>
            <table style="width:100%;">
                <tr>
                    <td class="auto-style3">Hola,
                        <asp:Label ID="lblUser" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Button ID="btnIrGestion" runat="server" OnClick="btnIrGestion_Click" Text="Ir a Gestión" Visible="False" />
                        <asp:Button ID="btnCerrarSesion" runat="server" OnClick="btnCerrarSesion_Click" Text="Cerrar Sesión" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td>Entradas a las que tiene acceso:</td>
                </tr>
                <tr>
                    <td class="auto-style5"></td>
                    <td class="auto-style6">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvEntradas" runat="server" CellPadding="8" AutoGenerateColumns="false" HorizontalAlign="Center" OnRowCommand="verContraseña">
                            <Columns>
                                 <asp:BoundField DataField="Id." HeaderText="Id." />
                                 <asp:BoundField DataField="Descripción" HeaderText="Descripción" />
                                 <asp:BoundField DataField="Email" HeaderText="Email" />
                                 <asp:ButtonField ButtonType="Button" HeaderText="Contraseña" CommandName="btnVerContraseña" Text="Ver contraseña"/>
                             </Columns>
                        </asp:GridView>
                                <asp:Label ID="lblMostrarContraseña" runat="server" Text="Label" Visible="False" CssClass="labelPass"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                </table>
        </div>
    </form>
</body>
</html>
