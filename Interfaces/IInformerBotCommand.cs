using System.Threading.Tasks;
using Bot.Contexts;
using Telegram.Bot;

namespace Bot.Interfaces
{
    public interface IInformerBotCommand
    {
        public string Name { get; }

        Task ExecuteAsync(object transaction, ApplicationDbContext db, ITelegramBotClient botClient);
    }
}
