using PCBuilderLibrary.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PCBuilderLibrary
{
    //Класс для описания текущей сборки (тех комплектующих, которые пользователь уже выбрал)
    public class PCConfiguration
    {
        //Хранение информации о сборке
        public string BuildName { get; set; } = "Безымянная конфигурация";
        public CPU SelectCPU { get; set; }
        public GPU SelectGPU { get; set; }
        public PSU SelectPSU { get; set; }
        public RAM SelectRAM { get; set; }
        public Case SelectCase { get; set; }
        public Cooler SelectCooler { get; set; }
        public Motherboard SelectMotherboard { get; set; }
        public Storage SelectStorage { get; set; }

        //Вычисляем итоговую стоимость и итоговое энергопотребление
        public int TotalCost => CalculateTotalCost();
        public int TotalPower => CalculateTotalPower();

        public int CalculateTotalCost()
        {
            int total = 0;

            if (SelectCPU != null) total += SelectCPU.Price;
            if (SelectGPU != null) total += SelectGPU.Price;
            if (SelectPSU != null) total += SelectPSU.Price;
            if (SelectRAM != null) total += SelectRAM.Price;
            if (SelectCase != null) total += SelectCase.Price;
            if (SelectCooler != null) total += SelectCooler.Price;
            if (SelectMotherboard != null) total += SelectMotherboard.Price;
            if (SelectStorage != null) total += SelectStorage.Price;

            return total;
        }

        public int CalculateTotalPower()
        {
            int total = 0;

            if (SelectCPU != null) total += SelectCPU.PowerConsumption;
            if (SelectGPU != null) total += SelectGPU.PowerConsumption;
            if (SelectPSU != null) total += SelectPSU.PowerConsumption;
            if (SelectRAM != null) total += SelectRAM.PowerConsumption;
            if (SelectCase != null) total += SelectCase.PowerConsumption;
            if (SelectCooler != null) total += SelectCooler.PowerConsumption;
            if (SelectMotherboard != null) total += SelectMotherboard.PowerConsumption;
            if (SelectStorage != null) total += SelectStorage.PowerConsumption;

            return total;
        }

        //Метод для проверки совместимости комплектующих
        public List<string> CheckCompatibility()
        {
            var issues = new List<string>();

            //Проверка совместимости процессора и материнской платы
            if (SelectCPU != null && SelectMotherboard != null)
            {
                if (SelectCPU.Socket != SelectMotherboard.Socket)
                {
                    issues.Add($"Сокет процессора '{SelectCPU.Name}' ({SelectCPU.Socket}) несовместим с сокетом материнской платы '{SelectMotherboard.Name}' ({SelectMotherboard.Socket}).");
                }

            }
            //Напоминалки, чтобы пользователь не забыл добавить их в сборку
            else if (SelectCPU != null && SelectMotherboard == null)
            {
                issues.Add("Материнская плата не выбрана для проверки совместимости с процессором.");
            }
            else if (SelectCPU == null && SelectMotherboard != null)
            {
                issues.Add("Процессор не выбран для проверки совместимости с материнской платой.");
            }

            //Проверка совместимости кулера и процессора
            if (SelectCooler != null && SelectCPU != null)
            {
                //Проверяем, прокрывает ли кулер тепловыделение процессора
                if (SelectCooler.MaxTDP < SelectCPU.PowerConsumption)
                {
                    issues.Add($"Максимальное TDP кулера '{SelectCooler.Name}' ({SelectCooler.MaxTDP} Вт) меньше TDP процессора '{SelectCPU.Name}' ({SelectCPU.PowerConsumption} Вт). Требуется более мощный кулер.");
                }
                //Проверяем поддерживает ли кулер установку в сокет выбранного процессора
                if (SelectCooler.SupportedSockets != null && !SelectCooler.SupportedSockets.Contains(SelectCPU.Socket))
                {
                    issues.Add($"Кулер '{SelectCooler.Name}' не поддерживает сокет процессора '{SelectCPU.Name}' ({SelectCPU.Socket}). Поддерживаемые сокеты кулером: {string.Join(", ", SelectCooler.SupportedSockets)}.");
                }
            }

            //Проверка совместимости кулера и корпуса
            if (SelectCooler != null && SelectCase != null)
            {
                //Проверяем подходит ли кулер по высоте под выбранный корпус
                if (SelectCooler.Height > SelectCase.MaxCoolerHeight)
                {
                    issues.Add($"Высота кулера '{SelectCooler.Name}' ({SelectCooler.Height} мм) превышает максимально допустимую высоту кулера для корпуса '{SelectCase.Name}' ({SelectCase.MaxCoolerHeight} мм).");
                }
            }
            
            //Проверка совместимости оперативной памяти и материнской платы 
            if (SelectRAM != null && SelectMotherboard != null)
            {
                //Проверяем поместится ли данного количество модулей оперативной памяти в выбранную материнскую плату
                if (SelectRAM.Modules > SelectMotherboard.RAMSlots)
                {
                    issues.Add($"Количество модулей оперативной памяти '{SelectRAM.Name}' ({SelectRAM.Modules}) превышает количество слотов на материнской плате '{SelectMotherboard.Name}' ({SelectMotherboard.RAMSlots}).");
                }
                //Проверяем совместимости типа оперативной памяти с поддерживаемым материнской платы
                if (SelectRAM != null && SelectMotherboard.RAMType != null && SelectMotherboard.RAMType != SelectRAM.Type)
                {
                    issues.Add($"Тип оперативной памяти '{SelectRAM.Name}' ({SelectRAM.Type}) несовместим с типом, поддерживаемым материнской платой '{SelectMotherboard.Name}' ({SelectMotherboard.RAMType}).");
                }
                //Проверяем поддерживает ли мат.плата выбраный объем оперативной памяти
                if (SelectRAM.Capacity > SelectMotherboard.MaxRAM)
                {
                    issues.Add($"Объем оперативной памяти '{SelectRAM.Name}' ({SelectRAM.Capacity}GB) превышает максимальный объем, поддерживаемый материнской платой '{SelectMotherboard.Name}' ({SelectMotherboard.MaxRAM}GB).");
                }
            }

            //Проверяем блок питания
            if (SelectPSU != null)
            {
                int requiredPower = CalculateTotalPower();
                //Выводим сообщение, если мощности блока питания недостаточно чтобы запитать текущую конфигурацию
                if (SelectPSU.Power < requiredPower)
                {
                    issues.Add($"Мощность блока питания '{SelectPSU.Name}' ({SelectPSU.Power} Вт) недостаточна. Требуется как минимум {requiredPower} Вт (текущее потребление).");
                }
                //Выводим сообщение, если мощности блока питания хватает впритык, так как рекомендуется брать с запасом
                else if (SelectPSU.Power < requiredPower * 1.25) 
                {
                    issues.Add($"Мощность блока питания '{SelectPSU.Name}' ({SelectPSU.Power} Вт) может быть недостаточной с учетом пиковых нагрузок и будущего апгрейда. Рекомендуется {Math.Ceiling(requiredPower * 1.25)} Вт или больше.");
                }
            }
            //Напоминалка о том, что нужно добавить блок питания
            else if (CalculateTotalPower() > 0) 
            {
                issues.Add("Блок питания не выбран, но выбраны компоненты, требующие питания.");
            }
            
            //Проверяем совместимость материнской платы и корпуса
            if (SelectMotherboard != null && SelectCase != null)
            {
                //caseSupportedFormFactors заносим в список, так как корпуса поддерживают довольно много форм-факторов, но иногда не все
                var caseSupportedFormFactors = SelectCase.FormFactor?.Split(',').Select(f => f.Trim().ToUpperInvariant()).ToList() ?? new List<string>();
                //Тут уже проверяем соответсвует ли форм-фактор материнской плате поддерживаему форм-фактору корпуса
                if (!string.IsNullOrEmpty(SelectMotherboard.FormFactor) && !caseSupportedFormFactors.Contains(SelectMotherboard.FormFactor.ToUpperInvariant()))
                {
                    issues.Add($"Форм-фактор материнской платы '{SelectMotherboard.Name}' ({SelectMotherboard.FormFactor}) не поддерживается корпусом '{SelectCase.Name}'. Поддерживаемые корпусом: {SelectCase.FormFactor}.");
                }
            }

            //Проверяем количество SATA и M.2 слотов на материнской плате
            if (SelectStorage != null && SelectMotherboard != null)
            {
                //Если выбран накопитель с форматом подключения M.2, а на материнской плате отсутсвует соответсвующий слот, выводим сообщение
                if (SelectStorage.Interface == "NVMe" && (SelectStorage.FormFactor == "M.2" || SelectStorage.FormFactor == "M2"))
                {
                    if (SelectMotherboard.NumM2Slots < 1)
                    {
                        issues.Add($"Материнская плата '{SelectMotherboard.Name}' не имеет M.2 слотов, необходимых для NVMe SSD '{SelectStorage.Name}'.");
                    }
                }
                //Если выбран накопитель с форматом подключения SATA, а на материнской плате отсутсвует соответсвующий слот, выводим сообщение
                else if (SelectStorage.Interface == "SATA")
                {
                     if (SelectMotherboard.NumSataSlots < 1)
                    {
                         issues.Add($"Материнская плата '{SelectMotherboard.Name}' не имеет SATA слотов, необходимых для SATA накопителя '{SelectStorage.Name}'.");
                    }
                }
            }

            //Проверка совместимости видеокарты и корпуса по длине
            if (SelectGPU != null && SelectCase != null)
            {
                if (SelectGPU.GpuLength > 0 && SelectCase.MaxGPULength > 0)
                {
                    if (SelectGPU.GpuLength > SelectCase.MaxGPULength)
                    {
                        issues.Add($"Длина видеокарты '{SelectGPU.Name}' ({SelectGPU.GpuLength} мм) превышает максимально допустимую длину для корпуса '{SelectCase.Name}' ({SelectCase.MaxGPULength} мм).");
                    }
                }
            }

            return issues;
        }
    }
}
