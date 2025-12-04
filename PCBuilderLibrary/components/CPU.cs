using System;

namespace PCBuilderLibrary.Components
{
    //Определяем свойства, специфичны для процессора
    public class CPU: Component
    {
        public string Socket { get; set; }
        public int Cores { get; set; }
        public double BaseClockSpeed { get; set; }
        public bool HasIntegratedGraphic { get; set; }
        //Не определяем тепловыделение, так как оно равно энергопотреблению, а оно описано в базовом классе

        //Показывает основные характеристика около каждого из процессоров при выборе из списка
        public string ComponentInfoForDisplay
        {
            get 
            { 
                return $"{Cores} ядер, {BaseClockSpeed} ГГц, {Socket}, {PowerConsumption} Вт"; 
            }
        }
    }
}
