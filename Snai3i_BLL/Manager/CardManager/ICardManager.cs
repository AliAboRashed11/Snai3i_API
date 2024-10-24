using Snai3i_BLL.DTO.CardDTOS;
using Snai3i_BLL.DTO.CraftDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Manager.CardManager
{
    public interface ICardManager
    {
      

        

    

       
      
        public Task Delete(int id);
        

        public Task Increase (int id);  
        public Task Decrease (int id);

		Task<IEnumerable<CardDTO>> GetAllCardsAsync(string buyerId);
		Task AddCardAsync(CardDTO cardDto);
		

	}
}
