using Snai3i_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.service.MapService
{
    public interface IMapService
    {

        public Task<CustomMap> GetLocationFromAddressAsync( string address );  

    }
}
