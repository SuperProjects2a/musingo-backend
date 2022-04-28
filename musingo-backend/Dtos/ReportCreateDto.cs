using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using musingo_backend.Models;

namespace musingo_backend.Dtos;

public class ReportCreateDto
{
    [Required]
    public int OfferId { get; set; }
    [EnumDataType(typeof(Reason))]
    [Required]
    public string Reason { get; set; }
    [DefaultValue("")]
    [MaxLength(200)]
    public string? Text { get; set; }
}