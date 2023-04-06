namespace Tests
{
    [TestFixture]
    public class MovieDataInit
    {
        [Test]
        public void ShouldSetTestDataInFilmsdb()
        {
            
        }


        // [Test]
        // //[Category("init")]
        // [Ignore("TODO: formatting input data")]
        // public void ShouldResetAllTheTestData()
        // {
        //     // Get all data file names from the DataImport folder
        //     // Directory.GetFiles(Utility.ImportPath));
            
        //     if(!File.Exists(Utility.JwtDbPath))
        //         Assert.Fail("Jwt.db not found");

        //     if(File.Exists(Utility.FilmsApiDbPath))
        //         Assert.Fail("FilmsApi.db not found");

        //     using (var sc = new SQLiteConnection(Utility.FilmsApiConnectionString))
        //     using (var tr = sc.BeginTransaction())
        //     using (var cmd = sc.CreateCommand())
        //     {
        //         // Check to see if empty database
        //         cmd.CommandText = @"SELECT COUNT(*) FROM movie;";
        //         var result1 = cmd.ExecuteScalar();
        //         if (result1 != null && (int)result1 > 0)
        //         {
        //             // Truncate Films db tables
        //             cmd.CommandText = @"DELETE FROM movie; DELETE FROM movie_rating; DELETE FROM actor;";
        //             cmd.ExecuteNonQuery();
        //         }

        //         cmd.CommandText = @"INSERT INTO movie ([Id],[Title],[ReleaseDate],[Runtime]) VALUES (@id,@title,@releaseDate,@runTime)";

        //         var idParameter = new SQLiteParameter("@id", SqliteType.Integer);
        //         var titleParameter = new SQLiteParameter("@title", SqliteType.Text);
        //         var releaseDateParameter = new SQLiteParameter("@releaseDate", SqliteType.Text);
        //         var runTimeParameter = new SQLiteParameter("@runTime", SqliteType.Integer);

        //         cmd.Parameters.Add(idParameter);
        //         cmd.Parameters.Add(titleParameter);
        //         cmd.Parameters.Add(releaseDateParameter);
        //         cmd.Parameters.Add(runTimeParameter);

        //         int id = 0;
        //         string title = string.Empty;
        //         DateOnly releaseDate = DateOnly.MinValue;
        //         int runTime = 0;
        //         string[] columns = new string[0];

        //         // Insert a lot of movie data
        //         foreach (var line in File.ReadAllLines(Path.Combine(Utility.ImportPath, "Movie.csv")))
        //         {
        //             columns = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

        //             id = int.Parse(columns[0]);
        //             title = columns[1];
        //             releaseDate = DateOnly.Parse(columns[2]);
        //             runTime = int.Parse(columns[3]);

        //             idParameter.Value = id;
        //             titleParameter.Value = title;
        //             releaseDateParameter.Value = releaseDate;
        //             runTimeParameter.Value = runTime;

        //             cmd.ExecuteNonQuery();
        //         }

        //         tr.Commit();
        //     }

        //     Assert.Pass();
        // }
    }
}