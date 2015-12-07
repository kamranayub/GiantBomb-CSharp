using System.Collections.Generic;
using System.Threading.Tasks;
using GiantBomb.Api.Model;

namespace GiantBomb.Api
{
    public partial class GiantBombRestClient
    {
        public Region GetRegion(int id, string[] limitFields = null)
        {
            return GetRegionAsync(id, limitFields).WaitForResult();
        }

        public Task<Region> GetRegionAsync(int id, string[] limitFields = null)
        {
            return GetSingleResourceAsync<Region>("region", ResourceTypes.Regions, id, limitFields);
        }

        public IEnumerable<Region> GetRegions(int page = 1, int pageSize = GiantBombBase.DefaultLimit, string[] limitFields = null)
        {
            return GetRegionsAsync(page, pageSize, limitFields).WaitForResult();
        }

        public Task<IEnumerable<Region>> GetRegionsAsync(int page = 1, int pageSize = GiantBombBase.DefaultLimit, string[] limitFields = null)
        {
            return GetListResourceAsync<Region>("regions", page, pageSize, limitFields);
        }
    }
}
