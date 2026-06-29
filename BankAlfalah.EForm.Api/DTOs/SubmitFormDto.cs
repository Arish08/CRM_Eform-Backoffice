namespace BankAlfalah.EForm.Api.DTOs;

public class SubmitFormDto
{
    public int FormId { get; set; }

    public Dictionary<string, string> Answers { get; set; } = new();
}