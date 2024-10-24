using AutoMapper;
using Microsoft.AspNetCore.Http;
using Snai3i_BLL.DTO.ReviewDto;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.ReviewsRepository;
using Snai3i_DAL.Data.Repository.UserWorkerTransactionUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Manager.ReviewsManager
{
    public class ReviewManager : IReviewManager
    {
        private readonly IUserWorkerTransactionUnitOfWork _userWorkerTransaction;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper mapper;
        //private readonly IReviewsRepository _reviewsRepository;

        public ReviewManager(IUserWorkerTransactionUnitOfWork userWorkerTransaction,
            IHttpContextAccessor httpContextAccessor )
        {
            _userWorkerTransaction = userWorkerTransaction;
            _httpContextAccessor = httpContextAccessor;
            //_reviewsRepository = reviewsRepository;
        }
        public async Task AddReviewAsync( ReviewAddDTO reviewAddDTO)
        {
            try
            {
                //var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                //var userName = _httpContextAccessor.HttpContext.User.Identity.Name;

                var review = new Review()
                {
                    UserId = reviewAddDTO.UserId,
                    WorkerId = reviewAddDTO.WorkerId,
                    OrderId = reviewAddDTO.OrderId,
                    Rate = reviewAddDTO.Rating,
                    Comment = reviewAddDTO.Comment,
                }; 
                

                _userWorkerTransaction.CreateTransaction();

                await _userWorkerTransaction.Reviewss.InsertAsync(review);

                await _userWorkerTransaction.Save();
                _userWorkerTransaction.Commit();


            }
            catch (Exception ex) { 
                _userWorkerTransaction.Rollback();
            }

        }

        public async Task<IEnumerable<ReviewReadDTO>> GetReviewsForWorkerAsync(string workerId)
        {

            var reviews = await _userWorkerTransaction.Reviewss.GetReviewsForWorkerAsync( workerId ) ;
            if (reviews == null)
            {
                throw new Exception("worker not found");
            }
            return reviews.Select(review => new ReviewReadDTO()
            {
                UserName = review.user.UserName,    // Access individual review and its related entities
                workername = review.worker.UserName,    // Assuming Worker has a 'Name' property
                Rate = review.Rate,
                Comment = review.Comment
            }).ToList();
        }
        public async Task<ReviewReadDTO> GetByIdAsync(int id)
        {
            var ReviewModel = await _userWorkerTransaction.Reviewss.getreviewsbyid(id);
            if (ReviewModel == null)
            {
                throw new Exception("not found"); 
            }

            var ReviewReadDTo = new ReviewReadDTO
            {
                UserName = ReviewModel.user.UserName,
                workername = ReviewModel.worker.UserName,
                Rate = ReviewModel.Rate,
                Comment = ReviewModel.Comment,
            };



            return ReviewReadDTo;
        }
        public async Task UpdateAsync(ReviewUpdateDTO reviewUpdateDTO)
        {
            try
            {


                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userName = _httpContextAccessor.HttpContext.User.Identity.Name;

                var ReviewModel = await _userWorkerTransaction.Reviewss.GetByIdAsync(reviewUpdateDTO.Id);
                ReviewModel.Rate = reviewUpdateDTO.Rate;
                ReviewModel.Comment = reviewUpdateDTO.Comment; 

                _userWorkerTransaction.CreateTransaction();

                await _userWorkerTransaction.Reviewss.UpdateAsync(ReviewModel);
                await _userWorkerTransaction.Save();
                _userWorkerTransaction.Commit();

            }
            catch (Exception ex) {
                _userWorkerTransaction.Rollback(); 
            }

        }
        public async Task DeleteAsync(int id)
        {

            var ReviewModel = await _userWorkerTransaction.Reviewss.GetByIdAsync(id);
            if(ReviewModel == null)
            {
                throw new Exception(" review dosen't exist "); 
            }
            try
            {


                ReviewModel.deletedbyId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                ReviewModel.deleteddatetime = DateTime.Now;
                _userWorkerTransaction.CreateTransaction();
                await _userWorkerTransaction.Reviewss.DeleteAsync(id);
                await _userWorkerTransaction.Save();
                _userWorkerTransaction.Commit();
            }
            catch (Exception ex) {
                _userWorkerTransaction.Rollback(); 
            }
        }
    }
}
