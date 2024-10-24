using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Models
{
    public class Deletebase
    {
    public bool Isdeleted { get; set; }
    public string? deletedbyId { get; set; }

    public DateTime? deleteddatetime { get; set; }

     public string? createdbyId { get; set; }

    public string? createdbyName { get; set; }

    public DateTime? createddatetime { get; set; }

    public string? modifiedbyId { get; set; }

    public string? modifiedbyName { get; set; }

    public DateTime? modifiedDatetime { get; set; }


    }
}
