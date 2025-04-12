namespace YachtingBot;

public class SimpleQuestion : ISerializedQuestion
{
    public string? Image { get; init; }
    public required string Text { get; init; }
    public required string[] Variants { get; init; }
    public required int RightAnswer { get; init; }

    public IEnumerable<IntermediateQuestionInfo> ToQuestions()
    {
        yield return new IntermediateQuestionInfo
        {
            Image = Image,
            Question = Text,
            Text = "",
            RightAnswers = [Variants[RightAnswer].Trim()],
            Variants = Variants.Select(v => v.Trim()).ToArray(),
            AnswerThreshold = null,
            NoKeyboard = false
        };
    }
}