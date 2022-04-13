using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using musingo_backend.Models;

namespace musingo_backend.Dtos;

public enum Sorting
{
    Latest,
    Ascending,
    Descending
}
public class FilterOfferDto
{
    public string? Search { get; set; }
    [EnumDataType(typeof(ItemCategory))]
    public string? Category { get; set; }
    [Range(0d, 1000000d)]
    public double? PriceFrom { get; set; }
    [Range(0d, 1000000d)]
    public double? PriceTo { get; set; }

    [EnumDataType(typeof(Sorting))]
    [DefaultValue(Dtos.Sorting.Latest)]
    public string? Sorting { get; set; }
}