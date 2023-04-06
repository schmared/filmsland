namespace filmsApi.Models.Parameters;

public interface ISearchParameter
{
    int? Page { get; set; }
    int? PageSize { get; set; }
}