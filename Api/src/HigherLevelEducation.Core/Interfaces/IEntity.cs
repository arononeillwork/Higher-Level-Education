using System.ComponentModel.DataAnnotations;

namespace HigherLevelEducation.Core.Interfaces;

public abstract class Entity
{ 
     public int Id { get; init; }
     
     [Required]
     public string Name { get; set; }

     // ToDo: GetUserId() from HttpContext
     public int CreatedBy { get; set; } = 1;
     
     public DateTimeOffset CreatedOn { get; set; }
     
     public DateTimeOffset? ModifiedOn { get; set; }

     public bool Deleted { get; set; } = false;
}