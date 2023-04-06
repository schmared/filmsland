namespace filmsApi.Services
{
    public interface IQueryService<T, TParam>
    {
        T? Create(T entity);
        T? Read(int id);
        IList<T> ReadList(TParam parameters);
        T? Update(T entity);
        bool Delete(int id);
        IQueryable<T> QueryConditionalSearchParameters(IQueryable<T> query, TParam searchParameters);
    }

    /// <summary>
    /// Extention method to IQueriable that attempts to make easier boolean logic for Where clauses in-line
    /// </summary>
    public static class LINQExtensions
    {
        public static IQueryable<T> If<T>(
            this IQueryable<T> query,
            bool should,
            params Func<IQueryable<T>, IQueryable<T>>[] transforms) =>
                should
                    ? transforms.Aggregate(query, (current, transform) => transform.Invoke(current))
                    : query;
    }            
}