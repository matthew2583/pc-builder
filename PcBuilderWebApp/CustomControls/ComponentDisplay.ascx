<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ComponentDisplay.ascx.cs" Inherits="PcBuilderWebApp.CustomControls.ComponentDisplay" %>

<div class="component-item">
    <h4><asp:Label ID="lblComponentName" runat="server"></asp:Label></h4>
    <p><strong>Производитель:</strong> <asp:Label ID="lblManufacturer" runat="server"></asp:Label></p>
    <p><strong>Цена:</strong> <asp:Label ID="lblPrice" runat="server" CssClass="price"></asp:Label> руб.</p>
    <asp:Button ID="btnSelect" runat="server" Text="Выбрать" CssClass="btn-select" OnClick="btnSelect_Click" />
</div>

