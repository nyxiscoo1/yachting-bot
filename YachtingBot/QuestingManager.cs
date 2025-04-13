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
        var shuffledQuestions = _source.ToArray();
        Random.Shared.Shuffle(shuffledQuestions);

        foreach (var q in shuffledQuestions)
        {
            var shuffledVariants = q.Variants.ToArray();
            Random.Shared.Shuffle(shuffledVariants);

            result.Add(new QuestionInfo
            {
                Index = result.Count + 1,
                Image = q.Image,
                Question = q.Question,
                Text = q.Text,
                RightAnswers = q.RightAnswers,
                Variants = shuffledVariants,
                AnswerThreshold = q.AnswerThreshold ?? q.RightAnswers.Length,
                NoKeyboard = q.NoKeyboard,
                FixedAnswerOrder = q.FixedAnswerOrder
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