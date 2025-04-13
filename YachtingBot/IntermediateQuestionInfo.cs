namespace YachtingBot;

public class IntermediateQuestionInfo
{
    public required string? Image { get; init; }
    public required string Question { get; init; }
    public required string Text { get; init; }
    public required string[] RightAnswers { get; init; }
    public required IReadOnlyList<string> Variants { get; init; }
    public required int? AnswerThreshold { get; init; }
    public required bool NoKeyboard { get; init; }
    public required bool FixedAnswerOrder { get; init; }
}