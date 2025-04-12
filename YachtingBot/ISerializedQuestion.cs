namespace YachtingBot;

public interface ISerializedQuestion
{
    IEnumerable<IntermediateQuestionInfo> ToQuestions();
}