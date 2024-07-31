using AlikAndFlorasWedding.Data;
using AlikAndFlorasWedding.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AlikAndFlorasWedding.Services.ReviewService;

public class ReviewService : IReviewService
{
    private readonly DataContext _context;

    public ReviewService(DataContext context)
    {
        _context = context;
    }
    
    public async Task SaveNewReviewAsync(ReviewModel review)
    {
        await _context.Reviews.AddAsync(new Review
        {
            CreationDateTime = DateTime.Now.ToUniversalTime(),
            Name = review.Name,
            Phone = review.Phone,
            Text = review.Text,
            Email = review.Email,
            SitePage = review.SitePage,
            AdditionalInfo = review.AdditionalInfo,
            IsApproved = false,
            Rate = review.Rate,
            UtmInfo = review.UtmInfo,
        });

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ReviewModel>> GetApprovedReviewsAsync()
    {
        return await _context.Reviews
            .Where(r => r.IsApproved)
            .Select(r => new ReviewModel
            {
                Name = r.Name,
                Phone = r.Phone,
                Email = r.Email,
                Text = r.Text,
                Rate = r.Rate,
                UtmInfo = r.UtmInfo,
                SitePage = r.SitePage,
                AdditionalInfo = r.AdditionalInfo,
                IsApproved = r.IsApproved
            }).ToListAsync();
    }

    public async Task<IEnumerable<ReviewModel>> GetAllReviewsAsync()
    {
        return await _context.Reviews
            .Select(r => new ReviewModel
            {
                Id = r.Id,
                Name = r.Name,
                Phone = r.Phone,
                Email = r.Email,
                Text = r.Text,
                Rate = r.Rate,
                UtmInfo = r.UtmInfo,
                SitePage = r.SitePage,
                AdditionalInfo = r.AdditionalInfo,
                IsApproved = r.IsApproved
            }).ToListAsync();
    }

    public async Task<bool> ApproveReviewAsync(int id)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);

        if (review == null || review.IsApproved)
        {
            return false;
        }
        
        review.IsApproved = true; 
        var savedCount = await _context.SaveChangesAsync();
        return savedCount > 0;
    }
}