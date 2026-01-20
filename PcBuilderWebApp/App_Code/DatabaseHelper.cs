using System;
using System.Collections.Generic;
using System.Linq;
using PCBuilderLibrary;
using PCBuilderLibrary.Components;

namespace PcBuilderWebApp
{
    // Класс для работы с базой данных через LINQ
    public class DatabaseHelper : IDisposable
    {
        private readonly PCBuilderDataContext context;

        public DatabaseHelper()
        {
            context = new PCBuilderDataContext();
        }

        // Получить все компоненты определенного типа
        public List<Component> GetComponentsByType(string componentType)
        {
            var query = from c in context.Components
                       where c.ComponentType == componentType
                       select c;

            var components = new List<Component>();
            foreach (var dbComp in query.ToList())
            {
                var component = ConvertToComponent(dbComp);
                if (component != null)
                    components.Add(component);
            }
            return components;
        }

        // Получить все компоненты
        public List<Component> GetAllComponents()
        {
            var query = from c in context.Components
                       select c;

            var components = new List<Component>();
            foreach (var dbComp in query.ToList())
            {
                var component = ConvertToComponent(dbComp);
                if (component != null)
                    components.Add(component);
            }
            return components;
        }

        // Конвертация из БД в объект Component с использованием JOIN
        private Component ConvertToComponent(ComponentDB dbComp)
        {
            if (dbComp == null || string.IsNullOrWhiteSpace(dbComp.ComponentType))
            {
                return null;
            }

            Component component = null;

            switch (dbComp.ComponentType.ToLowerInvariant())
            {
                case "cpu":
                    var cpuDb = (from cpu in context.CPUs
                                where cpu.Id == dbComp.Id
                                select cpu).FirstOrDefault();
                    if (cpuDb != null)
                    {
                        component = new CPU
                        {
                            Id = dbComp.Id,
                            Name = dbComp.Name,
                            Manufacturer = dbComp.Manufacturer,
                            Price = dbComp.Price,
                            PowerConsumption = dbComp.PowerConsumption,
                            Socket = cpuDb.Socket,
                            Cores = cpuDb.Cores,
                            BaseClockSpeed = cpuDb.BaseClockSpeed,
                            HasIntegratedGraphic = cpuDb.HasIntegratedGraphic
                        };
                    }
                    break;

                case "gpu":
                    var gpuDb = (from gpu in context.GPUs
                                where gpu.Id == dbComp.Id
                                select gpu).FirstOrDefault();
                    if (gpuDb != null)
                    {
                        component = new GPU
                        {
                            Id = dbComp.Id,
                            Name = dbComp.Name,
                            Manufacturer = dbComp.Manufacturer,
                            Price = dbComp.Price,
                            PowerConsumption = dbComp.PowerConsumption,
                            Model = gpuDb.Model,
                            MemorySize = gpuDb.MemorySize,
                            MemoryType = gpuDb.MemoryType,
                            PCIExpressVer = gpuDb.PCIExpressVer,
                            GpuLength = gpuDb.GpuLength
                        };
                    }
                    break;

                case "motherboard":
                    var mbDb = (from mb in context.Motherboards
                               where mb.Id == dbComp.Id
                               select mb).FirstOrDefault();
                    if (mbDb != null)
                    {
                        component = new Motherboard
                        {
                            Id = dbComp.Id,
                            Name = dbComp.Name,
                            Manufacturer = dbComp.Manufacturer,
                            Price = dbComp.Price,
                            PowerConsumption = dbComp.PowerConsumption,
                            Socket = mbDb.Socket,
                            FormFactor = mbDb.FormFactor,
                            Chipset = mbDb.Chipset,
                            RAMSlots = mbDb.RAMSlots,
                            MaxRAM = mbDb.MaxRAM,
                            RAMType = mbDb.RAMType,
                            NumM2Slots = mbDb.NumM2Slots,
                            NumSataSlots = mbDb.NumSataSlots
                        };
                    }
                    break;

                case "ram":
                    var ramDb = (from ram in context.RAMs
                               where ram.Id == dbComp.Id
                               select ram).FirstOrDefault();
                    if (ramDb != null)
                    {
                        component = new RAM
                        {
                            Id = dbComp.Id,
                            Name = dbComp.Name,
                            Manufacturer = dbComp.Manufacturer,
                            Price = dbComp.Price,
                            PowerConsumption = dbComp.PowerConsumption,
                            Type = ramDb.Type,
                            Capacity = ramDb.Capacity,
                            Speed = ramDb.Speed,
                            Modules = ramDb.Modules
                        };
                    }
                    break;

                case "psu":
                    var psuDb = (from psu in context.PSUs
                               where psu.Id == dbComp.Id
                               select psu).FirstOrDefault();
                    if (psuDb != null)
                    {
                        component = new PSU
                        {
                            Id = dbComp.Id,
                            Name = dbComp.Name,
                            Manufacturer = dbComp.Manufacturer,
                            Price = dbComp.Price,
                            PowerConsumption = dbComp.PowerConsumption,
                            Power = psuDb.Power,
                            EfficiencyRating = psuDb.EfficiencyRating,
                            Modular = psuDb.Modular
                        };
                    }
                    break;

                case "storage":
                    var storageDb = (from storage in context.Storages
                                   where storage.Id == dbComp.Id
                                   select storage).FirstOrDefault();
                    if (storageDb != null)
                    {
                        component = new Storage
                        {
                            Id = dbComp.Id,
                            Name = dbComp.Name,
                            Manufacturer = dbComp.Manufacturer,
                            Price = dbComp.Price,
                            PowerConsumption = dbComp.PowerConsumption,
                            Type = storageDb.StorageType,
                            Capacity = storageDb.Capacity,
                            Interface = storageDb.Interface,
                            FormFactor = storageDb.FormFactor
                        };
                    }
                    break;

                case "cooler":
                    var coolerDb = (from cooler in context.Coolers
                                   where cooler.Id == dbComp.Id
                                   select cooler).FirstOrDefault();
                    if (coolerDb != null)
                    {
                        component = new Cooler
                        {
                            Id = dbComp.Id,
                            Name = dbComp.Name,
                            Manufacturer = dbComp.Manufacturer,
                            Price = dbComp.Price,
                            PowerConsumption = dbComp.PowerConsumption,
                            Type = coolerDb.CoolerType,
                            SupportedSockets = coolerDb.SupportedSockets?.Split(',').Select(s => s.Trim()).ToList() ?? new List<string>(),
                            MaxTDP = coolerDb.MaxTDP,
                            NoiseLevel = coolerDb.NoiseLevel,
                            Height = coolerDb.Height
                        };
                    }
                    break;

                case "case":
                    var caseDb = (from pcCase in context.Cases
                                where pcCase.Id == dbComp.Id
                                select pcCase).FirstOrDefault();
                    if (caseDb != null)
                    {
                        component = new Case
                        {
                            Id = dbComp.Id,
                            Name = dbComp.Name,
                            Manufacturer = dbComp.Manufacturer,
                            Price = dbComp.Price,
                            PowerConsumption = dbComp.PowerConsumption,
                            Type = caseDb.CaseType,
                            FormFactor = caseDb.FormFactor,
                            MaxGPULength = caseDb.MaxGPULength,
                            MaxCoolerHeight = caseDb.MaxCoolerHeight
                        };
                    }
                    break;
            }

            return component;
        }

