namespace filmsApi.Models.Parameters
{
    public class ActorSearchParameters : ISearchParameter
    {
        public string? NamePart { get; set; } = string.Empty;
        public int? YearOfBirth { get; set; } 
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}