using filmsApi.DataAccess;
using filmsApi.Models;
using filmsApi.Models.Parameters;
using Microsoft.EntityFrameworkCore;

namespace filmsApi.Services;

public interface IActorService : IQueryService<Actor, ActorSearchParameters> {}
public class ActorService : IActorService
{
    private readonly FilmsContext _context;
    public ActorService(FilmsContext context) => _context = context;

    /// <summary>
    /// Retrieve record with exact matching identitifier field
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Record if found with the matching identifier</returns>
    public Actor? Read(int id) => _context.Actors?.Include(a => a.Movies).Where(m => m.Id == id).FirstOrDefault();

    /// <summary>
    /// Retrieve records matching provided search criteria
    /// </summary>
    /// <param name="actorSearchParameters"></param>
    /// <returns>List of records matching the provided search criteria</returns>
    public IList<Actor> ReadList(ActorSearchParameters actorSearchParameters)
    {
        var query = _context.Actors?.Include(a => a.Movies).AsQueryable();

        if (query == null)
            return new List<Actor>();
        
        var filteredQuery = QueryConditionalSearchParameters(query, actorSearchParameters)
                .OrderBy(actor => actor.Id);
        
        if (filteredQuery != null && actorSearchParameters.Page != null && actorSearchParameters.PageSize != null)
            return filteredQuery
                .Skip(actorSearchParameters.Page.Value * actorSearchParameters.PageSize.Value)
                .Take(actorSearchParameters.PageSize.Value)
                .ToList();

        return  filteredQuery?.ToList() ?? new List<Actor>();
    }

    /// <summary>
    /// Creates a new record in the database
    /// </summary>
    /// <param name="actor"></param>
    /// <returns>The newly created record if successful</returns>
    public Actor? Create(Actor actor)
    {
        // See if record already exists
        if (Read(actor.Id) != null)
            return actor;

        // Insert the new record into the database
        _context.Actors?.Add(actor);
        _context.SaveChanges();

        //Get the newly set SCOPE_IDENTITY value
        int id = actor.Id;

        //retrieve the newly created object from the database to return
        return Read(id);
    }

    /// <summary>
    /// Modifies an existing record in the database
    /// </summary>
    /// <param name="actor"></param>
    /// <returns>The modified record if successful</returns>
    public Actor? Update(Actor actor)
    {
        // Ensure record already exists
        var record = Read(actor.Id);
        if (record == null)
            return null;

        // Update the record in the database
        record.Name = actor.Name;
        record.YearOfBirth = actor.YearOfBirth;
        _context.SaveChanges();

        // Return updated object from the databaase
        return Read(actor.Id);
    }

    /// <summary>
    /// Deletes a specified record in the database that matches the provided identifier
    /// </summary>
    /// <param name="id"></param>
    /// <returns>True if record is deleted, false if unsuccessful or record does not exist</returns>
    public bool Delete(int id)
    {
        // See if record exists
        var actor = Read(id);

        // If not say it's deleted (return true)
        if (actor == null)
            return true;

        // Else remove the record
        _context.Remove(actor);
        _context.SaveChanges();

        // If there are not any records left or it is no longer found then say it's deleted (return true)
        return _context.Actors?.All(m => m.Id != id) ?? true;
    }

    /// <summary>
    /// Bulids a list of conditional 'where clause' conditions into an IQueryable
    /// </summary>
    /// <param name="query"></param>
    /// <param name="actorSearchParameters"></param>
    /// <returns>The modified IQueryable object</returns>
    public IQueryable<Actor> QueryConditionalSearchParameters(IQueryable<Actor> query, ActorSearchParameters actorSearchParameters)
    {
        return query

        //Search if Actor Name contains search string
        .If(actorSearchParameters.NamePart != null,
            query => query.Where(actor => actor.Name.Contains(actorSearchParameters.NamePart ?? string.Empty)))

        //Search for Actor DOB being equal to or greater than the supplied value
        .If(actorSearchParameters.YearOfBirth != null,
            query => query.Where(m => m.YearOfBirth >= actorSearchParameters.YearOfBirth));
    }
}