        // Получить ID компонента по имени и типу
        private int? GetComponentId(Component component, string componentType)
        {
            if (component == null) return null;

            var query = from c in context.Components
                       where c.ComponentType == componentType && c.Name == component.Name
                       select c.Id;

            return query.FirstOrDefault();
        }

        // Сохранить конфигурацию
        public void SaveConfiguration(PCConfiguration config, string name)
        {
            var configDb = new ConfigurationDB
            {
                ConfigurationName = name,
                CPU_Id = GetComponentId(config.SelectCPU, "CPU"),
                GPU_Id = GetComponentId(config.SelectGPU, "GPU"),
                Motherboard_Id = GetComponentId(config.SelectMotherboard, "Motherboard"),
                RAM_Id = GetComponentId(config.SelectRAM, "RAM"),
                Storage_Id = GetComponentId(config.SelectStorage, "Storage"),
                PSU_Id = GetComponentId(config.SelectPSU, "PSU"),
                Cooler_Id = GetComponentId(config.SelectCooler, "Cooler"),
                Case_Id = GetComponentId(config.SelectCase, "Case"),
                TotalCost = config.TotalCost,
                TotalPower = config.TotalPower,
                CreatedAt = DateTime.Now
            };

            context.Configurations.InsertOnSubmit(configDb);
            context.SubmitChanges();
        }

        // Получить все сохраненные конфигурации
        public List<ConfigurationDB> GetSavedConfigurations()
        {
            var query = from c in context.Configurations
                       orderby c.CreatedAt descending
                       select c;
            return query.ToList();
        }

        // Загрузить конфигурацию
        public PCConfiguration LoadConfiguration(int id)
        {
            var configDb = context.Configurations.FirstOrDefault(c => c.Id == id);
            if (configDb == null)
                return new PCConfiguration();

            var config = new PCConfiguration
            {
                BuildName = configDb.ConfigurationName
            };

            // Загружаем компоненты по ID
            if (configDb.CPU_Id.HasValue)
                config.SelectCPU = GetComponentById(configDb.CPU_Id.Value, "CPU") as CPU;

            if (configDb.GPU_Id.HasValue)
                config.SelectGPU = GetComponentById(configDb.GPU_Id.Value, "GPU") as GPU;

            if (configDb.Motherboard_Id.HasValue)
                config.SelectMotherboard = GetComponentById(configDb.Motherboard_Id.Value, "Motherboard") as Motherboard;

            if (configDb.RAM_Id.HasValue)
                config.SelectRAM = GetComponentById(configDb.RAM_Id.Value, "RAM") as RAM;

            if (configDb.Storage_Id.HasValue)
                config.SelectStorage = GetComponentById(configDb.Storage_Id.Value, "Storage") as Storage;

            if (configDb.PSU_Id.HasValue)
                config.SelectPSU = GetComponentById(configDb.PSU_Id.Value, "PSU") as PSU;

            if (configDb.Cooler_Id.HasValue)
                config.SelectCooler = GetComponentById(configDb.Cooler_Id.Value, "Cooler") as Cooler;

            if (configDb.Case_Id.HasValue)
                config.SelectCase = GetComponentById(configDb.Case_Id.Value, "Case") as Case;

            return config;
        }

        // Получить компонент по ID
        private Component GetComponentById(int id, string componentType)
        {
            var dbComp = context.Components.FirstOrDefault(c => c.Id == id && c.ComponentType == componentType);
            if (dbComp != null)
                return ConvertToComponent(dbComp);
            return null;
        }

        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
