using System.ComponentModel.DataAnnotations;

namespace ToDoList.Web.Models;

public sealed class NewTaskModel
{
    [Required(ErrorMessage = "Title is required")]
    public required string Title { get; init; }
    
    [MaxLength(200, ErrorMessage = "Description is too long")]
    public string? Description { get; init; }
    
    [DataType(DataType.DateTime, ErrorMessage = "Input is not date")]
    public DateTime? ExpirationDate { get; init; }
}