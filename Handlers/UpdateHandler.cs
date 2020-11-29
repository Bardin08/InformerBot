using System.Threading.Tasks;
using Bot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.Handlers
{
    public class UpdateHandler : IUpdateHandler
    {
        private readonly ITelegramBotClient _botClient;

        public UpdateHandler(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task HandleUpdateAsync(Update update)
        {
            if (update.Message is Message m && m.Text != null)
            {
                await _botClient.SendTextMessageAsync(m.Chat.Id, "Msg received");
            }
        }
    }
}
