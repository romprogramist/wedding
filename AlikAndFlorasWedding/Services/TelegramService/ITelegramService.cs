using AlikAndFlorasWedding.Models.Dtos;

namespace AlikAndFlorasWedding.Services.TelegramService;

public interface ITelegramService
{
    Task SendTelegramAsync(TelegramDto request);
}