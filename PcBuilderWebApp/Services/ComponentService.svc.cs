using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using PCBuilderLibrary.Components;

namespace PcBuilderWebApp.Services
{
    // Реализация WCF-службы для получения компонентов — делегирует работу DatabaseHelper
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ComponentService : IComponentService
    {
        // Возвращает список компонентов по типу, используя DatabaseHelper
        public List<Component> GetComponentsByType(string componentType)
        {
            var dbHelper = new DatabaseHelper();
            return dbHelper.GetComponentsByType(componentType);
        }

        // Возвращает все компоненты
        public List<Component> GetAllComponents()
        {
            var dbHelper = new DatabaseHelper();
            return dbHelper.GetAllComponents();
        }

        // Возвращает компонент по ID и типу 
        public Component GetComponentById(int id, string componentType)
        {
            var dbHelper = new DatabaseHelper();
            var components = dbHelper.GetComponentsByType(componentType);
            return components.FirstOrDefault();
        }
    }
}

