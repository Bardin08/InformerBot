using System.Threading.Tasks;
using Bot.Contexts;
using Bot.Interfaces;
using Bot.Models;
using Telegram.Bot;

namespace Bot.Commands
{
    internal sealed class CreatePostCommand : IInformerBotCommand
    {
        public string Name => "/createpost";

        internal delegate void PostCreatingEvent(PostCreationTransaction transaction);
        internal static event PostCreatingEvent TransactionInitiated;

        public Task ExecuteAsync(object transaction, ApplicationDbContext db, ITelegramBotClient botClient)
        {
            (transaction as TransactionBase).IsComplete = true;
            TransactionInitiated?.Invoke(new PostCreationTransaction((transaction as TransactionBase).RecepientId)
            {
                TransactionType = "postcreating",
                PostCreatingState = new Behaviour.PostCreatingStates.PostCreatingBegin()
            });

            return Task.CompletedTask;
        }
    }
}
