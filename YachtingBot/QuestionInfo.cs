namespace YachtingBot;

public class QuestionInfo
{
    public required int Index { get; init; }
    public required string? Image { get; init; }
    public required string Question { get; init; }
    public required string Text { get; init; }
    public required string[] RightAnswers { get; init; }
    public required IReadOnlyList<string> Variants { get; init; }
    public required int AnswerThreshold { get; init; }
    public required bool NoKeyboard { get; init; }
    public required bool FixedAnswerOrder { get; init; }

    private HashSet<string> _answers = new();

    public AnswerResults CheckAnswer(string text)
    {
        if (IsRightAnswer(text))
        {
            _answers.Add(text);
            if (_answers.Count >= AnswerThreshold)
                return AnswerResults.Correct;

            return AnswerResults.NeedMore;
        }
        else
        {
            return AnswerResults.Incorrect;
        }
    }

    private bool IsRightAnswer(string text)
    {
        if (FixedAnswerOrder)
        {
            return RightAnswers[_answers.Count].ToLowerInvariant() == text.ToLowerInvariant();
        }

        return RightAnswers.Select(x => x.ToLowerInvariant()).Contains(text.ToLowerInvariant());
    }
}