namespace YachtingBot;

public class MultiAnswerQuestion : ISerializedQuestion
{
    public string? Image { get; init; }
    public required string Text { get; init; }
    public required string[] Variants { get; init; }
    public required int[] RightAnswers { get; init; }
    public int? AnswerThreshold { get; init; }
    public bool NoKeyboard { get; init; }
    public bool FixedAnswerOrder { get; init; }

    public IEnumerable<IntermediateQuestionInfo> ToQuestions()
    {
        yield return new IntermediateQuestionInfo
        {
            Image = Image,
            Question = Text,
            Text = "",
            RightAnswers = RightAnswers.Select(a=> Variants[a].Trim()).ToArray(),
            Variants = Variants.Select(v => v.Trim()).ToArray(),
            AnswerThreshold = AnswerThreshold,
            NoKeyboard = NoKeyboard,
            FixedAnswerOrder = FixedAnswerOrder
        };
    }
}