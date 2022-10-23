using System.ComponentModel.DataAnnotations;

namespace SearchDuplicatesText.Models.DataBase;

public class ExpFile : BaseModel
{
    [Required]
    public string? Name { get; set; }
    public int NumberOfPart { get; set; }
}