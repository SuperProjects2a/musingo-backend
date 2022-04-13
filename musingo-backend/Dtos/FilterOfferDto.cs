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
    public double? PriceFrom { get; set; }
    public double? PriceTo { get; set; }

    [EnumDataType(typeof(Sorting))]
    public string? Sorting { get; set; }
}