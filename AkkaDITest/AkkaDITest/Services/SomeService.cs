namespace AkkaDITest.Services
{
    public interface ISomeService
    {
        string ReturnValue(string input);
    }

    public class SomeService : ISomeService
    {
        public string ReturnValue(string input)
        {
            return $"SomeService_{input}";
        }
    }
}
