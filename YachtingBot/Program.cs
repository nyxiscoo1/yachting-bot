using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using YachtingBot;

var tokenPath = Path.Combine(AppContext.BaseDirectory, "token.txt");
if (!File.Exists(tokenPath))
{
    Console.WriteLine($"Задайте токен в файле {tokenPath}");
    return 1;
}

var token = File.ReadAllText(tokenPath, Encoding.UTF8);

var source = new QuestionSource();
var data = source.LoadQuestions();
var manager = new QuestingManager(data);
var keyboardLayoutBuilder = new KeyboardLayoutBuilder();

var bot = new TelegramBotClient(token);

using var cts = new CancellationTokenSource();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool, so we use cancellation token
bot.StartReceiving(
    updateHandler: HandleUpdate,
    errorHandler: HandleError,
    cancellationToken: cts.Token
);

// Tell the user the bot is online
Console.WriteLine("Start listening for updates. Press enter to stop");
#if DEBUG
Console.ReadLine();
#else
cts.Token.WaitHandle.WaitOne();
#endif

// Send cancellation request to stop the bot
cts.Cancel();

return 0;


// Each time a user interacts with the bot, this method is called
async Task HandleUpdate(ITelegramBotClient _, Update update, CancellationToken cancellationToken)
{
    switch (update.Type)
    {
        // A message was received
        case UpdateType.Message:
            await HandleMessage(update.Message!);
            break;

        // A button was pressed
        case UpdateType.CallbackQuery:
            await HandleButton(update.CallbackQuery!);
            break;
    }
}

async Task HandleError(ITelegramBotClient _, Exception exception, CancellationToken cancellationToken)
{
    await Console.Error.WriteLineAsync(exception.Message);
}

async Task HandleMessage(Message msg)
{
    var user = msg.From;
    var text = msg.Text ?? string.Empty;

    if (user is null)
        return;

    // Print to console
    Console.WriteLine($"{user.FirstName} wrote {text}");

    if (text.StartsWith("/"))
    {
        await HandleCommand(user.Id, text);
    }
    else if (text.Length > 0)
    {
        await VerifyAnswer(user.Id, text);
    }
    else
    {
        //await SendCurrentQuestion(user.Id);
    }
}

async Task HandleCommand(long userId, string command)
{
    switch (command)
    {
        case "/start":
            await HandleStartCommand(userId);
            break;
        case "/skip":
            await HandleSkipCommand(userId);
            break;
    }

    await Task.CompletedTask;
}

async Task SendCurrentQuestion(long userId)
{
    try
    {
        var q = manager.GetCurrent(userId);
        if (q.Image != null)
        {
            await SendPhoto(userId, q.Image, $"<b>{q.Index}. " + q.Question + "</b>\n\n" + q.Text,
                KeyboardFromVariants(q));
        }
        else
        {
            if (!q.Text.StartsWith("/"))
            {
                await bot.SendMessage(userId, $"<b>{q.Index}. " + q.Question + "</b>\n\n" + q.Text, ParseMode.Html,
                    replyMarkup: KeyboardFromVariants(q));
            }
            else
            {
                await SendPhoto(userId, q.Text, $"<b>{q.Index}. " + q.Question + "</b>",
                    replyMarkup: KeyboardFromVariants(q));
            }
        }
    }
    catch (TestNotStartedException)
    {
        await HandleStartCommand(userId);
    }
}

async Task SendPhoto(long userId, string imagePath, string? caption = null, ReplyMarkup? replyMarkup = null)
{
    var fullPath = BuildImagePath(imagePath);
    using var stream = File.OpenRead(fullPath);
    var image = InputFile.FromStream(stream);
    await bot.SendPhoto(userId, image, caption: caption, ParseMode.Html, replyMarkup: replyMarkup);
}

ReplyMarkup? KeyboardFromVariants(QuestionInfo q)
{
    if (q.NoKeyboard)
        return new ReplyKeyboardRemove();

    var layout = keyboardLayoutBuilder.ForVariants(q.Variants);

    return new ReplyKeyboardMarkup(layout.Select(r =>
        r.Select(x => new KeyboardButton(x))))
    {
        ResizeKeyboard = false
    };
}

async Task VerifyAnswer(long userId, string text)
{
    try
    {
        if (text.ToLowerInvariant() == "совбез")
        {
            await SendPlanktonich(userId);
            return;
        }

        var q = manager.GetCurrent(userId);
        var answerResult = q.CheckAnswer(text);
        if (answerResult == AnswerResults.Incorrect)
        {
            await SendIncorrectAnswer(userId);
            return;
        }
        if (answerResult == AnswerResults.NeedMore)
        {
            await SendMultyAnswer(userId);
            return;
        }

        await SendCorrectAnswer(userId);

        await MoveToNextQuestion(userId);
    }
    catch (TestNotStartedException)
    {
        await HandleStartCommand(userId);
    }
}

async Task SendIncorrectAnswer(long userId)
{
    await bot.SendMessage(userId, "\u274c \u274c \u274c", ParseMode.Html);
}

async Task SendMultyAnswer(long userId)
{
    await bot.SendMessage(userId, "А еще?", ParseMode.Html);
}

Task HandleButton(CallbackQuery query)
{
    return Task.CompletedTask;
}

async Task SendCorrectAnswer(long userId)
{
    await bot.SendMessage(userId, "\ud83d\udc4dПравильно!", ParseMode.Html);
}

string BuildImagePath(string s)
{
    return Path.Combine(AppContext.BaseDirectory, "img", s.TrimStart('/'));
}

async Task HandleStartCommand(long userId)
{
    manager.Reset(userId);
    await SendGreetings(userId);
    await SendCurrentQuestion(userId);
}

async Task HandleSkipCommand(long userId)
{
    try
    {
        await MoveToNextQuestion(userId);
    }
    catch (TestNotStartedException)
    {
        await HandleStartCommand(userId);
    }
}

async Task SendGreetings(long userId)
{
    await bot.SendMessage(userId, "<b>Добро пожаловать в тест!</b>\n\nДавай проверим твои знания.", ParseMode.Html);
}

async Task MoveToNextQuestion(long l)
{

    if (manager.MoveNext(l))
    {
        await SendCurrentQuestion(l);
    }
    else
    {
        await SendTestingDone(l);
    }
}

async Task SendTestingDone(long userId)
{
    await bot.SendMessage(userId, "Поздравляю, вы прошли тест!", ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
}

async Task SendPlanktonich(long userId)
{
    var fullPath = BuildImagePath("/undefined_agadjxcaalce2us.webp");
    using var stream = File.OpenRead(fullPath);
    var image = InputFile.FromStream(stream);

    await bot.SendSticker(userId, image);
}

