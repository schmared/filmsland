namespace Tests;

public static class Utility
{
    private static string _importPath = string.Empty;
    public static string ImportPath
    {
        get
        {
            if (_importPath == string.Empty)
            {
                var path1 = Directory.GetParent(Environment.CurrentDirectory)?.FullName ?? string.Empty;
                if (path1 == string.Empty)
                    throw new FileLoadException("Could not find DataImport file path.");

                var path2 = Directory.GetParent(path1)?.FullName ?? string.Empty;
                if (path2 == string.Empty)
                    throw new FileLoadException("Could not find DataImport file path.");

                var path3 = Directory.GetParent(path2)?.FullName ?? string.Empty;
                if (path3 == string.Empty)
                    throw new FileLoadException("Could not find DataImport file path.");
                                
                _importPath = Path.Combine(path3, "DataImport");
            }

            return _importPath;
        }
    }

    public static string FilmsApiPath => @"..\..\..\..\filmsApi\";
    public static string JwtDbPath => @"..\..\..\..\filmsApi\Jwt.db";
    public static string FilmsApiDbPath => @"..\..\..\..\filmsApi\FilmsApi.db";
    public static string JwtConnectionString => @"Data Source='..\..\..\..\filmsApi\Jwt.db';Cache=Shared";
    public static string FilmsApiConnectionString => @"Data Source='..\..\..\..\filmsApi\FilmsApi.db';Cache=Shared";
}