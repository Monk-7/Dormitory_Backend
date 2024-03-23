using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAPI.Models
{
    public class Invoice
    {
        [Key]
        public string? idInvoice { get; set; }
        public string? idRoom { get; set; }
        public int roomName { get; set; } = 0!;
        public int roomPrice { get; set; } = 0!;
        public int electricityPrice { get; set; } = 0!;
        public int waterPrice { get; set; } = 0!; 
        public int electricityUnit { get; set; } = 0!;
        public int waterUnit { get; set; } = 0!; 
        public int furniturePrice { get; set; } = 0!;
        public int internetPrice { get; set; } = 0!;
        public int parkingPrice { get; set; } = 0!;
        public int other { get; set; } = 0!;
        public int total { get; set; } = 0!;
        public bool status { get; set; } = false!;
        public bool statusShow { get; set; } = false!;
        public DateTimeOffset? dueDate { get; set; }
        public DateTimeOffset? timesTamp { get; set; }
        
    }

    public class CreateInvoice
    {
        public string idUser { get; set; } = null!;
        public List<GetMeter> meterAll { get; set; } = new List<GetMeter>();
        public DateTimeOffset? dueDate { get; set; }
        
    }

    public class GetInvoice
    {
        public string? idDormitory { get; set; }
        public string? idBuilding { get; set; }
        public string dormitoryName { get; set; } = null!;
        public string buildingName { get; set; } = null!;
        public List<Invoice> invoiceAll { get; set; } = new List<Invoice>();
    }

    

}
