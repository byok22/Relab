using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Shared.Dtos
{
    public class GenericUpdateDto
    {        
        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User? User{ get; set; }
        public string? Message { get; set; }
        public string? Changes { get; set; }= string.Empty;
    }
}