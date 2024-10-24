using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.CardRepository
{
   public interface ICartRebo:IGenericRepository<Card>
    {
        

      
        public int IncreaseCount( Card card ,int count);
        public int DecreaseCount(Card card, int count);
        public Task<Card> GetCardByUseAndTool(string buyerId, int ToolId);

		Task<IEnumerable<Card>> GetAllCardsAsync(string buyerId);
		Task AddCardAsync(Card card);
		Task RemoveCardAsync(int cardId);
		Task<Card> GetCardByIdAsync(int cardId); 



    }
}
