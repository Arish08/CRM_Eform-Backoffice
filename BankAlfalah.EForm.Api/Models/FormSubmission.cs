namespace BankAlfalah.EForm.Api.Models;

public class FormSubmission
{
    public int Id { get; set; }

    public int FormId { get; set; }

    public Form? Form { get; set; }

    public DateTime SubmittedAt { get; set; } = DateTime.Now;

    public string DataJson { get; set; } = string.Empty;
}