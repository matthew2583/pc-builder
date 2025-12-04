using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Configuration;

namespace PcBuilderWebApp
{
    // Класс для работы с базой данных через LINQ to SQL
    [Table(Name = "Components")]
    public class ComponentDB
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string ComponentType { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Manufacturer { get; set; }

        [Column]
        public int Price { get; set; }

        [Column]
        public int PowerConsumption { get; set; }

        [Column]
        public DateTime? CreatedAt { get; set; }

        [Column]
        public DateTime? UpdatedAt { get; set; }
    }

    [Table(Name = "CPUs")]
    public class CPUDB
    {
        [Column(IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Socket { get; set; }

        [Column]
        public int Cores { get; set; }

        [Column]
        public double BaseClockSpeed { get; set; }

        [Column]
        public bool HasIntegratedGraphic { get; set; }
    }

    [Table(Name = "GPUs")]
    public class GPUDB
    {
        [Column(IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Model { get; set; }

        [Column]
        public int MemorySize { get; set; }

        [Column]
        public string MemoryType { get; set; }

        [Column]
        public string PCIExpressVer { get; set; }

        [Column]
        public int GpuLength { get; set; }
    }

    [Table(Name = "Motherboards")]
    public class MotherboardDB
    {
        [Column(IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Socket { get; set; }

        [Column]
        public string FormFactor { get; set; }

        [Column]
        public string Chipset { get; set; }

        [Column]
        public int RAMSlots { get; set; }

        [Column]
        public int MaxRAM { get; set; }

        [Column]
        public string RAMType { get; set; }

        [Column]
        public int NumM2Slots { get; set; }

        [Column]
        public int NumSataSlots { get; set; }
    }

    [Table(Name = "RAMs")]
    public class RAMDB
    {
        [Column(IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Type { get; set; }

        [Column]
        public int Capacity { get; set; }

        [Column]
        public int Speed { get; set; }

        [Column]
        public int Modules { get; set; }
    }

    [Table(Name = "Storages")]
    public class StorageDB
    {
        [Column(IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string StorageType { get; set; }

        [Column]
        public int Capacity { get; set; }

        [Column]
        public string Interface { get; set; }

        [Column]
        public string FormFactor { get; set; }
    }

    [Table(Name = "PSUs")]
    public class PSUDB
    {
        [Column(IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int Power { get; set; }

        [Column]
        public string EfficiencyRating { get; set; }

        [Column]
        public bool Modular { get; set; }
    }

    [Table(Name = "Coolers")]
    public class CoolerDB
    {
        [Column(IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string CoolerType { get; set; }

        [Column]
        public string SupportedSockets { get; set; }

        [Column]
        public int MaxTDP { get; set; }

        [Column]
        public double NoiseLevel { get; set; }

        [Column]
        public int Height { get; set; }
    }

    [Table(Name = "Cases")]
    public class CaseDB
    {
        [Column(IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string CaseType { get; set; }

        [Column]
        public string FormFactor { get; set; }

        [Column]
        public int MaxGPULength { get; set; }

        [Column]
        public int MaxCoolerHeight { get; set; }
    }

    [Table(Name = "Configurations")]
    public class ConfigurationDB
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string ConfigurationName { get; set; }

        [Column]
        public int? CPU_Id { get; set; }

        [Column]
        public int? GPU_Id { get; set; }

        [Column]
        public int? Motherboard_Id { get; set; }

        [Column]
        public int? RAM_Id { get; set; }

        [Column]
        public int? Storage_Id { get; set; }

        [Column]
        public int? PSU_Id { get; set; }

        [Column]
        public int? Cooler_Id { get; set; }

        [Column]
        public int? Case_Id { get; set; }

        [Column]
        public int TotalCost { get; set; }

        [Column]
        public int TotalPower { get; set; }

        [Column]
        public DateTime? CreatedAt { get; set; }

        [Column]
        public DateTime? UpdatedAt { get; set; }
    }

    public class PCBuilderDataContext : DataContext
    {
        public PCBuilderDataContext() : base(ConfigurationManager.ConnectionStrings["PCBuilderConnection"].ConnectionString)
        {
        }

        public Table<ComponentDB> Components => GetTable<ComponentDB>();
        public Table<CPUDB> CPUs => GetTable<CPUDB>();
        public Table<GPUDB> GPUs => GetTable<GPUDB>();
        public Table<MotherboardDB> Motherboards => GetTable<MotherboardDB>();
        public Table<RAMDB> RAMs => GetTable<RAMDB>();
        public Table<StorageDB> Storages => GetTable<StorageDB>();
        public Table<PSUDB> PSUs => GetTable<PSUDB>();
        public Table<CoolerDB> Coolers => GetTable<CoolerDB>();
        public Table<CaseDB> Cases => GetTable<CaseDB>();
        public Table<ConfigurationDB> Configurations => GetTable<ConfigurationDB>();
    }
}

