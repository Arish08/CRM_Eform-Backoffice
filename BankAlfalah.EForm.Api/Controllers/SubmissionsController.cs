using System.Text.Json;
using BankAlfalah.EForm.Api.Data;
using BankAlfalah.EForm.Api.DTOs;
using BankAlfalah.EForm.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankAlfalah.EForm.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubmissionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SubmissionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitForm(SubmitFormDto dto)
    {
        var formExists = await _context.Forms.AnyAsync(f => f.Id == dto.FormId);

        if (!formExists)
            return NotFound("Form not found");

        var submission = new FormSubmission
        {
            FormId = dto.FormId,
            DataJson = JsonSerializer.Serialize(dto.Answers)
        };

        _context.FormSubmissions.Add(submission);
        await _context.SaveChangesAsync();

        return Ok(submission);
    }

    [HttpGet("form/{formId}")]
    public async Task<IActionResult> GetSubmissionsByForm(int formId)
    {
        var submissions = await _context.FormSubmissions
            .Where(s => s.FormId == formId)
            .ToListAsync();

        return Ok(submissions);
    }
}