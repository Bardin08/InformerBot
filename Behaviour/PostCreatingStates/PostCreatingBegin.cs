using System.Threading.Tasks;
using Bot.Contexts;
using Bot.Dictionaries;
using Bot.Interfaces;
using Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.Behaviour.PostCreatingStates
{
    internal sealed class PostCreatingBegin : IPostCreatingState
    {
        public async Task Update(Message message, PostCreationTransaction transaction, ApplicationDbContext dbContext, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(transaction.RecepientId, BotDictionary.PostCreatingSendPostText);

            transaction.PostCreatingState = new PostTextReceived();
        }
    }
}
