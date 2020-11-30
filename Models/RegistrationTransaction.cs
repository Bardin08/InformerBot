using Bot.Interfaces;

namespace Bot.Models
{
    internal sealed class RegistrationTransaction : TransactionBase
    {
        public RegistrationTransaction(int recepientId) : base(recepientId)
        {
            TransactionType = "registration";
        }

        public IRegistrationState RegistationState { get; set; }
    }
}
