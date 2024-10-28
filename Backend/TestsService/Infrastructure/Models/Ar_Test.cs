namespace Infrastructure.Models
{
    public class Ar_Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Start { get; set; }  // Puede ser nullable como en la DB
        public DateTime? End { get; set; }    // Puede ser nullable como en la DB
        public int? SamplesId { get; set; }   // Puede ser nullable como en la DB
        public int? SamplesQuantity { get; set; }
        public decimal? SamplesWeight { get; set; }
        public decimal? SamplesSize { get; set; }
        public string SpecialInstructions { get; set; }
        public int? ProfileId { get; set; }   // Puede ser nullable como en la DB
        public string? ProfileName { get; set; }
        public string? ProfileUrl { get; set; }
        public int? EnginnerId { get; set; }  // Puede ser nullable como en la DB
        public string? EmployeeNumber { get; set; }
        public string? EnginnerName { get; set; }
        public string Status { get; set; }
        public string LastUpdatedMessage { get; set; }
        public int? IdRequest { get; set; }   // Puede ser nullable como en la DB
        public DateTime CreatedAt { get; set; }  // No nullable según la definición en DB
        public DateTime UpdatedAt { get; set; }  // No nullable según la definición en DB
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
         //attachmentsCount
        public int AttachmentsCount { get; set; }
         //techniciansCount
        public int TechniciansCount { get; set; }

         //specificationsCount
        public int SpecificationsCount { get; set; }
    
             //equipmentsCount  
        public int EquipmentsCount { get; set; }

        
        
    }
}