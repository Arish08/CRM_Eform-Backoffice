using BankAlfalah.EForm.Api.Data;
using BankAlfalah.EForm.Api.DTOs;
using BankAlfalah.EForm.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankAlfalah.EForm.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormsController : ControllerBase
{
    private readonly AppDbContext _context;

    public FormsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetForms()
    {
        var forms = await _context.Forms
            .Include(f => f.Fields)
            .ToListAsync();

        return Ok(forms);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFormById(int id)
    {
        var form = await _context.Forms
            .Include(f => f.Fields.OrderBy(x => x.SortOrder))
            .FirstOrDefaultAsync(f => f.Id == id);

        if (form == null)
            return NotFound();

        return Ok(form);
    }

    [HttpPost]
    public async Task<IActionResult> CreateForm(CreateFormDto dto)
    {
        var form = new Form
        {
            Title = dto.Title,
            Description = dto.Description,
            Fields = dto.Fields.Select((field, index) => new FormField
            {
                Label = field.Label,
                Type = field.Type,
                Required = field.Required,
                SortOrder = index + 1
            }).ToList()
        };

        _context.Forms.Add(form);
        await _context.SaveChangesAsync();

        return Ok(form);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteForm(int id)
    {
        var form = await _context.Forms
            .Include(f => f.Fields)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (form == null)
            return NotFound();

        _context.Forms.Remove(form);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{formId}/fields")]
public async Task<IActionResult> AddFieldToForm(int formId, FormFieldDto dto)
{
    var formExists = await _context.Forms.AnyAsync(f => f.Id == formId);

    if (!formExists)
        return NotFound("Form not found");

    var maxSortOrder = await _context.FormFields
        .Where(f => f.FormId == formId)
        .MaxAsync(f => (int?)f.SortOrder) ?? 0;

    var field = new FormField
    {
        FormId = formId,
        Label = dto.Label,
        Type = dto.Type,
        Required = dto.Required,
        SortOrder = maxSortOrder + 1
    };

    _context.FormFields.Add(field);
    await _context.SaveChangesAsync();

    return Ok(field);
}
}