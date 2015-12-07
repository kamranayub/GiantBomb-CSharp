using System.Collections.Generic;
using System.Threading.Tasks;
using GiantBomb.Api.Model;

namespace GiantBomb.Api {
    public partial class GiantBombRestClient {
        public Platform GetPlatform(int id, string[] limitFields = null)
        {
            return GetPlatformAsync(id, limitFields).WaitForResult();
        }

        public Task<Platform> GetPlatformAsync(int id, string[] limitFields = null) {
            return GetSingleResourceAsync<Platform>("platform", ResourceTypes.Platforms, id, limitFields);
        }

        public IEnumerable<Platform> GetPlatforms(int page = 1, int pageSize = GiantBombBase.DefaultLimit,
            string[] limitFields = null)
        {
            return GetPlatformsAsync(page, pageSize, limitFields).WaitForResult();
        }

        public Task<IEnumerable<Platform>> GetPlatformsAsync(int page = 1, int pageSize = GiantBombBase.DefaultLimit, string[] limitFields = null) {
            return GetListResourceAsync<Platform>("platforms", page, pageSize, limitFields);
        }
    }
}
