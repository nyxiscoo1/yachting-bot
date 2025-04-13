using FluentAssertions;

namespace YachtingBot.Tests
{
    public class QuestingManagerTests
    {
        [Test]
        public void Should_handle_sorting_question()
        {
            const long userId = 123;

            var source = new MultiAnswerQuestion
            {
                Text = "Расставьте возможные ЧП по степени вероятности наступления на борту яхты. Первым - наиболее вероятное событие.",
                Variants = ["Пожар", "МОВ (человек за бортом)", "Травма членов экипажа", "Затопление", "Критические поломки"],
                RightAnswers = [2, 1, 0, 3, 4],
                FixedAnswerOrder = true
            }.ToQuestions().ToArray();
            var sut =  new QuestingManager(source);
            sut.Reset(userId);
            var q = sut.GetCurrent(userId);

            q.CheckAnswer("Пожар").Should().Be(AnswerResults.Incorrect);
            q.CheckAnswer("Травма членов экипажа").Should().Be(AnswerResults.NeedMore);
            q.CheckAnswer("Травма членов экипажа").Should().Be(AnswerResults.Incorrect);
            q.CheckAnswer("Пожар").Should().Be(AnswerResults.Incorrect);
            q.CheckAnswer("МОВ (человек за бортом)").Should().Be(AnswerResults.NeedMore);
            q.CheckAnswer("Травма членов экипажа").Should().Be(AnswerResults.Incorrect);
            q.CheckAnswer("Пожар").Should().Be(AnswerResults.NeedMore);
            q.CheckAnswer("Пожар").Should().Be(AnswerResults.Incorrect);
            q.CheckAnswer("Затопление").Should().Be(AnswerResults.NeedMore);
            q.CheckAnswer("Критические поломки").Should().Be(AnswerResults.Correct);
        }
    }
}
