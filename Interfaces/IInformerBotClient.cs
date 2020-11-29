using Telegram.Bot;

namespace Bot.Interfaces
{
    public interface IInformerBotClient
    {
        public ITelegramBotClient BotClient { get; set; }
    }
}
