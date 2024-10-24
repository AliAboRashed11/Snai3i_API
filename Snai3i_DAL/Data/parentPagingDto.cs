using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.ParentPagingDto
{
    public class parentPagingDto
    {
        const int MaxPageSize = 50;

        public int PageNumber { get; set; }
        private int _PageSize = 10;

        public int pagesize
        {
            get
            {
                return _PageSize;
            }

            set
            {
                _PageSize = (value > MaxPageSize ? MaxPageSize : value);
            }
        }


    }
}
