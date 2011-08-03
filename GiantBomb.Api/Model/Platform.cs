using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiantBomb.Api.Model {
    public class Platform {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ApiDetailUrl { get; set; }
        public string SiteDetailUrl { get; set; }
    }
}
