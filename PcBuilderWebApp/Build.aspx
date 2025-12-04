<%@ Page Title="Моя сборка" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Build.aspx.cs" Inherits="PcBuilderWebApp.Build" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .build-section {
            margin: 25px 0;
            padding: 20px;
            border: 1px solid #e0e0e0;
            border-radius: 8px;
            background: #fafafa;
        }
        .build-section h2 {
            color: #333;
            margin-top: 0;
            font-size: 20px;
            font-weight: 600;
            padding-bottom: 8px;
            border-bottom: 1px solid #e0e0e0;
        }
        .component-selected {
            background: #e8f5e9;
            padding: 15px;
            margin: 10px 0;
            border-left: 4px solid #4caf50;
            border-radius: 6px;
        }
        .component-empty {
            background: #fff3e0;
            padding: 15px;
            margin: 10px 0;
            border-left: 4px solid #ff9800;
            border-radius: 6px;
            color: #666;
        }
        .summary {
            background: #667eea;
            color: white;
            padding: 20px;
            border-radius: 8px;
            margin: 25px 0;
        }
        .summary h2 {
            margin-top: 0;
            font-size: 20px;
            font-weight: 600;
        }
        .summary p {
            font-size: 15px;
            margin: 8px 0;
        }
        .compatibility-issues {
            background: #ffebee;
            border: 2px solid #f44336;
            padding: 15px;
            margin: 20px 0;
            border-radius: 6px;
        }
        .compatibility-issues h3 {
            color: #d32f2f;
            margin-top: 0;
            font-size: 18px;
            font-weight: 600;
        }
        .compatibility-issues ul {
            margin: 10px 0;
            padding-left: 20px;
        }
        .compatibility-issues li {
            margin: 6px 0;
            color: #c62828;
        }
        .compatibility-ok {
            background: #e8f5e9;
            border: 2px solid #4caf50;
            padding: 15px;
            margin: 20px 0;
            border-radius: 6px;
        }
        .compatibility-ok h3 {
            color: #2e7d32;
            margin-top: 0;
            font-size: 18px;
            font-weight: 600;
        }
        .btn-save {
            background: #4caf50;
            color: white;
            border: none;
            padding: 12px 24px;
            border-radius: 6px;
            cursor: pointer;
            font-size: 15px;
            font-weight: 600;
            margin-top: 15px;
            transition: background-color 0.2s;
        }
        .btn-save:hover {
            background: #45a049;
        }
        .btn-remove {
            background: #f44336;
            color: white !important;
            border: none;
            padding: 6px 12px;
            border-radius: 5px;
            cursor: pointer;
            font-size: 12px;
            margin-left: 10px;
            transition: background-color 0.2s;
            text-decoration: none;
            display: inline-block;
        }
        .btn-remove:hover {
            background: #d32f2f;
            color: white !important;
            text-decoration: none;
        }
        #txtConfigName {
            padding: 8px 12px;
            border: 1px solid #ddd;
            border-radius: 5px;
            font-size: 14px;
            width: 300px;
        }
        #txtConfigName:focus {
            outline: none;
            border-color: #667eea;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Моя сборка ПК</h1>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="build-section">
                <h2>Выбранные компоненты</h2>
                
                <asp:Panel ID="pnlCPU" runat="server" CssClass="component-empty">
                    <strong>Процессор:</strong> <asp:Label ID="lblCPU" runat="server" Text="Не выбран"></asp:Label>
                    <asp:LinkButton ID="btnRemoveCPU" runat="server" Text="Удалить" CssClass="btn-remove" OnClick="btnRemove_Click" CommandArgument="CPU" Visible="false" CausesValidation="false" />
                </asp:Panel>
                
                <asp:Panel ID="pnlGPU" runat="server" CssClass="component-empty">
                    <strong>Видеокарта:</strong> <asp:Label ID="lblGPU" runat="server" Text="Не выбрана"></asp:Label>
                    <asp:LinkButton ID="btnRemoveGPU" runat="server" Text="Удалить" CssClass="btn-remove" OnClick="btnRemove_Click" CommandArgument="GPU" Visible="false" CausesValidation="false" />
                </asp:Panel>
                
                <asp:Panel ID="pnlMotherboard" runat="server" CssClass="component-empty">
                    <strong>Материнская плата:</strong> <asp:Label ID="lblMotherboard" runat="server" Text="Не выбрана"></asp:Label>
                    <asp:LinkButton ID="btnRemoveMotherboard" runat="server" Text="Удалить" CssClass="btn-remove" OnClick="btnRemove_Click" CommandArgument="Motherboard" Visible="false" CausesValidation="false" />
                </asp:Panel>
                
                <asp:Panel ID="pnlRAM" runat="server" CssClass="component-empty">
                    <strong>Оперативная память:</strong> <asp:Label ID="lblRAM" runat="server" Text="Не выбрана"></asp:Label>
                    <asp:LinkButton ID="btnRemoveRAM" runat="server" Text="Удалить" CssClass="btn-remove" OnClick="btnRemove_Click" CommandArgument="RAM" Visible="false" CausesValidation="false" />
                </asp:Panel>
                
                <asp:Panel ID="pnlPSU" runat="server" CssClass="component-empty">
                    <strong>Блок питания:</strong> <asp:Label ID="lblPSU" runat="server" Text="Не выбран"></asp:Label>
                    <asp:LinkButton ID="btnRemovePSU" runat="server" Text="Удалить" CssClass="btn-remove" OnClick="btnRemove_Click" CommandArgument="PSU" Visible="false" CausesValidation="false" />
                </asp:Panel>
                
                <asp:Panel ID="pnlStorage" runat="server" CssClass="component-empty">
                    <strong>Накопитель:</strong> <asp:Label ID="lblStorage" runat="server" Text="Не выбран"></asp:Label>
                    <asp:LinkButton ID="btnRemoveStorage" runat="server" Text="Удалить" CssClass="btn-remove" OnClick="btnRemove_Click" CommandArgument="Storage" Visible="false" CausesValidation="false" />
                </asp:Panel>
                
                <asp:Panel ID="pnlCooler" runat="server" CssClass="component-empty">
                    <strong>Кулер:</strong> <asp:Label ID="lblCooler" runat="server" Text="Не выбран"></asp:Label>
                    <asp:LinkButton ID="btnRemoveCooler" runat="server" Text="Удалить" CssClass="btn-remove" OnClick="btnRemove_Click" CommandArgument="Cooler" Visible="false" CausesValidation="false" />
                </asp:Panel>
                
                <asp:Panel ID="pnlCase" runat="server" CssClass="component-empty">
                    <strong>Корпус:</strong> <asp:Label ID="lblCase" runat="server" Text="Не выбран"></asp:Label>
                    <asp:LinkButton ID="btnRemoveCase" runat="server" Text="Удалить" CssClass="btn-remove" OnClick="btnRemove_Click" CommandArgument="Case" Visible="false" CausesValidation="false" />
                </asp:Panel>
            </div>

            <div class="summary">
                <h2>Итоговая информация</h2>
                <p><strong>Общая стоимость:</strong> <asp:Label ID="lblTotalCost" runat="server" Text="0"></asp:Label> руб.</p>
                <p><strong>Общее энергопотребление:</strong> <asp:Label ID="lblTotalPower" runat="server" Text="0"></asp:Label> Вт</p>
            </div>

            <div id="divCompatibility" runat="server">
            </div>

            <div>
                <asp:Label ID="lblConfigName" runat="server" Text="Название сборки:"></asp:Label>
                <asp:TextBox ID="txtConfigName" runat="server" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvConfigName" runat="server" 
                    ControlToValidate="txtConfigName" 
                    ErrorMessage="Введите название сборки" 
                    Display="Dynamic"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                <br />
                <asp:Button ID="btnSaveConfiguration" runat="server" Text="Сохранить сборку" 
                    CssClass="btn-save" OnClick="btnSaveConfiguration_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

