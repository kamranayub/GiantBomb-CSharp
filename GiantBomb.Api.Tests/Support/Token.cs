using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GiantBomb.Api.Tests.Support
{
    public static class Token
    {
        // relative to bin/Configuration
        const string FilePath = @"..\..\Support\api_token.private";

        /// <summary>
        /// Gets the API token for tests. Located in api_token.private folder in same folder or GIANTBOMB_API_TOKEN environment variable (CI).
        /// </summary>
        /// <returns></returns>
        public static string GetToken()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            if (File.Exists(Path.Combine(basePath, FilePath)))
            {
                return File.ReadAllText(FilePath);
            }
            else
            {
                return Environment.GetEnvironmentVariable("GIANTBOMB_API_TOKEN");
            }
            
        }
    }
}
