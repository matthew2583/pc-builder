using System;

namespace PCBuilderLibrary.Components
{
    //Определяем специфичные свойства для накопителей
    public class Storage : Component
    {
        public string Type { get; set; }
        public int Capacity { get; set; }
        public string Interface { get; set; }
        public string FormFactor { get; set; }

        //Определяем основные свойства накопителя при выборе из списка
        public string ComponentInfoForDisplay
        {
            get 
            { 
                return $"{Capacity} ГБ {Type}, {Interface}, {PowerConsumption} Вт"; 
            }
        }
    }
}
