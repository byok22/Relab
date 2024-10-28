using Domain.Enums;
using Domain.Generics;
using Domain.Models.Employees;
using Domain.Models.Equipments;
using Domain.Models.Generics;

namespace Domain.Models
{
    public class Test: BasicFieldsModels
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime Start{ get; set; }
        public DateTime End { get; set; }
        public Samples? Samples { get; set; }
        public string? SpecialInstructions { get; set; }
        public Attachment? Profile { get; set; }
        public List<Attachment>? Attachments { get; set; }
        //attachmentsCount
        public int AttachmentsCount { get; set; }
        public Employee? Enginner{get; set;}
        public List<Employee>? Technicians{get;set;}
        //techniciansCount
        public int TechniciansCount { get; set; }
        public List<Specification>? Specifications { get; set; }
        //specificationsCount
        public int SpecificationsCount { get; set; }
        public List<Equipment>?Equipments { get; set; }
        //equipmentsCount
        public int EquipmentsCount { get; set; }
        public TestStatusEnum Status { get; set; }
        public List<ChangeStatusTest>? changeStatusTest{ get; set; }
        public string? LastUpdatedMessage{get;set; }
        public int? idRequest{get;set; }
        public List<GenericUpdate>? updates { get; set; }
                
    }
}