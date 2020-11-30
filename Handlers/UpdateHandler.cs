using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bot.Commands;
using Bot.Contexts;
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
        private readonly ApplicationDbContext _dbContext;

        private readonly List<object> _transactions;

        public UpdateHandler(ITelegramBotClient botClient, ICommandHandler commandHandler, ApplicationDbContext dbContext)
        {
            _botClient = botClient;
            _commandHandler = commandHandler;
            _dbContext = dbContext;

            _transactions = new List<object>();

            RegistrationCommand.TransactionInitiated += RegistrationInitialized;
        }

        public async Task HandleUpdateAsync(Update update)
        {
            if (update.Message is Message message)
            {
                await RemoveCompleteTransactionsForUser(update.Message.From.Id);

                await ProcessMessage(message);
            }
            else if (update.CallbackQuery is CallbackQuery query)
            {
                await RemoveCompleteTransactionsForUser(update.CallbackQuery.From.Id);

                await ProcessCallbackQuery(query);
            }
        }

        private async Task ProcessCallbackQuery(CallbackQuery query)
        {
            var message = query.Message;
            message.From.Id = query.From.Id;
            message.Text = query.Data;

            await ProcessMessage(message);
        }

        private async Task ProcessMessage(Message message)
        {
            if (message.Text.StartsWith("/"))
            {
                _transactions?.Remove(_transactions.Find(t => (t as TransactionBase)?.TransactionId == message.From.Id));

                var command = message.Text.Contains(' ') ? message.Text.Substring(0, message.Text.IndexOf(' ')) : message.Text;

                var userTransaction = new CommandTransaction(message.From.Id, command);

                _transactions.Add(userTransaction);

                await _commandHandler.HandleCommandAsync(userTransaction);
            }
            else if (message.Text != null)
            {
                var userTransaction = _transactions.Find(x => (x as TransactionBase)?.TransactionId == message.From.Id);

                if ((userTransaction as TransactionBase)?.TransactionType == "registration")
                {
                    (userTransaction as RegistrationTransaction)?.RegistationState.Update(message, userTransaction as RegistrationTransaction, _dbContext, _botClient);
                }
            }
        }

        private void RegistrationInitialized(RegistrationTransaction transaction)
        {
            _transactions.Add(transaction);

            transaction?.RegistationState.Update(new Message(), transaction, _dbContext, _botClient);
        }

        private async Task RemoveCompleteTransactionsForUser(int userId)
        {
            foreach (var transaction in _transactions.Where(x => (x as TransactionBase)?.IsComplete == true
                                                                && (x as TransactionBase)?.RecepientId == userId).ToList())
            {
                foreach (var id in (transaction as TransactionBase)?.MessageIds)
                {
                    await _botClient.DeleteMessageAsync((transaction as TransactionBase)?.RecepientId, id);
                }

                _transactions.Remove(transaction);
            }
        }
    }
}
