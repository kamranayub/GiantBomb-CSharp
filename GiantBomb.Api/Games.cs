using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiantBomb.Api.Model;
using RestSharp;

namespace GiantBomb.Api
{
    public partial class GiantBombRestClient
    {

        /// <summary>
        /// Gets a game with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Game GetGame(int id)
        {
            var request = new RestRequest
            {
                Resource = "game/{GameId}", 
                RootElement = "results"
            };

            request.Method = Method.GET;
            request.AddUrlSegment("GameId", id.ToString());

            return Execute<Game>(request);
        }
    }
}
