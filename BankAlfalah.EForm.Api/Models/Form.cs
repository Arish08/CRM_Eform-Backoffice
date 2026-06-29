namespace BankAlfalah.EForm.Api.Models;

public class Form
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public List<FormField> Fields { get; set; } = new();
}