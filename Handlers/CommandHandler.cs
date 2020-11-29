using Bot.Commands;
using Bot.Contexts;
using Bot.Interfaces;
using Telegram.Bot;

namespace Bot.Handlers
{
    internal sealed class CommandHandler : ICommandHandler
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ITelegramBotClient _botClient;

        public CommandHandler(ApplicationDbContext dbContext, ITelegramBotClient botClient)
        {
            _dbContext = dbContext;
            _botClient = botClient;
        }

        public async void HandleCommandAsync(object transaction)
        {
            await new StartCommand().ExecuteAsync(transaction, _dbContext, _botClient);
        }
    }
}
