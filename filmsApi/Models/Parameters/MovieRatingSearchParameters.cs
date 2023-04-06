namespace filmsApi.Models.Parameters;

public class MovieRatingSearchParameters : ISearchParameter
{
    public ParentalGuide? Rating { get; set; }
    public bool? NoChildRatings { get; set; }
    public bool? NoAdultRatings { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
}