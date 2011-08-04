using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiantBomb.Api.Model;
using RestSharp;

namespace GiantBomb.Api {
    public partial class GiantBombRestClient {
        public Platform GetPlatform(int id, string[] limitFields = null) {
            return GetSingleResource<Platform>("platform", id, limitFields);
        }

        /// <summary>
        /// Gets list of games
        /// </summary>        
        /// <returns></returns>
        public IEnumerable<Platform> GetPlatforms(int page = 1, int pageSize = 20, string[] limitFields = null) {
            return GetListResource<Platform>("platforms", page, pageSize, limitFields);
        }
    }
}
