using System.Collections.Generic;

namespace Bot.Models
{
    internal class TransactionBase
    {
        public TransactionBase(int recepientId)
        {
            IsComplete = false;
            TransactionId = RecepientId = recepientId;
            MessageIds = new List<int>();
        }

        public bool IsComplete { get; set; }

        public int RecepientId { get; set; }

        public int TransactionId { get; set; }

        public string TransactionType { get; set; }

        public List<int> MessageIds { get; set; }
    }
}
