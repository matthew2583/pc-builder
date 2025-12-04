using System;

namespace PCBuilderLibrary.Components
{
    //Определяес специфичные свойства для материнских плат
    public class Motherboard : Component
    {
        public string Socket { get; set; }
        public string Chipset { get; set; }
        public string FormFactor { get; set; }
        public string RAMType { get; set; }
        public int MaxRAM { get; set; }
        public int RAMSlots { get; set; }
        public int NumM2Slots { get; set; }
        public int NumSataSlots { get; set; }

        //Показываем основые свойства для материнской платы при выборе из списка
        public string ComponentInfoForDisplay
        {
            get 
            { 
                return $"{Socket}, {Chipset}, {RAMType}, {FormFactor}, {PowerConsumption} Вт"; 
            }
        }
    }
}
