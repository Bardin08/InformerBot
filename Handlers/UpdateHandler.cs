using System.Threading.Tasks;
using Bot.Interfaces;
using Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.Handlers
{
    public class UpdateHandler : IUpdateHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ICommandHandler _commandHandler;

        public UpdateHandler(ITelegramBotClient botClient, ICommandHandler commandHandler)
        {
            _botClient = botClient;
            _commandHandler = commandHandler;
        }

        public async Task HandleUpdateAsync(Update update)
        {
            if (update.Message is Message m && m.Text.Equals("/start"))
            {
                _commandHandler.HandleCommandAsync(new CommandTransaction(update.Message.From.Id, update.Message.Text));
            }
            else if (update.Message is Message msg && msg.Text != null)
            {
                await _botClient.SendTextMessageAsync(msg.Chat.Id, "Msg received");
            }
        }
    }
}
