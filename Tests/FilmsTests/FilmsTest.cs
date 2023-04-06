using filmsApi.Models.Parameters;

namespace Tests
{
    [TestFixture]
    public class FilmsTests
    {
        private IConfiguration? _configuration;
        private FilmsContext? _context;
        private IActorService? _actorService;

        [SetUp]
        public void Setup()
        {
            _configuration = new ConfigurationBuilder().AddJsonStream(new MemoryStream(
                File.ReadAllBytes( "..\\..\\..\\appsettings.Production.json"))).Build();
            _context = new FilmsContext(_configuration);
            _actorService = new ActorService(_context);
        }

        [Test]
        public void FilmsTest()
        {
            var query = _context?.Actors?.AsQueryable();

            if (query == null)
            {
                Assert.Fail();
                return;
            }

            var origReduced = query.Expression.Reduce();

            var newQuery =_actorService?.QueryConditionalSearchParameters(query, new ActorSearchParameters { 
                NamePart = "Rob", YearOfBirth = 1956, Page = 1, PageSize = 2
            });

            if (newQuery == null)
            {
                Assert.Fail();
                return;
            }
            
            Assert.Pass();
        }
    }
}