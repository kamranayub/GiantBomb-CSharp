using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiantBomb.Api.Model;

namespace GiantBomb.Api
{
    public partial class GiantBombRestClient
    {

        /// <summary>
        /// Gets a region with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Region GetRegion(int id, string[] limitFields = null)
        {
            return GetSingleResource<Region>("region", id, limitFields);
        }

        /// <summary>
        /// Gets list of regions
        /// </summary>        
        /// <returns></returns>
        public IEnumerable<Region> GetRegions(int page = 1, int pageSize = 20, string[] limitFields = null)
        {
            return GetListResource<Region>("regions", page, pageSize, limitFields);
        }
    }
}
