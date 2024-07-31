using AlikAndFlorasWedding.Models;
using AlikAndFlorasWedding.Models.Dtos;
using AlikAndFlorasWedding.Services.EmailService;
using AlikAndFlorasWedding.Services.ReviewService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlikAndFlorasWedding.Controllers.API;

[ApiController]
[Route("api/review")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly IEmailService _emailService;
    private readonly IWebHostEnvironment _env;
    
    public ReviewController(IReviewService reviewService, IEmailService emailService, IWebHostEnvironment env)
    {
        _reviewService = reviewService;
        _emailService = emailService;
        _env = env;
    }
    
    [HttpPost]
    [Route("send")]
    public async Task<IActionResult> SendReview(ReviewModel review)
    {
        var html = _emailService.CreateBody(review);
        
        var request = new EmailDto
        {
            Subject = "Заявка",
            Body = html
        };

        await _reviewService.SaveNewReviewAsync(review);
        
        if (_env.IsDevelopment()) return Ok();
        
        await _emailService.SendEmailAsync(request);
        return Ok();
    }
    
    [HttpGet]
    [Route("approved")]
    public async Task<IActionResult> GetReviews()
    {
        return Ok(await _reviewService.GetApprovedReviewsAsync());
    }
    
    [HttpGet]
    [Route("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllReviews()
    {
        return Ok(await _reviewService.GetAllReviewsAsync());
    }
    
    [HttpPut]
    [Route("approve/{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ApproveReview(int id)
    {
        var result = await _reviewService.ApproveReviewAsync(id);

        if (result)
        {
            return Ok();
        }

        return BadRequest();
    }
}