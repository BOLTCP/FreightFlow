namespace FreightFlow.Models
{
    public class Global2
    {
        public static int TakeDeliId { get; set; } = 123;

    }
    public class OrderHistoryViewModel
    {
        public int ClientId { get; set; }
        public List<FreightInventories> FreightInventories { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Equipments> Equipments { get; set; }
    }
}
