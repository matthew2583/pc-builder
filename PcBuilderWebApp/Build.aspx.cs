using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using PCBuilderLibrary;
using PCBuilderLibrary.Components;

namespace PcBuilderWebApp
{
    // Страница отображения текущей сборки и работы с ней
    public partial class Build : System.Web.UI.Page
    {
        // Загрузка страницы: подгружаем текущую конфигурацию из сессии
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCurrentConfiguration();
        }

        // Загружает текущую конфигурацию из сессии и обновляет UI
        private void LoadCurrentConfiguration()
        {
            PCConfiguration config = Session["CurrentConfiguration"] as PCConfiguration;
            if (config == null)
            {
                config = new PCConfiguration();
                Session["CurrentConfiguration"] = config;
            }

            DisplayComponent(config.SelectCPU, pnlCPU, lblCPU, btnRemoveCPU);
            DisplayComponent(config.SelectGPU, pnlGPU, lblGPU, btnRemoveGPU);
            DisplayComponent(config.SelectMotherboard, pnlMotherboard, lblMotherboard, btnRemoveMotherboard);
            DisplayComponent(config.SelectRAM, pnlRAM, lblRAM, btnRemoveRAM);
            DisplayComponent(config.SelectPSU, pnlPSU, lblPSU, btnRemovePSU);
            DisplayComponent(config.SelectStorage, pnlStorage, lblStorage, btnRemoveStorage);
            DisplayComponent(config.SelectCooler, pnlCooler, lblCooler, btnRemoveCooler);
            DisplayComponent(config.SelectCase, pnlCase, lblCase, btnRemoveCase);

            lblTotalCost.Text = config.TotalCost.ToString("N0");
            lblTotalPower.Text = config.TotalPower.ToString();

            CheckCompatibility(config);
        }

        // Обновляет визуальный блок конкретного типа компонента
        private void DisplayComponent(Component component, Panel panel, Label label, LinkButton button)
        {
            if (component != null)
            {
                panel.CssClass = "component-selected";
                label.Text = $"{component.Manufacturer} {component.Name} ({component.Price:N0} руб.)";
                button.Visible = true;
            }
            else
            {
                panel.CssClass = "component-empty";
                label.Text = "Не выбран";
                button.Visible = false;
            }
        }

        // Обработчик удаления выбранного компонента из конфигурации
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            LinkButton button = sender as LinkButton;
            if (button == null)
            {
                return;
            }

            string componentType = button.CommandArgument;
            if (string.IsNullOrEmpty(componentType))
                return;

            PCConfiguration config = Session["CurrentConfiguration"] as PCConfiguration;
            if (config == null)
            {
                config = new PCConfiguration();
                Session["CurrentConfiguration"] = config;
            }

            switch (componentType.ToLower())
            {
                case "cpu": config.SelectCPU = null; break;
                case "gpu": config.SelectGPU = null; break;
                case "motherboard": config.SelectMotherboard = null; break;
                case "ram": config.SelectRAM = null; break;
                case "psu": config.SelectPSU = null; break;
                case "storage": config.SelectStorage = null; break;
                case "cooler": config.SelectCooler = null; break;
                case "case": config.SelectCase = null; break;
            }
            
            Session["CurrentConfiguration"] = config;
            LoadCurrentConfiguration();
            UpdatePanel1.Update();
        }

        // Отображает результаты проверки совместимости для текущей конфигурации
        private void CheckCompatibility(PCConfiguration config)
        {
            var issues = config.CheckCompatibility();

            if (issues.Count > 0)
            {
                divCompatibility.Attributes["class"] = "compatibility-issues";
                divCompatibility.InnerHtml = "<h3>Проблемы совместимости:</h3><ul>";
                foreach (var issue in issues)
                {
                    divCompatibility.InnerHtml += $"<li>{issue}</li>";
                }
                divCompatibility.InnerHtml += "</ul>";
            }
            else
            {
                divCompatibility.Attributes["class"] = "compatibility-ok";
                divCompatibility.InnerHtml = "<h3>✓ Все компоненты совместимы!</h3>";
            }
        }

        // Сохраняет текущую конфигурацию в базу данных
        protected void btnSaveConfiguration_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                PCConfiguration config = Session["CurrentConfiguration"] as PCConfiguration;
                if (config != null)
                {
                    config.BuildName = txtConfigName.Text;
                    using (var dbHelper = new DatabaseHelper())
                    {
                        dbHelper.SaveConfiguration(config, txtConfigName.Text);
                    }
                    
                    Response.Write("<script>alert('Сборка сохранена успешно!');</script>");
                    Response.Redirect("Configurations.aspx");
                }
            }
        }
    }
}

