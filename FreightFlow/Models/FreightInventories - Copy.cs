using Microsoft.EntityFrameworkCore;
/*
public class Global1
{
    public static int ClientId { get; set; } = 123;

}*/

namespace FreightFlow.Models
{
    public class FreightInventories
    {

        public int DeliveryId { get; set; }
        public required string DeliveryName { get; set; }
        public required string Origin { get; set; }

        public required string Destination { get; set; }

        public required string Contents { get; set; }
        public required decimal Weight { get; set; }
        public required int Length { get; set; }
        public required int Width { get; set; }
        public required int Height { get; set; }
        public required DateTime ScheduledLoad { get; set; }
        public required DateTime ScheduledDelivery { get; set; }
        public required string Status { get; set; }
        public string? Description { get; set; }
        public int Id { get; set; }
        public required Customer Customer { get; set; }

    }
}
