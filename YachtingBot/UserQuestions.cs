namespace YachtingBot;

public class UserQuestions
{
    public IReadOnlyList<QuestionInfo> Questions { get; }

    public int CurrentQuestion { get; private set; }

    public UserQuestions(IReadOnlyList<QuestionInfo> questions)
    {
        Questions = questions;
    }

    public bool NoMoreQuestions()
    {
        return CurrentQuestion == Questions.Count - 1;
    }

    public void MoveNext()
    {
        if (NoMoreQuestions())
            throw new InvalidOperationException("No more questions");

        CurrentQuestion++;
    }
}