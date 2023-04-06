namespace Tests
{
    [TestFixture]
    public class JwtTests
    {
        private IUserInfoService? _userInfoService;
        private IConfiguration? _configuration;
        private JwtContext? _context;

        [SetUp]
        public void Setup()
        {        
            _configuration = new ConfigurationBuilder().AddJsonStream(new MemoryStream(
                File.ReadAllBytes( "..\\..\\..\\appsettings.Production.json"))).Build();
            _context = new JwtContext(_configuration);
            _userInfoService = new UserInfoService(_configuration, _context);
        }

        [Test]
        public void ShouldValidateUserData()
        {
            if (_userInfoService == null)
            {
                Assert.Fail();
                return;
            }

            Assert.IsTrue(_userInfoService.IsValidUserData(new filmsApi.Models.UserInfo { Email = "admin@adminplace.com", Password = "admin" }));
            Assert.IsFalse(_userInfoService.IsValidUserData(new filmsApi.Models.UserInfo { Email = null, Password = null }));
        }


        [Test]
        public void ShouldValidateUserEmailAndPassword()
        {
            if (_userInfoService == null)
            {
                Assert.Fail();
                return;
            }

            var email = "admin@adminplace.com";
            var password = "admin";

            var (userData, userInfoResponse) = _userInfoService.VerifyUserAndPassword(email, password);

            Assert.NotNull(userData);
            Assert.True(userInfoResponse == UserInfoResponse.Good);
        }

        [Test]
        public void ShouldUserNotExistWhenEmailAndPasswordWhenInvalidUser()
        {
            if (_userInfoService == null)
            {
                Assert.Fail();
                return;
            }

            var email = "dne@no.com";
            var password = "no";

            var (userData, userInfoResponse) = _userInfoService.VerifyUserAndPassword(email, password);

            Assert.IsNull(userData);
            Assert.True(userInfoResponse == UserInfoResponse.DoesNotExist);
        }

        [Test]
        public void ShouldNotValidateUserEmailAndPasswordWhenUserIsEmpty()
        {
            if (_userInfoService == null)
            {
                Assert.Fail();
                return;
            }

            var email = string.Empty;
            var password = string.Empty;

            var (userData, userInfoResponse) = _userInfoService.VerifyUserAndPassword(email, password);

            Assert.IsNull(userData);
            Assert.True(userInfoResponse == UserInfoResponse.Invalid);
        }

        [Test]
        public void ShouldNotValidateUserEmailAndPasswordWhenInvalidNull()
        {
            if (_userInfoService == null)
            {
                Assert.Fail();
                return;
            }

            string? email = null;
            string? password = null;

            var (userData, userInfoResponse) = _userInfoService.VerifyUserAndPassword(email, password);

            Assert.IsNull(userData);
            Assert.True(userInfoResponse == UserInfoResponse.Invalid);
        }
    }
}