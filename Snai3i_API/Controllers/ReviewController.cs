using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snai3i_BLL.DTO.ReviewDto;
using Snai3i_BLL.Manager.ReviewsManager;

namespace Snai3i_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewManager _reviewManager;

        public ReviewController(IReviewManager reviewManager )
        {
            _reviewManager = reviewManager;
        }
        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewAddDTO reviewAddDTO)
        {
            try
            {
                await _reviewManager.AddReviewAsync(reviewAddDTO);
                return Ok("Review Added Successfully");
            }
            catch (Exception ex) {

                return Content($" unfortunately your review is not added + {ex.InnerException.Message}"); 
            }
        }
        [HttpGet]
        [Route("{workerId}")]
        public async Task<IActionResult> GetReviewsForWorker(string workerId)
        {
            try
            {
                var reviews = await _reviewManager.GetReviewsForWorkerAsync(workerId);
                return Ok(reviews);

            }
            catch (Exception ex) {

                return NotFound(ex.InnerException.Message); 
            }
        }
        [HttpGet]
        [Route("{Id:int}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var review = await _reviewManager.GetByIdAsync(Id);
            if (review == null)
            {
                return NotFound("Key Not Found");
            }
            return Ok(review);
        }

        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> Edit(int Id, ReviewUpdateDTO reviewUpdateDTO)
        {
                if (Id != reviewUpdateDTO.Id)
                {
                    return BadRequest();
                }

                await _reviewManager.UpdateAsync(reviewUpdateDTO);
                return NoContent();
        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> DeleteReview(int Id)
        {
            try
            {
                await _reviewManager.DeleteAsync(Id);
                return Content("your review is deleted successfuly!");

            }
            catch (Exception ex) {
                return NotFound(ex.InnerException.Message); 
            }


        }
    }
}
