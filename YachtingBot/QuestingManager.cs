namespace YachtingBot;

public class QuestingManager
{
    private readonly Dictionary<long, UserQuestions> _questions = new();

    private IReadOnlyList<IntermediateQuestionInfo> _source;

    public QuestingManager(IReadOnlyList<IntermediateQuestionInfo> source)
    {
        _source = source;
    }

    private IReadOnlyList<QuestionInfo> LoadQuestions()
    {
        var result = new List<QuestionInfo>();
        var shuffled = _source.ToArray();
        Random.Shared.Shuffle(shuffled);

        foreach (var q in shuffled)
        {
            result.Add(new QuestionInfo
            {
                Index = result.Count + 1,
                Image = q.Image,
                Question = q.Question,
                Text = q.Text,
                RightAnswers = q.RightAnswers,
                Variants = q.Variants,
                AnswerThreshold = q.AnswerThreshold ?? q.RightAnswers.Length,
                NoKeyboard = q.NoKeyboard
            });
        }
        return result;
    }

    public void Reset(long userId)
    {
        _questions[userId] = new(LoadQuestions());
    }

    public QuestionInfo GetCurrent(long userId)
    {
        var q = GetQuestions(userId);
        return q.Questions[q.CurrentQuestion];
    }

    public bool MoveNext(long userId)
    {
        var q = GetQuestions(userId);
        
        if (q.NoMoreQuestions())
            return false;

        q.MoveNext();
        return true;
    }

    private UserQuestions GetQuestions(long userId)
    {
        if (_questions.TryGetValue(userId, out var q))
            return q;

        throw new TestNotStartedException();
    }
}