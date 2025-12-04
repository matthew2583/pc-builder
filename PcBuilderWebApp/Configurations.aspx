<%@ Page Title="Сохраненные сборки" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Configurations.aspx.cs" Inherits="PcBuilderWebApp.Configurations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .config-list {
            margin: 30px 0;
        }
        .config-item {
            border: 1px solid #e0e0e0;
            padding: 20px;
            margin: 15px 0;
            background: white;
            border-radius: 8px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.08);
            transition: box-shadow 0.2s;
        }
        .config-item:hover {
            box-shadow: 0 4px 12px rgba(0,0,0,0.12);
            border-color: #667eea;
        }
        .config-item h3 {
            color: #667eea;
            margin-top: 0;
            font-size: 20px;
            font-weight: 600;
            padding-bottom: 8px;
            border-bottom: 1px solid #f0f0f0;
        }
        .config-info {
            color: #666;
            margin: 12px 0;
        }
        .config-info p {
            margin: 6px 0;
            font-size: 14px;
        }
        .config-info p strong {
            color: #333;
            font-weight: 600;
        }
        .btn-load {
            background: #667eea;
            color: white;
            border: none;
            padding: 8px 16px;
            border-radius: 5px;
            cursor: pointer;
            margin-right: 10px;
            font-weight: 500;
            transition: background-color 0.2s;
        }
        .btn-load:hover {
            background: #5568d3;
        }
        .btn-delete {
            background: #f44336;
            color: white;
            border: none;
            padding: 8px 16px;
            border-radius: 5px;
            cursor: pointer;
            font-weight: 500;
            transition: background-color 0.2s;
        }
        .btn-delete:hover {
            background: #d32f2f;
        }
        .empty-message {
            text-align: center;
            padding: 40px;
            color: #999;
            background: #f8f9fa;
            border-radius: 8px;
            border: 1px dashed #e0e0e0;
        }
        .empty-message p {
            font-size: 16px;
            margin: 0;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Сохраненные сборки</h1>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlEmptyMessage" runat="server" CssClass="empty-message" Visible="false">
                <p>Нет сохраненных сборок. Создайте сборку на странице "Моя сборка".</p>
            </asp:Panel>
            
            <asp:Repeater ID="rptConfigurations" runat="server" OnItemCommand="rptConfigurations_ItemCommand">
                <ItemTemplate>
                    <div class="config-item">
                        <h3><%# Eval("ConfigurationName") %></h3>
                        <div class="config-info">
                            <p>Дата создания: <%# ((DateTime?)Eval("CreatedAt")).HasValue ? ((DateTime?)Eval("CreatedAt")).Value.ToString("dd.MM.yyyy HH:mm") : "Не указана" %></p>
                            <p>Общая стоимость: <%# Eval("TotalCost", "{0:N0}") %> руб.</p>
                            <p>Энергопотребление: <%# Eval("TotalPower") %> Вт</p>
                        </div>
                        <asp:Button ID="btnLoad" runat="server" Text="Загрузить" 
                            CssClass="btn-load" 
                            CommandName="Load" 
                            CommandArgument='<%# Eval("Id") %>' />
                        <asp:Button ID="btnDelete" runat="server" Text="Удалить" 
                            CssClass="btn-delete" 
                            CommandName="Delete" 
                            CommandArgument='<%# Eval("Id") %>'
                            OnClientClick="return confirm('Вы уверены, что хотите удалить эту сборку?');" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

