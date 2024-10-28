using Domain.Enums;
using Domain.Generics;
using Domain.Models;
using Domain.Models.Employees;
using Domain.Models.Equipments;
using Domain.Models.Generics;

namespace Shared.Dtos
{
    public class TestDto: BasicFieldsModels
    {
         public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime Start{ get; set; }
        public DateTime End { get; set; }
        public SampleDto? Samples { get; set; }
        public string? SpecialInstructions { get; set; }
        public AttachmentDto? Profile { get; set; }
        public List<AttachmentDto>? Attachments { get; set; }
        public int AttachmentsCount { get; set; }
        public EmployeeDto? Enginner{get; set;}
        public List<EmployeeDto>? Technicians{get;set;}
        public int TechniciansCount { get; set; }
        public List<SpecificationDto>? Specifications { get; set; }
        public int SpecificationsCount { get; set; }
        public List<Equipment>? Equipments { get; set; }
        public int EquipmentsCount { get; set; }
        public TestStatusEnum Status { get; set; }
        public List<ChangeStatusTest>? changeStatusTest{ get; set; }
        public string? LastUpdatedMessage{get;set; }
        public int? idRequest{get;set; }
        public List<GenericUpdateDto>? updates { get; set; }
    }
}