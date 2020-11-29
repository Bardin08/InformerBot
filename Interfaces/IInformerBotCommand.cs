using System.Threading.Tasks;
using Telegram.Bot;

namespace Bot.Interfaces
{
    public interface IInformerBotCommand
    {
        public string Name { get; }

        Task ExecuteAsync(object transaction, ITelegramBotClient botClient);
    }
}
