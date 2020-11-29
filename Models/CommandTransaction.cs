namespace Bot.Models
{
    internal sealed class CommandTransaction : TransactionBase
    {
        public CommandTransaction(int recepientId) : base(recepientId)
        {
        }
    }
}
