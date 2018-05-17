namespace AkkaDITest.Messages
{
    public class ChildFailedMessage
    {
        public ChildFailedMessage(string fromWho)
        {
            FromWho = fromWho;
        }

        public string FromWho { get; }
    }
}
