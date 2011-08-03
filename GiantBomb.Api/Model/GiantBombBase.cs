using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiantBomb.Api.Model {
    public abstract class GiantBombBase {
        public string Error { get; set; }
        public int Limit { get; set; }
        public int NumberOfPageResults { get; set; }
        public int NumberOfTotalResults { get; set; }
        public int StatusCode { get; set; }

        public const int StatusOk = 1;
    }

    public class GiantBombResult<TResult> : GiantBombBase
    {
        public TResult Results { get; set; }
    }

    public class GiantBombResults<TResult> : GiantBombBase {
        public List<TResult> Results { get; set; }
    }
}
