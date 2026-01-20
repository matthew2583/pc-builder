using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using PCBuilderLibrary;

namespace PcBuilderWebApp
{
    // Страница просмотра и управления сохранёнными конфигурациями
    public partial class Configurations : System.Web.UI.Page
    {
        // Инициализация страницы и загрузка списка конфигураций при первом заходе
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadConfigurations();
            }
        }

        // Загружает сохранённые конфигурации из базы и обновляет UI
        private void LoadConfigurations()
        {
            using (var dbHelper = new DatabaseHelper())
            {
                var configurations = dbHelper.GetSavedConfigurations();
            
                if (configurations == null || configurations.Count == 0)
                {
                    pnlEmptyMessage.Visible = true;
                    rptConfigurations.Visible = false;
                }
                else
                {
                    pnlEmptyMessage.Visible = false;
                    rptConfigurations.Visible = true;
                    rptConfigurations.DataSource = configurations;
                    rptConfigurations.DataBind();
                }
            }
        }

        // Обработчик команд в списке конфигураций: загрузка или удаление
        protected void rptConfigurations_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Load")
            {
                int configId = int.Parse(e.CommandArgument.ToString());
                using (var dbHelper = new DatabaseHelper())
                {
                    var config = dbHelper.LoadConfiguration(configId);
                    Session["CurrentConfiguration"] = config;
                    Response.Redirect("Build.aspx");
                }
            }
            else if (e.CommandName == "Delete")
            {
                int configId = int.Parse(e.CommandArgument.ToString());
                using (var context = new PCBuilderDataContext())
                {
                    var config = context.Configurations.FirstOrDefault(c => c.Id == configId);
                    if (config != null)
                    {
                        context.Configurations.DeleteOnSubmit(config);
                        context.SubmitChanges();
                    }
                }
                LoadConfigurations();
            }
        }
    }
}

