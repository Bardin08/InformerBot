using System.Threading.Tasks;
using Bot.Contexts;
using Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.Interfaces
{
    internal interface IPostCreatingState
    {
        Task Update(Message message, PostCreationTransaction transaction, ApplicationDbContext dbContext, ITelegramBotClient botClient);
    }
}
