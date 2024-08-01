using Microsoft.EntityFrameworkCore;
using RSiteTemplate.Models.Dtos;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace AlikAndFlorasWedding.Services.TelegramService;

public class TelegramService : ITelegramService
{
    public async Task SendTelegramAsync(TelegramDto request)
    {
        var botToken = "7153586316:AAHlcDz7HwEVPxsY0N-yQ70Y3E8R6SRhr7E";

        var botClient = new TelegramBotClient(botToken);

        // Отправить сообщение каждому чату из списка
        foreach (var chatId in request.ChatIds)
        {
            await botClient.SendTextMessageAsync(chatId, request.Message);
        }

        // Запустить обработку обновлений в фоновой задаче
        _ = Task.Run(() => StartReceivingUpdates(botClient));
    }

    private async Task StartReceivingUpdates(TelegramBotClient botClient)
    {
        using CancellationTokenSource cts = new();

        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        botClient.StartReceiving(
            updateHandler: async (bot, update, token) => await HandleUpdateAsync(bot, update, token),
            pollingErrorHandler: async (bot, exception, token) => await HandlePollingErrorAsync(bot, exception, token),
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token
        );

        // Ожидать нажатия любой клавиши перед остановкой обработки обновлений
        Console.WriteLine("Press any key to stop receiving updates...");
        Console.ReadKey();

        // Отправить запрос на отмену для остановки приема обновлений
        cts.Cancel();
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, DbLoggerCategory.Update update, CancellationToken cancellationToken)
    {
        // Обработка обновлений, если это необходимо
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}