using Bot.Interfaces;
using Telegram.Bot;

namespace Bot
{
    public class InformerBotClient : IInformerBotClient
    {
        public ITelegramBotClient BotClient { get; set; }

        public InformerBotClient(ITelegramBotClient botClient)
        {
            BotClient = botClient;
        }
    }
}
