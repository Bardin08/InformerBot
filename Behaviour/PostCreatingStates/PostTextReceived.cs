using System.Threading.Tasks;
using Bot.Contexts;
using Bot.Interfaces;
using Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.Behaviour.PostCreatingStates
{
    internal class PostTextReceived : IPostCreatingState
    {
        public async Task Update(Message message, PostCreationTransaction transaction, ApplicationDbContext dbContext, ITelegramBotClient botClient)
        {
            if (message.Text == null)
            {
                transaction.PostCreatingState = new PostCreatingBegin();
                transaction?.PostCreatingState.Update(new Message(), transaction, dbContext, botClient);
            }
            else
            {
                transaction.PostText = message.Text;
                transaction.PostCreatingState = new SendPost();
                await transaction.PostCreatingState.Update(new Message(), transaction, dbContext, botClient);
            }
        }
    }
}