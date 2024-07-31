using AlikAndFlorasWedding.Models;
using AlikAndFlorasWedding.Models.Dtos;

namespace AlikAndFlorasWedding.Services.EmailService;

public interface IEmailService
{
    Task SendEmailAsync(EmailDto request);
    string CreateBody(ApplicationModel application);
    string CreateBody(ReviewModel review);
}