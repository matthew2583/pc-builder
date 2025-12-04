using System;
using System.Collections.Generic;

namespace PCBuilderLibrary.Components
{
    //Определяем свойства, которые специфичны для кулера (для процессора)
    public class Cooler : Component
    {
        public string Type { get; set; }
        public int MaxTDP { get; set; }
        public List<string> SupportedSockets { get; set; } = new List<string>();
        public double NoiseLevel { get; set; }
        public int Height { get; set; }

        //Показывает основные свойства при выборе кулера из списка
        public string ComponentInfoForDisplay
        {
            get 
            {
                return $"{Type}, TDP до {MaxTDP} Вт, {NoiseLevel} дБ"; 
            }
        }
    }
}
