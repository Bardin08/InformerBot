using Bot.Contexts;
using Bot.Dictionaries;
using Bot.Interfaces;
using Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.Behaviour.RegistrationStates
{
    internal sealed class RegistrationTransactionCreate : IRegistrationState
    {
        async void IRegistrationState.Update(Message message, RegistrationTransaction transaction, ApplicationDbContext dbContext, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(transaction.RecepientId, BotDictionary.AskUserFullName);

            transaction.RegistationState = new FullNameReceived();
        }
    }
}
