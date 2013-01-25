using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiantBomb.Api.Model;

namespace GiantBomb.Api
{
    public partial class GiantBombRestClient
    {
        public Region GetRegion(int id, string[] limitFields = null)
        {
            return GetSingleResource<Region>("region", ResourceTypes.Regions, id, limitFields);
        }

        public IEnumerable<Region> GetRegions(int page = 1, int pageSize = GiantBombBase.DefaultLimit, string[] limitFields = null)
        {
            return GetListResource<Region>("regions", page, pageSize, limitFields);
        }
    }
}
