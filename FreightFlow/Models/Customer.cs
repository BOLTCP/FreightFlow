using Microsoft.EntityFrameworkCore;
public class Global
{
     public static int ClientId { get; set; }
    
}

namespace FreightFlow.Models
{
    public class Customer
    {

        public int Id { get;  set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public required string Phone { get; set; }
        public required string Password { get; set; }
        

        
    }
}
