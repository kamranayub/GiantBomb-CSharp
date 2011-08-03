using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiantBomb.Api.Model;
using RestSharp;

namespace GiantBomb.Api {
    public partial class GiantBombRestClient {
        public Platform GetPlatform(int id)
        {
            var request = new RestRequest {
                Resource = "platform/{Id}//"
            };

            request.AddUrlSegment("Id", id.ToString());

            var result = Execute<GiantBombResult<Platform>>(request);

            if (result.StatusCode == GiantBombBase.StatusOk)
                return result.Results;

            return null;
            
        }

        /// <summary>
        /// Gets list of games
        /// </summary>        
        /// <returns></returns>
        public IEnumerable<Platform> GetPlatforms(int page = 1, int pageSize = 25) {
            var request = new RestRequest {
                Resource = "platforms//"
            };

            if (page > 1) {
                request.AddParameter("offset", pageSize * (page - 1));
            }

            request.AddParameter("limit", pageSize);

            var result = Execute<GiantBombResults<Platform>>(request);

            if (result.StatusCode == GiantBombBase.StatusOk)
                return result.Results;

            return null;
        }
    }
}
