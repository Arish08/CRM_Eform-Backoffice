namespace BankAlfalah.EForm.Api.Models;
using System.Text.Json.Serialization;

public class FormField
{
    public int Id { get; set; }

    public int FormId { get; set; }

     [JsonIgnore]
    public Form? Form { get; set; }

    public string Label { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public bool Required { get; set; }

    public int SortOrder { get; set; }
}