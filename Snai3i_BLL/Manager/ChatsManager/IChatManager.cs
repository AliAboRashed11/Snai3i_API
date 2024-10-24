using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Manager.ChatsManager
{
    public interface IChatManager
    {


        public Task<string> AddNewConversation(string userId, string recieverId);



    }
}
