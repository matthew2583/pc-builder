using System;

namespace PCBuilderLibrary.Components
{
    //Определяем специфичные свойства для корпуса
    public class Case : Component
    {
        public string Type { get; set; }
        public string FormFactor { get; set; }
        public int MaxGPULength { get; set; }
        public int MaxCoolerHeight { get; set; }
        //Энегопотребления у корпуса нет, так что в базе данных там стоит значение 0

        //Показываем основнуб информацию о корпусе при выборе из списка
        public string ComponentInfoForDisplay
        {
            get 
            { 
                return $"{Type}, {FormFactor}"; 
            }
        }
    }
}
