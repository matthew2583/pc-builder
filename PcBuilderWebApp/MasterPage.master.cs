using System;
using System.Web.UI;
using PCBuilderLibrary;

namespace PcBuilderWebApp
{
    // Мастер-страница приложения: общая инициализация сессии и общие элементы интерфейса
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        // При загрузке страницы обеспечиваем наличие сессионной конфигурации
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeSession();
        }

        // Инициализирует объект текущей сборки в сессии, если он отсутствует
        private void InitializeSession()
        {
            if (Session["CurrentConfiguration"] == null)
            {
                Session["CurrentConfiguration"] = new PCConfiguration();
            }
        }
    }
}
