using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi.Extensions;
using musingo_backend.CustomValidation;
using musingo_backend.Models;

namespace musingo_backend.Dtos;

public enum Sorting
{
    Latest,
    Oldest,
    Ascending,
    Descending
}
public class OfferFilterDto
{
    [MaxLength(50)]
    [DefaultValue("")]
    public string? Search { get; set; }
    [EnumDataType(typeof(ItemCategory))]
    public string? Category { get; set; }
    [Range(0d, 1000000d)]
    [NumberLessThan(nameof(PriceTo),ErrorMessage = $"{nameof(PriceTo)} can't be less than {nameof(PriceFrom)}")]
    public double? PriceFrom { get; set; }
    [Range(0d, 1000000d)]
    public double? PriceTo { get; set; }

    [EnumDataType(typeof(Sorting))]
    [DefaultValue(nameof(Dtos.Sorting.Latest))]
    public string? Sorting { get; set; }
}