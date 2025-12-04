<%@ Page Title="Конструктор ПК" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PcBuilderWebApp.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .main-container {
            display: flex;
            gap: 20px;
        }
        .components-area {
            flex: 1;
        }
        .current-build-panel {
            width: 320px;
            background: #f8f9fa;
            border: 1px solid #e0e0e0;
            border-radius: 8px;
            padding: 15px;
            position: sticky;
            top: 20px;
            align-self: flex-start;
            height: fit-content;
        }
        .current-build-panel h3 {
            margin-top: 0;
            margin-bottom: 10px;
            color: #667eea;
            font-size: 16px;
            border-bottom: 2px solid #667eea;
            padding-bottom: 6px;
        }
        .build-summary {
            margin: 10px 0;
            padding: 12px;
            background: white;
            border-radius: 6px;
            border: 1px solid #e0e0e0;
        }
        .build-summary p {
            margin: 6px 0;
            font-size: 13px;
        }
        .build-summary strong {
            color: #333;
        }
        .build-item {
            padding: 6px 0;
            border-bottom: 1px solid #f0f0f0;
            font-size: 12px;
        }
        .build-item:last-child {
            border-bottom: none;
        }
        .build-item-name {
            color: #333;
            font-weight: 500;
        }
        .build-item-empty {
            color: #999;
            font-style: italic;
        }
        .btn-view-full {
            background: #667eea;
            color: white;
            border: none;
            padding: 8px 16px;
            border-radius: 6px;
            cursor: pointer;
            width: 100%;
            margin-top: 10px;
            font-weight: 500;
            text-decoration: none;
            display: block;
            text-align: center;
            transition: background-color 0.2s;
            font-size: 13px;
        }
        .btn-view-full:hover {
            background: #5568d3;
        }
        .compatibility-warning {
            margin: 10px 0;
            padding: 8px 10px;
            background: #fff3cd;
            border: 2px solid #ffc107;
            border-radius: 6px;
            display: flex;
            align-items: center;
            gap: 8px;
        }
        .warning-icon {
            font-size: 18px;
            flex-shrink: 0;
        }
        .warning-text {
            flex: 1;
        }
        .warning-text strong {
            color: #856404;
            font-size: 12px;
        }
        .component-section {
            margin: 25px 0;
            padding: 20px;
            border: 1px solid #e0e0e0;
            border-radius: 8px;
            background: #fafafa;
        }
        .component-section h2 {
            color: #667eea;
            margin-top: 0;
            font-size: 20px;
            font-weight: 600;
            padding-bottom: 8px;
            border-bottom: 1px solid #e0e0e0;
        }
        .component-list {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
            gap: 15px;
            margin-top: 15px;
        }
        .component-item {
            border: 1px solid #ddd;
            padding: 15px;
            background: white;
            border-radius: 6px;
            transition: box-shadow 0.2s;
        }
        .component-item:hover {
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
            border-color: #667eea;
        }
        .component-item h4 {
            margin: 0 0 8px 0;
            color: #333;
            font-size: 16px;
            font-weight: 600;
        }
        .component-item p {
            margin: 6px 0;
            color: #666;
            font-size: 13px;
        }
        .component-item p strong {
            color: #333;
        }
        .btn-select {
            background: #667eea;
            color: white;
            border: none;
            padding: 8px 16px;
            border-radius: 5px;
            cursor: pointer;
            margin-top: 10px;
            font-weight: 500;
            width: 100%;
            transition: background-color 0.2s;
        }
        .btn-select:hover {
            background: #5568d3;
        }
        .section-filters {
            margin-bottom: 15px;
            padding: 12px;
            background: #f8f9fa;
            border-radius: 5px;
            border: 1px solid #e0e0e0;
            display: flex;
            align-items: center;
            flex-wrap: wrap;
            gap: 8px;
        }
        .section-filters label {
            font-weight: 500;
            color: #555;
            font-size: 13px;
        }
        .filter-dropdown,
        .filter-input {
            padding: 5px 8px;
            border: 1px solid #ddd;
            border-radius: 4px;
            font-size: 13px;
        }
        .filter-dropdown:focus,
        .filter-input:focus {
            outline: none;
            border-color: #667eea;
        }
        .btn-filter-small {
            background: #667eea;
            color: white;
            border: none;
            padding: 5px 12px;
            border-radius: 4px;
            cursor: pointer;
            font-weight: 500;
            font-size: 13px;
            transition: background-color 0.2s;
        }
        .btn-filter-small:hover {
            background: #5568d3;
        }
        .btn-reset {
            background: #999;
            color: white;
            border: none;
            padding: 5px 12px;
            border-radius: 4px;
            cursor: pointer;
            font-weight: 500;
            font-size: 13px;
            transition: background-color 0.2s;
        }
        .btn-reset:hover {
            background: #777;
        }
        .validator-error {
            font-size: 11px;
            margin-left: 5px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Выбор компонентов для сборки ПК</h1>
    
    <div class="main-container">
        <div class="components-area">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

            <div class="component-section">
                <h2>Процессоры (CPU)</h2>
                <div class="section-filters">
                    <asp:Label ID="lblCPUManufacturer" runat="server" Text="Производитель:"></asp:Label>
                    <asp:DropDownList ID="ddlCPUManufacturer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCPUManufacturer_SelectedIndexChanged" CssClass="filter-dropdown">
                        <asp:ListItem Value="">Все</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblCPUPrice" runat="server" Text="Цена до:" style="margin-left: 15px;"></asp:Label>
                    <asp:TextBox ID="txtCPUPrice" runat="server" Width="100px" CssClass="filter-input"></asp:TextBox>
                    <asp:CompareValidator ID="cvCPUPrice" runat="server" 
                        ControlToValidate="txtCPUPrice" 
                        Type="Integer" 
                        Operator="DataTypeCheck" 
                        ErrorMessage="Число"
                        Display="Dynamic"
                        ForeColor="Red"
                        CssClass="validator-error"></asp:CompareValidator>
                    <asp:Button ID="btnCPUFilter" runat="server" Text="Применить" OnClick="btnCPUFilter_Click" CssClass="btn-filter-small" style="margin-left: 10px;" />
                    <asp:Button ID="btnCPUReset" runat="server" Text="Сбросить" OnClick="btnCPUReset_Click" CssClass="btn-reset" style="margin-left: 5px;" />
                </div>
                <div class="component-list">
                    <asp:Repeater ID="rptCPU" runat="server" OnItemCommand="rptCPU_ItemCommand">
                        <ItemTemplate>
                            <div class="component-item">
                                <h4><%# Eval("Manufacturer") %> <%# Eval("Name") %></h4>
                                <p><strong>Цена:</strong> <%# Eval("Price", "{0:N0}") %> руб.</p>
                                <p><%# ((PCBuilderLibrary.Components.CPU)Container.DataItem).ComponentInfoForDisplay %></p>
                                <asp:Button ID="btnSelect" runat="server" Text="Выбрать" CssClass="btn-select" 
                                    CommandName="Select" CommandArgument='<%# Eval("Name") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="component-section">
                <h2>Видеокарты (GPU)</h2>
                <div class="section-filters">
                    <asp:Label ID="lblGPUManufacturer" runat="server" Text="Производитель:"></asp:Label>
                    <asp:DropDownList ID="ddlGPUManufacturer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGPUManufacturer_SelectedIndexChanged" CssClass="filter-dropdown">
                        <asp:ListItem Value="">Все</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblGPUPrice" runat="server" Text="Цена до:" style="margin-left: 15px;"></asp:Label>
                    <asp:TextBox ID="txtGPUPrice" runat="server" Width="100px" CssClass="filter-input"></asp:TextBox>
                    <asp:CompareValidator ID="cvGPUPrice" runat="server" 
                        ControlToValidate="txtGPUPrice" 
                        Type="Integer" 
                        Operator="DataTypeCheck" 
                        ErrorMessage="Число"
                        Display="Dynamic"
                        ForeColor="Red"
                        CssClass="validator-error"></asp:CompareValidator>
                    <asp:Button ID="btnGPUFilter" runat="server" Text="Применить" OnClick="btnGPUFilter_Click" CssClass="btn-filter-small" style="margin-left: 10px;" />
                    <asp:Button ID="btnGPUReset" runat="server" Text="Сбросить" OnClick="btnGPUReset_Click" CssClass="btn-reset" style="margin-left: 5px;" />
                </div>
                <div class="component-list">
                    <asp:Repeater ID="rptGPU" runat="server" OnItemCommand="rptGPU_ItemCommand">
                        <ItemTemplate>
                            <div class="component-item">
                                <h4><%# Eval("Manufacturer") %> <%# Eval("Name") %></h4>
                                <p><strong>Цена:</strong> <%# Eval("Price", "{0:N0}") %> руб.</p>
                                <p><%# ((PCBuilderLibrary.Components.GPU)Container.DataItem).ComponentInfoForDisplay %></p>
                                <asp:Button ID="btnSelect" runat="server" Text="Выбрать" CssClass="btn-select" 
                                    CommandName="Select" CommandArgument='<%# Eval("Name") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="component-section">
                <h2>Материнские платы</h2>
                <div class="section-filters">
                    <asp:Label ID="lblMBManufacturer" runat="server" Text="Производитель:"></asp:Label>
                    <asp:DropDownList ID="ddlMBManufacturer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMBManufacturer_SelectedIndexChanged" CssClass="filter-dropdown">
                        <asp:ListItem Value="">Все</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblMBPrice" runat="server" Text="Цена до:" style="margin-left: 15px;"></asp:Label>
                    <asp:TextBox ID="txtMBPrice" runat="server" Width="100px" CssClass="filter-input"></asp:TextBox>
                    <asp:CompareValidator ID="cvMBPrice" runat="server" 
                        ControlToValidate="txtMBPrice" 
                        Type="Integer" 
                        Operator="DataTypeCheck" 
                        ErrorMessage="Число"
                        Display="Dynamic"
                        ForeColor="Red"
                        CssClass="validator-error"></asp:CompareValidator>
                    <asp:Button ID="btnMBFilter" runat="server" Text="Применить" OnClick="btnMBFilter_Click" CssClass="btn-filter-small" style="margin-left: 10px;" />
                    <asp:Button ID="btnMBReset" runat="server" Text="Сбросить" OnClick="btnMBReset_Click" CssClass="btn-reset" style="margin-left: 5px;" />
                </div>
                <div class="component-list">
                    <asp:Repeater ID="rptMotherboard" runat="server" OnItemCommand="rptMotherboard_ItemCommand">
                        <ItemTemplate>
                            <div class="component-item">
                                <h4><%# Eval("Manufacturer") %> <%# Eval("Name") %></h4>
                                <p><strong>Цена:</strong> <%# Eval("Price", "{0:N0}") %> руб.</p>
                                <p><%# ((PCBuilderLibrary.Components.Motherboard)Container.DataItem).ComponentInfoForDisplay %></p>
                                <asp:Button ID="btnSelect" runat="server" Text="Выбрать" CssClass="btn-select" 
                                    CommandName="Select" CommandArgument='<%# Eval("Name") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="component-section">
                <h2>Оперативная память (RAM)</h2>
                <div class="section-filters">
                    <asp:Label ID="lblRAMManufacturer" runat="server" Text="Производитель:"></asp:Label>
                    <asp:DropDownList ID="ddlRAMManufacturer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRAMManufacturer_SelectedIndexChanged" CssClass="filter-dropdown">
                        <asp:ListItem Value="">Все</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblRAMPrice" runat="server" Text="Цена до:" style="margin-left: 15px;"></asp:Label>
                    <asp:TextBox ID="txtRAMPrice" runat="server" Width="100px" CssClass="filter-input"></asp:TextBox>
                    <asp:CompareValidator ID="cvRAMPrice" runat="server" 
                        ControlToValidate="txtRAMPrice" 
                        Type="Integer" 
                        Operator="DataTypeCheck" 
                        ErrorMessage="Число"
                        Display="Dynamic"
                        ForeColor="Red"
                        CssClass="validator-error"></asp:CompareValidator>
                    <asp:Button ID="btnRAMFilter" runat="server" Text="Применить" OnClick="btnRAMFilter_Click" CssClass="btn-filter-small" style="margin-left: 10px;" />
                    <asp:Button ID="btnRAMReset" runat="server" Text="Сбросить" OnClick="btnRAMReset_Click" CssClass="btn-reset" style="margin-left: 5px;" />
                </div>
                <div class="component-list">
                    <asp:Repeater ID="rptRAM" runat="server" OnItemCommand="rptRAM_ItemCommand">
                        <ItemTemplate>
                            <div class="component-item">
                                <h4><%# Eval("Manufacturer") %> <%# Eval("Name") %></h4>
                                <p><strong>Цена:</strong> <%# Eval("Price", "{0:N0}") %> руб.</p>
                                <p><%# ((PCBuilderLibrary.Components.RAM)Container.DataItem).ComponentInfoForDisplay %></p>
                                <asp:Button ID="btnSelect" runat="server" Text="Выбрать" CssClass="btn-select" 
                                    CommandName="Select" CommandArgument='<%# Eval("Name") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="component-section">
                <h2>Блоки питания (PSU)</h2>
                <div class="section-filters">
                    <asp:Label ID="lblPSUManufacturer" runat="server" Text="Производитель:"></asp:Label>
                    <asp:DropDownList ID="ddlPSUManufacturer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPSUManufacturer_SelectedIndexChanged" CssClass="filter-dropdown">
                        <asp:ListItem Value="">Все</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblPSUPrice" runat="server" Text="Цена до:" style="margin-left: 15px;"></asp:Label>
                    <asp:TextBox ID="txtPSUPrice" runat="server" Width="100px" CssClass="filter-input"></asp:TextBox>
                    <asp:CompareValidator ID="cvPSUPrice" runat="server" 
                        ControlToValidate="txtPSUPrice" 
                        Type="Integer" 
                        Operator="DataTypeCheck" 
                        ErrorMessage="Число"
                        Display="Dynamic"
                        ForeColor="Red"
                        CssClass="validator-error"></asp:CompareValidator>
                    <asp:Button ID="btnPSUFilter" runat="server" Text="Применить" OnClick="btnPSUFilter_Click" CssClass="btn-filter-small" style="margin-left: 10px;" />
                    <asp:Button ID="btnPSUReset" runat="server" Text="Сбросить" OnClick="btnPSUReset_Click" CssClass="btn-reset" style="margin-left: 5px;" />
                </div>
                <div class="component-list">
                    <asp:Repeater ID="rptPSU" runat="server" OnItemCommand="rptPSU_ItemCommand">
                        <ItemTemplate>
                            <div class="component-item">
                                <h4><%# Eval("Manufacturer") %> <%# Eval("Name") %></h4>
                                <p><strong>Цена:</strong> <%# Eval("Price", "{0:N0}") %> руб.</p>
                                <p><%# ((PCBuilderLibrary.Components.PSU)Container.DataItem).ComponentInfoForDisplay %></p>
                                <asp:Button ID="btnSelect" runat="server" Text="Выбрать" CssClass="btn-select" 
                                    CommandName="Select" CommandArgument='<%# Eval("Name") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="component-section">
                <h2>Накопители</h2>
                <div class="section-filters">
                    <asp:Label ID="lblStorageManufacturer" runat="server" Text="Производитель:"></asp:Label>
                    <asp:DropDownList ID="ddlStorageManufacturer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStorageManufacturer_SelectedIndexChanged" CssClass="filter-dropdown">
                        <asp:ListItem Value="">Все</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblStoragePrice" runat="server" Text="Цена до:" style="margin-left: 15px;"></asp:Label>
                    <asp:TextBox ID="txtStoragePrice" runat="server" Width="100px" CssClass="filter-input"></asp:TextBox>
                    <asp:CompareValidator ID="cvStoragePrice" runat="server" 
                        ControlToValidate="txtStoragePrice" 
                        Type="Integer" 
                        Operator="DataTypeCheck" 
                        ErrorMessage="Число"
                        Display="Dynamic"
                        ForeColor="Red"
                        CssClass="validator-error"></asp:CompareValidator>
                    <asp:Button ID="btnStorageFilter" runat="server" Text="Применить" OnClick="btnStorageFilter_Click" CssClass="btn-filter-small" style="margin-left: 10px;" />
                    <asp:Button ID="btnStorageReset" runat="server" Text="Сбросить" OnClick="btnStorageReset_Click" CssClass="btn-reset" style="margin-left: 5px;" />
                </div>
                <div class="component-list">
                    <asp:Repeater ID="rptStorage" runat="server" OnItemCommand="rptStorage_ItemCommand">
                        <ItemTemplate>
                            <div class="component-item">
                                <h4><%# Eval("Manufacturer") %> <%# Eval("Name") %></h4>
                                <p><strong>Цена:</strong> <%# Eval("Price", "{0:N0}") %> руб.</p>
                                <p><%# ((PCBuilderLibrary.Components.Storage)Container.DataItem).ComponentInfoForDisplay %></p>
                                <asp:Button ID="btnSelect" runat="server" Text="Выбрать" CssClass="btn-select" 
                                    CommandName="Select" CommandArgument='<%# Eval("Name") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="component-section">
                <h2>Кулеры</h2>
                <div class="section-filters">
                    <asp:Label ID="lblCoolerManufacturer" runat="server" Text="Производитель:"></asp:Label>
                    <asp:DropDownList ID="ddlCoolerManufacturer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCoolerManufacturer_SelectedIndexChanged" CssClass="filter-dropdown">
                        <asp:ListItem Value="">Все</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblCoolerPrice" runat="server" Text="Цена до:" style="margin-left: 15px;"></asp:Label>
                    <asp:TextBox ID="txtCoolerPrice" runat="server" Width="100px" CssClass="filter-input"></asp:TextBox>
                    <asp:CompareValidator ID="cvCoolerPrice" runat="server" 
                        ControlToValidate="txtCoolerPrice" 
                        Type="Integer" 
                        Operator="DataTypeCheck" 
                        ErrorMessage="Число"
                        Display="Dynamic"
                        ForeColor="Red"
                        CssClass="validator-error"></asp:CompareValidator>
                    <asp:Button ID="btnCoolerFilter" runat="server" Text="Применить" OnClick="btnCoolerFilter_Click" CssClass="btn-filter-small" style="margin-left: 10px;" />
                    <asp:Button ID="btnCoolerReset" runat="server" Text="Сбросить" OnClick="btnCoolerReset_Click" CssClass="btn-reset" style="margin-left: 5px;" />
                </div>
                <div class="component-list">
                    <asp:Repeater ID="rptCooler" runat="server" OnItemCommand="rptCooler_ItemCommand">
                        <ItemTemplate>
                            <div class="component-item">
                                <h4><%# Eval("Manufacturer") %> <%# Eval("Name") %></h4>
                                <p><strong>Цена:</strong> <%# Eval("Price", "{0:N0}") %> руб.</p>
                                <p><%# ((PCBuilderLibrary.Components.Cooler)Container.DataItem).ComponentInfoForDisplay %></p>
                                <asp:Button ID="btnSelect" runat="server" Text="Выбрать" CssClass="btn-select" 
                                    CommandName="Select" CommandArgument='<%# Eval("Name") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="component-section">
                <h2>Корпуса</h2>
                <div class="section-filters">
                    <asp:Label ID="lblCaseManufacturer" runat="server" Text="Производитель:"></asp:Label>
                    <asp:DropDownList ID="ddlCaseManufacturer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCaseManufacturer_SelectedIndexChanged" CssClass="filter-dropdown">
                        <asp:ListItem Value="">Все</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblCasePrice" runat="server" Text="Цена до:" style="margin-left: 15px;"></asp:Label>
                    <asp:TextBox ID="txtCasePrice" runat="server" Width="100px" CssClass="filter-input"></asp:TextBox>
                    <asp:CompareValidator ID="cvCasePrice" runat="server" 
                        ControlToValidate="txtCasePrice" 
                        Type="Integer" 
                        Operator="DataTypeCheck" 
                        ErrorMessage="Число"
                        Display="Dynamic"
                        ForeColor="Red"
                        CssClass="validator-error"></asp:CompareValidator>
                    <asp:Button ID="btnCaseFilter" runat="server" Text="Применить" OnClick="btnCaseFilter_Click" CssClass="btn-filter-small" style="margin-left: 10px;" />
                    <asp:Button ID="btnCaseReset" runat="server" Text="Сбросить" OnClick="btnCaseReset_Click" CssClass="btn-reset" style="margin-left: 5px;" />
                </div>
                <div class="component-list">
                    <asp:Repeater ID="rptCase" runat="server" OnItemCommand="rptCase_ItemCommand">
                        <ItemTemplate>
                            <div class="component-item">
                                <h4><%# Eval("Manufacturer") %> <%# Eval("Name") %></h4>
                                <p><strong>Цена:</strong> <%# Eval("Price", "{0:N0}") %> руб.</p>
                                <p><%# ((PCBuilderLibrary.Components.Case)Container.DataItem).ComponentInfoForDisplay %></p>
                                <asp:Button ID="btnSelect" runat="server" Text="Выбрать" CssClass="btn-select" 
                                    CommandName="Select" CommandArgument='<%# Eval("Name") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        
        <div class="current-build-panel">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <h3>Текущая сборка</h3>
                    <div class="build-summary">
                        <div class="build-item">
                            <strong>Процессор:</strong><br />
                            <span id="spanCPU" runat="server" class="build-item-empty">Не выбран</span>
                        </div>
                        <div class="build-item">
                            <strong>Видеокарта:</strong><br />
                            <span id="spanGPU" runat="server" class="build-item-empty">Не выбрана</span>
                        </div>
                        <div class="build-item">
                            <strong>Материнская плата:</strong><br />
                            <span id="spanMotherboard" runat="server" class="build-item-empty">Не выбрана</span>
                        </div>
                        <div class="build-item">
                            <strong>Память:</strong><br />
                            <span id="spanRAM" runat="server" class="build-item-empty">Не выбрана</span>
                        </div>
                        <div class="build-item">
                            <strong>Блок питания:</strong><br />
                            <span id="spanPSU" runat="server" class="build-item-empty">Не выбран</span>
                        </div>
                        <div class="build-item">
                            <strong>Накопитель:</strong><br />
                            <span id="spanStorage" runat="server" class="build-item-empty">Не выбран</span>
                        </div>
                        <div class="build-item">
                            <strong>Кулер:</strong><br />
                            <span id="spanCooler" runat="server" class="build-item-empty">Не выбран</span>
                        </div>
                        <div class="build-item">
                            <strong>Корпус:</strong><br />
                            <span id="spanCase" runat="server" class="build-item-empty">Не выбран</span>
                        </div>
                    </div>
                    <asp:Panel ID="pnlCompatibilityWarning" runat="server" CssClass="compatibility-warning" Visible="false">
                        <div class="warning-icon">⚠️</div>
                        <div class="warning-text">
                            <strong>Есть проблемы совместимости!</strong>
                        </div>
                    </asp:Panel>
                    <div class="build-summary">
                        <p><strong>Общая стоимость:</strong> <asp:Label ID="lblQuickTotalCost" runat="server" Text="0"></asp:Label> руб.</p>
                        <p><strong>Энергопотребление:</strong> <asp:Label ID="lblQuickTotalPower" runat="server" Text="0"></asp:Label> Вт</p>
                    </div>
                    <asp:HyperLink ID="lnkViewFull" runat="server" NavigateUrl="~/Build.aspx" CssClass="btn-view-full">Подробнее →</asp:HyperLink>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

