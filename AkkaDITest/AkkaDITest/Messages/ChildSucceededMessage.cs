namespace AkkaDITest.Messages
{
    public class ChildSucceededMessage
    {
        public ChildSucceededMessage(string fromWho)
        {
            FromWho = fromWho;
        }

        public string FromWho { get; }
    }
}
