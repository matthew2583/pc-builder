using System;
using System.Collections.Generic;
using System.ServiceModel;
using PCBuilderLibrary.Components;

namespace PcBuilderWebApp.Services
{
    // WCF-интерфейс для получения данных о компонентах (используется извне через службу)
    [ServiceContract]
    public interface IComponentService
    {
        // Возвращает список компонентов указанного типа (CPU, GPU и т.д.)
        [OperationContract]
        List<Component> GetComponentsByType(string componentType);

        // Возвращает все компоненты
        [OperationContract]
        List<Component> GetAllComponents();

        // Возвращает конкретный компонент по ID и типу
        [OperationContract]
        Component GetComponentById(int id, string componentType);
    }
}

