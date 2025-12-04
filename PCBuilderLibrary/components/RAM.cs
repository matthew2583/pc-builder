using System;

namespace PCBuilderLibrary.Components
{
    //Определяем специфичные свойства для оперативной памяти
    public class RAM: Component
    {
        public string Type { get; set; }
        public int Capacity { get; set; }
        public int Speed { get; set; }
        public int Modules { get; set; }

        //Показываем основные свойства оперативной памяти при выборе из списка
        public string ComponentInfoForDisplay
        {
            get 
            {
                string capacityPerModule = Modules > 0 ? $"{Capacity / Modules}ГБ" : $"{Capacity}ГБ";
                string moduleCount = Modules > 1 ? $"{Modules}x" : "";
                return $"{moduleCount}{capacityPerModule} {Type} {Speed} МГц, {PowerConsumption} Вт"; 
            }
        }
    }
}
