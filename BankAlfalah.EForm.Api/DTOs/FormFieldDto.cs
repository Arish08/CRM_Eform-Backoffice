namespace BankAlfalah.EForm.Api.DTOs;

public class FormFieldDto
{
    public string Label { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public bool Required { get; set; }

    public int SortOrder { get; set; }
}