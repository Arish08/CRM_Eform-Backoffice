namespace BankAlfalah.EForm.Api.DTOs;

public class CreateFormDto
{
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public List<FormFieldDto> Fields { get; set; } = new();
}