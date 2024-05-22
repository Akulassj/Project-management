using ProjectManagement.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

public class Program
{
    private static readonly string BotToken = "7098372214:AAEzXYZBa79pHUaBihl1epPbDrkpUvxhuEo";
    private static readonly ITelegramBotClient botClient = new TelegramBotClient(BotToken);
    private static readonly HttpClient httpClient = new HttpClient();

    public static async Task Main()
    {
        using var cts = new CancellationTokenSource();

        // Start receiving updates
        botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            new ReceiverOptions { AllowedUpdates = { } },
            cancellationToken: cts.Token
        );

        Console.WriteLine("Bot is up and running...");
        Console.ReadLine();
        cts.Cancel();
    }

    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.Message && update.Message.Type == MessageType.Text)
        {
            var chatId = update.Message.Chat.Id;
            var text = update.Message.Text;

            if (text.StartsWith("/start"))
            {
                await SendWelcomeMessage(chatId, cancellationToken);
            }
            else if (text.StartsWith("/tasks"))
            {
                await HandleTasksCommand(chatId, cancellationToken);
            }
            else if (text.StartsWith("/todaytasks"))
            {
                await HandleTodayTasksCommand(chatId, cancellationToken);
            }
            else if (text.StartsWith("/menu"))
            {
                await SendMenu(chatId, cancellationToken);
            }
            else if (text.StartsWith("/help"))
            {
                await SendHelpMessage(chatId, cancellationToken);
            }
            else
            {
                await botClient.SendTextMessageAsync(chatId, "Неизвестная команда. Пожалуйста используйте /menu чтобы увидеть доступные команды.", cancellationToken: cancellationToken);
            }
        }
        else if (update.Type == UpdateType.CallbackQuery)
        {
            var callbackQuery = update.CallbackQuery;
            var chatId = callbackQuery.Message.Chat.Id;
            var data = callbackQuery.Data;

            if (data.StartsWith("/tasks"))
            {
                await HandleTasksCommand(chatId, cancellationToken);
            }
            else if (data.StartsWith("/todaytasks"))
            {
                await HandleTodayTasksCommand(chatId, cancellationToken);
            }
        }
    }

    private static async Task SendWelcomeMessage(long chatId, CancellationToken cancellationToken)
    {
        var welcomeMessage = "Добро пожаловать, я ваш бот для управления проектами.\n\n" +
                             "Вы можете управлят мной с помощью следующих команд:\n" +
                             "/start - Стартовое сообщение\n" +
                             "/tasks - Получить ваши задачи\n" +
                             "/todaytasks - Получить задачи на сегодня\n" +
                             "/menu - Увидеть доступные действия\n" +
                             "/help - Помощь";

        await botClient.SendTextMessageAsync(chatId, welcomeMessage, cancellationToken: cancellationToken);
        await SendMenu(chatId, cancellationToken);
    }


    //private static async Task HandleTasksCommand(long chatId, CancellationToken cancellationToken)
    //{
    //    var telegramChatId = chatId.ToString();
    //    var tasks = await GetTasksByTelegramChatId(telegramChatId);

    //    if (tasks != null && tasks.Any())
    //    {
    //        var responseMessage = string.Join("\n\n", tasks.Select(t => $"Название задачи: {t.Name}\nОписание: {t.Description}\nСтатус Задачи:{t.Status}"));

    //        await botClient.SendTextMessageAsync(chatId, responseMessage, cancellationToken: cancellationToken);
    //    }
    //    else
    //    {
    //        await botClient.SendTextMessageAsync(chatId, "Нет задач для вашего аккаунта.", cancellationToken: cancellationToken);
    //    }
    //}
    private static async Task HandleTasksCommand(long chatId, CancellationToken cancellationToken)
    {
        var telegramChatId = chatId.ToString();
        var response = await httpClient.GetAsync($"https://localhost:7220/api/ProjectTask/GetTasksByTelegramChatIdWithProjects?telegramChatId={telegramChatId}");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var tasks = JsonSerializer.Deserialize<List<ProjectTask>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (tasks != null && tasks.Any())
            {
                var responseMessage = string.Join("\n\n", tasks.Select(t => $"Проект: {t.Project.Name}\nНазвание задачи: {t.Name}\nОписание: {t.Description}\nСтатус Задачи: {t.Status}"));

                await botClient.SendTextMessageAsync(chatId, responseMessage, cancellationToken: cancellationToken);
            }
            else
            {
                await botClient.SendTextMessageAsync(chatId, "Нет задач для вашего аккаунта.", cancellationToken: cancellationToken);
            }
        }
        else
        {
            await botClient.SendTextMessageAsync(chatId, "Ошибка при получении задач.", cancellationToken: cancellationToken);
        }
    }


    private static async Task HandleTodayTasksCommand(long chatId, CancellationToken cancellationToken)
    {
        var telegramChatId = chatId.ToString();
        var response = await httpClient.GetAsync($"https://localhost:7220/api/ProjectTask/GetTodayTasksByTelegramChatId?telegramChatId={telegramChatId}");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var tasks = JsonSerializer.Deserialize<List<ProjectTask>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (tasks != null && tasks.Any())
            {
                var responseMessage = string.Join("\n\n", tasks.Select(t => $"Проект: {t.Project.Name}\nНазвание задачи: {t.Name}\nОписание: {t.Description}\nСтатус Задачи: {t.Status}\nДата Завершения: {t.CompletedAt?.ToString("yyyy-MM-dd")}"));

                await botClient.SendTextMessageAsync(chatId, responseMessage, cancellationToken: cancellationToken);
            }
            else
            {
                await botClient.SendTextMessageAsync(chatId, "На сегодня нет задач.", cancellationToken: cancellationToken);
            }
        }
        else
        {
            await botClient.SendTextMessageAsync(chatId, "Ошибка при получении задач.", cancellationToken: cancellationToken);
        }
    }
    private static async Task SendMenu(long chatId, CancellationToken cancellationToken)
    {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("View Tasks", "/tasks"),
                InlineKeyboardButton.WithCallbackData("Today's Tasks", "/todaytasks")
            },
        });

        await botClient.SendTextMessageAsync(chatId, "Выберите действие:", replyMarkup: inlineKeyboard, cancellationToken: cancellationToken);
    }

    private static async Task SendHelpMessage(long chatId, CancellationToken cancellationToken)
    {
        var helpMessage = "Доступные команды:\n" +
                          "/start - Стартовое сообщение\n" +
                          "/tasks - Получить ваши задачи\n" +
                          "/menu - Увидеть доступные действия\n" +
                          "/help - Помощь";

        await botClient.SendTextMessageAsync(chatId, helpMessage, cancellationToken: cancellationToken);
    }

    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(exception.Message);
        return Task.CompletedTask;
    }

    private static async Task<List<ProjectTask>> GetTasksByTelegramChatId(string telegramChatId)
    {
        var tasks = await httpClient.GetFromJsonAsync<List<ProjectTask>>($"https://localhost:7220/api/ProjectTask/GetTasksByTelegramChatId?telegramChatId={telegramChatId}");
        return tasks ?? new List<ProjectTask>();
    }
}
