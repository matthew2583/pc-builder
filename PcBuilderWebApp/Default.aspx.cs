using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using PCBuilderLibrary;
using PCBuilderLibrary.Components;

namespace PcBuilderWebApp
{
    // Главная страница приложения: список компонентов и быстрый доступ к текущей сборке
    public partial class Default : System.Web.UI.Page
    {
        // Инициализация страницы: загружаем данные и настраиваем панели 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllComponents();
                LoadManufacturersForCategories();
                LoadComponents();
                UpdateCurrentBuildPanel();
            }
        }

        // Загружает все компоненты из БД один раз и сохраняет в Application для повторного использования
        private void LoadAllComponents()
        {
            if (Application["AllComponents"] != null)
            {
                return;
            }

            using (var helper = new DatabaseHelper())
            {
                var allComponents = helper.GetAllComponents();
                Application["AllComponents"] = allComponents;
            }
        }

        private List<Component> GetCachedComponents()
        {
            var cached = Application["AllComponents"] as List<Component>;
            if (cached == null)
            {
                LoadAllComponents();
                cached = Application["AllComponents"] as List<Component> ?? new List<Component>();
            }
            return cached;
        }

        // Формирует списки производителей для каждой категории компонентов 
        private void LoadManufacturersForCategories()
        {
            var allComponents = GetCachedComponents();

            LoadManufacturersForType(allComponents.OfType<CPU>().ToList(), ddlCPUManufacturer);
            LoadManufacturersForType(allComponents.OfType<GPU>().ToList(), ddlGPUManufacturer);
            LoadManufacturersForType(allComponents.OfType<Motherboard>().ToList(), ddlMBManufacturer);
            LoadManufacturersForType(allComponents.OfType<RAM>().ToList(), ddlRAMManufacturer);
            LoadManufacturersForType(allComponents.OfType<PSU>().ToList(), ddlPSUManufacturer);
            LoadManufacturersForType(allComponents.OfType<Storage>().ToList(), ddlStorageManufacturer);
            LoadManufacturersForType(allComponents.OfType<Cooler>().ToList(), ddlCoolerManufacturer);
            LoadManufacturersForType(allComponents.OfType<Case>().ToList(), ddlCaseManufacturer);
        }

        // Заполняет DropDownList уникальными именами производителей для указанной категории
        private void LoadManufacturersForType<T>(List<T> components, DropDownList ddl) where T : Component
        {
            var manufacturers = components
                .Select(c => c.Manufacturer)
                .Distinct()
                .OrderBy(m => m)
                .ToList();

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("Все", ""));
            foreach (var manufacturer in manufacturers)
            {
                ddl.Items.Add(new ListItem(manufacturer, manufacturer));
            }
        }

        // Загружает и привязывает отфильтрованные списки компонентов к Repeater-ам
        private void LoadComponents()
        {
            var allComponents = GetCachedComponents();

            rptCPU.DataSource = FilterComponents(allComponents.OfType<CPU>().ToList(), ddlCPUManufacturer.SelectedValue, txtCPUPrice.Text);
            rptCPU.DataBind();

            rptGPU.DataSource = FilterComponents(allComponents.OfType<GPU>().ToList(), ddlGPUManufacturer.SelectedValue, txtGPUPrice.Text);
            rptGPU.DataBind();

            rptMotherboard.DataSource = FilterComponents(allComponents.OfType<Motherboard>().ToList(), ddlMBManufacturer.SelectedValue, txtMBPrice.Text);
            rptMotherboard.DataBind();

            rptRAM.DataSource = FilterComponents(allComponents.OfType<RAM>().ToList(), ddlRAMManufacturer.SelectedValue, txtRAMPrice.Text);
            rptRAM.DataBind();

            rptPSU.DataSource = FilterComponents(allComponents.OfType<PSU>().ToList(), ddlPSUManufacturer.SelectedValue, txtPSUPrice.Text);
            rptPSU.DataBind();

            rptStorage.DataSource = FilterComponents(allComponents.OfType<Storage>().ToList(), ddlStorageManufacturer.SelectedValue, txtStoragePrice.Text);
            rptStorage.DataBind();

            rptCooler.DataSource = FilterComponents(allComponents.OfType<Cooler>().ToList(), ddlCoolerManufacturer.SelectedValue, txtCoolerPrice.Text);
            rptCooler.DataBind();

            rptCase.DataSource = FilterComponents(allComponents.OfType<Case>().ToList(), ddlCaseManufacturer.SelectedValue, txtCasePrice.Text);
            rptCase.DataBind();
        }

        // Применяет фильтры производителя и максимальной цены к списку компонентов
        private List<T> FilterComponents<T>(List<T> components, string manufacturer, string maxPriceText) where T : Component
        {
            var filtered = components.AsQueryable();

            if (!string.IsNullOrEmpty(manufacturer))
            {
                filtered = filtered.Where(c => c.Manufacturer == manufacturer);
            }

            if (!string.IsNullOrEmpty(maxPriceText) && int.TryParse(maxPriceText, out int maxPrice))
            {
                filtered = filtered.Where(c => c.Price <= maxPrice);
            }

            return filtered.ToList();
        }

        private void ReloadComponents(bool requireValidation = false)
        {
            if (requireValidation && !Page.IsValid)
            {
                return;
            }

            LoadComponents();
        }

        protected void ddlCPUManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadComponents();
        }

        protected void btnCPUFilter_Click(object sender, EventArgs e)
        {
            ReloadComponents(true);
        }

        protected void ddlGPUManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadComponents();
        }

        protected void btnGPUFilter_Click(object sender, EventArgs e)
        {
            ReloadComponents(true);
        }

        protected void ddlMBManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadComponents();
        }

        protected void btnMBFilter_Click(object sender, EventArgs e)
        {
            ReloadComponents(true);
        }

        protected void ddlRAMManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadComponents();
        }

        protected void btnRAMFilter_Click(object sender, EventArgs e)
        {
            ReloadComponents(true);
        }

        protected void ddlPSUManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadComponents();
        }

        protected void btnPSUFilter_Click(object sender, EventArgs e)
        {
            ReloadComponents(true);
        }

        protected void ddlStorageManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadComponents();
        }

        protected void btnStorageFilter_Click(object sender, EventArgs e)
        {
            ReloadComponents(true);
        }

        protected void ddlCoolerManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadComponents();
        }

        protected void btnCoolerFilter_Click(object sender, EventArgs e)
        {
            ReloadComponents(true);
        }

        protected void ddlCaseManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadComponents();
        }

        protected void btnCaseFilter_Click(object sender, EventArgs e)
        {
            ReloadComponents(true);
        }

        protected void btnCPUReset_Click(object sender, EventArgs e)
        {
            ddlCPUManufacturer.SelectedValue = "";
            txtCPUPrice.Text = "";
            ReloadComponents();
        }

        protected void btnGPUReset_Click(object sender, EventArgs e)
        {
            ddlGPUManufacturer.SelectedValue = "";
            txtGPUPrice.Text = "";
            ReloadComponents();
        }

        protected void btnMBReset_Click(object sender, EventArgs e)
        {
            ddlMBManufacturer.SelectedValue = "";
            txtMBPrice.Text = "";
            ReloadComponents();
        }

        protected void btnRAMReset_Click(object sender, EventArgs e)
        {
            ddlRAMManufacturer.SelectedValue = "";
            txtRAMPrice.Text = "";
            ReloadComponents();
        }

        protected void btnPSUReset_Click(object sender, EventArgs e)
        {
            ddlPSUManufacturer.SelectedValue = "";
            txtPSUPrice.Text = "";
            ReloadComponents();
        }

        protected void btnStorageReset_Click(object sender, EventArgs e)
        {
            ddlStorageManufacturer.SelectedValue = "";
            txtStoragePrice.Text = "";
            ReloadComponents();
        }

        protected void btnCoolerReset_Click(object sender, EventArgs e)
        {
            ddlCoolerManufacturer.SelectedValue = "";
            txtCoolerPrice.Text = "";
            ReloadComponents();
        }

        protected void btnCaseReset_Click(object sender, EventArgs e)
        {
            ddlCaseManufacturer.SelectedValue = "";
            txtCasePrice.Text = "";
            ReloadComponents();
        }

        // Добавляет выбранный компонент в сессионную конфигурацию по типу
        private void AddComponentToSession(Component component, string componentType)
        {
            PCConfiguration config = Session["CurrentConfiguration"] as PCConfiguration;
            if (config == null)
            {
                config = new PCConfiguration();
                Session["CurrentConfiguration"] = config;
            }

            switch (componentType.ToLower())
            {
                case "cpu":
                    config.SelectCPU = component as CPU;
                    break;
                case "gpu":
                    config.SelectGPU = component as GPU;
                    break;
                case "motherboard":
                    config.SelectMotherboard = component as Motherboard;
                    break;
                case "ram":
                    config.SelectRAM = component as RAM;
                    break;
                case "psu":
                    config.SelectPSU = component as PSU;
                    break;
                case "storage":
                    config.SelectStorage = component as Storage;
                    break;
                case "cooler":
                    config.SelectCooler = component as Cooler;
                    break;
                case "case":
                    config.SelectCase = component as Case;
                    break;
            }

            Session["CurrentConfiguration"] = config;
            UpdateCurrentBuildPanel();
            UpdatePanel2.Update();
        }

        // Обновляет панель быстрого просмотра текущей сборки (имена, стоимость, предупреждения)
        private void UpdateCurrentBuildPanel()
        {
            PCConfiguration config = Session["CurrentConfiguration"] as PCConfiguration;
            if (config == null)
            {
                config = new PCConfiguration();
                Session["CurrentConfiguration"] = config;
            }

            spanCPU.InnerText = config.SelectCPU != null ? $"{config.SelectCPU.Manufacturer} {config.SelectCPU.Name}" : "Не выбран";
            spanCPU.Attributes["class"] = config.SelectCPU != null ? "build-item-name" : "build-item-empty";

            spanGPU.InnerText = config.SelectGPU != null ? $"{config.SelectGPU.Manufacturer} {config.SelectGPU.Name}" : "Не выбрана";
            spanGPU.Attributes["class"] = config.SelectGPU != null ? "build-item-name" : "build-item-empty";

            spanMotherboard.InnerText = config.SelectMotherboard != null ? $"{config.SelectMotherboard.Manufacturer} {config.SelectMotherboard.Name}" : "Не выбрана";
            spanMotherboard.Attributes["class"] = config.SelectMotherboard != null ? "build-item-name" : "build-item-empty";

            spanRAM.InnerText = config.SelectRAM != null ? $"{config.SelectRAM.Manufacturer} {config.SelectRAM.Name}" : "Не выбрана";
            spanRAM.Attributes["class"] = config.SelectRAM != null ? "build-item-name" : "build-item-empty";

            spanPSU.InnerText = config.SelectPSU != null ? $"{config.SelectPSU.Manufacturer} {config.SelectPSU.Name}" : "Не выбран";
            spanPSU.Attributes["class"] = config.SelectPSU != null ? "build-item-name" : "build-item-empty";

            spanStorage.InnerText = config.SelectStorage != null ? $"{config.SelectStorage.Manufacturer} {config.SelectStorage.Name}" : "Не выбран";
            spanStorage.Attributes["class"] = config.SelectStorage != null ? "build-item-name" : "build-item-empty";

            spanCooler.InnerText = config.SelectCooler != null ? $"{config.SelectCooler.Manufacturer} {config.SelectCooler.Name}" : "Не выбран";
            spanCooler.Attributes["class"] = config.SelectCooler != null ? "build-item-name" : "build-item-empty";

            spanCase.InnerText = config.SelectCase != null ? $"{config.SelectCase.Manufacturer} {config.SelectCase.Name}" : "Не выбран";
            spanCase.Attributes["class"] = config.SelectCase != null ? "build-item-name" : "build-item-empty";

            lblQuickTotalCost.Text = config.TotalCost.ToString("N0");
            lblQuickTotalPower.Text = config.TotalPower.ToString();

            var compatibilityIssues = config.CheckCompatibility();
            pnlCompatibilityWarning.Visible = compatibilityIssues.Count > 0;
        }

        // Обработчики выбора компонента в списках 
        protected void rptCPU_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                var componentName = e.CommandArgument.ToString();
                var allComponents = GetCachedComponents();
                var component = allComponents.OfType<CPU>()
                    .FirstOrDefault(c => c.Name == componentName);
                if (component != null)
                {
                    AddComponentToSession(component, "CPU");
                }
            }
        }

        protected void rptGPU_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                var componentName = e.CommandArgument.ToString();
                var allComponents = GetCachedComponents();
                var component = allComponents.OfType<GPU>()
                    .FirstOrDefault(c => c.Name == componentName);
                if (component != null)
                {
                    AddComponentToSession(component, "GPU");
                }
            }
        }

        protected void rptMotherboard_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                var componentName = e.CommandArgument.ToString();
                var allComponents = GetCachedComponents();
                var component = allComponents.OfType<Motherboard>()
                    .FirstOrDefault(c => c.Name == componentName);
                if (component != null)
                {
                    AddComponentToSession(component, "Motherboard");
                }
            }
        }

        protected void rptRAM_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                var componentName = e.CommandArgument.ToString();
                var allComponents = GetCachedComponents();
                var component = allComponents.OfType<RAM>()
                    .FirstOrDefault(c => c.Name == componentName);
                if (component != null)
                {
                    AddComponentToSession(component, "RAM");
                }
            }
        }

        protected void rptPSU_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                var componentName = e.CommandArgument.ToString();
                var allComponents = GetCachedComponents();
                var component = allComponents.OfType<PSU>()
                    .FirstOrDefault(c => c.Name == componentName);
                if (component != null)
                {
                    AddComponentToSession(component, "PSU");
                }
            }
        }

        protected void rptStorage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                var componentName = e.CommandArgument.ToString();
                var allComponents = GetCachedComponents();
                var component = allComponents.OfType<Storage>()
                    .FirstOrDefault(c => c.Name == componentName);
                if (component != null)
                {
                    AddComponentToSession(component, "Storage");
                }
            }
        }

        protected void rptCooler_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                var componentName = e.CommandArgument.ToString();
                var allComponents = GetCachedComponents();
                var component = allComponents.OfType<Cooler>()
                    .FirstOrDefault(c => c.Name == componentName);
                if (component != null)
                {
                    AddComponentToSession(component, "Cooler");
                }
            }
        }

        protected void rptCase_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                var componentName = e.CommandArgument.ToString();
                var allComponents = GetCachedComponents();
                var component = allComponents.OfType<Case>()
                    .FirstOrDefault(c => c.Name == componentName);
                if (component != null)
                {
                    AddComponentToSession(component, "Case");
                }
            }
        }
    }
}

