using Microsoft.EntityFrameworkCore;
using Snai3i_DAL.Data.Context;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.CardRepository
{
    public class CartRebo : GenericRepository<Card>, ICartRebo
    {
        private readonly SnaiiDBContext _context;

        public CartRebo(SnaiiDBContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Card>> GetAllCart()
        {
            return await _context.cards
                           .Include(c => c.tool)
                          
                           .Include(c => c.size)
                           .Include(c => c.buyer)
            .ToListAsync();
        }

        public async Task<Card> GetByIdCart(int id)
        {
            return await _context.cards
                                       .Include(c => c.tool)
                       
                           .Include(c => c.size)
                           .Include(c => c.buyer)
                                       .FirstOrDefaultAsync(c => c.Id == id);

        }

        public async Task<IEnumerable<Card>> GetCardbybuyer(string Id)
        {
            return await _context.cards.Include(c => c.tool)
                           
                           .Include(c => c.size)
                           .Include(c => c.buyer).Where(a=> a.BuyerId == Id).ToListAsync();
        }

        public async Task<Card> GetCardByUseAndTool(string buyerId, int ToolId)
        {
            return await _context.cards
                           .FirstOrDefaultAsync(a => a.BuyerId == buyerId && a.ToolId==ToolId);
        }

        public int IncreaseCount(Card card, int count)
        {
            card.Quantity += count;
            return card.Quantity;
        }



        public int DecreaseCount(Card card, int count)
        {
            card.Quantity -= count;
            return card.Quantity;
        }

		public async Task<IEnumerable<Card>> GetAllCardsAsync(string buyerId)
		{
			return await _context.cards
		  .Include(c => c.tool)
		  .Include(c => c.size)
		  
		  .Where(c => c.BuyerId == buyerId)
		  .ToListAsync();
		}

		public async Task AddCardAsync(Card card)
		{
			await _context.cards.AddAsync(card);
			await _context.SaveChangesAsync();
		}

		public async Task RemoveCardAsync(int cardId)
		{
			var card = await _context.cards.FindAsync(cardId);
			if (card != null)
			{
				_context.cards.Remove(card);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<Card> GetCardByIdAsync(int cardId)
		{
			return await _context.cards
		   .Include(c => c.tool)
		   .Include(c => c.size)
		   
		   .FirstOrDefaultAsync(c => c.Id == cardId);
		}
	}
}
