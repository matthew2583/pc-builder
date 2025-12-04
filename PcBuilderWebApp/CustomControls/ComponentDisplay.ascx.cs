using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PcBuilderWebApp.CustomControls
{
    // Пользовательский контрол для отображения краткой информации о компоненте
    public partial class ComponentDisplay : UserControl
    {
        // Свойства, заполняемые родительской страницей/повторителем
        public string ComponentName { get; set; }
        public string Manufacturer { get; set; }
        public int Price { get; set; }
        public string ComponentType { get; set; }

        // Инициализация контролa: выводит имя, производителя и цену
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblComponentName.Text = ComponentName;
                lblManufacturer.Text = Manufacturer;
                lblPrice.Text = Price.ToString("N0");
                btnSelect.CommandArgument = ComponentName;
                btnSelect.CommandName = "Select";
            }
        }
    }
}

