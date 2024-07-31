﻿using AlikAndFlorasWedding.Models;
using AlikAndFlorasWedding.Models.Dtos;
using AlikAndFlorasWedding.Services.ApplicationService;
using AlikAndFlorasWedding.Services.EmailService;
using Microsoft.AspNetCore.Mvc;

namespace AlikAndFlorasWedding.Controllers.API;

[ApiController]
[Route("api/application")]
public class ApplicationController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly IApplicationService _applicationService;
    private readonly IWebHostEnvironment _env;

    public ApplicationController(IEmailService emailService, IWebHostEnvironment env, IApplicationService applicationService)
    {
        _emailService = emailService;
        _applicationService = applicationService;
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
        
        await _applicationService.SaveApplicationAsync(application);

        if (_env.IsDevelopment()) return Ok();
        
        await _emailService.SendEmailAsync(request);
        return Ok();
    }
}