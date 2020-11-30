using Bot.Contexts;
using Bot.Dictionaries;
using Bot.Interfaces;
using Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.Behaviour.RegistrationStates
{
    internal class AskGroup : IRegistrationState
    {
        async void IRegistrationState.Update(Message message, RegistrationTransaction transaction, ApplicationDbContext dbContext, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(transaction.RecepientId, BotDictionary.AskGroup);

            transaction.RegistationState = new GroupReceived();
        }
    }
}