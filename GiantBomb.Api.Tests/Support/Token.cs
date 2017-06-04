using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GiantBomb.Api.Tests.Support
{
    public static class Token
    {
        public const string EnvironmentVariable = "GIANTBOMB_API_TOKEN";

        // relative to bin/Configuration
        static readonly string FilePath = Path.Combine("..", "..", "..", "Support", "api_token.private");

        public static string SearchPath => Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), FilePath));

        /// <summary>
        /// Gets the API token for tests. Located in api_token.private folder in same folder or GIANTBOMB_API_TOKEN environment variable (CI).
        /// </summary>
        /// <returns></returns>
        public static string GetToken()
        {
            if (File.Exists(SearchPath))
            {
                return File.ReadAllText(FilePath);
            }
            else
            {
                return Environment.GetEnvironmentVariable(EnvironmentVariable);
            }
            
        }
    }
}
