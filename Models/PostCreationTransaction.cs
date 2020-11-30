using Bot.Interfaces;

namespace Bot.Models
{
    internal sealed class PostCreationTransaction : TransactionBase
    {
        public PostCreationTransaction(int recepientId) : base(recepientId)
        {
        }

        public string PostText { get; set; }

        public IPostCreatingState PostCreatingState { get; set; }
    }
}
