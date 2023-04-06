namespace filmsApi.Models
{
    public interface IActor
    {
        int Id { get; set; }
        string Name { get; set; }
        int YearOfBirth { get; set; }
    }

    public class Actor : IActor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int YearOfBirth { get; set; }
        public IList<Movie> Movies { get; set; } = new List<Movie>();
    }
}