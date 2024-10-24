using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Models
{
    public enum usertype
    {
       SuperAdmin ,Admin , User , Worker 
    }


    public class ApplicationUser :  IdentityUser
    {




        public string LastName { get; set; }


        public string  image {  get; set; }

        public bool Isdeleted { get; set; }
        public string? deletedbyId { get; set; }

        public DateTime? deleteddatetime { get; set; }

        public string? createdbyId { get; set; }

        public string? createdbyName { get; set; }

        public DateTime? createddatetime { get; set; }

        public string? modifiedbyId { get; set; }

        public string? modifiedbyName { get; set; }

        public DateTime? modifiedDatetime { get; set; }

        public ICollection<Chat> SentChats { get; set; } = new HashSet<Chat>();
        public ICollection<Chat> ReceivedChats { get; set; } = new HashSet<Chat>();

        public ICollection<Tool> tools { get; set; } = new HashSet<Tool>();


        public ICollection<Card> cards { get; set; } = new HashSet<Card>();



    }
}
