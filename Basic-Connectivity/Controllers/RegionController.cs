using Basic_Connectivity.Models;
using Basic_Connectivity.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity.Controllers
{
    public class RegionController
    {
        private Region _region;
        private RegionView _regionview;

        public RegionController(Region region, RegionView regionview)
        {
            _region = region;
            _regionview = regionview;
        }
    }
}
