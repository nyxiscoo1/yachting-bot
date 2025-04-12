using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using YachtingBot;

var source = new QuestionSource();
var data = source.LoadQuestions();
var manager = new QuestingManager(data);

var bot = new TelegramBotClient("<your_token>");

using var cts = new CancellationTokenSource();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool, so we use cancellation token
bot.StartReceiving(
    updateHandler: HandleUpdate,
    errorHandler: HandleError,
    cancellationToken: cts.Token
);

// Tell the user the bot is online
Console.WriteLine("Start listening for updates. Press enter to stop");
Console.ReadLine();

// Send cancellation request to stop the bot
cts.Cancel();


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

    var shuffled = q.Variants.ToArray();
    Random.Shared.Shuffle(shuffled);
    return new ReplyKeyboardMarkup(shuffled.Select(x =>
        new[] { new KeyboardButton(x) }))
    {
        ResizeKeyboard = false
    };
}

async Task VerifyAnswer(long userId, string text)
{
    try
    {
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

        if (manager.MoveNext(userId))
        {
            await SendCurrentQuestion(userId);
        }
        else
        {
            await SendTestingDone(userId);
        }
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

async Task SendTestingDone(long userId)
{
    await bot.SendMessage(userId, "Поздравляю, вы прошли тест!", ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
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

async Task SendGreetings(long userId)
{
    await bot.SendMessage(userId, "<b>Добро пожаловать в тест!</b>\n\nДавай проверим твои знания.", ParseMode.Html);
}
