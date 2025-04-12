namespace YachtingBot;

public class TestNotStartedException : Exception
{
    public TestNotStartedException()
    {
    }

    public TestNotStartedException(string message) : base(message)
    {
    }

    public TestNotStartedException(string message, Exception inner) : base(message, inner)
    {
    }
}