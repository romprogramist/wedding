using AlikAndFlorasWedding.Models;
using AlikAndFlorasWedding.Models.Dtos;
using AlikAndFlorasWedding.Services.ApplicationService;
using AlikAndFlorasWedding.Services.EmailService;
using AlikAndFlorasWedding.Services.TelegramService;
using Microsoft.AspNetCore.Mvc;
using RSiteTemplate.Models.Dtos;

namespace AlikAndFlorasWedding.Controllers.API;

[ApiController]
[Route("api/application")]
public class ApplicationController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly ITelegramService _telegramService;
    private readonly IApplicationService _applicationService;
    private readonly IWebHostEnvironment _env;

    public ApplicationController(IEmailService emailService, IWebHostEnvironment env, IApplicationService applicationService, ITelegramService telegramService)
    {
        _emailService = emailService;
        _applicationService = applicationService;
        _telegramService = telegramService;
        _env = env;
    }
    
    [HttpPost]
    [Route("send")]
    public async Task<IActionResult> SendApplication(ApplicationModel application)
    {
        var html = _emailService.CreateBody(application);

        var request = new EmailDto
        {
            Subject = "Заявка",
            Body = html
        };

        var requestTelegram = new TelegramDto
        {
            ChatIds = new List<string> {"168614886", "343210857"},
            Message = "*Имя:* " + application.Name + "\n" +
                      "*Телефон:* " + application.Phone + "\n" 
        };

        // await _applicationService.SaveApplicationAsync(application);
    
        // if (_env.IsDevelopment()) return Ok();

        await _emailService.SendEmailAsync(request);
        _ = Task.Run(async () => await _telegramService.SendTelegramAsync(requestTelegram));

        return Ok();
    }
}