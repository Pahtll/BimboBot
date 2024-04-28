using BimboTelegramBot.Secrets;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

public class Program
{
    private const string token = Token.BotToken;

    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message)
            return;

        var chatId = message.Chat.Id;

        if (message.Text != null)
        {
            switch (message.Text)
            {
                case "Стикер" or "Sticker":
                    await botClient.SendStickerAsync(
                                            chatId: chatId,
                                            sticker: InputFile.FromUri("https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp"),
                                            cancellationToken: cancellationToken);
                    return;

                case "Видео" or "Video":
                    await botClient.SendTextMessageAsync(
                                            chatId: chatId,
                                            text: "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                                            cancellationToken: cancellationToken);
                    return;

                case "/inline":
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new List<InlineKeyboardButton[]>
                        {
                            new InlineKeyboardButton[]
                            {
                                InlineKeyboardButton.WithUrl("Best site ever", "msdn.com"),
                                InlineKeyboardButton.WithCallbackData("Some Button", "button1")
                            },
                            new InlineKeyboardButton[]
                            {
                                InlineKeyboardButton.WithCallbackData("Another Button", "button2"),
                                InlineKeyboardButton.WithCallbackData("Another another Button", "button3")
                            }
                        });
                    await botClient.SendTextMessageAsync(
                                            chatId: chatId,
                                            text: "Inline keyboard",
                                            replyMarkup: inlineKeyboard,
                                            cancellationToken: cancellationToken);
                    return;

                case "/reply":
                    var replyKeyboard = new ReplyKeyboardMarkup(
                        new List<KeyboardButton[]>
                        {
                            new KeyboardButton[]
                            {
                                new KeyboardButton("Sup"),
                                new KeyboardButton("Bye")
                            },
                            new KeyboardButton[]
                            {
                                new KeyboardButton("Voice call ?")
                            },
                            new KeyboardButton[]
                            {
                                new KeyboardButton("Write message for neighbor :3")
                            }
                        })
                    {
                        ResizeKeyboard = true
                    };

                    await botClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: $"Reply Keyboard",
                                        replyMarkup: replyKeyboard,
                                        cancellationToken: cancellationToken);

                    return;
            }

            await Console.Out.WriteLineAsync($"Recived message: '{message.Text}' in '{chatId}'");

            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"You said:\n{message.Text}",
                cancellationToken: cancellationToken);
        }

        if (message.Photo != null)
        {
            await Console.Out.WriteLineAsync($"Recived message: '{message.Text}' in '{chatId}'");

            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Don't send photos to me",
                cancellationToken: cancellationToken);
        }

        if (message.Voice is not null || 
            message.Audio is not null)
        {
            await Console.Out.WriteLineAsync($"Recived message: '{message.Text}' in '{chatId}'");

            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Why r u send audio ?\nThat's f*ckin' text-bot don't send voice or audio.\n",
                cancellationToken: cancellationToken);
        }
    }

    private static async Task HandlePollingErrorAsync(
        ITelegramBotClient botClient, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);

        await Task.CompletedTask;
    }

    private static async Task Main(string[] args)
    {
        ITelegramBotClient bimboBotClient = new TelegramBotClient(token);

        using CancellationTokenSource cts = new();

        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        bimboBotClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token);

        var user = await bimboBotClient.GetMeAsync();
        await Console.Out.WriteLineAsync($"Started listening for @{user.Username}");

        Console.ReadLine();
    }
}