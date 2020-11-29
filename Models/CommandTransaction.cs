namespace Bot.Models
{
    internal sealed class CommandTransaction : TransactionBase
    {
        public CommandTransaction(int recepientId, string command) : base(recepientId)
        {
            Command = command;
        }

        public string Command { get; set; }
    }
}
