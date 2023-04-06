namespace filmsApi.Models.Parameters;

public class MovieSearchParameters : ISearchParameter
{
    public string? TitlePart { get; set; } = string.Empty;
    public ParentalGuide? ParentalGuide { get; set; }
    public int? Released { get; set; }
    public bool? ReleasedAfter { get; set; }
    public int? Length { get; set; }
    public bool? LengthLessThan { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string? Actor { get; set; }
}