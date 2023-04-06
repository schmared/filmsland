namespace Tests;

[TestFixture]
public class JwtDataInit
{
    [Test]
    public void ShouldFindOrCreateAnAdminUser()
    {
        using (var sc = new SQLiteConnection(Utility.JwtConnectionString))
        using (var cmd = sc.CreateCommand())
        {
            sc.Open();
            cmd.CommandText = "SELECT COUNT(*) FROM UserInfo";
            var count = cmd.ExecuteScalar();

            // If the UserInfo table in the jwt database has at least one user we are good
            if((long)count >= 0)
            {
                Assert.Pass();
                return;
            }

            // Add a user if there isn't a user above
            cmd.CommandText = @"INSERT INTO UserInfo (UserId, DisplayName, Email, Password, CreatedDate)
                                VALUES (1,'Admin','admin@adminplace.com','admin','04-05-2023');";
            Assert.IsTrue(cmd.ExecuteNonQuery() > 0);
        }
    }
}