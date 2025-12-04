using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace PCBuilderLibrary.Components
{

    //Определяем общие свойства, которые будут у каждого компонента
    public abstract class Component
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int Price { get; set; }
        public int PowerConsumption { get; set; }
    }
}
