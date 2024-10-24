using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.service
{
    public interface Ifileservice
    {
        public Tuple<int, string> SaveImage(IFormFile Imagefile);

        public bool DeleteImage(string productImage);
    }
}
