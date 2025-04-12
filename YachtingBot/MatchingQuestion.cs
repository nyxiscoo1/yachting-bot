namespace YachtingBot;

public class MatchingQuestion : ISerializedQuestion
{
    public string? Image { get; init; }
    public required string Text { get; init; }
    public required Dictionary<string, string> Variants { get; init; }

    public IEnumerable<IntermediateQuestionInfo> ToQuestions()
    {
        return Variants.Select(x => new IntermediateQuestionInfo
        {
            Image = Image,
            Question = Text,
            Text = x.Key,
            RightAnswers = [x.Value.Trim()],
            Variants = Variants.Select(v => v.Value.Trim()).ToArray(),
            AnswerThreshold = null,
            NoKeyboard = false
        });
    }
}