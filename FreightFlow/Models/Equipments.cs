using System.ComponentModel.DataAnnotations;

public class Global2
{
    public static int EquipmentId { get; set; }

}

namespace FreightFlow.Models
{
    public class Equipments
    {
        [Key]public int EquipmentId { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? CurrentStatus { get; set; }
        public required int IDofDelivery { get; set; }
        public required string NameofDelivery { get; set; }
        public required string DescriptionofDelivery { get; set; }
        public decimal? LoadCapacity { get; set; }
        public required decimal CurrentLoad { get; set; }
        public required int LoadId { get; set; }


    }
}
