using System;

namespace PCBuilderLibrary.Components
{
    //Определяем специфичные свойства для блоков питания
    public class PSU : Component
    {
        public int Power { get; set; }
        public string EfficiencyRating { get; set; }
        public bool Modular { get; set; }

        //Показываем основную информацию о блоке питания при выборе из списка
        public string ComponentInfoForDisplay
        {
            get 
            { 
                return $"{Power} Вт, {EfficiencyRating}, {(Modular ? "Модульный" : "Немодульный")}"; 
            }
        }
    }
}
