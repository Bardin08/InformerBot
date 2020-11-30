using Bot.Contexts;
using Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.Interfaces
{
    public interface IRegistrationState
    {
        internal void Update(Message message, RegistrationTransaction transaction, ApplicationDbContext dbContext, ITelegramBotClient botClient);
    }
}
