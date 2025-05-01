using System.Text;
using FluentAssertions;

namespace YachtingBot.Tests;

public class QuestionSourceTests
{
    [Test]
    public void Answers_should_no_contain_incorrect_symbols()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var encoding = Encoding.GetEncoding(1251);

        var hasErrors = false;

        var source = new QuestionSource();
        foreach (var q in source.LoadQuestions())
        {
            foreach (var answer in q.Variants)
            {
                var convertedAnswer = encoding.GetString(encoding.GetBytes(answer));
                var hasQuestions = convertedAnswer.Contains("??");
                var hasFEFF = answer.Contains('\uFEFF');

                hasErrors |= hasQuestions;
                hasErrors |= hasFEFF;

                if (hasQuestions || hasFEFF)
                {
                    Console.WriteLine(q.Question);
                    Console.WriteLine("- " + convertedAnswer);
                }
            }
        }

        hasErrors.Should().Be(false);
    }
}