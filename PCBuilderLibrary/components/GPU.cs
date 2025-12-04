using System;

namespace PCBuilderLibrary.Components
{
    //Определяем основные свойства для видеокарт
    public class GPU : Component
    {
        public string Model { get; set; }
        public int MemorySize { get; set; }
        public string MemoryType { get; set; }
        public string PCIExpressVer { get; set; }
        public int GpuLength { get; set; }
        
        //Показываем основные свойства при выборе видеокарты из списка
        public string ComponentInfoForDisplay
        {
            get 
            { 
                return $"{MemorySize} ГБ {MemoryType}, PCI-E {PCIExpressVer}, {PowerConsumption} Вт"; 
            }
        }
    }
}
