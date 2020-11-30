using System.Threading.Tasks;
using Bot.Behaviour.RegistrationStates;
using Bot.Contexts;
using Bot.Interfaces;
using Bot.Models;
using Telegram.Bot;

namespace Bot.Commands
{
    public sealed class RegistrationCommand : IInformerBotCommand
    {
        internal delegate void UserRegistrationEvent(RegistrationTransaction transaction);
        internal static event UserRegistrationEvent TransactionInitiated;

        public string Name => "/register";

        public Task ExecuteAsync(object transaction, ApplicationDbContext db, ITelegramBotClient botClient)
        {
            (transaction as TransactionBase).IsComplete = true;
            TransactionInitiated?.Invoke(new RegistrationTransaction((transaction as CommandTransaction).RecepientId) { RegistationState = new RegistrationTransactionCreate() });

            return Task.CompletedTask;
        }
    }
}
