using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snai3i_BLL.DTO.CardDTOS;
using Snai3i_BLL.DTO.CraftDto;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.CardRepository;
using Snai3i_DAL.Data.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Manager.CardManager
{
    public class CardManager : ICardManager

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CardManager(IUnitOfWork unitOfWork, IMapper mapper )
        {
           
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Add

        public async Task Add(CardDTO cardDTO)
        {
            _unitOfWork.CreateTransaction();
            var card = await _unitOfWork.Cards.GetCardByUseAndTool(cardDTO.BuyerId, cardDTO.ToolId);
            if (card == null)
            {

                await _unitOfWork.Cards.InsertAsync(_mapper.Map<Card>(cardDTO));
            }
            else
            {
                _unitOfWork.Cards.IncreaseCount(card, cardDTO.Quantity);
            }
                await _unitOfWork.CompleteAsync();

                _unitOfWork.Commit();
            

        }

       
        //Delete

        public async Task Delete(int id)
        {
       
            _unitOfWork.CreateTransaction();
            await _unitOfWork.Cards.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            _unitOfWork.Commit();
        }

        //public async Task<IEnumerable<CardDTO>> GetCartbyuser(CardDTO cradDTO)
        //{
        //   var model = _mapper.Map<List<CardDTO>>(await _unitOfWork.Cards.GetCardbybuyer(cradDTO.BuyerId));
        //    foreach (var item in model)
        //    {
        //        cradDTO.TotalPrice += item.Price * item.Quantity;
        //    }
        //    return model;
        //}

        public async Task Increase(int id)
        {
            _unitOfWork.CreateTransaction();
            var shopping = await _unitOfWork.Cards.GetByIdCart(id);
            _unitOfWork.Cards.IncreaseCount(shopping, 1);
          await _unitOfWork .Cards.saveAsync();
            _unitOfWork.Commit();
           
        }
        public async Task Decrease(int id)
        {
            _unitOfWork.CreateTransaction();
            var shopping = await _unitOfWork.Cards.GetByIdCart(id);
            if (shopping.Quantity <= 1)
            {
                _unitOfWork.Cards.DeleteAsync(shopping);
                await _unitOfWork.Cards.saveAsync();
                _unitOfWork.Commit();
            }
            else
            {
                _unitOfWork.Cards.DecreaseCount(shopping, 1);
                await _unitOfWork.Cards.saveAsync();
                _unitOfWork.Commit();
            }
           
        }


        //async Task<CardDTO> ICardManager.GetById(int id)
        //{
        //    return _mapper.Map<CardDTO>(await _unitOfWork.Cards.GetByIdCart(id));
        //}

		public async Task<IEnumerable<CardDTO>> GetAllCardsAsync(string buyerId)
		{
			var cards = await _unitOfWork.Cards.GetAllCardsAsync(buyerId);

			var cardDtos = cards.Select(card => new CardDTO
			{
				ToolName = card.tool.Name,
				Quantity = card.Quantity,
				Size = card.size.ToolSize,
				
				Price = card.size.Price  // apply sale price if available
			});

			return cardDtos;
		}

		public async Task AddCardAsync(CardDTO cardDto)
		{
			var card = new Card
			{
				ToolId = cardDto.ToolId,
				SizeId = cardDto.SizeId,
				Quantity = cardDto.Quantity,
				BuyerId = cardDto.BuyerId
			};

			await _unitOfWork.Cards.AddCardAsync(card);
		}

		public async Task RemoveCardAsync(int cardId)
		{
			await _unitOfWork.Cards.RemoveCardAsync(cardId);
		}

		 }
	


	}
    
