using System.Linq;
using System.Threading.Tasks;
using Bot.Contexts;
using Bot.Interfaces;
using Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.Behaviour.PostCreatingStates
{
    internal class PostSent : IPostCreatingState
    {
        public async Task Update(Message message, PostCreationTransaction transaction, ApplicationDbContext dbContext, ITelegramBotClient botClient)
        {
            var ids = dbContext.Users.Select(u => u.Id).Where(u => true).ToList();

            foreach (var id in ids)
            {
                await botClient.SendTextMessageAsync(id, transaction.PostText);
            }

        }
    }
}