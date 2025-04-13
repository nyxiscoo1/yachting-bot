using FluentAssertions;

namespace YachtingBot.Tests;

public class QuestionInfoTests
{
    [Test]
    public void Should_check_simple_answer()
    {
        var q = new QuestionInfo
        {
            Index = 1,
            Image = null,
            Question = "q?",
            Text = "do",
            RightAnswers = ["123"],
            Variants = ["123", "234"],
            AnswerThreshold = 1,
            NoKeyboard = false,
            FixedAnswerOrder = false
        };

        q.CheckAnswer("234").Should().Be(AnswerResults.Incorrect);
        q.CheckAnswer("123").Should().Be(AnswerResults.Correct);
    }

    [Test]
    public void Should_check_multi_answer()
    {
        var q = new QuestionInfo
        {
            Index = 1,
            Image = null,
            Question = "q?",
            Text = "do",
            RightAnswers = ["123", "234"],
            Variants = ["123", "234", "345"],
            AnswerThreshold = 2,
            NoKeyboard = false,
            FixedAnswerOrder = false
        };

        q.CheckAnswer("345").Should().Be(AnswerResults.Incorrect);
        q.CheckAnswer("234").Should().Be(AnswerResults.NeedMore);
        q.CheckAnswer("345").Should().Be(AnswerResults.Incorrect);
        q.CheckAnswer("234").Should().Be(AnswerResults.NeedMore);
        q.CheckAnswer("123").Should().Be(AnswerResults.Correct);
    }

    [Test]
    public void Should_check_multi_answer_with_threshold()
    {
        var q = new QuestionInfo
        {
            Index = 1,
            Image = null,
            Question = "q?",
            Text = "do",
            RightAnswers = ["123", "234", "456"],
            Variants = ["123", "234", "345", "456", "789"],
            AnswerThreshold = 2,
            NoKeyboard = false,
            FixedAnswerOrder = false
        };

        q.CheckAnswer("345").Should().Be(AnswerResults.Incorrect);
        q.CheckAnswer("234").Should().Be(AnswerResults.NeedMore);
        q.CheckAnswer("345").Should().Be(AnswerResults.Incorrect);
        q.CheckAnswer("234").Should().Be(AnswerResults.NeedMore);
        q.CheckAnswer("456").Should().Be(AnswerResults.Correct);
    }

    [Test]
    public void Should_check_sort_answer()
    {
        var q = new QuestionInfo
        {
            Index = 1,
            Image = null,
            Question = "q?",
            Text = "do",
            RightAnswers = ["345", "234", "123"],
            Variants = ["123", "234", "345"],
            AnswerThreshold = 3,
            NoKeyboard = false,
            FixedAnswerOrder = true
        };

        q.CheckAnswer("234").Should().Be(AnswerResults.Incorrect);
        q.CheckAnswer("345").Should().Be(AnswerResults.NeedMore);
        q.CheckAnswer("345").Should().Be(AnswerResults.Incorrect);
        q.CheckAnswer("123").Should().Be(AnswerResults.Incorrect);
        q.CheckAnswer("234").Should().Be(AnswerResults.NeedMore);
        q.CheckAnswer("345").Should().Be(AnswerResults.Incorrect);
        q.CheckAnswer("123").Should().Be(AnswerResults.Correct);
    }
}