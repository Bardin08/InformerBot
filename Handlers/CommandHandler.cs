using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Bot.Commands;
using Bot.Contexts;
using Bot.Dictionaries;
using Bot.Interfaces;
using Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Bot.Handlers
{
    internal sealed class CommandHandler : ICommandHandler
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ITelegramBotClient _botClient;

        private List<IInformerBotCommand> _commands;

        public CommandHandler(ApplicationDbContext dbContext, ITelegramBotClient botClient)
        {
            _dbContext = dbContext;
            _botClient = botClient;
        }

        public async Task HandleCommandAsync(object transaction)
        {
            LoadCommands();

            var rCommand = _commands?.FirstOrDefault(x => x.Name == (transaction as CommandTransaction)?.Command);

            if (rCommand != null)
            {
                await rCommand.ExecuteAsync(transaction, _dbContext, _botClient);
            }
            else
            {
                var message = string.Format(BotDictionary.CommandNofFound, (transaction as CommandTransaction)?.Command);
                await _botClient.SendTextMessageAsync((transaction as TransactionBase)?.RecepientId, message, ParseMode.Markdown);
            }
        }

        private void LoadCommands()
        {
            _commands = new List<IInformerBotCommand>();

            var commands = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.GetInterface("IInformerBotCommand") != null);

            foreach (var command in commands)
            {
                _commands.Add(Activator.CreateInstance(command) as IInformerBotCommand);
            }
        }
    }
}
